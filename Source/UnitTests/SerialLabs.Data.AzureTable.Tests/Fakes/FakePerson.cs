using System;

namespace SerialLabs.Data.AzureTable.Tests
{
    class FakePerson
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public DateTime? Date { get; set; }
        public FakeAddress Address { get; set; }
    }
}
