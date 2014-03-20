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

        public void Add(string key, EntityProperty value)
        {
            _properties.Add(key, value);
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

        public void Add(string key, bool value)
        {
            _properties.Add(key, new EntityProperty(value));
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

        public void Add(string key, byte[] value)
        {
            _properties.Add(key, new EntityProperty(value));
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

        public void Add(string key, DateTime? value)
        {
            Add(key, value.Value);
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

        public void Add(string key, DateTime value)
        {
            _properties.Add(key, new EntityProperty(value));
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

        public void Add(string key, DateTimeOffset? value)
        {
            Add(key, value.Value);
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

        public void Add(string key, DateTimeOffset value)
        {
            _properties.Add(key, new EntityProperty(value));
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

        public void Add(string key, double value)
        {
            _properties.Add(key, new EntityProperty(value));
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
        public void Add(string key, Guid value)
        {
            _properties.Add(key, new EntityProperty(value));
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
        public void Add(string key, int value)
        {
            _properties.Add(key, new EntityProperty(value));
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
        public void Add(string key, long value)
        {
            _properties.Add(key, new EntityProperty(value));
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
        public void Add(string key, string value)
        {
            _properties.Add(key, new EntityProperty(value));
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
        /// Add complex types
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add<T>(string key, T value)
            where T : new()
        {
            if (!_knownTypes.ContainsKey(key))
                _knownTypes.Add(key, typeof(T));
            string json = JsonConvert.SerializeObject(value);
            _properties.Add(key, new EntityProperty(json));

        }
        public void Set<T>(string key, T value)
            where T : new()
        {
            string json = JsonConvert.SerializeObject(value);
            _properties[key] = new EntityProperty(json);

        }

        public void Get<T>(string key, out T value)
        {
            EntityProperty prop;
            _properties.TryGetValue(key, out prop);
            value = JsonConvert.DeserializeObject<T>(prop.StringValue);
        }


        public override bool Equals(Object obj)
        {


            if (obj == null) { return false; }
            if (!(obj is DynamicEntity)) { return false; }

            DynamicEntity dynamicEntity = (DynamicEntity)obj;

            if (dynamicEntity._properties.Count != _properties.Count) { return false; }

            foreach (var pair in _properties)
            {
                if (!dynamicEntity._properties[pair.Key].Equals(pair.Value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
