using AlliED.Extensions;
using AlliED.Helpers;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlliED.Impl.ViewsImpl;

internal static class MapWindowImpl
{
    public static void Register(MapWindow window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(MapWindow window)
    {
        window.Activated += (s, e) => TMapForm_FormActivate(window, null);
        window.Closed += (s, e) => TMapForm_FormClose(window, 0, out _);

        window.KeyDown += (s, e) => TMapForm_FormKeyDown(window, s, e.Key, Keyboard.Modifiers);
        window.Timer1Event += (s, e) => TMapForm_Timer1Timer(window);
    }

    // L004F6148
    public static void FormCreate(MapWindow MapForm)
    {
        if (AlliedVariables.s_AlliedAplicationWidth == 0x280 || AlliedVariables.s_AlliedAplicationHeight == 0x1E0)
        {
            AlliedVariables.s_AlliedForm1Window!.SelectionBox.SetMaxDropDownCount(0x1E);
        }

        AlliedVariables.s_V0x005AFCBC = new TBitmap((int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualWidth, (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight);
        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!;
        eax1.FontFace = "MS Sans Serif";
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!;
        eax1.FontSize = 0x08;

        AlliedVariables.s_V0x005AFCC0 = new TBitmap((int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualWidth, (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight);
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC0)!;
        eax1.FontFace = "MS Sans Serif"; ;
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC0)!;
        eax1.FontSize = 0x08;

        AlliedVariables.s_V0x005AFCC4 = new TBitmap(0x24F, 0x28);

        AlliedVariables.s_V0x005B7042 = 0x01;
        AlliedVariables.s_V0x005B7004 = 0x01;
        AlliedVariables.s_V0x005B7005 = 0;
        AlliedVariables.s_V0x005B7043 = 0x01;
        AlliedVariables.s_V0x005B7045 = 0x01;
        AlliedVariables.s_V0x00543B52 = 0;
        AlliedVariables.s_V0x005B7046 = false;
        AlliedVariables.s_V0x005B7047 = true;

        TMapForm_FormActivate(AlliedVariables.s_TMapForm_Instance!, AlliedVariables.s_TMapForm_Instance!);
        TMapForm_Proc_004F672C(MapForm);
    }

    // L004F6B48
    public static void TMapForm_Proc_004F6B48(MapWindow eax0)
    {
        int edi = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int esi = 0; esi < edi; esi++)
        {
            bool ebp01 = false;
            bool ebp02 = false;
            bool ebp03 = false;
            bool ebp04 = false;

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
            CraftIdEnum bl = eax1.FlightGroupStruct.CraftId;

            if (AlliedVariables.s_AlliedForm1Window!.EasyBtn.IsChecked == true)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

                if (BtBitString((int)eax1.FlightGroupStruct.ArrivalDifficulty, AlliedVariables.s_V0x00533CC0))
                {
                    ebp04 = true;
                }
            }
            else if (AlliedVariables.s_AlliedForm1Window!.MedBtn.IsChecked == true)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

                if (BtBitString((int)eax1.FlightGroupStruct.ArrivalDifficulty, AlliedVariables.s_V0x00533CE0))
                {
                    ebp04 = true;
                }
            }
            else if (AlliedVariables.s_AlliedForm1Window!.HardBtn.IsChecked == true)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

                if (BtBitString((int)eax1.FlightGroupStruct.ArrivalDifficulty, AlliedVariables.s_V0x00533D00))
                {
                    ebp04 = true;
                }
            }
            else
            {
                ebp04 = true;
            }

            if (AlliedVariables.s_AlliedForm1Window!.ShowStartBtn.IsChecked == false)
            {
                ebp03 = true;
            }
            else
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

                if (eax1.m001443)
                {
                    ebp03 = true;
                }
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

            switch (eax1.FlightGroupStruct.Iff)
            {
                case 0x00:
                    ebp01 = AlliedVariables.s_AlliedForm1Window!.RebIFF.IsChecked == true;
                    break;

                case 0x01:
                    ebp01 = AlliedVariables.s_AlliedForm1Window!.ImpIFF.IsChecked == true;
                    break;

                case 0x02:
                    ebp01 = AlliedVariables.s_AlliedForm1Window!.BluIFF.IsChecked == true;
                    break;

                case 0x03:
                    ebp01 = AlliedVariables.s_AlliedForm1Window!.YellIFF.IsChecked == true;
                    break;

                case 0x04:
                    ebp01 = AlliedVariables.s_AlliedForm1Window!.Red2IFF.IsChecked == true;
                    break;

                case 0x05:
                    ebp01 = AlliedVariables.s_AlliedForm1Window!.PurpIFF.IsChecked == true;
                    break;
            }

            if (BtBitString((int)bl, AlliedVariables.s_V0x00533C40))
            {
                ebp02 = AlliedVariables.s_AlliedForm1Window!.CapShipsOn.IsChecked == true;
            }
            else if (BtBitString((int)bl, AlliedVariables.s_V0x00533BC0))
            {
                ebp02 = AlliedVariables.s_AlliedForm1Window!.FightersOn.IsChecked == true;
            }
            else if (BtBitString((int)bl, AlliedVariables.s_V0x00533BE0) || BtBitString((int)bl, AlliedVariables.s_V0x00533C00))
            {
                ebp02 = AlliedVariables.s_AlliedForm1Window!.TransportsOn.IsChecked == true;
            }
            else if (BtBitString((int)bl, AlliedVariables.s_V0x00533C60))
            {
                ebp02 = AlliedVariables.s_AlliedForm1Window!.PlatformsOn.IsChecked == true;
            }
            else if (BtBitString((int)bl, AlliedVariables.s_V0x00533C80))
            {
                ebp02 = AlliedVariables.s_AlliedForm1Window!.ObjectsOn.IsChecked == true;
            }
            else if (BtBitString((int)bl, AlliedVariables.s_V0x00533C20))
            {
                ebp02 = AlliedVariables.s_AlliedForm1Window!.FRTsOn.IsChecked == true;
            }
            else if (BtBitString((int)bl, AlliedVariables.s_V0x00533CA0))
            {
                ebp02 = true;
            }

            if (bl >= CraftIdEnum._184__1_0 && bl <= CraftIdEnum._227__1_0)
            {
                ebp02 = true;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

            byte al3 = eax1.m001444[AlliedVariables.s_CurrentRegion - 1];

            if (ebp01 && ebp02 && ebp03 && ebp04 && al3 != 0)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.m00147A = 0x01;
            }
            else
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.m00147A = 0;
            }
        }

        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex = AlliedVariables.s_V0x005B7050;

        if (AlliedVariables.s_V0x005B704C != 0)
        {
            Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
        }
    }

    // L004F6324
    public static void TMapForm_FormActivate(MapWindow MapForm, object? edx0)
    {
        AlliedVariables.s_V0x00543C9A = 0;
        Controls_TControl_SetText(AlliedVariables.s_TMapForm_Instance!, "Flight Group Map  - (" + Path.GetFileName(AlliedVariables.s_V0x00543BF8) + ")");

        if (AlliedVariables.s_V0x005B6D13 == 0)
        {
            MapForm.Close();
        }
        else if (AlliedVariables.s_V0x005B6D14 == 0)
        {
            Controls_TControl_SetTop(MapForm, (int)MapForm.Top - 0x0F);
            TMapForm_Proc_004F672C(MapForm);

            AlliedVariables.s_V0x005B6D14 = 0x01;
            AlliedVariables.s_V0x005B7044 = 0x01;
            AlliedVariables.s_V0x005B7007 = true;
        }

        if (AlliedVariables.s_V0x005B7045 != 0)
        {
            switch (AlliedVariables.s_WPsDefaultRadioIndexSetting)
            {
                case WPsDefaultRadioEnum.AllWPs:
                    ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.AllWPS, true);
                    Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                    break;

                case WPsDefaultRadioEnum.CurrentWPs:
                    ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.CurrWPs, true);
                    break;
            }

            if (AlliedVariables.s_NamesOnChkSetting)
            {
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.NamesOn, true);
            }

            if (AlliedVariables.s_DefaultPalletOnChkSetting)
            {
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.PaletOn, true);
            }

            if (AlliedVariables.s_DefaultFGListMapSetting)
            {
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.ShowFGList, true);
                Form1WindowImpl.TForm1_ShowFGListClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.ShowFGList);
            }

            if (!AlliedVariables.s_DefaultHypOnChkSetting)
            {
                Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.LinkHyppoint1, false);
            }

            AlliedVariables.s_V0x005B7045 = 0;
        }

        if (AlliedVariables.s_DarkGridChkSetting)
        {
            AlliedVariables.s_V0x005B6FF0 = 0x00393B7D;
            AlliedVariables.s_V0x005B6FF4 = 0x00292B4D;
            AlliedVariables.s_V0x005B6FF8 = 0x00181B2D;
        }
        else
        {
            AlliedVariables.s_V0x005B6FF0 = 0x000000FF;
            AlliedVariables.s_V0x005B6FF4 = 0x00000095;
            AlliedVariables.s_V0x005B6FF8 = 0x00000057;
        }

        switch (AlliedVariables.s_AutoCtrGrpIndexSetting)
        {
            case AutoCtrGrpEnum.LockCtr:
                Buttons_TSpeedButton_SetDown(AlliedVariables.s_AlliedForm1Window!.LockCtr, true);
                break;

            case AutoCtrGrpEnum.LockCurr:
                Buttons_TSpeedButton_SetDown(AlliedVariables.s_AlliedForm1Window!.LockCurr, true);
                break;

            case AutoCtrGrpEnum.LockZero:
                Buttons_TSpeedButton_SetDown(AlliedVariables.s_AlliedForm1Window!.LockZero, true);
                break;

            case AutoCtrGrpEnum.LockBattleCtr:
                Buttons_TSpeedButton_SetDown(AlliedVariables.s_AlliedForm1Window!.LockBattleCtr, true);
                break;
        }

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L004F672C
    private static void TMapForm_Proc_004F672C(MapWindow MapForm)
    {
        AlliedVariables.s_V0x005B6D68 = (int)AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualWidth / 2;
        AlliedVariables.s_V0x005B6D6C = (int)AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualHeight / 2;
        AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        AlliedVariables.s_V0x005B7044 = 0x01;
        AlliedVariables.s_V0x00543D4C = 0x20;
        AlliedVariables.s_V0x005B7058 = 0x01;
        AlliedVariables.s_V0x005B705C = 0;
        AlliedVariables.s_V0x005B706C = 0x01;
        AlliedVariables.s_V0x005B7070 = 0x01;
        AlliedVariables.s_V0x005B7064 = 0;
        AlliedVariables.s_V0x005B7020 = 0;
        AlliedVariables.s_V0x005B7028 = 0;
        AlliedVariables.s_V0x005B7030 = 0;
        AlliedVariables.s_V0x005B7038 = 0;
        AlliedVariables.s_V0x00543D44 = 0x01;
        AlliedVariables.s_V0x00543D48 = 0x01;
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SetItems(AlliedVariables.s_V0x00543BC0);
        AlliedVariables.s_V0x005B7050 = 0;
        AlliedVariables.s_V0x005B7054 = 0;
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SetItems(AlliedVariables.s_V0x00543BC0);
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex = 0;
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex = 0;
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.CurrOnly, false);
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.XY, true);
        Form1WindowImpl.TForm1_FitBattleBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.FitBattleBtn);
        AlliedVariables.s_V0x005B7078 = Unit_00513838_Proc_0051C1B4() & 0xFF;
        TMapForm__PROC_004F7AB0(MapForm, (byte)AlliedVariables.s_V0x005B7078);
    }

    // L004F8A94
    public static void TMapForm_Proc_004F8A94(MapWindow? eax0)
    {
        if (AlliedVariables.s_V0x005B6D14 == 0)
        {
            return;
        }

        double width = AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualWidth;
        double height = AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualHeight;

        AlliedVariables.s_V0x005AFCBC!.Resize((int)width, (int)height);
        AlliedVariables.s_V0x005AFCC0!.Resize((int)width, (int)height);
        AlliedVariables.s_V0x005B6D68 = (int)width / 2;
        AlliedVariables.s_V0x005B6D6C = (int)height / 2;
        AlliedVariables.s_V0x005B7010 = width / 780.0f;
        AlliedVariables.s_V0x005B7018 = height / 560.0f;
    }

    // L004F68E8
    public static void TMapForm__PROC_004F68E8(MapWindow eax0, int edx0, int ecx0)
    {
        AlliedVariables.s_V0x005B7020 = 0.0f - TMapForm_Proc_004F65C8(eax0, 0, (double)edx0);
        AlliedVariables.s_V0x005B7028 = TMapForm_Proc_004F65C8(eax0, 0x01, (double)ecx0);
        AlliedVariables.s_V0x005B7030 = 0.0f - TMapForm_Proc_004F6640(eax0, 0, (double)AlliedVariables.s_V0x005B6D68);
        AlliedVariables.s_V0x005B7038 = 0.0f - TMapForm_Proc_004F6640(eax0, 0x01, (double)AlliedVariables.s_V0x005B6D6C);

        TMapForm__PROC_004F6A80(eax0);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004F65C8
    public static double TMapForm_Proc_004F65C8(MapWindow eax0, int edx0, double A4)
    {
        double esp08;

        if (edx0 == 0)
        {
            esp08 = AlliedVariables.s_V0x00543D4C / 160.0f * A4;
        }
        else if (AlliedVariables.s_V0x005B705C == 0 && AlliedVariables.s_V0x005B7058 == 0x01)
        {
            esp08 = AlliedVariables.s_V0x00543D4C / 160.0f * A4;
        }
        else
        {
            esp08 = 0.0f - AlliedVariables.s_V0x00543D4C / 160.0f * A4;
        }

        return esp08;
    }

    // L004F6640
    public static double TMapForm_Proc_004F6640(MapWindow eax0, int edx0, double A4)
    {
        double ebp08;

        if (edx0 == 0)
        {
            ebp08 = ((A4 - AlliedVariables.s_V0x005B6D68) - AlliedVariables.s_V0x005B7020) / AlliedVariables.s_V0x00543D4C * 160.0f;
        }
        else
        {
            if (AlliedVariables.s_V0x005B705C == 0 && AlliedVariables.s_V0x005B7058 == 0x01)
            {
                ebp08 = ((A4 - AlliedVariables.s_V0x005B6D6C) - AlliedVariables.s_V0x005B7028) / AlliedVariables.s_V0x00543D4C * 160.0f;
            }
            else
            {
                ebp08 = 0.0f - ((A4 - AlliedVariables.s_V0x005B6D6C) - AlliedVariables.s_V0x005B7028) / AlliedVariables.s_V0x00543D4C * 160.0f;
            }
        }

        return ebp08;
    }

    // L004F6A80
    public static void TMapForm__PROC_004F6A80(MapWindow eax0)
    {
        AlliedVariables.s_V0x005B7042 = 0;

        int edx1 = (int)Math.Round(0.0f - AlliedVariables.s_V0x005B7020);
        StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.XScroll, edx1);

        int edx2 = (int)Math.Round(0.0f - AlliedVariables.s_V0x005B7028);
        StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.YScroll, edx2);

        AlliedVariables.s_V0x005B7042 = 0x01;
    }

    // L0051C1B4
    private static uint Unit_00513838_Proc_0051C1B4()
    {
        uint edi = 0;
        int esi = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

            if (eax.FlightGroupStruct.PlayerNumber == 0x01)
            {
                eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                edi = eax.FlightGroupStruct.Iff;
            }
        }

        if (edi == 0x32)
        {
            edi = 0;
        }

        return edi;
    }

    // L004F7AB0
    private static void TMapForm__PROC_004F7AB0(MapWindow MapForm, byte edx0)
    {
        AlliedVariables.s_V0x005AFCC4!.RenderOpen();

        short bp = edx0;

        if (bp == 0x04)
        {
            bp = 0x01;
        }
        else if (bp == 0x05)
        {
            bp = 0x04;
        }

        bp = (short)(bp * 0x14);

        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC4!)!;
        eax1.Color = 0x00CC0020;

        for (short ebx = 0, edi = 0; ebx < 0x29; ebx++)
        {
            switch (ebx)
            {
                case 0x09:
                case 0x0A:
                case 0x1E:
                case 0x26:
                    continue;
            }

            short si0 = (short)(ebx * 0x13);
            TRect esp04 = new(si0, bp, si0 + 0x10, bp + 0x14);
            TRect esp14 = new(edi * 0x10 - 0x01, 0, edi * 0x10 + 0x0F, 0x14);

            Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC4!)!, esp14, ExtCtrls_TImage_GetCanvas(AlliedVariables.s_AlliedForm1Window!.Icons)!, esp04);
            edi++;
        }

        short esp00 = 0x29;

        for (short ebx = 0, edi = 0; ebx < 0x27; ebx++)
        {
            short si0 = (short)(esp00 * 0x13);
            TRect esp04 = new(si0, bp, si0 + 0x10, bp + 0x14);
            TRect esp14 = new(edi * 0x10 - 0x01, 0x14, edi * 0x10 + 0x0F, 0x28);
            Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC4!)!, esp14, ExtCtrls_TImage_GetCanvas(AlliedVariables.s_AlliedForm1Window!.Icons)!, esp04);

            edi++;

            switch (edi)
            {
                case 0x18:
                case 0x1F:
                case 0x20:
                case 0x21:
                    continue;
            }

            esp00++;
        }

        {
            short si1 = 0x487;
            TRect esp04 = new(si1, bp, si1 + 0x10, bp + 0x14);
            TRect esp14 = new(0x1FF, 0x14, 0x20F, 0x28);
            Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC4!)!, esp14, ExtCtrls_TImage_GetCanvas(AlliedVariables.s_AlliedForm1Window!.Icons)!, esp04);
        }

        {
            short si2 = 0x49A;
            TRect esp04 = new(si2, bp, si2 + 0x10, bp + 0x14);
            TRect esp14 = new(0x20F, 0x14, 0x21F, 0x28);
            Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC4!)!, esp14, ExtCtrls_TImage_GetCanvas(AlliedVariables.s_AlliedForm1Window!.Icons)!, esp04);
        }

        AlliedVariables.s_V0x005AFCC4!.RenderClose();

        // todo
        //AlliedVariables.s_AlliedForm1Window!.Icons.Save("palette0.bmp");
        //File.WriteAllBytes("palette0.bin", StringImageHelpers.CreateFrameDataRaw(Form1WindowResources.Icons));
    }

    // L004F894C
    public static void TMapForm_PROC_004F894C(MapWindow eax0)
    {
        AlliedVariables.s_V0x005B6FE0 = TMapForm_Proc_004F6640(eax0, 0, 0.0) / 160.0f;
        AlliedVariables.s_V0x005B6FE8 = TMapForm_Proc_004F6640(eax0, 0, AlliedVariables.s_V0x005AFCBC!.Width) / 160.0f;

        if (AlliedVariables.s_V0x005B705C == 0 && AlliedVariables.s_V0x005B7058 == 0x01)
        {
            AlliedVariables.s_V0x005B6FD0 = TMapForm_Proc_004F6640(eax0, 0x01, 0.0) / 160.0f;
            AlliedVariables.s_V0x005B6FD8 = TMapForm_Proc_004F6640(eax0, 0x01, AlliedVariables.s_V0x005AFCBC!.Height) / 160.0f;
        }
        else
        {
            AlliedVariables.s_V0x005B6FD0 = 0.0f - TMapForm_Proc_004F6640(eax0, 0x01, 0.0) / 160.0f;
            AlliedVariables.s_V0x005B6FD8 = 0.0f - TMapForm_Proc_004F6640(eax0, 0x01, AlliedVariables.s_V0x005AFCBC!.Height) / 160.0f;
        }
    }

    // L004F7E58
    public static void TMapForm__PROC_004F7E58(MapWindow eax0, int edx0, TBitmap ecx0)
    {
        S0xFGObject ebx = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0);

        AlliedVariables.s_V0x005B6D40 = ebx.m001448;
        AlliedVariables.s_V0x005B6D48 = ebx.m001450;
        AlliedVariables.s_V0x005B6D38 = ebx.m001458;
        AlliedVariables.s_V0x005B6D30 = ebx.m001460;
        AlliedVariables.s_V0x005B6D50 = ebx.m001468;
        AlliedVariables.s_V0x005B6D58 = ebx.m001470;

        Graphics_TPen_SetColor(AlliedVariables.s_V0x00543D38!, AlliedVariables.s_V0x005B6FFC);
        Graphics_TPen_SetStyle(AlliedVariables.s_V0x00543D38!, 0);

        AlliedVariables.s_V0x005B6D20 = ebx.FlightGroupStruct.CraftId;

        // todo
        /*
        switch (AlliedVariables.s_V0x005B6D20)
        {
            case CraftIdEnum._000__1_0:
                break;

            case CraftIdEnum._001_0_0_Xwing:
                AlliedDrawCraft_004DE558_001_0_0_Xwing();
                break;

            case CraftIdEnum._002_0_1_Ywing:
                AlliedDrawCraft_004DE6F4_002_0_1_Ywing();
                break;

            case CraftIdEnum._003_0_2_Awing:
                AlliedDrawCraft_004DE914_003_0_2_Awing();
                break;

            case CraftIdEnum._004_0_3_Bwing:
                AlliedDrawCraft_004DEAC4_004_0_3_Bwing();
                break;

            case CraftIdEnum._005_0_4_TieFighter:
            case CraftIdEnum._115_0_18_TieBigGun:
            case CraftIdEnum._118_0_21_TieBooster:
                AlliedDrawCraft_004DEC8C_005_0_4_TieFighter();
                break;

            case CraftIdEnum._006_0_5_TieInterceptor:
            case CraftIdEnum._116_0_19_TieWarheads:
                AlliedDrawCraft_004DF08C_006_0_5_TieInterceptor();
                break;

            case CraftIdEnum._007_0_6_TieBomber:
                AlliedDrawCraft_004DF2C0_007_0_6_TieBomber();
                break;

            case CraftIdEnum._008_0_7_TieAdvanced:
                AlliedDrawCraft_004DF5F8_008_0_7_TieAdvanced();
                break;

            case CraftIdEnum._009_0_8_TieDefender:
                AlliedDrawCraft_004DF6C4_009_0_8_TieDefender();
                break;

            case CraftIdEnum._010_0_9_IrdFighter:
                AlliedDrawCraft_004F4150_010_0_9_IrdFighter();
                break;

            case CraftIdEnum._011_0_10_ToscanFighter:
                AlliedDrawCraft_004F01CC_011_0_10_ToscanFighter();
                break;

            case CraftIdEnum._012_0_11_MissileBoat:
                AlliedDrawCraft_004EF7C0_012_0_11_MissileBoat();
                break;

            case CraftIdEnum._013_0_12_Twing:
                AlliedDrawCraft_004DFF10_013_0_12_Twing();
                break;

            case CraftIdEnum._014_0_13_Z_95:
                AlliedDrawCraft_004DFBC4_014_0_13_Z_95();
                break;

            case CraftIdEnum._015_0_14_R41:
                AlliedDrawCraft_004DFD54_015_0_14_R41();
                break;

            case CraftIdEnum._016_0_15_AssaultGunboat:
                AlliedDrawCraft_004DF8A0_016_0_15_AssaultGunboat();
                break;

            case CraftIdEnum._017_0_37_Shuttle:
                AlliedDrawCraft_004E0580_017_0_37_Shuttle();
                break;

            case CraftIdEnum._018_0_38_EscortShuttle:
                AlliedDrawCraft_004E02EC_018_0_38_EscortShuttle();
                break;

            case CraftIdEnum._019_0_42_SystemPatrolCraft:
                AlliedDrawCraft_004D7538_019_0_42_SystemPatrolCraft();
                break;

            case CraftIdEnum._020_0_43_ScoutCraft:
                AlliedDrawCraft_004E8910_020_0_43_ScoutCraft();
                break;

            case CraftIdEnum._021_0_39_StormtrooperTransport:
                AlliedDrawCraft_004DFA84_021_0_39_StormtrooperTransport();
                break;

            case CraftIdEnum._022_0_40_AssaultTransport:
                AlliedDrawCraft_004E0998_022_0_40_AssaultTransport();
                break;

            case CraftIdEnum._023_0_41_EscortTransport:
                AlliedDrawCraft_004E0B74_023_0_41_EscortTransport();
                break;

            case CraftIdEnum._024_0_32_Tug:
                AlliedDrawCraft_004E08BC_024_0_32_Tug();
                break;

            case CraftIdEnum._025_0_33_CombatUtilityVehicle:
                AlliedDrawCraft_004E1488_025_0_33_CombatUtilityVehicle();
                break;

            case CraftIdEnum._026_0_53_ContainerBrick:
                AlliedDrawCraft_004DB474_026_0_53_ContainerBrick();
                break;

            case CraftIdEnum._027_0_54_ContainerHexBox:
                AlliedDrawCraft_004DB4C4_027_0_54_ContainerHexBox();
                break;

            case CraftIdEnum._028_0_55_ContainerTube:
                AlliedDrawCraft_004DB7D4_028_0_55_ContainerTube();
                break;

            case CraftIdEnum._029_0_56_ContainerPronged:
                AlliedDrawCraft_004DBB3C_029_0_56_ContainerPronged();
                break;

            case CraftIdEnum._030_0_34_HeavyLifter:
                AlliedDrawCraft_004DE208_030_0_34_HeavyLifter();
                break;

            case CraftIdEnum._031_0_35_MoleMiner:
                AlliedDrawCraft_004F5A28_031_0_35_MoleMiner();
                break;

            case CraftIdEnum._032_0_68_BulkFreighter:
                AlliedDrawCraft_004DB3E4_032_0_68_BulkFreighter();
                break;

            case CraftIdEnum._033_0_69_CargoFerry:
                AlliedDrawCraft_004DE3FC_033_0_69_CargoFerry();
                break;

            case CraftIdEnum._034_0_70_ModularConveyor:
                AlliedDrawCraft_004D54BC_034_0_70_ModularConveyor();
                break;

            case CraftIdEnum._035_0_71_ContainerTransport:
                AlliedDrawCraft_004E1F1C_035_0_71_ContainerTransport();
                break;

            case CraftIdEnum._036_0_72_TunaBoat2:
                AlliedDrawCraft_004D58A4_036_0_72_TunaBoat2();
                break;

            case CraftIdEnum._037_0_44_MuurianTransport:
                AlliedDrawCraft_004D72F0_037_0_44_MuurianTransport();
                break;

            case CraftIdEnum._038_0_45_CorellianTransport2:
            case CraftIdEnum._039_0_46_MilleniumFalcon2:
                AlliedDrawCraft_004E0D98_038_0_45_CorellianTransport2();
                break;

            case CraftIdEnum._040_0_80_Corvette2:
                AlliedDrawCraft_004DB208_040_0_80_Corvette2();
                break;

            case CraftIdEnum._041_0_81_ModCorvette:
                AlliedDrawCraft_004DCB00_041_0_81_ModCorvette();
                break;

            case CraftIdEnum._042_0_82_Frigate2:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004D69B4_042_0_82_Frigate2();
                }
                else
                {
                    AlliedDrawCraft_004D666C_042_0_82_Frigate2();
                }

                break;

            case CraftIdEnum._043_0_83_ModFrigate:
                AlliedDrawCraft_004D7058_043_0_83_ModFrigate();
                break;

            case CraftIdEnum._044_0_84_PassengerLiner:
                AlliedDrawCraft_004E818C_044_0_84_PassengerLiner();
                break;

            case CraftIdEnum._045_0_85_CarrackCruiser:
                AlliedDrawCraft_004E8BF4_045_0_85_CarrackCruiser();
                break;

            case CraftIdEnum._046_0_86_StrikeCruiser:
                AlliedDrawCraft_004D9C10_046_0_86_StrikeCruiser();
                break;

            case CraftIdEnum._047_0_87_EscortCarrier:
                AlliedDrawCraft_004E012C_047_0_87_EscortCarrier();
                break;

            case CraftIdEnum._048_0_88_Dreadnaught2:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004D91D0_048_0_88_Dreadnaught2();
                }
                else
                {
                    AlliedDrawCraft_004D8DAC_048_0_88_Dreadnaught2();
                }

                break;

            case CraftIdEnum._049_0_89_CalamariCruiserNew:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004E1AA0_049_0_89_CalamariCruiserNew();
                }
                else
                {
                    AlliedDrawCraft_004D5B0C_049_0_89_CalamariCruiserNew_050_0_90_LightCalamariCruiser();
                }

                break;

            case CraftIdEnum._050_0_90_LightCalamariCruiser:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004D5FC8_050_0_90_LightCalamariCruiser();
                }
                else
                {
                    AlliedDrawCraft_004D5B0C_049_0_89_CalamariCruiserNew_050_0_90_LightCalamariCruiser();
                }

                break;

            case CraftIdEnum._051_0_91_Interdictor2:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004DA764_051_0_91_Interdictor2();
                }
                else
                {
                    AlliedDrawCraft_004DA4D4_051_0_91_Interdictor2();
                }

                break;

            case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004DD020_229_0_92_VictoryStarDestroyer2();
                }
                else
                {
                    AlliedDrawCraft_004DCCBC_229_0_92_VictoryStarDestroyer2();
                }

                break;

            case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004E72CC_230_0_141_ImperialStarDestroyer2();
                }
                else
                {
                    AlliedDrawCraft_004D54B8_230_0_141_ImperialStarDestroyer2();
                }

                break;

            case CraftIdEnum._054_0_94_SuperStarDestroyer:
                AlliedDrawCraft_004DAC54_054_0_94_SuperStarDestroyer();
                break;

            case CraftIdEnum._055_0_57_ContainerHemisphere:
                AlliedDrawCraft_004DBDDC_055_0_57_ContainerHemisphere();
                break;

            case CraftIdEnum._056_0_58_ContainerSlotted:
                AlliedDrawCraft_004DBEEC_056_0_58_ContainerSlotted();
                break;

            case CraftIdEnum._057_0_59_ContainerHourglass:
                AlliedDrawCraft_004DC0D0_057_0_59_ContainerHourglass();
                break;

            case CraftIdEnum._058_0_60_ContainerGem:
                AlliedDrawCraft_004DC284_058_0_60_ContainerGem();
                break;

            case CraftIdEnum._059_0_61_ContainerYshaped:
                AlliedDrawCraft_004DC454_059_0_61_ContainerYshaped();
                break;

            case CraftIdEnum._060_0_102_Platform1:
            case CraftIdEnum._064_0_106_Platform5:
            case CraftIdEnum._065_0_107_Platform6:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004D77B0_060_0_102_Platform1();
                }
                else
                {
                    AlliedDrawCraft_004D77AC_060_0_102_Platform1();
                }

                break;

            case CraftIdEnum._061_0_103_Platform2:
            case CraftIdEnum._063_0_105_Platform4:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004D7A1C_061_0_103_Platform2();
                }
                else
                {
                    AlliedDrawCraft_004D7A18_061_0_103_Platform2();
                }

                break;

            case CraftIdEnum._062_0_104_Platform3:
                if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
                {
                    AlliedDrawCraft_004D7B50_062_0_104_Platform3();
                }
                else
                {
                    AlliedDrawCraft_004D7B4C_062_0_104_Platform3();
                }

                break;

            case CraftIdEnum._066_0_108_AsteroidBase:
                AlliedDrawCraft_004DDC6C_066_0_108_AsteroidBase();
                break;

            case CraftIdEnum._067_0_133_AsteroidLaserBattery:
                AlliedDrawCraft_004F1CE4_067_0_133_AsteroidLaserBattery();
                break;

            case CraftIdEnum._068_0_134_AsteroidWarheadLauncher:
                AlliedDrawCraft_004F2134_068_0_134_AsteroidWarheadLauncher();
                break;

            case CraftIdEnum._069_0_109_Factory:
                AlliedDrawCraft_004DA27C_069_0_109_Factory();
                break;

            case CraftIdEnum._070_1_0_SatB:
                AlliedDrawCraft_004F5630_070_1_0_SatB();
                break;

            case CraftIdEnum._071_1_1_SatC:
                AlliedDrawCraft_004F5718_071_1_1_SatC();
                break;

            case CraftIdEnum._072_1_2_SatD:
                AlliedDrawCraft_004F57FC_072_1_2_SatD();
                break;

            case CraftIdEnum._073_1_2_SatD:
                break;

            case CraftIdEnum._074_1_2_SatD:
                break;

            case CraftIdEnum._075_1_3_MineA:
                AlliedDrawCraft_004F4CF0_075_1_3_MineA();
                break;

            case CraftIdEnum._076_1_4_MineB:
                AlliedDrawCraft_004F4DE8_076_1_4_MineB();
                break;

            case CraftIdEnum._077_1_5_MineC:
                AlliedDrawCraft_004F4F84_077_1_5_MineC();
                break;

            case CraftIdEnum._078_1_6_GunPlatform:
                AlliedDrawCraft_004EFF30_078_1_6_GunPlatform();
                break;

            case CraftIdEnum._079_1_5_MineC:
                break;

            case CraftIdEnum._080_1_7_Probe:
                break;

            case CraftIdEnum._081_1_50_ProbeCapsule:
                break;

            case CraftIdEnum._082_1_7_Probe:
                break;

            case CraftIdEnum._083_1_47_BuoyC:
                break;

            case CraftIdEnum._084_1_48_BuoyB:
                break;

            case CraftIdEnum._085_1_8_BuoyFaux:
                break;

            case CraftIdEnum._086_1_11_AsteroidHR1:
                break;

            case CraftIdEnum._087_1_11_AsteroidHR1:
                break;

            case CraftIdEnum._088_1_49_BuoyRendez:
                break;

            case CraftIdEnum._089_0_62_CargoCanister:
                AlliedDrawCraft_004E3D58_089_0_62_CargoCanister();
                break;

            case CraftIdEnum._090_0_110_ShipYard:
                AlliedDrawCraft_004D9854_090_0_110_ShipYard();
                break;

            case CraftIdEnum._091_0_111_RepairYard:
                AlliedDrawCraft_004D472C_091_0_111_RepairYard();
                break;

            case CraftIdEnum._092_0_95_ModStrikeCruiser:
                AlliedDrawCraft_004D9E84_092_0_95_ModStrikeCruiser();
                break;

            case CraftIdEnum._093_0_96_LancerFrigate:
                AlliedDrawCraft_004EE674_093_0_96_LancerFrigate();
                break;

            case CraftIdEnum._094_0_97_BulkCruiser:
                AlliedDrawCraft_004E37D8_094_0_97_BulkCruiser();
                break;

            case CraftIdEnum._095_0_98_AssaultFrigate:
                AlliedDrawCraft_004E2F48_095_0_98_AssaultFrigate();
                break;

            case CraftIdEnum._096_0_99_CorellianGunship:
                AlliedDrawCraft_004E20F4_096_0_99_CorellianGunship();
                break;

            case CraftIdEnum._097_0_48_ImpLandingCraft:
                AlliedDrawCraft_004E07B0_097_0_48_ImpLandingCraft();
                break;

            case CraftIdEnum._098_0_49_AssaultShuttle:
                AlliedDrawCraft_004EFB14_098_0_49_AssaultShuttle();
                break;

            case CraftIdEnum._099_0_100_MarauderCorvette:
                AlliedDrawCraft_004EE01C_099_0_100_MarauderCorvette();
                break;

            case CraftIdEnum._100_0_73_StarGalleon:
                AlliedDrawCraft_004ED9D0_100_0_73_StarGalleon();
                break;

            case CraftIdEnum._101_0_101_ImpResearchShip:
                AlliedDrawCraft_004EB670_101_0_101_ImpResearchShip();
                break;

            case CraftIdEnum._102_0_50_LuxuryYacht:
                AlliedDrawCraft_004EFC78_102_0_50_LuxuryYacht();
                break;

            case CraftIdEnum._103_0_51_FerryboatLiner:
                AlliedDrawCraft_004EC1B4_103_0_51_FerryboatLiner();
                break;

            case CraftIdEnum._104_0_74_ModActionTransport:
                AlliedDrawCraft_004ED380_104_0_74_ModActionTransport();
                break;

            case CraftIdEnum._105_0_75_MobquetTransport:
                AlliedDrawCraft_004E1CD8_105_0_75_MobquetTransport();
                break;

            case CraftIdEnum._106_0_76_XiytiarTransport:
                AlliedDrawCraft_004E3E24_106_0_76_XiytiarTransport();
                break;

            case CraftIdEnum._107_0_77_FreighterConB:
                AlliedDrawCraft_004EEF7C_107_0_77_FreighterConB();
                break;

            case CraftIdEnum._108_0_78_FreighterConG:
                AlliedDrawCraft_004EEC44_108_0_78_FreighterConG();
                break;

            case CraftIdEnum._109_0_79_FreighterBox:
                AlliedDrawCraft_004EF274_109_0_79_FreighterBox();
                break;

            case CraftIdEnum._110_0_52_FamilyTransport:
                AlliedDrawCraft_004E111C_110_0_52_FamilyTransport();
                break;

            case CraftIdEnum._111_0_47_Outrider:
                AlliedDrawCraft_004F4320_111_0_47_Outrider();
                break;

            case CraftIdEnum._112_0_142_Suprosa:
                AlliedDrawCraft_004E2A2C_112_0_142_Suprosa();
                break;

            case CraftIdEnum._113_0_16_SkiprayBlastBoat:
                AlliedDrawCraft_004F3DD0_113_0_16_SkiprayBlastBoat();
                break;

            case CraftIdEnum._114_0_17_TieBizarro:
                AlliedDrawCraft_004DEF4C_114_0_17_TieBizarro();
                break;

            case CraftIdEnum._117_0_20_TieBomb:
                AlliedDrawCraft_004DF43C_117_0_20_TieBomb();
                break;

            case CraftIdEnum._119_0_22_CloakshapeFighter:
                AlliedDrawCraft_004F07E8_119_0_22_CloakshapeFighter();
                break;

            case CraftIdEnum._120_0_23_RazorFighter:
                AlliedDrawCraft_004F36FC_120_0_23_RazorFighter();
                break;

            case CraftIdEnum._121_0_24_PlanetaryFighter:
                AlliedDrawCraft_004F3320_121_0_24_PlanetaryFighter();
                break;

            case CraftIdEnum._122_0_25_SupaFighter:
                AlliedDrawCraft_004F2C58_122_0_25_SupaFighter();
                break;

            case CraftIdEnum._123_0_26_Piggyback:
                AlliedDrawCraft_004F3B4C_123_0_26_Piggyback();
                break;

            case CraftIdEnum._124_0_27_Booster:
                break;

            case CraftIdEnum._125_0_28_PreybirdFighter:
                AlliedDrawCraft_004F04B8_125_0_28_PreybirdFighter();
                break;

            case CraftIdEnum._126_0_29_Xwing:
                break;

            case CraftIdEnum._127_0_30_SlaveOne:
                AlliedDrawCraft_004F2994_127_0_30_SlaveOne();
                break;

            case CraftIdEnum._128_0_31_SlaveTwo:
                AlliedDrawCraft_004F316C_128_0_31_SlaveTwo();
                break;

            case CraftIdEnum._129_0_112_GolanOne:
                AlliedDrawCraft_004E5AD0_129_0_112_GolanOne();
                break;

            case CraftIdEnum._130_0_113_GolanTwo:
                AlliedDrawCraft_004E61A8_130_0_113_GolanTwo();
                break;

            case CraftIdEnum._131_0_114_GolanThree:
                AlliedDrawCraft_004E6A00_131_0_114_GolanThree();
                break;

            case CraftIdEnum._132_0_115_DerilynPlatform:
                AlliedDrawCraft_004EA144_132_0_115_DerilynPlatform();
                break;

            case CraftIdEnum._133_0_116_SensorArray:
                AlliedDrawCraft_004E7C18_133_0_116_SensorArray();
                break;

            case CraftIdEnum._134_0_117_CommRelay:
                AlliedDrawCraft_004EBF88_134_0_117_CommRelay();
                break;

            case CraftIdEnum._135_0_118_SpaceColony1:
                AlliedDrawCraft_004E9C00_135_0_118_SpaceColony1();
                break;

            case CraftIdEnum._136_0_119_SpaceColony2:
                AlliedDrawCraft_004E90E8_136_0_119_SpaceColony2();
                break;

            case CraftIdEnum._137_0_120_SpaceColony3:
                AlliedDrawCraft_004E9E34_137_0_120_SpaceColony3();
                break;

            case CraftIdEnum._138_0_121_Casino:
                AlliedDrawCraft_004EA6AC_138_0_121_Casino();
                break;

            case CraftIdEnum._139_0_122_CargoFacility1:
                AlliedDrawCraft_004E57C8_139_0_122_CargoFacility1();
                break;

            case CraftIdEnum._140_0_123_CargoFacility2:
                AlliedDrawCraft_004E4DA0_140_0_123_CargoFacility2();
                break;

            case CraftIdEnum._141_0_124_AsteroidMiningUnit:
                AlliedDrawCraft_004EC704_141_0_124_AsteroidMiningUnit();
                break;

            case CraftIdEnum._142_0_125_ProcessingPlant:
                AlliedDrawCraft_004ECB74_142_0_125_ProcessingPlant();
                break;

            case CraftIdEnum._143_0_126_RebelPlatform:
                AlliedDrawCraft_004E4128_143_0_126_RebelPlatform();
                break;

            case CraftIdEnum._144_0_127_ImpResearchCenter:
                AlliedDrawCraft_004EAE08_144_0_127_ImpResearchCenter();
                break;

            case CraftIdEnum._145_0_128_FamilyBase:
                AlliedDrawCraft_004E23B8_145_0_128_FamilyBase();
                break;

            case CraftIdEnum._146_0_129_FamilyRepairYard:
                AlliedDrawCraft_004D4CA8_146_0_129_FamilyRepairYard();
                break;

            case CraftIdEnum._147_0_130_PirateShipyard:
                AlliedDrawCraft_004F0AE4_147_0_130_PirateShipyard();
                break;

            case CraftIdEnum._148_0_131_IndustrialComplex:
                AlliedDrawCraft_004EB1AC_148_0_131_IndustrialComplex();
                break;

            case CraftIdEnum._149_0_132_PirateJunkyardBase:
                break;

            case CraftIdEnum._150_0_36_EscapePod:
                AlliedDrawCraft_004F5B60_150_0_36_EscapePod();
                break;

            case CraftIdEnum._151_0_63_PropaneTank:
                AlliedDrawCraft_004DC8A8_151_0_63_PropaneTank();
                break;

            case CraftIdEnum._152_0_64_ContainerGrande:
                AlliedDrawCraft_004DC5A4_152_0_64_ContainerGrande();
                break;

            case CraftIdEnum._153_0_65_ContainerBox:
                AlliedDrawCraft_004DC654_153_0_65_ContainerBox();
                break;

            case CraftIdEnum._154_0_66_ContainerSphere:
                AlliedDrawCraft_004DC700_154_0_66_ContainerSphere();
                break;

            case CraftIdEnum._155_0_67_ContainerHanger:
                AlliedDrawCraft_004EC584_155_0_67_ContainerHanger();
                break;

            case CraftIdEnum._156_1_43_GunPad:
            case CraftIdEnum._157_1_44_GunWarheadPad:
                AlliedDrawCraft_004F2370_156_1_43_GunPad(AlliedVariables.s_V0x005B6D20 == CraftIdEnum._157_1_44_GunWarheadPad ? 1 : 0);
                break;

            case CraftIdEnum._158_1_45_ProximityMineA:
                AlliedDrawCraft_004F58E4_158_1_45_ProximityMineA();
                break;

            case CraftIdEnum._159_1_46_ProximityMineB:
                AlliedDrawCraft_004F5950_159_1_46_ProximityMineB();
                break;

            case CraftIdEnum._160_1_41_HomingMineA:
                break;

            case CraftIdEnum._161_1_42_HomingMineB:
                break;

            case CraftIdEnum._162_1_40_LaserBat:
                AlliedDrawCraft_004F537C_162_1_40_LaserBat();
                break;

            case CraftIdEnum._163_1_39_IonBat:
                AlliedDrawCraft_004F54D0_163_1_39_IonBat();
                break;

            case CraftIdEnum._164_0_136_CrewCabinFront:
                AlliedDrawCraft_004F494C_164_0_136_CrewCabinFront();
                break;

            case CraftIdEnum._165_0_137_ConnectorRod:
                break;

            case CraftIdEnum._166_0_138_EngineBack:
                break;

            case CraftIdEnum._167_0_0_Xwing:
                break;

            case CraftIdEnum._168_0_0_Xwing:
                break;

            case CraftIdEnum._169_0_139_EngineFront:
                AlliedDrawCraft_004F2570_169_0_139_EngineFront();
                break;

            case CraftIdEnum._170_0_140_CrewCabinBack:
                break;

            case CraftIdEnum._171_0_0_Xwing:
                break;

            case CraftIdEnum._172_0_0_Xwing:
                break;

            case CraftIdEnum._173_0_0_Xwing:
                break;

            case CraftIdEnum._174_0_153_EscapePodA:
                AlliedDrawCraft_004F5C4C_174_0_153_EscapePodA();
                break;

            case CraftIdEnum._175_1_51_RebelPilot:
                break;

            case CraftIdEnum._176_1_52_ImperialPilot:
                break;

            case CraftIdEnum._177_1_53_CivilianPilot:
                break;

            case CraftIdEnum._178_1_54_ZeroGStormtrooper:
                break;

            case CraftIdEnum._179_1_55_ZeroGUtility:
                break;

            case CraftIdEnum._180_1_56_Marko:
                break;

            case CraftIdEnum._181_1_57_R2D2:
                break;

            case CraftIdEnum._182_1_57_R2D2:
                break;

            case CraftIdEnum._183_9001_1100_ResData_Backdrop:
                break;

            case CraftIdEnum._184__1_0:
                break;

            case CraftIdEnum._185__1_0:
                break;

            case CraftIdEnum._186__1_0:
                break;

            case CraftIdEnum._187__1_0:
                break;

            case CraftIdEnum._188__1_0:
                break;

            case CraftIdEnum._189__1_0:
                break;

            case CraftIdEnum._190__1_0:
                break;

            case CraftIdEnum._191__1_0:
                break;

            case CraftIdEnum._192__1_0:
                break;

            case CraftIdEnum._193__1_0:
                break;

            case CraftIdEnum._194__1_0:
                break;

            case CraftIdEnum._195__1_0:
                break;

            case CraftIdEnum._196__1_0:
                break;

            case CraftIdEnum._197__1_0:
                break;

            case CraftIdEnum._198__1_0:
                break;

            case CraftIdEnum._199__1_0:
                break;

            case CraftIdEnum._200__1_0:
                break;

            case CraftIdEnum._201__1_0:
                break;

            case CraftIdEnum._202__1_0:
                break;

            case CraftIdEnum._203__1_0:
                break;

            case CraftIdEnum._204__1_0:
                break;

            case CraftIdEnum._205__1_0:
                break;

            case CraftIdEnum._206__1_0:
                break;

            case CraftIdEnum._207__1_0:
                break;

            case CraftIdEnum._208__1_0:
                break;

            case CraftIdEnum._209__1_0:
                break;

            case CraftIdEnum._210__1_0:
                break;

            case CraftIdEnum._211__1_0:
                break;

            case CraftIdEnum._212__1_0:
                break;

            case CraftIdEnum._213__1_0:
                break;

            case CraftIdEnum._214__1_0:
                break;

            case CraftIdEnum._215__1_0:
                break;

            case CraftIdEnum._216__1_0:
                break;

            case CraftIdEnum._217__1_0:
                break;

            case CraftIdEnum._218__1_0:
                break;

            case CraftIdEnum._219__1_0:
                break;

            case CraftIdEnum._220__1_0:
                break;

            case CraftIdEnum._221__1_0:
                break;

            case CraftIdEnum._222__1_0:
                break;

            case CraftIdEnum._223__1_0:
                break;

            case CraftIdEnum._224__1_0:
                break;

            case CraftIdEnum._225__1_0:
                break;

            case CraftIdEnum._226__1_0:
                break;

            case CraftIdEnum._227__1_0:
                break;

            case CraftIdEnum._228_0_135_CalamariWinged:
                AlliedDrawCraft_004E1624_228_0_135_CalamariWinged();
                break;
        }
        */

        // todo
        CraftIdEnum craft = AlliedVariables.s_V0x005B6D20;
        AlliedVariables.s_V0x005B6D20 = CraftIdEnum._000__1_0;

        var data = OptHelpers.GetCraftData((int)craft);

        foreach (var line in data.geometry)
        {
            var p0 = line.Item1;
            var p1 = line.Item2;
            L004BE2B0(0, 0, p0.Z, -p0.Y, p0.X);
            L004BE2B0(1, 0, p1.Z, -p1.Y, p1.X);
        }

        // test
        //L004BE2B0(0, 0, 0.0, 0.0, 0.0);
        //L004BE2B0(1, 0, 0.0, 0.0, 1000.0);

        AlliedVariables.s_V0x005B6D20 = craft;
    }

    // L004BE2B0
    /// <remarks>edx0 is not used. Set it to 0.</remarks>
    public static void L004BE2B0(byte al0, int edx0, double A4, double AC, double A14)
    {
        double ebp40 = A4 * AlliedVariables.s_V0x005B6D58 - A14 * AlliedVariables.s_V0x005B6D50;
        double ebp38 = A14 * AlliedVariables.s_V0x005B6D58 + A4 * AlliedVariables.s_V0x005B6D50;

        if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
        {
            switch (AlliedVariables.s_V0x005B6D20)
            {
                case CraftIdEnum._001_0_0_Xwing:
                    AC = AC - 16.0f;
                    break;

                case CraftIdEnum._032_0_68_BulkFreighter:
                    break;

                case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
                    break;

                case CraftIdEnum._135_0_118_SpaceColony1:
                    AC = AC - 70.0f;
                    break;

                case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
                    break;
            }
        }
        else
        {
            if ((int)AlliedVariables.s_V0x005B6D20 == 0x32)
            {
                AC = AC + 50.0f;
            }
        }

        double ebp08 = 0;
        double ebp10 = 0;
        double ebp18 = 0;

        switch (AlliedVariables.s_AlliedMapOrientation)
        {
            case MapOrientationEnum.XY:
                ebp08 = -(ebp38 * AlliedVariables.s_V0x005B6D28);
                ebp10 = -(AC * AlliedVariables.s_V0x005B6D28);
                ebp18 = -ebp40 * AlliedVariables.s_V0x005B6D28;
                break;

            case MapOrientationEnum.XZ:
                ebp08 = -ebp38 * AlliedVariables.s_V0x005B6D28;
                ebp18 = AC * AlliedVariables.s_V0x005B6D28;
                ebp10 = -ebp40 * AlliedVariables.s_V0x005B6D28;
                break;

            case MapOrientationEnum.YZ:
                ebp18 = ebp38 * AlliedVariables.s_V0x005B6D28;
                ebp08 = AC * AlliedVariables.s_V0x005B6D28;
                ebp10 = -ebp40 * AlliedVariables.s_V0x005B6D28;
                break;

            case MapOrientationEnum.YX:
                ebp10 = ebp38 * AlliedVariables.s_V0x005B6D28;
                ebp08 = -(AC * AlliedVariables.s_V0x005B6D28);
                ebp18 = -ebp40 * AlliedVariables.s_V0x005B6D28;
                break;
        }

        double ebp28 = 0;
        double ebp30 = 0;
        double ebp20 = 0;

        switch (AlliedVariables.s_AlliedMapOrientation)
        {
            case MapOrientationEnum.XY:
                ebp28 = AlliedVariables.s_V0x005B6D40 * ebp10 - AlliedVariables.s_V0x005B6D48 * ebp18;
                AlliedVariables.s_V0x005B6D74 = (int)Math.Round(AlliedVariables.s_V0x005B6D60 - (AlliedVariables.s_V0x005B6D38 * ebp28 - AlliedVariables.s_V0x005B6D30 * ebp08));
                AlliedVariables.s_V0x005B6D70 = (int)Math.Round(AlliedVariables.s_V0x005B6D64 - (AlliedVariables.s_V0x005B6D30 * ebp28 + AlliedVariables.s_V0x005B6D38 * ebp08));
                break;

            case MapOrientationEnum.XZ:
                ebp30 = AlliedVariables.s_V0x005B6D48 * ebp10 + AlliedVariables.s_V0x005B6D40 * ebp18;
                AlliedVariables.s_V0x005B6D74 = (int)Math.Round(AlliedVariables.s_V0x005B6D38 * ebp30 + AlliedVariables.s_V0x005B6D60 + AlliedVariables.s_V0x005B6D30 * ebp08);
                AlliedVariables.s_V0x005B6D70 = (int)Math.Round(AlliedVariables.s_V0x005B6D64 - AlliedVariables.s_V0x005B6D48 * ebp18 + AlliedVariables.s_V0x005B6D40 * ebp10);
                break;

            case MapOrientationEnum.YZ:
                ebp20 = AlliedVariables.s_V0x005B6D48 * ebp10 + AlliedVariables.s_V0x005B6D40 * ebp08;
                AlliedVariables.s_V0x005B6D74 = (int)Math.Round(AlliedVariables.s_V0x005B6D38 * ebp18 + AlliedVariables.s_V0x005B6D60 + AlliedVariables.s_V0x005B6D30 * ebp20);
                AlliedVariables.s_V0x005B6D70 = (int)Math.Round(AlliedVariables.s_V0x005B6D64 - AlliedVariables.s_V0x005B6D48 * ebp08 + AlliedVariables.s_V0x005B6D40 * ebp10);
                break;

            case MapOrientationEnum.YX:
                ebp28 = AlliedVariables.s_V0x005B6D40 * ebp10 + AlliedVariables.s_V0x005B6D48 * ebp18;
                AlliedVariables.s_V0x005B6D74 = (int)Math.Round(AlliedVariables.s_V0x005B6D38 * ebp28 - AlliedVariables.s_V0x005B6D30 * ebp08 + AlliedVariables.s_V0x005B6D60);
                AlliedVariables.s_V0x005B6D70 = (int)Math.Round(AlliedVariables.s_V0x005B6D30 * ebp28 + AlliedVariables.s_V0x005B6D38 * ebp08 + AlliedVariables.s_V0x005B6D64);
                break;
        }

        if (al0 != 0)
        {
            Graphics_TCanvas_LineTo(AlliedVariables.s_V0x00543D38!, AlliedVariables.s_V0x005B6D74, AlliedVariables.s_V0x005B6D70);
        }
        else
        {
            Graphics_TCanvas_MoveTo(AlliedVariables.s_V0x00543D38!, AlliedVariables.s_V0x005B6D74, AlliedVariables.s_V0x005B6D70);
        }
    }

    // L004F8B98
    private static void TMapForm_ScrollBox1Resize(MapWindow? MapForm)
    {
        TMapForm_Proc_004F8A94(MapForm);
    }

    // L004F6F68
    public static void TMapForm__PROC_004F6F68(MapWindow MapForm)
    {
        if (AlliedVariables.s_AlliedForm1Window!.XY.IsChecked == true)
        {
            AlliedVariables.s_V0x005B705C = 0;
            AlliedVariables.s_V0x005B7058 = 0x01;
            AlliedVariables.s_V0x005B7060 = 0x02;
            AlliedVariables.s_AlliedMapOrientation = MapOrientationEnum.XY;
            Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.XYView1, true);
        }
        else if (AlliedVariables.s_AlliedForm1Window!.XZ.IsChecked == true)
        {
            AlliedVariables.s_V0x005B705C = 0;
            AlliedVariables.s_V0x005B7058 = 0x02;
            AlliedVariables.s_V0x005B7060 = 0x01;
            AlliedVariables.s_AlliedMapOrientation = MapOrientationEnum.XZ;
            Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.XZView1, true);
        }
        else
        {
            AlliedVariables.s_V0x005B705C = 0x01;
            AlliedVariables.s_V0x005B7058 = 0x02;
            AlliedVariables.s_V0x005B7060 = 0;
            AlliedVariables.s_AlliedMapOrientation = MapOrientationEnum.YZ;
            Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.YZView1, true);
        }

        switch (AlliedVariables.s_AutoCtrGrpIndexSetting)
        {
            case AutoCtrGrpEnum.LockCtr:
                Form1WindowImpl.TForm1_CentreMapBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TMapForm_Instance!);
                break;

            case AutoCtrGrpEnum.LockCurr:
                Form1WindowImpl.TForm1_CtrFGBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TMapForm_Instance!);
                break;

            case AutoCtrGrpEnum.LockZero:
                AlliedVariables.s_V0x005B7028 = TMapForm_Proc_004F65C8(MapForm, 0x01, 0.0);
                AlliedVariables.s_V0x005B7038 = 0.0f - TMapForm_Proc_004F6640(MapForm, 0x01, AlliedVariables.s_V0x005B6D6C);
                TMapForm__PROC_004F6A80(MapForm);
                break;

            case AutoCtrGrpEnum.LockBattleCtr:
                Form1WindowImpl.TForm1_FitBattleBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.FitBattleBtn);
                break;
        }

        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004F8BBC
    public static void TMapForm_Proc_004F8BBC(MapWindow eax0)
    {
        int ebp08 = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int ebp04 = 0; ebp04 < ebp08; ebp04++)
        {
            S0xFGObject ecx = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp04);

            for (int edx = 0; edx < 0x03; edx++)
            {
                short ax = ecx.FlightGroupStruct.StartPoints[0].Position[edx];
                ecx.m00147C[edx].M000000[0] = ax;
            }

            for (int edx = 0; edx < 0x03; edx++)
            {
                for (int eax = 0; eax < 0x04; eax++)
                {
                    short bx = ecx.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[eax].Position[edx];
                    ecx.m00147C[edx].M000008[eax] = bx;
                }

                for (int eax = 0; eax < 0x04; eax++)
                {
                    short bx = ecx.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[4 + eax].Position[edx];
                    ecx.m00147C[edx].M000010[eax] = bx;
                }
            }

            for (int eax = 0; eax < 0x08; eax++)
            {
                short dx = ecx.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[eax].IsUsed;
                ecx.IsWPEnabled[4 + eax] = dx;
            }

            Unit_00513838_Proc_0051D53C(ebp04);
        }
    }

    // L004F7D90
    public static void TMapForm__PROC_004F7D90(MapWindow MapForm, int edx0)
    {
        AlliedVariables.s_V0x00543B18 = edx0;
        AlliedVariables.s_V0x005B7078 = Unit_00513838_Proc_0051C204(edx0) & 0xFFU;
        TMapForm__PROC_004F7AB0(MapForm, (byte)AlliedVariables.s_V0x005B7078);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L0051C204
    private static byte Unit_00513838_Proc_0051C204(int eax0)
    {
        byte esp00 = 0x32;
        bool bl = false;

        int ebp = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int esi = 0; esi < ebp; esi++)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

            if (eax0 != eax1.FlightGroupStruct.Team)
            {
                continue;
            }

            if (!bl)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                esp00 = eax1.FlightGroupStruct.Iff;
                bl = true;
            }
        }

        byte al1;

        if (esp00 == 0x32)
        {
            al1 = AlliedVariables.s_V0x00543C78[eax0];
        }
        else
        {
            al1 = esp00;
        }

        return al1;
    }

    // L004F66F0
    public static string TMapForm_Proc_004F66F0(MapWindow MapForm, double A4)
    {
        double v0 = A4 / 160.0f;
        return Allied_FloatToText(0x02, 0x07, 0x02, v0);
    }

    // L004F6A0C
    private static void TMapForm_FormClose(MapWindow MapForm, int A8, out byte AC)
    {
        AC = 0;

        if (AlliedVariables.s_V0x00543B52 != 0)
        {
            TMapForm__PROC_004F6A2C(MapForm);
            return;
        }

        AC = 0;
        //AlliedVariables.s_TMapForm_Instance!.Visibility = Visibility.Visible;
    }

    // L004F6A2C
    public static void TMapForm__PROC_004F6A2C(MapWindow MapForm)
    {
        AlliedVariables.s_V0x005AFCBC?.Close();
        AlliedVariables.s_V0x005AFCBC = null;
        AlliedVariables.s_V0x005AFCC0?.Close();
        AlliedVariables.s_V0x005AFCC0 = null;
        AlliedVariables.s_V0x005AFCC4?.Close();
        AlliedVariables.s_V0x005AFCC4 = null;
    }

    // L004F78F4
    private static void TMapForm_Timer1Timer(MapWindow MapForm)
    {
        AlliedVariables.s_V0x005B7074 += 1;

        if (AlliedVariables.s_V0x005B7074 <= 0x02)
        {
            return;
        }

        if (AlliedVariables.s_V0x005B7008 != 0)
        {
            if (AlliedVariables.s_V0x005B700C != 0)
            {
                StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.XScroll, (int)AlliedVariables.s_AlliedForm1Window!.XScroll.Value - 0x07);
            }

            if (AlliedVariables.s_V0x005B700D != 0)
            {
                StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.XScroll, (int)AlliedVariables.s_AlliedForm1Window!.XScroll.Value + 0x07);
            }

            if (AlliedVariables.s_V0x005B700A != 0)
            {
                StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.YScroll, (int)AlliedVariables.s_AlliedForm1Window!.YScroll.Value - 0x07);
            }

            if (AlliedVariables.s_V0x005B700B != 0)
            {
                StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.YScroll, (int)AlliedVariables.s_AlliedForm1Window!.YScroll.Value + 0x07);
            }
        }
        else
        {
            if (AlliedVariables.s_V0x005B7009 != 0)
            {
                if (AlliedVariables.s_V0x00543D4C >= 0x08)
                {
                    StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, (int)AlliedVariables.s_AlliedForm1Window!.ZoomBar.Value + (int)Math.Round(AlliedVariables.s_V0x00543D4C / (float)(0x14 - AlliedVariables.s_ZoomSpeedScrollPositionSetting)));
                }
                else
                {
                    StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, (int)AlliedVariables.s_AlliedForm1Window!.ZoomBar.Value + 1);
                }
            }
        }
    }

    // L004F6F50
    private static void TMapForm_FormKeyDown(MapWindow MapForm, object? Sender, Key key, ModifierKeys modifiers)
    {
        TMapForm_Proc_004F7160(MapForm, key, modifiers);
    }

    // L004F7160
    private static void TMapForm_Proc_004F7160(MapWindow MapForm, Key key, ModifierKeys modifiers)
    {
        switch (key)
        {
            case Key.D0:
                TMapForm__PROC_004F7D90(MapForm, 0x09);
                break;

            case Key.D1:
                TMapForm__PROC_004F7D90(MapForm, 0);
                break;

            case Key.D2:
                TMapForm__PROC_004F7D90(MapForm, 0x01);
                break;

            case Key.D3:
                TMapForm__PROC_004F7D90(MapForm, 0x02);
                break;

            case Key.D4:
                TMapForm__PROC_004F7D90(MapForm, 0x03);
                break;

            case Key.D5:
                TMapForm__PROC_004F7D90(MapForm, 0x04);
                break;

            case Key.D6:
                TMapForm__PROC_004F7D90(MapForm, 0x05);
                break;

            case Key.D7:
                TMapForm__PROC_004F7D90(MapForm, 0x06);
                break;

            case Key.D8:
                TMapForm__PROC_004F7D90(MapForm, 0x07);
                break;

            case Key.D9:
                TMapForm__PROC_004F7D90(MapForm, 0x08);
                break;

            case Key.D:
                Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                break;

            case Key.P:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.PaletOn, AlliedVariables.s_AlliedForm1Window!.PaletOn.IsChecked != true);
                Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                break;

            case Key.T:
                Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                break;
        }

        if (modifiers == ModifierKeys.Control)
        {
            switch (key)
            {
                case Key.A:
                    Form1WindowImpl.TForm1_SaveAsBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.AllIFF);
                    break;

                case Key.G:
                    AlliedVariables.s_V0x005B7047 = !AlliedVariables.s_V0x005B7047;
                    Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                    break;

                case Key.I:
                    AlliedVariables.s_V0x005B7046 = !AlliedVariables.s_V0x005B7046;
                    Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                    break;

                case Key.S:
                    Form1WindowImpl.TForm1_SaveBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.AllIFF);
                    break;

                case Key.W:
                    AlliedVariables.s_V0x005B7007 = !AlliedVariables.s_V0x005B7007;
                    Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                    break;
            }
        }
        else
        {
            switch (key)
            {
                case Key.Return:
                    Form1WindowImpl.TForm1_CentreMapBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.CentreMapBtn);
                    break;

                case Key.Space:
                    if (AlliedVariables.s_AlliedForm1Window!.XY.IsChecked == true)
                    {
                        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.XZ, true);
                    }
                    else if (AlliedVariables.s_AlliedForm1Window!.XZ.IsChecked == true)
                    {
                        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.YZ, true);
                    }
                    else
                    {
                        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.XY, true);
                    }

                    TMapForm__PROC_004F6F68(MapForm);
                    break;

                case Key.Delete:
                    {
                        AlliedVariables.s_V0x00543B0C = AlliedVariables.s_V0x005B7050;
                        Unit_00513838_Proc_00518EB4();
                        Unit_00513838_Proc_0051DB68();
                        AlliedVariables.s_V0x005B7050 = AlliedVariables.s_V0x00543B0C;
                        AlliedVariables.s_V0x005B7054 = 0x01;
                        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SetItems(AlliedVariables.s_V0x00543BC0);
                        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex = AlliedVariables.s_V0x005B7050;

                        if (AlliedVariables.s_FlightGroupObjectsList.Count - 1 < AlliedVariables.s_V0x005B7068)
                        {
                            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
                            AlliedVariables.s_V0x005B7044 = 0x01;
                        }

                        S0xFGObject eax1;

                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
                        eax1.m00147A = 0x01;
                        AlliedVariables.s_V0x005B7043 = 0;
                        Form1WindowImpl.TForm1_CurrOnlyClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.CurrOnly);
                        AlliedVariables.s_V0x005B7043 = 0x01;
                        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);

                        int esi1 = AlliedVariables.s_FlightGroupObjectsList.Count;

                        for (int ebx = 0; ebx < esi1; ebx++)
                        {
                            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx, false);

                            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                            eax.m001479 = 0;
                        }

                        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543B0C, true);

                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
                        eax1.m001479 = 0x01;
                        break;
                    }

                case Key.A:
                    StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, 0x320);
                    break;

                case Key.C:
                    if (AlliedVariables.s_AlliedForm1Window!.CurrOnly.IsChecked == true)
                    {
                        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.CurrOnly, false);
                    }
                    else
                    {
                        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.CurrOnly, true);
                    }

                    Form1WindowImpl.TForm1_CurrOnlyClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.CurrOnly);
                    break;

                case Key.F:
                    Form1WindowImpl.TForm1_CtrFGBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.CtrFGBtn);
                    break;

                case Key.L:
                    if (AlliedVariables.s_AlliedForm1Window!.LockBtn.IsChecked == true)
                    {
                        Buttons_TSpeedButton_SetDown(AlliedVariables.s_AlliedForm1Window!.LockBtn, false);
                    }
                    else
                    {
                        Buttons_TSpeedButton_SetDown(AlliedVariables.s_AlliedForm1Window!.LockBtn, true);
                    }

                    Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                    break;

                case Key.N:
                    if (AlliedVariables.s_AlliedForm1Window!.NamesOn.IsChecked == true)
                    {
                        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.NamesOn, false);
                    }
                    else
                    {
                        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.NamesOn, true);
                    }

                    Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                    break;

                case Key.S:
                    Form1WindowImpl.TForm1_ZoomTo16BtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.ZoomTo16Btn);
                    break;

                case Key.W:
                    ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.AllWPS, AlliedVariables.s_AlliedForm1Window!.AllWPS.IsChecked != true);
                    Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
                    break;

                case Key.X:
                    if (AlliedVariables.s_V0x00543D4C < 0x64)
                    {
                        StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, AlliedVariables.s_V0x00543D4C - 0x04);
                    }
                    else
                    {
                        StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, AlliedVariables.s_V0x00543D4C - 0x06);
                    }

                    break;

                case Key.Z:
                    if (AlliedVariables.s_V0x00543D4C < 0x64)
                    {
                        StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, AlliedVariables.s_V0x00543D4C + 0x04);
                    }
                    else
                    {
                        StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, AlliedVariables.s_V0x00543D4C + 0x06);
                    }

                    break;

                case Key.F1:
                    ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.XY, true);
                    TMapForm__PROC_004F6F68(MapForm);
                    break;

                case Key.F2:
                    ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.XZ, true);
                    TMapForm__PROC_004F6F68(MapForm);
                    break;

                case Key.F3:
                    ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.YZ, true);
                    TMapForm__PROC_004F6F68(MapForm);
                    break;

                case Key.OemPlus:
                    TMapForm_ZoomInButClick(MapForm, AlliedVariables.s_TMapForm_Instance!);
                    break;

                case Key.OemMinus:
                    TMapForm_ZoomOutButClick(MapForm, AlliedVariables.s_TMapForm_Instance!);
                    break;
            }
        }
    }

    // L004F7D0C
    public static int TMapForm__PROC_004F7D0C(MapWindow MapForm, byte edx0)
    {
        if (edx0 > 0x47)
        {
            return edx0 + 0x12;
        }

        if (edx0 > 0x45)
        {
            return edx0 + 0x10;
        }

        if (edx0 > 0x43)
        {
            return edx0 + 0x0F;
        }

        if (edx0 > 0x42)
        {
            return edx0 + 0x0D;
        }

        if (edx0 > 0x3E)
        {
            return edx0 + 0x0C;
        }

        if (edx0 > 0x38)
        {
            return edx0 + 0x09;
        }

        if (edx0 > 0x23)
        {
            return edx0 + 0x04;
        }

        if (edx0 > 0x1C)
        {
            return edx0 + 0x03;
        }

        if (edx0 > 0x09)
        {
            return edx0 + 0x02;
        }

        return edx0;
    }

    // L004F8CC4
    public static void TMapForm__PROC_004F8CC4(MapWindow MapForm)
    {
        if (AlliedVariables.s_V0x005B7044 != 0)
        {
            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        }
        else if (AlliedVariables.s_FlightGroupObjectsList.Count - 1 < AlliedVariables.s_V0x005B7068)
        {
            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        }

        int edx1 = AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex;
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex = edx1;
        AlliedVariables.s_V0x005B7050 = AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex;
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SetItems(AlliedVariables.s_V0x00543BC0);
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex = AlliedVariables.s_V0x005B7050;

        int ebx1 = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int esi = 0; esi < ebx1; esi++)
        {
            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
            eax.m00147A = 0x01;
        }

        if (AlliedVariables.s_V0x00543D11 != 0)
        {
            ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.XY, true);
            ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.CurrOnly, false);
            AlliedVariables.s_V0x005B7064 = 0;
            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
            AlliedVariables.s_V0x005B7044 = 0x01;
            TMapForm__PROC_004F6F68(MapForm);
            StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, 0x20);
            Form1WindowImpl.TForm1_FitBattleBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.FitBattleBtn);
            Form1WindowImpl.TForm1_AllIFFClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.AllIFF);
            AlliedVariables.s_V0x00543D11 = 0;
        }

        TMapForm_Proc_004F6B48(MapForm);
    }

    // L004F7A4C
    public static void TMapForm__PROC_004F7A4C(MapWindow MapForm)
    {
        S0xFGObject eax1;
        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
        int eax2 = Integer_Negate_L0051C034(eax1.m00147C[AlliedVariables.s_V0x005B7058].M000000[0]);
        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
        TMapForm__PROC_004F68E8(MapForm, eax1.m00147C[AlliedVariables.s_V0x005B705C].M000000[0], eax2);
    }

    // L004F66E8
    private static void TMapForm_ZoomInButClick(MapWindow MapForm, object? Sender)
    {
    }

    // L004F66EC
    private static void TMapForm_ZoomOutButClick(MapWindow MapForm, object? Sender)
    {
    }

    // L004F6724
    private static void TMapForm_YScrollChange(MapWindow MapForm)
    {
    }

    // L004F6728
    private static void TMapForm_BriefingBoxChange(MapWindow MapForm)
    {
    }

    // L004F6AE4
    private static void TMapForm_XSnapRadiosClick(MapWindow MapForm)
    {
    }

    // L004F6AE8
    private static void TMapForm_MapListClick(MapWindow MapForm)
    {
        if (AlliedVariables.s_AlliedForm1Window!.CurrOnly.IsChecked == true)
        {
            AlliedVariables.s_V0x005B7064 = AlliedVariables.s_V0x005B7050;
            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_V0x005B7064;
            AlliedVariables.s_V0x005B7044 = 0;
        }

        TMapForm_Proc_004F6B3C(MapForm);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004F6B3C
    private static void TMapForm_Proc_004F6B3C(MapWindow MapForm)
    {
    }

    // L004F6B40
    private static void TMapForm_RebIFFClick(MapWindow MapForm)
    {
        TMapForm_Proc_004F6B48(MapForm);
    }

    // L004F6F64
    private static void TMapForm_ShowFGListClick(MapWindow MapForm)
    {
    }

    // L004F710C
    public static void TMapForm_Proc_004F710C(MapWindow MapForm)
    {
        if (AlliedVariables.s_AlliedForm1Window!.CurrWPs.IsChecked == true)
        {
            AlliedVariables.s_V0x005B7070 = 0x01;
            AlliedVariables.s_V0x005B706C = 0x01;
        }
        else
        {
            AlliedVariables.s_V0x005B7070 = 0x01;
            AlliedVariables.s_V0x005B706C = 0x01;
        }

        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004F7A48
    private static void TMapForm_YSnapRadiosClick(MapWindow MapForm)
    {
    }

    // L004F7DD0
    public static void TMapForm__PROC_004F7DD0(MapWindow MapForm)
    {
        if (AlliedVariables.s_AlliedForm1Window!.LockCtr.IsChecked == true)
        {
            AlliedVariables.s_AutoCtrGrpIndexSetting = AutoCtrGrpEnum.LockCtr;
            return;
        }

        if (AlliedVariables.s_AlliedForm1Window!.LockCurr.IsChecked == true)
        {
            AlliedVariables.s_AutoCtrGrpIndexSetting = AutoCtrGrpEnum.LockCurr;
            return;
        }

        if (AlliedVariables.s_AlliedForm1Window!.LockZero.IsChecked == true)
        {
            AlliedVariables.s_AutoCtrGrpIndexSetting = AutoCtrGrpEnum.LockZero;
            return;
        }

        if (AlliedVariables.s_AlliedForm1Window!.LockBattleCtr.IsChecked == true)
        {
            AlliedVariables.s_AutoCtrGrpIndexSetting = AutoCtrGrpEnum.LockBattleCtr;
            return;
        }

        AlliedVariables.s_AutoCtrGrpIndexSetting = AutoCtrGrpEnum.None;
    }

    // L004F893C
    private static void TMapForm_LockCtrClick(MapWindow MapForm)
    {
        TMapForm__PROC_004F7DD0(MapForm);
    }

    // L004F8944
    private static void TMapForm_SpeedButton3Click(MapWindow MapForm)
    {
    }

    // L004F8948
    private static void TMapForm_SpeedButton2Click(MapWindow MapForm)
    {
    }

    // L004F8BA0
    private static void TMapForm_N11Click(MapWindow MapForm, MenuItem Sender)
    {
        AlliedVariables.s_V0x00543B18 = Menus_TMenuItem_GetMenuIndex(Sender);
        Unit_00513838_Proc_0051F42C();
    }

    // L00518EB4
    private static void Unit_00513838_Proc_00518EB4()
    {
        if (AlliedVariables.s_FlightGroupObjectsList.Count > 0x01 && AlliedVariables.s_FlightGroupObjectsList.Count > AlliedVariables.s_V0x00543B0C)
        {
            Classes_TList_Delete(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
            AlliedVariables.s_AlliedForm1Window!.ShipList.DeleteItem(AlliedVariables.s_V0x00543B0C);
            AlliedVariables.s_TieFileHeader.FlightGroupsCount--;

            if (AlliedVariables.s_FlightGroupObjectsList.Count <= AlliedVariables.s_V0x00543B0C)
            {
                AlliedVariables.s_V0x00543B0C = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
            }

            DatapadWindowImpl.TDatapad__PROC_004C05F0(AlliedVariables.s_TDatapad_Instance!);

            if (AlliedVariables.s_FlightGroupObjectsList.Count > 0)
            {
                AlliedVariables.s_V0x005AFE90 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
                DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
                AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex = AlliedVariables.s_V0x00543B0C;
            }

            Unit_00513838_Proc_0051DB68();
            Form1WindowImpl.L005192A0(AlliedVariables.s_V0x00543CC0, 0x58);
        }
        else
        {
            AlliedVariables.s_V0x005AFE90.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
            Unit_00513838_Proc_0051DB68();
            DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
        }

        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543B0C, true);
        S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
        eax1.m001479 = 0x01;
        Unit_00513838_Proc_005146A4();
    }
}
