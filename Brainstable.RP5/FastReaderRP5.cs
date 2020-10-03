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
            return MetaDataRP5.CreateFromFileCsv(fileName);
        }

        /// <summary>
        /// Прочитать схему
        /// </summary>
        /// /// <param name="fileName">Путь к файлу</param>
        /// <returns>Схема</returns>
        public static SchemaRP5 ReadSchemaFromCsv(string fileName)
        {
            return SchemaRP5.CreateFromFileCsv(fileName);
        }
    }
}
