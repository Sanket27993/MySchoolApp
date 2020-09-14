using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySchool
{
    public class LoginViewModel
    {
        public event EventHandler<OperationEventArgs> OpeartaionEvent;
        public UserOAuthenticationModel UserOAuthenticationData;

        private UserOAuthenticationDB _authenticationDB;
        private ValidationLogin _validationLogin;
        private StudentDB _studentDB;
        public LoginViewModel()
        {
            _validationLogin = new ValidationLogin();
            _authenticationDB = new UserOAuthenticationDB();
            _studentDB = new StudentDB();
            //SaveUserData();
            //StudentDummyData();
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        public void UserLogin(UserOAuthenticationModel userOAuthentication)
        {
            bool IsValid = _validationLogin.Validate(userOAuthentication);
            if (IsValid)
            {
                GetUserData(userOAuthentication);
            }
            else
            {
                EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, _validationLogin.Message,(int)EnumOperation.Get, (int)EnumResult.fail);
            }
        }

        /// <summary>
        /// Get user Data
        /// </summary>
        /// <param name="userData"></param>
        private void GetUserData(UserOAuthenticationModel userData)
        {
            try
            {
                var allusers = _authenticationDB.AllUserData;
                if (allusers != null && allusers.Count > 0)
                {
                    var user = _authenticationDB.AllUserData.FirstOrDefault(rc => rc.UserName == userData.UserName && rc.Password == userData.Password);
                    if (user != null)
                    {
                        UserOAuthenticationData = user;
                        EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "Success",(int)EnumOperation.Get, (int)EnumResult.success);
                    }
                    else
                    {
                        EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "Invalid User", (int)EnumOperation.Get, (int)EnumResult.fail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void SaveUserData()
        {
            List<UserOAuthenticationModel> users = new List<UserOAuthenticationModel>();
            UserOAuthenticationModel userOAuthenticationModel = new UserOAuthenticationModel();
            userOAuthenticationModel.IsAdmin = true;
            userOAuthenticationModel.UserName = "Sanket123";
            userOAuthenticationModel.Password = "1234";
            userOAuthenticationModel.UserID = 1;

            users.Add(userOAuthenticationModel);

            // userOAuthenticationModel = new UserOAuthenticationModel();
            //userOAuthenticationModel.IsAdmin = false;
            //userOAuthenticationModel.UserName = "Devang123";
            //userOAuthenticationModel.Password = "1234";
            //userOAuthenticationModel.UserID = 2;
            //users.Add(userOAuthenticationModel);

            foreach (var data in users)
            {
                _authenticationDB.SaveUserData(data);
            }
        }

        private void StudentDummyData()
        {
            List<StudentDetailsModel> studentDetailsModels = new List<StudentDetailsModel>();

            StudentDetailsModel studentDetails = new StudentDetailsModel
            {
                StudentID = 1,
                Name = "Divya Prajapati",
                Gender = "Female",
                StudentClass = "9th B",
                Birthdate = "02/02/1990",
                EmailID = "test@test.com",
                Address = "Prajapatiwadi Chhipvad Valsad",
                City = "valsad",
                Street = "chipwad",
                State = "Gujarat",
                PostalCode = "396001",
                IsAudited = false,
                Country = "India",
                ContactNo = "1234567890"
            };
            UserOAuthenticationModel userOAuthentication = new UserOAuthenticationModel
            {
                IsAdmin = true,
                StudentID = 1,
                UserName = "Divya123",
                Password = "1234",
                UserID = 1
            };
            studentDetails.userOAuthenticationModel = userOAuthentication;
            studentDetailsModels.Add(studentDetails);

            studentDetails = new StudentDetailsModel
            {
                StudentID = 2,
                Name = "Devang Prajapati",
                Gender = "Male",
                StudentClass = "10th B",
                Birthdate = "02/02/1990",
                EmailID = "test@test.com",
                Address = "Prajapatiwadi Chhipvad Valsad",
                City = "valsad",
                Street = "chipwad",
                State = "Gujarat",
                PostalCode = "396001",
                IsAudited = false,
                Country = "India",
                ContactNo = "1234567890"
            };
            userOAuthentication = new UserOAuthenticationModel
            {
                IsAdmin = false,
                StudentID = 2,
                UserName = "Devang123",
                Password = "1234",
                UserID = 2
            };
            studentDetails.userOAuthenticationModel = userOAuthentication;
            studentDetailsModels.Add(studentDetails);
            studentDetails = new StudentDetailsModel
            {
                StudentID = 3,
                Name = "Smit Prajapati",
                Gender = "Male",
                StudentClass = "10th B",
                Birthdate = "02/02/1990",
                EmailID = "test@test.com",
                Address = "Prajapatiwadi Chhipvad Valsad",
                City = "valsad",
                Street = "chipwad",
                State = "Gujarat",
                PostalCode = "396001",
                IsAudited = false,
                Country = "India",
                ContactNo = "1234567890"
            };
            userOAuthentication = new UserOAuthenticationModel
            {
                IsAdmin = false,
                StudentID = 2,
                UserName = "Smit123",
                Password = "1234",
                UserID = 2
            };
            studentDetails.userOAuthenticationModel = userOAuthentication;
            studentDetailsModels.Add(studentDetails);
            //userOAuthentication = new UserOAuthenticationModel
            //{
            //    IsAdmin = true,
            //    StudentID = 0,
            //    UserName = "Sanket123",
            //    Password = "1234",
            //    UserID = 4
            //};

            //studentDetails = new StudentDetailsModel();
            //studentDetails.userOAuthenticationModel = userOAuthentication;
            //studentDetailsModels.Add(studentDetails);

            foreach (var data in studentDetailsModels)
            {
                if (data != null)
                {
                    _studentDB.SaveStudentData(data);
                    _authenticationDB.SaveUserData(data.userOAuthenticationModel);
                }
            }
        }

    }
}
