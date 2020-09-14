using System.Collections.Generic;
using System.Linq;

namespace MySchool
{
    /// <summary>
    /// this interface for Fetch
    /// </summary>

    public interface IFetch
    {
        List<T> Execute<T>(IDatabaseconnection _Idatabaseconnection) where T : new();
        T ExecuteSingle<T>(IDatabaseconnection _Idatabaseconnection) where T : new();

    }
    /// <summary>
    /// this class for fetch record
    /// </summary>
    public class Fetch : IFetch
    {
       readonly string _Query;
        public Fetch(string query)
        {
            _Query = query;
        }

        public List<T> Execute<T>(IDatabaseconnection _Idatabaseconnection) where T : new()
        {
            var result = _Idatabaseconnection.SQLiteConnection.Query<T>(_Query);
            return result;
        }

        public T ExecuteSingle<T>(IDatabaseconnection _Idatabaseconnection) where T : new()
        {
            var result = _Idatabaseconnection.SQLiteConnection.Query<T>(_Query);
            return result.FirstOrDefault();
        }
    }
}
