namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xMissionObject
{
    public const int Size = 0x23F2;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xTieFileHeader TieFileHeader = new();
}
