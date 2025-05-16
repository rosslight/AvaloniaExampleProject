using System.Text.Json.Serialization;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.Business;
using AvaloniaExampleProject.ViewModels;
using Darp.Utils.Assets;
using Darp.Utils.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection serviceCollection) =>
        serviceCollection
            .AddAppDataAssetsService("AvaloniaExampleProject")
            .AddConfigurationFile<MainConfig, IAppDataAssetsService>("config.json", JsonContext.Default.MainConfig)
            .AddSingleton<IThemeService, ThemeService>()
            .AddTransient<MainViewModel>()
            .AddTransient<WelcomeViewModel>()
            .AddTransient<SettingsViewModel>()
            .AddLocalization();

    private static IServiceCollection AddLocalization(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton(Resources.Default);
}

[JsonSerializable(typeof(MainConfig))]
public sealed partial class JsonContext : JsonSerializerContext;
