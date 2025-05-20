using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.Business;
using AvaloniaExampleProject.Models;
using AvaloniaExampleProject.ViewModels;
using Darp.Utils.Assets;
using Darp.Utils.Configuration;
using Darp.Utils.Dialog;
using Darp.Utils.Dialog.FluentAvalonia;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection serviceCollection) =>
        serviceCollection
            // Configure core services
            .AddSingleton<IDialogService, AvaloniaDialogService>()
            .AddAppDataAssetsService("AvaloniaExampleProject")
            .AddConfigurationFile<MainConfig, IAppDataAssetsService>("config.json", JsonContext.Default.MainConfig)
            .AddLocalization()
            .AddSingleton<IThemeService, ThemeService>()
            // Configure ViewModels
            .AddTransient<MainWindowViewModel>()
            .AddTransient<MainViewModel>()
            .AddTransient<WelcomeViewModel>()
            .AddTransient<SettingsViewModel>();

    private static IServiceCollection AddLocalization(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton(Resources.Default);
}
