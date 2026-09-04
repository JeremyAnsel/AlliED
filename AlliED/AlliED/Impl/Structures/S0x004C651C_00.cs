using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004C651C_00
{
    public const int Size = 0x005A;

    /* 0x0000 */
    private string _M000000 = string.Empty;
    public string M000000 { get => _M000000; set => _M000000 = value.WithMaxLength(64); }
    /* 0x0040 */
    public uint m000040;
    /* 0x0044 */
    public uint m000044;
    /* 0x0048 */
    public byte m000048;
    /* 0xF000049 */
    public byte[] unk000049 = new byte[1];
    /* 0x004A */
    public byte m00004A;
    /* 0x004B */
    public byte m00004B;
    /* 0xF00004C */
    public byte[] unk00004C = new byte[12];
    /* 0x0058 */
    public byte m000058;
    /* 0xF000059 */
    public byte[] unk000059 = new byte[1];

    public static S0x004C651C_00 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004C651C_00();
        obj.M000000 = array.ReadFixedLengthString(0x00, 64);
        obj.m000040 = BitConverter.ToUInt32(array, 0x40);
        obj.m000044 = BitConverter.ToUInt32(array, 0x44);
        obj.m000048 = array[0x48];
        obj.unk000049 = array.Subarray(0x49, 1);
        obj.m00004A = array[0x4A];
        obj.m00004B = array[0x4B];
        obj.unk00004C = array.Subarray(0x4C, 12);
        obj.m000058 = array[0x58];
        obj.unk000059 = array.Subarray(0x59, 1);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x000, M000000, 64);
        BitConverter.GetBytes(m000040).CopyTo(array, 0x40);
        BitConverter.GetBytes(m000044).CopyTo(array, 0x44);
        array[0x48] = m000048;
        unk000049.CopyTo(array, 0x49);
        array[0x4A] = m00004A;
        array[0x4B] = m00004B;
        unk00004C.CopyTo(array, 0x4C);
        array[0x58] = m000058;
        unk000059.CopyTo(array, 0x59);
        return array;
    }
}
