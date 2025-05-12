namespace AvaloniaExampleProject.Utilities;

/// <summary> Used only because a reference to System.Reactive is unnecessary at this moment </summary>
internal static class ReactiveExtensions
{
    public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext) =>
        source.Subscribe(new FuncSubscriber<T>(onNext, null, null));

    public static IDisposable Subscribe<T>(
        this IObservable<T> source,
        Action<T> onNext,
        Action<Exception> onError,
        Action onCompleted
    ) => source.Subscribe(new FuncSubscriber<T>(onNext, onError, onCompleted));

    public static IObservable<TResult> Select<T, TResult>(this IObservable<T> source, Func<T, TResult> onSelect)
    {
        return Observable.Create<TResult>(observer =>
            source.Subscribe(value => observer.OnNext(onSelect(value)), observer.OnError, observer.OnCompleted)
        );
    }
}

/// <summary> Used only because a reference to System.Reactive is unnecessary at this moment </summary>
internal static class Observable
{
    public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> onSubscribe) =>
        new FuncObservable<T>(onSubscribe);
}

/// <summary> Used only because a reference to System.Reactive is unnecessary at this moment </summary>
internal sealed class Disposable : IDisposable
{
    public static readonly IDisposable Empty = new Disposable();

    public void Dispose() { }

    public static IDisposable Create<T>(T state, Action<T> onDispose) => new FuncDisposable<T>(state, onDispose);

    public static IDisposable Create(Action onDispose) => new FuncDisposable<Action>(onDispose, a => a());
}

file sealed class FuncDisposable<T>(T state, Action<T> onDispose) : IDisposable
{
    private readonly T _state = state;
    private readonly Action<T> _onDispose = onDispose;

    public void Dispose() => _onDispose(_state);
}

file sealed class FuncObservable<T>(Func<IObserver<T>, IDisposable> onSubscribe) : IObservable<T>
{
    private readonly Func<IObserver<T>, IDisposable> _onSubscribe = onSubscribe;

    public IDisposable Subscribe(IObserver<T> observer) => _onSubscribe(observer);
}

file sealed class FuncSubscriber<T>(Action<T> onNext, Action<Exception>? onError, Action? onCompleted) : IObserver<T>
{
    private readonly Action<T> _onNext = onNext;
    private readonly Action<Exception>? _onError = onError;
    private readonly Action? _onCompleted = onCompleted;

    public void OnNext(T value) => _onNext(value);

    public void OnCompleted() => _onCompleted?.Invoke();

    public void OnError(Exception error)
    {
        if (_onError is null)
            throw error;
        _onError(error);
    }
}
