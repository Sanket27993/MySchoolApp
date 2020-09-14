using System;
using System.Collections.Generic;
using System.Text;

namespace MySchool
{
    public partial class ValidationLogin
    {
        public string Message { get; set; }
        public bool isValid { get; set; }
      

        /// <summary>
        /// Set sign in validation
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public bool Validate(UserOAuthenticationModel userOAuthentication)
        {

            if (string.IsNullOrEmpty(userOAuthentication.UserName))
            {
                Message = AppConstant.USERNAME_EMPTY_MESSAGE;
                isValid = false;
            }
            else if (string.IsNullOrEmpty(userOAuthentication.Password))
            {
                Message = AppConstant.PASSWORD_EMPTY_MESSAGE;
                isValid = false;
            }
            else
            {
                Message = string.Empty;
                isValid = true;
            }
            return isValid;
        }
    }
}
