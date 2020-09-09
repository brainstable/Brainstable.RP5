using NUnit.Framework;

namespace Brainstable.RP5.Test
{
    [TestFixture]
    public class TestReaderCsvAllDays
    {
        private const string DIR = @"D:\source\Repository\Brainstable.RP5\Brainstable.RP5.Test\Data";


        private string fileName = DIR + "\\29866.01.01.2020.05.09.2020.1.0.0.ru.ansi.00000000.csv";

        private ReaderRP5 reader;

        [SetUp]
        public void LoadFile()
        {
            reader = new ReaderRP5Csv();
            reader.Read(fileName);
        }

        [Test]
        public void TestReader()
        {
            Assert.IsTrue(reader.TypeLoadFileRp5 == TypeLoadFileRP5.Csv);
            Assert.IsTrue(reader.FileName == fileName);
            
        }

        [Test]
        public void TestMetaData()
        {
            MetaDataRP5 meta = reader.MetaDataRP5;
            Assert.IsTrue(meta.Station == "Минусинск");
            Assert.IsTrue(meta.InnerTypeFetch.Contains("все дни"));
        }

        
        [Test]
        public void TestCountData()
        {
            Assert.IsTrue(reader.StringArrayData.Length == 1986);
        }
    }
}
