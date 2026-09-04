using AlliED.Extensions;
using System.Globalization;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class WaypointsWindowImpl
{
    public static void Register(WaypointsWindow window)
    {
        SetBindings(window);
        //FormCreate(window);

        window.Activated += (s, e) => FormCreate(window);

        window.Closing += (s, e) =>
        {
            if (MainImpl.IsMainOpened)
            {
                //e.Cancel = true;
                window.Hide();
                window.Owner.Activate();
            }
        };
    }

    private static void SetBindings(WaypointsWindow window)
    {
        window.Closed += (s, e) =>
        {
            TWPform_FormClose(window);
            TWPform_FormDestroy(window);
        };

        window.RollEdit.TextChanged += (s, e) => TWPform_RollEditChange(window, (TextBox)s);
        window.ToolButton1.Click += (s, e) => TWPform_ToolButton1Click(window, (RadioButton)s);
        window.ToolButton2.Click += (s, e) => TWPform_ToolButton1Click(window, (RadioButton)s);
        window.ToolButton3.Click += (s, e) => TWPform_ToolButton1Click(window, (RadioButton)s);
        window.ToolButton4.Click += (s, e) => TWPform_ToolButton1Click(window, (RadioButton)s);
        window.Start1Region.SelectionChanged += (s, e) => TWPform_Start1RegionChange(window, (ComboBox)s);
        window.Start2Region.SelectionChanged += (s, e) => TWPform_Start1RegionChange(window, (ComboBox)s);
        window.Start3Region.SelectionChanged += (s, e) => TWPform_Start1RegionChange(window, (ComboBox)s);
        window.Start4Region.SelectionChanged += (s, e) => TWPform_Start1RegionChange(window, (ComboBox)s);
        window.OrderWPEnabledList.ObservableItemChanged += (s, e) => TWPform_OrderWPEnabledListClick(window);
        window.WPEnabledList.ObservableItemChanged += (s, e) => TWPform_WPEnabledListClick(window);
        window.OrderWPGrid.ObservableItemChanged += (s, e) => TWPform_OrderWPGridSetEditText(
            window,
            null,
            window.OrderWPGrid.SelectedIndex % window.OrderWPGrid.ColumnCount,
            ((CollectionTextItem)s!).Text,
            window.OrderWPGrid.SelectedIndex / window.OrderWPGrid.ColumnCount);
        window.WPGrid.ObservableItemChanged += (s, e) => TWPform_WPGridSetEditText(
            window,
            null,
            window.WPGrid.SelectedIndex % window.WPGrid.ColumnCount,
            ((CollectionTextItem)s!).Text,
            window.WPGrid.SelectedIndex / window.WPGrid.ColumnCount);
        window.WPRawGrid.ObservableItemChanged += (s, e) => TWPform_WPRawGridSetEditText(
            window,
            null,
            window.WPRawGrid.SelectedIndex % window.WPRawGrid.ColumnCount,
            ((CollectionTextItem)s!).Text,
            window.WPRawGrid.SelectedIndex / window.WPRawGrid.ColumnCount);
    }

    // L0050B994
    private static void FormCreate(WaypointsWindow WPform)
    {
        AlliedVariables.s_V0x005B6D18 = 0x01;

        if (Application_GetPixelsPerInch() == 0x78)
        {
            Controls_TSizeConstraints_SetConstraints(WPform.M000074(), 0, 0x1FB);
            Controls_TSizeConstraints_SetConstraints(WPform.M000074(), 0x01, AlliedPixelsScaleMul((int)WPform.Width));
            Controls_TSizeConstraints_SetConstraints(WPform.M000074(), 0x02, (int)WPform.M000074().MaxHeight);
            Controls_TSizeConstraints_SetConstraints(WPform.M000074(), 0x03, (int)WPform.M000074().MaxWidth);
            //Controls_TControl_SetLeft(WPform.ToolOrient, AlliedPixelsScaleMul(WPform.ToolOrient.GetLeft()));
            //Controls_TControl_SetTop(WPform.ToolOrient, AlliedPixelsScaleMul(WPform.ToolOrient.GetTop()));
        }

        TWPform_PROC_0050BA2C(WPform);
    }

    // L0050C3CC
    private static void TWPform_FormClose(WaypointsWindow WPform)
    {
        Form1WindowImpl.TForm1_L0052BD20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TWPform_Instance!, AlliedVariables.s_V0x005AFC74.m000030);
        TApplication_PostMessage_B021(AlliedVariables.s_TWPform_Instance!);
    }

    // L0050C3F4
    private static void TWPform_FormDestroy(WaypointsWindow WPform)
    {
        AlliedVariables.s_V0x005B6D18 = 0;
        Form1WindowImpl.TForm1_L0052BD20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TWPform_Instance!, AlliedVariables.s_V0x005AFC74.m000030);
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.MapEditWPBtn, false);
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.WPEditor1, false);
    }

    // L0050BA2C
    public static void TWPform_PROC_0050BA2C(WaypointsWindow eax0)
    {
        S0xFGObject edi = AlliedVariables.s_V0x005AFE90;
        byte ebp01 = AlliedVariables.s_V0x00543C9A;
        AlliedVariables.s_V0x00543C9A = 0;

        TWPform__PROC_0050BE10(eax0);
        TWPform_Proc_0050BEF0(eax0);
        TWPform_Proc_0050C064(eax0);

        for (int ebx = 0; ebx < 0x04; ebx++)
        {
            short cx = edi.IsWPEnabled[ebx];
            StdCtrls_TCustomListBox_SetSelected(eax0.WPEnabledList, ebx, cx != 0);
        }

        for (int ebx = 0; ebx < 0x02; ebx++)
        {
            short cx = edi.FlightGroupStruct.m000DAE[ebx].IsWPEnabled;
            StdCtrls_TCustomListBox_SetSelected(eax0.WPEnabledList, 4 + ebx, cx != 0);
        }

        eax0.WPEnabledList.Update();

        byte al1 = edi.FlightGroupStruct.Yaw;

        if (al1 == 0x40)
        {
            ComCtrls_TToolButton_SetDown(eax0.ToolButton2, true);
        }
        else if (al1 == 0x80)
        {
            ComCtrls_TToolButton_SetDown(eax0.ToolButton3, true);
        }
        else if (al1 == 0xC0)
        {
            ComCtrls_TToolButton_SetDown(eax0.ToolButton4, true);
        }
        else
        {
            ComCtrls_TToolButton_SetDown(eax0.ToolButton1, true);
        }

        for (int ebx = 0; ebx < 8; ebx++)
        {
            short cx = edi.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[ebx].IsUsed;
            StdCtrls_TCustomListBox_SetSelected(eax0.OrderWPEnabledList, ebx, cx != 0);
        }

        eax0.OrderWPEnabledList.Update();
        TWPform__PROC_0050C440(eax0, !StdCtrls_TCustomListBox_GetSelected(eax0.OrderWPEnabledList, 0));

        eax0.Start1Region.SelectedIndex = edi.FlightGroupStruct.StartPointRegions[0];
        eax0.Start2Region.SelectedIndex = edi.FlightGroupStruct.StartPointRegions[1];
        eax0.Start3Region.SelectedIndex = edi.FlightGroupStruct.StartPointRegions[2];
        eax0.Start4Region.SelectedIndex = edi.FlightGroupStruct.StartPointRegions[3];

        int eax2 = TWPform__PROC_0050C59C(eax0, edi.FlightGroupStruct.Roll);
        Controls_TControl_SetText(eax0.RollEdit, eax2.ToString(CultureInfo.InvariantCulture));

        AlliedVariables.s_V0x00543C9A = ebp01;
    }

    // L0050BE10
    private static void TWPform__PROC_0050BE10(WaypointsWindow eax0)
    {
        for (int esi = 0; esi < 3; esi++)
        {
            for (int ebx = 0; ebx < 6; ebx++)
            {
                double ebp10;

                if (ebx == 0x04 || ebx == 0x05)
                {
                    ebp10 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.m000DAE[ebx - 0x04].Position[esi] / 160.0f;
                }
                else
                {
                    ebp10 = AlliedVariables.s_V0x005AFE90.m00147C[esi].M000000[ebx] / 160.0f;
                }

                string ebp14 = Allied_FloatToText(0x02, 0x07, 0x02, ebp10);
                Grids_TStringGrid_SetCells(eax0.WPGrid, esi, ebx, ebp14);
            }
        }
    }

    // L0050BEF0
    public static void TWPform_Proc_0050BEF0(WaypointsWindow eax0)
    {
        string ebp1C_2 = AlliedVariables.s_TieFileHeader.Header.Regions[AlliedVariables.s_CurrentRegion - 1].Name;
        string ebp1C_0 = Form1WindowImpl.TForm1_Proc_00526288(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)]);
        Controls_TControl_SetText(eax0.Label1, "At \"" + ebp1C_2 + "\" - " + ebp1C_0);

        for (int esi = 0; esi < 8; esi++)
        {
            for (int ebx = 0; ebx < 3; ebx++)
            {
                double ebp08 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[esi].Position[ebx] / 160.0f;
                string ebp1C_4 = Allied_FloatToText(0x02, 0x07, 0x02, ebp08);
                Grids_TStringGrid_SetCells(eax0.OrderWPGrid, ebx, esi, ebp1C_4);
            }
        }
    }

    // L0050C064
    private static void TWPform_Proc_0050C064(WaypointsWindow eax0)
    {
        for (int esi = 0; esi < 0x03; esi++)
        {
            for (int ebx = 0; ebx < 0x0C; ebx++)
            {
                short ax1;

                if (ebx < 4)
                {
                    ax1 = AlliedVariables.s_V0x005AFE90.m00147C[esi].M000000[ebx];
                }
                else if (ebx < 8)
                {
                    ax1 = AlliedVariables.s_V0x005AFE90.m00147C[esi].M000008[ebx - 4];
                }
                else
                {
                    ax1 = AlliedVariables.s_V0x005AFE90.m00147C[esi].M000010[ebx - 8];
                }

                Grids_TStringGrid_SetCells(eax0.WPRawGrid, esi, ebx, ax1.ToString(CultureInfo.InvariantCulture));
            }
        }
    }

    // L0050C440
    private static void TWPform__PROC_0050C440(WaypointsWindow eax0, bool edx0)
    {
        eax0.ToolButton1.IsEnabled = edx0;
        eax0.ToolButton2.IsEnabled = edx0;
        eax0.ToolButton3.IsEnabled = edx0;
        eax0.ToolButton4.IsEnabled = edx0;
        eax0.ToolOrient.Update();
    }

    // L0050C59C
    private static int TWPform__PROC_0050C59C(WaypointsWindow eax0, byte edx0)
    {
        if (edx0 == 0)
        {
            return 0;
        }

        return (int)Math.Round((0x100 - edx0) * 1.40625f);
    }

    // L0050C5C8
    private static int TWPform_Proc_0050C5C8(WaypointsWindow eax0, byte edx0)
    {
        return (int)Math.Round(256.0f - edx0 / 1.40625f);
    }

    // L0050C4B4
    private static void TWPform_RollEditChange(WaypointsWindow WPform, TextBox edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        string ebp08 = Controls_TControl_GetText(edx0);
        int ebp04 = TWPform_Proc_0050C5C8(WPform, (byte)StrRec_try_to_int_L0051E3BC(ebp08));

        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                if (Convert.ToInt32(edx0.Tag) == 0x08)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Roll = (byte)ebp04;
                }
            }

            Unit_00513838_Proc_00520528(ebx);
        }

        Unit_00513838_Proc_0051467C();
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L0050C48C
    private static void TWPform_ToolButton1Click(WaypointsWindow WPform, RadioButton Button)
    {
        Unit_00513838_Proc_0051E614(0x18, Convert.ToInt32(Button.Tag));
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L0050BD04
    private static void TWPform_Start1RegionChange(WaypointsWindow WPform, ComboBox edx0)
    {
        Unit_00513838_Proc_0051DBC8(Convert.ToInt32(edx0.Tag), edx0.SelectedIndex);
    }

    // L0050BD24
    private static void TWPform_OrderWPEnabledListClick(WaypointsWindow WPform)
    {
        Unit_00513838_Proc_0051467C();

        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                int ebp = WPform.OrderWPEnabledList.SelectedIndex;

                //short dx = StdCtrls_TCustomListBox_GetSelected(WPform.OrderWPEnabledList, ebp) & 0x7F;
                short dx = StdCtrls_TCustomListBox_GetSelected(WPform.OrderWPEnabledList, ebp) ? (short)1 : (short)0;
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[ebx].IsUsed = dx;

                Unit_00513838_Proc_0051D53C(ebx);
                MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
                Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
            }
        }

        TWPform__PROC_0050C440(WPform, !StdCtrls_TCustomListBox_GetSelected(WPform.OrderWPEnabledList, 0));
    }

    // L0050BC1C
    private static void TWPform_WPEnabledListClick(WaypointsWindow WPform)
    {
        AlliedVariables.s_V0x00543B54 = 0x01;

        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        Unit_00513838_Proc_0051467C();

        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                continue;
            }

            int edi = WPform.WPEnabledList.SelectedIndex;

            if (edi < 0x04)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                //eax1.IsWPEnabled[edi] = (short)(StdCtrls_TCustomListBox_GetSelected(WPform.WPEnabledList, edi) & 0x7F);
                eax1.IsWPEnabled[edi] = StdCtrls_TCustomListBox_GetSelected(WPform.WPEnabledList, edi) ? (short)1 : (short)0;
            }
            else
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                //eax1.FlightGroupStruct.m000DAE[edi - 4].IsWPEnabled = (short)(StdCtrls_TCustomListBox_GetSelected(WPform.WPEnabledList, edi) & 0x7F);
                eax1.FlightGroupStruct.m000DAE[edi - 4].IsWPEnabled = StdCtrls_TCustomListBox_GetSelected(WPform.WPEnabledList, edi) ? (short)1 : (short)0;
            }

            Unit_00513838_Proc_0051D53C(ebx);
            Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
        }
    }

    // L0050C2D8
    private static void TWPform_OrderWPGridSetEditText(WaypointsWindow WPform, object? edx0, int column, string A4, int row)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        Unit_00513838_Proc_0051467C();

        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                continue;
            }

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[row].Position[column] = (short)Unit_00513838_Proc_0051443C(A4);

            Unit_00513838_Proc_0051D53C(ebx);
        }

        TWPform_Proc_0050C064(WPform);
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L0050C1C0
    private static void TWPform_WPGridSetEditText(WaypointsWindow WPform, object? edx0, int column, string A4, int row)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        Unit_00513838_Proc_0051467C();

        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                continue;
            }

            if (row < 0x04)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.m00147C[column].M000000[row] = (short)Unit_00513838_Proc_0051443C(A4);

                if (row == 0)
                {
                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax2.FlightGroupStruct.StartPoints[0].Position[column] = (short)Unit_00513838_Proc_0051443C(A4);
                }
            }
            else
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.m000DAE[row - 4].Position[column] = (short)Unit_00513838_Proc_0051443C(A4);
            }

            Unit_00513838_Proc_0051D53C(ebx);
        }

        TWPform_Proc_0050C064(WPform);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L0050C0F4
    private static void TWPform_WPRawGridSetEditText(WaypointsWindow WPform, object? edx0, int ecx0, string A4, int A8)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        Unit_00513838_Proc_0051467C();

        if (string.IsNullOrEmpty(A4))
        {
            return;
        }

        if (string.Equals(A4, "-", StringComparison.Ordinal))
        {
            return;
        }

        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                continue;
            }

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

            if (int.TryParse(A4, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v))
            {
                eax1.m00147C[ecx0].M000000[A8] = (short)v;
            }
            else
            {
                eax1.m00147C[ecx0].M000000[A8] = 0;
            }

            Unit_00513838_Proc_0051D53C(ebx);
        }

        TWPform__PROC_0050BE10(WPform);
    }
}
