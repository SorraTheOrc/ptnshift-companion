using System.Diagnostics.CodeAnalysis;
using Core.Image;
using Shouldly;
using SkiaSharp;

namespace Tests.GUI;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class ImageConverterTests
{
    private ImageConverter Sut { get; } = new();

    private static byte[] GetBytesFromFile(string fileName) => File.ReadAllBytes(fileName);

    [Fact]
    public void When_converting_to_bitmap()
    {
        var bytes = GetBytesFromFile("bgra8888bytes.bin");
        const int width = 960;
        const int height = 160;

        using var result = Sut.ConvertPixelBytesToBitmap(bytes, SKColorType.Bgra8888, width, height);

        result.Width.ShouldBe(width);
        result.Height.ShouldBe(height);
    }

    [Fact]
    public void When_converting_to_16bit()
    {
        var bytes = GetBytesFromFile("bgra8888bytes.bin");
        const int width = 960;
        const int height = 160;
        var frame = new byte[2048 * 160];

        Sut.ConvertBgra32ToRgb16(bytes, frame, width, height);

        var expected = GetBytesFromFile("push2framebytes.bin");
        frame.ShouldBe(expected);
    }
}
