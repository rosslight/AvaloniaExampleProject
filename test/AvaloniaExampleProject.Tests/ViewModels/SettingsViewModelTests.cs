﻿using System.Globalization;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Avalonia.Headless.XUnit;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.Business;
using AvaloniaExampleProject.Models;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;
using Darp.Utils.Assets;
using Darp.Utils.Assets.Abstractions;
using Darp.Utils.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using static AvaloniaExampleProject.Tests.VerifyHelpers;

namespace AvaloniaExampleProject.Tests.ViewModels;

public class SettingsViewModelTests
{
    private readonly IConfigurationService<MainConfig> _configurationService;
    private readonly ServiceProvider _services;

    public SettingsViewModelTests()
    {
        _services = new ServiceCollection()
            .AddSingleton<IAssetsService>(new MemoryAssetsService("path/to/config"))
            .AddConfigurationFile<MainConfig, IAssetsService>("config.json")
            .AddAppServices()
            .AddLogging()
            .BuildServiceProvider();
        _configurationService = _services.GetRequiredService<IConfigurationService<MainConfig>>();
    }

    [AvaloniaFact]
    public Task Render()
    {
        var control = new SettingsView { ViewModel = TestAppBuilder.Services.GetRequiredService<SettingsViewModel>() };
        return VerifyControl(control).ScrubMembersWithType<Resources>();
    }

    [AvaloniaFact]
    public Task Render_AboutExpanded()
    {
        var control = new SettingsView
        {
            ViewModel = TestAppBuilder.Services.GetRequiredService<SettingsViewModel>(),
            AboutSettingsExpander = { IsExpanded = true },
        };
        return VerifyControl(control);
    }

    [Fact(Timeout = 1000)]
    public async Task ThemeChange()
    {
        const string newTheme = ThemeService.LightTheme;
        var tempConfig = new MainConfig(new UserPreferencesConfig("en", ThemeService.DarkTheme));
        await _configurationService.WriteConfigurationAsync(tempConfig);
        var viewModel = _services.GetRequiredService<SettingsViewModel>();
        var themeService = _services.GetRequiredService<IThemeService>();

        var themeTask = themeService.RequestedThemeVariant.FirstAsync().ToTask();
        viewModel.SelectedTheme = newTheme;
        var observedThemeVariant = await themeTask;

        observedThemeVariant.Key.ShouldBe(newTheme);
        _configurationService.Config.UserPreferences.SelectedTheme.ShouldBe(newTheme);
        viewModel.SelectedTheme.ShouldBe(newTheme);
    }

    [Fact(Timeout = 1000)]
    public async Task LanguageChange()
    {
        var newLanguage = CultureInfo.GetCultureInfo(Resources.Languages.German);
        var tempConfig = new MainConfig(new UserPreferencesConfig("en", ThemeService.DarkTheme));
        await _configurationService.WriteConfigurationAsync(tempConfig);
        var viewModel = _services.GetRequiredService<SettingsViewModel>();
        var i18N = _services.GetRequiredService<Resources>();
        i18N.Culture = new CultureInfo(Resources.Languages.Default);

        var configTask = _configurationService.Observe(x => x.UserPreferences.SelectedLanguage).FirstAsync().ToTask();
        viewModel.SelectedLanguage = newLanguage;
        string observedLanguage = await configTask;

        i18N.Settings_Title.ShouldBe("Einstellungen");
        observedLanguage.ShouldBe(newLanguage.Name);
        _configurationService.Config.UserPreferences.SelectedLanguage.ShouldBe(newLanguage.Name);
        viewModel.SelectedLanguage.ShouldBe(newLanguage);
    }
}
