using AlliED.Extensions;
using System.Globalization;
using System.IO;
using System.Windows;

namespace AlliED.Impl.ViewsImpl;

internal static class ErrorBoxImpl
{
    public static void Register(ErrorBox window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(ErrorBox window)
    {
        window.Closed += (s, e) => TErrForm_FormClose(window);
        window.OmitWarnsChk.Click += (s, e) => TErrForm_OmitWarnsChkClick(window);
    }

    // L00511A48
    private static void FormCreate(ErrorBox ErrForm)
    {
        ErrForm.ErrMemo.Clear();
        ErrForm.ErrMemo.SetItems(AlliedVariables.s_TErrForm_Items1);
        ErrForm.OmitWarnsChk.IsChecked = AlliedVariables.s_TErrForm_OmitWarns;

        if (!AlliedVariables.s_TErrForm_OmitWarns)
        {
            int esi = AlliedVariables.s_TErrForm_Items2.GetCount();

            for (int ebp04 = 0; ebp04 < esi; ebp04++)
            {
                string ebp08 = AlliedVariables.s_TErrForm_Items2.GetText(ebp04);
                ErrForm.ErrMemo.AddLine(ebp08);
            }
        }
    }

    // L00511B48
    private static bool TErrForm_L00511B48(ErrorBox? ErrForm, string edx0)
    {
        bool bl0 = true;

        int esi0 = edx0.Length;
        int edi0 = 0;

        for (; edi0 < esi0; edi0++)
        {
            if (!BtBitString((byte)edx0[edi0], AlliedVariables.s_V0x00533B80))
            {
                edi0++;
                break;
            }
        }

        if (edi0 < 2)
        {
            bl0 = false;
        }

        string ebp14_3 = edx0[edi0 - 1].ToString(CultureInfo.InvariantCulture).ToUpperInvariant();

        if (!string.Equals(ebp14_3, "B", StringComparison.Ordinal))
        {
            bl0 = false;
        }

        if (bl0)
        {
            for (; edi0 < esi0; edi0++)
            {
                if (!BtBitString((byte)edx0[edi0], AlliedVariables.s_V0x00533B80))
                {
                    edi0++;
                    break;
                }
            }
        }

        string ebp14_1 = edx0[edi0 - 1].ToString(CultureInfo.InvariantCulture).ToUpperInvariant();

        if (!string.Equals(ebp14_1, "M", StringComparison.Ordinal))
        {
            bl0 = false;
        }

        if (edi0 < esi0)
        {
            if (!BtBitString((byte)edx0[edi0], AlliedVariables.s_V0x00533B80))
            {
                bl0 = false;
            }
        }

        //bl0 = 0;
        // System_DoneExcept();

        return bl0;
    }

    // L00510F18
    public static void TErrForm_L00510F18(ErrorBox? ErrForm, bool edx0)
    {
        AlliedVariables.s_TErrForm_Items1 = new();
        AlliedVariables.s_TErrForm_Items2 = new();

        int ebp00C = 0;
        int[] ebp050 = new int[10];
        bool[] ebp018 = new bool[8];
        bool ebp019 = false;
        bool ebp01A = true;

        string ebp20 = Path.GetFileName(AlliedVariables.s_V0x00543BF8);

        if (AlliedVariables.s_CheckFilenameOptionSetting)
        {
            if (!string.Equals(ebp20, "Unnamed", StringComparison.Ordinal))
            {
                if (!TErrForm_L00511B48(ErrForm, ebp20))
                {
                    string ebpEF0 = string.Format(CultureInfo.InvariantCulture, "WARNING: Filename \"{0}\" has incorrect format and will cause the game to crash if it has a briefing.", ebp20);
                    AlliedVariables.s_TErrForm_Items2.Add(ebpEF0);
                    AlliedVariables.s_TErrForm_Items2.Add("                    Format must be: 1B <Battle Number> M <Mission Number><anything else?>");
                    AlliedVariables.s_TErrForm_Items2.Add("                    e.g. 1B9M2ThisIsAMissionName.tie");
                }
            }
        }

        for (int ebp08 = 0; ebp08 < AlliedVariables.s_FlightGroupObjectsList.Count; ebp08++)
        {
            S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp08);

            S0xTieFlightGroup ebpEEC = S0xTieFlightGroup.FromByteArray(eax0.FlightGroupStruct.ToByteArray());

            if (ebpEEC.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop)
            {
                ebp019 = true;
            }

            if (ebpEEC.PlayerNumber == 0x01
                && ebpEEC.StartFgUsed == 0
                && ebpEEC.SecondaryStopFgUsed == 0
                && ebpEEC.PrimaryStopFgUsed == 0
                && ebpEEC.CaptureFgUsed == 0)
            {
                ebp01A = false;
            }

            if (ebpEEC.CraftId >= CraftIdEnum._001_0_0_Xwing)
            {
                if (ebpEEC.CraftId == CraftIdEnum._092_0_95_ModStrikeCruiser)
                {
                    AlliedVariables.s_TErrForm_Items2.Add("WARNING: Modified Strike Crusiers not included with XWA.");
                }
            }
            else
            {
                string ebpF30_13 = "ERROR: FG#" + (ebp08 + 1).ToString(CultureInfo.InvariantCulture) + " has no Ship type.";
                AlliedVariables.s_TErrForm_Items1.Add(ebpF30_13);
            }

            if (ebpEEC.PlayerNumber > 0)
            {
                ebp00C++;
                ebp050[ebpEEC.Team]++;

                if (ebp018[ebpEEC.PlayerNumber - 1])
                {
                    string ebpF30_11 = "ERROR: There is more than one Player " + ebpEEC.PlayerNumber.ToString(CultureInfo.InvariantCulture) + " All Players should have different Player numbers.";
                    AlliedVariables.s_TErrForm_Items1.Add(ebpF30_11);
                }
                else
                {
                    ebp018[ebpEEC.PlayerNumber - 1] = true;
                }
            }

            if (ebpEEC.CraftsCount > 0x06)
            {
                string ebpF30_9 = "WARNING: " + AlliedVariables.s_V0x00543BC0.GetText(ebp08) + " has " + ebpEEC.CraftsCount.ToString(CultureInfo.InvariantCulture) + " ships in FG. " + " XWA does not properly support FG Formations larger than 6 ships.";
                AlliedVariables.s_TErrForm_Items2.Add(ebpF30_9);
            }

            for (int ebp010 = 1; ebp010 < 5; ebp010++)
            {
                for (int esi = 1; esi < 5; esi++)
                {
                    S0xTieFlightGroupOrder ebx = ebpEEC.Orders[(esi - 1) * 4 + (ebp010 - 1)];

                    if (ebx.OrderId != TieOrderIdEnum._07_CapFree)
                    {
                        continue;
                    }

                    if (ebpEEC.CraftId == CraftIdEnum._042_0_82_Frigate2
                        || ebpEEC.CraftId == CraftIdEnum._043_0_83_ModFrigate
                        || ebpEEC.CraftId == CraftIdEnum._046_0_86_StrikeCruiser
                        || ebpEEC.CraftId == CraftIdEnum._048_0_88_Dreadnaught2
                        || ebpEEC.CraftId == CraftIdEnum._049_0_89_CalamariCruiserNew
                        || ebpEEC.CraftId == CraftIdEnum._050_0_90_LightCalamariCruiser
                        || ebpEEC.CraftId == CraftIdEnum._051_0_91_Interdictor2
                        || ebpEEC.CraftId == CraftIdEnum._052_0_92_VictoryStarDestroyer2
                        || ebpEEC.CraftId == CraftIdEnum._053_0_93_ImperialStarDestroyer2
                        || ebpEEC.CraftId == CraftIdEnum._054_0_94_SuperStarDestroyer
                        || ebpEEC.CraftId == CraftIdEnum._092_0_95_ModStrikeCruiser)
                    {
                        string ebpF30_6 = "WARNING: " + AlliedVariables.s_V0x00543BC0.GetText(ebp08) + " - Order " + ebp010.ToString(CultureInfo.InvariantCulture) + ", Region#" + esi.ToString(CultureInfo.InvariantCulture) + ": Capital ship with \"Attack\" order will" + " make it jump around. Use a Starship order such as Patrol and Attack instead.";
                        AlliedVariables.s_TErrForm_Items2.Add(ebpF30_6);
                    }

                    bool c = false;

                    if (!c)
                    {
                        if ((ebx.PrimaryTarget.ClassA == TieClassEnum.ShipCategory && ebx.PrimaryTarget.ParameterA == 0)
                            || (ebx.PrimaryTarget.ClassB == TieClassEnum.ShipCategory && ebx.PrimaryTarget.ParameterB == 0))
                        {
                            if (ebx.PrimaryTarget.Operator == 0x01)
                            {
                                c = true;
                            }
                        }
                    }

                    if (!c)
                    {
                        if ((ebx.SecondaryTarget.ClassA == TieClassEnum.ShipCategory && ebx.SecondaryTarget.ParameterA == 0)
                            || (ebx.SecondaryTarget.ClassB == TieClassEnum.ShipCategory && ebx.SecondaryTarget.ParameterB == 0))
                        {
                            if (ebx.SecondaryTarget.Operator == 0x01)
                            {
                                c = true;
                            }
                        }
                    }

                    if (!c)
                    {
                        continue;
                    }

                    string ebpF30_2 = "WARNING: " + AlliedVariables.s_V0x00543BC0.GetText(ebp08) + " - Order " + ebp010.ToString(CultureInfo.InvariantCulture) + ": Attack Starfighters has \"Either A or B\" flag turned on, therefore " + "it will attack ALL Starfighters in the mission, regardless of Team or IFF.";
                    AlliedVariables.s_TErrForm_Items2.Add(ebpF30_2);
                }
            }
        }

        if (!ebp019)
        {
            AlliedVariables.s_TErrForm_Items2.Add("WARNING: There are no backdrops in the mission. It will be dark!");
        }

        if (ebp00C < 0x01)
        {
            AlliedVariables.s_TErrForm_Items1.Add("ERROR: There are no Player Craft in this mission.");
        }
        else if (ebp050[0] < 0x01)
        {
            AlliedVariables.s_TErrForm_Items2.Add("WARNING: There are no player craft on Team 1. Player craft must be on Team 1 in order to hyper jump.");
        }

        if (AlliedVariables.s_TieFileHeader.Header.MissionType < 0x08)
        {
            if (ebp00C > 0x01)
            {
                AlliedVariables.s_TErrForm_Items1.Add("ERROR: There is more than one player in Single player mission type.");
            }
            else if (!ebp01A && AlliedVariables.s_TieFileHeader.Header.MissionType != 0x05)
            {
                if (ebp00C > 0)
                {
                    AlliedVariables.s_TErrForm_Items2.Add("WARNING: Player has no Mother ship assigned.");
                }
            }
        }

        int ebx0 = AlliedVariables.s_TErrForm_Items1.GetCount();

        if (!AlliedVariables.s_TErrForm_OmitWarns)
        {
            ebx0 += AlliedVariables.s_TErrForm_Items2.GetCount();
        }

        if (ebx0 > 0)
        {
            AlliedVariables.s_TErrForm_Instance = MainImpl.CreateErrorBox();
            AlliedVariables.s_TErrForm_Instance.Owner = Application.Current.MainWindow;
            AlliedVariables.s_TErrForm_Instance.ShowDialog();
            AlliedVariables.s_TErrForm_Instance = null;
        }
        else
        {
            if (edx0)
            {
                Allied_ShowMessageWithTimer(0x0320, "No known errors.");
            }
        }
    }

    // L00511B0C
    private static void TErrForm_FormClose(ErrorBox ErrForm)
    {
        AlliedVariables.s_TErrForm_Items1.Clear();
        AlliedVariables.s_TErrForm_Items2.Clear();
    }

    // L00511B24
    private static void TErrForm_OmitWarnsChkClick(ErrorBox ErrForm)
    {
        AlliedVariables.s_TErrForm_OmitWarns = ErrForm.OmitWarnsChk.IsChecked == true;
    }
}
