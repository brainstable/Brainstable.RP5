using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Brainstable.RP5.Data.SqLite;

namespace Brainstable.RP5.Test.Console
{
    class Program
    {
        private static string fileName1, fileName2;
        private static string fileName3 = "SOP\\29766.01.12.2020.17.12.2020.1.0.0.ru.ansi.00000000.csv";

        private static string dir = "Merge";
        private static string outDir = "OutDir";

        static void Main(string[] args)
        {
            fileName1 = Path.Combine(dir, "29766.01.12.2020.17.12.2020.1.0.0.ru.ansi.00000000.csv.gz");
            fileName2 = Path.Combine(dir, "29766.10.12.2020.24.12.2020.1.0.0.ru.utf8.00000000.csv.gz");

            //Merger merger = new Merger();
            //var set = merger.JoinToSet(fileName1, new []{fileName2});
            //foreach (var observationPoint in set)
            //{
            //    System.Console.WriteLine(observationPoint.ToString());
            //}

            // Merger files
            //DateTime start = DateTime.Now;
            //DirectoryRP5 directory = new DirectoryRP5("FilesRP5");
            //for (int i = 0; i < directory.Files.Length; i++)
            //{
            //    System.Console.WriteLine(directory.Files[i]);
            //}

            //if (!Directory.Exists(outDir))
            //{
            //    Directory.CreateDirectory(outDir);
            //}

            //directory.MergeFiles(outDir);
            //DateTime end = DateTime.Now;
            //System.Console.WriteLine(end - start);
            //System.Console.WriteLine("Нажмите любую клавишу");

            //LoadSimpleObservetionPoints(fileName3);

            CreateDatabase();
            string conn = "data source=data\\mydatabase.db";
            Table.CreateTableMeteoStations(conn);
            MetaDataRP5 metaData = FastReaderRP5.ReadMetaDataFromCsv(fileName3);
            CRUDMeteoStations crud = new CRUDMeteoStations(conn);
            //crud.Delete(metaData.Identificator);
            crud.Insert(metaData.Identificator, metaData.Station, metaData.Country, metaData.Synoptic.TypeSynopticRp5.ToString(), metaData.Link);

            Table.CreateTableMeteoDataByPoints(metaData.Identificator, "data source=data\\mydatabase.db");
            CRUDMeteoData crudMeteo = new CRUDMeteoData("data source=data\\mydatabase.db");
            List<SimpleObservationPoint> list = FastReaderRP5.ReadListSimpleObservationPointsFromCsv(fileName3);
            crudMeteo.Insert("_" + metaData.Identificator + "P", list);
            //crudMeteo.Insert("_" + metaData.Identificator + "P", DateTime.Now, -25.2, null, -35.2, 0, 15);
            System.Console.WriteLine("END");
            System.Console.ReadKey();
        }

        private static void CreateDatabase()
        {
            Database.CreateDatabase("data\\mydatabase.db");
            //string conn = "data source=data\\mydatabase1.db";
            //System.Console.WriteLine(Table.TableExists("MeteoStations", conn));
            //Table.DeleteTable("MeteoStations", conn);
            //System.Console.WriteLine(Table.TableExists("MeteoStations", conn));
            //Table.CreateTableMeteoStations(conn);
            //Table.CreateTableMeteoDataByYears("29566", conn);
            //Table.CreateTableMeteoDataByMonth("29566", conn);
            //Table.CreateTableMeteoDataByDays("29566", conn);
            //Table.CreateTableMeteoDataByDecades("29566", conn);
            //Table.CreateTableMeteoDataByFiveDays("29566", conn);
            //Table.CreateTableMeteoDataByPoints("29566", conn);
            //System.Console.WriteLine(Table.TableExists("MeteoStations", conn));
        }

        private static void LoadSimpleObservetionPoints(string fileName)
        {
            List<SimpleObservationPoint> list = FastReaderRP5.ReadListSimpleObservationPointsFromCsv(fileName);
            foreach (var point in list)
            {
                System.Console.WriteLine($"{point.DateTime}\t{point.Temperature}\t{point.MinTemperature}\t{point.MaxTemperature}\t{point.Rainfall}\t{point.SnowHight}");
            }
        }
    }
}
