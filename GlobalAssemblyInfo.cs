using System.Reflection;
using System.Resources;

[assembly: AssemblyProduct("SharedLibraries")]
[assembly: AssemblyCompany("serial-labs")]
[assembly: AssemblyCopyright("Copyright © serial-labs 2014-2022")]
[assembly: AssemblyTrademark("serial-labs")]
[assembly: NeutralResourcesLanguage("en-US")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyVersion("0.2.*")]
[assembly: AssemblyFileVersion("0.2.*")]
[assembly: AssemblyInformationalVersion("1.9.1-phoenix")]