using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Brainstable.RP5.Test
{
    [TestFixture]
    public class TestMerger
    {
        private string fileName1, fileName2;

        private string dir =
            "D:\\source\\BrainstableRepos\\Brainstable.RP5\\tests\\Brainstable.RP5.Test\\bin\\Debug\\Merge";

        [SetUp]
        public void Initialize()
        {
            fileName1 = Path.Combine(dir, "29766.01.12.2020.17.12.2020.1.0.0.ru.ansi.00000000.csv.gz");
            fileName2 = Path.Combine(dir, "29766.10.12.2020.24.12.2020.1.0.0.ru.utf8.00000000.csv.gz");
        }

        [Test]
        public void TestOutFileName()
        {
            Merger merger = new Merger();
            string outFileName = merger.Join(fileName1, fileName2);
            Assert.IsTrue(Path.GetFileName(outFileName) == "29766.01.12.2020.24.12.2020.1.0.0.ru.ansi.00000000.csv");
        }
    }
}
