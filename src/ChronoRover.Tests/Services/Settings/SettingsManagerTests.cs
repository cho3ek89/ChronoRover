using ChronoRover.Models;
using ChronoRover.Providers.Settings;
using ChronoRover.Services.Settings;

using Moq;

using NUnit.Framework;

using SoundFlow.Structs;

namespace ChronoRover.Tests.Services.Settings;

[TestFixture]
public class SettingsManagerTests
{
    [Test]
    public void SettingsAreInitializedProperly()
    {
        var settingsProvider = new Mock<ISettingsProvider>();
        settingsProvider.Setup(s => s.GetAudioFormat())
            .Returns(AudioFormat.Cd)
            .Verifiable();
        settingsProvider.Setup(s => s.GetSignalType())
            .Returns(SignalType.Msf)
            .Verifiable();

        var settingsManager = new SettingsManager(settingsProvider.Object);

        Assert.That(settingsManager.AudioFormat, Is.EqualTo(AudioFormat.Cd));
        Assert.That(settingsManager.SignalType, Is.EqualTo(SignalType.Msf));

        settingsProvider.Verify();
    }
}