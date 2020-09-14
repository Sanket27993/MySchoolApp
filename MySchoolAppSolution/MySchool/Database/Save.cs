namespace MySchool
{
    /// <summary>
    /// this interface for save 
    /// </summary>
    public interface ISave
    {
        int Execute(IDatabaseconnection _Idatabaseconnection);
        int ExecuteWithoutReplace(IDatabaseconnection _Idatabaseconnection);
    }
    /// <summary>
    /// Save data in localdatabase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Save<T> : ISave
    {
        readonly T t;

        public Save(T value)
        {
            this.t = value;
        }

        public int Execute(IDatabaseconnection _Idatabaseconnection)
        {
            var result = _Idatabaseconnection.SQLiteConnection.InsertOrReplace(t);
            return result;
        }
        public int ExecuteWithoutReplace(IDatabaseconnection _Idatabaseconnection)
        {
            var result = _Idatabaseconnection.SQLiteConnection.Insert(t);
            return result;
        }
    }
}
