using AvaloniaExampleProject.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject.Models;

/// <summary> A static class which provides design data to views </summary>
public static class DesignData
{
    public static IServiceProvider Services => App.Current.Services;

    public static WelcomeViewModel WelcomeViewModel { get; } = Services.GetRequiredService<WelcomeViewModel>();
    public static SettingsViewModel SettingsViewModel { get; } = Services.GetRequiredService<SettingsViewModel>();
    public static MainViewModel MainViewModel { get; } = Services.GetRequiredService<MainViewModel>();
}
