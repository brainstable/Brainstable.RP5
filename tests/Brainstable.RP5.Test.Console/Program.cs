using System;
using System.IO;

namespace Brainstable.RP5.Test.Console
{
    class Program
    {
        private static string fileName1, fileName2;

        private static string dir = "Merge";
        private static string outDir = "OutDir";

        static void Main(string[] args)
        {
            //fileName1 = Path.Combine(dir, "29766.01.12.2020.17.12.2020.1.0.0.ru.ansi.00000000.csv.gz");
            //fileName2 = Path.Combine(dir, "29766.10.12.2020.24.12.2020.1.0.0.ru.utf8.00000000.csv.gz");

            //Merger merger = new Merger();
            //var set = merger.JoinToSet(fileName1, new []{fileName2});
            //foreach (var observationPoint in set)
            //{
            //    System.Console.WriteLine(observationPoint.ToString());
            //}

            // Merger files
            DateTime start = DateTime.Now;
            DirectoryRP5 directory = new DirectoryRP5("FilesRP5");
            for (int i = 0; i < directory.Files.Length; i++)
            {
                System.Console.WriteLine(directory.Files[i]);
            }

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            directory.MergeFiles(outDir);
            DateTime end = DateTime.Now;
            System.Console.WriteLine(end - start);
            System.Console.WriteLine("Нажмите любую клавишу");
            System.Console.ReadKey();
        }
    }
}
