using ChronoRover.Helpers;

using NUnit.Framework;

using System;

namespace ChronoRover.Tests.Helpers;

[TestFixture]
public class BitHelperTests
{
    [Test]
    [TestCaseSource(nameof(GetBcd1ReturnsCorrectValueTestData))]
    public void Bcd1ReturnsCorrectValue(int value, int pos, bool expectedResult)
    {
        var result = BitHelper.Bcd1(value, pos);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCaseSource(nameof(GetBcd10ReturnsCorrectValueTestData))]
    public void Bcd10ReturnsCorrectValue(int value, int pos, bool expectedResult)
    {
        var result = BitHelper.Bcd10(value, pos);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCaseSource(nameof(GetBcd100ReturnsCorrectValueTestData))]
    public void Bcd100ReturnsCorrectValue(int value, int pos, bool expectedResult)
    {
        var result = BitHelper.Bcd100(value, pos);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCaseSource(nameof(GetParityReturnsCorrectValueTestData))]
    public void ParityReturnsCorrectValue(bool[] values, bool expectedResult)
    {
        var result = BitHelper.Parity(values, values.Length);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    #region Test data

    private static object[] GetBcd1ReturnsCorrectValueTestData()
    {
        object[] a1 = [01, 0, true];
        object[] a2 = [04, 0, false];
        object[] a3 = [23, 1, true];
        object[] a4 = [20, 1, false];
        object[] a5 = [54, 2, true];
        object[] a6 = [09, 2, false];
        object[] a7 = [38, 3, true];
        object[] a8 = [14, 3, false];

        return [a1, a2, a3, a4, a5, a6, a7, a8,];
    }

    private static object[] GetBcd10ReturnsCorrectValueTestData()
    {
        object[] a1 = [19, 0, true];
        object[] a2 = [20, 0, false];
        object[] a3 = [21, 1, true];
        object[] a4 = [05, 1, false];
        object[] a5 = [54, 2, true];
        object[] a6 = [09, 2, false];
        object[] a7 = [80, 3, true];
        object[] a8 = [14, 3, false];

        return [a1, a2, a3, a4, a5, a6, a7, a8,];
    }

    private static object[] GetBcd100ReturnsCorrectValueTestData()
    {
        object[] a1 = [340, 0, true];
        object[] a2 = [265, 0, false];
        object[] a3 = [340, 1, true];
        object[] a4 = [121, 1, false];

        return [a1, a2, a3, a4,];
    }

    private static object[] GetParityReturnsCorrectValueTestData()
    {
        object[] t00 = [new[] { true }, true];
        object[] t01 = [new[] { true, true, true }, true];
        object[] t02 = [new[] { false, true, true, false, true }, true];

        object[] f00 = [Array.Empty<bool>(), false];
        object[] f01 = [new[] { false, false }, false];
        object[] f02 = [new[] { true, true }, false];
        object[] f03 = [new[] { true, true, true, true }, false];

        return
        [
            t00, t01, t02,
            f00, f01, f02, f03,
        ];
    }

    #endregion Test data
}