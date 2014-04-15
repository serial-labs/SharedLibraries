using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialLabs.Data;
using SerialLabs.UnitTestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SerialLabs.Tests.Types
{
    [TestClass]
    public class DescendingGuidTest
    {
        [TestMethod]
        public void CreateDescendingSortedGuid_WithSuccess()
        {
            DescendingSortedGuid idDesc = DescendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(idDesc);

            Console.WriteLine(idDesc.ToString());
        }

        [TestMethod]
        public void TryParseDescendingSortedGuid_WithSuccess()
        {
            AscendingSortedGuid guid;
            Assert.IsTrue(AscendingSortedGuid.TryParse("0635318522499400050_B77AD6F9624A4C2896E8545923E56502", out guid));
            Assert.AreEqual("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", guid.ToString());
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

            actual = expected.OrderByDescending(x => x.Id.ToString()).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);

            actual = expected.OrderByDescending(x => x.Id).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);
        }

        [TestMethod]
        public void DescendingSortedGuid_CompareTo_WithSuccess()
        {
            DescendingSortedGuid expected = DescendingSortedGuid.Empty;
            DescendingSortedGuid actual = new DescendingSortedGuid(DateTime.UtcNow, Guid.NewGuid());
            Assert.IsTrue(expected.CompareTo(actual) == 1);

            expected = new DescendingSortedGuid(DateTime.UtcNow.AddDays(1), Guid.NewGuid());
            actual = new DescendingSortedGuid(DateTime.UtcNow, Guid.NewGuid());
            Assert.IsTrue(expected.CompareTo(actual) == -1);

            expected = DescendingSortedGuid.Empty;
            actual = DescendingSortedGuid.Empty;
            Assert.IsTrue(expected.CompareTo(actual) == 0);
        }

        private static FakeItem<DescendingSortedGuid> NewItemDesc()
        {
            FakeItem<DescendingSortedGuid> item = new FakeItem<DescendingSortedGuid>();
            item.Id = DescendingSortedGuid.NewSortedGuid();
            item.Name = SerialLabs.Fakers.Name.FullName();
            return item;
        }

        private static void AssertCompare(DescendingSortedGuid expected, DescendingSortedGuid actual)
        {
            Assert.AreEqual(expected.Guid, actual.Guid);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp);
        }

        private static void AssertCompare(FakeItem<DescendingSortedGuid> expected, FakeItem<DescendingSortedGuid> actual)
        {
            AssertCompare(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
