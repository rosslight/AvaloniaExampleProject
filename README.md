# Avalonia ExampleProject

This repository contains an example project which can be used to base new projects on.

## Features

- Already configured repository
  - GitHub actions workflows
  - `editorconfig` and `.gitignore` setup
  - [CPM](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)
  - `.slnx` solution files
  - [CSharpier](https://csharpier.com/) as a code formatter
- Platform independent
  - Build for any desktop platform
  - Easily extensible to mobile platforms
- High performant
  - `PublishAot` and trimming is enabled by default
  - SplashScreen for resource loading on startup
- Personalization by default
  - Runtime switching of themes
  - Runtime switching of languages (using `.resx` and [source generation](https://github.com/rosslight/Darp.Utils/blob/main/src/Darp.Utils.ResxSourceGenerator/README.md))
- WinUI3 styling
  - Usage of [FluentAvalonia](https://github.com/amwx/FluentAvalonia) for WinUI3 styles and controls
- Industry tested
  - Desktop applications used in the Industry were build with the same technologies
- Opinionated toolset
  - [Microsoft Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) for DI
  - [Microsoft Logging](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging) for Logging
  - [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) for ObservableProperties and Commands
  - [Darp.Utils.Dialog](https://github.com/rosslight/Darp.Utils/tree/main?tab=readme-ov-file#darputilsdialog) for MVVM compatible dialogs
  - [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview) with source generation for configuration files

## Preview

![](doc/_media/app-preview.gif)

## How to use this project as a template

Install the rosslight templates nuget package:

````shell
dotnet new install Darp.Templates 
````

Then, create a new template using

```shell
dotnet new darp.avalonia.desktop -o MyApp
```

### GitHub Actions

- Go to the [workflow file](.github/workflows/build-test.yml)

## Further reading

As stated, this is a opinionated app. Take a look at other resources

- [Awesome Avalonia](https://github.com/AvaloniaCommunity/awesome-avalonia?tab=readme-ov-file#tutorials)
- [Avalonia docs](https://docs.avaloniaui.net/)
- [Modern.Net-Tutorial](https://github.com/mysteryx93/Modern.Net-Tutorial)

## About

This is a project by [rosslight GmbH](https://rosslight.de/)
