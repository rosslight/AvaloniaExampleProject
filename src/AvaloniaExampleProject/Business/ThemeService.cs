namespace AvaloniaExampleProject.Business;

public interface IThemeService
{
    string[] AvailableThemes { get; }
}

public sealed class ThemeService : IThemeService
{
    public const string DefaultTheme = "Default";
    public const string DarkTheme = "Dark";
    public const string LightTheme = "Light";

    public string[] AvailableThemes { get; } = [DefaultTheme, DarkTheme, LightTheme];
}
