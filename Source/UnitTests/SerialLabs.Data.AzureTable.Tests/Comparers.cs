using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialLabs.UnitTestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    static class Comparers
    {
        public static void AssertCompare(DynamicEntity expected, DynamicEntity actual)
        {
            CommonComparers.CheckNullReferences(expected, actual);

            if (expected == null) { return; }

            FakeDynamicEntity fakedExpected = new FakeDynamicEntity(expected);
            FakeDynamicEntity fakedActual = new FakeDynamicEntity(actual);

            if (fakedExpected.getProperties().Count != fakedActual.getProperties().Count) { Assert.Fail(); }

            foreach (var pair in fakedExpected.getProperties())
            {
                if (!fakedActual.getProperties()[pair.Key].Equals(pair.Value))
                {
                    Assert.Fail();
                }
            }
        }

        public static void AssertCompare(FakePerson expected, FakePerson actual)
        {
            CommonComparers.CheckNullReferences(expected, actual);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Number, actual.Number);
            CommonComparers.AreSimilar(expected.Date, actual.Date);
        }
    }
}
