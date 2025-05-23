using Avalonia.Headless.XUnit;
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
        return VerifyControl(control);
    }
}
