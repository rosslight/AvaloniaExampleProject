using System.ComponentModel;
using AvaloniaExampleProject.Utilities;

namespace AvaloniaExampleProject.Assets;

public partial class Resources : INotifyPropertyChanged
{
    private readonly PropertyChangedEventArgs _cultureChangedEventArgs = new(null);

    private Resources()
    {
        CultureChanged += (_, _) => PropertyChanged?.Invoke(this, _cultureChangedEventArgs);
    }

    public IObservable<string> Observe(Func<Resources, string> getter) => new ResourcesObservable(this, getter);

    public event PropertyChangedEventHandler? PropertyChanged;
}

file sealed class ResourcesObservable : IObservable<string>
{
    private readonly Lock _lock = new();
    private readonly List<IObserver<string>> _observers = [];
    private string _currentString;

    public ResourcesObservable(Resources resources, Func<Resources, string> getter)
    {
        _currentString = getter(resources);
        resources.CultureChanged += (_, _) =>
        {
            _currentString = getter(resources);
            lock (_lock)
            {
                for (int i = _observers.Count - 1; i >= 0; i--)
                    _observers[i].OnNext(_currentString);
            }
        };
    }

    public IDisposable Subscribe(IObserver<string> observer)
    {
        lock (_lock)
        {
            observer.OnNext(_currentString);
            _observers.Add(observer);
        }

        return Disposable.Create(
            (this, observer),
            static state =>
            {
                lock (state.Item1._lock)
                {
                    state.Item1._observers.Remove(state.observer);
                }
            }
        );
    }
}
