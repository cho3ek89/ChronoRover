using ChronoRover.UI.Localization;

using Microsoft.Extensions.Localization;

using Moq;

using NUnit.Framework;

using System;

namespace ChronoRover.Tests.UI.Localization;

[TestFixture]
public class LocalizerTests
{
    [Test]
    public void InitializeThrowsExceptionForNullArgument()
    {
        Assert.Throws<ArgumentNullException>(() => { Localizer<object>.Initialize(null); });
    }

    [Test]
    public void InitializeThrowsExceptionIfCalledForSecondTime()
    {
        var localizer = Mock.Of<IStringLocalizer<object>>();

        Assert.DoesNotThrow(() => Localizer<object>.Initialize(localizer));

        Assert.AreEqual(localizer, Localizer<object>.Instance);

        Assert.Throws<InvalidOperationException>(
            () => Localizer<object>.Initialize(localizer),
            "The localizer has already been initialized!");
    }
}