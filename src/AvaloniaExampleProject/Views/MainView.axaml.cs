using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using AvaloniaExampleProject.Business;
using AvaloniaExampleProject.ViewModels;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AvaloniaExampleProject.Views;

public partial class MainView : UserControlBase<MainViewModel>
{
    private readonly ILogger<MainView> _logger;

    public MainView(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<MainView>>();
        InitializeComponent();
        MainFrame.NavigationPageFactory = new DependencyInjectionPageFactory(serviceProvider);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        object? firstItem = NavView.MenuItems.FirstOrDefault();
        if (firstItem is not null)
            NavView.SelectedItem = firstItem;
    }

    private void TabStripControl_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        switch (e.SelectedItemContainer?.Tag)
        {
            case "Settings":
                MainFrame.Navigate(typeof(SettingsViewModel));
                break;
            case Type type:
                MainFrame.Navigate(type);
                break;
        }
    }

    private void MainFrame_OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        _logger.LogError(
            e.Exception,
            "Navigation to {Type} has failed because of {Message}",
            e.SourcePageType,
            e.Exception.Message
        );
    }
}

file sealed class DependencyInjectionPageFactory(IServiceProvider serviceProvider) : INavigationPageFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Control GetPage(Type srcType)
    {
        return new UserControl { Content = _serviceProvider.GetRequiredService(srcType) };
    }

    public Control GetPageFromObject(object target) => throw new NotSupportedException();
}
