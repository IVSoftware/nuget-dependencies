Visual Studio makes this kind of thing pretty straightforward if you know what levers to pull. So, let's say that for demo purposes we make a console app named `nuget-dependencies` that happens to use the `ClassLibA` nuget which is dependent on the `CommonClassLib` nuget. So here's what I wish someone had told me, in hopes it gets things moving in the right direction for you.
___
[![solution explorer][1]][1]

---
The three NuGet projects are similar, so let's just look at the **csproj** file for `ClassLibA` shown in full below. 
This tag makes it build to an adhoc folder relative to the solution:

`<PackageOutputPath>$(SolutionDir)\local-repo</PackageOutputPath>`

This causes it to pack the NuGets whenever the project builds.

`<GeneratePackageOnBuild>True</GeneratePackageOnBuild>`

And this is a [floating reference](https://learn.microsoft.com/en-us/nuget/concepts/dependency-resolution#floating-versions) to "the latest release version" of CommonClassLib.

`<PackageReference Include="CommonClassLib" Version="*" />`
```xml
<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<Title>ClassLibA</Title>
		<Description>Demo Only</Description>
		<PackageTags>portable;net;android;ios</PackageTags>
		<RepositoryUrl>https://github.com/IVSoftware/nuget-dependencies</RepositoryUrl>
		<PackageProjectUrl>https://github.com/IVSoftware/nuget-dependencies</PackageProjectUrl>
		<TargetFramework>netstandard2.0</TargetFramework>
		<AssemblyVersion>1.5.0</AssemblyVersion>
		<GeneratePackageOnBuild>True</GeneratePackageOnBuild>
		<Authors>IVSoft</Authors>
		<Owner>IVSoft</Owner>
		<Copyright>Copyright © 2024 IVSoft</Copyright>
		<FileVersion>$(AssemblyVersion)</FileVersion>
		<Version>$(AssemblyVersion)</Version>
		<PackageReadmeFile>README.md</PackageReadmeFile>
		<PackageLicenseExpression>MIT</PackageLicenseExpression>
		<PackageRequireLicenseAcceptance>True</PackageRequireLicenseAcceptance>
		<!--SignAssembly>True</SignAssembly-->
		<!--AssemblyOriginatorKeyFile></AssemblyOriginatorKeyFile-->
		<DebugType>embedded</DebugType>
		<PackageOutputPath>$(SolutionDir)\local-repo</PackageOutputPath>
	</PropertyGroup>
	<ItemGroup>
		<None Include="..\README.md">
			<Pack>True</Pack>
			<PackagePath>\</PackagePath>
		</None>
	</ItemGroup>
	<ItemGroup>
	  <PackageReference Include="CommonClassLib" Version="*" />
	</ItemGroup>
</Project>
```
___

Now build and inspect the local-repo folder:

[![local-repo][2]][2]

And connect to it using the settings gear of the Package Source drop down.

[![local NuGet repo][3]][3]

___

The main point I want to make regarding your question is that when you go to manage the NuGet packages for the console app, one would only need to install the ClassLibA (or the ClassLibB...) and the common library will be picked up as a transitive reference.

[![NuGet package manager][4]][4]

___

And as you stated `ClassLibA` and `ClassLibB` know nothing of each other. That said, if you were to use _both_ A and B in a project, and the versions were different, then you would resolve this by an explicit reference to the common NuGet in the client project. (Since _you_ are the publisher of all three packages however, this is not as likely as when third-party packages reference different versions of something like SQLite or JSON.)

  [1]: https://i.sstatic.net/6HGe2VBM.png
  [2]: https://i.sstatic.net/QSVZVQNn.png
  [3]: https://i.sstatic.net/mLIrpvTD.png
  [4]: https://i.sstatic.net/oT30CVJA.png