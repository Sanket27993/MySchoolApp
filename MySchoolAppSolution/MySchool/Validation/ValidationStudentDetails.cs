using System;
using System.Collections.Generic;
using System.Text;

namespace MySchool
{
    public class ValidationStudentDetails
    {

        public event EventHandler<ValidationEventArgs> validationEvent;
        readonly ValidationEventArgs validationEventArg = null;
        public ValidationStudentDetails()
        {
            validationEventArg = new ValidationEventArgs();
        }
        public void StudentDetailsValidation(StudentDetailsModel studentDetails, bool IsAdmin,int Operation)
        {
            
            if (IsAdmin && string.IsNullOrEmpty(studentDetails.Name))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_STUDENT_NAME, (int)EnumResult.fail, Operation);
            }
            else if (IsAdmin && string.IsNullOrEmpty(studentDetails.Gender))
            {
                SetValidationEventArg(AppConstant.PLEASE_SELECT_GENDER, (int)EnumResult.fail, Operation);
            }
            else if (IsAdmin && string.IsNullOrEmpty(studentDetails.StudentClass))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_STUDENT_CLASS, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.Address))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_ADDRESS, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.Street))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_STREET, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.City))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_CITY, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.State))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_STATE, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.Country))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_COUNTRY, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.PostalCode))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_ZIPCODE, (int)EnumResult.fail, Operation);
            }
            else if (!string.IsNullOrEmpty(studentDetails.PostalCode) && studentDetails.PostalCode.Length != 6)
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_VALID_ZIP_CODE, (int)EnumResult.fail, Operation);
            }
            else if (!string.IsNullOrEmpty(studentDetails.PostalCode) && Convert.ToInt32(studentDetails.PostalCode) == 0)
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_VALID_ZIP_CODE, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.EmailID))
            {
                SetValidationEventArg(AppConstant.EMAIL_EMPTY_MESSAGE, (int)EnumResult.fail, Operation);

            }
            else if (!Validation.IsEmailValid(studentDetails.EmailID))
            {
                SetValidationEventArg(AppConstant.VALID_EMAIL_MESSAGE, (int)EnumResult.fail, Operation);
            }
            else if (string.IsNullOrEmpty(studentDetails.ContactNo))
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_CONTACTNO, (int)EnumResult.fail, Operation);
            }
            else if (!string.IsNullOrEmpty(studentDetails.ContactNo) && studentDetails.ContactNo.Length != 10)
            {
                SetValidationEventArg(AppConstant.PLEASE_ENTER_VALID_CONTACTNO, (int)EnumResult.fail, Operation);
            }
            else
            {
                SetValidationEventArg(String.Empty, (int)EnumResult.success, Operation);
            }

        }

        private void SetValidationEventArg(string message, int result, int opeartaion)
        {
            validationEventArg.Message = message;
            validationEventArg.Result = result;
            validationEventArg.Opeartaion = opeartaion;
            validationEvent.Invoke(this, validationEventArg);
        }
    }
}
