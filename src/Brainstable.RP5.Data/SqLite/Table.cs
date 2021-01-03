using System.Data;
using System.Text;
using System.Data.SQLite;

namespace Brainstable.RP5.Data.SqLite
{
    public static class Table
    {
        public static void DeleteTable(string nameTable, string connectionString)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"DROP TABLE IF EXISTS [{nameTable}];");

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SQLiteCommand cmd = new SQLiteCommand(sb.ToString(), connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CreateTableMeteoStations(string connectionString)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CREATE TABLE IF NOT EXISTS [MeteoStations] (");
            sb.AppendLine("[Id] TEXT PRIMARY KEY");
            sb.AppendLine(", [Name] TEXT NOT NULL");
            sb.AppendLine(", [Country] TEXT NOT NULL");
            sb.AppendLine(", [TypeId] TEXT NOT NULL");
            sb.AppendLine(", [Site] TEXT NOT NULL");
            sb.AppendLine(", [Link] TEXT NOT NULL");
            sb.AppendLine(", [StartFetch] TEXT NOT NULL");
            sb.AppendLine(", [EndFetch] TEXT NOT NULL");
            sb.AppendLine(");");

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SQLiteCommand cmd = new SQLiteCommand(sb.ToString(), connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CreateTableMeteoDataByYears(string meteoStationId, string connectionString)
        {
            CreateTableMeteoData(meteoStationId, "Y", connectionString);
        }

        public static void CreateTableMeteoDataByMonth(string meteoStationId, string connectionString)
        {
            CreateTableMeteoData(meteoStationId, "M", connectionString);
        }

        public static void CreateTableMeteoDataByDays(string meteoStationId, string connectionString)
        {
            CreateTableMeteoData(meteoStationId, "D", connectionString);
        }

        public static void CreateTableMeteoDataByPoints(string meteoStationId, string connectionString)
        {
            CreateTableMeteoData(meteoStationId, "P", connectionString);
        }

        public static void CreateTableMeteoDataByDecades(string meteoStationId, string connectionString)
        {
            CreateTableMeteoData(meteoStationId, "D10", connectionString);
        }

        public static void CreateTableMeteoDataByFiveDays(string meteoStationId, string connectionString)
        {
            CreateTableMeteoData(meteoStationId, "D5", connectionString);
        }

        public static void CreateTableMeteoData(string meteoStationId, string suffix, string connectionString)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE IF NOT EXISTS [_{meteoStationId}{suffix}] (");
            sb.AppendLine("[Date] TEXT PRIMARY KEY");
            sb.AppendLine(", [T] REAL NULL");
            sb.AppendLine(", [TN] REAL NULL");
            sb.AppendLine(", [TX] REAL NULL");
            sb.AppendLine(", [R] REAL NULL");
            sb.AppendLine(", [SH] REAL NULL");
            sb.AppendLine(");");

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SQLiteCommand cmd = new SQLiteCommand(sb.ToString(), connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool TableExists(string nameTable, string connectionString)
        {
            string query = $@"select 
                            case when exists 
                                (select 1 from sqlite_master WHERE type='table' and name = '{nameTable}') 
                                then 1 
                                else 0 
                            end as TableExists";
            int count = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            count = dr.GetInt32(0);
                        }
                    }
                    dr.Close();
                }
            }
            return count == 1;
        }
    }
}
