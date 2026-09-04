using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xXvTTieBriefingCode
{
    public const int Size = 0x0334;

    /* 0x0000 */
    public short Length;
    /* 0x0002 */
    public short Time;
    /* 0x0004 */
    public short Index;
    /* 0x0006 */
    public short CodeSize;
    /* 0x0008 */
    public short Title;
    /* 0x000A */
    public short[] m00000A = new short[400];
    /* 0x032A */
    public byte[] ForTeam = new byte[10];

    public static S0xXvTTieBriefingCode FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var code = new S0xXvTTieBriefingCode();
        code.Length = BitConverter.ToInt16(array, 0x00);
        code.Time = BitConverter.ToInt16(array, 0x002);
        code.Index = BitConverter.ToInt16(array, 0x004);
        code.CodeSize = BitConverter.ToInt16(array, 0x006);
        code.Title = BitConverter.ToInt16(array, 0x008);
        for (int i = 0; i < 400; i++)
        {
            code.m00000A[i] = BitConverter.ToInt16(array, 0x00A + i * 0x02);
        }
        code.ForTeam = array.Subarray(0x32A, 10);
        return code;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(Length).CopyTo(array, 0x000);
        BitConverter.GetBytes(Time).CopyTo(array, 0x002);
        BitConverter.GetBytes(Index).CopyTo(array, 0x004);
        BitConverter.GetBytes(CodeSize).CopyTo(array, 0x006);
        BitConverter.GetBytes(Title).CopyTo(array, 0x008);
        for (int i = 0; i < 400; i++)
        {
            BitConverter.GetBytes(m00000A[i]).CopyTo(array, 0x00A + i * 0x02);
        }
        ForTeam.CopyTo(array, 0x32A);
        return array;
    }
}
