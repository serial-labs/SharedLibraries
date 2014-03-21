using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SerialLabs.Data.AzureTable
{
    public class DynamicEntity : TableEntity
    {
        private IDictionary<string, EntityProperty> _properties = new Dictionary<string, EntityProperty>();
        private IDictionary<string, Type> _knownTypes = new Dictionary<string, Type>();

        public DynamicEntity(string partitionKey, DateTime dateTime)
        {
            Guard.ArgumentNotNullOrWhiteSpace(partitionKey, "partitionKey");
            if (dateTime == null)
                throw new ArgumentException();

            PartitionKey = partitionKey;//.ToUpperInvariant();
            // Descending order - Newest first
            RowKey = String.Format(CultureInfo.InvariantCulture, "{0}-{1}",
                DateTime.MaxValue.Subtract(dateTime).TotalMilliseconds.ToString(CultureInfo.InvariantCulture),
                Guid.NewGuid());
        }
        public DynamicEntity(string partitionKey, string rowKey)
        {
            Guard.ArgumentNotNullOrWhiteSpace(partitionKey, "partitionKey");
            Guard.ArgumentNotNullOrWhiteSpace(rowKey, "rowKey");

            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        public DynamicEntity()
        { }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            _properties = properties;
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return _properties;
        }
        public void Set(string key, EntityProperty value)
        {
            _properties[key] = value;
        }

        public void Get(string key, out EntityProperty value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = prop;
        }

        public void Set(string key, bool value)
        {
            _properties[key] = new EntityProperty(value);
        }

        public void Get(string key, out bool value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = (bool)prop.BooleanValue;
        }

        public void Set(string key, byte[] value)
        {
            _properties[key] = new EntityProperty(value);
        }

        public void Get(string key, out byte[] value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = prop.BinaryValue;
        }

        public void Set(string key, DateTime? value)
        {
            Set(key, value.Value);
        }

        public void Get(string key, out DateTime? value)
        {
            DateTime val;
            Get(key, out val);
            value = val;
        }

        public void Set(string key, DateTime value)
        {
            _properties[key] = new EntityProperty(value);
        }

        public void Get(string key, out DateTime value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = prop.DateTimeOffsetValue.Value.LocalDateTime;
        }

        public void Set(string key, DateTimeOffset? value)
        {
            Set(key, value.Value);
        }
        public void Get(string key, out DateTimeOffset? value)
        {
            DateTimeOffset val;
            Get(key, out val);
            value = val;
        }

        public void Set(string key, DateTimeOffset value)
        {
            _properties[key] = new EntityProperty(value);
        }
        public void Get(string key, out DateTimeOffset value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = (DateTimeOffset)prop.DateTimeOffsetValue;
        }

        public void Set(string key, double value)
        {
            _properties[key] = new EntityProperty(value);
        }
        public void Get(string key, out double value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = (double)prop.DoubleValue;
        }

        public void Set(string key, Guid value)
        {
            _properties[key] = new EntityProperty(value);
        }
        public void Get(string key, out Guid value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = (Guid)prop.GuidValue;
        }

        public void Set(string key, int value)
        {
            _properties[key] = new EntityProperty(value);
        }
        public void Get(string key, out int value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = (int)prop.Int32Value;
        }

        public void Set(string key, long value)
        {
            _properties[key] = new EntityProperty(value);
        }
        public void Get(string key, out long value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = (long)prop.Int64Value;
        }

        public void Set(string key, string value)
        {
            _properties[key] = new EntityProperty(value);
        }
        public void Get(string key, out string value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = (string)prop.StringValue;
        }

        /// <summary>
        /// Set complex types
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value)
            where T : new()
        {
            if (!_knownTypes.ContainsKey(key))
                _knownTypes.Add(key, typeof(T));

            string json = JsonConvert.SerializeObject(value);
            _properties[key] = new EntityProperty(json);

        }

        public void Get<T>(string key, out T value)
        {
            if (typeof(T) == typeof(string))
            {

            }
            if (typeof(T) == typeof(int))
            {

            }

            // 
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = JsonConvert.DeserializeObject<T>(prop.StringValue);
        }
    }

    class Test
    {
        public Test()
        {
            DynamicEntity entity = new DynamicEntity();
            entity.ETag = "*";
            entity.Set<Address>("address", new Address { ZipCode = 78900 });
            // Save

            Address address;
            entity.Get<Address>("address", out address);


            // Read from cloud
            //DynamicEntity entity = ReadFromStorage(id);            
            entity.Get<Address>("address");
            Person p = new Person();
            p.Address = entity.Get<string>("address");
        }
    }
    class Person
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    class Address
    {
        public int ZipCode { get; set; }
    }
}
