using System.Runtime.CompilerServices;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using AvaloniaExampleProject.Assets;
using Shouldly;

namespace AvaloniaExampleProject.Tests;

public static class VerifyHelpers
{
    public static SettingsTask VerifyControl(TemplatedControl control, [CallerFilePath] string? callerFilePath = null)
    {
        var fontFamily = new FontFamily("avares://Avalonia.Fonts.Inter/Assets/Inter-Regular.ttf#Inter");
        control.FontFamily = fontFamily;
        control.Resources.Add("ContentControlThemeFontFamily", fontFamily);
        control.FontFamily.Name.ShouldBe("Inter");
        control.FontFamily.FamilyTypefaces.Count.ShouldBeGreaterThan(0);
        string directory =
            Path.GetFileNameWithoutExtension(callerFilePath) ?? throw new ArgumentNullException(nameof(callerFilePath));
        return Verify(control).ScrubMembersWithType<Resources>().UseDirectory(Path.Join("Snapshots", directory));
    }
}
