using System;
using System.Linq;
using ath_tetris.Enums;
using ReactiveUI;

namespace ath_tetris.Models;

public class GameBoard
{
    private readonly BlockType[,] _board;
    public BlockType this[int x, int y]
    {
        get => _board[x, y];
        set => _board[x, y] = value;
    }
    
    public int Rows => _board.GetLength(0);
    public int Columns => _board.GetLength(1);

    public GameBoard(int rows, int columns)
    {
        _board = new BlockType[rows, columns];
    }

    public bool IsInside(int x, int y) => x >= 0 && x < Rows && y >= 0 && y < Columns;

    public bool IsEmpty(int x, int y) => IsInside(x, y) && _board[x, y] == BlockType.Clear;

    public bool IsRowFull(int x)
    {
        for (var y = 0; y < Columns; y++)
        {
            if (_board[x, y] == 0)
                return false;
        }

        return true;
    }

    public bool IsRowEmpty(int x)
    {
        for (var y = 0; y < Columns; y++)
        {
            if (_board[x, y] != 0)
                return false;
        }

        return true;
    }

    public void ClearRow(int x)
    {
        for (var y = 0; y < Columns; y++)
        {
            _board[x, y] = BlockType.Clear;
        }
    }

    public void MoveRowDown(int x, int numRows)
    {
        for (var y = 0; y < Columns; y++)
        {
            _board[x + numRows, y] = _board[x, y];
            _board[x, y] = BlockType.Clear;
        }
    }

    public int ClearFullRows()
    {
        var cleared = 0;

        for (var x = Rows - 1; x >= 0; x--)
        {
            if (IsRowFull(x))
            {
                ClearRow(x);
                cleared++;
            }
            else if (cleared > 0)
            {
                MoveRowDown(x, cleared);
            }
        }

        return cleared * Columns;
    }
}