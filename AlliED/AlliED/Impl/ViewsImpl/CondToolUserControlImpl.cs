using AlliED.Controls;
using AlliED.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlliED.Impl.ViewsImpl;

internal static class CondToolUserControlImpl
{
    public static void Register(CondToolUserControl window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(CondToolUserControl window)
    {
        //window.Activated += (s, e) => TCondToolForm_FormActivate(window);
        //window.Closed += (s, e) => TCondToolForm_FormClose(window);

        window.IsVisibleChanged += (s, e) =>
        {
            if (window.Visibility == System.Windows.Visibility.Visible)
            {
                TCondToolForm_FormActivate(window);
            }
            else
            {
                TCondToolForm_FormClose(window);
            }
        };

        window.AddToClipBtn.Click += (s, e) => TCondToolForm_AddToClipBtnClick(window);
        window.PercentBox.SelectionChanged += (s, e) => TCondToolForm_PercentBoxChange(window, s);
        window.ClassBox.SelectionChanged += (s, e) => TCondToolForm_ClassBoxChange(window);
        window.IndexBox.SelectionChanged += (s, e) => TCondToolForm_IndexBoxChange(window);
        window.CondBox.SelectionChanged += (s, e) => TCondToolForm_CondBoxChange(window);
        window.CondUnk2.SelectionChanged += (s, e) => TCondToolForm_CondUnk2Change(window);
        window.RegionFGBox.SelectionChanged += (s, e) => TCondToolForm_CondUnk1Change(window);
        window.FGStrFail.TextChanged += (s, e) => TCondToolForm_FGStrIncompChange(window, (TextBox)s);
        window.FGStrSucc.TextChanged += (s, e) => TCondToolForm_FGStrIncompChange(window, (TextBox)s);
        window.FGStrIncomp.TextChanged += (s, e) => TCondToolForm_FGStrIncompChange(window, (TextBox)s);

        window.Cond1Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond1Lab,
            MouseAction.LeftClick,
            s => TCondToolForm_Cond1LabClick(window, (TextBlock)s),
            s => true));
        window.Cond1Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond1Lab,
            MouseAction.LeftDoubleClick,
            s => TCondToolForm_Cond1LabDblClick(window, (TextBlock)s),
            s => true));

        window.Cond2Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond2Lab,
            MouseAction.LeftClick,
            s => TCondToolForm_Cond1LabClick(window, (TextBlock)s),
            s => true));
        window.Cond2Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond2Lab,
            MouseAction.LeftDoubleClick,
            s => TCondToolForm_Cond1LabDblClick(window, (TextBlock)s),
            s => true));

        window.Cond3Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond3Lab,
            MouseAction.LeftClick,
            s => TCondToolForm_Cond1LabClick(window, (TextBlock)s),
            s => true));
        window.Cond3Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond3Lab,
            MouseAction.LeftDoubleClick,
            s => TCondToolForm_Cond1LabDblClick(window, (TextBlock)s),
            s => true));

        window.Cond4Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond4Lab,
            MouseAction.LeftClick,
            s => TCondToolForm_Cond1LabClick(window, (TextBlock)s),
            s => true));
        window.Cond4Lab.InputBindings.Add(CustomCommand.CreateMouseBinding(
            window.Cond4Lab,
            MouseAction.LeftDoubleClick,
            s => TCondToolForm_Cond1LabDblClick(window, (TextBlock)s),
            s => true));

        window.and2And.Click += (s, e) => TCondToolForm_and2AndClick(window);
        window.and2Or.Click += (s, e) => TCondToolForm_and2AndClick(window);
        window.and4And.Click += (s, e) => TCondToolForm_and4AndClick(window);
        window.And4Or.Click += (s, e) => TCondToolForm_and4AndClick(window);
        window.GlbAnd.Click += (s, e) => TCondToolForm_GlbAndClick(window);
        window.GlbOr.Click += (s, e) => TCondToolForm_GlbAndClick(window);
    }

    // L00510298
    private static void FormCreate(CondToolUserControl CondToolForm)
    {
        AlliedVariables.s_V0x00543990 = 0x01;
        CondToolForm.PercentBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_V0x00543BC8);
        CondToolForm.ClassBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_V0x00543BCC);
        CondToolForm.CondBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_V0x00543BD4);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Conds", AlliedVariables.s_V0x00543BD4, CondToolForm.CondBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Percents", AlliedVariables.s_V0x00543BC8, CondToolForm.PercentBox, true);
        AlliedLoadTStringsItemsFromFileAndFillComboBox("Class", AlliedVariables.s_V0x00543BCC, CondToolForm.ClassBox, true);
        CondToolForm.RegionFGBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
    }

    // L0051015C
    public static void TCondToolForm_PROC_0051015C(CondToolUserControl eax0, int edx0)
    {
        if (AlliedVariables.s_V0x00543990 == 0)
        {
            return;
        }

        Graphics_TFont_SetColor(eax0.Cond1Lab, 0);
        Graphics_TFont_SetColor(eax0.Cond2Lab, 0);
        Graphics_TFont_SetColor(eax0.Cond3Lab, 0);
        Graphics_TFont_SetColor(eax0.Cond4Lab, 0);

        Graphics_TFont_SetColor(AlliedVariables.s_TDatapad_Instance!.Cond5Lab, 0);
        Graphics_TFont_SetColor(AlliedVariables.s_TDatapad_Instance!.Cond6Lab, 0);

        switch (edx0)
        {
            case 0x01:
                Graphics_TFont_SetColor(eax0.Cond1Lab, 0x009103A9);
                break;

            case 0x02:
                Graphics_TFont_SetColor(eax0.Cond2Lab, 0x009103A9);
                break;

            case 0x03:
                Graphics_TFont_SetColor(eax0.Cond3Lab, 0x009103A9);
                break;

            case 0x04:
                Graphics_TFont_SetColor(eax0.Cond4Lab, 0x009103A9);
                break;

            case 0x05:
                Graphics_TFont_SetColor(AlliedVariables.s_TDatapad_Instance!.Cond5Lab, 0x009103A9);
                break;

            case 0x06:
                Graphics_TFont_SetColor(AlliedVariables.s_TDatapad_Instance!.Cond6Lab, 0x009103A9);
                break;
        }
    }

    // L0050F2E8
    public static void TCondToolForm_Proc_0050F2E8(CondToolUserControl eax0, S0xTieTrigger edx0)
    {
        S0xTieTrigger ebp06 = S0xTieTrigger.FromByteArray(edx0.ToByteArray());

        if (AlliedVariables.s_V0x00543990 != 0)
        {
            byte ebp07 = AlliedVariables.s_V0x00543C9A;
            bool ebp08 = AlliedVariables.s_V0x00543B53;

            AlliedVariables.s_V0x00543C9A = 0;

            Allied_ComboBox_SetSelectedIndex(eax0.PercentBox, (int)ebp06.Amount);
            Allied_ComboBox_SetSelectedIndex(eax0.CondBox, (int)ebp06.Condition);
            Allied_ComboBox_SetSelectedIndex(eax0.ClassBox, (int)ebp06.VariableType);
            Unit_00513838_Proc_00517AC0(eax0.IndexBox, ebp06.VariableType, ebp06.Variable);

            if (ebp06.Condition == TieConditionEnum.Within || ebp06.Condition == TieConditionEnum.Beyond)
            {
                eax0.RegionFGBox.SetItems(AlliedVariables.s_Strings_Regions);
                eax0.RegionFGBox.SelectedIndex = ebp06.Parameter;
            }
            else
            {
                eax0.RegionFGBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                eax0.RegionFGBox.PutItem(0, "0");
                eax0.RegionFGBox.SelectedIndex = ebp06.Parameter;
            }

            //Controls_TControl_SetText(eax0.CondUnk2, ebp06.Parameter2.ToString(CultureInfo.InvariantCulture));

            AlliedVariables.s_V0x00543B53 = ebp08;
            AlliedVariables.s_V0x00543C9A = ebp07;
        }
    }

    // L0051009C
    public static void TCondToolForm__PROC_0051009C(CondToolUserControl eax0, bool edx0, bool ecx0, bool A4)
    {
        if (AlliedVariables.s_V0x00543990 == 0)
        {
            return;
        }

        AlliedVariables.s_V0x00543C9A = 0;

        eax0.GlbOr.IsChecked = edx0;
        eax0.GlbAnd.IsChecked = eax0.GlbOr.IsChecked != true;

        eax0.and2Or.IsChecked = ecx0;
        eax0.and2And.IsChecked = eax0.and2Or.IsChecked != true;

        eax0.And4Or.IsChecked = A4;
        eax0.and4And.IsChecked = eax0.And4Or.IsChecked != true;

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L0050F450
    public static void TCondToolForm_Proc_0050F450(CondToolUserControl eax0, S0xTieTriggers edx0)
    {
        S0xTieTriggers ebp20 = S0xTieTriggers.FromByteArray(edx0.ToByteArray());

        if (AlliedVariables.s_V0x00543990 != 0)
        {
            Controls_TControl_SetText(eax0.Cond1Lab, Unit_00511CD0_Proc_005122D0(ebp20.Trigger_0[0]));
            Controls_TControl_SetText(eax0.Cond2Lab, Unit_00511CD0_Proc_005122D0(ebp20.Trigger_0[1]));
            Controls_TControl_SetText(eax0.Cond3Lab, Unit_00511CD0_Proc_005122D0(ebp20.Trigger_1[0]));
            Controls_TControl_SetText(eax0.Cond4Lab, Unit_00511CD0_Proc_005122D0(ebp20.Trigger_1[1]));

            eax0.Cond1Lab.Update();
            eax0.Cond2Lab.Update();
            eax0.Cond3Lab.Update();
            eax0.Cond4Lab.Update();
        }
    }

    // L005103A4
    private static void TCondToolForm_FormClose(CondToolUserControl CondToolForm)
    {
        AlliedVariables.s_V0x00543990 = 0;
    }

    // L00510414
    private static void TCondToolForm_FormActivate(CondToolUserControl CondToolForm)
    {
        AlliedVariables.s_V0x00543990 = 0x01;
    }

    // L00510834
    public static void TCondToolForm__PROC_00510834(CondToolUserControl? CondToolForm)
    {
        if (CondToolForm is null)
        {
            return;
        }

        if (AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage() == AlliedVariables.s_TDatapad_Instance!.FGGoals)
        {
            CondToolForm.IndexBox.SetItems(AlliedVariables.s_Strings_Musts);
            CondToolForm.ClassBox.SetItems(AlliedVariables.s_V0x00543BC0);
            CondToolForm.ClassBox.SelectedIndex = AlliedVariables.s_V0x00543B0C;
            CondToolForm.ClassBox.IsEnabled = false;
            CondToolForm.ClassBox.Update();
            Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label19, false);
            Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label2, true);
        }
        else
        {
            CondToolForm.ClassBox.SetItems(AlliedVariables.s_V0x00543BCC);
            Controls_TControl_SetColor(CondToolForm.ClassBox, 0x00FFFFFF);
            CondToolForm.ClassBox.IsEnabled = true;
            CondToolForm.ClassBox.Update();
            Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label19, true);
            Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label2, false);
        }

        DatapadFGPageEnum eax1 = (DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag);

        switch (eax1)
        {
            case DatapadFGPageEnum.Arrival:
            case DatapadFGPageEnum.Departure:
            case DatapadFGPageEnum.FGGoals:
            case DatapadFGPageEnum.Jump:
            case DatapadFGPageEnum.GGoals:
            case DatapadFGPageEnum.Messages:
                {
                    switch (eax1)
                    {
                        case DatapadFGPageEnum.Arrival:
                        case DatapadFGPageEnum.Departure:
                        case DatapadFGPageEnum.Jump:
                        case DatapadFGPageEnum.GGoals:
                        case DatapadFGPageEnum.Messages:
                            Controls_TControl_SetVisible(CondToolForm.Cond12Panel, true);
                            break;

                        default:
                            Controls_TControl_SetVisible(CondToolForm.Cond12Panel, false);
                            break;
                    }

                    int ebx = 0;

                    switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
                    {
                        case DatapadFGPageEnum.Arrival:
                        case DatapadFGPageEnum.GGoals:
                        case DatapadFGPageEnum.Messages:
                            Controls_TControl_SetHeight(CondToolForm.Cond12Panel, AlliedPixelsScaleDiv(200));
                            ebx = (int)CondToolForm.Cond12Panel.Height;
                            break;

                        default:
                            Controls_TControl_SetHeight(CondToolForm.Cond12Panel, AlliedPixelsScaleDiv(0x59));
                            ebx = (int)CondToolForm.Cond12Panel.Height;
                            break;
                    }

                    switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
                    {
                        case DatapadFGPageEnum.FGGoals:
                        case DatapadFGPageEnum.GGoals:
                            Controls_TControl_SetVisible(CondToolForm.StrPanel, true);
                            ebx += (int)CondToolForm.StrPanel.Height;
                            break;

                        default:
                            Controls_TControl_SetVisible(CondToolForm.StrPanel, false);
                            break;
                    }

                    if ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag) == DatapadFGPageEnum.FGGoals)
                    {
                        int height = AlliedVariables.s_TCondToolForm_Instance!.GetClientHeight();
                        if (height != 0)
                        {
                            Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!.Panel2, height);
                        }

                        if (AlliedVariables.s_V0x00543C64[4])
                        {
                            Controls_TControl_SetVisible(AlliedVariables.s_TDatapad_Instance!.Panel2, false);
                        }
                        else
                        {
                            Controls_TControl_SetVisible(AlliedVariables.s_TDatapad_Instance!.Panel2, true);
                        }
                    }
                    else
                    {
                        Controls_TControl_SetHeight(AlliedVariables.s_TDatapad_Instance!.Panel2, ebx + (int)CondToolForm.Selectors.Height);
                        Controls_TControl_SetVisible(AlliedVariables.s_TDatapad_Instance!.Panel2, true);
                    }

                    AlliedVariables.s_TCondToolForm_Instance!.Update();
                    AlliedVariables.s_TDatapad_Instance!.Panel2.Update();
                    break;
                }

            default:
                {
                    Controls_TControl_SetVisible(AlliedVariables.s_TDatapad_Instance!.Panel2, false);
                    break;
                }
        }

        switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Arrival:
                if (Application_GetPixelsPerInch() == 0x60)
                {
                    Controls_TControl_SetTop(AlliedVariables.s_TDatapad_Instance!.MothersPanel, 0xAF);
                }
                else
                {
                    Controls_TControl_SetTop(AlliedVariables.s_TDatapad_Instance!.MothersPanel, AlliedPixelsScaleMul(0xAF));
                }

                Controls_TControl_SetVisible(AlliedVariables.s_TDatapad_Instance!.MothersPanel, true);
                break;

            case DatapadFGPageEnum.Departure:
                if (Application_GetPixelsPerInch() == 0x60)
                {
                    Controls_TControl_SetTop(AlliedVariables.s_TDatapad_Instance!.MothersPanel, 0x8A);
                }
                else
                {
                    Controls_TControl_SetTop(AlliedVariables.s_TDatapad_Instance!.MothersPanel, AlliedPixelsScaleMul(0x8A));
                }

                Controls_TControl_SetVisible(AlliedVariables.s_TDatapad_Instance!.MothersPanel, true);
                break;

            default:
                Controls_TControl_SetVisible(AlliedVariables.s_TDatapad_Instance!.MothersPanel, false);
                break;
        }
    }

    // L00510018
    public static void TCondToolForm_Proc_00510018(CondToolUserControl eax0, TextBlock edx0, TextBlock ecx0, S0xTieFlightGroupTriggerPair A4)
    {
        Controls_TControl_SetText(edx0, Unit_00511CD0_Proc_005122D0(A4.Triggers[0]));
        Controls_TControl_SetText(ecx0, Unit_00511CD0_Proc_005122D0(A4.Triggers[1]));
    }

    // L005143FC
    public static void Unit_00513838_Proc_005143FC(RadioButton eax0, RadioButton edx0, bool ecx0)
    {
        eax0.IsChecked = ecx0;
        edx0.IsChecked = eax0.IsChecked != true;
    }

    // L005103D0
    private static void TCondToolForm_AddToClipBtnClick(CondToolUserControl CondToolForm)
    {
        S0xCondObjectStruct ebx = new();
        ebx.m000004 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
        AlliedVariables.s_V0x00543CF8.Add(ebx);
        Unit_00513838_Proc_0051CB44();
    }

    // L0050F558
    private static S0xTieTrigger TCondToolForm__PROC_0050F558(CondToolUserControl CondToolForm)
    {
        S0xTieTrigger ebp06 = new();

        if (AlliedVariables.s_V0x00543990 != 0)
        {
            ebp06.Amount = (TieAmountEnum)CondToolForm.PercentBox.SelectedIndex;
            ebp06.VariableType = (TieClassEnum)CondToolForm.ClassBox.SelectedIndex;

            if (ebp06.VariableType == TieClassEnum.ShipType)
            {
                ebp06.Variable = (byte)AlliedConvertShipSeqToCraftId((ShipSeqEnum)CondToolForm.IndexBox.SelectedIndex);
            }
            else
            {
                ebp06.Variable = (byte)CondToolForm.IndexBox.SelectedIndex;
            }

            ebp06.Condition = (TieConditionEnum)CondToolForm.CondBox.SelectedIndex;
            ebp06.Parameter = (byte)CondToolForm.RegionFGBox.SelectedIndex;

            //string ebp0C = Controls_TControl_GetText(CondToolForm.CondUnk2);
            //ebp06.Parameter2 = (byte)StrRec_try_to_int_L0051E3BC(ebp0C);
        }

        return ebp06;
    }

    // L0050FADC
    private static void TCondToolForm_PercentBoxChange(CondToolUserControl CondToolForm, object? edx0)
    {
        switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Arrival:
                Unit_00513838_Proc_0051BA40();
                break;

            case DatapadFGPageEnum.Departure:
                Unit_00513838_Proc_0051B850();
                break;

            case DatapadFGPageEnum.FGGoals:
                Form1WindowImpl.TForm1_Proc_00528DFC(AlliedVariables.s_AlliedForm1Window!);
                break;

            case DatapadFGPageEnum.Jump:
                Unit_00513838_Proc_0051B930();
                break;

            case DatapadFGPageEnum.GGoals:
                switch (AlliedVariables.s_GGoalCurrentTrigger - 1)
                {
                    case 0x00:
                    case 0x01:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[AlliedVariables.s_GGoalCurrentTrigger - 1] = TCondToolForm__PROC_0050F558(CondToolForm);
                        break;

                    case 0x02:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0] = TCondToolForm__PROC_0050F558(CondToolForm);
                        break;

                    case 0x03:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1] = TCondToolForm__PROC_0050F558(CondToolForm);
                        break;
                }

                TCondToolForm_Proc_0050F450(CondToolForm, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers);
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();
                }

                if (AlliedVariables.s_RadioMessagesObjectsList.Count < 0x01)
                {
                    int ebx1 = AlliedVariables.s_TCondToolForm_Instance!.PercentBox.SelectedIndex;
                    Form1WindowImpl.Unit_00513838_Proc_00518A44();
                    AlliedVariables.s_TCondToolForm_Instance!.PercentBox.SelectedIndex = ebx1;
                    AlliedVariables.s_V0x00543B10 = 0;
                }

                Unit_00513838_Proc_0051C5A4();
                break;
        }
    }

    // L0050F644
    private static void TCondToolForm_ClassBoxChange(CondToolUserControl CondToolForm)
    {
        switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Arrival:
                Unit_00513838_Proc_00517AC0(CondToolForm.IndexBox, (TieClassEnum)CondToolForm.ClassBox.SelectedIndex, 0);
                Unit_00513838_Proc_0051BA40();
                break;

            case DatapadFGPageEnum.Departure:
                Unit_00513838_Proc_00517AC0(CondToolForm.IndexBox, (TieClassEnum)CondToolForm.ClassBox.SelectedIndex, 0);
                Unit_00513838_Proc_0051B850();
                break;

            case DatapadFGPageEnum.Jump:
                Unit_00513838_Proc_00517AC0(CondToolForm.IndexBox, (TieClassEnum)CondToolForm.ClassBox.SelectedIndex, 0);
                Unit_00513838_Proc_0051B930();
                break;

            case DatapadFGPageEnum.GGoals:
                switch (AlliedVariables.s_GGoalCurrentTrigger)
                {
                    case 0x01:
                    case 0x02:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[AlliedVariables.s_GGoalCurrentTrigger - 1].VariableType = (TieClassEnum)Allied_ComboBox_GetSelectedIndex(CondToolForm.ClassBox);
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[AlliedVariables.s_GGoalCurrentTrigger - 1].Variable = 0;
                        break;

                    case 0x03:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0].VariableType = (TieClassEnum)Allied_ComboBox_GetSelectedIndex(CondToolForm.ClassBox);
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0].Variable = 0;
                        break;

                    case 0x04:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1].VariableType = (TieClassEnum)Allied_ComboBox_GetSelectedIndex(CondToolForm.ClassBox);
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1].Variable = 0;
                        break;
                }

                Unit_00513838_Proc_00517AC0(CondToolForm.IndexBox, (TieClassEnum)CondToolForm.ClassBox.SelectedIndex, 0);
                TCondToolForm_Proc_0050F450(CondToolForm, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers);
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();
                }

                if (AlliedVariables.s_RadioMessagesObjectsList.Count < 0x01)
                {
                    int esi = CondToolForm.ClassBox.SelectedIndex;
                    Form1WindowImpl.Unit_00513838_Proc_00518A44();
                    CondToolForm.ClassBox.SelectedIndex = esi;
                    AlliedVariables.s_V0x00543B10 = 0;
                }

                Unit_00513838_Proc_00517AC0(CondToolForm.IndexBox, (TieClassEnum)CondToolForm.ClassBox.SelectedIndex, 0);
                Unit_00513838_Proc_0051C5A4();
                break;
        }
    }

    // L0050F8FC
    private static void TCondToolForm_IndexBoxChange(CondToolUserControl CondToolForm)
    {
        CraftIdEnum eax1;

        if (CondToolForm.ClassBox.SelectedIndex == 0x02)
        {
            eax1 = AlliedConvertShipSeqToCraftId((ShipSeqEnum)CondToolForm.IndexBox.SelectedIndex);
        }
        else
        {
            eax1 = (CraftIdEnum)CondToolForm.IndexBox.SelectedIndex;
        }

        switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Arrival:
                Unit_00513838_Proc_0051BA40();
                break;

            case DatapadFGPageEnum.Departure:
                Unit_00513838_Proc_0051B850();
                break;

            case DatapadFGPageEnum.FGGoals:
                Form1WindowImpl.TForm1_Proc_00528DFC(AlliedVariables.s_AlliedForm1Window!);
                break;

            case DatapadFGPageEnum.Jump:
                Unit_00513838_Proc_0051B930();
                break;

            case DatapadFGPageEnum.GGoals:
                switch (AlliedVariables.s_GGoalCurrentTrigger - 1)
                {
                    case 0x00:
                    case 0x01:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[AlliedVariables.s_GGoalCurrentTrigger - 1].Variable = (byte)eax1;
                        break;

                    case 0x02:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0].Variable = (byte)eax1;
                        break;

                    case 0x03:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1].Variable = (byte)eax1;
                        break;
                }

                TCondToolForm_Proc_0050F450(CondToolForm, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers);
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();
                }

                if (AlliedVariables.s_RadioMessagesObjectsList.Count < 0x01 && AlliedVariables.s_RadioMessagesObjectsList.Count - 0x01 < 0)
                {
                    int esi = CondToolForm.IndexBox.SelectedIndex;
                    Form1WindowImpl.Unit_00513838_Proc_00518A44();
                    CondToolForm.IndexBox.SelectedIndex = esi;

                    AlliedVariables.s_V0x00543B10 = 0;
                }

                Unit_00513838_Proc_0051C5A4();
                break;
        }
    }

    // L0051B850
    private static void Unit_00513838_Proc_0051B850()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int ebx0 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx0; AlliedVariables.s_V0x00543CC0 += 1)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
            {
                continue;
            }

            S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
            S0xTieTrigger esp00 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
            eax0.FlightGroupStruct.DepartureTrigger.Triggers[AlliedVariables.s_V0x00543B2C - 1] = esp00;
        }

        S0xTieFlightGroupTriggerPair eax2 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.DepartureTrigger;
        TCondToolForm_Proc_00510018(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_TCondToolForm_Instance!.Cond1Lab, AlliedVariables.s_TCondToolForm_Instance!.Cond2Lab, eax2);
        Unit_00513838_Proc_0051467C();
    }

    // L0051BA40
    private static void Unit_00513838_Proc_0051BA40()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int ebx0 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx0; AlliedVariables.s_V0x00543CC0 += 1)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
            {
                continue;
            }

            switch (AlliedVariables.s_V0x00543B28)
            {
                case 0x01:
                case 0x02:
                    {
                        S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                        S0xTieTrigger esp00 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
                        eax0.FlightGroupStruct.ArrivalTrigger1.Triggers[AlliedVariables.s_V0x00543B28 - 1] = esp00;
                        break;
                    }

                case 0x03:
                case 0x04:
                    {
                        S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                        S0xTieTrigger esp00 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
                        eax0.FlightGroupStruct.ArrivalTrigger2.Triggers[AlliedVariables.s_V0x00543B28 - 3] = esp00;
                        break;
                    }
            }
        }

        Form1WindowImpl.TForm1_Proc_00528D70(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_0051467C();
    }

    // L0051B930
    private static void Unit_00513838_Proc_0051B930()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int ebx0 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx0; AlliedVariables.s_V0x00543CC0 += 1)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
            {
                continue;
            }

            S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);

            S0xTieTrigger esp00 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
            eax0.FlightGroupStruct.JumpTriggers[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Triggers[AlliedVariables.s_V0x00543B4C - 1] = esp00;
        }

        S0xTieFlightGroupTriggerPair eax2 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.JumpTriggers[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)];
        TCondToolForm_Proc_00510018(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_TCondToolForm_Instance!.Cond1Lab, AlliedVariables.s_TCondToolForm_Instance!.Cond2Lab, eax2);
        Unit_00513838_Proc_0051467C();
    }

    // L0051C5A4
    private static void Unit_00513838_Proc_0051C5A4()
    {
        if (AlliedVariables.s_V0x00543C9B == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            Unit_00513838_Proc_0051442C();
        }

        int ebx0 = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx0; AlliedVariables.s_V0x00543CC0 += 1)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
            {
                continue;
            }

            switch (AlliedVariables.s_V0x00543B3C)
            {
                case 0x01:
                case 0x02:
                    {
                        S0xTieRadioMessageObject eax0 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        S0xTieTrigger ebp08 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
                        eax0.RadioMessage.Condition.Trigger_0[AlliedVariables.s_V0x00543B3C - 1] = ebp08;
                        break;
                    }

                case 0x03:
                    {
                        S0xTieRadioMessageObject eax0 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        S0xTieTrigger ebp08 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
                        eax0.RadioMessage.Condition.Trigger_1[0] = ebp08;
                        break;
                    }

                case 0x04:
                    {
                        S0xTieRadioMessageObject eax0 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        S0xTieTrigger ebp08 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
                        eax0.RadioMessage.Condition.Trigger_1[1] = ebp08;
                        break;
                    }

                case 0x05:
                    {
                        S0xTieRadioMessageObject eax0 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        S0xTieTrigger ebp08 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
                        eax0.RadioMessage.Trigger1 = ebp08;
                        break;
                    }

                case 0x06:
                    {
                        S0xTieRadioMessageObject eax0 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                        S0xTieTrigger ebp08 = TCondToolForm__PROC_0050F558(AlliedVariables.s_TCondToolForm_Instance!);
                        eax0.RadioMessage.Trigger2 = ebp08;
                        break;
                    }
            }
        }

        TCondToolForm_Proc_0050F450(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6B58.RadioMessage.Condition);
        string ebp10_1 = Unit_00511CD0_Proc_005122D0(AlliedVariables.s_V0x005B6B58.RadioMessage.Trigger1);
        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.Cond5Lab, ebp10_1);
        string ebp10_0 = Unit_00511CD0_Proc_005122D0(AlliedVariables.s_V0x005B6B58.RadioMessage.Trigger2);
        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.Cond6Lab, ebp10_0);
    }

    // L0050FCC8
    private static void TCondToolForm_CondBoxChange(CondToolUserControl CondToolForm)
    {
        int edi1 = CondToolForm.RegionFGBox.SelectedIndex;
        TieConditionEnum eax1 = (TieConditionEnum)CondToolForm.CondBox.SelectedIndex;

        if (eax1 == TieConditionEnum.Within || eax1 == TieConditionEnum.Beyond)
        {
            CondToolForm.RegionFGBox.SetItems(AlliedVariables.s_Strings_Regions);
            CondToolForm.RegionFGBox.SelectedIndex = edi1;
        }
        else
        {
            CondToolForm.RegionFGBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
            CondToolForm.RegionFGBox.PutItem(0, "0");
            CondToolForm.RegionFGBox.SelectedIndex = edi1;
        }

        DatapadFGPageEnum eax2 = (DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag);

        switch (eax2)
        {
            case DatapadFGPageEnum.Arrival:
                Unit_00513838_Proc_0051BA40();
                break;

            case DatapadFGPageEnum.Departure:
                Unit_00513838_Proc_0051B850();
                break;

            case DatapadFGPageEnum.FGGoals:
                Form1WindowImpl.TForm1_Proc_00528DFC(AlliedVariables.s_AlliedForm1Window!);
                Unit_00513838_Proc_00517258();
                break;

            case DatapadFGPageEnum.Jump:
                Unit_00513838_Proc_0051B930();
                break;

            case DatapadFGPageEnum.GGoals:
                switch (AlliedVariables.s_GGoalCurrentTrigger - 1)
                {
                    case 0x00:
                    case 0x01:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[AlliedVariables.s_GGoalCurrentTrigger - 1].Condition = (TieConditionEnum)Allied_ComboBox_GetSelectedIndex(CondToolForm.CondBox);
                        break;

                    case 0x02:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0].Condition = (TieConditionEnum)Allied_ComboBox_GetSelectedIndex(CondToolForm.CondBox);
                        break;

                    case 0x03:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1].Condition = (TieConditionEnum)Allied_ComboBox_GetSelectedIndex(CondToolForm.CondBox);
                        break;
                }

                TCondToolForm_Proc_0050F450(CondToolForm, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers);
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();
                }

                if (AlliedVariables.s_RadioMessagesObjectsList.Count < 0x01 && AlliedVariables.s_RadioMessagesObjectsList.Count - 1 < 0)
                {
                    int edi = CondToolForm.CondBox.SelectedIndex;
                    Form1WindowImpl.Unit_00513838_Proc_00518A44();
                    CondToolForm.CondBox.SelectedIndex = edi;

                    AlliedVariables.s_V0x00543B10 = 0;
                }

                Unit_00513838_Proc_0051C5A4();
                break;
        }
    }

    // L005105FC
    private static void TCondToolForm_CondUnk2Change(CondToolUserControl CondToolForm)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        DatapadFGPageEnum eax1 = (DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag);

        switch (eax1)
        {
            case DatapadFGPageEnum.Arrival:
                Unit_00513838_Proc_0051BA40();
                break;

            case DatapadFGPageEnum.Departure:
                Unit_00513838_Proc_0051B850();
                break;

            case DatapadFGPageEnum.FGGoals:
                Form1WindowImpl.TForm1_Proc_00528DFC(AlliedVariables.s_AlliedForm1Window!);
                break;

            case DatapadFGPageEnum.Jump:
                Unit_00513838_Proc_0051B930();
                break;

            case DatapadFGPageEnum.GGoals:
                //switch (AlliedVariables.s_GGoalCurrentTrigger - 1)
                //{
                //    case 0x00:
                //    case 0x01:
                //        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[AlliedVariables.s_GGoalCurrentTrigger - 1].Parameter2 = (byte)StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(CondToolForm.CondUnk2));
                //        break;

                //    case 0x02:
                //        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0].Parameter2 = (byte)StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(CondToolForm.CondUnk2));
                //        break;

                //    case 0x03:
                //        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1].Parameter2 = (byte)StrRec_try_to_int_L0051E3BC(Controls_TControl_GetText(CondToolForm.CondUnk2));
                //        break;
                //}

                TCondToolForm_Proc_0050F450(CondToolForm, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers);
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();
                }

                if (AlliedVariables.s_RadioMessagesObjectsList.Count < 0x01 && AlliedVariables.s_RadioMessagesObjectsList.Count - 1 < 0)
                {
                    int esi = CondToolForm.CondBox.SelectedIndex;
                    Form1WindowImpl.Unit_00513838_Proc_00518A44();
                    CondToolForm.CondBox.SelectedIndex = esi;

                    AlliedVariables.s_V0x00543B10 = 0;
                }

                Unit_00513838_Proc_0051C5A4();
                break;
        }
    }

    // L0051041C
    private static void TCondToolForm_CondUnk1Change(CondToolUserControl CondToolForm)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        DatapadFGPageEnum eax1 = (DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag);

        switch (eax1)
        {
            case DatapadFGPageEnum.Arrival:
                Unit_00513838_Proc_0051BA40();
                break;

            case DatapadFGPageEnum.Departure:
                Unit_00513838_Proc_0051B850();
                break;

            case DatapadFGPageEnum.FGGoals:
                Form1WindowImpl.TForm1_Proc_00528DFC(AlliedVariables.s_AlliedForm1Window!);
                break;

            case DatapadFGPageEnum.Jump:
                Unit_00513838_Proc_0051B930();
                break;

            case DatapadFGPageEnum.GGoals:
                switch (AlliedVariables.s_GGoalCurrentTrigger - 1)
                {
                    case 0x00:
                    case 0x01:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[AlliedVariables.s_GGoalCurrentTrigger - 1].Parameter = (byte)CondToolForm.RegionFGBox.SelectedIndex;
                        break;

                    case 0x02:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0].Parameter = (byte)CondToolForm.RegionFGBox.SelectedIndex;
                        break;

                    case 0x03:
                        AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1].Parameter = (byte)CondToolForm.RegionFGBox.SelectedIndex;
                        break;
                }

                TCondToolForm_Proc_0050F450(CondToolForm, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers);
                Unit_00513838_Proc_005146A4();
                break;

            case DatapadFGPageEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
                {
                    Unit_00513838_Proc_0051442C();
                }

                if (AlliedVariables.s_RadioMessagesObjectsList.Count < 0x01 && AlliedVariables.s_RadioMessagesObjectsList.Count - 1 < 0)
                {
                    int esi = CondToolForm.CondBox.SelectedIndex;
                    Form1WindowImpl.Unit_00513838_Proc_00518A44();
                    CondToolForm.CondBox.SelectedIndex = esi;

                    AlliedVariables.s_V0x00543B10 = 0;
                }

                Unit_00513838_Proc_0051C5A4();
                break;
        }
    }

    // L00510BB8
    private static void TCondToolForm_FGStrIncompChange(CondToolUserControl CondToolForm, TextBox edx0)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        DatapadFGPageEnum eax1 = (DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag);

        switch (eax1)
        {
            case DatapadFGPageEnum.FGGoals:
                {
                    string ebp40 = Unit_00511CD0_Proc_0051213C(Controls_TControl_GetText(edx0));
                    switch (Convert.ToInt32(edx0.Tag))
                    {
                        case 1:
                            AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000000 = ebp40;
                            break;

                        case 2:
                            AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000040 = ebp40;
                            break;

                        case 3:
                            AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000080 = ebp40;
                            break;
                    }

                    break;
                }

            case DatapadFGPageEnum.GGoals:
                {
                    string ebp40 = Unit_00511CD0_Proc_0051213C(Controls_TControl_GetText(edx0));
                    switch (Convert.ToInt32(edx0.Tag))
                    {
                        case 1:
                            AlliedVariables.s_V0x005B6BBC.GGStrings[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex() * 4 + (AlliedVariables.s_GGoalCurrentTrigger - 1)].StrIncomp = ebp40;
                            break;

                        case 2:
                            AlliedVariables.s_V0x005B6BBC.GGStrings[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex() * 4 + (AlliedVariables.s_GGoalCurrentTrigger - 1)].StrSucc = ebp40;
                            break;

                        case 3:
                            AlliedVariables.s_V0x005B6BBC.GGStrings[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex() * 4 + (AlliedVariables.s_GGoalCurrentTrigger - 1)].StrFail = ebp40;
                            break;
                    }

                    break;
                }
        }

        Unit_00513838_Proc_005146A4();
    }

    // L005103AC
    private static void TCondToolForm_Cond1LabClick(CondToolUserControl CondToolForm, TextBlock Sender)
    {
        Unit_00513838_Proc_0051CFC4(Convert.ToInt32(Sender.Tag));
    }

    // L0051CFC4
    public static void Unit_00513838_Proc_0051CFC4(int eax0)
    {
        AlliedVariables.s_V0x00543C9A = 0;
        TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, eax0);

        switch ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag))
        {
            case DatapadFGPageEnum.Arrival:
                AlliedVariables.s_V0x00543B28 = eax0;

                if ((int)eax0 < 0x03)
                {
                    TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ArrivalTrigger1.Triggers[(int)eax0 - 1]);
                }
                else
                {
                    TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ArrivalTrigger2.Triggers[(int)eax0 - 3]);
                }

                break;

            case DatapadFGPageEnum.Departure:
                AlliedVariables.s_V0x00543B2C = eax0;
                TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.DepartureTrigger.Triggers[eax0 - 1]);
                break;

            case DatapadFGPageEnum.Jump:
                AlliedVariables.s_V0x00543B4C = eax0;
                TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.JumpTriggers[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Triggers[(int)eax0 - 1]);
                break;

            case DatapadFGPageEnum.GGoals:
                AlliedVariables.s_GGoalCurrentTrigger = eax0;

                switch (eax0)
                {
                    case 0x03:
                        TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[0]);
                        break;

                    case 0x04:
                        TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_1[1]);
                        break;

                    default:
                        TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6BB8.GlobalGoal.GlobalGoals[AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex()].Triggers.Trigger_0[eax0 - 1]);
                        break;
                }

                Unit_00513838_Proc_0051F09C(AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex(), eax0);
                break;

            case DatapadFGPageEnum.Messages:
                AlliedVariables.s_V0x00543B3C = eax0;
                Form1WindowImpl.L005291A4(AlliedVariables.s_AlliedForm1Window!);
                break;
        }

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L0050FF30
    public static void TCondToolForm_Cond1LabDblClick(CondToolUserControl CondToolForm, TextBlock Sender)
    {
        AlliedVariables.s_TClipForm_Instance = MainImpl.CreateClipWindow();

        Controls_TControl_SetText(AlliedVariables.s_TClipForm_Instance!, "Condition Clipboard");
        AlliedVariables.s_TClipForm_Instance.Owner = Application.Current.MainWindow;
        AlliedVariables.s_ClipboardType = ClipboardTypeEnum.Condition;
        AlliedVariables.s_TClipForm_Instance.ShowDialog();

        if (AlliedVariables.s_TClipForm_Instance.DialogResult == true)
        {
            int eax1 = AlliedVariables.s_TClipForm_Instance.ListBox1.SelectedIndex;
            S0xCondObjectStruct eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF8, eax1);
            TCondToolForm_Proc_0050F2E8(CondToolForm, eax2.m000004);
            TCondToolForm_PercentBoxChange(CondToolForm, CondToolForm.PercentBox);
            Unit_00513838_Proc_005146A4();
        }

        AlliedVariables.s_TClipForm_Instance = null;
    }

    // L005103B8
    private static void TCondToolForm_and2AndClick(CondToolUserControl CondToolForm)
    {
        DatapadWindowImpl.L0051CE68();
    }

    // L005103C8
    private static void TCondToolForm_and4AndClick(CondToolUserControl CondToolForm)
    {
        DatapadWindowImpl.Unit_00513838_Proc_0051CD4C();
    }

    // L005103C0
    private static void TCondToolForm_GlbAndClick(CondToolUserControl CondToolForm)
    {
        DatapadWindowImpl.Unit_00513838_Proc_0051CC2C();
    }
}
