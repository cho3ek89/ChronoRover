using ChronoRover.Providers.Signal;
using ChronoRover.Tests.TestUtils;

using NUnit.Framework;

using System;
using System.Collections.Generic;

namespace ChronoRover.Tests.Providers.Signal;

[TestFixture]
public class Dcf77SignalProviderTests
{
    [Test]
    [TestCaseSource(nameof(GetDstBitsAreCorrectTestData))]
    public void DstBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[17..19], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetMinuteBitsAreCorrectTestData))]
    public void MinuteBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[21..28], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetMinuteParityBitIsCorrectTestData))]
    public void MinuteParityBitIsCorrect(DateTime date, bool expectedBit)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[28], Is.EqualTo(expectedBit));
    }

    [Test]
    [TestCaseSource(nameof(GetHourBitsAreCorrectTestData))]
    public void HourBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[29..35], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetHourParityBitIsCorrectTestData))]
    public void HourParityBitIsCorrect(DateTime date, bool expectedBit)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[35], Is.EqualTo(expectedBit));
    }

    [Test]
    [TestCaseSource(nameof(GetDayBitsAreCorrectTestData))]
    public void DayBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[36..42], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetWeekDayBitsAreCorrectTestData))]
    public void WeekDayBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[42..45], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetMonthBitsAreCorrectTestData))]
    public void MonthBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[45..50], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetYearBitsAreCorrectTestData))]
    public void YearBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[50..58], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetDateParityBitIsCorrectTestData))]
    public void DateParityBitIsCorrect(DateTime date, bool expectedBit)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[58], Is.EqualTo(expectedBit));
    }

    [Test]
    [TestCaseSource(nameof(GetDateTimeAgnosticBitsAreCorrectTestData))]
    public void DateTimeAgnosticBitsAreCorrect(DateTime date)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[..17], Has.All.EqualTo(false));
        Assert.That(bits[19], Is.EqualTo(false));
        Assert.That(bits[20], Is.EqualTo(true));
        Assert.That(bits[59], Is.EqualTo(false));
    }

    [Test]
    [TestCaseSource(nameof(GetBitsArrayLengthIsAlways60TestData))]
    public void BitsArrayLengthIsAlways60(DateTime date)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits.Length, Is.EqualTo(60));
    }

    private static bool[] GetMinuteSignal(DateTime date)
    {
        var provider = new Dcf77SignalProvider(
            TimeZoneUtils.GetDefaultTimeZoneProvider());

        return provider.GetMinuteSignal(date);
    }

    #region Test data

    private static object[] GetDstBitsAreCorrectTestData()
    {
        object[] tc1 = [GetDate(3), new[] { false, true }]; // no DST
        object[] tc2 = [GetDate(4), new[] { true, false }]; // is DST

        return [tc1, tc2];

        static DateTime GetDate(int month) => new(2000, month, 1);
    }

    private static object[] GetMinuteBitsAreCorrectTestData()
    {
        object[] m00 = [GetDate(00), new[] { false, false, false, false, false, false, false }];
        object[] m01 = [GetDate(01), new[] { true, false, false, false, false, false, false }];
        object[] m02 = [GetDate(02), new[] { false, true, false, false, false, false, false }];
        object[] m03 = [GetDate(03), new[] { true, true, false, false, false, false, false }];
        object[] m04 = [GetDate(04), new[] { false, false, true, false, false, false, false }];
        object[] m05 = [GetDate(05), new[] { true, false, true, false, false, false, false }];
        object[] m06 = [GetDate(06), new[] { false, true, true, false, false, false, false }];
        object[] m07 = [GetDate(07), new[] { true, true, true, false, false, false, false }];
        object[] m08 = [GetDate(08), new[] { false, false, false, true, false, false, false }];
        object[] m09 = [GetDate(09), new[] { true, false, false, true, false, false, false }];
        object[] m10 = [GetDate(10), new[] { false, false, false, false, true, false, false }];
        object[] m11 = [GetDate(11), new[] { true, false, false, false, true, false, false }];
        object[] m12 = [GetDate(12), new[] { false, true, false, false, true, false, false }];
        object[] m13 = [GetDate(13), new[] { true, true, false, false, true, false, false }];
        object[] m14 = [GetDate(14), new[] { false, false, true, false, true, false, false }];
        object[] m15 = [GetDate(15), new[] { true, false, true, false, true, false, false }];
        object[] m16 = [GetDate(16), new[] { false, true, true, false, true, false, false }];
        object[] m17 = [GetDate(17), new[] { true, true, true, false, true, false, false }];
        object[] m18 = [GetDate(18), new[] { false, false, false, true, true, false, false }];
        object[] m19 = [GetDate(19), new[] { true, false, false, true, true, false, false }];
        object[] m20 = [GetDate(20), new[] { false, false, false, false, false, true, false }];
        object[] m21 = [GetDate(21), new[] { true, false, false, false, false, true, false }];
        object[] m22 = [GetDate(22), new[] { false, true, false, false, false, true, false }];
        object[] m23 = [GetDate(23), new[] { true, true, false, false, false, true, false }];
        object[] m24 = [GetDate(24), new[] { false, false, true, false, false, true, false }];
        object[] m25 = [GetDate(25), new[] { true, false, true, false, false, true, false }];
        object[] m26 = [GetDate(26), new[] { false, true, true, false, false, true, false }];
        object[] m27 = [GetDate(27), new[] { true, true, true, false, false, true, false }];
        object[] m28 = [GetDate(28), new[] { false, false, false, true, false, true, false }];
        object[] m29 = [GetDate(29), new[] { true, false, false, true, false, true, false }];
        object[] m30 = [GetDate(30), new[] { false, false, false, false, true, true, false }];
        object[] m31 = [GetDate(31), new[] { true, false, false, false, true, true, false }];
        object[] m32 = [GetDate(32), new[] { false, true, false, false, true, true, false }];
        object[] m33 = [GetDate(33), new[] { true, true, false, false, true, true, false }];
        object[] m34 = [GetDate(34), new[] { false, false, true, false, true, true, false }];
        object[] m35 = [GetDate(35), new[] { true, false, true, false, true, true, false }];
        object[] m36 = [GetDate(36), new[] { false, true, true, false, true, true, false }];
        object[] m37 = [GetDate(37), new[] { true, true, true, false, true, true, false }];
        object[] m38 = [GetDate(38), new[] { false, false, false, true, true, true, false }];
        object[] m39 = [GetDate(39), new[] { true, false, false, true, true, true, false }];
        object[] m40 = [GetDate(40), new[] { false, false, false, false, false, false, true }];
        object[] m41 = [GetDate(41), new[] { true, false, false, false, false, false, true }];
        object[] m42 = [GetDate(42), new[] { false, true, false, false, false, false, true }];
        object[] m43 = [GetDate(43), new[] { true, true, false, false, false, false, true }];
        object[] m44 = [GetDate(44), new[] { false, false, true, false, false, false, true }];
        object[] m45 = [GetDate(45), new[] { true, false, true, false, false, false, true }];
        object[] m46 = [GetDate(46), new[] { false, true, true, false, false, false, true }];
        object[] m47 = [GetDate(47), new[] { true, true, true, false, false, false, true }];
        object[] m48 = [GetDate(48), new[] { false, false, false, true, false, false, true }];
        object[] m49 = [GetDate(49), new[] { true, false, false, true, false, false, true }];
        object[] m50 = [GetDate(50), new[] { false, false, false, false, true, false, true }];
        object[] m51 = [GetDate(51), new[] { true, false, false, false, true, false, true }];
        object[] m52 = [GetDate(52), new[] { false, true, false, false, true, false, true }];
        object[] m53 = [GetDate(53), new[] { true, true, false, false, true, false, true }];
        object[] m54 = [GetDate(54), new[] { false, false, true, false, true, false, true }];
        object[] m55 = [GetDate(55), new[] { true, false, true, false, true, false, true }];
        object[] m56 = [GetDate(56), new[] { false, true, true, false, true, false, true }];
        object[] m57 = [GetDate(57), new[] { true, true, true, false, true, false, true }];
        object[] m58 = [GetDate(58), new[] { false, false, false, true, true, false, true }];
        object[] m59 = [GetDate(59), new[] { true, false, false, true, true, false, true }];

        return
        [
            m00, m01, m02, m03, m04, m05, m06, m07, m08, m09,
            m10, m11, m12, m13, m14, m15, m16, m17, m18, m19,
            m20, m21, m22, m23, m24, m25, m26, m27, m28, m29,
            m30, m31, m32, m33, m34, m35, m36, m37, m38, m39,
            m40, m41, m42, m43, m44, m45, m46, m47, m48, m49,
            m50, m51, m52, m53, m54, m55, m56, m57, m58, m59,
        ];

        static DateTime GetDate(int minute) => new(2000, 1, 1, 1, minute, 1);
    }

    private static object[] GetMinuteParityBitIsCorrectTestData()
    {
        object[] m00 = [GetDate(00), false];
        object[] m01 = [GetDate(01), true];

        return [m00, m01];

        static DateTime GetDate(int minute) => new(2000, 1, 1, 1, minute, 1);
    }

    private static object[] GetHourBitsAreCorrectTestData()
    {
        object[] h00 = [GetDate(00), new[] { false, false, false, false, false, false }];
        object[] h01 = [GetDate(01), new[] { true, false, false, false, false, false }];
        object[] h02 = [GetDate(02), new[] { false, true, false, false, false, false }];
        object[] h03 = [GetDate(03), new[] { true, true, false, false, false, false }];
        object[] h04 = [GetDate(04), new[] { false, false, true, false, false, false }];
        object[] h05 = [GetDate(05), new[] { true, false, true, false, false, false }];
        object[] h06 = [GetDate(06), new[] { false, true, true, false, false, false }];
        object[] h07 = [GetDate(07), new[] { true, true, true, false, false, false }];
        object[] h08 = [GetDate(08), new[] { false, false, false, true, false, false }];
        object[] h09 = [GetDate(09), new[] { true, false, false, true, false, false }];
        object[] h10 = [GetDate(10), new[] { false, false, false, false, true, false }];
        object[] h11 = [GetDate(11), new[] { true, false, false, false, true, false }];
        object[] h12 = [GetDate(12), new[] { false, true, false, false, true, false }];
        object[] h13 = [GetDate(13), new[] { true, true, false, false, true, false }];
        object[] h14 = [GetDate(14), new[] { false, false, true, false, true, false }];
        object[] h15 = [GetDate(15), new[] { true, false, true, false, true, false }];
        object[] h16 = [GetDate(16), new[] { false, true, true, false, true, false }];
        object[] h17 = [GetDate(17), new[] { true, true, true, false, true, false }];
        object[] h18 = [GetDate(18), new[] { false, false, false, true, true, false }];
        object[] h19 = [GetDate(19), new[] { true, false, false, true, true, false }];
        object[] h20 = [GetDate(20), new[] { false, false, false, false, false, true }];
        object[] h21 = [GetDate(21), new[] { true, false, false, false, false, true }];
        object[] h22 = [GetDate(22), new[] { false, true, false, false, false, true }];
        object[] h23 = [GetDate(23), new[] { true, true, false, false, false, true }];

        return
        [
            h00, h01, h02, h03, h04, h05, h06, h07, h08, h09, h10, h11,
            h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23,
        ];

        static DateTime GetDate(int hour) => new(2000, 1, 1, hour, 1, 1);
    }

    private static object[] GetHourParityBitIsCorrectTestData()
    {
        object[] h00 = [GetDate(00), false];
        object[] h01 = [GetDate(01), true];

        return [h00, h01];

        static DateTime GetDate(int hour) => new(2000, 1, 1, hour, 1, 1);
    }

    private static object[] GetDayBitsAreCorrectTestData()
    {
        object[] d01 = [GetDate(01), new[] { true, false, false, false, false, false }];
        object[] d02 = [GetDate(02), new[] { false, true, false, false, false, false }];
        object[] d03 = [GetDate(03), new[] { true, true, false, false, false, false }];
        object[] d04 = [GetDate(04), new[] { false, false, true, false, false, false }];
        object[] d05 = [GetDate(05), new[] { true, false, true, false, false, false }];
        object[] d06 = [GetDate(06), new[] { false, true, true, false, false, false }];
        object[] d07 = [GetDate(07), new[] { true, true, true, false, false, false }];
        object[] d08 = [GetDate(08), new[] { false, false, false, true, false, false }];
        object[] d09 = [GetDate(09), new[] { true, false, false, true, false, false }];
        object[] d10 = [GetDate(10), new[] { false, false, false, false, true, false }];
        object[] d11 = [GetDate(11), new[] { true, false, false, false, true, false }];
        object[] d12 = [GetDate(12), new[] { false, true, false, false, true, false }];
        object[] d13 = [GetDate(13), new[] { true, true, false, false, true, false }];
        object[] d14 = [GetDate(14), new[] { false, false, true, false, true, false }];
        object[] d15 = [GetDate(15), new[] { true, false, true, false, true, false }];
        object[] d16 = [GetDate(16), new[] { false, true, true, false, true, false }];
        object[] d17 = [GetDate(17), new[] { true, true, true, false, true, false }];
        object[] d18 = [GetDate(18), new[] { false, false, false, true, true, false }];
        object[] d19 = [GetDate(19), new[] { true, false, false, true, true, false }];
        object[] d20 = [GetDate(20), new[] { false, false, false, false, false, true }];
        object[] d21 = [GetDate(21), new[] { true, false, false, false, false, true }];
        object[] d22 = [GetDate(22), new[] { false, true, false, false, false, true }];
        object[] d23 = [GetDate(23), new[] { true, true, false, false, false, true }];
        object[] d24 = [GetDate(24), new[] { false, false, true, false, false, true }];
        object[] d25 = [GetDate(25), new[] { true, false, true, false, false, true }];
        object[] d26 = [GetDate(26), new[] { false, true, true, false, false, true }];
        object[] d27 = [GetDate(27), new[] { true, true, true, false, false, true }];
        object[] d28 = [GetDate(28), new[] { false, false, false, true, false, true }];
        object[] d29 = [GetDate(29), new[] { true, false, false, true, false, true }];
        object[] d30 = [GetDate(30), new[] { false, false, false, false, true, true }];
        object[] d31 = [GetDate(31), new[] { true, false, false, false, true, true }];

        return
        [
            d01, d02, d03, d04, d05, d06, d07, d08, d09, d10,
            d11, d12, d13, d14, d15, d16, d17, d18, d19, d20,
            d21, d22, d23, d24, d25, d26, d27, d28, d29, d30,
            d31,
        ];

        static DateTime GetDate(int day) => new(2000, 1, day, 1, 1, 1);
    }

    private static object[] GetWeekDayBitsAreCorrectTestData()
    {
        object[] mon = [GetDate(3), new[] { true, false, false }];
        object[] tue = [GetDate(4), new[] { false, true, false }];
        object[] wed = [GetDate(5), new[] { true, true, false }];
        object[] thu = [GetDate(6), new[] { false, false, true }];
        object[] fri = [GetDate(7), new[] { true, false, true }];
        object[] sat = [GetDate(8), new[] { false, true, true }];
        object[] sun = [GetDate(9), new[] { true, true, true }];

        return [mon, tue, wed, thu, fri, sat, sun];

        static DateTime GetDate(int day) => new(2000, 1, day, 1, 1, 1);
    }

    private static object[] GetMonthBitsAreCorrectTestData()
    {
        object[] m01 = [GetDate(01), new[] { true, false, false, false, false }];
        object[] m02 = [GetDate(02), new[] { false, true, false, false, false }];
        object[] m03 = [GetDate(03), new[] { true, true, false, false, false }];
        object[] m04 = [GetDate(04), new[] { false, false, true, false, false }];
        object[] m05 = [GetDate(05), new[] { true, false, true, false, false }];
        object[] m06 = [GetDate(06), new[] { false, true, true, false, false }];
        object[] m07 = [GetDate(07), new[] { true, true, true, false, false }];
        object[] m08 = [GetDate(08), new[] { false, false, false, true, false }];
        object[] m09 = [GetDate(09), new[] { true, false, false, true, false }];
        object[] m10 = [GetDate(10), new[] { false, false, false, false, true }];
        object[] m11 = [GetDate(11), new[] { true, false, false, false, true }];

        return [m01, m02, m03, m04, m05, m06, m07, m08, m09, m10, m11];

        static DateTime GetDate(int month) => new(2000, month, 1, 1, 1, 1);
    }

    private static object[] GetYearBitsAreCorrectTestData()
    {
        object[] y2000 = [GetDate(2000), new[] { false, false, false, false, false, false, false, false }];
        object[] y2001 = [GetDate(2001), new[] { true, false, false, false, false, false, false, false }];
        object[] y2002 = [GetDate(2002), new[] { false, true, false, false, false, false, false, false }];
        object[] y2003 = [GetDate(2003), new[] { true, true, false, false, false, false, false, false }];
        object[] y2004 = [GetDate(2004), new[] { false, false, true, false, false, false, false, false }];
        object[] y2005 = [GetDate(2005), new[] { true, false, true, false, false, false, false, false }];
        object[] y2006 = [GetDate(2006), new[] { false, true, true, false, false, false, false, false }];
        object[] y2007 = [GetDate(2007), new[] { true, true, true, false, false, false, false, false }];
        object[] y2008 = [GetDate(2008), new[] { false, false, false, true, false, false, false, false }];
        object[] y2009 = [GetDate(2009), new[] { true, false, false, true, false, false, false, false }];
        object[] y2010 = [GetDate(2010), new[] { false, false, false, false, true, false, false, false }];
        object[] y2011 = [GetDate(2011), new[] { true, false, false, false, true, false, false, false }];
        object[] y2012 = [GetDate(2012), new[] { false, true, false, false, true, false, false, false }];
        object[] y2013 = [GetDate(2013), new[] { true, true, false, false, true, false, false, false }];
        object[] y2014 = [GetDate(2014), new[] { false, false, true, false, true, false, false, false }];
        object[] y2015 = [GetDate(2015), new[] { true, false, true, false, true, false, false, false }];
        object[] y2016 = [GetDate(2016), new[] { false, true, true, false, true, false, false, false }];
        object[] y2017 = [GetDate(2017), new[] { true, true, true, false, true, false, false, false }];
        object[] y2018 = [GetDate(2018), new[] { false, false, false, true, true, false, false, false }];
        object[] y2019 = [GetDate(2019), new[] { true, false, false, true, true, false, false, false }];
        object[] y2020 = [GetDate(2020), new[] { false, false, false, false, false, true, false, false }];
        object[] y2021 = [GetDate(2021), new[] { true, false, false, false, false, true, false, false }];
        object[] y2022 = [GetDate(2022), new[] { false, true, false, false, false, true, false, false }];
        object[] y2023 = [GetDate(2023), new[] { true, true, false, false, false, true, false, false }];
        object[] y2024 = [GetDate(2024), new[] { false, false, true, false, false, true, false, false }];
        object[] y2025 = [GetDate(2025), new[] { true, false, true, false, false, true, false, false }];
        object[] y2026 = [GetDate(2026), new[] { false, true, true, false, false, true, false, false }];
        object[] y2027 = [GetDate(2027), new[] { true, true, true, false, false, true, false, false }];
        object[] y2028 = [GetDate(2028), new[] { false, false, false, true, false, true, false, false }];
        object[] y2029 = [GetDate(2029), new[] { true, false, false, true, false, true, false, false }];
        object[] y2030 = [GetDate(2030), new[] { false, false, false, false, true, true, false, false }];
        object[] y2031 = [GetDate(2031), new[] { true, false, false, false, true, true, false, false }];
        object[] y2032 = [GetDate(2032), new[] { false, true, false, false, true, true, false, false }];
        object[] y2033 = [GetDate(2033), new[] { true, true, false, false, true, true, false, false }];
        object[] y2034 = [GetDate(2034), new[] { false, false, true, false, true, true, false, false }];
        object[] y2035 = [GetDate(2035), new[] { true, false, true, false, true, true, false, false }];
        object[] y2036 = [GetDate(2036), new[] { false, true, true, false, true, true, false, false }];
        object[] y2037 = [GetDate(2037), new[] { true, true, true, false, true, true, false, false }];
        object[] y2038 = [GetDate(2038), new[] { false, false, false, true, true, true, false, false }];
        object[] y2039 = [GetDate(2039), new[] { true, false, false, true, true, true, false, false }];
        object[] y2040 = [GetDate(2040), new[] { false, false, false, false, false, false, true, false }];
        object[] y2041 = [GetDate(2041), new[] { true, false, false, false, false, false, true, false }];
        object[] y2042 = [GetDate(2042), new[] { false, true, false, false, false, false, true, false }];
        object[] y2043 = [GetDate(2043), new[] { true, true, false, false, false, false, true, false }];
        object[] y2044 = [GetDate(2044), new[] { false, false, true, false, false, false, true, false }];
        object[] y2045 = [GetDate(2045), new[] { true, false, true, false, false, false, true, false }];
        object[] y2046 = [GetDate(2046), new[] { false, true, true, false, false, false, true, false }];
        object[] y2047 = [GetDate(2047), new[] { true, true, true, false, false, false, true, false }];
        object[] y2048 = [GetDate(2048), new[] { false, false, false, true, false, false, true, false }];
        object[] y2049 = [GetDate(2049), new[] { true, false, false, true, false, false, true, false }];
        object[] y2050 = [GetDate(2050), new[] { false, false, false, false, true, false, true, false }];
        object[] y2051 = [GetDate(2051), new[] { true, false, false, false, true, false, true, false }];
        object[] y2052 = [GetDate(2052), new[] { false, true, false, false, true, false, true, false }];
        object[] y2053 = [GetDate(2053), new[] { true, true, false, false, true, false, true, false }];
        object[] y2054 = [GetDate(2054), new[] { false, false, true, false, true, false, true, false }];
        object[] y2055 = [GetDate(2055), new[] { true, false, true, false, true, false, true, false }];
        object[] y2056 = [GetDate(2056), new[] { false, true, true, false, true, false, true, false }];
        object[] y2057 = [GetDate(2057), new[] { true, true, true, false, true, false, true, false }];
        object[] y2058 = [GetDate(2058), new[] { false, false, false, true, true, false, true, false }];
        object[] y2059 = [GetDate(2059), new[] { true, false, false, true, true, false, true, false }];
        object[] y2060 = [GetDate(2060), new[] { false, false, false, false, false, true, true, false }];
        object[] y2061 = [GetDate(2061), new[] { true, false, false, false, false, true, true, false }];
        object[] y2062 = [GetDate(2062), new[] { false, true, false, false, false, true, true, false }];
        object[] y2063 = [GetDate(2063), new[] { true, true, false, false, false, true, true, false }];
        object[] y2064 = [GetDate(2064), new[] { false, false, true, false, false, true, true, false }];
        object[] y2065 = [GetDate(2065), new[] { true, false, true, false, false, true, true, false }];
        object[] y2066 = [GetDate(2066), new[] { false, true, true, false, false, true, true, false }];
        object[] y2067 = [GetDate(2067), new[] { true, true, true, false, false, true, true, false }];
        object[] y2068 = [GetDate(2068), new[] { false, false, false, true, false, true, true, false }];
        object[] y2069 = [GetDate(2069), new[] { true, false, false, true, false, true, true, false }];
        object[] y2070 = [GetDate(2070), new[] { false, false, false, false, true, true, true, false }];
        object[] y2071 = [GetDate(2071), new[] { true, false, false, false, true, true, true, false }];
        object[] y2072 = [GetDate(2072), new[] { false, true, false, false, true, true, true, false }];
        object[] y2073 = [GetDate(2073), new[] { true, true, false, false, true, true, true, false }];
        object[] y2074 = [GetDate(2074), new[] { false, false, true, false, true, true, true, false }];
        object[] y2075 = [GetDate(2075), new[] { true, false, true, false, true, true, true, false }];
        object[] y2076 = [GetDate(2076), new[] { false, true, true, false, true, true, true, false }];
        object[] y2077 = [GetDate(2077), new[] { true, true, true, false, true, true, true, false }];
        object[] y2078 = [GetDate(2078), new[] { false, false, false, true, true, true, true, false }];
        object[] y2079 = [GetDate(2079), new[] { true, false, false, true, true, true, true, false }];
        object[] y2080 = [GetDate(2080), new[] { false, false, false, false, false, false, false, true }];
        object[] y2081 = [GetDate(2081), new[] { true, false, false, false, false, false, false, true }];
        object[] y2082 = [GetDate(2082), new[] { false, true, false, false, false, false, false, true }];
        object[] y2083 = [GetDate(2083), new[] { true, true, false, false, false, false, false, true }];
        object[] y2084 = [GetDate(2084), new[] { false, false, true, false, false, false, false, true }];
        object[] y2085 = [GetDate(2085), new[] { true, false, true, false, false, false, false, true }];
        object[] y2086 = [GetDate(2086), new[] { false, true, true, false, false, false, false, true }];
        object[] y2087 = [GetDate(2087), new[] { true, true, true, false, false, false, false, true }];
        object[] y2088 = [GetDate(2088), new[] { false, false, false, true, false, false, false, true }];
        object[] y2089 = [GetDate(2089), new[] { true, false, false, true, false, false, false, true }];
        object[] y2090 = [GetDate(2090), new[] { false, false, false, false, true, false, false, true }];
        object[] y2091 = [GetDate(2091), new[] { true, false, false, false, true, false, false, true }];
        object[] y2092 = [GetDate(2092), new[] { false, true, false, false, true, false, false, true }];
        object[] y2093 = [GetDate(2093), new[] { true, true, false, false, true, false, false, true }];
        object[] y2094 = [GetDate(2094), new[] { false, false, true, false, true, false, false, true }];
        object[] y2095 = [GetDate(2095), new[] { true, false, true, false, true, false, false, true }];
        object[] y2096 = [GetDate(2096), new[] { false, true, true, false, true, false, false, true }];
        object[] y2097 = [GetDate(2097), new[] { true, true, true, false, true, false, false, true }];
        object[] y2098 = [GetDate(2098), new[] { false, false, false, true, true, false, false, true }];
        object[] y2099 = [GetDate(2099), new[] { true, false, false, true, true, false, false, true }];

        return
        [
            y2000, y2001, y2002, y2003, y2004, y2005, y2006, y2007, y2008, y2009,
            y2010, y2011, y2012, y2013, y2014, y2015, y2016, y2017, y2018, y2019,
            y2020, y2021, y2022, y2023, y2024, y2025, y2026, y2027, y2028, y2029,
            y2030, y2031, y2032, y2033, y2034, y2035, y2036, y2037, y2038, y2039,
            y2040, y2041, y2042, y2043, y2044, y2045, y2046, y2047, y2048, y2049,
            y2050, y2051, y2052, y2053, y2054, y2055, y2056, y2057, y2058, y2059,
            y2060, y2061, y2062, y2063, y2064, y2065, y2066, y2067, y2068, y2069,
            y2070, y2071, y2072, y2073, y2074, y2075, y2076, y2077, y2078, y2079,
            y2080, y2081, y2082, y2083, y2084, y2085, y2086, y2087, y2088, y2089,
            y2090, y2091, y2092, y2093, y2094, y2095, y2096, y2097, y2098, y2099,
        ];

        static DateTime GetDate(int year) => new(year, 1, 1, 1, 1, 1);
    }

    private static object[] GetDateParityBitIsCorrectTestData()
    {
        object[] d02 = [GetDate(day: 02), true];
        object[] d03 = [GetDate(day: 03), false];
        object[] m03 = [GetDate(month: 03), true];
        object[] m07 = [GetDate(month: 07), false];
        object[] y2006 = [GetDate(year: 2006), true];
        object[] y2007 = [GetDate(year: 2007), false];

        return
        [
            d02, d03,
            m03, m07,
            y2006, y2007,
        ];

        static DateTime GetDate(int year = 2000, int month = 1, int day = 1) =>
            new(year, month, day, 1, 1, 1);
    }

    private static object[] GetDateTimeAgnosticBitsAreCorrectTestData()
    {
        var result = new List<object>();

        // We just check about 100 DateTime objects.
        var startDate = new DateTime(2026, 1, 1, 0, 0, 0);
        var endDate = new DateTime(2036, 1, 1, 0, 0, 0);
        const int minInterval = 52588;

        for (var date = startDate; date < endDate; date = date.AddMinutes(minInterval))
        {
            object[] xxx = [date];
            result.Add(xxx);
        }

        return result.ToArray();
    }

    private static object[] GetBitsArrayLengthIsAlways60TestData() =>
        GetDateTimeAgnosticBitsAreCorrectTestData();

    #endregion Test data
}