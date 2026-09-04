using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004CE208_02_00
{
    public const int Size = 0x0028;

    /* 0x0000 */
    public uint m000000;
    /* 0x0004 */
    public uint m000004;
    /* 0x0008 */
    public byte m000008;
    /* 0x0009 */
    public byte m000009;
    /* 0x000A */
    public bool m00000A;
    /* 0x000B */
    public uint m00000B;
    /* 0x000F */
    public uint m00000F;
    /* 0x0013 */
    public byte m000013;
    /* 0x0014 */
    public byte m000014;
    /* 0x0015 */
    public bool m000015;
    /* 0xF000016 */
    public byte[] unk000016 = new byte[16];
    /* 0x0026 */
    public byte TimePassed;
    /* 0x0027 */
    public bool Operator;

    public static S0x004CE208_02_00 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004CE208_02_00();
        obj.m000000 = BitConverter.ToUInt32(array, 0x00);
        obj.m000004 = BitConverter.ToUInt32(array, 0x04);
        obj.m000008 = array[0x08];
        obj.m000009 = array[0x09];
        obj.m00000A = array[0x0A] != 0;
        obj.m00000B = BitConverter.ToUInt32(array, 0x0B);
        obj.m00000F = BitConverter.ToUInt32(array, 0x0F);
        obj.m000013 = array[0x13];
        obj.m000014 = array[0x14];
        obj.m000015 = array[0x15] != 0;
        array.ReadUnknown(0x16, obj.unk000016);
        obj.TimePassed = array[0x26];
        obj.Operator = array[0x27] != 0;
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        BitConverter.GetBytes(m000004).CopyTo(array, 0x04);
        array[0x08] = m000008;
        array[0x09] = m000009;
        array[0x0A] = m00000A ? (byte)1 : (byte)0;
        BitConverter.GetBytes(m00000B).CopyTo(array, 0x0B);
        BitConverter.GetBytes(m00000F).CopyTo(array, 0x0F);
        array[0x13] = m000013;
        array[0x14] = m000014;
        array[0x15] = m000015 ? (byte)1 : (byte)0;
        array.WriteUnknown(0x16, unk000016);
        array[0x26] = TimePassed;
        array[0x27] = Operator ? (byte)1 : (byte)0;
        return array;
    }
}
