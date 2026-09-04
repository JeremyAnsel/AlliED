using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlliED.Impl.ViewsImpl;

internal static class UnknownsWindowImpl
{
    public static void Register(UnknownsWindow window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(UnknownsWindow window)
    {
        window.Activated += (s, e) => TUnksForm_FormActivate(window);
        window.Closed += (s, e) => TUnksForm_FormClose(window);

        window.U15.TextChanged += (s, e) => TUnksForm_U3Change(window, (TextBox)s);
        window.U4.TextChanged += (s, e) => TUnksForm_U3Change(window, (TextBox)s);
        window.U10.TextChanged += (s, e) => TUnksForm_U3Change(window, (TextBox)s);
        window.U14.TextChanged += (s, e) => TUnksForm_U3Change(window, (TextBox)s);
        window.U3.TextChanged += (s, e) => TUnksForm_U3Change(window, (TextBox)s);
        window.A3.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.A4.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U29.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U31.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U33.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U35.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U37.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U39.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U41.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
        window.U43.Click += (s, e) => TUnksForm_A3Click(window, (CheckBox)s);
    }

    // L004D25E8
    private static void FormCreate(UnknownsWindow UnksForm)
    {
        AlliedVariables.s_V0x005423F8 = 0x01;

        if (TApplication_GetWidth() != 0x280
            && TApplication_GetHeight() != 0x1E0)
        {
            return;
        }

        AlliedVariables.s_TUnksForm_Instance!.FontFamily = new FontFamily("MS Serif");
        AlliedVariables.s_TUnksForm_Instance!.FontSize = 0x09;
        Controls_TControl_SetHeight(AlliedVariables.s_TUnksForm_Instance!, 0x1DB);
        Controls_TControl_SetWidth(AlliedVariables.s_TUnksForm_Instance!, 0x280);
        TApplication_L00468080(AlliedVariables.s_TUnksForm_Instance!, 0x01);
        Controls_TWinControl_ScaleBy(AlliedVariables.s_TUnksForm_Instance!, TApplication_GetWidth(), 0x32A);
        TApplication_RecreateWnd(AlliedVariables.s_TUnksForm_Instance!, 0x04);
    }

    // L004D25D8
    public static void TUnksForm_FormActivate(UnknownsWindow UnksForm)
    {
        TUnksForm_Proc_004D26E8(UnksForm);
    }

    // L004D25E0
    private static void TUnksForm_FormClose(UnknownsWindow UnksForm)
    {
        AlliedVariables.s_V0x005423F8 = 0;
    }

    // L004D26E8
    public static void TUnksForm_Proc_004D26E8(UnknownsWindow UnksForm)
    {
        S0xFGObject esi = AlliedVariables.s_V0x005AFE90;

        byte bl0 = AlliedVariables.s_V0x00543C9A;
        AlliedVariables.s_V0x00543C9A = 0;

        TUnksForm_SetIntegerText(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U3, esi.FlightGroupStruct.WaveDelay);
        TUnksForm_SetIntegerText(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U4, esi.FlightGroupStruct.WaveContinuous);
        TUnksForm_SetIntegerText(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U10, esi.FlightGroupStruct.LinkId);
        TUnksForm_SetIntegerText(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U14, esi.FlightGroupStruct.ArrivalRandomDelaySeconds);
        TUnksForm_SetIntegerText(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U15, esi.FlightGroupStruct.CurStartFg);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.A3, esi.FlightGroupStruct.m000DC0);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.A4, esi.FlightGroupStruct.m000DC1);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U29, esi.FlightGroupStruct.m000E29);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U31, esi.FlightGroupStruct.m000E2B);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U33, esi.FlightGroupStruct.m000E2D);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U35, esi.FlightGroupStruct.m000E2F);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U37, esi.FlightGroupStruct.m000E31);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U39, esi.FlightGroupStruct.m000E33);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U41, esi.FlightGroupStruct.m000E35);
        TUnksForm_SetChecked(UnksForm, AlliedVariables.s_TUnksForm_Instance!.U43, esi.FlightGroupStruct.m000E37);

        AlliedVariables.s_V0x00543C9A = bl0;
    }

    // L004D268C
    private static void TUnksForm_SetIntegerText(UnknownsWindow UnksForm, TextBox edx0, short ecx0)
    {
        Controls_TControl_SetText(edx0, ecx0.ToString(CultureInfo.InvariantCulture));
    }

    // L004D26DC
    private static void TUnksForm_SetChecked(UnknownsWindow UnksForm, CheckBox edx0, bool ecx0)
    {
        edx0.IsChecked = ecx0;
    }

    // L004D2864
    private static void TUnksForm_U3Change(UnknownsWindow UnksForm, TextBox edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        string ebp08 = Controls_TControl_GetText(edx0);
        int.TryParse(ebp08, out int ebp04);

        int count = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < count; ebx++)
        {
            if (StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                S0xFGObject eax;

                switch ((UnksFormControlEnum)Convert.ToInt32(edx0.Tag))
                {
                    case (UnksFormControlEnum)0x03:
                        eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax.FlightGroupStruct.WaveDelay = (byte)ebp04;
                        break;

                    case (UnksFormControlEnum)0x04:
                        eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax.FlightGroupStruct.WaveContinuous = (byte)ebp04;
                        break;

                    case (UnksFormControlEnum)0x08:
                        eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax.FlightGroupStruct.Roll = (byte)ebp04;
                        break;

                    case (UnksFormControlEnum)0x0A:
                        eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax.FlightGroupStruct.LinkId = (byte)ebp04;
                        break;

                    case (UnksFormControlEnum)0x0C:
                        eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax.FlightGroupStruct.LinkUnused = (byte)ebp04;
                        break;

                    case (UnksFormControlEnum)0x0E:
                        eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax.FlightGroupStruct.ArrivalRandomDelaySeconds = (byte)ebp04;
                        break;

                    case (UnksFormControlEnum)0x0F:
                        eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax.FlightGroupStruct.CurStartFg = (byte)ebp04;
                        break;
                }
            }

            Unit_00513838_Proc_00520528(ebx);
        }

        Unit_00513838_Proc_0051467C();
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004D2A24
    private static void TUnksForm_A3Click(UnknownsWindow UnksForm, CheckBox edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
            return;

        byte ebp = (byte)((edx0.IsChecked == true ? 1 : 0) & 0x7F);
        int ebx = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int esi = 0; esi < ebx; esi++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, esi))
            {
                continue;
            }

            S0xFGObject eax;

            switch ((UnksFormControlEnum)Convert.ToInt32(edx0.Tag))
            {
                case (UnksFormControlEnum)0x1D:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E29 = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x1E:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000DC0 = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x1F:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E2B = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x21:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E2D = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x23:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E2F = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x25:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E31 = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x27:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E33 = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x28:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000DC1 = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x29:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E35 = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x2B:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.m000E37 = ebp != 0;
                    break;

                case (UnksFormControlEnum)0x46:
                    eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax.FlightGroupStruct.WaveNumberingOff = ebp;
                    break;
            }
        }

        Unit_00513838_Proc_0051467C();
        Unit_00513838_Proc_00517258();
    }
}
