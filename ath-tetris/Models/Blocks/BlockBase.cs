using System;
using System.Collections.Generic;
using System.Linq;
using ath_tetris.Enums;

namespace ath_tetris.Models.Blocks;

public abstract class BlockBase
{
    public abstract BlockType Type { get; }
    
    protected abstract Position[][] Tiles { get; }
    protected abstract Position StartOffset { get; }

    private int _rotation;
    private Position _offset;

    public BlockBase()
    {
        _offset = new Position(StartOffset.X, StartOffset.Y);
    }

    public IEnumerable<Position> TilePositions(bool skipOffset = false)
    {
        return Tiles[_rotation].Select(pos =>
        {
            var x = skipOffset ? pos.X : pos.X + _offset.X;
            var y = skipOffset ? pos.Y : pos.Y + _offset.Y;
            
            return new Position(x, y);
        });
    }

    public void RotateClockwise()
    {
        _rotation = (_rotation + 1) % Tiles.Length;
    }

    public void RotateCounterClockwise()
    {
        _rotation = _rotation == 0 ? Tiles.Length - 1 : _rotation - 1;
    }

    public void Move(int xOffset, int yOffset)
    {
        _offset.X += xOffset;
        _offset.Y += yOffset;
    }

    public void Reset()
    {
        _rotation = 0;
        _offset.X = StartOffset.X;
        _offset.Y = StartOffset.Y;
    }
}