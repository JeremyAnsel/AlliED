using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004CF108
{
    public const int Size = 0x0094;

    /* 0x0000 */
    private string _M000000 = string.Empty;
    public string M000000 { get => _M000000; set => _M000000 = value.WithMaxLength(16); }
    /* 0x0010 */
    private string _M000010 = string.Empty;
    public string M000010 { get => _M000010; set => _M000010 = value.WithMaxLength(16); }
    /* 0x0020 */
    private string _M000020 = string.Empty;
    public string M000020 { get => _M000020; set => _M000020 = value.WithMaxLength(16); }
    /* 0x0030 */
    public short m000030;
    /* 0x0032 */
    public short m000032;
    /* 0x0034 */
    public short m000034;
    /* 0x0036 */
    public short m000036;
    /* 0x0038 */
    public short m000038;
    /* 0x003A */
    public byte m00003A;
    /* 0xF00003B */
    public byte[] unk00003B = new byte[1];
    /* 0x003C */
    public short m00003C;
    /* 0x003E */
    public short m00003E;
    /* 0x0040 */
    public byte m000040;
    /* 0xF000041 */
    public byte[] unk000041 = new byte[1];
    /* 0x0042 */
    public short m000042;
    /* 0x0044 */
    public short m000044;
    /* 0x0046 */
    public short m000046;
    /* 0x0048 */
    public short[] m000048 = new short[21];
    /* 0x0072 */
    public short m000072;
    /* 0x0074 */
    public short[] m000074 = new short[3];
    /* 0x007A */
    public short[] m00007A = new short[2];
    /* 0x007E */
    public short m00007E;
    /* 0x0080 */
    public byte m000080;
    /* 0xF000081 */
    public byte[] unk000081 = new byte[1];
    /* 0x0082 */
    public short m000082;
    /* 0x0084 */
    public byte m000084;
    /* 0xF000085 */
    public byte[] unk000085 = new byte[1];
    /* 0x0086 */
    public short m000086;
    /* 0x0088 */
    public short m000088;
    /* 0xF00008A */
    public byte[] unk00008A = new byte[2];
    /* 0x008C */
    public byte m00008C;
    /* 0xF00008D */
    public byte[] unk00008D = new byte[1];
    /* 0x008E */
    public short m00008E;
    /* 0x0090 */
    public short m000090;
    /* 0x0092 */
    public short m000092;

    public static S0x004CF108 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004CF108();
        obj.M000000 = array.ReadFixedLengthString(0x00, 16);
        obj.M000010 = array.ReadFixedLengthString(0x10, 16);
        obj.M000020 = array.ReadFixedLengthString(0x20, 16);
        obj.m000030 = BitConverter.ToInt16(array, 0x30);
        obj.m000032 = BitConverter.ToInt16(array, 0x32);
        obj.m000034 = BitConverter.ToInt16(array, 0x34);
        obj.m000036 = BitConverter.ToInt16(array, 0x36);
        obj.m000038 = BitConverter.ToInt16(array, 0x38);
        obj.m00003A = array[0x3A];
        array.ReadUnknown(0x3B, obj.unk00003B);
        obj.m00003C = BitConverter.ToInt16(array, 0x3C);
        obj.m00003E = BitConverter.ToInt16(array, 0x3E);
        obj.m000040 = array[0x40];
        array.ReadUnknown(0x41, obj.unk000041);
        obj.m000042 = BitConverter.ToInt16(array, 0x42);
        obj.m000044 = BitConverter.ToInt16(array, 0x44);
        obj.m000046 = BitConverter.ToInt16(array, 0x46);
        for (int i = 0; i < 21; i++)
        {
            obj.m000048[i] = BitConverter.ToInt16(array, 0x48 + i * 0x02);
        }
        obj.m000072 = BitConverter.ToInt16(array, 0x72);
        for (int i = 0; i < 3; i++)
        {
            obj.m000074[i] = BitConverter.ToInt16(array, 0x74 + i * 0x02);
        }
        for (int i = 0; i < 2; i++)
        {
            obj.m00007A[i] = BitConverter.ToInt16(array, 0x7A + i * 0x02);
        }
        obj.m00007E = BitConverter.ToInt16(array, 0x7E);
        obj.m000080 = array[0x80];
        array.ReadUnknown(0x81, obj.unk000081);
        obj.m000082 = BitConverter.ToInt16(array, 0x82);
        obj.m000084 = array[0x84];
        array.ReadUnknown(0x85, obj.unk000085);
        obj.m000086 = BitConverter.ToInt16(array, 0x86);
        obj.m000088 = BitConverter.ToInt16(array, 0x88);
        array.ReadUnknown(0x8A, obj.unk00008A);
        obj.m00008C = array[0x8C];
        array.ReadUnknown(0x8D, obj.unk00008D);
        obj.m00008E = BitConverter.ToInt16(array, 0x8E);
        obj.m000090 = BitConverter.ToInt16(array, 0x90);
        obj.m000092 = BitConverter.ToInt16(array, 0x92);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x00, M000000, 16);
        array.WriteFixedLengthString(0x10, M000010, 16);
        array.WriteFixedLengthString(0x20, M000020, 16);
        BitConverter.GetBytes(m000030).CopyTo(array, 0x30);
        BitConverter.GetBytes(m000032).CopyTo(array, 0x32);
        BitConverter.GetBytes(m000034).CopyTo(array, 0x34);
        BitConverter.GetBytes(m000036).CopyTo(array, 0x36);
        BitConverter.GetBytes(m000038).CopyTo(array, 0x38);
        array[0x3A] = m00003A;
        array.WriteUnknown(0x3B, unk00003B);
        BitConverter.GetBytes(m00003C).CopyTo(array, 0x3C);
        BitConverter.GetBytes(m00003E).CopyTo(array, 0x3E);
        array[0x40] = m000040;
        array.WriteUnknown(0x41, unk000041);
        BitConverter.GetBytes(m000042).CopyTo(array, 0x42);
        BitConverter.GetBytes(m000044).CopyTo(array, 0x44);
        BitConverter.GetBytes(m000046).CopyTo(array, 0x46);
        for (int i = 0; i < 21; i++)
        {
            BitConverter.GetBytes(m000048[i]).CopyTo(array, 0x48 + i * 0x02);
        }
        BitConverter.GetBytes(m000072).CopyTo(array, 0x72);
        for (int i = 0; i < 3; i++)
        {
            BitConverter.GetBytes(m000074[i]).CopyTo(array, 0x74 + i * 0x02);
        }
        for (int i = 0; i < 2; i++)
        {
            BitConverter.GetBytes(m00007A[i]).CopyTo(array, 0x7A + i * 0x02);
        }
        BitConverter.GetBytes(m00007E).CopyTo(array, 0x7E);
        array[0x80] = m000080;
        array.WriteUnknown(0x81, unk000081);
        BitConverter.GetBytes(m000082).CopyTo(array, 0x82);
        array[0x84] = m000084;
        array.WriteUnknown(0x85, unk000085);
        BitConverter.GetBytes(m000086).CopyTo(array, 0x86);
        BitConverter.GetBytes(m000088).CopyTo(array, 0x88);
        array.WriteUnknown(0x8A, unk00008A);
        array[0x8C] = m00008C;
        array.WriteUnknown(0x8D, unk00008D);
        BitConverter.GetBytes(m00008E).CopyTo(array, 0x8E);
        BitConverter.GetBytes(m000090).CopyTo(array, 0x90);
        BitConverter.GetBytes(m000092).CopyTo(array, 0x92);
        return array;
    }
}
