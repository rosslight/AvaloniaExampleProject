namespace AvaloniaExampleProject.Models;

// Warning: Source generated JSON serialization can behave differently than reflection-based serialization!
// The provided pattern with optional and nullable constructor parameters and default values on explicit properties works well, even though it looks stupid.
public sealed record MainConfig(UserPreferencesConfig? UserPreferences = null)
{
    public MainConfig()
        : this(UserPreferences: null) { }

    public UserPreferencesConfig UserPreferences { get; set; } = UserPreferences ?? new UserPreferencesConfig();
}

public sealed record UserPreferencesConfig(string? SelectedLanguage = null, string? SelectedTheme = null)
{
    public string SelectedLanguage { get; init; } = SelectedLanguage ?? "en-EN";
    public string SelectedTheme { get; init; } = SelectedTheme ?? "Default";
}
