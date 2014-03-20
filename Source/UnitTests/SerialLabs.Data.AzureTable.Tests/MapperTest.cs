using Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void DtoToDomainTest()
        {
            Mapper.Person expectedPerson = new Mapper.Person();
            Mapper<Person> mapper = new PersonMapper();
            DynamicEntity dto = Helper.GeneratePersonDynamicEntity(expectedPerson);
            Mapper.Person p = mapper.DtoToDomain(dto);
            Assert.AreEqual(expectedPerson, p);

        }
    }
}
