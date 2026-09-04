using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xXvTGGStrings
{
    public const int Size = 0x00C0;

    /* 0x0000 */
    private string _StrIncomp = string.Empty;
    public string StrIncomp { get => _StrIncomp; set => _StrIncomp = value.WithMaxLength(64); }

    /* 0x0040 */
    private string _StrSucc = string.Empty;
    public string StrSucc { get => _StrSucc; set => _StrSucc = value.WithMaxLength(64); }

    /* 0x0080 */
    private string _StrFail = string.Empty;
    public string StrFail { get => _StrFail; set => _StrFail = value.WithMaxLength(64); }

    public static S0xXvTGGStrings FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var strings = new S0xXvTGGStrings();
        strings.StrIncomp = array.ReadFixedLengthString(0x00, 64);
        strings.StrSucc = array.ReadFixedLengthString(0x40, 64);
        strings.StrFail = array.ReadFixedLengthString(0x80, 64);
        return strings;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x00, StrIncomp, 64);
        array.WriteFixedLengthString(0x40, StrSucc, 64);
        array.WriteFixedLengthString(0x80, StrFail, 64);
        return array;
    }
}
