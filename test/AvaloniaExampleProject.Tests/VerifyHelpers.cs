using System.Runtime.CompilerServices;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using AvaloniaExampleProject.Assets;

namespace AvaloniaExampleProject.Tests;

public static class VerifyHelpers
{
    public static SettingsTask VerifyControl(TemplatedControl control, [CallerFilePath] string? callerFilePath = null)
    {
        control.FontFamily = new FontFamily("avares://Avalonia.Fonts.Inter/Assets/Inter-Regular.ttf#Inter");
        string directory =
            Path.GetFileNameWithoutExtension(callerFilePath) ?? throw new ArgumentNullException(nameof(callerFilePath));
        return Verify(control).ScrubMembersWithType<Resources>().UseDirectory(Path.Join("Snapshots", directory));
    }
}
