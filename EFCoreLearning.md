#Learning about Entity framework Core

I'm familiar with EF6 but I'll try to cover the basics in what I research for EF Core

My first hit on google...

[tutorial on ef core](http://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)

I'm using the InMemory Provier as a test. We will not use this for our demo.

Just read something about integrating the CLI->

Add <DotNetCliToolReference> node as shown below. This is an extra step you need to perform in order to execute EF Core 2.0 commands from dotnet CLI in VS2017
This could help with problems I'm having with docfx.

While Generating the DBContext constructor, I came across the following error.

NotNullAttribute  not accesible due it's defined as Internal in its assembly'. Apparantly this is a Resharper helper class for code validation.
But if you are not using Resharper or don't want to have this validation happen, just remove the attribute, it has not code behind. It's just a marker of Resharper to validate the code.


Damn no migration for InMemory, I'll those those tests for later.


Hmm no Many 2 Many relations without creating the mappingtable as an entity. Could be a deal breaker to advice use of EFCore ( and .NET Core) for projects with many such relations.


I'll add more info as I learn about this.



