﻿using System.Drawing;

namespace TagCloudPainter.Coloring;

public class WhiteWordColoring : IWordColoring
{
    public Color GetColor()
    {
        return Color.White;
    }
}