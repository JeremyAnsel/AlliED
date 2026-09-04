using AlliED.Controls;
using AlliED.Extensions;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlliED.Impl.ViewsImpl;

internal static class WavListBoxImpl
{
    public static void Register(WavListBox window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(WavListBox window)
    {
        window.Closed += (s, e) => TWAVListForm_FormClose(window);
        window.PilotChoice.MouseLeftButtonUp += (s, e) => TWAVListForm_PilotChoiceClick(window, s);
        window.TacChoice.MouseLeftButtonUp += (s, e) => TWAVListForm_TacChoiceClick(window, s);
        window.SpeedButton1.Click += (s, e) => TWAVListForm_SpeedButton1Click(window, s);
        window.CheckBox1.Click += (s, e) => TWAVListForm_CheckBox1Click(window);
        window.SearchEdit.KeyDown += (s, e) => TWAVListForm_SearchEditKeyDown(window, s, e.Key, Keyboard.Modifiers);
        window.OKBtn.Click += (s, e) => TWAVListForm_OKBtnClick(window, s);
        window.VoiceTabs.SelectionChanged += (s, e) =>
        {
            TWAVListForm_VoiceTabsChanging(window);
            TWAVListForm_VoiceTabsChange(window, s);
        };

        window.ListBox1.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.ListBox1,
            MouseAction.LeftClick,
            s => TWAVListForm_ListBox1Click(window, s),
            s => true));
        window.ListBox1.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.ListBox1,
            MouseAction.LeftDoubleClick,
            s => TWAVListForm_ListBox1DblClick(window, s),
            s => true));

        window.ListBox1.DrawItem += (sender, index) => TWAVListForm_ListBox1DrawItem(window, sender, index, null, null);
    }

    // L004D2FA0
    private static void FormCreate(WavListBox WAVListForm)
    {
        if (TApplication_GetWidth() == 0x280 || TApplication_GetHeight() == 0x1E0)
        {
            TApplication_L00468080(WAVListForm, 0x01);
            Graphics_TFont_SetName(WAVListForm, "MS Serif");
            Graphics_TFont_SetSize(WAVListForm, 0x09);
            Controls_TWinControl_ScaleBy(WAVListForm, TApplication_GetWidth(), 0x32A);
            Controls_TControl_SetHeight(WAVListForm, 0x1BD);
            Controls_TControl_SetWidth(WAVListForm, 0x23A);
            TApplication_RecreateWnd(WAVListForm, 0x04);
        }

        AlliedVariables.s_TacticalOfficersVoiceList = new();
        AlliedVariables.s_V0x00542408 = new();
        AlliedVariables.s_V0x0054240C = new();
        AlliedVariables.s_PilotsVoiceList = new();
        AlliedVariables.s_V0x00542414 = new();
        AlliedVariables.s_CD1VoiceList = new();
        AlliedVariables.s_V0x0054241C = new();
        AlliedVariables.s_CD2VoiceList = new();
        AlliedVariables.s_V0x00542424 = new();
        AlliedVariables.s_CustomVoiceList = new();
        AlliedVariables.s_V0x0054242C = new();

        WAVListForm.CheckBox1.IsChecked = AlliedVariables.s_V0x00543C9D != 0;
        AlliedVariables.s_V0x00542438 = 0;
        AlliedLoadTStringsItemsFromFileAndFillComboBox("TacWAVs", AlliedVariables.s_TacticalOfficersVoiceList, AlliedVariables.s_AlliedForm1Window!.SelectionBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("PilotWAVs", AlliedVariables.s_PilotsVoiceList, AlliedVariables.s_AlliedForm1Window!.SelectionBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("CD1wavs", AlliedVariables.s_CD1VoiceList, AlliedVariables.s_AlliedForm1Window!.SelectionBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("CD2wavs", AlliedVariables.s_CD2VoiceList, AlliedVariables.s_AlliedForm1Window!.SelectionBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Customwavs", AlliedVariables.s_CustomVoiceList, AlliedVariables.s_AlliedForm1Window!.SelectionBox, false);

        int ebx = AlliedVariables.s_PilotsVoiceList.GetCount();

        for (int esi = 0; esi < ebx; esi++)
        {
            TWAVListForm_Proc_004D3E64(WAVListForm, esi, AlliedVariables.s_PilotsVoiceList, AlliedVariables.s_V0x00542414);
        }

        ebx = AlliedVariables.s_TacticalOfficersVoiceList.GetCount();

        for (int esi = 0; esi < ebx; esi++)
        {
            TWAVListForm_Proc_004D3E64(WAVListForm, esi, AlliedVariables.s_TacticalOfficersVoiceList, AlliedVariables.s_V0x00542408);
        }

        ebx = AlliedVariables.s_CD1VoiceList.GetCount();

        for (int esi = 0; esi < ebx; esi++)
        {
            TWAVListForm_Proc_004D3E64(WAVListForm, esi, AlliedVariables.s_CD1VoiceList, AlliedVariables.s_V0x0054241C);
        }

        ebx = AlliedVariables.s_CD2VoiceList.GetCount();

        for (int esi = 0; esi < ebx; esi++)
        {
            TWAVListForm_Proc_004D3E64(WAVListForm, esi, AlliedVariables.s_CD2VoiceList, AlliedVariables.s_V0x00542424);
        }

        ebx = AlliedVariables.s_CustomVoiceList.GetCount();

        for (int esi = 0; esi < ebx; esi++)
        {
            TWAVListForm_Proc_004D3E64(WAVListForm, esi, AlliedVariables.s_CustomVoiceList, AlliedVariables.s_V0x0054242C);
        }

        switch (AlliedVariables.s_VoiceTabsPageIndex)
        {
            case VoiceTabsPageEnum.TacticalOfficers:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_TacticalOfficersVoiceList.Items);
                break;

            case VoiceTabsPageEnum.Pilots:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_PilotsVoiceList.Items);
                break;

            case VoiceTabsPageEnum.CD1:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_CD1VoiceList.Items);
                break;

            case VoiceTabsPageEnum.CD2:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_CD2VoiceList.Items);
                break;

            case VoiceTabsPageEnum.Custom:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_CustomVoiceList.Items);
                break;
        }

        WAVListForm.VoiceTabs.SelectedIndex = (int)AlliedVariables.s_VoiceTabsPageIndex;
        int eax1 = TWAVListForm__PROC_004D4668(WAVListForm, (AlliedVariables.s_V0x00543D20)[WAVListForm.VoiceTabs.SelectedIndex]);
        WAVListForm.ListBox1.SelectedIndex = eax1;
        TWAVListForm_Proc_004D4340(WAVListForm);
        Controls_TControl_SetText(WAVListForm.SearchEdit, AlliedVariables.s_WAVListFormSearchString);
        AlliedVariables.s_V0x00542434 = AlliedVariables.s_AlliedForm1Window!.WAVplayer.SoundLocation;
        ExtCtrls_TCustomRadioGroup_SetItemIndex(WAVListForm.PilotChoice, AlliedVariables.s_WAVListForm_PilotChoice_ItemIndex);
        ExtCtrls_TCustomRadioGroup_SetItemIndex(WAVListForm.TacChoice, AlliedVariables.s_WAVListForm_TacChoice_ItemIndex);
        TWAVListForm_ListBox1Click(WAVListForm, WAVListForm.ListBox1);
    }

    // L004D3418
    private static void TWAVListForm_PilotChoiceClick(WavListBox WAVListForm, object? Sender)
    {
        AlliedVariables.s_WAVListForm_PilotChoice_ItemIndex = WAVListForm.PilotChoice.GetItemIndex();
        TWAVListForm_ListBox1Click(WAVListForm, Sender);
    }

    // L004D3530
    private static void TWAVListForm_TacChoiceClick(WavListBox WAVListForm, object? Sender)
    {
        AlliedVariables.s_WAVListForm_TacChoice_ItemIndex = WAVListForm.TacChoice.GetItemIndex();
        TWAVListForm_ListBox1Click(WAVListForm, Sender);
    }

    // L004D3434
    private static void TWAVListForm_FormClose(WavListBox WAVListForm)
    {
        AlliedVariables.s_VoiceTabsPageIndex = (VoiceTabsPageEnum)WAVListForm.VoiceTabs.SelectedIndex;
        AlliedVariables.s_WAVListFormSearchString = Controls_TControl_GetText(WAVListForm.SearchEdit);
        AlliedVariables.s_V0x00543D20[WAVListForm.VoiceTabs.SelectedIndex] = (short)WAVListForm.ListBox1.SelectedIndex;

        AlliedVariables.s_TacticalOfficersVoiceList = new();
        AlliedVariables.s_V0x00542408 = new();
        AlliedVariables.s_V0x0054240C = new();
        AlliedVariables.s_PilotsVoiceList = new();
        AlliedVariables.s_V0x00542414 = new();
        AlliedVariables.s_CD1VoiceList = new();
        AlliedVariables.s_V0x0054241C = new();
        AlliedVariables.s_CD2VoiceList = new();
        AlliedVariables.s_V0x00542424 = new();
        AlliedVariables.s_CustomVoiceList = new();
        AlliedVariables.s_V0x0054242C = new();

        AlliedVariables.s_V0x00542434 = string.Empty;
        AlliedVariables.s_V0x00542430 = string.Empty;
    }

    // L004D43E8
    private static void TWAVListForm_SpeedButton1Click(WavListBox WAVListForm, object? Sender)
    {
        int ebp04 = WAVListForm.ListBox1.SelectedIndex;

        if (ebp04 < 0)
        {
            ebp04 = 0;
        }

        string ebp0C = Controls_TControl_GetText(WAVListForm.SearchEdit);

        if (!string.IsNullOrEmpty(ebp0C)
            && WAVListForm.ListBox1.Items.Count > 0
            && WAVListForm.ListBox1.SelectedIndex < WAVListForm.ListBox1.Items.Count)
        {
            int esi = WAVListForm.ListBox1.SelectedIndex + 1;

            if (esi > WAVListForm.ListBox1.Items.Count - 1)
            {
                esi = WAVListForm.ListBox1.Items.Count - 1;
            }

            bool ebp05 = false;

            while (true)
            {
                string ebp1C_2 = WAVListForm.ListBox1.GetItemText(esi).ToUpperInvariant();
                string ebp1C_0 = Controls_TControl_GetText(WAVListForm.SearchEdit).ToUpperInvariant();

                if (ebp1C_2.IndexOf(ebp1C_0) > 0)
                {
                    ebp05 = true;
                }

                esi++;

                if (ebp05)
                {
                    break;
                }

                if (esi >= WAVListForm.ListBox1.Items.Count)
                {
                    break;
                }
            }

            if (!ebp05)
            {
                esi = 0;

                while (true)
                {
                    string ebp2C_2 = WAVListForm.ListBox1.GetItemText(esi).ToUpperInvariant();
                    string ebp2C_0 = Controls_TControl_GetText(WAVListForm.SearchEdit).ToUpperInvariant();

                    if (ebp2C_2.IndexOf(ebp2C_0) > 0)
                    {
                        ebp05 = true;
                    }

                    esi++;

                    if (ebp05)
                    {
                        break;
                    }

                    if (esi >= ebp04)
                    {
                        break;
                    }
                }
            }

            if (ebp05)
            {
                WAVListForm.ListBox1.SelectedIndex = esi - 1;
                TWAVListForm_ListBox1Click(WAVListForm, WAVListForm.ListBox1);
            }
            else
            {
                Allied_ShowMessageWithTimer(0x3E8, "  Not found.  ");
            }
        }
    }

    // L004D3BF4
    private static void TWAVListForm_CheckBox1Click(WavListBox WAVListForm)
    {
        AlliedVariables.s_V0x00543C9D = WAVListForm.CheckBox1.IsChecked == true ? (byte)1 : (byte)0;
    }

    // L004D3BA0
    private static void TWAVListForm_Proc_004D3BA0(WavListBox WAVListForm)
    {
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.Stop();
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.Load();
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.Play();
    }

    // L004D43D0
    private static void TWAVListForm_SearchEditKeyDown(WavListBox WAVListForm, object? Sender, Key key, ModifierKeys modifiers)
    {
        if (key == Key.Escape)
        {
            TWAVListForm_SpeedButton1Click(WAVListForm, WAVListForm.SpeedButton1);
        }
    }

    // L004D3C10
    private static void TWAVListForm_OKBtnClick(WavListBox WAVListForm, object? Sender)
    {
        AlliedVariables.s_V0x00542438 = 0x01;

        if (AlliedVariables.s_V0x00542438 != 0)
        {
            if (string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
            {
                TModalResultEnum ax1 = MessageBox_ShowConfirmation("There is no WAV .lst file for this mission. Create one?", null);

                if (ax1 == TModalResultEnum.Yes)
                {
                    Form1WindowImpl.TForm1_CreateLstClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.CreateLst);
                }
            }
        }

        bool jmp = false;

        if (AlliedVariables.s_V0x00542438 == 0)
        {
            jmp = true;
        }
        else
        {
            if (string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
            {
                jmp = true;
            }
        }

        if (!jmp)
        {
            string ebp08_1 = AlliedVariables.s_V0x00542430;

            if (AlliedVariables.s_V0x00542439 != 0)
            {
                string ebp08_0 = AlliedVariables.s_XWADirLabSetting + "\\wave\\";
                System_LStrDelete(ref ebp08_1, 0x01, ebp08_0.Length);
            }
            else
            {
                System_LStrDelete(ref ebp08_1, 0x01, 0x08);
            }

            if (Unit_00513838_Proc_0051D348_Returns_0x01(AlliedVariables.s_V0x00543C3C, ebp08_1))
            {
                if (AlliedVariables.s_AlliedForm1Window!.EndMsgWav.IsChecked == true)
                {
                    int edx1 = AlliedVariables.s_AlliedForm1Window!.MsgStrList.SelectedIndex + 0x40;
                    AlliedVariables.s_V0x00543C3C.Put(edx1, ebp08_1);
                }
                else
                {
                    AlliedVariables.s_V0x00543C3C.Put(AlliedVariables.s_V0x00543B10, ebp08_1);
                }

                Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.WAVfileEd, ebp08_1);
                AlliedVariables.s_V0x00543B59 = 0x01;
                WAVListForm.Close();
            }
            else
            {
                AlliedVariables.s_V0x00542438 = 0;
            }
        }
        else
        {
            AlliedVariables.s_AlliedForm1Window!.WAVplayer.SoundLocation = AlliedVariables.s_V0x00542434;
            AlliedVariables.s_AlliedForm1Window!.WAVplayer.Load();
            AlliedVariables.s_AlliedForm1Window!.WAVplayer.Play();
        }
    }

    // L004D463C
    private static void TWAVListForm_VoiceTabsChanging(WavListBox WAVListForm)
    {
        AlliedVariables.s_V0x00543D20[WAVListForm.VoiceTabs.SelectedIndex] = (short)WAVListForm.ListBox1.SelectedIndex;
    }

    // L004D400C
    private static void TWAVListForm_VoiceTabsChange(WavListBox WAVListForm, object? Sender)
    {
        TWAVListForm_Proc_004D4340(WAVListForm);
        WAVListForm.ListBox1.Clear();

        VoiceTabsPageEnum eax1 = (VoiceTabsPageEnum)WAVListForm.VoiceTabs.SelectedIndex;

        switch (eax1)
        {
            case VoiceTabsPageEnum.TacticalOfficers:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_TacticalOfficersVoiceList.Items);
                break;

            case VoiceTabsPageEnum.Pilots:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_PilotsVoiceList.Items);
                break;

            case VoiceTabsPageEnum.CD1:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_CD1VoiceList.Items);
                break;

            case VoiceTabsPageEnum.CD2:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_CD2VoiceList.Items);
                break;

            case VoiceTabsPageEnum.Custom:
                StdCtrls_TCustomListBox_SetItems(WAVListForm.ListBox1, AlliedVariables.s_CustomVoiceList.Items);
                break;
        }

        WAVListForm.ListBox1.SelectedIndex = TWAVListForm__PROC_004D4668(WAVListForm, AlliedVariables.s_V0x00543D20[WAVListForm.VoiceTabs.SelectedIndex]);
        AlliedVariables.s_WAVListForm_PilotChoice_ItemIndex = WAVListForm.PilotChoice.GetItemIndex();

        TWAVListForm_ListBox1Click(WAVListForm, Sender);
    }

    // L004D4668
    private static int TWAVListForm__PROC_004D4668(WavListBox WAVListForm, int edx0)
    {
        if (edx0 < 0)
        {
            return 0;
        }

        if (edx0 <= WAVListForm.ListBox1.Items.Count - 1)
        {
            return edx0;
        }

        return WAVListForm.ListBox1.Items.Count - 1;
    }

    // L004D4340
    private static void TWAVListForm_Proc_004D4340(WavListBox WAVListForm)
    {
        VoiceTabsPageEnum eax1 = (VoiceTabsPageEnum)WAVListForm.VoiceTabs.SelectedIndex;

        switch (eax1)
        {
            case VoiceTabsPageEnum.TacticalOfficers:
                Controls_TControl_SetVisible(WAVListForm.TacChoice, true);
                Controls_TControl_SetVisible(WAVListForm.PilotChoice, false);
                break;

            case VoiceTabsPageEnum.Pilots:
                Controls_TControl_SetVisible(WAVListForm.PilotChoice, true);
                Controls_TControl_SetVisible(WAVListForm.TacChoice, false);
                break;

            case VoiceTabsPageEnum.CD1:
            case VoiceTabsPageEnum.CD2:
            case VoiceTabsPageEnum.Custom:
                Controls_TControl_SetVisible(WAVListForm.TacChoice, false);
                Controls_TControl_SetVisible(WAVListForm.PilotChoice, false);
                break;
        }

        WAVListForm.TacChoice.InvalidateVisual();
        WAVListForm.PilotChoice.InvalidateVisual();
    }

    // L004D354C
    private static void TWAVListForm_ListBox1Click(WavListBox WAVListForm, object? Sender)
    {
        AlliedVariables.s_V0x00542439 = 0;
        string ebp40_15 = System_LStrFromChar(AlliedVariables.s_AlliedDriveLetter) + ":\\wave\\";

        switch ((VoiceTabsPageEnum)WAVListForm.VoiceTabs.SelectedIndex)
        {
            case VoiceTabsPageEnum.TacticalOfficers:
                {
                    switch ((TacticalOfficersEnum)WAVListForm.TacChoice.GetItemIndex())
                    {
                        case TacticalOfficersEnum.Devers:
                            ebp40_15 += "Devers\\G0DE";
                            break;

                        case TacticalOfficersEnum.Kupalo:
                            ebp40_15 += "Kupalo\\G0KU";
                            break;

                        case TacticalOfficersEnum.Zaletta:
                            ebp40_15 += "Zaletta\\G0ZL";
                            break;

                        case TacticalOfficersEnum.Emkay:
                            ebp40_15 += "Emkay\\G0MC";
                            break;
                    }

                    break;
                }

            case VoiceTabsPageEnum.Pilots:
                {
                    PilotChoiceEnum esi0 = (PilotChoiceEnum)WAVListForm.PilotChoice.GetItemIndex();

                    switch (esi0)
                    {
                        case PilotChoiceEnum.AERON_AZZAMEEN:
                            // "AERON_AZZAMEEN\A0AE"
                            ebp40_15 += "AERON_AZZAMEEN\\A0AE";
                            break;

                        case PilotChoiceEnum.EMON_AZZAMEEN:
                            ebp40_15 += "EMON_AZZAMEEN\\A0EM";
                            break;

                        case PilotChoiceEnum.EMKAY:
                            ebp40_15 += "Emkay\\A0MC";
                            break;

                        default:
                            ebp40_15 += "REBEL_PILOT" + ((int)esi0 + 1).ToString(CultureInfo.InvariantCulture) + "\\A0";
                            break;
                    }

                    PilotChoiceEnum esi1 = (PilotChoiceEnum)WAVListForm.PilotChoice.GetItemIndex();

                    switch (esi1)
                    {
                        case PilotChoiceEnum.REBEL_PILOT1:
                        case PilotChoiceEnum.REBEL_PILOT2:
                        case PilotChoiceEnum.REBEL_PILOT3:
                        case PilotChoiceEnum.REBEL_PILOT4:
                        case PilotChoiceEnum.REBEL_PILOT5:
                        case PilotChoiceEnum.REBEL_PILOT6:
                        case PilotChoiceEnum.REBEL_PILOT7:
                        case PilotChoiceEnum.REBEL_PILOT8:
                        case PilotChoiceEnum.REBEL_PILOT9:
                            ebp40_15 += "p" + ((int)esi1 + 1).ToString(CultureInfo.InvariantCulture);
                            break;

                        case PilotChoiceEnum.REBEL_PILOT10:
                        case PilotChoiceEnum.REBEL_PILOT11:
                        case PilotChoiceEnum.REBEL_PILOT12:
                            ebp40_15 += "l" + ((int)esi1 - 0x09).ToString(CultureInfo.InvariantCulture);
                            break;
                    }

                    break;
                }
        }

        if (WAVListForm.ListBox1.SelectedIndex < 0)
        {
            WAVListForm.ListBox1.SelectedIndex = 0;
        }

        if (WAVListForm.ListBox1.Items.Count > 0)
        {
            VoiceTabsPageEnum eax1 = (VoiceTabsPageEnum)WAVListForm.VoiceTabs.SelectedIndex;

            switch (eax1)
            {
                case VoiceTabsPageEnum.TacticalOfficers:
                    {
                        int edx1 = WAVListForm.ListBox1.SelectedIndex;
                        AlliedVariables.s_V0x00542430 = ebp40_15 + AlliedVariables.s_V0x00542408.GetText(edx1) + ".wav";
                        break;
                    }

                case VoiceTabsPageEnum.Pilots:
                    {
                        if (WAVListForm.PilotChoice.GetItemIndex() <= 0x0B)
                        {
                            int edx1 = WAVListForm.ListBox1.SelectedIndex;
                            AlliedVariables.s_V0x00542430 = ebp40_15 + AlliedVariables.s_V0x00542414.GetText(edx1) + ".wav";
                        }
                        else
                        {
                            int edx1 = WAVListForm.ListBox1.SelectedIndex;
                            int edx2 = StrRec_try_to_int_L0051E3BC(AlliedVariables.s_V0x00542414.GetText(edx1));

                            int esi;

                            switch (edx2)
                            {
                                case 0x0C:
                                case 0x0D:
                                case 0x0E:
                                case 0x0F:
                                case 0x10:
                                case 0x11:
                                case 0x12:
                                case 0x13:
                                case 0x14:
                                case 0x15:
                                case 0x16:
                                case 0x17:
                                case 0x18:
                                case 0x19:
                                case 0x1A:
                                case 0x1B:
                                case 0x1C:
                                case 0x1D:
                                case 0x1E:
                                case 0x1F:
                                    esi = edx2 - 0x0B;
                                    break;

                                case 0x2B:
                                case 0x2C:
                                case 0x2D:
                                case 0x2E:
                                case 0x2F:
                                case 0x30:
                                case 0x31:
                                case 0x32:
                                case 0x33:
                                case 0x34:
                                case 0x35:
                                case 0x36:
                                case 0x37:
                                case 0x38:
                                case 0x39:
                                case 0x3A:
                                case 0x3B:
                                case 0x3C:
                                case 0x3D:
                                case 0x3E:
                                case 0x3F:
                                case 0x40:
                                case 0x41:
                                case 0x42:
                                case 0x43:
                                case 0x44:
                                case 0x45:
                                case 0x46:
                                case 0x47:
                                case 0x48:
                                case 0x49:
                                case 0x4A:
                                case 0x4B:
                                    esi = edx2 - 0x16;
                                    break;

                                case 0x4C:
                                    esi = 0x3B;
                                    break;

                                case 0x4D:
                                case 0x4E:
                                case 0x4F:
                                case 0x50:
                                case 0x51:
                                    esi = edx2 - 0x17;
                                    break;

                                case 0x52:
                                case 0x53:
                                case 0x54:
                                case 0x55:
                                case 0x56:
                                case 0x57:
                                case 0x58:
                                case 0x59:
                                case 0x5A:
                                case 0x5B:
                                case 0x5C:
                                case 0x5D:
                                case 0x5E:
                                case 0x5F:
                                case 0x60:
                                case 0x61:
                                case 0x62:
                                case 0x63:
                                case 0x64:
                                case 0x65:
                                case 0x66:
                                case 0x67:
                                case 0x68:
                                case 0x69:
                                case 0x6A:
                                case 0x6B:
                                case 0x6C:
                                case 0x6D:
                                case 0x6E:
                                case 0x6F:
                                case 0x70:
                                case 0x71:
                                case 0x72:
                                case 0x73:
                                case 0x74:
                                case 0x75:
                                case 0x76:
                                case 0x77:
                                case 0x78:
                                case 0x79:
                                    esi = edx2 - 0x16;
                                    break;

                                case 0x7A:
                                case 0x7B:
                                case 0x7C:
                                case 0x7D:
                                case 0x7E:
                                case 0x7F:
                                case 0x80:
                                case 0x81:
                                case 0x82:
                                case 0x83:
                                case 0x84:
                                case 0x85:
                                case 0x86:
                                case 0x87:
                                case 0x88:
                                case 0x89:
                                case 0x8A:
                                case 0x8B:
                                case 0x8C:
                                case 0x8D:
                                case 0x8E:
                                case 0x8F:
                                case 0x90:
                                case 0x91:
                                case 0x92:
                                    esi = edx2 - 0x17;
                                    break;

                                case 0x93:
                                case 0x94:
                                case 0x95:
                                case 0x96:
                                case 0x97:
                                case 0x98:
                                case 0x99:
                                case 0x9A:
                                case 0x9B:
                                case 0x9C:
                                case 0x9D:
                                case 0x9E:
                                case 0x9F:
                                case 0xA0:
                                case 0xA1:
                                case 0xA2:
                                case 0xA3:
                                case 0xA4:
                                case 0xA5:
                                case 0xA6:
                                case 0xA7:
                                case 0xA8:
                                case 0xA9:
                                case 0xAA:
                                case 0xAB:
                                case 0xAC:
                                case 0xAD:
                                case 0xAE:
                                case 0xAF:
                                case 0xB0:
                                case 0xB1:
                                case 0xB2:
                                case 0xB3:
                                case 0xB4:
                                case 0xB5:
                                case 0xB6:
                                case 0xB7:
                                case 0xB8:
                                    esi = edx2 - 0x16;
                                    break;

                                case 0xB9:
                                case 0xBA:
                                case 0xBB:
                                case 0xBC:
                                case 0xBD:
                                case 0xBE:
                                case 0xBF:
                                case 0xC0:
                                case 0xC1:
                                case 0xC2:
                                case 0xC3:
                                case 0xC4:
                                    esi = edx2 - 0x17;
                                    break;

                                case 0x112:
                                case 0x113:
                                    if (WAVListForm.PilotChoice.GetItemIndex() == 0x0C)
                                    {
                                        esi = edx2 - 0x64;
                                    }
                                    else
                                    {
                                        esi = 0;
                                    }

                                    break;

                                default:
                                    esi = 0;
                                    break;
                            }

                            string ebp40_13;

                            switch (esi)
                            {
                                case 0x00:
                                case 0x01:
                                case 0x02:
                                case 0x03:
                                case 0x04:
                                case 0x05:
                                case 0x06:
                                case 0x07:
                                case 0x08:
                                case 0x09:
                                    ebp40_13 = "00" + esi.ToString(CultureInfo.InvariantCulture);
                                    break;

                                case 0x0A:
                                case 0x0B:
                                case 0x0C:
                                case 0x0D:
                                case 0x0E:
                                case 0x0F:
                                case 0x10:
                                case 0x11:
                                case 0x12:
                                case 0x13:
                                case 0x14:
                                case 0x15:
                                case 0x16:
                                case 0x17:
                                case 0x18:
                                case 0x19:
                                case 0x1A:
                                case 0x1B:
                                case 0x1C:
                                case 0x1D:
                                case 0x1E:
                                case 0x1F:
                                case 0x20:
                                case 0x21:
                                case 0x22:
                                case 0x23:
                                case 0x24:
                                case 0x25:
                                case 0x26:
                                case 0x27:
                                case 0x28:
                                case 0x29:
                                case 0x2A:
                                case 0x2B:
                                case 0x2C:
                                case 0x2D:
                                case 0x2E:
                                case 0x2F:
                                case 0x30:
                                case 0x31:
                                case 0x32:
                                case 0x33:
                                case 0x34:
                                case 0x35:
                                case 0x36:
                                case 0x37:
                                case 0x38:
                                case 0x39:
                                case 0x3A:
                                case 0x3B:
                                case 0x3C:
                                case 0x3D:
                                case 0x3E:
                                case 0x3F:
                                case 0x40:
                                case 0x41:
                                case 0x42:
                                case 0x43:
                                case 0x44:
                                case 0x45:
                                case 0x46:
                                case 0x47:
                                case 0x48:
                                case 0x49:
                                case 0x4A:
                                case 0x4B:
                                case 0x4C:
                                case 0x4D:
                                case 0x4E:
                                case 0x4F:
                                case 0x50:
                                case 0x51:
                                case 0x52:
                                case 0x53:
                                case 0x54:
                                case 0x55:
                                case 0x56:
                                case 0x57:
                                case 0x58:
                                case 0x59:
                                case 0x5A:
                                case 0x5B:
                                case 0x5C:
                                case 0x5D:
                                case 0x5E:
                                case 0x5F:
                                case 0x60:
                                case 0x61:
                                case 0x62:
                                case 0x63:
                                    ebp40_13 = "0" + esi.ToString(CultureInfo.InvariantCulture);
                                    break;

                                default:
                                    ebp40_13 = esi.ToString(CultureInfo.InvariantCulture);
                                    break;
                            }

                            AlliedVariables.s_V0x00542430 = ebp40_15 + ebp40_13 + ".wav";
                        }

                        break;
                    }

                case VoiceTabsPageEnum.CD1:
                    {
                        int edx1 = WAVListForm.ListBox1.SelectedIndex;
                        AlliedVariables.s_V0x00542430 = ebp40_15 + AlliedVariables.s_V0x0054241C.GetText(edx1) + ".wav";
                        break;
                    }

                case VoiceTabsPageEnum.CD2:
                    {
                        int edx1 = WAVListForm.ListBox1.SelectedIndex;
                        AlliedVariables.s_V0x00542430 = ebp40_15 + AlliedVariables.s_V0x00542424.GetText(edx1) + ".wav";
                        break;
                    }

                case VoiceTabsPageEnum.Custom:
                    {
                        int edx1 = WAVListForm.ListBox1.SelectedIndex;
                        AlliedVariables.s_V0x00542430 = ebp40_15 + AlliedVariables.s_V0x0054242C.GetText(edx1) + ".wav";
                        break;
                    }
            }
        }

        string ebp40_14 = AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x00542430[2..];

        if (File.Exists(ebp40_14))
        {
            AlliedVariables.s_V0x00542430 = ebp40_14;
            AlliedVariables.s_V0x00542439 = 0x01;
        }

        if (File.Exists(AlliedVariables.s_V0x00542430))
        {
            AlliedVariables.s_AlliedForm1Window!.WAVplayer.SoundLocation = AlliedVariables.s_V0x00542430;

            if (WAVListForm.ListBox1.SelectedIndex >= 0 && WAVListForm.CheckBox1.IsChecked == true)
            {
                TWAVListForm_Proc_004D3BA0(WAVListForm);
            }
        }
    }

    // L004D3E58
    private static void TWAVListForm_ListBox1DblClick(WavListBox WAVListForm, object? Sender)
    {
        TWAVListForm_OKBtnClick(WAVListForm, WAVListForm.OKBtn);
    }

    // L004D3E64
    private static void TWAVListForm_Proc_004D3E64(WavListBox WAVListForm, int edx0, TStrings ecx0, TStrings A4)
    {
        string ebp20_6 = ecx0.GetText(edx0);

        if (!string.IsNullOrEmpty(ebp20_6) && ebp20_6[0] == ' ')
        {
            A4.Add(" ");
        }
        else if (!string.IsNullOrEmpty(ebp20_6) && ebp20_6[0] == '+')
        {
            A4.Add(A4.GetText(edx0 - 1));
            ecx0.Put(edx0, "              " + ebp20_6[1..]);
        }
        else if (string.IsNullOrEmpty(ebp20_6))
        {
            A4.Add(" ");
        }
        else
        {
            StringBuilder ebp20_5 = new();
            int edi = 0x01;

            while (true)
            {
                char bl = ebp20_6[edi - 1];

                if (bl == ' ')
                {
                    break;
                }

                ebp20_5.Append(System_LStrFromChar(bl));
                edi++;
            }

            A4.Add(ebp20_5.ToString());
            string ebp20_0 = ebp20_6[edi..];
            ecx0.Put(edx0, ebp20_0);
        }
    }

    // L004D40FC
    private static void TWAVListForm_ListBox1DrawItem(WavListBox WAVListForm, ListBox Sender, int index, object? A4, object? rect)
    {
        ListBoxItem item = Sender.GetItem(index);
        string ebp08 = WAVListForm.ListBox1.GetItemText(index);

        int eax2 = WAVListForm.VoiceTabs.SelectedIndex;

        if (eax2 >= 0x02 && eax2 < 0x05)
        {
            if (ebp08.Length > 0x03)
            {
                if (BtBitString((byte)ebp08[0], AlliedVariables.s_V0x00533B80))
                {
                    string ebp1C = System_LStrFromChar(ebp08[0]);
                    uint edx1 = AlliedGetIffColor((byte)StrRec_try_to_int_L0051E3BC(ebp1C), 0);
                    Graphics_TFont_SetColor(item, edx1);
                }
                else
                {
                    Graphics_TFont_SetColor(item, 0x00FFFFFF);
                }
            }
            else
            {
                Graphics_TFont_SetColor(item, 0x0000FF00);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(ebp08) && ebp08.IndexOf("    ") > 0)
            {
                Graphics_TFont_SetColor(item, 0x0000A800);
            }
            else if (!string.IsNullOrEmpty(ebp08) && ebp08[0] == ' ')
            {
                Graphics_TFont_SetColor(item, 0x00FFFFFF);
            }
            else
            {
                Graphics_TFont_SetColor(item, 0x0000FF00);
            }
        }
    }
}
