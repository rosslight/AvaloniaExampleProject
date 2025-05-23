using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Media;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;
using FluentAvalonia.Styling;
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

    [AvaloniaFact]
    public Task Render_FluentAvalonia()
    {
        var control = new UserControl
        {
            Content = new TextBlock { Text = "This is some random text!" },
            Foreground = new SolidColorBrush(Color.Parse("Black")),
        };
        control.Background = new SolidColorBrush(0xFFFFFFFF);
        return VerifyControl(control);
    }
    /*

    [AvaloniaFact]
    public Task Render_Default()
    {
        var control = new UserControl
        {
            Content = new TextBlock { Text = "This is some random text!" },
            Foreground = new SolidColorBrush(Color.Parse("Black")),
        };
        control.Background = new SolidColorBrush(0xFFFFFFFF);
        var styles = Application.Current!.Styles;
        styles.RemoveAll(styles.OfType<FluentAvaloniaTheme>());
        return VerifyControl(control);
    }

    [AvaloniaFact]
    public Task Render_FontSize()
    {
        var control = new UserControl
        {
            Content = new TextBlock { Text = "This is some random text!" },
            FontSize = 14,
            Foreground = new SolidColorBrush(Color.Parse("Black")),
        };
        control.Background = new SolidColorBrush(0xFFFFFFFF);
        var styles = Application.Current!.Styles;
        styles.RemoveAll(styles.OfType<FluentAvaloniaTheme>());
        return VerifyControl(control);
    }

    [AvaloniaFact]
    public Task Render_FontWeight()
    {
        var control = new UserControl
        {
            Content = new TextBlock { Text = "This is some random text!" },
            FontWeight = FontWeight.SemiBold,
            Foreground = new SolidColorBrush(Color.Parse("Black")),
        };
        control.Background = new SolidColorBrush(0xFFFFFFFF);
        var styles = Application.Current!.Styles;
        styles.RemoveAll(styles.OfType<FluentAvaloniaTheme>());
        return VerifyControl(control);
    }

    [AvaloniaFact]
    public Task Render_FontSizeAndWeight()
    {
        var control = new UserControl
        {
            Content = new TextBlock { Text = "This is some random text!" },
            FontWeight = FontWeight.SemiBold,
            FontSize = 14,
        };
        var styles = Application.Current!.Styles;
        styles.RemoveAll(styles.OfType<FluentAvaloniaTheme>());
        return VerifyControl(control);
    }
    */
}
