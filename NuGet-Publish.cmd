ECHO OFF

ECHO Packaging SerialLabs
nuget pack Source\SerialLabs\SerialLabs.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs

ECHO Packaging SerialLabs.Data
nuget pack Source\SerialLabs.Data\SerialLabs.Data.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.Data

ECHO Packaging SerialLabs.Data.Entity
nuget pack Source\SerialLabs.Data.Entity\SerialLabs.Data.Entity.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.Data.Entity

ECHO Packaging SerialLabs.ExceptionHandling
nuget pack Source\SerialLabs.ExceptionHandling\SerialLabs.ExceptionHandling.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.ExceptionHandling

ECHO Packaging SerialLabs.ExceptionHandling.Logging
nuget pack Source\SerialLabs.ExceptionHandling.Logging\SerialLabs.ExceptionHandling.Logging.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.ExceptionHandling.Logging

ECHO Packaging SerialLabs.Logging
nuget pack Source\SerialLabs.Logging\SerialLabs.Logging.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.Logging

ECHO Packaging SerialLabs.Logging.CloudStorage
nuget pack Source\SerialLabs.Logging.CloudStorage\SerialLabs.Logging.CloudStorage.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.Logging.CloudStorage

ECHO Packaging SerialLabs.Web
nuget pack Source\SerialLabs.Web\SerialLabs.Web.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.Web

ECHO Packaging SerialLabs.Web.Http
nuget pack Source\SerialLabs.Web.Http\SerialLabs.Web.Http.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.Web.Http

ECHO Packaging SerialLabs.Web.Mvc
nuget pack Source\SerialLabs.Web.Mvc\SerialLabs.Web.Mvc.csproj -Prop Configuration=Release -Symbols -IncludeReferencedProjects -OutputDirectory .\Publish\SerialLabs.Web.Mvc

