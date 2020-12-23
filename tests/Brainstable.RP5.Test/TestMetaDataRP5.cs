using System;
using NUnit.Framework;

namespace Brainstable.RP5.Test
{
    [TestFixture]
    public class TestMetaDataRP5
    {
        private MetaDataRP5 meta;

        [SetUp]
        public void InitializeData()
        {
            string[] arr = new string[5];
            arr[0] = "# Метеостанция Минусинск, Россия, WMO_ID=29866, выборка с 01.01.2020 по 05.09.2020, все дни";
            arr[1] = "# Кодировка: ANSI";
            arr[2] = "# Информация предоставлена сайтом \"Расписание Погоды\", rp5.ru";
            arr[3] = "# Пожалуйста, при использовании данных, любезно указывайте названный сайт.";
            arr[4] = "# Обозначения метеопараметров см. по адресу http://rp5.ru/archive.php?wmo_id=29866&lang=ru";

            meta = MetaDataRP5.CreateFromArrayString(arr);
        }

        [Test]
        public void TestLocaleMeteostantion()
        {
            Assert.IsTrue(meta.Synoptic.Station == "Минусинск");
            Assert.IsTrue(meta.Synoptic.Country == "Россия");
        }

        [Test]
        public void TestInnerLocaleMeteostantion()
        {
            Assert.IsTrue(meta.InnerStation == "Метеостанция Минусинск");
            Assert.IsTrue(meta.InnerCountry == "Россия");
        }

        [Test]
        public void TestEncoding()
        {
            Assert.IsTrue(meta.InnerEncoding == "Кодировка: ANSI");
            Assert.IsTrue(meta.Encoding == "ANSI");
        }

        [Test]
        public void TestTypeFetch()
        {
            Assert.IsTrue(meta.InnerTypeFetch == "все дни");
            Assert.IsTrue(meta.TypeFetchRp5 == TypeFetchRP5.AllDays);
        }

        [Test]
        public void TestFetch()
        {
            Assert.IsTrue(meta.InnerFetch == "выборка с 01.01.2020 по 05.09.2020");
            Assert.IsTrue(meta.StartFetch == Convert.ToDateTime("01.01.2020"));
            Assert.IsTrue(meta.EndFetch == Convert.ToDateTime("05.09.2020"));
        }
    }
}
