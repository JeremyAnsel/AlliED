using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x00541E18
{
    public const int Size = 0x00CC;

    /* 0x0000 */
    public byte m000000;
    /* 0xF000001 */
    public byte[] unk000001 = new byte[5];
    /* 0x0006 */
    public short m000006;
    /* 0x0008 */
    private string _PrimarySuccessMessage1 = string.Empty;
    public string PrimarySuccessMessage1 { get => _PrimarySuccessMessage1; set => _PrimarySuccessMessage1 = value.WithMaxLength(64); }
    /* 0x0048 */
    private string _PrimarySuccessMessage2 = string.Empty;
    public string PrimarySuccessMessage2 { get => _PrimarySuccessMessage2; set => _PrimarySuccessMessage2 = value.WithMaxLength(64); }
    /* 0x0088 */
    private string _SecondarySuccessMessage1 = string.Empty;
    public string SecondarySuccessMessage1 { get => _SecondarySuccessMessage1; set => _SecondarySuccessMessage1 = value.WithMaxLength(64); }
    /* 0x00C8 */
    public short m0000C8;
    /* 0x00CA */
    public short m0000CA;

    public static S0x00541E18 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x00541E18();
        buffer.m000000 = array[0x00];
        array.ReadUnknown(0x01, buffer.unk000001);
        buffer.m000006 = BitConverter.ToInt16(array, 0x06);
        buffer.PrimarySuccessMessage1 = array.ReadFixedLengthString(0x08, 64);
        buffer.PrimarySuccessMessage2 = array.ReadFixedLengthString(0x48, 64);
        buffer.SecondarySuccessMessage1 = array.ReadFixedLengthString(0x88, 64);
        buffer.m0000C8 = BitConverter.ToInt16(array, 0xC8);
        buffer.m0000CA = BitConverter.ToInt16(array, 0xCA);
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array[0x00] = m000000;
        array.WriteUnknown(0x01, unk000001);
        BitConverter.GetBytes(m000006).CopyTo(array, 0x06);
        array.WriteFixedLengthString(0x08, PrimarySuccessMessage1, 64);
        array.WriteFixedLengthString(0x48, PrimarySuccessMessage2, 64);
        array.WriteFixedLengthString(0x88, SecondarySuccessMessage1, 64);
        BitConverter.GetBytes(m0000C8).CopyTo(array, 0xC8);
        BitConverter.GetBytes(m0000CA).CopyTo(array, 0xCA);
        return array;
    }
}
