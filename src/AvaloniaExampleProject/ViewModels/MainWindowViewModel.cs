using AvaloniaExampleProject.Business;

namespace AvaloniaExampleProject.ViewModels;

public sealed class MainWindowViewModel(IThemeService themeService) : ViewModelBase
{
    public IThemeService ThemeService { get; } = themeService;
}
