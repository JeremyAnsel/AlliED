using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x005341C0
{
    public const int Size = 0x0016;

    /* 0x0000 */
    public short FlightGroupsCount;
    /* 0x0002 */
    public short RadioMessagesCount;
    /* 0xF000004 */
    public byte[] unk000000 = new byte[18];

    public static S0x005341C0 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x005341C0();
        buffer.FlightGroupsCount = BitConverter.ToInt16(array, 0x00);
        buffer.RadioMessagesCount = BitConverter.ToInt16(array, 0x02);
        array.ReadUnknown(0x04, buffer.unk000000);
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(FlightGroupsCount).CopyTo(array, 0x00);
        BitConverter.GetBytes(RadioMessagesCount).CopyTo(array, 0x02);
        array.WriteUnknown(0x04, unk000000);
        return array;
    }
}
