using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using AvaloniaExampleProject.Utilities;
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

        ConfigureTitleBar();

        SplashScreen = new MainAppSplashScreen(this);
    }

    private void ConfigureTitleBar()
    {
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        TitleBar.Height = 44;

        // Update background colors
        _ = Resources
            .GetResourceObservable("AppBarButtonBackgroundPointerOver")
            .Subscribe(o =>
            {
                if (o is not SolidColorBrush brush)
                    return;
                TitleBar.ButtonHoverBackgroundColor = brush.Color;
            });
        _ = Resources
            .GetResourceObservable("AppBarButtonBackgroundPressed")
            .Subscribe(o =>
            {
                if (o is not SolidColorBrush brush)
                    return;
                TitleBar.ButtonPressedBackgroundColor = brush.Color;
            });
    }

    internal async Task LoadAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        Dispatcher.UIThread.Invoke(() =>
        {
            WindowContent.Content = _provider.GetRequiredService<MainView>();
        });
    }
}

file sealed class MainAppSplashScreen(MainWindow owner) : IApplicationSplashScreen
{
    private readonly MainWindow _owner = owner;

    public string? AppName => null;
    public IImage AppIcon =>
        new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaExampleProject/Assets/splashscreen.png")));
    public object? SplashScreenContent => null;
    public int MinimumShowTime => 1000;

    public async Task RunTasks(CancellationToken cancellationToken)
    {
        await _owner.LoadAsync(cancellationToken);
    }
}
