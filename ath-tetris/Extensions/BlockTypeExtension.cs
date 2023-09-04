using ath_tetris.Attributes;
using ath_tetris.Enums;
using Avalonia.Media;

namespace ath_tetris.Extensions;

public static class BlockTypeExtension
{
    public static IBrush GetColor(this BlockType blockType)
    {
        var memberInfo = typeof(BlockType).GetMember(blockType.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(BlockColor), false);
        if (attributes.Length > 0 && attributes[0] is BlockColor blockColorAttribute)
        {
            return blockColorAttribute.GetColor();
        }

        return Brushes.Transparent;
    }
}