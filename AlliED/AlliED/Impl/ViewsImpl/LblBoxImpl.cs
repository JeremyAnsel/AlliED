using AlliED.Extensions;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class LblBoxImpl
{
    public static void Register(LblBox window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(LblBox window)
    {
        window.Closed += (s, e) => TLBLForm_FormClose(window, null, null);
        window.TabControl1.SelectionChanged += (s, e) => TLBLForm_TabControl1Change(window, s);
        window.CancelBtn.Click += (s, e) => TLBLForm_CancelBtnClick(window);
        window.OKBtn.Click += (s, e) => TLBLForm_OKBtnClick(window);
        window.ListBox1.DrawItem += (sender, index) => TLBLForm_ListBox1DrawItem(window, sender, index, null, null);
        window.ListBox1.SelectionChanged += (s, e) => TLBLForm_ListBox1Click(window);
        window.ListBox1.MouseDoubleClick += (s, e) => TLBLForm_OKBtnClick(window);
    }

    // L004C7144
    private static void FormCreate(LblBox lblForm)
    {
        if (TApplication_GetWidth() == 0x280 || TApplication_GetHeight() == 0x1E0)
        {
            Graphics_TFont_SetName(lblForm, "MS Serif");
            Graphics_TFont_SetSize(lblForm, 0x09);
            TApplication_L00468080(lblForm, 0x01);
            Controls_TWinControl_ScaleBy(lblForm, TApplication_GetWidth(), 0x32A);
            TApplication_RecreateWnd(lblForm, 0x04);
        }

        if (Application_GetPixelsPerInch() == 0x78)
        {
            StdCtrls_TCustomListBox_SetItemHeight(lblForm.ListBox1, 0x10);
        }

        AlliedVariables.s_V0x0053BD68 = new();
        AlliedVariables.s_V0x0053BD6C = new();
        AlliedVariables.s_V0x0053BD70 = new();

        TLBLForm_TabControl1Change(lblForm, lblForm.TabControl1);
        lblForm.ListBox1.SelectedIndex = 0;
    }

    // L004C7678
    private static void TLBLForm_TabControl1Change(LblBox lblForm, object? edx0)
    {
        LblFormPageEnum eax1 = (LblFormPageEnum)lblForm.TabControl1.SelectedIndex;

        switch (eax1)
        {
            case LblFormPageEnum.Campaign:
                AlliedVariables.s_V0x0053BD78 = "\\Missions";
                break;

            case LblFormPageEnum.Melee:
                AlliedVariables.s_V0x0053BD78 = "\\Melee";
                break;
        }

        string ebp0C_2 = AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x0053BD78 + "\\Mission.lst";

        if (File.Exists(ebp0C_2))
        {
            string ebp0C_1 = AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x0053BD78 + "\\Mission.lst";
            AlliedVariables.s_V0x0053BD6C.LoadFromFile(ebp0C_1);
            TLBLForm_Proc_004C7238(lblForm);
        }
        else
        {
            string ebp0C_0 = "Can't find " + AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x0053BD78 + "\\Mission.lst";
            MessageBox_ShowError(ebp0C_0);
        }
    }

    // L004C7238
    private static void TLBLForm_Proc_004C7238(LblBox lblForm)
    {
        lblForm.ListBox1.Clear();
        AlliedVariables.s_V0x0053BD68.Clear();
        AlliedVariables.s_V0x0053BD70.Clear();

        int ebx0 = AlliedVariables.s_V0x0053BD6C.GetCount();

        for (int esi = 0; esi < ebx0; esi++)
        {
            string ebp24_7 = AlliedVariables.s_V0x0053BD6C.GetText(esi);

            if (ebp24_7.ToUpperInvariant().IndexOf("//") > 0)
            {
                AlliedVariables.s_V0x0053BD70.Add(string.Empty);
                AlliedVariables.s_V0x0053BD68.Add("----");
            }
            else if (ebp24_7.IndexOf("[") > 0 && ebp24_7.IndexOf("]") > 0)
            {
                string ebp24_5 = TLBLForm_Proc_004C7868(lblForm, ebp24_7);
                AlliedVariables.s_V0x0053BD70.Add(ebp24_5);
                AlliedVariables.s_V0x0053BD68.Add("----");
            }
            else
            {
                if (ebp24_7.ToUpperInvariant().IndexOf(".TIE") > 0 && esi < AlliedVariables.s_V0x0053BD6C.GetCount())
                {
                    AlliedVariables.s_V0x0053BD70.Add(Unit_00513838_Proc_0051F4B8(AlliedVariables.s_V0x0053BD6C.GetText(esi + 1)));
                    AlliedVariables.s_V0x0053BD68.Add(AlliedVariables.s_V0x0053BD6C.GetText(esi));
                }
            }
        }

        if (string.IsNullOrEmpty(AlliedVariables.s_V0x0053BD70.GetText(0)))
        {
            AlliedVariables.s_V0x0053BD70.Delete(0);
            AlliedVariables.s_V0x0053BD68.Delete(0);
        }

        StdCtrls_TCustomListBox_SetItems(lblForm.ListBox1, AlliedVariables.s_V0x0053BD70.Items);
    }

    // L004C7868
    private static string TLBLForm_Proc_004C7868(LblBox lblForm, string edx0)
    {
        string ebp04 = edx0;
        System_LStrDelete(ref ebp04, 0x01, ebp04.IndexOf("HEADER!") + 0x06 + 1);
        return ebp04;
    }

    // L0051F4B8
    public static string Unit_00513838_Proc_0051F4B8(string eax0)
    {
        string ebp04 = eax0;
        System_LStrDelete(ref ebp04, 0x01, ebp04.IndexOf("DESC!") + 0x04 + 1);
        return ebp04;
    }

    // L004C7CC0
    private static void TLBLForm_CancelBtnClick(LblBox lblForm)
    {
    }

    // L004C78E4
    private static void TLBLForm_OKBtnClick(LblBox lblForm)
    {
        //Buttons_L00467140_SetVisible(AlliedVariables.s_TLBLForm_Instance!, false);

        string ebp34_11 = TLBLForm_Proc_004C77CC(lblForm, AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex));
        string ebp34_12 = AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x0053BD78 + "\\" + ebp34_11;
        string ebp34_9 = AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex);

        if (!string.Equals(ebp34_9, "----", StringComparison.Ordinal) && !string.Equals(AlliedVariables.s_V0x00543BF8, ebp34_11, StringComparison.Ordinal))
        {
            AlliedVariables.s_V0x00543BF8 = AlliedVariables.s_V0x0053BD74 + AlliedVariables.s_V0x0053BD78 + "\\" + TLBLForm_Proc_004C77CC(lblForm, AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex));

            if (File.Exists(AlliedVariables.s_V0x00543BF8))
            {
                AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
                TForm1_ReadTieMission(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x00543BF8);
                AlliedVariables.s_V0x00543B50 = 0x01;
            }
            else
            {
                string ebp34_6 = AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x0053BD78 + "\\" + TLBLForm_Proc_004C77CC(lblForm, AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex));

                if (File.Exists(ebp34_6))
                {
                    AlliedVariables.s_V0x00543BF8 = AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x0053BD78 + "\\" + TLBLForm_Proc_004C77CC(lblForm, AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex));
                    AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
                    TForm1_ReadTieMission(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x00543BF8);
                    AlliedVariables.s_V0x00543B50 = 0x01;
                }
                else
                {
                    MessageBox_ShowError(AlliedVariables.s_V0x00543BF8 + " does not exist!");
                }
            }
        }

        if (!string.Equals(AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex), "----", StringComparison.Ordinal))
        {
            AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
        }
    }

    // L004C77CC
    private static string TLBLForm_Proc_004C77CC(LblBox lblForm, string edx0)
    {
        string ebp04 = edx0;

        if (ebp04.IndexOf(" ") - 1 < 0)
        {
            return ebp04;
        }

        int eax1 = ebp04.Length;

        while (true)
        {
            eax1--;

            if (eax1 == 0x01)
            {
                break;
            }

            if (ebp04[eax1 - 1] == ' ')
            {
                break;
            }
        }

        System_LStrDelete(ref ebp04, 0x01, eax1);
        return ebp04;
    }

    // L004C7458
    private static void TLBLForm_ListBox1DrawItem(LblBox lblForm, ListBox Sender, int index, object? A4, object? rect)
    {
        if (index < AlliedVariables.s_V0x0053BD70.GetCount())
        {
            string ebp1C_1 = AlliedVariables.s_V0x0053BD70.GetText(index);
            ListBoxItem item = lblForm.ListBox1.GetItem(index);

            if (ebp1C_1.IndexOf("[") >= 0)
            {
                item.FontWeight = FontWeights.Bold;
            }
            else
            {
                item.FontWeight = FontWeights.Normal;
            }
        }
    }

    // L004C7574
    private static void TLBLForm_ListBox1Click(LblBox lblForm)
    {
        string str = AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex);
        string ebp10_3 = TLBLForm_Proc_004C77CC(lblForm, str);
        Controls_TControl_SetText(lblForm, "Load by .lst - [" + ebp10_3 + "]");

        if (lblForm.BrowseChk.IsChecked == true)
        {
            Unit_00513838_Proc_00520A40(AlliedVariables.s_XWADirLabSetting + AlliedVariables.s_V0x0053BD78 + "\\" + ebp10_3);
        }
    }

    // L004C7BCC
    private static void TLBLForm_FormClose(LblBox lblForm, object? edx0, object? ecx0)
    {
        Form1WindowImpl.TForm1_L0052BD20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TLBLForm_Instance!, AlliedVariables.s_V0x005AFC74.m00003C);
        AlliedVariables.s_V0x005AFC74.m00003C.m000008 = lblForm.BrowseChk.IsChecked == true ? (byte)1 : (byte)0;

        if (lblForm.BrowseChk.IsChecked == true /*&& lblForm.DialogResult != 0x02*/)
        {
            string ebp04 = AlliedVariables.s_V0x0053BD68.GetText(lblForm.ListBox1.SelectedIndex);

            if (!string.Equals(ebp04, "----", StringComparison.Ordinal))
            {
                AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
            }
        }

        Menus_TMenuItem_SetEnabled(AlliedVariables.s_AlliedForm1Window!.Loadbylst2, true);

        //*ecx0 = 0x02;

        // L004C7CC8
        AlliedVariables.s_V0x0053BD78 = string.Empty;
        AlliedVariables.s_V0x0053BD74 = string.Empty;
    }

    // L00520A40
    private static void Unit_00513838_Proc_00520A40(string str)
    {
        bool bl = false;

        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            if (Form1WindowImpl.Unit_00513838_Proc_0051710C())
            {
                TModalResultEnum ax0 = MessageBox_ShowConfirmation("Current Mission has been changed. Save it?", "cancel");

                if (ax0 == TModalResultEnum.Yes)
                {
                    if (AlliedVariables.s_V0x00543B50 != 0)
                    {
                        Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0x01, 0);
                    }
                    else
                    {
                        Form1WindowImpl.TForm1_SaveAsBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.SaveAsBtn);
                    }

                    Form1WindowImpl.Unit_00513838_Proc_00518694();
                    bl = true;
                }
                else if (ax0 == TModalResultEnum.No)
                {
                    Form1WindowImpl.Unit_00513838_Proc_00518694();
                    bl = true;
                }
            }
            else
            {
                Form1WindowImpl.Unit_00513838_Proc_00518694();
                bl = true;
            }
        }

        if (bl && File.Exists(str))
        {
            AlliedVariables.s_V0x00543BF8 = str;
            TForm1_ReadTieMission(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x00543BF8);
        }
    }
}
