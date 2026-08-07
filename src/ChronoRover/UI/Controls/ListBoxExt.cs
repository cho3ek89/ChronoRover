using Avalonia.Controls;
using Avalonia.Input;

using System;

namespace ChronoRover.UI.Controls;

public class ListBoxExt : ListBox
{
    protected override Type StyleKeyOverride => typeof(ListBox);

    /// <remarks>
    /// Overrides the default <see cref="ListBox"/> behavior that changes
    /// the selected item when the user presses the arrow keys.
    /// </remarks>
    protected override void OnKeyDown(KeyEventArgs e)
    {
    }
}