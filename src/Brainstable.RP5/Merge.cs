using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstable.RP5
{
    /// <summary>
    /// Класс для объединения файлов RP5
    /// </summary>
    public class Merge
    {
        private readonly Encoding encoding = Encoding.GetEncoding(1251); // ANSI Кодировка по умолчанию для корректного отображения в Excel
        private string pathRepository;
        private string tail = "1.0.0.ru.ansi.00000000.csv";

        #region Properties

        /// <summary>
        /// Кодировка ANSI по умолчанию для выходного файла (для корректного отображения в Excel)
        /// </summary>
        public Encoding Encoding => encoding;
        /// <summary>
        /// Директория репозитория (папка для сохранения выходных файлов)
        /// </summary>
        public string Repository => pathRepository;

        #endregion

        public void Join(string outFileName, string fileName1, string fileName2)
        {
            ReaderRP5 reader1 = new ReaderRP5Csv();


        }


    }
}
