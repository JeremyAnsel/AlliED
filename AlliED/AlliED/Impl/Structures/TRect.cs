using System.Windows;

namespace AlliED.Impl.Structures;

public struct TRect
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public TRect(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Rect ToRect()
    {
        return new Rect(Left, Top, Right - Left, Bottom - Top);
    }

    public Int32Rect ToInt32Rect()
    {
        return new Int32Rect(Left, Top, Right - Left, Bottom - Top);
    }
}
