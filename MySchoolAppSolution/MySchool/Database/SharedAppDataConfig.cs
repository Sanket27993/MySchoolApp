using System.IO;
using System.Threading.Tasks;

namespace MySchool
{
    /// <summary>
    /// Class used to set shared app data.
    /// </summary>
    public static class SharedAppDataConfig
    {
        
        private static string dbpath;

        public static string LocalDatabasePath
        {
            get
            {
                return dbpath;
            }
            set
            {
                dbpath = value;
               
                InilizedDatabase();
            }
        }

        public static string PDFPath { get; set; }

        public  static void InilizedDatabase()
        {
            DatabaseConnection databaseConnection = DatabaseConnection.Instance;
            databaseConnection.DatabasePath = dbpath;       
        }
    }
}
