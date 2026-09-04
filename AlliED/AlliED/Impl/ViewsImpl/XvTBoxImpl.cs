using AlliED.Extensions;
using System.Globalization;
using System.IO;
using System.Windows;

namespace AlliED.Impl.ViewsImpl;

internal static class XvTBoxImpl
{
    public static void Register(XvTBox window)
    {
        SetBindings(window);
    }

    private static void SetBindings(XvTBox window)
    {
        window.RegionPickBox.SelectionChanged += (s, e) => TXvTForm_RegionPickBoxChange(window, s);
    }

    // L004CED0C
    private static void TXvTForm_RegionPickBoxChange(XvTBox XvTForm, object? Sender)
    {
        if (XvTForm.RegionPickBox.SelectedIndex == 0)
        {
            XvTForm.OrderPickBox.IsEnabled = true;
            return;
        }

        int edx1 = XvTForm.RegionPickBox.SelectedIndex - 1;
        XvTForm.OrderPickBox.SelectedIndex = edx1;
        XvTForm.OrderPickBox.IsEnabled = false;
    }

    // L004CB0A8
    public static void Allied_ReadTieMission_XvT(XvTBox eax0)
    {
        AlliedVariables.s_V0x0053BDB8 = 0x01;

        TieFileVersionEnum bx = AlliedVariables.s_TieFileVersion;

        for (int i = 0; i < 50; i++)
        {
            AlliedVariables.s_V0x00541BBC[1 + i] = -1;
        }

        for (int i = 0; i < 400; i++)
        {
            AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[i] = 0;
        }

        for (int i = 0; i < 10008; i++)
        {
            AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m0036BA[i] = 0;
        }

        TXvTForm__PROC_004CB1E0(eax0);
        TXvTForm_Proc_004CB34C(eax0);
        TXvTForm_Proc_004CC454(eax0);
        TXvTForm__PROC_004CC6E4(eax0);
        Allied_ReadTieMission_XWA_Teams(AlliedVariables.s_AlliedForm1Window!);
        TXvTForm__PROC_004CC790(eax0);
        TXvTForm_Proc_004CCA9C(eax0);
        TXvTForm_Proc_004CCB04(eax0);

        if (bx == TieFileVersionEnum.XvT)
        {
            if (AlliedVariables.s_V0x00535E8C != 0)
            {
                AlliedVariables.s_TMemoForm_Instance!.EditMemo1.Clear();
                AlliedVariables.s_TMemoForm_Instance!.EditMemo2.Clear();
                AlliedVariables.s_TMemoForm_Instance!.EditMemo3.Clear();
                AlliedVariables.s_TMemoForm_Instance!.EditMemo4.Clear();
            }

            Unit_00513838_Proc_0051D4BC();

            AlliedVariables.s_TieMission_WinDebriefing.Text = string.Empty;
            AlliedVariables.s_TieMission_LostDebriefing.Text = string.Empty;
        }
        else if (bx == TieFileVersionEnum.Bop)
        {
            Allied_ReadTieMission_XWA_WinDebriefing();
            Allied_ReadTieMission_XWA_LostDebriefing();
            Allied_ReadTieMission_XWA_Description();
        }

        if (AlliedVariables.s_V0x00535E8C != 0)
        {
            MemoWindowImpl.TMemoForm_Proc_004C418C(AlliedVariables.s_TMemoForm_Instance!);
        }

        AlliedVariables.s_TieFileVersion = TieFileVersionEnum.XWA;
        AlliedVariables.s_TieMission_LostDebriefing.Text = "#" + AlliedVariables.s_TieMission_LostDebriefing.Text;

        Unit_00513838_Proc_0051DB68();
    }

    // L004CB1E0
    private static void TXvTForm__PROC_004CB1E0(XvTBox eax0)
    {
        byte[] ebpA2 = new byte[0xA2];
        AlliedVariables.s_TieFileHandle.BlockRead(ebpA2, 0xA2, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();

        S0xMissionObject eax1 = new();
        AlliedVariables.s_TieFileHeader = S0xTieFileHeader.FromByteArray(eax1.TieFileHeader.ToByteArray());
        AlliedVariables.s_TieFileVersion = TieFileVersionEnum.XWA;
        AlliedVariables.s_TieFileHeader.Header.WinType = 0x01;
        AlliedVariables.s_TieFileHeader.Header.AllWayShown = true;
        AlliedVariables.s_TieFileHeader.Header.MissionType = 0x06;
        AlliedVariables.s_TieFileHeader.Header.BriefingCodeSizeType = 0x62;

        for (int esi = 0; esi < 4; esi++)
        {
            string ebp130_1 = "Region " + (esi + 0x01).ToString(CultureInfo.InvariantCulture);
            S0xTieRegion ebp128 = new();
            Unit_00511CD0_Proc_005121FC(ebp130_1, ref ebp128);
            AlliedVariables.s_TieFileHeader.Header.Regions[esi] = ebp128;
        }

        AlliedVariables.s_TieFileHeader.FlightGroupsCount = BitConverter.ToInt16(ebpA2, 0x00);
        AlliedVariables.s_TieFileHeader.RadioMessagesCount = BitConverter.ToInt16(ebpA2, 0x02);
        AlliedVariables.s_TieFileHeader.Header.TimeLimit = ebpA2[0x64];

        Unit_00513838_Proc_00517564();
    }

    // L004CB34C
    private static void TXvTForm_Proc_004CB34C(XvTBox eax0)
    {
        AlliedVariables.s_V0x0053BDB0 = 0;
        AlliedVariables.s_V0x0053BDB4 = 0;

        bool ebp29 = false;
        byte[] ebp1E = new byte[0x0A];

        for (int ebp08 = 0; ebp08 < 0x0A; ebp08++)
        {
            ebp1E[ebp08] = 0x01;
        }

        short eax1 = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int ebp30 = 0; ebp30 < eax1; ebp30++)
        {
            int ebp08 = ebp30 + 0x01;

            byte[] buffer596 = new byte[S0x004CD5C0_00.Size];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer596, 0x562, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
            S0x004CD5C0_00 ebp596 = S0x004CD5C0_00.FromByteArray(buffer596);
            S0xFGObject ebp14 = new();
            ebp14.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
            ebp14.FlightGroupStruct.Name = ebp596.Name;
            ebp14.FlightGroupStruct.Cargo = ebp596.Cargo;
            ebp14.FlightGroupStruct.SpecialCargo = ebp596.SpecialCargo;

            {
                byte[] buffer = ebp14.FlightGroupStruct.ToByteArray();
                ebp596.m000050.CopyTo(buffer, 0x0069);
                ebp14.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(buffer);
            }

            ebp14.FlightGroupStruct.ArrivalDifficulty = (ArrivalDifficultyEnum)ebp596.m000050[0x1D];

            bool jump0 = false;
            string ebp28_0 = 0x01.ToString(CultureInfo.InvariantCulture);
            string ebp5CC_12 = System_LStrFromPCharLen(ebp596.M000014, 0x14);
            string ebp28_1 = ebp5CC_12.ToUpperInvariant();

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "PRI") > 0 || ebp28_1.IndexOf("APRI") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.PRImary;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "BAS") > 0 || ebp28_1.IndexOf("ABAS") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.BASe;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "STA") > 0 || ebp28_1.IndexOf("ASTA") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.STAtion;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "MIS") > 0 || ebp28_1.IndexOf("AMIS") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.MISsionCritical;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "CON") > 0 || ebp28_1.IndexOf("ACON") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.CONvoy;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "REL") > 0 || ebp28_1.IndexOf("AREL") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.RELoad;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "STR") > 0 || ebp28_1.IndexOf("ASTR") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.STRike;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "SEC") > 0 || ebp28_1.IndexOf("ASEC") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.SECondary;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "TER") > 0 || ebp28_1.IndexOf("ATER") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.TERtiary;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "RES") > 0 || ebp28_1.IndexOf("ARES") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.RESearch;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "MAN") > 0 || ebp28_1.IndexOf("AMAN") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRole0 = TacticalRoleEnum.MANufacturing;
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                    jump0 = true;
                }
            }

            if (!jump0)
            {
                if (ebp28_1.IndexOf(ebp28_0 + "COM") > 0 || ebp28_1.IndexOf("ACOM") > 0)
                {
                    ebp14.FlightGroupStruct.TacticalRoleUsed0 = TacticalRoleUsedEnum.NoTC;
                }
            }

            if (ebp14.FlightGroupStruct.CraftId == CraftIdEnum._087_1_11_AsteroidHR1)
            {
                ebp29 = true;
                ebp14.FlightGroupStruct.CraftId = CraftIdEnum._183_9001_1100_ResData_Backdrop;
                ebp14.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8("1.0 1.0 1.0");
                ebp14.FlightGroupStruct.Cargo = Unit_00511CD0_Proc_00511EB8("1.0");

                ebp14.FlightGroupStruct.PlanetId = (byte)ebp14.FlightGroupStruct.Status1 switch
                {
                    0x00 => 0x0D,
                    0x01 => 0x02,
                    0x02 => 0x11,
                    0x03 => 0x02,
                    0x04 => 0x16,
                    0x05 => 0x3C,
                    0x06 => 0x11,
                    0x07 => 0x1C,
                    0x08 => 0x01,
                    0x09 => 0x27,
                    0x0A => 0x29,
                    0x0B => 0x13,
                    0x0C => 0x54,
                    0x0D => 0x56,
                    0x0E => 0x5A,
                    0x0F => 0x5C,
                    0x10 => 0x47,
                    _ => 0x01,
                };

                ebp14.FlightGroupStruct.SpecialCargo = (byte)ebp14.FlightGroupStruct.Status1 switch
                {
                    0x00 or 0x01 or 0x02 or 0x03 or 0x04 or 0x05 or 0x06 or 0x07 => Unit_00511CD0_Proc_00511EB8("0.4"),
                    0x08 or 0x09 or 0x0A => Unit_00511CD0_Proc_00511EB8("1.0"),
                    0x0B => Unit_00511CD0_Proc_00511EB8("1.5"),
                    0x0C or 0x0D or 0x0E or 0x0F or 0x10 => Unit_00511CD0_Proc_00511EB8("0.3"),
                    _ => Unit_00511CD0_Proc_00511EB8("0.5"),
                };

                ebp14.FlightGroupStruct.GlobalCargoIndex = (byte)ebp14.FlightGroupStruct.Status1 switch
                {
                    0x06 or 0x07 or 0x09 => 0x01,
                    0x0A => 0x02,
                    _ => 0,
                };

                ebp14.FlightGroupStruct.Status1 = FlightGroupStatusEnum.Normal;
            }

            {
                byte[] buffer = ebp14.FlightGroupStruct.ToByteArray();
                ebp596.m00051A.CopyTo(buffer, 0x0DBE);
                ebp14.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(buffer);
            }

            ebp14.FlightGroupStruct.ArrivalTrigger1 = TXvTForm__PROC_004CCBC4(eax0, ebp596.m00006E);
            ebp14.FlightGroupStruct.ArrivalTrigger2 = TXvTForm__PROC_004CCBC4(eax0, ebp596.m000079);
            ebp14.FlightGroupStruct.ArrivalTriggersOperator = ebp596.m000084;
            ebp14.FlightGroupStruct.ArrivalDelayMinutes = ebp596.m000086;
            ebp14.FlightGroupStruct.ArrivalDelaySeconds = ebp596.m000087;
            ebp14.FlightGroupStruct.DepartureTrigger = TXvTForm__PROC_004CCBC4(eax0, ebp596.m000088);

            {
                byte[] buffer = ebp14.FlightGroupStruct.ToByteArray();
                ebp596.m000093.CopyTo(buffer, 0x00BC);
                ebp14.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(buffer);
            }

            {
                byte[] buffer = ebp14.FlightGroupStruct.ToByteArray();
                ebp596.m00009A.CopyTo(buffer, 0x00C2);
                ebp14.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(buffer);
            }

            for (int ebp0C = 0x01; ebp0C < 0x05; ebp0C++)
            {
                for (int ebp10 = 0x01; ebp10 < 0x05; ebp10++)
                {
                    S0x004CD5C0_01 edi = ebp596.m0000A2[ebp10 - 1];

                    for (int ebx = 0x01; ebx < 0x14; ebx++)
                    {
                        if (ebx == 0x03)
                        {
                            switch ((TieOrderIdEnum)ebp596.m0000A2[0].m000000[0])
                            {
                                case TieOrderIdEnum._12_BoardToGive:
                                case TieOrderIdEnum._13_BoardToTake:
                                case TieOrderIdEnum._14_BoardToExchange:
                                case TieOrderIdEnum._15_BoardToCapture:
                                case TieOrderIdEnum._16_BoardToDestroy:
                                case TieOrderIdEnum._17_BoardToPickup:
                                case TieOrderIdEnum._18_DropOff:
                                case TieOrderIdEnum._19_Wait:
                                case TieOrderIdEnum._20_Wait:
                                case TieOrderIdEnum._31_BoardToContact:
                                case TieOrderIdEnum._32_BoardToRepair:
                                case TieOrderIdEnum._36_SelfDestroy:
                                    ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].Var0 = TXvTForm_PROC_004CCE7C(eax0, edi.m000000[ebx - 1]);
                                    break;

                                default:
                                    ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].Var0 = edi.m000000[ebx - 1];
                                    break;
                            }
                        }
                        else
                        {
                            byte[] buffer = ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].ToByteArray();
                            buffer[ebx - 1] = edi.m000000[ebx - 1];
                            ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)] = S0xTieFlightGroupOrder.FromByteArray(buffer);
                        }
                    }

                    for (int ebx = 0x01; ebx < 0x41; ebx++)
                    {
                        ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].m000054[ebx - 1] = 0;
                    }
                }
            }

            for (int ebp0C = 0x01; ebp0C < 0x05; ebp0C++)
            {
                for (int ebp10 = 0x01; ebp10 < 0x05; ebp10++)
                {
                    for (int ebx = 0x01; ebx < 0x14; ebx++)
                    {
                        switch (ebx)
                        {
                            case 0x07: // SecondaryTarget.ClassA
                                if (ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].SecondaryTarget.ClassA == TieClassEnum.ShipType)
                                {
                                    ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].SecondaryTarget.ParameterA++;
                                }
                                break;

                            case 0x08: // SecondaryTarget.ClassB
                                if (ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].SecondaryTarget.ClassB == TieClassEnum.ShipType)
                                {
                                    ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].SecondaryTarget.ParameterB++;
                                }
                                break;

                            case (int)0x0D: // PrimaryTarget.ClassA
                                if (ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].PrimaryTarget.ClassA == TieClassEnum.ShipType)
                                {
                                    ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].PrimaryTarget.ParameterA++;
                                }
                                break;

                            case (int)0x0F: // PrimaryTarget.ClassB
                                if (ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].PrimaryTarget.ClassB == TieClassEnum.ShipType)
                                {
                                    ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].PrimaryTarget.ParameterB++;
                                }
                                break;
                        }
                    }
                }
            }

            for (int ebp0C = 0x01; ebp0C < 0x05; ebp0C++)
            {
                ebp14.FlightGroupStruct.JumpTriggers[(ebp0C - 1) * 4 + 3] = TXvTForm__PROC_004CCBC4(eax0, ebp596.m0001EA);
            }

            AlliedVariables.s_V0x00541BA4 = 0;

            for (int ebx = 0; ebx < 0x08; ebx++)
            {
                if (ebp596.m000466[3].m000008[ebx] == 0x01)
                {
                    AlliedVariables.s_V0x00541BA4 = 0x05 + ebx;
                }
            }

            AlliedVariables.s_V0x00541BA4 -= 0x03;

            if (AlliedVariables.s_V0x00541BA4 < 0)
            {
                AlliedVariables.s_V0x00541BA4 = 0x01;
            }

            for (int esi = 0; esi < 0x03; esi++)
            {
                S0x004CD5C0_02 ebx = ebp596.m000466[esi];

                for (int eax = 0x01; eax < 0x05; eax++)
                {
                    ebp14.m00147C[esi].M000000[eax - 1] = ebx.m000000[eax - 1];
                }
            }

            for (int eax = 0x01; eax < 0x05; eax++)
            {
                ebp14.IsWPEnabled[eax - 0x01] = ebp596.m000466[3].m000000[eax - 1];
            }

            for (int ebp0C = 0x01; ebp0C < 0x05; ebp0C++)
            {
                for (int ebp10 = 0x01; ebp10 < 0x05; ebp10++)
                {
                    for (int esi = 0; esi < 0x03; esi++)
                    {
                        for (int edx = 0; edx < 0x08; edx++)
                        {
                            ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].Waypoints[edx].Position[esi] = ebp596.m000466[esi].m000008[edx];
                        }
                    }

                    for (int ecx = 0; ecx < 0x08; ecx++)
                    {
                        ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].Waypoints[ecx].IsUsed = ebp596.m000466[3].m000008[ecx];
                    }

                    if (ebp596.m000466[3].m00001A == 0x01)
                    {
                        for (int esi = 0; esi < 0x04; esi++)
                        {
                            ebp14.m00147C[esi].M000000[3] = ebp596.m000466[esi].m00001A;
                        }

                        ebp14.IsWPEnabled[3] = 0x01;
                    }
                }
            }

            for (int ebp0C = 0; ebp0C < 0x04; ebp0C++)
            {
                ebp14.FlightGroupStruct.StartPoints[0].Position[ebp0C] = ebp596.m000466[ebp0C].m000000[0];
            }

            if (ebp596.m000466[3].m00001C[0] == 0x01)
            {
                if (AlliedVariables.s_V0x0053BDB4 < 0x33)
                {
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 0] = 0;
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 1] = 0x1A;
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 2] = (short)AlliedVariables.s_V0x0053BDB4;
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 3] = ebp596.m000050[2];
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 4] = ebp596.m000050[7];
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 5] = 0;
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 6] = 0x1C;
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 7] = (short)AlliedVariables.s_V0x0053BDB4;
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 8] = ebp596.m000466[0].m00001C[0];
                    AlliedVariables.s_V0x0053BDBC.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + 9] = ebp596.m000466[1].m00001C[0];
                    AlliedVariables.s_V0x00541BBC[ebp30] = AlliedVariables.s_V0x0053BDB4;
                    AlliedVariables.s_V0x0053BDB4 += 1;
                    AlliedVariables.s_V0x0053BDB0 += 0x0A;
                }
            }

            for (int esi = 0; esi < 0x08; esi++)
            {
                ebp14.FlightGroupStruct.Goals[esi] = TXvTForm__PROC_004CCD28(eax0, ebp596.m0001F5[esi]);
            }

            for (int esi = 0; esi < 0x08; esi++)
            {
                ebp14.FlightGroupStruct.OptionalWarheads[esi] = ebp596.OptionalWarheads[esi];
            }

            for (int esi = 0; esi < 0x06; esi++)
            {
                ebp14.FlightGroupStruct.OptionalBeams[esi] = ebp596.OptionalBeams[esi];
            }

            for (int esi = 0; esi < 0x04; esi++)
            {
                ebp14.FlightGroupStruct.OptionalCounterMeasures[esi] = ebp596.OptionalCounterMeasures[esi];
            }

            ebp14.FlightGroupStruct.OptionalCraftCategory = ebp596.OptionalCraftCategory;

            for (int esi = 0; esi < 0x08; esi++)
            {
                ebp14.FlightGroupStruct.OptionalCraftsId[esi] = ebp596.OptionalCraftsId[esi];
            }

            for (int esi = 0; esi < 0x06; esi++)
            {
                ebp14.FlightGroupStruct.OptionalCraftsCount[esi] = ebp596.OptionalCraftsCount[esi];
            }

            for (int esi = 0; esi < 0x04; esi++)
            {
                ebp14.FlightGroupStruct.OptionalCraftsWaves[esi] = ebp596.OptionalCraftsWaves[esi];
            }

            if (AlliedVariables.s_UseAutoChkSetting)
            {
                ebp14.AutoLink = true;
            }

            ebp14.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(ebp14.FlightGroupStruct));

            if (AlliedVariables.s_V0x00543C84.M000000[2].M000000)
            {
                if (AlliedVariables.s_V0x00543C84.M000000[2].M000006 && ebp14.FlightGroupStruct.PlayerNumber > 0x01)
                {
                    ebp14.FlightGroupStruct.PlayerNumber = 0;
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C40))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[2].M000001 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[2].M000001;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C60))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[2].M000002 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[2].M000002;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C20))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[2].M000003 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[2].M000003;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533BC0))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[2].M000004 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[2].M000004;
                    }
                }

                if (BtBitString((int)ebp14.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533BE0))
                {
                    if (AlliedVariables.s_V0x00543C84.M000000[2].M000005 < ebp14.FlightGroupStruct.AIRank)
                    {
                        ebp14.FlightGroupStruct.AIRank = AlliedVariables.s_V0x00543C84.M000000[2].M000005;
                    }
                }
            }

            AlliedVariables.s_FlightGroupObjectsList.Add(ebp14);
            Unit_00513838_Proc_0051D53C(ebp08 - 1);

            if (AlliedVariables.s_V0x00543C78[ebp14.FlightGroupStruct.Team] < 0x0A)
            {
                if (ebp1E[ebp14.FlightGroupStruct.Team] != 0)
                {
                    AlliedVariables.s_V0x00543C78[ebp14.FlightGroupStruct.Team] = ebp14.FlightGroupStruct.Iff;
                    ebp1E[ebp14.FlightGroupStruct.Team] = 0;
                }
            }
        }

        if (!ebp29)
        {
            TXvTForm__PROC_004CCE90(eax0);
        }

        AlliedVariables.s_CurrentRegion = 0x01;
        AlliedVariables.s_CurrentOrderInRegion = 0x01;

        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        Unit_00513838_Proc_00520924();
    }

    // L004CCBC4
    private static S0xTieFlightGroupTriggerPair TXvTForm__PROC_004CCBC4(XvTBox eax0, S0x004CE838 edx0)
    {
        S0xTieFlightGroupTriggerPair esp0B = new();
        S0x004CE838 esp00 = S0x004CE838.FromByteArray(edx0.ToByteArray());
        esp0B.Triggers[0] = TXvTForm_Proc_004CCB6C(eax0, esp00.m000000);
        esp0B.Triggers[1] = TXvTForm_Proc_004CCB6C(eax0, esp00.m000004);
        esp0B.TriggerUsed[0] = esp00.m000008;
        esp0B.TriggerUsed[1] = esp00.m000009;
        esp0B.Operator = esp00.m00000A;
        esp0B.m00000F = 0;
        return esp0B;
    }

    // L004CCD28
    private static S0xTieFlightGroupGoal TXvTForm__PROC_004CCD28(XvTBox eax0, S0x004CD5C0_03 edx0)
    {
        S0x004CD5C0_03 esp00 = S0x004CD5C0_03.FromByteArray(edx0.ToByteArray());
        S0xTieFlightGroupGoal esp4E = new();

        esp4E.GoalType = esp00.GoalType;
        esp4E.Condition = esp00.Condition;
        esp4E.Amount = esp00.Amount;

        if (esp00.Points > 0x0C)
        {
            esp4E.Points = 0x7F;
        }
        else
        {
            esp4E.Points = (byte)(esp00.Points * 0x0A);
        }

        esp4E.Time = 0;

        for (int eax = 0; eax < 0x0A; eax++)
        {
            esp4E.AppliesToTeams[1 + eax] = esp00.AppliesToTeams[1 + eax];
        }

        esp4E.SequenceNumber = esp00.AppliesToTeams[9];
        esp4E.AppliesToTeams[0] = esp00.AppliesToTeams[0];
        esp4E.SequenceNumber = 0;

        for (int eax = 0; eax < 0x40; eax++)
        {
            esp4E.m000010[eax] = esp00.m00000E[eax];
        }

        return esp4E;
    }

    // L004CCB6C
    private static S0xTieTrigger TXvTForm_Proc_004CCB6C(XvTBox eax0, uint edx0)
    {
        byte[] esp00 = BitConverter.GetBytes(edx0);
        S0xTieTrigger esp04 = new();

        esp04.Condition = (TieConditionEnum)esp00[0];
        esp04.VariableType = (TieClassEnum)esp00[1];

        if (esp00[1] == 0x02)
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

    // L004CC454
    private static void TXvTForm_Proc_004CC454(XvTBox eax0)
    {
        int ebp0C = AlliedVariables.s_TieFileHeader.RadioMessagesCount;

        for (int ebp08 = 0; ebp08 < ebp0C; ebp08++)
        {
            byte[] ebp80 = new byte[0x74];
            AlliedVariables.s_TieFileHandle.BlockRead(ebp80, 0x74, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();

            S0xTieRadioMessageObject esi = new();
            byte ebx = ebp80[0x02];

            if (ebx == 0x31)
            {
                esi.RadioMessage.Side = 0;
                esi.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp80.ReadFixedLengthString(0x03, 0x3F), 0x3F));
            }
            else if (ebx == 0x32)
            {
                esi.RadioMessage.Side = 0x02;
                esi.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp80.ReadFixedLengthString(0x03, 0x3F), 0x3F));
            }
            else if (ebx == 0x33)
            {
                esi.RadioMessage.Side = 0x03;
                esi.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(System_LStrFromPCharLen(ebp80.ReadFixedLengthString(0x03, 0x3F), 0x3F));
            }
            else
            {
                esi.RadioMessage.Side = 0x01;
                string ebp118 = new string((char)ebx, 1) + ebp80.ReadFixedLengthString(0x03, 0x3F);
                esi.RadioMessage.Message = Unit_00511CD0_Proc_0051213C(ebp118);
            }

            esi.RadioMessage.Id = BitConverter.ToInt16(ebp80, 0x00);

            for (int edx = 0; edx < 0x0A; edx++)
            {
                esi.RadioMessage.ForTeam[edx] = ebp80[0x42 + edx];
            }

            esi.RadioMessage.Condition = TXvTForm__PROC_004CCC48(eax0, ebp80.Subarray(0x4C, 0x28));
            esi.RadioMessage.Operator = ebp80[0x73] != 0;
            esi.RadioMessage.TimePassed = TXvTForm_PROC_004CCE7C(eax0, ebp80[0x72]);
            AlliedVariables.s_RadioMessagesObjectsList.Add(esi);
            esi.M0000A6 = (byte)Unit_00513838_Proc_0051D874(ebp08);
        }

        Unit_00513838_Proc_0051477C();
    }

    // L004CCC48
    private static S0xTieTriggers TXvTForm__PROC_004CCC48(XvTBox eax0, byte[] edx0)
    {
        S0xTieTriggers esp28 = new();
        esp28.Trigger_0[0] = TXvTForm_Proc_004CCB6C(eax0, BitConverter.ToUInt32(edx0, 0x00));
        esp28.Trigger_0[1] = TXvTForm_Proc_004CCB6C(eax0, BitConverter.ToUInt32(edx0, 0x04));
        esp28.TriggerUsed_0[0] = edx0[0x08];
        esp28.TriggerUsed_0[1] = edx0[0x09];
        esp28.Operator_0 = edx0[0x0A] != 0;
        esp28.Unused0F_0 = 0;
        esp28.Trigger_1[0] = TXvTForm_Proc_004CCB6C(eax0, BitConverter.ToUInt32(edx0, 0x0B));
        esp28.Trigger_1[1] = TXvTForm_Proc_004CCB6C(eax0, BitConverter.ToUInt32(edx0, 0x0F));
        esp28.TriggerUsed_1[0] = edx0[0x13];
        esp28.TriggerUsed_1[1] = edx0[0x14];
        esp28.Operator_1 = edx0[0x15] != 0;
        esp28.Unused0F_1 = edx0[0x27];
        return esp28;
    }

    // L004CC6E4
    private static void TXvTForm__PROC_004CC6E4(XvTBox eax0)
    {
        for (int esp04 = 0; esp04 < 0x0A; esp04++)
        {
            byte[] esp08 = new byte[0x80];
            AlliedVariables.s_TieFileHandle.BlockRead(esp08, 0x80, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();

            S0xTieGlobalGoalObject edi = new();
            edi.GlobalGoal.Count = 0x03;

            for (int esi0 = 0; esi0 < 3; esi0++)
            {
                byte[] ebx = esp08.Subarray(0x02 + esi0 * 0x2A, 0x2A);
                edi.GlobalGoal.GlobalGoals[esi0] = TXvTForm__PROC_004CCDD0(eax0, ebx);

                byte[] goal = edi.GlobalGoal.GlobalGoals[esi0].ToByteArray();
                goal[0x0020 + 11] = ebx[0x27];
                edi.GlobalGoal.GlobalGoals[esi0] = S0xTieGlobalGoal.FromByteArray(goal);
            }

            AlliedVariables.s_GlobalGoalsObjectsList.Add(edi);
        }
    }

    // L004CCDD0
    private static S0xTieGlobalGoal TXvTForm__PROC_004CCDD0(XvTBox eax0, byte[] edx0)
    {
        S0xTieTriggers espA4 = TXvTForm__PROC_004CCC48(eax0, edx0);
        S0xTieGlobalGoal esp2A = new();
        esp2A.Triggers = espA4;

        if (edx0[0x29] > 0x0C)
        {
            esp2A.Points = 0x7F;
        }
        else
        {
            esp2A.Points = (byte)(edx0[0x29] * 0x0A);
        }

        esp2A.Op = edx0[0x0A] != 0;
        esp2A.Name = string.Empty;
        esp2A.TimePassed = 0;

        esp2A.Version = 0;

        for (int i = 0; i < 0x46; i++)
        {
            esp2A.m000034[i] = 0;
        }

        return esp2A;
    }

    // L004CC790
    public static void TXvTForm__PROC_004CC790(XvTBox eax0)
    {
        S0xTieBriefing esi = AlliedVariables.s_V0x0053BDBC;

        for (int edi0 = 0; edi0 < 8; edi0++)
        {
            byte[] esp00 = new byte[0x334];
            AlliedVariables.s_TieFileHandle.BlockRead(esp00, 0x334, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();

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

            if (edi0 + 1 != AlliedVariables.s_V0x0053BDB8)
            {
                continue;
            }

            esi.BriefingData.BriefingCode.ForTeam[0] = 0x01;
            esi.BriefingData.BriefingCode.Length = (short)Math.Round(BitConverter.ToInt16(esp00, 0x00) * 1.25f);
            esi.BriefingData.BriefingCode.Time = BitConverter.ToInt16(esp00, 0x02);
            esi.BriefingData.BriefingCode.Index = BitConverter.ToInt16(esp00, 0x04);
            esi.BriefingData.BriefingCode.Title = BitConverter.ToInt16(esp00, 0x08);
            esi.BriefingData.BriefingCode.CodeSize = (short)(AlliedVariables.s_V0x0053BDB0 + BitConverter.ToInt16(esp00, 0x06));

            for (int ebx0 = 0; ebx0 < 0x190; ebx0++)
            {
                short bp = BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);

                if (bp == 0x270F)
                {
                    esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = (short)Math.Round((float)bp);
                    ebx0++;
                }
                else
                {
                    esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = (short)Math.Round(bp * 1.25f);
                    ebx0++;
                }

                BriefingCommandEnum ax1 = (BriefingCommandEnum)BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);
                esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = (short)ax1;
                ebx0++;

                switch (ax1)
                {
                    case BriefingCommandEnum.BriefingTitle:
                    case BriefingCommandEnum.BriefingText:
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);
                        ebx0++;
                        break;

                    case BriefingCommandEnum.MoveMap:
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = BitConverter.ToInt16(esp00, 0x0A7 + ebx0 * 0x02);
                        ebx0++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);
                        ebx0++;
                        break;

                    case BriefingCommandEnum.ScaleMap:
                        {
                            esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = (short)Math.Round(BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02) * 1.4);
                            ebx0++;

                            esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = (short)Math.Round(BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02) * 1.4);
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
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = (short)AlliedVariables.s_V0x00541BBC[BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02)];
                        ebx0++;
                        break;

                    case BriefingCommandEnum.TextTag1:
                    case BriefingCommandEnum.TextTag2:
                    case BriefingCommandEnum.TextTag3:
                    case BriefingCommandEnum.TextTag4:
                    case BriefingCommandEnum.TextTag5:
                    case BriefingCommandEnum.TextTag6:
                    case BriefingCommandEnum.TextTag7:
                    case BriefingCommandEnum.TextTag8:
                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);
                        ebx0++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);
                        ebx0++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);
                        ebx0++;

                        esi.BriefingData.BriefingCode.m00000A[AlliedVariables.s_V0x0053BDB0 + ebx0] = BitConverter.ToInt16(esp00, 0x0A + ebx0 * 0x02);
                        ebx0++;
                        break;
                }
            }

            AlliedVariables.s_Allied_Briefing[0] = esi.Clone();
        }
    }

    // L004CCA9C
    private static void TXvTForm_Proc_004CCA9C(XvTBox eax0)
    {
        int ebx0 = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int esi0 = 0; esi0 < ebx0; esi0++)
        {
            byte[] buffer = new byte[0x600];
            AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x600, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi0);
            eax1.m000E42 = S0xFGObject_000E42.FromByteArray(buffer);
        }
    }

    // L004CCB04
    private static void TXvTForm_Proc_004CCB04(XvTBox eax0)
    {
        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            byte[] esp00 = new byte[0xC0 * 28];
            AlliedVariables.s_TieFileHandle.BlockRead(esp00, 0xC0 * 28, AlliedVariables.s_V0x00543C40);
            System_L004028C4_CheckError();

            S0xXvTGGStrObject eax1 = new();
            for (int i = 0; i < 28; i++)
            {
                eax1.GGStrings[i] = S0xXvTGGStrings.FromByteArray(esp00.Subarray(i * 0xC0, 0xC0));
            }

            AlliedVariables.s_V0x00571188.Add(eax1);
        }
    }

    // L004CCE7C
    public static byte TXvTForm_PROC_004CCE7C(XvTBox eax0, int edx0)
    {
        if (edx0 <= 0x04)
        {
            return (byte)(edx0 * 0x05);
        }

        return (byte)(0x14 + (edx0 - 0x04));
    }

    // L004CCE90
    public static void TXvTForm__PROC_004CCE90(XvTBox eax0)
    {
        S0xFGObject esi = new();
        esi.FlightGroupStruct.CraftId = CraftIdEnum._183_9001_1100_ResData_Backdrop;
        esi.FlightGroupStruct.CraftsCount = 0x01;
        esi.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8("1.0 1.0 1.0");
        esi.FlightGroupStruct.Cargo = Unit_00511CD0_Proc_00511EB8("1.0");
        esi.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8("1.0");

        esi.FlightGroupStruct.Team = 0x09;
        esi.FlightGroupStruct.Iff = 0x02;
        esi.m00147C[0].M000000[0] = 0x19;
        esi.m00147C[1].M000000[0] = 0x19;
        esi.IsWPEnabled[0] = 0x01;

        for (int eax = 0; eax < 0x08; eax++)
        {
            esi.FlightGroupStruct.Goals[eax].Condition = TieConditionEnum.Never;
        }

        System_Randomize();
        byte ebx = (byte)(0x01 + System_RandInt(0x3B));
        esi.FlightGroupStruct.PlanetId = ebx;

        if (ebx == 0x19 || ebx == 0x1B || ebx == 0x37)
        {
            esi.FlightGroupStruct.PlanetId = 0x48;
        }

        System_Randomize();
        esi.FlightGroupStruct.GlobalCargoIndex = (byte)System_RandInt(0x07);

        if (AlliedVariables.s_UseAutoChkSetting)
        {
            esi.AutoLink = true;
        }

        esi.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(esi.FlightGroupStruct));

        AlliedVariables.s_FlightGroupObjectsList.Add(esi);
        Unit_00513838_Proc_0051D53C(AlliedVariables.s_FlightGroupObjectsList.Count - 1);
    }

    // L004CED60
    private static string TXvTForm__PROC_004CED60(XvTBox XvTForm)
    {
        string ebp08_1 = Unit_00513838_Proc_0051B024(AlliedVariables.s_V0x00541BB4, (MissTypePickBoxEnum)XvTForm.MissTypePickBox.SelectedIndex);
        string ebp08_0 = Path.GetFileName(AlliedVariables.s_V0x00543BF8);
        return ebp08_1 + ebp08_0;
    }

    // L0051B024
    private static string Unit_00513838_Proc_0051B024(MissionExportTypeEnum eax0, MissTypePickBoxEnum edx0)
    {
        bool bl = true;
        string ecx0 = string.Empty;
        string ebp14_2 = string.Empty;

        switch (eax0)
        {
            case MissionExportTypeEnum.XvT:
                {
                    int esi = AlliedVariables.s_XvTDirLabSetting.ToUpperInvariant().IndexOf("XWINGTIE");

                    if (esi > 0)
                    {
                        ebp14_2 = AlliedVariables.s_XvTDirLabSetting[..(esi + 0x07)];
                    }
                    else
                    {
                        bl = false;
                        ecx0 = AlliedVariables.s_XvTDirLabSetting;
                    }

                    break;
                }

            case MissionExportTypeEnum.BoP:
                {
                    int esi = AlliedVariables.s_BoPDirLabSetting.ToUpperInvariant().IndexOf("BALANCEOFPOWER");

                    if (esi > 0)
                    {
                        ebp14_2 = AlliedVariables.s_BoPDirLabSetting[..(esi + 0x0D)];
                    }
                    else
                    {
                        ecx0 = AlliedVariables.s_BoPDirLabSetting;
                    }

                    break;
                }
        }

        if (bl)
        {
            switch (edx0)
            {
                case MissTypePickBoxEnum.Training:
                case MissTypePickBoxEnum.MultiplayerTraining:
                    ebp14_2 += "\\Train";
                    break;

                case MissTypePickBoxEnum.Melee:
                    ebp14_2 += "\\Melee";
                    break;

                case MissTypePickBoxEnum.MultiplayerCombat:
                    ebp14_2 += "\\Combat";
                    break;
            }

            ecx0 = ebp14_2 + "\\";
        }

        return ecx0;
    }

    // L004CE9E0
    private static S0x004CE208_02 TXvTForm__PROC_004CE9E0(XvTBox XvTForm, S0xTieGlobalGoal edx0)
    {
        S0xTieGlobalGoal esp00 = S0xTieGlobalGoal.FromByteArray(edx0.ToByteArray());
        S0x004CE208_02 esp7A = new();
        esp7A.m000000 = TXvTForm__PROC_004CE8A4(XvTForm, esp00.Triggers);

        esp7A.m000029 = (byte)(esp00.Points / 0x0A);
        esp7A.m000000.m00000A = esp00.Op;
        esp7A.m000028 = 0;

        return esp7A;
    }

    // L004CE8A4
    private static S0x004CE208_02_00 TXvTForm__PROC_004CE8A4(XvTBox XvTForm, S0xTieTriggers edx0)
    {
        S0x004CE208_02_00 esp20 = new();
        esp20.m000000 = TXvTForm__PROC_004CE7B0(XvTForm, edx0.Trigger_0[0]);
        esp20.m000004 = TXvTForm__PROC_004CE7B0(XvTForm, edx0.Trigger_0[1]);
        esp20.m000008 = edx0.TriggerUsed_0[0];
        esp20.m000009 = edx0.TriggerUsed_0[1];
        esp20.m00000A = edx0.Operator_0;
        esp20.m00000B = TXvTForm__PROC_004CE7B0(XvTForm, edx0.Trigger_1[0]);
        esp20.m00000F = TXvTForm__PROC_004CE7B0(XvTForm, edx0.Trigger_1[1]);
        esp20.m000013 = edx0.TriggerUsed_1[0];
        esp20.m000014 = edx0.TriggerUsed_1[1];
        esp20.m000015 = edx0.Operator_1;
        esp20.Operator = edx0.Unused0F_1 != 0;
        return esp20;
    }

    // L004CE7B0
    private static uint TXvTForm__PROC_004CE7B0(XvTBox XvTForm, S0xTieTrigger edx0)
    {
        byte[] esp0A = new byte[4];

        esp0A[0] = (byte)TXvTForm__PROC_004CEA74(XvTForm, edx0.Condition);

        if (edx0.VariableType == TieClassEnum.ShipType)
        {
            esp0A[2] = (byte)(edx0.Variable - 1);
        }
        else
        {
            esp0A[2] = edx0.Variable;
        }

        esp0A[1] = (byte)edx0.VariableType;

        if (edx0.VariableType == TieClassEnum.FlightGroup)
        {
            if (AlliedVariables.s_V0x00541BBC[edx0.Variable] < 0)
            {
                esp0A[0] = 0;
                esp0A[2] = 0;
            }
            else
            {
                esp0A[2] = (byte)AlliedVariables.s_V0x00541BBC[edx0.Variable];
            }
        }

        esp0A[3] = (byte)edx0.Amount;

        uint esp06 = BitConverter.ToUInt32(esp0A, 0);
        return esp06;
    }

    // L004CEA98
    private static CraftIdEnum TXvTForm__PROC_004CEA98(XvTBox XvTForm, CraftIdEnum edx0)
    {
        switch (edx0)
        {
            case CraftIdEnum._010_0_9_IrdFighter:
            case CraftIdEnum._113_0_16_SkiprayBlastBoat:
            case CraftIdEnum._119_0_22_CloakshapeFighter:
            case CraftIdEnum._121_0_24_PlanetaryFighter:
            case CraftIdEnum._122_0_25_SupaFighter:
            case CraftIdEnum._125_0_28_PreybirdFighter:
            case CraftIdEnum._126_0_29_Xwing:
            case CraftIdEnum._127_0_30_SlaveOne:
            case CraftIdEnum._128_0_31_SlaveTwo:
                return CraftIdEnum._015_0_14_R41;

            case CraftIdEnum._011_0_10_ToscanFighter:
            case CraftIdEnum._120_0_23_RazorFighter:
            case CraftIdEnum._123_0_26_Piggyback:
                return CraftIdEnum._013_0_12_Twing;

            case CraftIdEnum._039_0_46_MilleniumFalcon2:
            case CraftIdEnum._110_0_52_FamilyTransport:
                return CraftIdEnum._038_0_45_CorellianTransport2;

            case CraftIdEnum._072_1_2_SatD:
                return CraftIdEnum._071_1_1_SatC;

            case CraftIdEnum._081_1_50_ProbeCapsule:
                return CraftIdEnum._080_1_7_Probe;

            case CraftIdEnum._085_1_8_BuoyFaux:
                return CraftIdEnum._083_1_47_BuoyC;

            case CraftIdEnum._088_1_49_BuoyRendez:
                return CraftIdEnum._084_1_48_BuoyB;

            case CraftIdEnum._089_0_62_CargoCanister:
            case CraftIdEnum._153_0_65_ContainerBox:
            case CraftIdEnum._154_0_66_ContainerSphere:
                return CraftIdEnum._058_0_60_ContainerGem;

            case CraftIdEnum._093_0_96_LancerFrigate:
            case CraftIdEnum._095_0_98_AssaultFrigate:
                return CraftIdEnum._042_0_82_Frigate2;

            case CraftIdEnum._094_0_97_BulkCruiser:
                return CraftIdEnum._045_0_85_CarrackCruiser;

            case CraftIdEnum._096_0_99_CorellianGunship:
            case CraftIdEnum._104_0_74_ModActionTransport:
                return CraftIdEnum._041_0_81_ModCorvette;

            case CraftIdEnum._097_0_48_ImpLandingCraft:
                return CraftIdEnum._017_0_37_Shuttle;

            case CraftIdEnum._098_0_49_AssaultShuttle:
                return CraftIdEnum._022_0_40_AssaultTransport;

            case CraftIdEnum._099_0_100_MarauderCorvette:
                return CraftIdEnum._043_0_83_ModFrigate;

            case CraftIdEnum._100_0_73_StarGalleon:
                return CraftIdEnum._035_0_71_ContainerTransport;

            case CraftIdEnum._101_0_101_ImpResearchShip:
                return CraftIdEnum._047_0_87_EscortCarrier;

            case CraftIdEnum._102_0_50_LuxuryYacht:
                return CraftIdEnum._037_0_44_MuurianTransport;

            case CraftIdEnum._103_0_51_FerryboatLiner:
                return CraftIdEnum._044_0_84_PassengerLiner;

            case CraftIdEnum._105_0_75_MobquetTransport:
            case CraftIdEnum._106_0_76_XiytiarTransport:
                return CraftIdEnum._034_0_70_ModularConveyor;

            case CraftIdEnum._107_0_77_FreighterConB:
            case CraftIdEnum._108_0_78_FreighterConG:
            case CraftIdEnum._109_0_79_FreighterBox:
            case CraftIdEnum._164_0_136_CrewCabinFront:
            case CraftIdEnum._165_0_137_ConnectorRod:
            case CraftIdEnum._166_0_138_EngineBack:
            case CraftIdEnum._167_0_0_Xwing:
            case CraftIdEnum._168_0_0_Xwing:
            case CraftIdEnum._169_0_139_EngineFront:
            case CraftIdEnum._170_0_140_CrewCabinBack:
            case CraftIdEnum._171_0_0_Xwing:
            case CraftIdEnum._172_0_0_Xwing:
            case CraftIdEnum._173_0_0_Xwing:
                return CraftIdEnum._032_0_68_BulkFreighter;

            case CraftIdEnum._114_0_17_TieBizarro:
            case CraftIdEnum._115_0_18_TieBigGun:
            case CraftIdEnum._116_0_19_TieWarheads:
            case CraftIdEnum._117_0_20_TieBomb:
            case CraftIdEnum._118_0_21_TieBooster:
                return CraftIdEnum._005_0_4_TieFighter;

            case CraftIdEnum._129_0_112_GolanOne:
            case CraftIdEnum._130_0_113_GolanTwo:
            case CraftIdEnum._131_0_114_GolanThree:
            case CraftIdEnum._143_0_126_RebelPlatform:
            case CraftIdEnum._145_0_128_FamilyBase:
                return CraftIdEnum._090_0_110_ShipYard;

            case CraftIdEnum._132_0_115_DerilynPlatform:
            case CraftIdEnum._135_0_118_SpaceColony1:
            case CraftIdEnum._136_0_119_SpaceColony2:
            case CraftIdEnum._137_0_120_SpaceColony3:
            case CraftIdEnum._138_0_121_Casino:
            case CraftIdEnum._139_0_122_CargoFacility1:
            case CraftIdEnum._140_0_123_CargoFacility2:
            case CraftIdEnum._144_0_127_ImpResearchCenter:
                return CraftIdEnum._069_0_109_Factory;

            case CraftIdEnum._133_0_116_SensorArray:
            case CraftIdEnum._134_0_117_CommRelay:
                return CraftIdEnum._070_1_0_SatB;

            case CraftIdEnum._141_0_124_AsteroidMiningUnit:
            case CraftIdEnum._142_0_125_ProcessingPlant:
                return CraftIdEnum._060_0_102_Platform1;

            case CraftIdEnum._146_0_129_FamilyRepairYard:
                return CraftIdEnum._091_0_111_RepairYard;

            case CraftIdEnum._147_0_130_PirateShipyard:
                return CraftIdEnum._066_0_108_AsteroidBase;

            case CraftIdEnum._151_0_63_PropaneTank:
            case CraftIdEnum._155_0_67_ContainerHanger:
                return CraftIdEnum._026_0_53_ContainerBrick;

            case CraftIdEnum._152_0_64_ContainerGrande:
                return CraftIdEnum._059_0_61_ContainerYshaped;

            case CraftIdEnum._156_1_43_GunPad:
            case CraftIdEnum._157_1_44_GunWarheadPad:
                return CraftIdEnum._078_1_6_GunPlatform;

            case CraftIdEnum._158_1_45_ProximityMineA:
            case CraftIdEnum._159_1_46_ProximityMineB:
            case CraftIdEnum._160_1_41_HomingMineA:
            case CraftIdEnum._161_1_42_HomingMineB:
            case CraftIdEnum._162_1_40_LaserBat:
                return CraftIdEnum._075_1_3_MineA;

            case CraftIdEnum._163_1_39_IonBat:
                return CraftIdEnum._076_1_4_MineB;

            case CraftIdEnum._174_0_153_EscapePodA:
            case CraftIdEnum._175_1_51_RebelPilot:
            case CraftIdEnum._176_1_52_ImperialPilot:
            case CraftIdEnum._177_1_53_CivilianPilot:
            case CraftIdEnum._178_1_54_ZeroGStormtrooper:
            case CraftIdEnum._179_1_55_ZeroGUtility:
            case CraftIdEnum._180_1_56_Marko:
            case CraftIdEnum._181_1_57_R2D2:
            case CraftIdEnum._182_1_57_R2D2:
                return CraftIdEnum._083_1_47_BuoyC;

            case CraftIdEnum._183_9001_1100_ResData_Backdrop:
                return CraftIdEnum._087_1_11_AsteroidHR1;

            case CraftIdEnum._228_0_135_CalamariWinged:
                return CraftIdEnum._049_0_89_CalamariCruiserNew;

            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
                return CraftIdEnum._052_0_92_VictoryStarDestroyer2;

            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                return CraftIdEnum._053_0_93_ImperialStarDestroyer2;
        }

        return edx0;
    }

    // L004CEA74
    private static TieConditionEnum TXvTForm__PROC_004CEA74(XvTBox XvTForm, TieConditionEnum edx0)
    {
        if (edx0 == TieConditionEnum.Arrival)
        {
            return TieConditionEnum.Created;
        }

        if (edx0 == TieConditionEnum.Departure)
        {
            return TieConditionEnum.ComeAndGo;
        }

        if (edx0 <= TieConditionEnum.Departure)
        {
            return edx0;
        }

        return TieConditionEnum.Always;
    }

    // L004CEA64
    private static byte TXvTForm__PROC_004CEA64(XvTBox XvTForm, byte edx0)
    {
        return (byte)(edx0 / 0x05);
    }

    // L004CCFD8
    public static void TXvTForm_Proc_004CCFD8(XvTBox XvTForm, MissionExportTypeEnum edx0)
    {
        AlliedVariables.s_V0x00541BA8 = AlliedVariables.s_CurrentRegion;
        AlliedVariables.s_V0x00541BAC = 0x01;
        AlliedVariables.s_V0x00541BB4 = edx0;

        AlliedVariables.s_TXvTForm_Instance = MainImpl.CreateXvTBox();

        switch (edx0)
        {
            case MissionExportTypeEnum.XvT:
                Controls_TControl_SetText(AlliedVariables.s_TXvTForm_Instance, "Export to XvT file");
                break;

            case MissionExportTypeEnum.BoP:
                Controls_TControl_SetText(AlliedVariables.s_TXvTForm_Instance, "Export to BoP file");
                break;
        }

        AlliedVariables.s_TXvTForm_Instance.RegionPickBox.AddItem("All Regions");

        S0xTieRegion[] ebx1 = AlliedVariables.s_TieFileHeader.Header.Regions;

        for (int esi = 0x01; esi < 0x05; esi++)
        {
            S0xTieRegion ebx = ebx1[esi - 1];
            string ebp08 = string.Format(CultureInfo.InvariantCulture, "Region {0}: {1}", esi, ebx.Name);
            AlliedVariables.s_TXvTForm_Instance.RegionPickBox.AddItem(ebp08);
        }

        AlliedVariables.s_TXvTForm_Instance.RegionPickBox.SelectedIndex = AlliedVariables.s_V0x00541BA8;
        AlliedVariables.s_TXvTForm_Instance.OrderPickBox.SetItems(AlliedVariables.s_TXvTForm_Instance.RegionPickBox.Items);
        AlliedVariables.s_TXvTForm_Instance.OrderPickBox.DeleteItem(0);
        AlliedVariables.s_TXvTForm_Instance.OrderPickBox.SelectedIndex = AlliedVariables.s_V0x00541BA8 - 1;

        if (AlliedVariables.s_TieFileHeader.Header.MissionType == 0x04)
        {
            AlliedVariables.s_TXvTForm_Instance.MissTypePickBox.SelectedIndex = 0x02;
        }
        else
        {
            AlliedVariables.s_TXvTForm_Instance.MissTypePickBox.SelectedIndex = 0;
        }

        AlliedVariables.s_TXvTForm_Instance.Owner = Application.Current.MainWindow;
        AlliedVariables.s_TXvTForm_Instance.ShowDialog();

        AlliedVariables.s_V0x00541BA8 = AlliedVariables.s_TXvTForm_Instance.RegionPickBox.SelectedIndex;
        AlliedVariables.s_V0x00541BAC = AlliedVariables.s_TXvTForm_Instance.OrderPickBox.SelectedIndex + 1;
        AlliedVariables.s_V0x00541BB8 = AlliedVariables.s_TXvTForm_Instance.MissTypePickBox.SelectedIndex;

        AlliedVariables.s_AlliedForm1Window!.ExportSaveDlg.FileName = TXvTForm__PROC_004CED60(AlliedVariables.s_TXvTForm_Instance);

        if (AlliedVariables.s_TXvTForm_Instance.DialogResult == true)
        {
            if (AlliedVariables.s_AlliedForm1Window!.ExportSaveDlg.ShowDialog() == true)
            {
                string ebp28 = AlliedVariables.s_AlliedForm1Window!.ExportSaveDlg.FileName;
                TXvTForm_Proc_004CD330(AlliedVariables.s_TXvTForm_Instance, ebp28, edx0);
            }
        }

        AlliedVariables.s_TXvTForm_Instance = null;
    }

    // L004CD330
    private static void TXvTForm_Proc_004CD330(XvTBox XvTForm, string edx0, MissionExportTypeEnum ecx0)
    {
        AlliedVariables.s_TieFileHandle.Assign(edx0);
        AlliedVariables.s_TieFileHandle.OpenFileForWrite(0x01);
        System_L004028C4_CheckError();

        AlliedVariables.s_V0x00543C70 = 0;
        AlliedVariables.s_V0x00543C74 = 0;

        AlliedVariables.s_TieFileHandle.BlockWrite(BitConverter.GetBytes((ushort)ecx0), 0x02, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
        TXvTForm__PROC_004CD4C4(XvTForm);
        TXvTForm__PROC_004CD5C0(XvTForm);
        TXvTForm_Proc_004CE090(XvTForm);
        TXvTForm__PROC_004CE208(XvTForm);
        Allied_WriteTieMission_Teams(AlliedVariables.s_AlliedForm1Window!);
        TXvTForm__PROC_004CE2B0(XvTForm);
        TXvTForm_Proc_004CE6D0(XvTForm);
        TXvTForm_Proc_004CE734(XvTForm);

        switch (ecx0)
        {
            case MissionExportTypeEnum.XvT:
                AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_Description, 0x400, AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();
                break;

            case MissionExportTypeEnum.BoP:
                AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_WinDebriefing, 0x1000, AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();
                AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_LostDebriefing, 0x1000, AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();
                AlliedVariables.s_TieFileHandle.BlockWrite(AlliedVariables.s_TieMission_Description, 0x1000, AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();
                break;
        }

        AlliedVariables.s_TieFileHandle.Close();
        System_L004028C4_CheckError();
    }

    // L004CD4C4
    private static void TXvTForm__PROC_004CD4C4(XvTBox XvTForm)
    {
        for (int ebx = 0; ebx < 0x97; ebx++)
        {
            AlliedVariables.s_V0x00541BBC[ebx] = -1;
        }

        AlliedVariables.s_V0x00541BB0 = 0;

        short esi = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

            if (Unit_00511CD0_Proc_005125E4(AlliedVariables.s_V0x00541BA8, BitConverter.ToUInt32(eax1.m001444, 0)) == 0)
            {
                continue;
            }

            if (ebx <= 0x96)
            {
                AlliedVariables.s_V0x00541BBC[ebx] = AlliedVariables.s_V0x00541BB0;
            }

            AlliedVariables.s_V0x00541BB0 += 1;
        }

        S0x004CD4C4 esp00 = new();
        esp00.m000000 = (short)AlliedVariables.s_V0x00541BB0;
        esp00.m000002 = AlliedVariables.s_TieFileHeader.RadioMessagesCount;
        esp00.m000062 = (byte)AlliedVariables.s_V0x00541BB8;
        esp00.m000064 = AlliedVariables.s_TieFileHeader.Header.TimeLimit;
        esp00.m000065 = 0;

        for (int ebx = 0; ebx < 0x5E; ebx++)
        {
            esp00.m000004[ebx] = 0;
        }

        esp00.m000063 = 0;

        for (int ebx = 0; ebx < 0x3C; ebx++)
        {
            esp00.m000066[ebx] = 0;
        }

        AlliedVariables.s_TieFileHandle.BlockWrite(esp00.ToByteArray(), 0xA2, AlliedVariables.s_V0x00543C44);
        System_L004028C4_CheckError();
    }

    // L005125E4
    private static byte Unit_00511CD0_Proc_005125E4(int eax0, uint edx0)
    {
        byte dl = 0;

        if (eax0 == 0)
        {
            dl = 0x01;
        }
        else if (BitConverter.GetBytes(edx0)[eax0 - 1] != 0)
        {
            dl = 0x01;
        }

        return dl;
    }

    // L004CE93C
    private static S0x004CD5C0_03 TXvTForm__PROC_004CE93C(XvTBox XvTForm, S0xTieFlightGroupGoal edx0)
    {
        S0x004CD5C0_03 esp50 = new();
        esp50.GoalType = edx0.GoalType;
        esp50.Condition = TXvTForm__PROC_004CEA74(XvTForm, edx0.Condition);
        esp50.Amount = edx0.Amount;
        esp50.Points = (byte)(edx0.Points / 0x0A);

        for (int eax = 0; eax < 0x0A; eax++)
        {
            esp50.AppliesToTeams[eax + 1] = edx0.AppliesToTeams[eax + 1];
        }

        esp50.AppliesToTeams[9] = edx0.SequenceNumber;
        esp50.AppliesToTeams[0] = edx0.AppliesToTeams[0];

        for (int eax = 0; eax < 0x40; eax++)
        {
            esp50.m00000E[eax] = edx0.m000010[eax];
        }

        return esp50;
    }

    // L004CE838
    private static S0x004CE838 TXvTForm__PROC_004CE838(XvTBox XvTForm, S0xTieFlightGroupTriggerPair edx0)
    {
        S0x004CE838 esp10 = new();
        esp10.m000000 = TXvTForm__PROC_004CE7B0(XvTForm, edx0.Triggers[0]);
        esp10.m000004 = TXvTForm__PROC_004CE7B0(XvTForm, edx0.Triggers[1]);
        esp10.m000008 = edx0.TriggerUsed[0];
        esp10.m000009 = edx0.TriggerUsed[1];
        esp10.m00000A = edx0.Operator;
        return esp10;
    }

    // L004CE734
    private static void TXvTForm_Proc_004CE734(XvTBox XvTForm)
    {
        for (int edi = 0; edi < 0x0A; edi++)
        {
            S0xXvTGGStrings[] esp0000 = new S0xXvTGGStrings[28];

            for (int esi = 0; esi < 0x03; esi++)
            {
                S0xXvTGGStrObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00571188, edi);
                for (int i = 0; i < 4; i++)
                {
                    esp0000[esi * 4 + i] = S0xXvTGGStrings.FromByteArray(eax1.GGStrings[esi * 4 + i].ToByteArray());
                }
            }

            byte[] buffer = new byte[0x1500];
            for (int i = 0; i < 28; i++)
            {
                esp0000[i].ToByteArray().CopyTo(buffer, i * 0xC0);
            }

            AlliedVariables.s_TieFileHandle.BlockWrite(buffer, 0x1500, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L004CE6D0
    private static void TXvTForm_Proc_004CE6D0(XvTBox XvTForm)
    {
        int ebx = AlliedVariables.s_V0x00541BB0;

        for (int esi = 0; esi < ebx; esi++)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
            AlliedVariables.s_TieFileHandle.BlockWrite(eax1.m000E42.ToByteArray(), 0x600, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L004CE2B0
    private static void TXvTForm__PROC_004CE2B0(XvTBox XvTForm)
    {
        bool ebp0D = false;

        const double d_004CE6B8 = 0.8;
        const double d_004CE6C4 = 0.71;

        for (int ebp04 = 0; ebp04 < 0x08; ebp04++)
        {
            S0xTieBriefing ebp612C = AlliedVariables.s_Allied_Briefing[ebp04].Clone();
            S0xXvTTieBriefingCode ebp348 = new();

            ebp348.Length = (short)Math.Round(ebp612C.BriefingData.BriefingCode.Length * d_004CE6B8);
            ebp348.Time = ebp612C.BriefingData.BriefingCode.Time;
            ebp348.Index = ebp612C.BriefingData.BriefingCode.Index;
            ebp348.Title = ebp612C.BriefingData.BriefingCode.Title;

            for (int ebx = 0; ebx < 0x08; ebx++)
            {
                ebp348.ForTeam[ebx] = ebp612C.BriefingData.BriefingCode.ForTeam[ebx];
            }

            for (int ebx = 0; ebx < 0x190; ebx++)
            {
                ebp348.m00000A[ebx] = 0;
            }

            int esi1 = 0x01;
            int ebx1 = 0x01;

            while (true)
            {
                short di = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];

                if (di == 0x270F)
                {
                    ebp348.m00000A[esi1 - 1] = (short)Math.Round((double)di);
                    ebp0D = true;
                }
                else
                {
                    ebp348.m00000A[esi1 - 1] = (short)Math.Round(di * d_004CE6B8);
                }

                ebx1++;

                BriefingCommandEnum ax1 = (BriefingCommandEnum)ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];

                if (ax1 >= BriefingCommandEnum.PageBreak && ax1 < BriefingCommandEnum.NewIcon)
                {
                    esi1++;
                    ebp348.m00000A[esi1 - 1] = (short)ax1;
                    esi1++;
                }

                ebx1++;

                switch (ax1)
                {
                    case BriefingCommandEnum.BriefingTitle:
                    case BriefingCommandEnum.BriefingText:
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        break;

                    case BriefingCommandEnum.MoveMap:
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        break;

                    case BriefingCommandEnum.ScaleMap:
                        ebp348.m00000A[esi1 - 1] = (short)Math.Round(ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1] * d_004CE6C4);
                        ebx1++;
                        esi1++;
                        ebp348.m00000A[esi1 - 1] = (short)Math.Round(ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1] * d_004CE6C4);
                        ebx1++;
                        esi1++;
                        break;

                    case BriefingCommandEnum.FlightGroupTags1:
                    case BriefingCommandEnum.FlightGroupTags2:
                    case BriefingCommandEnum.FlightGroupTags3:
                    case BriefingCommandEnum.FlightGroupTags4:
                    case BriefingCommandEnum.FlightGroupTags5:
                    case BriefingCommandEnum.FlightGroupTags6:
                    case BriefingCommandEnum.FlightGroupTags7:
                    case BriefingCommandEnum.FlightGroupTags8:
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        break;

                    case BriefingCommandEnum.TextTag1:
                    case BriefingCommandEnum.TextTag2:
                    case BriefingCommandEnum.TextTag3:
                    case BriefingCommandEnum.TextTag4:
                    case BriefingCommandEnum.TextTag5:
                    case BriefingCommandEnum.TextTag6:
                    case BriefingCommandEnum.TextTag7:
                    case BriefingCommandEnum.TextTag8:
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        ebp348.m00000A[esi1 - 1] = ebp612C.BriefingData.BriefingCode.m00000A[ebx1 - 1];
                        ebx1++;
                        esi1++;
                        break;

                    case BriefingCommandEnum.NewIcon:
                        ebx1 += 0x03;
                        break;

                    case BriefingCommandEnum.ShowShipData:
                    case BriefingCommandEnum.RotateIcon:
                        ebx1 += 0x02;
                        break;

                    case BriefingCommandEnum.MoveIcon:
                        ebx1 += 0x03;
                        break;

                    case BriefingCommandEnum.ChangeRegion:
                        ebx1++;
                        break;
                }

                if (esi1 >= 0x190)
                {
                    break;
                }

                if (ebx1 > 0x1AF4)
                {
                    break;
                }

                if (ebp0D)
                {
                    break;
                }
            }

            ebp348.CodeSize = (short)(esi1 + 1);

            AlliedVariables.s_TieFileHandle.BlockWrite(ebp348.ToByteArray(), 0x334, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();

            for (int ebx = 0; ebx < 0x20; ebx++)
            {
                string ebp0C = ebp612C.BriefingTags.GetText(ebx);
                short ebp08 = (short)ebp0C.Length;
                AlliedVariables.s_TieFileHandle.BlockWrite(BitConverter.GetBytes(ebp08), 0x02, AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();

                for (int edi = 0; edi < ebp08; edi++)
                {
                    byte eax = (byte)ebp0C[edi];
                    AlliedVariables.s_TieFileHandle.BlockWrite(new byte[] { eax }, 0x01, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }
            }

            for (int ebx = 0; ebx < 0x20; ebx++)
            {
                string ebp0C = ebp612C.BriefingStrings.GetText(ebx);
                short ebp08 = (short)ebp0C.Length;
                AlliedVariables.s_TieFileHandle.BlockWrite(BitConverter.GetBytes(ebp08), 0x02, AlliedVariables.s_V0x00543C44);
                System_L004028C4_CheckError();

                for (int edi = 0; edi < ebp08; edi++)
                {
                    byte eax = (byte)ebp0C[edi];
                    AlliedVariables.s_TieFileHandle.BlockWrite(new byte[] { eax }, 0x01, AlliedVariables.s_V0x00543C44);
                    System_L004028C4_CheckError();
                }
            }
        }
    }

    // L004CE208
    private static void TXvTForm__PROC_004CE208(XvTBox XvTForm)
    {
        for (int esp04 = 0; esp04 < 0x0A; esp04++)
        {
            S0xTieGlobalGoalObject edi = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, esp04);

            S0x004CE208_00 esp08 = new();
            esp08.m000000 = 0x03;

            for (int esi = 0; esi < 0x03; esi++)
            {
                esp08.m000002[esi] = TXvTForm__PROC_004CE9E0(XvTForm, edi.GlobalGoal.GlobalGoals[esi]);
                esp08.m000002[esi].m000000.Operator = edi.GlobalGoal.GlobalGoals[esi].Op;
            }

            AlliedVariables.s_TieFileHandle.BlockWrite(esp08.ToByteArray(), 0x80, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L004CE090
    private static void TXvTForm_Proc_004CE090(XvTBox XvTForm)
    {
        int ebp0C = AlliedVariables.s_TieFileHeader.RadioMessagesCount;

        for (int ebp08 = 0; ebp08 < ebp0C; ebp08++)
        {
            S0xTieRadioMessageObject esi = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebp08);

            S0x004CE090 ebp80 = new();

            switch (esi.RadioMessage.Side)
            {
                case 0x00:
                case 0x05:
                    ebp80.m000002 = 0x31;
                    break;

                case 0x01:
                case 0x04:
                    ebp80.m000002 = 0x20;
                    break;

                case 0x02:
                    ebp80.m000002 = 0x32;
                    break;

                case 0x03:
                    ebp80.m000002 = 0x33;
                    break;
            }

            string ebpC4 = System_LStrFromPCharLen(esi.RadioMessage.Message, 0x40);
            ebp80.m000003 = Unit_00511CD0_Proc_00511FC4(ebpC4);
            ebp80.m000000 = esi.RadioMessage.Id;

            for (int edx = 0; edx < 0x0A; edx++)
            {
                ebp80.ForTeam[edx] = esi.RadioMessage.ForTeam[edx];
            }

            ebp80.m00004C = TXvTForm__PROC_004CE8A4(XvTForm, esi.RadioMessage.Condition);
            ebp80.m00004C.Operator = esi.RadioMessage.Operator;
            ebp80.m00004C.TimePassed = TXvTForm__PROC_004CEA64(XvTForm, esi.RadioMessage.TimePassed);

            AlliedVariables.s_TieFileHandle.BlockWrite(ebp80.ToByteArray(), 0x74, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L004CD5C0
    private static void TXvTForm__PROC_004CD5C0(XvTBox XvTForm)
    {
        AlliedVariables.s_V0x0053BDB0 = 0;
        AlliedVariables.s_V0x0053BDB4 = 0;

        int ebp20 = AlliedVariables.s_TieFileHeader.FlightGroupsCount;

        for (int ebp08 = 0; ebp08 < ebp20; ebp08++)
        {
            S0xFGObject ebp14 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp08);

            if (Unit_00511CD0_Proc_005125E4(AlliedVariables.s_V0x00541BA8, BitConverter.ToUInt32(ebp14.m001444, 0)) == 0)
            {
                continue;
            }

            S0x004CD5C0_00 ebp582 = new();

            ebp582.m000085 = 0;
            ebp582.m000465 = 0;

            for (int esi = 0; esi < 0x04; esi++)
            {
                ebp582.m000516[esi] = 0;
            }

            for (int esi = 0; esi < 0x08; esi++)
            {
                ebp582.m000528[esi] = 0;
            }

            ebp582.m000561 = 0;
            ebp582.Name = ebp14.FlightGroupStruct.Name;
            ebp582.Cargo = ebp14.FlightGroupStruct.Cargo;
            ebp582.SpecialCargo = ebp14.FlightGroupStruct.SpecialCargo;
            ebp14.FlightGroupStruct.ToByteArray().Subarray(0x0069, 0x1E).CopyTo(ebp582.m000050, 0);
            ebp582.m000050[2] = (byte)TXvTForm__PROC_004CEA98(XvTForm, ebp14.FlightGroupStruct.CraftId);
            ebp582.m000050[29] = (byte)ebp14.FlightGroupStruct.ArrivalDifficulty;

            string ebp1C_1 = string.Empty;

            if (ebp14.FlightGroupStruct.TacticalRoleUsed0 == TacticalRoleUsedEnum.NoTC)
            {
                switch (ebp14.FlightGroupStruct.TacticalRole0)
                {
                    case TacticalRoleEnum.COMmand:
                        ebp1C_1 = "1COM";
                        break;

                    case TacticalRoleEnum.BASe:
                        ebp1C_1 = "1BAS";
                        break;

                    case TacticalRoleEnum.STAtion:
                        ebp1C_1 = "1STA";
                        break;

                    case TacticalRoleEnum.MISsionCritical:
                        ebp1C_1 = "1MIS";
                        break;

                    case TacticalRoleEnum.CONvoy:
                        ebp1C_1 = "1CON";
                        break;

                    case TacticalRoleEnum.STRike:
                        ebp1C_1 = "1STR";
                        break;

                    case TacticalRoleEnum.RELoad:
                        ebp1C_1 = "1REL";
                        break;

                    case TacticalRoleEnum.PRImary:
                        ebp1C_1 = "1PRI";
                        break;

                    case TacticalRoleEnum.SECondary:
                        ebp1C_1 = "1SEC";
                        break;

                    case TacticalRoleEnum.TERtiary:
                        ebp1C_1 = "1TER";
                        break;

                    case TacticalRoleEnum.RESearch:
                        ebp1C_1 = "1RES";
                        break;

                    case TacticalRoleEnum.MANufacturing:
                        ebp1C_1 = "1MAN";
                        break;
                }
            }

            ebp582.M000014 = Unit_00511CD0_Proc_00511EB8(ebp1C_1);
            ebp14.FlightGroupStruct.ToByteArray().Subarray(0x0DBE, 0x0E).CopyTo(ebp582.m00051A, 0);
            ebp582.m00006E = TXvTForm__PROC_004CE838(XvTForm, ebp14.FlightGroupStruct.ArrivalTrigger1);
            ebp582.m000079 = TXvTForm__PROC_004CE838(XvTForm, ebp14.FlightGroupStruct.ArrivalTrigger2);
            ebp582.m000084 = ebp14.FlightGroupStruct.ArrivalTriggersOperator;
            ebp582.m000086 = ebp14.FlightGroupStruct.ArrivalDelayMinutes;
            ebp582.m000087 = ebp14.FlightGroupStruct.ArrivalDelaySeconds;
            ebp582.m000088 = TXvTForm__PROC_004CE838(XvTForm, ebp14.FlightGroupStruct.DepartureTrigger);
            ebp14.FlightGroupStruct.ToByteArray().Subarray(0x00BC, 0x06).CopyTo(ebp582.m000093, 0);
            ebp14.FlightGroupStruct.ToByteArray().Subarray(0x00C2, 0x08).CopyTo(ebp582.m00009A, 0);

            for (int esi = 0x01, eax = 0x08, edx = 0x09; esi < 0x05; esi++)
            {
                if (ebp582.m00009A[edx - 0x08] == 0x01)
                {
                    int ecx = ebp582.m00009A[eax - 0x08];

                    if (AlliedVariables.s_V0x00541BBC[ecx] > -1)
                    {
                        ebp582.m00009A[eax - 0x08] = (byte)AlliedVariables.s_V0x00541BBC[ecx];
                    }
                    else
                    {
                        ebp582.m00009A[eax - 0x08] = 0;
                        ebp582.m00009A[edx - 0x08] = 0;
                    }
                }

                switch (esi)
                {
                    case 0x01:
                        eax = 0x0A;
                        edx = 0x0B;
                        break;

                    case 0x02:
                        eax = 0x0C;
                        edx = 0x0D;
                        break;

                    case 0x03:
                        eax = 0x0E;
                        edx = 0x0F;
                        break;
                }
            }

            int ebp0C = AlliedVariables.s_V0x00541BAC;

            for (int ebp10 = 0x01; ebp10 < 0x05; ebp10++)
            {
                S0x004CD5C0_01 ebx = ebp582.m0000A2[ebp10 - 1];

                for (int esi = 0x01; esi < 0x14; esi++)
                {
                    if (esi == 0x03)
                    {
                        TieOrderIdEnum al1 = ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].OrderId;

                        switch (al1)
                        {
                            case TieOrderIdEnum._12_BoardToGive:
                            case TieOrderIdEnum._13_BoardToTake:
                            case TieOrderIdEnum._14_BoardToExchange:
                            case TieOrderIdEnum._15_BoardToCapture:
                            case TieOrderIdEnum._16_BoardToDestroy:
                            case TieOrderIdEnum._17_BoardToPickup:
                            case TieOrderIdEnum._18_DropOff:
                            case TieOrderIdEnum._19_Wait:
                            case TieOrderIdEnum._20_Wait:
                            case TieOrderIdEnum._31_BoardToContact:
                            case TieOrderIdEnum._32_BoardToRepair:
                            case TieOrderIdEnum._36_SelfDestroy:
                                ebx.m000000[esi - 1] = TXvTForm__PROC_004CEA64(XvTForm, ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].Var0);
                                break;

                            default:
                                ebx.m000000[esi - 1] = ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].Var0;
                                break;
                        }
                    }
                    else
                    {
                        ebx.m000000[esi - 1] = ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10 - 1)].ToByteArray()[esi - 1];
                    }
                }

                switch ((TieOrderIdEnum)ebx.m000000[0])
                {
                    case TieOrderIdEnum._40_Deliver:
                        ebx.m000000[0] = (byte)TieOrderIdEnum._18_DropOff;
                        break;

                    case TieOrderIdEnum._42_CapFree:
                        ebx.m000000[0] = (byte)TieOrderIdEnum._07_CapFree;
                        break;

                    case TieOrderIdEnum._44_Disabled:
                        ebx.m000000[0] = (byte)TieOrderIdEnum._06_WaitForBoard;
                        break;

                    case TieOrderIdEnum._45_RepairOneself:
                        ebx.m000000[0] = (byte)TieOrderIdEnum._32_BoardToRepair;
                        break;

                    case TieOrderIdEnum._50_Hyperspace:
                        ebx.m000000[0] = (byte)TieOrderIdEnum._01_FlyHome;
                        break;

                    case TieOrderIdEnum._52_TransferCargo:
                        ebx.m000000[0] = (byte)TieOrderIdEnum._14_BoardToExchange;
                        break;

                    default:
                        if (ebx.m000000[0] > (byte)TieOrderIdEnum._39_Release)
                        {
                            ebx.m000000[0] = (byte)TieOrderIdEnum._00_Stationary;
                        }

                        break;
                }
            }

            for (int ebp10 = 0; ebp10 < 0x04; ebp10++)
            {
                S0x004CD5C0_01 ebx = ebp582.m0000A2[ebp10];

                for (int esi = 0; esi < 0x13; esi++)
                {
                    switch (esi)
                    {
                        case 0x06: // ClassA
                        case 0x07: // ClassB
                            if (ebx.m000000[esi + 0] == 0x02)
                            {
                                ebx.m000000[esi + 2]--;
                            }
                            else if (ebx.m000000[esi + 0] == 0x01)
                            {
                                if (AlliedVariables.s_V0x00541BBC[ebx.m000000[esi + 2]] < 0)
                                {
                                    ebx.m000000[esi + 0] = 0;
                                    ebx.m000000[esi + 2] = 0;
                                }
                                else
                                {
                                    ebx.m000000[esi + 2] = (byte)AlliedVariables.s_V0x00541BBC[ebx.m000000[esi + 2]];
                                }
                            }

                            break;

                        case 0x0C: // ClassA
                        case 0x0E: // ClassB
                            if (ebx.m000000[esi + 0] == 0x02)
                            {
                                ebx.m000000[esi + 1]--;
                            }
                            else if (ebx.m000000[esi + 0] == 0x01)
                            {
                                if (AlliedVariables.s_V0x00541BBC[ebx.m000000[esi + 1]] < 0)
                                {
                                    ebx.m000000[esi + 0] = 0;
                                    ebx.m000000[esi + 1] = 0;
                                }
                                else
                                {
                                    ebx.m000000[esi + 1] = (byte)AlliedVariables.s_V0x00541BBC[ebx.m000000[esi + 1]];
                                }
                            }

                            break;
                    }
                }
            }

            for (int ebp10 = 0; ebp10 < 0x04; ebp10++)
            {
                ebp582.m0000A2[ebp10].m000013 = Unit_00511CD0_Proc_00511FC4(string.Empty);
            }

            switch ((TieOrderIdEnum)ebp582.m0000A2[0].m000000[0])
            {
                case TieOrderIdEnum._07_CapFree:
                case TieOrderIdEnum._08_CapEscorters:
                    ebp582.m0000A2[0].m000013 = Unit_00511CD0_Proc_00511FC4("Attack");
                    break;

                case TieOrderIdEnum._09_CapRespond:
                    ebp582.m0000A2[0].m000013 = Unit_00511CD0_Proc_00511FC4("Protect");
                    break;

                case TieOrderIdEnum._10_Escort:
                    ebp582.m0000A2[0].m000013 = Unit_00511CD0_Proc_00511FC4("Escort");
                    break;

                case TieOrderIdEnum._11_Disable:
                    ebp582.m0000A2[0].m000013 = Unit_00511CD0_Proc_00511FC4("Disable");
                    break;
            }

            ebp582.m0001EA = TXvTForm__PROC_004CE838(XvTForm, ebp14.FlightGroupStruct.JumpTriggers[(ebp0C - 1) * 4]);

            for (int esi = 0; esi < 0x03; esi++)
            {
                for (int eax = 0x01; eax < 0x05; eax++)
                {
                    ebp582.m000466[esi].m000000[eax - 1] = ebp14.m00147C[esi].M000000[eax - 1];
                }
            }

            for (int eax = 0x01; eax < 0x05; eax++)
            {
                ebp582.m000466[3].m000000[eax - 1] = ebp14.IsWPEnabled[eax - 1];
            }

            ebp0C = AlliedVariables.s_V0x00541BAC;

            int ebp10a = 0x01;

            for (int esi = 0; esi < 0x03; esi++)
            {
                for (int eax = 0x05; eax < 0x0D; eax++)
                {
                    ebp582.m000466[esi].m000008[eax - 0x05] = ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10a - 1)].Waypoints[eax - 0x05].Position[0];
                }
            }

            for (int eax = 0x05; eax < 0x0D; eax++)
            {
                ebp582.m000466[3].m000008[eax - 0x05] = ebp14.FlightGroupStruct.Orders[(ebp0C - 1) * 4 + (ebp10a - 1)].Waypoints[eax - 0x05].IsUsed;
            }

            for (int esi = 0; esi < 0x04; esi++)
            {
                ebp582.m000466[esi].m000018 = 0;
                ebp582.m000466[esi].m00001A = 0;
            }

            if (ebp14.IsWPEnabled[3] == 0x01)
            {
                for (int esi = 0; esi < 0x04; esi++)
                {
                    ebp582.m000466[esi].m00001A = ebp14.m00147C[esi].M000000[3];
                    ebp582.m000466[esi].m000000[3] = 0;
                }
            }

            for (int esi = 0; esi < 0x03; esi++)
            {
                for (int eax = 0; eax < 0x08; eax++)
                {
                    ebp582.m000466[esi].m00001C[eax] = ebp582.m000466[esi].m000000[0];
                }
            }

            ebp582.m000466[3].m00001C[0] = 0x01;

            for (int esi = 0; esi < 0x08; esi++)
            {
                ebp582.m0001F5[esi] = TXvTForm__PROC_004CE93C(XvTForm, ebp14.FlightGroupStruct.Goals[esi]);
            }

            for (int esi = 0; esi < 0x08; esi++)
            {
                ebp582.OptionalWarheads[esi] = ebp14.FlightGroupStruct.OptionalWarheads[esi];
            }

            for (int esi = 0; esi < 0x06; esi++)
            {
                ebp582.OptionalBeams[esi] = ebp14.FlightGroupStruct.OptionalBeams[esi];
            }

            for (int esi = 0; esi < 0x04; esi++)
            {
                ebp582.OptionalCounterMeasures[esi] = ebp14.FlightGroupStruct.OptionalCounterMeasures[esi];
            }

            ebp582.OptionalCraftCategory = ebp14.FlightGroupStruct.OptionalCraftCategory;

            for (int esi = 0; esi < 0x08; esi++)
            {
                ebp582.OptionalCraftsId[esi] = ebp14.FlightGroupStruct.OptionalCraftsId[esi];
            }

            for (int esi = 0; esi < 0x06; esi++)
            {
                ebp582.OptionalCraftsCount[esi] = ebp14.FlightGroupStruct.OptionalCraftsCount[esi];
            }

            for (int esi = 0; esi < 0x04; esi++)
            {
                ebp582.OptionalCraftsWaves[esi] = ebp14.FlightGroupStruct.OptionalCraftsWaves[esi];
            }

            AlliedVariables.s_TieFileHandle.BlockWrite(ebp582.ToByteArray(), 0x562, AlliedVariables.s_V0x00543C44);
            System_L004028C4_CheckError();
        }
    }

    // L0051D4BC
    private static void Unit_00513838_Proc_0051D4BC()
    {
        byte[] buffer = new byte[0x400];
        AlliedVariables.s_TieFileHandle.BlockRead(buffer, 0x400, AlliedVariables.s_V0x00543C40);
        System_L004028C4_CheckError();
        AlliedVariables.s_TieMission_Description.Text = buffer.ReadFixedLengthString(0, 0x400);
    }
}
