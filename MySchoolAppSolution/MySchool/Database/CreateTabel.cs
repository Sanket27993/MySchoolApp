using SQLite;

namespace MySchool
{
    /// <summary>
    /// this interface for create tabel
    /// </summary>
    public interface ICreateTabel
    {
        int Execute<T>(IDatabaseconnection _Idatabaseconnection);
    }
    /// <summary>
    /// this class for createtabel
    /// </summary>
    public class CreateTabel : ICreateTabel
    {
        public int Execute<T>(IDatabaseconnection _Idatabaseconnection)
        {
            CreateTableResult result = _Idatabaseconnection.SQLiteConnection.CreateTable<T>();
            return (int)result;
        }
    }
}
