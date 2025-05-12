using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;

namespace AvaloniaExampleProject;

public sealed class ViewLocator : IDataTemplate
{
    public Control Build(object? param) =>
        param switch
        {
            not ViewModelBase => new TextBlock { Text = $"No VM provided: {param?.GetType()}" },
            MainWindowViewModel vm => new MainWindow { ViewModel = vm },
            _ => new TextBlock { Text = $"VM Not Registered: {param.GetType()}" },
        };

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
