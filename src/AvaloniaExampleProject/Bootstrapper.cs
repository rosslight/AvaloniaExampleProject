using System.Diagnostics.CodeAnalysis;
using AvaloniaExampleProject.ViewModels;
using AvaloniaExampleProject.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaExampleProject;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddViewModel<MainWindow, MainWindowViewModel>();
    }

    private static IServiceCollection AddViewModel<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TView,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel
    >(this IServiceCollection serviceCollection)
        where TView : class, IViewBase<TViewModel>, new()
        where TViewModel : class
    {
        return serviceCollection
            .AddTransient<TViewModel>()
            .AddTransient<TView>(provider => new TView { ViewModel = provider.GetRequiredService<TViewModel>() });
    }
}
