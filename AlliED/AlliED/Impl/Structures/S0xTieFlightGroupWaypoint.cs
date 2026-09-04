namespace AlliED.Impl.Structures;

internal class S0xTieFlightGroupWaypoint
{
    public const int Size = 0x0008;

    /* 0x0000 */
    public short[] Position = new short[3];
    /* 0x0006 */
    public short IsUsed;

    public static S0xTieFlightGroupWaypoint FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var waypoint = new S0xTieFlightGroupWaypoint();
        waypoint.Position[0] = BitConverter.ToInt16(array, 0x00);
        waypoint.Position[1] = BitConverter.ToInt16(array, 0x02);
        waypoint.Position[2] = BitConverter.ToInt16(array, 0x04);
        waypoint.IsUsed = BitConverter.ToInt16(array, 0x06);
        return waypoint;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(Position[0]).CopyTo(array, 0x00);
        BitConverter.GetBytes(Position[1]).CopyTo(array, 0x02);
        BitConverter.GetBytes(Position[2]).CopyTo(array, 0x04);
        BitConverter.GetBytes(IsUsed).CopyTo(array, 0x06);
        return array;
    }
}
