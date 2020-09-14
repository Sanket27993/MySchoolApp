using System;
using System.Collections.Generic;
using System.Text;

namespace MySchool
{
    public class AppConstant
    {

        public static readonly string EMAIL_VALIDATION_REGEX = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public static readonly string DATEFORMAT_MMDDYYYY = "MM'/'dd'/'yyyy";
        public static readonly string USERNAME_EMPTY_MESSAGE = "Username can not be empty.";
        public static readonly string PASSWORD_EMPTY_MESSAGE = "Password can not be empty.";
        public static readonly string VALID_EMAIL_MESSAGE = "Please enter a valid email address.";
        public static readonly string PLEASE_ENTER_STUDENT_NAME = "Please enter student name";
        public static readonly string PLEASE_SELECT_GENDER = "Please select gender";
        public static readonly string PLEASE_ENTER_STUDENT_CLASS = "Please enter student class";
        public static readonly string PLEASE_ENTER_ADDRESS = "Please enter address";
        public static readonly string PLEASE_ENTER_STREET = "Please enter street";
        public static readonly string PLEASE_ENTER_CITY = "Please enter city";
        public static readonly string PLEASE_ENTER_STATE = " Please enter state";
        public static readonly string PLEASE_ENTER_COUNTRY = "  Please enter country";
        public static readonly string PLEASE_ENTER_ZIPCODE = "  Please enter postalcode.";
        public static readonly string PLEASE_ENTER_VALID_ZIP_CODE = "Please enter valid postalcode.";
        public static readonly string EMAIL_EMPTY_MESSAGE = "Email can not be empty.";
        public static readonly string PLEASE_ENTER_CONTACTNO = "Please enter contactno";
        public static readonly string PLEASE_ENTER_VALID_CONTACTNO = "Please enter valid contactno";




        public static readonly string PrefIsAdmin = "prefisadmin";
        public static readonly string PrefStudentID = "prefstudentid";
        public static readonly string PrefUserName = "prefusername";
        public static readonly string PrefPassword = "prefpassword";
    }
}
