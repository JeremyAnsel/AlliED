using AlliED.Extensions;
using System.Globalization;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class HeaderBoxImpl
{
    public static void Register(HeaderBox window)
    {
        SetBindings(window);
        //FormCreate(window);
    }

    private static void SetBindings(HeaderBox window)
    {
        window.Loaded += (s, e) => FormCreate(window);
        window.Closed += (s, e) => THeaderForm_FormClose(window);

        window.RegionNameCollection.CollectionChanged += (s, e) => THeaderForm_RegionNameGridSetEditText(window, null, 0, null, 0);
        window.Str3Collection.CollectionChanged += (s, e) => THeaderForm_RegionNameGridSetEditText(window, null, 0, null, 0);
        window.Str2Collection.CollectionChanged += (s, e) => THeaderForm_RegionNameGridSetEditText(window, null, 0, null, 0);
        window.HeaderStringCollection.CollectionChanged += (s, e) => THeaderForm_HeaderStringGridSetEditText(window, null, 0, null, 0);
        window.Str2Grid.SelectionChanged += (s, e) => THeaderForm_Str2GridSelectCell(window, (ListView)s, null, null, 0);
        window.HangarBox.SelectionChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.Edit2.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.Edit3.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.Edit7.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.Edit8.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.Edit10.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.C1.Click += (s, e) => THeaderForm_Edit1Change(window);
        window.C2.Click += (s, e) => THeaderForm_Edit1Change(window);
        window.C3.Click += (s, e) => THeaderForm_Edit1Change(window);
        window.C4.Click += (s, e) => THeaderForm_Edit1Change(window);
        window.V1.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.V2.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.V3.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.V4.TextChanged += (s, e) => THeaderForm_Edit1Change(window);
        window.Str2_5.TextChanged += (s, e) => THeaderForm_Str2_5Change(window, (TextBox)s);
        window.Str2_9.TextChanged += (s, e) => THeaderForm_Str2_5Change(window, (TextBox)s);
        window.Str2_12.TextChanged += (s, e) => THeaderForm_Str2_5Change(window, (TextBox)s);
    }

    // L004C48D8
    private static void FormCreate(HeaderBox HeaderForm)
    {
        AlliedVariables.s_V0x00543C9A = 0;
        AlliedVariables.s_V0x00535E9C = 0x01;

        HeaderForm.MotherBox2.SetItems(HeaderForm.MotherBox1.Items);

        if (Application_GetPixelsPerInch() == 0x78)
        {
            Grids_TCustomGrid_SetDefaultRowHeight(HeaderForm.RegionNameGrid, AlliedPixelsScaleMul(0x12));
            Grids_TCustomGrid_SetDefaultColWidth(HeaderForm.RegionNameGrid, AlliedPixelsScaleMul(0x14B));
            Grids_TCustomGrid_SetDefaultRowHeight(HeaderForm.HeaderStringGrid, AlliedPixelsScaleMul(0x12));
        }

        Grids_TCustomGrid_SetColWidths(HeaderForm.RegionNameGrid, 0, 0x0F);
        Grids_TCustomGrid_SetColWidths(HeaderForm.HeaderStringGrid, 0, 0x0F);
        Grids_TCustomGrid_SetColWidths(HeaderForm.Str3Grid, 0, 0x11);
        Grids_TCustomGrid_SetColWidths(HeaderForm.Str2Grid, 0, 0x11);

        for (int ebx = 0; ebx < 4; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.RegionNameGrid, 0, ebx, (ebx + 1).ToString(CultureInfo.InvariantCulture));
        }

        for (int ebx = 0; ebx < 4; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.HeaderStringGrid, 0, ebx, (ebx + 3).ToString(CultureInfo.InvariantCulture));
        }

        for (int ebx = 0; ebx < 0x48; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.Str3Grid, 0, ebx, ebx.ToString(CultureInfo.InvariantCulture));
        }

        for (int ebx = 0; ebx < 0x10; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.Str2Grid, 0, ebx, (ebx + 1).ToString(CultureInfo.InvariantCulture));
        }

        for (int ebx = 0; ebx < 0x04; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.RegionNameGrid, 0x01, ebx, System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Regions[ebx].Name, 0x84));
        }

        for (int ebx = 0; ebx < 0x04; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.HeaderStringGrid, 0x01, ebx, System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.IffNames[ebx].Text, 0x14));
        }

        for (int ebx = 0; ebx < 0x48; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.Str3Grid, 0x01, ebx, System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Sets[ebx].Name, 0x57));
        }

        for (int ebx = 0; ebx < 0x10; ebx++)
        {
            Grids_TStringGrid_SetCells(HeaderForm.Str2Grid, 0x01, ebx, System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.GlobalCargos[ebx].Cargo, 0x40));
        }

        THeaderForm__PROC_004C52A0(HeaderForm, 0x01);

        HeaderForm.U3.IsChecked = AlliedVariables.s_TieFileHeader.Header.WinType != 0;
        HeaderForm.U6.IsChecked = AlliedVariables.s_TieFileHeader.Header.AllWayShown;
        HeaderForm.HangarBox.SelectedIndex = AlliedVariables.s_TieFileHeader.Header.MissionType;
        Controls_TControl_SetText(HeaderForm.Edit2, AlliedVariables.s_TieFileHeader.Header.IsGoalMelee.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(HeaderForm.Edit3, AlliedVariables.s_TieFileHeader.Header.TimeLimit.ToString(CultureInfo.InvariantCulture));
        HeaderForm.Edit4.IsChecked = AlliedVariables.s_TieFileHeader.Header.EndImmediately;
        HeaderForm.Edit5.SelectedIndex = AlliedVariables.s_TieFileHeader.Header.TacticalOfficer;
        HeaderForm.MotherBox1.SelectedIndex = AlliedVariables.s_TieFileHeader.Header.BriefingLogo;
        Controls_TControl_SetText(HeaderForm.Edit7, AlliedVariables.s_TieFileHeader.Header.m0023AC.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(HeaderForm.Edit8, AlliedVariables.s_TieFileHeader.Header.BriefingCodeSizeType.ToString(CultureInfo.InvariantCulture));
        HeaderForm.MotherBox2.SelectedIndex = AlliedVariables.s_TieFileHeader.Header.m0023AE;
        Controls_TControl_SetText(HeaderForm.Edit10, AlliedVariables.s_TieFileHeader.Header.m0023AF.ToString(CultureInfo.InvariantCulture));
        HeaderForm.C1.IsChecked = AlliedVariables.s_V0x00570FF8.m000000[0] != 0;
        HeaderForm.C2.IsChecked = AlliedVariables.s_V0x00570FF8.m000000[1] != 0;
        HeaderForm.C3.IsChecked = AlliedVariables.s_V0x00570FF8.m000000[2] != 0;
        HeaderForm.C4.IsChecked = AlliedVariables.s_V0x00570FF8.m000000[3] != 0;
        Controls_TControl_SetText(HeaderForm.V1, AlliedVariables.s_V0x00571000.m000000[0].ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(HeaderForm.V2, AlliedVariables.s_V0x00571000.m000000[1].ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(HeaderForm.V3, AlliedVariables.s_V0x00571000.m000000[2].ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(HeaderForm.V4, AlliedVariables.s_V0x00571000.m000000[3].ToString(CultureInfo.InvariantCulture));

        ComCtrls_TPageControl_SetActivePage(HeaderForm.PageControl1, ComCtrls_TPageControl_GetPage(HeaderForm.PageControl1, AlliedVariables.s_V0x00543AF6));
        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L004C52A0
    private static void THeaderForm__PROC_004C52A0(HeaderBox HeaderForm, int edx0)
    {
        Controls_TControl_SetText(HeaderForm.Str2_5, AlliedVariables.s_TieFileHeader.Header.GlobalCargos[edx0 - 1].Count.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(HeaderForm.Str2_9, AlliedVariables.s_TieFileHeader.Header.GlobalCargos[edx0 - 1].CargoType.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(HeaderForm.Str2_12, AlliedVariables.s_TieFileHeader.Header.GlobalCargos[edx0 - 1].Volatility.ToString(CultureInfo.InvariantCulture));
    }

    // L004C5264
    private static void THeaderForm_RegionNameGridSetEditText(HeaderBox HeaderForm, object? edx0, int ecx0, string? A4, int A8)
    {
        Unit_00513838_Proc_005146A4();
    }

    // L004C5270
    private static void THeaderForm_HeaderStringGridSetEditText(HeaderBox HeaderForm, object? edx0, int ecx0, string? A4, int A8)
    {
        Unit_00513838_Proc_005146A4();
    }

    // L004C5284
    private static void THeaderForm_Str2GridSelectCell(HeaderBox HeaderForm, ListView? edx0, object? ecx0, object? A4, int A8)
    {
        int index = edx0!.SelectedIndex;
        AlliedVariables.s_V0x00535E9C = index + 1;

        THeaderForm__PROC_004C52A0(HeaderForm, AlliedVariables.s_V0x00535E9C);
    }

    // L004C527C
    private static void THeaderForm_Edit1Change(HeaderBox HeaderForm)
    {
        Unit_00513838_Proc_005146A4();
    }

    // L004C5354
    private static void THeaderForm_Str2_5Change(HeaderBox HeaderForm, TextBox edx0)
    {
        string ebp04 = Controls_TControl_GetText(edx0);
        byte value = (byte)StrRec_try_to_int_L0051E3BC(ebp04);
        S0xTieHeaderGlobalCargo cargo = AlliedVariables.s_TieFileHeader.Header.GlobalCargos[AlliedVariables.s_V0x00535E9C - 1];

        switch (Convert.ToInt32(edx0.Tag) - 1)
        {
            case 4:
                cargo.Count = value;
                break;

            case 8:
                cargo.CargoType = value;
                break;

            case 11:
                cargo.Volatility = value;
                break;

            default:
                throw new InvalidOperationException();
        }
    }

    // L004C4E04
    private static void THeaderForm_FormClose(HeaderBox HeaderForm)
    {
        if (HeaderForm.DialogResult == true)
        {
            Unit_00513838_Proc_005146A4();

            for (int ebx = 0; ebx < 0x04; ebx++)
            {
                string ebp88 = Grids_TStringGrid_GetCells(HeaderForm.RegionNameGrid, 0x01, ebx);
                S0xTieRegion ebp84 = new();
                Unit_00511CD0_Proc_005121FC(ebp88, ref ebp84);
                AlliedVariables.s_TieFileHeader.Header.Regions[ebx] = ebp84;
            }

            for (int ebx = 0; ebx < 0x04; ebx++)
            {
                string ebpA0 = Grids_TStringGrid_GetCells(HeaderForm.HeaderStringGrid, 0x01, ebx);
                string ebp9C = Unit_00511CD0_Proc_00511EB8(ebpA0);
                AlliedVariables.s_TieFileHeader.Header.IffNames[ebx] = new TFixedString(0x14, ebp9C);
            }

            for (int ebx = 0; ebx < 0x48; ebx++)
            {
                string ebpFC = Grids_TStringGrid_GetCells(HeaderForm.Str3Grid, 0x01, ebx);
                S0xTieHeaderSet ebpF8 = Unit_00511CD0_Proc_0051207C(ebpFC);
                AlliedVariables.s_TieFileHeader.Header.Sets[ebx] = ebpF8;
            }

            for (int ebx = 0; ebx < 0x10; ebx++)
            {
                string ebp140 = Grids_TStringGrid_GetCells(HeaderForm.Str2Grid, 0x01, ebx);
                string ebp13C = Unit_00511CD0_Proc_0051213C(ebp140);
                AlliedVariables.s_TieFileHeader.Header.GlobalCargos[ebx].Cargo = ebp13C;
            }

            Unit_00513838_Proc_00520B74();

            AlliedVariables.s_TieFileHeader.Header.WinType = HeaderForm.U3.IsChecked == true ? (byte)1 : (byte)0;
            AlliedVariables.s_TieFileHeader.Header.AllWayShown = HeaderForm.U6.IsChecked == true;
            AlliedVariables.s_TieFileHeader.Header.MissionType = (byte)HeaderForm.HangarBox.SelectedIndex;
            AlliedVariables.s_TieFileHeader.Header.IsGoalMelee = (byte)Allied_StrRec_to_int(Controls_TControl_GetText(HeaderForm.Edit2));
            AlliedVariables.s_TieFileHeader.Header.TimeLimit = (byte)Allied_StrRec_to_int(Controls_TControl_GetText(HeaderForm.Edit3));
            AlliedVariables.s_TieFileHeader.Header.EndImmediately = HeaderForm.Edit4.IsChecked == true;
            AlliedVariables.s_TieFileHeader.Header.TacticalOfficer = (byte)HeaderForm.Edit5.SelectedIndex;
            AlliedVariables.s_TieFileHeader.Header.BriefingLogo = (byte)HeaderForm.MotherBox1.SelectedIndex;
            AlliedVariables.s_TieFileHeader.Header.m0023AC = (byte)Allied_StrRec_to_int(Controls_TControl_GetText(HeaderForm.Edit7));
            AlliedVariables.s_TieFileHeader.Header.BriefingCodeSizeType = (byte)Allied_StrRec_to_int(Controls_TControl_GetText(HeaderForm.Edit8));
            AlliedVariables.s_TieFileHeader.Header.m0023AE = (byte)HeaderForm.MotherBox2.SelectedIndex;
            AlliedVariables.s_TieFileHeader.Header.m0023AF = (byte)Allied_StrRec_to_int(Controls_TControl_GetText(HeaderForm.Edit10));
            AlliedVariables.s_V0x00570FF8.m000000[0] = HeaderForm.C1.IsChecked == true ? (byte)1 : (byte)0;
            AlliedVariables.s_V0x00570FF8.m000000[1] = HeaderForm.C2.IsChecked == true ? (byte)1 : (byte)0;
            AlliedVariables.s_V0x00570FF8.m000000[2] = HeaderForm.C3.IsChecked == true ? (byte)1 : (byte)0;
            AlliedVariables.s_V0x00570FF8.m000000[3] = HeaderForm.C4.IsChecked == true ? (byte)1 : (byte)0;
            AlliedVariables.s_V0x00571000.m000000[0] = (byte)StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(HeaderForm.V1));
            AlliedVariables.s_V0x00571000.m000000[1] = (byte)StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(HeaderForm.V2));
            AlliedVariables.s_V0x00571000.m000000[2] = (byte)StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(HeaderForm.V3));
            AlliedVariables.s_V0x00571000.m000000[3] = (byte)StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(HeaderForm.V4));
        }

        AlliedVariables.s_V0x00543AF6 = (byte)HeaderForm.PageControl1.GetActivePage().TabIndex;
    }
}
