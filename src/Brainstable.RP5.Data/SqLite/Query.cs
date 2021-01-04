using System.Data;
using System.Data.SQLite;

namespace Brainstable.RP5.Data.SqLite
{
    public static class Query
    {
        public static void RunExcuteNonQuery(string query, string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static void RunExecuteNonQueryIfExists(string queryExists, string query, string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (var cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = queryExists;
                        var obj = cmd.ExecuteScalar();
                        if (obj == null || int.Parse(obj.ToString()) != 1)
                        {
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}