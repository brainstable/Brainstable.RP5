using System.IO;
using System.Data.SQLite;

namespace Brainstable.RP5.Data.SqLite
{
    public static class Database
    {
        public static void CreateDatabase(string fileName, bool isOverWrite = false)
        {
            if (File.Exists(fileName))
            {
                if (isOverWrite)
                {
                    File.Delete(fileName);
                    SQLiteConnection.CreateFile(fileName);
                }
            }
            else
            {
                SQLiteConnection.CreateFile(fileName);
            }
        }

        public static void DeleteDatabase(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}
