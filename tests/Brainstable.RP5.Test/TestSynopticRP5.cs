using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Brainstable.RP5.Test
{
    [TestFixture()]
    public class TestSynopticRP5
    {
        string line = "# Метеостанция Минусинск, Россия, WMO_ID=29866, выборка с 01.01.2020 по 05.09.2020, все дни";
        private SynopticRP5 synoptic;

        [SetUp]
        public void InitializeData()
        {
            synoptic = SynopticRP5.CreateFromLine(line);
            ;
        }

        [Test]
        public void TestLocation()
        {
            Assert.IsTrue(synoptic.Station == "Минусинск");
            Assert.IsTrue(synoptic.Country == "Россия");
        }

        [Test]
        public void TestIdentificator()
        {
            Assert.IsTrue(synoptic.Identificator == "29866");
        }

        [Test]
        public void TestTypeSynoptic()
        {
            Assert.IsTrue(synoptic.TypeSynopticRp5 == TypeSynopticRP5.WMO_ID);
            Assert.IsTrue(synoptic.StringTypeSynoptic == "WMO_ID");
        }

        [Test]
        public void TestDefault()
        {
            SynopticRP5 syn = new SynopticRP5();
            Assert.IsTrue(syn.TypeSynopticRp5 == TypeSynopticRP5.Unknown);
            Assert.IsTrue(syn.StringTypeSynoptic == "Unknown");
        }

    }
}
