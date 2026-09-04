using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class TCommand
{
    public const int Size = 0x04A4;

    /* 0x0000 */
    public short Time;
    /* 0x0002 */
    public BriefingCommandEnum BriefingCommand;
    /* 0x0004 */
    public CraftIdEnum CraftId;
    /* 0x0005 */
    public byte padding000005;
    /* 0x0006 */
    public IconRotationEnum Rotation;
    /* 0x0007 */
    public byte padding000007;
    /* 0x0008 */
    public short IconIndex;
    /* 0x000A */
    public short Parameter;
    /* 0x000C */
    public short ColorIndex;
    /* 0x000E */
    public short X;
    /* 0x0010 */
    public short Y;
    /* 0xF000012 */
    public byte[] unk000012 = new byte[2];
    /* 0x0014 */
    public int m000014;
    /* 0x0018 */
    public int m000018;
    /* 0x001C */
    public int m00001C;
    /* 0x0020 */
    public int m000020;
    /* 0x0024 */
    public int m000024;
    /* 0x0028 */
    public int m000028;
    /* 0x002C */
    public int m00002C;
    /* 0x0030 */
    public int m000030;
    /* 0x0034 */
    public byte m000034;
    /* 0x0035 */
    public byte m000035;
    /* 0x0036 */
    public byte m000036;
    /* 0x0037 */
    public byte m000037;
    /* 0x0038 */
    public byte m000038;
    /* 0x0039 */
    public byte m000039;
    /* 0xF00003A */
    public byte[] unk00003A = new byte[2];
    /* 0x003C */
    public S0x00542EDC[] m00003C = ArrayHelpers.CreateArray<S0x00542EDC>(51);
    /* 0x0438 */
    public S0x00542E58[] m000438 = ArrayHelpers.CreateArray<S0x00542E58>(8);
    /* 0x0458 */
    public int[] m000458 = new int[8];
    /* 0x0478 */
    public int[] m000478 = new int[8];
    /* 0x0498 */
    public int m000498;
    /* 0x049C */
    public byte m00049C;
    /* 0xF00049D */
    public byte[] unk00049D = new byte[3];
    /* 0x04A0 */
    public CraftIdEnum m0004A0;
    /* 0x04A1 */
    public byte[] padding0004A0 = new byte[3];

    public TCommand Clone()
    {
        var obj = (TCommand)this.MemberwiseClone();

        obj.m00003C = new S0x00542EDC[51];
        for (int i = 0; i < m00003C.Length; i++)
        {
            obj.m00003C[i] = m00003C[i].Clone();
        }

        obj.m000438 = new S0x00542E58[8];
        for (int i = 0; i < m000438.Length; i++)
        {
            obj.m000438[i] = m000438[i].Clone();
        }

        return obj;
    }
}
