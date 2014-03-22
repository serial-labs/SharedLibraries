using System;

namespace SerialLabs.Data.AzureTable.Tests
{
    /// <summary>
    ///  This class helps for generating values for tests
    /// </summary>
    internal static class Helper
    {
        public const string StorageConnectionString = "UseDevelopmentStorage=true;";
        public const string ProxiedStorageConnectionString = "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://ipv4.fiddler";


        public static string RandomPartitionKey
        {
            get { return Guid.NewGuid().ToString(); }
        }

        public static string RandomRowKey
        {
            get { return Guid.NewGuid().ToString(); }
        }

        public static string DailyTableName
        {
            get { return "Table" + DateTime.UtcNow.ToString("yyyyMMdd"); }
        }

        public static FakePerson CreateFakePerson()
        {
            return CreateFakePerson(Guid.NewGuid().ToString());
        }
        public static FakePerson CreateFakePerson(string id)
        {
            return new FakePerson
            {
                Id = id,
                FirstName = Fakers.Name.FirstName(),
                LastName = Fakers.Name.LastName(),
                Number = RandomNumberGenerator.Int(Int32.MaxValue),
                Date = DateTime.UtcNow,
                Address = new FakeAddress()
            };
        }

        public static DynamicEntity CreateFakeDynamicPerson(string partitionKey, string rowKey)
        {
            FakePerson person = CreateFakePerson(rowKey);
            return CreateFakeDynamicPerson(partitionKey, person);
        }
        public static DynamicEntity CreateFakeDynamicPerson(string partitionKey, FakePerson person)
        {
            DynamicEntity dynEnt = new DynamicEntity(partitionKey, person.Id);
            dynEnt.Set("FirstName", person.FirstName);
            dynEnt.Set("LastName", person.LastName);
            dynEnt.Set("Date", person.Date);
            dynEnt.Set("Number", person.Number);
            dynEnt.Set("Address", person.Address);
            return dynEnt;
        }
    }
}
