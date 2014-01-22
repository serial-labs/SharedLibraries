
namespace SerialLabs.Logging.Configuration
{
    /// <summary>
    /// This enumeration defines the options that the <see cref="EmailTraceListener"/>
    /// can use to authenticate to the STMP server.
    /// </summary>
    public enum EmailAuthenticationMode
    {
        /// <summary>
        /// No authentication
        /// </summary>
        None = 0,

        /// <summary>
        /// Use the Windows credentials for the current process
        /// </summary>
        WindowsCredentials,

        /// <summary>
        /// Pass a user name and password
        /// </summary>
        UserNameAndPassword
    }
}
