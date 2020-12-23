using NUnit.Framework;

namespace Brainstable.RP5.Test
{
    [TestFixture]
    public class TestObservationPoint
    {
        private string line =
            "\"05.09.2020 04:00\";\"10.6\";\"736.3\";\"759.1\";\"1.8\";\"95\";\"Ветер, дующий с запада\";\"2\";\"\";\"\";\"100%.\";\"Ливневый(ые) дождь(и) умеренный(ые) или сильный(ые) в срок наблюдения или за последний час. \";\"Ливень(ливни).\";\"Облака покрывали более половины неба в течение всего соответствующего периода.\";\"\";\"\";\"Кучево-дождевые волокнистые (часто с наковальней), либо с кучево-дождевыми лысыми, кучевыми, слоистыми, разорванно-дождевыми, либо без них.\";\"100%.\";\"600-1000\";\"\";\"\";\"\";\"9.8\";\"\";\"\";\"\";\"\";\"\";\"\";";

        private string lineSchema =
            "\"Местное время в Минусинске\";\"T\";\"Po\";\"P\";\"Pa\";\"U\";\"DD\";\"Ff\";\"ff10\";\"ff3\";\"N\";\"WW\";\"W1\";\"W2\";\"Tn\";\"Tx\";\"Cl\";\"Nh\";\"H\";\"Cm\";\"Ch\";\"VV\";\"Td\";\"RRR\";\"tR\";\"E\";\"Tg\";\"E'\";\"sss\"";

        private ObservationPoint p;

        [SetUp]
        public void InitializeData()
        {
            p = ObservationPoint.CreateFromLine(line, SchemaRP5.CreateFromLineSchema(lineSchema));
        }

        [Test]
        public void TestDateTime()
        {
            Assert.IsTrue(p.Id == "05.09.2020 04:00");
        }

        [Test]
        public void TestT()
        {
            Assert.IsTrue(p.T == "10.6");
        }
    }
}