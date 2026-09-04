namespace AlliED.Impl.Structures;

internal class AlliedMapBitmap
{
    public const int Size = 0x0008;

    /* 0x0000 */
    public TBitmap? pBitmap;
    /* 0x0004 */
    public byte IsLoaded;
    /* 0xF000005 */
    public byte[] unk000005 = new byte[3];
}
