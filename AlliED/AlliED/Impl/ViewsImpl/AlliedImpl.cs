using AlliED.Extensions;
using AlliED.Helpers;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class AlliedImpl
{
    // L005184DC
    public static void Allied_ShowMessageWithTimer(int interval, string message)
    {
        TimeForm.ShowTimeForm(Application.Current.MainWindow, message, interval);
    }

    // L00514104
    public static void MessageBox_ShowInformation(string text)
    {
        Xceed.Wpf.Toolkit.MessageBox.Show(text, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // L00514158
    public static void MessageBox_ShowError(string text)
    {
        Xceed.Wpf.Toolkit.MessageBox.Show(text, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    // L005141AC
    public static void MessageBox_ShowWarning(string text)
    {
        Xceed.Wpf.Toolkit.MessageBox.Show(text, string.Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    // L00514200
    public static TModalResultEnum MessageBox_ShowConfirmation(string msg, string? cancelStr)
    {
        MessageBoxResult result;

        if (cancelStr == "cancel")
        {
            result = Xceed.Wpf.Toolkit.MessageBox.Show(msg, string.Empty, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }
        else
        {
            result = Xceed.Wpf.Toolkit.MessageBox.Show(msg, string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        TModalResultEnum ebx = result switch
        {
            MessageBoxResult.None => TModalResultEnum.None,
            MessageBoxResult.OK => TModalResultEnum.Ok,
            MessageBoxResult.Cancel => TModalResultEnum.Cancel,
            MessageBoxResult.Yes => TModalResultEnum.Yes,
            MessageBoxResult.No => TModalResultEnum.No,
            _ => TModalResultEnum.None,
        };

        return ebx;
    }

    // L0051DB68
    public static void Unit_00513838_Proc_0051DB68()
    {
        TieFileVersionEnum version = AlliedVariables.s_TieFileVersion;

        switch (version)
        {
            case TieFileVersionEnum.XWing:
                L00515534();
                break;

            case TieFileVersionEnum.XWA:
                L00514C10();
                break;
        }
    }

    // L005196CC
    public static void Unit_00513838_Proc_005196CC()
    {
        AlliedVariables.s_V0x00543B0C = 0;
        AlliedVariables.s_V0x005424C8 = 0;
        AlliedVariables.s_CurrentOrderInRegion = 0x01;
        Unit_00513838_Proc_00519B48(0x01);
        AlliedVariables.s_CurrentRegion = 0x01;
        Unit_00513838_Proc_005146E8();
        Unit_00513838_Proc_0051726C();
        AlliedVariables.s_V0x00543BF8 = "Unnamed";
        Unit_00513838_Proc_0051872C();
        L00516DDC();
        Unit_00513838_Proc_00517518();
        Unit_00513838_Proc_0051DB68();

        S0xMissionObject eax0 = new();
        AlliedVariables.s_TieFileHeader = eax0.TieFileHeader;

        AlliedVariables.s_TieFileVersion = TieFileVersionEnum.XWA;
        AlliedVariables.s_TieFileHeader.Header.WinType = 0x01;
        AlliedVariables.s_TieFileHeader.Header.AllWayShown = true;
        AlliedVariables.s_TieFileHeader.Header.MissionType = 0x06;
        AlliedVariables.s_TieFileHeader.Header.BriefingCodeSizeType = 0x62;

        for (int ebx = 0; ebx < 4; ebx++)
        {
            S0xTieRegion ebp84 = new();
            Unit_00511CD0_Proc_005121FC("Region " + (ebx + 1).ToString(CultureInfo.InvariantCulture), ref ebp84);
            AlliedVariables.s_TieFileHeader.Header.Regions[ebx] = ebp84;
        }

        AlliedVariables.s_TieFileHeader.FlightGroupsCount = (short)AlliedVariables.s_FlightGroupObjectsList.Count;

        if (AlliedVariables.s_FlightGroupObjectsList.Count > 0)
        {
            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, 0);
            eax.FlightGroupStruct.PlayerNumber = 0x01;
        }

        Unit_00513838_Proc_0051DB68();
        AlliedVariables.s_TieFileHeader.RadioMessagesCount = 0;

        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            S0xTieGlobalGoalObject eax1 = new();
            eax1.GlobalGoal.Count = 0x03;
            AlliedVariables.s_GlobalGoalsObjectsList.Add(eax1);
        }

        AlliedVariables.s_V0x005B6BB8 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);

        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            S0xTieTeamObject eax1 = new();
            eax1.Team.TeamCreated = 0x01;
            AlliedVariables.s_TeamsObjectsList.Add(eax1);
        }

        L005182B8(0, 0x01);
        L0051A1E8();

        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            S0xXvTGGStrObject eax1 = new();
            AlliedVariables.s_V0x00571188.Add(eax1);
        }

        ArrayHelpers.ClearArray(AlliedVariables.s_V0x0056A874);
        AlliedVariables.s_V0x00570FF8 = new();
        AlliedVariables.s_V0x00570FFC = new();
        AlliedVariables.s_V0x00571000 = new();
        AlliedVariables.s_V0x00546144 = new();
        AlliedVariables.s_V0x0054A578 = new();
        AlliedVariables.s_V0x0054A5E4 = new();
        AlliedVariables.s_V0x0054AE60 = new();
        AlliedVariables.s_V0x0054AEAC = new();
        AlliedVariables.s_V0x0056A2AC = new();
        AlliedVariables.s_V0x00571004 = new();
        AlliedVariables.s_V0x005710F0 = new();
        AlliedVariables.s_V0x0057111C = new();

        if (AlliedVariables.s_V0x00535E8C != 0)
        {
            AlliedVariables.s_TMemoForm_Instance!.EditMemo1.Clear();
            AlliedVariables.s_TMemoForm_Instance!.EditMemo2.Clear();
            AlliedVariables.s_TMemoForm_Instance!.EditMemo3.Clear();
            AlliedVariables.s_TMemoForm_Instance!.EditMemo4.Clear();
        }

        AlliedVariables.s_TieMission_Description.Text = "$#";
        AlliedVariables.s_TieMission_WinDebriefing.Text = string.Empty;
        AlliedVariables.s_TieMission_LostDebriefing.Text = "#";
        AlliedVariables.s_TieMission_Notes.Text = string.Empty;

        AlliedVariables.s_V0x00543B10 = 0;

        AlliedVariables.s_V0x005B6B58 = new();
        AlliedVariables.s_V0x005B6B58.RadioMessage = S0xTieRadioMessage.FromByteArray(AlliedVariables.s_V0x005B6AB4.ToByteArray());
        Form1WindowImpl.TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6AB4);
        Unit_00513838_Proc_00517564();

        AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex = 0;

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.MsgtextEd, null);
        }

        L005146CC();

        AlliedVariables.s_AlliedForm1Window!.SaveBtn.IsEnabled = false;

        AlliedVariables.s_V0x00543B59 = 0;
        AlliedVariables.s_V0x00543B54 = 0;
        AlliedVariables.s_V0x00543B55 = 0;

        // System_DoneExcept();
    }

    // L00519B48
    public static void Unit_00513838_Proc_00519B48(int eax0)
    {
        if (AlliedVariables.s_V0x005B6D17 != 0)
        {
            AlliedVariables.s_TOrderSel_Instance!.Dispatcher.Invoke(() =>
            {
                AlliedVariables.s_TOrderSel_Instance!.OrderSelBox.SelectedIndex = eax0;
            });
        }
    }

    // L005146E8
    public static void Unit_00513838_Proc_005146E8()
    {
        AlliedVariables.s_AlliedForm1Window!.Dispatcher.Invoke(() =>
        {
            AlliedVariables.s_AlliedForm1Window!.MsgStrList?.Clear();
        });

        AlliedVariables.s_AlliedForm1Window!.Dispatcher.Invoke(() =>
        {
            AlliedVariables.s_AlliedForm1Window!.ShipList?.Clear();
        });

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            AlliedVariables.s_TDatapad_Instance!.Dispatcher.Invoke(() =>
            {
                AlliedVariables.s_TDatapad_Instance!.FGgoalList?.Clear();
            });
        }

        // System_DoneExcept();
    }

    // L0051726C
    public static void Unit_00513838_Proc_0051726C()
    {
        for (int dl = 0; dl < 0x0A; dl++)
        {
            AlliedVariables.s_V0x00543C78[dl] = 0;
        }
    }

    // L0051872C
    public static void Unit_00513838_Proc_0051872C()
    {
        AlliedVariables.s_V0x00543C04 = "none";
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.WAVfileEd, "No matching .lst file");
        AlliedVariables.s_V0x00543C3C.Clear();
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.Stop();
    }

    // L00516DDC
    public static void L00516DDC()
    {
        S0xTieFlightGroup esi0 = AlliedVariables.s_V0x005B5C74;

        AlliedVariables.s_V0x005B6AB4.Side = 0;
        AlliedVariables.s_V0x005B6AB4.Message = "New Message";
        AlliedVariables.s_V0x005B6B5C = new();
        AlliedVariables.s_V0x005B6B5C.RadioMessage = S0xTieRadioMessage.FromByteArray(AlliedVariables.s_V0x005B6AB4.ToByteArray());
        esi0.Name = "Unnamed";
        esi0.CraftId = AlliedVariables.s_DefaultShipBoxSettingIndex;
        esi0.AIRank = (byte)AlliedVariables.s_DefaultAIBoxSettingIndex;
        esi0.SpecialCraft = 0x01;
        esi0.Team = 0;
        esi0.CraftsCount = 0x01;
        esi0.FormationSpacing = 0x06;
        esi0.Markings = 0;
        esi0.Iff = AlliedVariables.s_V0x00543C78[0];

        for (int ecx = 0; ecx < 0x04; ecx++)
        {
            for (int edx = 0; edx < 0x04; edx++)
            {
                S0xTieFlightGroupOrder eax = esi0.Orders[edx * 4 + ecx];
                eax.Throttle = 0x0A;
                eax.PrimaryTarget.Operator = 0x01;
                eax.SecondaryTarget.Operator = 0x01;
                eax.Var0 = 0x01;
                eax.Var1 = 0x01;
            }
        }

        esi0.Pitch = 0x40;
        esi0.TacticalRoleUsed0 = (TacticalRoleUsedEnum)0xFF;
        esi0.TacticalRoleUsed1 = (TacticalRoleUsedEnum)0xFF;
        esi0.Comm = 0x02;
        esi0.GlobalCargoIndex = 0xFF;
        esi0.SpecialCargoIndex = 0xFF;

        for (int edx = 0; edx < 0x08; edx++)
        {
            S0xTieFlightGroupGoal eax = esi0.Goals[edx];
            eax.Condition = TieConditionEnum.Never;
        }

        for (int edx = 0; edx < 0x02; edx++)
        {
            S0xTieTrigger eax = esi0.ArrivalTrigger1.Triggers[edx];
            eax.VariableType = TieClassEnum.FlightGroup;
        }

        AlliedVariables.s_V0x005AFE94 = new();
        AlliedVariables.s_V0x005AFE90 = new();

        S0xFGObject ebx = new();
        ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(esi0.ToByteArray());
        ebx.IsWPEnabled[0] = 0x01;

        if (AlliedVariables.s_UseAutoChkSetting)
        {
            ebx.AutoLink = true;
        }

        ebx.m001448 = 1.0;
        ebx.m001450 = 0;
        ebx.m001458 = 0;
        ebx.m001460 = -1.0;
        ebx.m001468 = 0;
        ebx.m001470 = 1.0;
        Unit_00513838_Proc_0051D614();
        ebx.m001444[0] = 0x01;

        AlliedVariables.s_FlightGroupObjectsList.Add(ebx);

        AlliedVariables.s_V0x005AFE90.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(esi0.ToByteArray());
        AlliedVariables.s_V0x00543B54 = 0;

        L005146CC();

        AlliedVariables.s_V0x005AFE94 = new();
        AlliedVariables.s_V0x005AFE94.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(esi0.ToByteArray());
    }

    // L0051D614
    public static void Unit_00513838_Proc_0051D614()
    {
        AlliedVariables.s_V0x00543C4C[0] = 0;
        AlliedVariables.s_V0x00543C4C[1] = 0;
        AlliedVariables.s_V0x00543C4C[2] = 0;
        AlliedVariables.s_V0x00543C58[0] = 0x4B0;
        AlliedVariables.s_V0x00543C58[1] = 0x4B0;
        AlliedVariables.s_V0x00543C58[2] = 0x4B0;

        int ebp1C = 0;

        if (AlliedVariables.s_FlightGroupObjectsList.Count > 0)
        {
            S0xFGObject ebx0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, 0);

            double ebp28a = Unit_004B57A4_Proc_004B5844(ebx0.FlightGroupStruct.CraftId);

            if (ebp28a > 0.69999999999999996)
            {
                ebp28a = ebp28a / 2.0f;
            }

            int eax0 = (int)Math.Round(ebp28a * 160.0f);

            int ebp04 = ebx0.m00147C[0].M000000[0] + eax0;
            int ebp08 = ebx0.m00147C[0].M000000[0] - eax0;
            int ebp0C = ebx0.m00147C[1].M000000[0] + eax0;
            int ebp10 = ebx0.m00147C[1].M000000[0] - eax0;
            int ebp14 = ebx0.m00147C[2].M000000[0] + eax0;
            int ebp18 = ebx0.m00147C[2].M000000[0] - eax0;

            int edi0 = AlliedVariables.s_FlightGroupObjectsList.Count;

            for (int esi = 0x01; esi < edi0; esi++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

                if (eax1.m00147A == 0)
                {
                    continue;
                }

                ebp1C++;

                S0xFGObject ebx1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

                double ebp28 = Unit_004B57A4_Proc_004B5844(ebx1.FlightGroupStruct.CraftId);

                if (ebp28 > 0.69999999999999996)
                {
                    ebp28 = ebp28 / 2.0f;
                }

                int eax2 = (int)Math.Round(ebp28 * 160.0f);

                if (ebx1.m00147C[0].M000000[0] > ebp04)
                {
                    ebp04 = ebx1.m00147C[0].M000000[0] + eax2;
                }
                else if (ebx1.m00147C[0].M000000[0] < ebp08)
                {
                    ebp08 = ebx1.m00147C[0].M000000[0] - eax2;
                }

                if (ebx1.m00147C[1].M000000[0] > ebp0C)
                {
                    ebp0C = ebx1.m00147C[1].M000000[0] + eax2;
                }
                else if (ebx1.m00147C[1].M000000[0] < ebp10)
                {
                    ebp10 = ebx1.m00147C[1].M000000[0] - eax2;
                }

                if (ebx1.m00147C[2].M000000[0] > ebp14)
                {
                    ebp14 = ebx1.m00147C[2].M000000[0] + eax2;
                }
                else if (ebx1.m00147C[2].M000000[0] < ebp18)
                {
                    ebp18 = ebx1.m00147C[2].M000000[0] - eax2;
                }
            }

            if (ebp1C > 0)
            {
                AlliedVariables.s_V0x00543C58[0] = Math.Abs(ebp04 - ebp08);
                AlliedVariables.s_V0x00543C4C[0] = ebp08 + (ebp04 - ebp08) / 2;
                AlliedVariables.s_V0x00543C58[1] = Math.Abs(ebp0C - ebp10);
                AlliedVariables.s_V0x00543C4C[1] = ebp10 + (ebp0C - ebp10) / 2;
                AlliedVariables.s_V0x00543C58[2] = Math.Abs(ebp14 - ebp18);
                AlliedVariables.s_V0x00543C4C[2] = ebp18 + (ebp14 - ebp18) / 2;
            }
        }

        // System_DoneExcept();
    }

    // L005146CC
    public static void L005146CC()
    {
        AlliedVariables.s_V0x00543B53 = false;
        AlliedVariables.s_AlliedForm1Window!.SaveBtn.IsEnabled = false;
    }

    // L00517518
    public static void Unit_00513838_Proc_00517518()
    {
    }

    // L005121FC
    public static void Unit_00511CD0_Proc_005121FC(string eax0, ref S0xTieRegion edx0)
    {
        S0xTieRegion ebp8C = new();
        ebp8C.Name = eax0;
        edx0 = S0xTieRegion.FromByteArray(ebp8C.ToByteArray());
    }

    // L00514C10
    public static void L00514C10()
    {
        short[] ebp80 = new short[50];

        StdCtrls_TCustomListBox_SetItems(AlliedVariables.s_AlliedForm1Window!.FGMirror, AlliedVariables.s_AlliedForm1Window!.ShipList.Items);

        int eax0 = AlliedVariables.s_AlliedForm1Window!.FGMirror.Items.Count;

        for (int ebp04 = 0; ebp04 < eax0; ebp04++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebp04))
            {
                continue;
            }

            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.FGMirror, ebp04, true);
        }

        AlliedVariables.s_AlliedForm1Window!.ShipList.Clear();
        AlliedVariables.s_V0x00543BC0.Clear();
        AlliedVariables.s_Strings_Regions.Clear();
        AlliedVariables.s_Strings_Regions.Add("0");

        for (int ebp04 = 1; ebp04 < 5; ebp04++)
        {
            string ebp84 = string.Format(CultureInfo.InvariantCulture, "Region #{0}", ebp04);
            AlliedVariables.s_Strings_Regions.Add(ebp84);
        }

        if (AlliedVariables.s_FlightGroupObjectsList.Count > 0)
        {
            int eax1 = AlliedVariables.s_FlightGroupObjectsList.Count;

            for (int ebp04 = 0; ebp04 < eax1; ebp04++)
            {
                S0xFGObject ebp08 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp04);

                string ebp14_2 = Form1WindowImpl.TForm1_Proc_005258A8(AlliedVariables.s_AlliedForm1Window!, ebp08);
                string ebpAC_7 = AlliedGetCraftShortString(ebp08.FlightGroupStruct.CraftId);
                string ebpAC_6 = System_LStrFromPCharLen(ebp08.FlightGroupStruct.Name, 0x14);
                string ebp14_1 = ebpAC_7 + " " + ebpAC_6;

                if (ebp08.FlightGroupStruct.PlayerNumber != 0)
                {
                    ebp14_1 += " (" + ebp08.FlightGroupStruct.PlayerNumber.ToString(CultureInfo.InvariantCulture) + ")";
                }

                if (ebp08.FlightGroupStruct.GlobalUnitId != 0
                    && ebp08.FlightGroupStruct.GlobalUnitId <= 0x32
                    && ebp08.FlightGroupStruct.WaveNumberingOff == 0)
                {
                    string ebp14_0 = System_LStrFromPCharLen(ebp08.FlightGroupStruct.Name, 0x14);

                    byte ebp17 = 0;

                    if (ebp14_0.Length > 0)
                    {
                        ebp17 = (byte)ebp14_0[^1];
                    }

                    if (BtBitString(ebp17, AlliedVariables.s_V0x00533B80) == false && ebp14_0.Length > 0)
                    {
                        ebp14_2 += " " + (ebp80[ebp08.FlightGroupStruct.GlobalUnitId - 1] + 1).ToString(CultureInfo.InvariantCulture);

                        if (ebp08.FlightGroupStruct.PlayerNumber == 0)
                        {
                            ebp14_1 += " " + (ebp80[ebp08.FlightGroupStruct.GlobalUnitId - 1] + 1).ToString(CultureInfo.InvariantCulture);
                        }

                        ebp80[ebp08.FlightGroupStruct.GlobalUnitId - 1] += ebp08.FlightGroupStruct.CraftsCount;
                    }
                }

                byte ebp16 = 0;

                if (ebp08.FlightGroupStruct.TacticalRoleUsed0 == TacticalRoleUsedEnum.NoTC)
                {
                    if (ebp08.FlightGroupStruct.TacticalRole0 == TacticalRoleEnum.MISsionCritical)
                    {
                        ebp14_2 += " *";
                    }

                    if (ebp08.FlightGroupStruct.TacticalRole0 == TacticalRoleEnum.PRImary)
                    {
                        ebp14_2 += " ^";
                    }
                }

                if (AlliedVariables.s_V0x00543D0B != 0)
                {
                    byte ebp15 = 0x01;
                    byte bl = 0x01;

                    if (ebp16 == 0)
                    {
                        ebp14_2 += " ";
                    }

                    for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < 8; AlliedVariables.s_V0x00543CC0 += 1)
                    {
                        if (ebp08.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543CC0].Condition == TieConditionEnum.Never)
                        {
                            continue;
                        }

                        if (ebp08.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543CC0].Condition == TieConditionEnum.Always)
                        {
                            continue;
                        }

                        TieFGGoalTypeEnum al0 = ebp08.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543CC0].GoalType;

                        if (al0 >= TieFGGoalTypeEnum.Bonus)
                        {
                            if (al0 < (TieFGGoalTypeEnum)0x04)
                            {
                                if (ebp15 != 0)
                                {
                                    ebp14_2 += "+";
                                    ebp15 = 0;
                                }
                            }

                            continue;
                        }

                        if (bl != 0)
                        {
                            ebp14_2 += " [";
                            bl = 0;
                        }

                        byte al1 = AlliedVariables.s_TieFileHeader.Header.MissionType;

                        if (al1 == 0 || al1 == 0x04)
                        {
                            ebp14_2 += (ebp08.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543CC0].AppliesToTeams[1] + 1).ToString(CultureInfo.InvariantCulture);
                        }

                        if (ebp08.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543CC0].GoalType == TieFGGoalTypeEnum.Loss)
                        {
                            ebp14_2 += "N";
                        }

                        switch (ebp08.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543CC0].Condition)
                        {
                            case TieConditionEnum.Created:
                                {
                                    ebp14_2 += "a";
                                    break;
                                }

                            case TieConditionEnum.Destroyed:
                                {
                                    ebp14_2 += "d";
                                    break;
                                }

                            case TieConditionEnum.Seized:
                                {
                                    ebp14_2 += "c";
                                    break;
                                }

                            case TieConditionEnum.Inspected:
                                {
                                    ebp14_2 += "i";
                                    break;
                                }

                            case TieConditionEnum.Boarded:
                                {
                                    ebp14_2 += "b";
                                    break;
                                }

                            case TieConditionEnum.Docked:
                                {
                                    ebp14_2 += "dk";
                                    break;
                                }

                            case TieConditionEnum.Disabled:
                                {
                                    ebp14_2 += "di";
                                    break;
                                }

                            case TieConditionEnum.CompletedMission:
                                {
                                    ebp14_2 += "m";
                                    break;
                                }

                            case TieConditionEnum.BePickedUp:
                                {
                                    ebp14_2 += "bg";
                                    break;
                                }

                            case TieConditionEnum.Withdrew:
                                {
                                    ebp14_2 += "w";
                                    break;
                                }

                            default:
                                {
                                    ebp14_2 += "*";
                                    break;
                                }
                        }
                    }

                    if (bl == 0)
                    {
                        ebp14_2 += "]";
                    }
                }

                AlliedVariables.s_AlliedForm1Window!.ShipList.AddItem(ebp14_2);
                AlliedVariables.s_V0x00543BC0.Add(ebp14_1);
                AlliedVariables.s_Strings_Regions.Add(ebp14_1);
            }

            if (AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count > 0
                && AlliedVariables.s_AlliedForm1Window!.FGMirror.Items.Count > 0)
            {
                int eax2 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

                for (int ebp04 = 0; ebp04 < eax2; ebp04++)
                {
                    if (AlliedVariables.s_AlliedForm1Window!.FGMirror.Items.Count <= ebp04)
                    {
                        continue;
                    }

                    if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.FGMirror, ebp04))
                    {
                        continue;
                    }

                    StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebp04, true);
                }
            }
        }

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            DatapadFGPageEnum page = (DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag);

            if (page == DatapadFGPageEnum.Arrival || page == DatapadFGPageEnum.Departure)
            {
                int ebp04 = AlliedVariables.s_TDatapad_Instance!.ArrMotherBox.SelectedIndex;
                int ebx = AlliedVariables.s_TDatapad_Instance!.ArrAltmotherBox.SelectedIndex;

                DatapadWindowImpl.TDatapad_Proc_004C2F30(AlliedVariables.s_TDatapad_Instance!);

                AlliedVariables.s_TDatapad_Instance!.ArrMotherBox.SelectedIndex = ebp04;
                AlliedVariables.s_TDatapad_Instance!.ArrAltmotherBox.SelectedIndex = ebx;
            }
        }

        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SetItems(AlliedVariables.s_V0x00543BC0);
        AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex = AlliedVariables.s_V0x00543B0C;
    }

    // L0051DB88
    public static string AlliedGetCraftShortString(CraftIdEnum craftId)
    {
        int count = AlliedVariables.s_Strings_Short.GetCount();

        if ((int)craftId < count)
        {
            return AlliedVariables.s_Strings_Short.GetText((int)craftId);
        }

        return "Unk";
    }

    // L005182B8
    public static void L005182B8(byte al0, byte dl0)
    {
        AlliedVariables.s_V0x00543C78[0] = al0;
        AlliedVariables.s_V0x00543C78[1] = dl0;

        if (al0 == 0)
        {
            S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
            eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Rebel");
            eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0x01);
            eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Imperial");
        }
        else if (al0 == 0x01)
        {
            S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
            eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Imperial");
            eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0x01);
            eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Rebel");
        }
    }

    // L00511E04
    public static string Unit_00511CD0_Proc_00511E04(string eax0)
    {
        return eax0.WithMaxLength(16);
    }

    // L0051A1E8
    private static void L0051A1E8()
    {
        for (int ebp04 = 0; ebp04 < 0x08; ebp04++)
        {
            S0xTieBriefing ebp5DEC = new();

            ebp5DEC.BriefingData.BriefingCode.m00000A.Clear();
            ebp5DEC.BriefingData.BriefingCode.ForTeam.Clear();
            ebp5DEC.BriefingData.BriefingCode.m0036BA.Clear();
            ebp5DEC.BriefingData.m004414.Clear();

            ebp5DEC.BriefingData.BriefingCode.Time = 0;
            ebp5DEC.BriefingData.BriefingCode.Index = 0;
            ebp5DEC.BriefingData.BriefingCode.Title = 0;
            ebp5DEC.BriefingData.BriefingCode.CodeSize = 0x02;
            ebp5DEC.BriefingData.BriefingCode.m00000A[0] = 0x270F;
            ebp5DEC.BriefingData.BriefingCode.m00000A[1] = 0x22;
            ebp5DEC.BriefingData.BriefingCode.Length = 0x258;

            ebp5DEC.BriefingData.BriefingCode.ForTeam[ebp04] = 0x01;

            ebp5DEC.BriefingTags = new();
            ebp5DEC.BriefingStrings = new();

            L0052856C(AlliedVariables.s_AlliedForm1Window!, ebp5DEC.BriefingTags);
            L0052856C(AlliedVariables.s_AlliedForm1Window!, ebp5DEC.BriefingStrings);

            AlliedVariables.s_Allied_Briefing[ebp04] = ebp5DEC;
        }
    }

    // L0052856C
    private static void L0052856C(Form1Window Form1, TStrings edx0)
    {
        edx0.Clear();

        for (int bl = 0; bl < 0x80; bl++)
        {
            edx0.Add(string.Empty);
        }
    }

    // L005157D0
    public static string Unit_00513838_Proc_005157D0(string eax0, byte edx0)
    {
        string ebp0C;
        char ebx;

        if (BtBitString((byte)eax0[0], AlliedVariables.s_V0x00533BA0))
        {
            ebx = eax0[0];
            ebp0C = eax0;
            System_LStrDelete(ref ebp0C, 0x01, 0x01);
        }
        else
        {
            ebx = 'x';
            ebp0C = eax0;
        }

        if (edx0 == 0x79)
        {
            AlliedVariables.s_V0x005B6D10 = ebx;
        }

        return ebp0C;
    }

    // L0051F534
    public static int Allied_Time_ToSeconds_L0051F534(int eax0)
    {
        if (eax0 > 0x14)
        {
            eax0 = 0x14 + (eax0 - 0x14) * 0x05;
        }

        return eax0;
    }

    // L0051C47C
    public static string Allied_TimeInSeconds_ToMinutesSecondsString(int eax0)
    {
        string ebp0C_2 = string.Empty;

        if (eax0 > 0x3B)
        {
            string ebp0C_1 = (eax0 / 0x3C).ToString(CultureInfo.InvariantCulture);
            ebp0C_2 += ebp0C_1 + ":";
        }
        else
        {
            ebp0C_2 += "0:";
        }

        int esi = eax0 % 0x3C;

        if (esi < 0x0A)
        {
            ebp0C_2 += "0";
        }

        ebp0C_2 += esi.ToString(CultureInfo.InvariantCulture);
        return ebp0C_2;
    }

    // L00517564
    public static void Unit_00513838_Proc_00517564()
    {
    }

    // L00513F30
    public static void Unit_00513838_Proc_00513F30()
    {
        AlliedVariables.s_V0x005B6BBC = Classes_TList_Get(AlliedVariables.s_V0x00571188, 0);

        Form1WindowImpl.L005273D4(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6BBC.GGStrings);
        Form1WindowImpl.Unit_00513838_Proc_005187A0();

        if (AlliedVariables.s_TieFileHeader.RadioMessagesCount > 0)
        {
            AlliedVariables.s_V0x005B6B58 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, 0);

            Form1WindowImpl.TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6B58.RadioMessage);

            if (AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count > 0)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, 0, true);
            }
        }

        L00517568(0);
        AlliedVariables.s_V0x00543B18 = 0;
        Unit_00513838_Proc_00515BE0(0);
        AlliedVariables.s_V0x00543B20 = 0;
        AlliedVariables.s_V0x00543B3C = 0x01;
        AlliedVariables.s_V0x00543B28 = 0x01;
        AlliedVariables.s_V0x00543B2C = 0x01;
        AlliedVariables.s_V0x00543B4C = 0x01;
        CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, 0x01);
        L005146CC();
        AlliedVariables.s_V0x00543B54 = 0;
        AlliedVariables.s_V0x00543B55 = 0;

        if (AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count > 0)
        {
            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, 0, true);

            S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, 0);
            eax2.m001479 = 0x01;
        }
    }

    // L00515A04
    public static void Unit_00513838_Proc_00515A04()
    {
        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            if (AlliedVariables.s_Strings_Teams.GetCount() > 0)
            {
                AlliedVariables.s_TDatapad_Instance!.SeenByList.Clear();

                for (int ebp04 = 0; ebp04 < 0x08; ebp04++)
                {
                    AlliedVariables.s_TDatapad_Instance!.SeenByList.AddItem(AlliedVariables.s_Strings_Teams.GetText(ebp04));
                }
            }
        }
    }

    // L0051BF50
    public static void L0051BF50(int eax0)
    {
        for (int ebx = 0; ebx < 8; ebx++)
        {
            S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, eax0);
            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.FriendsList, ebx, eax1.Team.TeamAllied[ebx] != TieAllegeanceEnum.Hostile);
        }
    }

    // L00517568
    public static void L00517568(int eax0)
    {
        AlliedVariables.s_V0x005AFE90 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
        AlliedVariables.s_V0x005424C8 = AlliedVariables.s_V0x00543B0C;
        DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
        AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex = eax0;
    }

    // L00515BE0
    public static void Unit_00513838_Proc_00515BE0(int eax0)
    {
        AlliedVariables.s_V0x00543C9A = 0;

        bool bl = AlliedVariables.s_V0x00543B53;

        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Label52, "Team " + (eax0 + 1).ToString(CultureInfo.InvariantCulture) + " Name");
        AlliedVariables.s_V0x005B6BB4 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, eax0);
        S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, eax0);
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.TeamName1Ed, System_LStrFromPCharLen(eax1.Team.Name, 0x10));
        AlliedVariables.s_AlliedForm1Window!.TeamIFFBox1.SelectedIndex = AlliedVariables.s_V0x00543C78[eax0];
        L0051727C(eax0);
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Succ1Ed, System_LStrFromPCharLen(AlliedVariables.s_V0x005B6BB4.Team.PrimarySuccessMessage1, 0x40));
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Succ2Ed, System_LStrFromPCharLen(AlliedVariables.s_V0x005B6BB4.Team.PrimarySuccessMessage2, 0x40));
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Fail1Ed, System_LStrFromPCharLen(AlliedVariables.s_V0x005B6BB4.Team.PrimaryFailureMessage1, 0x40));
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Fail2Ed, System_LStrFromPCharLen(AlliedVariables.s_V0x005B6BB4.Team.PrimaryFailureMessage2, 0x40));
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Sec1Ed, System_LStrFromPCharLen(AlliedVariables.s_V0x005B6BB4.Team.SecondarySuccessMessage1, 0x40));
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Sec2Ed, System_LStrFromPCharLen(AlliedVariables.s_V0x005B6BB4.Team.SecondarySuccessMessage2, 0x40));
        L0051BF50(eax0);

        AlliedVariables.s_V0x00543B53 = bl;
        AlliedVariables.s_AlliedForm1Window!.SaveBtn.IsEnabled = bl;

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L0051727C
    private static void L0051727C(int eax0)
    {
        uint ebx = AlliedGetIffColor(AlliedVariables.s_V0x00543C78[eax0 & 0xff], 0);

        Graphics_TFont_SetColor(AlliedVariables.s_AlliedForm1Window!.Succ1Ed, ebx);
        Graphics_TFont_SetColor(AlliedVariables.s_AlliedForm1Window!.Succ2Ed, ebx);
        Graphics_TFont_SetColor(AlliedVariables.s_AlliedForm1Window!.Fail1Ed, ebx);
        Graphics_TFont_SetColor(AlliedVariables.s_AlliedForm1Window!.Fail2Ed, ebx);
        Graphics_TFont_SetColor(AlliedVariables.s_AlliedForm1Window!.Sec1Ed, ebx);
        Graphics_TFont_SetColor(AlliedVariables.s_AlliedForm1Window!.Sec2Ed, ebx);
    }

    // L005142C0
    public static uint AlliedGetIffColor(byte iff, byte edx0)
    {
        uint ecx = 0;

        if (edx0 == 0x00)
        {
            ecx = (iff & 0xff) switch
            {
                0x00 => 0x0000FF00,
                0x01 => 0x000000FF,
                0x02 => 0x00FF8000,
                0x03 => 0x0000FFFF,
                0x04 => 0x000000FF,
                0x05 => 0x00FD55DB,
                _ => 0x00FF8000,
            };
            return ecx;
        }

        if (edx0 == 0x01)
        {
            ecx = (iff & 0xff) switch
            {
                0x00 => 0x0095FF95,
                0x01 => 0x008C8CFF,
                0x02 => 0x00FFCC99,
                0x03 => 0x00B0FFFF,
                0x04 => 0x000000FF,
                0x05 => 0x00FDACED,
                _ => 0x00FFCC99,
            };
            return ecx;
        }

        if (edx0 == 0x02)
        {
            ecx = (iff & 0xff) switch
            {
                0x00 => 0x0000EA00,
                0x01 => 0x000000FF,
                0x02 => 0x00FF8000,
                0x03 => 0x0000FFFF,
                0x04 => 0x000000FF,
                0x05 => 0x00FC25D1,
                _ => 0x00FF8000,
            };
            return ecx;
        }

        return ecx;
    }

    // L004B5844
    public static double Unit_004B57A4_Proc_004B5844(CraftIdEnum eax0)
    {
        double esp08;

        if ((int)eax0 <= 0xFF && BtBitString((int)eax0, AlliedVariables.s_V0x00533B40))
        {
            esp08 = 0.005;
        }
        else
        {
            switch (eax0)
            {
                case CraftIdEnum._020_0_43_ScoutCraft:
                case CraftIdEnum._026_0_53_ContainerBrick:
                case CraftIdEnum._028_0_55_ContainerTube:
                case CraftIdEnum._151_0_63_PropaneTank:
                    esp08 = 0.08;
                    break;

                case CraftIdEnum._027_0_54_ContainerHexBox:
                case CraftIdEnum._029_0_56_ContainerPronged:
                case CraftIdEnum._055_0_57_ContainerHemisphere:
                case CraftIdEnum._056_0_58_ContainerSlotted:
                case CraftIdEnum._059_0_61_ContainerYshaped:
                case CraftIdEnum._102_0_50_LuxuryYacht:
                    esp08 = 0.05;
                    break;

                case CraftIdEnum._032_0_68_BulkFreighter:
                case CraftIdEnum._033_0_69_CargoFerry:
                case CraftIdEnum._034_0_70_ModularConveyor:
                case CraftIdEnum._036_0_72_TunaBoat2:
                case CraftIdEnum._037_0_44_MuurianTransport:
                case CraftIdEnum._040_0_80_Corvette2:
                case CraftIdEnum._041_0_81_ModCorvette:
                case CraftIdEnum._104_0_74_ModActionTransport:
                case CraftIdEnum._105_0_75_MobquetTransport:
                case CraftIdEnum._112_0_142_Suprosa:
                    esp08 = 0.15;
                    break;

                case CraftIdEnum._035_0_71_ContainerTransport:
                case CraftIdEnum._042_0_82_Frigate2:
                case CraftIdEnum._043_0_83_ModFrigate:
                case CraftIdEnum._045_0_85_CarrackCruiser:
                case CraftIdEnum._103_0_51_FerryboatLiner:
                case CraftIdEnum._141_0_124_AsteroidMiningUnit:
                    esp08 = 0.36;
                    break;

                case CraftIdEnum._044_0_84_PassengerLiner:
                case CraftIdEnum._164_0_136_CrewCabinFront:
                    esp08 = 0.43;
                    break;

                case CraftIdEnum._046_0_86_StrikeCruiser:
                case CraftIdEnum._047_0_87_EscortCarrier:
                case CraftIdEnum._048_0_88_Dreadnaught2:
                case CraftIdEnum._050_0_90_LightCalamariCruiser:
                case CraftIdEnum._051_0_91_Interdictor2:
                case CraftIdEnum._069_0_109_Factory:
                case CraftIdEnum._092_0_95_ModStrikeCruiser:
                case CraftIdEnum._094_0_97_BulkCruiser:
                    esp08 = 0.6;
                    break;

                case CraftIdEnum._049_0_89_CalamariCruiserNew:
                case CraftIdEnum._090_0_110_ShipYard:
                case CraftIdEnum._091_0_111_RepairYard:
                case CraftIdEnum._133_0_116_SensorArray:
                case CraftIdEnum._139_0_122_CargoFacility1:
                case CraftIdEnum._145_0_128_FamilyBase:
                case CraftIdEnum._228_0_135_CalamariWinged:
                    esp08 = 1.6;
                    break;

                case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
                case CraftIdEnum._066_0_108_AsteroidBase:
                case CraftIdEnum._147_0_130_PirateShipyard:
                case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
                    esp08 = 0.9;
                    break;

                case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
                case CraftIdEnum._130_0_113_GolanTwo:
                case CraftIdEnum._135_0_118_SpaceColony1:
                case CraftIdEnum._136_0_119_SpaceColony2:
                case CraftIdEnum._146_0_129_FamilyRepairYard:
                case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                    esp08 = 2.0;
                    break;

                case CraftIdEnum._054_0_94_SuperStarDestroyer:
                    if (AlliedVariables.s_SSD17chkSetting)
                    {
                        esp08 = 17.6;
                    }
                    else
                    {
                        esp08 = 8.0;
                    }

                    break;

                case CraftIdEnum._057_0_59_ContainerHourglass:
                case CraftIdEnum._058_0_60_ContainerGem:
                    esp08 = 0.025;
                    break;

                case CraftIdEnum._060_0_102_Platform1:
                case CraftIdEnum._061_0_103_Platform2:
                case CraftIdEnum._062_0_104_Platform3:
                case CraftIdEnum._063_0_105_Platform4:
                case CraftIdEnum._064_0_106_Platform5:
                case CraftIdEnum._065_0_107_Platform6:
                case CraftIdEnum._129_0_112_GolanOne:
                case CraftIdEnum._138_0_121_Casino:
                case CraftIdEnum._148_0_131_IndustrialComplex:
                    esp08 = 1.0;
                    break;

                case CraftIdEnum._067_0_133_AsteroidLaserBattery:
                case CraftIdEnum._068_0_134_AsteroidWarheadLauncher:
                case CraftIdEnum._152_0_64_ContainerGrande:
                    esp08 = 0.1;
                    break;

                case CraftIdEnum._093_0_96_LancerFrigate:
                    esp08 = 0.23;
                    break;

                case CraftIdEnum._095_0_98_AssaultFrigate:
                case CraftIdEnum._101_0_101_ImpResearchShip:
                case CraftIdEnum._132_0_115_DerilynPlatform:
                case CraftIdEnum._137_0_120_SpaceColony3:
                    esp08 = 0.7;
                    break;

                case CraftIdEnum._096_0_99_CorellianGunship:
                case CraftIdEnum._144_0_127_ImpResearchCenter:
                    esp08 = 1.3;
                    break;

                case CraftIdEnum._098_0_49_AssaultShuttle:
                    esp08 = 0.03;
                    break;

                case CraftIdEnum._099_0_100_MarauderCorvette:
                case CraftIdEnum._106_0_76_XiytiarTransport:
                    esp08 = 0.18;
                    break;

                case CraftIdEnum._100_0_73_StarGalleon:
                    esp08 = 0.3;
                    break;

                case CraftIdEnum._107_0_77_FreighterConB:
                case CraftIdEnum._155_0_67_ContainerHanger:
                    esp08 = 0.2;
                    break;

                case CraftIdEnum._108_0_78_FreighterConG:
                case CraftIdEnum._109_0_79_FreighterBox:
                    esp08 = 0.12;
                    break;

                case CraftIdEnum._131_0_114_GolanThree:
                case CraftIdEnum._140_0_123_CargoFacility2:
                case CraftIdEnum._143_0_126_RebelPlatform:
                    esp08 = 2.4;
                    break;

                case CraftIdEnum._134_0_117_CommRelay:
                    esp08 = 0.5;
                    break;

                case CraftIdEnum._142_0_125_ProcessingPlant:
                    esp08 = 0.39;
                    break;

                default:
                    esp08 = 0.01;
                    break;
            }
        }

        double esp00 = esp08 / 2.0f;
        return esp00;
    }

    // L005208F8
    public static bool Unit_00513838_Proc_005208F8(int eax0)
    {
        S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);

        if (eax.FlightGroupStruct.StartPointRegions[0] == AlliedVariables.s_CurrentRegion - 1)
        {
            return true;
        }

        return false;
    }

    // L0051D8A0
    public static void L0051D8A0(int eax0)
    {
        // todo

        MenuItem ebx0 = new();
        Menus_TMenuItem_SetCaption(ebx0, AlliedHistoryCreateMenuText(eax0));
        MenuItem eax1 = Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.ShipListPopUp, 0x0C);
        //ebx0.m000088 = eax1.m000088;
        //ebx0.m00008C = eax1.m00008C;

        Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.ShipListPopUp, 0x0C), true);
        Menus_TMenuItem_Add(AlliedVariables.s_AlliedForm1Window!.Reopen1, ebx0);

        MenuItem ebx1 = new();
        Menus_TMenuItem_SetCaption(ebx1, AlliedHistoryCreateMenuText(eax0));
        MenuItem eax2 = Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.ShipListPopUp, 0x0C);
        //ebx1->m000088 = eax2->m000088;
        //ebx1->m00008C = eax2->m00008C;
        Menus_TMenuItem_Add(AlliedVariables.s_AlliedForm1Window!.HistPopUp, ebx1);
    }

    // L0051DA0C
    public static string AlliedHistoryCreateMenuText(int eax0)
    {
        string edx0;

        if (AlliedVariables.s_DirInHistChkSetting)
        {
            string ebp14_3 = AlliedVariables.s_Allied_FilenamesHistory.GetText(eax0);
            edx0 = "_" + eax0.ToString(CultureInfo.InvariantCulture) + " " + ebp14_3;
        }
        else
        {
            string ebp14_1 = Path.GetFileName(AlliedVariables.s_Allied_FilenamesHistory.GetText(eax0));
            edx0 = "_" + eax0.ToString(CultureInfo.InvariantCulture) + " " + ebp14_1;
        }

        return edx0;
    }

    // L0051FE4C
    public static ShipSeqEnum AlliedConvertCraftIdToShipSeq(CraftIdEnum eax0)
    {
        string ebp04 = ((int)eax0).ToString(CultureInfo.InvariantCulture);
        ShipSeqEnum ebx = (ShipSeqEnum)AlliedVariables.s_Strings_ShipSeq.IndexOf(ebp04);
        return ebx;
    }

    // L0051D1C4
    public static void AlliedHistoryAddStr(TStrings StringList, string str)
    {
        int ebx0 = StringList.IndexOf(str);

        if (ebx0 > -1)
        {
            StringList.Delete(ebx0);
        }

        if (StringList.GetCount() > 0)
        {
            StringList.Insert(0, str);
        }
        else
        {
            StringList.Add(str);
        }

        if (StringList.GetCount() > 0x09)
        {
            StringList.Delete(StringList.GetCount() - 1);
        }

        if (StringList.GetCount() > Menus_TMenuItem_GetCount(AlliedVariables.s_AlliedForm1Window!.Reopen1))
        {
            L0051D8A0(StringList.GetCount() - 1);
        }

        int eax0 = StringList.GetCount() - 1;

        for (int ebx = 0; ebx < eax0; ebx++)
        {
            string ebp08_0 = AlliedHistoryCreateMenuText(ebx);

            if (ebx < Menus_TMenuItem_GetCount(AlliedVariables.s_AlliedForm1Window!.Reopen1))
            {
                Menus_TMenuItem_SetCaption(Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.Reopen1, ebx), ebp08_0);
            }

            if (ebx < Menus_TMenuItem_GetCount(AlliedVariables.s_AlliedForm1Window!.HistPopUp))
            {
                Menus_TMenuItem_SetCaption(Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.HistPopUp, ebx), ebp08_0);
            }
        }
    }

    // L0052398C
    public static void TForm1_ReadTieMission(Form1Window Form1, string str)
    {
        TieFileVersionEnum ebx0 = 0;

        if (!File.Exists(str))
        {
            MessageBox_ShowError(AlliedVariables.s_V0x00543BF8 + " does not exist.");
        }
        else
        {
            TieFileVersionEnum esi0 = AlliedVariables.s_TieFileVersion;
            AlliedVariables.s_V0x005B704C = 0;
            Unit_00513838_Proc_0051726C();
            AlliedVariables.s_V0x005B6D19 = 0;

            AlliedVariables.s_TieFileHandle.Assign(str);
            AlliedVariables.s_TieFileHandle.OpenFileForRead(0x01);
            System_L004028C4_CheckError();

            Unit_00513838_Proc_005144F8();

            AlliedVariables.s_V0x00543B0C = 0;
            AlliedVariables.s_V0x00543B10 = 0;
            AlliedVariables.s_V0x00543B14 = 0;
            AlliedVariables.s_CurrentRegion = 0x01;
            AlliedVariables.s_CurrentOrderInRegion = 0x01;
            AlliedVariables.s_V0x005B7050 = 0;

            if (AlliedVariables.s_V0x005B6D17 != 0)
            {
                AlliedVariables.s_TOrderSel_Instance!.RegionTabs.SelectedIndex = 0;
                AlliedVariables.s_TOrderSel_Instance!.OrderSelBox.SelectedIndex = 0;
                Menus_TMenuItem_SetChecked(Form1.Region11, true);
                Menus_TMenuItem_SetChecked(Form1.Order11, true);
            }

            AlliedVariables.s_V0x00543B18 = 0;

            Unit_00513838_Proc_005146E8();

            for (int ebx = 0; ebx < 0x109; ebx++)
            {
                S0x0056A874 edx = AlliedVariables.s_V0x0056A874[ebx];
                edx.unk000000.Clear();
            }

            AlliedVariables.s_V0x00570FF8.m000000.Clear();
            AlliedVariables.s_V0x00570FFC.unk000000.Clear();
            AlliedVariables.s_V0x00571000.m000000.Clear();
            AlliedVariables.s_V0x00546144.unk000000.Clear();
            AlliedVariables.s_V0x0054A578.unk000000.Clear();
            AlliedVariables.s_V0x0054A5E4.unk000000.Clear();
            AlliedVariables.s_V0x0054AE60.unk000000.Clear();
            AlliedVariables.s_V0x0054AEAC.unk000000.Clear();
            AlliedVariables.s_V0x0056A2AC.unk000000.Clear();
            AlliedVariables.s_V0x00571004.unk000000.Clear();
            AlliedVariables.s_V0x005710F0.unk000000.Clear();
            AlliedVariables.s_V0x0057111C.unk000000.Clear();

            AlliedVariables.s_V0x00543D0C = 0x01;

            byte[] buffer = new byte[2];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00535EA4);
            AlliedVariables.s_TieFileVersion = (TieFileVersionEnum)BitConverter.ToUInt16(buffer, 0);
            System_L004028C4_CheckError();

            ebx0 = AlliedVariables.s_TieFileVersion;

            switch (AlliedVariables.s_TieFileVersion)
            {
                case TieFileVersionEnum.XWing:
                    {
                        Allied_ReadTieMission_01(esi0);
                        break;
                    }

                case TieFileVersionEnum.TieFighter:
                    {
                        Allied_ReadTieMission_02();
                        break;
                    }

                case TieFileVersionEnum.XvT:
                case TieFileVersionEnum.Bop:
                    {
                        XvTBoxImpl.Allied_ReadTieMission_XvT(AlliedVariables.s_TXvTForm_Instance!);
                        break;
                    }

                case TieFileVersionEnum.XWA:
                    {
                        Allied_ReadTieMission_XWA(esi0);
                        break;
                    }

                default:
                    {
                        MessageBox_ShowError("Unrecognized file type");
                        AlliedVariables.s_V0x00543D0C = 0;
                        break;
                    }
            }

            AlliedVariables.s_TieFileHandle.Close();
            System_L004028C4_CheckError();

            if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWing)
            {
                Unit_00513838_Proc_005140A8();
                AlliedVariables.s_V0x005B6BB8 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);
                string ebp5E20_1 = Path.GetFileName(str);
                string ebp10_1 = string.Empty;
                string ebp5E20_2 = " " + ProductVersionHelpers.GetNameAndVersion() + " - (" + ebp5E20_1 + ") - " + ebp10_1;
                Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!, ebp5E20_2);
                DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
                AlliedVariables.s_V0x00543C9C = 0x01;
                DatapadWindowImpl.TDatapad__PROC_004C05F0(AlliedVariables.s_TDatapad_Instance!);
                AlliedVariables.s_TErrForm_OmitWarns = false;
                AlliedVariables.s_V0x00543B50 = 0x01;

                if (AlliedVariables.s_V0x005B6D14 != 0)
                {
                    AlliedVariables.s_V0x00543D11 = 0x01;
                }
            }
            else if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XvT || AlliedVariables.s_TieFileVersion == TieFileVersionEnum.Bop || AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
            {
                Unit_00513838_Proc_00513F30();
                AlliedVariables.s_V0x005B6BB8 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);

                string ebp10_1 = string.Empty;

                if (AlliedVariables.s_Allied_Briefing[0].BriefingStrings.GetCount() > 0)
                {
                    ebp10_1 = AlliedVariables.s_Allied_Briefing[0].BriefingStrings.GetText(0);
                }

                if (ebp10_1.Length > 1)
                {
                    if (ebp10_1[0] == '>')
                    {
                        System_LStrDelete(ref ebp10_1, 0x01, 0x01);
                    }
                }

                string ebp5E20_9 = Path.GetFileName(str);
                string ebp5E20_10 = " " + ProductVersionHelpers.GetNameAndVersion() + " - (" + ebp5E20_9 + ") - " + ebp10_1;
                Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!, ebp5E20_10);
                DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
                AlliedVariables.s_V0x00543C9C = 0x01;
                DatapadWindowImpl.TDatapad__PROC_004C05F0(AlliedVariables.s_TDatapad_Instance!);
                AlliedVariables.s_TErrForm_OmitWarns = false;
                AlliedVariables.s_V0x00543B50 = 0x01;

                string ebp10_0 = Allied_GetFileNameWithLstExtension(str);
                AlliedVariables.s_V0x00543C04 = AlliedVariables.s_XWADirLabSetting + "\\Wave\\MissionVoice\\" + ebp10_0;

                if (!File.Exists(AlliedVariables.s_V0x00543C04))
                {
                    string ebp5E20_8 = System_LStrFromChar(AlliedVariables.s_AlliedDriveLetter);
                    AlliedVariables.s_V0x00543C04 = ebp5E20_8 + ":\\Wave\\MissionVoice\\" + ebp10_0;
                }

                if (!File.Exists(AlliedVariables.s_V0x00543C04))
                {
                    Unit_00513838_Proc_0051872C();
                }
                else
                {
                    AlliedVariables.s_V0x00543C3C.LoadFromFile(AlliedVariables.s_V0x00543C04);
                    Controls_TControl_SetVisible(Form1.WAVfileEd, true);

                    if (AlliedVariables.s_V0x00543C3C.GetCount() > 0)
                    {
                        Controls_TControl_SetText(Form1.WAVfileEd, AlliedVariables.s_V0x00543C3C.GetText(AlliedVariables.s_V0x00543B10));
                        string ebp5E20_6 = AlliedVariables.s_V0x00543C3C.GetText(AlliedVariables.s_V0x00543B10);
                        Form1.WAVplayer.SoundLocation = AlliedVariables.s_XWADirLabSetting + "\\wave\\" + ebp5E20_6;
                    }
                }

                AlliedVariables.s_V0x00543B59 = 0;
                Unit_00513838_Proc_0051477C();

                if (AlliedVariables.s_V0x005B6D14 != 0)
                {
                    AlliedVariables.s_V0x00543D11 = 0x01;
                }

                if (AlliedVariables.s_V0x00543D0C != 0
                    && AlliedVariables.s_DoBackupsChkSetting
                    && AlliedVariables.s_V0x00543BF8[0] != AlliedVariables.s_AlliedDriveLetter)
                {
                    string ebp5E20_5 = Path.GetFileName(AlliedVariables.s_V0x00543BF8).ToUpperInvariant();

                    if (!string.Equals(ebp5E20_5, "BACKUP.TIE", StringComparison.Ordinal)
                        && !string.Equals(AlliedVariables.s_V0x00543BF8, "Unnamed", StringComparison.Ordinal))
                    {
                        string ebp5E20_3 = Path.GetDirectoryName(AlliedVariables.s_V0x00543BF8) + "\\Backup.tie";
                        Allied_WriteTieMission(ebp5E20_3, 0, 0);
                    }
                }
            }
            else
            {
                Unit_00513838_Proc_005196CC();
                Unit_00513838_Proc_00513F30();
            }

            ComCtrls_TToolButton_SetDown(Form1.R1btn, true);

            Unit_00513838_Proc_00519B48(0x01);
        }

        AlliedVariables.s_V0x005B7064 = 0;
        AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        AlliedVariables.s_V0x005B7044 = 0x01;

        ComCtrls_TToolButton_SetDown(Form1.CurrOnly, false);
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        MapWindowImpl.TMapForm_Proc_004F6B48(AlliedVariables.s_TMapForm_Instance!);
        AlliedVariables.s_V0x005B704C = 0x01;

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            Form1WindowImpl.TForm1_FitBattleBtnClick(Form1, Form1.FitBattleBtn);
        }

        OrderSelWindowImpl.TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);
        S0xTieBriefing ebp5DF4 = AlliedVariables.s_Allied_Briefing[0];

        if ((short)ebx0 < 0 && AlliedVariables.s_V0x005B6D19 != 0)
        {
            TModalResultEnum ax2 = MessageBox_ShowConfirmation("Some FGs have cargo turned off (probably a TIE Fighter conversion.)\r\n\r\nTurn cargo on?", null);

            if (ax2 == TModalResultEnum.Yes)
            {
                int esi1 = AlliedVariables.s_FlightGroupObjectsList.Count;

                for (int ebx = 0; ebx < esi1; ebx++)
                {
                    S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                    if (eax3.FlightGroupStruct.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop)
                    {
                        continue;
                    }

                    eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                    if (eax3.FlightGroupStruct.GlobalCargoIndex == 0)
                    {
                        eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax3.FlightGroupStruct.GlobalCargoIndex = 0xFF;
                    }
                }

                Unit_00513838_Proc_005146A4();
            }
        }
    }

    // L005144F8
    public static void Unit_00513838_Proc_005144F8()
    {
        AlliedVariables.s_FlightGroupObjectsList.Clear();
        AlliedVariables.s_RadioMessagesObjectsList.Clear();
        AlliedVariables.s_GlobalGoalsObjectsList.Clear();
        AlliedVariables.s_TeamsObjectsList.Clear();
        AlliedVariables.s_V0x00571188.Clear();
    }

    // L00519F38
    public static void Allied_ReadTieMission_XWA(TieFileVersionEnum eax0)
    {
        TFileRec ebx = AlliedVariables.s_TieFileHandle;
        S0x00570FF8 ebp = AlliedVariables.s_V0x00570FF8;

        AlliedVariables.s_V0x00543C9F = 0;

        Allied_ReadTieMission_XWA_FileHeader(AlliedVariables.s_AlliedForm1Window!);
        Allied_ReadTieMission_XWA_FlightGroups(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_0051DB68();
        Allied_ReadTieMission_XWA_RadioMessages(AlliedVariables.s_AlliedForm1Window!);
        Allied_ReadTieMission_XWA_GlobalGoals(AlliedVariables.s_AlliedForm1Window!);
        Allied_ReadTieMission_XWA_Teams(AlliedVariables.s_AlliedForm1Window!);
        Allied_ReadTieMission_XWA_Briefing(AlliedVariables.s_AlliedForm1Window!);

        ebx.BlockRead(AlliedVariables.s_V0x00546144.unk000000, 0x4432, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(ebp.m000000, 0, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(AlliedVariables.s_V0x00570FFC.unk000000, 0, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();

        if (ebp.m000000[0] == 0x01)
        {
            ebx.BlockRead(AlliedVariables.s_V0x00571000.m000000, 0, 0x01, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
        }

        ebx.BlockRead(AlliedVariables.s_V0x00571004.unk000000, 0xEA, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(ebp.m000000, 1, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(AlliedVariables.s_V0x00570FFC.unk000000, 1, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();

        if (ebp.m000000[1] == 0x01)
        {
            ebx.BlockRead(AlliedVariables.s_V0x00571000.m000000, 1, 0x01, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
        }

        ebx.BlockRead(AlliedVariables.s_V0x005710F0.unk000000, 0x2C, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(ebp.m000000, 2, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(AlliedVariables.s_V0x00570FFC.unk000000, 2, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();

        if (ebp.m000000[2] == 0x01)
        {
            ebx.BlockRead(AlliedVariables.s_V0x00571000.m000000, 2, 0x01, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
        }

        ebx.BlockRead(AlliedVariables.s_V0x0057111C.unk000000, 0x58, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(ebp.m000000, 3, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(AlliedVariables.s_V0x00570FFC.unk000000, 3, 0x01, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();

        if (ebp.m000000[3] == 0x01)
        {
            ebx.BlockRead(AlliedVariables.s_V0x00571000.m000000, 3, 0x01, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
        }

        ebx.BlockRead(AlliedVariables.s_V0x0054A578.unk000000, 0x6C, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(AlliedVariables.s_TieMission_Notes, 0x1000, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        ebx.BlockRead(AlliedVariables.s_V0x0054A5E4.unk000000, 0x87C, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();

        for (int i = 0; i < 265; i++)
        {
            ebx.BlockRead(AlliedVariables.s_V0x0056A874[i].unk000000, 0x64, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
        }

        L00524664(AlliedVariables.s_AlliedForm1Window!);
        L00524A28(AlliedVariables.s_AlliedForm1Window!);
        L0051E184();

        if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.Bop || AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
        {
            if (eax0 != TieFileVersionEnum.Bop)
            {
                Allied_SetTieFileVersion_To_0x12();
            }

            Allied_ReadTieMission_XWA_WinDebriefing();
            Allied_ReadTieMission_XWA_LostDebriefing();
            Allied_ReadTieMission_XWA_Description();
        }

        if (AlliedVariables.s_V0x00535E8C != 0)
        {
            MemoWindowImpl.TMemoForm_Proc_004C418C(AlliedVariables.s_TMemoForm_Instance!);
        }
    }

    // L005242A4
    public static void Allied_ReadTieMission_XWA_FileHeader(Form1Window Form1)
    {
        byte[] buffer = new byte[S0xTieFileHeader.Size];
        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x23EE, AlliedVariables.s_V0x00535EA4);
        AlliedVariables.s_TieFileHeader = S0xTieFileHeader.FromByteArray(buffer);
        System_L004028C4_CheckError();
        AlliedVariables.s_V0x00543D50 = 0x23EE;
        Unit_00513838_Proc_00517564();
        Unit_00513838_Proc_00520B74();
    }

    // L00520B74
    public static void Unit_00513838_Proc_00520B74()
    {
        AlliedVariables.s_Strings_IFF.Clear();
        AlliedVariables.s_Strings_IFF.Add("");
        AlliedVariables.s_Strings_IFF.Add("");

        for (int ebx = 0; ebx < 4; ebx++)
        {
            AlliedVariables.s_Strings_IFF.Add(System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.IffNames[ebx].Text, 0x14));
        }

        if (AlliedVariables.s_V0x005B6D16 != 0)
        {
            int esi = AlliedVariables.s_TShipExt_Instance!.IFFBox.SelectedIndex;

            for (int ebx = 0; ebx < 0x06; ebx++)
            {
                if (string.IsNullOrEmpty(AlliedVariables.s_Strings_IFF.GetText(ebx)))
                {
                    AlliedVariables.s_Strings_IFF.Put(ebx, AlliedVariables.s_TDatapad_T1ClassStrings.GetText(ebx));
                }
            }

            AlliedVariables.s_TShipExt_Instance!.IFFBox.SetItems(AlliedVariables.s_Strings_IFF);
            AlliedVariables.s_TShipExt_Instance!.IFFBox.SelectedIndex = esi;
        }
    }

    // L005242D8
    public static void Allied_ReadTieMission_XWA_FlightGroups(Form1Window Form1)
    {
        bool[] esp00 = new bool[0x0A];

        for (int edx = 0; edx < 0x0A; edx++)
        {
            esp00[edx] = true;
        }

        AlliedVariables.s_V0x005B6D19 = 0;

        byte[] buffer = new byte[S0xTieFlightGroup.Size];
        int edi0 = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int edi = 0; edi < edi0; edi++)
        {
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0xE3E, AlliedVariables.s_V0x00535EA4);
            S0xTieFlightGroup esp10 = S0xTieFlightGroup.FromByteArray(buffer);
            System_L004028C4_CheckError();

            S0xFGObject esi = new();
            esi.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(esp10.ToByteArray());

            if (AlliedVariables.s_UseAutoChkSetting)
            {
                esi.AutoLink = true;
            }

            for (int ecx = 0; ecx < 3; ecx++)
            {
                for (int edx = 0; edx < 4; edx++)
                {
                    esi.m00147C[ecx].M000000[edx] = esp10.StartPoints[edx].Position[ecx];
                }
            }

            for (int edx = 0; edx < 4; edx++)
            {
                esi.IsWPEnabled[edx] = esp10.StartPoints[edx].IsUsed;
            }

            esi.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(esi.FlightGroupStruct));
            esi.m001479 = 0;
            esi.m00147A = 0x01;

            AlliedVariables.s_FlightGroupObjectsList.Add(esi);

            if (esi.FlightGroupStruct.CraftId != CraftIdEnum._183_9001_1100_ResData_Backdrop)
            {
                if (esi.FlightGroupStruct.GlobalCargoIndex == 0)
                {
                    AlliedVariables.s_V0x005B6D19 = 0x01;
                }
            }

            byte al = esi.FlightGroupStruct.Team;

            if (AlliedVariables.s_V0x00543C78[al] < 0x0A)
            {
                if (esp00[al])
                {
                    AlliedVariables.s_V0x00543C78[al] = esi.FlightGroupStruct.Iff;
                    esp00[al] = false;
                }
            }
        }

        Unit_00513838_Proc_00520924();
    }

    // L00512568
    public static uint Unit_00511CD0_Proc_00512568(S0xTieFlightGroup eax0)
    {
        S0xTieFlightGroup esp08 = S0xTieFlightGroup.FromByteArray(eax0.ToByteArray());
        byte[] esp04 = new byte[4];

        for (int ecx = 0; ecx < 4; ecx++)
        {
            esp04[ecx] = 0;
        }

        esp04[esp08.StartPointRegions[0]] = 0x01;

        for (int ecx = 0; ecx < 4; ecx++)
        {
            for (int edx = 0; edx < 4; edx++)
            {
                S0xTieFlightGroupOrder eax = esp08.Orders[ecx * 4 + edx];

                if (eax.OrderId == TieOrderIdEnum._50_Hyperspace)
                {
                    byte esi = eax.Var0;
                    esp04[esi] = 0x01;
                }
            }
        }

        uint esp00 = BitConverter.ToUInt32(esp04, 0);
        return esp00;
    }

    // L00520924
    public static void Unit_00513838_Proc_00520924()
    {
        bool[] esp00 = new bool[4];

        for (int edi = 0; edi < 4; edi++)
        {
            esp00[edi] = false;
        }

        int ebp = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int edi = 0; edi < ebp; edi++)
        {
            for (int ebx = 0; ebx < 4; ebx++)
            {
                S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);

                if (eax.m001444[ebx] != 0)
                {
                    esp00[ebx] = true;
                }
            }
        }

        if (esp00[0])
        {
            AlliedVariables.s_AlliedForm1Window!.R1btn.IsEnabled = true;
        }
        else
        {
            AlliedVariables.s_AlliedForm1Window!.R1btn.IsEnabled = false;
        }

        if (esp00[1])
        {
            AlliedVariables.s_AlliedForm1Window!.R2btn.IsEnabled = true;
        }
        else
        {
            AlliedVariables.s_AlliedForm1Window!.R2btn.IsEnabled = false;
        }

        if (esp00[2])
        {
            AlliedVariables.s_AlliedForm1Window!.R3Btn.IsEnabled = true;
        }
        else
        {
            AlliedVariables.s_AlliedForm1Window!.R3Btn.IsEnabled = false;
        }

        if (esp00[3])
        {
            AlliedVariables.s_AlliedForm1Window!.R4Btn.IsEnabled = true;
        }
        else
        {
            AlliedVariables.s_AlliedForm1Window!.R4Btn.IsEnabled = false;
        }
    }

    // L00524430
    public static void Allied_ReadTieMission_XWA_RadioMessages(Form1Window Form1)
    {
        byte[] buffer = new byte[S0xTieRadioMessage.Size];
        short esi0 = AlliedVariables.s_TieFileHeader.RadioMessagesCount;

        for (int edi0 = 0; edi0 < esi0; edi0++)
        {
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0xA2, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            S0xTieRadioMessage esp00 = S0xTieRadioMessage.FromByteArray(buffer);

            S0xTieRadioMessageObject ebx = new();
            ebx.RadioMessage = S0xTieRadioMessage.FromByteArray(esp00.ToByteArray());
            AlliedVariables.s_RadioMessagesObjectsList.Add(ebx);
            ebx.M0000A6 = (byte)Unit_00513838_Proc_0051D874(edi0);
        }

        if (AlliedVariables.s_TieFileHeader.RadioMessagesCount < 0x01)
        {
            if (AlliedVariables.s_V0x005B6D15 != 0)
            {
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.MsgtextEd, "0");
            }
        }

        Unit_00513838_Proc_0051477C();
    }

    // L0051D874
    public static int Unit_00513838_Proc_0051D874(int eax0)
    {
        int ebx = 0;

        for (int esi = 0; esi < 8; esi++)
        {
            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, eax0);

            if (eax1.RadioMessage.ForTeam[esi] == 0x01)
            {
                ebx = 0x01;
            }
        }

        return ebx;
    }

    // L0051477C
    public static void Unit_00513838_Proc_0051477C()
    {
        if (AlliedVariables.s_AlliedForm1Window!.EndMsgWav.IsChecked == true)
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.SelectedIndex;
            AlliedVariables.s_AlliedForm1Window!.MsgStrList.Clear();

            for (int ebx = 0; ebx < 6; ebx++)
            {
                string ebp04 = string.Empty;

                if (ebx < 0x02)
                {
                    ebp04 = "S";
                }
                else if (ebx < 0x04)
                {
                    ebp04 = "F";
                }
                else if (ebx < 0x06)
                {
                    ebp04 = "s";
                }

                switch (ebx)
                {
                    case 0x00: // PrimarySuccessMessage1
                    case 0x02: // PrimaryFailureMessage1
                    case 0x04: // SecondarySuccessMessage1
                        ebp04 += "1: ";
                        break;

                    case 0x01: // PrimarySuccessMessage2
                    case 0x03: // PrimaryFailureMessage2
                    case 0x05: // SecondarySuccessMessage2
                        ebp04 += "2: ";
                        break;
                }

                switch (ebx)
                {
                    case 0x00:
                        ebp04 += AlliedVariables.s_V0x005B6BB4.Team.PrimarySuccessMessage1;
                        break;

                    case 0x01:
                        ebp04 += AlliedVariables.s_V0x005B6BB4.Team.PrimarySuccessMessage2;
                        break;

                    case 0x02:
                        ebp04 += AlliedVariables.s_V0x005B6BB4.Team.PrimaryFailureMessage1;
                        break;

                    case 0x03:
                        ebp04 += AlliedVariables.s_V0x005B6BB4.Team.PrimaryFailureMessage2;
                        break;

                    case 0x04:
                        ebp04 += AlliedVariables.s_V0x005B6BB4.Team.SecondarySuccessMessage1;
                        break;

                    case 0x05:
                        ebp04 += AlliedVariables.s_V0x005B6BB4.Team.SecondarySuccessMessage2;
                        break;

                }

                AlliedVariables.s_AlliedForm1Window!.MsgStrList.AddItem(ebp04);
            }

            if (esi > 0x06)
            {
                esi = 0;
            }

            AlliedVariables.s_AlliedForm1Window!.MsgStrList.SelectedIndex = esi;
            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, esi, true);
        }
        else
        {
            bool[] ebp69 = new bool[0x65];

            if (AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count > 0)
            {
                int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

                for (int ebx = 0; ebx < esi; ebx++)
                {
                    if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, ebx))
                    {
                        continue;
                    }

                    ebp69[ebx] = true;
                }
            }

            AlliedVariables.s_AlliedForm1Window!.MsgStrList.Clear();

            int eax0 = AlliedVariables.s_RadioMessagesObjectsList.Count;

            if (eax0 > 0)
            {
                for (int ebx = 0; ebx < eax0; ebx++)
                {
                    S0xTieRadioMessageObject edi = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebx);

                    string ebp04 = string.Empty;

                    if (ebx < 0x0A)
                    {
                        ebp04 = " ";
                    }

                    ebp04 += ebx.ToString(CultureInfo.InvariantCulture) + ":  ";
                    ebp04 += Unit_00513838_Proc_005157D0(System_LStrFromPCharLen(edi.RadioMessage.Message, 0x40), 0x6E);

                    if (ebx < AlliedVariables.s_V0x00543C3C.GetCount())
                    {
                        if (string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal) || string.Equals(AlliedVariables.s_V0x00543C3C.GetText(ebx).ToUpperInvariant(), "DUMMY.WAV", StringComparison.Ordinal))
                        {
                            ebp04 = "  " + ebp04;
                        }
                        else
                        {
                            ebp04 = "* " + ebp04;
                        }
                    }

                    AlliedVariables.s_AlliedForm1Window!.MsgStrList.AddItem(ebp04);
                }

                if (AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count > 0)
                {
                    int esi = AlliedVariables.s_RadioMessagesObjectsList.Count;

                    for (int ebx = 0; ebx < esi; ebx++)
                    {
                        if (!ebp69[ebx])
                        {
                            continue;
                        }

                        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, ebx, true);
                    }
                }
            }

            AlliedVariables.s_V0x00543B55 = 0;
        }
    }

    // L005244E0
    public static void Allied_ReadTieMission_XWA_GlobalGoals(Form1Window Form1)
    {
        byte[] buffer = new byte[S0xTieGlobalGoalGroup.Size];

        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x170, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            S0xTieGlobalGoalGroup esp00 = S0xTieGlobalGoalGroup.FromByteArray(buffer);
            S0xTieGlobalGoalObject eax1 = new();
            eax1.GlobalGoal = S0xTieGlobalGoalGroup.FromByteArray(esp00.ToByteArray());
            AlliedVariables.s_GlobalGoalsObjectsList.Add(eax1);
        }
    }

    // L00524540
    public static void Allied_ReadTieMission_XWA_Teams(Form1Window Form1)
    {
        byte[] buffer = new byte[S0xTieTeam.Size];

        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x1E7, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            S0xTieTeam esp00 = S0xTieTeam.FromByteArray(buffer);
            S0xTieTeamObject eax1 = new();
            eax1.Team = S0xTieTeam.FromByteArray(esp00.ToByteArray());
            AlliedVariables.s_TeamsObjectsList.Add(eax1);
        }
    }

    // L005245A0
    public static void Allied_ReadTieMission_XWA_Briefing(Form1Window Form1)
    {
        byte[] buffer = new byte[0x4414];
        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x4414, AlliedVariables.s_V0x00535EA4);
        System_L004028C4_CheckError();
        S0xTieBriefingData esp0000 = new();
        esp0000.BriefingCode = S0xTieBriefingCode.FromByteArray(buffer);
        S0xTieBriefing esp5DDC = new();
        esp5DDC.BriefingData = S0xTieBriefingData.FromByteArray(esp0000.ToByteArray());
        esp5DDC.BriefingTags = new();
        esp5DDC.BriefingStrings = new();
        Unit_00513838_Proc_0051D3D8(esp5DDC.BriefingTags, 0x80);
        Unit_00513838_Proc_0051D3D8(esp5DDC.BriefingStrings, 0x80);
        AlliedVariables.s_Allied_Briefing[0] = esp5DDC;
    }

    // L0051D3D8
    public static void Unit_00513838_Proc_0051D3D8(TStrings eax0, int edx0)
    {
        byte[] buffer = new byte[2];

        eax0.Clear();

        for (int esi = 0; esi < edx0; esi++)
        {
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
            short ebp08 = BitConverter.ToInt16(buffer, 0);

            string ebp04 = string.Empty;

            for (int ebx = 0; ebx < ebp08; ebx++)
            {
                AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00543C40);
                System_L004028C4_CheckError();
                byte ebp05 = buffer[0];

                if (ebp08 == 0x01 && ebp05 == 0)
                {
                    continue;
                }

                ebp04 += new string((char)ebp05, 1);
            }

            eax0.Add(ebp04);
        }
    }

    // L00524664
    public static void L00524664(Form1Window Form1)
    {
        byte[] buffer = new byte[0x3F];

        int ebp0C = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int ebp04 = 0; ebp04 < ebp0C; ebp04++)
        {
            S0xFGObject_000E42 ebp60C = new();

            for (int esi0 = 0; esi0 < 8; esi0++)
            {
                S0xFGObject_000E42_000000 ebx = ebp60C.M000000[esi0];
                byte ebp05;

                AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00535EA4);
                System_L004028C4_CheckError();
                ebp05 = buffer[0];

                if (ebp05 != 0)
                {
                    AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x3F, AlliedVariables.s_V0x00535EA4);
                    System_L004028C4_CheckError();
                    string ebp718 = buffer.ReadFixedLengthString(0, 0x3F);
                    string ebp690 = (new string((char)ebp05, 1) + ebp718).WithMaxLength(0x40);
                    string ebp68C = Unit_00511CD0_Proc_0051213C(ebp690);
                    ebx.M000000 = ebp68C;
                }
                else
                {
                    string ebp68C = Unit_00511CD0_Proc_0051213C(string.Empty);
                    ebx.M000000 = ebp68C;
                }

                AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00535EA4);
                System_L004028C4_CheckError();
                ebp05 = buffer[0];

                if (ebp05 != 0)
                {
                    AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x3F, AlliedVariables.s_V0x00535EA4);
                    System_L004028C4_CheckError();
                    string ebp718 = buffer.ReadFixedLengthString(0, 0x3F);
                    string ebp6D8 = (new string((char)ebp05, 1) + ebp718).WithMaxLength(0x40);
                    string ebp68C = Unit_00511CD0_Proc_0051213C(ebp6D8);
                    ebx.M000040 = ebp68C;
                }
                else
                {
                    string ebp68C = Unit_00511CD0_Proc_0051213C(string.Empty);
                    ebx.M000040 = ebp68C;
                }

                AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00535EA4);
                System_L004028C4_CheckError();
                ebp05 = buffer[0];

                if (ebp05 != 0)
                {
                    AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x3F, AlliedVariables.s_V0x00535EA4);
                    System_L004028C4_CheckError();
                    string ebp718 = buffer.ReadFixedLengthString(0, 0x3F);
                    string ebp6D8 = (new string((char)ebp05, 1) + ebp718).WithMaxLength(0x40);
                    string ebp68C = Unit_00511CD0_Proc_0051213C(ebp6D8);
                    ebx.M000080 = ebp68C;
                }
                else
                {
                    string ebp68C = Unit_00511CD0_Proc_0051213C(string.Empty);
                    ebx.M000080 = ebp68C;
                }
            }

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp04);
            eax1.m000E42 = ebp60C;
        }
    }

    // L00524A28
    public static void L00524A28(Form1Window Form1)
    {
        byte[] buffer = new byte[0x3F];

        S0xXvTGGStrings[] ebp910 = ArrayHelpers.CreateArray<S0xXvTGGStrings>(12);
        byte ebp09;

        for (int ebp04 = 0; ebp04 < 0x0A; ebp04++)
        {
            for (int ebp08 = 0; ebp08 < 0x03; ebp08++)
            {
                for (int esi = 0; esi < 0x04; esi++)
                {
                    S0xXvTGGStrings ebx = ebp910[ebp08 * 4 + esi];

                    AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00535EA4);
                    System_L004028C4_CheckError();
                    ebp09 = buffer[0];

                    if (ebp09 != 0)
                    {
                        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x3F, AlliedVariables.s_V0x00535EA4);
                        System_L004028C4_CheckError();
                        string ebpA1C = buffer.ReadFixedLengthString(0, 0x3F);
                        string ebp994 = (new string((char)ebp09, 1) + ebpA1C).WithMaxLength(0x40);
                        string ebp990 = Unit_00511CD0_Proc_0051213C(ebp994);
                        ebx.StrIncomp = ebp990;
                    }
                    else
                    {
                        string ebp990 = Unit_00511CD0_Proc_0051213C(string.Empty);
                        ebx.StrIncomp = ebp990;
                    }

                    AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00535EA4);
                    System_L004028C4_CheckError();
                    ebp09 = buffer[0];

                    if (ebp09 != 0)
                    {
                        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x3F, AlliedVariables.s_V0x00535EA4);
                        System_L004028C4_CheckError();
                        string ebpA1C = buffer.ReadFixedLengthString(0, 0x3F);
                        string ebp9DC = (new string((char)ebp09, 1) + ebpA1C).WithMaxLength(0x40);
                        string ebp990 = Unit_00511CD0_Proc_0051213C(ebp9DC);
                        ebx.StrSucc = ebp990;
                    }
                    else
                    {
                        string ebp990 = Unit_00511CD0_Proc_0051213C(string.Empty);
                        ebx.StrSucc = ebp990;
                    }

                    AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00535EA4);
                    System_L004028C4_CheckError();
                    ebp09 = buffer[0];

                    if (ebp09 != 0)
                    {
                        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x3F, AlliedVariables.s_V0x00535EA4);
                        System_L004028C4_CheckError();
                        string ebpA1C = buffer.ReadFixedLengthString(0, 0x3F);
                        string ebp990 = (new string((char)ebp09, 1) + ebpA1C).WithMaxLength(0x40);
                        ebx.StrFail = ebp990;
                    }
                    else
                    {
                        string ebp990 = Unit_00511CD0_Proc_0051213C(string.Empty);
                        ebx.StrFail = ebp990;
                    }
                }
            }

            S0xXvTGGStrObject eax1 = new();
            for (int i = 0; i < 12; i++)
            {
                eax1.GGStrings[i] = S0xXvTGGStrings.FromByteArray(ebp910[i].ToByteArray());
            }

            AlliedVariables.s_V0x00571188.Add(eax1);
        }
    }

    // L0051E184
    private static void L0051E184()
    {
        AlliedVariables.s_TieFileHandle.BlockRead(AlliedVariables.s_V0x0054AE60.unk000000, 0x49, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();

        for (int edi = 0; edi < 0x7D0; edi++)
        {
            S0x00533EA8 esi = AlliedVariables.s_V0x0054B67C[edi];

            AlliedVariables.s_TieFileHandle.BlockRead(AlliedVariables.s_V0x0054AEAC.unk000000, edi, 0x01, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();

            if (AlliedVariables.s_V0x0054AEAC.unk000000[0] == 0)
            {
                continue;
            }

            AlliedVariables.s_TieFileHandle.BlockRead(esi.unk000000, 0x3F, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
        }

        AlliedVariables.s_TieFileHandle.BlockRead(AlliedVariables.s_V0x0056A2AC.unk000000, 0x5C7, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
    }

    // L0051724C
    public static void Allied_SetTieFileVersion_To_0x12()
    {
        AlliedVariables.s_TieFileVersion = TieFileVersionEnum.XWA;
    }

    // L0051D4DC
    public static void Allied_ReadTieMission_XWA_WinDebriefing()
    {
        AlliedVariables.s_TieFileHandle.BlockRead(AlliedVariables.s_TieMission_WinDebriefing, 0x1000, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
    }

    // L0051D4FC
    public static void Allied_ReadTieMission_XWA_LostDebriefing()
    {
        AlliedVariables.s_TieFileHandle.BlockRead(AlliedVariables.s_TieMission_LostDebriefing, 0x1000, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
    }

    // L0051D51C
    public static void Allied_ReadTieMission_XWA_Description()
    {
        AlliedVariables.s_TieFileHandle.BlockRead(AlliedVariables.s_TieMission_Description, 0x1000, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
    }

    // L00518460
    public static void Unit_00513838_Proc_00518460()
    {
        if (AlliedVariables.s_V0x005B6D15 == 0)
        {
            return;
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            for (int ebx = 0; ebx < 8; ebx++)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.SeenByList, ebx, false);
            }
        }

        for (int ebx = 0; ebx < 8; ebx++)
        {
            if (AlliedVariables.s_V0x005B6B58.RadioMessage.ForTeam[ebx] != 0)
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_TDatapad_Instance!.SeenByList, ebx, true);
            }
        }

        AlliedVariables.s_TDatapad_Instance!.SeenByList.Update();
    }

    // L005122D0
    public static string Unit_00511CD0_Proc_005122D0(S0xTieTrigger eax0)
    {
        S0xTieTrigger ebp06 = S0xTieTrigger.FromByteArray(eax0.ToByteArray());
        string ebp28_6 = string.Empty;

        if (ebp06.Condition == TieConditionEnum.Always)
        {
            ebp28_6 = "(Always true)";

            if ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag) == DatapadFGPageEnum.Arrival)
            {
                if (ebp06.VariableType == TieClassEnum.FlightGroup && ebp06.Variable == 0 && ebp06.Amount == TieAmountEnum._100Percent)
                {
                    ebp28_6 += " (In Mission Craft List)";
                }
            }
        }
        else if (ebp06.Condition == TieConditionEnum.Never)
        {
            ebp28_6 = "(Never true)";
        }
        else
        {
            if (AlliedVariables.s_V0x00543BC8.GetCount() > (byte)ebp06.Amount)
            {
                ebp28_6 = AlliedVariables.s_V0x00543BC8.GetText((byte)ebp06.Amount) + " of ";
            }

            ebp28_6 += Unit_00513838_Proc_005175C0(ebp06.VariableType, ebp06.Variable);

            if (AlliedVariables.s_V0x00543BD4.GetCount() > (int)ebp06.Condition)
            {
                ebp28_6 += " must " + AlliedVariables.s_V0x00543BD4.GetText((int)ebp06.Condition);
            }

            if (ebp06.Condition < TieConditionEnum.Within)
            {
                ebp28_6 += ebp06.Parameter.ToString(CultureInfo.InvariantCulture);
            }

            if (ebp06.Condition < TieConditionEnum.Carried)
            {
                if (AlliedVariables.s_Strings_Regions.GetCount() > ebp06.Parameter)
                {
                    ebp28_6 += " " + AlliedVariables.s_Strings_Regions.GetText(ebp06.Parameter);
                }
                else
                {
                    ebp28_6 += ebp06.Parameter.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        return ebp28_6;
    }

    // L0051D53C
    public static void Unit_00513838_Proc_0051D53C(int eax0)
    {
        S0xFGObject esi0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);

        esi0.m001478 = 0x01;

        for (int edi = 0; edi < 0x08; edi++)
        {
            if (esi0.IsWPEnabled[0x04 + edi] != 0x01)
            {
                continue;
            }

            esi0.m001478 = (byte)(0x05 + edi);

            int ebx = 0x05 + edi;
            bool eax = false;

            while (ebx >= 0x02)
            {
                ebx--;

                if (ebx < 0x05)
                {
                    ebx = 0x01;
                }

                if (esi0.IsWPEnabled[ebx - 1] == 0x01)
                {
                    eax = true;
                }

                if (eax)
                {
                    break;
                }
            }

            if (eax)
            {
                esi0.m00147C[3].SetValue(4 + edi, Form1WindowImpl.TForm1_Proc_0052AE38(AlliedVariables.s_AlliedForm1Window!, esi0, (byte)(0x05 + edi), (byte)ebx));
            }
        }

        esi0.m00147C[3].M000000[3] = Form1WindowImpl.TForm1_Proc_0052AE38(AlliedVariables.s_AlliedForm1Window!, esi0, 0x04, esi0.m001478);

        Unit_00513838_Proc_00520528(eax0);
    }

    // L00520528
    public static void Unit_00513838_Proc_00520528(int eax0)
    {
        S0xFGObject ebx = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);

        double esp00;
        double esp10;
        double esp08;

        if (ebx.IsWPEnabled[4] == 0x01)
        {
            if (Unit_00513838_Proc_005208F8(eax0) || ebx.FlightGroupStruct.PlayerNumber > 0)
            {
                esp00 = ebx.m00147C[2].M000008[0] - ebx.m00147C[2].M000000[0];
                esp10 = ebx.m00147C[1].M000008[0] - ebx.m00147C[1].M000000[0];
                esp08 = ebx.m00147C[0].M000008[0] - ebx.m00147C[0].M000000[0];
            }
            else if (ebx.IsWPEnabled[4] == 0x01)
            {
                esp00 = ebx.m00147C[2].M000008[0] - AlliedVariables.s_V0x00543D30[2];
                esp10 = ebx.m00147C[1].M000008[0] - AlliedVariables.s_V0x00543D30[1];
                esp08 = ebx.m00147C[0].M000008[0] - AlliedVariables.s_V0x00543D30[0];
            }
            else
            {
                esp00 = -ebx.m00147C[2].M000000[0];
                esp10 = -ebx.m00147C[1].M000000[0];
                esp08 = -ebx.m00147C[0].M000000[0];
            }

            if (esp08 == 0.0f && esp10 == 0.0f)
            {
                if (esp00 == 0.0f)
                {
                    esp08 = 1.0;
                    esp10 = -1.0;
                    esp00 = 1.0;
                }
                else
                {
                    esp08 = 1.0;
                    esp10 = -1.0;
                }
            }
        }
        else
        {
            esp00 = 0;
            esp10 = 0;
            esp08 = 0;

            ebx.m001448 = 1.0;
            ebx.m001450 = 0;

            if (ebx.FlightGroupStruct.Yaw == 0x40)
            {
                ebx.m001458 = 1.0;
                ebx.m001460 = 0;
            }
            else if (ebx.FlightGroupStruct.Yaw == 0x80)
            {
                ebx.m001458 = 0;
                ebx.m001460 = 1.0;
            }
            else if (ebx.FlightGroupStruct.Yaw == 0xC0)
            {
                ebx.m001458 = -1.0;
                ebx.m001460 = 0;
            }
            else
            {
                ebx.m001458 = 0;
                ebx.m001460 = -1.0;
            }
        }

        double esp18a = Math.Sqrt(esp08 * esp08 + esp10 * esp10 + esp00 * esp00);

        if (esp18a != 0.0f)
        {
            double esp20 = Math.Sqrt(esp08 * esp08 + esp10 * esp10);
            ebx.m001448 = esp20 / esp18a;
            ebx.m001450 = esp00 / esp18a;
        }

        double esp18b = Math.Sqrt(esp08 * esp08 + esp10 * esp10);

        if (esp18b != 0.0f)
        {
            ebx.m001458 = esp08 / esp18b;
            ebx.m001460 = esp10 / esp18b;
        }

        double esp28;

        if (ebx.FlightGroupStruct.Roll > 0)
        {
            esp28 = (256.0f - ebx.FlightGroupStruct.Roll / 128.0f) * 3.1415926535897931;
        }
        else
        {
            esp28 = 0;
        }

        ebx.m001470 = Math.Cos(esp28);
        ebx.m001468 = Math.Sin(esp28);
    }

    // L005175C0
    public static string Unit_00513838_Proc_005175C0(TieClassEnum eax0, byte edx0)
    {
        string ebp30_9;

        switch (eax0)
        {
            case TieClassEnum.None:
                ebp30_9 = string.Empty;
                break;

            case TieClassEnum.FlightGroup:
                if (AlliedVariables.s_FlightGroupObjectsList.Count > edx0)
                {
                    ebp30_9 = AlliedVariables.s_V0x00543BC0.GetText(edx0);
                }
                else
                {
                    ebp30_9 = "FG#" + edx0.ToString(CultureInfo.InvariantCulture);
                }

                break;

            case TieClassEnum.ShipType:
                ebp30_9 = AlliedVariables.s_Strings_Ships.GetText((int)AlliedConvertCraftIdToShipSeq((CraftIdEnum)edx0)) + "s";
                break;

            case TieClassEnum.ShipCategory:
                ebp30_9 = AlliedVariables.s_Strings_ShipCats.GetText(edx0);
                break;

            case TieClassEnum.ObjectCategory:
                ebp30_9 = AlliedVariables.s_Strings_ObjCats.GetText(edx0);
                break;

            case TieClassEnum.Iff:
                if (edx0 < 0x02)
                {
                    ebp30_9 = AlliedVariables.s_Strings_IFF.GetText(edx0) + "s";
                }
                else
                {
                    string ebp30_8 = System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.IffNames[edx0 - 0x02].Text, 0x14);

                    if (string.IsNullOrEmpty(ebp30_8))
                    {
                        ebp30_9 = AlliedVariables.s_Strings_IFF.GetText(edx0) + "s";
                    }
                    else
                    {
                        ebp30_9 = ebp30_8 + "s";
                    }
                }

                break;

            case TieClassEnum.CraftWithOrder:
                ebp30_9 = "Craft order: " + AlliedVariables.s_Strings_Orders.GetText(edx0);
                break;

            case TieClassEnum.CraftWhen:
                ebp30_9 = AlliedVariables.s_Strings_When.GetText(edx0);
                break;

            case TieClassEnum.GlobalGroup:
                {
                    string ebp30_8 = System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Sets[edx0].Name, 0x57);

                    if (string.IsNullOrEmpty(ebp30_8))
                    {
                        ebp30_9 = "GGroup " + edx0.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        ebp30_9 = string.Format(CultureInfo.InvariantCulture, "GG{0}: {1}", edx0, ebp30_8);
                    }

                    break;
                }

            case TieClassEnum.Team:
                ebp30_9 = string.Empty;

                if (edx0 < 0x0A)
                {
                    ebp30_9 = AlliedVariables.s_Strings_Teams.GetText(edx0);
                }

                if (ebp30_9.Length > 0x02)
                {
                    System_LStrDelete(ref ebp30_9, 0x01, 0x02);
                }

                ebp30_9 = "Team " + ebp30_9;
                break;

            case TieClassEnum.PlayerOfGlobalGroup:
                ebp30_9 = "Player of GG " + edx0.ToString(CultureInfo.InvariantCulture);
                break;

            case TieClassEnum.NotTeam:
                ebp30_9 = string.Empty;

                if (edx0 < 0x0A)
                {
                    ebp30_9 = AlliedVariables.s_Strings_Teams.GetText(edx0);
                }

                if (ebp30_9.Length > 0x02)
                {
                    System_LStrDelete(ref ebp30_9, 0x01, 0x02);
                }

                ebp30_9 = "any Team but " + ebp30_9;
                break;

            case TieClassEnum.GlobalUnit:
                ebp30_9 = "GUnit " + edx0.ToString(CultureInfo.InvariantCulture);
                break;

            case TieClassEnum.RadioMessage:
                ebp30_9 = "Message #" + edx0.ToString(CultureInfo.InvariantCulture);
                break;

            default:
                ebp30_9 = ((byte)eax0).ToString(CultureInfo.InvariantCulture) + "? " + edx0.ToString(CultureInfo.InvariantCulture);
                break;
        }

        return ebp30_9;

        // sprint_int_L004091CC( edx0, ecx0 );
        // System_DoneExcept();
    }

    // L0051F42C
    public static void Unit_00513838_Proc_0051F42C()
    {
        AlliedVariables.s_V0x005B6BB8 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, AlliedVariables.s_V0x00543B18);
        AlliedVariables.s_V0x005B6BBC = Classes_TList_Get(AlliedVariables.s_V0x00571188, AlliedVariables.s_V0x00543B18);

        if (AlliedVariables.s_AlliedForm1Window!.PaletOn.IsChecked == true)
        {
            MapWindowImpl.TMapForm__PROC_004F7D90(AlliedVariables.s_TMapForm_Instance!, AlliedVariables.s_V0x00543B18);
        }

        Unit_00513838_Proc_00515BE0(AlliedVariables.s_V0x00543B18);
        DatapadWindowImpl.TDatapad_GGRadiosClick(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_TDatapad_Instance!.GGRadios);
        DatapadWindowImpl.TDatapad_SetTitle(AlliedVariables.s_TDatapad_Instance!);
    }

    // L00517AC0
    public static void Unit_00513838_Proc_00517AC0(ComboBox eax0, TieClassEnum edx0, byte ecx0)
    {
        switch (edx0)
        {
            case TieClassEnum.None:
                eax0.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                break;

            case TieClassEnum.FlightGroup:
                eax0.SetItems(AlliedVariables.s_V0x00543BC0);
                break;

            case TieClassEnum.ShipType:
                eax0.SetItems(AlliedVariables.s_Strings_Ships);
                break;

            case TieClassEnum.ShipCategory:
                eax0.SetItems(AlliedVariables.s_Strings_ShipCats);
                break;

            case TieClassEnum.ObjectCategory:
                eax0.SetItems(AlliedVariables.s_Strings_ObjCats);
                break;

            case TieClassEnum.Iff:
                eax0.SetItems(AlliedVariables.s_Strings_IFF);
                break;

            case TieClassEnum.CraftWithOrder:
                eax0.SetItems(AlliedVariables.s_Strings_Orders);
                break;

            case TieClassEnum.CraftWhen:
                eax0.SetItems(AlliedVariables.s_Strings_When);
                break;

            case TieClassEnum.Team:
            case TieClassEnum.NotTeam:
                eax0.SetItems(AlliedVariables.s_Strings_Teams);
                break;

            case TieClassEnum.GlobalGroup:
            case TieClassEnum.PlayerOfGlobalGroup:
                eax0.Clear();

                for (int ebp08 = 0; ebp08 < 0x48; ebp08++)
                {
                    S0xTieHeaderSet ebp0C = AlliedVariables.s_TieFileHeader.Header.Sets[ebp08];
                    // (char*)ebp0C, 0x57
                    string ebp28_1 = ebp0C.Name;
                    string ebp10 = string.Format(CultureInfo.InvariantCulture, "{0}: {1}", ebp08, ebp28_1);
                    eax0.AddItem(ebp10);
                }

                break;

            default:
                eax0.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                eax0.PutItem(0, "0");
                break;
        }

        if (edx0 == TieClassEnum.ShipType)
        {
            eax0.SelectedIndex = (int)AlliedConvertCraftIdToShipSeq((CraftIdEnum)ecx0);
        }
        else
        {
            eax0.SelectedIndex = ecx0;
        }

        eax0.Update();
    }

    // L0051F09C
    public static void Unit_00513838_Proc_0051F09C(int eax0, int edx0)
    {
        if (AlliedVariables.s_V0x00543990 != 0)
        {
            AlliedVariables.s_V0x00543C9A = 0;

            switch (AlliedVariables.s_TDatapad_Instance!.GGRadios.GetItemIndex())
            {
                case 0x00:
                    Graphics_TFont_SetColor(AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp, 0x0000FFFF);
                    Graphics_TFont_SetColor(AlliedVariables.s_TCondToolForm_Instance!.FGStrSucc, 0x0000FF00);
                    Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.Label63, "Success");
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp, true);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label62, true);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.FGStrFail, true);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label73, true);
                    break;

                case 0x01:
                    Graphics_TFont_SetColor(AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp, 0x00FF9000);
                    Graphics_TFont_SetColor(AlliedVariables.s_TCondToolForm_Instance!.FGStrSucc, 0x000000FF);
                    Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.Label63, "        Fail");
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp, true);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label62, true);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.FGStrFail, false);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label73, false);
                    break;

                case 0x02:
                    Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.Label63, "Success");
                    Graphics_TFont_SetColor(AlliedVariables.s_TCondToolForm_Instance!.FGStrSucc, 0x0000FF00);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp, false);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label62, false);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.FGStrFail, false);
                    Controls_TControl_SetVisible(AlliedVariables.s_TCondToolForm_Instance!.Label73, false);
                    break;
            }

            Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp, AlliedVariables.s_V0x005B6BBC.GGStrings[eax0 * 4 + (edx0 - 1)].StrIncomp);
            Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.FGStrSucc, AlliedVariables.s_V0x005B6BBC.GGStrings[eax0 * 4 + (edx0 - 1)].StrSucc);
            Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.FGStrFail, AlliedVariables.s_V0x005B6BBC.GGStrings[eax0 * 4 + (edx0 - 1)].StrFail);

            AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp.Update();
            AlliedVariables.s_TCondToolForm_Instance!.FGStrFail.Update();
            AlliedVariables.s_TCondToolForm_Instance!.Label73.Update();
            AlliedVariables.s_TCondToolForm_Instance!.Label63.Update();
            AlliedVariables.s_TCondToolForm_Instance!.Label62.Update();
        }
    }

    // L005146A4
    public static void Unit_00513838_Proc_005146A4()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        AlliedVariables.s_V0x00543B53 = true;
        AlliedVariables.s_AlliedForm1Window!.SaveBtn.IsEnabled = true;
    }

    // L0051467C
    public static void Unit_00513838_Proc_0051467C()
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        AlliedVariables.s_V0x00543B54 = 0x01;
        AlliedVariables.s_AlliedForm1Window!.SaveBtn.IsEnabled = true;
    }

    // L00511EB8
    public static string Unit_00511CD0_Proc_00511EB8(string eax0)
    {
        return eax0.WithMaxLength(20);
    }

    // L0051C034
    public static int Integer_Negate_L0051C034(int eax0)
    {
        return 0 - eax0;
    }

    // L0051E43C
    public static void Unit_00513838_Proc_0051E43C(bool eax0, int edx0)
    {
        if ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag) == DatapadFGPageEnum.OrderPage)
        {
            DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_CurrentOrderInRegion);
            Unit_00513838_Proc_00516414(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].OrderId);
        }

        if (AlliedVariables.s_V0x005B6D18 != 0)
        {
            WaypointsWindowImpl.TWPform_Proc_0050BEF0(AlliedVariables.s_TWPform_Instance!);

            for (int esi = 0; esi < 0x08; esi++)
            {
                StdCtrls_TCustomListBox_SetSelected(
                    AlliedVariables.s_TWPform_Instance!.OrderWPEnabledList,
                    esi,
                    AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[esi].IsUsed != 0
                    );
            }

            AlliedVariables.s_TWPform_Instance!.OrderWPEnabledList.Update();
        }

        AlliedVariables.s_V0x005B704C = 0;
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        MapWindowImpl.TMapForm_Proc_004F6B48(AlliedVariables.s_TMapForm_Instance!);
        OrderSelWindowImpl.TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);
        AlliedVariables.s_V0x005B704C = 0x01;

        if (eax0)
        {
            Form1WindowImpl.TForm1_FitBattleBtnClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.FitBattleBtn);
        }
    }

    // L0051E9D4
    public static int AlliedGetControlTag(object? eax0)
    {
        if (eax0 is not FrameworkElement element)
        {
            throw new InvalidOperationException();
        }

        return Convert.ToInt32(element.Tag);
    }

    // L00516414
    public static void Unit_00513838_Proc_00516414(TieOrderIdEnum eax0)
    {
        int ebp04 = Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.OrderP1);
        int ebp08 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var1;

        switch (eax0)
        {
            case TieOrderIdEnum._02_Form:
            case TieOrderIdEnum._03_FormEvade:
            case TieOrderIdEnum._21_StarshipForm:
            case TieOrderIdEnum._52_TransferCargo:
            case TieOrderIdEnum._59_StartOver:
            case TieOrderIdEnum._63_FollowTarget:
            case TieOrderIdEnum._64_Homing:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "# of loops:");
                break;

            case TieOrderIdEnum._04_RendezVous1:
            case TieOrderIdEnum._06_WaitForBoard:
                /* 0x00516B5C =  */
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "# of dockings:");
                break;

            case TieOrderIdEnum._07_CapFree:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, Allied_OptMeshType_ToString((MeshTypeEnum)ebp04));
                break;

            case TieOrderIdEnum._12_BoardToGive:
            case TieOrderIdEnum._13_BoardToTake:
            case TieOrderIdEnum._14_BoardToExchange:
            case TieOrderIdEnum._15_BoardToCapture:
            case TieOrderIdEnum._16_BoardToDestroy:
            case TieOrderIdEnum._17_BoardToPickup:
            case TieOrderIdEnum._31_BoardToContact:
            case TieOrderIdEnum._32_BoardToRepair:
            case TieOrderIdEnum._58_Stationary:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, string.Format(CultureInfo.InvariantCulture, "Dock time ({0}):", Allied_TimeInSeconds_ToMinutesSecondsString(Allied_Time_ToSeconds_L0051F534(ebp04))));
                break;

            case TieOrderIdEnum._18_DropOff:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, string.Format(CultureInfo.InvariantCulture, "Deploy time ({0}):", Allied_TimeInSeconds_ToMinutesSecondsString(Allied_Time_ToSeconds_L0051F534(ebp04))));
                break;

            case TieOrderIdEnum._19_Wait:
            case TieOrderIdEnum._20_Wait:
            case TieOrderIdEnum._57_Park:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, string.Format(CultureInfo.InvariantCulture, "Wait time ({0}):", Allied_TimeInSeconds_ToMinutesSecondsString(Allied_Time_ToSeconds_L0051F534(ebp04))));
                break;

            case TieOrderIdEnum._36_SelfDestroy:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, string.Format(CultureInfo.InvariantCulture, "Delay time ({0}):", Allied_TimeInSeconds_ToMinutesSecondsString(Allied_Time_ToSeconds_L0051F534(ebp04))));
                break;

            case TieOrderIdEnum._43_Backup:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "Objects:");
                break;

            case TieOrderIdEnum._44_Disabled:
                if (ebp04 == 0)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "Missiles:No");
                }
                else
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "Missiles:Yes");
                }

                break;

            case TieOrderIdEnum._45_RepairOneself:
            case TieOrderIdEnum._61_WorkOn:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, string.Format(CultureInfo.InvariantCulture, "Time ({0}):", Allied_TimeInSeconds_ToMinutesSecondsString(Allied_Time_ToSeconds_L0051F534(ebp04))));
                break;

            case TieOrderIdEnum._46_ChangeSides:
            case TieOrderIdEnum._47_SelfCapture:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "IFF:");
                break;

            case TieOrderIdEnum._48_Stationary:
                if (AlliedVariables.s_Strings_Ships.GetCount() > ebp04)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, AlliedVariables.s_Strings_Ships.GetText(ebp04));
                }
                else
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "None");
                }

                break;

            case TieOrderIdEnum._50_Hyperspace:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "Region #" + ebp04.ToString(CultureInfo.InvariantCulture));
                break;

            case TieOrderIdEnum._51_Stationary:
                if (AlliedVariables.s_Strings_Missiles.GetCount() > ebp04)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, AlliedVariables.s_Strings_Missiles.GetText(ebp04));
                }
                else
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "None");
                }

                break;

            case TieOrderIdEnum._56_Stationary:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "On FG:");
                break;

            case TieOrderIdEnum._60_Stationary:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "From FG:");
                break;

            default:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P1Lab, "No Effect?:");
                break;
        }

        switch (eax0)
        {
            case TieOrderIdEnum._10_Escort:
                if (ebp08 == 0)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Attack player:No");
                }
                else
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Attack player:Yes");
                }

                break;

            case TieOrderIdEnum._18_DropOff:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "FG# to drop off:");
                break;

            case TieOrderIdEnum._46_ChangeSides:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Team #:");
                break;

            case TieOrderIdEnum._50_Hyperspace:
                if (ebp08 == 0)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Wait: Yes");
                }
                else
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Wait: No");
                }

                break;

            case TieOrderIdEnum._56_Stationary:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "From GG:");
                break;

            case TieOrderIdEnum._57_Park:
                if (ebp08 == 0)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Outside");
                }
                else
                {
                    /* 0x00516D4C =  */
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Inside");
                }

                break;

            case TieOrderIdEnum._61_WorkOn:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, Allied_OptMeshType_ToString((MeshTypeEnum)Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.OrderP2)));
                break;

            case TieOrderIdEnum._64_Homing:
                if (ebp08 == 0)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Clockwise");
                }
                else
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "Counter Clockwise");
                }

                break;

            default:
                if (eax0 < TieOrderIdEnum._33_Stationary)
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "# of dockings:");
                }
                else
                {
                    Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P2Lab, "No Effect?:");
                }

                break;
        }

        switch (eax0)
        {
            case TieOrderIdEnum._50_Hyperspace:
            case TieOrderIdEnum._61_WorkOn:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P3Lab, "# of loops:");
                break;

            case TieOrderIdEnum._56_Stationary:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P3Lab, "# of dockings:");
                break;

            default:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.P3Lab, "No Effect?:");
                break;
        }

        Controls_TControl_SetLeft(AlliedVariables.s_TDatapad_Instance!.P1Lab, 0x64 - (int)AlliedVariables.s_TDatapad_Instance!.P1Lab.ActualWidth);
        AlliedVariables.s_TDatapad_Instance!.P1Lab.Update();
        Controls_TControl_SetLeft(AlliedVariables.s_TDatapad_Instance!.P2Lab, 0xFF - (int)AlliedVariables.s_TDatapad_Instance!.P2Lab.ActualWidth);
        AlliedVariables.s_TDatapad_Instance!.P2Lab.Update();
        Controls_TControl_SetLeft(AlliedVariables.s_TDatapad_Instance!.P3Lab, 0x199 - (int)AlliedVariables.s_TDatapad_Instance!.P3Lab.ActualWidth);
        AlliedVariables.s_TDatapad_Instance!.P3Lab.Update();

        if (AlliedVariables.s_FlightGroupObjectsList.Count - 1 < AlliedVariables.s_V0x00543B0C)
        {
            AlliedVariables.s_V0x00543B0C = 0;
        }

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

    // L00515F1C
    public static string Allied_OptMeshType_ToString(MeshTypeEnum eax0)
    {
        return eax0 switch
        {
            MeshTypeEnum.Default => "Default",
            MeshTypeEnum.MainHull => "MainHull",
            MeshTypeEnum.Wing => "Wing",
            MeshTypeEnum.Fuselage => "Furesultelage",
            MeshTypeEnum.GunTurret => "GunTurret",
            MeshTypeEnum.SmallGun => "resultmallgun",
            MeshTypeEnum.Engine => "Engine",
            MeshTypeEnum.Bridge => "Bridge",
            MeshTypeEnum.ShieldGen => "resulthieldGen",
            MeshTypeEnum.EnergyGen => "EnergyGen",
            MeshTypeEnum.Launcher => "Launcher",
            MeshTypeEnum.CommSys => "Commresulty",
            MeshTypeEnum.BeamSys => "Beamresulty",
            MeshTypeEnum.CommandSys => "CommandBeam",
            MeshTypeEnum.DockingPlat => "DockingPlat",
            MeshTypeEnum.LandingPlat => "LandingPlat",
            MeshTypeEnum.Hangar => "Hangar",
            MeshTypeEnum.CargoPod => "CargoPod",
            MeshTypeEnum.MiscHull => "Miresultchull",
            MeshTypeEnum.Antenna => "Antenna",
            MeshTypeEnum.RotaryWing => "RotWing",
            MeshTypeEnum.RotaryGunTurret => "RotGunTurret",
            MeshTypeEnum.RotaryLauncher => "RotLauncher",
            MeshTypeEnum.RotaryCommSys => "RotCommresulty",
            MeshTypeEnum.RotaryBeamSys => "RotBeamresulty",
            MeshTypeEnum.RotaryCommandSys => "RotCommandBeam",
            MeshTypeEnum.Hatch => "Hatch",
            MeshTypeEnum.Custom => "Curesulttom2",
            MeshTypeEnum.WeaponSys1 => "Curesulttom3",
            MeshTypeEnum.WeaponSys2 => "Curesulttom4",
            MeshTypeEnum.PowerReg => "PowerReg",
            MeshTypeEnum.Reactor => "Reactor",
            _ => " ",
        };
    }

    // L0051CA50
    public static void Unit_00513838_Proc_0051CA50()
    {
        TFileRec ebp14C = new();
        ebp14C.Assign(AlliedVariables.s_AlliedDirectoryPath + "\\Orders.clp");
        ebp14C.OpenFileForWrite(0x94);
        System_L004028C4_CheckError();

        int ebx = AlliedVariables.s_V0x00543CFC.Count;

        for (int esi = 0; esi < ebx; esi++)
        {
            S0xOrdObject eax0 = Classes_TList_Get(AlliedVariables.s_V0x00543CFC, esi);
            ebp14C.WriteRec(eax0.m000004.ToByteArray());
            System_L004028C4_CheckError();
        }

        ebp14C.Close();
        System_L004028C4_CheckError();
    }

    // L0051CB44
    public static void Unit_00513838_Proc_0051CB44()
    {
        TFileRec ebp154 = new();
        ebp154.Assign(AlliedVariables.s_AlliedDirectoryPath + "\\Conds.clp");
        ebp154.OpenFileForWrite(0x06);
        System_L004028C4_CheckError();

        int ebx = AlliedVariables.s_V0x00543CF8.Count;

        for (int esi = 0; esi < ebx; esi++)
        {
            S0xCondObjectStruct eax0 = Classes_TList_Get(AlliedVariables.s_V0x00543CF8, esi);
            ebp154.WriteRec(eax0.m000004.ToByteArray());
            System_L004028C4_CheckError();
        }

        ebp154.Close();
        System_L004028C4_CheckError();
    }

    // L0051E614
    public static void Unit_00513838_Proc_0051E614(int eax0, int edx0)
    {
        if (eax0 < 0 || eax0 >= 0x20)
        {
            throw new ArgumentOutOfRangeException(nameof(eax0));
        }

        int ebp04 = edx0;

        if (eax0 == 0x12)
        {
            ebp04--;
        }

        // upgrade
        if (ebp04 < 0)
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
            byte[] flightgroup = eax1.FlightGroupStruct.ToByteArray();
            flightgroup[0x0068 + eax0] = (byte)ebp04;
            eax1.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(flightgroup);

            switch (eax0)
            {
                case 0x03:
                    {
                        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                        if (!eax2.AutoLink)
                        {
                            break;
                        }

                        if (ebp04 <= 0xE7 && BtBitString(ebp04, AlliedVariables.s_V0x0051E880))
                        {
                            L00517050(0, ebx);
                        }
                        else if (ebp04 < 0xE7 && BtBitString(ebp04, AlliedVariables.s_V0x0051E8A0))
                        {
                            L00517050(0x01, ebx);
                        }

                        break;
                    }

                case 0x04:
                    {
                        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                        if (eax2.FlightGroupStruct.SpecialCraft >= eax3.FlightGroupStruct.CraftsCount - 1)
                        {
                            S0xFGObject eax4 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                            S0xFGObject eax5 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                            eax5.FlightGroupStruct.SpecialCraft = eax4.FlightGroupStruct.CraftsCount;
                        }

                        if (AlliedVariables.s_V0x005B6D16 == 0)
                        {
                            break;
                        }

                        if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.SpecialCraft >= AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftsCount)
                        {
                            AlliedVariables.s_TShipExt_Instance!.SpecShpSpin.SelectedIndex = Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.FGSizeSpin);
                        }

                        break;
                    }

                case 0x09:
                    {
                        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                        if (!eax2.AutoLink)
                        {
                            break;
                        }

                        eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax2.FlightGroupStruct.Iff = AlliedVariables.s_V0x00543C78[ebp04];

                        if (AlliedVariables.s_V0x005B6D16 == 0)
                        {
                            break;
                        }

                        AlliedVariables.s_TShipExt_Instance!.IFFBox.SelectedIndex = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Iff;
                        break;
                    }
            }
        }

        Unit_00513838_Proc_0051467C();

        if (eax0 <= 0x1F && BtBitString(eax0, AlliedVariables.s_V0x0051E8C0))
        {
            Unit_00513838_Proc_00517258();
        }

        switch (eax0)
        {
            case 0x03:
            case 0x04:
            case 0x1F:
                DatapadWindowImpl.TDatapad__PROC_004C05F0(AlliedVariables.s_TDatapad_Instance!);
                break;
        }

        if (eax0 == 0x15)
        {
            DatapadWindowImpl.L004BF830(AlliedVariables.s_TDatapad_Instance!);
        }

        if (eax0 == 0x03)
        {
            ShipExtUserControlImpl.Unit_00513838_Proc_00517308();
            ShipExtUserControlImpl.Unit_00513838_Proc_0051DDA8();
        }
    }

    // L0051DBC8
    public static void Unit_00513838_Proc_0051DBC8(int eax0, int edx0)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            Unit_00513838_Proc_0051467C();

            int eax1 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            if (eax1 > 0)
            {
                AlliedVariables.s_V0x00543CC0 = 0;
                for (int ebp10 = 0; ebp10 < eax1; ebp10++, AlliedVariables.s_V0x00543CC0++)
                {
                    if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
                    {
                        continue;
                    }

                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                    byte ebp0C = eax2.FlightGroupStruct.StartPointRegions[eax0 - 1];
                    eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                    eax2.FlightGroupStruct.StartPointRegions[eax0 - 1] = (byte)edx0;
                    eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                    uint ebx2 = Unit_00511CD0_Proc_00512568(eax2.FlightGroupStruct);
                    eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                    eax2.m001444 = BitConverter.GetBytes(ebx2);

                    if (AlliedVariables.s_LockOrdToRegOptionSetting)
                    {
                        for (int ebx = 0; ebx < 0x04; ebx++)
                        {
                            eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);

                            byte[] ebpA4 = eax2.FlightGroupStruct.Orders[ebp0C * 4 + ebx].ToByteArray();
                            eax2.FlightGroupStruct.Orders[ebp0C * 4 + ebx] = S0xTieFlightGroupOrder.FromByteArray(eax2.FlightGroupStruct.Orders[edx0 * 4 + ebx].ToByteArray());
                            eax2.FlightGroupStruct.Orders[edx0 * 4 + ebx] = S0xTieFlightGroupOrder.FromByteArray(ebpA4);

                            OrderSelWindowImpl.TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);
                        }
                    }
                }
            }

            MapWindowImpl.TMapForm_Proc_004F6B48(AlliedVariables.s_TMapForm_Instance!);
        }

        Unit_00513838_Proc_00520924();
    }

    // L0051443C
    public static int Unit_00513838_Proc_0051443C(string eax0)
    {
        int ebp08 = 0;

        if (string.IsNullOrEmpty(eax0))
        {
            return ebp08;
        }

        if (string.Equals(eax0, "-", StringComparison.Ordinal))
        {
            return ebp08;
        }

        double ebp10 = double.Parse(eax0, CultureInfo.InvariantCulture);
        ebp10 = ebp10 * 160.0f;
        ebp08 = (int)Math.Round(ebp10);

        return ebp08;
    }

    // L0051B2B0
    public static void Allied_WriteTieMission(string eax0, byte edx0, byte ecx0)
    {
        if (AlliedVariables.s_V0x00535E8C != 0)
        {
            MemoWindowImpl.TMemoForm_Proc_004C4328(AlliedVariables.s_TMemoForm_Instance!);
        }

        AlliedVariables.s_TieFileHandle.Assign(eax0);
        AlliedVariables.s_TieFileHandle.OpenFileForWrite(0x01);
        System_L004028C4_CheckError();

        AlliedVariables.s_V0x00543C70 = 0;
        AlliedVariables.s_V0x00543C74 = 0;

        AlliedVariables.s_TieFileHandle.BlockWrite(BitConverter.GetBytes((short)AlliedVariables.s_TieFileVersion), AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        Allied_WriteTieMission_Header(AlliedVariables.s_AlliedForm1Window!);
        Allied_WriteTieMission_FlightGroups(AlliedVariables.s_AlliedForm1Window!);
        Allied_WriteTieMission_RadioMessages(AlliedVariables.s_AlliedForm1Window!);
        Allied_WriteTieMission_GlobalGoals(AlliedVariables.s_AlliedForm1Window!);
        Allied_WriteTieMission_Teams(AlliedVariables.s_AlliedForm1Window!);
        Allied_WriteTieMission_Briefing(AlliedVariables.s_AlliedForm1Window!);

        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00546144.unk000000, 0x4432, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FF8.m000000, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FFC.unk000000, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        if (AlliedVariables.s_V0x00570FF8.m000000[0] == 0x01)
        {
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00571000.m000000, 0x01, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }

        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00571004.unk000000, 0xEA, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FF8.m000000, 0x01, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FFC.unk000000, 0x01, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        if (AlliedVariables.s_V0x00570FF8.m000000[1] == 0x01)
        {
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00571000.m000000, 0x01, 0x01, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }

        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x005710F0.unk000000, 0x2C, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FF8.m000000, 0x02, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FFC.unk000000, 0x02, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        if (AlliedVariables.s_V0x00570FF8.m000000[2] == 0x01)
        {
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00571000.m000000, 0x02, 0x01, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }

        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x0057111C.unk000000, 0x58, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FF8.m000000, 0x03, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00570FFC.unk000000, 0x03, 0x01, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        if (AlliedVariables.s_V0x00570FF8.m000000[3] == 0x01)
        {
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x00571000.m000000, 0x03, 0x01, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }

        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x0054A578.unk000000, 0x6C, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_Notes, 0x1000, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x0054A5E4.unk000000, 0x87C, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        //int edx1 = System_LStrLen_L00404DE0(ebp18[3]);

        //for (int eax1 = 0; eax1 < edx1; eax1++)
        //{
        //    char* ecx = (char*)ebp18[3];
        //    char cl = ecx[eax1];

        //    AlliedVariables.s_V0x0056A874.m00490B.m00005A[eax1] = cl;
        //}

        for (int i = 0; i < AlliedVariables.s_V0x0056A874.Length; i++)
        {
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x0056A874[i].unk000000, 0x64, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }

        L0052523C(AlliedVariables.s_AlliedForm1Window!);
        L00525424(AlliedVariables.s_AlliedForm1Window!);
        L0051E218();

        if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XvT)
        {
            Form1WindowImpl.L0052560C(AlliedVariables.s_AlliedForm1Window!);
        }
        else if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.Bop || AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
        {
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_WinDebriefing, 0x1000, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_LostDebriefing, 0x1000, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_Description, 0x1000, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }

        if (edx0 != 0)
        {
            string ebp18_1 = Path.GetFileName(eax0);
            string ebp18_0 = AlliedVariables.s_Allied_Briefing[0].BriefingStrings.GetText(0);
            string ebp18_2 = " " + ProductVersionHelpers.GetNameAndVersion() + " - (" + ebp18_1 + ") - " + ebp18_0;
            Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!, ebp18_2);
        }

        AlliedVariables.s_TieFileHandle.Close();
        System_L004028C4_CheckError();

        if (ecx0 == 0)
        {
            if (edx0 != 0)
            {
                AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
            }

            AlliedVariables.s_V0x00543B50 = 0x01;
            L005146CC();
            AlliedVariables.s_V0x00543B54 = 0;
            AlliedVariables.s_V0x00543B55 = 0;
        }

        Form1WindowImpl.Unit_00513838_Proc_00518694();

        if (edx0 != 0 && AlliedVariables.s_AlliedForm1Window!.ErrCheckOn1.IsChecked)
        {
            ErrorBoxImpl.TErrForm_L00510F18(AlliedVariables.s_TErrForm_Instance!, false);
        }
    }

    // L00524DFC
    private static void Allied_WriteTieMission_Header(Form1Window eax0)
    {
        AlliedVariables.s_TieFileHeader.FlightGroupsCount = (short)AlliedVariables.s_FlightGroupObjectsList.Count;
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieFileHeader.ToByteArray(), AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
    }

    // L00524E2C
    private static void Allied_WriteTieMission_FlightGroups(Form1Window eax0)
    {
        int esp00 = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int edi = 0; edi < esp00; edi++)
        {

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);
            S0xTieFlightGroup esp08 = S0xTieFlightGroup.FromByteArray(eax1.FlightGroupStruct.ToByteArray());

            if (Unit_00513838_Proc_005208F8(edi))
            {
                for (int ebp = 0; ebp < 4; ebp++)
                {
                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);

                    for (int ebx = 0; ebx < 3; ebx++)
                    {
                        esp08.StartPoints[ebp].Position[ebx] = eax2.m00147C[ebx].M000000[ebp];
                    }

                    esp08.StartPoints[ebp].IsUsed = eax2.m00147C[3].M000000[ebp];
                }
            }

            if (esp08.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop)
            {
                if (esp08.StartPoints[0].Position[0] == 0
                    && esp08.StartPoints[0].Position[1] == 0
                    && esp08.StartPoints[0].Position[2] == 0)
                {
                    esp08.StartPoints[0].Position[2] = 0x08;
                }
            }

            for (int ebx = 0; ebx < 4; ebx++)
            {
                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);
                esp08.StartPoints[ebx].IsUsed = eax2.IsWPEnabled[ebx];
            }

            AlliedVariables.s_TieFileHandle.BlockWrite(esp08.ToByteArray(), AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L00524F50
    private static void Allied_WriteTieMission_RadioMessages(Form1Window eax0)
    {
        int ebx = AlliedVariables.s_TieFileHeader.RadioMessagesCount;

        for (int esi = 0; esi < ebx; esi++)
        {
            S0xTieRadioMessageObject eax = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, esi);
            AlliedVariables.s_TieFileHandle.BlockWrite(eax.RadioMessage.ToByteArray(), AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L00524FB4
    private static void Allied_WriteTieMission_GlobalGoals(Form1Window eax0)
    {
        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            S0xTieGlobalGoalObject eax = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, ebx);
            AlliedVariables.s_TieFileHandle.BlockWrite(eax.GlobalGoal.ToByteArray(), AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L00525008
    public static void Allied_WriteTieMission_Teams(Form1Window eax0)
    {
        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            S0xTieTeamObject eax = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, ebx);
            AlliedVariables.s_TieFileHandle.BlockWrite(eax.Team.ToByteArray(), 0x1E7, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L0052505C
    private static void Allied_WriteTieMission_Briefing(Form1Window eax0)
    {
        S0xTieBriefing ebpBBCC = AlliedVariables.s_Allied_Briefing[0];
        S0xTieBriefingData ebp5DE8 = ebpBBCC.BriefingData;

        AlliedVariables.s_TieFileHandle.BlockWrite(ebp5DE8.BriefingCode.ToByteArray(), AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        for (int ebp04 = 0; ebp04 < 0x80; ebp04++)
        {
            TStrings ebx0 = ebpBBCC.BriefingTags;

            int eax1 = ebx0.GetCount();
            string ebp0C = string.Empty;

            if (eax1 > ebp04)
            {
                ebp0C = ebx0.GetText(ebp04);
            }

            AlliedVariables.s_TieFileHandle.BlockWrite(BitConverter.GetBytes((short)ebp0C.Length), AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();

            if (ebp0C.Length > 0)
            {
                AlliedVariables.s_TieFileHandle.BlockWrite(Encoding.ASCII.GetBytes(ebp0C), AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();
            }
        }

        for (int ebp04 = 0; ebp04 < 0x80; ebp04++)
        {
            TStrings ebx0 = ebpBBCC.BriefingStrings;

            int eax1 = ebx0.GetCount();
            string ebp0C = string.Empty;

            if (eax1 > ebp04)
            {
                ebp0C = ebx0.GetText(ebp04);
            }

            AlliedVariables.s_TieFileHandle.BlockWrite(BitConverter.GetBytes((short)ebp0C.Length), AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();

            if (ebp0C.Length > 0)
            {
                AlliedVariables.s_TieFileHandle.BlockWrite(Encoding.ASCII.GetBytes(ebp0C), AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();

            }
        }
    }

    // L0052523C
    private static void L0052523C(Form1Window eax0)
    {
        string ebp64C;

        int ebp0C = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int ebp04 = 0; ebp04 < ebp0C; ebp04++)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp04);
            S0xFGObject_000E42 ebp60C = eax1.m000E42;

            for (int esi = 0; esi < 8; esi++)
            {
                S0xFGObject_000E42_000000 ebx = ebp60C.M000000[esi];

                ebp64C = Unit_00511CD0_Proc_0051213C(ebx.M000000.WithMaxLength(0x40));

                if (string.IsNullOrEmpty(ebp64C))
                {
                    AlliedVariables.s_TieFileHandle.BlockWrite((byte)0, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }
                else
                {
                    AlliedVariables.s_TieFileHandle.BlockWrite(ebp64C, 0x40, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }

                ebp64C = Unit_00511CD0_Proc_0051213C(ebx.M000040.WithMaxLength(0x40));

                if (string.IsNullOrEmpty(ebp64C))
                {
                    AlliedVariables.s_TieFileHandle.BlockWrite((byte)0, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }
                else
                {
                    AlliedVariables.s_TieFileHandle.BlockWrite(ebp64C, 0x40, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }

                ebp64C = Unit_00511CD0_Proc_0051213C(ebx.M000080.WithMaxLength(0x40));

                if (string.IsNullOrEmpty(ebp64C))
                {
                    AlliedVariables.s_TieFileHandle.BlockWrite((byte)0, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }
                else
                {
                    AlliedVariables.s_TieFileHandle.BlockWrite(ebp64C, 0x40, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }
            }
        }
    }

    // L00525424
    private static void L00525424(Form1Window eax0)
    {
        string ebp94C;

        for (int ebp04 = 0; ebp04 < 10; ebp04++)
        {
            S0xXvTGGStrObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00571188, ebp04);
            S0xXvTGGStrings[] ebp90C = eax1.GGStrings;

            for (int ebp08 = 0; ebp08 < 3; ebp08++)
            {
                for (int esi = 0; esi < 4; esi++)
                {
                    S0xXvTGGStrings ebx = ebp90C[ebp08 * 4 + esi];

                    ebp94C = Unit_00511CD0_Proc_0051213C(ebx.StrIncomp.WithMaxLength(0x40));

                    if (string.IsNullOrEmpty(ebp94C))
                    {
                        AlliedVariables.s_TieFileHandle.BlockWrite((byte)0, AlliedVariables.s_V0x00543C44);
                        System_L004028C4_CheckError();
                    }
                    else
                    {
                        AlliedVariables.s_TieFileHandle.BlockWrite(ebp94C, 0x40, AlliedVariables.s_V0x00543C44);
                        System_L004028C4_CheckError();
                    }

                    ebp94C = Unit_00511CD0_Proc_0051213C(ebx.StrSucc.WithMaxLength(0x40));

                    if (string.IsNullOrEmpty(ebp94C))
                    {
                        AlliedVariables.s_TieFileHandle.BlockWrite((byte)0, AlliedVariables.s_V0x00543C44);
                        System_L004028C4_CheckError();
                    }
                    else
                    {
                        AlliedVariables.s_TieFileHandle.BlockWrite(ebp94C, 0x40, AlliedVariables.s_V0x00543C44);
                        System_L004028C4_CheckError();
                    }

                    ebp94C = Unit_00511CD0_Proc_0051213C(ebx.StrFail.WithMaxLength(0x40));

                    if (string.IsNullOrEmpty(ebp94C))
                    {
                        AlliedVariables.s_TieFileHandle.BlockWrite((byte)0, AlliedVariables.s_V0x00543C44);
                        System_L004028C4_CheckError();
                    }
                    else
                    {
                        AlliedVariables.s_TieFileHandle.BlockWrite(ebp94C, 0x40, AlliedVariables.s_V0x00543C44);
                        System_L004028C4_CheckError();
                    }
                }
            }
        }
    }

    // L0051E218
    private static void L0051E218()
    {
        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x0054AE60.unk000000, 0x49, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();

        for (int edi = 0; edi < 0x7D0; edi++)
        {
            byte ebx = AlliedVariables.s_V0x0054AEAC.unk000000[edi];

            AlliedVariables.s_TieFileHandle.BlockWrite(ebx, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();

            if (ebx == 0)
            {
                continue;
            }

            AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x0054B67C[edi].ToByteArray(), AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }

        AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_V0x0056A2AC.unk000000, 0x5C7, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
    }

    // L00517050
    private static void L00517050(int eax0, int edx0)
    {
        S0xFGObject? eax1;

        switch (eax0)
        {
            case 0x00:
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0);
                eax1.FlightGroupStruct.Iff = 0;
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0);
                eax1.FlightGroupStruct.Team = L005173F4();
                break;

            case 0x01:
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0);
                eax1.FlightGroupStruct.Iff = 0x01;
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0);
                eax1.FlightGroupStruct.Team = L00517424();
                break;
        }

        if (AlliedVariables.s_V0x005B6D16 != 0)
        {
            Allied_ComboBox_SetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.TeamBox, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Team);
            Allied_ComboBox_SetSelectedIndex(AlliedVariables.s_TShipExt_Instance!.IFFBox, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Iff);
        }

        DatapadWindowImpl.TDatapad__PROC_004BFC68(AlliedVariables.s_TDatapad_Instance!);
    }

    // L0051FEA0
    public static CraftIdEnum AlliedConvertShipSeqToCraftId(ShipSeqEnum eax0)
    {
        string ebp04 = AlliedVariables.s_Strings_ShipSeq.GetText((int)eax0);
        CraftIdEnum ebx = (CraftIdEnum)int.Parse(ebp04);
        return ebx;
    }

    // L00517258
    public static void Unit_00513838_Proc_00517258()
    {
        if ((AlliedVariables.s_V0x00543C99 & AlliedVariables.s_V0x00543C9A) != 0)
        {
            Unit_00513838_Proc_0051DB68();
        }
    }

    // L0051442C
    public static void Unit_00513838_Proc_0051442C()
    {
        AlliedVariables.s_V0x00543B55 = 0x01;

        Unit_00513838_Proc_005146A4();
    }

    // L0051207C
    public static S0xTieHeaderSet Unit_00511CD0_Proc_0051207C(string eax0)
    {
        //  todo
        S0xTieHeaderSet ebp5F = new();
        ebp5F.Name = eax0;
        return ebp5F;
    }

    // L0051EF8C
    public static void Unit_00513838_Proc_0051EF8C(int eax0, int edx0)
    {
        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx))
            {
                continue;
            }

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            byte[] fg = eax1.FlightGroupStruct.ToByteArray();
            fg[0x0DBE + eax0 - 1] = (byte)edx0;
            eax1.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(fg);
        }

        switch (eax0)
        {
            case 0x07:
                Unit_00513838_Proc_00517258();
                break;

            case 0x0B:
                Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.ExpLabel, Unit_00513838_Proc_0051C274(Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.ExpTimeSpin)));
                break;

            case 0x0D:
                Unit_00513838_Proc_00517258();
                break;
        }

        Unit_00513838_Proc_0051467C();
    }

    // L00511FC4
    public static S0x00533EA8 Unit_00511CD0_Proc_00511FC4(string eax0)
    {
        int esi;

        if (eax0.Length == 0)
        {
            esi = 0;
        }
        else if (eax0.Length < 0x3F)
        {
            esi = eax0.Length + 1;
        }
        else
        {
            esi = 0x3F;
        }

        S0x00533EA8 ebp43 = new();

        for (int eax = 0; eax < esi; eax++)
        {
            ebp43.unk000000[eax] = (byte)eax0[eax];
        }

        for (int eax = esi; eax < 0x3F; eax++)
        {
            ebp43.unk000000[eax] = 0;
        }

        return ebp43;
    }

    // L004C53F4
    private static void Allied_ReadTieMission_01(TieFileVersionEnum eax0)
    {
        AlliedVariables.s_V0x00535EAC = 0;
        AlliedVariables.s_V0x00543C9F = 0;

        Unit_00513838_Proc_005196CC();

        if (AlliedVariables.s_FlightGroupObjectsList.Count > 0)
        {
            Classes_TList_Delete(AlliedVariables.s_FlightGroupObjectsList, 0);
        }

        for (int eax = 0; eax < 0x190; eax++)
        {
            AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[eax] = 0;
        }

        for (int eax = 0; eax < 0xD50; eax++)
        {
            AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m0036BA[eax] = 0;
        }

        for (int eax = 0; eax < 0x0A; eax++)
        {
            AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.ForTeam[eax] = 0;
        }

        for (int eax = 0; eax < 0x19BE; eax++)
        {
            AlliedVariables.s_V0x00535EB0.BriefingData.m004414[eax] = 0;
        }

        for (int eax = 0; eax < 0x32; eax++)
        {
            AlliedVariables.s_V0x0053BC94[1 + eax] = -1;
        }

        S0xTieTeamObject? eax1 = null;

        {
            byte[] buffer = new byte[S0x005341C0.Size];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x16, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            AlliedVariables.s_V0x005AFCC8 = S0x005341C0.FromByteArray(buffer);
        }

        AlliedVariables.s_TieFileHeader.FlightGroupsCount = AlliedVariables.s_V0x005AFCC8.FlightGroupsCount;
        AlliedVariables.s_TieFileHeader.RadioMessagesCount = AlliedVariables.s_V0x005AFCC8.RadioMessagesCount;

        {
            byte[] buffer = new byte[S0x005346C4.Size];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x180, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            AlliedVariables.s_V0x005AFCE0 = S0x005346C4.FromByteArray(buffer);
        }

        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax1.Team.PrimarySuccessMessage1 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x005AFCE0.M000000, 0x40));
        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax1.Team.PrimarySuccessMessage2 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x005AFCE0.M000040, 0x40));
        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax1.Team.SecondarySuccessMessage1 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x005AFCE0.M000080, 0x40));
        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax1.Team.SecondarySuccessMessage2 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x005AFCE0.M0000C0, 0x40));
        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax1.Team.PrimaryFailureMessage1 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x005AFCE0.M000100, 0x40));
        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax1.Team.PrimaryFailureMessage2 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x005AFCE0.M000140, 0x40));

        {
            short ebp02 = 0;
            byte[] buffer = new byte[2];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 2, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            ebp02 = BitConverter.ToInt16(buffer, 0);
        }

        L005182B8(0x01, 0);

        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Empire");
        eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 1);
        eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Rebel");

        {
            byte[] buffer = new byte[S0x005346AC.Size];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x30, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            AlliedVariables.s_V0x005AFE60 = S0x005346AC.FromByteArray(buffer);
        }

        for (int esi = 0; esi < 4; esi++)
        {
            TFixedString ebx = AlliedVariables.s_V0x005AFE60.M000000[esi];
            TFixedString edi = AlliedVariables.s_TieFileHeader.Header.IffNames[esi];

            string ebp08 = ebx.Text;

            if (string.IsNullOrEmpty(ebp08))
            {
                continue;
            }

            if (ebp08[0] == '1')
            {
                ebp08 = ebp08[2..];
            }
            else if (esi != 0x02)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
                eax1.Team.TeamAllied[0x02 + esi] = TieAllegeanceEnum.Friendly;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0x02 + esi);
            eax1.Team.Name = Unit_00511CD0_Proc_00511E04(ebp08);

            edi.Text = Unit_00511CD0_Proc_00511EB8(ebp08);
        }

        L004C5824();
        Unit_00513838_Proc_0051DB68();
        L004C651C();
        L004C67C0();
        L004C6904();
        L004C6BC8();
    }

    // L004C5824
    private static void L004C5824()
    {
        bool ebp15 = false;

        AlliedVariables.s_V0x00535EA8 = 0;

        int ebp1C = AlliedVariables.s_V0x005AFCC8.FlightGroupsCount;

        for (int ebp04 = 0; ebp04 < ebp1C; ebp04++)
        {
            byte[] ebp148 = new byte[0x124];
            AlliedVariables.s_TieFileHandle.BlockRead(ebp148, 0x124, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();

            S0xFGObject ebp14 = new();

            ebp14.FlightGroupStruct.TacticalRoleUsed0 = (TacticalRoleUsedEnum)0xFF;
            ebp14.FlightGroupStruct.TacticalRoleUsed1 = (TacticalRoleUsedEnum)0xFF;
            ebp14.FlightGroupStruct.Comm = 0x02;
            ebp14.FlightGroupStruct.GlobalCargoIndex = 0xFF;
            ebp14.FlightGroupStruct.SpecialCargoIndex = 0xFF;

            ebp14.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(System_LStrFromPCharLen(ebp148.ReadFixedLengthString(0x00, 0x0C), 0x0C));
            ebp14.FlightGroupStruct.Cargo = Unit_00511CD0_Proc_00511EB8(System_LStrFromPCharLen(ebp148.ReadFixedLengthString(0x18, 0x0C), 0x0C));
            ebp14.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8(System_LStrFromPCharLen(ebp148.ReadFixedLengthString(0x24, 0x0C), 0x0C));

            ebp14.FlightGroupStruct.SpecialCraft = ebp148[0x30];
            ebp14.FlightGroupStruct.RandomSpecialCraft = ebp148[0x31];
            ebp14.FlightGroupStruct.CraftId = (CraftIdEnum)ebp148[0x32];
            ebp14.FlightGroupStruct.CraftsCount = ebp148[0x33];
            ebp14.FlightGroupStruct.Status1 = (FlightGroupStatusEnum)ebp148[0x34];
            ebp14.FlightGroupStruct.WarheadType = ebp148[0x35];
            ebp14.FlightGroupStruct.BeamType = ebp148[0x36];
            ebp14.FlightGroupStruct.Iff = ebp148[0x37];
            ebp14.FlightGroupStruct.Pitch = ebp148[0x44];

            if (ebp148[0x37] == 0)
            {
                ebp14.FlightGroupStruct.Team = 0x01;
            }
            else if (ebp148[0x37] == 0x01)
            {
                ebp14.FlightGroupStruct.Team = 0;
            }
            else
            {
                ebp14.FlightGroupStruct.Team = ebp148[0x37];
            }

            ebp14.FlightGroupStruct.TacticalRoleUsed0 = (TacticalRoleUsedEnum)0xFF;
            ebp14.FlightGroupStruct.TacticalRoleUsed1 = (TacticalRoleUsedEnum)0xFF;

            if (ebp14.FlightGroupStruct.CraftId == CraftIdEnum._087_1_11_AsteroidHR1)
            {
                ebp15 = true;

                ebp14.FlightGroupStruct.CraftId = CraftIdEnum._183_9001_1100_ResData_Backdrop;
                ebp14.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8("1.0 1.0 1.0");
                ebp14.FlightGroupStruct.Cargo = Unit_00511CD0_Proc_00511EB8("1.0");

                ebp14.FlightGroupStruct.PlanetId = (byte)ebp14.FlightGroupStruct.Status1 switch
                {
                    0x00 => 0x11,
                    0x01 => 0x3C,
                    0x02 => 0x1C,
                    0x03 => 0x30,
                    0x04 => 0x1C,
                    0x05 => 0x04,
                    0x06 => 0x06,
                    0x07 => 0x34,
                    0x08 => 0x0D,
                    _ => 0x01,
                };

                if ((ebp14.FlightGroupStruct.Status1 >= FlightGroupStatusEnum.Normal && ebp14.FlightGroupStruct.Status1 <= FlightGroupStatusEnum.HalfWarheads)
                    || (ebp14.FlightGroupStruct.Status1 >= FlightGroupStatusEnum.LasersDamaged && ebp14.FlightGroupStruct.Status1 <= FlightGroupStatusEnum.ShieldsAdded))
                {
                    ebp14.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8("0.5");
                }
                else if (ebp14.FlightGroupStruct.Status1 >= FlightGroupStatusEnum.NoShieldsDamaged && ebp14.FlightGroupStruct.Status1 <= FlightGroupStatusEnum.HalfShieldsDamaged)
                {
                    ebp14.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8("1.0");
                }
                else
                {
                    ebp14.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8("0.5");
                }

                if (ebp14.FlightGroupStruct.Status1 == FlightGroupStatusEnum.DoubleWarheads || ebp14.FlightGroupStruct.Status1 == FlightGroupStatusEnum.NoShieldsDamaged)
                {
                    ebp14.FlightGroupStruct.GlobalCargoIndex = 0x02;
                }
                else if (ebp14.FlightGroupStruct.Status1 == FlightGroupStatusEnum.HalfWarheads || ebp14.FlightGroupStruct.Status1 == FlightGroupStatusEnum.HyperdriveDamaged)
                {
                    ebp14.FlightGroupStruct.GlobalCargoIndex = 0x01;
                }
                else
                {
                    ebp14.FlightGroupStruct.GlobalCargoIndex = 0x03;
                }

                ebp14.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Normal;
            }

            ebp14.FlightGroupStruct.AIRank = ebp148[0x38];

            if (ebp148[0x38] == 0x05)
            {
                ebp14.FlightGroupStruct.Status2 = FlightGroupStatusEnum.Indestructible;
            }

            ebp14.FlightGroupStruct.Markings = ebp148[0x39];

            if (ebp148[0x3A] == 0)
            {
                ebp14.FlightGroupStruct.Radio = 0;
            }
            else
            {
                ebp14.FlightGroupStruct.Radio = 0x09;
            }

            ebp14.FlightGroupStruct.FormationType = ebp148[0x3C];
            ebp14.FlightGroupStruct.FormationSpacing = ebp148[0x3D];
            ebp14.FlightGroupStruct.GlobalGroupId = ebp148[0x3E];
            ebp14.FlightGroupStruct.WavesCount = ebp148[0x40];

            if (ebp148[0x42] > 0)
            {
                ebp14.FlightGroupStruct.GlobalUnitId = 0x01;
                ebp14.FlightGroupStruct.PlayerNumber = 0x01;
            }

            ebp14.FlightGroupStruct.ArrivalDifficulty = (ArrivalDifficultyEnum)ebp148[0x49];
            ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[0] = L004C6DA0(BitConverter.ToUInt32(ebp148, 0x4A));
            ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[1] = L004C6DA0(BitConverter.ToUInt32(ebp148, 0x4E));
            ebp14.FlightGroupStruct.ArrivalDelayMinutes = ebp148[0x54];
            ebp14.FlightGroupStruct.ArrivalDelaySeconds = ebp148[0x55];
            ebp14.FlightGroupStruct.ArrivalTrigger1.Operator = ebp148[0x52] != 0;
            ebp14.FlightGroupStruct.DepartureTrigger.Triggers[0] = L004C6DA0(BitConverter.ToUInt32(ebp148, 0x56));
            ebp14.FlightGroupStruct.StartFg = ebp148[0x60];
            ebp14.FlightGroupStruct.StartFgUsed = ebp148[0x61];
            ebp14.FlightGroupStruct.PrimaryStopFg = ebp148[0x62];
            ebp14.FlightGroupStruct.PrimaryStopFgUsed = ebp148[0x63];
            ebp14.FlightGroupStruct.SecondaryStopFg = ebp148[0x64];
            ebp14.FlightGroupStruct.SecondaryStopFgUsed = ebp148[0x65];
            ebp14.FlightGroupStruct.CaptureFg = ebp148[0x66];
            ebp14.FlightGroupStruct.CaptureFgUsed = ebp148[0x67];
            ebp14.FlightGroupStruct.AbortCondition = ebp148[0x5C];

            for (int ebp10 = 0; ebp10 < 0x03; ebp10++)
            {
                byte[] ebx = ebp148.Subarray(0x68 + ebp10 * 0x12, 0x12);

                for (int esi = 0; esi < 0x04; esi++)
                {
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].Var2 = BitConverter.ToInt16(ebx, 0x00);

                    switch (ebx[0x00])
                    {
                        case 0x0C:
                        case 0x0D:
                        case 0x0E:
                        case 0x0F:
                        case 0x10:
                        case 0x11:
                        case 0x12:
                        case 0x13:
                        case 0x14:
                        case 0x1F:
                        case 0x20:
                        case 0x24:
                            ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SecondaryTarget.ClassA = (TieClassEnum)XvTBoxImpl.TXvTForm_PROC_004CCE7C(AlliedVariables.s_TXvTForm_Instance!, ebx[0x02]);
                            break;

                        default:
                            ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SecondaryTarget.ClassA = (TieClassEnum)ebx[0x02];
                            break;
                    }

                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SecondaryTarget.ClassB = (TieClassEnum)ebx[0x03];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.Operator = ebx[0x0C];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SpeedMph = BitConverter.ToInt16(ebx, 0x0E);
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SecondaryTarget.Operator = ebx[0x06];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SecondaryTarget.m000005 = ebx[0x07];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.m000005 = ebx[0x0D];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.ClassA = (TieClassEnum)ebx[0x08];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.ParameterA = ebx[0x09];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].Waypoints[0].Position[0] = ebx[0x10];
                    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.ClassB = (TieClassEnum)ebx[0x0A];

                    if (ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.Operator == 0x02)
                    {
                        ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.m000005++;
                    }

                    // todo
                    //if (ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SpeedMph == 0x02)
                    //{
                    //    ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].m000013++;
                    //}

                    if (ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SecondaryTarget.Operator == 0x02)
                    {
                        ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.ClassA++;
                    }

                    if (ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].SecondaryTarget.m000005 == 0x02)
                    {
                        ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].PrimaryTarget.ParameterA++;
                    }
                }
            }

            for (int edx = 0; edx < 0x04; edx++)
            {
                byte[] eax = ebp148.Subarray(0x9E + edx * 0x02, 0x02);

                if (eax[0x01] == 0)
                {
                    ebp14.FlightGroupStruct.Goals[edx].Amount = TieAmountEnum._100Percent;
                }
                else if (eax[0x01] == 0x01)
                {
                    ebp14.FlightGroupStruct.Goals[edx].Amount = TieAmountEnum._50Percent;
                }
                else
                {
                    ebp14.FlightGroupStruct.Goals[edx].Amount = TieAmountEnum._50Percent + eax[0x01];
                }

                ebp14.FlightGroupStruct.Goals[edx].Condition = (TieConditionEnum)eax[0];

                if (eax[0] < 0x07 || eax[0] == 0x08)
                {
                    if (edx == 0)
                    {
                        ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.PRImary;
                        ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    }
                }

                if (edx < 0x04)
                {
                    ebp14.FlightGroupStruct.Goals[edx].GoalType = TieFGGoalTypeEnum.Bonus;
                }

                if (eax[0] == 0x09)
                {
                    if (edx == 0x03)
                    {
                        ebp14.FlightGroupStruct.Goals[edx].GoalType = TieFGGoalTypeEnum.Sec;
                    }
                    else
                    {
                        ebp14.FlightGroupStruct.Goals[edx].GoalType = TieFGGoalTypeEnum.Loss;
                        ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.MISsionCritical;
                        ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    }

                    ebp14.FlightGroupStruct.Goals[edx].Condition = TieConditionEnum.Destroyed;
                }
                else if (eax[0] < 0x0D)
                {
                    if (edx == 0x03)
                    {
                        ebp14.FlightGroupStruct.Goals[edx].GoalType = TieFGGoalTypeEnum.Bonus;
                    }
                    else
                    {
                        ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.MISsionCritical;
                        ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    }
                }
            }

            for (int edx = 0x04; edx < 0x08; edx++)
            {
                ebp14.FlightGroupStruct.Goals[edx].Condition = TieConditionEnum.Never;
            }

            for (int edx = 0; edx < 0x08; edx++)
            {
                ebp14.FlightGroupStruct.Goals[edx].AppliesToTeams[0] = 0x01;
            }

            if (BitConverter.ToInt16(ebp148, 0xA6) > 0x3F)
            {
                ebp14.FlightGroupStruct.Goals[3].Points = 0x7F;
            }
            else
            {
                ebp14.FlightGroupStruct.Goals[3].Points = (byte)(BitConverter.ToInt16(ebp148, 0xA6) * 0x02);
            }

            for (int ecx = 0; ecx < 0x03; ecx++)
            {
                byte[] ebx = ebp148.Subarray(0xA8 + ecx * 0x1E, 0x1E);

                for (int eax = 0; eax < 0x04; eax++)
                {
                    ebp14.m00147C[ecx].M000000[eax] = BitConverter.ToInt16(ebx, eax * 0x02);
                }
            }

            for (int eax = 0; eax < 0x04; eax++)
            {
                ebp14.IsWPEnabled[eax] = BitConverter.ToInt16(ebp148, 0x102 + eax * 0x02);
            }

            for (int esi = 0; esi < 0x04; esi++)
            {
                for (int ebp10 = 0; ebp10 < 0x04; ebp10++)
                {
                    for (int ecx = 0; ecx < 0x03; ecx++)
                    {
                        for (int eax = 0; eax < 0x08; eax++)
                        {
                            ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].Waypoints[0].Position[ecx] = BitConverter.ToInt16(ebp148, 0xB0 + ecx * 0x1E + eax * 0x02);
                        }
                    }

                    for (int eax = 0; eax < 0x08; eax++)
                    {
                        ebp14.FlightGroupStruct.Orders[esi * 4 + ebp10].Waypoints[eax].IsUsed = BitConverter.ToInt16(ebp148, 0x10A + eax * 0x02);
                    }

                    if (BitConverter.ToInt16(ebp148, 0x11C) == 0x11C)
                    {
                        for (int ecx = 0; ecx < 0x04; ecx++)
                        {
                            ebp14.m00147C[ecx].M000000[3] = BitConverter.ToInt16(ebp148, 0xC2 + ecx * 0x1E);
                        }

                        ebp14.IsWPEnabled[3] = 0x01;
                    }
                }
            }

            for (int esi = 0; esi < 0x04; esi++)
            {
                ebp14.FlightGroupStruct.StartPoints[0].Position[esi] = BitConverter.ToInt16(ebp148, 0xC2 + 0xA8 + esi * 0x1E);
            }

            if (BitConverter.ToInt16(ebp148, 0x11E) == 0x01 && AlliedVariables.s_V0x00535EAC < 0x33)
            {
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8] = 0;
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x01] = 0x1A;
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x02] = (short)AlliedVariables.s_V0x00535EAC;
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x03] = ebp148[0x32];
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x04] = ebp148[0x37];
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x05] = 0;
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x06] = 0x1C;
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x07] = (short)AlliedVariables.s_V0x00535EAC;
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x08] = BitConverter.ToInt16(ebp148, 0xC4);
                AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + 0x09] = BitConverter.ToInt16(ebp148, 0xE2);

                AlliedVariables.s_V0x0053BC94[ebp04] = AlliedVariables.s_V0x00535EAC;
                AlliedVariables.s_V0x00535EAC++;
                AlliedVariables.s_V0x00535EA8 += 0x0A;
            }

            if (AlliedVariables.s_UseAutoChkSetting)
            {
                ebp14.AutoLink = true;
            }

            BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(ebp14.FlightGroupStruct)).CopyTo(ebp14.m001444, 0);

            if (AlliedVariables.s_V0x00543C84.M000000[1].M000000)
            {
                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C40))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[1].M000001 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[1].M000001;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C60))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[1].M000002 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[1].M000002;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C20))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[1].M000003 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[1].M000003;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533BC0))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[1].M000004 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[1].M000004;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533BE0))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[1].M000005 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[1].M000005;
                    }
                }
            }

            AlliedVariables.s_FlightGroupObjectsList.Add(ebp14);
            Unit_00513838_Proc_0051D53C(ebp04);
        }

        if (!ebp15)
        {
            XvTBoxImpl.TXvTForm__PROC_004CCE90(AlliedVariables.s_TXvTForm_Instance!);
        }

        Unit_00513838_Proc_00520924();
    }

    // L004C6DA0
    private static S0xTieTrigger L004C6DA0(uint eax0)
    {
        byte[] esp00 = BitConverter.GetBytes(eax0);
        S0xTieTrigger esp04 = new();

        esp04.Condition = (TieConditionEnum)esp00[0];
        esp04.VariableType = (TieClassEnum)esp00[1];

        if (esp04.VariableType == TieClassEnum.ShipType)
        {
            esp04.Variable = (byte)(esp00[2] + 1);
        }
        else
        {
            esp04.Variable = esp00[2];
        }

        esp04.Amount = (TieAmountEnum)esp00[3];
        esp04.Parameter = 0;

        return esp04;
    }

    // L004C651C
    private static void L004C651C()
    {
        int esi0 = AlliedVariables.s_V0x005AFCC8.RadioMessagesCount;

        for (int edi0 = 0; edi0 < esi0; edi0++)
        {
            byte[] buffer = new byte[S0x004C651C_00.Size];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x5A, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            S0x004C651C_00 ebp5A = S0x004C651C_00.FromByteArray(buffer);

            S0xTieRadioMessageObject ebx = new();
            ebx.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp5A.M000000, 0x40));
            ebx.RadioMessage.Condition.Trigger_0[0] = L004C6DA0(ebp5A.m000040);
            ebx.RadioMessage.Condition.Trigger_0[1] = L004C6DA0(ebp5A.m000044);
            ebx.RadioMessage.Condition.Operator_0 = ebp5A.m000048 != 0;
            ebx.RadioMessage.Condition.TriggerUsed_0[0] = ebp5A.m00004A;
            ebx.RadioMessage.Condition.TriggerUsed_0[1] = ebp5A.m00004B;

            if (ebp5A.M000000.Length > 0 && ebp5A.M000000[0] == '1')
            {
                ebx.RadioMessage.Side = 0;
                ebx.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp5A.M000000, 0x40));
            }
            else if (ebp5A.M000000.Length > 0 && ebp5A.M000000[0] == '2')
            {
                ebx.RadioMessage.Side = 2;
                ebx.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp5A.M000000, 0x40));
            }
            else if (ebp5A.M000000.Length > 0 && ebp5A.M000000[0] == '3')
            {
                ebx.RadioMessage.Side = 5;
                ebx.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp5A.M000000, 0x40));
            }
            else
            {
                ebx.RadioMessage.Side = 1;
                ebx.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp5A.M000000, 0x40));
            }

            ebx.RadioMessage.TimePassed = XvTBoxImpl.TXvTForm_PROC_004CCE7C(AlliedVariables.s_TXvTForm_Instance!, ebp5A.m000058);
            ebx.M0000A6 = 0x01;
            ebx.RadioMessage.ForTeam[0] = 0x01;
            ebx.RadioMessage.Id = (short)edi0;

            AlliedVariables.s_RadioMessagesObjectsList.Add(ebx);
        }

        Unit_00513838_Proc_0051477C();
    }

    // L004C67C0
    private static void L004C67C0()
    {
        for (int ebx = 0; ebx < 3; ebx++)
        {
            int esi = 0;

            if (ebx == 0)
            {
                esi = 0;
            }
            else if (ebx == 1)
            {
                esi = 2;
            }
            else if (ebx == 2)
            {
                esi = 2;
            }

            byte[] buffer = new byte[S0x004C67C0.Size];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x1C, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            S0x004C67C0 esp00 = S0x004C67C0.FromByteArray(buffer);

            if (ebx == 0 || ebx == 2)
            {
                S0xTieGlobalGoalObject eax;
                eax = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);
                eax.GlobalGoal.GlobalGoals[esi].Triggers.Trigger_0[0] = L004C6DA0(esp00.m000000);
                eax = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);
                eax.GlobalGoal.GlobalGoals[esi].Triggers.Trigger_0[1] = L004C6DA0(esp00.m000004);
                eax = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);
                eax.GlobalGoal.GlobalGoals[esi].Triggers.Operator_0 = esp00.m000019 != 0;
            }
            else if (ebx == 1)
            {
                S0xTieGlobalGoalObject eax;
                eax = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);
                eax.GlobalGoal.GlobalGoals[2].Triggers.Trigger_1[0] = L004C6DA0(esp00.m000000);
                eax = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);
                eax.GlobalGoal.GlobalGoals[2].Triggers.Trigger_1[1] = L004C6DA0(esp00.m000004);
                eax = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, 0);
                eax.GlobalGoal.GlobalGoals[2].Triggers.Operator_0 = esp00.m000019 != 0;
            }
        }
    }

    // L004C6904
    public static void L004C6904()
    {
        S0xTieBriefing esi = AlliedVariables.s_V0x00535EB0;

        byte[] buffer = new byte[S0x004C6904.Size];
        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x32A, AlliedVariables.s_V0x00535EA4);
        System_L004028C4_CheckError();
        S0x004C6904 esp00 = S0x004C6904.FromByteArray(buffer);

        esi.BriefingTags = new();
        esi.BriefingStrings = new();

        Unit_00513838_Proc_0051D3D8(esi.BriefingTags, 0x20);

        for (int ebx = 0; ebx < 0x60; ebx++)
        {
            esi.BriefingTags.Add(string.Empty);
        }

        Unit_00513838_Proc_0051D3D8(esi.BriefingStrings, 0x20);

        for (int ebx = 0; ebx < 0x60; ebx++)
        {
            esi.BriefingStrings.Add(string.Empty);
        }

        esi.BriefingData.BriefingCode.ForTeam[0] = 0x01;

        esi.BriefingData.BriefingCode.Length = (short)Math.Round(esp00.m000000 * 1.6);
        esi.BriefingData.BriefingCode.Time = esp00.m000002;
        esi.BriefingData.BriefingCode.Index = esp00.m000004;
        esi.BriefingData.BriefingCode.Title = esp00.m000008;
        esi.BriefingData.BriefingCode.Length = (short)(AlliedVariables.s_V0x00535EA8 + esp00.m000006);

        for (int ebx = 0; ebx < 0x190;)
        {
            short bp = esp00.m00000A[ebx];

            if (bp == 0x270F)
            {
                esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = (short)Math.Round((double)bp);
            }
            else
            {
                esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = (short)Math.Round(bp * 1.6);
            }

            ebx++;

            BriefingCommandEnum ax0 = (BriefingCommandEnum)esp00.m00000A[ebx];
            esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = (short)ax0;
            ebx++;

            switch (ax0)
            {
                case BriefingCommandEnum.None:
                case BriefingCommandEnum.Seek:
                case BriefingCommandEnum.ShipIndex:
                case BriefingCommandEnum.PageBreak:
                case BriefingCommandEnum.ClearFlightGroupTags:
                case BriefingCommandEnum.ClearTextTags:
                    break;

                case BriefingCommandEnum.BriefingTitle:
                case BriefingCommandEnum.BriefingText:
                    {
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = esp00.m00000A[ebx];
                        ebx++;
                        break;
                    }

                case BriefingCommandEnum.MoveMap:
                    {
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = esp00.m00000A[ebx];
                        ebx++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = esp00.m00000A[ebx];
                        ebx++;
                        break;
                    }

                case BriefingCommandEnum.ScaleMap:
                    {
                        int eax = (int)Math.Round(esp00.m00000A[ebx] * 1.8);
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = (short)eax;
                        ebx++;

                        eax = (int)Math.Round(esp00.m00000A[ebx] * 1.8);
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = (short)eax;
                        ebx++;
                        break;
                    }

                case BriefingCommandEnum.FlightGroupTags1:
                case BriefingCommandEnum.FlightGroupTags2:
                case BriefingCommandEnum.FlightGroupTags3:
                case BriefingCommandEnum.FlightGroupTags4:
                case BriefingCommandEnum.FlightGroupTags5:
                case BriefingCommandEnum.FlightGroupTags6:
                case BriefingCommandEnum.FlightGroupTags7:
                case BriefingCommandEnum.FlightGroupTags8:
                    {
                        int ax1 = AlliedVariables.s_V0x0053BC94[esp00.m00000A[ebx]];
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = (short)ax1;
                        ebx++;
                        break;
                    }

                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    {
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = esp00.m00000A[ebx];
                        ebx++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = esp00.m00000A[ebx];
                        ebx++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = esp00.m00000A[ebx];
                        ebx++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x00535EA8 + ebx] = esp00.m00000A[ebx];
                        ebx++;
                        break;
                    }
            }
        }

        AlliedVariables.s_Allied_Briefing[0] = esi.Clone();
    }

    // L004C6BC8
    private static void L004C6BC8()
    {
        string ebp18_3 = string.Empty;
        string ebp18_2 = string.Empty;
        string ebp18_1 = string.Empty;
        string ebp18_0 = "$$";
        byte[] buffer = new byte[2];

        for (int esi = 0; esi < 0x14; esi++)
        {
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00535EA4);
            System_L004028C4_CheckError();
            short ebp02 = BitConverter.ToInt16(buffer, 0);

            string ebp18_4 = string.Empty;

            for (int ebx = 0; ebx < ebp02; ebx++)
            {
                AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00535EA4);
                System_L004028C4_CheckError();
                char ebp19 = (char)buffer[0];

                ebp18_4 += System_LStrFromChar(ebp19);

                if (ebp19 == '?')
                {
                    ebp18_4 += ebp18_0;
                }
            }

            if (!string.IsNullOrEmpty(ebp18_4))
            {
                if (esi < 0x0A)
                {
                    ebp18_3 += ebp18_4 + ebp18_0;
                }
                else if (esi < 0x14)
                {
                    if (ebp18_4[0] == 0x05)
                    {
                        ebp18_4 = ebp18_4[0x03..];
                        ebp18_2 += ebp18_4 + ebp18_0;
                    }
                    else
                    {
                        ebp18_4 = ebp18_4[0x03..];
                        ebp18_1 += ebp18_4 + ebp18_0;
                    }
                }
            }

            L004C6DF4(ebp18_3, AlliedVariables.s_TieMission_Description, false);
            L004C6DF4(ebp18_1, AlliedVariables.s_TieMission_WinDebriefing, false);
            L004C6DF4(ebp18_2, AlliedVariables.s_TieMission_LostDebriefing, true);
        }
    }

    // L004C6DF4
    private static void L004C6DF4(string eax0, TFixedString edx0, bool ecx0)
    {
        char[] ebp08 = edx0.ToCharArray();

        if (ecx0)
        {
            ebp08[0] = '#';
        }

        for (int eax = 0; eax < eax0.Length; eax++)
        {
            int esi;

            if (ecx0)
            {
                esi = 0x02 + eax;
            }
            else
            {
                esi = eax;
            }

            char cl = eax0[eax];

            if (cl == 0x01)
            {
                ebp08[esi] = ']';
            }
            else if (cl == 0x02)
            {
                ebp08[esi] = '[';
            }
            else if (cl == 0x0A)
            {
                ebp08[esi] = ' ';
            }
            else
            {
                ebp08[esi] = eax0[eax];
            }
        }

        edx0.UpdateCharArray(ebp08);
    }

    // L004CEE18
    private static void Allied_ReadTieMission_02()
    {
        AlliedVariables.s_V0x00543C9F = 0;
        AlliedVariables.s_V0x00542054 = new();
        AlliedVariables.s_V0x00541EE4 = 0;
        AlliedVariables.s_V0x00542034 = AlliedVariables.s_V0x00543BF8;
        Unit_00513838_Proc_005196CC();
        AlliedVariables.s_V0x00543BF8 = AlliedVariables.s_V0x00542034;
        AlliedVariables.s_FlightGroupObjectsList.Clear();
        L004CF0C0();
        L004CF108();
        L004D0FBC();
        L005182B8(0, 0x01);

        AlliedVariables.s_V0x00543C78[0x02] = 0x02;

        S0xTieTeamObject eax0 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0x02);
        eax0.Team.Name = Unit_00511CD0_Proc_00511E04("Neutral");

        eax0 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax0.Team.PrimarySuccessMessage1 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x00541E18.PrimarySuccessMessage1, 0x40));

        eax0 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax0.Team.PrimarySuccessMessage2 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x00541E18.PrimarySuccessMessage2, 0x40));

        eax0 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, 0);
        eax0.Team.SecondarySuccessMessage1 = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(AlliedVariables.s_V0x00541E18.SecondarySuccessMessage1, 0x40));

        Unit_00513838_Proc_0051DB68();

        if (AlliedVariables.s_V0x00541EE4 != 0)
        {
            MessageBox_ShowInformation("This mission should include the first Death Star as a Backdrop.");
        }

        if (AlliedVariables.s_V0x00541E18.m000006 == 0x01)
        {
            MessageBox_ShowInformation("This mission should be fought over the Death Star surface.");
        }

        Unit_004CEE18_Proc_004D15CC(AlliedVariables.s_V0x00542034[0x01..^2] + "brf");
        AlliedVariables.s_V0x00543C9F = 0x01;
    }

    // L004CF0C0
    private static void L004CF0C0()
    {
        byte[] buffer = new byte[0xCC];
        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0xCC, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        AlliedVariables.s_V0x00541E18 = S0x00541E18.FromByteArray(buffer);

        AlliedVariables.s_TieFileHeader.Header.TimeLimit = AlliedVariables.s_V0x00541E18.m000000;
        AlliedVariables.s_TieFileHeader.FlightGroupsCount = (short)(AlliedVariables.s_V0x00541E18.m0000C8 + AlliedVariables.s_V0x00541E18.m0000CA);
    }

    // L004CF108
    private static void L004CF108()
    {
        short ebp2Ca = AlliedVariables.s_V0x00541E18.m0000C8;

        for (int ebp2C = 0; ebp2C < ebp2Ca; ebp2C++)
        {
            int ebp04 = 0x01 + ebp2C;

            byte[] buffer = new byte[0x94];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x94, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
            S0x004CF108 ebpC8 = S0x004CF108.FromByteArray(buffer);

            S0xFGObject ebp14 = new();
            ebp14.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
            ebp14.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(AlliedStrCapitalize(System_LStrFromPCharLen(ebpC8.M000000, 0x10)));
            ebp14.FlightGroupStruct.Cargo = Unit_00511CD0_Proc_00511EB8(AlliedStrCapitalize(System_LStrFromPCharLen(ebpC8.M000010, 0x10)));
            ebp14.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8(AlliedStrCapitalize(System_LStrFromPCharLen(ebpC8.M000020, 0x10)));

            if (ebpC8.m000030 < ebpC8.m000038)
            {
                ebp14.FlightGroupStruct.SpecialCraft = (byte)ebpC8.m000030;
            }
            else
            {
                ebp14.FlightGroupStruct.SpecialCraft = (byte)ebpC8.m000038;
            }

            CraftIdEnum ebp15a = CraftIdEnum._000__1_0;

            switch ((byte)ebpC8.m000032)
            {
                case 0x00:
                case 0x01:
                case 0x03:
                    ebp15a = (CraftIdEnum)((int)CraftIdEnum._000__1_0 + ebpC8.m000032);
                    break;

                case 0x02:
                    if ((byte)ebpC8.m000036 >= 0x0A)
                    {
                        ebp15a = CraftIdEnum._004_0_3_Bwing;
                    }
                    else
                    {
                        ebp15a = CraftIdEnum._002_0_1_Ywing;
                    }

                    break;

                case 0x04:
                case 0x05:
                case 0x06:
                    ebp15a = (CraftIdEnum)((int)CraftIdEnum._005_0_4_TieFighter + (ebpC8.m000032 - 0x04));
                    break;

                case 0x07:
                    ebp15a = CraftIdEnum._016_0_15_AssaultGunboat;
                    break;

                case 0x08:
                    ebp15a = CraftIdEnum._021_0_39_StormtrooperTransport;
                    ebp14.FlightGroupStruct.FormationSpacing = 0x0F;
                    break;

                case 0x09:
                    ebp15a = CraftIdEnum._017_0_37_Shuttle;
                    ebp14.FlightGroupStruct.FormationSpacing = 0x0F;
                    break;

                case 0x0A:
                    ebp15a = CraftIdEnum._024_0_32_Tug;
                    break;

                case 0x0B:
                    ebp15a = CraftIdEnum._026_0_53_ContainerBrick;
                    break;

                case 0x0C:
                    ebp15a = CraftIdEnum._032_0_68_BulkFreighter;
                    break;

                case 0x0D:
                    ebp15a = CraftIdEnum._049_0_89_CalamariCruiserNew;
                    break;

                case 0x0E:
                    ebp15a = CraftIdEnum._042_0_82_Frigate2;
                    break;

                case 0x0F:
                    ebp15a = CraftIdEnum._040_0_80_Corvette2;
                    break;

                case 0x10:
                    ebp15a = CraftIdEnum._053_0_93_ImperialStarDestroyer2;
                    break;

                case 0x11:
                    ebp15a = CraftIdEnum._008_0_7_TieAdvanced;
                    break;

                case 0x12:
                    ebp15a = CraftIdEnum._004_0_3_Bwing;
                    break;
            }

            ebp14.FlightGroupStruct.CraftId = ebp15a;

            byte ebp15 = 0;

            switch ((byte)ebpC8.m000034)
            {
                case 0x00:
                    {
                        switch (ebpC8.m000032)
                        {
                            case 0x00:
                            case 0x01:
                            case 0x02:
                            case 0x03:
                            case 0x0D:
                            case 0x12:
                                ebp15 = 0;
                                ebp14.FlightGroupStruct.Team = 0;
                                ebp14.FlightGroupStruct.Radio = 0x01;
                                break;

                            case 0x04:
                            case 0x05:
                            case 0x06:
                            case 0x07:
                            case 0x0C:
                            case 0x10:
                            case 0x11:
                                ebp15 = 0x01;
                                ebp14.FlightGroupStruct.Team = 0x01;
                                break;

                            default:
                                ebp15 = 0x02;
                                ebp14.FlightGroupStruct.Team = 0x02;
                                break;
                        }

                        break;
                    }

                case 0x01:
                    {
                        ebp15 = 0;
                        ebp14.FlightGroupStruct.Team = 0;
                        break;
                    }

                case 0x02:
                    {
                        ebp15 = 0x01;
                        ebp14.FlightGroupStruct.Team = 0x01;
                        break;
                    }

                case 0x03:
                    {
                        ebp15 = 0x02;
                        ebp14.FlightGroupStruct.Team = 0x02;
                        break;
                    }
            }

            ebp14.FlightGroupStruct.Iff = ebp15;

            switch (ebpC8.m000036)
            {
                case 0x00:
                case 0x01:
                    ebp14.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Normal;
                    break;

                case 0x02:
                case 0x03:
                    if (ebpC8.m000036 < 0x0A)
                    {
                        ebp14.FlightGroupStruct.Status1 = (FlightGroupStatusEnum)ebpC8.m000036;
                    }

                    break;
            }

            ebp14.FlightGroupStruct.CraftsCount = (byte)ebpC8.m000038;
            ebp14.FlightGroupStruct.WavesCount = ebpC8.m00003A;

            if (ebpC8.m000036 != 0x01 && ebpC8.m000036 != 0x0B)
            {
                switch (ebpC8.m000032)
                {
                    case 0x01:
                    case 0x02:
                    case 0x06:
                        ebp14.FlightGroupStruct.WarheadType = 0x04;
                        break;

                    case 0x03:
                    case 0x07:
                        ebp14.FlightGroupStruct.WarheadType = 0x03;
                        break;
                }
            }

            switch (ebpC8.m000032)
            {
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x05:
                case 0x06:
                case 0x07:
                case 0x11:
                    ebp14.FlightGroupStruct.AbortCondition = 0x04;
                    break;

                case 0x04:
                    ebp14.FlightGroupStruct.AbortCondition = 0x09;
                    break;
            }

            switch (ebpC8.m000032)
            {
                case 0x04:
                case 0x05:
                case 0x06:
                    if (ebpC8.m000036 == 0x03)
                    {
                        ebp14.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Normal;
                    }

                    break;

                default:
                    ebp14.FlightGroupStruct.Markings = ebpC8.m00008C;
                    break;
            }

            if (ebpC8.m00003C > 0)
            {
                ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[0].VariableType = TieClassEnum.FlightGroup;
                ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[0].Amount = TieAmountEnum.AtLeastOne;

                if (ebpC8.m00003C == 0x02)
                {
                    ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[0].Amount = TieAmountEnum._100Percent;
                }

                ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[0].Variable = ebpC8.m000040;
            }

            switch (ebpC8.m00003C)
            {
                case 0x00:
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x05:
                    ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[0].Condition = (TieConditionEnum)ebpC8.m00003C;
                    break;

                case 0x04:
                case 0x06:
                    ebp14.FlightGroupStruct.ArrivalTrigger1.Triggers[0].Condition = (TieConditionEnum)(ebpC8.m00003C + 0x02);
                    break;
            }

            short cx0 = 0;

            if (ebpC8.m00003E < 0x15)
            {
                ebp14.FlightGroupStruct.ArrivalDelayMinutes = (byte)ebpC8.m00003E;
            }
            else
            {
                cx0 = (short)((ebpC8.m00003E - 0x14) * 0x06);
            }

            if (cx0 > 0x3B)
            {
                ebp14.FlightGroupStruct.ArrivalDelayMinutes = (byte)(cx0 / 0x3C);
                ebp14.FlightGroupStruct.ArrivalDelaySeconds = (byte)(cx0 % 0x3C);
            }
            else
            {
                ebp14.FlightGroupStruct.ArrivalDelaySeconds = (byte)cx0;
            }

            ebp14.FlightGroupStruct.StartFgUsed = (byte)(ebpC8.m000044 ^ 0x01);
            ebp14.FlightGroupStruct.PrimaryStopFgUsed = (byte)(ebpC8.m000046 ^ 0x01);

            if (ebpC8.m000042 > AlliedVariables.s_V0x00541E18.m0000C8)
            {
                ebp14.FlightGroupStruct.StartFg = 0;
            }
            else
            {
                ebp14.FlightGroupStruct.StartFg = (byte)ebpC8.m000042;
            }

            ebp14.FlightGroupStruct.PrimaryStopFg = ebp14.FlightGroupStruct.StartFg;

            for (int ecx = 0; ecx < 0x03; ecx++)
            {
                if (ecx == 0x01)
                {
                    ebp14.m00147C[ecx].M000000[0] = (short)-ebpC8.m000048[ecx * 7 + 0];
                    ebp14.FlightGroupStruct.StartPoints[0].Position[ecx] = (short)-ebpC8.m000048[ecx * 7 + 0];
                }
                else
                {
                    ebp14.m00147C[ecx].M000000[0] = ebpC8.m000048[ecx * 7 + 0];
                    ebp14.FlightGroupStruct.StartPoints[0].Position[ecx] = ebpC8.m000048[ecx * 7 + 0];
                }
            }

            for (int ecx = 0; ecx < 0x03; ecx++)
            {
                if (ecx == 0x01)
                {
                    ebp14.m00147C[ecx].M000000[3] = ebpC8.m000048[ecx * 7 + 6];
                }
                else
                {
                    ebp14.m00147C[ecx].M000000[3] = ebpC8.m000048[ecx * 7 + 6];
                }
            }

            for (int ecx = 0; ecx < 0x03; ecx++)
            {
                for (int eax = 0; eax < 0x02; eax++)
                {
                    if (ecx == 0x01)
                    {
                        ebp14.m00147C[ecx].M000000[1 + eax] = (short)-ebpC8.m000048[ecx * 7 + 4 + eax];
                    }
                    else
                    {
                        ebp14.m00147C[ecx].M000000[1 + eax] = ebpC8.m000048[ecx * 7 + 4 + eax];
                    }
                }
            }

            ebp14.IsWPEnabled[0] = ebpC8.m000072;
            ebp14.IsWPEnabled[3] = ebpC8.m00007E;

            for (int eax = 0; eax < 0x02; eax++)
            {
                ebp14.IsWPEnabled[1 + eax] = ebpC8.m00007A[eax];
            }

            for (int ebp10 = 0x01; ebp10 < 0x04; ebp10++)
            {
                for (int ebx = 0x01; ebx < 0x04; ebx++)
                {
                    for (int ecx = 0; ecx < 0x03; ecx++)
                    {
                        for (int eax = 0; eax < 0x03; eax++)
                        {
                            if (ecx == 0x01)
                            {
                                ebp14.FlightGroupStruct.Orders[(ebp10 - 1) * 4 + (ebx - 1)].Waypoints[eax].Position[0] = (short)-ebpC8.m000048[ecx * 7 + 1 + eax];
                            }
                            else
                            {
                                ebp14.FlightGroupStruct.Orders[(ebp10 - 1) * 4 + (ebx - 1)].Waypoints[eax].Position[0] = ebpC8.m000048[ecx * 7 + 1 + eax];
                            }
                        }
                    }

                    for (int eax = 0; eax < 0x03; eax++)
                    {
                        ebp14.FlightGroupStruct.Orders[(ebp10 - 1) * 4 + (ebx - 1)].Waypoints[eax].IsUsed = ebpC8.m000074[eax];
                    }
                }
            }

            if (ebp04 == 0x01)
            {
                ebp14.FlightGroupStruct.PlayerNumber = 0x01;
            }

            if (ebpC8.m000082 > 0)
            {
                ebp14.FlightGroupStruct.PlayerCraft = (byte)(ebpC8.m000082 - 1);

                if (ebp04 != 0x01)
                {
                    S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, 0);
                    eax.FlightGroupStruct.PlayerNumber = 0;
                }

                ebp14.FlightGroupStruct.PlayerNumber = 0x01;
                ebp14.FlightGroupStruct.GlobalUnitId = 0x01;
            }

            if (ebpC8.m000082 > 0 || ebp04 == 0x01)
            {
                switch (ebpC8.m000086)
                {
                    case 0x06:
                    case 0x07:
                        ebp14.FlightGroupStruct.Role = Unit_00511CD0_Proc_00511EB8("Escort");
                        break;

                    case 0x08:
                    case 0x09:
                    case 0x0A:
                    case 0x14:
                    case 0x15:
                    case 0x16:
                    case 0x17:
                        ebp14.FlightGroupStruct.Role = Unit_00511CD0_Proc_00511EB8("Attack");
                        break;

                    case 0x12:
                    case 0x13:
                    case 0x18:
                    case 0x19:
                        ebp14.FlightGroupStruct.Role = Unit_00511CD0_Proc_00511EB8("Disable");
                        break;
                }
            }

            ebp14.FlightGroupStruct.AIRank = ebpC8.m000084;

            for (int ebx = 0x01; ebx < 0x05; ebx++)
            {
                ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].OrderId = L004D146C(ebpC8.m000086);

                if (ebpC8.m000090 >= 0 && ebpC8.m000086 != 0x1D)
                {
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ParameterA = (byte)ebpC8.m000090;
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ClassA = TieClassEnum.FlightGroup;
                }

                if (ebpC8.m000092 >= 0 && ebpC8.m000086 != 0x1D)
                {
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ParameterB = (byte)ebpC8.m000092;
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ClassB = TieClassEnum.FlightGroup;
                }

                if (ebpC8.m000090 >= 0 && ebpC8.m000092 >= 0 && ebpC8.m000090 != ebpC8.m000092 && ebpC8.m000086 == 0x1F)
                {
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].Var0 = 0x02;
                }

                switch (ebpC8.m000086)
                {
                    case 0x08:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassA = TieClassEnum.CraftWithOrder;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterA = 0x0A;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        break;

                    case 0x0A:
                    case 0x13:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassA = TieClassEnum.ShipCategory;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterA = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].OrderId = TieOrderIdEnum._07_CapFree;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ClassA = TieClassEnum.ShipCategory;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ParameterA = 0x01;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ClassA = TieClassEnum.ShipCategory;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ParameterA = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        break;

                    case 0x14:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassA = TieClassEnum.ShipType;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterA = 0x15;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        break;

                    case 0x15:
                    case 0x18:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].OrderId = ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].OrderId;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ClassA = TieClassEnum.ShipType;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ParameterA = 0x20;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].PrimaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ClassA = TieClassEnum.ShipType;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ParameterA = 0x28;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4 + 1].SecondaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        break;

                    case 0x16:
                    case 0x19:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.Operator = 0;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassA = TieClassEnum.ShipCategory;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterA = 0x03;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        break;

                    case 0x17:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ClassA = TieClassEnum.ObjectCategory;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ParameterA = 0x02;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ClassB = TieClassEnum.ShipCategory;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].PrimaryTarget.ParameterB = 0x06;
                        break;

                    case 0x1A:
                    case 0x1B:
                    case 0x1C:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ClassB = TieClassEnum.Iff;
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].SecondaryTarget.ParameterB = (byte)L004D1554(L004D1560(ebpC8.m000034, ebpC8.m000032));
                        break;
                }
;

                if (ebpC8.m000086 == 0x1A)
                {
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].Throttle = 0;
                }
                else if (ebpC8.m000088 == 0)
                {
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].Throttle = 0x0A;
                }
                else
                {
                    ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].Throttle = (byte)(ebpC8.m000088 + 1);
                }

                switch (ebpC8.m000086)
                {
                    case 0x0D:
                    case 0x0E:
                    case 0x0F:
                    case 0x10:
                    case 0x11:
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].Var0 = (byte)L004D22CC(ebpC8.m000088 * 0x3C);
                        ebp14.FlightGroupStruct.Orders[(ebx - 1) * 4].Throttle = 0x0A;
                        break;
                }
            }

            ebp14.FlightGroupStruct.FormationType = ebpC8.m000080;
            ebp14.FlightGroupStruct.Goals[0].AppliesToTeams[0] = 0x01;

            switch (ebpC8.m00008E)
            {
                case 0x05:
                case 0x06:
                case 0x07:
                case 0x08:
                case 0x0E:
                    ebp14.FlightGroupStruct.Goals[0].Amount = TieAmountEnum.AtLeastSpecial;
                    break;

                case 0x09:
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x0F:
                    ebp14.FlightGroupStruct.Goals[0].Amount = TieAmountEnum._50Percent;
                    break;
            }

            string ebp28_3 = AlliedVariables.s_Strings_Short.GetText((int)ebp14.FlightGroupStruct.CraftId) + " " + AlliedStrCapitalize(System_LStrFromPCharLen(ebpC8.M000000, 0x10)) + ": ";
            string ebp28_2 = string.Empty;
            string ebp28_1 = string.Empty;
            string ebp28_0 = string.Empty;

            switch (ebpC8.m00008E)
            {
                case 0x05:
                    ebp28_2 = "Special ship destroyed";
                    ebp28_1 = "Special ship can't be destroyed";
                    ebp28_0 = "Destroy special ship";
                    break;

                case 0x06:
                    ebp28_2 = "special ship survived";
                    ebp28_1 = "special ship destroyed";
                    ebp28_0 = "special ship from being destroyed";
                    break;

                case 0x07:
                    ebp28_2 = "special ship captured";
                    ebp28_1 = "special ship can't be captured";
                    ebp28_0 = "Capture special ship ";
                    break;

                case 0x08:
                    ebp28_2 = "special ship boarded";
                    ebp28_1 = "special ship can't be boarded";
                    ebp28_0 = "Special ship must be boarded";
                    break;

                case 0x0E:
                    ebp28_2 = "special ship identified";
                    ebp28_1 = "special ship can't be identified";
                    ebp28_0 = "Identify special ship ";
                    break;
            }

            switch (ebpC8.m00008E)
            {
                case 0x03:
                    ebp14.m000E42.M000000[0].M000000 = Unit_00511CD0_Proc_0051213C(ebp28_3 + string.Empty);
                    ebp14.m000E42.M000000[0].M000080 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "can't be captured");
                    ebp14.m000E42.M000000[0].M000040 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "captured");
                    break;

                case 0x05:
                case 0x06:
                case 0x07:
                case 0x08:
                case 0x0E:
                    ebp14.m000E42.M000000[0].M000000 = Unit_00511CD0_Proc_0051213C(ebp28_3 + ebp28_0);
                    ebp14.m000E42.M000000[0].M000080 = Unit_00511CD0_Proc_0051213C(ebp28_3 + ebp28_1);
                    ebp14.m000E42.M000000[0].M000040 = Unit_00511CD0_Proc_0051213C(ebp28_3 + ebp28_2);
                    break;
            }

            switch (ebpC8.m00008E)
            {
                case 0x03:
                    ebp14.m000E42.M000000[0].M000000 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "Capture");
                    ebp14.m000E42.M000000[0].M000080 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "can't be captured");
                    ebp14.m000E42.M000000[0].M000040 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "captured");
                    break;

                case 0x0B:
                    ebp14.m000E42.M000000[0].M000000 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "Capture 50%");
                    ebp14.m000E42.M000000[0].M000080 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "50% can't be captured");
                    ebp14.m000E42.M000000[0].M000040 = Unit_00511CD0_Proc_0051213C(ebp28_3 + "50% captured");
                    break;
            }

            switch (ebpC8.m00008E)
            {
                case 0x01:
                case 0x05:
                case 0x09:
                    ebp14.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.Destroyed;
                    break;

                case 0x02:
                case 0x06:
                case 0x0A:
                    ebp14.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.CompletedMission;
                    break;

                case 0x03:
                case 0x07:
                case 0x0B:
                    ebp14.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.BePickedUp;
                    break;

                case 0x04:
                case 0x08:
                case 0x0C:
                    ebp14.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.Boarded;
                    break;

                case 0x0D:
                case 0x0E:
                case 0x0F:
                    ebp14.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.Inspected;
                    break;

                case 0x10:
                    ebp14.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.Created;
                    break;
            }
;

            if (ebpC8.m000032 == 0x0B || ebpC8.m000086 == 0)
            {
                switch (ebpC8.m00008E)
                {
                    case 0x02:
                    case 0x06:
                    case 0x0A:
                        ebp14.FlightGroupStruct.Goals[0].GoalType = TieFGGoalTypeEnum.Loss;
                        ebp14.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.Destroyed;
                        break;
                }
            }

            switch (ebpC8.m00008E)
            {
                case 0x01:
                case 0x03:
                case 0x04:
                case 0x05:
                case 0x08:
                case 0x09:
                case 0x0B:
                case 0x0C:
                case 0x0D:
                case 0x0F:
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.PRImary;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    break;

                case 0x02:
                case 0x06:
                case 0x0A:
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.MISsionCritical;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    break;
            }

            ebp28_3 = "[X Wing]    ";

            switch (ebpC8.m000086)
            {
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x1E:
                case 0x1F:
                case 0x20:
                    ebp28_3 += "SShip ";
                    break;
            }

            switch (ebpC8.m000086)
            {
                case 0x00:
                    ebp28_3 += "Stationary";
                    break;

                case 0x01:
                    ebp28_3 += "Go Home";
                    break;

                case 0x02:
                case 0x04:
                case 0x1C:
                    ebp28_3 += "Circle ";
                    break;

                case 0x03:
                case 0x05:
                case 0x1B:
                    ebp28_3 += "Fly once ";
                    break;

                case 0x06:
                    ebp28_3 += "Close ";
                    break;

                case 0x07:
                    ebp28_3 += "Loose ";
                    break;

                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x14:
                case 0x15:
                case 0x16:
                case 0x17:
                    ebp28_3 += "Attack ";
                    break;

                case 0x0B:
                    ebp28_3 += "Rendezvous";
                    break;

                case 0x0C:
                    ebp28_3 += "Disabled";
                    break;

                case 0x0D:
                case 0x0E:
                case 0x0F:
                case 0x10:
                case 0x11:
                    ebp28_3 += "Board to ";
                    break;

                case 0x12:
                case 0x13:
                case 0x18:
                case 0x19:
                    ebp28_3 += "Disable ";
                    break;
            }
;

            switch (ebpC8.m000086)
            {
                case 0x02:
                case 0x03:
                    ebp28_3 += "and ignore";
                    break;

                case 0x04:
                case 0x05:
                    ebp28_3 += "and evade";
                    break;

                case 0x06:
                case 0x07:
                    ebp28_3 += "escort";
                    break;

                case 0x08:
                    ebp28_3 += "escorters";
                    break;

                case 0x09:
                case 0x12:
                    ebp28_3 += "primary and secondary targets";
                    break;

                case 0x0A:
                case 0x13:
                    ebp28_3 += "all enemies (means fighters, TRNs, SHUs)";
                    break;

                case 0x0D:
                    ebp28_3 += "deliver";
                    break;

                case 0x0E:
                    ebp28_3 += "take";
                    break;

                case 0x0F:
                    ebp28_3 += "exchange";
                    break;

                case 0x10:
                    ebp28_3 += "capture";
                    break;

                case 0x11:
                    ebp28_3 += "destroy";
                    break;

                case 0x14:
                    ebp28_3 += "Transports";
                    break;

                case 0x15:
                case 0x18:
                    ebp28_3 += "Freighters (includes CRVs)";
                    break;

                case 0x16:
                case 0x19:
                    ebp28_3 += "Starships";
                    break;

                case 0x17:
                    ebp28_3 += "satellites and mines";
                    break;

                case 0x1A:
                    ebp28_3 += "sit and fire";
                    break;

                case 0x1B:
                case 0x1C:
                    ebp28_3 += "through WPs";
                    break;

                case 0x1D:
                    ebp28_3 += "await return";
                    break;

                case 0x1E:
                    ebp28_3 += "await launch of targets";
                    break;

                case 0x1F:
                    ebp28_3 += "await boarding by targets";
                    break;

                case 0x20:
                    ebp28_3 += "wait until targets?";
                    break;
            }

            ebp28_3 += "   -   Pri ";

            if (ebpC8.m000090 < 0)
            {
                ebp28_3 += " <none>";
            }
            else
            {
                ebp28_3 += "#" + (ebpC8.m000090 + 1).ToString(CultureInfo.InvariantCulture);
            }

            ebp28_3 += ",  Sec ";

            if (ebpC8.m000092 < 0)
            {
                ebp28_3 += "<none>";
            }
            else
            {
                ebp28_3 += "#" + (ebpC8.m000092 + 1).ToString(CultureInfo.InvariantCulture);
            }

            AlliedVariables.s_V0x00542054.Add(ebp28_3);

            if (AlliedVariables.s_UseAutoChkSetting)
            {
                ebp14.AutoLink = true;
            }

            ebp14.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(ebp14.FlightGroupStruct));

            if (AlliedVariables.s_V0x00543C84.M000000[0].M000000)
            {
                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C40))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[0].M000001 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[0].M000001;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C60))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[0].M000002 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[0].M000002;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C20))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[0].M000003 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[0].M000003;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533BC0))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[0].M000004 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[0].M000004;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533BE0))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[0].M000005 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[0].M000005;
                    }
                }
            }

            AlliedVariables.s_FlightGroupObjectsList.Add(ebp14);
            Unit_00513838_Proc_0051D53C(ebp04 - 1);

            AlliedVariables.s_CurrentRegion = 0x01;
            AlliedVariables.s_CurrentOrderInRegion = 0x01;

            MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        }
    }

    // L004D0FBC
    private static void L004D0FBC()
    {
        bool ebp02 = false;
        byte ebp01 = 0;

        int eax1 = AlliedVariables.s_V0x00541E18.m0000CA;

        for (int ebp08 = 0; ebp08 < eax1; ebp08++)
        {
            byte[] buffer = new byte[0x46];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x46, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
            S0x004D0FBC ebp4E = S0x004D0FBC.FromByteArray(buffer);

            S0xFGObject ebx = new();
            ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
            AlliedVariables.s_V0x00542054.Add("[X Wing]    Hold Steady");
            ebx.FlightGroupStruct.CraftsCount = ebp4E.CraftsCount;
            ebx.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8(AlliedStrCapitalize(System_LStrFromPCharLen(ebp4E.Name, 0x32)));

            if (ebp4E.m000032 >= 0x22 && ebp4E.m000032 <= 0x31)
            {
                ebp02 = true;
                ebx.FlightGroupStruct.CraftId = CraftIdEnum._183_9001_1100_ResData_Backdrop;
                ebx.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8("1.0 1.0 1.0");
                ebx.FlightGroupStruct.Cargo = Unit_00511CD0_Proc_00511EB8("1.0");
                ebx.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8("0.5");

                switch (ebp4E.m000032)
                {
                    case 0x22:
                        ebx.FlightGroupStruct.PlanetId = 0x11;
                        break;

                    case 0x23:
                        ebx.FlightGroupStruct.PlanetId = 0x28;
                        break;

                    case 0x24:
                        ebx.FlightGroupStruct.PlanetId = 0x1B;
                        break;

                    case 0x25:
                        ebx.FlightGroupStruct.PlanetId = 0x29;
                        break;

                    case 0x26:
                        ebx.FlightGroupStruct.PlanetId = 0x27;
                        break;

                    case 0x27:
                        ebx.FlightGroupStruct.PlanetId = 0x0F;
                        break;

                    case 0x28:
                        ebx.FlightGroupStruct.PlanetId = 0x11;
                        ebx.FlightGroupStruct.GlobalCargoIndex = 0x01;
                        break;

                    case 0x29:
                        ebx.FlightGroupStruct.PlanetId = 0x1C;
                        break;

                    case 0x2A:
                    case 0x2B:
                    case 0x2C:
                    case 0x2D:
                    case 0x2E:
                    case 0x2F:
                    case 0x30:
                        ebx.FlightGroupStruct.PlanetId = 0x2A;
                        ebx.FlightGroupStruct.GlobalCargoIndex = (byte)(ebp4E.m000032 - 0x29);
                        break;

                    case 0x31:
                        ebx.FlightGroupStruct.PlanetId = 0x3F;
                        break;
                }

                if (ebp4E.m000032 == 0x31)
                {
                    AlliedVariables.s_V0x00541EE4 = 0x01;
                }
            }
            else if (ebp4E.m000032 >= 0x1A && ebp4E.m000032 <= 0x21)
            {
                ebx.FlightGroupStruct.CraftId = CraftIdEnum._086_1_11_AsteroidHR1;

                if (ebx.FlightGroupStruct.CraftsCount < 0x06)
                {
                    ebx.FlightGroupStruct.CraftsCount = 0x06;
                }
            }

            switch (ebp4E.m000032)
            {
                case 0x12:
                case 0x13:
                case 0x14:
                case 0x15:
                    ebx.FlightGroupStruct.CraftId = CraftIdEnum._075_1_3_MineA;
                    ebx.FlightGroupStruct.Orders[0].PrimaryTarget.ClassA = TieClassEnum.Iff;
                    ebx.FlightGroupStruct.Orders[0].PrimaryTarget.ParameterA = (byte)L004D1554(L004D1560(ebp4E.m000034, ebp4E.m000032));
                    break;

                case 0x16:
                    ebx.FlightGroupStruct.CraftId = CraftIdEnum._070_1_0_SatB;
                    break;

                case 0x17:
                    ebx.FlightGroupStruct.CraftId = CraftIdEnum._083_1_47_BuoyC;
                    break;

                case 0x18:
                    ebx.FlightGroupStruct.CraftId = CraftIdEnum._080_1_7_Probe;
                    break;
            }

            switch ((byte)ebp4E.m000034)
            {
                case 0x00:
                    if (ebp4E.m000032 >= 0x22 && ebp4E.m000032 <= 0x31)
                    {
                        ebp01 = 0x03;
                        ebx.FlightGroupStruct.Team = 0x09;
                    }
                    else
                    {
                        ebp01 = 0x01;
                        ebx.FlightGroupStruct.Team = 0x01;
                    }

                    break;

                case 0x01:
                    ebp01 = 0;
                    ebx.FlightGroupStruct.Team = 0;
                    break;

                case 0x02:
                    ebp01 = 0x01;
                    ebx.FlightGroupStruct.Team = 0x01;
                    break;

                case 0x03:
                    ebp01 = 0x02;
                    ebx.FlightGroupStruct.Team = 0x02;
                    break;
            }

            ebx.FlightGroupStruct.Iff = ebp01;

            for (int edx = 0; edx < 0x03; edx++)
            {
                if (edx == 0x01)
                {
                    ebx.m00147C[edx].M000000[0] = (short)-ebp4E.m00003A[edx];
                    ebx.FlightGroupStruct.StartPoints[0].Position[edx] = (short)-ebp4E.m00003A[edx];
                }
                else
                {
                    ebx.m00147C[edx].M000000[0] = ebp4E.m00003A[edx];
                    ebx.FlightGroupStruct.StartPoints[0].Position[edx] = ebp4E.m00003A[edx];
                }
            }

            ebx.IsWPEnabled[0] = 0x01;
            ebx.FlightGroupStruct.Goals[0].AppliesToTeams[0] = 0x01;

            if (ebp4E.m000036 > 0x03)
            {
                ebx.FlightGroupStruct.Goals[0].Condition = TieConditionEnum.Destroyed;
                ebx.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.PRImary;
                ebx.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
            }

            if (AlliedVariables.s_UseAutoChkSetting)
            {
                ebx.AutoLink = true;
            }

            ebx.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(ebx.FlightGroupStruct));

            AlliedVariables.s_FlightGroupObjectsList.Add(ebx);
        }

        if (!ebp02)
        {
            XvTBoxImpl.TXvTForm__PROC_004CCE90(AlliedVariables.s_TXvTForm_Instance!);
        }

        Unit_00513838_Proc_00520924();
    }

    // L004D13A4
    private static string AlliedStrCapitalize(string eax0)
    {
        if (eax0.Length == 0)
        {
            return string.Empty;
        }

        if (eax0.Length == 1)
        {
            return eax0[..1].ToUpperInvariant();
        }

        return eax0[..1].ToUpperInvariant() + eax0[1..].ToLowerInvariant();
    }

    // L004D146C
    private static TieOrderIdEnum L004D146C(int eax0)
    {
        return eax0 switch
        {
            0x00 or 0x01 => (TieOrderIdEnum)((int)TieOrderIdEnum._00_Stationary + eax0),
            0x02 or 0x03 => TieOrderIdEnum._02_Form,
            0x04 or 0x05 => TieOrderIdEnum._03_FormEvade,
            0x06 or 0x07 => TieOrderIdEnum._09_CapRespond,
            0x08 or 0x09 or 0x0A => TieOrderIdEnum._07_CapFree,
            0x0B => TieOrderIdEnum._04_RendezVous1,
            0x0C => TieOrderIdEnum._05_Disabled,
            0x0D or 0x0E or 0x0F or 0x10 or 0x11 => (TieOrderIdEnum)((int)TieOrderIdEnum._12_BoardToGive + (eax0 - 0x0D)),
            0x12 or 0x13 or 0x18 or 0x19 => TieOrderIdEnum._11_Disable,
            0x14 or 0x15 or 0x16 or 0x17 => TieOrderIdEnum._07_CapFree,
            0x1A => TieOrderIdEnum._26_StarshipAttack,
            0x1B => TieOrderIdEnum._21_StarshipForm,
            0x1C => TieOrderIdEnum._26_StarshipAttack,
            0x1D => TieOrderIdEnum._22_StarshipWaitReturn,
            0x1E => TieOrderIdEnum._23_StarshipWaitCreate,
            _ => TieOrderIdEnum._05_Disabled,
        };
    }

    // L004D1554
    private static int L004D1554(int eax0)
    {
        return eax0 == 1 ? 0 : 1;
    }

    // L004D1560
    private static int L004D1560(int eax0, int edx0)
    {
        int ecx1 = 0;

        switch (eax0)
        {
            case 0x00:
                ecx1 = edx0 switch
                {
                    0x00 or 0x01 or 0x02 or 0x03 or 0x0D => 0,
                    0x04 or 0x05 or 0x06 or 0x07 or 0x0C or 0x10 or 0x11 or 0x12 or 0x13 or 0x14 or 0x15 => 0x01,
                    _ => 0x02,
                };
                break;

            case 0x01:
                ecx1 = 0;
                break;

            case 0x02:
                ecx1 = 0x01;
                break;

            case 0x03:
                ecx1 = 0x02;
                break;
        }

        return ecx1;
    }

    // L004D22CC
    private static int L004D22CC(int eax0)
    {
        if (eax0 <= 0x14)
        {
            return eax0;
        }

        return 0x14 + (eax0 - 0x14) / 0x05;
    }

    // L004D225C
    private static int L004D225C(int eax0, int edx0)
    {
        int ecx = 0;

        switch (eax0)
        {
            case 0x00:
                ecx = edx0 switch
                {
                    0x00 or 0x01 or 0x02 or 0x03 or 0x0D or 0x12 or 0x19 => 0,
                    0x04 or 0x05 or 0x06 or 0x07 or 0x0C or 0x10 or 0x11 => 0x01,
                    _ => 0x02,
                };
                ;

                break;

            case 0x01:
                ecx = 0;
                break;

            case 0x02:
                ecx = 0x01;
                break;

            case 0x03:
                ecx = 0x02;
                break;
        }

        return ecx;
    }

    // L004D2134
    private static int L004D2134(int eax0)
    {
        return eax0 switch
        {
            0x00 or 0x01 or 0x02 or 0x03 => eax0,
            0x04 or 0x05 or 0x06 => eax0 + 1,
            0x07 => 0x10,
            0x08 => 0x15,
            0x09 => 0x11,
            0x0A => 0x18,
            0x0B => 0x1A,
            0x0C => 0x20,
            0x0D => 0x31,
            0x0E => 0x2A,
            0x0F => 0x28,
            0x10 => 0x35,
            0x11 => 0x08,
            0x12 or 0x13 or 0x14 or 0x15 => 0x4B,
            0x16 => 0x46,
            0x17 => 0x53,
            0x18 => 0x50,
            0x19 => 0x04,
            0x1A or 0x1B or 0x1C or 0x1D or 0x1E or 0x1F or 0x20 or 0x21 => 0x56,
            0x27 => 0xE3,
            _ => 0xB7,
        };
    }

    // L004D1E60
    private static void L004D1E60()
    {
        S0xTieBriefing ebp = AlliedVariables.s_Allied_Briefing[0];

        BriefingWindowImpl.TBrfForm__PROC_004FC9F8(AlliedVariables.s_TBrfForm_Instance!);

        for (int ebx0 = 0x01; ebx0 < 0x190;)
        {
            TCommandObject edi0 = new();
            TCommand esp000 = new();

            if (AlliedVariables.s_V0x00542058[ebx0 - 1] == 0x270F)
            {
                esp000.Time = AlliedVariables.s_V0x00542058[ebx0 - 1];
            }
            else
            {
                esp000.Time = (short)Math.Round(AlliedVariables.s_V0x00542058[ebx0 - 1] * 2.5f);
            }

            ebx0++;

            esp000.BriefingCommand = L004D1DD4(AlliedVariables.s_V0x00542058[ebx0 - 1]);
            ebx0++;

            switch (esp000.BriefingCommand)
            {
                case BriefingCommandEnum.BriefingTitle:
                case BriefingCommandEnum.BriefingText:
                    {
                        esp000.Parameter = (short)(AlliedVariables.s_V0x00542058[ebx0 - 1] - 1);
                        ebx0++;
                        break;
                    }

                case BriefingCommandEnum.MoveMap:
                    {
                        esp000.X = AlliedVariables.s_V0x00542058[ebx0 - 1];
                        ebx0++;
                        esp000.Y = AlliedVariables.s_V0x00542058[ebx0 - 1];
                        ebx0++;
                        break;
                    }

                case BriefingCommandEnum.ScaleMap:
                    {
                        esp000.X = (short)Math.Round(AlliedVariables.s_V0x00542058[ebx0 - 1] * 2.6);
                        ebx0++;
                        esp000.Y = (short)Math.Round(AlliedVariables.s_V0x00542058[ebx0 - 1] * 2.6);
                        ebx0++;
                        break;
                    }

                case BriefingCommandEnum.FlightGroupTags1:
                case BriefingCommandEnum.FlightGroupTags2:
                case BriefingCommandEnum.FlightGroupTags3:
                case BriefingCommandEnum.FlightGroupTags4:
                case BriefingCommandEnum.FlightGroupTags5:
                case BriefingCommandEnum.FlightGroupTags6:
                case BriefingCommandEnum.FlightGroupTags7:
                case BriefingCommandEnum.FlightGroupTags8:
                    {
                        esp000.Parameter = AlliedVariables.s_V0x00542058[ebx0 - 1];
                        ebx0++;
                        break;
                    }

                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    {
                        esp000.Parameter = AlliedVariables.s_V0x00542058[ebx0 - 1];
                        ebx0++;
                        esp000.X = AlliedVariables.s_V0x00542058[ebx0 - 1];
                        ebx0++;
                        esp000.Y = AlliedVariables.s_V0x00542058[ebx0 - 1];
                        ebx0++;
                        break;
                    }
            }

            edi0.m000004 = esp000;
            AlliedVariables.s_V0x00543CF4.Add(edi0);

            if (ebx0 > AlliedVariables.s_V0x00542378.m000000)
            {
                break;
            }
        }

        int edi = AlliedVariables.s_V0x00543CF4.Count;

        for (int ebx = 0; ebx < edi; ebx++)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);
            TCommand esp000 = eax1.m000004;
            ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = esp000.Time;
            AlliedVariables.s_V0x005423EC += 1;
            ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = (short)esp000.BriefingCommand;
            AlliedVariables.s_V0x005423EC += 1;

            switch (esp000.BriefingCommand)
            {
                case BriefingCommandEnum.BriefingTitle:
                case BriefingCommandEnum.BriefingText:
                case BriefingCommandEnum.FlightGroupTags1:
                case BriefingCommandEnum.FlightGroupTags2:
                case BriefingCommandEnum.FlightGroupTags3:
                case BriefingCommandEnum.FlightGroupTags4:
                case BriefingCommandEnum.FlightGroupTags5:
                case BriefingCommandEnum.FlightGroupTags6:
                case BriefingCommandEnum.FlightGroupTags7:
                case BriefingCommandEnum.FlightGroupTags8:
                    ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = esp000.Parameter;
                    AlliedVariables.s_V0x005423EC += 1;
                    break;

                case BriefingCommandEnum.MoveMap:
                case BriefingCommandEnum.ScaleMap:
                    ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = esp000.X;
                    AlliedVariables.s_V0x005423EC += 1;
                    ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = esp000.Y;
                    AlliedVariables.s_V0x005423EC += 1;
                    break;

                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = esp000.Parameter;
                    AlliedVariables.s_V0x005423EC += 1;
                    ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = esp000.X;
                    AlliedVariables.s_V0x005423EC += 1;
                    ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = esp000.Y;
                    AlliedVariables.s_V0x005423EC += 1;
                    ebp.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x005423EC - 1] = 0x02;
                    AlliedVariables.s_V0x005423EC += 1;
                    break;
            }

            ebp.BriefingData.BriefingCode.CodeSize = (short)(AlliedVariables.s_V0x005423EC - 1);
            ebp.BriefingData.BriefingCode.Length = (short)Math.Round(AlliedVariables.s_V0x00542380.m000068 * 2.5f);
            ebp.BriefingData.BriefingCode.ForTeam[0] = 0x01;
        }

        AlliedVariables.s_V0x00543CF4.Clear();
    }

    // L004D1DD4
    private static BriefingCommandEnum L004D1DD4(int eax0)
    {
        return eax0 switch
        {
            0x01 => BriefingCommandEnum.BriefingEnd,
            0x0A => BriefingCommandEnum.ClearTextTags,
            0x0B => BriefingCommandEnum.BriefingTitle,
            0x0C => BriefingCommandEnum.BriefingText,
            0x0F => BriefingCommandEnum.MoveMap,
            0x10 => BriefingCommandEnum.ScaleMap,
            0x15 or 0x16 or 0x17 or 0x18 or 0x19 => (BriefingCommandEnum)((int)BriefingCommandEnum.ClearFlightGroupTags + (eax0 - 0x15)),
            0x1A or 0x1B or 0x1C or 0x1D or 0x1E => (BriefingCommandEnum)((int)BriefingCommandEnum.ClearTextTags + (eax0 - 0x1A)),
            _ => BriefingCommandEnum.BriefingEnd,
        };
    }

    // L004D1D50
    private static void L004D1D50()
    {
        AlliedVariables.s_V0x00542044 = 0x01;
        byte[] buffer = new byte[0x06];
        AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x06, AlliedVariables.s_V0x00542040);
        System_L004028C4_CheckError();
        AlliedVariables.s_V0x00542378 = S0x00542038.FromByteArray(buffer);
        L004D1A68();

        int esi0 = AlliedVariables.s_V0x00542378.m000000 + 1;

        for (int esi = 0; esi < esi0; esi++)
        {
            byte[] esp00Buffer = new byte[2];
            AlliedVariables.s_V0x00541EE8.BlockRead(esp00Buffer, 0x02, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            short esp00 = BitConverter.ToInt16(esp00Buffer, 0);
            L004D1A68();
            AlliedVariables.s_V0x00542058[esi] = esp00;
        }

        if (AlliedVariables.s_V0x00542048 == 0x01)
        {
            L004D1E60();
        }
    }

    // L004D1B94
    private static void L004D1B94()
    {
        byte[] buffer = new byte[2];
        AlliedVariables.s_Allied_Briefing[0].BriefingStrings.Clear();
        StringBuilder ebp1009 = new();

        for (int edi0 = 0; edi0 < 0x20; edi0++)
        {
            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            short ebp02 = BitConverter.ToInt16(buffer, 0);
            L004D1A68();
            StringBuilder ebp08 = new();

            for (int ebx = 0; ebx < ebp02; ebx++)
            {
                AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00542040);
                System_L004028C4_CheckError();
                char ebp09 = (char)buffer[0];
                L004D1A68();
                ebp08.Append(System_LStrFromChar(ebp09));

                if (ebp02 > 0xC8)
                {
                    ebp1009.Append(ebp09);
                }
            }

            AlliedVariables.s_Allied_Briefing[0].BriefingStrings.Add(ebp08.ToString());

            for (int ebx = 0; ebx < ebp02; ebx++)
            {
                AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00542040);
                System_L004028C4_CheckError();
                char ebp09 = (char)buffer[0];
                L004D1A68();
            }

            if (ebp02 > 0xC8)
            {
                ebp1009.Append("\r\n\r\n");
            }
        }

        AlliedVariables.s_TieMission_Description.Text = ebp1009.ToString();

        if (AlliedVariables.s_V0x00535E8C != 0)
        {
            MemoWindowImpl.TMemoForm_Proc_004C418C(AlliedVariables.s_TMemoForm_Instance!);
        }
    }

    // L004D1A68
    private static void L004D1A68()
    {
        AlliedVariables.s_V0x0054204C = AlliedVariables.s_V0x00542040 / 2 + AlliedVariables.s_V0x0054204C;
    }

    // L004D1A8C
    private static void L004D1A8C()
    {
        byte[] buffer = new byte[1];
        AlliedVariables.s_Allied_Briefing[0].BriefingTags.Clear();

        for (int ebp01 = 0; ebp01 < 0x20; ebp01++)
        {
            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            byte ebp03 = buffer[0];
            L004D1A68();
            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            byte ebp02 = buffer[0];
            L004D1A68();
            StringBuilder ebp08 = new();

            if (ebp03 > 0 && ebp03 < 0x3C)
            {
                for (int ebx = 0; ebx < ebp03; ebx++)
                {
                    AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x01, AlliedVariables.s_V0x00542040);
                    System_L004028C4_CheckError();
                    char ebp09 = (char)buffer[0];
                    L004D1A68();
                    ebp08.Append(System_LStrFromChar(ebp09));
                }
            }

            AlliedVariables.s_Allied_Briefing[0].BriefingTags.Add(ebp08.ToString());
        }
    }

    // L004D15CC
    public static void Unit_004CEE18_Proc_004D15CC(string eax0)
    {
        int[] epb30C = new int[52];

        for (int eax = 0; eax < 0x32; eax++)
        {
            epb30C[2 + eax] = -1;
        }

        AlliedVariables.s_V0x00542044 = 0x01;
        AlliedVariables.s_V0x0054204C = 0;

        if (!File.Exists(eax0))
        {
            MessageBox_ShowError("Can't find matching .brf file!");
            return;
        }

        byte[] buffer = new byte[0x100];
        S0x00542038[] ebpE2 = ArrayHelpers.CreateArray<S0x00542038>(32);
        short ebp1C = 0;
        short ebp20 = 0;
        short[] ebp122 = new short[32];

        AlliedVariables.s_V0x00541EE8.Assign(eax0);
        AlliedVariables.s_V0x00541EE8.OpenFileForRead(0x01);
        System_L004028C4_CheckError();
        AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x06, AlliedVariables.s_V0x00542040);
        System_L004028C4_CheckError();
        AlliedVariables.s_V0x00542038 = S0x00542038.FromByteArray(buffer.Subarray(0, 0x06));
        AlliedVariables.s_V0x0054204C = AlliedVariables.s_V0x00542040 + AlliedVariables.s_V0x0054204C;

        for (short ebp06 = 0x01; ebp06 <= AlliedVariables.s_V0x00542038.m000004; ebp06++)
        {
            for (short ebp24 = 0; ebp24 < AlliedVariables.s_V0x00542038.m000002; ebp24++)
            {
                S0x00542038 ebp28 = ebpE2[ebp24];

                AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x06, AlliedVariables.s_V0x00542040);
                System_L004028C4_CheckError();
                S0x00542038 ebp0C = S0x00542038.FromByteArray(buffer.Subarray(0, 0x06));
                AlliedVariables.s_V0x0054204C = AlliedVariables.s_V0x00542040 + AlliedVariables.s_V0x0054204C;

                if (ebp06 == 0x02)
                {
                    ebp28.m000000 = ebp0C.m000000;
                    ebp28.m000002 = ebp0C.m000002;
                    ebp28.m000004 = ebp0C.m000004;
                }
            }
        }

        for (short ebp22 = 0; ebp22 < AlliedVariables.s_V0x00542038.m000002; ebp22++)
        {
            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x40, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            for (int i = 0; i < 32; i++)
            {
                ebp122[i] = BitConverter.ToInt16(buffer, i * 0x02);
            }

            L004D1A68();
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 0] = 0;
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 1] = 0x1A;
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 2] = ebp20;
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 3] = (short)L004D2134(ebp122[0]);
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 4] = (short)L004D225C(ebp122[1], ebp122[0]);
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 5] = 0;
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 6] = 0x1C;
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 7] = ebp20;
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 8] = ebpE2[ebp22].m000000;
            AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.m00000A[ebp1C + 9] = ebpE2[ebp22].m000002;
            epb30C[ebp22] = ebp20;
            ebp20++;
            ebp1C += 0x0A;
        }

        AlliedVariables.s_V0x005423EC = ebp1C + 1;
        AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x6A, AlliedVariables.s_V0x00542040);
        System_L004028C4_CheckError();
        AlliedVariables.s_V0x00542380 = S0x00542380.FromByteArray(buffer.Subarray(0, 0x6A));
        L004D1A68();

        if (AlliedVariables.s_V0x00542380.m000000 > 0x02)
        {
            MessageBox_ShowError(".brf file has strange structure. Unable to convert it.");
        }
        else
        {
            for (AlliedVariables.s_V0x00542048 = 0x01; AlliedVariables.s_V0x00542048 <= AlliedVariables.s_V0x00542380.m000066; AlliedVariables.s_V0x00542048 += 1)
            {
                L004D1D50();
            }

            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x06, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            S0x00542038 ebp16 = S0x00542038.FromByteArray(buffer.Subarray(0, 0x06));
            L004D1A68();
            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0xC0, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            L004D1A68();

            for (short ebp22 = 0; ebp22 < AlliedVariables.s_V0x00542038.m000002; ebp22++)
            {
                AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x5A, AlliedVariables.s_V0x00542040);
                System_L004028C4_CheckError();
                L004D1A68();
            }

            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            L004D1A68();
            L004D1A8C();
            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            L004D1A68();
            AlliedVariables.s_V0x00541EE8.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00542040);
            System_L004028C4_CheckError();
            L004D1A68();
            L004D1B94();
        }

        AlliedVariables.s_V0x00541EE8.Close();
        System_L004028C4_CheckError();

        AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.Time = 0;
        AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.Index = 0;
        AlliedVariables.s_Allied_Briefing[0].BriefingData.BriefingCode.Title = 0;
    }

    // L0051D348
    public static bool Unit_00513838_Proc_0051D348_Returns_0x01(TStrings eax0, string edx0)
    {
        return true;
    }

    // L0051F778
    public static void Unit_00513838_Proc_0051F778(string eax0)
    {
        if (!File.Exists(eax0))
        {
            return;
        }

        AlliedVariables.s_TieFileHandle.Assign(eax0);
        AlliedVariables.s_TieFileHandle.OpenFileForRead(0x01);
        System_L004028C4_CheckError();

        byte[] buffer = new byte[0x02];
        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x02, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        TieFileVersionEnum ebp06 = (TieFileVersionEnum)BitConverter.ToInt16(buffer, 0);

        AlliedVariables.s_V0x00535EA8 = 0;
        AlliedVariables.s_V0x00535EAC = 0;

        switch (ebp06)
        {
            case TieFileVersionEnum.XWing:
                {
                    byte[] ebp941E = new byte[0x16];
                    AlliedVariables.s_TieFileHandle.BlockRead(ebp941E, 0x16, AlliedVariables.s_V0x00543C40);
                    System_L004028C4_CheckError();

                    byte[] ebp9582 = new byte[0x40];
                    for (int ebx = 0; ebx < 0x06; ebx++)
                    {
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp9582, 0x40, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    AlliedVariables.s_TieFileHandle.BlockRead(ebp9582, 0x02, AlliedVariables.s_V0x00543C40);
                    System_L004028C4_CheckError();
                    AlliedVariables.s_TieFileHandle.BlockRead(ebp9582, 0x30, AlliedVariables.s_V0x00543C40);
                    System_L004028C4_CheckError();

                    for (int ebx = 0; ebx < BitConverter.ToInt16(ebp941E, 0x00); ebx++)
                    {
                        byte[] ebp9542 = new byte[0x124];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp9542, 0x124, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();

                        if (BitConverter.ToInt16(ebp9542, 0x11E) != 0x01 || AlliedVariables.s_V0x00535EAC >= 0x33)
                        {
                            continue;
                        }

                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[0 + AlliedVariables.s_V0x00535EA8] = 0;
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[1 + AlliedVariables.s_V0x00535EA8] = 0x1A;
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[2 + AlliedVariables.s_V0x00535EA8] = (short)AlliedVariables.s_V0x00535EAC;
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[3 + AlliedVariables.s_V0x00535EA8] = ebp9542[0x032];
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[4 + AlliedVariables.s_V0x00535EA8] = ebp9542[0x037];
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[5 + AlliedVariables.s_V0x00535EA8] = 0;
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[6 + AlliedVariables.s_V0x00535EA8] = 0x1C;
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[7 + AlliedVariables.s_V0x00535EA8] = (short)AlliedVariables.s_V0x00535EAC;
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[8 + AlliedVariables.s_V0x00535EA8] = BitConverter.ToInt16(ebp9542, 0x0C4);
                        AlliedVariables.s_V0x00535EB0.BriefingData.BriefingCode.m00000A[9 + AlliedVariables.s_V0x00535EA8] = BitConverter.ToInt16(ebp9542, 0x0E2);

                        AlliedVariables.s_V0x0053BC94[ebx] = AlliedVariables.s_V0x00535EAC;
                        AlliedVariables.s_V0x00535EAC += 1;
                        AlliedVariables.s_V0x00535EA8 += 0x0A;
                    }

                    for (int ebx = 0; ebx < BitConverter.ToInt16(ebp941E, 0x02); ebx++)
                    {
                        byte[] ebp95DC = new byte[0x5A];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp95DC, 0x5A, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    for (int ebx = 0; ebx < 0x03; ebx++)
                    {
                        byte[] ebp95F8 = new byte[0x1C];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp95F8, 0x1C, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    //byte ebp95EF = 0x01;
                    L004C6904();
                    break;
                }

            case TieFileVersionEnum.XvT:
            case TieFileVersionEnum.Bop:
                {
                    AlliedVariables.s_V0x0053BDB4 = 0;
                    AlliedVariables.s_V0x0053BDB0 = 0;
                    AlliedVariables.s_V0x0053BDB8 = 0x01;

                    byte[] ebp969A = new byte[0xA2];
                    AlliedVariables.s_TieFileHandle.BlockRead(ebp969A, 0xA2, AlliedVariables.s_V0x00543C40);
                    System_L004028C4_CheckError();

                    switch (ebp969A[0x62])
                    {
                        case 0x02:
                        case 0x03:
                        case 0x04:
                            {
                                TModalResultEnum ax1 = MessageBox_ShowConfirmation("Import Briefing for Team 1? (If not, Briefing 2 will be imported)", null);

                                if (ax1 == TModalResultEnum.Yes)
                                {
                                    AlliedVariables.s_V0x0053BDB8 = 0x01;
                                }
                                else
                                {
                                    AlliedVariables.s_V0x0053BDB8 = 0x02;
                                }

                                break;
                            }
                    }

                    for (int ebx = 0; ebx < BitConverter.ToInt16(ebp969A, 0x0000); ebx++)
                    {
                        byte[] ebp9BFC = new byte[0x562];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp9BFC, 0x562, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();

                        if (BitConverter.ToInt16(ebp9BFC, 0x506) != 0x01 || AlliedVariables.s_V0x0053BDB4 >= 0x33)
                        {
                            continue;
                        }

                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[0 + AlliedVariables.s_V0x0053BDB0] = 0;
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[1 + AlliedVariables.s_V0x0053BDB0] = 0x1A;
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[2 + AlliedVariables.s_V0x0053BDB0] = (short)AlliedVariables.s_V0x0053BDB4;
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[3 + AlliedVariables.s_V0x0053BDB0] = ebp9BFC[0x52];
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[4 + AlliedVariables.s_V0x0053BDB0] = ebp9BFC[0x57];
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[5 + AlliedVariables.s_V0x0053BDB0] = 0;
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[6 + AlliedVariables.s_V0x0053BDB0] = 0x1C;
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[7 + AlliedVariables.s_V0x0053BDB0] = (short)AlliedVariables.s_V0x0053BDB4;
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[8 + AlliedVariables.s_V0x0053BDB0] = BitConverter.ToInt16(ebp9BFC, 0x482);
                        AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[9 + AlliedVariables.s_V0x0053BDB0] = BitConverter.ToInt16(ebp9BFC, 0x4AE);

                        AlliedVariables.s_V0x0053BC94[ebx] = AlliedVariables.s_V0x0053BDB4;
                        AlliedVariables.s_V0x0053BDB4 += 1;
                        AlliedVariables.s_V0x0053BDB0 += 0x0A;
                    }

                    for (int ebx = 0; ebx < BitConverter.ToInt16(ebp969A, 0x0002); ebx++)
                    {
                        byte[] ebp32D4 = new byte[0x74];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp32D4, 0x74, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    byte[] ebp23F4 = new byte[0x1306];
                    AlliedVariables.s_TieFileHandle.BlockRead(ebp23F4, 0x500, AlliedVariables.s_V0x00543C40);
                    System_L004028C4_CheckError();
                    AlliedVariables.s_TieFileHandle.BlockRead(ebp23F4, 0x1306, AlliedVariables.s_V0x00543C40);
                    System_L004028C4_CheckError();

                    XvTBoxImpl.TXvTForm__PROC_004CC790(AlliedVariables.s_TXvTForm_Instance!);
                    break;
                }

            case TieFileVersionEnum.XWA:
                {
                    byte[] ebp23F4 = new byte[0x23EE];
                    AlliedVariables.s_TieFileHandle.BlockRead(ebp23F4, 0x23EE, AlliedVariables.s_V0x00543C40);
                    System_L004028C4_CheckError();

                    for (int ebx = 0; ebx < BitConverter.ToInt16(ebp23F4, 0x0000); ebx++)
                    {
                        byte[] ebp3232 = new byte[0xE3E];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp3232, 0xE3E, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    for (int ebx = 0; ebx < BitConverter.ToInt16(ebp23F4, 0x0002); ebx++)
                    {
                        byte[] ebp32D4 = new byte[0xA2];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp32D4, 0xA2, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    for (int ebx = 0; ebx < 0x0A; ebx++)
                    {
                        byte[] ebp3444 = new byte[0x170];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp3444, 0x170, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    for (int ebx = 0; ebx < 0x0A; ebx++)
                    {
                        byte[] ebp362C = new byte[0x1E7];
                        AlliedVariables.s_TieFileHandle.BlockRead(ebp362C, 0x1E7, AlliedVariables.s_V0x00543C40);
                        System_L004028C4_CheckError();
                    }

                    byte[] ebp4FF4 = new byte[0x19C8];

                    Allied_ReadTieMission_XWA_Briefing(AlliedVariables.s_AlliedForm1Window!);
                    break;
                }
        }

        AlliedVariables.s_TieFileHandle.Close();
        System_L004028C4_CheckError();
    }

    // L005140A8
    private static void Unit_00513838_Proc_005140A8()
    {
        L005146CC();

        AlliedVariables.s_V0x00543B54 = 0;
        AlliedVariables.s_V0x00543B55 = 0;

        if (AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count > 0)
        {
            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, 0, true);

            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, 0);
            eax.m001479 = 0x01;
        }
    }

    // L005173F4
    private static byte L005173F4()
    {
        byte[] ecx = AlliedVariables.s_V0x00543C78;
        byte al = 0;
        byte dl = 0;

        while (true)
        {
            if (ecx[al] == 0)
            {
                dl = al;
            }

            al++;

            if (al > 0x07 || ecx[al - 1] == 0)
            {
                break;
            }
        }

        if (dl > 0x0A)
        {
            dl = 0x0A;
        }

        return dl;
    }

    // L00515534
    private static void L00515534()
    {
        StdCtrls_TCustomListBox_SetItems(AlliedVariables.s_AlliedForm1Window!.FGMirror, AlliedVariables.s_AlliedForm1Window!.ShipList.Items);

        int ebx0 = AlliedVariables.s_AlliedForm1Window!.FGMirror.Items.Count;

        for (int esi0 = 0; esi0 < ebx0; esi0++)
        {
            if (StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, esi0))
            {
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.FGMirror, esi0, true);
            }
        }

        AlliedVariables.s_AlliedForm1Window!.ShipList.Clear();
        AlliedVariables.s_V0x00543BC0.Clear();

        int eax0 = AlliedVariables.s_FlightGroupObjectsList.Count;

        if (eax0 > 0)
        {
            for (int esi0 = 0; esi0 < eax0; esi0++)
            {
                S0xFGObject edi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi0);

                string ebp18_5 = Form1WindowImpl.L00525C14(AlliedVariables.s_AlliedForm1Window!, edi);
                string ebp18_4 = AlliedGetCraftShortString(edi.FlightGroupStruct.CraftId) + " " + System_LStrFromPCharLen(edi.FlightGroupStruct.Name, 0x14);

                if (edi.FlightGroupStruct.PlayerNumber != 0)
                {
                    ebp18_4 += " (" + edi.FlightGroupStruct.PlayerNumber.ToString(CultureInfo.InvariantCulture) + ")";
                }

                AlliedVariables.s_AlliedForm1Window!.ShipList.AddItem(ebp18_5);
                AlliedVariables.s_V0x00543BC0.Add(ebp18_4);
            }

            if (AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count > 0)
            {
                if (AlliedVariables.s_AlliedForm1Window!.FGMirror.Items.Count > 0)
                {
                    int eax1 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

                    for (int esi0 = 0; esi0 < eax1; esi0++)
                    {
                        if (esi0 >= AlliedVariables.s_AlliedForm1Window!.FGMirror.Items.Count)
                        {
                            continue;
                        }

                        if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.FGMirror, esi0))
                        {
                            continue;
                        }

                        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, esi0, true);
                    }
                }
            }
        }
    }

    // L00517520
    public static void L00517520(int eax0, byte edx0, ref byte ecx0)
    {
        switch (edx0)
        {
            case 0x44:
                if (ecx0 == eax0)
                {
                    ecx0 += 1;
                }
                else if (ecx0 == eax0 + 1)
                {
                    ecx0 -= 1;
                }

                break;

            case 0x55:
                if (ecx0 == eax0)
                {
                    ecx0 -= 1;
                }
                else if (ecx0 == eax0 - 1)
                {
                    ecx0 += 1;
                }

                break;

            case 0x58:
                if (ecx0 >= eax0)
                {
                    ecx0 -= 1;
                }

                break;
        }
    }

    // L0051C570
    public static int Unit_00513838_Proc_0051C570(int eax0, int edx0)
    {
        if (edx0 <= 0)
        {
            return (int)Math.Round(eax0 * AlliedVariables.s_V0x00543CDC);
        }

        if (edx0 == 0x01)
        {
            return (int)Math.Round(eax0 * AlliedVariables.s_V0x00543CD4);
        }

        return eax0;
    }

    // L00517424
    private static byte L00517424()
    {
        byte ebp01 = 0;
        byte bl = 0;

        while (true)
        {
            S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, bl);
            string ebp10_2 = System_LStrFromPCharLen(eax1.Team.Name, 0x10).ToUpperInvariant();

            if (AlliedVariables.s_V0x00543C78[bl] == 0x01)
            {
                ebp01 = bl;
            }
            else
            {
                if (string.Equals(ebp10_2, "IMPERIAL", StringComparison.Ordinal) || string.Equals(ebp10_2, "EMPIRE", StringComparison.Ordinal))
                {
                    ebp01 = bl;
                }
            }

            bl++;

            if (bl > 0x07 || AlliedVariables.s_V0x00543C78[bl - 1] == 0x01)
            {
                break;
            }
        }

        if (ebp01 > 0x0A)
        {
            ebp01 = 0x0A;
        }

        return ebp01;
    }

    // L0051C274
    private static string Unit_00513838_Proc_0051C274(int eax0)
    {
        int ebx = eax0;
        string ebp04 = string.Empty;

        int esi = 0;

        if (ebx > 0x38 && ebx < 0x55)
        {
            ebx -= 0x37;
        }

        if (ebx > 0x55)
        {
            esi = -2;
        }
        else if (ebx >= 0x23)
        {
            esi = (int)Math.Round(ebx * 4.6);
        }
        else if (ebx >= 0x0F)
        {
            esi = (int)Math.Round(ebx * 4.5f);
        }
        else if (ebx >= 0x08)
        {
            esi = (int)Math.Round(ebx * 4.2);
        }
        else if (ebx >= 0x06)
        {
            esi = (int)Math.Round(ebx * 4.0);
        }
        else
        {
            switch (ebx)
            {
                case 0x00:
                    esi = -1;
                    break;

                case 0x01:
                    esi = 0;
                    break;

                case 0x02:
                    esi = 0x05;
                    break;

                case 0x03:
                    esi = 0x09;
                    break;

                case 0x04:
                    esi = 0x0E;
                    break;

                case 0x05:
                    esi = 0x13;
                    break;
            }
        }

        if (esi == -1)
        {
            ebp04 = " = (default)";
        }
        else if (esi == -2)
        {
            ebp04 = " = (untested)";
        }
        else if (esi == 0)
        {
            ebp04 = " = 0:00";
        }
        else
        {
            ebp04 = " = " + Allied_TimeInSeconds_ToMinutesSecondsString(esi) + " (approx)";
        }

        return ebp04;
    }
}
