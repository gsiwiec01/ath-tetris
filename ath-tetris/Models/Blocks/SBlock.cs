﻿using ath_tetris.Enums;

namespace ath_tetris.Models.Blocks;

public class SBlock : BlockBase
{
    public override BlockType Type => BlockType.S;
    protected override Position[][] Tiles => new[]
    {
        new Position[] {new(0, 1), new(0, 2), new(1, 0), new(1, 1)},
        new Position[] {new(0, 1), new(1, 1), new(1, 2), new(2, 2)},
        new Position[] {new(1, 1), new(1, 2), new(2, 0), new(2, 1)},
        new Position[] {new(0, 0), new(1, 0), new(1, 1), new(2, 1)},
    };

    protected override Position StartOffset => new(0, 3);
}