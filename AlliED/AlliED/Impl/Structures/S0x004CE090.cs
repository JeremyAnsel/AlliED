using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004CE090
{
    public const int Size = 0x0074;

    /* 0x0000 */
    public short m000000;
    /* 0x0002 */
    public byte m000002;
    /* 0x0003 */
    public S0x00533EA8 m000003 = new();
    /* 0x0042 */
    public byte[] ForTeam = new byte[10];
    /* 0x004C */
    public S0x004CE208_02_00 m00004C = new();

    public static S0x004CE090 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004CE090();
        obj.m000000 = BitConverter.ToInt16(array, 0x00);
        obj.m000002 = array[0x02];
        obj.m000003 = S0x00533EA8.FromByteArray(array.Subarray(0x03, 0x3F));
        obj.ForTeam = array.Subarray(0x42, 10);
        obj.m00004C = S0x004CE208_02_00.FromByteArray(array.Subarray(0x4C, 0x28));
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        array[0x02] = m000002;
        m000003.ToByteArray().CopyTo(array, 0x03);
        ForTeam.CopyTo(array, 0x42);
        m00004C.ToByteArray().CopyTo(array, 0x4C);
        return array;
    }
}
