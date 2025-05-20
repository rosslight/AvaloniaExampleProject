using AvaloniaExampleProject.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject.Models;

/// <summary> A static class which provides design data to views </summary>
public static class DesignData
{
    public static IServiceProvider ServiceProvider
    {
        get => App.IsDesignMode ? App.ServiceProvider! : throw new Exception("Not in design mode");
    }

    public static WelcomeViewModel WelcomeViewModel { get; } = ServiceProvider.GetRequiredService<WelcomeViewModel>();
    public static SettingsViewModel SettingsViewModel { get; } =
        ServiceProvider.GetRequiredService<SettingsViewModel>();
}
