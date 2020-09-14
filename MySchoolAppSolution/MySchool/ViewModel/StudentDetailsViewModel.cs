using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySchool
{
    public class StudentDetailsViewModel
    {
        public event EventHandler<OperationEventArgs> OpeartaionEvent;
        public StudentDetailsModel studentDetails;
        public List<StudentDetailsModel> AllStudentList;
        public List<UserOAuthenticationModel> UserList;

        private StudentDB _studentDB;
        private UserOAuthenticationDB _authenticationDB;
        private ValidationStudentDetails _validationStudentDetails;
        private StudentDetailsModel _saveAndUpdatestudentDetails;

        public StudentDetailsViewModel()
        {
            _validationStudentDetails = new ValidationStudentDetails();
            _validationStudentDetails.validationEvent += ValidationStudentDetails_validationEvent;
            _studentDB = new StudentDB();
            _authenticationDB = new UserOAuthenticationDB();
            AllStudentList = _studentDB.AllStudentData;
            UserList = _authenticationDB.AllUserData;
        }

        /// <summary>
        /// Get Student details
        /// </summary>
        /// <param name="StudentID"></param>
        public void GetStudentData(int StudentID)
        {
            try
            {
                var allStudents = _studentDB.AllStudentData;
                if (allStudents != null && allStudents.Count > 0)
                {
                    var studentData = allStudents.Where(rc => rc.StudentID == StudentID).FirstOrDefault();
                    if (studentData != null)
                    {
                        studentDetails = studentData;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        public void UpdateAudited(StudentDetailsModel updatestudentDetails)
        {
            try
            {
                _studentDB.UpdateStudentData(updatestudentDetails);
                EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "Audit has been successfully!!", (int)EnumOperation.Update, (int)EnumResult.success);

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }

        public void UploadDocument(StudentDetailsModel updatestudentDetails)
        {
            try
            {
                _studentDB.UpdateStudentData(updatestudentDetails);
                EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "Document uploded successfully!", (int)EnumOperation.Update, (int)EnumResult.success);

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }

        public void UpdateStudentData(StudentDetailsModel updatestudentDetails,bool IsAdmin)
        {
            try
            {
                _saveAndUpdatestudentDetails = updatestudentDetails;
                 _validationStudentDetails.StudentDetailsValidation(_saveAndUpdatestudentDetails, IsAdmin,(int)EnumOperation.Update);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SaveStudentData(StudentDetailsModel saveStudentDetails, bool IsAdmin)
        {
            try
            {
                
                _saveAndUpdatestudentDetails = saveStudentDetails;
                _validationStudentDetails.StudentDetailsValidation(_saveAndUpdatestudentDetails, IsAdmin, (int)EnumOperation.Insert);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsStudentExist(StudentDetailsModel saveStudentDetails)
        {
            var stuData = AllStudentList.Where(rc => rc.Name.ToLower() == saveStudentDetails.Name.ToLower()).FirstOrDefault();
            if(stuData!=null)
            {
                return true;
            }

            return false;
        }
        public void DeleteStudentData(StudentDetailsModel deleteStudentDetails)
        {
            try
            {
                _studentDB.DeleteStudentData(deleteStudentDetails);
                var userdata = UserList.Where(rc => rc.StudentID == deleteStudentDetails.StudentID).FirstOrDefault();
                if(userdata!=null)
                {
                    _authenticationDB.DeleteStudentData(userdata);
                }
                EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "Student Details has been Deleted successfully !", (int)EnumOperation.Delete, (int)EnumResult.success);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ValidationStudentDetails_validationEvent(object sender, ValidationEventArgs e)
        {
            if (e.Result == (int)EnumResult.success)
            {
                if(e.Opeartaion==(int)EnumOperation.Update)
                {
                    _studentDB.UpdateStudentData(_saveAndUpdatestudentDetails);
                    EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "Student Details has been updated successfully!", (int)EnumOperation.Update, (int)EnumResult.success);
                }
                else if (e.Opeartaion == (int)EnumOperation.Insert)
                {
                    bool isExist = IsStudentExist(_saveAndUpdatestudentDetails);
                    if(!isExist)
                    {
                        SavetStudentData();
                        EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "Student Details has been save successfully!", (int)EnumOperation.Insert, (int)EnumResult.success);
                    }
                    else
                    {
                        EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, "This Student already exist.", (int)EnumOperation.Insert, (int)EnumResult.success);
                    }
                }
            }
            else
            {
                EventInvoke.Instance.SetViewModelEventArg(OpeartaionEvent, e.Message, (int)EnumOperation.Update, (int)EnumResult.fail);
            }

        }

        private void SavetStudentData()
        {
            if(_saveAndUpdatestudentDetails!=null)
            {
                _studentDB.SaveStudentData(_saveAndUpdatestudentDetails);
                UserOAuthenticationModel userOAuthentication = new UserOAuthenticationModel
                {
                    IsAdmin = false,
                    StudentID = _saveAndUpdatestudentDetails.StudentID,
                    UserName = "Student" + _saveAndUpdatestudentDetails.StudentID,
                    Password = "1234"
                };
                _authenticationDB.SaveUserData(userOAuthentication);
            }
        }
    }
}
