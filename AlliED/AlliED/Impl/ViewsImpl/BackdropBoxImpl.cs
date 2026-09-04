using AlliED.Extensions;
using AlliED.Helpers;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlliED.Impl.ViewsImpl;

internal static class BackdropBoxImpl
{
    public static void Register(BackdropBox window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(BackdropBox window)
    {
        window.Image1.MouseDown += (s, e) =>
        {
            if (e.ClickCount != 1)
            {
                return;
            }
            Point position = e.GetPosition((Image)s);
            TBDropForm_Image1MouseDown(window, s, (int)position.Y, (int)position.X, e.ToTShiftState());
        };
        window.ToolBar1.SelectionChanged += (s, e) => TBDropForm_ToolButton1Click(window, (ListBox)s);
        window.ColorPanel.SelectedColorChanged += (s, e) => TBDropForm_ColorPanelClick(window);
        window.Button2.Click += (s, e) => TBDropForm_Button2Click(window);
    }

    // L0050D608
    private static void FormCreate(BackdropBox BDropForm)
    {
        byte index = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.GlobalCargoIndex;
        BDropForm.ToolBar1.SelectedIndex = (index >= 0 && index <= 6) ? index : 0;

        AlliedVariables.s_Allied_BackdropShadowIndex = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.GlobalCargoIndex;
        Controls_TControl_SetColor(BDropForm.ColorPanel, TBDropForm_L0050D78C(BDropForm, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Name.WithMaxLength(0x14)));
        BDropForm.ColorPanel.SelectedColor = ColorHelpers.FromUInt32(TBDropForm_L0050D78C(BDropForm, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Name.WithMaxLength(0x14)));
        AlliedVariables.s_Allied_BackdropPlanetIndex = AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SelectedIndex;
        Controls_TControl_SetText(BDropForm.Label2, "#" + AlliedVariables.s_Allied_BackdropPlanetIndex.ToString(CultureInfo.InvariantCulture));
    }

    // L0050D78C
    private static uint TBDropForm_L0050D78C(BackdropBox BDropForm, string edx0)
    {
        string ebp14_4 = edx0;

        int ebp1C = ebp14_4.Length;
        int ebp18 = 1;
        string ebp14_2 = "0";
        string ebp14_1 = "0";
        string ebp14_0 = "0";

        if (ebp1C > 0 && !string.Equals(ebp14_4, "Unnamed", StringComparison.Ordinal))
        {
            try
            {
                string ebp14_3 = string.Empty;

                while (true)
                {
                    ebp14_3 += ebp14_4[ebp18 - 1];
                    ebp18++;

                    if (ebp18 - 1 >= ebp1C || ebp14_4[ebp18 - 1] == ' ')
                        break;
                }

                ebp18++;

                if (!string.IsNullOrEmpty(ebp14_3))
                {
                    ebp14_2 = ebp14_3;
                }
            }
            catch
            {
                ebp14_2 = "0";
            }

            try
            {
                string ebp14_3 = string.Empty;

                while (true)
                {
                    ebp14_3 += ebp14_4[ebp18 - 1];
                    ebp18++;

                    if (ebp18 - 1 >= ebp1C || ebp14_4[ebp18 - 1] == ' ')
                        break;
                }

                ebp18++;

                if (!string.IsNullOrEmpty(ebp14_3))
                {
                    ebp14_1 = ebp14_3;
                }
            }
            catch
            {
                ebp14_1 = "0";
            }

            try
            {
                string ebp14_3 = string.Empty;

                while (true)
                {
                    ebp14_3 += ebp14_4[ebp18 - 1];
                    ebp18++;

                    if (ebp18 - 1 >= ebp1C || ebp14_4[ebp18 - 1] == ' ')
                        break;
                }

                if (!string.IsNullOrEmpty(ebp14_3))
                {
                    ebp14_0 = ebp14_3;
                }
            }
            catch
            {
                ebp14_0 = "0";
            }

        }

        uint ebx;

        try
        {
            uint bl0 = (byte)Math.Round(float.Parse(ebp14_2, CultureInfo.InvariantCulture) * 255.0f);
            uint ebp1D = (byte)Math.Round(float.Parse(ebp14_1, CultureInfo.InvariantCulture) * 255.0f);
            uint al0 = (byte)Math.Round(float.Parse(ebp14_0, CultureInfo.InvariantCulture) * 255.0f);

            ebx = bl0 | (ebp1D << 8) | (al0 << 16);
        }
        catch
        {
            ebx = 0;
        }

        return ebx;
    }

    // L0050D3F8
    private static void TBDropForm_Image1MouseDown(BackdropBox BDropForm, object? Sender, int A4, int A8, TShiftState AC)
    {
        AlliedVariables.s_Allied_BackdropPlanetIndex = (A8 / 0x33 + 1) + (A4 / 0x33) * 0x0C;
        Controls_TControl_SetText(BDropForm.Label2, "#" + AlliedVariables.s_Allied_BackdropPlanetIndex.ToString(CultureInfo.InvariantCulture));
    }

    // L0050D780
    private static void TBDropForm_ToolButton1Click(BackdropBox BDropForm, ListBox Sender)
    {
        AlliedVariables.s_Allied_BackdropShadowIndex = Sender.SelectedIndex;
    }

    // L0050DA64
    private static void TBDropForm_ColorPanelClick(BackdropBox BDropForm)
    {
        try
        {
            Controls_TControl_SetColor(BDropForm.ColorPanel, BDropForm.ColorPanel.SelectedColor);
        }
        catch
        {
            MessageBox_ShowWarning("Color selection dialog failed to load.");
        }
    }

    // L0050D49C
    private static void TBDropForm_Button2Click(BackdropBox BDropForm)
    {
        AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SelectedIndex = AlliedVariables.s_Allied_BackdropPlanetIndex;
        ShipExtUserControlImpl.TShipExt_SpecShpSpinChange(AlliedVariables.s_TShipExt_Instance!, BDropForm.Button2);

        int count = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < count; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                continue;
            }

            S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            eax0.FlightGroupStruct.GlobalCargoIndex = (byte)AlliedVariables.s_Allied_BackdropShadowIndex;

            string ebp20_1 = TBDropForm_L0050DB00(BDropForm, BDropForm.ColorPanel.SelectedColor);
            string ebp20_2 = TBDropForm_L0050DC10(BDropForm, ebp20_1);
            string ebp14 = Unit_00511CD0_Proc_00511EB8(ebp20_2);

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            eax1.FlightGroupStruct.Name = ebp14.WithMaxLength(0x14);
        }

        Unit_00513838_Proc_0051467C();
        AlliedVariables.s_V0x00543C9A = 0;
        string ebp20_0 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Name.WithMaxLength(0x14);
        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.ShipName, ebp20_0);
        DatapadWindowImpl.TDatapad_SetTitle(AlliedVariables.s_TDatapad_Instance!);
        AlliedVariables.s_V0x00543C9A = 0x01;
        Unit_00513838_Proc_00517258();
    }

    // L0050DB00
    private static string TBDropForm_L0050DB00(BackdropBox BDropForm, Color? edx0)
    {
        uint colorHex = ColorHelpers.FromColor(edx0);
        string ebp14_2 = Allied_UIntToHexString(colorHex & 0xff, 0x02);
        string ebp14_1 = Allied_UIntToHexString((colorHex >> 8) & 0xff, 0x02);
        string ebp14_0 = Allied_UIntToHexString((colorHex >> 16) & 0xff, 0x02);
        string ebp14_3 = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", ebp14_2, ebp14_1, ebp14_0);
        return ebp14_3;
    }

    // L0050DC10
    private static string TBDropForm_L0050DC10(BackdropBox BDropForm, string edx0)
    {
        string ebp14_2 = edx0.Substring(0, 2);
        string ebp14_1 = edx0.Substring(3, 2);
        string ebp14_0 = edx0.Substring(6, 2);

        byte ebx = string.IsNullOrEmpty(ebp14_2) ? (byte)0 : (byte)((TBDropForm_HexCharToInt(BDropForm, ebp14_2[0]) << 4) + TBDropForm_HexCharToInt(BDropForm, ebp14_2[1]));
        byte ebp15 = string.IsNullOrEmpty(ebp14_1) ? (byte)0 : (byte)((TBDropForm_HexCharToInt(BDropForm, ebp14_1[0]) << 4) + TBDropForm_HexCharToInt(BDropForm, ebp14_1[1]));
        byte ebp16 = string.IsNullOrEmpty(ebp14_0) ? (byte)0 : (byte)((TBDropForm_HexCharToInt(BDropForm, ebp14_0[0]) << 4) + TBDropForm_HexCharToInt(BDropForm, ebp14_0[1]));

        ebp14_2 = Allied_FloatToText(0, 0x02, 0x03, ebx / 255.0f);
        ebp14_1 = Allied_FloatToText(0, 0x02, 0x03, ebp15 / 255.0f);
        ebp14_0 = Allied_FloatToText(0, 0x02, 0x03, ebp16 / 255.0f);

        if (string.Equals(ebp14_2, "1", StringComparison.Ordinal))
        {
            ebp14_2 = "1.0";
        }

        if (string.Equals(ebp14_1, "1", StringComparison.Ordinal))
        {
            ebp14_1 = "1.0";
        }

        if (string.Equals(ebp14_0, "1", StringComparison.Ordinal))
        {
            ebp14_0 = "1.0";
        }

        if (string.Equals(ebp14_2, "0", StringComparison.Ordinal))
        {
            ebp14_2 = "0.0";
        }

        if (string.Equals(ebp14_1, "0", StringComparison.Ordinal))
        {
            ebp14_1 = "0.0";
        }

        if (string.Equals(ebp14_0, "0", StringComparison.Ordinal))
        {
            ebp14_0 = "0.0";
        }

        string ebp14_3 = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", ebp14_2, ebp14_1, ebp14_0);
        return ebp14_3;
    }

    // L0050DECC
    private static int TBDropForm_HexCharToInt(BackdropBox BDropForm, char A8)
    {
        int ebx = 0;

        if (A8 >= '0' && A8 <= '9')
        {
            ebx = A8 - '0';
        }
        else if (A8 >= 'A' && A8 <= 'F')
        {
            ebx = 0x0A + (A8 - 'A');
        }

        return ebx;
    }
}
