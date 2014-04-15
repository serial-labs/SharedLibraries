using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialLabs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SerialLabs.Tests.Domain
{
    [TestClass]
    public class SortedGuidTest
    {
        [TestMethod]
        public void CreateSortedGuid_WithSuccess()
        {
            AscendingSortedGuid idAsc = AscendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(idAsc);

            Console.WriteLine(idAsc.ToString());

            DescendingSortedGuid idDesc = DescendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(idDesc);

            Console.WriteLine(idDesc.ToString());
        }

        [TestMethod]
        public void TryParseAscendingGuid_WithSuccess()
        {
            AscendingSortedGuid guid;
            Assert.IsTrue(AscendingSortedGuid.TryParse("0635318522499400050_B77AD6F9624A4C2896E8545923E56502", out guid));
            Assert.AreEqual("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", guid.ToString());
        }
        [TestMethod]
        public void TryParseDescendingGuid_WithSuccess()
        {
            AscendingSortedGuid guid;
            Assert.IsTrue(AscendingSortedGuid.TryParse("0635318522499400050_B77AD6F9624A4C2896E8545923E56502", out guid));
            Assert.AreEqual("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", guid.ToString());
        }

        [TestMethod]
        public void ParseAscendingSortedGuid_WithSuccess()
        {
            AscendingSortedGuid expected = AscendingSortedGuid.NewSortedGuid();
            AscendingSortedGuid actual = AscendingSortedGuid.Parse(expected.ToString());

            AssertCompare(expected, actual);
        }

        [TestMethod]
        public void ParseDescendingSortedGuid_WithSuccess()
        {
            DescendingSortedGuid expected = DescendingSortedGuid.NewSortedGuid();
            DescendingSortedGuid actual = DescendingSortedGuid.Parse(expected.ToString());

            AssertCompare(expected, actual);

        }

        [TestMethod]
        public void DescendingSortedGuid_Order_WithSuccess()
        {
            List<FakeItem<DescendingSortedGuid>> expected = new List<FakeItem<DescendingSortedGuid>>();

            for (int i = 0; i < 3; i++)
            {
                var item = NewItemDesc();
                expected.Add(item);
                Thread.Sleep(10); // slowing down
            }

            List<FakeItem<DescendingSortedGuid>> actual = new List<FakeItem<DescendingSortedGuid>>();

            actual = expected.OrderBy(x => x.Id.ToString()).ToList();

            /* not complete */

        }

        [TestMethod]
        public void AscendingSortedGuid_Order_WithSuccess()
        {
            List<FakeItem<AscendingSortedGuid>> expected = new List<FakeItem<AscendingSortedGuid>>();

            for (int i = 0; i < 3; i++)
            {
                var item = NewItemAsc();
                expected.Add(item);
                Thread.Sleep(10); // slowing down                
            }

            List<FakeItem<AscendingSortedGuid>> actual = new List<FakeItem<AscendingSortedGuid>>();

            actual = expected.OrderBy(x => x.Id.ToString()).ToList();

            /* not complete */

        }


        private static void AssertCompare(DescendingSortedGuid expected, DescendingSortedGuid actual)
        {
            Assert.AreEqual(expected.Guid, actual.Guid);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp);
        }


        private static void AssertCompare(AscendingSortedGuid expected, AscendingSortedGuid actual)
        {
            Assert.AreEqual(expected.Guid, actual.Guid);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp);
        }

        class FakeItem<IdType>
        {
            public IdType Id { get; set; }
            public string Name { get; set; }
        }

        private FakeItem<DescendingSortedGuid> NewItemDesc()
        {
            FakeItem<DescendingSortedGuid> item = new FakeItem<DescendingSortedGuid>();
            item.Id = DescendingSortedGuid.NewSortedGuid();
            item.Name = " name ";

            return item;
        }

        private FakeItem<AscendingSortedGuid> NewItemAsc()
        {
            FakeItem<AscendingSortedGuid> item = new FakeItem<AscendingSortedGuid>();
            item.Id = AscendingSortedGuid.NewSortedGuid();
            item.Name = " name ";

            return item;
        }
    }
}
