using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004D0FBC
{
    public const int Size = 0x0046;

    /* 0x0000 */
    private string _Name = string.Empty;
    public string Name { get => _Name; set => _Name = value.WithMaxLength(50); }
    /* 0x0032 */
    public short m000032;
    /* 0x0034 */
    public short m000034;
    /* 0x0036 */
    public short m000036;
    /* 0x0038 */
    public byte CraftsCount;
    /* 0xF000039 */
    public byte[] unk000039 = new byte[1];
    /* 0x003A */
    public short[] m00003A = new short[3];
    /* 0xF000040 */
    public byte[] unk000040 = new byte[6];

    public static S0x004D0FBC FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004D0FBC();
        obj.Name = array.ReadFixedLengthString(0x00, 50);
        obj.m000032 = BitConverter.ToInt16(array, 0x32);
        obj.m000034 = BitConverter.ToInt16(array, 0x34);
        obj.m000036 = BitConverter.ToInt16(array, 0x36);
        obj.CraftsCount = array[0x38];
        array.ReadUnknown(0x39, obj.unk000039);
        for (int i = 0; i < 3; i++)
        {
            obj.m00003A[i] = BitConverter.ToInt16(array, 0x3A + i * 0x02);
        }
        array.ReadUnknown(0x40, obj.unk000040);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x00, Name, 50);
        BitConverter.GetBytes(m000032).CopyTo(array, 0x32);
        BitConverter.GetBytes(m000034).CopyTo(array, 0x34);
        BitConverter.GetBytes(m000036).CopyTo(array, 0x36);
        array[0x38] = CraftsCount;
        array.WriteUnknown(0x39, unk000039);
        for (int i = 0; i < 3; i++)
        {
            BitConverter.GetBytes(m00003A[i]).CopyTo(array, 0x3A + i * 0x02);
        }
        array.WriteUnknown(0x40, unk000040);
        return array;
    }
}
