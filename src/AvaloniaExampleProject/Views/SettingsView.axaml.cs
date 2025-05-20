using Avalonia.Data.Converters;
using AvaloniaExampleProject.Business;
using AvaloniaExampleProject.ViewModels;

namespace AvaloniaExampleProject.Views;

public sealed partial class SettingsView : UserControlBase<SettingsViewModel>
{
    public SettingsView()
    {
        InitializeComponent();
    }

    public static readonly IValueConverter ThemeTranslationConverter = new FuncValueConverter<string, string>(s =>
        s switch
        {
            ThemeService.DefaultTheme => Assets.Resources.Default.Settings_Theme_Default,
            ThemeService.LightTheme => Assets.Resources.Default.Settings_Theme_Light,
            ThemeService.DarkTheme => Assets.Resources.Default.Settings_Theme_Dark,
            _ => "n/a",
        }
    );
}
