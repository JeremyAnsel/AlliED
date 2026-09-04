namespace AlliED.Impl.Structures;

internal class S0x00542EDC
{
    public const int Size = 0x0014;

    /* 0x0000 */
    public CraftIdEnum CraftId;
    /* 0x0001 */
    public byte Iff;
    /* 0x0002 */
    public short X;
    /* 0x0004 */
    public short Y;
    /* 0x0006 */
    public byte m000006;
    /* 0x0007 */
    public IconRotationEnum Rotation;
    /* 0x0008 */
    public int m000008;
    /* 0x000C */
    public int m00000C;
    /* 0xF000010 */
    public byte[] unk000010 = new byte[4];

    public S0x00542EDC Clone()
    {
        return (S0x00542EDC)MemberwiseClone();
    }
}
