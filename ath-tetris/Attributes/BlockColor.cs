using System;
using System.Text.RegularExpressions;
using Avalonia.Media;

namespace ath_tetris.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class BlockColor : Attribute
{
    private IBrush? Color { get; }

    public BlockColor(string hex)
    {
        var regex = new Regex(@"^#[0-9a-fA-F]{6}$");
        if (!regex.IsMatch(hex))
        {
            throw new ArgumentException("Invalid brush string: " + hex);
        }
        
        Color = (SolidColorBrush) new BrushConverter().ConvertFrom(hex);
    }

    public IBrush GetColor() => Color;
}