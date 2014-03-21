using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    /// <summary>
    ///  This class helps for generating values for tests
    /// </summary>
    public class Helper
    {
        //const string ConnectionString = "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://ipv4.fiddler";
        public const string ConnectionStringForTest = "UseDevelopmentStorage=true";
        public const string partitionKey = "partitionKeyForTest";

        /// <summary>
        ///  Generate a random table name in the correct format
        /// </summary>
        public static string NewTableName()
        {
            string res = Guid.NewGuid().ToString().Replace("-", "");
            if (char.IsNumber(res[0]))
            {
                res = res.Remove(0, 1);
                res = res.Insert(0, "a");
            }
            return res;
        }

        public static DynamicEntity GeneratePersonDynamicEntity()
        {
            Person p = new Person();
            DynamicEntity dynEnt = new DynamicEntity(partitionKey, p.Id);
            dynEnt.Set("FirstName", p.FirstName);
            dynEnt.Set("LastName", p.LastName);
            dynEnt.Set("Date", p.Date);
            dynEnt.Set("Number", p.Number);
            dynEnt.Set("Adr", p.Adr);
            return dynEnt;
        }
        public static DynamicEntity GeneratePersonDynamicEntity(Mapper.Person p)
        {
            DynamicEntity dynEnt = new DynamicEntity(partitionKey, p.Id);
            dynEnt.Set("Id", p.Id);
            dynEnt.Set("FirstName", p.FirstName);
            dynEnt.Set("LastName", p.LastName);
            dynEnt.Set("Date", p.Date);
            dynEnt.Set("Number", p.Number);
            dynEnt.Set("Adr", p.Adr);
            return dynEnt;
        }

        // Example Entities
        public class Person
        {
            public static string defaultId = "1";
            public Person()
            {
                Id = defaultId;
                FirstName = "John";
                LastName = "Doe";
                Number = 123;
                Date = DateTime.Parse("01/01/2014");
                Adr = new Address();

            }
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Number { get; set; }
            public DateTime? Date { get; set; }
            public Address Adr { get; set; }

            public override bool Equals(Object obj)
            {

                if (obj == null) { return false; }
                if (!(obj is Person)) { return false; }

                Person p2 = (Person)obj;

                if (!Id.Equals(p2.Id)) { return false; }
                if (!FirstName.Equals(p2.FirstName)) { return false; }
                if (!LastName.Equals(p2.LastName)) { return false; }
                if (!Number.Equals(p2.Number)) { return false; }
                if (!Date.Equals(p2.Date)) { return false; }
                if (!Adr.Number.Equals(p2.Adr.Number)) { return false; }
                if (!Adr.Street.Equals(p2.Adr.Street)) { return false; }


                return true;
            }
        }

        public class Address
        {
            public Address()
            {
                Number = 10;
                Street = "abc";
            }
            public int Number { get; set; }
            public string Street { get; set; }

        }

        // Dynamic Entity Compare

        public static bool AssertCompare(DynamicEntity dE1, DynamicEntity dE2)
        {
            FakeDynamicEntity e1 = new FakeDynamicEntity(dE1);
            FakeDynamicEntity e2 = new FakeDynamicEntity(dE2);
            
            if (e1 == null || e2 == null) { return false; }


            if (e1.getProperties().Count != e2.getProperties().Count) { return false; }

            foreach (var pair in e1.getProperties())
            {
                if (!e2.getProperties()[pair.Key].Equals(pair.Value))
                {
                    return false;
                }
            }

            return true;
        }

        public class FakeDynamicEntity : DynamicEntity
        {
            public FakeDynamicEntity(DynamicEntity e):base(e)
            {
                
            }
            public IDictionary<string,EntityProperty> getProperties()
            {
                return _properties;
            }
        }

    }
}
