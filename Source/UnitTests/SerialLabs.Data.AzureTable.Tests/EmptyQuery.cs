using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    class EmptyQuery : ITableStorageQuery<TableEntity>
    {

        public Task<ICollection<TableEntity>> Execute(CloudTable table)
        {
            return Task.FromResult<ICollection<TableEntity>>(new List<TableEntity>());
        }

        public string UniqueIdentifier
        {
            get { return "EmptyQuery"; }
        }
    }
}
