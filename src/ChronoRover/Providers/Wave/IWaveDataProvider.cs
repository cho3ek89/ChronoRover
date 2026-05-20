using System;

namespace ChronoRover.Providers.Wave;

public interface IWaveDataProvider
{
    short[] GetWaveData(int sampleRate, DateTime dateTime, bool stripPassedMs = false);
}