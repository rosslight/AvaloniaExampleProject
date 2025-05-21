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
        var viewModel = TestAppBuilder.Services.GetRequiredService<SettingsViewModel>();
        var control = new SettingsView { ViewModel = viewModel };
        return VerifyControl(control).ScrubMembersWithType<Resources>();
    }
}
