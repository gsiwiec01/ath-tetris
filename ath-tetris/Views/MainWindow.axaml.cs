using System;
using System.Linq;
using System.Threading.Tasks;
using ath_tetris.Enums;
using ath_tetris.Extensions;
using ath_tetris.Models.Blocks;
using ath_tetris.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace ath_tetris.Views;

public partial class MainWindow : Window
{
    private readonly TextBlock _scoreText;
    private readonly StackPanel _blockPreviewsPanel;
    private readonly StackPanel _gameOverPanel;
    
    private readonly Canvas _board;
    private Rectangle[,] _boardControls;

    private readonly Canvas _currBlock;
    private Rectangle[,] _currBlockControls;

    private readonly Canvas _nextBlock;
    private Rectangle[,] _nextBlockControls;

    private Game _game;

    private const int TileSize = 25;

    public MainWindow()
    {
        Opened += OpenedHandler;

        InitializeComponent();

        _game = new Game();

        _board = this.FindControl<Canvas>("Board");
        _boardControls = SetupCanvas(_board, _game.Board.Rows, _game.Board.Columns);

        _currBlock = this.FindControl<Canvas>("CurrBlock");
        _currBlock.Width = 4 * TileSize;
        _currBlock.Height = 4 * TileSize;
        _currBlock.Background = Brushes.Black;

        _nextBlock = this.FindControl<Canvas>("NextBlock");
        _nextBlock.Width = 4 * TileSize;
        _nextBlock.Height = 4 * TileSize;
        _nextBlock.Background = Brushes.Black;
        
        _scoreText = this.FindControl<TextBlock>("Score");
        _blockPreviewsPanel = this.FindControl<StackPanel>("BlockPreviewsPanel");
        _gameOverPanel = this.FindControl<StackPanel>("GameOverPanel");
    }

    private Rectangle[,] SetupCanvas(Canvas canvas, int rows, int columns)
    {
        var controls = new Rectangle[columns, rows];

        for (var x = 0; x < columns; x++)
        {
            for (var y = 0; y < rows; y++)
            {
                var rect = new Rectangle
                {
                    Width = TileSize,
                    Height = TileSize,
                    Fill = BlockType.Clear.GetColor()
                };

                Canvas.SetLeft(rect, x * TileSize);
                Canvas.SetTop(rect, y * TileSize);

                canvas.Children.Add(rect);
                controls[x, y] = rect;
            }
        }

        return controls;
    }

    private void Draw()
    {
        DrawBoard();
        DrawCurrentBlock();
        DrawBlockPreview(_game.CurrBlock, _currBlock);
        DrawBlockPreview(_game.Queue.NextBlock, _nextBlock);

        _scoreText.Text = _game.Score.ToString();
        _gameOverPanel.IsVisible = _game.GameOver;
        _blockPreviewsPanel.IsVisible = !_game.GameOver;
    }

    private async Task Loop()
    {
        Draw();

        while (!_game.GameOver)
        {
            var delay = Math.Max(75, 1000 - _game.Score * 25);
            await Task.Delay(delay);

            _game.MoveBlockDown();
            Draw();
        }
    }

    private void DrawBoard()
    {
        for (var x = 0; x < _game.Board.Columns; x++)
        {
            for (var y = 0; y < _game.Board.Rows; y++)
            {
                _boardControls[x, y].Opacity = 1;
                _boardControls[x, y].Fill = _game.Board[y, x].GetColor();
            }
        }
    }

    private void DrawBlockPreview(BlockBase block, Canvas canvas)
    {
        var center = canvas.Width / 2;

        var tilePositions = block.TilePositions(true).ToList();
        var blockWidth = tilePositions.Max(p => p.X) * TileSize;
        var blockHeight = tilePositions.Max(p => p.Y) * TileSize;

        var startX = center - (blockWidth / 2d);
        var startY = center - (blockHeight / 2d);
        
        canvas.Children.Clear();
        
        foreach (var pos in tilePositions)
        {
            var x = startX + (pos.X * TileSize) - TileSize / 2d;
            var y = startY + (pos.Y * TileSize) - TileSize / 2d;
            
            var rect = new Rectangle
            {
                Width = TileSize,
                Height = TileSize,
                Fill = block.Type.GetColor(),
            };
            
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            
            canvas.Children.Add(rect);
        }
    }

    private void DrawCurrentBlock()
    {
        foreach (var pos in _game.CurrBlock.TilePositions())
        {
            _boardControls[pos.Y, pos.X].Opacity = 1;
            _boardControls[pos.Y, pos.X].Fill = _game.CurrBlock.Type.GetColor();
        }
    }

    public void KeyDownHandler(object sender, KeyEventArgs e)
    {
        if (_game.GameOver)
        {
            return;
        }

        switch (e.Key)
        {
            case Key.D:
                _game.MoveBlockLeft();
                break;
            case Key.A:
                _game.MoveBlockRight();
                break;
            case Key.S:
                _game.MoveBlockDown();
                break;
            case Key.E:
                _game.RotateBlockClockwise();
                break;
            case Key.Q:
                _game.RotateBlockCounterClockwise();
                break;

            default:
                return;
        }

        Draw();
    }

    private async void OpenedHandler(object? sender, EventArgs e)
    {
        await Loop();
    }

    private async void StartNewGame(object? sender, RoutedEventArgs e)
    {
        _game = new Game();
        await Loop();
    }
}