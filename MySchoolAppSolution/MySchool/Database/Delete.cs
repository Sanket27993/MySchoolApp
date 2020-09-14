using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchool
{
    public interface IDelete
    {
        int Execute(IDatabaseconnection _Idatabaseconnection);
        int DeleteExecute(IDatabaseconnection _Idatabaseconnection);
    }
    public class Delete<T> : IDelete
    {
        readonly T t;
        readonly string _Query;
        public Delete(string query)
        {
            _Query = query;
        }

        public Delete(T value)
        {
            this.t = value;
        }

        public int Execute(IDatabaseconnection _Idatabaseconnection) 
        {
            var result = _Idatabaseconnection.SQLiteConnection.ExecuteScalar<int>(_Query);
            return result;
        }

        public int DeleteExecute(IDatabaseconnection _Idatabaseconnection)
        {
            var result = _Idatabaseconnection.SQLiteConnection.Delete(t);
            return result;
        }

    }
}
