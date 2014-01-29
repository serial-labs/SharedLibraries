using System.Reflection;
using System.Resources;

[assembly: AssemblyProduct("SharedLibraries")]
[assembly: AssemblyCompany("serial-labs")]
[assembly: AssemblyCopyright("Copyright © serial-labs 2014")]
[assembly: AssemblyTrademark("serial-labs")]
[assembly: NeutralResourcesLanguage("en-US")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyVersion("0.1.0.*")]
[assembly: AssemblyFileVersion("0.1.0.0")]
[assembly: AssemblyInformationalVersion("Beta 1.0.0")]