using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieBriefingCode
{
    public const int Size = 0x4414;

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
    public short[] m00000A = new short[7000];
    /* 0x36BA */
    public byte[] m0036BA = new byte[3408];
    /* 0x440A */
    public byte[] ForTeam = new byte[10];

    public static S0xTieBriefingCode FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var code = new S0xTieBriefingCode();
        code.Length = BitConverter.ToInt16(array, 0x0000);
        code.Time = BitConverter.ToInt16(array, 0x0002);
        code.Index = BitConverter.ToInt16(array, 0x0004);
        code.CodeSize = BitConverter.ToInt16(array, 0x0006);
        code.Title = BitConverter.ToInt16(array, 0x0008);
        for (int i = 0; i < 7000; i++)
        {
            code.m00000A[i] = BitConverter.ToInt16(array, 0x000A + i * 0x02);
        }
        code.m0036BA = array.Subarray(0x36BA, 3408);
        code.ForTeam = array.Subarray(0x440A, 10);
        return code;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(Length).CopyTo(array, 0x0000);
        BitConverter.GetBytes(Time).CopyTo(array, 0x0002);
        BitConverter.GetBytes(Index).CopyTo(array, 0x0004);
        BitConverter.GetBytes(CodeSize).CopyTo(array, 0x0006);
        BitConverter.GetBytes(Title).CopyTo(array, 0x0008);
        for (int i = 0; i < 7000; i++)
        {
            BitConverter.GetBytes(m00000A[i]).CopyTo(array, 0x000A + i * 0x02);
        }
        m0036BA.CopyTo(array, 0x36BA);
        ForTeam.CopyTo(array, 0x440A);
        return array;
    }
}
