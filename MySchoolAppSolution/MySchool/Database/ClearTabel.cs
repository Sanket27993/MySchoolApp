using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchool
{
    public interface IClearTable
    {
        int Execute(string tablename, IDatabaseconnection _Idatabaseconnection);
    }
    /// <summary>
    /// this class for createtabel
    /// </summary>
    public class ClearTabel : IClearTable
    {
        public int Execute(string tablename, IDatabaseconnection _Idatabaseconnection)
        {
            var result = _Idatabaseconnection.SQLiteConnection.Execute("delete from " + tablename);
            return result;
        }
    }

}
