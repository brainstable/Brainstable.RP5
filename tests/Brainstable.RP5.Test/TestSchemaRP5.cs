using NUnit.Framework;

namespace Brainstable.RP5.Test
{
    [TestFixture]
    public class TestSchemaRP5
    {
        private string line = "\"Местное время в Минусинске\";\"T\";\"Po\";\"P\";\"Pa\";\"U\";\"DD\";\"Ff\";\"ff10\";\"ff3\";\"N\";\"WW\";\"W1\";\"W2\";\"Tn\";\"Tx\";\"Cl\";\"Nh\";\"H\";\"Cm\";\"Ch\";\"VV\";\"Td\";\"RRR\";\"tR\";\"E\";\"Tg\";\"E'\";\"sss\"";
        private SchemaRP5 schema;
        
        [SetUp]
        public void CreateSchema()
        {
            schema = SchemaRP5.CreateFromLineSchema(line);
        }

        [Test]
        public void TestCountFields()
        {
            Assert.IsTrue(schema.CountFields == 29);
        }

        [Test]
        public void TestNameByIndex()
        {
            Assert.IsTrue(schema.GetNameByIndex(28) == "SSS");
        }

        [Test]
        public void TestIndexatorNameByIndex()
        {
            Assert.IsTrue(schema[28] == "SSS");
        }

        [Test]
        public void TestOriginalNameByIndex()
        {
            Assert.IsTrue(schema.GetOriginalNameByIndex(28) == "sss");
        }

        [Test]
        public void TestFirstNameField()
        {
            Assert.IsTrue(schema.NameFirstField == "Местное время в Минусинске");
        }

        [Test]
        public void TestIndexByName()
        {
            Assert.IsTrue(schema.GetIndexByName("Po") == 2);
            Assert.IsTrue(schema.GetIndexByName("PO") == 2);
        }

        [Test]
        public void TestIndexatorIndexByName()
        {
            Assert.IsTrue(schema["Po"] == 2);
            Assert.IsTrue(schema["PO"] == 2);
        }

        [Test]
        public void TestDescriptionByName()
        {
            Assert.IsTrue(schema.GetDescriptionByName("Po") == SchemaRP5.DescriptionFields["Po"]);
            Assert.IsTrue(schema.GetDescriptionByName("Po") == SchemaRP5.DescriptionFields["PO"]);
        }

        [Test]
        public void TestDescriptionByIndex()
        {
            Assert.IsTrue(schema.GetDescriptionByIndex(2) == SchemaRP5.DescriptionFields["Po"]);
            Assert.IsTrue(schema.GetDescriptionByIndex(2) == SchemaRP5.DescriptionFields["PO"]);
        }
    }
}