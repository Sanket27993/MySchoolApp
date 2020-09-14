using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchool
{
    public interface IUpdate
    {
        int Execute(IDatabaseconnection _Idatabaseconnection);
    }
    public class Update<T> : IUpdate
    {
        readonly T t;

        public Update(T value)
        {
            this.t = value;

       
        }

        public int Execute(IDatabaseconnection _Idatabaseconnection)
        {
            var result = _Idatabaseconnection.SQLiteConnection.Update(t);
            return result;
        }
    }
}
