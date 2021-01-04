using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Brainstable.RP5.Data.SqLite
{
    public class CRUDMeteoData
    {
        private readonly string connectionString;

        public CRUDMeteoData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Insert(string tableName, DateTime dateTime, double? t, double? tn, double? tx, double? r, double? sh)
        {
            Query.RunExcuteNonQuery(GetQueryInsert(tableName, dateTime, t, tn, tx, r, sh), connectionString);
        }

        public void Insert(string tableName, List<SimpleObservationPoint> points)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        foreach (var point in points)
                        {
                            string query1 =
                                $"SELECT 1 FROM {tableName} WHERE Date = '{point.DateTime.ToString("yyyy-MM-dd hh:mm:sss")}'";
                            if (point.MinTemperature.HasValue)
                            {
                                ;
                            }

                            string query2 = GetQueryInsert(tableName, point.DateTime, point.Temperature,
                                point.MinTemperature, point.MaxTemperature, point.Rainfall, point.SnowHight);
                            Query.RunExecuteNonQueryIfExists(query1, query2, connectionString);
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

        private string GetQueryInsert(string tableName, DateTime dateTime, double? t, double? tn, double? tx, double? r, double? sh)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"INSERT INTO [{tableName}] (Date, T, TN, TX, R, SH) VALUES (");
            sb.Append($"'{dateTime.ToString("yyyy-MM-dd hh:mm:sss")}'");
            sb.Append(", ");
            sb.Append(t.HasValue ? $"{t.Value.ToString().Replace(',', '.')}" : "NULL");
            sb.Append(", ");
            sb.Append(tn.HasValue ? $"{tn.Value.ToString().Replace(',', '.')}" : "NULL");
            sb.Append(", ");
            sb.Append(tx.HasValue ? $"{tx.Value.ToString().Replace(',', '.')}" : "NULL");
            sb.Append(", ");
            sb.Append(r.HasValue ? $"{r.Value.ToString().Replace(',', '.')}" : "NULL");
            sb.Append(", ");
            sb.Append(sh.HasValue ? $"{sh.Value.ToString().Replace(',', '.')}" : "NULL");
            sb.Append(");");
            return sb.ToString();
        }
    }
}