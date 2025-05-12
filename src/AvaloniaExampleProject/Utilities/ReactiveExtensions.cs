namespace AvaloniaExampleProject.Utilities;

/// <summary> Used only because no reference to System.Reactive is necessary at this moment </summary>
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

    public static IObservable<(T1 First, T2 Second)> CombineLatest<T1, T2>(
        this IObservable<T1> source,
        IObservable<T2> other
    ) => source.CombineLatest(other, (t1, t2) => (t1, t2));

    public static IObservable<TCombined> CombineLatest<T1, T2, TCombined>(
        this IObservable<T1> source,
        IObservable<T2> other,
        Func<T1, T2, TCombined> onCombined
    )
    {
        T1? currentT1 = default;
        bool hasT1Value = false;
        T2? currentT2 = default;
        bool hasT2Value = false;
        return Observable.Create<TCombined>(observer =>
        {
            var sub1 = source.Subscribe(
                value =>
                {
                    currentT1 = value;
                    hasT1Value = true;
                    if (hasT2Value)
                        observer.OnNext(onCombined(currentT1, currentT2!));
                },
                observer.OnError,
                observer.OnCompleted
            );
            var sub2 = other.Subscribe(
                value =>
                {
                    currentT2 = value;
                    hasT2Value = true;
                    if (hasT1Value)
                        observer.OnNext(onCombined(currentT1!, currentT2));
                },
                observer.OnError,
                observer.OnCompleted
            );
            return new CombinedDisposable(sub1, sub2);
        });
    }
}

internal static class Observable
{
    public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> onSubscribe) =>
        new FuncObservable<T>(onSubscribe);
}

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

file sealed class CombinedDisposable(IDisposable disposable1, IDisposable disposable2) : IDisposable
{
    private readonly IDisposable _disposable1 = disposable1;
    private readonly IDisposable _disposable2 = disposable2;

    public void Dispose()
    {
        _disposable1.Dispose();
        _disposable2.Dispose();
    }
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
