[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]




<!-- PROJECT TITLE -->
<div align="center">
	<h1>Weather Forecast Fix</h1>
	<p>
		A <a href="http://www.derailvalley.com/">Derail Valley</a> mod that loads via <a href="https://www.nexusmods.com/site/mods/21">Unity Mod Manager</a>.
		<br />
		<br />
		<a href="https://github.com/heizpilz/dv-weather-forecast-fix/issues">Report Bug</a>
		·
		<a href="https://github.com/heizpilz/dv-weather-forecast-fix/issues">Request Feature</a>
	</p>
</div>




<!-- TABLE OF CONTENTS -->
<details>
	<summary>Table of Contents</summary>
	<ol>
		<li><a href="#about-the-project">About The Project</a></li>
		<li><a href="#building">Building</a></li>
		<li><a href="#packaging">Packaging</a></li>
		<li><a href="#license">License</a></li>
	</ol>
</details>




<!-- ABOUT THE PROJECT -->

## About The Project

**This mod is obsolete as of Build 99, as the vanilla bug is fixed.**

While poking around for the bonus end time mod, I came across the issue with the current weather forecast system. So here I try to fix it.

This mod can safely be disabled at all times.

It will probably break and get redundant when the devs get to implementing a proper fix for this issue. I strongly advise to check the release notes of upcoming game updates and uninstall this mod if changes to the weather forecast are announced. **This is now the case. The mod should be uninstalled when running Build 99 or higher.**

Get the mod from <a href="https://www.nexusmods.com/derailvalley/mods/800">Nexus</a>



<!-- BUILDING -->

## Building

Building the project requires some initial setup, after which running `dotnet build` will do a Debug build or running `dotnet build -c Release` will do a Release build.

### References Setup

After cloning the repository, some setup is required in order to successfully build the mod DLLs. You will need to create a new [Directory.Build.targets][references-url] file to specify your local reference paths. This file will be located in the main directory, next to WeatherForecastFix.sln.

Below is an example of the necessary structure. When creating your targets file, you will need to replace the reference paths with the corresponding folders on your system. Make sure to include semicolons **between** each of the paths and no semicolon after the last path. Also note that any shortcuts you might use in file explorer—such as %ProgramFiles%—won't be expanded in these paths. You have to use full, absolute paths.
```xml
<Project>
	<PropertyGroup>
		<ReferencePath>
			C:\Program Files (x86)\Steam\steamapps\common\Derail Valley\DerailValley_Data\Managed\;
			C:\Program Files (x86)\Steam\steamapps\common\Derail Valley\DerailValley_Data\Managed\UnityModManager\
		</ReferencePath>
		<AssemblySearchPaths>$(AssemblySearchPaths);$(ReferencePath);</AssemblySearchPaths>
	</PropertyGroup>
</Project>
```

### Line Endings Setup

It's recommended to use Git's [autocrlf mode][autocrlf-url] on Windows. Activate this by running `git config --global core.autocrlf true`.




<!-- PACKAGING -->

## Packaging

To package a build for distribution, you can run the `package.ps1` PowerShell script in the root of the project. If no parameters are supplied, it will create a .zip file ready for distribution in the dist directory. A post build event is configured to run this automatically after each successful Release build.

Linux: `pwsh ./package.ps1`
Windows: `powershell -executionpolicy bypass .\package.ps1`


### Parameters

Some parameters are available for the packaging script.

#### -NoArchive

Leave the package contents uncompressed in the output directory.

#### -OutputDirectory

Specify a different output directory.
For instance, this can be used in conjunction with `-NoArchive` to copy the mod files into your Derail Valley installation directory.




<!-- LICENSE -->

## License

Source code is distributed under the MIT license.
See [LICENSE][license-url] for more information.




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[contributors-shield]: https://img.shields.io/github/contributors/heizpilz/dv-weather-forecast-fix.svg?style=for-the-badge
[contributors-url]: https://github.com/heizpilz/dv-weather-forecast-fix/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/heizpilz/dv-weather-forecast-fix.svg?style=for-the-badge
[forks-url]: https://github.com/heizpilz/dv-weather-forecast-fix/network/members
[stars-shield]: https://img.shields.io/github/stars/heizpilz/dv-weather-forecast-fix.svg?style=for-the-badge
[stars-url]: https://github.com/heizpilz/dv-weather-forecast-fix/stargazers
[issues-shield]: https://img.shields.io/github/issues/heizpilz/dv-weather-forecast-fix.svg?style=for-the-badge
[issues-url]: https://github.com/heizpilz/dv-weather-forecast-fix/issues
[license-shield]: https://img.shields.io/github/license/heizpilz/dv-weather-forecast-fix.svg?style=for-the-badge
[license-url]: https://github.com/heizpilz/dv-weather-forecast-fix/blob/main/LICENSE
[references-url]: https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2022
[autocrlf-url]: https://www.git-scm.com/book/en/v2/Customizing-Git-Git-Configuration#_formatting_and_whitespace
