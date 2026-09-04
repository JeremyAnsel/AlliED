using AlliED.Extensions;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class ShipExtUserControlImpl
{
    public static void Register(ShipExtUserControl window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(ShipExtUserControl window)
    {
        window.FormBtn.Click += (s, e) => TShipExt_FormBtnClick(window);
        window.PickBDrops.Click += (s, e) => TShipExt_PickBDropsClick(window);
        window.AutoLinkChk.Click += (s, e) => TShipExt_AutoLinkChkClick(window);
        window.Status2Box.SelectionChanged += (s, e) => TShipExt_CounterBoxChange(window, (ComboBox)s);
        window.CounterBox.SelectionChanged += (s, e) => TShipExt_CounterBoxChange(window, (ComboBox)s);
        window.IFFBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.ColorBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.StatusBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.AIBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.RadioBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.FormBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.SpacingSpin.ValueChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.MissleBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.BeamBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.AppearBox.SelectionChanged += (s, e) => TShipExt_AIBoxChange(window, s);
        window.RandomChk.Click += (s, e) => TShipExt_RandomChkClick(window);
        window.SpecShpSpin.SelectionChanged += (s, e) => TShipExt_SpecShpSpinChange(window, s);
        window.CargoEd.SelectionChanged += (s, e) => TShipExt_CargoEdChange(window, s);
        window.SpecCargEd.SelectionChanged += (s, e) => TShipExt_SpecCargEdChange(window, s);
    }

    // L0050E438
    private static void FormCreate(ShipExtUserControl ShipExt)
    {
        AlliedVariables.s_V0x005B6D16 = 0x01;

        if (AlliedVariables.s_Strings_Status.GetCount() <= 0)
        {
            AlliedLoadTStringsItemsFromFileAndFillComboBox("Status", AlliedVariables.s_Strings_Status, ShipExt.StatusBox, true);
        }

        if (AlliedVariables.s_Strings_Status.GetCount() <= 0)
        {
            ShipExt.StatusBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_Status);
        }

        ShipExt.FormBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_Form);
        Unit_00513838_Proc_00515A04();

        for (int ebx = 0x01; ebx < 0x0A; ebx++)
        {
            ShipExt.CargoEd.AddItem("0." + ebx.ToString(CultureInfo.InvariantCulture));
        }

        for (int ebx = 0x01; ebx < 0x07; ebx++)
        {
            ShipExt.CargoEd.AddItem(ebx.ToString(CultureInfo.InvariantCulture) + ".0");
        }

        ShipExt.SpecCargEd.SetItems(ShipExt.CargoEd.Items);
        ShipExt.IFFBox.SetItems(AlliedVariables.s_Strings_IFF);
        ShipExt.StatusBox.SetItems(AlliedVariables.s_Strings_Status);
        ShipExt.Status2Box.SetItems(AlliedVariables.s_Strings_Status);
    }

    // L0050EAC0
    public static void TShipExt__PROC_0050EAC0(ShipExtUserControl eax0)
    {
        S0xTieFlightGroup ebpE3E = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ToByteArray());
        AlliedVariables.s_V0x00543C9A = 0;

        Unit_00513838_Proc_00517308();
        Controls_TControl_SetText(eax0.CargoEd, System_LStrFromPCharLen(ebpE3E.Cargo, 0x14));
        Controls_TControl_SetText(eax0.SpecCargEd, System_LStrFromPCharLen(ebpE3E.SpecialCargo, 0x14));

        if (ebpE3E.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop)
        {
            eax0.SpecShpSpin.SelectedIndex = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.PlanetId;
        }
        else
        {
            eax0.SpecShpSpin.SelectedIndex = ebpE3E.SpecialCraft;
        }

        eax0.AppearBox.SelectedIndex = (int)ebpE3E.ArrivalDifficulty;
        eax0.RandomChk.IsChecked = ebpE3E.RandomSpecialCraft != 0;

        if (eax0.SpecShpSpin.SelectedIndex + 1 > AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftsCount
            && eax0.RandomChk.IsChecked == false
            && AlliedVariables.s_V0x00543B5C == 0)
        {
            Controls_TControl_SetColor(eax0.SpecCargEd, 0x8000000F);
        }
        else
        {
            Controls_TControl_SetColor(eax0.SpecCargEd, 0x80000005);
        }

        Unit_00513838_Proc_0051DDA8();
        Allied_ComboBox_SetSelectedIndex(eax0.StatusBox, (int)ebpE3E.Status1);
        eax0.Status2Box.SelectedIndex = (int)ebpE3E.Status2;
        Allied_ComboBox_SetSelectedIndex(eax0.MissleBox, ebpE3E.WarheadType);
        Allied_ComboBox_SetSelectedIndex(eax0.BeamBox, ebpE3E.BeamType);
        Allied_ComboBox_SetSelectedIndex(eax0.CounterBox, ebpE3E.CounterMeasuresType);
        Allied_ComboBox_SetSelectedIndex(eax0.RadioBox, ebpE3E.Radio);
        Allied_ComboBox_SetSelectedIndex(eax0.AIBox, ebpE3E.AIRank);
        Allied_ComboBox_SetSelectedIndex(eax0.IFFBox, ebpE3E.Iff);
        Allied_ComboBox_SetSelectedIndex(eax0.ColorBox, ebpE3E.Markings);
        Allied_ComboBox_SetSelectedIndex(eax0.FormBox, ebpE3E.FormationType);
        Spin_TSpinEdit_SetValue(eax0.SpacingSpin, ebpE3E.FormationSpacing);
        Buttons_TSpeedButton_SetDown(eax0.AutoLinkChk, AlliedVariables.s_V0x005AFE90.AutoLink);

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L00517308
    public static void Unit_00513838_Proc_00517308()
    {
        if (AlliedVariables.s_V0x005B6D16 == 0)
        {
            return;
        }

        if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftId == CraftIdEnum._087_1_11_AsteroidHR1)
        {
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label4, "  Planet");
            AlliedVariables.s_TShipExt_Instance!.StatusBox.SetItems(AlliedVariables.s_Strings_Planets);
            AlliedVariables.s_TShipExt_Instance!.StatusBox.SelectedIndex = (int)AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Status1;
        }
        else
        {
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label4, "Status 1");
            AlliedVariables.s_TShipExt_Instance!.StatusBox.SetItems(AlliedVariables.s_Strings_Status);
            AlliedVariables.s_TShipExt_Instance!.StatusBox.SelectedIndex = (int)AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Status1;
        }
    }

    // L0051DDA8
    public static void Unit_00513838_Proc_0051DDA8()
    {
        if (AlliedVariables.s_V0x005B6D16 == 0)
        {
            return;
        }

        if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop && AlliedVariables.s_V0x00543B5C == 0)
        {
            AlliedVariables.s_V0x00543B5C = 0x01;

            Controls_TControl_SetWidth(AlliedVariables.s_TShipExt_Instance!.SpecShpSpin, 0x79);
            Controls_TControl_SetVisible(AlliedVariables.s_TShipExt_Instance!.Label45, false);
            Controls_TControl_SetVisible(AlliedVariables.s_TShipExt_Instance!.Label37, false);
            Controls_TControl_SetVisible(AlliedVariables.s_TShipExt_Instance!.PickBDrops, true);

            if (AlliedVariables.s_Allied_DoesBackdropsTxt_FileExist)
            {
                AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SetItems(AlliedVariables.s_Strings_Backdrops);
            }
            else
            {
                AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
            }

            AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SelectedIndex = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.PlanetId;

            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label32, "  Bright:"); // "  Bright:"
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label31, "    Size:"); // "    Size:"
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label45, " Back"); // " Back"
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label37, " drop:"); // " drop:"
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.GroupBox6, "Backdrop"); // "Backdrop"
            Controls_TControl_SetColor(AlliedVariables.s_TShipExt_Instance!.SpecCargEd, 0x80000005);
        }
        else if ((AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop ? 0 : (1 & AlliedVariables.s_V0x00543B5C)) != 0)
        {
            AlliedVariables.s_V0x00543B5C = 0;

            Controls_TControl_SetWidth(AlliedVariables.s_TShipExt_Instance!.SpecShpSpin, 0x2A);
            AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
            AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.DeleteItem(0);
            AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SelectedIndex = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.SpecialCraft;
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label32, "Default:"); // "Default:"
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label31, "Special:"); // "Special:"
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label45, "Spec"); // "Spec"
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.Label37, "Ship#:"); // "Ship#:"
            Controls_TControl_SetVisible(AlliedVariables.s_TShipExt_Instance!.PickBDrops, false);
            Controls_TControl_SetVisible(AlliedVariables.s_TShipExt_Instance!.Label45, true);
            Controls_TControl_SetVisible(AlliedVariables.s_TShipExt_Instance!.Label37, true);
            Controls_TControl_SetText(AlliedVariables.s_TShipExt_Instance!.GroupBox6, "Cargo"); // "Cargo"

            if (AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SelectedIndex + 1 > AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftsCount
                && AlliedVariables.s_TShipExt_Instance!.RandomChk.IsChecked == false)
            {
                Controls_TControl_SetColor(AlliedVariables.s_TShipExt_Instance!.SpecCargEd, 0x8000000F);
            }
            else
            {
                Controls_TControl_SetColor(AlliedVariables.s_TShipExt_Instance!.SpecCargEd, 0x80000005);
            }
        }
    }

    // L0050EAB4
    private static void TShipExt_FormDestroy(ShipExtUserControl ShipExt)
    {
        AlliedVariables.s_V0x005B6D16 = 0;
    }

    // L0050E5DC
    private static void TShipExt_FormClose(ShipExtUserControl ShipExt)
    {
        AlliedVariables.s_V0x005B6D16 = 0;
    }

    // L0050ED24
    private static void TShipExt_FormBtnClick(ShipExtUserControl ShipExt)
    {
        AlliedVariables.s_TFormForm_Instance = MainImpl.CreateFormationBox();
        AlliedVariables.s_TFormForm_Instance.Owner = Application.Current.MainWindow;
        AlliedVariables.s_TFormForm_Instance.ShowDialog();
        AlliedVariables.s_TFormForm_Instance = null;
    }

    // L0050ED84
    private static void TShipExt_PickBDropsClick(ShipExtUserControl ShipExt)
    {
        AlliedVariables.s_TBDropForm_Instance = MainImpl.CreateBackdropBox();
        AlliedVariables.s_TBDropForm_Instance.Owner = Application.Current.MainWindow;
        AlliedVariables.s_TBDropForm_Instance.ShowDialog();
        AlliedVariables.s_TBDropForm_Instance = null;
    }

    // L0050E7F0
    public static void TShipExt_SpecShpSpinChange(ShipExtUserControl Sender, object? Button)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int count0 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < count0; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                if (eax0.FlightGroupStruct.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop)
                {
                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax2.FlightGroupStruct.PlanetId = (byte)Sender.SpecShpSpin.SelectedIndex;
                }
                else
                {
                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax2.FlightGroupStruct.SpecialCraft = (byte)Sender.SpecShpSpin.SelectedIndex;
                }
            }

            Unit_00513838_Proc_0051467C();
        }

        if (Sender.SpecShpSpin.SelectedIndex >= AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftsCount && Sender.RandomChk.IsChecked != true)
        {
            Controls_TControl_SetColor(Sender.SpecCargEd, 0x8000000F);
        }
        else
        {
            Controls_TControl_SetColor(Sender.SpecCargEd, 0x80000005);
        }
    }

    // L0050EA14
    private static void TShipExt_AutoLinkChkClick(ShipExtUserControl ShipExt)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
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
            eax1.AutoLink = ShipExt.AutoLinkChk.IsChecked == true;
        }
    }

    // L0050EA88
    private static void TShipExt_CounterBoxChange(ShipExtUserControl ShipExt, ComboBox edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedGetControlTag(edx0);
        int eax1 = DatapadWindowImpl.Unit_00513838_Proc_0051E8C4(edx0);
        Unit_00513838_Proc_0051EF8C(esi, eax1);
    }

    // L0050E5E8
    private static void TShipExt_AIBoxChange(ShipExtUserControl ShipExt, object? edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedGetControlTag(edx0);
        int edx1 = DatapadWindowImpl.Unit_00513838_Proc_0051E8C4(edx0);
        Unit_00513838_Proc_0051E614(esi, edx1);

        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L0050E8F8
    private static void TShipExt_RandomChkClick(ShipExtUserControl ShipExt)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        if (ShipExt.RandomChk.IsChecked == true)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.RandomSpecialCraft = 0x01;
            }

            Controls_TControl_SetColor(ShipExt.SpecCargEd, 0x80000005);
        }
        else
        {
            int eax1 = Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.FGSizeSpin);

            if (ShipExt.SpecShpSpin.SelectedIndex > eax1)
            {
                Controls_TControl_SetColor(ShipExt.SpecCargEd, 0x8000000F);
            }

            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax2.FlightGroupStruct.RandomSpecialCraft = 0;
            }
        }

        Unit_00513838_Proc_0051467C();
    }

    // L0050E620
    private static void TShipExt_CargoEdChange(ShipExtUserControl ShipExt, object? edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        if (AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count > 0)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                string ebp18 = Controls_TControl_GetText(ShipExt.CargoEd);
                string ebp14 = Unit_00511CD0_Proc_00511EB8(ebp18);

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Cargo = ebp14.WithMaxLength(0x14);
            }
        }

        Unit_00513838_Proc_0051467C();
    }

    // L0050E708
    private static void TShipExt_SpecCargEdChange(ShipExtUserControl ShipExt, object? edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        if (AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count > 0)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                string ebp18 = Controls_TControl_GetText(ShipExt.SpecCargEd);
                string ebp14 = Unit_00511CD0_Proc_00511EB8(ebp18);

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.SpecialCargo = ebp14.WithMaxLength(0x14);
            }
        }

        Unit_00513838_Proc_0051467C();
    }
}
