@ECHO OFF
ECHO Delete Existing Packages
for /r %%f in (*.nupkg) do del %%f
ECHO Packing Projects
NuGet Pack ../Source/SerialLabs/SerialLabs.csproj -OutputDirectory ".\SerialLabs" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Data/SerialLabs.Data.csproj -OutputDirectory ".\SerialLabs.Data" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Data.AzureTable/SerialLabs.Data.AzureTable.csproj -OutputDirectory ".\SerialLabs.Data.AzureTable" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Data.EntityFramework/SerialLabs.Data.EntityFramework.csproj -OutputDirectory ".\SerialLabs.Data.EntityFramework" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.ExceptionHandling/SerialLabs.ExceptionHandling.csproj -OutputDirectory ".\SerialLabs.ExceptionHandling" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.ExceptionHandling.Logging/SerialLabs.ExceptionHandling.Logging.csproj -OutputDirectory ".\SerialLabs.ExceptionHandling.Logging" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Fakers/SerialLabs.Fakers.csproj -OutputDirectory ".\SerialLabs.Fakers" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Logging/SerialLabs.Logging.csproj -OutputDirectory ".\SerialLabs.Logging" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Logging.AzureTable/SerialLabs.Logging.AzureTable.csproj -OutputDirectory ".\SerialLabs.Logging.AzureTable" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Web/SerialLabs.Web.csproj -OutputDirectory ".\SerialLabs.Web" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Web.Http/SerialLabs.Web.Http.csproj -OutputDirectory ".\SerialLabs.Web.Http" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Web.Mvc/SerialLabs.Web.Mvc.csproj -OutputDirectory ".\SerialLabs.Web.Mvc" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ../Source/SerialLabs.Monitoring/SerialLabs.Monitoring.csproj -OutputDirectory ".\SerialLabs.Monitoring" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
