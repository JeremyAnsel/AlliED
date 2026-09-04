using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0xTieTriggers
{
    public const int Size = 0x0020;

    /* 0x0000 */
    public S0xTieTrigger[] Trigger_0 = ArrayHelpers.CreateArray<S0xTieTrigger>(2);
    /* 0x000C */
    public byte[] TriggerUsed_0 = new byte[2];
    /* 0x000E */
    public bool Operator_0;
    /* 0x000F */
    public byte Unused0F_0;
    /* 0x0010 */
    public S0xTieTrigger[] Trigger_1 = ArrayHelpers.CreateArray<S0xTieTrigger>(2);
    /* 0x001C */
    public byte[] TriggerUsed_1 = new byte[2];
    /* 0x001E */
    public bool Operator_1;
    /* 0x001F */
    public byte Unused0F_1;

    public static S0xTieTriggers FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var triggers = new S0xTieTriggers();
        triggers.Trigger_0[0] = S0xTieTrigger.FromByteArray(array.Subarray(0x00, 0x06));
        triggers.Trigger_0[1] = S0xTieTrigger.FromByteArray(array.Subarray(0x06, 0x06));
        triggers.TriggerUsed_0 = array.Subarray(0x0C, 0x02);
        triggers.Operator_0 = array[0x0E] != 0;
        triggers.Unused0F_0 = array[0x0F];
        triggers.Trigger_1[0] = S0xTieTrigger.FromByteArray(array.Subarray(0x10, 0x06));
        triggers.Trigger_1[1] = S0xTieTrigger.FromByteArray(array.Subarray(0x16, 0x06));
        triggers.TriggerUsed_1 = array.Subarray(0x1C, 0x02);
        triggers.Operator_1 = array[0x1E] != 0;
        triggers.Unused0F_1 = array[0x1F];
        return triggers;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        Trigger_0[0].ToByteArray().CopyTo(array, 0x00);
        Trigger_0[1].ToByteArray().CopyTo(array, 0x06);
        TriggerUsed_0.CopyTo(array, 0x0C);
        array[0x0E] = Operator_0 ? (byte)1 : (byte)0;
        array[0x0F] = Unused0F_0;
        Trigger_1[0].ToByteArray().CopyTo(array, 0x10);
        Trigger_1[1].ToByteArray().CopyTo(array, 0x16);
        TriggerUsed_1.CopyTo(array, 0x1C);
        array[0x1E] = Operator_1 ? (byte)1 : (byte)0;
        array[0x1F] = Unused0F_1;
        return array;
    }
}
