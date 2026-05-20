using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Runtime.CompilerServices;

namespace ChronoRover.UI.Signal.ViewModels;

public abstract class ValuesViewModel<T> : ObservableObject
{
    protected T[] Values = [];

    public Action OnValuesUpdated { get; set; }

    public T[] GetValues() => Values;

    public void PrependValue(T value)
    {
        Array.Copy(Values, 0, Values, 1, Values.Length - 1); // shift right
        Values[0] = value;

        OnValuesUpdated?.Invoke();
    }

    public void SetValues(T[] values)
    {
        Array.Copy(values, 0, Values, 0, values.Length);

        OnValuesUpdated?.Invoke();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearValues()
    {
        for (var i = 0; i <= Values.Length - 1; i++)
        {
            Values[i] = default;
        }

        OnValuesUpdated?.Invoke();
    }
}