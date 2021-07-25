<p align="center">
   <img width=200px src="https://avatars.githubusercontent.com/u/69518398?s=200&v=4" >
</p>
<br/>

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

## Contributing

The style guide is defined in the [`.editorconfig`](./.editorconfig) at the root of this repository and it will be picked up in intellisense by capable editors. Please follow the provided style for consistency.

## License

Vignette is Copyright &copy; 2020 Ayane Satomi and the Vignette Authors, licensed under Non-Profit Open Source License v3.0. For the full license text please see the [LICENSE](./LICENSE) file in this repository.

## Commercial Use and Support

While Vignette is NPOSL-3.0, We do not provide commercial support, there is nothing stopping you from using it commercially but if you want proper dedicated support from the Vignette engineers, we highly recommend the Enterprise tier on our [Open Collective](https://opencollective.com/vignette).
