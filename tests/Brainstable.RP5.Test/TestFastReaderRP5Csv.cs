using System;
using NUnit.Framework;

namespace Brainstable.RP5.Test
{
    [TestFixture]
    public class TestFastReaderRP5Csv
    {
        private string fileNameAllDays;

        [SetUp]
        public void Initialize()
        {
            fileNameAllDays = Environment.CurrentDirectory + "\\Data\\29866.01.01.2020.05.09.2020.1.0.0.ru.ansi.00000000.csv";
        }

        [Test]
        public void TestGetMetaData()
        {
            MetaDataRP5 md = FastReaderRP5.ReadMetaDataFromCsv(fileNameAllDays);
            Assert.IsTrue(md.Station.Contains("Минусинск"));
            Assert.IsTrue(md.InnerTypeFetch.Contains("все дни"));
            Assert.IsTrue(md.Identificator == "29866");
        }

        [Test]
        public void TestSchemaCountColumns()
        {
            SchemaRP5 schema = FastReaderRP5.ReadSchemaFromCsv(fileNameAllDays);
            Assert.IsTrue(schema.CountFields == 29);
        }

        [Test]
        public void TestSchemaFirstColumn()
        {
            SchemaRP5 schema = FastReaderRP5.ReadSchemaFromCsv(fileNameAllDays);
            Assert.IsTrue(schema.NameFirstField == "Местное время в Минусинске");
        }

        [Test]
        public void TestSchemaLastColumn()
        {
            SchemaRP5 schema = FastReaderRP5.ReadSchemaFromCsv(fileNameAllDays);
            Assert.IsTrue(schema.Schema[schema.Schema.Length - 1] == "sss");
        }
    }
}