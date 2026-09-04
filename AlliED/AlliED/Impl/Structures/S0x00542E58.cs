namespace AlliED.Impl.Structures;

internal class S0x00542E58
{
    public const int Size = 0x0004;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[1];
    /* 0x0001 */
    public byte m000001;
    /* 0x0002 */
    public short m000002;

    public S0x00542E58 Clone()
    {
        return (S0x00542E58)MemberwiseClone();
    }
}
