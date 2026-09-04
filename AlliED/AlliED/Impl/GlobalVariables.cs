using AlliED.Helpers;
using System.Collections;

namespace AlliED.Impl;

internal class GlobalVariables
{
    public static GlobalVariables AlliedVariables = new();

    // V0x00506DB8
    public readonly BitArray s_V0x00506DB8 = new(new byte[]
    {
        224,
        19,
        1,
        0,
        0,
        0,
        120,
        0,
        0,
        0,
        0,
        0,
        54,
        0,
        126,
        0,
        0,
        0,
        1,
        0,
        0,
        0,
        5,
        0,
        0,
        0,
        0,
        0,
        104,
        0,
        0,
        0,
    });

    // V0x00506DD8
    public readonly BitArray s_V0x00506DD8 = new(new byte[]
    {
        30,
        64,
        0,
        0,
        16,
        0,
        6,
        0,
        0,
        0,
        0,
        144,
        0,
        0,
        0,
        0,
        0,
        128,
        0,
        0,
        0,
        128,
        0,
        0,
        0,
        0,
        0,
        0,
        16,
        0,
        0,
        0,
    });

    // V0x0051E880
    public readonly BitArray s_V0x0051E880 = new(new byte[]
    {
        30,
        64,
        0,
        0,
        16,
        0,
        6,
        0,
        0,
        0,
        0,
        144,
        0,
        0,
        0,
        0,
        0,
        128,
        0,
        0,
        0,
        128,
        0,
        0,
        0,
        0,
        0,
        0,
        16,
        0,
        0,
        0,
    });

    // V0x0051E8A0
    public readonly BitArray s_V0x0051E8A0 = new(new byte[]
    {
        224,
        19,
        1,
        0,
        0,
        0,
        120,
        0,
        0,
        0,
        0,
        0,
        54,
        0,
        126,
        0,
        0,
        0,
        1,
        0,
        0,
        0,
        5,
        0,
        0,
        0,
        0,
        0,
        104,
        0,
        0,
        0,
    });

    // V0x0051E8C0
    public readonly BitArray s_V0x0051E8C0 = new(new byte[]
    {
        24,
        3,
        37,
        128,
        85,
        139,
        236,
        106,
        0,
        83,
        86,
        87,
        139,
        216,
        51,
        192,
        85,
        104,
        197,
        233,
        81,
        0,
        100,
        255,
        48,
        100,
        137,
        32,
        161,
        240,
        63,
        83,
    });

    // V0x00520170
    public readonly BitArray s_V0x00520170 = new(new byte[]
    {
        0,
        0,
        16,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00520508
    public readonly BitArray s_V0x00520508 = new(new byte[]
    {
        0,
        0,
        16,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x0052ED70
    public readonly BitArray s_V0x0052ED70 = new(new byte[]
    {
        60,
        0,
        0,
        128,
        255,
        255,
        120,
        217,
        28,
        0,
        0,
        0,
        85,
        139,
        236,
        83,
        86,
        87,
        139,
        218,
        139,
        248,
        51,
        192,
        85,
        104,
        63,
        238,
        82,
        0,
        100,
        255,
    });

    // V0x005320B0
    public nint s_GlobalModule;

    // V0x00533B20
    public readonly BitArray s_V0x00533B20 = new(new byte[]
    {
        0,
        0,
        24,
        124,
        63,
        255,
        255,
        255,
        63,
        0,
        0,
        254,
        253,
        63,
        1,
        0,
        254,
        255,
        159,
        15,
        16,
        2,
        0,
        0,
        0,
        0,
        0,
        0,
        112,
        0,
        0,
        0,
    });

    // V0x00533B40
    public readonly BitArray s_V0x00533B40 = new(new byte[]
    {
        254,
        255,
        231,
        131,
        192,
        0,
        0,
        0,
        192,
        57,
        0,
        0,
        2,
        192,
        254,
        175,
        1,
        0,
        64,
        240,
        12,
        64,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533B60
    public readonly BitArray s_V0x00533B60 = new(new byte[]
    {
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        64,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533B80
    public readonly BitArray s_V0x00533B80 = new(new byte[]
    {
        0,
        0,
        0,
        0,
        0,
        0,
        255,
        3,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533BA0
    public readonly BitArray s_V0x00533BA0 = new(new byte[]
    {
        0,
        0,
        0,
        0,
        0,
        0,
        14,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533BC0
    public readonly BitArray s_V0x00533BC0 = new(new byte[]
    {
        254,
        255,
        1,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        254,
        175,
        1,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533BE0
    public readonly BitArray s_V0x00533BE0 = new(new byte[]
    {
        0,
        0,
        254,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        70,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533C00
    public readonly BitArray s_V0x00533C00 = new(new byte[]
    {
        0,
        0,
        0,
        195,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        64,
        0,
        0,
        64,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533C20
    public readonly BitArray s_V0x00533C20 = new(new byte[]
    {
        0,
        0,
        0,
        60,
        255,
        3,
        128,
        15,
        0,
        0,
        0,
        2,
        145,
        255,
        1,
        0,
        0,
        0,
        128,
        15,
        16,
        2,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533C40
    public readonly BitArray s_V0x00533C40 = new(new byte[]
    {
        0,
        0,
        0,
        0,
        0,
        252,
        127,
        0,
        0,
        0,
        0,
        240,
        40,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        112,
        0,
        0,
        0,
    });

    // V0x00533C60
    public readonly BitArray s_V0x00533C60 = new(new byte[]
    {
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        240,
        63,
        64,
        0,
        12,
        0,
        0,
        0,
        0,
        254,
        255,
        31,
        48,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533C80
    public readonly BitArray s_V0x00533C80 = new(new byte[]
    {
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        192,
        57,
        251,
        1,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        192,
        14,
        128,
        255,
        0,
        0,
        0,
        0,
        0,
        8,
        0,
        0,
        0,
    });

    // V0x00533CA0
    public readonly BitArray s_V0x00533CA0 = new(new byte[]
    {
        1,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        135,
        4,
        16,
        0,
        0,
        0,
        80,
        0,
        0,
        32,
        0,
        225,
        61,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533CC0
    public readonly BitArray s_V0x00533CC0 = new(new byte[]
    {
        35,
        1,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533CE0
    public readonly BitArray s_V0x00533CE0 = new(new byte[]
    {
        53,
        2,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00533D00
    public readonly BitArray s_V0x00533D00 = new(new byte[]
    {
        25,
        4,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
    });

    // V0x00535708
    public int s_TlsIndex;

    // V0x00535710
    public nint s_HInstance;

    // V0x005357F8
    public byte s_V0x005357F8;

    // V0x00535C30
    public nint s_TApplication_Instance;

    // V0x00535C34
    public nint s_Allied_TScreen;

    // V0x00535C94
    public TimeForm? s_TTimeForm_Instance;

    // V0x00535C98
    public int s_TTimeForm_RefCount;

    // V0x00535D04
    //public BrowseForm? s_TBrowseForm_Instance;

    // V0x00535D08
    //public int s_TBrowseForm_RefCount;

    // V0x00535E20
    public ImportBox? s_TImportForm_Instance;

    // V0x00535E24
    public int s_TImportForm_RefCount;

    // V0x00535E28
    public ChoiceBox? s_TChoiceBox_Instance;

    // V0x00535E2C
    public int s_TChoiceBox_RefCount;

    // V0x00535E30
    public AboutBox? s_TAboutBox_Instance;

    // V0x00535E34
    public int s_TAboutBox_RefCount;

    // V0x00535E54
    //public BrowseFileForm? s_TBrowseFileForm_Instance;

    // V0x00535E58
    //public int s_TBrowseFileForm_RefCount;

    // V0x00535E5C
    public GoalViewWindow? s_TGoalViewForm_Instance;

    // V0x00535E60
    public TStrings s_V0x00535E60 = new();

    // V0x00535E64
    public TStrings s_V0x00535E64 = new();

    // V0x00535E68
    public TStrings s_V0x00535E68 = new();

    // V0x00535E6C
    public TStrings s_V0x00535E6C = new();

    // V0x00535E70
    public int s_TGoalViewForm_RefCount;

    // V0x00535E78
    public DatapadWindow? s_TDatapad_Instance;

    // V0x00535E7C
    public int s_TDatapad_RefCount;

    // V0x00535E80
    public HyperBox? s_THyperForm_Instance;

    // V0x00535E84
    public int s_THyperForm_RefCount;

    // V0x00535E88
    public MemoWindow? s_TMemoForm_Instance;

    // V0x00535E8C
    public byte s_V0x00535E8C;

    // V0x00535E8D
    public byte s_V0x00535E8D;

    // V0x00535E8E
    public byte s_V0x00535E8E;

    // V0x00535E90
    public MemoPageEnum s_MemoForm_PageIndex;

    // V0x00535E94
    public int s_TMemoForm_RefCount;

    // V0x00535E98
    public HeaderBox? s_THeaderForm_Instance;

    // V0x00535E9C
    public int s_V0x00535E9C;

    // V0x00535EA0
    public int s_THeaderForm_RefCount;

    // V0x00535EA4
    public int s_V0x00535EA4;

    // V0x00535EA8
    public int s_V0x00535EA8;

    // V0x00535EAC
    public int s_V0x00535EAC;

    // V0x00535EB0
    public S0xTieBriefing s_V0x00535EB0 = new();

    // V0x0053BC94
    public int[] s_V0x0053BC94 = new int[51];

    // V0x0053BD64
    public LblBox? s_TLBLForm_Instance;

    // V0x0053BD68
    public TStrings s_V0x0053BD68 = new();

    // V0x0053BD6C
    public TStrings s_V0x0053BD6C = new();

    // V0x0053BD70
    public TStrings s_V0x0053BD70 = new();

    // V0x0053BD74
    public string s_V0x0053BD74 = string.Empty;

    // V0x0053BD78
    public string s_V0x0053BD78 = string.Empty;

    // V0x0053BD7C
    public int s_TLBLForm_RefCount;

    // V0x0053BD80
    public ClipWindow? s_TClipForm_Instance;

    // V0x0053BD84
    public ClipboardTypeEnum s_ClipboardType;

    // V0x0053BD85
    public byte s_V0x0053BD85;

    // V0x0053BD86
    public byte s_V0x0053BD86;

    // V0x0053BD88
    public TStrings s_Allied_Strings_SpeedTxt = new();

    // V0x0053BD8C
    public string s_Allied_SpeedTxt_FileName = string.Empty;

    // V0x0053BD90
    public int s_TClipForm_RefCount;

    // V0x0053BD94
    public FormationBox? s_TFormForm_Instance;

    // V0x0053BD98
    public int s_AlliedCurrentFormation;

    // V0x0053BD9C
    public int s_V0x0053BD9C;

    // V0x0053BDA0
    public TBitmap? s_V0x0053BDA0;

    // V0x0053BDA4
    public bool s_V0x0053BDA4;

    // V0x0053BDA8
    public int s_TFormForm_RefCount;

    // V0x0053BDAC
    public XvTBox? s_TXvTForm_Instance;

    // V0x0053BDB0
    public int s_V0x0053BDB0;

    // V0x0053BDB4
    public int s_V0x0053BDB4;

    // V0x0053BDB8
    public short s_V0x0053BDB8;

    // V0x0053BDBC
    public S0xTieBriefing s_V0x0053BDBC = new();

    // V0x0053F476
    public byte[] s_V0x0053F476 = new byte[10008];

    // V0x00541BA0
    public int s_TXvTForm_RefCount;

    // V0x00541BA4
    public int s_V0x00541BA4;

    // V0x00541BA8
    public int s_V0x00541BA8;

    // V0x00541BAC
    public int s_V0x00541BAC;

    // V0x00541BB0
    public int s_V0x00541BB0;

    // V0x00541BB4
    public MissionExportTypeEnum s_V0x00541BB4;

    // V0x00541BB8
    public int s_V0x00541BB8;

    // V0x00541BBC
    /// <remarks>unknown size</remarks>
    public int[] s_V0x00541BBC = new int[151];

    // V0x00541E18
    public S0x00541E18 s_V0x00541E18 = new();

    // V0x00541EE0
    public short s_V0x00541EE0;

    // V0x00541EE4
    public byte s_V0x00541EE4;

    // V0x00541EE8
    public TFileRec s_V0x00541EE8 = new();

    // V0x00542034
    public string s_V0x00542034 = string.Empty;

    // V0x00542038
    public S0x00542038 s_V0x00542038 = new();

    // V0x00542040
    public int s_V0x00542040;

    // V0x00542044
    public int s_V0x00542044;

    // V0x00542048
    public int s_V0x00542048;

    // V0x0054204C
    public double s_V0x0054204C;

    // V0x00542054
    public TStrings s_V0x00542054 = new();

    // V0x00542058
    /// <remarks>unknown size</remarks>
    public short[] s_V0x00542058 = new short[1];

    // V0x00542378
    public S0x00542038 s_V0x00542378 = new();

    // V0x00542380
    public S0x00542380 s_V0x00542380 = new();

    // V0x005423EC
    public int s_V0x005423EC;

    // V0x005423F4
    public UnknownsWindow? s_TUnksForm_Instance;

    // V0x005423F8
    public byte s_V0x005423F8;

    // V0x005423FC
    public int s_TUnksForm_RefCount;

    // V0x00542400
    public WavListBox? s_TWAVListForm_Instance;

    // V0x00542404
    public TStrings s_TacticalOfficersVoiceList = new();

    // V0x00542408
    public TStrings s_V0x00542408 = new();

    // V0x0054240C
    public TStrings s_V0x0054240C = new();

    // V0x00542410
    public TStrings s_PilotsVoiceList = new();

    // V0x00542414
    public TStrings s_V0x00542414 = new();

    // V0x00542418
    public TStrings s_CD1VoiceList = new();

    // V0x0054241C
    public TStrings s_V0x0054241C = new();

    // V0x00542420
    public TStrings s_CD2VoiceList = new();

    // V0x00542424
    public TStrings s_V0x00542424 = new();

    // V0x00542428
    public TStrings s_CustomVoiceList = new();

    // V0x0054242C
    public TStrings s_V0x0054242C = new();

    // V0x00542430
    public string s_V0x00542430 = string.Empty;

    // V0x00542434
    public string s_V0x00542434 = string.Empty;

    // V0x00542438
    public byte s_V0x00542438;

    // V0x00542439
    public byte s_V0x00542439;

    // V0x0054243C
    public int s_TWAVListForm_RefCount;

    // V0x00542448
    public int s_V0x00542448;

    // V0x0054244C
    public int s_V0x0054244C;

    // V0x00542464
    public MapWindow? s_TMapForm_Instance;

    // V0x00542468
    public int s_TMapForm_RefCount;

    // V0x0054246C
    public LstWindow? s_TlstForm_Instance;

    // V0x00542470
    public short s_V0x00542470;

    // V0x00542472
    public short s_V0x00542472;

    // V0x00542474
    public int s_TlstForm_RefCount;

    // V0x00542478
    public BriefingWindow? s_TBrfForm_Instance;

    // V0x0054247C
    public TStrings s_V0x0054247C = new();

    // V0x00542480
    public TStrings s_V0x00542480 = new();

    // V0x00542484
    public TStrings s_V0x00542484 = new();

    // V0x00542488
    public TStrings s_V0x00542488 = new();

    // V0x0054248C
    public TStrings s_V0x0054248C = new();

    // V0x00542490
    public TStrings s_V0x00542490 = new();

    // V0x00542494
    public int s_V0x00542494;

    // V0x00542498
    public int s_V0x00542498;

    // V0x0054249C
    public int s_V0x0054249C;

    // V0x005424A0
    public int s_V0x005424A0;

    // V0x005424A4
    public int s_V0x005424A4;

    // V0x005424A8
    public int s_V0x005424A8;

    // V0x005424AC
    public int s_V0x005424AC;

    // V0x005424B0
    public int s_V0x005424B0;

    // V0x005424B4
    public int s_V0x005424B4;

    // V0x005424B8
    public int s_V0x005424B8;

    // V0x005424BC
    public int s_V0x005424BC;

    // V0x005424C0
    public int s_V0x005424C0;

    // V0x005424C4
    public int s_V0x005424C4;

    // V0x005424C8
    public int s_V0x005424C8;

    // V0x005424CC
    public int s_V0x005424CC;

    // V0x005424D0
    public int s_BrfFormXSnapBtnStep;

    // V0x005424D4
    public int s_BrfFormYSnapBtnStep;

    // V0x005424D8
    public int s_V0x005424D8;

    // V0x005424DC
    public int s_V0x005424DC;

    // V0x005424E0
    public int s_V0x005424E0;

    // V0x005424E4
    public int s_V0x005424E4;

    // V0x005424E8
    public int s_V0x005424E8;

    // V0x005424EC
    public int s_V0x005424EC;

    // V0x005424F0
    public int s_V0x005424F0;

    // V0x005424F4
    public int s_V0x005424F4;

    // V0x005424F8
    public int s_V0x005424F8;

    // V0x005424FC
    public int s_V0x005424FC;

    // V0x00542500
    public int s_V0x00542500;

    // V0x00542504
    public byte s_V0x00542504;

    // V0x00542505
    public byte s_V0x00542505;

    // V0x00542506
    public byte s_V0x00542506;

    // V0x00542507
    public byte s_V0x00542507;

    // V0x00542508
    public byte s_V0x00542508;

    // V0x00542509
    public byte s_V0x00542509;

    // V0x0054250A
    public byte s_V0x0054250A;

    // V0x0054250B
    public byte s_V0x0054250B;

    // V0x0054250C
    public byte s_V0x0054250C;

    // V0x0054250D
    public byte s_V0x0054250D;

    // V0x0054250E
    public bool s_V0x0054250E;

    // V0x00542510
    public TCommand s_V0x00542510 = new();

    // V0x005429B4
    public TCommand s_V0x005429B4 = new();

    // V0x005429B6
    public short s_V0x005429B6;

    // V0x00542E58
    public S0x00542E58[] s_V0x00542E58 = ArrayHelpers.CreateArray<S0x00542E58>(8);

    // V0x00542E78
    public int[] s_V0x00542E78 = new int[8];

    // V0x00542E98
    public int[] s_V0x00542E98 = new int[8];

    // V0x00542EB8
    public TBitmap? s_V0x00542EB8;

    // V0x00542EBC
    public TBitmap? s_V0x00542EBC;

    // V0x00542EC0
    public string s_V0x00542EC0 = string.Empty;

    // V0x00542EC4
    public byte s_V0x00542EC4;

    // V0x00542EC5
    public byte s_V0x00542EC5;

    // V0x00542EC6
    public byte s_V0x00542EC6;

    // V0x00542EC7
    public byte s_V0x00542EC7;

    // V0x00542EC8
    public byte s_V0x00542EC8;

    // V0x00542EC9
    public byte s_V0x00542EC9;

    // V0x00542ECA
    public byte s_V0x00542ECA;

    // V0x00542ECB
    public byte s_V0x00542ECB;

    // V0x00542ECC
    public byte s_V0x00542ECC;

    // V0x00542ECD
    public bool s_V0x00542ECD;

    // V0x00542ECE
    public byte s_V0x00542ECE;

    // V0x00542ECF
    public byte s_V0x00542ECF;

    // V0x00542ED0
    public byte s_V0x00542ED0;

    // V0x00542ED1
    public byte s_V0x00542ED1;

    // V0x00542ED2
    public byte s_V0x00542ED2;

    // V0x00542ED3
    public byte s_V0x00542ED3;

    // V0x00542ED4
    public CraftIdEnum s_V0x00542ED4;

    // V0x00542ED8
    public int s_V0x00542ED8;

    // V0x00542EDC
    /// <remarks>unknown size</remarks>
    public S0x00542EDC[] s_V0x00542EDC = ArrayHelpers.CreateArray<S0x00542EDC>(51);

    // V0x005432D8
    /// <remarks>unknown size</remarks>
    public TBitmap?[] s_V0x005432D8 = new TBitmap?[51];

    // V0x005433A4
    public TBitmap? s_V0x005433A4;

    // V0x005433A8
    public TBitmap? s_V0x005433A8;

    // V0x005433AC
    public TRect s_V0x005433AC;

    // V0x005433BC
    public TRect s_V0x005433BC;

    // V0x005433CC
    public TRect s_V0x005433CC;

    // V0x005433DC
    public TRect s_V0x005433DC;

    // V0x005433EC
    public int s_V0x005433EC;

    // V0x005433F0
    public int s_V0x005433F0;

    // V0x005433F4
    public int s_V0x005433F4;

    // V0x005433F8
    public int s_V0x005433F8;

    // V0x005433FC
    public int s_V0x005433FC;

    // V0x00543400
    public int s_V0x00543400;

    // V0x00543404
    public IconRotationEnum s_BriefingCurrentIconRotation;

    // V0x00543408
    public byte s_V0x00543408;

    // V0x0054340C
    public string s_V0x0054340C = string.Empty;

    // V0x00543410
    public AlliedMapBitmap[] s_AlliedMapBitmaps = ArrayHelpers.CreateArray<AlliedMapBitmap>(165);

    // V0x00543938
    public S0x00533F48 s_V0x00543938 = new();

    // V0x0054393C
    public short s_IconSpeedOptionSetting;

    // V0x00543944
    public byte s_V0x00543944;

    // V0x00543948
    public byte s_V0x00543948;

    // V0x0054394C
    public string s_V0x0054394C = string.Empty;

    // V0x00543950
    public string s_V0x00543950 = string.Empty;

    // V0x00543958
    public PreferencesWindow? s_TPrefForm_Instance;

    // V0x0054395C
    public bool s_V0x0054395C;

    // V0x00543960
    public int s_TPrefForm_RefCount;

    // V0x00543964
    public WaypointsWindow? s_TWPform_Instance;

    // V0x00543968
    public int s_TWPform_RefCount;

    // V0x0054396C
    public OrderSelWindow? s_TOrderSel_Instance;

    // V0x00543970
    public int s_TOrderSel_RefCount;

    // V0x00543974
    public BackdropBox? s_TBDropForm_Instance;

    // V0x00543978
    public int s_Allied_BackdropPlanetIndex;

    // V0x0054397C
    public int s_Allied_BackdropShadowIndex;

    // V0x00543980
    public int s_TBDropForm_RefCount;

    // V0x00543984
    public ShipExtWindow? s_TShipExtWindow_Instance;
    public ShipExtUserControl? s_TShipExt_Instance;

    // V0x00543988
    public int s_TShipExt_RefCount;

    // V0x0054398C
    public CondToolWindow? s_TCondToolFormWindow_Instance;
    public CondToolUserControl? s_TCondToolForm_Instance;

    // V0x00543990
    public byte s_V0x00543990;

    // V0x00543994
    public int s_TCondToolForm_RefCount;

    // V0x00543998
    public ErrorBox? s_TErrForm_Instance;

    // V0x0054399C
    public TStrings s_TErrForm_Items1 = new();

    // V0x005439A0
    public TStrings s_TErrForm_Items2 = new();

    // V0x005439A4
    public int s_TErrForm_RefCount;

    // V0x005439A8
    public S0x00534640 s_V0x005439A8 = new();

    // V0x005439C4
    public string s_AlliedDirectoryDriveLetter = string.Empty;

    // V0x005439C8
    public string s_Allied_InstallPath = string.Empty;

    // V0x005439CC
    public TFixedString s_V0x005439CC = new TFixedString(256);

    // V0x00543ACC
    public int[] s_V0x00543ACC = new int[8];

    // V0x00543AF4
    public TieFileVersionEnum s_TieFileVersion;

    // V0x00543AF6
    public byte s_V0x00543AF6;

    // V0x00543AFC
    public LibWindow? s_TLibForm_Instance;

    // V0x00543B00
    public List<S0xFGObject> s_LibFormFlightGroupObjectsList = new();

    // V0x00543B04
    public bool s_LibFormHasChanged;

    // V0x00543B08
    public int s_TLibForm_RefCount;

    // V0x00543B0C
    public int s_V0x00543B0C;

    // V0x00543B10
    public int s_V0x00543B10;

    // V0x00543B14
    public int s_V0x00543B14;

    // V0x00543B18
    public int s_V0x00543B18;

    // V0x00543B1C
    public int s_CurrentOrderInRegion;

    // V0x00543B20
    public int s_V0x00543B20;

    // V0x00543B24
    public int s_V0x00543B24;

    // V0x00543B28
    public int s_V0x00543B28;

    // V0x00543B2C
    public int s_V0x00543B2C;

    // V0x00543B30
    public int s_GGoalCurrentTrigger;

    // V0x00543B34
    public DatapadFGPageEnum s_AlliedDatapadCurrentPage;

    // V0x00543B38
    public int s_CurrentRegion;

    // V0x00543B3C
    public int s_V0x00543B3C;

    // V0x00543B40
    public int s_V0x00543B40;

    // V0x00543B44
    public int s_V0x00543B44;

    // V0x00543B48
    public int s_V0x00543B48;

    // V0x00543B4C
    public int s_V0x00543B4C;

    // V0x00543B50
    public byte s_V0x00543B50;

    // V0x00543B51
    public byte s_V0x00543B51;

    // V0x00543B52
    public byte s_V0x00543B52;

    // V0x00543B53
    public bool s_V0x00543B53;

    // V0x00543B54
    public byte s_V0x00543B54;

    // V0x00543B55
    public byte s_V0x00543B55;

    // V0x00543B56
    public byte s_V0x00543B56;

    // V0x00543B57
    public bool s_UseAutoChkSetting;

    // V0x00543B58
    public bool s_PlayableChkSetting;

    // V0x00543B59
    public byte s_V0x00543B59;

    // V0x00543B5A
    public bool s_TErrForm_OmitWarns;

    // V0x00543B5B
    public byte s_V0x00543B5B;

    // V0x00543B5C
    public byte s_V0x00543B5C;

    // V0x00543B60
    public TStrings s_Strings_Ships = new();

    // V0x00543B64
    public TStrings s_V0x00543B64 = new();

    // V0x00543B68
    public TStrings s_Strings_FGNames = new();

    // V0x00543B6C
    public TStrings s_Strings_Players = new();

    // V0x00543B70
    public TStrings s_Strings_Teams = new();

    // V0x00543B74
    public TStrings s_Strings_Status = new();

    // V0x00543B78
    public TStrings s_Strings_AI = new();

    // V0x00543B7C
    public TStrings s_Strings_IFF = new();

    // V0x00543B80
    public TStrings s_Strings_Colors = new();

    // V0x00543B84
    public TStrings s_Strings_Radio = new();

    // V0x00543B88
    public TStrings s_Strings_Form = new();

    // V0x00543B8C
    public TStrings s_Strings_Missiles = new();

    // V0x00543B90
    public TStrings s_Strings_Counters = new();

    // V0x00543B94
    public TStrings s_Strings_Beams = new();

    // V0x00543B98
    public TStrings s_Strings_CMD = new();

    // V0x00543B9C
    public TStrings s_Strings_OpShips = new();

    // V0x00543BA0
    public TStrings s_Strings_Orders = new();

    // V0x00543BA4
    public TStrings s_Strings_Speeds = new();

    // V0x00543BA8
    public TStrings s_V0x00543BA8 = new();

    // V0x00543BAC
    public TStrings s_Allied_Numbers_NoneTo255 = new();

    // V0x00543BB0
    public TStrings s_Strings_Backdrops = new();

    // V0x00543BB4
    public TStrings s_Strings_Musts = new();

    // V0x00543BB8
    public TStrings s_Strings_ObjCats = new();

    // V0x00543BBC
    public TStrings s_Strings_ShipCats = new();

    // V0x00543BC0
    public TStrings s_V0x00543BC0 = new();

    // V0x00543BC4
    public TStrings s_Strings_Regions = new();

    // V0x00543BC8
    public TStrings s_V0x00543BC8 = new();

    // V0x00543BCC
    public TStrings s_V0x00543BCC = new();

    // V0x00543BD0
    public TStrings s_Strings_Short = new();

    // V0x00543BD4
    public TStrings s_V0x00543BD4 = new();

    // V0x00543BD8
    public TStrings s_Strings_Planets = new();

    // V0x00543BDC
    public TStrings s_Strings_WPEnable = new();

    // V0x00543BE0
    public TStrings s_Strings_When = new();

    // V0x00543BE4
    public TStrings s_Strings_DepWhen = new();

    // V0x00543BE8
    public TStrings s_V0x00543BE8 = new();

    // V0x00543BEC
    public TStrings s_Strings_OrdTexts = new();

    // V0x00543BF0
    public TStrings s_Strings_ShipSeq = new();

    // V0x00543BF4
    public TStrings s_TDatapad_T1ClassStrings = new();

    // V0x00543BF8
    public string s_V0x00543BF8 = string.Empty;

    // V0x00543BFC
    public string s_V0x00543BFC = string.Empty;

    // V0x00543C00
    public string s_OPTDirLabSetting = string.Empty;

    // V0x00543C04
    public string s_V0x00543C04 = string.Empty;

    // V0x00543C08
    public string s_FlightGroupLibraryFileName = string.Empty;

    // V0x00543C0C
    public string s_StartDirLabSetting = string.Empty;

    // V0x00543C10
    public string s_V0x00543C10 = string.Empty;

    // V0x00543C14
    public string s_BriefingsDirectorySetting = string.Empty;

    // V0x00543C18
    public string s_XWDirLabSetting = string.Empty;

    // V0x00543C1C
    public string s_XvTDirLabSetting = string.Empty;

    // V0x00543C20
    public string s_TFDirLabSetting = string.Empty;

    // V0x00543C24
    public string s_XWADirLabSetting = string.Empty;

    // V0x00543C28
    public string s_BoPDirLabSetting = string.Empty;

    // V0x00543C30
    public string s_AlliedDirectoryPath = string.Empty;

    // V0x00543C34
    public string s_WaveDirLabSetting = string.Empty;

    // V0x00543C38
    public TStrings s_Allied_FilenamesHistory = new();

    // V0x00543C3C
    public TStrings s_V0x00543C3C = new();

    // V0x00543C40
    public int s_V0x00543C40;

    // V0x00543C44
    /// <remarks>unknown type</remarks>
    public int s_V0x00543C44;

    // V0x00543C48
    public int s_V0x00543C48;

    // V0x00543C4C
    public int[] s_V0x00543C4C = new int[3];

    // V0x00543C58
    public int[] s_V0x00543C58 = new int[3];

    // V0x00543C64
    public bool[] s_V0x00543C64 = new bool[12];

    // V0x00543C70
    /// <remarks>unknown type</remarks>
    public int s_V0x00543C70;

    // V0x00543C74
    /// <remarks>unknown type</remarks>
    public int s_V0x00543C74;

    // V0x00543C78
    public byte[] s_V0x00543C78 = new byte[10];

    // V0x00543C84
    public S0x00534058 s_V0x00543C84 = new();

    // V0x00543C99
    public byte s_V0x00543C99;

    // V0x00543C9A
    public byte s_V0x00543C9A;

    // V0x00543C9B
    public byte s_V0x00543C9B;

    // V0x00543C9C
    public byte s_V0x00543C9C;

    // V0x00543C9D
    public byte s_V0x00543C9D;

    // V0x00543C9E
    public byte s_V0x00543C9E;

    // V0x00543C9F
    public byte s_V0x00543C9F;

    // V0x00543CA0
    public bool s_EnableWPsChkSetting;

    // V0x00543CA1
    public bool s_LinkColorChkSetting;

    // V0x00543CA2
    public bool s_Allied_DoesBackdropsTxt_FileExist;

    // V0x00543CA3
    public bool s_ResChkSetting;

    // V0x00543CA4
    public int s_WAVListForm_TacChoice_ItemIndex;

    // V0x00543CA8
    public int s_WAVListForm_PilotChoice_ItemIndex;

    // V0x00543CAC
    public VoiceTabsPageEnum s_VoiceTabsPageIndex;

    // V0x00543CB0
    public CraftIdEnum s_DefaultShipBoxSettingIndex;

    // V0x00543CB4
    public int s_DefaultAIBoxSettingIndex;

    // V0x00543CB8
    public int s_IconZoomEditSetting;

    // V0x00543CBC
    public int s_DblClickLinkRadioIndexSetting;

    // V0x00543CC0
    public int s_V0x00543CC0;

    // V0x00543CC4
    public int s_V0x00543CC4;

    // V0x00543CC8
    public int s_V0x00543CC8;

    // V0x00543CCC
    public int s_AlliedAplicationWidth;

    // V0x00543CD0
    public int s_AlliedAplicationHeight;

    // V0x00543CD4
    public double s_V0x00543CD4;

    // V0x00543CDC
    public double s_V0x00543CDC;

    // V0x00543CE4
    public double s_V0x00543CE4;

    // V0x00543CEC
    public double s_V0x00543CEC;

    // V0x00543CF4
    public List<TCommandObject> s_V0x00543CF4 = new();

    // V0x00543CF8
    public List<S0xCondObjectStruct> s_V0x00543CF8 = new();

    // V0x00543CFC
    public List<S0xOrdObject> s_V0x00543CFC = new();

    // V0x00543D00
    public bool s_ConfDeletesChkSetting;

    // V0x00543D01
    public bool s_UseWizChkSetting;

    // V0x00543D02
    public bool s_CenteringSetting;

    // V0x00543D03
    public bool s_ConfirmCloseMapSetting;

    // V0x00543D04
    public bool s_NamesOnChkSetting;

    // V0x00543D05
    public bool s_DefaultHypOnChkSetting;

    // V0x00543D06
    public bool s_DefaultFGListMapSetting;

    // V0x00543D07
    public bool s_DefaultPalletOnChkSetting;

    // V0x00543D08
    public bool s_DefaultOptionsOnChkSetting;

    // V0x00543D09
    public bool s_GhostMapChkSetting;

    // V0x00543D0A
    public bool s_SSD17chkSetting;

    // V0x00543D0B
    public byte s_V0x00543D0B;

    // V0x00543D0C
    public byte s_V0x00543D0C;

    // V0x00543D0D
    public bool s_ConfSaveChkSetting;

    // V0x00543D0E
    public bool s_LimitShrinkChkSetting;

    // V0x00543D0F
    public bool s_DarkGridChkSetting;

    // V0x00543D10
    public bool s_OnlyXYChkSetting;

    // V0x00543D11
    public byte s_V0x00543D11;

    // V0x00543D12
    public bool s_DirInHistChkSetting;

    // V0x00543D13
    public bool s_DoBackupsChkSetting;

    // V0x00543D14
    public bool s_BlackenChkSetting;

    // V0x00543D15
    public bool s_LockOrdToRegOptionSetting;

    // V0x00543D16
    public bool s_CheckFilenameOptionSetting;

    // V0x00543D17
    public bool s_ShowNumbersMapSetting;

    // V0x00543D18
    public bool s_OverwriteBKUPOptionSetting;

    // V0x00543D19
    public byte s_MissionFormatRadioIndexSetting;

    // V0x00543D1A
    public OpenWithRadioEnum s_OpenWithRadioIndexSetting;

    // V0x00543D1B
    public WPsDefaultRadioEnum s_WPsDefaultRadioIndexSetting;

    // V0x00543D1C
    public AutoCtrGrpEnum s_AutoCtrGrpIndexSetting;

    // V0x00543D1D
    public byte s_ZoomSpeedScrollPositionSetting;

    // V0x00543D20
    public short[] s_V0x00543D20 = new short[6];

    // V0x00543D2C
    public string s_WAVListFormSearchString = string.Empty;

    // V0x00543D30
    public short[] s_V0x00543D30 = new short[3];

    // V0x00543D38
    //public Canvas? s_V0x00543D38;
    public TBitmap? s_V0x00543D38;

    // V0x00543D40
    public TBitmap? s_V0x00543D40;

    // V0x00543D44
    public int s_V0x00543D44;

    // V0x00543D48
    public int s_V0x00543D48;

    // V0x00543D4C
    public int s_V0x00543D4C;

    // V0x00543D50
    public int s_V0x00543D50;

    // V0x00543D54
    public S0xTieFileHeader s_TieFileHeader = new();

    // V0x00546144
    public S0x00546144 s_V0x00546144 = new();

    // V0x0054A578
    public S0x0054A578 s_V0x0054A578 = new();

    // V0x0054A5E4
    public S0x0054A5E4 s_V0x0054A5E4 = new();

    // V0x0054AE60
    public S0x0054AE60 s_V0x0054AE60 = new();

    // V0x0054AEAC
    public S0x0054AEAC s_V0x0054AEAC = new();

    // V0x0054B67C
    public S0x00533EA8[] s_V0x0054B67C = ArrayHelpers.CreateArray<S0x00533EA8>(2000);

    // V0x0056A2AC
    public S0x0056A2AC s_V0x0056A2AC = new();

    // V0x0056A874
    public S0x0056A874[] s_V0x0056A874 = ArrayHelpers.CreateArray<S0x0056A874>(265);

    // V0x00570FF8
    public S0x00570FF8 s_V0x00570FF8 = new();

    // V0x00570FFC
    public S0x00570FFC s_V0x00570FFC = new();

    // V0x00571000
    public S0x00571000 s_V0x00571000 = new();

    // V0x00571004
    public S0x00571004 s_V0x00571004 = new();

    // V0x005710F0
    public S0x005710F0 s_V0x005710F0 = new();

    // V0x0057111C
    public S0x0057111C s_V0x0057111C = new();

    // V0x00571174
    public List<S0xFGObject> s_FlightGroupObjectsList = new();

    // V0x00571178
    public List<S0xTieRadioMessageObject> s_RadioMessagesObjectsList = new();

    // V0x0057117C
    public List<S0xTieGlobalGoalObject> s_GlobalGoalsObjectsList = new();

    // V0x00571180
    public List<S0xTieTeamObject> s_TeamsObjectsList = new();

    // V0x00571184
    public List<object> s_V0x00571184 = new();

    // V0x00571188
    public List<S0xXvTGGStrObject> s_V0x00571188 = new();

    // V0x0057118C
    public S0xTieBriefing[] s_Allied_Briefing = ArrayHelpers.CreateArray<S0xTieBriefing>(10);

    // V0x005ABC74
    public TFixedString s_TieMission_Description = new(4096);

    // V0x005ACC74
    public TFixedString s_TieMission_WinDebriefing = new(4096);

    // V0x005ADC74
    public TFixedString s_TieMission_LostDebriefing = new(4096);

    // V0x005AEC74
    public TFixedString s_TieMission_Notes = new(4096);

    // V0x005AFC74
    public S0x005342B0 s_V0x005AFC74 = new();

    // V0x005AFCBC
    public TBitmap? s_V0x005AFCBC;

    // V0x005AFCC0
    public TBitmap? s_V0x005AFCC0;

    // V0x005AFCC4
    public TBitmap? s_V0x005AFCC4;

    // V0x005AFCC8
    public S0x005341C0 s_V0x005AFCC8 = new();

    // V0x005AFCE0
    public S0x005346C4 s_V0x005AFCE0 = new();

    // V0x005AFE60
    public S0x005346AC s_V0x005AFE60 = new();

    // V0x005AFE90
    public S0xFGObject s_V0x005AFE90 = new();

    // V0x005AFE94
    public S0xFGObject s_V0x005AFE94 = new();

    // V0x005AFE98
    public S0xTieBriefingData s_TieBriefingData_Instance = new();

    // V0x005B5C74
    public S0xTieFlightGroup s_V0x005B5C74 = new();

    // V0x005B6AB4
    public S0xTieRadioMessage s_V0x005B6AB4 = new();

    // V0x005B6B58
    public S0xTieRadioMessageObject s_V0x005B6B58 = new();

    // V0x005B6B5C
    public S0xTieRadioMessageObject s_V0x005B6B5C = new();

    // V0x005B6B64
    public S0xTieFlightGroupGoal s_V0x005B6B64 = new();

    // V0x005B6BB4
    public S0xTieTeamObject s_V0x005B6BB4 = new();

    // V0x005B6BB8
    public S0xTieGlobalGoalObject s_V0x005B6BB8 = new();

    // V0x005B6BBC
    public S0xXvTGGStrObject s_V0x005B6BBC = new();

    // V0x005B6BC0
    public Form1Window? s_AlliedForm1Window;

    // V0x005B6BC4
    public TFileRec s_TieFileHandle = new();

    // V0x005B6D10
    public char s_V0x005B6D10;

    // V0x005B6D11
    public byte s_V0x005B6D11;

    // V0x005B6D12
    public byte s_V0x005B6D12;

    // V0x005B6D13
    public byte s_V0x005B6D13;

    // V0x005B6D14
    public byte s_V0x005B6D14;

    // V0x005B6D15
    public byte s_V0x005B6D15;

    // V0x005B6D16
    public byte s_V0x005B6D16;

    // V0x005B6D17
    public byte s_V0x005B6D17;

    // V0x005B6D18
    public byte s_V0x005B6D18;

    // V0x005B6D19
    public byte s_V0x005B6D19;

    // V0x005B6D1C
    public int s_V0x005B6D1C;

    // V0x005B6D20
    //public int s_V0x005B6D20;
    public CraftIdEnum s_V0x005B6D20;

    // V0x005B6D24
    public MapOrientationEnum s_AlliedMapOrientation;

    // V0x005B6D28
    public double s_V0x005B6D28;

    // V0x005B6D30
    public double s_V0x005B6D30;

    // V0x005B6D38
    public double s_V0x005B6D38;

    // V0x005B6D40
    public double s_V0x005B6D40;

    // V0x005B6D48
    public double s_V0x005B6D48;

    // V0x005B6D50
    public double s_V0x005B6D50;

    // V0x005B6D58
    public double s_V0x005B6D58;

    // V0x005B6D60
    public int s_V0x005B6D60;

    // V0x005B6D64
    public int s_V0x005B6D64;

    // V0x005B6D68
    public int s_V0x005B6D68;

    // V0x005B6D6C
    public int s_V0x005B6D6C;

    // V0x005B6D70
    public int s_V0x005B6D70;

    // V0x005B6D74
    public int s_V0x005B6D74;

    // V0x005B6D78
    public TPoint[] s_V0x005B6D78 = new TPoint[75];

    // V0x005B6FD0
    public double s_V0x005B6FD0;

    // V0x005B6FD8
    public double s_V0x005B6FD8;

    // V0x005B6FE0
    public double s_V0x005B6FE0;

    // V0x005B6FE8
    public double s_V0x005B6FE8;

    // V0x005B6FF0
    public uint s_V0x005B6FF0;

    // V0x005B6FF4
    public uint s_V0x005B6FF4;

    // V0x005B6FF8
    public uint s_V0x005B6FF8;

    // V0x005B6FFC
    public uint s_V0x005B6FFC;

    // V0x005B7000
    public uint s_V0x005B7000;

    // V0x005B7004
    public byte s_V0x005B7004;

    // V0x005B7005
    public byte s_V0x005B7005;

    // V0x005B7006
    public bool s_V0x005B7006;

    // V0x005B7007
    public bool s_V0x005B7007;

    // V0x005B7008
    public byte s_V0x005B7008;

    // V0x005B7009
    public byte s_V0x005B7009;

    // V0x005B700A
    public byte s_V0x005B700A;

    // V0x005B700B
    public byte s_V0x005B700B;

    // V0x005B700C
    public byte s_V0x005B700C;

    // V0x005B700D
    public byte s_V0x005B700D;

    // V0x005B7010
    public double s_V0x005B7010;

    // V0x005B7018
    public double s_V0x005B7018;

    // V0x005B7020
    public double s_V0x005B7020;

    // V0x005B7028
    public double s_V0x005B7028;

    // V0x005B7030
    public double s_V0x005B7030;

    // V0x005B7038
    public double s_V0x005B7038;

    // V0x005B7040
    public byte s_V0x005B7040;

    // V0x005B7041
    public byte s_V0x005B7041;

    // V0x005B7042
    public byte s_V0x005B7042;

    // V0x005B7043
    public byte s_V0x005B7043;

    // V0x005B7044
    public byte s_V0x005B7044;

    // V0x005B7045
    public byte s_V0x005B7045;

    // V0x005B7046
    public bool s_V0x005B7046;

    // V0x005B7047
    public bool s_V0x005B7047;

    // V0x005B7048
    public bool s_V0x005B7048;

    // V0x005B7049
    public bool s_MpoLayoutSetting;

    // V0x005B704A
    public bool s_V0x005B704A;

    // V0x005B704B
    public bool s_V0x005B704B;

    // V0x005B704C
    public byte s_V0x005B704C;

    // V0x005B7050
    public int s_V0x005B7050;

    // V0x005B7054
    public int s_V0x005B7054;

    // V0x005B7058
    public int s_V0x005B7058;

    // V0x005B705C
    public int s_V0x005B705C;

    // V0x005B7060
    public int s_V0x005B7060;

    // V0x005B7064
    public int s_V0x005B7064;

    // V0x005B7068
    public int s_V0x005B7068;

    // V0x005B706C
    public int s_V0x005B706C;

    // V0x005B7070
    public int s_V0x005B7070;

    // V0x005B7074
    public int s_V0x005B7074;

    // V0x005B7078
    public uint s_V0x005B7078;

    // V0x005B707C
    public int s_V0x005B707C;

    // V0x005B7080
    public int s_V0x005B7080;

    // V0x005B7084
    public int s_V0x005B7084;

    // V0x005B7088
    public int[] s_V0x005B7088 = new int[3];

    // V0x005B7094
    public TBitmap? s_V0x005B7094;

    // V0x005B7098
    public TRect s_V0x005B7098;

    // V0x005B70A8
    public byte s_V0x005B70A8;

    // V0x005B70AC
    public string s_V0x005B70AC = string.Empty;

    // V0x005B70B0
    public char s_AlliedDriveLetter;
}
