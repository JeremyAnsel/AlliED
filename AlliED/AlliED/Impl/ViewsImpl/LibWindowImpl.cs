using AlliED.Extensions;
using System.IO;
using System.Windows.Controls;

namespace AlliED.Impl.ViewsImpl;

internal static class LibWindowImpl
{
    public static void Register(LibWindow window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(LibWindow window)
    {
        window.Activated += (s, e) => TLibForm_FormActivate(window);

        window.LibShipList.DrawItem += (sender, index) => TLibForm_LibShipListDrawItem(window, sender, index, null, null);
        window.AddBut.Click += (s, e) => TLibForm_AddButClick(window);
        window.GrabFGBut.Click += (s, e) => TLibForm_GrabFGButClick(window);
        window.LibUpBut.Click += (s, e) => TLibForm_LibUpButClick(window);
        window.LibDownBut.Click += (s, e) => TLibForm_LibDownButClick(window);
        window.DeleteLibFGBut.Click += (s, e) => TLibForm_DeleteLibFGButClick(window);
        window.NewLibBut.Click += (s, e) => TLibForm_NewLibButClick(window);
        window.LoadLibBut.Click += (s, e) => TLibForm_LoadLibButClick(window);
        window.SaveAsLibBut.Click += (s, e) => TLibForm_SaveAsLibButClick(window);
        window.SaveLibBut.Click += (s, e) => TLibForm_SaveLibButClick(window);
    }

    // L00513228
    private static void FormCreate(LibWindow LibForm)
    {
        TLibForm_PROC_00513188(LibForm);

        if (TApplication_GetWidth() != 0x280 && TApplication_GetHeight() != 0x1E0)
        {
            return;
        }

        Graphics_TFont_SetName(LibForm, "MS Serif");
        Graphics_TFont_SetSize(LibForm, 0x09);
        Controls_TControl_SetHeight(LibForm, 0x1B8);
        Controls_TControl_SetWidth(LibForm, 0x262);
        TApplication_L00468080(LibForm, 0x01);
        Controls_TWinControl_ScaleBy(LibForm, TApplication_GetWidth(), 0x32A);
        TApplication_RecreateWnd(LibForm, 0x04);
    }

    // L005134B8
    public static void TLibForm_ShowSaveIfChanged(LibWindow? LibForm, string? cancelStr)
    {
        if (AlliedVariables.s_LibFormHasChanged)
        {
            if (MessageBox_ShowConfirmation("FG Library file has been changed. Save it?", cancelStr) == TModalResultEnum.Yes)
            {
                TLibForm_Proc_00512C28(LibForm);
            }
        }
    }

    // L005132CC
    public static void TLibForm_Proc_005132CC(LibWindow? LibForm)
    {
        //int esi = AlliedVariables.s_LibFormFlightGroupObjectsList.Count;

        //for (int ebx = 0; ebx < esi; ebx++)
        //{
        //    if (Classes_TList_Get(AlliedVariables.s_LibFormFlightGroupObjectsList, ebx) == 0)
        //    {
        //        continue;
        //    }

        //    System_TObject_Free(Classes_TList_Get(*edi, ebx));
        //}

        AlliedVariables.s_LibFormFlightGroupObjectsList.Clear();
    }

    // L00513188
    private static void TLibForm_PROC_00513188(LibWindow LibForm)
    {
        LibForm.LibShipList.Clear();

        int ebx = AlliedVariables.s_LibFormFlightGroupObjectsList.Count;

        for (int edi = 0; edi < ebx; edi++)
        {
            S0xFGObject esi = Classes_TList_Get(AlliedVariables.s_LibFormFlightGroupObjectsList, edi);
            string ebp08 = Form1WindowImpl.TForm1_Proc_005258A8(AlliedVariables.s_AlliedForm1Window!, esi);
            LibForm.LibShipList.AddItem(ebp08);
        }
    }

    // L00513404
    private static void TLibForm_FormActivate(LibWindow LibForm)
    {
        Controls_TControl_SetTop(LibForm, (int)LibForm.Top - 0x0F);
        string ebp08_1 = "Flight Group Library - [" + Path.GetFileName(AlliedVariables.s_FlightGroupLibraryFileName) + "]";
        Controls_TControl_SetText(AlliedVariables.s_TLibForm_Instance!, ebp08_1);
    }

    // L0051309C
    private static void TLibForm_LibShipListDrawItem(LibWindow LibForm, ListBox Sender, int index, object? A4, object? rect)
    {
        if (index < AlliedVariables.s_LibFormFlightGroupObjectsList.Count)
        {
            ListBoxItem item = Sender.GetItem(index);
            S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_LibFormFlightGroupObjectsList, index);
            Graphics_TFont_SetColor(item, AlliedGetIffColor(eax2.FlightGroupStruct.Iff, 0));
        }
    }

    // L00512EB8
    private static void TLibForm_AddButClick(LibWindow LibForm)
    {
        int esp08 = LibForm.LibShipList.Items.Count;

        for (int esp04 = 0; esp04 < esp08; esp04++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(LibForm.LibShipList, esp04))
            {
                continue;
            }

            S0xFGObject ebx = new();

            if (AlliedVariables.s_UseAutoChkSetting)
            {
                ebx.AutoLink = true;
            }

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_LibFormFlightGroupObjectsList, esp04);
            ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(eax1.FlightGroupStruct.ToByteArray());

            for (int edx = 0; edx < 0x03; edx++)
            {
                for (int eax = 0; eax < 0x04; eax++)
                {
                    ebx.m00147C[edx].M000000[eax] = ebx.FlightGroupStruct.StartPoints[eax].Position[edx];
                }
            }

            for (int eax = 0; eax < 0x04; eax++)
            {
                ebx.IsWPEnabled[eax] = ebx.FlightGroupStruct.StartPoints[eax].IsUsed;
            }

            byte esi0 = ebx.FlightGroupStruct.StartPointRegions[0];
            ebx.FlightGroupStruct.StartPointRegions[0] = (byte)(AlliedVariables.s_CurrentRegion - 1);

            for (int eax = 0; eax < 0x04; eax++)
            {
                S0xTieFlightGroupOrder esp0C = S0xTieFlightGroupOrder.FromByteArray(ebx.FlightGroupStruct.Orders[esi0 * 4 + eax].ToByteArray());
                ebx.FlightGroupStruct.Orders[esi0 * 4 + eax] = S0xTieFlightGroupOrder.FromByteArray(ebx.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + eax].ToByteArray());
                ebx.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + eax] = S0xTieFlightGroupOrder.FromByteArray(esp0C.ToByteArray());
            }

            ebx.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(ebx.FlightGroupStruct));

            AlliedVariables.s_FlightGroupObjectsList.Add(ebx);
            Unit_00513838_Proc_0051D53C(AlliedVariables.s_FlightGroupObjectsList.Count - 1);
            AlliedVariables.s_TieFileHeader.FlightGroupsCount++;
            AlliedVariables.s_V0x00543B53 = true;
            Unit_00513838_Proc_0051DB68();
        }

        AlliedVariables.s_V0x005B7064 = 0;
        AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        AlliedVariables.s_V0x005B7044 = 0x01;
        Form1WindowImpl.TForm1_Proc_0052D3F8(AlliedVariables.s_AlliedForm1Window!);
        Unit_00513838_Proc_00520924();
    }

    // L00513674
    private static void TLibForm_GrabFGButClick(LibWindow LibForm)
    {
        TLibForm_Proc_0051330C(LibForm);
        AlliedVariables.s_LibFormHasChanged = true;
        TLibForm_PROC_00513188(LibForm);
    }

    // L0051330C
    public static void TLibForm_Proc_0051330C(LibWindow LibForm)
    {
        int esp00 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int edi = 0; edi < esp00; edi++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, edi))
            {
                continue;
            }

            S0xFGObject ebp = new();
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);
            ebp.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(eax1.FlightGroupStruct.ToByteArray());

            for (int esi = 0; esi < 4; esi++)
            {
                for (int ebx = 0; ebx < 4; ebx++)
                {
                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);
                    ebp.FlightGroupStruct.StartPoints[esi].Position[ebx] = eax2.m00147C[ebx].M000000[esi];
                }
            }

            for (int ebx = 0; ebx < 4; ebx++)
            {
                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);
                ebp.FlightGroupStruct.StartPoints[ebx].IsUsed = eax2.IsWPEnabled[ebx];
            }

            AlliedVariables.s_LibFormFlightGroupObjectsList.Add(ebp);
        }
    }

    // L00513690
    private static void TLibForm_LibUpButClick(LibWindow LibForm)
    {
        int ebx = LibForm.LibShipList.SelectedIndex;

        if (ebx <= 0)
        {
            return;
        }

        int edi = ebx - 1;
        Classes_TList_Exchange(AlliedVariables.s_LibFormFlightGroupObjectsList, ebx, edi);
        TLibForm_PROC_00513188(LibForm);
        StdCtrls_TCustomListBox_SetSelected(LibForm.LibShipList, edi, true);
        AlliedVariables.s_LibFormHasChanged = true;
    }

    // L005136DC
    private static void TLibForm_LibDownButClick(LibWindow LibForm)
    {
        int ebx = LibForm.LibShipList.SelectedIndex;

        if (ebx >= AlliedVariables.s_LibFormFlightGroupObjectsList.Count - 1)
        {
            return;
        }

        int edi = ebx + 1;
        Classes_TList_Exchange(AlliedVariables.s_LibFormFlightGroupObjectsList, ebx, edi);
        TLibForm_PROC_00513188(LibForm);
        StdCtrls_TCustomListBox_SetSelected(LibForm.LibShipList, edi, true);
        AlliedVariables.s_LibFormHasChanged = true;
    }

    // L00513730
    private static void TLibForm_DeleteLibFGButClick(LibWindow LibForm)
    {
        if (AlliedVariables.s_ConfDeletesChkSetting && AlliedVariables.s_LibFormFlightGroupObjectsList.Count > 0)
        {
            if (MessageBox_ShowConfirmation("Delete Current Library Entry - Are You Sure?", null) != TModalResultEnum.Yes)
            {
                return;
            }
        }

        TLibForm_Proc_005137A4(LibForm);
    }

    // L005137A4
    private static void TLibForm_Proc_005137A4(LibWindow LibForm)
    {
        if (AlliedVariables.s_LibFormFlightGroupObjectsList.Count <= 0)
        {
            return;
        }

        int esi = LibForm.LibShipList.SelectedIndex;

        Classes_TList_Delete(AlliedVariables.s_LibFormFlightGroupObjectsList, esi);
        TLibForm_PROC_00513188(LibForm);

        int eax1 = AlliedVariables.s_LibFormFlightGroupObjectsList.Count - 1;

        if (esi > eax1)
        {
            esi = eax1;
        }

        StdCtrls_TCustomListBox_SetSelected(LibForm.LibShipList, esi, true);
        AlliedVariables.s_LibFormHasChanged = true;
    }

    // L005135F8
    private static void TLibForm_NewLibButClick(LibWindow LibForm)
    {
        TLibForm_ShowSaveIfChanged(LibForm, null);
        TLibForm_Proc_005132CC(LibForm);
        TLibForm_PROC_00513188(LibForm);
        AlliedVariables.s_FlightGroupLibraryFileName = "none";
        Controls_TControl_SetText(AlliedVariables.s_TLibForm_Instance!, "Flight Group Library - [none]");
        AlliedVariables.s_LibFormHasChanged = false;
    }

    // L00513174
    private static void TLibForm_LoadLibButClick(LibWindow LibForm)
    {
        TLibForm_ShowSaveIfChanged(LibForm, null);
        TLibForm_ShowOpenFGLibrary(AlliedVariables.s_TLibForm_Instance!);
    }

    // L00512AE0
    private static void TLibForm_ShowOpenFGLibrary(LibWindow LibForm)
    {
        if (Directory.Exists(AlliedVariables.s_AlliedDirectoryPath))
        {
            Dialogs_TOpenDialog_SetInitialDir(AlliedVariables.s_AlliedForm1Window!.OpenDialog3, AlliedVariables.s_AlliedDirectoryPath);
        }

        if (AlliedVariables.s_AlliedForm1Window!.OpenDialog3.ShowDialog() == true)
        {
            AlliedVariables.s_FlightGroupLibraryFileName = AlliedVariables.s_AlliedForm1Window!.OpenDialog3.FileName;
            TLibForm_Proc_00512C70(LibForm);
            string ebp0C_0 = Path.GetFileName(AlliedVariables.s_FlightGroupLibraryFileName);
            Controls_TControl_SetText(AlliedVariables.s_TLibForm_Instance!, "Flight Group Library - [" + ebp0C_0 + "]");
            TLibForm_PROC_00513188(LibForm);
            StdCtrls_TCustomListBox_SetSelected(LibForm.LibShipList, LibForm.LibShipList.SelectedIndex, true);
            AlliedVariables.s_LibFormHasChanged = false;
        }
    }

    // L00512C70
    private static void TLibForm_Proc_00512C70(LibWindow LibForm)
    {
        TFileRec ebp14C = new();
        ebp14C.Assign(AlliedVariables.s_FlightGroupLibraryFileName);

        ebp14C.OpenFileForRead(0xE3E);
        System_L004028C4_CheckError();

        AlliedVariables.s_LibFormFlightGroupObjectsList = new();

        while (true)
        {
            bool al1 = ebp14C.EofFile();
            System_L004028C4_CheckError();

            if (al1)
            {
                break;
            }

            byte[] buffer = new byte[0xE3E];
            ebp14C.ReadRec(buffer);
            System_L004028C4_CheckError();
            S0xTieFlightGroup ebpF8A = S0xTieFlightGroup.FromByteArray(buffer);

            S0xFGObject eax2 = new();
            eax2.FlightGroupStruct = ebpF8A;

            AlliedVariables.s_LibFormFlightGroupObjectsList.Add(eax2);
        }

        ebp14C.Close();
        System_L004028C4_CheckError();
    }

    // L00513550
    private static void TLibForm_SaveAsLibButClick(LibWindow LibForm)
    {
        TLibForm_LibSaveAsDialogAndWrite(LibForm);
    }

    // L00513558
    private static void TLibForm_LibSaveAsDialogAndWrite(LibWindow LibForm)
    {
        if (Directory.Exists(AlliedVariables.s_AlliedDirectoryPath))
        {
            Dialogs_TOpenDialog_SetInitialDir(AlliedVariables.s_AlliedForm1Window!.SaveDialog2, AlliedVariables.s_AlliedDirectoryPath);
        }

        if (AlliedVariables.s_AlliedForm1Window!.SaveDialog2.ShowDialog() == true)
        {
            AlliedVariables.s_FlightGroupLibraryFileName = AlliedVariables.s_AlliedForm1Window!.SaveDialog2.FileName;
            TLibForm_WriteLibFile(LibForm);
        }
    }

    // L005134B0
    private static void TLibForm_SaveLibButClick(LibWindow LibForm)
    {
        TLibForm_Proc_00512C28(LibForm);
    }

    // L00512C28
    private static void TLibForm_Proc_00512C28(LibWindow LibForm)
    {
        if (File.Exists(AlliedVariables.s_FlightGroupLibraryFileName))
        {
            if (!string.Equals(AlliedVariables.s_FlightGroupLibraryFileName, "none", StringComparison.Ordinal))
            {
                TLibForm_WriteLibFile(LibForm);
                return;
            }
        }

        TLibForm_LibSaveAsDialogAndWrite(LibForm);
    }

    // L00512D44
    private static void TLibForm_WriteLibFile(LibWindow LibForm)
    {
        TFileRec ebp14C = new();
        ebp14C.Assign(AlliedVariables.s_FlightGroupLibraryFileName);
        ebp14C.OpenFileForWrite(0xE3E);
        System_L004028C4_CheckError();

        int ebx = AlliedVariables.s_LibFormFlightGroupObjectsList.Count;

        for (int esi = 0; esi < ebx; esi++)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_LibFormFlightGroupObjectsList, esi);
            ebp14C.WriteRec(eax1.FlightGroupStruct.ToByteArray());
            System_L004028C4_CheckError();
        }

        ebp14C.Close();
        System_L004028C4_CheckError();
        AlliedVariables.s_LibFormHasChanged = false;

        string ebpF94_0 = Path.GetFileName(AlliedVariables.s_FlightGroupLibraryFileName);
        Controls_TControl_SetText(AlliedVariables.s_TLibForm_Instance!, "Flight Group Library - [" + ebpF94_0 + "]");
    }
}
