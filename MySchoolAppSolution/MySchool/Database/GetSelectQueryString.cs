
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchool
{
    public static class GetSelectQueryString
    {
        public static string GetQuery(string tableName, string columName, string value)
        {
            var query = "SELECT * FROM " + tableName + " WHERE " + columName + " = " + value;
            return query;
        }

        public static string GetQuery(string tableName)
        {
            var query = "SELECT * FROM " + tableName;
            return query;
        }
    }
}
