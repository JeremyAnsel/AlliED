using AlliED.Extensions;
using System.Globalization;
using System.IO;
using System.Text;

namespace AlliED.Impl.ViewsImpl;

internal static class LstWindowImpl
{
    public static void Register(LstWindow window)
    {
        SetBindings(window);
        FormActivate(window);
    }

    private static void SetBindings(LstWindow window)
    {
        window.PageControl1.SelectionChanged += (s, e) => TlstForm_PageControl1Change(window);
        window.AutoAddBut.Click += (s, e) => TlstForm_AutoAddButClick(window);
        window.BackupLstBtn.Click += (s, e) => TlstForm_BackupLstBtnClick(window);
        window.OKBtn.Click += (s, e) => TlstForm_OKBtnClick(window);
        window.Button1.Click += (s, e) => TlstForm_Button1Click(window);
    }

    // L004F91E4
    private static void FormActivate(LstWindow LstForm)
    {
        string ebp04 = AlliedVariables.s_XWADirLabSetting + "\\Missions\\Mission.lst";

        if (File.Exists(ebp04))
        {
            LstForm.MissionLstMemo.LoadFromFile(ebp04);
        }
        else
        {
            MessageBox_ShowError("Can't find " + ebp04);
        }

        ebp04 = AlliedVariables.s_XWADirLabSetting + "\\Melee\\Mission.lst";

        if (File.Exists(ebp04))
        {
            LstForm.MeleeLstMemo.LoadFromFile(ebp04);
        }
        else
        {
            MessageBox_ShowError("Can't find " + ebp04);
        }

        AlliedVariables.s_V0x00542470 = 0;
        int ebp0Ca = LstForm.MissionLstMemo.Items.Count;

        for (int ebx = 0; ebx < ebp0Ca; ebx++)
        {
            if (!Unit_00513838_Proc_0051BF88(LstForm.MissionLstMemo.GetItemText(ebx)))
            {
                continue;
            }

            if (Allied_StrRec_to_int(LstForm.MissionLstMemo.GetItemText(ebx)) > AlliedVariables.s_V0x00542470)
            {
                AlliedVariables.s_V0x00542470 = (short)Allied_StrRec_to_int(LstForm.MissionLstMemo.GetItemText(ebx));
            }
        }

        bool ebp05 = false;
        int ebp0Cb = LstForm.MeleeLstMemo.Items.Count;

        for (int ebx = 0; ebx < ebp0Cb; ebx++)
        {
            if (!Unit_00513838_Proc_0051BF88(LstForm.MeleeLstMemo.GetItemText(ebx)))
            {
                continue;
            }

            if (Allied_StrRec_to_int(LstForm.MeleeLstMemo.GetItemText(ebx)) == AlliedVariables.s_V0x00542470)
            {
                ebp05 = true;
            }
        }

        if (ebp05)
        {
            int ebp0Cc = LstForm.MeleeLstMemo.Items.Count;

            for (int ebx = 0; ebx < ebp0Cc; ebx++)
            {
                if (!Unit_00513838_Proc_0051BF88(LstForm.MeleeLstMemo.GetItemText(ebx)))
                {
                    continue;
                }

                if (Allied_StrRec_to_int(LstForm.MeleeLstMemo.GetItemText(ebx)) > AlliedVariables.s_V0x00542470)
                {
                    AlliedVariables.s_V0x00542470 = (short)Allied_StrRec_to_int(LstForm.MeleeLstMemo.GetItemText(ebx));
                }
            }
        }

        AlliedVariables.s_V0x00542470 += 1;
        Controls_TControl_SetText(LstForm.LstNum, AlliedVariables.s_V0x00542470.ToString(CultureInfo.InvariantCulture));
        string ebp3C_0 = string.Format(CultureInfo.InvariantCulture, "My mission {0}", AlliedVariables.s_V0x00542470);
        Controls_TControl_SetText(LstForm.Edit1, ebp3C_0);
        Controls_TControl_SetText(LstForm.Edit2, Path.GetFileName(AlliedVariables.s_V0x00543BF8));
        AlliedVariables.s_V0x00542472 = 0;

        int ebp0Cd = LstForm.MissionLstMemo.Items.Count;

        for (int ebx = 0; ebx < ebp0Cd; ebx++)
        {
            if (LstForm.MissionLstMemo.GetItemText(ebx).IndexOf("BATTLE_") <= 0)
            {
                continue;
            }

            int eax = TlstForm_Proc_004F9CD4(LstForm, LstForm.MissionLstMemo.GetItemText(ebx));

            if (eax > AlliedVariables.s_V0x00542472)
            {
                AlliedVariables.s_V0x00542472 = (short)eax;
            }
        }

        AlliedVariables.s_V0x00542472 += 1;
        Controls_TControl_SetText(LstForm.BattleNum, AlliedVariables.s_V0x00542472.ToString(CultureInfo.InvariantCulture));
        string ebp58_0 = string.Format(CultureInfo.InvariantCulture, "Battle {0}: ", Controls_TControl_GetText(LstForm.BattleNum));
        Controls_TControl_SetText(LstForm.BattleDesc, ebp58_0);
    }

    // L004F9D90
    private static void TlstForm_PageControl1Change(LstWindow LstForm)
    {
        switch ((LstFormPageControlEnum)Convert.ToInt32(LstForm.PageControl1.GetActivePage().Tag))
        {
            case LstFormPageControlEnum.Missions:
                LstForm.Button1.IsEnabled = true;
                LstForm.AutoAddBut.IsEnabled = true;
                break;

            case LstFormPageControlEnum.Melee:
                LstForm.Button1.IsEnabled = false;
                LstForm.AutoAddBut.IsEnabled = false;
                break;
        }
    }

    // L004F9674
    private static void TlstForm_AutoAddButClick(LstWindow LstForm)
    {
        int esi = LstForm.MissionLstMemo.Items.Count;
        //string ebp04 = "\r";

        if (string.IsNullOrEmpty(Controls_TControl_GetText(LstForm.Edit1)))
        {
            MessageBox_ShowError("You must write a description in the Mission Description box");
        }
        else
        {
            switch ((LstFormPageControlEnum)Convert.ToInt32(LstForm.PageControl1.GetActivePage().Tag))
            {
                case LstFormPageControlEnum.Missions:
                    {
                        string ebp0C = string.Format(CultureInfo.InvariantCulture, "!MISSION_{0}_DESC!{1}", Controls_TControl_GetText(LstForm.LstNum), Controls_TControl_GetText(LstForm.Edit1));
                        LstForm.MissionLstMemo.InsertItem(esi - 1, ebp0C);
                        LstForm.MissionLstMemo.InsertItem(esi - 1, "* " + Controls_TControl_GetText(LstForm.Edit2));
                        LstForm.MissionLstMemo.InsertItem(esi - 1, Controls_TControl_GetText(LstForm.LstNum));
                        break;
                    }

                case LstFormPageControlEnum.Melee:
                    {
                        LstForm.MeleeLstMemo.InsertItem(esi - 1, Controls_TControl_GetText(LstForm.LstNum));
                        LstForm.MeleeLstMemo.InsertItem(esi - 1, Controls_TControl_GetText(LstForm.Edit2));
                        LstForm.MeleeLstMemo.InsertItem(esi - 1, Controls_TControl_GetText(LstForm.Edit1));
                        break;
                    }
            }
        }

        AlliedVariables.s_V0x00542470 += 1;
        Controls_TControl_SetText(LstForm.LstNum, AlliedVariables.s_V0x00542470.ToString(CultureInfo.InvariantCulture));
        string ebp44_0 = string.Format(CultureInfo.InvariantCulture, "My mission {0}", AlliedVariables.s_V0x00542470.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(LstForm.Edit1, ebp44_0);
    }

    // L004F9968
    private static void TlstForm_BackupLstBtnClick(LstWindow LstForm)
    {
        switch ((LstFormPageControlEnum)Convert.ToInt32(LstForm.PageControl1.GetActivePage().Tag))
        {
            case LstFormPageControlEnum.Missions:
                LstForm.MissionLstMemo.SaveToFile(AlliedVariables.s_XWADirLabSetting + "\\Missions\\Mission.~ls");
                break;

            case LstFormPageControlEnum.Melee:
                LstForm.MeleeLstMemo.SaveToFile(AlliedVariables.s_XWADirLabSetting + "\\Melee\\Mission.~ls");
                break;
        }
    }

    // L004F9A50
    private static void TlstForm_OKBtnClick(LstWindow LstForm)
    {
        LstForm.MissionLstMemo.SaveToFile(AlliedVariables.s_XWADirLabSetting + "\\Missions\\Mission.lst");
        LstForm.MeleeLstMemo.SaveToFile(AlliedVariables.s_XWADirLabSetting + "\\Melee\\Mission.lst");
    }

    // L004F9CD4
    private static int TlstForm_Proc_004F9CD4(LstWindow LstForm, string edx0)
    {
        StringBuilder ebp0C_1 = new();

        for (int ebx = edx0.IndexOf("_"); BtBitString((byte)edx0[ebx], AlliedVariables.s_V0x00533B80) && ebx < edx0.Length; ebx++)
        {
            ebp0C_1.Append(System_LStrFromChar(edx0[ebx]));
        }

        int ebx1 = StrRec_try_to_int_L0051E3BC(ebp0C_1.ToString());
        return ebx1;
    }

    // L004F9B20
    private static void TlstForm_Button1Click(LstWindow LstForm)
    {
        int esi0 = LstForm.MissionLstMemo.Items.Count;
        LstForm.MissionLstMemo.InsertItem(esi0 - 1, "//");
        string ebp04 = string.Format(CultureInfo.InvariantCulture, "!BATTLE_{0}_HEADER![{1}]", Controls_TControl_GetText(LstForm.BattleNum), Controls_TControl_GetText(LstForm.BattleDesc));
        LstForm.MissionLstMemo.InsertItem(esi0 - 1, ebp04);
        LstForm.MissionLstMemo.InsertItem(esi0 - 1, "//");
        AlliedVariables.s_V0x00542472 += 1;
        Controls_TControl_SetText(LstForm.BattleNum, AlliedVariables.s_V0x00542472.ToString(CultureInfo.InvariantCulture));
        string ebp24_0 = string.Format(CultureInfo.InvariantCulture, "Battle {0}: ", Controls_TControl_GetText(LstForm.BattleNum));
        Controls_TControl_SetText(LstForm.BattleDesc, ebp24_0);
    }

    // L0051BF88
    private static bool Unit_00513838_Proc_0051BF88(string eax0)
    {
        bool ebp05 = false;
        int esi = 0x01;

        if (!string.IsNullOrEmpty(eax0))
        {
            ebp05 = true;

            do
            {
                if (!BtBitString((byte)eax0[esi - 0x01], AlliedVariables.s_V0x00533B80))
                {
                    ebp05 = false;
                }

                esi++;
            }
            while (ebp05 && esi <= eax0.Length);
        }

        return ebp05;
    }
}
