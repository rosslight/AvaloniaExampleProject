﻿using Avalonia;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject.Desktop;

internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        var provider = new ServiceCollection().AddAppServices().BuildServiceProvider();
        return AppBuilder.Configure(() => new App(provider)).UsePlatformDetect().WithInterFont().LogToTrace();
    }
}
