using Avalonia.Headless.XUnit;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;
using Microsoft.Extensions.DependencyInjection;
using static AvaloniaExampleProject.Tests.VerifyHelpers;

namespace AvaloniaExampleProject.Tests.ViewModels;

public class SettingsViewModelTests
{
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
}
