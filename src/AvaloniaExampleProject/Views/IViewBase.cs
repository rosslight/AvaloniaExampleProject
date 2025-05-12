namespace AvaloniaExampleProject.Views;

/// <summary> The base interface for each view with a corresponding ViewModel </summary>
/// <typeparam name="T"> The type of the ViewModel </typeparam>
public interface IViewBase<T>
{
    /// <summary> The viewModel of the control. Setting the viewModel sets the DataContext as well. </summary>
    T ViewModel { get; init; }
}
