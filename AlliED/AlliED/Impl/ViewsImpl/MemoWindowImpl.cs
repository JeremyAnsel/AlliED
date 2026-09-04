using System.Windows.Controls;
using System.Windows.Input;

namespace AlliED.Impl.ViewsImpl;

internal static class MemoWindowImpl
{
    public static void Register(MemoWindow window)
    {
        SetBindings(window);
        FormCreate(window);

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

    private static void SetBindings(MemoWindow window)
    {
        window.Closed += (s, e) =>
        {
            TMemoForm_FormClose(window);
            TMemoForm_FormDestroy(window);
        };

        window.KeyDown += (s, e) => TMemoForm_FormKeyDown(window, s, e.Key, Keyboard.Modifiers);

        window.EditMemo1.TextChanged += (s, e) => TMemoForm_EditMemoChange(window);
        window.EditMemo2.TextChanged += (s, e) => TMemoForm_EditMemoChange(window);
        window.EditMemo3.TextChanged += (s, e) => TMemoForm_EditMemoChange(window);
        window.EditMemo4.TextChanged += (s, e) => TMemoForm_EditMemoChange(window);
        window.MemoPages.SelectionChanged += (s, e) =>
        {
            TMemoForm_MemoPagesChanging(window);
            TMemoForm_MemoPagesChange(window);
        };
    }

    // L004C4158
    private static void FormCreate(MemoWindow MemoForm)
    {
        MemoForm.MemoPages.SelectedIndex = (int)AlliedVariables.s_MemoForm_PageIndex;
        TMemoForm_Proc_004C418C(MemoForm);

        AlliedVariables.s_V0x00535E8C = 0x01;
        AlliedVariables.s_V0x00535E8D = 0;
    }

    // L004C418C
    public static void TMemoForm_Proc_004C418C(MemoWindow MemoForm)
    {
        AlliedVariables.s_V0x00535E8E = 0x01;

        switch ((MemoPageEnum)MemoForm.MemoPages.SelectedIndex)
        {
            case MemoPageEnum.Notes:
                TMemoForm_Proc_004C4278(MemoForm, 0x80000005, 0x00000000, MemoForm.EditMemo1);
                Form1WindowImpl.TForm1_Proc_0052A600(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TieMission_Notes, MemoForm.EditMemo1);
                break;

            case MemoPageEnum.Description:
                TMemoForm_Proc_004C4278(MemoForm, 0x004F003F, 0x00E4E4E4, MemoForm.EditMemo2);
                Form1WindowImpl.TForm1_Proc_0052A600(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TieMission_Description, MemoForm.EditMemo2);
                break;

            case MemoPageEnum.WinDebrief:
                TMemoForm_Proc_004C4278(MemoForm, 0x0042022F, 0x003EEAFD, MemoForm.EditMemo3);
                Form1WindowImpl.TForm1_Proc_0052A600(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TieMission_WinDebriefing, MemoForm.EditMemo3);
                break;

            case MemoPageEnum.Hints:
                TMemoForm_Proc_004C4278(MemoForm, 0x00080808, 0x00D4D4D4, MemoForm.EditMemo4);
                Form1WindowImpl.TForm1_Proc_0052A600(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TieMission_LostDebriefing, MemoForm.EditMemo4);
                break;
        }

        AlliedVariables.s_V0x00535E8D = 0;
        AlliedVariables.s_V0x00535E8E = 0;
    }

    // L004C4278
    private static void TMemoForm_Proc_004C4278(MemoWindow MemoForm, uint edx0, uint ecx0, Control control)
    {
        Controls_TControl_SetColor(control, edx0);
        Graphics_TFont_SetColor(control, ecx0);
    }

    // L004C4184
    private static void TMemoForm_FormClose(MemoWindow MemoForm)
    {
        //System_TObject_Free(MemoForm);
    }

    // L004C43B4
    private static void TMemoForm_FormDestroy(MemoWindow MemoForm)
    {
        AlliedVariables.s_V0x00535E8C = 0;
        AlliedVariables.s_MemoForm_PageIndex = (MemoPageEnum)MemoForm.MemoPages.SelectedIndex;
        TMemoForm_Proc_004C4328(MemoForm);
        Form1WindowImpl.TForm1_L0052BD20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TMemoForm_Instance!, AlliedVariables.s_V0x005AFC74.m000000);
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.DescBtn, false);
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.TextSections1, AlliedVariables.s_AlliedForm1Window!.DescBtn.IsChecked == true);
    }

    // L004C4328
    public static void TMemoForm_Proc_004C4328(MemoWindow MemoForm)
    {
        if (AlliedVariables.s_V0x00535E8D == 0)
        {
            return;
        }

        switch ((MemoPageEnum)MemoForm.MemoPages.SelectedIndex)
        {
            case MemoPageEnum.Notes:
                TMemoForm_Proc_004C42BC(MemoForm, MemoForm.EditMemo1, AlliedVariables.s_TieMission_Notes);
                break;

            case MemoPageEnum.Description:
                TMemoForm_Proc_004C42BC(MemoForm, MemoForm.EditMemo2, AlliedVariables.s_TieMission_Description);
                break;

            case MemoPageEnum.WinDebrief:
                TMemoForm_Proc_004C42BC(MemoForm, MemoForm.EditMemo3, AlliedVariables.s_TieMission_WinDebriefing);
                break;

            case MemoPageEnum.Hints:
                TMemoForm_Proc_004C42BC(MemoForm, MemoForm.EditMemo4, AlliedVariables.s_TieMission_LostDebriefing);
                break;
        }

        AlliedVariables.s_V0x00535E8D = 0;
    }

    // L004C42A4
    private static void TMemoForm_EditMemoChange(MemoWindow MemoForm)
    {
        if (AlliedVariables.s_V0x00535E8E == 0)
        {
            AlliedVariables.s_V0x00535E8D = 0x01;

            Unit_00513838_Proc_005146A4();
        }
    }

    // L004C43AC
    private static void TMemoForm_FormKeyDown(MemoWindow MemoForm, object? Sender, Key key, ModifierKeys modifiers)
    {
    }

    // L004C429C
    private static void TMemoForm_MemoPagesChanging(MemoWindow MemoForm)
    {
        TMemoForm_Proc_004C4328(MemoForm);
    }

    // L004C4270
    private static void TMemoForm_MemoPagesChange(MemoWindow MemoForm)
    {
        TMemoForm_Proc_004C418C(MemoForm);
    }

    // L004C42BC
    private static void TMemoForm_Proc_004C42BC(MemoWindow MemoForm, TextBox edx0, TFixedString ecx0)
    {
        char[] ecx1 = new char[ecx0.MaxLength + 1];

        int edi = 0;
        int length = Math.Min(edx0.Text.Length, ecx0.MaxLength);

        for (int eax = 0; eax < length; eax++)
        {
            char dl = edx0.Text[eax];

            if (dl == '\r')
            {
                ecx1[edi] = '$';
                edi++;
            }
            else if (dl != '\n')
            {
                ecx1[edi] = dl;
                edi++;
            }
        }

        ecx1[edi] = '\0';
        ecx0.Text = new string(ecx1, 0, edi);
    }
}
