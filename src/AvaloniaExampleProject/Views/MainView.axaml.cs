using Avalonia.Controls;
using Avalonia.Data.Converters;
using AvaloniaExampleProject.Business;
using AvaloniaExampleProject.ViewModels;
using FluentAvalonia.UI.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject.Views;

public partial class MainView : UserControlBase<MainViewModel>
{
    public static readonly IValueConverter ThemeTranslationConverter = new FuncValueConverter<string, string>(s =>
        s switch
        {
            ThemeService.DefaultTheme => Assets.Resources.Default.Settings_Theme_Default,
            ThemeService.LightTheme => Assets.Resources.Default.Settings_Theme_Light,
            ThemeService.DarkTheme => Assets.Resources.Default.Settings_Theme_Dark,
            _ => "n/a",
        }
    );

    public MainView(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        MainFrame.NavigationPageFactory = new DependencyInjectionPageFactory(serviceProvider);
    }

    private void TabStripControl_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        switch (e.SelectedItemContainer.Tag)
        {
            case "Settings":
                MainFrame.Navigate(typeof(SettingsViewModel));
                break;
            case Type type:
                MainFrame.Navigate(type);
                break;
        }
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
