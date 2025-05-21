using System.Runtime.CompilerServices;
using Avalonia.Controls;
using AvaloniaExampleProject.Assets;

namespace AvaloniaExampleProject.Tests;

public static class VerifyHelpers
{
    public static SettingsTask VerifyControl(Control control, [CallerFilePath] string? callerFilePath = null)
    {
        string directory =
            Path.GetFileNameWithoutExtension(callerFilePath) ?? throw new ArgumentNullException(nameof(callerFilePath));
        return Verify(control).ScrubMembersWithType<Resources>().UseDirectory(Path.Join("Snapshots", directory));
    }
}
