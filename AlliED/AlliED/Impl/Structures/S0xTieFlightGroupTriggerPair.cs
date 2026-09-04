using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0xTieFlightGroupTriggerPair
{
    public const int Size = 0x0010;

    /* 0x0000 */
    public S0xTieTrigger[] Triggers = ArrayHelpers.CreateArray<S0xTieTrigger>(2);
    /* 0x000C */
    public byte[] TriggerUsed = new byte[2];
    /* 0x000E */
    public bool Operator;
    /* 0x000F */
    public byte m00000F;

    public static S0xTieFlightGroupTriggerPair FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var pair = new S0xTieFlightGroupTriggerPair();
        pair.Triggers[0] = S0xTieTrigger.FromByteArray(array.Subarray(0x00, 0x06));
        pair.Triggers[1] = S0xTieTrigger.FromByteArray(array.Subarray(0x06, 0x06));
        pair.TriggerUsed = array.Subarray(0x0C, 0x02);
        pair.Operator = array[0x0E] != 0;
        pair.m00000F = array[0x0F];
        return pair;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        Triggers[0].ToByteArray().CopyTo(array, 0x00);
        Triggers[1].ToByteArray().CopyTo(array, 0x06);
        TriggerUsed.CopyTo(array, 0x0C);
        array[0x0E] = Operator ? (byte)1 : (byte)0;
        array[0x0F] = m00000F;
        return array;
    }
}
