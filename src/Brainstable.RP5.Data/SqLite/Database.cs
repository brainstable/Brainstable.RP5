using System;
using System.IO;
using System.Data.SQLite;

namespace Brainstable.RP5.Data.SqLite
{
    public static class Database
    {
        public static void CreateDatabase(string fileName, bool isOverWrite = false)
        {
            string dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(fileName))
            {
                if (isOverWrite)
                {
                    try
                    {
                        File.Delete(fileName);
                        SQLiteConnection.CreateFile(fileName);
                    }
                    catch (IOException e)
                    {
                        throw new IOException("Невозможно получить доступ к файлу");
                    }
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
