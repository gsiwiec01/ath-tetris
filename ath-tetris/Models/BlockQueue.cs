using System;
using ath_tetris.Models.Blocks;

namespace ath_tetris.Models;

public class BlockQueue
{
    private readonly Random _random = new();
    private readonly BlockBase[] _blocks = new BlockBase[]
    {
        new IBlock(),
        new JBlock(),
        new LBlock(),
        new OBlock(),
        new SBlock(),
        new TBlock(),
        new ZBlock()
    };
    
    public BlockBase NextBlock { get; private set; }

    public BlockQueue()
    {
        NextBlock = GetRandomBlock();
    }

    public BlockBase GetAndUpdate()
    {
        var block = NextBlock;

        do
        {
            NextBlock = GetRandomBlock();
        } while (block.Type == NextBlock.Type);

        return block;
    }

    private BlockBase GetRandomBlock() => _blocks[_random.Next(_blocks.Length)];
}