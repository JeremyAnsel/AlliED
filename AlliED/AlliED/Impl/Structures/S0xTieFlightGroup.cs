using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0xTieFlightGroup
{
    public const int Size = 0x0E3E;

    /* 0x0000 */
    private string _name = string.Empty;
    public string Name { get => _name; set => _name = value.WithMaxLength(20); }
    /* 0x0014 */
    public TacticalRoleUsedEnum TacticalRoleUsed0;
    /* 0x0015 */
    public TacticalRoleUsedEnum TacticalRoleUsed1;
    /* 0x0016 */
    public TacticalRoleEnum TacticalRole0;
    /* 0x0017 */
    public TacticalRoleEnum TacticalRole1;
    /* 0x0018 */
    public byte Comm;
    /* 0x0019 */
    public byte GlobalCargoIndex;
    /* 0x001A */
    public byte SpecialCargoIndex;
    /* 0xF00001B */
    public byte[] unk00001B = new byte[13];
    /* 0x0028 */
    private string _cargo = string.Empty;
    public string Cargo { get => _cargo; set => _cargo = value.WithMaxLength(20); }
    /* 0x003C */
    private string _specialCargo = string.Empty;
    public string SpecialCargo { get => _specialCargo; set => _specialCargo = value.WithMaxLength(20); }
    /* 0x0050 */
    private string _role = string.Empty;
    public string Role { get => _role; set => _role = value.WithMaxLength(25); }
    /* 0x0069 */
    public byte SpecialCraft;
    /* 0x006A */
    public byte RandomSpecialCraft;
    /* 0x006B */
    public CraftIdEnum CraftId;
    /* 0x006C */
    public byte CraftsCount;
    /* 0x006D */
    public FlightGroupStatusEnum Status1;
    /* 0x006E */
    public byte WarheadType;
    /* 0x006F */
    public byte BeamType;
    /* 0x0070 */
    public byte Iff;
    /* 0x0071 */
    public byte Team;
    /* 0x0072 */
    public byte AIRank;
    /* 0x0073 */
    public byte Markings;
    /* 0x0074 */
    public byte Radio;
    /* 0xF000075 */
    public byte[] unk000075 = new byte[1];
    /* 0x0076 */
    public byte FormationType;
    /* 0x0077 */
    public byte FormationSpacing;
    /* 0x0078 */
    public byte GlobalGroupId;
    /* 0xF000079 */
    public byte[] unk000079 = new byte[1];
    /* 0x007A */
    public byte WavesCount;
    /* 0x007B */
    public byte WaveDelay;
    /* 0x007C */
    public byte WaveContinuous;
    /* 0x007D */
    public byte PlayerNumber;
    /* 0x007E */
    public bool ArriveOnlyIfPlayer;
    /* 0x007F */
    public byte PlayerCraft;
    /* 0x0080 */
    public byte Yaw;
    /* 0x0081 */
    public byte Pitch;
    /* 0x0082 */
    public byte Roll;
    /* 0xF000083 */
    public byte[] unk000083 = new byte[1];
    /* 0x0084 */
    public byte LinkId;
    /* 0xF000085 */
    public byte[] unk000085 = new byte[1];
    /* 0x0086 */
    public byte LinkUnused;
    /* 0x0087 */
    public ArrivalDifficultyEnum ArrivalDifficulty;
    /* 0x0088 */
    public S0xTieFlightGroupTriggerPair ArrivalTrigger1 = new();
    /* 0x0098 */
    public S0xTieFlightGroupTriggerPair ArrivalTrigger2 = new();
    /* 0x00A8 */
    public bool ArrivalTriggersOperator;
    /* 0xF0000A9 */
    public byte[] unk0000A9 = new byte[1];
    /* 0x00AA */
    public byte ArrivalDelayMinutes;
    /* 0x00AB */
    public byte ArrivalDelaySeconds;
    /* 0x00AC */
    public S0xTieFlightGroupTriggerPair DepartureTrigger = new();
    /* 0x00BC */
    public byte DepartureDelayMinutes;
    /* 0x00BD */
    public byte DepartureDelaySeconds;
    /* 0x00BE */
    public byte AbortCondition;
    /* 0x00BF */
    public byte ArrivalRandomDelaySeconds;
    /* 0x00C0 */
    public short CurStartFg;
    /* 0x00C2 */
    public byte StartFg;
    /* 0x00C3 */
    public byte StartFgUsed;
    /* 0x00C4 */
    public byte PrimaryStopFg;
    /* 0x00C5 */
    public byte PrimaryStopFgUsed;
    /* 0x00C6 */
    public byte SecondaryStopFg;
    /* 0x00C7 */
    public byte SecondaryStopFgUsed;
    /* 0x00C8 */
    public byte CaptureFg;
    /* 0x00C9 */
    public byte CaptureFgUsed;
    /* 0x00CA */
    public S0xTieFlightGroupOrder[] Orders = ArrayHelpers.CreateArray<S0xTieFlightGroupOrder>(16);
    /* 0x0A0A */
    public S0xTieFlightGroupTriggerPair[] JumpTriggers = ArrayHelpers.CreateArray<S0xTieFlightGroupTriggerPair>(16);
    /* 0x0B0A */
    public S0xTieFlightGroupGoal[] Goals = ArrayHelpers.CreateArray<S0xTieFlightGroupGoal>(8);
    /* 0x0D8A */
    public S0xTieFlightGroupWaypoint[] StartPoints = ArrayHelpers.CreateArray<S0xTieFlightGroupWaypoint>(4);
    /* 0x0DAA */
    public byte[] StartPointRegions = new byte[4];
    /* 0x0DAE */
    public S0xTieFlightGroup_000DAE[] m000DAE = ArrayHelpers.CreateArray<S0xTieFlightGroup_000DAE>(2);
    /* 0x0DBE */
    public byte m000DBE; // unknown type
    /* 0xF000DBF */
    public byte[] unk000DBF = new byte[1];
    /* 0x0DC0 */
    public bool m000DC0;
    /* 0x0DC1 */
    public bool m000DC1;
    /* 0xF000DC2 */
    public byte[] unk000DC2 = new byte[2];
    /* 0x0DC4 */
    public byte WaveNumberingOff;
    /* 0xF000DC5 */
    public byte[] unk000DC5 = new byte[2];
    /* 0x0DC7 */
    public byte CounterMeasuresType;
    /* 0x0DC8 */
    public byte CraftExplosionTime;
    /* 0x0DC9 */
    public FlightGroupStatusEnum Status2;
    /* 0x0DCA */
    public byte GlobalUnitId;
    /* 0xF000DCB */
    public byte[] unk000DCB = new byte[1];
    /* 0x0DCC */
    public byte[] OptionalWarheads = new byte[8];
    /* 0x0DD4 */
    public byte[] OptionalBeams = new byte[6];
    /* 0x0DDA */
    public byte[] OptionalCounterMeasures = new byte[4];
    /* 0x0DDE */
    public byte OptionalCraftCategory;
    /* 0x0DDF */
    public CraftIdEnum[] OptionalCraftsId = new CraftIdEnum[10];
    /* 0x0DE9 */
    public byte[] OptionalCraftsCount = new byte[10];
    /* 0x0DF3 */
    public byte[] OptionalCraftsWaves = new byte[10];
    /* 0x0DFD */
    private string _pilotVoice = string.Empty;
    public string PilotVoice { get => _pilotVoice; set => _pilotVoice = value.WithMaxLength(20); }
    /* 0xF000E11 */
    public byte[] unk000E11 = new byte[1];
    /* 0x0E12 */
    public byte PlanetId;
    /* 0xF000E13 */
    public byte[] unk000E13 = new byte[22];
    /* 0x0E29 */
    public bool m000E29;
    /* 0xF000E2A */
    public byte[] unk000E2A = new byte[1];
    /* 0x0E2B */
    public bool m000E2B;
    /* 0xF000E2C */
    public byte[] unk000E2C = new byte[1];
    /* 0x0E2D */
    public bool m000E2D;
    /* 0xF000E2E */
    public byte[] unk000E2E = new byte[1];
    /* 0x0E2F */
    public bool m000E2F;
    /* 0xF000E30 */
    public byte[] unk000E30 = new byte[1];
    /* 0x0E31 */
    public bool m000E31;
    /* 0xF000E32 */
    public byte[] unk000E32 = new byte[1];
    /* 0x0E33 */
    public bool m000E33;
    /* 0xF000E34 */
    public byte[] unk000E34 = new byte[1];
    /* 0x0E35 */
    public bool m000E35;
    /* 0xF000E36 */
    public byte[] unk000E36 = new byte[1];
    /* 0x0E37 */
    public bool m000E37;
    /* 0xF000E38 */
    public byte[] unk000E38 = new byte[6];

    public static S0xTieFlightGroup FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var fg = new S0xTieFlightGroup();
        fg.Name = array.ReadFixedLengthString(0x000, 20);
        fg.TacticalRoleUsed0 = (TacticalRoleUsedEnum)array[0x014];
        fg.TacticalRoleUsed1 = (TacticalRoleUsedEnum)array[0x015];
        fg.TacticalRole0 = (TacticalRoleEnum)array[0x016];
        fg.TacticalRole1 = (TacticalRoleEnum)array[0x017];
        fg.Comm = array[0x018];
        fg.GlobalCargoIndex = array[0x019];
        fg.SpecialCargoIndex = array[0x01A];
        array.ReadUnknown(0x01B, fg.unk00001B);
        fg.Cargo = array.ReadFixedLengthString(0x028, 20);
        fg.SpecialCargo = array.ReadFixedLengthString(0x03C, 20);
        fg.Role = array.ReadFixedLengthString(0x050, 25);
        fg.SpecialCraft = array[0x069];
        fg.RandomSpecialCraft = array[0x06A];
        fg.CraftId = (CraftIdEnum)array[0x06B];
        fg.CraftsCount = array[0x06C];
        fg.Status1 = (FlightGroupStatusEnum)array[0x06D];
        fg.WarheadType = array[0x06E];
        fg.BeamType = array[0x06F];
        fg.Iff = array[0x070];
        fg.Team = array[0x071];
        fg.AIRank = array[0x072];
        fg.Markings = array[0x073];
        fg.Radio = array[0x074];
        array.ReadUnknown(0x075, fg.unk000075);
        fg.FormationType = array[0x076];
        fg.FormationSpacing = array[0x077];
        fg.GlobalGroupId = array[0x078];
        array.ReadUnknown(0x079, fg.unk000079);
        fg.WavesCount = array[0x07A];
        fg.WaveDelay = array[0x07B];
        fg.WaveContinuous = array[0x07C];
        fg.PlayerNumber = array[0x07D];
        fg.ArriveOnlyIfPlayer = array[0x07E] != 0;
        fg.PlayerCraft = array[0x07F];
        fg.Yaw = array[0x080];
        fg.Pitch = array[0x081];
        fg.Roll = array[0x082];
        array.ReadUnknown(0x083, fg.unk000083);
        fg.LinkId = array[0x084];
        array.ReadUnknown(0x085, fg.unk000085);
        fg.LinkUnused = array[0x086];
        fg.ArrivalDifficulty = (ArrivalDifficultyEnum)array[0x087];
        fg.ArrivalTrigger1 = S0xTieFlightGroupTriggerPair.FromByteArray(array.Subarray(0x088, 0x10));
        fg.ArrivalTrigger2 = S0xTieFlightGroupTriggerPair.FromByteArray(array.Subarray(0x098, 0x10));
        fg.ArrivalTriggersOperator = array[0x0A8] != 0;
        array.ReadUnknown(0x0A9, fg.unk0000A9);
        fg.ArrivalDelayMinutes = array[0x0AA];
        fg.ArrivalDelaySeconds = array[0x0AB];
        fg.DepartureTrigger = S0xTieFlightGroupTriggerPair.FromByteArray(array.Subarray(0x0AC, 0x10));
        fg.DepartureDelayMinutes = array[0x0BC];
        fg.DepartureDelaySeconds = array[0x0BD];
        fg.AbortCondition = array[0x0BE];
        fg.ArrivalRandomDelaySeconds = array[0x0BF];
        fg.CurStartFg = BitConverter.ToInt16(array, 0x0C0);
        fg.StartFg = array[0x0C2];
        fg.StartFgUsed = array[0x0C3];
        fg.PrimaryStopFg = array[0x0C4];
        fg.PrimaryStopFgUsed = array[0x0C5];
        fg.SecondaryStopFg = array[0x0C6];
        fg.SecondaryStopFgUsed = array[0x0C7];
        fg.CaptureFg = array[0x0C8];
        fg.CaptureFgUsed = array[0x0C9];
        for (int i = 0; i < 16; i++)
        {
            fg.Orders[i] = S0xTieFlightGroupOrder.FromByteArray(array.Subarray(0x0CA + i * 0x94, 0x94));
        }
        for (int i = 0; i < 16; i++)
        {
            fg.JumpTriggers[i] = S0xTieFlightGroupTriggerPair.FromByteArray(array.Subarray(0xA0A + i * 0x10, 0x10));
        }
        for (int i = 0; i < 8; i++)
        {
            fg.Goals[i] = S0xTieFlightGroupGoal.FromByteArray(array.Subarray(0xB0A + i * 0x50, 0x50));
        }
        for (int i = 0; i < 4; i++)
        {
            fg.StartPoints[i] = S0xTieFlightGroupWaypoint.FromByteArray(array.Subarray(0xD8A + i * 0x08, 0x08));
        }
        for (int i = 0; i < 4; i++)
        {
            fg.StartPointRegions[i] = array[0xDAA + i];
        }
        for (int i = 0; i < 2; i++)
        {
            fg.m000DAE[i] = S0xTieFlightGroup_000DAE.FromByteArray(array.Subarray(0xDAE + i * 0x08, 0x08));
        }
        fg.m000DBE = array[0xDBE];
        array.ReadUnknown(0xDBF, fg.unk000DBF);
        fg.m000DC0 = array[0xDC0] != 0;
        fg.m000DC1 = array[0xDC1] != 0;
        array.ReadUnknown(0xDC2, fg.unk000DC2);
        fg.WaveNumberingOff = array[0xDC4];
        array.ReadUnknown(0xDC5, fg.unk000DC5);
        fg.CounterMeasuresType = array[0xDC7];
        fg.CraftExplosionTime = array[0xDC8];
        fg.Status2 = (FlightGroupStatusEnum)array[0xDC9];
        fg.GlobalUnitId = array[0xDCA];
        array.ReadUnknown(0xDCB, fg.unk000DCB);
        fg.OptionalWarheads = array.Subarray(0xDCC, 8);
        fg.OptionalBeams = array.Subarray(0xDD4, 6);
        fg.OptionalCounterMeasures = array.Subarray(0xDDA, 4);
        fg.OptionalCraftCategory = array[0xDDE];
        for (int i = 0; i < 10; i++)
        {
            fg.OptionalCraftsId[i] = (CraftIdEnum)array[0xDDF + i];
        }
        fg.OptionalCraftsCount = array.Subarray(0xDE9, 10);
        fg.OptionalCraftsWaves = array.Subarray(0xDF3, 10);
        fg.PilotVoice = array.ReadFixedLengthString(0xDFD, 20);
        array.ReadUnknown(0xE11, fg.unk000E11);
        fg.PlanetId = array[0xE12];
        array.ReadUnknown(0xE13, fg.unk000E13);
        fg.m000E29 = array[0xE29] != 0;
        array.ReadUnknown(0xE2A, fg.unk000E2A);
        fg.m000E2B = array[0xE2B] != 0;
        array.ReadUnknown(0xE2C, fg.unk000E2C);
        fg.m000E2D = array[0xE2D] != 0;
        array.ReadUnknown(0xE2E, fg.unk000E2E);
        fg.m000E2F = array[0xE2F] != 0;
        array.ReadUnknown(0xE30, fg.unk000E30);
        fg.m000E31 = array[0xE31] != 0;
        array.ReadUnknown(0xE32, fg.unk000E32);
        fg.m000E33 = array[0xE33] != 0;
        array.ReadUnknown(0xE34, fg.unk000E34);
        fg.m000E35 = array[0xE35] != 0;
        array.ReadUnknown(0xE36, fg.unk000E36);
        fg.m000E37 = array[0xE37] != 0;
        array.ReadUnknown(0xE38, fg.unk000E38);
        return fg;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x000, Name, 20);
        array[0x014] = (byte)TacticalRoleUsed0;
        array[0x015] = (byte)TacticalRoleUsed1;
        array[0x016] = (byte)TacticalRole0;
        array[0x017] = (byte)TacticalRole1;
        array[0x018] = Comm;
        array[0x019] = GlobalCargoIndex;
        array[0x01A] = SpecialCargoIndex;
        array.WriteUnknown(0x01B, unk00001B);
        array.WriteFixedLengthString(0x028, Cargo, 20);
        array.WriteFixedLengthString(0x03C, SpecialCargo, 20);
        array.WriteFixedLengthString(0x050, Role, 25);
        array[0x069] = SpecialCraft;
        array[0x06A] = RandomSpecialCraft;
        array[0x06B] = (byte)CraftId;
        array[0x06C] = CraftsCount;
        array[0x06D] = (byte)Status1;
        array[0x06E] = WarheadType;
        array[0x06F] = BeamType;
        array[0x070] = Iff;
        array[0x071] = Team;
        array[0x072] = AIRank;
        array[0x073] = Markings;
        array[0x074] = Radio;
        array.WriteUnknown(0x075, unk000075);
        array[0x076] = FormationType;
        array[0x077] = FormationSpacing;
        array[0x078] = GlobalGroupId;
        array.WriteUnknown(0x079, unk000079);
        array[0x07A] = WavesCount;
        array[0x07B] = WaveDelay;
        array[0x07C] = WaveContinuous;
        array[0x07D] = PlayerNumber;
        array[0x07E] = ArriveOnlyIfPlayer ? (byte)1 : (byte)0;
        array[0x07F] = PlayerCraft;
        array[0x080] = Yaw;
        array[0x081] = Pitch;
        array[0x082] = Roll;
        array.WriteUnknown(0x083, unk000083);
        array[0x084] = LinkId;
        array.WriteUnknown(0x085, unk000085);
        array[0x086] = LinkUnused;
        array[0x087] = (byte)ArrivalDifficulty;
        ArrivalTrigger1.ToByteArray().CopyTo(array, 0x088);
        ArrivalTrigger2.ToByteArray().CopyTo(array, 0x098);
        array[0x0A8] = ArrivalTriggersOperator ? (byte)1 : (byte)0;
        array.WriteUnknown(0x0A9, unk0000A9);
        array[0x0AA] = ArrivalDelayMinutes;
        array[0x0AB] = ArrivalDelaySeconds;
        DepartureTrigger.ToByteArray().CopyTo(array, 0x0AC);
        array[0x0BC] = DepartureDelayMinutes;
        array[0x0BD] = DepartureDelaySeconds;
        array[0x0BE] = AbortCondition;
        array[0x0BF] = ArrivalRandomDelaySeconds;
        BitConverter.GetBytes(CurStartFg).CopyTo(array, 0x0C0);
        array[0x0C2] = StartFg;
        array[0x0C3] = StartFgUsed;
        array[0x0C4] = PrimaryStopFg;
        array[0x0C5] = PrimaryStopFgUsed;
        array[0x0C6] = SecondaryStopFg;
        array[0x0C7] = SecondaryStopFgUsed;
        array[0x0C8] = CaptureFg;
        array[0x0C9] = CaptureFgUsed;
        for (int i = 0; i < 16; i++)
        {
            Orders[i].ToByteArray().CopyTo(array, 0x0CA + i * 0x94);
        }
        for (int i = 0; i < 16; i++)
        {
            JumpTriggers[i].ToByteArray().CopyTo(array, 0xA0A + i * 0x10);
        }
        for (int i = 0; i < 8; i++)
        {
            Goals[i].ToByteArray().CopyTo(array, 0xB0A + i * 0x50);
        }
        for (int i = 0; i < 4; i++)
        {
            StartPoints[i].ToByteArray().CopyTo(array, 0xD8A + i * 0x08);
        }
        for (int i = 0; i < 4; i++)
        {
            array[0xDAA + i] = StartPointRegions[i];
        }
        for (int i = 0; i < 2; i++)
        {
            m000DAE[i].ToByteArray().CopyTo(array, 0xDAE + i * 0x08);
        }
        array[0xDBE] = m000DBE;
        array.WriteUnknown(0xDBF, unk000DBF);
        array[0xDC0] = m000DC0 ? (byte)1 : (byte)0;
        array[0xDC1] = m000DC1 ? (byte)1 : (byte)0;
        array.WriteUnknown(0xDC2, unk000DC2);
        array[0xDC4] = WaveNumberingOff;
        array.WriteUnknown(0xDC5, unk000DC5);
        array[0xDC7] = CounterMeasuresType;
        array[0xDC8] = CraftExplosionTime;
        array[0xDC9] = (byte)Status2;
        array[0xDCA] = GlobalUnitId;
        array.WriteUnknown(0xDCB, unk000DCB);
        OptionalWarheads.CopyTo(array, 0xDCC);
        OptionalBeams.CopyTo(array, 0xDD4);
        OptionalCounterMeasures.CopyTo(array, 0xDDA);
        array[0xDDE] = OptionalCraftCategory;
        for (int i = 0; i < 10; i++)
        {
            array[0xDDF + i] = (byte)OptionalCraftsId[i];
        }
        OptionalCraftsCount.CopyTo(array, 0xDE9);
        OptionalCraftsWaves.CopyTo(array, 0xDF3);
        array.WriteFixedLengthString(0xDFD, PilotVoice, 20);
        array.WriteUnknown(0xE11, unk000E11);
        array[0xE12] = PlanetId;
        array.WriteUnknown(0xE13, unk000E13);
        array[0xE29] = m000E29 ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE2A, unk000E2A);
        array[0xE2B] = m000E2B ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE2C, unk000E2C);
        array[0xE2D] = m000E2D ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE2E, unk000E2E);
        array[0xE2F] = m000E2F ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE30, unk000E30);
        array[0xE31] = m000E31 ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE32, unk000E32);
        array[0xE33] = m000E33 ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE34, unk000E34);
        array[0xE35] = m000E35 ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE36, unk000E36);
        array[0xE37] = m000E37 ? (byte)1 : (byte)0;
        array[0xE37] = m000E37 ? (byte)1 : (byte)0;
        array.WriteUnknown(0xE38, unk000E38);
        return array;
    }
}
