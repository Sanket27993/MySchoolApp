using SQLite;

namespace MySchool
{
    /// <summary>
    ///this interface for Database Connection
    /// </summary>
    public interface IDatabaseconnection
    {
        SQLiteConnection SQLiteConnection { get; set; }
        string DatabasePath { get; set; }
    }
    /// <summary>
    /// this class for Database Connection
    /// </summary>
    public class DatabaseConnection : IDatabaseconnection
    {
        string _DatabasePath;
        private static DatabaseConnection _databaseConnection;
        public SQLiteConnection SQLiteConnection { get; set; }
        public string DatabasePath
        {
            get
            { return _DatabasePath; }
            set
            {
                _DatabasePath = value;
                Connection();
            }
        }
        public static DatabaseConnection Instance
        {
            get
            {
                if (_databaseConnection == null)
                    _databaseConnection = new DatabaseConnection();

                return _databaseConnection;
            }
        }
        /// <summary>
        /// this method for connection with sqliite Database
        /// </summary>
        private void Connection()
        {
            SQLiteConnection = new SQLiteConnection(DatabasePath);
        }
    }
}
