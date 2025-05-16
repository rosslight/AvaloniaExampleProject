using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;

namespace AvaloniaExampleProject;

/// <summary>
/// A ViewLocator class. When added to the <see cref="Application.DataTemplates"/>, ViewModels are resolved to their corresponding view
/// </summary>
public sealed class ViewLocator(IServiceProvider serviceProvider) : IDataTemplate
{
    public Control Build(object? param)
    {
        return param switch
        {
            not ViewModelBase => new TextBlock { Text = $"No VM provided: {param?.GetType()}" },
            WelcomeViewModel vm => new WelcomeView { ViewModel = vm },
            SettingsViewModel vm => new SettingsView { ViewModel = vm },
            _ => new TextBlock { Text = $"VM Not Registered: {param.GetType()}" },
        };
    }

    public bool Match(object? data) => data is ViewModelBase;
}
