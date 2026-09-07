using AlliED.Controls;
using AlliED.Extensions;
using AlliED.Helpers;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace AlliED.Impl.ViewsImpl;

internal static class DatapadWindowImpl
{
    public static void Register(DatapadWindow window)
    {
        SetBindings(window);
        FormCreate(window);

        window.Closed += (s, e) =>
        {
            AlliedVariables.s_TShipExt_Instance = null;
        };

        window.Closing += (s, e) =>
        {
            if (MainImpl.IsMainOpened)
            {
                e.Cancel = true;
                window.Hide();
                window.Owner.Activate();
            }
        };

        //window.Loaded += (s, e) =>
        //{
        //    window.FGPages.SelectedItem = window.Arrival;
        //};
    }

    private static void SetBindings(DatapadWindow window)
    {
        window.Activated += (s, e) => TDatapad_FormActivate(window);
        window.Closing += (s, e) => TDatapad_FormClose(window);

        window.KeyDown += (s, e) => TDatapad_FormKeyDown(window, s, e.Key, Keyboard.Modifiers);

        window.ExtBtn.Click += (s, e) => TDatapad_ExtBtnClick(window, null);
        //window.FGPages.SelectionChanged += (s, e) => TDatapad_FGPagesChange(window, s);
        window.ShipBox.SelectionChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.WaveSpin.ValueChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.PlayerBox.SelectionChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.TeamBox.SelectionChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.FGSizeSpin.ValueChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.GGSpin.ValueChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.PlrPosSpin.SelectionChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.OrientationBox.SelectionChanged += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.ArrIfPlrChk.Click += (s, e) => TDatapad_ShipBoxChange(window, s);
        window.Button1.Click += (s, e) => TDatapad_Button1Click(window, s);
        window.GUSpin.ValueChanged += (s, e) => TDatapad_GUSpinChange(window, s);
        window.ExpTimeSpin.ValueChanged += (s, e) => TDatapad_GUSpinChange(window, s);
        window.ShipName.SelectionChanged += (s, e) => TDatapad_ShipNameChange(window);
        window.NumberNameChk.Click += (s, e) => TDatapad_NumberNameChkClick(window, s);
        window.ToolR1.Click += (s, e) => TDatapad_ToolR1Click(window, (RadioButton)s);
        window.ToolR2.Click += (s, e) => TDatapad_ToolR1Click(window, (RadioButton)s);
        window.ToolR3.Click += (s, e) => TDatapad_ToolR1Click(window, (RadioButton)s);
        window.ToolR4.Click += (s, e) => TDatapad_ToolR1Click(window, (RadioButton)s);
        window.MissleSelBox.MouseLeftButtonUp += (s, e) => TDatapad_MissleSelBoxClick(window, s);
        window.MissleSelBox.KeyUp += (s, e) => TDatapad_MissleSelBoxClick(window, s);
        window.CounterSelbox.MouseLeftButtonUp += (s, e) => TDatapad_MissleSelBoxClick(window, s);
        window.CounterSelbox.KeyUp += (s, e) => TDatapad_MissleSelBoxClick(window, s);
        window.BeamSelBox.MouseLeftButtonUp += (s, e) => TDatapad_MissleSelBoxClick(window, s);
        window.BeamSelBox.KeyUp += (s, e) => TDatapad_MissleSelBoxClick(window, s);
        window.AllOptionsBut.Click += (s, e) => TDatapad_AllOptionsButClick(window, s);
        window.OpShipTypes.SelectionChanged += (s, e) => TDatapad_OpShipTypesChange(window, s);
        window.OpShipWavesSpin.ValueChanged += (s, e) => TDatapad_OpShipWavesSpinChange(window, s);
        window.OpFGCountSpin.ValueChanged += (s, e) => TDatapad_OpFGCountSpinChange(window, s);
        window.OpShipBox.SelectionChanged += (s, e) => TDatapad_OpShipBoxChange(window, s);
        window.OpFGList.MouseLeftButtonUp += (s, e) => TDatapad_OpFGListClick(window, s);
        window.OrderBox.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.OrderSpeedBox.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.MGLTBox.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.OneAnd2Chk.Click += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T1Class.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T2Class.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T1Index.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T2Index.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.ThreeAnd4Chk.Click += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T3Class.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T4Class.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T3Index.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.T4Index.SelectionChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.OrderP1.ValueChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.OrderP2.ValueChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.OrderP3.ValueChanged += (s, e) => TDatapad_OrderBoxChange(window, s);
        window.ArrMotherBox.SelectionChanged += (s, e) => TDatapad_ArrMotherBoxChange(window, s);
        window.ArrAltmotherBox.SelectionChanged += (s, e) => TDatapad_ArrMotherBoxChange(window, s);
        window.ArrMotherBox.DrawItem += (sender, index) => TDatapad_ArrMotherBoxDrawItem(window, sender, index, null, null);
        window.ArrAltmotherBox.DrawItem += (sender, index) => TDatapad_ArrMotherBoxDrawItem(window, sender, index, null, null);
        window.ArrMotherBox.SelectionChanged += (s, e) =>
        {
            if (window.ArrMotherBox.SelectedIndex != -1)
            {
                ComboBoxItem item = window.ArrMotherBox.GetItem(window.ArrMotherBox.SelectedIndex);
                window.ArrMotherBox.Foreground = item.Foreground;
            }

            if (window.ArrMotherBox.SelectedIndex != -1)
            {
                TDatapad_ArrMotherBoxChange(window, s);
            }
        };
        window.ArrAltmotherBox.SelectionChanged += (s, e) =>
        {
            if (window.ArrAltmotherBox.SelectedIndex != -1)
            {
                ComboBoxItem item = window.ArrAltmotherBox.GetItem(window.ArrAltmotherBox.SelectedIndex);
                window.ArrAltmotherBox.Foreground = item.Foreground;
            }

            if (window.ArrAltmotherBox.SelectedIndex != -1)
            {
                TDatapad_ArrMotherBoxChange(window, s);
            }
        };
        window.Edit1.TextChanged += (s, e) => TDatapad_RoleBoxChange(window, s);
        window.RoleBox.SelectionChanged += (s, e) => TDatapad_RoleBoxChange(window, s);
        window.Edit2.TextChanged += (s, e) => TDatapad_RoleBoxChange(window, s);
        window.Edit5.TextChanged += (s, e) => TDatapad_RoleBoxChange(window, s);
        window.Edit7.TextChanged += (s, e) => TDatapad_RoleBoxChange(window, s);
        window.Role2Box.SelectionChanged += (s, e) => TDatapad_RoleBoxChange(window, s);
        window.Edit6.TextChanged += (s, e) => TDatapad_RoleBoxChange(window, s);
        window.UseRole1.Click += (s, e) => TDatapad_UseRole1Click(window, s);
        window.UseRole2.Click += (s, e) => TDatapad_UseRole2Click(window, s);
        window.OrderDescEd.SelectionChanged += (s, e) => TDatapad_OrderDescEdChange(window, s);
        window.ArrMinSpin.TextChanged += (s, e) => TDatapad_ArrMinSpinChange(window, s);
        window.ArrSecSpin.TextChanged += (s, e) => TDatapad_ArrSecSpinChange(window, s);
        window.DifficultyBox.SelectionChanged += (s, e) => TDatapad_DifficultyBoxChange(window, s);
        window.DepartWhenBox.SelectionChanged += (s, e) => TDatapad_DepartWhenBoxChange(window, s);
        window.DepMin.TextChanged += (s, e) => TDatapad_DepMinChange(window, s);
        window.DepSec.TextChanged += (s, e) => TDatapad_DepSecChange(window, s);
        window.FGgoalList.MouseLeftButtonUp += (s, e) => TDatapad_FGgoalListClick(window, s);
        window.FGgoalList.DrawItem += (sender, index) => TDatapad_FGgoalListDrawItem(window, sender, index, null, null);
        window.FGGoalTeamBox.SelectionChanged += (s, e) => TDatapad_FGGoalTeamBoxChange(window, s);
        window.FGGoalPointsSpin.ValueChanged += (s, e) => TDatapad_FGGoalPointsSpinChange(window, s);
        window.FGGoalUnk.Click += (s, e) => TDatapad_FGGoalUnkClick(window, s);
        window.FGGoalTimeSpin.TextChanged += (s, e) => TDatapad_FGGoalTimeSpinChange(window, s);
        window.AddOrderBtn.Click += (s, e) => TDatapad_AddOrderBtnClick(window, s);
        window.OrdDescText.TextChanged += (s, e) => TDatapad_OrdDescTextChange(window, s);
        window.GGRadios.SetMouseLeftButtonUp((s, e) => TDatapad_GGRadiosClick(window, s));
        window.GGPointsED.ValueChanged += (s, e) => TDatapad_GGPointsEDChange(window, s);
        window.GGUnk2.TextChanged += (s, e) => TDatapad_GGUnk2Change(window, s);
        window.GGUnk3.TextChanged += (s, e) => TDatapad_GGUnk3Change(window, s);
        window.GGUnk4_1.TextChanged += (s, e) => TDatapad_GGUnk4_1Change(window, (TextBox)s);
        window.GGUnk4_2.TextChanged += (s, e) => TDatapad_GGUnk4_1Change(window, (TextBox)s);
        window.GGUnk4_5.TextChanged += (s, e) => TDatapad_GGUnk4_1Change(window, (TextBox)s);
        window.MsgtextEd.TextChanged += (s, e) => TDatapad_MsgtextEdChange(window, s);
        window.MsgColorBox.SelectionChanged += (s, e) => TDatapad_MsgColorBoxChange(window, s);
        window.MsgDelaySpin.ValueChanged += (s, e) => TDatapad_MsgDelaySpinChange(window, s);
        window.SeenByList.MouseLeftButtonUp += (s, e) => TDatapad_SeenByListClick(window, s);
        window.MsgUnk3.TextChanged += (s, e) => TDatapad_MsgUnk3Change(window, s);

        window.Cond5Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond5Lab,
            MouseAction.LeftClick,
            s => TDatapad_Cond5LabClick(window, (TextBlock)s),
            s => true));
        window.Cond5Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond5Lab,
            MouseAction.LeftDoubleClick,
            s => TDatapad_Cond5LabDblClick(window, (TextBlock)s),
            s => true));
        window.Cond6Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond6Lab,
            MouseAction.LeftClick,
            s => TDatapad_Cond6LabClick(window, (TextBlock)s),
            s => true));
        window.Cond6Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond6Lab,
            MouseAction.LeftDoubleClick,
            s => TDatapad_Cond5LabDblClick(window, (TextBlock)s),
            s => true));

        window.and5And.Click += (s, e) => TDatapad_and5AndClick(window, s);
        window.And5Or.Click += (s, e) => TDatapad_and5AndClick(window, s);
        window.MsgID.TextChanged += (s, e) => TDatapad_MsgIDChange(window, s);
    }

    // L004C0078
    public static void FormCreate(DatapadWindow Datapad)
    {
        AlliedVariables.s_V0x005B6D15 = 0x01;
        ComCtrls_TPageControl_SetActivePage(Datapad.FGPages, Datapad.Ship);
        ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(Datapad.FGPages, 0x09), false);

        if (Application_GetPixelsPerInch() == 0x60)
        {
            Controls_TControl_SetLeft(Datapad.MothersPanel, 0x170);
        }
        else
        {
            Controls_TControl_SetLeft(Datapad.MothersPanel, AlliedPixelsScaleMul(300));
            ToolBar ebx = Datapad.ToolBar1;
            Controls_TControl_SetLeft(ebx, AlliedPixelsScaleMul(ebx.GetLeft() + 0x05));
            StdCtrls_TCustomListBox_SetItemHeight(Datapad.FGgoalList, 12);
        }

        Datapad.OrderBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_Orders);
        Datapad.T1Class.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_TDatapad_T1ClassStrings);

        if (AlliedVariables.s_Strings_IFF.GetCount() > 0)
        {
            for (int ebx = 0; ebx < 0x06; ebx++)
            {
                if (string.IsNullOrEmpty(AlliedVariables.s_Strings_IFF.GetText(ebx)))
                {
                    string ebp10_2 = AlliedVariables.s_TDatapad_T1ClassStrings.GetText(ebx);
                    AlliedVariables.s_Strings_IFF.Put(ebx, ebp10_2);
                }
            }
        }
        else
        {
            for (int ebx = 0; ebx < 0x06; ebx++)
            {
                AlliedVariables.s_Strings_IFF.Add(AlliedVariables.s_TDatapad_T1ClassStrings.GetText(ebx));
            }
        }

        Datapad.T2Class.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_Missiles);
        Datapad.T3Class.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_Counters);
        Datapad.T4Class.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_Beams);
        Datapad.T2Index.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_When);
        Datapad.T4Index.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_OrdTexts);
        Form1WindowImpl.TForm1_L005290F8(AlliedVariables.s_AlliedForm1Window!);

        if (AlliedVariables.s_V0x005439A8.m000004 == 0)
        {
            TDatapad__PROC_004C13DC(Datapad, true);

            if (AlliedVariables.s_V0x00543C64[0])
            {
                Buttons_TSpeedButton_SetDown(Datapad.ExtBtn, true);
            }

            TDatapad_ExtBtnClick(Datapad, Datapad.ExtBtn);
            AlliedVariables.s_AlliedForm1Window!.TeamIFFBox1.SetItems(AlliedVariables.s_Strings_IFF);
            Datapad.MGLTBox.Clear();

            for (int ebx = 0; ebx < 0x100; ebx++)
            {
                long eax_edx = (long)Math.Round(ebx * 2.2235294117650000) / 0x01;
                Datapad.MGLTBox.AddItem(eax_edx.ToString(CultureInfo.InvariantCulture));
            }

            Datapad.MGLTBox.PutItem(0, "default");
            Datapad.OrientationBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
            Datapad.OrientationBox.PutItem(0, "On edge");
            Datapad.OrientationBox.PutItem(0x40, "Flat");
            Datapad.OrientationBox.PutItem(0xC0, "Upside down");
            L0051A334();

            if (AlliedVariables.s_AlliedAplicationWidth > 0x280)
            {
                Controls_TControl_SetTop(Datapad, (int)AlliedVariables.s_AlliedForm1Window!.Top + 0x45);
            }
            else
            {
                Controls_TControl_SetTop(Datapad, (int)AlliedVariables.s_AlliedForm1Window!.Top);
            }

            Controls_TControl_SetLeft(Datapad, (int)AlliedVariables.s_AlliedForm1Window!.Left + 0x11B);
            TDatapad_Proc_004BF834(Datapad);
        }
    }

    // L004C0FE4
    private static void TDatapad_FormActivate(DatapadWindow Datapad)
    {
        Datapad.FGPages.SelectionChanged += (s, e) =>
        {
            if (e.Source != s)
            {
                return;
            }

            Form1WindowImpl.TForm1_FGPagesChange(AlliedVariables.s_AlliedForm1Window!, null);
            TDatapad_FGPagesChange(Datapad, s);
        };
    }

    // L004BF834
    public static void TDatapad_Proc_004BF834(DatapadWindow eax0)
    {
        AlliedVariables.s_V0x00543C9A = 0;

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            switch ((DatapadFGPageEnum)Convert.ToInt32(eax0.FGPages.GetActivePage().Tag))
            {
                case DatapadFGPageEnum.Ship:
                    L004BF9D4(eax0);
                    break;

                case DatapadFGPageEnum.Options:
                    Form1WindowImpl.TForm1_Proc_0052769C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
                    break;

                case DatapadFGPageEnum.Arrival:
                    Form1WindowImpl.TForm1_Proc_00527B64(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
                    break;

                case DatapadFGPageEnum.Departure:
                    Form1WindowImpl.TForm1_Proc_00527D44(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
                    break;

                case DatapadFGPageEnum.FGGoals:
                    Form1WindowImpl.TForm1_Proc_00527F7C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
                    break;

                case DatapadFGPageEnum.OrderPage:
                    TDatapad__PROC_004C0D6C(eax0);
                    break;

                case (DatapadFGPageEnum)0x06:
                    if (AlliedVariables.s_V0x005B6D18 != 0)
                    {
                        WaypointsWindowImpl.TWPform_PROC_0050BA2C(AlliedVariables.s_TWPform_Instance!);
                    }

                    break;

                case DatapadFGPageEnum.Jump:
                    Form1WindowImpl.TForm1_Proc_00528118(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
                    break;

                case DatapadFGPageEnum.Role:
                    Form1WindowImpl.TForm1_Proc_005278A0(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
                    break;

                case (DatapadFGPageEnum)0x09:
                    break;

                case DatapadFGPageEnum.GGoals:
                    TDatapad_GGRadiosClick(eax0, eax0.GGRadios);
                    break;

                case DatapadFGPageEnum.Messages:
                    Form1WindowImpl.TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6B58.RadioMessage);
                    break;
            }

            AlliedVariables.s_V0x00543B54 = 0;
        }

        if (AlliedVariables.s_V0x005423F8 != 0)
        {
            UnknownsWindowImpl.TUnksForm_Proc_004D26E8(AlliedVariables.s_TUnksForm_Instance!);
        }

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L004C13DC
    private static void TDatapad__PROC_004C13DC(DatapadWindow Datapad, bool edx0)
    {
        if (edx0)
        {
            if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop)
            {
                AlliedVariables.s_V0x00543B5C = 0;
            }
            else
            {
                AlliedVariables.s_V0x00543B5C = 0x01;
            }

            DatapadFGPageEnum edx1 = (DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag);

            switch (edx1)
            {
                case DatapadFGPageEnum.Ship:
                    if (AlliedVariables.s_V0x005B6D16 == 0)
                    {
                        AlliedVariables.s_TShipExtWindow_Instance = MainImpl.CreateShipExtWindow();
                        ControlHelpers.SetParent(AlliedVariables.s_TShipExtWindow_Instance!, AlliedVariables.s_TDatapad_Instance!.Panel1);
                        AlliedVariables.s_TShipExt_Instance = (ShipExtUserControl)AlliedVariables.s_TDatapad_Instance!.Panel1.Content;
                        ShipExtUserControlImpl.Register(AlliedVariables.s_TShipExt_Instance);
                        Controls_TControl_SetAlign(AlliedVariables.s_TShipExt_Instance!, TAlignEnum.Client);

                        AlliedVariables.s_TShipExtWindow_Instance?.Close();
                        AlliedVariables.s_TShipExtWindow_Instance = null;
                    }

                    ShipExtUserControlImpl.TShipExt__PROC_0050EAC0(AlliedVariables.s_TShipExt_Instance!);
                    TApplication_BringToFront(AlliedVariables.s_TShipExt_Instance!);
                    Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleDiv(500));
                    break;

                case DatapadFGPageEnum.FGGoals:
                    Controls_TControl_SetVisible(Datapad.Panel2, true);
                    break;
            }
        }
        else
        {
            DatapadFGPageEnum edx1 = (DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag);

            switch (edx1)
            {
                case DatapadFGPageEnum.Ship:
                    if (AlliedVariables.s_V0x005B6D16 != 0)
                    {
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleDiv(200));
                        TApplication_PostMessage_B021(AlliedVariables.s_TShipExt_Instance!);
                    }

                    break;

                case DatapadFGPageEnum.FGGoals:
                    Controls_TControl_SetVisible(Datapad.Panel2, false);
                    break;
            }
        }
    }

    // L004C0FE8
    private static void TDatapad_ExtBtnClick(DatapadWindow Datapad, object? Sender)
    {
        DatapadFGPageEnum eax1 = (DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag);
        bool dl1 = Datapad.ExtBtn.IsChecked == true;
        AlliedVariables.s_V0x00543C64[(int)eax1] = dl1;

        switch (eax1)
        {
            case DatapadFGPageEnum.Ship:
                TDatapad__PROC_004C13DC(Datapad, dl1);
                return;

            case DatapadFGPageEnum.FGGoals:
                Controls_TControl_SetVisible(Datapad.Panel2, dl1);
                TDatapad__PROC_004C1048(Datapad);
                return;
        }

        TDatapad__PROC_004C1048(Datapad);
    }

    // L0051A334
    private static void L0051A334()
    {
        TStrings ebp08 = new();

        if (File.Exists(AlliedVariables.s_AlliedDirectoryPath + "\\Data\\Teams.txt"))
        {
            AlliedVariables.s_AlliedForm1Window!.TeamName1Ed.LoadFromFile(AlliedVariables.s_AlliedDirectoryPath + "\\Data\\Teams.txt");
        }

        if (File.Exists(AlliedVariables.s_XWADirLabSetting + "\\ShipList.txt"))
        {
            AlliedVariables.s_Strings_Ships.Clear();
            AlliedVariables.s_Strings_Ships.Add("(None)");

            ebp08.LoadFromFile(AlliedVariables.s_XWADirLabSetting + "\\ShipList.txt");

            int eax0 = ebp08.GetCount();

            for (int ebp04 = 0; ebp04 < eax0; ebp04++)
            {
                string ebp0C = ebp08.GetText(ebp04);

                if (string.IsNullOrEmpty(ebp0C))
                {
                    continue;
                }

                ebp0C = ebp0C[1..^1];
                ebp0C = ebp0C[(ebp0C.IndexOf("!") + 1)..];
                ebp0C = ebp0C.Substring(0, ebp0C.IndexOf(","));

                if (!string.IsNullOrEmpty(ebp0C))
                {
                    AlliedVariables.s_Strings_Ships.Add(ebp0C);
                }
            }
        }
        else
        {
            AlliedLoadTStringsItemsFromFileAndFillComboBox("Ships", AlliedVariables.s_Strings_Ships, AlliedVariables.s_TDatapad_Instance!.ShipBox, true);
        }

        if (File.Exists(AlliedVariables.s_XWADirLabSetting + "\\SpecDesc.txt"))
        {
            AlliedVariables.s_Strings_Short.Clear();
            AlliedVariables.s_Strings_Short.Add("---");
            ebp08.LoadFromFile(AlliedVariables.s_XWADirLabSetting + "\\SpecDesc.txt");

            int ebp04 = 0;

            do
            {
                string ebp0C = ebp08.GetText(ebp04);
                int ebx0 = ebp0C.IndexOf("(") + 1;

                if (ebx0 != 0)
                {
                    ebp0C = ebp0C[ebx0..^1];
                }
                else
                {
                    ebp0C = string.Empty;
                }

                if (string.IsNullOrEmpty(ebp0C))
                {
                    ebp0C = (ebp04 / 0x05).ToString(CultureInfo.InvariantCulture);
                }

                int ecx0 = ebp04 / 0x05;

                if (ecx0 == 0xB7)
                {
                    AlliedVariables.s_Strings_Short.Add("B/Drop");
                }
                else if (ecx0 == 0xE3)
                {
                    AlliedVariables.s_Strings_Short.Add("DSII");
                }
                else if (ecx0 == 0x56)
                {
                    AlliedVariables.s_Strings_Short.Add("Asteriod");
                }
                else if (ecx0 == 0x57)
                {
                    AlliedVariables.s_Strings_Short.Add("Planet");
                }
                else
                {
                    if (!string.Equals(ebp0C, "0", StringComparison.Ordinal))
                    {
                        AlliedVariables.s_Strings_Short.Add(ebp0C);
                    }
                }

                ebp04 += 0x05;
            } while (ebp04 < ebp08.GetCount());
        }
        else
        {
            AlliedLoadTStringsItemsFromFileAndFillComboBox("Short", AlliedVariables.s_Strings_Short, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        }

        if (File.Exists(AlliedVariables.s_AlliedDirectoryPath + "\\Data\\ShipSeq.txt"))
        {
            AlliedVariables.s_Strings_ShipSeq.LoadFromFile(AlliedVariables.s_AlliedDirectoryPath + "\\Data\\ShipSeq.txt");
        }
        else
        {
            for (int ebp04 = 0; ebp04 < 0x100; ebp04++)
            {
                AlliedVariables.s_Strings_ShipSeq.Add(ebp04.ToString(CultureInfo.InvariantCulture));
            }
        }

        ebp08.Clear();

        int eax1 = AlliedVariables.s_Strings_Ships.GetCount();

        for (int ebp04 = 0; ebp04 < eax1; ebp04++)
        {
            ebp08.Add(AlliedVariables.s_Strings_Ships.GetText(ebp04));
        }

        AlliedVariables.s_Strings_Ships.Clear();

        int eax2 = AlliedVariables.s_Strings_ShipSeq.GetCount();

        for (int ebp04 = 0; ebp04 < eax2; ebp04++)
        {
            if (string.Equals(AlliedVariables.s_Strings_ShipSeq.GetText(ebp04), "0", StringComparison.Ordinal) && ebp04 != 0)
            {
                AlliedVariables.s_Strings_Ships.Add("");
                continue;
            }

            int ebx0 = int.Parse(AlliedVariables.s_Strings_ShipSeq.GetText(ebp04), CultureInfo.InvariantCulture);

            if (ebx0 < ebp08.GetCount())
            {
                AlliedVariables.s_Strings_Ships.Add(ebp08.GetText(ebx0));
            }
        }

        AlliedVariables.s_TDatapad_Instance!.ShipBox.SetItems(AlliedVariables.s_Strings_Ships);

        AlliedLoadTStringsItemsFromFileAndFillComboBox("FGNames", AlliedVariables.s_Strings_FGNames, AlliedVariables.s_TDatapad_Instance!.ShipName, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("OpShips", AlliedVariables.s_Strings_OpShips, AlliedVariables.s_TDatapad_Instance!.OpShipTypes, true);

        if (AlliedVariables.s_Allied_FilenamesHistory.GetCount() <= 0)
        {
            Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.ShipListPopUp, 0x0C), false);
        }
        else
        {
            int eax3 = AlliedVariables.s_Allied_FilenamesHistory.GetCount();

            for (int ebp04 = 0; ebp04 < eax3; ebp04++)
            {
                L0051D8A0(ebp04);
            }
        }

        if (File.Exists(AlliedVariables.s_AlliedDirectoryPath + "\\Data\\Backdrops.txt"))
        {
            AlliedVariables.s_Allied_DoesBackdropsTxt_FileExist = true;
        }
        else
        {
            AlliedVariables.s_Allied_DoesBackdropsTxt_FileExist = false;
        }

        AlliedLoadTStringsItemsFromFileAndFillComboBox("Backdrops", AlliedVariables.s_Strings_Backdrops, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Orders", AlliedVariables.s_Strings_Orders, AlliedVariables.s_TDatapad_Instance!.OrderBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("CMD", AlliedVariables.s_Strings_CMD, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Planets", AlliedVariables.s_Strings_Planets, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Musts", AlliedVariables.s_Strings_Musts, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Speeds", AlliedVariables.s_Strings_Speeds, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);

        int eax4 = AlliedVariables.s_Strings_Speeds.GetCount();

        for (int ebp04 = 0; ebp04 < eax4; ebp04++)
        {
            int ebp64 = int.Parse(AlliedVariables.s_Strings_Speeds.GetText(ebp04), CultureInfo.InvariantCulture);
            string ebp60_1 = ((long)Math.Round(ebp64 * 0.45)).ToString(CultureInfo.InvariantCulture);
            AlliedVariables.s_Strings_Speeds.Put(ebp04, ebp60_1);
        }

        AlliedLoadTStringsItemsFromFileAndFillComboBox("Players", AlliedVariables.s_Strings_Players, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("AI", AlliedVariables.s_Strings_AI, AlliedVariables.s_TShipExt_Instance!.AIBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("IFF", AlliedVariables.s_Strings_IFF, AlliedVariables.s_TShipExt_Instance!.IFFBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Radio", AlliedVariables.s_Strings_Radio, AlliedVariables.s_TShipExt_Instance!.RadioBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Form", AlliedVariables.s_Strings_Form, AlliedVariables.s_TShipExt_Instance!.FormBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Missiles", AlliedVariables.s_Strings_Missiles, AlliedVariables.s_TShipExt_Instance!.MissleBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Counters", AlliedVariables.s_Strings_Counters, AlliedVariables.s_TShipExt_Instance!.CounterBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Beams", AlliedVariables.s_Strings_Beams, AlliedVariables.s_TShipExt_Instance!.BeamBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("ObjCats", AlliedVariables.s_Strings_ObjCats, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("ShipCats", AlliedVariables.s_Strings_ShipCats, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("WPEnable", AlliedVariables.s_Strings_WPEnable, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("When", AlliedVariables.s_Strings_When, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("DepWhen", AlliedVariables.s_Strings_DepWhen, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("OrdTexts", AlliedVariables.s_Strings_OrdTexts, AlliedVariables.s_TDatapad_Instance!.ShipBox, false);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Colors", AlliedVariables.s_Strings_Colors, AlliedVariables.s_TShipExt_Instance!.ColorBox, true);

        Unit_00513838_Proc_0051DB68();
    }

    // L004BF9D4
    private static void L004BF9D4(DatapadWindow eax0)
    {
        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            S0xTieFlightGroup ebpE3E = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ToByteArray());

            AlliedVariables.s_V0x00543C9A = 0;

            if (ebpE3E.StartPointRegions[0] == 0)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR1, true);
            }
            else if (ebpE3E.StartPointRegions[0] == 0x01)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR2, true);
            }
            else if (ebpE3E.StartPointRegions[0] == 0x02)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR3, true);
            }
            else if (ebpE3E.StartPointRegions[0] == 0x03)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR4, true);
            }

            if (ebpE3E.StartPointRegions[0] != 0)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR1, false);
            }

            if (ebpE3E.StartPointRegions[0] != 0x01)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR2, false);
            }

            if (ebpE3E.StartPointRegions[0] != 0x02)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR3, false);
            }

            if (ebpE3E.StartPointRegions[0] != 0x03)
            {
                ComCtrls_TToolButton_SetDown(eax0.ToolR4, false);
            }

            if (AlliedVariables.s_AlliedDatapadCurrentPage != DatapadFGPageEnum.Ship)
            {
                Unit_00513838_Proc_00519F10();
            }

            Graphics_TFont_SetColor(eax0.ShipBox, AlliedGetIffColor(ebpE3E.Iff, 0));
            Graphics_TFont_SetColor(eax0.ShipName, eax0.ShipBox.GetFontColor());
            Graphics_TFont_SetColor(eax0.FGSizeSpin, eax0.ShipBox.GetFontColor());
            Controls_TControl_SetText(eax0.ShipName, System_LStrFromPCharLen(ebpE3E.Name, 0x14));
            eax0.PlrPosSpin.SelectedIndex = ebpE3E.PlayerCraft;
            Spin_TSpinEdit_SetValue(eax0.GGSpin, ebpE3E.GlobalGroupId);
            Spin_TSpinEdit_SetValue(eax0.GUSpin, ebpE3E.GlobalUnitId);
            eax0.NumberNameChk.IsChecked = (ebpE3E.WaveNumberingOff ^ 0x01) != 0;
            Spin_TSpinEdit_SetValue(eax0.FGSizeSpin, ebpE3E.CraftsCount);
            eax0.ShipBox.SelectedIndex = (int)AlliedConvertCraftIdToShipSeq(ebpE3E.CraftId);
            Spin_TSpinEdit_SetValue(eax0.WaveSpin, ebpE3E.WavesCount + 1);
            Allied_ComboBox_SetSelectedIndex(eax0.TeamBox, ebpE3E.Team);
            Allied_ComboBox_SetSelectedIndex(eax0.PlayerBox, ebpE3E.PlayerNumber);

            if (AlliedVariables.s_V0x005B6D16 != 0)
            {
                ShipExtUserControlImpl.TShipExt__PROC_0050EAC0(AlliedVariables.s_TShipExt_Instance!);
            }
        }

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L004C30F8
    public static void TDatapad_SetTitle(DatapadWindow Sender)
    {
        string ebp04 = string.Empty;

        DatapadFGPageEnum eax = (DatapadFGPageEnum)Convert.ToInt32(Sender.FGPages.GetActivePage().Tag);

        if (eax < (DatapadFGPageEnum)0x09)
        {
            string ebp20 = Form1WindowImpl.TForm1_GetCraftString_NameAndShort(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
            ebp04 = string.Format(CultureInfo.InvariantCulture, "Flight Group #{0} of {1}:  {2}", AlliedVariables.s_V0x00543B0C + 1, AlliedVariables.s_TieFileHeader.FlightGroupsCount, ebp20);
        }
        else if (eax == DatapadFGPageEnum.GGoals)
        {
            string ebp2C = AlliedVariables.s_Strings_Teams.GetText(AlliedVariables.s_V0x00543B18);
            ebp04 = string.Format(CultureInfo.InvariantCulture, "Global Goals for Team {0}", ebp2C);
        }
        else if (eax == DatapadFGPageEnum.Messages)
        {
            ebp04 = string.Format(CultureInfo.InvariantCulture, "Message #{0} of {1}", AlliedVariables.s_AlliedForm1Window!.MsgStrList.SelectedIndex + 1, AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count);
        }

        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!, ebp04);
    }

    // L004C05F0
    public static void TDatapad__PROC_004C05F0(DatapadWindow eax0)
    {
        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            int esi = Unit_00513838_Proc_00519BF4();

            TDatapad_SetTitle(eax0);
            Controls_TControl_SetText(eax0.TotalFGsLab, Unit_00513838_Proc_00519B6C().ToString(CultureInfo.InvariantCulture) + " Craft (medium)");
            Controls_TControl_SetText(eax0.TotalatStartLab, Unit_00513838_Proc_00519D68().ToString(CultureInfo.InvariantCulture) + " craft at 30 sec");

            if (esi == 0x01)
            {
                Controls_TControl_SetText(eax0.TotalObLab, esi.ToString(CultureInfo.InvariantCulture) + " Object");
            }
            else
            {
                Controls_TControl_SetText(eax0.TotalObLab, esi.ToString(CultureInfo.InvariantCulture) + " Objects");
            }
        }
    }

    // L00519BF4
    private static int Unit_00513838_Proc_00519BF4()
    {
        int edi0 = 0;

        int ebx = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int esi0 = 0; esi0 < ebx; esi0++)
        {
            S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi0);
            S0xTieFlightGroup esp00 = eax0.FlightGroupStruct;

            if (esp00.CraftId >= CraftIdEnum._075_1_3_MineA)
            {
                if (esp00.CraftId <= CraftIdEnum._077_1_5_MineC)
                {
                    if (esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x00
                        || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x02
                        || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x04
                        || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x05
                        || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x09)
                    {
                        edi0 += esp00.CraftsCount * esp00.CraftsCount;
                    }

                    continue;
                }

                if (esp00.CraftId >= CraftIdEnum._090_0_110_ShipYard)
                {
                    continue;
                }
            }

            if (esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x00
                || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x02
                || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x04
                || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x05
                || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x09)
            {
                edi0 += esp00.CraftsCount;
            }
        }

        return edi0;
    }

    // L00519B6C
    private static int Unit_00513838_Proc_00519B6C()
    {
        int edi0 = 0;

        int ebx = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int esi0 = 0; esi0 < ebx; esi0++)
        {
            S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi0);
            S0xTieFlightGroup esp00 = eax0.FlightGroupStruct;

            if ((esp00.CraftId > CraftIdEnum._000__1_0 && esp00.CraftId <= CraftIdEnum._069_0_109_Factory)
                || esp00.CraftId == CraftIdEnum._078_1_6_GunPlatform
                || esp00.CraftId < CraftIdEnum._093_0_96_LancerFrigate)
            {
                if (esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x00
                    || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x02
                    || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x04
                    || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x05
                    || esp00.ArrivalDifficulty == (ArrivalDifficultyEnum)0x09)
                {
                    edi0 += esp00.CraftsCount;
                }
            }
        }

        return edi0;
    }

    // L00519D68
    private static int Unit_00513838_Proc_00519D68()
    {
        int edi0 = 0;

        int esi = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!L00519C9C(ebx))
            {
                continue;
            }

            S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

            edi0 += eax0.FlightGroupStruct.CraftsCount;
        }

        return edi0;
    }

    // L00519C9C
    private static bool L00519C9C(int eax0)
    {
        bool esp00 = false;

        S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
        S0xTieFlightGroup esp02 = eax1.FlightGroupStruct;
        bool bl = false;

        if (esp02.ArrivalTrigger1.Triggers[0].Condition == TieConditionEnum.Always
            && esp02.ArrivalTrigger1.Triggers[1].Condition == TieConditionEnum.Always
            && esp02.ArrivalTrigger2.Triggers[0].Condition == TieConditionEnum.Always
            && esp02.ArrivalTrigger2.Triggers[1].Condition == TieConditionEnum.Always
            && esp02.ArrivalDelayMinutes < 0x01
            && esp02.ArrivalDelaySeconds < 0x1E)
        {
            bl = true;
        }

        if ((bl && esp02.CraftId > CraftIdEnum._000__1_0 && esp02.CraftId <= CraftIdEnum._069_0_109_Factory)
            || esp02.CraftId == CraftIdEnum._078_1_6_GunPlatform
            || esp02.CraftId < CraftIdEnum._093_0_96_LancerFrigate)
        {
            if (AlliedVariables.s_V0x00533CE0[(int)esp02.ArrivalDifficulty])
            {
                esp00 = true;
            }
        }

        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
        eax1.m001443 = bl;

        return esp00;
    }

    // L004C2A30
    public static void TDatapad_GGRadiosClick(DatapadWindow eax0, object? edx0)
    {
        bool ebp01 = AlliedVariables.s_V0x00543B53;
        AlliedVariables.s_V0x00543C9A = 0;
        int esi = eax0.GGRadios.GetItemIndex();
        AlliedVariables.s_GGoalCurrentTrigger = 0x01;

        CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, 0x01);
        CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[esi].Triggers.Trigger_0[0]);
        Unit_00513838_Proc_0051F09C(eax0.GGRadios.GetItemIndex(), AlliedVariables.s_GGoalCurrentTrigger);
        CondToolUserControlImpl.TCondToolForm_Proc_0050F450(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[esi].Triggers);
        CondToolUserControlImpl.TCondToolForm__PROC_0051009C(
            AlliedVariables.s_TCondToolForm_Instance!,
            AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[esi].Op,
            AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[esi].Triggers.Operator_0,
            AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[esi].Triggers.Operator_1);

        AlliedVariables.s_V0x00543C9A = 0;

        Controls_TControl_SetText(eax0.GGPointsED, (AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[esi].Points * 0x19).ToString(CultureInfo.InvariantCulture));
        string name = AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[eax0.GGRadios.GetItemIndex()].Name;
        Controls_TControl_SetText(eax0.GGUnk2, (name.Length > 7 ? (int)name[7] : 0).ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(eax0.GGUnk3, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[eax0.GGRadios.GetItemIndex()].TimePassed.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(eax0.GGUnk4_1, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[eax0.GGRadios.GetItemIndex()].m000034[0].ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(eax0.GGUnk4_2, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[eax0.GGRadios.GetItemIndex()].m000034[1].ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(eax0.GGUnk4_5, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[eax0.GGRadios.GetItemIndex()].m000034[4].ToString(CultureInfo.InvariantCulture));

        AlliedVariables.s_V0x00543C9A = 0x01;
        AlliedVariables.s_V0x00543B53 = ebp01;

        AlliedVariables.s_AlliedForm1Window!.SaveBtn.IsEnabled = ebp01;
    }

    // L004C1048
    private static void TDatapad__PROC_004C1048(DatapadWindow Datapad)
    {
        if (Application_GetPixelsPerInch() == 0x60)
        {
            DatapadFGPageEnum eax1 = (DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag);

            if (!AlliedVariables.s_V0x00543C64[(int)eax1])
            {
                switch (eax1)
                {
                    case DatapadFGPageEnum.Ship:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleDiv(200));
                        break;

                    case DatapadFGPageEnum.Options:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0xBE);
                        break;

                    case DatapadFGPageEnum.Arrival:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0x12A);
                        break;

                    case DatapadFGPageEnum.Departure:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0x102);
                        break;

                    case DatapadFGPageEnum.FGGoals:
                        Controls_TControl_SetVisible(Datapad.Panel2, false);
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0xA9);
                        Datapad.FGGoals.Update();
                        break;

                    case DatapadFGPageEnum.OrderPage:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0x123);
                        break;

                    case (DatapadFGPageEnum)0x06:
                        break;

                    case DatapadFGPageEnum.Jump:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0xCF);
                        break;

                    case DatapadFGPageEnum.Role:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0xB2);
                        break;

                    case (DatapadFGPageEnum)0x09:
                        break;

                    case DatapadFGPageEnum.GGoals:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0x190);
                        break;

                    case DatapadFGPageEnum.Messages:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0x1AE);
                        break;
                }
            }
            else
            {
                switch (eax1)
                {
                    case DatapadFGPageEnum.Ship:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleDiv(500));
                        break;

                    case DatapadFGPageEnum.Options:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0x19C);
                        break;

                    case DatapadFGPageEnum.FGGoals:
                        Controls_TControl_SetVisible(Datapad.Panel2, true);
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, 0x166);
                        Datapad.FGGoals.Update();
                        break;
                }
            }
        }
        else
        {
            DatapadFGPageEnum eax1 = (DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag);

            if (!AlliedVariables.s_V0x00543C64[(int)eax1])
            {
                switch (eax1)
                {
                    case DatapadFGPageEnum.Ship:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleDiv(200));
                        break;

                    case DatapadFGPageEnum.Options:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0xBE));
                        break;

                    case DatapadFGPageEnum.Arrival:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0x12A));
                        break;

                    case DatapadFGPageEnum.Departure:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0x102));
                        break;

                    case DatapadFGPageEnum.FGGoals:
                        Controls_TControl_SetVisible(Datapad.Panel2, false);
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0xA9));
                        break;

                    case DatapadFGPageEnum.OrderPage:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0x123));
                        break;

                    case (DatapadFGPageEnum)0x06:
                        break;

                    case DatapadFGPageEnum.Jump:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0xCF));
                        break;

                    case DatapadFGPageEnum.Role:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0xB2));
                        break;

                    case (DatapadFGPageEnum)0x09:
                        break;

                    case DatapadFGPageEnum.GGoals:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0x190));
                        break;

                    case DatapadFGPageEnum.Messages:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0x1AE));
                        break;
                }
            }
            else
            {
                switch (eax1)
                {
                    case DatapadFGPageEnum.Ship:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleDiv(500));
                        break;

                    case DatapadFGPageEnum.Options:
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0x19C));
                        break;

                    case DatapadFGPageEnum.FGGoals:
                        Controls_TControl_SetVisible(Datapad.Panel2, true);
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!, AlliedPixelsScaleMul(0x166));
                        break;
                }
            }
        }
    }

    // L004C042C
    public static void TDatapad_FGPagesChange(DatapadWindow Datapad, object? Sender)
    {
        if (Datapad.FGPages.GetActivePage() == Datapad.FGGoals)
        {
            Controls_TControl_SetTop(Datapad.Panel2, AlliedPixelsScaleDiv(0xBC));
        }
        else
        {
            Controls_TControl_SetTop(Datapad.Panel2, 0x14);
        }

        switch ((DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Ship:
                Controls_TControl_SetTop(Datapad.ExtPanel, AlliedPixelsScaleDiv(0x68));
                Controls_TControl_SetVisible(Datapad.ExtPanel, true);
                break;

            case DatapadFGPageEnum.Options:
                Controls_TControl_SetTop(Datapad.ExtPanel, AlliedPixelsScaleDiv(0x7D));
                Controls_TControl_SetVisible(Datapad.ExtPanel, true);
                break;

            case DatapadFGPageEnum.FGGoals:
                Controls_TControl_SetTop(Datapad.ExtPanel, AlliedPixelsScaleDiv(114));
                Controls_TControl_SetVisible(Datapad.ExtPanel, true);
                break;

            case DatapadFGPageEnum.OrderPage:
            case DatapadFGPageEnum.Jump:
                Form1WindowImpl.TForm1_Proc_0052F464(AlliedVariables.s_AlliedForm1Window!, true);
                Controls_TControl_SetVisible(Datapad.ExtPanel, false);
                break;

            default:
                Controls_TControl_SetVisible(Datapad.ExtPanel, false);
                break;
        }

        Buttons_TSpeedButton_SetDown(Datapad.ExtBtn, AlliedVariables.s_V0x00543C64[Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag)]);
        CondToolUserControlImpl.TCondToolForm__PROC_00510834(AlliedVariables.s_TCondToolForm_Instance);
        TDatapad__PROC_004C1048(Datapad);
        Datapad.FGPages.GetActivePage().Update();
        TDatapad_Proc_004BF834(Datapad);

        byte bl = AlliedVariables.s_V0x00543B54;
        AlliedVariables.s_AlliedDatapadCurrentPage = (DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag);

        if ((DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag) >= DatapadFGPageEnum.Ship && (DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag) <= DatapadFGPageEnum.Role)
        {
            AlliedVariables.s_V0x00543CC4 = Datapad.FGPages.GetPageIndex();
        }

        TDatapad_SetTitle(Datapad);
        AlliedVariables.s_V0x00543B54 = bl;
    }

    // L004C2E1C
    private static void TDatapad_FormClose(DatapadWindow Datapad)
    {
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.ShowDatapad, false);
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Datapad1, false);

        if ((Form1OverallPagesEnum)Convert.ToInt32(AlliedVariables.s_AlliedForm1Window!.OverallPages.GetActivePage().Tag) == Form1OverallPagesEnum.FlightGroups)
        {
            AlliedVariables.s_V0x005B7048 = false;
        }
    }

    // L00519F10
    private static void Unit_00513838_Proc_00519F10()
    {
        AlliedVariables.s_TDatapad_Instance!.TeamBox.SetItems(AlliedVariables.s_Strings_Teams);
        TDatapad__PROC_004C05F0(AlliedVariables.s_TDatapad_Instance!);
    }

    // L004C0774
    public static void TDatapad_Proc_004C0774(DatapadWindow Datapad, int edx0)
    {
        byte ebp01 = AlliedVariables.s_V0x00543C9A;
        AlliedVariables.s_V0x00543C9A = 0;
        S0xTieFlightGroup ebpE40 = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ToByteArray());

        switch ((DatapadFGPageEnum)Convert.ToInt32(Datapad.FGPages.GetActivePage().Tag))
        {
            case (DatapadFGPageEnum)0x06:
                if (AlliedVariables.s_V0x005B6D18 != 0)
                {
                    WaypointsWindowImpl.TWPform_PROC_0050BA2C(AlliedVariables.s_TWPform_Instance!);
                }

                break;

            case DatapadFGPageEnum.Jump:
                Form1WindowImpl.TForm1_Proc_00528118(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct);
                break;
        }

        Graphics_TFont_SetColor(Datapad.OrderBox, AlliedGetIffColor(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Iff, 0));
        Allied_ComboBox_SetSelectedIndex(Datapad.OrderBox, (int)ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].OrderId);

        if (AlliedVariables.s_Strings_OrdTexts.GetCount() > (int)ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].OrderId)
        {
            Controls_TControl_SetText(Datapad.XWOrderLab, AlliedVariables.s_Strings_OrdTexts.GetText((int)ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].OrderId));
        }

        Datapad.XWOrderLab.Update();
        Allied_ComboBox_SetSelectedIndex(Datapad.T1Class, (int)ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].PrimaryTarget.ClassA);
        Allied_ComboBox_SetSelectedIndex(Datapad.T2Class, (int)ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].PrimaryTarget.ClassB);
        Allied_ComboBox_SetSelectedIndex(Datapad.T3Class, (int)ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SecondaryTarget.ClassA);
        Allied_ComboBox_SetSelectedIndex(Datapad.T4Class, (int)ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SecondaryTarget.ClassB);
        Allied_ComboBox_SetSelectedIndex(Datapad.OrderSpeedBox, ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].Throttle);
        Unit_00513838_Proc_00517AC0(
            Datapad.T1Index,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].PrimaryTarget.ClassA,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].PrimaryTarget.ParameterA
            );
        Unit_00513838_Proc_00517AC0(
            Datapad.T2Index,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].PrimaryTarget.ClassB,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].PrimaryTarget.ParameterB
            );
        Unit_00513838_Proc_00517AC0(
            Datapad.T3Index,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SecondaryTarget.ClassA,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SecondaryTarget.ParameterA
            );
        Unit_00513838_Proc_00517AC0(
            Datapad.T4Index,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SecondaryTarget.ClassB,
            ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SecondaryTarget.ParameterB
            );

        if (ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].OrderId == TieOrderIdEnum._50_Hyperspace)
        {
            Spin_TSpinEdit_SetValue(Datapad.OrderP1, ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].Var0 + 1);
        }
        else
        {
            Spin_TSpinEdit_SetValue(Datapad.OrderP1, ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].Var0);
        }

        Spin_TSpinEdit_SetValue(Datapad.OrderP2, ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].Var1);
        Spin_TSpinEdit_SetValue(Datapad.OrderP3, ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].Var2);

        Datapad.OrderP1.Update();
        Datapad.OrderP2.Update();
        Datapad.OrderP3.Update();
        Datapad.MGLTBox.SelectedIndex = ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SpeedMph;
        Datapad.MGLTBox.Update();
        Datapad.ThreeAnd4Chk.IsChecked = ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].SecondaryTarget.Operator == 0;
        Datapad.OneAnd2Chk.IsChecked = ebpE40.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (edx0 - 1)].PrimaryTarget.Operator == 0;
        Datapad.ThreeAnd4Chk.Update();
        Datapad.OneAnd2Chk.Update();

        AlliedVariables.s_V0x00543CC8 = 0x198 + AlliedVariables.s_V0x00543B0C * 0x10 + (AlliedVariables.s_CurrentRegion - 1) * 0x04 + (AlliedVariables.s_CurrentOrderInRegion - 1);

        char ebpE4C = (char)AlliedVariables.s_V0x0054AEAC.unk000000[AlliedVariables.s_V0x00543CC8 - 1];
        string ebpE90 = ebpE4C.ToString(CultureInfo.InvariantCulture);
        S0x00533EA8 ebpED0 = S0x00533EA8.FromByteArray(AlliedVariables.s_V0x0054B67C[AlliedVariables.s_V0x00543CC8 - 1].ToByteArray());
        string ebpED0_str = ebpED0.unk000000.ReadFixedLengthString(0, Math.Min(ebpED0.unk000000.Length, 0x40));
        string ebpE48_0 = ebpE90 + ebpED0_str;
        Controls_TControl_SetText(Datapad.OrdDescText, ebpE48_0);

        if (Datapad.OrderBox.SelectedIndex == 0x32)
        {
            Datapad.OrderP1.SetM000220(0x01);
        }
        else
        {
            Datapad.OrderP1.SetM000220(0);
        }

        AlliedVariables.s_V0x00543C9A = ebp01;
    }

    // L004C2C74
    private static void TDatapad_FormKeyDown(DatapadWindow Datapad, object? Sender, Key key, ModifierKeys modifiers)
    {
    }

    // L0051B1C8
    public static void Unit_00513838_Proc_0051B1C8()
    {
        DatapadWindow esi = AlliedVariables.s_TDatapad_Instance!;
        byte bl = AlliedVariables.s_V0x00543B54;

        Graphics_TFont_SetColor(esi.OpShipBox, AlliedGetIffColor(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Iff, 0));
        Graphics_TFont_SetColor(esi.OpFGCountSpin, esi.OpShipBox.GetFontColor());
        Graphics_TFont_SetColor(esi.OpShipWavesSpin, esi.OpShipBox.GetFontColor());

        int index = AlliedVariables.s_V0x00543B24;
        if (index == -1)
        {
            index = 0;
        }
        esi.OpShipBox.SelectedIndex = (int)AlliedConvertCraftIdToShipSeq(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsId[index]);
        Spin_TSpinEdit_SetValue(esi.OpFGCountSpin, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsCount[index]);
        Spin_TSpinEdit_SetValue(esi.OpShipWavesSpin, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsWaves[index] + 1);

        AlliedVariables.s_V0x00543B54 = bl;
    }

    // L004C2F30
    public static void TDatapad_Proc_004C2F30(DatapadWindow eax0)
    {
        eax0.ArrMotherBox.SetItems(AlliedVariables.s_V0x00543BC0);
        eax0.ArrMotherBox.InsertItem(0, "Hyperspace");
        eax0.ArrAltmotherBox.SetItems(AlliedVariables.s_V0x00543BC0);
        eax0.ArrAltmotherBox.InsertItem(0, "Hyperspace");
    }

    // L004C191C
    public static void TDatapad__PROC_004C191C(DatapadWindow eax0)
    {
        byte ebp01 = (byte)AlliedVariables.s_V0x00543B48;

        eax0.FGgoalList.Clear();

        for (int ebx = 0; ebx < 0x08; ebx++)
        {
            string ebp08 = Form1WindowImpl.TForm1_Proc_00525FEC(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Goals[ebx]);
            eax0.FGgoalList.AddItem(ebp08);
        }

        AlliedVariables.s_V0x00543B48 = ebp01;
        eax0.FGgoalList.SelectedIndex = ebp01;
    }

    // L004C0D6C
    public static void TDatapad__PROC_004C0D6C(DatapadWindow eax0)
    {
        S0xTieFlightGroup ebpE3E = AlliedVariables.s_V0x005AFE90.FlightGroupStruct;

        if (AlliedVariables.s_AlliedDatapadCurrentPage != DatapadFGPageEnum.OrderPage)
        {
            TDatapad_L004C0EAC(eax0);
        }

        TDatapad_Proc_004C0774(eax0, AlliedVariables.s_CurrentOrderInRegion);
        Unit_00513838_Proc_00516414(ebpE3E.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].OrderId);
    }

    // L004C0EAC
    public static void TDatapad_L004C0EAC(DatapadWindow eax0)
    {
        eax0.T1Class.SetItems(AlliedVariables.s_V0x00543BCC);
        eax0.T2Class.SetItems(AlliedVariables.s_V0x00543BCC);
        eax0.T3Class.SetItems(AlliedVariables.s_V0x00543BCC);
        eax0.T4Class.SetItems(AlliedVariables.s_V0x00543BCC);

        eax0.T1Class.Update();
        eax0.T2Class.Update();
        eax0.T3Class.Update();
        eax0.T4Class.Update();
    }

    private static bool _TDatapad_ShipBoxChangeCalled = false;

    // L004BFCC4
    private static void TDatapad_ShipBoxChange(DatapadWindow Datapad, object edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        if (_TDatapad_ShipBoxChangeCalled)
        {
            return;
        }

        _TDatapad_ShipBoxChangeCalled = true;

        int esi = AlliedGetControlTag(edx0);
        int edx1 = Unit_00513838_Proc_0051E8C4(edx0);
        Unit_00513838_Proc_0051E614(esi, edx1);

        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_0051DB68();

        _TDatapad_ShipBoxChangeCalled = false;
    }

    // L0051E8C4
    public static int Unit_00513838_Proc_0051E8C4(object? eax0)
    {
        int esi = 0;
        ComboBox edi = AlliedVariables.s_TDatapad_Instance!.ShipBox;

        if (eax0 == edi)
        {
            esi = (int)AlliedConvertShipSeqToCraftId((ShipSeqEnum)edi.SelectedIndex);
        }
        else if (eax0 is ComboBox comboBox)
        {
            esi = comboBox.SelectedIndex;
        }
        else if (eax0 is IntegerUpDown integerUpDown)
        {
            esi = Spin_TSpinEdit_GetValue(integerUpDown);
        }
        else if (eax0 is CheckBox checkBox)
        {
            esi = (checkBox.IsChecked == true ? 1 : 0) & 0x7F;
        }
        //else if (System_IsClass((TObject*)eax0, s_V0x004309D8) != 0)
        //{
        //    string ebp04 = Controls_TControl_GetText(eax0);
        //    esi = StrRec_try_to_int_L0051E3BC(ebp04);
        //}
        //else if (System_IsClass((TObject*)eax0, s_V0x00433F74) != 0)
        //{
        //    esi = (eax0.IsChecked == true ? 1 : 0) & 0x7F;
        //}
        else
        {
            throw new ArgumentOutOfRangeException(nameof(eax0));
        }

        return esi;
    }

    // L004C2170
    private static void TDatapad_Button1Click(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x005423F8 == 0)
        {
            AlliedVariables.s_TUnksForm_Instance = MainImpl.CreateUnknownsWindow();
            AlliedVariables.s_TUnksForm_Instance.Owner = Datapad;
        }

        TApplication_BringToFront(AlliedVariables.s_TUnksForm_Instance!);
    }

    // L004BF830
    public static void L004BF830(DatapadWindow eax0)
    {
    }

    // L0051CE68
    public static void L0051CE68()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        DatapadFGPageEnum eax0 = (DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag);

        switch (eax0)
        {
            case DatapadFGPageEnum.Arrival:
                L0051BB5C(0x01);
                break;

            case DatapadFGPageEnum.Departure:
                L0051BB5C(0);
                break;

            case DatapadFGPageEnum.Jump:
                L0051BB5C(0);
                break;

            case DatapadFGPageEnum.GGoals:
                AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Operator_0 = AlliedVariables.s_TCondToolForm_Instance!.and2Or.IsChecked == true;
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();

                    int ebx = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

                    for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx; AlliedVariables.s_V0x00543CC0 += 1)
                    {
                        if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
                        {
                            continue;
                        }

                        S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        eax1.RadioMessage.Condition.Operator_0 = AlliedVariables.s_TCondToolForm_Instance!.and2Or.IsChecked == true;
                    }
                }

                break;
        }
    }

    // L0051CD4C
    public static void Unit_00513838_Proc_0051CD4C()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Arrival:
                {
                    L0051BB5C(0x02);
                    break;
                }

            case DatapadFGPageEnum.GGoals:
                {
                    AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Operator_1 = AlliedVariables.s_TCondToolForm_Instance!.And4Or.IsChecked == true;
                    Unit_00513838_Proc_005146A4();
                    break;
                }

            case DatapadFGPageEnum.Messages:
                {
                    if (AlliedVariables.s_RadioMessagesObjectsList.Count <= 0)
                        break;

                    Unit_00513838_Proc_0051442C();

                    int ebx = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

                    for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx; AlliedVariables.s_V0x00543CC0 += 1)
                    {
                        if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
                        {
                            continue;
                        }

                        S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        eax1.RadioMessage.Condition.Operator_1 = AlliedVariables.s_TCondToolForm_Instance!.And4Or.IsChecked == true;
                    }

                    break;
                }
        }
    }

    // L0051CC2C
    public static void Unit_00513838_Proc_0051CC2C()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Arrival:
                L0051BB5C(0);
                break;

            case DatapadFGPageEnum.GGoals:
                AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Op = AlliedVariables.s_TCondToolForm_Instance!.GlbOr.IsChecked == true;
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();

                    int ebx = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

                    for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx; AlliedVariables.s_V0x00543CC0 += 1)
                    {
                        if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
                        {
                            continue;
                        }

                        S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        eax1.RadioMessage.Operator = AlliedVariables.s_TCondToolForm_Instance!.GlbOr.IsChecked == true; ;
                    }
                }

                break;
        }
    }

    // L0051BB5C
    private static void L0051BB5C(byte eax0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < esi; AlliedVariables.s_V0x00543CC0 += 1)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
            {
                continue;
            }

            switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
            {
                case DatapadFGPageEnum.Arrival:
                    {
                        switch (eax0)
                        {
                            case 0x00:
                                {
                                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                                    eax1.FlightGroupStruct.ArrivalTriggersOperator = AlliedVariables.s_TCondToolForm_Instance!.GlbOr.IsChecked == true;
                                    break;
                                }

                            case 0x01:
                                {
                                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                                    eax1.FlightGroupStruct.ArrivalTrigger1.Operator = AlliedVariables.s_TCondToolForm_Instance!.and2Or.IsChecked == true;
                                    break;
                                }

                            case 0x02:
                                {
                                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                                    eax1.FlightGroupStruct.ArrivalTrigger2.Operator = AlliedVariables.s_TCondToolForm_Instance!.And4Or.IsChecked == true;
                                    break;
                                }
                        }

                        break;
                    }

                case DatapadFGPageEnum.Departure:
                    {
                        S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                        eax1.FlightGroupStruct.DepartureTrigger.Operator = AlliedVariables.s_TCondToolForm_Instance!.and2Or.IsChecked == true;
                        break;
                    }

                case DatapadFGPageEnum.Jump:
                    {
                        S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                        eax1.FlightGroupStruct.JumpTriggers[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Operator = AlliedVariables.s_TCondToolForm_Instance!.and2Or.IsChecked == true;
                        break;
                    }
            }
        }

        Unit_00513838_Proc_0051467C();
    }

    // L004BFC68
    public static void TDatapad__PROC_004BFC68(DatapadWindow Datapad)
    {
        Graphics_TFont_SetColor(Datapad.ShipBox, AlliedGetIffColor(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Iff, 0));
        Datapad.ShipName.Foreground = Datapad.ShipBox.Foreground;
        Datapad.FGSizeSpin.Foreground = Datapad.ShipBox.Foreground;
    }

    // L004C0748
    private static void TDatapad_GUSpinChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedGetControlTag(Sender);
        int edx = Unit_00513838_Proc_0051E8C4(Sender);
        Unit_00513838_Proc_0051EF8C(esi, edx);
    }

    // L004BFD00
    private static void TDatapad_ShipNameChange(DatapadWindow Datapad)
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

                Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx).FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(Controls_TControl_GetText(Datapad.ShipName));

                if (!AlliedVariables.s_LinkColorChkSetting)
                {
                    continue;
                }

                string shipName = Controls_TControl_GetText(Datapad.ShipName).ToUpperInvariant();

                if (shipName.Equals("RED", StringComparison.Ordinal) || shipName.Equals("ROGUE", StringComparison.Ordinal))
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Markings = 0;
                }
                else if (shipName.Equals("GOLD", StringComparison.Ordinal) || shipName.Equals("YELLOW", StringComparison.Ordinal))
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Markings = 0x01;
                }
                else if (shipName.Equals("BLUE", StringComparison.Ordinal))
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Markings = 0x02;
                }
                else if (shipName.Equals("GREEN", StringComparison.Ordinal))
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Markings = 0x03;
                }

                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                if (eax2.FlightGroupStruct.CraftId >= CraftIdEnum._005_0_4_TieFighter && eax2.FlightGroupStruct.CraftId <= CraftIdEnum._009_0_8_TieDefender)
                {
                    S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                    if (eax3.FlightGroupStruct.Markings >= 0x00 && eax3.FlightGroupStruct.Markings <= 0x02)
                    {
                        Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx).FlightGroupStruct.Markings++;
                    }
                }

                if (AlliedVariables.s_V0x005B6D16 != 0)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    AlliedVariables.s_TShipExt_Instance!.ColorBox.SelectedIndex = eax1.FlightGroupStruct.Markings;
                }
            }
        }

        Unit_00513838_Proc_0051467C();
        Unit_00513838_Proc_00517258();
    }

    // L004C3690
    private static void TDatapad_NumberNameChkClick(DatapadWindow Datapad, object? Sender)
    {
        byte edx1 = (byte)(((Datapad.NumberNameChk.IsChecked != true) ? 1 : 0) & 0x7F);
        Unit_00513838_Proc_0051EF8C(0x07, edx1);
    }

    // L004C2E68
    private static void TDatapad_ToolR1Click(DatapadWindow Datapad, RadioButton Sender)
    {
        Unit_00513838_Proc_0051DBC8(0x01, Convert.ToInt32(Sender.Tag));
    }

    // L004C1D78
    private static void TDatapad_MissleSelBoxClick(DatapadWindow Datapad, object? Sender)
    {
        Form1WindowImpl.TForm1_Proc_00528AE8(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_0051467C();
    }

    // L004C1D8C
    private static void TDatapad_AllOptionsButClick(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9C != 0)
        {
            byte ebx1 = (byte)AlliedVariables.s_TDatapad_Instance!.MissleSelBox.Items.Count;

            for (byte esp00 = 0; esp00 < ebx1; esp00++)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.MissleSelBox, esp00, true);
            }

            byte ebx2 = (byte)AlliedVariables.s_TDatapad_Instance!.CounterSelbox.Items.Count;

            for (byte esp00 = 0; esp00 < ebx2; esp00++)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.CounterSelbox, esp00, true);
            }

            byte ebx3 = (byte)AlliedVariables.s_TDatapad_Instance!.BeamSelBox.Items.Count;

            for (byte esp00 = 0; esp00 < ebx3; esp00++)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.BeamSelBox, esp00, true);
            }

            AlliedVariables.s_V0x00543C9C = 0;
        }
        else
        {
            byte ebx1 = (byte)AlliedVariables.s_TDatapad_Instance!.MissleSelBox.Items.Count;

            for (byte esp00 = 0; esp00 < ebx1; esp00++)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.MissleSelBox, esp00, false);
            }

            byte ebx2 = (byte)AlliedVariables.s_TDatapad_Instance!.BeamSelBox.Items.Count;

            for (byte esp00 = 0; esp00 < ebx2; esp00++)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.BeamSelBox, esp00, false);
            }

            byte ebx3 = (byte)AlliedVariables.s_TDatapad_Instance!.CounterSelbox.Items.Count;

            for (byte esp00 = 0; esp00 < ebx3; esp00++)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.CounterSelbox, esp00, false);
            }

            AlliedVariables.s_V0x00543C9C = 0x01;
        }

        Form1WindowImpl.TForm1_Proc_00528AE8(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_0051467C();
    }

    // L004C1AF8
    private static void TDatapad_OpShipTypesChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            Unit_00513838_Proc_0051467C();

            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.OptionalCraftCategory = (byte)Datapad.OpShipTypes.SelectedIndex;
            }
        }

        if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftCategory == 0x04)
        {
            Unit_00513838_Proc_00515E38();
        }
        else
        {
            Datapad.OpFGList.Clear();
        }
    }

    // L004C1B98
    private static void TDatapad_OpShipWavesSpinChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                int index = AlliedVariables.s_V0x00543B24;
                if (index == -1)
                {
                    index = 0;
                }
                eax1.FlightGroupStruct.OptionalCraftsWaves[index] = (byte)(Spin_TSpinEdit_GetValue(Datapad.OpShipWavesSpin) - 1);
            }

            Unit_00513838_Proc_0051467C();
        }

        Unit_00513838_Proc_00515E38();
    }

    // L004C1C20
    private static void TDatapad_OpFGCountSpinChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                int index = AlliedVariables.s_V0x00543B24;
                if (index == -1)
                {
                    index = 0;
                }
                eax1.FlightGroupStruct.OptionalCraftsCount[index] = (byte)Spin_TSpinEdit_GetValue(Datapad.OpFGCountSpin);
            }

            Unit_00513838_Proc_0051467C();
        }

        Unit_00513838_Proc_00515E38();
    }

    // L004C1CA8
    private static void TDatapad_OpShipBoxChange(DatapadWindow Datapad, object? Sender)
    {
        int index = AlliedVariables.s_V0x00543B24;
        if (index == -1)
        {
            index = 0;
        }

        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.OptionalCraftsId[index] = AlliedConvertShipSeqToCraftId((ShipSeqEnum)Datapad.OpShipBox.SelectedIndex);
            }

            Unit_00513838_Proc_0051467C();
        }

        if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsCount[index] < 0x01)
        {
            AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsCount[index] = 0x01;
            Spin_TSpinEdit_SetValue(Datapad.OpFGCountSpin, 0x01);
        }

        Unit_00513838_Proc_00515E38();
    }

    // L004C1AD8
    private static void TDatapad_OpFGListClick(DatapadWindow Datapad, object? Sender)
    {
        AlliedVariables.s_V0x00543B24 = Datapad.OpFGList.SelectedIndex;
        Unit_00513838_Proc_0051B1C8();
    }

    // L00515E38
    public static void Unit_00513838_Proc_00515E38()
    {
        byte ebp01 = (byte)AlliedVariables.s_TDatapad_Instance!.OpFGList.SelectedIndex;
        AlliedVariables.s_TDatapad_Instance!.OpFGList.Clear();

        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            string ebp08 = L00515ACC(
                AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsId[ebx],
                AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsCount[ebx],
                (byte)(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.OptionalCraftsWaves[ebx] + 1));

            AlliedVariables.s_TDatapad_Instance!.OpFGList.AddItem(ebp08);
        }

        AlliedVariables.s_TDatapad_Instance!.OpFGList.SelectedIndex = ebp01;
    }

    // L00515ACC
    private static string L00515ACC(CraftIdEnum craftId, byte craftCount, byte craftWaves)
    {
        string ebp14_3;

        if (craftCount == 0)
        {
            ebp14_3 = "<None>";
        }
        else
        {
            ebp14_3 = craftWaves.ToString(CultureInfo.InvariantCulture)
                + " x [ " + craftCount.ToString(CultureInfo.InvariantCulture) + " ]  "
                + AlliedVariables.s_Strings_Ships.GetText((int)AlliedConvertCraftIdToShipSeq(craftId));
        }

        return ebp14_3;
    }

    // L004C0E00
    private static void TDatapad_OrderBoxChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi;
        int ebx;

        if (Sender is CheckBox edi)
        {
            esi = Convert.ToInt32(edi.Tag);
            ebx = (byte)((edi.IsChecked != true ? 1 : 0) & 0x7F);
        }
        else
        {
            esi = AlliedGetControlTag(Sender);
            ebx = Unit_00513838_Proc_0051E8C4(Sender);

            if ((esi == 0x01 && ebx == 0x32) || (esi == 0x03 && Datapad.OrderBox.SelectedIndex == 0x32))
            {
                Datapad.OrderP1.SetM000220(0x01);
            }
            else
            {
                Datapad.OrderP1.SetM000220(0);
            }
        }

        Unit_00513838_Proc_0051EAB8(esi, ebx);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004C0F44
    private static void TDatapad_ArrMotherBoxChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        MothersPanelTagEnum ebx0 = (MothersPanelTagEnum)AlliedGetControlTag(Sender);
        int eax1 = Unit_00513838_Proc_0051E8C4(Sender);
        int esi;

        if (eax1 > 0)
        {
            esi = 0x01;
            eax1--;
        }
        else
        {
            esi = 0;
        }

        int ebx = (int)ebx0;

        switch (ebx0)
        {
            case MothersPanelTagEnum.ArrMotherBox:
                if (AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage() == AlliedVariables.s_TDatapad_Instance!.Departure)
                {
                    ebx = 0x09;
                }

                break;

            case MothersPanelTagEnum.ArrAltmotherBox:
                if (AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage() == AlliedVariables.s_TDatapad_Instance!.Departure)
                {
                    ebx = 0x0D;
                }

                break;
        }

        Unit_00513838_Proc_0051EF24(ebx, eax1);
        Unit_00513838_Proc_0051EF24(ebx + 1, esi);
    }

    // L0051EF24
    private static void Unit_00513838_Proc_0051EF24(int eax0, int edx0)
    {
        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                continue;
            }

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

            switch (eax0)
            {
                case 0x07:
                    eax1.FlightGroupStruct.StartFg = (byte)edx0;
                    break;

                case 0x08:
                    eax1.FlightGroupStruct.StartFgUsed = (byte)edx0;
                    break;

                case 0x09:
                    eax1.FlightGroupStruct.PrimaryStopFg = (byte)edx0;
                    break;

                case 0x0A:
                    eax1.FlightGroupStruct.PrimaryStopFgUsed = (byte)edx0;
                    break;

                case 0x0B:
                    eax1.FlightGroupStruct.SecondaryStopFg = (byte)edx0;
                    break;

                case 0x0C:
                    eax1.FlightGroupStruct.SecondaryStopFgUsed = (byte)edx0;
                    break;

                case 0x0D:
                    eax1.FlightGroupStruct.CaptureFg = (byte)edx0;
                    break;

                case 0x0E:
                    eax1.FlightGroupStruct.CaptureFgUsed = (byte)edx0;
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        Unit_00513838_Proc_0051467C();
    }

    // L004C2FA8
    private static void TDatapad_ArrMotherBoxDrawItem(DatapadWindow Datapad, ComboBox Sender, int index, object? A4, object? rect)
    {
        if (index < AlliedVariables.s_FlightGroupObjectsList.Count + 1)
        {
            ComboBoxItem item = Sender.GetItem(index);

            if (index > 0)
            {
                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index - 1);

                switch (eax2.FlightGroupStruct.ArrivalDifficulty)
                {
                    case (ArrivalDifficultyEnum)0x06:
                    case (ArrivalDifficultyEnum)0x07:
                        {
                            Graphics_TFont_SetColor(item, 0x00808080);
                            break;
                        }

                    default:
                        {
                            S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index - 1);

                            if (eax3.m001444[AlliedVariables.s_CurrentRegion - 1] == 0 && AlliedVariables.s_BlackenChkSetting)
                            {
                                Graphics_TFont_SetColor(item, 0x004F4E4F);
                            }
                            else
                            {
                                S0xFGObject eax4 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index - 1);
                                Graphics_TFont_SetColor(item, AlliedGetIffColor(eax4.FlightGroupStruct.Iff, 0));
                            }

                            break;
                        }
                }
            }
            else
            {
                Graphics_TFont_SetColor(item, 0x00FFFFFF);
            }
        }
    }

    private static bool _TDatapad_RoleBoxChangeCalled = false;

    // L004C1F28
    private static void TDatapad_RoleBoxChange(DatapadWindow Datapad, object? Sender)
    {
        if (_TDatapad_RoleBoxChangeCalled)
        {
            return;
        }

        _TDatapad_RoleBoxChangeCalled = true;

        Unit_00513838_Proc_0051E2AC(Sender);

        byte bl = AlliedVariables.s_V0x00543C9A;
        AlliedVariables.s_V0x00543C9A = 0;

        Datapad.UseRole1.IsChecked = Controls_TControl_GetText(Datapad.Edit1).Equals("0", StringComparison.Ordinal);
        Datapad.UseRole2.IsChecked = Controls_TControl_GetText(Datapad.Edit2).Equals("0", StringComparison.Ordinal);

        AlliedVariables.s_V0x00543C9A = bl;
        Unit_00513838_Proc_0051DB68();

        _TDatapad_RoleBoxChangeCalled = false;
    }

    // L0051E2AC
    public static void Unit_00513838_Proc_0051E2AC(object? eax0)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int edi;
            int ebp04;

            if (eax0 is ComboBox combo)
            {
                edi = combo.SelectedIndex;
                ebp04 = Convert.ToInt32(combo.Tag);
            }
            else if (eax0 is TextBox box)
            {
                edi = StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(box));
                ebp04 = Convert.ToInt32(box.Tag);
            }
            else
            {
                throw new InvalidOperationException();
            }

            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                switch (ebp04)
                {
                    case 1:
                        eax1.FlightGroupStruct.TacticalRoleUsed0 = (TacticalRoleUsedEnum)edi;
                        break;

                    case 2:
                        eax1.FlightGroupStruct.TacticalRoleUsed1 = (TacticalRoleUsedEnum)edi;
                        break;

                    case 3:
                        eax1.FlightGroupStruct.TacticalRole0 = (TacticalRoleEnum)edi;
                        break;

                    case 4:
                        eax1.FlightGroupStruct.TacticalRole1 = (TacticalRoleEnum)edi;
                        break;

                    case 5:
                        eax1.FlightGroupStruct.Comm = (byte)edi;
                        break;

                    case 6:
                        eax1.FlightGroupStruct.GlobalCargoIndex = (byte)edi;
                        break;

                    case 7:
                        eax1.FlightGroupStruct.SpecialCargoIndex = (byte)edi;
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }

            Unit_00513838_Proc_0051467C();
        }
    }

    // L004C2E78
    private static void TDatapad_UseRole1Click(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        if (Datapad.UseRole1.IsChecked == true)
        {
            Controls_TControl_SetText(Datapad.Edit1, "0");
        }
        else
        {
            Controls_TControl_SetText(Datapad.Edit1, "-1");
        }
    }

    // L004C2ED4
    private static void TDatapad_UseRole2Click(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        if (Datapad.UseRole2.IsChecked == true)
        {
            Controls_TControl_SetText(Datapad.Edit2, "0");
        }
        else
        {
            Controls_TControl_SetText(Datapad.Edit2, "-1");
        }
    }

    // L004C2018
    private static void TDatapad_OrderDescEdChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        Unit_00513838_Proc_0051467C();

        if (AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count > 0)
        {
            int ebx = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int esi = 0; esi < ebx; esi++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, esi))
                {
                    continue;
                }

                switch (AlliedVariables.s_TieFileVersion)
                {
                    case TieFileVersionEnum.XvT:
                    case TieFileVersionEnum.Bop:
                        {
                            string ebp14 = Unit_00511CD0_Proc_00511EB8(Controls_TControl_GetText(Datapad.OrderDescEd));
                            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                            eax1.FlightGroupStruct.Role = ebp14;
                            break;
                        }

                    case TieFileVersionEnum.XWA:
                        {
                            string ebp28 = Unit_00511CD0_Proc_00511E04(Controls_TControl_GetText(Datapad.OrderDescEd));
                            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                            eax1.FlightGroupStruct.PilotVoice = ebp28;
                            break;
                        }
                }
            }
        }
    }

    // L004C1538
    private static void TDatapad_ArrMinSpinChange(DatapadWindow Datapad, object? Sender)
    {
        string ebp0C_2 = Controls_TControl_GetText(Datapad.ArrMinSpin);

        if (!string.IsNullOrEmpty(ebp0C_2))
        {
            if (!string.Equals(ebp0C_2, "-", StringComparison.Ordinal))
            {
                if (AlliedVariables.s_V0x00543C9A != 0)
                {
                    int edi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

                    for (int esi = 0; esi < edi; esi++)
                    {
                        if (StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, esi))
                        {
                            string ebp0C_0 = Controls_TControl_GetText(Datapad.ArrMinSpin);
                            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                            eax1.FlightGroupStruct.ArrivalDelayMinutes = (byte)Allied_StrRec_to_int(ebp0C_0);
                        }

                        Unit_00513838_Proc_0051467C();
                    }
                }
            }
        }
    }

    // L004C1638
    private static void TDatapad_ArrSecSpinChange(DatapadWindow Datapad, object? Sender)
    {
        string ebp0C_2 = Controls_TControl_GetText(Datapad.ArrSecSpin);

        if (!string.IsNullOrEmpty(ebp0C_2))
        {
            if (!string.Equals(ebp0C_2, "-", StringComparison.Ordinal))
            {
                if (AlliedVariables.s_V0x00543C9A != 0)
                {
                    int edi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

                    for (int esi = 0; esi < edi; esi++)
                    {
                        if (StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, esi))
                        {
                            string ebp0C_0 = Controls_TControl_GetText(Datapad.ArrSecSpin);
                            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                            eax1.FlightGroupStruct.ArrivalDelaySeconds = (byte)Allied_StrRec_to_int(ebp0C_0);
                        }

                        Unit_00513838_Proc_0051467C();
                    }
                }
            }
        }
    }

    // L004C34E0
    private static void TDatapad_DifficultyBoxChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedGetControlTag(Sender);
        int edx1 = Unit_00513838_Proc_0051E8C4(Sender);
        Unit_00513838_Proc_0051E614(esi, edx1);
        MapWindowImpl.TMapForm_Proc_004F6B48(AlliedVariables.s_TMapForm_Instance!);
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_0051DB68();
    }

    // L004C3484
    private static void TDatapad_DepartWhenBoxChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedGetControlTag(Sender);
        int edx = Unit_00513838_Proc_0051E8C4(Sender);
        Unit_00513838_Proc_0051EF24(esi, edx);
    }

    // L004C1738
    private static void TDatapad_DepMinChange(DatapadWindow Datapad, object? Sender)
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

            string ebp04 = Controls_TControl_GetText(Datapad.DepMin);
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            eax1.FlightGroupStruct.DepartureDelayMinutes = (byte)StrRec_try_to_int_L0051E3BC(ebp04);
        }

        Unit_00513838_Proc_0051467C();
    }

    // L004C17EC
    private static void TDatapad_DepSecChange(DatapadWindow Datapad, object? Sender)
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

            string ebp04 = Controls_TControl_GetText(Datapad.DepSec);
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            eax1.FlightGroupStruct.DepartureDelaySeconds = (byte)StrRec_try_to_int_L0051E3BC(ebp04);
        }

        Unit_00513838_Proc_0051467C();
    }

    // L004C18A0
    private static void TDatapad_FGgoalListClick(DatapadWindow Datapad, object? Sender)
    {
        AlliedVariables.s_V0x00543B48 = Datapad.FGgoalList.SelectedIndex;
        AlliedVariables.s_V0x005B6B64 = S0xTieFlightGroupGoal.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].ToByteArray());
        AlliedVariables.s_V0x00543C9A = 0;
        Form1WindowImpl.TForm1_Proc_0052870C(AlliedVariables.s_AlliedForm1Window!);
        AlliedVariables.s_V0x00543C9A = 0x01;
        Datapad.FGgoalList.SelectedIndex = AlliedVariables.s_V0x00543B48;
    }

    // L004C19D4
    private static void TDatapad_FGgoalListDrawItem(DatapadWindow Datapad, ListBox Sender, int index, object? A4, object? rect)
    {
        ListBoxItem item = Sender.GetItem(index);
        byte eax2 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Goals[index].AppliesToTeams[1];
        byte iff = eax2 == (byte)255 ? (byte)0 : AlliedVariables.s_V0x00543C78[eax2];
        Graphics_TFont_SetColor(item, AlliedGetIffColor(iff, 0));
    }

    // L004C21A4
    private static void TDatapad_FGGoalTeamBoxChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[1] = (byte)Datapad.FGGoalTeamBox.SelectedIndex;

                AlliedVariables.s_V0x00543C9A = 0;

                if (Allied_ComboBox_GetSelectedIndex(Datapad.FGGoalTeamBox) == 0)
                {
                    eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[0] = 0x01;
                    Datapad.FGGoalUnk.IsChecked = true;
                }
                else
                {
                    eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[0] = 0;
                    Datapad.FGGoalUnk.IsChecked = false;
                }

                AlliedVariables.s_V0x00543C9A = 0x01;
            }
        }

        TDatapad__PROC_004C191C(AlliedVariables.s_TDatapad_Instance!);
        Unit_00513838_Proc_00517258();
    }

    // L004C23DC
    private static void TDatapad_FGGoalPointsSpinChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        string ebp08_1 = Controls_TControl_GetText(Datapad.FGGoalPointsSpin);

        if (!string.IsNullOrEmpty(ebp08_1))
        {
            if (!string.Equals(ebp08_1, "-", StringComparison.Ordinal))
            {
                int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

                for (int ebx = 0; ebx < esi; ebx++)
                {
                    if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                    {
                        continue;
                    }

                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].Points = (byte)(Spin_TSpinEdit_GetValue(Datapad.FGGoalPointsSpin) / 0x19);
                }
            }
        }

        TDatapad__PROC_004C191C(AlliedVariables.s_TDatapad_Instance!);
        Unit_00513838_Proc_0051467C();
    }

    // L004C24F4
    private static void TDatapad_FGGoalUnkClick(DatapadWindow Datapad, object? Sender)
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
            eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[0] = Datapad.FGGoalUnk.IsChecked == true ? (byte)1 : (byte)0;
        }

        Unit_00513838_Proc_0051467C();
    }

    // L004C22D0
    private static void TDatapad_FGGoalTimeSpinChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        string ebp0C_2 = Controls_TControl_GetText(Datapad.FGGoalTimeSpin);

        if (!string.IsNullOrEmpty(ebp0C_2))
        {
            if (!string.Equals(ebp0C_2, "-", StringComparison.Ordinal))
            {
                int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

                for (int ebx = 0; ebx < esi; ebx++)
                {
                    if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
                    {
                        continue;
                    }

                    string ebp0C_0 = Controls_TControl_GetText(Datapad.FGGoalTimeSpin);
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[9] = (byte)Allied_StrRec_to_int(ebp0C_0);
                }
            }
        }
    }

    // L004C3434
    private static void TDatapad_AddOrderBtnClick(DatapadWindow Datapad, object? Sender)
    {
        S0xOrdObject ebx = new();
        ebx.m000004 = Form1WindowImpl.TForm1_Proc_0052B144(AlliedVariables.s_AlliedForm1Window!);
        AlliedVariables.s_V0x00543CFC.Add(ebx);
        Unit_00513838_Proc_0051CA50();
    }

    // L004C3528
    private static void TDatapad_OrdDescTextChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        Unit_00513838_Proc_0051467C();
        string ebp04 = Controls_TControl_GetText(Datapad.OrdDescText);

        if (ebp04.Length > 0)
        {
            AlliedVariables.s_V0x0054AEAC.unk000000[AlliedVariables.s_V0x00543CC8 - 1] = (byte)ebp04[0];
        }
        else
        {
            AlliedVariables.s_V0x0054AEAC.unk000000[AlliedVariables.s_V0x00543CC8 - 1] = 0;
        }

        if (ebp04.Length > 1)
        {
            string ebp48 = ebp04[2..];
            S0x00533EA8 ebp44 = Unit_00511CD0_Proc_00511FC4(ebp48);
            AlliedVariables.s_V0x0054B67C[AlliedVariables.s_V0x00543CC8 - 1] = ebp44;
        }
        else
        {
            S0x00533EA8 ebp44 = Unit_00511CD0_Proc_00511FC4(string.Empty);
            AlliedVariables.s_V0x0054B67C[AlliedVariables.s_V0x00543CC8 - 1] = ebp44;
        }
    }

    // L004C257C
    private static void TDatapad_GGPointsEDChange(DatapadWindow Datapad, object? Sender)
    {
        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[Datapad.GGRadios.GetItemIndex()].Points = (byte)(Spin_TSpinEdit_GetValue(Datapad.GGPointsED) / 0x19);
        Unit_00513838_Proc_005146A4();
    }

    // L004C25B4
    private static void TDatapad_GGUnk2Change(DatapadWindow Datapad, object? Sender)
    {
        string ebp04 = Controls_TControl_GetText(Datapad.GGUnk2);
        char[] name = AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[Datapad.GGRadios.GetItemIndex()].Name.ToCharArray();
        // todo
        if (name.Length > 7)
        {
            name[7] = (char)(byte)StrRec_try_to_int_L0051E3BC(ebp04);
        }
        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[Datapad.GGRadios.GetItemIndex()].Name = new string(name);
        Unit_00513838_Proc_005146A4();
    }

    // L004C2620
    private static void TDatapad_GGUnk3Change(DatapadWindow Datapad, object? Sender)
    {
        string ebp04 = Controls_TControl_GetText(Datapad.GGUnk3);
        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[Datapad.GGRadios.GetItemIndex()].TimePassed = (byte)StrRec_try_to_int_L0051E3BC(ebp04);
        Unit_00513838_Proc_005146A4();
    }

    // L004C268C
    private static void TDatapad_GGUnk4_1Change(DatapadWindow Datapad, TextBox Sender)
    {
        int edi = Convert.ToInt32(Sender.Tag);
        string ebp04 = Controls_TControl_GetText(Sender);
        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[Datapad.GGRadios.GetItemIndex()].m000034[edi - 1] = (byte)StrRec_try_to_int_L0051E3BC(ebp04);
        Unit_00513838_Proc_005146A4();
    }

    // L004C2990
    private static void TDatapad_MsgtextEdChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9B == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            string ebp40 = Unit_00511CD0_Proc_0051213C(Controls_TControl_GetText(AlliedVariables.s_TDatapad_Instance!.MsgtextEd));
            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543B10);
            eax1.RadioMessage.Message = ebp40;
            Unit_00513838_Proc_0051477C();
            Unit_00513838_Proc_0051442C();
        }
    }

    // L004C2700
    private static void TDatapad_MsgColorBoxChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9B == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            Unit_00513838_Proc_0051442C();
        }

        int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, ebx))
            {
                continue;
            }

            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebx);
            eax1.RadioMessage.Side = (byte)Allied_ComboBox_GetSelectedIndex(Datapad.MsgColorBox);
        }

        Unit_00513838_Proc_0051477C();
    }

    // L004C27B4
    private static void TDatapad_MsgDelaySpinChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9B == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            Unit_00513838_Proc_0051442C();
        }

        int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, ebx))
            {
                continue;
            }

            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebx);
            eax1.RadioMessage.TimePassed = (byte)Spin_TSpinEdit_GetValue(Datapad.MsgDelaySpin);
        }

        string ebp08_0 = Allied_TimeInSeconds_ToMinutesSecondsString(Allied_Time_ToSeconds_L0051F534(Spin_TSpinEdit_GetValue(Datapad.MsgDelaySpin)));
        Controls_TControl_SetText(Datapad.MsgDelayLab, "Delay:  " + ebp08_0);
    }

    // L004C34B0
    private static void TDatapad_SeenByListClick(DatapadWindow Datapad, object? Sender)
    {
        Form1WindowImpl.Unit_00513838_Proc_005183C0();
        AlliedVariables.s_V0x00543B55 = 0x01;
        AlliedVariables.s_V0x005B6B58.M0000A6 = (byte)Unit_00513838_Proc_0051D874(AlliedVariables.s_V0x00543B10);
        Unit_00513838_Proc_0051477C();
    }

    // L004C28C4
    private static void TDatapad_MsgUnk3Change(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9B == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            Unit_00513838_Proc_0051442C();
        }

        int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, ebx))
            {
                continue;
            }

            string ebp04 = Controls_TControl_GetText(Datapad.MsgUnk3);
            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebx);
            eax1.RadioMessage.Fg = (byte)StrRec_try_to_int_L0051E3BC(ebp04);
        }

        Unit_00513838_Proc_0051477C();
    }

    // L004C2C7C
    private static void TDatapad_Cond5LabClick(DatapadWindow Datapad, object? Sender)
    {
        CondToolUserControlImpl.Unit_00513838_Proc_0051CFC4(0x05);
    }

    // L004C3678
    private static void TDatapad_Cond5LabDblClick(DatapadWindow Datapad, object? Sender)
    {
        CondToolUserControlImpl.TCondToolForm_Cond1LabDblClick(AlliedVariables.s_TCondToolForm_Instance!, Datapad.Cond5Lab);
    }

    // L004C2C88
    private static void TDatapad_Cond6LabClick(DatapadWindow Datapad, object? Sender)
    {
        CondToolUserControlImpl.Unit_00513838_Proc_0051CFC4(0x06);
    }

    // L004C2C94
    private static void TDatapad_and5AndClick(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9B == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count <= 0)
        {
            return;
        }

        Unit_00513838_Proc_0051442C();

        int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, ebx))
            {
                continue;
            }

            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebx);
            eax1.RadioMessage.TriggersOperator = Datapad.and5And.IsChecked == true ? (byte)1 : (byte)0;
        }
    }

    // L004C2D1C
    private static void TDatapad_MsgIDChange(DatapadWindow Datapad, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9B == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            Unit_00513838_Proc_0051442C();
        }

        int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, ebx))
            {
                continue;
            }

            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebx);
            Unit_00511CD0_Proc_00511D50(Controls_TControl_GetText(Datapad.MsgID), eax1.RadioMessage.M00007C);
        }
    }

    // L00511D50
    private static void Unit_00511CD0_Proc_00511D50(string eax0, byte[] edx0)
    {
        if (edx0.Length != 0x08)
        {
            throw new ArgumentOutOfRangeException();
        }

        int esi;

        if (eax0.Length == 0)
        {
            esi = 0;
        }
        else if (eax0.Length < 0x08)
        {
            esi = eax0.Length;
        }
        else
        {
            esi = 0x08;
        }

        for (int i = 0; i < esi; i++)
        {
            edx0[i] = (byte)eax0[i];
        }

        for (int eax = esi; eax < 0x08; eax++)
        {
            edx0[eax] = 0;
        }
    }

    // L004C2DF8
    private static void TDatapad_FGGoalPercentChange(DatapadWindow Datapad, object? Sender)
    {
        Form1WindowImpl.TForm1_Proc_00528DFC(AlliedVariables.s_AlliedForm1Window!);
    }

    // L004C2E08
    private static void TDatapad_FGGoalCondChange(DatapadWindow Datapad, object? Sender)
    {
        Form1WindowImpl.TForm1_Proc_00528DFC(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_00517258();
    }

    // L004C32C8
    public static void TDatapad_Proc_004C32C8(DatapadWindow Datapad, out int left, out int top)
    {
        int ebp0C = 0;
        int ebp08 = 0;

        int eax1 = ((int)AlliedVariables.s_AlliedForm1Window!.ActualWidth - (int)AlliedVariables.s_AlliedForm1Window!.OverallPages.ActualWidth - 0x1EA) / 2;

        if (eax1 > 0)
        {
            ebp0C = eax1 + (int)AlliedVariables.s_AlliedForm1Window!.Left + (int)AlliedVariables.s_AlliedForm1Window!.OverallPages.ActualWidth;
        }
        else
        {
            ebp0C = (int)AlliedVariables.s_AlliedForm1Window!.Left + (int)AlliedVariables.s_AlliedForm1Window!.ActualWidth - 0x1EA - 0x05;
        }

        switch (TApplication_GetWidth())
        {
            case 0x320:
                ebp0C -= 0x08;
                break;

            case 0x400:
                if (AlliedVariables.s_AlliedForm1Window!.M00022B() == 0x02)
                {
                    ebp0C += 0x0A;
                }
                else
                {
                    ebp0C -= 0x0A;
                }

                break;
        }

        int eax2 = ((int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight - 0x1F4) / 2;

        if (eax2 > 0)
        {
            ebp08 = eax2 + (int)AlliedVariables.s_AlliedForm1Window!.Top + (int)AlliedVariables.s_AlliedForm1Window!.ControlBar1.ActualHeight + 0x2B;
        }
        else
        {
            ebp08 = (int)AlliedVariables.s_AlliedForm1Window!.Top + (int)AlliedVariables.s_AlliedForm1Window!.ActualHeight - 0x1FC;
        }

        if (TApplication_GetHeight() == 0x258)
        {
            if (AlliedVariables.s_AlliedForm1Window!.M00022B() == 0x02)
            {
                ebp08 -= 0x05;
            }
        }

        if (Application_GetPixelsPerInch() == 0x78)
        {
            const double d = 0.8;
            ebp0C = (int)Math.Round(ebp0C * d);
            ebp08 = (int)Math.Round(ebp08 * d);
        }

        left = ebp0C;
        top = ebp08;
    }

    // L0051EAB8
    private static void Unit_00513838_Proc_0051EAB8(int eax0, int edx0)
    {
        int ebp04 = edx0;

        if (AlliedVariables.s_TDatapad_Instance!.OrderBox.SelectedIndex == 0x32 && eax0 == 0x03)
        {
            ebp04--;

            if (ebp04 < 0)
            {
                ebp04 = 0;
            }
        }

        if ((eax0 == 0x0E && AlliedVariables.s_TDatapad_Instance!.T1Class.SelectedIndex == 0x02)
            || (eax0 == 0x10 && AlliedVariables.s_TDatapad_Instance!.T2Class.SelectedIndex == 0x02)
            || (eax0 == 0x09 && AlliedVariables.s_TDatapad_Instance!.T3Class.SelectedIndex == 0x02)
            || (eax0 == 0x0A && AlliedVariables.s_TDatapad_Instance!.T4Class.SelectedIndex == 0x02))
        {
            ebp04 = (int)AlliedConvertShipSeqToCraftId((ShipSeqEnum)ebp04);
        }

        int edi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int esi = 0; esi < edi; esi++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, esi))
            {
                continue;
            }

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
            switch (eax0 - 0x03)
            {
                case 0:
                    eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var0 = (byte)ebp04;
                    break;

                case 1:
                    eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var1 = (byte)ebp04;
                    break;

                case 2:
                    eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var2 = (byte)ebp04;
                    break;

                case 3:
                    //eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Parameters3 = (byte)ebp04;
                    //break;
                    throw new NotImplementedException();
            }

            if (eax0 == 0 || eax0 == 0x01)
            {
                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax3.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(eax2.FlightGroupStruct));
            }

            switch (eax0)
            {
                case 0x07:
                    {
                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SecondaryTarget.ParameterA = 0;
                        break;
                    }

                case 0x08:
                    {
                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SecondaryTarget.ParameterB = 0;
                        break;
                    }

                case 0x0D:
                    {
                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].PrimaryTarget.ParameterA = 0;
                        break;
                    }

                case 0x0F:
                    {
                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].PrimaryTarget.ParameterB = 0;
                        break;
                    }
            }
        }

        Unit_00513838_Proc_0051467C();

        switch (eax0)
        {
            case 0x01:
                {
                    if (AlliedVariables.s_V0x00543C9F != 0)
                    {
                        break;
                    }

                    if (AlliedVariables.s_TDatapad_Instance!.OrderBox.SelectedIndex < AlliedVariables.s_Strings_OrdTexts.GetCount())
                    {
                        int edx1 = AlliedVariables.s_TDatapad_Instance!.OrderBox.SelectedIndex;
                        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.XWOrderLab, AlliedVariables.s_Strings_OrdTexts.GetText(edx1));
                    }
                    else
                    {
                        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.XWOrderLab, "Unknown");
                    }

                    break;
                }

            case 0x07:
                {
                    Unit_00513838_Proc_00517AC0(AlliedVariables.s_TDatapad_Instance!.T3Index, (TieClassEnum)ebp04, 0);
                    break;
                }

            case 0x08:
                {
                    Unit_00513838_Proc_00517AC0(AlliedVariables.s_TDatapad_Instance!.T4Index, (TieClassEnum)ebp04, 0);
                    break;
                }

            case 0x0D:
                {
                    Unit_00513838_Proc_00517AC0(AlliedVariables.s_TDatapad_Instance!.T1Index, (TieClassEnum)ebp04, 0);
                    break;
                }

            case 0x0F:
                {
                    Unit_00513838_Proc_00517AC0(AlliedVariables.s_TDatapad_Instance!.T2Index, (TieClassEnum)ebp04, 0);
                    break;
                }
        }
;

        switch (eax0)
        {
            case 0x01:
            case 0x07:
            case 0x08:
            case 0x09:
            case 0x0A:
            case 0x0B:
            case 0x0D:
            case 0x0E:
            case 0x0F:
            case 0x10:
            case 0x11:
                OrderSelWindowImpl.TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);
                break;
        }

        switch (eax0)
        {
            case 0x01:
            case 0x03:
            case 0x04:
                Unit_00513838_Proc_00516414((TieOrderIdEnum)Allied_ComboBox_GetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.OrderBox));
                break;
        }

        if (eax0 == 0x02)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);

            if (eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Throttle < 0x0A)
            {
                AlliedVariables.s_TDatapad_Instance!.MGLTBox.IsEnabled = false;
            }
            else
            {
                AlliedVariables.s_TDatapad_Instance!.MGLTBox.IsEnabled = true;
            }
        }

        Unit_00513838_Proc_00520924();
    }
}
