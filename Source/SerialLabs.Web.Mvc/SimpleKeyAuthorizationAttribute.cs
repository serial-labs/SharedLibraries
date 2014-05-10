using System;
using System.Configuration;
using System.Web.Mvc;

namespace SerialLabs.Web.Mvc
{
    /// <summary>
    /// A simple authorize attribute which enforce a query string key authorization
    /// </summary>
    public class SimpleKeyAuthorizationAttribute : AuthorizeAttribute
    {
        private string _key;

        /// <summary>
        /// Creates a new instance of the <see cref="SimpleKeyAuthorizationAttribute"/>
        /// </summary>
        /// <param name="appSettingName"></param>
        public SimpleKeyAuthorizationAttribute(string appSettingName)
        {
            Guard.ArgumentNotNullOrWhiteSpace(appSettingName, "appSettingName");
            _key = ConfigurationManager.AppSettings[appSettingName];
        }

        /// <summary>
        /// Authorize
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (String.IsNullOrWhiteSpace(_key))
                return;

            string[] values = filterContext.HttpContext.Request.QueryString.GetValues("key");
            if (values == null || values.Length == 0 || values[0].ToString() != _key)
                filterContext.Result = new HttpNotFoundResult();
        }
    }
}
