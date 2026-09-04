using AlliED.Extensions;
using Microsoft.Win32;
using SharpDialogs.Wpf;
using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class PreferencesWindowImpl
{
    public static void Register(PreferencesWindow window)
    {
        SetBindings(window);
    }

    private static void SetBindings(PreferencesWindow window)
    {
        window.Activated += (s, e) => TPrefForm_FormActivate(window);
        window.Closed += (s, e) => TPrefForm_FormClose(window);

        window.UseAutoChk.Click += (s, e) => TPrefForm_UseAutoChkClick(window);
        window.Button2.Click += (s, e) => TPrefForm_Button2Click(window);
        window.StartDirBrwsBut.Click += (s, e) => TPrefForm_StartDirBrwsButClick(window);
        window.XvTBrwsBut.Click += (s, e) => TPrefForm_XvTBrwsButClick(window);
        window.EDBrwsBut.Click += (s, e) => TPrefForm_EDBrwsButClick(window);
        window.Button1.Click += (s, e) => TPrefForm_Button1Click(window);
        window.OPTBrwsBtn.Click += (s, e) => TPrefForm_OPTBrwsBtnClick(window);
        window.Button3.Click += (s, e) => TPrefForm_Button3Click(window);
        window.Button4.Click += (s, e) => TPrefForm_Button4Click(window);
        window.Button5.Click += (s, e) => TPrefForm_Button5Click(window);
        window.Button6.Click += (s, e) => TPrefForm_Button6Click(window);
        window.ImportBtn.Click += (s, e) => TPrefForm_ImportBtnClick(window, (Button)s);
        window.Button7.Click += (s, e) => TPrefForm_ImportBtnClick(window, (Button)s);
        window.Button8.Click += (s, e) => TPrefForm_ImportBtnClick(window, (Button)s);
    }

    // L0050A34C
    private static void TPrefForm_FormActivate(PreferencesWindow PrefForm)
    {
        PrefForm.DefaultShipBox.SetItems(AlliedVariables.s_Strings_Short);
        Controls_TControl_SetText(PrefForm.StartDirLab, AlliedVariables.s_StartDirLabSetting);
        Controls_TControl_SetText(PrefForm.XWADirLab, AlliedVariables.s_XWADirLabSetting);
        Controls_TControl_SetText(PrefForm.AlliEDDirLab, AlliedVariables.s_AlliedDirectoryPath);
        Controls_TControl_SetText(PrefForm.XWDirLab, AlliedVariables.s_XWDirLabSetting);
        Controls_TControl_SetText(PrefForm.TFDirLab, AlliedVariables.s_TFDirLabSetting);
        Controls_TControl_SetText(PrefForm.XvTDirLab, AlliedVariables.s_XvTDirLabSetting);
        Controls_TControl_SetText(PrefForm.BoPDirLab, AlliedVariables.s_BoPDirLabSetting);
        Controls_TControl_SetText(PrefForm.WaveDirLab, AlliedVariables.s_WaveDirLabSetting);
        Controls_TControl_SetText(PrefForm.OPTDirLab, AlliedVariables.s_OPTDirLabSetting);
        PrefForm.UseAutoChk.IsChecked = AlliedVariables.s_UseAutoChkSetting;
        PrefForm.PlayableChk.IsChecked = AlliedVariables.s_PlayableChkSetting;
        PrefForm.ConfDeletesChk.IsChecked = AlliedVariables.s_ConfDeletesChkSetting;
        PrefForm.BlackenChk.IsChecked = AlliedVariables.s_BlackenChkSetting;
        PrefForm.EnableWPsChk.IsChecked = AlliedVariables.s_EnableWPsChkSetting;
        PrefForm.LinkColorChk.IsChecked = AlliedVariables.s_LinkColorChkSetting;
        PrefForm.ResChk.IsChecked = AlliedVariables.s_ResChkSetting;
        PrefForm.UseWizChk.IsChecked = AlliedVariables.s_UseWizChkSetting;
        PrefForm.Centering.IsChecked = AlliedVariables.s_CenteringSetting;
        ExtCtrls_TCustomRadioGroup_SetItemIndex(PrefForm.MissionFormatRadio, AlliedVariables.s_MissionFormatRadioIndexSetting);
        ExtCtrls_TCustomRadioGroup_SetItemIndex(PrefForm.OpenWithRadio, (int)AlliedVariables.s_OpenWithRadioIndexSetting);
        PrefForm.GhostMapChk.IsChecked = AlliedVariables.s_GhostMapChkSetting;
        PrefForm.SSD17chk.IsChecked = AlliedVariables.s_SSD17chkSetting;
        PrefForm.DirInHistChk.IsChecked = AlliedVariables.s_DirInHistChkSetting;
        ExtCtrls_TCustomRadioGroup_SetItemIndex(PrefForm.WPsDefaultRadio, (int)AlliedVariables.s_WPsDefaultRadioIndexSetting);
        ExtCtrls_TCustomRadioGroup_SetItemIndex(PrefForm.AutoCtrGrp, (int)AlliedVariables.s_AutoCtrGrpIndexSetting);
        StdCtrls_TScrollBar_SetPosition(PrefForm.ZoomSpeedScroll, AlliedVariables.s_ZoomSpeedScrollPositionSetting);
        ExtCtrls_TCustomRadioGroup_SetItemIndex(PrefForm.DblClickLinkRadio, AlliedVariables.s_DblClickLinkRadioIndexSetting);
        PrefForm.NamesOnChk.IsChecked = AlliedVariables.s_NamesOnChkSetting;
        PrefForm.DefaultPalletOnChk.IsChecked = AlliedVariables.s_DefaultPalletOnChkSetting;
        PrefForm.DefaultHypOnChk.IsChecked = AlliedVariables.s_DefaultHypOnChkSetting;
        PrefForm.DefaultOptionsOnChk.IsChecked = AlliedVariables.s_DefaultOptionsOnChkSetting;
        PrefForm.LimitShrinkChk.IsChecked = AlliedVariables.s_LimitShrinkChkSetting;
        PrefForm.DarkGridChk.IsChecked = AlliedVariables.s_DarkGridChkSetting;
        PrefForm.OnlyXYChk.IsChecked = AlliedVariables.s_OnlyXYChkSetting;
        Controls_TControl_SetText(PrefForm.IconZoomEdit, AlliedVariables.s_IconZoomEditSetting.ToString(CultureInfo.InvariantCulture));
        PrefForm.DefaultShipBox.SelectedIndex = (int)AlliedVariables.s_DefaultShipBoxSettingIndex;
        PrefForm.DefaultAIBox.SelectedIndex = AlliedVariables.s_DefaultAIBoxSettingIndex;
        PrefForm.ConfSaveChk.IsChecked = AlliedVariables.s_ConfSaveChkSetting;
        PrefForm.DoBackupsChk.IsChecked = AlliedVariables.s_DoBackupsChkSetting;
        PrefForm.CheckBox1.IsChecked = AlliedVariables.s_V0x00543C84.M000000[0].M000000;
        PrefForm.CheckBox2.IsChecked = AlliedVariables.s_V0x00543C84.M000000[1].M000000;
        PrefForm.CheckBox3.IsChecked = AlliedVariables.s_V0x00543C84.M000000[2].M000000;

        if (string.Equals(AlliedVariables.s_StartDirLabSetting, "Undefined", StringComparison.Ordinal))
        {
            AlliedVariables.s_StartDirLabSetting = "C:";
        }

        if (string.Equals(AlliedVariables.s_XWADirLabSetting, "Undefined", StringComparison.Ordinal))
        {
            AlliedVariables.s_XWADirLabSetting = "C:";
        }

        if (string.Equals(AlliedVariables.s_AlliedDirectoryPath, "Undefined", StringComparison.Ordinal))
        {
            AlliedVariables.s_AlliedDirectoryPath = "C:";
        }

        if (string.Equals(AlliedVariables.s_WaveDirLabSetting, "Undefined", StringComparison.Ordinal))
        {
            AlliedVariables.s_WaveDirLabSetting = AlliedVariables.s_XWADirLabSetting + "\\wave\\";
        }
    }

    // L0050ABDC
    private static void TPrefForm_UseAutoChkClick(PreferencesWindow PrefForm)
    {
        if (PrefForm.UseAutoChk.IsChecked == true)
        {
            AlliedVariables.s_UseAutoChkSetting = true;
        }
        else
        {
            AlliedVariables.s_UseAutoChkSetting = false;
        }

        int ebx0 = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int esi = 0; esi < ebx0; esi++)
        {
            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
            eax.AutoLink = AlliedVariables.s_UseAutoChkSetting;
        }

        if (AlliedVariables.s_V0x005B6D16 != 0)
        {
            Buttons_TSpeedButton_SetDown(AlliedVariables.s_TShipExt_Instance!.AutoLinkChk, AlliedVariables.s_UseAutoChkSetting);
        }
    }

    // L0050AFEC
    private static void TPrefForm_Button2Click(PreferencesWindow PrefForm)
    {
        Unit_00507130_Proc_0050992C();
        AlliedVariables.s_V0x00543948 = 0x01;
    }

    // L0050992C
    private static void Unit_00507130_Proc_0050992C()
    {
        using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
        baseKey.DeleteSubKeyTree("SOFTWARE\\Troy's Editors\\AlliED", false);
    }

    // L0050AE40
    private static string TPrefForm_ChosenDir(PreferencesWindow PrefForm, string edx0, TextBlock ecx0)
    {
        //string ebp0C = AlliedVariables.s_XWADirLabSetting;

        string initialDirectory;

        if (Directory.Exists(edx0))
        {
            initialDirectory = edx0;
        }
        else
        {
            initialDirectory = "C:\\";
        }

        string? directory = SharpFolderBrowserDialogWpf.ShowSingleSelect(PrefForm, null, initialDirectory);

        if (directory is null)
        {
            return edx0;
        }

        Controls_TControl_SetText(ecx0, directory);
        return directory;
    }

    // L0050AC60
    private static void TPrefForm_StartDirBrwsButClick(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_StartDirLabSetting, PrefForm.StartDirLab);
        AlliedVariables.s_StartDirLabSetting = directory;
    }

    // L0050ACC0
    private static void TPrefForm_XvTBrwsButClick(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_XWADirLabSetting, PrefForm.XWADirLab);
        AlliedVariables.s_XWADirLabSetting = directory;
    }

    // L0050AD20
    private static void TPrefForm_EDBrwsButClick(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_AlliedDirectoryPath, PrefForm.AlliEDDirLab);
        AlliedVariables.s_AlliedDirectoryPath = directory;
    }

    // L0050AD80
    private static void TPrefForm_Button1Click(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_WaveDirLabSetting, PrefForm.WaveDirLab);
        AlliedVariables.s_WaveDirLabSetting = directory;
    }

    // L0050ADE0
    private static void TPrefForm_OPTBrwsBtnClick(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_OPTDirLabSetting, PrefForm.OPTDirLab);
        AlliedVariables.s_OPTDirLabSetting = directory;
    }

    // L0050AFFC
    private static void TPrefForm_Button3Click(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_XWDirLabSetting, PrefForm.XWDirLab);
        AlliedVariables.s_XWDirLabSetting = directory;
    }

    // L0050B05C
    private static void TPrefForm_Button4Click(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_TFDirLabSetting, PrefForm.TFDirLab);
        AlliedVariables.s_TFDirLabSetting = directory;
    }

    // L0050B0BC
    private static void TPrefForm_Button5Click(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_XvTDirLabSetting, PrefForm.XvTDirLab);
        AlliedVariables.s_XvTDirLabSetting = directory;
    }

    // L0050B4DC
    private static void TPrefForm_Button6Click(PreferencesWindow PrefForm)
    {
        string directory = TPrefForm_ChosenDir(PrefForm, AlliedVariables.s_BoPDirLabSetting, PrefForm.BoPDirLab);
        AlliedVariables.s_BoPDirLabSetting = directory;
    }

    // L0050B4D0
    private static void TPrefForm_ImportBtnClick(PreferencesWindow PrefForm, Button Button)
    {
        TPrefForm__PROC_0050B11C(PrefForm, (MissionImportTypeEnum)Convert.ToInt32(Button.Tag));
    }

    // L0050B11C
    private static void TPrefForm__PROC_0050B11C(PreferencesWindow PrefForm, MissionImportTypeEnum edx0)
    {
        int ebp01 = (int)edx0;
        string ebp08 = string.Empty;

        switch (edx0)
        {
            case MissionImportTypeEnum.XWing:
                ebp08 = "X-Wing";
                break;

            case MissionImportTypeEnum.TieFighter:
                ebp08 = "T/F";
                break;

            case MissionImportTypeEnum.XvT_BoP:
                ebp08 = "XvT/BoP";
                break;
        }

        ImportBox s_TImportForm = new()
        {
            Owner = PrefForm
        };

        Controls_TControl_SetText(s_TImportForm, Controls_TControl_GetText(s_TImportForm) + " for " + ebp08);
        s_TImportForm.ComboBox2.SetItems(s_TImportForm.ComboBox1.Items);
        s_TImportForm.ComboBox3.SetItems(s_TImportForm.ComboBox1.Items);
        s_TImportForm.ComboBox4.SetItems(s_TImportForm.ComboBox1.Items);
        s_TImportForm.ComboBox5.SetItems(s_TImportForm.ComboBox1.Items);

        s_TImportForm.ComboBox1.SelectedIndex = AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000001;
        s_TImportForm.ComboBox2.SelectedIndex = AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000002;
        s_TImportForm.ComboBox3.SelectedIndex = AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000003;
        s_TImportForm.ComboBox4.SelectedIndex = AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000004;
        s_TImportForm.ComboBox5.SelectedIndex = AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000005;

        s_TImportForm.CheckBox6.IsChecked = AlliedVariables.s_V0x00543C84.M000000[0x02].M000006;

        if (edx0 == MissionImportTypeEnum.XvT_BoP)
        {
            s_TImportForm.CheckBox6.IsEnabled = true;
        }

        if (s_TImportForm.ShowDialog() == true)
        {
            AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000001 = (byte)s_TImportForm.ComboBox1.SelectedIndex;
            AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000002 = (byte)s_TImportForm.ComboBox2.SelectedIndex;
            AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000003 = (byte)s_TImportForm.ComboBox3.SelectedIndex;
            AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000004 = (byte)s_TImportForm.ComboBox4.SelectedIndex;
            AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000005 = (byte)s_TImportForm.ComboBox5.SelectedIndex;
            AlliedVariables.s_V0x00543C84.M000000[ebp01 - 1].M000006 = s_TImportForm.CheckBox6.IsChecked == true;
        }
    }

    // L0050A7D8
    private static void TPrefForm_FormClose(PreferencesWindow PrefForm)
    {
        if (PrefForm.DialogResult != true)
        {
            return;
        }

        AlliedVariables.s_StartDirLabSetting = Controls_TControl_GetText(PrefForm.StartDirLab);
        AlliedVariables.s_XWADirLabSetting = Controls_TControl_GetText(PrefForm.XWADirLab);
        AlliedVariables.s_AlliedDirectoryPath = Controls_TControl_GetText(PrefForm.AlliEDDirLab);
        AlliedVariables.s_WaveDirLabSetting = Controls_TControl_GetText(PrefForm.WaveDirLab);
        AlliedVariables.s_OPTDirLabSetting = Controls_TControl_GetText(PrefForm.OPTDirLab);

        AlliedVariables.s_UseAutoChkSetting = PrefForm.UseAutoChk.IsChecked == true;
        AlliedVariables.s_ConfDeletesChkSetting = PrefForm.ConfDeletesChk.IsChecked == true;
        AlliedVariables.s_BlackenChkSetting = PrefForm.BlackenChk.IsChecked == true;
        AlliedVariables.s_EnableWPsChkSetting = PrefForm.EnableWPsChk.IsChecked == true;
        AlliedVariables.s_LinkColorChkSetting = PrefForm.LinkColorChk.IsChecked == true;
        AlliedVariables.s_ResChkSetting = PrefForm.ResChk.IsChecked == true;
        AlliedVariables.s_UseWizChkSetting = PrefForm.UseWizChk.IsChecked == true;
        AlliedVariables.s_CenteringSetting = PrefForm.Centering.IsChecked == true;
        AlliedVariables.s_MissionFormatRadioIndexSetting = (byte)PrefForm.MissionFormatRadio.GetItemIndex();
        AlliedVariables.s_OpenWithRadioIndexSetting = (OpenWithRadioEnum)PrefForm.OpenWithRadio.GetItemIndex();
        AlliedVariables.s_PlayableChkSetting = PrefForm.PlayableChk.IsChecked == true;
        AlliedVariables.s_GhostMapChkSetting = PrefForm.GhostMapChk.IsChecked == true;
        AlliedVariables.s_SSD17chkSetting = PrefForm.SSD17chk.IsChecked == true;
        AlliedVariables.s_DirInHistChkSetting = PrefForm.DirInHistChk.IsChecked == true;
        AlliedVariables.s_WPsDefaultRadioIndexSetting = (WPsDefaultRadioEnum)PrefForm.WPsDefaultRadio.GetItemIndex();
        AlliedVariables.s_AutoCtrGrpIndexSetting = (AutoCtrGrpEnum)PrefForm.AutoCtrGrp.GetItemIndex();
        AlliedVariables.s_ZoomSpeedScrollPositionSetting = (byte)PrefForm.ZoomSpeedScroll.Value;
        AlliedVariables.s_DblClickLinkRadioIndexSetting = PrefForm.DblClickLinkRadio.GetItemIndex();
        AlliedVariables.s_NamesOnChkSetting = PrefForm.NamesOnChk.IsChecked == true;
        AlliedVariables.s_DefaultPalletOnChkSetting = PrefForm.DefaultPalletOnChk.IsChecked == true;
        AlliedVariables.s_DefaultHypOnChkSetting = PrefForm.DefaultHypOnChk.IsChecked == true;
        AlliedVariables.s_DefaultOptionsOnChkSetting = PrefForm.DefaultOptionsOnChk.IsChecked == true;
        AlliedVariables.s_LimitShrinkChkSetting = PrefForm.LimitShrinkChk.IsChecked == true;
        AlliedVariables.s_DarkGridChkSetting = PrefForm.DarkGridChk.IsChecked == true;
        AlliedVariables.s_OnlyXYChkSetting = PrefForm.OnlyXYChk.IsChecked == true;

        string ebp18_0 = Controls_TControl_GetText(PrefForm.IconZoomEdit);
        int eax1 = int.Parse(ebp18_0);

        if (eax1 < 0x04)
        {
            eax1 = 0x04;
        }
        else if (eax1 > 0x7D0)
        {
            eax1 = 0x7D0;
        }

        AlliedVariables.s_IconZoomEditSetting = eax1;

        AlliedVariables.s_DefaultShipBoxSettingIndex = (CraftIdEnum)PrefForm.DefaultShipBox.SelectedIndex;

        if (AlliedVariables.s_DefaultShipBoxSettingIndex < CraftIdEnum._000__1_0)
        {
            AlliedVariables.s_DefaultShipBoxSettingIndex = CraftIdEnum._001_0_0_Xwing;
        }

        AlliedVariables.s_V0x005B5C74.CraftId = AlliedVariables.s_DefaultShipBoxSettingIndex;

        AlliedVariables.s_DefaultAIBoxSettingIndex = PrefForm.DefaultAIBox.SelectedIndex;
        AlliedVariables.s_V0x005B5C74.AIRank = (byte)AlliedVariables.s_DefaultAIBoxSettingIndex;

        AlliedVariables.s_ConfSaveChkSetting = PrefForm.ConfSaveChk.IsChecked == true;
        AlliedVariables.s_DoBackupsChkSetting = PrefForm.DoBackupsChk.IsChecked == true;
        AlliedVariables.s_V0x00543C84.M000000[0].M000000 = PrefForm.CheckBox1.IsChecked == true;
        AlliedVariables.s_V0x00543C84.M000000[1].M000000 = PrefForm.CheckBox2.IsChecked == true;
        AlliedVariables.s_V0x00543C84.M000000[2].M000000 = PrefForm.CheckBox3.IsChecked == true;
    }
}
