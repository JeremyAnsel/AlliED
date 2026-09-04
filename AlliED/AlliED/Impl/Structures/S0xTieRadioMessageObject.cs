namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xTieRadioMessageObject
{
    public const int Size = 0x00A7;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xTieRadioMessage RadioMessage = new();
    /* 0x00A6 */
    public byte M0000A6;
}
