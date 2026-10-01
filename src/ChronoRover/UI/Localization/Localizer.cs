using Microsoft.Extensions.Localization;

using System;

namespace ChronoRover.UI.Localization;

public static class Localizer<T>
{
    public static IStringLocalizer<T> Instance { get; private set; }

    public static void Initialize(IStringLocalizer<T> localizer)
    {
        if (Instance == null)
            Instance = localizer ?? throw new ArgumentNullException(nameof(localizer));
        else
            throw new InvalidOperationException("The localizer has already been initialized!");
    }
}