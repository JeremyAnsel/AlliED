namespace AlliED.Impl.Enumerations;

internal enum TMsgDlgBtnEnum : ushort
{
    None = 0,
    Yes = 1,
    No = 2,
    OK = 4,
    Cancel = 8,
    Abort = 16,
    Retry = 32,
    Ignore = 64,
    All = 128,
    NoToAll = 256,
    YesToAll = 512,
    bHelp = 1024,
    Close = 2048,
}
