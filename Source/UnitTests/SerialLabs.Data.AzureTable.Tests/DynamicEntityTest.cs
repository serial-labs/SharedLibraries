using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class DynamicEntityTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDynamicEntity_WithDate_NoPartition()
        {
            DynamicEntity de = new DynamicEntity("", DateTime.Now);
        }


        [TestMethod]
        public void CreateDynamicEntity_WithRow_Success()
        {
            DynamicEntity de = new DynamicEntity("aa", "aa");
            Assert.IsNotNull(de);
        }

        [TestMethod]
        public void CreateDynamicEntity_WithDate_Success()
        {
            DynamicEntity de = new DynamicEntity("aa", DateTime.Now);
            Assert.IsNotNull(de);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDynamicEntity_WithRow_NoPartition()
        {
            DynamicEntity de = new DynamicEntity("", "aa");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDynamicEntity_NoRow()
        {
            DynamicEntity de = new DynamicEntity("aa", "");
        }

        [TestMethod]

        public void DynamicEntity_Equal()
        {
            DynamicEntity entity1 = Helper.GeneratePersonDynamicEntity();
            DynamicEntity entity2 = Helper.GeneratePersonDynamicEntity();
            Assert.IsTrue(Helper.AssertCompare(entity1, entity2));
        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_String()
        {
            DynamicEntity testEntity = new DynamicEntity();
            string actual, expected = "hatem";

            testEntity.Set("String", expected);
            //testEntity.Get("String", out actual);
            actual = testEntity.Get<string>("String");

            Assert.AreEqual(actual, expected);

         /*   expected += "++";
            testEntity.Set("String", expected);
            testEntity.Get("String", out actual);

            Assert.AreEqual(actual, expected);*/

        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_Int()
        {
            DynamicEntity testEntity = new DynamicEntity();
            int actual, expected = 123;

            testEntity.Set("Int", expected);
            actual = testEntity.Get<int>("Int");

            Assert.AreEqual(actual, expected);

            expected++;
            testEntity.Set("Int", expected);
            actual = testEntity.Get<int>("Int");

            Assert.AreEqual(actual, expected);

        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_Long()
        {
            DynamicEntity testEntity = new DynamicEntity();
            long actual, expected = 123;

            testEntity.Set("Long", expected);
            actual = testEntity.Get<long>("Long");

            Assert.AreEqual(actual, expected);

            /*expected++;
            testEntity.Set("Long", expected);
            actual = testEntity.Get<long>("Long");

            Assert.AreEqual(actual, expected);*/

        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_Bool()
        {
            DynamicEntity testEntity = new DynamicEntity();
            bool actual, expected = true;

            testEntity.Set("Bool", expected);
            actual=testEntity.Get<bool>("Bool");

            Assert.AreEqual(actual, expected);

          /*  expected = false;
            testEntity.Set("Bool", expected);
            testEntity.Get("Bool", out actual);

            Assert.AreEqual(actual, expected);*/

        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_Byte()
        {
            DynamicEntity testEntity = new DynamicEntity();
            byte[] actual, expected = new byte[10];

            testEntity.Set("Byte", expected);
            actual=testEntity.Get<Byte[]>("Byte");

            Assert.AreEqual(actual, expected);

            /*expected = new byte[5];
            testEntity.Set("Byte", expected);
            testEntity.Get("Byte", out actual);

            Assert.AreEqual(actual, expected);
            */
        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_DateTime()
        {
            DynamicEntity testEntity = new DynamicEntity();
            DateTime actual, expected = DateTime.Today;

            testEntity.Set("DateTime", expected);
            actual=testEntity.Get<DateTime>("DateTime");

            Assert.AreEqual(actual, expected);
            
            expected.AddDays(1);
            testEntity.Set("DateTime", expected);
            actual=testEntity.Get<DateTime>("DateTime");

            Assert.AreEqual(actual, expected);

            // DateTime?

            DynamicEntity testEntity0 = new DynamicEntity();
            DateTime? actual0, expected0 = DateTime.Today;

            testEntity0.Set("DateTime", expected0);
            actual0=testEntity0.Get<DateTime?>("DateTime");

            Assert.AreEqual(actual0, expected0);

            expected0.Value.AddDays(1);
            testEntity0.Set("DateTime", expected0);
            actual0=testEntity0.Get<DateTime?>("DateTime");

            Assert.AreEqual(actual0, expected0);
            
        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_DateTimeOffset()
        {
            DynamicEntity testEntity = new DynamicEntity();
            DateTimeOffset actual, expected = DateTime.Today;

            testEntity.Set("DateTimeOffset", expected);
            actual=testEntity.Get<DateTimeOffset>("DateTimeOffset");

            Assert.AreEqual(actual, expected);

            expected.AddDays(1);
            testEntity.Set("DateTimeOffset", expected);
            actual=testEntity.Get<DateTimeOffset>("DateTimeOffset");

            Assert.AreEqual(actual, expected);

            // DateTimeOffset?

            DynamicEntity testEntity0 = new DynamicEntity();
            DateTimeOffset? actual0, expected0 = DateTime.Today;

            testEntity0.Set("DateTimeOffset", expected0);
            actual0=testEntity0.Get<DateTimeOffset?>("DateTimeOffset");

            Assert.AreEqual(actual0, expected0);

            expected0.Value.AddDays(1);
            testEntity0.Set("DateTimeOffset", expected0);
            actual0=testEntity0.Get<DateTimeOffset?>("DateTimeOffset");

            Assert.AreEqual(actual0, expected0);
            
        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_Double()
        {
            DynamicEntity testEntity = new DynamicEntity();
            double actual, expected = 0.1;

            testEntity.Set("Double", expected);
            actual=testEntity.Get<double>("Double");

            Assert.AreEqual(actual, expected);
            /*
            expected += 1;
            testEntity.Set("Double", expected);
            testEntity.Get("Double", out actual);

            Assert.AreEqual(actual, expected);
            */
        }

        [TestMethod]
        public void DynamicEntity_AddGetSet_Guid()
        {
            DynamicEntity testEntity = new DynamicEntity();
            Guid actual, expected = Guid.NewGuid();

            testEntity.Set("Guid", expected);
            actual=testEntity.Get<Guid>("Guid");

            Assert.AreEqual(actual, expected);
            /*
            expected = Guid.NewGuid();
            testEntity.Set("Guid", expected);
            testEntity.Get("Guid", out actual);

            Assert.AreEqual(actual, expected);
            */
        }


        [TestMethod]
        public void DynamicEntity_AddGetSet_Complex()
        {
            DynamicEntity testEntity = new DynamicEntity();
            Helper.Person actual, expected = new Helper.Person();

            testEntity.Set("Complex", expected);
            actual = testEntity.Get<Helper.Person>("Complex");

            Assert.AreEqual(actual, expected);

            expected.Number = 654;
            testEntity.Set("Complex", expected);
            actual = testEntity.Get<Helper.Person>("Complex");

            Assert.AreEqual(actual, expected);

        }
    }
}
