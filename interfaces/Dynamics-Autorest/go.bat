@echo off

echo Updating meta data

dotnet run -p ..\OData.OpenAPI\odata2openapi\odata2openapi.csproj spice

echo Updating client

autorest --verbose Readme.md
