using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Brainstable.RP5.Data.SqLite
{
    public class CRUDMeteoStations
    {
        private readonly string connectionString;

        public CRUDMeteoStations(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MetaDataRP5 Select(string meteoStationId)
        {

            return null;
        }

        public void Delete(string id)
        {
            string query = $"DELETE FROM [MeteoStations] WHERE [Id] = '{id}';";

            Query.RunExcuteNonQuery(query, connectionString);
        }
        
        public void Insert(string id, string name, string country, string typeId, string link)
        {
            StringBuilder query1 = new StringBuilder();
            query1.AppendLine($"SELECT 1 FROM [MeteoStations] WHERE [Id] = '{id}';");
            
            StringBuilder query2 = new StringBuilder();
            query2.AppendLine("INSERT INTO [MeteoStations] (Id, Name, Country, Link, TypeId) VALUES (");
            query2.AppendLine($"'{id}', '{name}', '{country}', '{link}', '{typeId}');");

            Query.RunExecuteNonQueryIfExists(query1.ToString(), query2.ToString(), connectionString);
        }
    }
}
