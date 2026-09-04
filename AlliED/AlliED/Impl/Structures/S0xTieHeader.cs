using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0xTieHeader
{
    public const int Size = 0x23EA;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[2];
    /* 0x0002 */
    public byte WinType;
    /* 0xF000003 */
    public byte[] unk000003 = new byte[2];
    /* 0x0005 */
    public bool AllWayShown;
    /* 0xF000006 */
    public byte[] unk000006 = new byte[8];
    /* 0x000E */
    public TFixedString[] IffNames = ArrayHelpers.CreateArray(4, () => new TFixedString(20));
    /* 0x005E */
    public S0xTieRegion[] Regions = ArrayHelpers.CreateArray<S0xTieRegion>(4);
    /* 0x026E */
    public S0xTieHeaderGlobalCargo[] GlobalCargos = ArrayHelpers.CreateArray<S0xTieHeaderGlobalCargo>(16);
    /* 0x0B2E */
    public S0xTieHeaderSet[] Sets = ArrayHelpers.CreateArray<S0xTieHeaderSet>(72);
    /* 0x23A6 */
    public byte MissionType;
    /* 0x23A7 */
    public byte IsGoalMelee;
    /* 0x23A8 */
    public byte TimeLimit;
    /* 0x23A9 */
    public bool EndImmediately;
    /* 0x23AA */
    public byte TacticalOfficer;
    /* 0x23AB */
    public byte BriefingLogo;
    /* 0x23AC */
    public byte m0023AC;
    /* 0x23AD */
    public byte BriefingCodeSizeType;
    /* 0x23AE */
    public byte m0023AE;
    /* 0x23AF */
    public byte m0023AF;
    /* 0xF0023B0 */
    public byte[] unk0023B0 = new byte[58];

    public static S0xTieHeader FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var header = new S0xTieHeader();
        array.ReadUnknown(0x0000, header.unk000000);
        header.WinType = array[0x0002];
        array.ReadUnknown(0x0003, header.unk000003);
        header.AllWayShown = array[0x0005] != 0;
        array.ReadUnknown(0x0006, header.unk000006);
        for (int i = 0; i < 4; i++)
        {
            header.IffNames[i].Text = array.ReadFixedLengthString(0x000E + i * 0x14, 20);
        }
        for (int i = 0; i < 4; i++)
        {
            header.Regions[i] = S0xTieRegion.FromByteArray(array.Subarray(0x005E + i * 0x84, 0x84));
        }
        for (int i = 0; i < 16; i++)
        {
            header.GlobalCargos[i] = S0xTieHeaderGlobalCargo.FromByteArray(array.Subarray(0x026E + i * 0x8C, 0x8C));
        }
        for (int i = 0; i < 72; i++)
        {
            header.Sets[i] = S0xTieHeaderSet.FromByteArray(array.Subarray(0x0B2E + i * 0x57, 0x57));
        }
        header.MissionType = array[0x23A6];
        header.IsGoalMelee = array[0x23A7];
        header.TimeLimit = array[0x23A8];
        header.EndImmediately = array[0x23A9] != 0;
        header.TacticalOfficer = array[0x23AA];
        header.BriefingLogo = array[0x23AB];
        header.m0023AC = array[0x23AC];
        header.BriefingCodeSizeType = array[0x23AD];
        header.m0023AE = array[0x23AE];
        header.m0023AF = array[0x23AF];
        array.ReadUnknown(0x23B0, header.unk0023B0);
        return header;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteUnknown(0x0000, unk000000);
        array[0x0002] = WinType;
        array.WriteUnknown(0x0003, unk000003);
        array[0x0005] = AllWayShown ? (byte)1 : (byte)0;
        array.WriteUnknown(0x0006, unk000006);

        for (int i = 0; i < 4; i++)
        {
            array.WriteFixedLengthString(0x000E + i * 0x14, IffNames[i].Text, 20);
        }
        for (int i = 0; i < 4; i++)
        {
            Regions[i].ToByteArray().CopyTo(array, 0x005E + i * 0x84);
        }
        for (int i = 0; i < 16; i++)
        {
            GlobalCargos[i].ToByteArray().CopyTo(array, 0x026E + i * 0x8C);
        }
        for (int i = 0; i < 72; i++)
        {
            Sets[i].ToByteArray().CopyTo(array, 0x0B2E + i * 0x57);
        }
        array[0x23A6] = MissionType;
        array[0x23A7] = IsGoalMelee;
        array[0x23A8] = TimeLimit;
        array[0x23A9] = EndImmediately ? (byte)1 : (byte)0;
        array[0x23AA] = TacticalOfficer;
        array[0x23AB] = BriefingLogo;
        array[0x23AC] = m0023AC;
        array[0x23AD] = BriefingCodeSizeType;
        array[0x23AE] = m0023AE;
        array[0x23AF] = m0023AF;
        array.WriteUnknown(0x23B0, unk0023B0);
        return array;
    }
}
