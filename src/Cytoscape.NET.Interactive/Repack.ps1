# Clean up the previously-cached NuGet packages.
# Lower-case is intentional (that's how nuget stores those packages).
Remove-Item -Recurse ~\.nuget\packages\cytoscape.net.interactive* -Force
Remove-Item -Recurse ~\.nuget\packages\cytoscape.net* -Force

# build and pack Plotly.NET.Interactive
cd ../../
./build.cmd
dotnet pack -c Release -p:PackageVersion=0.0.1-dev -o "./pkg"
cd src/Cytoscape.NET.Interactive
