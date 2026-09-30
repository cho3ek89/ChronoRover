using Avalonia.Markup.Xaml;

using ChronoRover.UI.Localization;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Extensions;

public class LocalizationExtension(string key) : MarkupExtension
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public string Key { get; } = key;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var result = Localizer<App>.Instance[Key];
        return result.Value;
    }
}