namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xCondObjectStruct
{
    public const int Size = 0x000A;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xTieTrigger m000004 = new();
}
