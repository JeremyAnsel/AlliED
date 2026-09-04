using System.Windows;

namespace AlliED.Impl.Structures;

internal struct TPoint
{
    public int X;
    public int Y;

    public TPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point ToPoint()
    {
        return new Point(X, Y);
    }
}
