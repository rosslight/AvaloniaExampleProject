using System.ComponentModel;
using System.Reactive.Disposables;
using Avalonia.Styling;
using AvaloniaExampleProject.Models;
using Darp.Utils.Configuration;

namespace AvaloniaExampleProject.Business;

public interface IThemeService
{
    string[] AvailableThemes { get; }
    IObservable<ThemeVariant> RequestedThemeVariant { get; }
}

public sealed class ThemeService(IConfigurationService<MainConfig> configurationService) : IThemeService
{
    public const string DefaultTheme = "Default";
    public const string DarkTheme = "Dark";
    public const string LightTheme = "Light";

    private readonly IConfigurationService<MainConfig> _configurationService = configurationService;

    public string[] AvailableThemes { get; } = [DefaultTheme, DarkTheme, LightTheme];

    public IObservable<ThemeVariant> RequestedThemeVariant =>
        _configurationService.Observe(config => ThemeVariantFromString(config.UserPreferences.SelectedTheme));

    private static ThemeVariant ThemeVariantFromString(string? theme) =>
        theme switch
        {
            DarkTheme => ThemeVariant.Dark,
            LightTheme => ThemeVariant.Light,
            _ => ThemeVariant.Default,
        };
}
