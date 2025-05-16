using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.Business;

namespace AvaloniaExampleProject.ViewModels;

public sealed class MainWindowViewModel(Resources i18N, IThemeService themeService) : ViewModelBase
{
    public Resources I18N { get; } = i18N;
    public IThemeService ThemeService { get; } = themeService;
}
