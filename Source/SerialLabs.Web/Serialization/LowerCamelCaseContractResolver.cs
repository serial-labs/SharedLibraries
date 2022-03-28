using Newtonsoft.Json.Serialization;
using System;

namespace SerialLabs.Serialization
{
    /// <summary>
    /// Resolves properties in lower camel case (lowerCamelCase)
    /// </summary>
    public class LowerCamelCaseContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// Returns the property names as lower camel cased (lowerCamelCase)
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            string result = base.ResolvePropertyName(propertyName);
            return String.IsNullOrEmpty(result) ? result : result[0].ToString().ToLowerInvariant() + result.Substring(1, result.Length - 1);
        }
    }
}
