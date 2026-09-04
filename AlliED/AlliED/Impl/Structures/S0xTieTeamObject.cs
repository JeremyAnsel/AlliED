namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xTieTeamObject
{
    public const int Size = 0x01EC;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xTieTeam Team = new();
}
