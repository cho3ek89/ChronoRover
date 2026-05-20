using CommunityToolkit.Mvvm.Messaging.Messages;

using System;

namespace ChronoRover.UI.Models.Messages;

public class ErrorMessage(string title, Exception value)
    : ValueChangedMessage<(string, Exception)>((title, value))
{
    public void Deconstruct(out string title, out Exception exception)
    {
        title = Value.Item1;
        exception = Value.Item2;
    }
}