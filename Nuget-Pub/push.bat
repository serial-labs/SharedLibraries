ECHO OFF
for /R %%f in (*.nupkg) do (
	NuGet push %%f
)