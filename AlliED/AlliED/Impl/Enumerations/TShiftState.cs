namespace AlliED.Impl.Enumerations;

[Flags]
internal enum TShiftState
{
    None = 0,

    Shift = 1,

    Alt = 2,

    Control = 4,

    LeftButton = 8,

    RightButton = 16,
}
