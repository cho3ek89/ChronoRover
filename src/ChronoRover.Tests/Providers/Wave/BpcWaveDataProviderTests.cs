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
public class BpcWaveDataProviderTests
{
    [Test]
    [TestCaseSource(nameof(GetWaveDataIsCorrectForSecondTestData))]
    public void WaveDataIsCorrectForSecond(int second, bool[] signalBits, string expectedSamplesFileName)
    {
        const int sampleRate = 44_100;

        var provider = new BpcWaveDataProvider(
            GetSignalProviderFactory(signalBits));

        var samples = provider.GetWaveData(sampleRate, new DateTime()).Chunk(sampleRate).ToArray();
        var samplesPerSecond = samples[second == 0 ? 59 : second - 1];

        var expectedSamplesPerSecond = GetSamplesFromCsv(expectedSamplesFileName);

        Assert.That(samplesPerSecond, Is.EqualTo(expectedSamplesPerSecond));
    }

    [Test]
    [TestCaseSource(nameof(GetWaveDataIsCorrectlyStrippedTestData))]
    public void WaveDataLengthIsCorrect(int second, int millisecond, int sampleRate, int expectedSamplesCount)
    {
        var dateTime = new DateTime(2020, 10, 10, 1, 1, second, millisecond);

        var provider = new BpcWaveDataProvider(
            GetSignalProviderFactory(new bool[120]));

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
        var result = new bool[120];
        foreach (var id in idsOfTrueBits)
            result[id] = true;

        return result;
    }

    private static short[] GetSamplesFromCsv(string fileName)
    {
        var filePath = Path.Combine("TestData", "Wave", "Providers", "BpcWaveDataProviderTests", fileName);
        var data = CsvUtils.GetCsvData(filePath);

        return data.Select(s => Convert.ToInt16(s[0])).ToArray();
    }

    #endregion Helpers

    #region Test data

    private static object[] GetWaveDataIsCorrectForSecondTestData()
    {
        var tcs = new object[60 * 4];

        for (var sec = 0; sec < 60; sec++)
        {
            tcs[sec * 4 + 0] = new object[]
            {
                sec,
                GetSignalBits(),
                IsMarkerSecond(sec) ? "SamplesForMarker.csv" : "SamplesFor00.csv"
            };
            tcs[sec * 4 + 1] = new object[]
            {
                sec,
                GetSignalBits(sec * 2 + 1),
                IsMarkerSecond(sec) ? "SamplesForMarker.csv" : "SamplesFor01.csv"
            };
            tcs[sec * 4 + 2] = new object[]
            {
                sec,
                GetSignalBits(sec * 2),
                IsMarkerSecond(sec) ? "SamplesForMarker.csv" : "SamplesFor10.csv"
            };
            tcs[sec * 4 + 3] = new object[]
            {
                sec,
                GetSignalBits(sec * 2, sec * 2 + 1),
                IsMarkerSecond(sec) ? "SamplesForMarker.csv" : "SamplesFor11.csv"
            };
        }

        return tcs;

        static bool IsMarkerSecond(int second) => second is 0 or 20 or 40;
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