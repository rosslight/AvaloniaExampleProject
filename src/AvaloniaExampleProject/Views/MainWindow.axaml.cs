using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
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
        SplashScreen = new MainAppSplashScreen(this);
    }

    internal async Task LoadAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        Dispatcher.UIThread.Invoke(() =>
        {
            Content = _provider.GetRequiredService<MainView>();
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
