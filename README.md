<p align="center">
   <img width=500px src="branding.svg" >
</p>

[![Discord](https://img.shields.io/discord/746656644196335647?color=7289DA&label=%20&logo=discord&logoColor=white)](https://discord.gg/3yMf3Y9) [![GitHub Super-Linter](https://github.com/vignette-project/vignette/workflows/Lint/badge.svg)](https://github.com/marketplace/actions/super-linter)

Vignette is a face tracking software for characters using [osu!framework](https://github.com/ppy/osu-framework). Unlike most solutions, Vignette is:

- Made with osu!framework, the game framework that powers [osu!lazer](https://github.com/ppy/osu), the next iteration of [osu!](https://osu.ppy.sh).
- Open source, from the very core.
- Always evolving - Vignette improves every update, and it tries to know you better too, literally.

## Running

We provide releases from GitHub Releases and also from Visual Studio App Center. Vignette releases builds for a select few people before we create a release here, so pay attention.

You can also run Vignette by cloning the repository and running this command in your terminal.
```
dotnet run --project Vignette.Desktop
```

## Developing

Please make sure you meet the prerequisites:
- A desktop platform with [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed.
- Have provided proper credentials to access the [GitHub Packages](https://github.com/orgs/vignette-project/packages).
- Provide your own copy of the [Live2D Cubism SDK](https://www.live2d.com/en/download/cubism-sdk/).

### Setting up GitHub Packages

Open your NuGet Configuration (global or user-scoped) and append this inside the configuration:

```xml
<packageSourceCredentials>
   <Vignette>
      <add key="Username" value="YourGitHubUserName"/>
      <add key="ClearTextPassword" value="YourGitHubPAT"/>
   </Vignette>
</packageSourceCredentials>
```

Your GitHub PAT (personal access token) should have the `read:packages` scope to access this organization's NuGet feed.

- See [this article](https://docs.github.com/en/packages/guides/configuring-dotnet-cli-for-use-with-github-packages) for more information about configuring GitHub Packages for NuGet and .NET.
- See [this](https://docs.github.com/en/github/authenticating-to-github/creating-a-personal-access-token) also for more information about creating a personal access token.

### Setting up the Live2D Cubism SDK

Due to restrictions, we are unable to provide the SDK publicly. However, you can still run Vignette by manually including these files when building.
1. Download the SDK through [this link](https://www.live2d.com/en/download/cubism-sdk/).
2. Extract the correct library for your operating system and architecture in the root of the build directory.

With this, the application will be able to pickup the libraries and start.

## Contributing

The style guide is defined in the [`.editorconfig`](./.editorconfig) at the root of this repository and it will be picked up in intellisense by capable editors. Please follow the provided style for consistency.

## License

Vignette is Copyright &copy; 2020 Ayane Satomi and the Vignette Authors, licensed under Non-Profit Open Source License v3.0. For the full license text please see the [LICENSE](./LICENSE.md) file in this repository.

## Commercial Use

While Vignette is NPOSLv3, for-profit agencies who wish to use Vignette for their performers, please contact [Ayane Satomi](mailto:chinodesuuu@gmail.com) for a negotiation.