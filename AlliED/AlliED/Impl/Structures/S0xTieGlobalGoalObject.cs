namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xTieGlobalGoalObject
{
    public const int Size = 0x0174;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xTieGlobalGoalGroup GlobalGoal = new();
}
