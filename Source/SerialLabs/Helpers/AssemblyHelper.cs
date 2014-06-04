using System.Diagnostics;
using System.Reflection;

namespace SerialLabs
{
    public static class AssemblyHelper
    {
        public static string GetInformationalVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        public static string GetInformationalVersion(Assembly assembly)
        {
            return FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
        }
    }
}
