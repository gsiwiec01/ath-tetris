using ath_tetris.Attributes;
using Avalonia.Media;

namespace app_tests;

public class BlockColorTests
{
    [Fact]
    public void CanCreateBlockColorAttributeWithValidHex()
    {
        var attribute = new BlockColor("#FF0000");
        Assert.NotNull(attribute.Color);
        Assert.Equal(new SolidColorBrush(Color.FromRgb(255, 0, 0)), attribute.Color);
    }

    [Fact]
    public void CanCreateBlockColorAttributeWithInvalidHex()
    {
        var attribute = new BlockColor("invalid");
        Assert.Null(attribute.Color);
    }
}