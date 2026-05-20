using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.Providers.Time.TimeWindow;

public abstract class MinuteEnumerable : IMinuteEnumerable
{
    protected DateTime Minute;

    protected abstract void Initialize();

    [SuppressMessage("ReSharper", "IteratorNeverReturns")]
    public IEnumerator<DateTime> GetEnumerator()
    {
        Initialize();

        while (true)
        {
            yield return Minute;
            Minute = Minute.AddMinutes(1);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}