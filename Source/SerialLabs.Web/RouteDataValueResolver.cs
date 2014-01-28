using System;
using System.Web;

namespace SerialLabs.Web
{
    /// <summary>
    /// Helper class to resolve route data values
    /// </summary>
    public static class RouteDataValueResolver
    {
        /// <summary>
        /// Find the value corresponding to the given key name.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When key does not exist</exception>
        public static string Resolve(HttpContextBase context, string keyName)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrWhiteSpace(keyName, "keyName");

            if (context.Request == null)
                throw new InvalidOperationException("HttpContextBase Request is null");
            if (context.Request.RequestContext == null)
                throw new InvalidOperationException("HttpContextBase Request RequestContext is null");
            if (!context.Request.RequestContext.RouteData.Values.ContainsKey(keyName))
                return null;
            return context.Request.RequestContext.RouteData.Values[keyName].ToString();
        }
    }
}
