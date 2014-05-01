using System.Security.Claims;
using System.Security.Principal;

namespace SerialLabs
{
    /// <summary>
    /// Provides extensions methods for the <see cref="IPrincipal"/> interface
    /// </summary>
    public static class IPrincipalExtensions
    {
        /// <summary>
        /// Returns the current <see cref="IPrincipal"/> as a <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static ClaimsPrincipal AsClaimsPrincipal(this IPrincipal principal)
        {
            return principal as ClaimsPrincipal;
        }
    }
}
