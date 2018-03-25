# Entity Framework Migrations .Net Core

## To enable migrations in .Net Core:

EF Core includes a set of additional commands for the dotnet CLI, starting with dotnet ef. 
In order to use the dotnet ef CLI commands, your application’s .csproj file needs to contain the following entry:

<ItemGroup>
  <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.1" />
</ItemGroup>

The .NET Core CLI tools for EF Core also require a separate package called 
Microsoft.EntityFrameworkCore.Design


## Useful links

https://docs.microsoft.com/en-us/ef/core/get-started/install/

Problem when DbContext is in a .Net Standard project:
https://stackoverflow.com/questions/44430963/is-ef-core-add-migration-supported-from-net-standard-library

https://docs.microsoft.com/en-us/ef/core/modeling/#methods-of-configuration
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/migrations#introduction-to-migrations