using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace SerialLabs.Data.AzureTable.Tests
{
    class FakeDynamicEntity : DynamicEntity
    {
        public FakeDynamicEntity(DynamicEntity e)
            : base(e)
        {

        }
        public IDictionary<string, EntityProperty> getProperties()
        {
            return _properties;
        }
    }
}
