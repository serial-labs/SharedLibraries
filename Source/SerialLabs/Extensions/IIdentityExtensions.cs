using System.Security.Claims;
using System.Security.Principal;

namespace SerialLabs
{
    /// <summary>
    /// Provides extension methods for the <see cref="IIdentity"/> interface.
    /// </summary>
    public static class IIdentityExtensions
    {
        /// <summary>
        /// Returns the current <see cref="IIdentity"/> as a <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static ClaimsIdentity AsClaimsIdentity(this IIdentity identity)
        {
            return identity as ClaimsIdentity;
        }
    }
}
