using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class CompositeIdTest
    {
        [TestMethod]
        public void CompositeIdTest_New()
        {
            CompositeId idAsc = CompositeIdAsc.NewCompositeId();
            Assert.IsNotNull(idAsc);

            Console.WriteLine(idAsc.ToString());

            CompositeId idDesc = CompositeIdDesc.NewCompositeId();
            Assert.IsNotNull(idDesc);

            Console.WriteLine(idDesc.ToString());
        }


        [TestMethod]
        public void CompositeIdAsc_Parse()
        {
            CompositeId expected = CompositeIdAsc.NewCompositeId();

            CompositeId actual = CompositeIdAsc.Parse(expected.ToString());

            AssertCompare(expected, actual);
            
        }

        [TestMethod]
        public void CompositeIdDesc_Parse()
        {
            CompositeId expected = CompositeIdDesc.NewCompositeId();

            CompositeId actual = CompositeIdDesc.Parse(expected.ToString());

            AssertCompare(expected, actual);

        }

        [TestMethod]
        public void CompositeIdDesc_Order()
        {
            List<FakeItem<CompositeIdDesc>> expected = new List<FakeItem<CompositeIdDesc>>();



            for (int i = 0; i < 3; i++)
            {
                var item = NewItemDesc();
                expected.Add(item);                
                Thread.Sleep(10); // slowing down                
            }

            List<FakeItem<CompositeIdDesc>> actual = new List<FakeItem<CompositeIdDesc>>();

            actual = expected.OrderBy(x => x.Id.ToString()).ToList();

            AssertCompare(expected.First(), actual.Last());

        }

        [TestMethod]
        public void CompositeIdAsc_Order()
        {
            List<FakeItem<CompositeIdAsc>> expected = new List<FakeItem<CompositeIdAsc>>();



            for (int i = 0; i < 3; i++)
            {
                var item = NewItemAsc();
                expected.Add(item);
                Thread.Sleep(10); // slowing down                
            }

            List<FakeItem<CompositeIdAsc>> actual = new List<FakeItem<CompositeIdAsc>>();

            actual = expected.OrderBy(x => x.Id.ToString()).ToList();
            
            AssertCompare(expected.First(),actual.First());

        }



        private static void AssertCompare(CompositeId expected, CompositeId actual)
        {
            Assert.AreEqual(expected.GuId, actual.GuId);
            Assert.AreEqual(expected.DateUtc, actual.DateUtc);
        }

        private static void AssertCompare(FakeItem<CompositeIdDesc> expected, FakeItem<CompositeIdDesc> actual)
        {
            AssertCompare(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        private static void AssertCompare(FakeItem<CompositeIdAsc> expected, FakeItem<CompositeIdAsc> actual)
        {
            AssertCompare(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        class FakeItem<IdType>
        {
            public IdType Id { get; set; }
            public string Name { get; set; }
        }

        private FakeItem<CompositeIdDesc> NewItemDesc()
        {
            FakeItem<CompositeIdDesc> item = new FakeItem<CompositeIdDesc>();
            item.Id = CompositeIdDesc.NewCompositeId();
            item.Name = " name ";

            return item;
        }

        private FakeItem<CompositeIdAsc> NewItemAsc()
        {
            FakeItem<CompositeIdAsc> item = new FakeItem<CompositeIdAsc>();
            item.Id = CompositeIdAsc.NewCompositeId();
            item.Name = " name ";

            return item;
        }
    }
}
