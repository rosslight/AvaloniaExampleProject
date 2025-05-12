using System.Diagnostics.CodeAnalysis;
using AvaloniaExampleProject.Assets;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection serviceCollection) =>
        serviceCollection.AddViewModel<MainView, MainViewModel>().AddLocalization();

    private static IServiceCollection AddViewModel<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TView,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel
    >(this IServiceCollection serviceCollection)
        where TView : UserControlBase<TViewModel>, new()
        where TViewModel : ViewModelBase
    {
        return serviceCollection
            .AddTransient<TViewModel>()
            .AddTransient<TView>(provider => new TView { ViewModel = provider.GetRequiredService<TViewModel>() });
    }

    private static IServiceCollection AddLocalization(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton(Resources.Default);
}
