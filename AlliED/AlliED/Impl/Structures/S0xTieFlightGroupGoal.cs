using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieFlightGroupGoal
{
    public const int Size = 0x0050;

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
    public byte Time;
    /* 0x000F */
    public byte SequenceNumber;
    /* 0x0010 */
    public byte[] m000010 = new byte[64];

    public static S0xTieFlightGroupGoal FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var goal = new S0xTieFlightGroupGoal();
        goal.GoalType = (TieFGGoalTypeEnum)array[0x00];
        goal.Condition = (TieConditionEnum)array[0x01];
        goal.Amount = (TieAmountEnum)array[0x02];
        goal.Points = array[0x03];
        goal.AppliesToTeams = array.Subarray(0x04, 10);
        goal.Time = array[0x0E];
        goal.SequenceNumber = array[0x0F];
        array.ReadUnknown(0x10, goal.m000010);
        return goal;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array[0x00] = (byte)GoalType;
        array[0x01] = (byte)Condition;
        array[0x02] = (byte)Amount;
        array[0x03] = Points;
        AppliesToTeams.CopyTo(array, 0x04);
        array[0x0E] = Time;
        array[0x0F] = SequenceNumber;
        array.WriteUnknown(0x10, m000010);
        return array;
    }
}
