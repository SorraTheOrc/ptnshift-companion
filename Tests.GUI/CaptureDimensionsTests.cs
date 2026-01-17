using Core.Capturing;
using Shouldly;
using Xunit;

namespace Tests.GUI;

public class CaptureDimensionsTests
{
    [Fact]
    public void Should_match_presets_from_frame_length()
    {
        CaptureDimensionPresets.TryFromFrameLength(
            CaptureDimensionPresets.Small.FrameByteLength,
            out var small).ShouldBeTrue();
        small.ShouldBe(CaptureDimensionPresets.Small);

        CaptureDimensionPresets.TryFromFrameLength(
            CaptureDimensionPresets.Large.FrameByteLength,
            out var large).ShouldBeTrue();
        large.ShouldBe(CaptureDimensionPresets.Large);
    }

    [Fact]
    public void Should_match_presets_from_bgra_length()
    {
        var length = CaptureDimensionPresets.Small.Width
                     * CaptureDimensionPresets.Small.CaptureHeight
                     * 4;

        CaptureDimensionPresets.TryFromFrameLength(length, out var dimensions).ShouldBeTrue();
        dimensions.ShouldBe(CaptureDimensionPresets.Small);
    }
}
