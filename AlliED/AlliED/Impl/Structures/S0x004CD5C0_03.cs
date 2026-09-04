using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004CD5C0_03
{
    public const int Size = 0x004E;

    /* 0x0000 */
    public TieFGGoalTypeEnum GoalType;
    /* 0x0001 */
    public TieConditionEnum Condition;
    /* 0x0002 */
    public TieAmountEnum Amount;
    /* 0x0003 */
    public byte Points;
    /* 0x0004 */
    public byte[] AppliesToTeams = new byte[10];
    /* 0x000E */
    public byte[] m00000E = new byte[64];

    public static S0x004CD5C0_03 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x004CD5C0_03();
        buffer.GoalType = (TieFGGoalTypeEnum)array[0x00];
        buffer.Condition = (TieConditionEnum)array[0x01];
        buffer.Amount = (TieAmountEnum)array[0x02];
        buffer.Points = array[0x03];
        buffer.AppliesToTeams = array.Subarray(0x04, 10);
        buffer.m00000E = array.Subarray(0x0E, 64);
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array[0x00] = (byte)GoalType;
        array[0x01] = (byte)Condition;
        array[0x02] = (byte)Amount;
        array[0x03] = Points;
        AppliesToTeams.CopyTo(array, 0x04);
        m00000E.CopyTo(array, 0x0E);
        return array;
    }
}
