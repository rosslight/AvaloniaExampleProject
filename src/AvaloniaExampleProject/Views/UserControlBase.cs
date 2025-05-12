using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;

namespace AvaloniaExampleProject.Views;

/// <summary> A base class which provides a typed <see cref="ViewModel"/> property for an <see cref="UserControl"/> </summary>
/// <typeparam name="T"> The type of the ViewModel </typeparam>
public abstract class UserControlBase<T> : UserControl, IViewBase<T>
    where T : notnull
{
    /// <inheritdoc cref="UserControl.DataContext" />
    public new object? DataContext
    {
        get => base.DataContext;
        private set => base.DataContext = value;
    }

    /// <inheritdoc />
    public required T ViewModel
    {
        get =>
            (T)(
                base.DataContext
                ?? throw new ArgumentNullException(
                    nameof(DataContext),
                    "Cannot retrieve ViewModel. The DataContext is null"
                )
            );
        [MemberNotNull(nameof(base.DataContext))]
        init => DataContext = value;
    }
}
