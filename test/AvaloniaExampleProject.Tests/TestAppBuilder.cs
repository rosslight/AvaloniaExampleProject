using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Headless;
using AvaloniaExampleProject.Business;
using AvaloniaExampleProject.Models;
using AvaloniaExampleProject.Tests;
using Darp.Utils.Configuration;
using FluentAvalonia.Styling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace AvaloniaExampleProject.Tests;

public class TestAppBuilder
{
    [ModuleInitializer]
    public static void Init() => VerifierSettings.InitializePlugins();

    private static readonly MainConfig MainConfig = new()
    {
        UserPreferences = new UserPreferencesConfig { SelectedLanguage = "en-EN", SelectedTheme = "Default" },
    };

    public static IServiceProvider Services { get; private set; } = null!;

    public static IConfigurationService<MainConfig> SubstituteForMainConfigService()
    {
        IConfigurationService<MainConfig> mockConfig = Substitute.For<IConfigurationService<MainConfig>>();
        MainConfig tempConfig = MainConfig;
        mockConfig.Config.Returns(_ => tempConfig);
        mockConfig
            .WriteConfigurationAsync(Arg.Any<MainConfig>())
            .Returns(callInfo =>
            {
                tempConfig = callInfo.Arg<MainConfig>();
                return Task.FromResult(tempConfig);
            });
        mockConfig.LoadConfigurationAsync(Arg.Any<CancellationToken>()).Returns(_ => tempConfig);
        return mockConfig;
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        var appInformationService = Substitute.For<IAppInformationService>();
        appInformationService.Version.Returns("1.2.3-aabbccdd");
        IServiceProvider provider = new ServiceCollection()
            .AddLogging(builder => builder.AddXUnit())
            .AddSingleton(SubstituteForMainConfigService())
            .AddAppServices()
            .AddSingleton(appInformationService)
            .BuildServiceProvider();
        Services = provider;
        return AppBuilder
            .Configure(() => new App(provider))
            .UseSkia()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false })
            .AfterSetup(builder =>
            {
                if (builder.Instance is null)
                    throw new Exception("Instance is null");
                builder.Instance.Styles.Add(
                    new FluentAvaloniaTheme { PreferSystemTheme = false, PreferUserAccentColor = false }
                );
            });
    }
}
