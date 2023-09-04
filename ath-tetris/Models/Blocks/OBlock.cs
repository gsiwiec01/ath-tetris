using ath_tetris.Enums;

namespace ath_tetris.Models.Blocks;

public class OBlock : BlockBase
{
    public override BlockType Type => BlockType.O;

    protected override Position[][] Tiles => new[]
    {
        new Position[] {new(0, 0), new(0, 1), new(1, 0), new(1, 1)}
    };

    protected override Position StartOffset => new(0, 4);
}