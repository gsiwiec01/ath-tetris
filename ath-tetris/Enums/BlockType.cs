using ath_tetris.Attributes;
using Avalonia.Media;

namespace ath_tetris.Enums;

public enum BlockType
{
    [BlockColor("#000000")]
    Clear = 0,
    
    [BlockColor("#FF0000")]
    I = 1,
    
    [BlockColor("#FF00FF")]
    J = 2,
    
    [BlockColor("#FFFF00")]
    L = 3,
    
    [BlockColor("#00FFFF")]
    O = 4,
    
    [BlockColor("#0000FF")]
    S = 5,
    
    [BlockColor("#999999")]
    T = 6,
    
    [BlockColor("#00FF00")]
    Z = 7,
}