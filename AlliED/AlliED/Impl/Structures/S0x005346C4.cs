using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x005346C4
{
    public const int Size = 0x0180;

    /* 0x0000 */
    private string _M000000 = string.Empty;
    public string M000000 { get => _M000000; set => _M000000 = value.WithMaxLength(64); }

    /* 0x0040 */
    private string _M000040 = string.Empty;
    public string M000040 { get => _M000040; set => _M000040 = value.WithMaxLength(64); }

    /* 0x0080 */
    private string _M000080 = string.Empty;
    public string M000080 { get => _M000080; set => _M000080 = value.WithMaxLength(64); }

    /* 0x00C0 */
    private string _M0000C0 = string.Empty;
    public string M0000C0 { get => _M0000C0; set => _M0000C0 = value.WithMaxLength(64); }

    /* 0x0100 */
    private string _M000100 = string.Empty;
    public string M000100 { get => _M000100; set => _M000100 = value.WithMaxLength(64); }

    /* 0x0140 */
    private string _M000140 = string.Empty;
    public string M000140 { get => _M000140; set => _M000140 = value.WithMaxLength(64); }

    public static S0x005346C4 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x005346C4();
        buffer.M000000 = array.ReadFixedLengthString(0x000, 64);
        buffer.M000040 = array.ReadFixedLengthString(0x040, 64);
        buffer.M000080 = array.ReadFixedLengthString(0x080, 64);
        buffer.M0000C0 = array.ReadFixedLengthString(0x0C0, 64);
        buffer.M000100 = array.ReadFixedLengthString(0x100, 64);
        buffer.M000140 = array.ReadFixedLengthString(0x140, 64);
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x000, M000000, 64);
        array.WriteFixedLengthString(0x040, M000040, 64);
        array.WriteFixedLengthString(0x080, M000080, 64);
        array.WriteFixedLengthString(0x0C0, M0000C0, 64);
        array.WriteFixedLengthString(0x100, M000100, 64);
        array.WriteFixedLengthString(0x140, M000140, 64);
        return array;
    }
}
