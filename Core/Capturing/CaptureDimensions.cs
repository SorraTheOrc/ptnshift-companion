using System.Collections.Immutable;
using System.Linq;

namespace Core.Capturing;

public record CaptureDimensions(int Width, int VisibleHeight)
{
    public int CaptureHeight => VisibleHeight + 1;
    public int FrameByteLength => Width * CaptureHeight * CaptureDimensionPresets.RgbBytesPerPixel;
    public override string ToString() => $"{Width}x{VisibleHeight}";
}

public static class CaptureDimensionPresets
{
    public const int RgbBytesPerPixel = 3;
    public const int BgraBytesPerPixel = 4;
    public const int DefaultWidth = 1280;
    public const int DefaultVisibleHeight = 160;

    public static CaptureDimensions Small { get; } = new(960, 120);
    public static CaptureDimensions Large { get; } = new(1280, 160);

    public static CaptureDimensions Default { get; } = Large;

    public static ImmutableArray<CaptureDimensions> All { get; } =
        [Small, Large];

    public static bool TryFromFrameLength(int length, out CaptureDimensions dimensions)
    {
        var match = All.FirstOrDefault(x =>
            x.FrameByteLength == length
            || x.Width * x.CaptureHeight * BgraBytesPerPixel == length);
        if (match == default)
        {
            dimensions = Default;
            return false;
        }

        dimensions = match;
        return true;
    }
}
