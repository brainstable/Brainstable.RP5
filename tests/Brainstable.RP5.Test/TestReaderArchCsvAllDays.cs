using System;
using NUnit.Framework;

namespace Brainstable.RP5.Test
{
    [TestFixture]
    public class TestReaderArchCsvAllDays
    {
        private string fileName;
        private ReaderRP5 reader;

        [SetUp]
        public void LoadFile()
        {
            fileName = "D:\\source\\BrainstableRepos\\Brainstable.RP5\\tests\\Brainstable.RP5.Test\\bin\\Debug\\Data\\29866.01.01.2020.05.09.2020.1.0.0.ru.ansi.00000000.csv.gz";
            reader = new ReaderRP5();
        }

        [Test]
        public void TestMetaData()
        {
            reader.ReadWithoutData(fileName);
            MetaDataRP5 meta = reader.MetaData;
            Assert.IsTrue(meta.Station == "Минусинск");
            Assert.IsTrue(meta.InnerTypeFetch.Contains("все дни"));
        }
        
        [Test]
        public void TestCountData()
        {
            var list = reader.ReadToListString(fileName);
            int count = list.Count;
            Assert.IsTrue(count == 1986);
        }
    }
}