using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0xTieFlightGroupOrder
{
    public const int Size = 0x0094;

    /* 0x0000 */
    public TieOrderIdEnum OrderId;
    /* 0x0001 */
    public byte Throttle;
    /* 0x0002 */
    public byte Var0;
    /* 0x0003 */
    public byte Var1;
    /* 0x0004 */
    public short Var2;
    /* 0x0006 */
    public S0xTieFlightGroupOrderSecondaryTarget SecondaryTarget = new();
    /* 0x000C */
    public S0xTieFlightGroupOrderPrimaryTarget PrimaryTarget = new();
    /* 0x0012 */
    public short SpeedMph;
    /* 0x0014 */
    public S0xTieFlightGroupWaypoint[] Waypoints = ArrayHelpers.CreateArray<S0xTieFlightGroupWaypoint>(8);
    /* 0x0054 */
    public byte[] m000054 = new byte[64]; // unknown type

    public static S0xTieFlightGroupOrder FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var order = new S0xTieFlightGroupOrder();
        order.OrderId = (TieOrderIdEnum)array[0x00];
        order.Throttle = array[0x01];
        order.Var0 = array[0x02];
        order.Var1 = array[0x03];
        order.Var2 = BitConverter.ToInt16(array, 0x04);
        order.SecondaryTarget = S0xTieFlightGroupOrderSecondaryTarget.FromByteArray(array.Subarray(0x06, 0x06));
        order.PrimaryTarget = S0xTieFlightGroupOrderPrimaryTarget.FromByteArray(array.Subarray(0x0C, 0x06));
        order.SpeedMph = BitConverter.ToInt16(array, 0x12);
        for (int i = 0; i < 8; i++)
        {
            order.Waypoints[i] = S0xTieFlightGroupWaypoint.FromByteArray(array.Subarray(0x14 + i * 0x08, 0x08));
        }
        array.ReadUnknown(0x54, order.m000054);
        return order;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array[0x00] = (byte)OrderId;
        array[0x01] = Throttle;
        array[0x02] = Var0;
        array[0x03] = Var1;
        BitConverter.GetBytes(Var2).CopyTo(array, 0x04);
        SecondaryTarget.ToByteArray().CopyTo(array, 0x06);
        PrimaryTarget.ToByteArray().CopyTo(array, 0x0C);
        BitConverter.GetBytes(SpeedMph).CopyTo(array, 0x12);
        for (int i = 0; i < 8; i++)
        {
            Waypoints[i].ToByteArray().CopyTo(array, 0x14 + i * 0x08);
        }
        array.WriteUnknown(0x54, m000054);
        return array;
    }
}
