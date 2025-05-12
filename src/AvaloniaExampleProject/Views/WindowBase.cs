using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;

namespace AvaloniaExampleProject.Views;

/// <summary> A base class which provides a typed <see cref="ViewModel"/> property for a <see cref="Window"/> </summary>
/// <typeparam name="T"> The type of the ViewModel </typeparam>
public abstract class WindowBase<T> : Window
    where T : notnull
{
    /// <inheritdoc cref="UserControl.DataContext" />
    public new object? DataContext
    {
        get => base.DataContext;
        private set => base.DataContext = value;
    }

    /// <summary> The viewModel of the control. Setting the viewModel sets the DataContext as well. </summary>
    public required T ViewModel
    {
        get => (T)(base.DataContext ?? throw new ArgumentNullException());
        [MemberNotNull(nameof(base.DataContext))]
        init => DataContext = value;
    }
}
