using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieBriefingData
{
    public const int Size = 0x5DDC;

    /* 0x0000 */
    public S0xTieBriefingCode BriefingCode = new();
    /* 0x4414 */
    public byte[] m004414 = new byte[6600];

    public static S0xTieBriefingData FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var data = new S0xTieBriefingData();
        data.BriefingCode = S0xTieBriefingCode.FromByteArray(array.Subarray(0x0000, 0x4414));
        data.m004414 = array.Subarray(0x4414, 6600);
        return data;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BriefingCode.ToByteArray().CopyTo(array, 0x0000);
        m004414.CopyTo(array, 0x4414);
        return array;
    }
}
