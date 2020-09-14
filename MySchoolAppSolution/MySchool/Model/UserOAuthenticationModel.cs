using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySchool
{
    

    public static class ConstUserOAuthentication
    {
        public const string TableName = "UserOAuthentication";
        public const string UserID = "userid";
        public const string UserName = "username";
        public const string Password = "password";
        public const string StudentID = "studentid";
        public const string IsAdmin = "isadmin";
       
    }

    [Table(ConstUserOAuthentication.TableName)]
    public class UserOAuthenticationModel
    {
        [Column(ConstUserOAuthentication.UserID)]
        public int UserID { get; set; }

        [Column(ConstUserOAuthentication.UserName)]
        public string UserName { get; set; }

        [Column(ConstUserOAuthentication.Password)]
        public string Password { get; set; }

        [Column(ConstUserOAuthentication.StudentID)]
        public int StudentID { get; set; }

        [Column(ConstUserOAuthentication.IsAdmin)]
        public bool IsAdmin { get; set; }
    }
}
