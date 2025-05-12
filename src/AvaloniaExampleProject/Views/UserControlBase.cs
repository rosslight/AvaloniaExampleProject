using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using AvaloniaExampleProject.ViewModels;

namespace AvaloniaExampleProject.Views;

/// <summary> A base class which provides a typed <see cref="ViewModel"/> property for an <see cref="UserControl"/> </summary>
/// <typeparam name="T"> The type of the ViewModel </typeparam>
public abstract class UserControlBase<T> : UserControl
    where T : ViewModelBase
{
    private const string DataContextNullMessage = "Cannot retrieve ViewModel. The DataContext is null";

    /// <inheritdoc cref="UserControl.DataContext" />
    public new object? DataContext
    {
        get => base.DataContext;
        private set => base.DataContext = value;
    }

    /// <summary> The viewModel of the control. Setting the viewModel sets the DataContext as well. </summary>
    public T ViewModel
    {
        get => (T)(base.DataContext ?? throw new ArgumentNullException(nameof(DataContext), DataContextNullMessage));
        [MemberNotNull(nameof(base.DataContext))]
        init => DataContext = value;
    }
}
