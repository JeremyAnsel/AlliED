using AlliED.Extensions;
using System.Globalization;
using System.IO;

namespace AlliED.Impl.ViewsImpl;

internal static class ClipWindowImpl
{
    public static void Register(ClipWindow window)
    {
        SetBindings(window);
    }

    private static void SetBindings(ClipWindow window)
    {
        window.Activated += (s, e) => TClipForm_FormActivate(window);
        window.Closed += (s, e) => TClipForm_FormClose(window);

        window.OKBtn.Click += (s, e) => TClipForm_OKBtnClick(window);
        window.SpeedEd.TextChanged += (s, e) => TClipForm_SpeedEdChange(window);
        window.ListBox1.MouseLeftButtonUp += (s, e) => TClipForm_ListBox1Click(window);
        window.ListBox1.MouseDoubleClick += (s, e) => TClipForm_ListBox1DblClick(window);
        window.DelClipBtn.Click += (s, e) => TClipForm_DelClipBtnClick(window);
        window.Up.Click += (s, e) => TClipForm_UpClick(window);
        window.Down.Click += (s, e) => TClipForm_DownClick(window);
        window.InsBtn.Click += (s, e) => TClipForm_InsBtnClick(window);
    }

    // L004C8024
    private static void TClipForm_FormActivate(ClipWindow ClipForm)
    {
        switch (AlliedVariables.s_ClipboardType)
        {
            case ClipboardTypeEnum.Condition:
                TClipForm_Proc_004C81B4(ClipForm);
                break;

            case ClipboardTypeEnum.Order:
                TClipForm__PROC_004C825C(ClipForm);
                break;

            case ClipboardTypeEnum.ShiplistSequence:
                {
                    int esi0 = AlliedVariables.s_Strings_Ships.GetCount();

                    for (int ebp04 = 0; ebp04 < esi0; ebp04++)
                    {
                        ClipForm.ListBox1.AddItem(AlliedVariables.s_Strings_Ships.GetText(ebp04));
                    }

                    StdCtrls_TCustomListBox_SetSelected(ClipForm.ListBox1, 0, true);
                    AlliedVariables.s_Allied_Strings_SpeedTxt = new();
                    AlliedVariables.s_Allied_SpeedTxt_FileName = AlliedVariables.s_AlliedDirectoryPath + "\\" + "Speeds.txt";

                    if (File.Exists(AlliedVariables.s_Allied_SpeedTxt_FileName))
                    {
                        AlliedVariables.s_Allied_Strings_SpeedTxt.LoadFromFile(AlliedVariables.s_Allied_SpeedTxt_FileName);
                    }
                    else
                    {
                        Controls_TControl_SetVisible(ClipForm.Label1, false);
                        Controls_TControl_SetVisible(ClipForm.SpeedEd, false);
                    }

                    break;
                }
        }

        ClipForm.ListBox1.SelectedIndex = 0;
        AlliedVariables.s_V0x0053BD85 = 0;
        AlliedVariables.s_V0x0053BD86 = 0;
    }

    // L004C8950
    private static void TClipForm_OKBtnClick(ClipWindow ClipForm)
    {
        if (AlliedVariables.s_V0x0053BD86 == 0)
        {
            return;
        }

        AlliedVariables.s_Allied_Strings_SpeedTxt.SaveToFile(AlliedVariables.s_Allied_SpeedTxt_FileName);

        int esi = AlliedVariables.s_Allied_Strings_SpeedTxt.GetCount();

        for (int ebx = 0; ebx < esi; ebx++)
        {
            string ebp08_0 = AlliedVariables.s_Allied_Strings_SpeedTxt.GetText(ebx);
            int eax_edx = (int)Math.Round(int.Parse(ebp08_0) * 0.45);
            AlliedVariables.s_Strings_Speeds.Put(ebx, eax_edx.ToString(CultureInfo.InvariantCulture));
        }

        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004C8710
    private static void TClipForm_FormClose(ClipWindow ClipForm)
    {
        switch (AlliedVariables.s_ClipboardType)
        {
            case ClipboardTypeEnum.Condition:
                Unit_00513838_Proc_0051CB44();
                break;

            case ClipboardTypeEnum.Order:
                Unit_00513838_Proc_0051CA50();
                break;

            case ClipboardTypeEnum.ShiplistSequence:
                if (AlliedVariables.s_V0x0053BD85 != 0)
                {
                    AlliedVariables.s_Strings_ShipSeq.SaveToFile(AlliedVariables.s_AlliedDirectoryPath + "\\ShipSeq.txt");
                }

                AlliedVariables.s_Allied_Strings_SpeedTxt.Clear();
                break;
        }
    }

    // L004C88D4
    private static void TClipForm_SpeedEdChange(ClipWindow ClipForm)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            string ebp04 = Controls_TControl_GetText(ClipForm.SpeedEd);
            ShipSeqEnum eax1 = (ShipSeqEnum)ClipForm.ListBox1.SelectedIndex;
            CraftIdEnum edx1 = AlliedConvertShipSeqToCraftId(eax1);
            AlliedVariables.s_Allied_Strings_SpeedTxt.Put((int)edx1, ebp04);
            AlliedVariables.s_V0x0053BD86 = 0x01;
        }
    }

    // L004C8850
    private static void TClipForm_ListBox1Click(ClipWindow ClipForm)
    {
        byte bl = AlliedVariables.s_V0x00543C9A;

        if (AlliedVariables.s_ClipboardType == ClipboardTypeEnum.ShiplistSequence)
        {
            ShipSeqEnum eax1 = (ShipSeqEnum)ClipForm.ListBox1.SelectedIndex;
            CraftIdEnum eax2 = AlliedConvertShipSeqToCraftId(eax1);
            Controls_TControl_SetText(ClipForm.SpeedEd, AlliedVariables.s_Allied_Strings_SpeedTxt.GetText((int)eax2));
        }

        AlliedVariables.s_V0x00543C9A = bl;
    }

    // L004C8004
    private static void TClipForm_ListBox1DblClick(ClipWindow ClipForm)
    {
        if (AlliedVariables.s_ClipboardType == ClipboardTypeEnum.ShiplistSequence)
        {
            return;
        }

        ClipForm.DialogResult = true;
        ClipForm.Close();
    }

    // L004C81B4
    private static void TClipForm_Proc_004C81B4(ClipWindow ClipForm)
    {
        ClipForm.ListBox1.Clear();

        if (AlliedVariables.s_V0x00543CF8.Count < 0x01)
        {
            ClipForm.OKBtn.IsEnabled = false;
        }
        else
        {
            int ebx = AlliedVariables.s_V0x00543CF8.Count;

            for (int edi = 0; edi < ebx; edi++)
            {
                S0xCondObjectStruct eax0 = Classes_TList_Get(AlliedVariables.s_V0x00543CF8, edi);
                string ebp04 = Unit_00511CD0_Proc_005122D0(eax0.m000004);
                ClipForm.ListBox1.AddItem(ebp04);
            }
        }
    }

    // L004C825C
    private static void TClipForm__PROC_004C825C(ClipWindow ClipForm)
    {
        ClipForm.ListBox1.Clear();

        if (AlliedVariables.s_V0x00543CFC.Count < 0x01)
        {
            ClipForm.OKBtn.IsEnabled = false;
        }
        else
        {
            int ebx = AlliedVariables.s_V0x00543CFC.Count;

            for (int ebp04 = 0; ebp04 < ebx; ebp04++)
            {
                S0xOrdObject eax0 = Classes_TList_Get(AlliedVariables.s_V0x00543CFC, ebp04);
                string ebp08 = Form1WindowImpl.TForm1_Proc_00526288(AlliedVariables.s_AlliedForm1Window!, eax0.m000004);
                ClipForm.ListBox1.AddItem(ebp08);
            }
        }
    }

    // L004C8318
    private static void TClipForm_DelClipBtnClick(ClipWindow ClipForm)
    {
        int esi = ClipForm.ListBox1.SelectedIndex;

        switch (AlliedVariables.s_ClipboardType)
        {
            case ClipboardTypeEnum.Condition:
                if (AlliedVariables.s_V0x00543CF8.Count > 0)
                {
                    Classes_TList_Delete(AlliedVariables.s_V0x00543CF8, esi);
                    TClipForm_Proc_004C81B4(ClipForm);
                }

                break;

            case ClipboardTypeEnum.Order:
                if (AlliedVariables.s_V0x00543CFC.Count > 0)
                {
                    Classes_TList_Delete(AlliedVariables.s_V0x00543CFC, esi);
                    TClipForm__PROC_004C825C(ClipForm);
                }

                break;

            case ClipboardTypeEnum.ShiplistSequence:
                {
                    string ebp04 = AlliedVariables.s_Strings_ShipSeq.GetText(esi);

                    if (string.Equals(ebp04, "0", StringComparison.Ordinal))
                    {
                        AlliedVariables.s_Strings_Ships.Delete(esi);
                        AlliedVariables.s_Strings_ShipSeq.Delete(esi);
                        ClipForm.ListBox1.DeleteItem(esi);

                        AlliedVariables.s_V0x0053BD85 = 0x01;
                    }

                    break;
                }
        }

        if (esi > ClipForm.ListBox1.Items.Count - 1)
        {
            esi = ClipForm.ListBox1.Items.Count - 1;
        }

        ClipForm.ListBox1.SelectedIndex = esi;
    }

    // L004C8460
    private static void TClipForm_UpClick(ClipWindow ClipForm)
    {
        int ebx = ClipForm.ListBox1.SelectedIndex;

        if (ebx > 0)
        {
            switch (AlliedVariables.s_ClipboardType)
            {
                case ClipboardTypeEnum.Condition:
                    Classes_TList_Exchange(AlliedVariables.s_V0x00543CF8, ebx, ebx - 1);
                    TClipForm_Proc_004C81B4(ClipForm);
                    break;

                case ClipboardTypeEnum.Order:
                    Classes_TList_Exchange(AlliedVariables.s_V0x00543CFC, ebx, ebx - 1);
                    TClipForm__PROC_004C825C(ClipForm);
                    break;

                case ClipboardTypeEnum.ShiplistSequence:
                    if (!StdCtrls_TCustomListBox_GetSelected(ClipForm.ListBox1, 0))
                    {
                        int esp00 = ClipForm.ListBox1.Items.Count;

                        for (int edi = 0; edi < esp00; edi++)
                        {
                            if (!StdCtrls_TCustomListBox_GetSelected(ClipForm.ListBox1, edi))
                            {
                                continue;
                            }

                            ClipForm.ListBox1.Exchange(edi, edi - 1);
                            AlliedVariables.s_Strings_ShipSeq.Exchange(edi, edi - 1);
                            AlliedVariables.s_Strings_Ships.Exchange(edi, edi - 1);
                            StdCtrls_TCustomListBox_SetSelected(ClipForm.ListBox1, edi, false);
                            StdCtrls_TCustomListBox_SetSelected(ClipForm.ListBox1, edi - 1, true);
                        }

                        AlliedVariables.s_V0x0053BD85 = 0x01;
                        ClipForm.ListBox1.SelectedIndex = ebx - 1;
                    }

                    break;
            }
        }

        if (ebx < 0x01)
        {
            ebx = 0x01;
        }

        if (AlliedVariables.s_ClipboardType < ClipboardTypeEnum.ShiplistSequence)
        {
            ClipForm.ListBox1.SelectedIndex = ebx - 1;
        }
    }

    // L004C85B0
    private static void TClipForm_DownClick(ClipWindow ClipForm)
    {
        int esi = ClipForm.ListBox1.SelectedIndex;

        if (esi < ClipForm.ListBox1.Items.Count - 1)
        {
            switch (AlliedVariables.s_ClipboardType)
            {
                case ClipboardTypeEnum.Condition:
                    Classes_TList_Exchange(AlliedVariables.s_V0x00543CF8, esi, esi + 1);
                    TClipForm_Proc_004C81B4(ClipForm);
                    break;

                case ClipboardTypeEnum.Order:
                    Classes_TList_Exchange(AlliedVariables.s_V0x00543CFC, esi, esi + 1);
                    TClipForm__PROC_004C825C(ClipForm);
                    break;

                case ClipboardTypeEnum.ShiplistSequence:
                    if (!StdCtrls_TCustomListBox_GetSelected(ClipForm.ListBox1, ClipForm.ListBox1.Items.Count - 1))
                    {
                        int edi0 = ClipForm.ListBox1.Items.Count - 1;

                        for (int edi = edi0; edi >= 0; edi--)
                        {
                            if (!StdCtrls_TCustomListBox_GetSelected(ClipForm.ListBox1, edi))
                            {
                                continue;
                            }

                            ClipForm.ListBox1.Exchange(edi, edi + 1);
                            AlliedVariables.s_Strings_ShipSeq.Exchange(edi, edi + 1);
                            AlliedVariables.s_Strings_Ships.Exchange(edi, edi + 1);
                            StdCtrls_TCustomListBox_SetSelected(ClipForm.ListBox1, edi, false);
                            StdCtrls_TCustomListBox_SetSelected(ClipForm.ListBox1, edi + 1, true);
                        }

                        AlliedVariables.s_V0x0053BD85 = 0x01;
                        ClipForm.ListBox1.SelectedIndex = esi + 1;
                    }

                    break;
            }
        }

        if (AlliedVariables.s_ClipboardType < ClipboardTypeEnum.ShiplistSequence)
        {
            ClipForm.ListBox1.SelectedIndex = esi + 1;
        }
    }

    // L004C87DC
    private static void TClipForm_InsBtnClick(ClipWindow ClipForm)
    {
        int ebx = ClipForm.ListBox1.SelectedIndex;
        ClipForm.ListBox1.InsertItem(ebx, "");
        AlliedVariables.s_Strings_Ships.Insert(ebx, "");
        AlliedVariables.s_Strings_ShipSeq.Insert(ebx, "0");
        AlliedVariables.s_V0x0053BD85 = 0x01;
        ClipForm.ListBox1.SelectedIndex = ebx;
    }
}
