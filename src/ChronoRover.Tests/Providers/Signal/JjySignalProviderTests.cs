using ChronoRover.Providers.Signal;
using ChronoRover.Tests.TestUtils;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ChronoRover.Tests.Providers.Signal;

[TestFixture]
public class JjySignalProviderTests
{
    [Test]
    [TestCaseSource(nameof(GetMinuteBitsAreCorrectTestData))]
    public void MinuteBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);
        var bitsMin = bits[01..04].Concat(bits[05..09]);

        Assert.That(bitsMin, Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetHourBitsAreCorrectTestData))]
    public void HourBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);
        var bitsHour = bits[12..14].Concat(bits[15..19]);

        Assert.That(bitsHour, Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(DayOfYearBitsAreCorrectTestData))]
    public void DayOfYearBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);
        var bitsDoy = bits[22..24].Concat(bits[25..29]).Concat(bits[30..34]);

        Assert.That(bitsDoy, Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetHourParityBitIsCorrectTestData))]
    public void HourParityBitIsCorrect(DateTime date, bool expectedBit)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[36], Is.EqualTo(expectedBit));
    }

    [Test]
    [TestCaseSource(nameof(GetMinuteParityBitIsCorrectTestData))]
    public void MinuteParityBitIsCorrect(DateTime date, bool expectedBit)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[37], Is.EqualTo(expectedBit));
    }

    [Test]
    [TestCaseSource(nameof(GetYearBitsAreCorrectTestData))]
    public void YearBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);
        var bitsYear = bits[41..49];

        Assert.That(bitsYear, Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetWeekDayBitsAreCorrectTestData))]
    public void WeekDayBitsAreCorrect(DateTime date, bool[] expectedBits)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[50..53], Is.EqualTo(expectedBits));
    }

    [Test]
    [TestCaseSource(nameof(GetDateTimeAgnosticBitsAreCorrectTestData))]
    public void DateTimeAgnosticBitsAreCorrect(DateTime date)
    {
        var bits = GetMinuteSignal(date);

        Assert.That(bits[00], Is.EqualTo(false));
        Assert.That(bits[04], Is.EqualTo(false));
        Assert.That(bits[09..12], Has.All.EqualTo(false));
        Assert.That(bits[14], Is.EqualTo(false));
        Assert.That(bits[19..22], Has.All.EqualTo(false));
        Assert.That(bits[24], Is.EqualTo(false));
        Assert.That(bits[29], Is.EqualTo(false));
        Assert.That(bits[34..36], Has.All.EqualTo(false));
        Assert.That(bits[38..41], Has.All.EqualTo(false));
        Assert.That(bits[49], Is.EqualTo(false));
        Assert.That(bits[53..59], Has.All.EqualTo(false));
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
        var provider = new JjySignalProvider(
            TimeZoneUtils.GetDefaultTimeZoneProvider());

        return provider.GetMinuteSignal(date);
    }

    #region Test data

    private static object[] GetMinuteBitsAreCorrectTestData()
    {
        object[] m00 = [GetDate(00), new[] { false, false, false, false, false, false, false }];
        object[] m01 = [GetDate(01), new[] { false, false, false, false, false, false, true }];
        object[] m02 = [GetDate(02), new[] { false, false, false, false, false, true, false }];
        object[] m03 = [GetDate(03), new[] { false, false, false, false, false, true, true }];
        object[] m04 = [GetDate(04), new[] { false, false, false, false, true, false, false }];
        object[] m05 = [GetDate(05), new[] { false, false, false, false, true, false, true }];
        object[] m06 = [GetDate(06), new[] { false, false, false, false, true, true, false }];
        object[] m07 = [GetDate(07), new[] { false, false, false, false, true, true, true }];
        object[] m08 = [GetDate(08), new[] { false, false, false, true, false, false, false }];
        object[] m09 = [GetDate(09), new[] { false, false, false, true, false, false, true }];
        object[] m10 = [GetDate(10), new[] { false, false, true, false, false, false, false }];
        object[] m11 = [GetDate(11), new[] { false, false, true, false, false, false, true }];
        object[] m12 = [GetDate(12), new[] { false, false, true, false, false, true, false }];
        object[] m13 = [GetDate(13), new[] { false, false, true, false, false, true, true }];
        object[] m14 = [GetDate(14), new[] { false, false, true, false, true, false, false }];
        object[] m15 = [GetDate(15), new[] { false, false, true, false, true, false, true }];
        object[] m16 = [GetDate(16), new[] { false, false, true, false, true, true, false }];
        object[] m17 = [GetDate(17), new[] { false, false, true, false, true, true, true }];
        object[] m18 = [GetDate(18), new[] { false, false, true, true, false, false, false }];
        object[] m19 = [GetDate(19), new[] { false, false, true, true, false, false, true }];
        object[] m20 = [GetDate(20), new[] { false, true, false, false, false, false, false }];
        object[] m21 = [GetDate(21), new[] { false, true, false, false, false, false, true }];
        object[] m22 = [GetDate(22), new[] { false, true, false, false, false, true, false }];
        object[] m23 = [GetDate(23), new[] { false, true, false, false, false, true, true }];
        object[] m24 = [GetDate(24), new[] { false, true, false, false, true, false, false }];
        object[] m25 = [GetDate(25), new[] { false, true, false, false, true, false, true }];
        object[] m26 = [GetDate(26), new[] { false, true, false, false, true, true, false }];
        object[] m27 = [GetDate(27), new[] { false, true, false, false, true, true, true }];
        object[] m28 = [GetDate(28), new[] { false, true, false, true, false, false, false }];
        object[] m29 = [GetDate(29), new[] { false, true, false, true, false, false, true }];
        object[] m30 = [GetDate(30), new[] { false, true, true, false, false, false, false }];
        object[] m31 = [GetDate(31), new[] { false, true, true, false, false, false, true }];
        object[] m32 = [GetDate(32), new[] { false, true, true, false, false, true, false }];
        object[] m33 = [GetDate(33), new[] { false, true, true, false, false, true, true }];
        object[] m34 = [GetDate(34), new[] { false, true, true, false, true, false, false }];
        object[] m35 = [GetDate(35), new[] { false, true, true, false, true, false, true }];
        object[] m36 = [GetDate(36), new[] { false, true, true, false, true, true, false }];
        object[] m37 = [GetDate(37), new[] { false, true, true, false, true, true, true }];
        object[] m38 = [GetDate(38), new[] { false, true, true, true, false, false, false }];
        object[] m39 = [GetDate(39), new[] { false, true, true, true, false, false, true }];
        object[] m40 = [GetDate(40), new[] { true, false, false, false, false, false, false }];
        object[] m41 = [GetDate(41), new[] { true, false, false, false, false, false, true }];
        object[] m42 = [GetDate(42), new[] { true, false, false, false, false, true, false }];
        object[] m43 = [GetDate(43), new[] { true, false, false, false, false, true, true }];
        object[] m44 = [GetDate(44), new[] { true, false, false, false, true, false, false }];
        object[] m45 = [GetDate(45), new[] { true, false, false, false, true, false, true }];
        object[] m46 = [GetDate(46), new[] { true, false, false, false, true, true, false }];
        object[] m47 = [GetDate(47), new[] { true, false, false, false, true, true, true }];
        object[] m48 = [GetDate(48), new[] { true, false, false, true, false, false, false }];
        object[] m49 = [GetDate(49), new[] { true, false, false, true, false, false, true }];
        object[] m50 = [GetDate(50), new[] { true, false, true, false, false, false, false }];
        object[] m51 = [GetDate(51), new[] { true, false, true, false, false, false, true }];
        object[] m52 = [GetDate(52), new[] { true, false, true, false, false, true, false }];
        object[] m53 = [GetDate(53), new[] { true, false, true, false, false, true, true }];
        object[] m54 = [GetDate(54), new[] { true, false, true, false, true, false, false }];
        object[] m55 = [GetDate(55), new[] { true, false, true, false, true, false, true }];
        object[] m56 = [GetDate(56), new[] { true, false, true, false, true, true, false }];
        object[] m57 = [GetDate(57), new[] { true, false, true, false, true, true, true }];
        object[] m58 = [GetDate(58), new[] { true, false, true, true, false, false, false }];
        object[] m59 = [GetDate(59), new[] { true, false, true, true, false, false, true }];

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

    private static object[] GetHourBitsAreCorrectTestData()
    {
        object[] h00 = [GetDate(00), new[] { false, false, false, false, false, false }];
        object[] h01 = [GetDate(01), new[] { false, false, false, false, false, true }];
        object[] h02 = [GetDate(02), new[] { false, false, false, false, true, false }];
        object[] h03 = [GetDate(03), new[] { false, false, false, false, true, true }];
        object[] h04 = [GetDate(04), new[] { false, false, false, true, false, false }];
        object[] h05 = [GetDate(05), new[] { false, false, false, true, false, true }];
        object[] h06 = [GetDate(06), new[] { false, false, false, true, true, false }];
        object[] h07 = [GetDate(07), new[] { false, false, false, true, true, true }];
        object[] h08 = [GetDate(08), new[] { false, false, true, false, false, false }];
        object[] h09 = [GetDate(09), new[] { false, false, true, false, false, true }];
        object[] h10 = [GetDate(10), new[] { false, true, false, false, false, false }];
        object[] h11 = [GetDate(11), new[] { false, true, false, false, false, true }];
        object[] h12 = [GetDate(12), new[] { false, true, false, false, true, false }];
        object[] h13 = [GetDate(13), new[] { false, true, false, false, true, true }];
        object[] h14 = [GetDate(14), new[] { false, true, false, true, false, false }];
        object[] h15 = [GetDate(15), new[] { false, true, false, true, false, true }];
        object[] h16 = [GetDate(16), new[] { false, true, false, true, true, false }];
        object[] h17 = [GetDate(17), new[] { false, true, false, true, true, true }];
        object[] h18 = [GetDate(18), new[] { false, true, true, false, false, false }];
        object[] h19 = [GetDate(19), new[] { false, true, true, false, false, true }];
        object[] h20 = [GetDate(20), new[] { true, false, false, false, false, false }];
        object[] h21 = [GetDate(21), new[] { true, false, false, false, false, true }];
        object[] h22 = [GetDate(22), new[] { true, false, false, false, true, false }];
        object[] h23 = [GetDate(23), new[] { true, false, false, false, true, true }];

        return
        [
            h00, h01, h02, h03, h04, h05, h06, h07, h08, h09, h10, h11,
            h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23,
        ];

        static DateTime GetDate(int hour) => new(2000, 1, 1, hour, 1, 1);
    }

    private static object[] DayOfYearBitsAreCorrectTestData()
    {
        object[] d001 =
            [GetDate(01, 01), new[] { false, false, false, false, false, false, false, false, false, true }];
        object[] d002 =
            [GetDate(01, 02), new[] { false, false, false, false, false, false, false, false, true, false }];
        object[] d003 = [GetDate(01, 03), new[] { false, false, false, false, false, false, false, false, true, true }];
        object[] d004 =
            [GetDate(01, 04), new[] { false, false, false, false, false, false, false, true, false, false }];
        object[] d005 = [GetDate(01, 05), new[] { false, false, false, false, false, false, false, true, false, true }];
        object[] d006 = [GetDate(01, 06), new[] { false, false, false, false, false, false, false, true, true, false }];
        object[] d007 = [GetDate(01, 07), new[] { false, false, false, false, false, false, false, true, true, true }];
        object[] d008 =
            [GetDate(01, 08), new[] { false, false, false, false, false, false, true, false, false, false }];
        object[] d009 = [GetDate(01, 09), new[] { false, false, false, false, false, false, true, false, false, true }];
        object[] d010 =
            [GetDate(01, 10), new[] { false, false, false, false, false, true, false, false, false, false }];
        object[] d011 = [GetDate(01, 11), new[] { false, false, false, false, false, true, false, false, false, true }];
        object[] d012 = [GetDate(01, 12), new[] { false, false, false, false, false, true, false, false, true, false }];
        object[] d013 = [GetDate(01, 13), new[] { false, false, false, false, false, true, false, false, true, true }];
        object[] d014 = [GetDate(01, 14), new[] { false, false, false, false, false, true, false, true, false, false }];
        object[] d015 = [GetDate(01, 15), new[] { false, false, false, false, false, true, false, true, false, true }];
        object[] d016 = [GetDate(01, 16), new[] { false, false, false, false, false, true, false, true, true, false }];
        object[] d017 = [GetDate(01, 17), new[] { false, false, false, false, false, true, false, true, true, true }];
        object[] d018 = [GetDate(01, 18), new[] { false, false, false, false, false, true, true, false, false, false }];
        object[] d019 = [GetDate(01, 19), new[] { false, false, false, false, false, true, true, false, false, true }];
        object[] d020 =
            [GetDate(01, 20), new[] { false, false, false, false, true, false, false, false, false, false }];
        object[] d021 = [GetDate(01, 21), new[] { false, false, false, false, true, false, false, false, false, true }];
        object[] d022 = [GetDate(01, 22), new[] { false, false, false, false, true, false, false, false, true, false }];
        object[] d023 = [GetDate(01, 23), new[] { false, false, false, false, true, false, false, false, true, true }];
        object[] d024 = [GetDate(01, 24), new[] { false, false, false, false, true, false, false, true, false, false }];
        object[] d025 = [GetDate(01, 25), new[] { false, false, false, false, true, false, false, true, false, true }];
        object[] d026 = [GetDate(01, 26), new[] { false, false, false, false, true, false, false, true, true, false }];
        object[] d027 = [GetDate(01, 27), new[] { false, false, false, false, true, false, false, true, true, true }];
        object[] d028 = [GetDate(01, 28), new[] { false, false, false, false, true, false, true, false, false, false }];
        object[] d029 = [GetDate(01, 29), new[] { false, false, false, false, true, false, true, false, false, true }];
        object[] d030 = [GetDate(01, 30), new[] { false, false, false, false, true, true, false, false, false, false }];
        object[] d031 = [GetDate(01, 31), new[] { false, false, false, false, true, true, false, false, false, true }];
        object[] d032 = [GetDate(02, 01), new[] { false, false, false, false, true, true, false, false, true, false }];
        object[] d033 = [GetDate(02, 02), new[] { false, false, false, false, true, true, false, false, true, true }];
        object[] d034 = [GetDate(02, 03), new[] { false, false, false, false, true, true, false, true, false, false }];
        object[] d035 = [GetDate(02, 04), new[] { false, false, false, false, true, true, false, true, false, true }];
        object[] d036 = [GetDate(02, 05), new[] { false, false, false, false, true, true, false, true, true, false }];
        object[] d037 = [GetDate(02, 06), new[] { false, false, false, false, true, true, false, true, true, true }];
        object[] d038 = [GetDate(02, 07), new[] { false, false, false, false, true, true, true, false, false, false }];
        object[] d039 = [GetDate(02, 08), new[] { false, false, false, false, true, true, true, false, false, true }];
        object[] d040 =
            [GetDate(02, 09), new[] { false, false, false, true, false, false, false, false, false, false }];
        object[] d041 = [GetDate(02, 10), new[] { false, false, false, true, false, false, false, false, false, true }];
        object[] d042 = [GetDate(02, 11), new[] { false, false, false, true, false, false, false, false, true, false }];
        object[] d043 = [GetDate(02, 12), new[] { false, false, false, true, false, false, false, false, true, true }];
        object[] d044 = [GetDate(02, 13), new[] { false, false, false, true, false, false, false, true, false, false }];
        object[] d045 = [GetDate(02, 14), new[] { false, false, false, true, false, false, false, true, false, true }];
        object[] d046 = [GetDate(02, 15), new[] { false, false, false, true, false, false, false, true, true, false }];
        object[] d047 = [GetDate(02, 16), new[] { false, false, false, true, false, false, false, true, true, true }];
        object[] d048 = [GetDate(02, 17), new[] { false, false, false, true, false, false, true, false, false, false }];
        object[] d049 = [GetDate(02, 18), new[] { false, false, false, true, false, false, true, false, false, true }];
        object[] d050 = [GetDate(02, 19), new[] { false, false, false, true, false, true, false, false, false, false }];
        object[] d051 = [GetDate(02, 20), new[] { false, false, false, true, false, true, false, false, false, true }];
        object[] d052 = [GetDate(02, 21), new[] { false, false, false, true, false, true, false, false, true, false }];
        object[] d053 = [GetDate(02, 22), new[] { false, false, false, true, false, true, false, false, true, true }];
        object[] d054 = [GetDate(02, 23), new[] { false, false, false, true, false, true, false, true, false, false }];
        object[] d055 = [GetDate(02, 24), new[] { false, false, false, true, false, true, false, true, false, true }];
        object[] d056 = [GetDate(02, 25), new[] { false, false, false, true, false, true, false, true, true, false }];
        object[] d057 = [GetDate(02, 26), new[] { false, false, false, true, false, true, false, true, true, true }];
        object[] d058 = [GetDate(02, 27), new[] { false, false, false, true, false, true, true, false, false, false }];
        object[] d059 = [GetDate(02, 28), new[] { false, false, false, true, false, true, true, false, false, true }];
        object[] d060 = [GetDate(02, 29), new[] { false, false, false, true, true, false, false, false, false, false }];
        object[] d061 = [GetDate(03, 01), new[] { false, false, false, true, true, false, false, false, false, true }];
        object[] d062 = [GetDate(03, 02), new[] { false, false, false, true, true, false, false, false, true, false }];
        object[] d063 = [GetDate(03, 03), new[] { false, false, false, true, true, false, false, false, true, true }];
        object[] d064 = [GetDate(03, 04), new[] { false, false, false, true, true, false, false, true, false, false }];
        object[] d065 = [GetDate(03, 05), new[] { false, false, false, true, true, false, false, true, false, true }];
        object[] d066 = [GetDate(03, 06), new[] { false, false, false, true, true, false, false, true, true, false }];
        object[] d067 = [GetDate(03, 07), new[] { false, false, false, true, true, false, false, true, true, true }];
        object[] d068 = [GetDate(03, 08), new[] { false, false, false, true, true, false, true, false, false, false }];
        object[] d069 = [GetDate(03, 09), new[] { false, false, false, true, true, false, true, false, false, true }];
        object[] d070 = [GetDate(03, 10), new[] { false, false, false, true, true, true, false, false, false, false }];
        object[] d071 = [GetDate(03, 11), new[] { false, false, false, true, true, true, false, false, false, true }];
        object[] d072 = [GetDate(03, 12), new[] { false, false, false, true, true, true, false, false, true, false }];
        object[] d073 = [GetDate(03, 13), new[] { false, false, false, true, true, true, false, false, true, true }];
        object[] d074 = [GetDate(03, 14), new[] { false, false, false, true, true, true, false, true, false, false }];
        object[] d075 = [GetDate(03, 15), new[] { false, false, false, true, true, true, false, true, false, true }];
        object[] d076 = [GetDate(03, 16), new[] { false, false, false, true, true, true, false, true, true, false }];
        object[] d077 = [GetDate(03, 17), new[] { false, false, false, true, true, true, false, true, true, true }];
        object[] d078 = [GetDate(03, 18), new[] { false, false, false, true, true, true, true, false, false, false }];
        object[] d079 = [GetDate(03, 19), new[] { false, false, false, true, true, true, true, false, false, true }];
        object[] d080 =
            [GetDate(03, 20), new[] { false, false, true, false, false, false, false, false, false, false }];
        object[] d081 = [GetDate(03, 21), new[] { false, false, true, false, false, false, false, false, false, true }];
        object[] d082 = [GetDate(03, 22), new[] { false, false, true, false, false, false, false, false, true, false }];
        object[] d083 = [GetDate(03, 23), new[] { false, false, true, false, false, false, false, false, true, true }];
        object[] d084 = [GetDate(03, 24), new[] { false, false, true, false, false, false, false, true, false, false }];
        object[] d085 = [GetDate(03, 25), new[] { false, false, true, false, false, false, false, true, false, true }];
        object[] d086 = [GetDate(03, 26), new[] { false, false, true, false, false, false, false, true, true, false }];
        object[] d087 = [GetDate(03, 27), new[] { false, false, true, false, false, false, false, true, true, true }];
        object[] d088 = [GetDate(03, 28), new[] { false, false, true, false, false, false, true, false, false, false }];
        object[] d089 = [GetDate(03, 29), new[] { false, false, true, false, false, false, true, false, false, true }];
        object[] d090 = [GetDate(03, 30), new[] { false, false, true, false, false, true, false, false, false, false }];
        object[] d091 = [GetDate(03, 31), new[] { false, false, true, false, false, true, false, false, false, true }];
        object[] d092 = [GetDate(04, 01), new[] { false, false, true, false, false, true, false, false, true, false }];
        object[] d093 = [GetDate(04, 02), new[] { false, false, true, false, false, true, false, false, true, true }];
        object[] d094 = [GetDate(04, 03), new[] { false, false, true, false, false, true, false, true, false, false }];
        object[] d095 = [GetDate(04, 04), new[] { false, false, true, false, false, true, false, true, false, true }];
        object[] d096 = [GetDate(04, 05), new[] { false, false, true, false, false, true, false, true, true, false }];
        object[] d097 = [GetDate(04, 06), new[] { false, false, true, false, false, true, false, true, true, true }];
        object[] d098 = [GetDate(04, 07), new[] { false, false, true, false, false, true, true, false, false, false }];
        object[] d099 = [GetDate(04, 08), new[] { false, false, true, false, false, true, true, false, false, true }];
        object[] d100 =
            [GetDate(04, 09), new[] { false, true, false, false, false, false, false, false, false, false }];
        object[] d101 = [GetDate(04, 10), new[] { false, true, false, false, false, false, false, false, false, true }];
        object[] d102 = [GetDate(04, 11), new[] { false, true, false, false, false, false, false, false, true, false }];
        object[] d103 = [GetDate(04, 12), new[] { false, true, false, false, false, false, false, false, true, true }];
        object[] d104 = [GetDate(04, 13), new[] { false, true, false, false, false, false, false, true, false, false }];
        object[] d105 = [GetDate(04, 14), new[] { false, true, false, false, false, false, false, true, false, true }];
        object[] d106 = [GetDate(04, 15), new[] { false, true, false, false, false, false, false, true, true, false }];
        object[] d107 = [GetDate(04, 16), new[] { false, true, false, false, false, false, false, true, true, true }];
        object[] d108 = [GetDate(04, 17), new[] { false, true, false, false, false, false, true, false, false, false }];
        object[] d109 = [GetDate(04, 18), new[] { false, true, false, false, false, false, true, false, false, true }];
        object[] d110 = [GetDate(04, 19), new[] { false, true, false, false, false, true, false, false, false, false }];
        object[] d111 = [GetDate(04, 20), new[] { false, true, false, false, false, true, false, false, false, true }];
        object[] d112 = [GetDate(04, 21), new[] { false, true, false, false, false, true, false, false, true, false }];
        object[] d113 = [GetDate(04, 22), new[] { false, true, false, false, false, true, false, false, true, true }];
        object[] d114 = [GetDate(04, 23), new[] { false, true, false, false, false, true, false, true, false, false }];
        object[] d115 = [GetDate(04, 24), new[] { false, true, false, false, false, true, false, true, false, true }];
        object[] d116 = [GetDate(04, 25), new[] { false, true, false, false, false, true, false, true, true, false }];
        object[] d117 = [GetDate(04, 26), new[] { false, true, false, false, false, true, false, true, true, true }];
        object[] d118 = [GetDate(04, 27), new[] { false, true, false, false, false, true, true, false, false, false }];
        object[] d119 = [GetDate(04, 28), new[] { false, true, false, false, false, true, true, false, false, true }];
        object[] d120 = [GetDate(04, 29), new[] { false, true, false, false, true, false, false, false, false, false }];
        object[] d121 = [GetDate(04, 30), new[] { false, true, false, false, true, false, false, false, false, true }];
        object[] d122 = [GetDate(05, 01), new[] { false, true, false, false, true, false, false, false, true, false }];
        object[] d123 = [GetDate(05, 02), new[] { false, true, false, false, true, false, false, false, true, true }];
        object[] d124 = [GetDate(05, 03), new[] { false, true, false, false, true, false, false, true, false, false }];
        object[] d125 = [GetDate(05, 04), new[] { false, true, false, false, true, false, false, true, false, true }];
        object[] d126 = [GetDate(05, 05), new[] { false, true, false, false, true, false, false, true, true, false }];
        object[] d127 = [GetDate(05, 06), new[] { false, true, false, false, true, false, false, true, true, true }];
        object[] d128 = [GetDate(05, 07), new[] { false, true, false, false, true, false, true, false, false, false }];
        object[] d129 = [GetDate(05, 08), new[] { false, true, false, false, true, false, true, false, false, true }];
        object[] d130 = [GetDate(05, 09), new[] { false, true, false, false, true, true, false, false, false, false }];
        object[] d131 = [GetDate(05, 10), new[] { false, true, false, false, true, true, false, false, false, true }];
        object[] d132 = [GetDate(05, 11), new[] { false, true, false, false, true, true, false, false, true, false }];
        object[] d133 = [GetDate(05, 12), new[] { false, true, false, false, true, true, false, false, true, true }];
        object[] d134 = [GetDate(05, 13), new[] { false, true, false, false, true, true, false, true, false, false }];
        object[] d135 = [GetDate(05, 14), new[] { false, true, false, false, true, true, false, true, false, true }];
        object[] d136 = [GetDate(05, 15), new[] { false, true, false, false, true, true, false, true, true, false }];
        object[] d137 = [GetDate(05, 16), new[] { false, true, false, false, true, true, false, true, true, true }];
        object[] d138 = [GetDate(05, 17), new[] { false, true, false, false, true, true, true, false, false, false }];
        object[] d139 = [GetDate(05, 18), new[] { false, true, false, false, true, true, true, false, false, true }];
        object[] d140 = [GetDate(05, 19), new[] { false, true, false, true, false, false, false, false, false, false }];
        object[] d141 = [GetDate(05, 20), new[] { false, true, false, true, false, false, false, false, false, true }];
        object[] d142 = [GetDate(05, 21), new[] { false, true, false, true, false, false, false, false, true, false }];
        object[] d143 = [GetDate(05, 22), new[] { false, true, false, true, false, false, false, false, true, true }];
        object[] d144 = [GetDate(05, 23), new[] { false, true, false, true, false, false, false, true, false, false }];
        object[] d145 = [GetDate(05, 24), new[] { false, true, false, true, false, false, false, true, false, true }];
        object[] d146 = [GetDate(05, 25), new[] { false, true, false, true, false, false, false, true, true, false }];
        object[] d147 = [GetDate(05, 26), new[] { false, true, false, true, false, false, false, true, true, true }];
        object[] d148 = [GetDate(05, 27), new[] { false, true, false, true, false, false, true, false, false, false }];
        object[] d149 = [GetDate(05, 28), new[] { false, true, false, true, false, false, true, false, false, true }];
        object[] d150 = [GetDate(05, 29), new[] { false, true, false, true, false, true, false, false, false, false }];
        object[] d151 = [GetDate(05, 30), new[] { false, true, false, true, false, true, false, false, false, true }];
        object[] d152 = [GetDate(05, 31), new[] { false, true, false, true, false, true, false, false, true, false }];
        object[] d153 = [GetDate(06, 01), new[] { false, true, false, true, false, true, false, false, true, true }];
        object[] d154 = [GetDate(06, 02), new[] { false, true, false, true, false, true, false, true, false, false }];
        object[] d155 = [GetDate(06, 03), new[] { false, true, false, true, false, true, false, true, false, true }];
        object[] d156 = [GetDate(06, 04), new[] { false, true, false, true, false, true, false, true, true, false }];
        object[] d157 = [GetDate(06, 05), new[] { false, true, false, true, false, true, false, true, true, true }];
        object[] d158 = [GetDate(06, 06), new[] { false, true, false, true, false, true, true, false, false, false }];
        object[] d159 = [GetDate(06, 07), new[] { false, true, false, true, false, true, true, false, false, true }];
        object[] d160 = [GetDate(06, 08), new[] { false, true, false, true, true, false, false, false, false, false }];
        object[] d161 = [GetDate(06, 09), new[] { false, true, false, true, true, false, false, false, false, true }];
        object[] d162 = [GetDate(06, 10), new[] { false, true, false, true, true, false, false, false, true, false }];
        object[] d163 = [GetDate(06, 11), new[] { false, true, false, true, true, false, false, false, true, true }];
        object[] d164 = [GetDate(06, 12), new[] { false, true, false, true, true, false, false, true, false, false }];
        object[] d165 = [GetDate(06, 13), new[] { false, true, false, true, true, false, false, true, false, true }];
        object[] d166 = [GetDate(06, 14), new[] { false, true, false, true, true, false, false, true, true, false }];
        object[] d167 = [GetDate(06, 15), new[] { false, true, false, true, true, false, false, true, true, true }];
        object[] d168 = [GetDate(06, 16), new[] { false, true, false, true, true, false, true, false, false, false }];
        object[] d169 = [GetDate(06, 17), new[] { false, true, false, true, true, false, true, false, false, true }];
        object[] d170 = [GetDate(06, 18), new[] { false, true, false, true, true, true, false, false, false, false }];
        object[] d171 = [GetDate(06, 19), new[] { false, true, false, true, true, true, false, false, false, true }];
        object[] d172 = [GetDate(06, 20), new[] { false, true, false, true, true, true, false, false, true, false }];
        object[] d173 = [GetDate(06, 21), new[] { false, true, false, true, true, true, false, false, true, true }];
        object[] d174 = [GetDate(06, 22), new[] { false, true, false, true, true, true, false, true, false, false }];
        object[] d175 = [GetDate(06, 23), new[] { false, true, false, true, true, true, false, true, false, true }];
        object[] d176 = [GetDate(06, 24), new[] { false, true, false, true, true, true, false, true, true, false }];
        object[] d177 = [GetDate(06, 25), new[] { false, true, false, true, true, true, false, true, true, true }];
        object[] d178 = [GetDate(06, 26), new[] { false, true, false, true, true, true, true, false, false, false }];
        object[] d179 = [GetDate(06, 27), new[] { false, true, false, true, true, true, true, false, false, true }];
        object[] d180 = [GetDate(06, 28), new[] { false, true, true, false, false, false, false, false, false, false }];
        object[] d181 = [GetDate(06, 29), new[] { false, true, true, false, false, false, false, false, false, true }];
        object[] d182 = [GetDate(06, 30), new[] { false, true, true, false, false, false, false, false, true, false }];
        object[] d183 = [GetDate(07, 01), new[] { false, true, true, false, false, false, false, false, true, true }];
        object[] d184 = [GetDate(07, 02), new[] { false, true, true, false, false, false, false, true, false, false }];
        object[] d185 = [GetDate(07, 03), new[] { false, true, true, false, false, false, false, true, false, true }];
        object[] d186 = [GetDate(07, 04), new[] { false, true, true, false, false, false, false, true, true, false }];
        object[] d187 = [GetDate(07, 05), new[] { false, true, true, false, false, false, false, true, true, true }];
        object[] d188 = [GetDate(07, 06), new[] { false, true, true, false, false, false, true, false, false, false }];
        object[] d189 = [GetDate(07, 07), new[] { false, true, true, false, false, false, true, false, false, true }];
        object[] d190 = [GetDate(07, 08), new[] { false, true, true, false, false, true, false, false, false, false }];
        object[] d191 = [GetDate(07, 09), new[] { false, true, true, false, false, true, false, false, false, true }];
        object[] d192 = [GetDate(07, 10), new[] { false, true, true, false, false, true, false, false, true, false }];
        object[] d193 = [GetDate(07, 11), new[] { false, true, true, false, false, true, false, false, true, true }];
        object[] d194 = [GetDate(07, 12), new[] { false, true, true, false, false, true, false, true, false, false }];
        object[] d195 = [GetDate(07, 13), new[] { false, true, true, false, false, true, false, true, false, true }];
        object[] d196 = [GetDate(07, 14), new[] { false, true, true, false, false, true, false, true, true, false }];
        object[] d197 = [GetDate(07, 15), new[] { false, true, true, false, false, true, false, true, true, true }];
        object[] d198 = [GetDate(07, 16), new[] { false, true, true, false, false, true, true, false, false, false }];
        object[] d199 = [GetDate(07, 17), new[] { false, true, true, false, false, true, true, false, false, true }];
        object[] d200 =
            [GetDate(07, 18), new[] { true, false, false, false, false, false, false, false, false, false }];
        object[] d201 = [GetDate(07, 19), new[] { true, false, false, false, false, false, false, false, false, true }];
        object[] d202 = [GetDate(07, 20), new[] { true, false, false, false, false, false, false, false, true, false }];
        object[] d203 = [GetDate(07, 21), new[] { true, false, false, false, false, false, false, false, true, true }];
        object[] d204 = [GetDate(07, 22), new[] { true, false, false, false, false, false, false, true, false, false }];
        object[] d205 = [GetDate(07, 23), new[] { true, false, false, false, false, false, false, true, false, true }];
        object[] d206 = [GetDate(07, 24), new[] { true, false, false, false, false, false, false, true, true, false }];
        object[] d207 = [GetDate(07, 25), new[] { true, false, false, false, false, false, false, true, true, true }];
        object[] d208 = [GetDate(07, 26), new[] { true, false, false, false, false, false, true, false, false, false }];
        object[] d209 = [GetDate(07, 27), new[] { true, false, false, false, false, false, true, false, false, true }];
        object[] d210 = [GetDate(07, 28), new[] { true, false, false, false, false, true, false, false, false, false }];
        object[] d211 = [GetDate(07, 29), new[] { true, false, false, false, false, true, false, false, false, true }];
        object[] d212 = [GetDate(07, 30), new[] { true, false, false, false, false, true, false, false, true, false }];
        object[] d213 = [GetDate(07, 31), new[] { true, false, false, false, false, true, false, false, true, true }];
        object[] d214 = [GetDate(08, 01), new[] { true, false, false, false, false, true, false, true, false, false }];
        object[] d215 = [GetDate(08, 02), new[] { true, false, false, false, false, true, false, true, false, true }];
        object[] d216 = [GetDate(08, 03), new[] { true, false, false, false, false, true, false, true, true, false }];
        object[] d217 = [GetDate(08, 04), new[] { true, false, false, false, false, true, false, true, true, true }];
        object[] d218 = [GetDate(08, 05), new[] { true, false, false, false, false, true, true, false, false, false }];
        object[] d219 = [GetDate(08, 06), new[] { true, false, false, false, false, true, true, false, false, true }];
        object[] d220 = [GetDate(08, 07), new[] { true, false, false, false, true, false, false, false, false, false }];
        object[] d221 = [GetDate(08, 08), new[] { true, false, false, false, true, false, false, false, false, true }];
        object[] d222 = [GetDate(08, 09), new[] { true, false, false, false, true, false, false, false, true, false }];
        object[] d223 = [GetDate(08, 10), new[] { true, false, false, false, true, false, false, false, true, true }];
        object[] d224 = [GetDate(08, 11), new[] { true, false, false, false, true, false, false, true, false, false }];
        object[] d225 = [GetDate(08, 12), new[] { true, false, false, false, true, false, false, true, false, true }];
        object[] d226 = [GetDate(08, 13), new[] { true, false, false, false, true, false, false, true, true, false }];
        object[] d227 = [GetDate(08, 14), new[] { true, false, false, false, true, false, false, true, true, true }];
        object[] d228 = [GetDate(08, 15), new[] { true, false, false, false, true, false, true, false, false, false }];
        object[] d229 = [GetDate(08, 16), new[] { true, false, false, false, true, false, true, false, false, true }];
        object[] d230 = [GetDate(08, 17), new[] { true, false, false, false, true, true, false, false, false, false }];
        object[] d231 = [GetDate(08, 18), new[] { true, false, false, false, true, true, false, false, false, true }];
        object[] d232 = [GetDate(08, 19), new[] { true, false, false, false, true, true, false, false, true, false }];
        object[] d233 = [GetDate(08, 20), new[] { true, false, false, false, true, true, false, false, true, true }];
        object[] d234 = [GetDate(08, 21), new[] { true, false, false, false, true, true, false, true, false, false }];
        object[] d235 = [GetDate(08, 22), new[] { true, false, false, false, true, true, false, true, false, true }];
        object[] d236 = [GetDate(08, 23), new[] { true, false, false, false, true, true, false, true, true, false }];
        object[] d237 = [GetDate(08, 24), new[] { true, false, false, false, true, true, false, true, true, true }];
        object[] d238 = [GetDate(08, 25), new[] { true, false, false, false, true, true, true, false, false, false }];
        object[] d239 = [GetDate(08, 26), new[] { true, false, false, false, true, true, true, false, false, true }];
        object[] d240 = [GetDate(08, 27), new[] { true, false, false, true, false, false, false, false, false, false }];
        object[] d241 = [GetDate(08, 28), new[] { true, false, false, true, false, false, false, false, false, true }];
        object[] d242 = [GetDate(08, 29), new[] { true, false, false, true, false, false, false, false, true, false }];
        object[] d243 = [GetDate(08, 30), new[] { true, false, false, true, false, false, false, false, true, true }];
        object[] d244 = [GetDate(08, 31), new[] { true, false, false, true, false, false, false, true, false, false }];
        object[] d245 = [GetDate(09, 01), new[] { true, false, false, true, false, false, false, true, false, true }];
        object[] d246 = [GetDate(09, 02), new[] { true, false, false, true, false, false, false, true, true, false }];
        object[] d247 = [GetDate(09, 03), new[] { true, false, false, true, false, false, false, true, true, true }];
        object[] d248 = [GetDate(09, 04), new[] { true, false, false, true, false, false, true, false, false, false }];
        object[] d249 = [GetDate(09, 05), new[] { true, false, false, true, false, false, true, false, false, true }];
        object[] d250 = [GetDate(09, 06), new[] { true, false, false, true, false, true, false, false, false, false }];
        object[] d251 = [GetDate(09, 07), new[] { true, false, false, true, false, true, false, false, false, true }];
        object[] d252 = [GetDate(09, 08), new[] { true, false, false, true, false, true, false, false, true, false }];
        object[] d253 = [GetDate(09, 09), new[] { true, false, false, true, false, true, false, false, true, true }];
        object[] d254 = [GetDate(09, 10), new[] { true, false, false, true, false, true, false, true, false, false }];
        object[] d255 = [GetDate(09, 11), new[] { true, false, false, true, false, true, false, true, false, true }];
        object[] d256 = [GetDate(09, 12), new[] { true, false, false, true, false, true, false, true, true, false }];
        object[] d257 = [GetDate(09, 13), new[] { true, false, false, true, false, true, false, true, true, true }];
        object[] d258 = [GetDate(09, 14), new[] { true, false, false, true, false, true, true, false, false, false }];
        object[] d259 = [GetDate(09, 15), new[] { true, false, false, true, false, true, true, false, false, true }];
        object[] d260 = [GetDate(09, 16), new[] { true, false, false, true, true, false, false, false, false, false }];
        object[] d261 = [GetDate(09, 17), new[] { true, false, false, true, true, false, false, false, false, true }];
        object[] d262 = [GetDate(09, 18), new[] { true, false, false, true, true, false, false, false, true, false }];
        object[] d263 = [GetDate(09, 19), new[] { true, false, false, true, true, false, false, false, true, true }];
        object[] d264 = [GetDate(09, 20), new[] { true, false, false, true, true, false, false, true, false, false }];
        object[] d265 = [GetDate(09, 21), new[] { true, false, false, true, true, false, false, true, false, true }];
        object[] d266 = [GetDate(09, 22), new[] { true, false, false, true, true, false, false, true, true, false }];
        object[] d267 = [GetDate(09, 23), new[] { true, false, false, true, true, false, false, true, true, true }];
        object[] d268 = [GetDate(09, 24), new[] { true, false, false, true, true, false, true, false, false, false }];
        object[] d269 = [GetDate(09, 25), new[] { true, false, false, true, true, false, true, false, false, true }];
        object[] d270 = [GetDate(09, 26), new[] { true, false, false, true, true, true, false, false, false, false }];
        object[] d271 = [GetDate(09, 27), new[] { true, false, false, true, true, true, false, false, false, true }];
        object[] d272 = [GetDate(09, 28), new[] { true, false, false, true, true, true, false, false, true, false }];
        object[] d273 = [GetDate(09, 29), new[] { true, false, false, true, true, true, false, false, true, true }];
        object[] d274 = [GetDate(09, 30), new[] { true, false, false, true, true, true, false, true, false, false }];
        object[] d275 = [GetDate(10, 01), new[] { true, false, false, true, true, true, false, true, false, true }];
        object[] d276 = [GetDate(10, 02), new[] { true, false, false, true, true, true, false, true, true, false }];
        object[] d277 = [GetDate(10, 03), new[] { true, false, false, true, true, true, false, true, true, true }];
        object[] d278 = [GetDate(10, 04), new[] { true, false, false, true, true, true, true, false, false, false }];
        object[] d279 = [GetDate(10, 05), new[] { true, false, false, true, true, true, true, false, false, true }];
        object[] d280 = [GetDate(10, 06), new[] { true, false, true, false, false, false, false, false, false, false }];
        object[] d281 = [GetDate(10, 07), new[] { true, false, true, false, false, false, false, false, false, true }];
        object[] d282 = [GetDate(10, 08), new[] { true, false, true, false, false, false, false, false, true, false }];
        object[] d283 = [GetDate(10, 09), new[] { true, false, true, false, false, false, false, false, true, true }];
        object[] d284 = [GetDate(10, 10), new[] { true, false, true, false, false, false, false, true, false, false }];
        object[] d285 = [GetDate(10, 11), new[] { true, false, true, false, false, false, false, true, false, true }];
        object[] d286 = [GetDate(10, 12), new[] { true, false, true, false, false, false, false, true, true, false }];
        object[] d287 = [GetDate(10, 13), new[] { true, false, true, false, false, false, false, true, true, true }];
        object[] d288 = [GetDate(10, 14), new[] { true, false, true, false, false, false, true, false, false, false }];
        object[] d289 = [GetDate(10, 15), new[] { true, false, true, false, false, false, true, false, false, true }];
        object[] d290 = [GetDate(10, 16), new[] { true, false, true, false, false, true, false, false, false, false }];
        object[] d291 = [GetDate(10, 17), new[] { true, false, true, false, false, true, false, false, false, true }];
        object[] d292 = [GetDate(10, 18), new[] { true, false, true, false, false, true, false, false, true, false }];
        object[] d293 = [GetDate(10, 19), new[] { true, false, true, false, false, true, false, false, true, true }];
        object[] d294 = [GetDate(10, 20), new[] { true, false, true, false, false, true, false, true, false, false }];
        object[] d295 = [GetDate(10, 21), new[] { true, false, true, false, false, true, false, true, false, true }];
        object[] d296 = [GetDate(10, 22), new[] { true, false, true, false, false, true, false, true, true, false }];
        object[] d297 = [GetDate(10, 23), new[] { true, false, true, false, false, true, false, true, true, true }];
        object[] d298 = [GetDate(10, 24), new[] { true, false, true, false, false, true, true, false, false, false }];
        object[] d299 = [GetDate(10, 25), new[] { true, false, true, false, false, true, true, false, false, true }];
        object[] d300 = [GetDate(10, 26), new[] { true, true, false, false, false, false, false, false, false, false }];
        object[] d301 = [GetDate(10, 27), new[] { true, true, false, false, false, false, false, false, false, true }];
        object[] d302 = [GetDate(10, 28), new[] { true, true, false, false, false, false, false, false, true, false }];
        object[] d303 = [GetDate(10, 29), new[] { true, true, false, false, false, false, false, false, true, true }];
        object[] d304 = [GetDate(10, 30), new[] { true, true, false, false, false, false, false, true, false, false }];
        object[] d305 = [GetDate(10, 31), new[] { true, true, false, false, false, false, false, true, false, true }];
        object[] d306 = [GetDate(11, 01), new[] { true, true, false, false, false, false, false, true, true, false }];
        object[] d307 = [GetDate(11, 02), new[] { true, true, false, false, false, false, false, true, true, true }];
        object[] d308 = [GetDate(11, 03), new[] { true, true, false, false, false, false, true, false, false, false }];
        object[] d309 = [GetDate(11, 04), new[] { true, true, false, false, false, false, true, false, false, true }];
        object[] d310 = [GetDate(11, 05), new[] { true, true, false, false, false, true, false, false, false, false }];
        object[] d311 = [GetDate(11, 06), new[] { true, true, false, false, false, true, false, false, false, true }];
        object[] d312 = [GetDate(11, 07), new[] { true, true, false, false, false, true, false, false, true, false }];
        object[] d313 = [GetDate(11, 08), new[] { true, true, false, false, false, true, false, false, true, true }];
        object[] d314 = [GetDate(11, 09), new[] { true, true, false, false, false, true, false, true, false, false }];
        object[] d315 = [GetDate(11, 10), new[] { true, true, false, false, false, true, false, true, false, true }];
        object[] d316 = [GetDate(11, 11), new[] { true, true, false, false, false, true, false, true, true, false }];
        object[] d317 = [GetDate(11, 12), new[] { true, true, false, false, false, true, false, true, true, true }];
        object[] d318 = [GetDate(11, 13), new[] { true, true, false, false, false, true, true, false, false, false }];
        object[] d319 = [GetDate(11, 14), new[] { true, true, false, false, false, true, true, false, false, true }];
        object[] d320 = [GetDate(11, 15), new[] { true, true, false, false, true, false, false, false, false, false }];
        object[] d321 = [GetDate(11, 16), new[] { true, true, false, false, true, false, false, false, false, true }];
        object[] d322 = [GetDate(11, 17), new[] { true, true, false, false, true, false, false, false, true, false }];
        object[] d323 = [GetDate(11, 18), new[] { true, true, false, false, true, false, false, false, true, true }];
        object[] d324 = [GetDate(11, 19), new[] { true, true, false, false, true, false, false, true, false, false }];
        object[] d325 = [GetDate(11, 20), new[] { true, true, false, false, true, false, false, true, false, true }];
        object[] d326 = [GetDate(11, 21), new[] { true, true, false, false, true, false, false, true, true, false }];
        object[] d327 = [GetDate(11, 22), new[] { true, true, false, false, true, false, false, true, true, true }];
        object[] d328 = [GetDate(11, 23), new[] { true, true, false, false, true, false, true, false, false, false }];
        object[] d329 = [GetDate(11, 24), new[] { true, true, false, false, true, false, true, false, false, true }];
        object[] d330 = [GetDate(11, 25), new[] { true, true, false, false, true, true, false, false, false, false }];
        object[] d331 = [GetDate(11, 26), new[] { true, true, false, false, true, true, false, false, false, true }];
        object[] d332 = [GetDate(11, 27), new[] { true, true, false, false, true, true, false, false, true, false }];
        object[] d333 = [GetDate(11, 28), new[] { true, true, false, false, true, true, false, false, true, true }];
        object[] d334 = [GetDate(11, 29), new[] { true, true, false, false, true, true, false, true, false, false }];
        object[] d335 = [GetDate(11, 30), new[] { true, true, false, false, true, true, false, true, false, true }];
        object[] d336 = [GetDate(12, 01), new[] { true, true, false, false, true, true, false, true, true, false }];
        object[] d337 = [GetDate(12, 02), new[] { true, true, false, false, true, true, false, true, true, true }];
        object[] d338 = [GetDate(12, 03), new[] { true, true, false, false, true, true, true, false, false, false }];
        object[] d339 = [GetDate(12, 04), new[] { true, true, false, false, true, true, true, false, false, true }];
        object[] d340 = [GetDate(12, 05), new[] { true, true, false, true, false, false, false, false, false, false }];
        object[] d341 = [GetDate(12, 06), new[] { true, true, false, true, false, false, false, false, false, true }];
        object[] d342 = [GetDate(12, 07), new[] { true, true, false, true, false, false, false, false, true, false }];
        object[] d343 = [GetDate(12, 08), new[] { true, true, false, true, false, false, false, false, true, true }];
        object[] d344 = [GetDate(12, 09), new[] { true, true, false, true, false, false, false, true, false, false }];
        object[] d345 = [GetDate(12, 10), new[] { true, true, false, true, false, false, false, true, false, true }];
        object[] d346 = [GetDate(12, 11), new[] { true, true, false, true, false, false, false, true, true, false }];
        object[] d347 = [GetDate(12, 12), new[] { true, true, false, true, false, false, false, true, true, true }];
        object[] d348 = [GetDate(12, 13), new[] { true, true, false, true, false, false, true, false, false, false }];
        object[] d349 = [GetDate(12, 14), new[] { true, true, false, true, false, false, true, false, false, true }];
        object[] d350 = [GetDate(12, 15), new[] { true, true, false, true, false, true, false, false, false, false }];
        object[] d351 = [GetDate(12, 16), new[] { true, true, false, true, false, true, false, false, false, true }];
        object[] d352 = [GetDate(12, 17), new[] { true, true, false, true, false, true, false, false, true, false }];
        object[] d353 = [GetDate(12, 18), new[] { true, true, false, true, false, true, false, false, true, true }];
        object[] d354 = [GetDate(12, 19), new[] { true, true, false, true, false, true, false, true, false, false }];
        object[] d355 = [GetDate(12, 20), new[] { true, true, false, true, false, true, false, true, false, true }];
        object[] d356 = [GetDate(12, 21), new[] { true, true, false, true, false, true, false, true, true, false }];
        object[] d357 = [GetDate(12, 22), new[] { true, true, false, true, false, true, false, true, true, true }];
        object[] d358 = [GetDate(12, 23), new[] { true, true, false, true, false, true, true, false, false, false }];
        object[] d359 = [GetDate(12, 24), new[] { true, true, false, true, false, true, true, false, false, true }];
        object[] d360 = [GetDate(12, 25), new[] { true, true, false, true, true, false, false, false, false, false }];
        object[] d361 = [GetDate(12, 26), new[] { true, true, false, true, true, false, false, false, false, true }];
        object[] d362 = [GetDate(12, 27), new[] { true, true, false, true, true, false, false, false, true, false }];
        object[] d363 = [GetDate(12, 28), new[] { true, true, false, true, true, false, false, false, true, true }];
        object[] d364 = [GetDate(12, 29), new[] { true, true, false, true, true, false, false, true, false, false }];
        object[] d365 = [GetDate(12, 30), new[] { true, true, false, true, true, false, false, true, false, true }];
        object[] d366 = [GetDate(12, 31), new[] { true, true, false, true, true, false, false, true, true, false }];


        return
        [
            d001, d002, d003, d004, d005, d006, d007, d008, d009, d010, d011, d012, d013, d014, d015, d016, d017, d018,
            d019, d020, d021, d022, d023, d024, d025, d026, d027, d028, d029, d030, d031, d032, d033, d034, d035, d036,
            d037, d038, d039, d040, d041, d042, d043, d044, d045, d046, d047, d048, d049, d050, d051, d052, d053, d054,
            d055, d056, d057, d058, d059, d060, d061, d062, d063, d064, d065, d066, d067, d068, d069, d070, d071, d072,
            d073, d074, d075, d076, d077, d078, d079, d080, d081, d082, d083, d084, d085, d086, d087, d088, d089, d090,
            d091, d092, d093, d094, d095, d096, d097, d098, d099, d100, d101, d102, d103, d104, d105, d106, d107, d108,
            d109, d110, d111, d112, d113, d114, d115, d116, d117, d118, d119, d120, d121, d122, d123, d124, d125, d126,
            d127, d128, d129, d130, d131, d132, d133, d134, d135, d136, d137, d138, d139, d140, d141, d142, d143, d144,
            d145, d146, d147, d148, d149, d150, d151, d152, d153, d154, d155, d156, d157, d158, d159, d160, d161, d162,
            d163, d164, d165, d166, d167, d168, d169, d170, d171, d172, d173, d174, d175, d176, d177, d178, d179, d180,
            d181, d182, d183, d184, d185, d186, d187, d188, d189, d190, d191, d192, d193, d194, d195, d196, d197, d198,
            d199, d200, d201, d202, d203, d204, d205, d206, d207, d208, d209, d210, d211, d212, d213, d214, d215, d216,
            d217, d218, d219, d220, d221, d222, d223, d224, d225, d226, d227, d228, d229, d230, d231, d232, d233, d234,
            d235, d236, d237, d238, d239, d240, d241, d242, d243, d244, d245, d246, d247, d248, d249, d250, d251, d252,
            d253, d254, d255, d256, d257, d258, d259, d260, d261, d262, d263, d264, d265, d266, d267, d268, d269, d270,
            d271, d272, d273, d274, d275, d276, d277, d278, d279, d280, d281, d282, d283, d284, d285, d286, d287, d288,
            d289, d290, d291, d292, d293, d294, d295, d296, d297, d298, d299, d300, d301, d302, d303, d304, d305, d306,
            d307, d308, d309, d310, d311, d312, d313, d314, d315, d316, d317, d318, d319, d320, d321, d322, d323, d324,
            d325, d326, d327, d328, d329, d330, d331, d332, d333, d334, d335, d336, d337, d338, d339, d340, d341, d342,
            d343, d344, d345, d346, d347, d348, d349, d350, d351, d352, d353, d354, d355, d356, d357, d358, d359, d360,
            d361, d362, d363, d364, d365, d366,
        ];

        static DateTime GetDate(int month, int day) => new(2024, month, day, 0, 0, 0);
    }

    private static object[] GetHourParityBitIsCorrectTestData()
    {
        object[] h00 = [GetDate(00), false];
        object[] h01 = [GetDate(01), true];

        return [h00, h01];

        static DateTime GetDate(int hour) => new(2000, 1, 1, hour, 1, 1);
    }

    private static object[] GetMinuteParityBitIsCorrectTestData()
    {
        object[] m00 = [GetDate(00), false];
        object[] m01 = [GetDate(01), true];

        return [m00, m01];

        static DateTime GetDate(int minute) => new(2000, 1, 1, 1, minute, 1);
    }

    private static object[] GetYearBitsAreCorrectTestData()
    {
        object[] y2000 = [GetDate(2000), new[] { false, false, false, false, false, false, false, false }];
        object[] y2001 = [GetDate(2001), new[] { false, false, false, false, false, false, false, true }];
        object[] y2002 = [GetDate(2002), new[] { false, false, false, false, false, false, true, false }];
        object[] y2003 = [GetDate(2003), new[] { false, false, false, false, false, false, true, true }];
        object[] y2004 = [GetDate(2004), new[] { false, false, false, false, false, true, false, false }];
        object[] y2005 = [GetDate(2005), new[] { false, false, false, false, false, true, false, true }];
        object[] y2006 = [GetDate(2006), new[] { false, false, false, false, false, true, true, false }];
        object[] y2007 = [GetDate(2007), new[] { false, false, false, false, false, true, true, true }];
        object[] y2008 = [GetDate(2008), new[] { false, false, false, false, true, false, false, false }];
        object[] y2009 = [GetDate(2009), new[] { false, false, false, false, true, false, false, true }];
        object[] y2010 = [GetDate(2010), new[] { false, false, false, true, false, false, false, false }];
        object[] y2011 = [GetDate(2011), new[] { false, false, false, true, false, false, false, true }];
        object[] y2012 = [GetDate(2012), new[] { false, false, false, true, false, false, true, false }];
        object[] y2013 = [GetDate(2013), new[] { false, false, false, true, false, false, true, true }];
        object[] y2014 = [GetDate(2014), new[] { false, false, false, true, false, true, false, false }];
        object[] y2015 = [GetDate(2015), new[] { false, false, false, true, false, true, false, true }];
        object[] y2016 = [GetDate(2016), new[] { false, false, false, true, false, true, true, false }];
        object[] y2017 = [GetDate(2017), new[] { false, false, false, true, false, true, true, true }];
        object[] y2018 = [GetDate(2018), new[] { false, false, false, true, true, false, false, false }];
        object[] y2019 = [GetDate(2019), new[] { false, false, false, true, true, false, false, true }];
        object[] y2020 = [GetDate(2020), new[] { false, false, true, false, false, false, false, false }];
        object[] y2021 = [GetDate(2021), new[] { false, false, true, false, false, false, false, true }];
        object[] y2022 = [GetDate(2022), new[] { false, false, true, false, false, false, true, false }];
        object[] y2023 = [GetDate(2023), new[] { false, false, true, false, false, false, true, true }];
        object[] y2024 = [GetDate(2024), new[] { false, false, true, false, false, true, false, false }];
        object[] y2025 = [GetDate(2025), new[] { false, false, true, false, false, true, false, true }];
        object[] y2026 = [GetDate(2026), new[] { false, false, true, false, false, true, true, false }];
        object[] y2027 = [GetDate(2027), new[] { false, false, true, false, false, true, true, true }];
        object[] y2028 = [GetDate(2028), new[] { false, false, true, false, true, false, false, false }];
        object[] y2029 = [GetDate(2029), new[] { false, false, true, false, true, false, false, true }];
        object[] y2030 = [GetDate(2030), new[] { false, false, true, true, false, false, false, false }];
        object[] y2031 = [GetDate(2031), new[] { false, false, true, true, false, false, false, true }];
        object[] y2032 = [GetDate(2032), new[] { false, false, true, true, false, false, true, false }];
        object[] y2033 = [GetDate(2033), new[] { false, false, true, true, false, false, true, true }];
        object[] y2034 = [GetDate(2034), new[] { false, false, true, true, false, true, false, false }];
        object[] y2035 = [GetDate(2035), new[] { false, false, true, true, false, true, false, true }];
        object[] y2036 = [GetDate(2036), new[] { false, false, true, true, false, true, true, false }];
        object[] y2037 = [GetDate(2037), new[] { false, false, true, true, false, true, true, true }];
        object[] y2038 = [GetDate(2038), new[] { false, false, true, true, true, false, false, false }];
        object[] y2039 = [GetDate(2039), new[] { false, false, true, true, true, false, false, true }];
        object[] y2040 = [GetDate(2040), new[] { false, true, false, false, false, false, false, false }];
        object[] y2041 = [GetDate(2041), new[] { false, true, false, false, false, false, false, true }];
        object[] y2042 = [GetDate(2042), new[] { false, true, false, false, false, false, true, false }];
        object[] y2043 = [GetDate(2043), new[] { false, true, false, false, false, false, true, true }];
        object[] y2044 = [GetDate(2044), new[] { false, true, false, false, false, true, false, false }];
        object[] y2045 = [GetDate(2045), new[] { false, true, false, false, false, true, false, true }];
        object[] y2046 = [GetDate(2046), new[] { false, true, false, false, false, true, true, false }];
        object[] y2047 = [GetDate(2047), new[] { false, true, false, false, false, true, true, true }];
        object[] y2048 = [GetDate(2048), new[] { false, true, false, false, true, false, false, false }];
        object[] y2049 = [GetDate(2049), new[] { false, true, false, false, true, false, false, true }];
        object[] y2050 = [GetDate(2050), new[] { false, true, false, true, false, false, false, false }];
        object[] y2051 = [GetDate(2051), new[] { false, true, false, true, false, false, false, true }];
        object[] y2052 = [GetDate(2052), new[] { false, true, false, true, false, false, true, false }];
        object[] y2053 = [GetDate(2053), new[] { false, true, false, true, false, false, true, true }];
        object[] y2054 = [GetDate(2054), new[] { false, true, false, true, false, true, false, false }];
        object[] y2055 = [GetDate(2055), new[] { false, true, false, true, false, true, false, true }];
        object[] y2056 = [GetDate(2056), new[] { false, true, false, true, false, true, true, false }];
        object[] y2057 = [GetDate(2057), new[] { false, true, false, true, false, true, true, true }];
        object[] y2058 = [GetDate(2058), new[] { false, true, false, true, true, false, false, false }];
        object[] y2059 = [GetDate(2059), new[] { false, true, false, true, true, false, false, true }];
        object[] y2060 = [GetDate(2060), new[] { false, true, true, false, false, false, false, false }];
        object[] y2061 = [GetDate(2061), new[] { false, true, true, false, false, false, false, true }];
        object[] y2062 = [GetDate(2062), new[] { false, true, true, false, false, false, true, false }];
        object[] y2063 = [GetDate(2063), new[] { false, true, true, false, false, false, true, true }];
        object[] y2064 = [GetDate(2064), new[] { false, true, true, false, false, true, false, false }];
        object[] y2065 = [GetDate(2065), new[] { false, true, true, false, false, true, false, true }];
        object[] y2066 = [GetDate(2066), new[] { false, true, true, false, false, true, true, false }];
        object[] y2067 = [GetDate(2067), new[] { false, true, true, false, false, true, true, true }];
        object[] y2068 = [GetDate(2068), new[] { false, true, true, false, true, false, false, false }];
        object[] y2069 = [GetDate(2069), new[] { false, true, true, false, true, false, false, true }];
        object[] y2070 = [GetDate(2070), new[] { false, true, true, true, false, false, false, false }];
        object[] y2071 = [GetDate(2071), new[] { false, true, true, true, false, false, false, true }];
        object[] y2072 = [GetDate(2072), new[] { false, true, true, true, false, false, true, false }];
        object[] y2073 = [GetDate(2073), new[] { false, true, true, true, false, false, true, true }];
        object[] y2074 = [GetDate(2074), new[] { false, true, true, true, false, true, false, false }];
        object[] y2075 = [GetDate(2075), new[] { false, true, true, true, false, true, false, true }];
        object[] y2076 = [GetDate(2076), new[] { false, true, true, true, false, true, true, false }];
        object[] y2077 = [GetDate(2077), new[] { false, true, true, true, false, true, true, true }];
        object[] y2078 = [GetDate(2078), new[] { false, true, true, true, true, false, false, false }];
        object[] y2079 = [GetDate(2079), new[] { false, true, true, true, true, false, false, true }];
        object[] y2080 = [GetDate(2080), new[] { true, false, false, false, false, false, false, false }];
        object[] y2081 = [GetDate(2081), new[] { true, false, false, false, false, false, false, true }];
        object[] y2082 = [GetDate(2082), new[] { true, false, false, false, false, false, true, false }];
        object[] y2083 = [GetDate(2083), new[] { true, false, false, false, false, false, true, true }];
        object[] y2084 = [GetDate(2084), new[] { true, false, false, false, false, true, false, false }];
        object[] y2085 = [GetDate(2085), new[] { true, false, false, false, false, true, false, true }];
        object[] y2086 = [GetDate(2086), new[] { true, false, false, false, false, true, true, false }];
        object[] y2087 = [GetDate(2087), new[] { true, false, false, false, false, true, true, true }];
        object[] y2088 = [GetDate(2088), new[] { true, false, false, false, true, false, false, false }];
        object[] y2089 = [GetDate(2089), new[] { true, false, false, false, true, false, false, true }];
        object[] y2090 = [GetDate(2090), new[] { true, false, false, true, false, false, false, false }];
        object[] y2091 = [GetDate(2091), new[] { true, false, false, true, false, false, false, true }];
        object[] y2092 = [GetDate(2092), new[] { true, false, false, true, false, false, true, false }];
        object[] y2093 = [GetDate(2093), new[] { true, false, false, true, false, false, true, true }];
        object[] y2094 = [GetDate(2094), new[] { true, false, false, true, false, true, false, false }];
        object[] y2095 = [GetDate(2095), new[] { true, false, false, true, false, true, false, true }];
        object[] y2096 = [GetDate(2096), new[] { true, false, false, true, false, true, true, false }];
        object[] y2097 = [GetDate(2097), new[] { true, false, false, true, false, true, true, true }];
        object[] y2098 = [GetDate(2098), new[] { true, false, false, true, true, false, false, false }];
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

    private static object[] GetWeekDayBitsAreCorrectTestData()
    {
        object[] mon = [GetDate(3), new[] { false, false, true }];
        object[] tue = [GetDate(4), new[] { false, true, false }];
        object[] wed = [GetDate(5), new[] { false, true, true }];
        object[] thu = [GetDate(6), new[] { true, false, false }];
        object[] fri = [GetDate(7), new[] { true, false, true }];
        object[] sat = [GetDate(8), new[] { true, true, false }];
        object[] sun = [GetDate(9), new[] { false, false, false }];

        return [mon, tue, wed, thu, fri, sat, sun];

        static DateTime GetDate(int day) => new(2000, 1, day, 1, 1, 1);
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