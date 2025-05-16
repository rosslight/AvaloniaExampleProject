using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AvaloniaExampleProject;

public sealed class App : Application
{
    private readonly IServiceProvider _provider;

    public App(IServiceProvider provider)
    {
        _provider = provider;
        DataTemplates.Add(new ViewLocator(provider));
    }

    public static string Version { get; } =
        typeof(App).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? throw new VersionNotFoundException("Could not get version");

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
#if DEBUG
        this.AttachDeveloperTools(options =>
            options.AddMicrosoftLoggerObservable(_provider.GetRequiredService<ILoggerFactory>())
        );
#endif
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var i18N = _provider.GetRequiredService<Resources>();
        i18N.Culture = new CultureInfo("de-de");
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow(_provider);
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary> Avoid duplicate validations from both Avalonia and the CommunityToolkit. </summary>
    /// <seealso href="https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins"/>
    [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "We are only removing validators")]
    private static void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove = BindingPlugins
            .DataValidators.OfType<DataAnnotationsValidationPlugin>()
            .ToArray();

        // remove each entry found
        foreach (DataAnnotationsValidationPlugin plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
