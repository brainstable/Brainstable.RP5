using System.IO;
using System.IO.Compression;

namespace Brainstable.RP5
{
    /// <summary>
    /// Архив GZ
    /// </summary>
    internal static class GZ
    {
        /// <summary>
        /// Извлечение архива в папку
        /// </summary>
        /// <param name="fileNameGz">Имя архива</param>
        /// <param name="directoryExtract">Папка извлечения</param>
        /// <returns>Путь к извлеченному файлу</returns>
        public static string Decompress(string fileNameGz, string directoryExtract)
        {
            if (!Directory.Exists(directoryExtract))
            {
                Directory.CreateDirectory(directoryExtract);
            }

            FileInfo fileToDecompress = new FileInfo(fileNameGz);

            // сам архив
            string currentFile = fileToDecompress.FullName; 
            // Название файла внутри архива
            string fileNameIn = currentFile.Remove(currentFile.Length - fileToDecompress.Extension.Length);
            // Путь к распакованному файлу
            string newFile = Path.Combine(directoryExtract, Path.GetFileName(fileNameIn));

            // если файл по данному пути существует, то удалить его
            if (File.Exists(newFile))
                File.Delete(newFile);

            // Открываем архив
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                //ZipArchive z = new ZipArchive(originalFileStream);
                //z.GetEntry("").Open();


                // Создаем файл
                using (FileStream decompressedFileStream = File.Create(newFile))
                {
                    
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
            

            return newFile;
        }

        /// <summary>
        /// Извлечение архива во временную папку пользователя
        /// </summary>
        /// <param name="fileNameGz">Имя архива</param>
        /// <returns>Путь к извлеченному файлу</returns>
        public static string DecompressTempFolder(string fileNameGz)
        {
            return Decompress(fileNameGz, Path.GetTempPath());
        }

        /// <summary>
        /// Извлечение архива в текущую папку
        /// </summary>
        /// <param name="fileNameGz">Имя архива</param>
        /// <returns>Путь к извлеченному файлу</returns>
        public static string DecompressCurrentFolder(string fileNameGz)
        {
            string currentFolder = Path.GetDirectoryName(fileNameGz);
            return Decompress(fileNameGz, currentFolder);
        }
    }


    /*
    /// <summary>
    /// Направление ветра
    /// </summary>
    internal enum WindDirection
    {
        /// <summary>
        /// Штиль, безветрие
        /// </summary>
        Calm = 0,
        /// <summary>
        /// Переменное напраление
        /// </summary>
        Variable = 1,
        /// <summary>
        /// Севера
        /// </summary>
        N = 2,
        /// <summary>
        /// Северо-северо-востока
        /// </summary>
        NNE,
        /// <summary>
        /// Северо-востока
        /// </summary>
        NE,
        /// <summary>
        /// Востоко-северо-востока
        /// </summary>
        ENE,
        /// <summary>
        /// Востока
        /// </summary>
        E,
        /// <summary>
        /// Востоко-юго-востока
        /// </summary>
        ESE,
        /// <summary>
        /// Юго-востока
        /// </summary>
        SE,
        /// <summary>
        /// Юго-юго-востока
        /// </summary>
        SSE,
        /// <summary>
        /// Юга
        /// </summary>
        S,
        /// <summary>
        /// Юго-юго-запада
        /// </summary>
        SSW,
        /// <summary>
        /// Юго-запада
        /// </summary>
        SW,
        /// <summary>
        /// Западо-юго-запада
        /// </summary>
        WSW,
        /// <summary>
        /// Запада
        /// </summary>
        W,
        /// <summary>
        /// Западо-северо-запада
        /// </summary>
        WNW,
        /// <summary>
        /// Северо-запада
        /// </summary>
        NW,
        /// <summary>
        /// Северо-северо-запада
        /// </summary>
        NNW
    }

    */
}
