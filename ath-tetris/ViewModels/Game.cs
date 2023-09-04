using System;
using System.Linq;
using ath_tetris.Models;
using ath_tetris.Models.Blocks;
using ReactiveUI;

namespace ath_tetris.ViewModels;

public class Game : ReactiveObject
{
    public GameBoard Board { get; } = new(22, 10);
    public BlockQueue Queue { get; } = new();
    
    public bool GameOver { get; private set; }
    public int Score { get; private set; }

    private BlockBase _currBlock;
    public BlockBase CurrBlock
    {
        get => _currBlock;
        private set
        {
            _currBlock = value;
            _currBlock.Reset();
        }
    }

    public Game()
    {
        CurrBlock = Queue.GetAndUpdate();
    }

    public bool BlockFits() => CurrBlock.TilePositions().All(pos => Board.IsEmpty(pos.X, pos.Y));

    public void RotateBlockClockwise()
    {
        CurrBlock.RotateClockwise();
        if (!BlockFits()) CurrBlock.RotateCounterClockwise();
    }

    public void RotateBlockCounterClockwise()
    {
        CurrBlock.RotateCounterClockwise();
        if (!BlockFits()) CurrBlock.RotateClockwise();
    }

    private void MoveBlock(int x, int y, Action? action = null)
    {
        CurrBlock.Move(x, y);
        if (BlockFits()) return;
        
        CurrBlock.Move(x * -1, y * -1);
        action?.Invoke();
    }
    
    public void MoveBlockLeft() => MoveBlock(0, 1);

    public void MoveBlockRight() => MoveBlock(0, -1);
    
    public void MoveBlockDown() => MoveBlock(1, 0, PlaceBlock);

    private bool IsGameOver() => !(Board.IsRowEmpty(0) && Board.IsRowEmpty(1));

    private void PlaceBlock()
    {
        foreach (var pos in CurrBlock.TilePositions())
        {
            Board[pos.X, pos.Y] = CurrBlock.Type;
        }

        Score += Board.ClearFullRows();

        if (IsGameOver())
        {
            GameOver = true;
        }
        else
        {
            CurrBlock = Queue.GetAndUpdate();
        }
    }
}