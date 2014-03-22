using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class TableRepositoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableRepositoryInstance_WithoutConfiguration_WithException()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(null);
        }

        [TestMethod]
        public void CreateTableRepositoryInstance_WithDefaultConfiguration_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
        }

        [TestMethod]
        public void InsertEntity_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            repository.Insert(new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public async Task InsertEntity_Async_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            await repository.InsertAsync(new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void UpdateEntity_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            TableEntity entity = new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            repository.Insert(entity);
            repository.Update(entity);
        }

        [TestMethod]
        public async Task UpdateEntity_Async_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            TableEntity entity = new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await repository.InsertAsync(entity);
            await repository.UpdateAsync(entity);
        }

        [TestMethod]
        public void DeleteEntity_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            TableEntity entity = new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            repository.Insert(entity);
            repository.Delete(entity);
        }

        [TestMethod]
        public void DeleteEntity_WithDetails_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            TableEntity entity = new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            repository.Insert(entity);
            repository.Delete(entity.PartitionKey, entity.RowKey);
        }

        [TestMethod]
        public async Task DeleteEntity_Async_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            TableEntity entity = new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await repository.InsertAsync(entity);
            await repository.DeleteAsync(entity);
        }

        [TestMethod]
        public async Task DeleteEntity_Async_WithDetails_WithSuccess()
        {
            TableRepository<TableEntity> repository = new TableRepository<TableEntity>(
                TableStorageConfiguration.CreateDefault(
                    Helper.DailyTableName,
                    Helper.StorageConnectionString));
            TableEntity entity = new TableEntity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await repository.InsertAsync(entity);
            await repository.DeleteAsync(entity.PartitionKey, entity.RowKey);
        }
    }
}
