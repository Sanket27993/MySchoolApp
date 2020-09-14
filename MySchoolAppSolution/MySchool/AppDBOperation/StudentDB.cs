using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MySchool
{
   
    public class StudentDB
    {
        private DataLayer _dataLayer;

        public StudentDB()
        {
            CreateTable();
        }
        private void CreateTable()
        {
            _dataLayer = GNDataLayer.GetNew();
            _dataLayer.EventSendResponse += DataLayer_EventSendResponse;
            _dataLayer.CreateTabel<StudentDetailsModel>(new CreateTabel());
        }

        public StudentDetailsModel StudentDetailData
        {
            get
            {
                return FetchStudentDetailsData();
            }
        }

        public List<StudentDetailsModel> AllStudentData
        {
            get
            {
                return FetchAllStudentDetails();
            }
        }

        /// <summary>
        /// Student data fetch from DB 
        /// </summary>
        /// <returns></returns>
        private StudentDetailsModel FetchStudentDetailsData()
        {
            string query = GetSelectQueryString.GetQuery(ConstStudentDetails.TableName);
            var studentDetail = _dataLayer.FetchSingle<StudentDetailsModel>(new Fetch(query));

            return studentDetail;
        }

        /// <summary>
        /// Fetcha All Student Data
        /// </summary>
        /// <returns></returns>
        private List<StudentDetailsModel> FetchAllStudentDetails()
        {
            string query = GetSelectQueryString.GetQuery(ConstStudentDetails.TableName);
            var studentDetails = _dataLayer.Fetch<StudentDetailsModel>(new Fetch(query));

            return studentDetails;
        }

        private void DataLayer_EventSendResponse(object sender, ResponseCodeEventArgs e)
        {
            //
        }

        /// <summary>
        /// Clear All Data 
        /// </summary>
        private void ClearTable()
        {
            _dataLayer.ClearTable(new ClearTabel(), ConstStudentDetails.TableName);
        }

        /// <summary>
        /// Save Student Data
        /// </summary>
        /// <param name="studentDetailsModel"></param>
        public void SaveStudentData(StudentDetailsModel studentDetailsModel)
        {
            try
            {
                if (studentDetailsModel != null)
                {
                    _dataLayer.SaveWithoutReplace(new Save<StudentDetailsModel>(studentDetailsModel));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Update Student Data
        /// </summary>
        /// <param name="studentDetailsModel"></param>
        public void UpdateStudentData(StudentDetailsModel studentDetailsModel)
        {
            try
            {
                if (studentDetailsModel != null)
                {
                    _dataLayer.Update(new Update<StudentDetailsModel>(studentDetailsModel));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }


        /// <summary>
        /// Delete student Data
        /// </summary>
        /// <param name="studentDetailsModel"></param>
        public void DeleteStudentData(StudentDetailsModel studentDetailsModel)
        {
            try
            {
                if (studentDetailsModel != null)
                {
                    _dataLayer.Delete(new Delete<StudentDetailsModel>(studentDetailsModel));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
