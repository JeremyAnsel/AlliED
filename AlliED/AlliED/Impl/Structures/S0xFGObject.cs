using AlliED.Helpers;

namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xFGObject
{
    public const int Size = 0x1508;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xTieFlightGroup FlightGroupStruct = new();
    /* 0x0E42 */
    public S0xFGObject_000E42 m000E42 = new();
    /* 0x1442 */
    public bool AutoLink;
    /* 0x1443 */
    public bool m001443;
    /* 0x1444 */
    public byte[] m001444 = new byte[4];
    /* 0x1448 */
    public double m001448;
    /* 0x1450 */
    public double m001450;
    /* 0x1458 */
    public double m001458;
    /* 0x1460 */
    public double m001460;
    /* 0x1468 */
    public double m001468;
    /* 0x1470 */
    public double m001470;
    /* 0x1478 */
    public byte m001478;
    /* 0x1479 */
    public byte m001479;
    /* 0x147A */
    public byte m00147A;
    /* 0xF00147B */
    public byte[] unk00147B = new byte[1];
    /* 0x147C */
    public S0xFGObject_00147C[] m00147C = ArrayHelpers.CreateArray<S0xFGObject_00147C>(4);
    /* 0x14DC */
    public short[] IsWPEnabled = new short[22];

    public S0xFGObject Clone()
    {
        var obj = new S0xFGObject();
        obj.unk000000 = (byte[])unk000000.Clone();
        obj.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(FlightGroupStruct.ToByteArray());
        obj.m000E42 = S0xFGObject_000E42.FromByteArray(m000E42.ToByteArray());
        obj.AutoLink = AutoLink;
        obj.m001443 = m001443;
        obj.m001444 = (byte[])m001444.Clone();
        obj.m001448 = m001448;
        obj.m001450 = m001450;
        obj.m001458 = m001458;
        obj.m001460 = m001460;
        obj.m001468 = m001468;
        obj.m001470 = m001470;
        obj.m001478 = m001478;
        obj.m001479 = m001479;
        obj.m00147A = m00147A;
        obj.unk00147B = (byte[])unk00147B.Clone();
        for (int i = 0; i < 4; i++)
        {
            obj.m00147C[i] = S0xFGObject_00147C.FromByteArray(m00147C[i].ToByteArray());
        }
        obj.IsWPEnabled = (short[])IsWPEnabled.Clone();
        return obj;
    }
}
