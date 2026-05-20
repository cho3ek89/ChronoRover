using ChronoRover.Models;
using ChronoRover.Providers.Signal;
using ChronoRover.Providers.Wave;
using ChronoRover.Tests.TestUtils;

using Moq;

using NUnit.Framework;

using System;
using System.IO;
using System.Linq;

namespace ChronoRover.Tests.Providers.Wave;

[TestFixture]
public class MsfWaveDataProviderTests
{
    [Test]
    [TestCaseSource(nameof(GetWaveDataIsCorrectForSecondTestData))]
    public void WaveDataIsCorrectForSecond(int second, bool[] signalBits, string expectedSamplesFileName)
    {
        const int sampleRate = 44_100;

        var provider = new MsfWaveDataProvider(
            GetSignalProviderFactory(signalBits));

        var samples = provider.GetWaveData(sampleRate, new DateTime()).Chunk(sampleRate).ToArray();
        var samplesPerSecond = samples[second];

        var expectedSamplesPerSecond = GetSamplesFromCsv(expectedSamplesFileName);

        Assert.That(samplesPerSecond, Is.EqualTo(expectedSamplesPerSecond));
    }

    [Test]
    [TestCaseSource(nameof(GetWaveDataIsCorrectlyStrippedTestData))]
    public void WaveDataLengthIsCorrect(int second, int millisecond, int sampleRate, int expectedSamplesCount)
    {
        var dateTime = new DateTime(2020, 10, 10, 1, 1, second, millisecond);

        var provider = new MsfWaveDataProvider(
            GetSignalProviderFactory(new bool[60]));

        var samples = provider.GetWaveData(sampleRate, dateTime, true);
        var samplesCount = samples.Length;

        Assert.That(samplesCount, Is.EqualTo(expectedSamplesCount));
    }

    #region Helpers

    private static ISignalProviderFactory GetSignalProviderFactory(bool[] array)
    {
        var signalProviderMock = new Mock<ISignalProvider>();
        signalProviderMock.Setup(s => s.GetMinuteSignal(It.IsAny<DateTime>()))
            .Returns(() => array);

        var signalProviderFactoryMock = new Mock<ISignalProviderFactory>();
        signalProviderFactoryMock.Setup(s => s.GetSignalProvider(It.IsAny<SignalType>()))
            .Returns(() => signalProviderMock.Object);

        return signalProviderFactoryMock.Object;
    }

    private static bool[] GetSignalBits(params int[] idsOfTrueBits)
    {
        var result = new bool[60];
        foreach (var id in idsOfTrueBits)
            result[id] = true;

        return result;
    }

    private static short[] GetSamplesFromCsv(string fileName)
    {
        var filePath = Path.Combine("TestData", "Wave", "Providers", "MsfWaveDataProviderTests", fileName);
        var data = CsvUtils.GetCsvData(filePath);

        return data.Select(s => Convert.ToInt16(s[0])).ToArray();
    }

    #endregion Helpers

    #region Test data

    private static object[] GetWaveDataIsCorrectForSecondTestData()
    {
        var tcs = new object[120];

        tcs[0] = new object[] { 0, GetSignalBits(), "SamplesForMarker.csv" };
        tcs[1] = new object[] { 0, GetSignalBits(0), "SamplesForMarker.csv" };

        for (var sec = 1; sec < 17; sec++)
        {
            tcs[sec * 2] = new object[] { sec, GetSignalBits(), "SamplesFor00.csv" };
            tcs[sec * 2 + 1] = new object[] { sec, GetSignalBits(sec), "SamplesFor01.csv" };
        }

        for (var sec = 17; sec < 52; sec++)
        {
            tcs[sec * 2] = new object[] { sec, GetSignalBits(), "SamplesFor00.csv" };
            tcs[sec * 2 + 1] = new object[] { sec, GetSignalBits(sec), "SamplesFor10.csv" };
        }

        tcs[52 * 2] = new object[] { 52, GetSignalBits(), "SamplesFor00.csv" };
        tcs[52 * 2 + 1] = new object[] { 52, GetSignalBits(52), "SamplesFor00.csv" };

        for (var sec = 53; sec < 59; sec++)
        {
            tcs[sec * 2] = new object[] { sec, GetSignalBits(), "SamplesFor10.csv" };
            tcs[sec * 2 + 1] = new object[] { sec, GetSignalBits(sec), "SamplesFor11.csv" };
        }

        tcs[59 * 2] = new object[] { 59, GetSignalBits(), "SamplesFor00.csv" };
        tcs[59 * 2 + 1] = new object[] { 59, GetSignalBits(59), "SamplesFor00.csv" };

        return tcs;
    }

    private static object[] GetWaveDataIsCorrectlyStrippedTestData()
    {
        const int sr1 = 100_000;
        const int sr2 = 44_100;

        return
        [
            new object[] { 00, 000, sr1, 6_000_000 },
            new object[] { 06, 000, sr1, 5_400_000 },
            new object[] { 00, 500, sr1, 5_950_000 },
            new object[] { 06, 400, sr1, 5_360_000 },
            new object[] { 30, 001, sr1, 2_999_900 },

            new object[] { 00, 000, sr2, 2_646_000 },
            new object[] { 07, 000, sr2, 2_337_300 },
            new object[] { 00, 523, sr2, 2_622_936 },
            new object[] { 08, 452, sr2, 2_273_267 },
            new object[] { 38, 101, sr2, 0_965_746 },
        ];
    }

    #endregion Test data
}