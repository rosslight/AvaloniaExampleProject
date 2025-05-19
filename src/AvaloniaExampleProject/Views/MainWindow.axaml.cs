using System.Globalization;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.ViewModels;
using Darp.Utils.Configuration;
using FluentAvalonia.UI.Windowing;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject.Views;

public sealed partial class MainWindow : AppWindow
{
    private readonly IServiceProvider _provider;

    public MainWindow(IServiceProvider provider)
    {
        _provider = provider;
        InitializeComponent();

        DataContext = provider.GetRequiredService<MainWindowViewModel>();

        ConfigureTitleBar();

        SplashScreen = new MainAppSplashScreen(this);
    }

    internal async Task LoadAsync(CancellationToken cancellationToken)
    {
        var configurationService = _provider.GetRequiredService<IConfigurationService<MainConfig>>();
        var config = await configurationService.LoadConfigurationAsync(cancellationToken);
        var i18N = _provider.GetRequiredService<Resources>();
        i18N.Culture = new CultureInfo(config.UserPreferences.SelectedLanguage);
        Dispatcher.UIThread.Invoke(() =>
        {
            WindowContent.Content = new MainView(_provider)
            {
                ViewModel = _provider.GetRequiredService<MainViewModel>(),
            };
        });
    }

    private void ConfigureTitleBar()
    {
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        TitleBar.Height = 44;

        // Update colors
        _ = SubscribeToColor("ButtonForegroundDisabled", color => TitleBar.ButtonInactiveForegroundColor = color);
        _ = SubscribeToColor("AppBarButtonBackgroundPressed", color => TitleBar.ButtonPressedBackgroundColor = color);
        _ = SubscribeToColor("AppBarButtonBackgroundPointerOver", color => TitleBar.ButtonHoverBackgroundColor = color);
    }

    private IDisposable SubscribeToColor(string resourceName, Action<Color> onNext) =>
        Resources
            .GetResourceObservable(resourceName)
            .Subscribe(o =>
            {
                if (o is SolidColorBrush brush)
                    onNext(brush.Color);
            });
}

file sealed class MainAppSplashScreen(MainWindow owner) : IApplicationSplashScreen
{
    private readonly MainWindow _owner = owner;

    public string? AppName => null;
    public IImage AppIcon =>
        new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaExampleProject/Assets/splashscreen.png")));
    public object? SplashScreenContent => null;
    public int MinimumShowTime => 700;

    public async Task RunTasks(CancellationToken cancellationToken)
    {
        await _owner.LoadAsync(cancellationToken);
    }
}
