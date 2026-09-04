using AlliED.Extensions;
using System.Globalization;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class GoalViewWindowImpl
{
    public static void Register(GoalViewWindow window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(GoalViewWindow window)
    {
        window.Closed += (s, e) => TGoalViewForm_FormClose(window);
        window.ListBox2.DrawItem += (sender, index) => TGoalViewForm_ListBox2DrawItem(window, sender, index);
    }

    // L004B481C
    private static void FormCreate(GoalViewWindow GoalViewForm)
    {
        AlliedVariables.s_V0x00535E60 = new();
        AlliedVariables.s_V0x00535E68 = new();
        AlliedVariables.s_V0x00535E64 = new();
        AlliedVariables.s_V0x00535E6C = new();

        string ebp08_1 = "Goal Summary for Team" + AlliedVariables.s_Strings_Teams.GetText(0);
        Controls_TControl_SetText(GoalViewForm, ebp08_1);
        TGoalViewForm_Proc_004B48F8(GoalViewForm, 0);
    }

    // L004B57A4
    public static uint TGoalViewForm_Proc_004B57A4(uint eax0, byte edx0)
    {
        double esp08 = edx0 / 100.0f;
        byte ebx = (byte)Runtime_L00407BEC_Return_eax(eax0);
        byte esp00 = (byte)Runtime_L00407BF0_Div_by_0x100(eax0);
        byte esp01 = (byte)Runtime_L00407BF4_Div_by_0x10000(eax0);
        ebx = (byte)Math.Round(ebx * esp08);
        esp00 = (byte)Math.Round(esp00 * esp08);
        esp01 = (byte)Math.Round(esp01 * esp08);
        uint r = Runtime_L00407BD0_Combine(ebx, esp00, esp01);
        return r;
    }

    // L004B5380
    private static void TGoalViewForm_FormClose(GoalViewWindow GoalViewForm)
    {
        AlliedVariables.s_V0x00535E60.Clear();
        AlliedVariables.s_V0x00535E68.Clear();
        AlliedVariables.s_V0x00535E64.Clear();
        AlliedVariables.s_V0x00535E6C.Clear();
    }

    // L004B48F8
    private static void TGoalViewForm_Proc_004B48F8(GoalViewWindow GoalViewForm, int edx0)
    {
        S0xTieGlobalGoalObject eax1 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, edx0);
        S0xTieGlobalGoal ebp86 = S0xTieGlobalGoal.FromByteArray(eax1.GlobalGoal.GlobalGoals[0].ToByteArray());
        TGoalViewForm_Proc_004B4FF0(GoalViewForm, '^', AlliedVariables.s_V0x00535E60, ebp86.Op, ebp86);

        if (AlliedVariables.s_V0x00535E60.GetCount() > 0)
        {
            AlliedVariables.s_V0x00535E60.Add("");
        }

        eax1 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, edx0);
        ebp86 = S0xTieGlobalGoal.FromByteArray(eax1.GlobalGoal.GlobalGoals[1].ToByteArray());
        TGoalViewForm_Proc_004B4FF0(GoalViewForm, '~', AlliedVariables.s_V0x00535E64, ebp86.Op, ebp86);

        if (AlliedVariables.s_V0x00535E64.GetCount() > 0)
        {
            AlliedVariables.s_V0x00535E64.Add("");
        }

        eax1 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, edx0);
        ebp86 = S0xTieGlobalGoal.FromByteArray(eax1.GlobalGoal.GlobalGoals[2].ToByteArray());
        TGoalViewForm_Proc_004B4FF0(GoalViewForm, '=', AlliedVariables.s_V0x00535E68, ebp86.Op, ebp86);

        int ebp0C = AlliedVariables.s_FlightGroupObjectsList.Count;
        for (int ebp08 = 0; ebp08 < ebp0C; ebp08++)
        {
            for (int esi = 0; esi < 8; esi++)
            {
                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp08);
                S0xTieFlightGroup ebpF14 = S0xTieFlightGroup.FromByteArray(eax2.FlightGroupStruct.ToByteArray());
                S0xTieFlightGroupGoal ebpD6 = S0xTieFlightGroupGoal.FromByteArray(ebpF14.Goals[esi].ToByteArray());

                if (ebpD6.Condition == TieConditionEnum.Always || ebpD6.Condition == TieConditionEnum.Never)
                {
                    continue;
                }

                if (ebpD6.AppliesToTeams[1] != 0 || ebpD6.AppliesToTeams[0] != 0x01)
                {
                    continue;
                }

                switch (ebpD6.GoalType)
                {
                    case TieFGGoalTypeEnum.Win:
                        {
                            string ebpF5C_15 = TGoalViewForm_GetCraftString_NameAndShort(ebpF14);
                            string ebpF5C_17 = "^" + TGoalViewForm_Proc_004B53AC(GoalViewForm, ebpD6, ebpF5C_15);
                            AlliedVariables.s_V0x00535E60.Add(ebpF5C_17);
                            break;
                        }

                    case TieFGGoalTypeEnum.Loss:
                        {
                            string ebpF5C_12 = TGoalViewForm_GetCraftString_NameAndShort(ebpF14);
                            string ebpF5C_14 = "~" + TGoalViewForm_Proc_004B53AC(GoalViewForm, ebpD6, ebpF5C_12);
                            AlliedVariables.s_V0x00535E64.Add(ebpF5C_14);
                            break;
                        }

                    case TieFGGoalTypeEnum.Bonus:
                        {
                            string ebpF5C_9 = TGoalViewForm_GetCraftString_NameAndShort(ebpF14);
                            string ebpF5C_11 = "=" + TGoalViewForm_Proc_004B53AC(GoalViewForm, ebpD6, ebpF5C_9);
                            AlliedVariables.s_V0x00535E6C.Add(ebpF5C_11);
                            break;
                        }

                    case TieFGGoalTypeEnum.Sec:
                        {
                            string ebpF5C_6 = TGoalViewForm_GetCraftString_NameAndShort(ebpF14);
                            string ebpF5C_8 = "=" + TGoalViewForm_Proc_004B53AC(GoalViewForm, ebpD6, ebpF5C_6);
                            AlliedVariables.s_V0x00535E6C.Add(ebpF5C_8);
                            break;
                        }
                }
            }
        }

        if (AlliedVariables.s_V0x00535E60.GetCount() > 0)
        {
            int edx1 = AlliedVariables.s_V0x00535E60.GetCount();

            if (string.IsNullOrEmpty(AlliedVariables.s_V0x00535E60.GetText(edx1 - 1)))
            {
                edx1 = AlliedVariables.s_V0x00535E60.GetCount();
                AlliedVariables.s_V0x00535E60.Delete(edx1 - 1);
            }
        }

        GoalViewForm.ListBox1.AddItem("#Primary");

        if (AlliedVariables.s_V0x00535E60.GetCount() <= 0)
        {
            GoalViewForm.ListBox1.AddItem("^(None)");
        }
        else
        {
            ebp0C = AlliedVariables.s_V0x00535E60.GetCount();

            for (int ebp08 = 0; ebp08 < ebp0C; ebp08++)
            {
                GoalViewForm.ListBox1.AddItem(AlliedVariables.s_V0x00535E60.GetText(ebp08));
            }
        }

        if (AlliedVariables.s_V0x00535E64.GetCount() > 0)
        {
            int edx1 = AlliedVariables.s_V0x00535E64.GetCount();
            if (string.IsNullOrEmpty(AlliedVariables.s_V0x00535E64.GetText(edx1 - 1)))
            {
                edx1 = AlliedVariables.s_V0x00535E64.GetCount();
                AlliedVariables.s_V0x00535E64.Delete(edx1 - 1);
            }
        }

        GoalViewForm.ListBox1.AddItem("");
        GoalViewForm.ListBox1.AddItem("#Prevent");

        if (AlliedVariables.s_V0x00535E64.GetCount() <= 0)
        {
            GoalViewForm.ListBox1.AddItem("~(None)");
        }
        else
        {
            ebp0C = AlliedVariables.s_V0x00535E64.GetCount();

            for (int ebp08 = 0; ebp08 < ebp0C; ebp08++)
            {
                GoalViewForm.ListBox1.AddItem(AlliedVariables.s_V0x00535E64.GetText(ebp08));
            }
        }

        GoalViewForm.ListBox1.AddItem("");
        GoalViewForm.ListBox1.AddItem("#Secondary");

        if (AlliedVariables.s_V0x00535E68.GetCount() <= 0)
        {
            GoalViewForm.ListBox1.AddItem("=(None)");
        }
        else
        {
            ebp0C = AlliedVariables.s_V0x00535E68.GetCount();

            for (int ebp08 = 0; ebp08 < ebp0C; ebp08++)
            {
                GoalViewForm.ListBox1.AddItem(AlliedVariables.s_V0x00535E68.GetText(ebp08));
            }
        }

        GoalViewForm.ListBox1.AddItem("");
        GoalViewForm.ListBox1.AddItem("#Bonus");

        if (AlliedVariables.s_V0x00535E6C.GetCount() <= 0)
        {
            GoalViewForm.ListBox1.AddItem("=(None)");
        }
        else
        {
            ebp0C = AlliedVariables.s_V0x00535E6C.GetCount();

            for (int ebp08 = 0; ebp08 < ebp0C; ebp08++)
            {
                GoalViewForm.ListBox1.AddItem(AlliedVariables.s_V0x00535E6C.GetText(ebp08));
            }
        }

        StdCtrls_TCustomListBox_SetItems(GoalViewForm.ListBox2, GoalViewForm.ListBox1.Items);
    }

    // L00512628
    private static string TGoalViewForm_GetCraftString_NameAndShort(S0xTieFlightGroup eax0)
    {
        string edx0 = AlliedGetCraftShortString(eax0.CraftId) + " " + eax0.Name;
        return edx0;
    }

    // L004B4FF0
    private static void TGoalViewForm_Proc_004B4FF0(GoalViewWindow GoalViewForm, char edx0, TStrings ecx0, bool A4, S0xTieGlobalGoal A8)
    {
        S0xTieTriggers ebp2C = S0xTieTriggers.FromByteArray(A8.Triggers.ToByteArray());

        int ebx = TGoalViewForm_Proc_004B521C(GoalViewForm, ebp2C);

        string ebp0C = string.Empty;
        if (ebp2C.Operator_0 == true || (ebp2C.Operator_1 == true && ebx > 0x03))
        {
            if (A4 == false)
            {
                ebp0C = edx0.ToString(CultureInfo.InvariantCulture) + "                ---And---";
            }
        }

        if (ebx > 0)
        {
            string ebp68_11 = Unit_00511CD0_Proc_005122D0(ebp2C.Trigger_0[0]);
            string ebp68_10 = TGoalViewForm__PROC_004B527C(GoalViewForm, edx0, ebp2C.Operator_0, 0, ebx, 0x01);
            ecx0.Add(edx0.ToString(CultureInfo.InvariantCulture) + ebp68_11);
            if (!string.IsNullOrEmpty(ebp68_10))
            {
                //ecx0.Add("");
                ecx0.Add(ebp68_10);
            }

            if (ebx > 0x01)
            {
                string ebp68_7 = Unit_00511CD0_Proc_005122D0(ebp2C.Trigger_0[1]);
                string ebp68_6 = TGoalViewForm__PROC_004B527C(GoalViewForm, edx0, A4, 0x01, ebx, 0x02);
                ecx0.Add(edx0.ToString(CultureInfo.InvariantCulture) + ebp68_7);
                if (!string.IsNullOrEmpty(ebp68_6))
                {
                    //ecx0.Add("");
                    ecx0.Add(ebp68_6);
                }

                if (ebx > 0x02)
                {
                    string ebp68_3 = Unit_00511CD0_Proc_005122D0(ebp2C.Trigger_1[0]);
                    string ebp68_2 = TGoalViewForm__PROC_004B527C(GoalViewForm, edx0, ebp2C.Operator_1, 0, ebx, 0x03);

                    if (!string.IsNullOrEmpty(ebp0C))
                    {
                        ecx0.Add(ebp0C);
                        //ecx0.Add(string.Empty);
                    }

                    ecx0.Add(edx0.ToString(CultureInfo.InvariantCulture) + ebp68_3);
                    if (!string.IsNullOrEmpty(ebp68_2))
                    {
                        //ecx0.Add("");
                        ecx0.Add(ebp68_2);
                    }

                    if (ebx > 0x03)
                    {
                        string ebp68_1 = Unit_00511CD0_Proc_005122D0(ebp2C.Trigger_1[1]);
                        ecx0.Add(edx0.ToString(CultureInfo.InvariantCulture) + ebp68_1);
                    }
                }
            }
        }
    }

    // L004B521C
    private static int TGoalViewForm_Proc_004B521C(GoalViewWindow GoalViewForm, S0xTieTriggers edx0)
    {
        int eax1 = 0;

        switch (edx0.Trigger_0[0].Condition)
        {
            case TieConditionEnum.Always:
            case TieConditionEnum.Never:
                break;

            default:
                eax1 = 0x01;
                break;
        }

        switch (edx0.Trigger_0[1].Condition)
        {
            case TieConditionEnum.Always:
            case TieConditionEnum.Never:
                break;

            default:
                eax1 = 0x02;
                break;
        }

        switch (edx0.Trigger_1[0].Condition)
        {
            case TieConditionEnum.Always:
            case TieConditionEnum.Never:
                break;

            default:
                eax1 = 0x03;
                break;
        }

        switch (edx0.Trigger_1[1].Condition)
        {
            case TieConditionEnum.Always:
            case TieConditionEnum.Never:
                break;

            default:
                eax1 = 0x04;
                break;
        }

        return eax1;
    }

    // L004B53AC
    private static string TGoalViewForm_Proc_004B53AC(GoalViewWindow GoalViewForm, S0xTieFlightGroupGoal edx0, string ecx0)
    {
        string ebp08_0 = string.Empty;

        S0xTieFlightGroupGoal ebp60 = S0xTieFlightGroupGoal.FromByteArray(edx0.ToByteArray());

        if (AlliedVariables.s_V0x005B6D15 != 0 && AlliedVariables.s_V0x00543BC8.GetCount() > 0)
        {
            if (ebp60.Condition == TieConditionEnum.Never)
            {
                ebp08_0 = "<None>";
            }
            else
            {
                ebp08_0 = AlliedVariables.s_V0x00543BC8.GetText((int)ebp60.Amount) + " of " + ecx0 + " " + AlliedVariables.s_Strings_Musts.GetText((int)ebp60.GoalType) + " ";
                ebp08_0 += AlliedVariables.s_V0x00543BD4.GetText((int)ebp60.Condition);

                if (ebp60.Condition == TieConditionEnum.Arrival || ebp60.Condition == TieConditionEnum.Departure)
                {
                    ebp08_0 += ebp60.Time.ToString(CultureInfo.InvariantCulture);
                }

                if (ebp60.Condition == TieConditionEnum.Within || ebp60.Condition == TieConditionEnum.Beyond)
                {
                    ebp08_0 += " " + AlliedVariables.s_Strings_Regions.GetText(ebp60.Time);
                }

                if (ebp60.Points != 0)
                {
                    double ebp10 = (double)ebp60.Points * 25.0f;
                    string ebp80 = ebp10.ToString(CultureInfo.InvariantCulture);
                    ebp08_0 += " : " + ebp80 + " Pts";
                }
            }
        }

        return ebp08_0;
    }

    // L004B527C
    private static string TGoalViewForm__PROC_004B527C(GoalViewWindow GoalViewForm, char edx0, bool ecx0, byte A8, int AC, int A10)
    {
        string A4 = string.Empty;

        if (AC > A10 && ecx0 == true)
        {
            if (A8 != 0)
            {
                A4 = edx0.ToString(CultureInfo.InvariantCulture) + "                 ---Or---";
            }
            else
            {
                A4 = edx0.ToString(CultureInfo.InvariantCulture) + "                    Or";
            }
        }

        return A4;
    }

    // L004B5608
    private static void TGoalViewForm_ListBox2DrawItem(GoalViewWindow GoalViewForm, ListBox sender, int index)
    {
        string ebp08 = GoalViewForm.ListBox2.GetItemText(index);
        ListBoxItem item = GoalViewForm.ListBox2.GetItem(index);

        if (item.Tag is null && ebp08.Length > 0)
        {
            item.Tag = ebp08[0];
            ebp08 = ebp08[1..];
            GoalViewForm.ListBox2.PutItem(index, ebp08);
        }

        if (item.Tag is not null)
        {
            switch ((char)item.Tag)
            {
                case '#':
                    Graphics_TFont_SetColor(item, 0x00FFFFFF);
                    break;

                case '^':
                    Graphics_TFont_SetColor(item, 0x003EEAFD);
                    break;

                case '~':
                    Graphics_TFont_SetColor(item, 0x00FFA54A);
                    break;

                default:
                    Graphics_TFont_SetColor(item, 0x0000FF00);
                    break;
            }
        }
    }
}
