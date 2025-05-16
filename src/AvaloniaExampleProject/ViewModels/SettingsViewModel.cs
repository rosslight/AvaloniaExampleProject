using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using Darp.Utils.Configuration;

namespace AvaloniaExampleProject.ViewModels;

public sealed partial class SettingsViewModel(
    IThemeService themeService,
    Resources i18N,
    IConfigurationService<MainConfig> configurationService
) : ViewModelBase
{
    private readonly IConfigurationService<MainConfig> _configurationService = configurationService;

    public IThemeService ThemeService { get; } = themeService;
    public Resources I18N { get; } = i18N;

    public static string[] AvailableLanguages { get; } = ["en-EN", "de-DE"];

    [ObservableProperty]
    public partial string SelectedLanguage { get; set; } = configurationService.Config.UserPreferences.SelectedLanguage;

    [ObservableProperty]
    public partial string SelectedTheme { get; set; } = configurationService.Config.UserPreferences.SelectedTheme;
}
