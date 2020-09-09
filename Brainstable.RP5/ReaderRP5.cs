using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brainstable.RP5
{
    /// <summary>
    /// Класс для чтения файлов RP5 и их архивов
    /// </summary>
    public abstract class ReaderRP5
    {
        private MetaDataRP5 metaDataRp5;
        private string[] stringArrayData;
        private SchemaRP5 schema;
        private Encoding encoding;
        private TypeLoadFileRP5 typeLoadFileRp5 = TypeLoadFileRP5.Unknown;
        private string fileName;
        private bool isArchive = false;
        private string decompressFileName = String.Empty;
        private bool isCleanDecompress = true;

        

        /// <summary>
        /// Метаданные
        /// </summary>
        public MetaDataRP5 MetaDataRP5 => metaDataRp5;

        /// <summary>
        /// Массив строк с данными
        /// </summary>
        public string[] StringArrayData => stringArrayData;

        /// <summary>
        /// Схема
        /// </summary>
        public SchemaRP5 Schema => schema;

        /// <summary>
        /// Кодировка файла
        /// </summary>
        public Encoding Encoding => encoding;

        /// <summary>
        /// Тип загружаемого файла
        /// </summary>
        public TypeLoadFileRP5 TypeLoadFileRp5 => typeLoadFileRp5;

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string FileName => fileName;

        /// <summary>
        /// Файл является архивом
        /// </summary>
        public bool IsArchive => isArchive;
        /// <summary>
        /// Путь к разархивируемому файлу
        /// </summary>
        public string DecompessFileName => decompressFileName;
        /// <summary>
        /// Удалять разархивированный файл
        /// </summary>
        public bool IsCleanDecompress => isCleanDecompress;

        public virtual void Read(string fileName, bool isCleanDecompess = true)
        {
            string tempFileName = fileName; // сохраняем временно оригинальный путь к файлу
            
            this.fileName = fileName;

            isArchive = false;
            decompressFileName = String.Empty;
            this.isCleanDecompress = isCleanDecompess;

            encoding = HelpMethods.CreateEncoding(fileName);

            string extension = Path.GetExtension(fileName);

            // Проверяем является ли файл архивом
            // если Да
            if (extension.Contains("gz"))
            {
                // то разорхивируем файл
                
                this.fileName = GZ.DecompressTempFolder(fileName);
                decompressFileName = this.fileName;
                isArchive = true;
            }

            // Проверяем является ли файлом CSV
            if (this.fileName.EndsWith("csv"))
            {
                typeLoadFileRp5 = isArchive ? TypeLoadFileRP5.ArchCsv : TypeLoadFileRP5.Csv;
            }
            // Проверяем является ли файлом Excel
            if (this.fileName.EndsWith("xls"))
            {
                typeLoadFileRp5 = isArchive ? TypeLoadFileRP5.ArchXls : TypeLoadFileRP5.Xls;
            }
            
            string[] arr = CreateArrayByLine(this.fileName);

            metaDataRp5 = GetMetaDataRp5(arr);
            schema = GetSchema(arr);
            stringArrayData = GetStringArray(arr);

            // Если архив, то удаляем извлеченный файл
            if (isCleanDecompess && isArchive)
            {
                if (File.Exists(this.fileName))
                    File.Delete(this.fileName);
            }
            this.fileName = tempFileName; 
        }


        protected abstract string[] CreateArrayByLine(string fileName);

        /// <summary>
        /// Очистить (удалить) разархивированный файл
        /// </summary>
        /// <returns>True - файл успешно удален</returns>
        public bool CleanDecompress()
        {
            if (decompressFileName.Length > 0)
            {
                if (File.Exists(decompressFileName))
                {
                    File.Delete(decompressFileName);
                    return true;
                }
            }

            return false;
        }

        #region Private methods

        private MetaDataRP5 GetMetaDataRp5(string[] arr)
        {
            string[] arrMetaData = new string[5];
            for (int i = 0; i < arrMetaData.Length; i++)
            {
                arrMetaData[i] = arr[i];
            }
            return MetaDataRP5.CreateFromArrayString(arrMetaData);
        }

        /// <summary>
        /// Получить массив строк
        /// </summary>
        /// <returns>Массив строк</returns>
        private string[] GetStringArray(string[] arr)
        {
            string[] target = new string[arr.Length - 7];
            Array.Copy(arr, 7, target, 0, target.Length);
            return target;
        }

        /// <summary>
        /// Получить схему
        /// </summary>
        /// <returns>Схема</returns>
        private SchemaRP5 GetSchema(string[] arr)
        {
            return SchemaRP5.CreateFromLineSchema(arr[6]);
        }

        private bool Validate(string fileName)
        {
            return true;
        }

        #endregion

        

    }
}