using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MySchool
{
    public class UserOAuthenticationDB
    {
        private DataLayer _dataLayer;

        public UserOAuthenticationDB()
        {
            CreateTable();
        }
        private void CreateTable()
        {
            _dataLayer = GNDataLayer.GetNew();
            _dataLayer.EventSendResponse += DataLayer_EventSendResponse;
            _dataLayer.CreateTabel<UserOAuthenticationModel>(new CreateTabel());
        }

        public UserOAuthenticationModel UserData
        {
            get
            {
                return FetchUserData();
            }
        }

        public List<UserOAuthenticationModel> AllUserData
        {
            get
            {
                return FetchAllUserData();
            }
        }

        /// <summary>
        /// Account data fetch from DB 
        /// </summary>
        /// <returns></returns>
        private UserOAuthenticationModel FetchUserData()
        {
            string query = GetSelectQueryString.GetQuery(ConstUserOAuthentication.TableName);
            var userOAuthentication = _dataLayer.FetchSingle<UserOAuthenticationModel>(new Fetch(query));

            return userOAuthentication;
        }


        /// <summary>
        /// Account data fetch from DB 
        /// </summary>
        /// <returns></returns>
        private List<UserOAuthenticationModel> FetchAllUserData()
        {
            string query = GetSelectQueryString.GetQuery(ConstUserOAuthentication.TableName);
            var userOAuthentications = _dataLayer.Fetch<UserOAuthenticationModel>(new Fetch(query));

            return userOAuthentications;
        }
        private void DataLayer_EventSendResponse(object sender, ResponseCodeEventArgs e)
        {
            //
        }

        /// <summary>
        /// All user data clear
        /// </summary>
        private void ClearTable()
        {
            _dataLayer.ClearTable(new ClearTabel(), ConstUserOAuthentication.TableName);
        }

        /// <summary>
        /// Save user data
        /// </summary>
        /// <param name="userOAuthentication"></param>
        public void SaveUserData(UserOAuthenticationModel userOAuthentication)
        {
            try
            {
              
                if (userOAuthentication != null)
                {
                    _dataLayer.SaveWithoutReplace(new Save<UserOAuthenticationModel>(userOAuthentication));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Delete User Data
        /// </summary>
        /// <param name="studentDetailsModel"></param>
        public void DeleteStudentData(UserOAuthenticationModel userOAuthentication)
        {
            try
            {
                if (userOAuthentication != null)
                {
                    _dataLayer.Delete(new Delete<UserOAuthenticationModel>(userOAuthentication));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
