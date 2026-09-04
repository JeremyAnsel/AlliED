namespace AlliED.Impl.Structures;

internal class TCommandObject
{
    public const int Size = 0x04A8;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public TCommand m000004 = new();
}
