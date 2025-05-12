using AvaloniaExampleProject.Assets;

namespace AvaloniaExampleProject.ViewModels;

public sealed class MainViewModel(Resources i18N) : ViewModelBase
{
    public Resources I18N { get; } = i18N;
}
