using System;
using System.Collections.Generic;
using System.IO;

namespace Brainstable.RP5
{
    /// <summary>
    /// Быстрое чтение метаданных и схемы из файлов
    /// </summary>
    public static class FastReaderRP5
    {
        /// <summary>
        /// Прочитать метаданные
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns></returns>
        public static MetaDataRP5 ReadMetaDataFromCsv(string fileName)
        {
            MetaDataRP5 meta = null;
            try
            {
                int counter = 0;
                string line;
                string[] arr = new string[5];

                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                while ((line = file.ReadLine()) != null)
                {
                    arr[counter++] = line;
                    if (counter == arr.Length)
                        break;
                }
                file.Close();
                meta = MetaDataRP5.CreateFromArrayString(arr);
            }
            catch (Exception e)
            {

            }
            return meta;
        }

        /// <summary>
        /// Прочитать схему
        /// </summary>
        /// /// <param name="fileName">Путь к файлу</param>
        /// <returns>Схема</returns>
        public static SchemaRP5 ReadSchemaFromCsv(string fileName)
        {
            SchemaRP5 schema = null;
            try
            {
                int counter = 0;
                string line;
                List<string> list = new List<string>();
                StreamReader file = new StreamReader(fileName, HelpMethods.CreateEncoding(fileName));
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 6)
                        break;
                    counter++;
                }
                file.Close();
                string[] arr = line.Trim().Replace("\"", "").Split(';');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i == arr.Length - 1)
                    {
                        if (string.IsNullOrEmpty(arr[i]))
                        {
                            continue;
                        }
                    }
                    list.Add(arr[i]);
                }

                schema = SchemaRP5.CreateFromArraySchema(list.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return schema;
        }
    }
}
