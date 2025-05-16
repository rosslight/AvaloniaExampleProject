using System.ComponentModel;
using Avalonia.Styling;
using AvaloniaExampleProject.Utilities;
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

    public string[] AvailableThemes { get; } = [DefaultTheme, DarkTheme, LightTheme];

    public IObservable<ThemeVariant> RequestedThemeVariant =>
        configurationService.Observe(config => ThemeVariantFromString(config.UserPreferences.SelectedTheme));

    private static ThemeVariant ThemeVariantFromString(string? theme) =>
        theme switch
        {
            DarkTheme => ThemeVariant.Dark,
            LightTheme => ThemeVariant.Light,
            _ => ThemeVariant.Default,
        };
}

public static class ConfigurationExtensions
{
    public static IObservable<T> Observe<TConfig, T>(
        this IConfigurationService<TConfig> configurationService,
        Func<TConfig, T> valueSelector
    ) => new ConfigurationObservable<TConfig, T>(configurationService, valueSelector);
}

file sealed class ConfigurationObservable<TConfig, T>(
    IConfigurationService<TConfig> configurationService,
    Func<TConfig, T> valueSelector
) : IObservable<T>
{
    private readonly IConfigurationService<TConfig> _configurationService = configurationService;
    private readonly Func<TConfig, T> _valueSelector = valueSelector;
    private T? _currentValue;

    public IDisposable Subscribe(IObserver<T> observer)
    {
        var handler = GetConfigChangedHandler(observer);
        _configurationService.PropertyChanged += handler;
        return Disposable.Create(() => _configurationService.PropertyChanged -= handler);
    }

    private PropertyChangedEventHandler GetConfigChangedHandler(IObserver<T> observer) =>
        (_, args) =>
        {
            if (args.PropertyName is not nameof(IConfigurationService<>.Config))
                return;
            var newValue = _valueSelector(_configurationService.Config);
            if (_currentValue?.Equals(newValue) is true)
                return;
            observer.OnNext(newValue);
            _currentValue = newValue;
        };
}
