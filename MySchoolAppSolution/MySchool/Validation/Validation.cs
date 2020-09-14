using System.Text.RegularExpressions;

namespace MySchool
{
    
        public static class Validation
        {
            public static bool IsEmailValid(string param)
            {
                return Regex.IsMatch(param, AppConstant.EMAIL_VALIDATION_REGEX, RegexOptions.IgnoreCase);
            }

        }
    
}
