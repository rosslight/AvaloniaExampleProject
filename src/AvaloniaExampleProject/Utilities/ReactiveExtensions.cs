namespace AvaloniaExampleProject.Utilities;

internal static class ReactiveExtensions
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext) =>
        observable.Subscribe(new FuncSubscriber<T>(onNext));
}

file sealed class FuncSubscriber<T>(Action<T> onNext) : IObserver<T>
{
    private readonly Action<T> _onNext = onNext;

    public void OnNext(T value) => _onNext(value);

    public void OnCompleted() { }

    public void OnError(Exception error) => throw error;
}
