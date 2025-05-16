using System.Globalization;
using AsyncAwaitBestPractices;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using Darp.Utils.Configuration;
using Microsoft.Extensions.Logging;

namespace AvaloniaExampleProject.ViewModels;

public sealed partial class SettingsViewModel(
    IThemeService themeService,
    Resources i18N,
    IConfigurationService<MainConfig> configurationService,
    ILogger<SettingsViewModel> logger
) : ViewModelBase
{
    private readonly IConfigurationService<MainConfig> _configurationService = configurationService;
    private readonly ILogger<SettingsViewModel> _logger = logger;

    public IThemeService ThemeService { get; } = themeService;
    public Resources I18N { get; } = i18N;

    public static string[] AvailableLanguages { get; } = ["en-EN", "de-DE"];

    [ObservableProperty]
    public partial string SelectedLanguage { get; set; } = configurationService.Config.UserPreferences.SelectedLanguage;

    [ObservableProperty]
    public partial string SelectedTheme { get; set; } = configurationService.Config.UserPreferences.SelectedTheme;

    partial void OnSelectedLanguageChanged(string value)
    {
        I18N.Culture = new CultureInfo(value);
        SaveConfig(c => c with { UserPreferences = c.UserPreferences with { SelectedLanguage = value } });
    }

    partial void OnSelectedThemeChanged(string value) =>
        SaveConfig(c => c with { UserPreferences = c.UserPreferences with { SelectedTheme = value } });

    private void SaveConfig(Func<MainConfig, MainConfig> createConfig)
    {
        Task.Run(async () =>
            {
                var currentConfig = _configurationService.Config;
                var newConfig = createConfig(currentConfig);
                await _configurationService.WriteConfigurationAsync(newConfig);
            })
            .SafeFireAndForget(e =>
                _logger.LogError(e, "Could not save configuration because of {Message}", e.Message)
            );
    }
}
