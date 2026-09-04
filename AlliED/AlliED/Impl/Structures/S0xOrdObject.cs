namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xOrdObject
{
    public const int Size = 0x0098;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xTieFlightGroupOrder m000004 = new();
}
