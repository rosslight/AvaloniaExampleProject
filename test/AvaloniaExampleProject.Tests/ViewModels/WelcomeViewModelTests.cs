using Avalonia.Headless.XUnit;
using Avalonia.Media;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;
using Microsoft.Extensions.DependencyInjection;
using static AvaloniaExampleProject.Tests.VerifyHelpers;

namespace AvaloniaExampleProject.Tests.ViewModels;

public sealed class WelcomeViewModelTests
{
    [AvaloniaFact]
    public Task Render()
    {
        var viewModel = TestAppBuilder.Services.GetRequiredService<WelcomeViewModel>();
        var control = new WelcomeView { ViewModel = viewModel };
        control.FontFamily = new FontFamily("Inter");
        return VerifyControl(control);
    }
}
