using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xFGObject_000E42_000000
{
    public const int Size = 0x00C0;

    /* 0x0000 */
    private string _m000000 = string.Empty;
    public string M000000 { get => _m000000; set => _m000000 = value.WithMaxLength(64); }

    /* 0x0040 */
    private string _m000040 = string.Empty;
    public string M000040 { get => _m000040; set => _m000040 = value.WithMaxLength(64); }

    /* 0x0080 */
    private string _m000080 = string.Empty;
    public string M000080 { get => _m000080; set => _m000080 = value.WithMaxLength(64); }

    public static S0xFGObject_000E42_000000 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0xFGObject_000E42_000000();
        obj.M000000 = array.ReadFixedLengthString(0x00, 0x40);
        obj.M000040 = array.ReadFixedLengthString(0x40, 0x40);
        obj.M000080 = array.ReadFixedLengthString(0x80, 0x40);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x00, M000000, 0x40);
        array.WriteFixedLengthString(0x40, M000040, 0x40);
        array.WriteFixedLengthString(0x80, M000080, 0x40);
        return array;
    }
}
