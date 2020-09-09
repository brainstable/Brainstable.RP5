using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brainstable.RP5.Merger
{
    /// <summary>
    /// Класс для объединения файлов RP5
    /// </summary>
    public class MergeRP5
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

        #region Ctors

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dirRepository">Путь к директории репозитория</param>
        public MergeRP5(string dirRepository)
        {
            this.pathRepository = dirRepository;

            if (!Directory.Exists(pathRepository)) // если директории не существует
            {
                Directory.CreateDirectory(pathRepository); // то создать ее
            }
        }

        #endregion

        /// <summary>
        /// Получить список метаданных
        /// </summary>
        /// <returns>Список метаданных</returns>
        public List<MetaDataRP5> GetListMetaData()
        {
            string[] files = Directory.GetFiles(pathRepository, $"*{tail}");
            List<MetaDataRP5> list = new List<MetaDataRP5>();
            if (files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    IReaderFileRP5 reader = new ReaderFileCsvRP5(files[i]);
                    list.Add(reader.MetaDataRP5);
                }
            }

            return list;
        }

        /// <summary>
        /// Обрезать концы файла, оставить от - до
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void Cut(string stationId, DateTime from, DateTime to)
        {
            string fileName = FindFileByStationId(stationId);
            if (!string.IsNullOrEmpty(fileName))
            {
                IReaderFileRP5 reader = new ReaderFileCsvRP5(fileName);
                SortedSet<ObservationPoint> points = reader.GetSortedSetObservationPoints();
                SortedSet<ObservationPoint> set = new SortedSet<ObservationPoint>();
                foreach (var point in points)
                {
                    if (point.DateTime >= from & point.DateTime <= to)
                        set.Add(point);
                }

                points = null;

                string newFileName = CreateFileName(stationId, from, to);
                Writer(newFileName, set, reader.MetaDataRP5, reader.Schema);
            }
        }

        /// <summary>
        /// Обрезать файл с начала до
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="to"></param>
        public void CutStart(string stationId, DateTime date)
        {
            string fileName = FindFileByStationId(stationId);
            if (!string.IsNullOrEmpty(fileName))
            {
                IReaderFileRP5 reader = new ReaderFileCsvRP5(fileName);
                SortedSet<ObservationPoint> points = reader.GetSortedSetObservationPoints();
                SortedSet<ObservationPoint> set = new SortedSet<ObservationPoint>();
                foreach (var point in points)
                {
                    if (point.DateTime >= date)
                        set.Add(point);
                }

                points = null;

                string newFileName = CreateFileName(stationId, date, reader.MaxDate);
                Writer(newFileName, set, reader.MetaDataRP5, reader.Schema);
            }
        }

        /// <summary>
        /// Обрезать файл с конца от
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="date"></param>
        public void CutEnd(string stationId, DateTime date)
        {
            string fileName = FindFileByStationId(stationId);
            if (!string.IsNullOrEmpty(fileName))
            {
                IReaderFileRP5 reader = new ReaderFileCsvRP5(fileName);
                SortedSet<ObservationPoint> points = reader.GetSortedSetObservationPoints();
                SortedSet<ObservationPoint> set = new SortedSet<ObservationPoint>();
                foreach (var point in points)
                {
                    if (point.DateTime <= date)
                        set.Add(point);
                }

                points = null;

                string newFileName = CreateFileName(stationId, reader.MinDate, date);
                Writer(newFileName, set, reader.MetaDataRP5, reader.Schema);
            }
        }

        /// <summary>
        /// Создать название файла
        /// </summary>
        /// <param name="stationId">ИД метеостанции</param>
        /// <param name="from">Минимальная дата</param>
        /// <param name="to">Максимальная дата</param>
        /// <returns>Название файла</returns>
        public string CreateFileName(string stationId, DateTime from, DateTime to)
        {
            return
                $"{stationId}.{from.Day:D2}.{from.Month:D2}.{from.Year}.{to.Day:D2}.{to.Month:D2}.{to.Year}.{tail}";
        }

        /// <summary>
        /// Найти в репозитории файл по ИД метеостанции
        /// </summary>
        /// <param name="stationId">ИД метеостанции</param>
        /// <returns>Путь к файлу</returns>
        public string FindFileByStationId(string stationId)
        {
            string fileName = "";
            string[] files = Directory.GetFiles(pathRepository, $"{stationId}*{tail}");
            if (files.Length > 0)
            {
                fileName = files[0]; // если файлов больше одного, то выбрать первый файл
            }

            return fileName;
        }

        /// <summary>
        /// Добавить файл в репозиторий
        /// </summary>
        /// <param name="fileName">Путь к добавляемому файлу</param>
        public void Add(string fileName)
        {
            // Создаем экземпляр считывателя файла rp5
            IReaderFileRP5 reader = new ReaderFileCsvRP5(fileName);
            // Считываем метаданные
            MetaDataRP5 metaDataRp5 = reader.MetaDataRP5;
            // Счиываем схему
            string[] schema = reader.Schema;
            // Считываем отсортированный набор точек наблюдения
            SortedSet<ObservationPoint> points = reader.GetSortedSetObservationPoints();


            string fileNameWrite = "";

            // Определяем ИД метеостанции
            string stationId = metaDataRp5.Identificator;
            // Ищем в репозитории файл с тем же ИД метеостанции
            string fileNameRepos = FindFileByStationId(stationId);
            if (!string.IsNullOrWhiteSpace(fileNameRepos)) // если файл существует
            {
                reader = new ReaderFileCsvRP5(fileNameRepos); // то считываем его
                // Считываем отсортированный набор точек наблюдения
                SortedSet<ObservationPoint> reposPoints = reader.GetSortedSetObservationPoints();
                // Объединяем наборы
                points.UnionWith(reposPoints);


                // Создаем путь к файлу для записи его в репозиторий
                fileNameWrite = Path.Combine(pathRepository, CreateFileName(stationId, points.Max.DateTime, points.Min.DateTime));

                Writer(fileNameWrite, points, metaDataRp5, schema);

                // Удаляем старый файл
                if (fileNameRepos != fileNameWrite)
                    File.Delete(fileNameRepos);
            }
            else
            {
                string pathRepos = reader.FileNameSource;
                fileNameWrite = Path.Combine(Repository, Path.GetFileName(pathRepos));
                if (reader.Encoding == Encoding.GetEncoding(1251))
                {
                    // Перемещаем файл в репозиторий
                    File.Move(pathRepos, fileNameWrite);
                }
                else
                {
                    Writer(fileNameWrite, points, metaDataRp5, schema);
                }
            }

            // Чистим за собой следы, если загружаемый файл является архивом
            if (reader.IsArchive)
                reader.DeleteFile();
        }

        private void Writer(string fileNameWrite, SortedSet<ObservationPoint> points, MetaDataRP5 metaDataRp5, string[] schema)
        {
            // Создаем список строк с метаданными
            List<string> listMeta = CreateMetaDataForWrite(metaDataRp5, points.Max.DateTime, points.Min.DateTime);
            // Создаем строку со схемой
            string strSchema = CreateLineSchema(schema);
            // Создаем список строк с данными
            List<string> list = ToListAllPoints(points, schema);
            // Уничтожаем точки
            points = null;
            // Объединяем списки для записи
            listMeta.Add(strSchema);
            listMeta.AddRange(list);
            // Записываем все в файл
            File.WriteAllLines(fileNameWrite, listMeta, encoding);
        }

        private List<string> CreateMetaDataForWrite(MetaDataRP5 meta, DateTime from, DateTime to)
        {
            List<string> list = new List<string>();
            list.Add($"# Метеостанция {meta.Station}, {meta.Country}, {meta.StringTypeSynoptic}={meta.Identificator}, " +
                     $"выборка с {from.Day:D2}.{from.Month:D2}.{from.Year} по {to.Day:D2}.{to.Month:D2}.{to.Year}, все дни");
            list.Add("# Кодировка: ANSI");
            list.Add("# Информация предоставлена сайтом \"Расписание Погоды\", rp5.ru");
            list.Add("# Пожалуйста, при использовании данных, любезно указывайте названный сайт.");
            list.Add($"# Обозначения метеопараметров см. по адресу {meta.Link}");
            list.Add("#");
            return list;
        }

        private string CreateLineSchema(string[] schema)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < schema.Length; i++)
            {
                sb.Append($"\"{schema[i]}\"");
                sb.Append(";");
            }

            return sb.ToString();
        }

        private List<string> ToListAllPoints(SortedSet<ObservationPoint> points, string[] schema)
        {
            List<string> list = new List<string>();
            HashSet<string> hash = new HashSet<string>();
            for (int i = 0; i < schema.Length; i++)
            {
                hash.Add(schema[i].ToUpper());
            }
            foreach (var p in points)
            {
                StringBuilder br = new StringBuilder();
                br.Append($"\"{p.DateTime.Day:D2}.{p.DateTime.Month:D2}.{p.DateTime.Year} {p.DateTime.Hour:D2}:{p.DateTime.Minute:D2}\"");
                br.Append(";");
                if (hash.Contains(nameof(p.T).ToUpper()))
                {
                    br.Append($"\"{p.T}\"");
                    br.Append(";");
                }
                if (hash.Contains("P0") | hash.Contains("PO"))
                {
                    br.Append($"\"{p.Po}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.P).ToUpper()))
                {
                    br.Append($"\"{p.P}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.PA).ToUpper()))
                {
                    br.Append($"\"{p.PA}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.U).ToUpper()))
                {
                    br.Append($"\"{p.U}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.DD).ToUpper()))
                {
                    br.Append($"\"{p.DD}\"");
                    br.Append(";");
                }

                if (hash.Contains(nameof(p.FF).ToUpper()))
                {
                    br.Append($"\"{p.FF}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.FF10).ToUpper()))
                {
                    br.Append($"\"{p.FF10}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.FF3).ToUpper()))
                {
                    br.Append($"\"{p.FF3}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.N).ToUpper()))
                {
                    br.Append($"\"{p.N}\"");
                    br.Append(";");
                }

                if (hash.Contains(nameof(p.WW).ToUpper()))
                {
                    br.Append($"\"{p.WW}\"");
                    br.Append(";");
                }
                if (hash.Contains("W'W'"))
                {
                    br.Append($"\"{p.W_W_}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.W1).ToUpper()))
                {
                    br.Append($"\"{p.W1}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.W2).ToUpper()))
                {
                    br.Append($"\"{p.W2}\"");
                    br.Append(";");
                }

                if (hash.Contains(nameof(p.Tn).ToUpper()))
                {
                    br.Append($"\"{p.Tn}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.Tx).ToUpper()))
                {
                    br.Append($"\"{p.Tx}\"");
                    br.Append(";");
                }

                if (hash.Contains(nameof(p.C).ToUpper()))
                {
                    br.Append($"\"{p.C}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.Cl).ToUpper()))
                {
                    br.Append($"\"{p.Cl}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.Nh).ToUpper()))
                {
                    br.Append($"\"{p.Nh}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.H).ToUpper()))
                {
                    br.Append($"\"{p.H}\"");
                    br.Append(";");
                }

                if (hash.Contains(nameof(p.Cm).ToUpper()))
                {
                    br.Append($"\"{p.Cm}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.Ch).ToUpper()))
                {
                    br.Append($"\"{p.Ch}\"");
                    br.Append(";");
                }

                if (hash.Contains(nameof(p.VV).ToUpper()))
                {
                    br.Append($"\"{p.VV}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.Td).ToUpper()))
                {
                    br.Append($"\"{p.Td}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.RRR).ToUpper()))
                {
                    br.Append($"\"{p.RRR}\"");
                    br.Append(";");
                }

                if (hash.Contains(nameof(p.TR).ToUpper()))
                {
                    br.Append($"\"{p.TR}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.E).ToUpper()))
                {
                    br.Append($"\"{p.E}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.Tg).ToUpper()))
                {
                    br.Append($"\"{p.Tg}\"");
                    br.Append(";");
                }

                if (hash.Contains("E'"))
                {
                    br.Append($"\"{p.Es}\"");
                    br.Append(";");
                }
                if (hash.Contains(nameof(p.SSS).ToUpper()))
                {
                    br.Append($"\"{p.SSS}\"");
                    br.Append(";");
                }

                list.Add(br.ToString());
            }

            return list;
        }

        /// <summary>
        /// Пакетное добавление и объединение файлов из папки в репозиторий
        /// </summary>
        /// <param name="pathFolderSource">Директория источника</param>
        public void PacketAddRepository(string pathFolderSource)
        {
            string[] fileName = Directory.GetFiles(pathFolderSource);
            for (int i = 0; i < fileName.Length; i++)
            {
                this.Add(fileName[i]);
            }
        }


        /// <summary>
        /// Пакетное добавление и объединение файлов из папки в репозиторий
        /// </summary>
        /// <param name="pathFolderSource">Директория источника</param>
        /// <param name="pathFolderRepository">Директория репозитория</param>
        public static void PacketAddRepository(string pathFolderSource, string pathFolderRepository)
        {
            string[] fileName = Directory.GetFiles(pathFolderSource);
            MergeRP5 repository = new MergeRP5(pathFolderRepository);
            for (int i = 0; i < fileName.Length; i++)
            {
                repository.Add(fileName[i]);
            }
        }

    }
}