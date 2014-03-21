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
        protected IDictionary<string, EntityProperty> _properties = new Dictionary<string, EntityProperty>();
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

        protected DynamicEntity(DynamicEntity entity)
        {
            _properties = entity._properties;
        }

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

        

        public void Set(string key, bool value)
        {
            _properties[key] = new EntityProperty(value);
        }

       

        public void Set(string key, byte[] value)
        {
            _properties[key] = new EntityProperty(value);
        }

        
        public void Set(string key, DateTime? value)
        {
            Set(key, value.Value);
        }

        

        public void Set(string key, DateTime value)
        {
            _properties[key] = new EntityProperty(value);
        }

        

        public void Set(string key, DateTimeOffset? value)
        {
            Set(key, value.Value);
        }
        

        public void Set(string key, DateTimeOffset value)
        {
            _properties[key] = new EntityProperty(value);
        }
        

        public void Set(string key, double value)
        {
            _properties[key] = new EntityProperty(value);
        }
        

        public void Set(string key, Guid value)
        {
            _properties[key] = new EntityProperty(value);
        }
        

        public void Set(string key, int value)
        {
            _properties[key] = new EntityProperty(value);
        }
        

        public void Set(string key, long value)
        {
            _properties[key] = new EntityProperty(value);
        }
        

        public void Set(string key, string value)
        {
            _properties[key] = new EntityProperty(value);
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

        public T Get<T>(string key) 
        {
            if (!_properties.ContainsKey(key))
            {
                throw new ArgumentException();
            }
            if((typeof(T)==typeof(DateTimeOffset?))||((typeof(T)==typeof(DateTime?))))
            {
                if(Nullable.GetUnderlyingType(typeof(T))==typeof(DateTime))
                {
                    return (T)Convert.ChangeType(_properties[key].DateTimeOffsetValue.Value.LocalDateTime, Nullable.GetUnderlyingType(typeof(T)));
                }
                if (Nullable.GetUnderlyingType(typeof(T)) == typeof(DateTimeOffset))
                {
                    return (T)Convert.ChangeType(_properties[key].DateTimeOffsetValue, Nullable.GetUnderlyingType(typeof(T)));
                }   
            }
            if (typeof(T) == typeof(string))
            {                
                return (T)Convert.ChangeType(_properties[key].StringValue, typeof(T));                
            }
            if (typeof(T) == typeof(int))
            {
                return (T)Convert.ChangeType(_properties[key].Int32Value, typeof(T));                
            }
            if (typeof(T) == typeof(long))
            {
                return (T)Convert.ChangeType(_properties[key].Int64Value, typeof(T));
            }
            if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(_properties[key].BooleanValue, typeof(T));
            }
            if (typeof(T) == typeof(DateTime))
            {
                return (T)Convert.ChangeType(_properties[key].DateTimeOffsetValue.Value.LocalDateTime, typeof(T));
            }
            if(typeof(T)==typeof(DateTimeOffset))
            {
                return (T)Convert.ChangeType(_properties[key].DateTimeOffsetValue, typeof(T));
            }
            if (typeof(T) == typeof(double))
            {
                return (T)Convert.ChangeType(_properties[key].DoubleValue, typeof(T));
            }
            if (typeof(T) == typeof(Guid))
            {
                return (T)Convert.ChangeType(_properties[key].GuidValue, typeof(T));
            }
            if (typeof(T) == typeof(EntityProperty))
            {
                return (T)Convert.ChangeType(_properties[key], typeof(T));
            }
            if (typeof(T) == typeof(Byte[]))
            {
                return (T)Convert.ChangeType(_properties[key].BinaryValue, typeof(T));
            }
            
            return JsonConvert.DeserializeObject<T>(_properties[key].StringValue);
        }
    }

}
