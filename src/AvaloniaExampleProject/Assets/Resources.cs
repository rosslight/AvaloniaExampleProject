using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reactive.Disposables;

namespace AvaloniaExampleProject.Assets;

public partial class Resources : INotifyPropertyChanged
{
    private readonly PropertyChangedEventArgs _cultureChangedEventArgs = new(null);

    /// <summary> All cultures available </summary>
    public static IReadOnlyList<CultureInfo> AvailableCultures { get; } =
        [CultureInfo.GetCultureInfo(Languages.Default), CultureInfo.GetCultureInfo(Languages.German)];

    /// <summary> Get a supported <see cref="CultureInfo"/> for the resources </summary>
    /// <param name="languageName"> The language name (e.g. 'en-US' or 'de') </param>
    /// <returns> The matching CultureInfo or the fallback culture </returns>
    public static CultureInfo GetCulture(string languageName)
    {
        string lowered = languageName.ToLowerInvariant();
        return AvailableCultures.FirstOrDefault(x => x.Name == lowered)
            ?? CultureInfo.GetCultureInfo(Languages.Default);
    }

    internal Resources()
    {
        CultureChanged += (_, _) => PropertyChanged?.Invoke(this, _cultureChangedEventArgs);
    }

    /// <summary> Observe the value of a certain resource </summary>
    /// <remarks> After subscription, the current value is published immediately </remarks>
    /// <param name="getter"> The getter for the resource </param>
    /// <returns> An observable that provides the current value of a resource </returns>
    public IObservable<string> Observe(Func<Resources, string> getter) => new ResourcesObservable(this, getter);

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary> All available languages </summary>
    public static class Languages
    {
        /// <summary> English </summary>
        /// <remarks> This is the base culture </remarks>
        public const string Default = "en";

        /// <summary> German (Standard) </summary>
        public const string German = "de";
    }
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
