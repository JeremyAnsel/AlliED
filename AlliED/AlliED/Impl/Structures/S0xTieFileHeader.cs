using AlliED.Extensions;

namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xTieFileHeader
{
    public const int Size = 0x23EE;

    /* 0x0000 */
    public short FlightGroupsCount;
    /* 0x0002 */
    public short RadioMessagesCount;
    /* 0x0004 */
    public S0xTieHeader Header = new();

    public static S0xTieFileHeader FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var header = new S0xTieFileHeader();
        header.FlightGroupsCount = BitConverter.ToInt16(array, 0x00);
        header.RadioMessagesCount = BitConverter.ToInt16(array, 0x02);
        header.Header = S0xTieHeader.FromByteArray(array.Subarray(0x04, 0x23EA));
        return header;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(FlightGroupsCount).CopyTo(array, 0x00);
        BitConverter.GetBytes(RadioMessagesCount).CopyTo(array, 0x02);
        Header.ToByteArray().CopyTo(array, 0x04);
        return array;
    }
}
