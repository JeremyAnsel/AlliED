using AlliED.Controls;
using AlliED.Extensions;
using AlliED.Helpers;
using Microsoft.Win32;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace AlliED.Impl.ViewsImpl;

internal static class Form1WindowImpl
{
    public static void Register(Form1Window window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(Form1Window window)
    {
        window.Loaded += (s, e) => TForm1_FormActivate(window);
        window.Closing += (s, e) => TForm1_FormClose(window, null, e);
        window.ShipList.DrawItem += (sender, index) => TForm1_ShipListDrawItem(window, sender, index);
        window.SelectionBox.DrawItem += (sender, index) => TForm1_SelectionBoxDrawItem(window, sender, index);
        window.PaintBox1.Paint += (sender, context) => TForm1_PaintBox1Paint(window, sender);
        window.ZoomBar.ValueChanged += (sender, e) => TForm1_ZoomBarChange(window, sender);

        window.ScrollBox1.SizeChanged += (sender, e) => TForm1_ScrollBox1Resize(window, sender);
        window.OverallPages.SizeChanged += (sender, e) => TForm1_OverallPagesResize(window, sender);

        window.PaintBox1.MouseDoubleClick += (s, e) => TForm1_PaintBox1DblClick(window, s);
        window.PaintBox1.MouseMove += (s, e) =>
        {
            Point position = e.GetPosition((Control)s);
            TForm1_PaintBox1MouseMove(window, s, e.ToTShiftState(), (int)position.Y, (int)position.X);
        };
        window.PaintBox1.MouseDown += (s, e) =>
        {
            if (e.ClickCount != 1)
            {
                return;
            }
            Point position = e.GetPosition((Control)s);
            TForm1_PaintBox1MouseDown(window, s, (int)position.Y, (int)position.X, e.ToTShiftState());
        };
        window.PaintBox1.MouseUp += (s, e) =>
        {
            Point position = e.GetPosition((Control)s);
            TForm1_PaintBox1MouseUp(window, s, (int)position.Y, (int)position.X, e.ToTShiftState());
            // todo
            TForm1_PaintBox1MouseMove(window, s, e.ToTShiftState(), (int)position.Y, (int)position.X);
        };

        window.FitBattleBtn.Click += (s, e) => TForm1_FitBattleBtnClick(window, s);
        window.NamesOn.Click += (s, e) => TForm1_NamesOnClick(window, s);
        window.LockBtn.Click += (s, e) => TForm1_NamesOnClick(window, s);
        window.XY.Click += (s, e) => TForm1_XYClick(window, s);
        window.XZ.Click += (s, e) => TForm1_XYClick(window, s);
        window.YZ.Click += (s, e) => TForm1_XYClick(window, s);
        window.ErrBtn.Click += (s, e) => TForm1_ErrBtnClick(window, s);
        window.PaletOn.Click += (s, e) => TForm1_PaletOnClick(window, s);
        window.TeamDropBtn.Click += (s, e) => TForm1_TeamDropClick(window, s);
        window.TeamDrop.SubmenuOpened += (s, e) => TForm1_TeamMenuPopup(window, s);
        for (int i = 0; i < window.TeamDrop.Items.Count; i++)
        {
            MenuItem item = Menus_TMenuItem_GetItem(window.TeamDrop, i);
            item.Click += (s, e) => TForm1_N11Click(window, item);
        }
        window.TabSheet14.ContextMenuOpening += (s, e) => TForm1_TeamMenuPopup(window, s);
        for (int i = 0; i < window.TabSheet14.ContextMenu.Items.Count; i++)
        {
            MenuItem item = Menus_TMenuItem_GetItem(window.TabSheet14.ContextMenu, i);
            item.Click += (s, e) => TForm1_N11Click(window, item);
        }
        window.CurrOnly.Click += (s, e) => TForm1_CurrOnlyClick(window, s);
        window.XScroll.ValueChanged += (s, e) => TForm1_XScrollChange(window, null);
        window.YScroll.ValueChanged += (s, e) => TForm1_YScrollChange(window, null);
        window.ShowOrderSel.Click += (s, e) => TForm1_ShowOrderSelClick(window, null);
        window.ShowFGList.Click += (s, e) => TForm1_ShowFGListClick(window, null);
        window.ShowDatapad.Click += (s, e) => TForm1_ShowDatapadClick(window, null);
        window.OverallPages.SelectionChanged += (s, e) =>
        {
            TForm1_OverallPagesChanging(window, null);
            TForm1_OverallPagesChange(window, null);
        };
        window.SelectionBox.SelectionChanged += (s, e) =>
        {
            if (window.SelectionBox.SelectedIndex != -1)
            {
                ComboBoxItem item = window.SelectionBox.GetItem(window.SelectionBox.SelectedIndex);
                window.SelectionBox.Foreground = item.Foreground;
            }

            if (window.SelectionBox.SelectedIndex != -1)
            {
                TForm1_SelectionBoxChange(window, s);
            }
        };
        window.DescBtn.Click += (s, e) => TForm1_DescBtnClick(window, null);
        window.MapEditWPBtn.Click += (s, e) => TForm1_MapEditWPBtnClick(window, null);
        window.ShipList.MouseLeftButtonUp += (s, e) => TForm1_ShipListClick(window, null);
        window.ShipList.KeyUp += (s, e) => TForm1_ShipListClick(window, null);
        window.ShipList.MouseDoubleClick += (s, e) => TForm1_ShipListDblClick(window, null);
        window.ShipList.MouseUp += (s, e) =>
        {
            Point position = e.GetPosition((Control)s);
            TForm1_ShipListMouseUp(window, s, (int)position.Y, (int)position.X, e.ToTShiftState());
        };
        window.MissionBtn.Click += (s, e) => TForm1_MissionBtnClick(window, s);
        window.BrfBtn.Click += (s, e) => TForm1_BrfBtnClick(window, s);
        window.Docklefttoolbartotop1.Click += (s, e) => TForm1_Docklefttoolbartotop1Click(window, s);
        window.Options1.Click += (s, e) => TForm1_Options1Click(window, s);
        window.XsnapPop.Opened += (s, e) => TForm1_XsnapPopPopup(window, s);
        window.YSnapPop.Opened += (s, e) => TForm1_YSnapPopPopup(window, s);
        window.MyPage1.Click += (s, e) => TForm1_MyPage1Click(window, s);
        window.LucasArts1.Click += (s, e) => TForm1_LucasArts1Click(window, s);
        window.TotallyGames1.Click += (s, e) => TForm1_TotallyGames1Click(window, s);
        window.DatamastersWebSite1.Click += (s, e) => TForm1_DatamastersWebSite1Click(window, s);
        window.XWingAllianceNet1.Click += (s, e) => TForm1_XWingAllianceNet1Click(window, s);
        window.StarWarscom1.Click += (s, e) => TForm1_StarWarscom1Click(window, s);
        window.LibShowBut.Click += (s, e) => TForm1_LibShowButClick(window, s);
        window.PutIntoLib.Click += (s, e) => TForm1_PutIntoLibClick(window, s);
        window.SpeedButton1.Click += (s, e) => TForm1_SpeedButton1Click(window, s);
        window.SpeedButton2.Click += (s, e) => TForm1_SpeedButton2Click(window, s);
        window.EndMsgWav.Click += (s, e) => TForm1_EndMsgWavClick(window, s);
        window.LockBattleCtr.Click += (s, e) => TForm1_LockBattleCtrClick(window, s);
        window.LockCtr.Click += (s, e) => TForm1_LockBattleCtrClick(window, s);
        window.LockCurr.Click += (s, e) => TForm1_LockBattleCtrClick(window, s);
        window.LockZero.Click += (s, e) => TForm1_LockBattleCtrClick(window, s);
        window.AllWPS.Click += (s, e) => TForm1_AllWPsClick(window, s);
        window.CurrWPs.Click += (s, e) => TForm1_AllWPsClick(window, s);
        window.CentreMapBtn.Click += (s, e) => TForm1_CentreMapBtnClick(window, s);
        window.CtrFGBtn.Click += (s, e) => TForm1_CtrFGBtnClick(window, s);
        window.ZoomTo800Btn.Click += (s, e) => TForm1_ZoomTo800BtnClick(window, s);
        window.ZoomTo16Btn.Click += (s, e) => TForm1_ZoomTo16BtnClick(window, s);
        window.AllIFF.Click += (s, e) => TForm1_AllIFFClick(window, s);
        window.RebIFF.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.ImpIFF.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.BluIFF.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.YellIFF.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.Red2IFF.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.PurpIFF.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.FightersOn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.CapShipsOn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.TransportsOn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.FRTsOn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.PlatformsOn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.ObjectsOn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.EasyBtn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.MedBtn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.HardBtn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.ShowStartBtn.Click += (s, e) => TForm1_RebIFFClick(window, s);
        window.Battle.Click += (s, e) => TForm1_BattleClick(window, s);
        window.Succ1Ed.TextChanged += (s, e) => TForm1_Succ1EdChange(window, (TextBox)s);
        window.Succ2Ed.TextChanged += (s, e) => TForm1_Succ1EdChange(window, (TextBox)s);
        window.Fail1Ed.TextChanged += (s, e) => TForm1_Succ1EdChange(window, (TextBox)s);
        window.Fail2Ed.TextChanged += (s, e) => TForm1_Succ1EdChange(window, (TextBox)s);
        window.Sec1Ed.TextChanged += (s, e) => TForm1_Succ1EdChange(window, (TextBox)s);
        window.Sec2Ed.TextChanged += (s, e) => TForm1_Succ1EdChange(window, (TextBox)s);
        window.JoinByRadio.Click += (s, e) => TForm1_JoinByRadioClick(window, s);
        window.TeamIFFBox1.SelectionChanged += (s, e) => TForm1_TeamIFFBox1Change(window, s);
        window.FriendsList.SelectionChanged += (s, e) => TForm1_FriendsListClick(window, s);
        window.TeamName1Ed.SelectionChanged += (s, e) => TForm1_TeamName1EdChange(window, s);
        window.CreateLst.Click += (s, e) => TForm1_CreateLstClick(window, s);
        window.SaveWAV.Click += (s, e) => TForm1_SaveWAVClick(window, s);
        window.ClearWAVBut.Click += (s, e) => TForm1_ClearWAVButClick(window, s);
        window.LoadWAV.Click += (s, e) => TForm1_LoadWAVClick(window, s);
        window.WAVlistBut.Click += (s, e) => TForm1_WAVlistButClick(window, s);
        window.MsgStrList.MouseLeftButtonUp += (s, e) => TForm1_MsgStrListClick(window, s);
        window.MsgStrList.MouseDoubleClick += (s, e) => TForm1_MsgStrListDblClick(window, s);
        window.MsgStrList.DrawItem += (sender, index) => TForm1_MsgStrListDrawItem(window, sender, index, null, null);

        //window.License += (s, e) => TForm1_License1Click(window, null);
        window.About += (s, e) => TForm1_About2Click(window, null);
        //window.FormBtn += (s, e) => TForm1_FormBtnClick(window, null);
        //window.PrefBut += (s, e) => TForm1_PrefButClick(window, null);
        window.ViewWavFileManager += (s, e) => TForm1_WAVfilemanager1Click(window, null);
        window.MapXYView += (s, e) => TForm1_XYView1Click(window, window.XYView1);
        window.MapXZView += (s, e) => TForm1_XYView1Click(window, window.XZView1);
        window.MapYZView += (s, e) => TForm1_XYView1Click(window, window.YZView1);
        window.OptionsPreferences += (s, e) => TForm1_Preferences1Click(window, null);
        window.GoalSummary += (s, e) => TForm1_GoalView1Click(window, null);
        window.ToolsHyperbuoyWizard += (s, e) => TForm1_AddHyperbuoys1Click(window, null);
        window.ToolsAddDefaultBackdrop += (s, e) => TForm1_AddBackdrop1Click(window, null);
        window.NewMission += (s, e) => TForm1_NewMissionButClick(window, null);
        window.SaveMission += (s, e) => TForm1_SaveBtnClick(window, null);
        window.OpenMission += (s, e) => TForm1_LoadBtnClick(window, null);
        window.SaveAsMission += (s, e) => TForm1_SaveAsBtnClick(window, null);
        window.LoadMissionByLst += (s, e) => TForm1_Reopen1Click(window, s);
        window.ViewMainPages += (s, e) => TForm1_ShipList1Click(window, null);
        window.ViewDatapad += (s, e) => TForm1_Datapad1Click(window, null);
        window.ViewOrderRegionSelector += (s, e) => TForm1_OrderRegionSelect1Click(window, null);
        window.Region1 += (s, e) => TForm1_Region11Click(window, window.Region11);
        window.Region2 += (s, e) => TForm1_Region11Click(window, window.Region21);
        window.Region3 += (s, e) => TForm1_Region11Click(window, window.Region31);
        window.Region4 += (s, e) => TForm1_Region11Click(window, window.Region41);
        window.ViewTextSections += (s, e) => TForm1_TextSections1Click(window, null);
        window.ViewWaypointsEditor += (s, e) => TForm1_WPEditor1Click(window, null);
        window.BackupMission += (s, e) => TForm1_Backup1Click(window, null);
        window.BrowseMissions += (s, e) => TForm1_Browse1Click(window, null);
        window.ImportXWing += (s, e) => TForm1_XWing1Click(window, window.XWing1);
        window.ImportTieFighter += (s, e) => TForm1_XWing1Click(window, window.TIEFighter1);
        window.ImportXvT += (s, e) => TForm1_XWing1Click(window, window.XvTBoP1);
        window.ImportBoP += (s, e) => TForm1_XWing1Click(window, window.BoP2);
        window.ExportXvT += (s, e) => TForm1_XvT1Click(window, window.XvT1);
        window.ExportBoP += (s, e) => TForm1_XvT1Click(window, window.BoP1);
        window.Order1 += (s, e) => TForm1_Order11Click(window, window.Order11);
        window.Order2 += (s, e) => TForm1_Order11Click(window, window.Order21);
        window.Order3 += (s, e) => TForm1_Order11Click(window, window.Order31);
        window.Order4 += (s, e) => TForm1_Order11Click(window, window.Order41);
        window.AddItem += (s, e) => TForm1_NewFGButClick(window, s);
        window.DeleteItem += (s, e) => TForm1_DeleteButClick(window, s);
        window.EditCopy += (s, e) => TForm1_CopyBtnClick(window, s);
        window.EditPaste += (s, e) => TForm1_PasteBtnClick(window, s);
        window.EditMoveUp += (s, e) => TForm1_UpBtnClick(window, s);
        window.EditMoveDown += (s, e) => TForm1_DownBtnClick(window, s);
        window.ClassicEditorView += (s, e) => TForm1_ClassicFGEditview1Click(window, null);
        window.ClassicMapView += (s, e) => TForm1_ClassicMapview1Click(window, null);
        window.OptionsLockOrdersToRegions += (s, e) => TForm1_Lockorders1Click(window, null);
        window.ErrorCheckingAutoOnSaving += (s, e) => TForm1_ErrCheckOn1Click(window, null);
        window.ErrorCheckingFilenameFormat += (s, e) => TForm1_CheckFilename1Click(window, window.CheckFilename1);
        window.BackingUpOverwrite += (s, e) => TForm1_Overwriteprevious1Click(window, window.Overwriteprevious1);
        window.BackingUpIncremental += (s, e) => TForm1_Overwriteprevious1Click(window, window.Incremental1);
        window.ToolsShiplistSequence += (s, e) => TForm1_ShipListSequence1Click(window, null);
        window.XSnap1.Click += (s, e) => TForm1_XSnap1Click(window, s);
        window.SnapXOff += (s, e) => TForm1_Off1Click(window, (string)e.Parameter);
        window.SnapX01 += (s, e) => TForm1_Off1Click(window, (string)e.Parameter);
        window.SnapX02 += (s, e) => TForm1_Off1Click(window, (string)e.Parameter);
        window.SnapX05 += (s, e) => TForm1_Off1Click(window, (string)e.Parameter);
        window.SnapX10 += (s, e) => TForm1_Off1Click(window, (string)e.Parameter);
        window.YSnap1.Click += (s, e) => TForm1_YSnap1Click(window, s);
        window.SnapYOff += (s, e) => TForm1_Off2Click(window, (string)e.Parameter);
        window.SnapY01 += (s, e) => TForm1_Off2Click(window, (string)e.Parameter);
        window.SnapY02 += (s, e) => TForm1_Off2Click(window, (string)e.Parameter);
        window.SnapY05 += (s, e) => TForm1_Off2Click(window, (string)e.Parameter);
        window.SnapY10 += (s, e) => TForm1_Off2Click(window, (string)e.Parameter);
        window.YX1.Click += (s, e) => TForm1_YX1Click(window, s);
        window.MapAllSnapOff += (s, e) => TForm1_AllSnapOff1Click(window, null);
        window.MapGridOn += (s, e) => TForm1_ToggleGrid1Click(window, window.ToggleGrid1);
        window.MapNumberGrid += (s, e) => TForm1_Numbergrid1Click(window, window.Numbergrid1);
        window.MapAllWaypoints += (s, e) => TForm1_Allwaypoints1Click(window, null);
        window.MapLinkHypPoint += (s, e) => TForm1_LinkHyppoint1Click(window, null);
        window.MapShowDistances += (s, e) => TForm1_ShowdistancesClick(window, null);
        window.MapShowTimes += (s, e) => TForm1_ShowtimesClick(window, null);
        window.MapMinWireframesSizes += (s, e) => TForm1_MinimumWireframesSizes1Click(window, null);
        window.MapIconsOnly += (s, e) => TForm1_ToggleIconsOnly1Click(window, window.ToggleIconsOnly1);
    }

    // L00523180
    private static void FormCreate(Form1Window Form1)
    {
        if (Application_GetPixelsPerInch() == 0x78)
        {
            StdCtrls_TCustomListBox_SetItemHeight(Form1.ShipList, AlliedPixelsScaleDiv(0x0D));
            StdCtrls_TCustomListBox_SetItemHeight(Form1.MsgStrList, AlliedPixelsScaleDiv(0x0D));
        }

        AlliedVariables.s_V0x005B6D11 = 0x01;

        // allied patch drive
        //s_AlliedDriveLetter = AlliedGetDriveLetter();
        AlliedVariables.s_AlliedDriveLetter = 'C';

        string ebp08_0 = TApplication_GetProgramFileName();
        string ebp08_1 = Runtime_GetDriveLetter(ebp08_0);
        AlliedVariables.s_AlliedDirectoryDriveLetter = ebp08_1;
        AlliedVariables.s_Allied_InstallPath = "C:\\";
        TBrfForm_ReadInstallPathFromRegistry();

        for (int i = 0; i < 6; i++)
        {
            AlliedVariables.s_V0x00543D20[i] = 0;
        }

        AlliedVariables.s_WAVListFormSearchString = string.Empty;

        //CALL_FUNCTION_ptr_naked_1(Form1->ControlBar2, mVtbl->SetParent, Form1->OverallPages);

        AlliedVariables.s_AlliedAplicationWidth = TApplication_GetWidth();
        AlliedVariables.s_AlliedAplicationHeight = TApplication_GetHeight();

        int ecx0 = (int)Math.Round(AlliedVariables.s_AlliedAplicationWidth * 4 / 5.0f);
        Controls_TSizeConstraints_SetConstraints(Form1.OverallPages, 0x01, ecx0);

        AlliedVariables.s_V0x00543CE4 = AlliedVariables.s_AlliedAplicationWidth / 800.0f;
        AlliedVariables.s_V0x00543CEC = AlliedVariables.s_AlliedAplicationHeight / 600.0f;

        //TApplication_LoadCursor(s_Allied_TScreenPtr->Application, 0x05, LoadCursorA(s_HInstance, "HANDGRAB"));

        AlliedVariables.s_V0x005B6D13 = 0x01;
        AlliedVariables.s_V0x005B6D14 = 0;
        AlliedVariables.s_TErrForm_OmitWarns = false;
        AlliedVariables.s_V0x0053BDA4 = false;
        AlliedVariables.s_V0x00543B56 = 0;
        AlliedVariables.s_V0x00543D0B = 0x01;

        if (AlliedVariables.s_AlliedAplicationWidth < 0x320)
        {
            MessageBox_ShowInformation("Note: Minimum resolution for correct function of AlliED is 800 x 600.");

            Unit_00513838_Proc_0051BD20(AlliedVariables.s_AlliedForm1Window!);
            TApplication_ShowWindow(AlliedVariables.s_AlliedForm1Window!, 0x02);
        }

        Unit_00513838_Proc_0051BEAC();

        AlliedVariables.s_V0x00543C99 = 0x01;
        AlliedVariables.s_V0x00543C9A = 0x01;
        AlliedVariables.s_V0x00543C9B = 0x01;
        AlliedVariables.s_V0x00543C9C = 0x01;

        //StdCtrls_TCustomListBox_SetStyle(Form1->ShipList, TListBoxStyleEnum_OwnerDrawFixed);
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!, ProductVersionHelpers.GetNameAndVersion());
        AlliedVariables.s_V0x00543C04 = "none";
        AlliedVariables.s_FlightGroupLibraryFileName = "none";

        TLibForm_Proc_00513838();
        L00513BA0();

        AlliedVariables.s_V0x00543B50 = 0;
        AlliedVariables.s_V0x00543B51 = 0;
        AlliedVariables.s_V0x00543B54 = 0;
        AlliedVariables.s_LibFormHasChanged = false;
        AlliedVariables.s_V0x00543C9D = 0x01;
        AlliedVariables.s_V0x00543BFC = "Unnamed";
        AlliedVariables.s_V0x00543C9E = 0x01;

        Form1.SelectionBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_Strings_Short);

        AlliedVariables.s_V0x005B7094 = Graphics_TBitmap_Create(0x10, 0x14);
        AlliedVariables.s_V0x00543D40 = Graphics_TBitmap_Create();
        AlliedVariables.s_V0x005B7098 = new TRect(0, 0, 0x10, 0x14);
    }

    // L00529338
    private static void TForm1_FormActivate(Form1Window Form1)
    {
        if (AlliedVariables.s_V0x005B6D11 != 0)
        {
            TForm1_Proc_0052A6DC(Form1);
        }
    }

    // L0052F114
    private static void TForm1_Proc_0052F114(Form1Window Form1, int edx0)
    {
        var dialog = MainImpl.CreateAboutBox();
        dialog.Owner = Form1;
        dialog.ShowDialog();
    }

    // L0052F19C
    //private static void TForm1_License1Click(Form1Window Form1, object? Sender)
    //{
    //    TForm1_Proc_0052F114(Form1, 0x02);
    //}

    // L0052F1A8
    private static void TForm1_About2Click(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_0052F114(Form1, 0x01);
    }

    // L0052B140
    //private static void TForm1_FormBtnClick(Form1Window Form1, object? Sender)
    //{
    //}

    // L00507130
    private static void TBrfForm_ReadInstallPathFromRegistry()
    {
        AlliedVariables.s_V0x00543948 = 0;

        string path;

        using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
        using (RegistryKey? key = baseKey.OpenSubKey(@"SOFTWARE\LucasArts Entertainment Company LLC\X-Wing Alliance\v1.0"))
        {
            if (key is null)
            {
                path = string.Empty;
            }
            else
            {
                path = (string)key.GetValue("Install Path");
            }
        }

        AlliedVariables.s_Allied_InstallPath = path;
    }

    // L0051BEAC
    private static void Unit_00513838_Proc_0051BEAC()
    {
        Graphics_TFont_SetColor(AlliedVariables.s_AlliedForm1Window!.TeamName1Ed, 0x0000FFFF);
    }

    // L00513838
    private static void TLibForm_Proc_00513838()
    {
        AlliedVariables.s_FlightGroupObjectsList = new();
        AlliedVariables.s_LibFormFlightGroupObjectsList = new();
        AlliedVariables.s_V0x00543CF4 = new();
        AlliedVariables.s_V0x00543CF8 = new();
        AlliedVariables.s_V0x00543CFC = new();
        AlliedVariables.s_RadioMessagesObjectsList = new();
        AlliedVariables.s_GlobalGoalsObjectsList = new();
        AlliedVariables.s_TeamsObjectsList = new();
        AlliedVariables.s_V0x00571184 = new();
        AlliedVariables.s_V0x00571188 = new();

        AlliedVariables.s_Strings_Backdrops = new();
        AlliedVariables.s_Strings_Ships = new();
        AlliedVariables.s_V0x00543B64 = new();
        AlliedVariables.s_Strings_FGNames = new();
        AlliedVariables.s_Strings_Players = new();
        AlliedVariables.s_Strings_Status = new();
        AlliedVariables.s_Strings_AI = new();
        AlliedVariables.s_Strings_IFF = new();
        AlliedVariables.s_Strings_Colors = new();
        AlliedVariables.s_Strings_Radio = new();
        AlliedVariables.s_Strings_Form = new();
        AlliedVariables.s_Strings_Missiles = new();
        AlliedVariables.s_Strings_Teams = new();
        AlliedVariables.s_Strings_Counters = new();
        AlliedVariables.s_Strings_Beams = new();
        AlliedVariables.s_Strings_CMD = new();
        AlliedVariables.s_Strings_OpShips = new();
        AlliedVariables.s_Strings_Orders = new();
        AlliedVariables.s_V0x00543BA8 = new();
        AlliedVariables.s_Strings_Speeds = new();
        AlliedVariables.s_Allied_FilenamesHistory = new();
        AlliedVariables.s_Allied_Numbers_NoneTo255 = new();
        AlliedVariables.s_Strings_Musts = new();
        AlliedVariables.s_Strings_ObjCats = new();
        AlliedVariables.s_Strings_ShipCats = new();
        AlliedVariables.s_V0x00543BC0 = new();
        AlliedVariables.s_Strings_Regions = new();
        AlliedVariables.s_V0x00543BC8 = new();
        AlliedVariables.s_V0x00543BCC = new();
        AlliedVariables.s_Strings_Short = new();
        AlliedVariables.s_V0x00543BD4 = new();
        AlliedVariables.s_Strings_Planets = new();
        AlliedVariables.s_V0x00543C3C = new();
        AlliedVariables.s_Strings_WPEnable = new();
        AlliedVariables.s_Strings_When = new();
        AlliedVariables.s_Strings_DepWhen = new();
        AlliedVariables.s_V0x00543BE8 = new();
        AlliedVariables.s_Strings_OrdTexts = new();
        AlliedVariables.s_Strings_ShipSeq = new();
        AlliedVariables.s_TDatapad_T1ClassStrings = new();
    }

    // L00513BA0
    private static void L00513BA0()
    {
        AlliedVariables.s_Allied_Numbers_NoneTo255.Add("none");

        for (int ebx = 0x01; ebx < 0x100; ebx++)
        {
            AlliedVariables.s_Allied_Numbers_NoneTo255.Add(ebx.ToString(CultureInfo.InvariantCulture));
        }

        AlliedVariables.s_Strings_Musts.Add("must");
        AlliedVariables.s_Strings_Musts.Add("must NOT");
        AlliedVariables.s_Strings_Musts.Add("BONUS must");
        AlliedVariables.s_Strings_Musts.Add("BONUS must NOT");

        AlliedVariables.s_Strings_ObjCats.Add("Space craft");
        AlliedVariables.s_Strings_ObjCats.Add("Weapons");
        AlliedVariables.s_Strings_ObjCats.Add("Satellites");

        for (int ebx = 0x04; ebx < 0x0B; ebx++)
        {
            AlliedVariables.s_Strings_ObjCats.Add(ebx.ToString(CultureInfo.InvariantCulture));
        }

        AlliedVariables.s_Strings_ShipCats.Add("Starfighters");
        AlliedVariables.s_Strings_ShipCats.Add("Transport craft");
        AlliedVariables.s_Strings_ShipCats.Add("Freighters/Containers");
        AlliedVariables.s_Strings_ShipCats.Add("Starships");
        AlliedVariables.s_Strings_ShipCats.Add("Utility craft");
        AlliedVariables.s_Strings_ShipCats.Add("Platforms");
        AlliedVariables.s_Strings_ShipCats.Add("Mines");

        for (int ebx = 0x07; ebx < 0x10; ebx++)
        {
            AlliedVariables.s_Strings_ShipCats.Add(ebx.ToString(CultureInfo.InvariantCulture));
        }

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            DatapadWindowImpl.TDatapad_L004C0EAC(AlliedVariables.s_TDatapad_Instance!);
        }

        TForm1_L005290F8(AlliedVariables.s_AlliedForm1Window!);

        AlliedVariables.s_AlliedForm1Window!.TeamIFFBox1.SetItems(AlliedVariables.s_Strings_IFF);

        AlliedVariables.s_Strings_Radio.Add("Off");

        for (int ebx = 0x01; ebx < 0x09; ebx++)
        {
            AlliedVariables.s_Strings_Radio.Add("Team " + ebx.ToString(CultureInfo.InvariantCulture));
        }

        for (int ebx = 0x01; ebx < 0x09; ebx++)
        {
            AlliedVariables.s_Strings_Radio.Add("Player " + ebx.ToString(CultureInfo.InvariantCulture));
        }
    }

    // L005290F8
    public static void TForm1_L005290F8(Form1Window Form1)
    {
        if (AlliedVariables.s_V0x005B6D15 == 0)
        {
            return;
        }

        if (AlliedVariables.s_TDatapad_Instance is null)
        {
            throw new ArgumentNullException(nameof(AlliedVariables.s_TDatapad_Instance));
        }

        AlliedVariables.s_TDatapad_Instance.MissleSelBox.SetItems(AlliedVariables.s_Strings_Missiles);
        AlliedVariables.s_TDatapad_Instance.MissleSelBox.Items.RemoveAt(0);

        AlliedVariables.s_TDatapad_Instance.BeamSelBox.SetItems(AlliedVariables.s_Strings_Beams);
        AlliedVariables.s_TDatapad_Instance.BeamSelBox.Items.RemoveAt(0);

        AlliedVariables.s_TDatapad_Instance.CounterSelbox.SetItems(AlliedVariables.s_Strings_Counters);
        AlliedVariables.s_TDatapad_Instance.CounterSelbox.Items.RemoveAt(0);
    }

    // L0052A6DC
    private static void TForm1_Proc_0052A6DC(Form1Window Form1)
    {
        AlliedVariables.s_V0x005B6D11 = 0;
        AlliedVariables.s_V0x005B6D12 = 0x01;
        AlliedVariables.s_V0x00543B5C = 0x01;

        string ebp3C_12 = "Can't find last loaded file";
        AlliedVariables.s_V0x005439CC.Text = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        string ebp3C_11 = AlliedVariables.s_V0x005439CC.Text + "\\AlliED.ini";

        if (File.Exists(ebp3C_11))
        {
            MessageBox_ShowInformation("You may need to reset your preferences. In particular, check \"Your AlliED Directory\".");
            string ebp3C_10 = AlliedVariables.s_V0x005439CC.Text + "\\AlliED.ini";
            File.Delete(ebp3C_10);
            Unit_00507130_Proc_00507584();
            Unit_00507130_Proc_005077D4(true);
        }
        else
        {
            Unit_00507130_Proc_00507584();
            Unit_00507130_Proc_005077D4(false);
        }

        if (AlliedVariables.s_V0x005439A8.m000004 == 0)
        {
            if (string.Equals(AlliedVariables.s_XWADirLabSetting, "Undefined") || string.Equals(AlliedVariables.s_AlliedDirectoryPath, "Undefined"))
            {
                AlliedVariables.s_AlliedDirectoryPath = AlliedVariables.s_AlliedDirectoryDriveLetter;
                Unit_00507130_Proc_005077D4(true);
            }

            Unit_00507130_Proc_0050739C();

            if (AlliedVariables.s_MpoLayoutSetting)
            {
                ComCtrls_TToolButton_SetDown(Form1.ShowFGList, true);
            }
            else
            {
                ComCtrls_TToolButton_SetDown(Form1.ShowFGList, false);
                TForm1_ShowFGListClick(Form1, Form1.ShowFGList);
            }

            Unit_00513838_Proc_005196CC();
            Unit_00513838_Proc_00513F30();
            AlliedVariables.s_V0x00543C10 = AlliedVariables.s_StartDirLabSetting;
            Dialogs_TOpenDialog_SetInitialDir(Form1.OpenDialog1, AlliedVariables.s_StartDirLabSetting);
            Dialogs_TOpenDialog_SetInitialDir(Form1.SaveDialog1, AlliedVariables.s_StartDirLabSetting);

            if (AlliedVariables.s_V0x00543990 != 0)
            {
                Unit_00513838_Proc_00516D84(
                    AlliedVariables.s_TCondToolForm_Instance!.PercentBox,
                    AlliedVariables.s_TCondToolForm_Instance!.ClassBox,
                    AlliedVariables.s_TCondToolForm_Instance!.IndexBox,
                    AlliedVariables.s_TCondToolForm_Instance!.CondBox
                    );
            }

            for (int ebp05 = 0; ebp05 < AlliedVariables.s_FlightGroupObjectsList.Count; ebp05++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp05);
                eax1.AutoLink = AlliedVariables.s_UseAutoChkSetting;
            }

            if (AlliedVariables.s_MpoLayoutSetting)
            {
                TApplication_L00468A40_SetActiveControl(Form1, Form1.ShipList);
            }

            if (AlliedVariables.s_V0x005B6D15 != 0)
            {
                DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
            }

            string ebp3C_9 = System_ParamStr(0x01);

            if (!string.IsNullOrEmpty(ebp3C_9))
            {
                AlliedVariables.s_V0x00543BF8 = string.Empty;

                int bl2 = System_ParamCount();

                for (int ebp05 = 0x01; ebp05 <= bl2; ebp05++)
                {
                    string ebp3C_8 = System_ParamStr(ebp05);
                    AlliedVariables.s_V0x00543BF8 += ebp3C_8 + " ";
                }

                string ebp3C_7 = AlliedVariables.s_V0x00543BF8;
                AlliedVariables.s_V0x00543BF8 = ebp3C_7;
                AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
                TForm1_ReadTieMission(Form1, AlliedVariables.s_V0x00543BF8);
            }
            else
            {
                switch (AlliedVariables.s_OpenWithRadioIndexSetting)
                {
                    case OpenWithRadioEnum.ChoiceBox:
                        {
                            AlliedVariables.s_TChoiceBox_Instance = MainImpl.CreateChoiceBox();
                            AlliedVariables.s_TChoiceBox_Instance.Owner = Form1;
                            AlliedVariables.s_TChoiceBox_Instance.ShowDialog();

                            if (AlliedVariables.s_TChoiceBox_Instance.DialogResult == true)
                            {
#if !DEBUG
                                try
#endif
                                {
                                    switch ((StartUpRadioEnum)AlliedVariables.s_TChoiceBox_Instance.StartUpRadio.SelectedIndex)
                                    {
                                        case StartUpRadioEnum.LoadExistingMission:
                                            {
                                                Unit_00513838_Proc_00515884(GameVersionEnum.Unknown);
                                                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, 0);
                                                eax1.m00147A = 0x01;
                                                AlliedVariables.s_V0x005B704C = 0x01;
                                                break;
                                            }

                                        case StartUpRadioEnum.CreateNewMission:
                                            {
                                                if (AlliedVariables.s_UseWizChkSetting)
                                                {
                                                    Unit_00513838_Proc_00519B44();
                                                }

                                                AlliedVariables.s_V0x005B704C = 0x01;
                                                break;
                                            }

                                        case StartUpRadioEnum.LoadLastUsedFile:
                                            {
                                                if (AlliedVariables.s_Allied_FilenamesHistory.GetCount() > 0)
                                                {
                                                    if (File.Exists(AlliedVariables.s_Allied_FilenamesHistory.GetText(0)))
                                                    {
                                                        AlliedVariables.s_V0x00543BF8 = AlliedVariables.s_Allied_FilenamesHistory.GetText(0);
                                                        TForm1_ReadTieMission(Form1, AlliedVariables.s_V0x00543BF8);
                                                    }
                                                    else
                                                    {
                                                        Allied_ShowMessageWithTimer(0x7D0, ebp3C_12);
                                                    }
                                                }
                                                else
                                                {
                                                    Allied_ShowMessageWithTimer(0x7D0, ebp3C_12);
                                                }

                                                break;
                                            }
                                    }
                                }
#if !DEBUG
                                catch
#endif
                                {
                                }
                            }

                            AlliedVariables.s_TChoiceBox_Instance = null;
                            break;
                        }

                    case OpenWithRadioEnum.OpenFileDialogue:
                        Unit_00513838_Proc_00515884(GameVersionEnum.Unknown);
                        break;

                    case OpenWithRadioEnum.LastUsedFile:
                        if (AlliedVariables.s_Allied_FilenamesHistory.GetCount() > 0)
                        {
                            AlliedVariables.s_V0x00543BF8 = AlliedVariables.s_Allied_FilenamesHistory.GetText(0);
                        }

                        if (File.Exists(AlliedVariables.s_V0x00543BF8))
                        {
                            TForm1_ReadTieMission(Form1, AlliedVariables.s_V0x00543BF8);
                        }
                        else
                        {
                            Allied_ShowMessageWithTimer(0x7D0, ebp3C_12);
                        }

                        break;
                }
            }

            AlliedVariables.s_V0x005B6D12 = 0;

            Unit_00513838_Proc_0051C83C();

            if (AlliedVariables.s_AlliedAplicationWidth == 0x320)
            {
                Controls_TControl_SetTop(AlliedVariables.s_AlliedForm1Window!, 0x01);
            }

            AlliedVariables.s_V0x005B70A8 = 0;
            AlliedVariables.s_V0x005B70AC = Unit_00513838_Proc_0051F674("Data\\Palette.bmp");

            if (File.Exists(AlliedVariables.s_V0x005B70AC))
            {
                AlliedVariables.s_V0x00543D40 = new TBitmap(AlliedVariables.s_V0x005B70AC);
                AlliedVariables.s_V0x005B70A8 = 0x01;
            }
            else
            {
                MessageBox_ShowError("Can't find Palette.bmp to draw Map icons. It should be in your AlliED directory as defined in Preferences:\r\n" + AlliedVariables.s_AlliedDirectoryPath);
            }

            // todo: upgrade
            //Controls_TControl_SetWidth(AlliedVariables.s_AlliedForm1Window!.OverallPages, AlliedVariables.s_V0x005B7088[0]);
            AlliedVariables.s_AlliedForm1Window!.OverallPages.MinWidth = AlliedVariables.s_V0x005B7088[0];

            if (AlliedVariables.s_V0x0054395C)
            {
                TApplication_ShowWindow(AlliedVariables.s_AlliedForm1Window!, 0x02);
            }

            Menus_TMenuItem_SetChecked(Form1.Numbergrid1, AlliedVariables.s_ShowNumbersMapSetting);
            AlliedVariables.s_V0x005B704C = 0x01;
            MapWindowImpl.TMapForm_Proc_004F6B48(AlliedVariables.s_TMapForm_Instance!);
        }
    }

    // L00507584
    private static void Unit_00507130_Proc_00507584()
    {
        AlliedVariables.s_V0x005439A8.m000000 = 0;
        AlliedVariables.s_V0x005439A8.m000002 = 0;

        using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
        using RegistryKey? key = baseKey.OpenSubKey("SOFTWARE\\Troy's Editors");

        AlliedVariables.s_V0x00543950 = RegistryReadKeyString(key, "AlliED", "AppName", "AlliED");
        AlliedVariables.s_V0x0054394C = RegistryReadKeyString(key, "AlliED", "AppVer", ProductVersionHelpers.GetVersion());

        Version.TryParse(ProductVersionHelpers.GetVersion(), out Version d);
        Version.TryParse(AlliedVariables.s_V0x0054394C, out Version f);

        if (f > d)
        {
            // todo
            //RegistryCreateKeyString(key, "AlliED", "AppVer", ProductVersionHelpers.GetVersion());
        }

        //s_V0x005439A8Ptr->m000004 = 0x01;
        // System_DoneExcept();

        AlliedVariables.s_V0x005439A8.AppInfo = RegistryReadKeyString(key, "AlliED", "AppInfo", "n/a");
        AlliedVariables.s_V0x005439A8.RegCode = RegistryReadKeyString(key, "AlliED", "RegCode", "n/a");
    }

    // L005077D4
    private static void Unit_00507130_Proc_005077D4(bool al0)
    {
        bool ebp01 = al0;
        bool ebp08 = false;

        using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
        using RegistryKey? key = baseKey.OpenSubKey("SOFTWARE\\Troy's Editors\\AlliED");

        AlliedVariables.s_XWADirLabSetting = RegistryReadKeyString(key, "Directories", "XWA", AlliedVariables.s_Allied_InstallPath);
        AlliedVariables.s_StartDirLabSetting = RegistryReadKeyString(key, "Directories", "Missions", AlliedVariables.s_Allied_InstallPath + "\\Missions");
        AlliedVariables.s_BriefingsDirectorySetting = RegistryReadKeyString(key, "Directories", "Briefings", AlliedVariables.s_StartDirLabSetting);
        string ebp18 = "C:\\Program Files\\LucasArts";
        AlliedVariables.s_XWDirLabSetting = RegistryReadKeyString(key, "Directories", "XWing", ebp18 + "\\XWing95\\X-Wing Data\\Mission");
        AlliedVariables.s_TFDirLabSetting = RegistryReadKeyString(key, "Directories", "T/F", ebp18 + "\\TIE95\\Mission");
        AlliedVariables.s_XvTDirLabSetting = RegistryReadKeyString(key, "Directories", "XvT", ebp18 + "\\XWingTie\\Train");
        AlliedVariables.s_BoPDirLabSetting = RegistryReadKeyString(key, "Directories", "BoP", ebp18 + "\\XWingTie\\BalanceOfPower\\Train");
        AlliedVariables.s_AlliedDirectoryPath = RegistryReadKeyString(key, "Directories", "AlliED", "Undefined");

        if (string.Equals(AlliedVariables.s_AlliedDirectoryPath, "Undefined"))
        {
            AlliedVariables.s_AlliedDirectoryPath = AlliedVariables.s_AlliedDirectoryDriveLetter;
            ebp01 = true;
            ebp08 = true;
        }

        // todo
        AlliedVariables.s_AlliedDirectoryPath = AlliedVariables.s_AlliedDirectoryDriveLetter;

        if (!Directory.Exists(AlliedVariables.s_AlliedDirectoryPath))
        {
            MessageBox_ShowWarning(string.Concat("WARNING: AlliED directory: \"", AlliedVariables.s_AlliedDirectoryPath, "\" does not exist. \r\n\r\nPlease set it in Preferences."));
        }

        AlliedVariables.s_WaveDirLabSetting = RegistryReadKeyString(key, "Directories", "Wave", AlliedVariables.s_Allied_InstallPath + "\\wave");
        AlliedVariables.s_OPTDirLabSetting = RegistryReadKeyString(key, "Directories", "OPT", AlliedVariables.s_Allied_InstallPath + "\\FLIGHTMODELS");
        AlliedVariables.s_UseAutoChkSetting = RegistryReadKeyBool(key, "Options", "Autolink", true);
        AlliedVariables.s_PlayableChkSetting = RegistryReadKeyBool(key, "Options", "CheckPlayable", true);
        AlliedVariables.s_ConfDeletesChkSetting = RegistryReadKeyBool(key, "Options", "ConfirmDeletes", true);
        AlliedVariables.s_BlackenChkSetting = RegistryReadKeyBool(key, "Options", "Blacken", true);
        AlliedVariables.s_EnableWPsChkSetting = RegistryReadKeyBool(key, "Options", "EnableWPs", true);
        AlliedVariables.s_LinkColorChkSetting = RegistryReadKeyBool(key, "Options", "LinkColor", true);
        AlliedVariables.s_ResChkSetting = RegistryReadKeyBool(key, "Options", "DPtoggle", true);
        AlliedVariables.s_UseWizChkSetting = RegistryReadKeyBool(key, "Options", "UseWiz", true);
        AlliedVariables.s_MissionFormatRadioIndexSetting = (byte)RegistryReadKeyInteger(key, "Options", "DefaultFormat", 0x01);
        AlliedVariables.s_OpenWithRadioIndexSetting = (OpenWithRadioEnum)RegistryReadKeyInteger(key, "Options", "OpenProgWith", (int)OpenWithRadioEnum.ChoiceBox);
        AlliedVariables.s_DirInHistChkSetting = RegistryReadKeyBool(key, "Options", "DirInReopen", true);
        AlliedVariables.s_DefaultShipBoxSettingIndex = (CraftIdEnum)RegistryReadKeyInteger(key, "Options", "DefaultShip", (int)CraftIdEnum._001_0_0_Xwing);
        AlliedVariables.s_DefaultAIBoxSettingIndex = RegistryReadKeyInteger(key, "Options", "DefaultAI", 0x02);
        AlliedVariables.s_ConfSaveChkSetting = RegistryReadKeyBool(key, "Options", "FileSavedMsg", false);
        AlliedVariables.s_DoBackupsChkSetting = RegistryReadKeyBool(key, "Options", "DoBackups", false);
        AlliedVariables.s_LockOrdToRegOptionSetting = RegistryReadKeyBool(key, "Options", "LockOrdToReg", true);
        AlliedVariables.s_CheckFilenameOptionSetting = RegistryReadKeyBool(key, "Options", "CheckFilename", true);
        AlliedVariables.s_OverwriteBKUPOptionSetting = RegistryReadKeyBool(key, "Options", "OverwriteBKUP", false);
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.ErrCheckOn1, RegistryReadKeyBool(key, "Options", "ErrorCheck", true));
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Overwriteprevious1, AlliedVariables.s_OverwriteBKUPOptionSetting);
        AlliedVariables.s_V0x00543938.ShowCurrDown = RegistryReadKeyBool(key, "Options", "BrfOpt1", false);
        AlliedVariables.s_V0x00543938.ShowNumsDown = RegistryReadKeyBool(key, "Options", "BrfOpt2", false);
        AlliedVariables.s_V0x00543938.StopAtStopDown = RegistryReadKeyBool(key, "Options", "BrfOpt3", false);
        AlliedVariables.s_V0x00543938.FastPlaybackBtnDown = RegistryReadKeyBool(key, "Options", "BrfOpt4", false);
        AlliedVariables.s_IconSpeedOptionSetting = (short)RegistryReadKeyInteger(key, "Options", "IconSpeed", 0x28);

        for (int ebp0C = 0x01; ebp0C < 0x04; ebp0C++)
        {
            S0x00534058_000000 esi = AlliedVariables.s_V0x00543C84.M000000[ebp0C - 1];
            esi.M000000 = RegistryReadKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", ebp0C, 0), 0) != 0;
            esi.M000001 = (byte)RegistryReadKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", ebp0C, 1), 0);
            esi.M000002 = (byte)RegistryReadKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", ebp0C, 2), 1);
            esi.M000003 = (byte)RegistryReadKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", ebp0C, 3), 2);
            esi.M000004 = (byte)RegistryReadKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", ebp0C, 4), 3);
            esi.M000005 = (byte)RegistryReadKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", ebp0C, 5), 2);
            esi.M000006 = RegistryReadKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", ebp0C, 6), 1) != 0;
        }

        AlliedVariables.s_CenteringSetting = RegistryReadKeyBool(key, "Map", "Centre", true);
        AlliedVariables.s_ConfirmCloseMapSetting = RegistryReadKeyBool(key, "Map", "ConfirmClose", true);
        AlliedVariables.s_GhostMapChkSetting = RegistryReadKeyBool(key, "Map", "GhostText", true);
        AlliedVariables.s_SSD17chkSetting = RegistryReadKeyBool(key, "Map", "17.6km SSD", false);
        AlliedVariables.s_WPsDefaultRadioIndexSetting = (WPsDefaultRadioEnum)RegistryReadKeyInteger(key, "Map", "DefaultWPs", (int)WPsDefaultRadioEnum.None);
        AlliedVariables.s_AutoCtrGrpIndexSetting = (AutoCtrGrpEnum)RegistryReadKeyInteger(key, "Map", "AutoCtr", (int)AutoCtrGrpEnum.LockCurr);
        AlliedVariables.s_ZoomSpeedScrollPositionSetting = (byte)RegistryReadKeyInteger(key, "Map", "ZoomSpeed", 0x0F);
        AlliedVariables.s_DblClickLinkRadioIndexSetting = RegistryReadKeyInteger(key, "Map", "DblClickLink", 0);
        AlliedVariables.s_NamesOnChkSetting = RegistryReadKeyBool(key, "Map", "DefaultName", false);
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Showtimes, RegistryReadKeyBool(key, "Map", "DefaultWPtime", false));
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.Showdistances, RegistryReadKeyBool(key, "Map", "DefaultWPdist", false));
        AlliedVariables.s_DefaultPalletOnChkSetting = RegistryReadKeyBool(key, "Map", "DefaultPalet", false);
        AlliedVariables.s_DefaultFGListMapSetting = RegistryReadKeyBool(key, "Map", "DefaultFGList", false);
        AlliedVariables.s_DefaultHypOnChkSetting = RegistryReadKeyBool(key, "Map", "DefaultHyp", true);
        AlliedVariables.s_DefaultOptionsOnChkSetting = RegistryReadKeyBool(key, "Map", "DefaultOptions", false);
        AlliedVariables.s_LimitShrinkChkSetting = RegistryReadKeyBool(key, "Map", "LimitShrink", true);
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.MinimumWireframesSizes1, AlliedVariables.s_LimitShrinkChkSetting);
        AlliedVariables.s_DarkGridChkSetting = RegistryReadKeyBool(key, "Map", "GhostGrid", true);
        AlliedVariables.s_ShowNumbersMapSetting = RegistryReadKeyBool(key, "Map", "ShowNumbers", true);
        AlliedVariables.s_OnlyXYChkSetting = RegistryReadKeyBool(key, "Map", "OnlyXY", false);
        AlliedVariables.s_IconZoomEditSetting = RegistryReadKeyInteger(key, "Map", "IconZoomSmall", 0x04);

        for (int ebx = 0; ebx < 0x03; ebx++)
        {
            AlliedVariables.s_V0x005B7088[ebx] = RegistryReadKeyInteger(key, "Layout", "lw" + ebx.ToString(CultureInfo.InvariantCulture), 0x11D);
        }

        AlliedVariables.s_V0x0054395C = RegistryReadKeyBool(key, "Layout", "wmx", false);
        AlliedVariables.s_V0x005B7048 = RegistryReadKeyBool(key, "Layout", "dpo", true);
        AlliedVariables.s_MpoLayoutSetting = RegistryReadKeyBool(key, "Layout", "mpo", true);

        static void S0x005342B0_00000C_Read(RegistryKey? key, S0x005342B0_00000C esi, int ebx)
        {
            esi.Top = (short)RegistryReadKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "1", 0);
            esi.Left = (short)RegistryReadKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "2", 0);
            esi.Height = (short)RegistryReadKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "3", 0);
            esi.Width = (short)RegistryReadKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "4", 0);
            esi.m000008 = (short)RegistryReadKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "5", 0);
        }

        S0x005342B0_00000C_Read(key, AlliedVariables.s_V0x005AFC74.m000000, 1);
        S0x005342B0_00000C_Read(key, AlliedVariables.s_V0x005AFC74.m00000C, 2);
        S0x005342B0_00000C_Read(key, AlliedVariables.s_V0x005AFC74.m000018, 3);
        S0x005342B0_00000C_Read(key, AlliedVariables.s_V0x005AFC74.m000024, 4);
        S0x005342B0_00000C_Read(key, AlliedVariables.s_V0x005AFC74.m000030, 5);
        S0x005342B0_00000C_Read(key, AlliedVariables.s_V0x005AFC74.m00003C, 6);

        if (AlliedVariables.s_V0x005AFC74.m000000.Height == 0)
        {
            TForm1_SetRect_L0052BE68(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFC74.m000000, 0x97, 0x177, 0x18C, 0x137);
        }

        if (AlliedVariables.s_V0x005AFC74.m000030.Height == 0)
        {
            TForm1_SetRect_L0052BE68(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFC74.m000030, 0xC6, 0x10B, 0x19A, 0x198);
        }

        if (AlliedVariables.s_V0x005AFC74.m000024.Width == 0)
        {
            TForm1_SetRect_L0052BE68(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFC74.m000024, 0x14, 0x111, 0xF7, 0x14);
        }

        if (AlliedVariables.s_V0x005AFC74.m00003C.Width == 0)
        {
            TForm1_SetRect_L0052BE68(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFC74.m00003C, 0x14, 0x160, 0x169, 0x14);
        }

        if (AlliedVariables.s_V0x005AFC74.m000018.Top == 0)
        {
            DatapadWindowImpl.TDatapad_Proc_004C32C8(AlliedVariables.s_TDatapad_Instance!, out int left, out int top);
            AlliedVariables.s_V0x005AFC74.m000018.Top = (short)top;
            AlliedVariables.s_V0x005AFC74.m000018.Left = (short)left;
        }

        if (AlliedVariables.s_V0x005AFC74.m00000C.Height == 0)
        {
            TForm1_SetRect_L0052BE68(
                AlliedVariables.s_AlliedForm1Window!,
                AlliedVariables.s_V0x005AFC74.m00000C,
                AlliedVariables.s_V0x005AFC74.m000018.Top + 0x19C,
                0x155,
                0x58,
                AlliedVariables.s_V0x005AFC74.m000018.Left
                );

            AlliedVariables.s_V0x005AFC74.m00000C.m000008 = 0x01;
        }

        AlliedVariables.s_V0x005B704A = AlliedVariables.s_V0x005AFC74.m00000C.m000008 != 0;

        for (int ebx = 0; ebx < 0x0C; ebx++)
        {
            AlliedVariables.s_V0x00543C64[ebx] = RegistryReadKeyBool(key, "Layout", "seo" + ebx.ToString(CultureInfo.InvariantCulture), true);

            if (ebx == 0x09)
            {
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.ShowWAVman, AlliedVariables.s_V0x00543C64[ebx]);
            }

            switch (ebx)
            {
                case 0x00:
                case 0x01:
                case 0x04:
                    break;

                default:
                    AlliedVariables.s_V0x00543C64[ebx] = false;
                    break;
            }
        }

        for (int ebx = 0; ebx < 0x08; ebx++)
        {
            AlliedVariables.s_V0x00543ACC[ebx] = RegistryReadKeyInteger(key, "Layout", "tbh" + ebx.ToString(CultureInfo.InvariantCulture), -1);
        }

        if (RegistryReadKeyInteger(key, "Layout", "tbh9", -1) != -1)
        {
            AlliedVariables.s_V0x005439A8.m000004 = 0xD3;
        }

        if (AlliedVariables.s_AlliedForm1Window!.ShowWAVman.IsChecked == true)
        {
            Controls_TControl_SetVisible(AlliedVariables.s_AlliedForm1Window!.WAVPanel, false);
        }

        TForm1_WAVfilemanager1Click(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.ShowWAVman);

        key?.Dispose();
        baseKey.Dispose();

        // System_DoneExcept();

        if (ebp01)
        {
            AlliedVariables.s_TPrefForm_Instance = MainImpl.CreatePreferencesWindow();
            AlliedVariables.s_TPrefForm_Instance.Owner = AlliedVariables.s_AlliedForm1Window!;

            if (ebp08)
            {
                ComCtrls_TPageControl_SetActivePage(AlliedVariables.s_TPrefForm_Instance.PageControl1, AlliedVariables.s_TPrefForm_Instance.Directories);
            }

            AlliedVariables.s_TPrefForm_Instance.ShowDialog();

            if (AlliedVariables.s_TPrefForm_Instance.DialogResult == true && AlliedVariables.s_V0x00543948 == 0)
            {
                Unit_00507130_Proc_00508BF4();
            }

            AlliedVariables.s_TPrefForm_Instance = null;
        }

        Unit_00513838_Proc_0051DB68();
        Menus_TMenuItem_SetChecked(AlliedVariables.s_AlliedForm1Window!.LinkHyppoint1, AlliedVariables.s_DefaultHypOnChkSetting);
    }

    // L0052FE1C
    private static void TForm1_WAVfilemanager1Click(Form1Window Form1, object? Sender)
    {
        Controls_TControl_SetVisible(Form1.WAVPanel, !Form1.WAVPanel.IsVisible);
        Menus_TMenuItem_SetChecked(Form1.WAVfilemanager1, Form1.WAVPanel.IsVisible);
        ComCtrls_TToolButton_SetDown(Form1.ShowWAVman, Form1.WAVPanel.IsVisible);

        if (!Form1.WAVPanel.IsVisible && Form1.EndMsgWav.IsChecked == true)
        {
            ComCtrls_TToolButton_SetDown(Form1.EndMsgWav, false);
            TForm1_EndMsgWavClick(Form1, Form1.EndMsgWav);
        }
    }

    // L0050739C
    private static void Unit_00507130_Proc_0050739C()
    {
        string ebp10_2 = AlliedVariables.s_AlliedDirectoryPath + "\\EDHistry.txt";

        if (File.Exists(ebp10_2))
        {
            AlliedLoadTStringsItemsFromFileAndFillComboBox("EDHistry", AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_AlliedForm1Window!.SelectionBox, false);
            File.Delete(ebp10_2);
        }
        else
        {
            AlliedVariables.s_Allied_FilenamesHistory.Clear();

            using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            using RegistryKey? key = baseKey.OpenSubKey("SOFTWARE\\Troy's Editors\\AlliED");

            int ebx = RegistryReadKeyInteger(key, "History", "Count", 0);

            for (int esi = 0; esi < ebx; esi++)
            {
                string ebp10_0 = string.Format(CultureInfo.InvariantCulture, "file{0}", esi + 1);
                string ebp10_1 = RegistryReadKeyString(key, "History", ebp10_0, "empty");
                AlliedVariables.s_Allied_FilenamesHistory.Add(ebp10_1);
            }
        }
    }

    // L00519B44
    private static void Unit_00513838_Proc_00519B44()
    {
    }

    // L005258A8
    public static string TForm1_Proc_005258A8(Form1Window Form1, S0xFGObject edx0)
    {
        string ebp28_9 = "   " + Allied_UIntToHexString(edx0.FlightGroupStruct.Team + 1, 0x01);

        if (edx0.FlightGroupStruct.GlobalGroupId < 0x0A)
        {
            ebp28_9 += "  ";
        }

        ebp28_9 += edx0.FlightGroupStruct.GlobalGroupId.ToString(CultureInfo.InvariantCulture);
        ebp28_9 += "  -";

        if (edx0.FlightGroupStruct.GlobalUnitId < 0x0A)
        {
            ebp28_9 += "  ";
        }

        ebp28_9 += edx0.FlightGroupStruct.GlobalUnitId.ToString(CultureInfo.InvariantCulture);

        if (edx0.FlightGroupStruct.PlayerNumber != 0)
        {
            if (edx0.FlightGroupStruct.ArriveOnlyIfPlayer)
            {
                ebp28_9 += "? ";
            }
            else
            {
                ebp28_9 += "   ";
            }

            ebp28_9 += "(" + edx0.FlightGroupStruct.PlayerNumber.ToString(CultureInfo.InvariantCulture) + ")  ";
        }
        else
        {
            if (edx0.FlightGroupStruct.ArrivalDifficulty == (ArrivalDifficultyEnum)0x00)
            {
                ebp28_9 += "         ";
            }
            else
            {
                switch (edx0.FlightGroupStruct.ArrivalDifficulty)
                {
                    case (ArrivalDifficultyEnum)0x01:
                    case (ArrivalDifficultyEnum)0x08:
                        {
                            ebp28_9 += "    E  ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x02:
                    case (ArrivalDifficultyEnum)0x09:
                        {
                            ebp28_9 += "    M  ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x03:
                    case (ArrivalDifficultyEnum)0x0A:
                        {
                            ebp28_9 += "    H  ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x04:
                        {
                            ebp28_9 += "  >E   ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x05:
                        {
                            ebp28_9 += "  <H  ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x06:
                    case (ArrivalDifficultyEnum)0x07:
                        {
                            ebp28_9 += "   ---   ";
                            break;
                        }
                }
            }
        }

        string ebp28_4 = (edx0.FlightGroupStruct.WavesCount + 1).ToString(CultureInfo.InvariantCulture);
        string ebp28_3 = edx0.FlightGroupStruct.CraftsCount.ToString(CultureInfo.InvariantCulture);
        string ebp28_2 = AlliedGetCraftShortString(edx0.FlightGroupStruct.CraftId);
        string ebp28_1 = System_LStrFromPCharLen(edx0.FlightGroupStruct.Name, 0x14);
        ebp28_9 += ebp28_4 + "  x  [ " + ebp28_3 + " ]  " + ebp28_2 + "  " + ebp28_1;

        return ebp28_9;
    }

    // L005273D4
    public static void L005273D4(Form1Window eax0, object? eax1)
    {
    }

    // L005187A0
    public static void Unit_00513838_Proc_005187A0()
    {
        AlliedVariables.s_Strings_Teams.Clear();

        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            string ebp18_4 = (ebx + 1).ToString(CultureInfo.InvariantCulture);
            S0xTieTeamObject eax0 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, ebx);
            string ebp18_3 = System_LStrFromPCharLen(eax0.Team.Name, 0x10);
            AlliedVariables.s_Strings_Teams.Add(ebp18_4 + "." + ebp18_3);

            if (AlliedVariables.s_V0x005B6D12 == 0)
            {
                string ebp18_2 = AlliedVariables.s_Strings_Teams.GetText(ebx);
                Menus_TMenuItem_SetCaption(Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.TabSheet14.ContextMenu, ebx), ebp18_2);
                Menus_TMenuItem_SetCaption(Menus_TMenuItem_GetItem(AlliedVariables.s_AlliedForm1Window!.TeamDrop, ebx), ebp18_2);
            }
        }

        if (AlliedVariables.s_V0x005B6D16 != 0)
        {
            for (int ebx = 0; ebx < 0x08; ebx++)
            {
                AlliedVariables.s_Strings_Radio.Put(ebx + 1, "Team " + AlliedVariables.s_Strings_Teams.GetText(ebx));
            }

            AlliedVariables.s_TDatapad_Instance!.TeamBox.SetItems(AlliedVariables.s_Strings_Teams);
            AlliedVariables.s_TShipExt_Instance!.RadioBox.SetItems(AlliedVariables.s_Strings_Radio);

            if ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag) == DatapadFGPageEnum.Ship)
            {
                S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
                AlliedVariables.s_TShipExt_Instance!.RadioBox.SelectedIndex = eax0.FlightGroupStruct.Radio;

                eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
                AlliedVariables.s_TDatapad_Instance!.TeamBox.SelectedIndex = eax0.FlightGroupStruct.Team;
            }

            if ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag) == DatapadFGPageEnum.FGGoals)
            {
                int ebx = Allied_ComboBox_GetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.FGGoalTeamBox);
                AlliedVariables.s_TDatapad_Instance!.FGGoalTeamBox.SetItems(AlliedVariables.s_Strings_Teams);
                Allied_ComboBox_SetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.FGGoalTeamBox, ebx);
            }
        }

        Unit_00513838_Proc_00515A04();
        L0051BEC8();

        if (AlliedVariables.s_V0x005B6D16 != 0)
        {
            AlliedVariables.s_TDatapad_Instance!.TeamBox.SetItems(AlliedVariables.s_Strings_Teams);
            AlliedVariables.s_TShipExt_Instance!.RadioBox.SetItems(AlliedVariables.s_Strings_Radio);
        }

        L0051751C();
    }

    // L0051BEC8
    public static void L0051BEC8()
    {
        AlliedVariables.s_AlliedForm1Window!.FriendsList.Clear();

        for (int ebx = 0; ebx < 8; ebx++)
        {
            AlliedVariables.s_AlliedForm1Window!.FriendsList.AddItem(AlliedVariables.s_Strings_Teams.GetText(ebx));
        }

        L0051BF50(AlliedVariables.s_V0x00543B18);
    }

    // L0051751C
    private static void L0051751C()
    {
    }

    // L00515884
    private static void Unit_00513838_Proc_00515884(GameVersionEnum eax0)
    {
        switch (eax0)
        {
            case GameVersionEnum.Unknown:
                {
                    Dialogs_TOpenDialog_SetInitialDir(AlliedVariables.s_AlliedForm1Window!.OpenDialog1, AlliedVariables.s_V0x00543C10);
                    break;
                }

            case GameVersionEnum.XWing:
                {
                    Dialogs_TOpenDialog_SetInitialDir(AlliedVariables.s_AlliedForm1Window!.OpenDialog1, AlliedVariables.s_XWDirLabSetting);
                    break;
                }

            case GameVersionEnum.TieFighter:
                {
                    Dialogs_TOpenDialog_SetInitialDir(AlliedVariables.s_AlliedForm1Window!.OpenDialog1, AlliedVariables.s_TFDirLabSetting);
                    break;
                }

            case GameVersionEnum.XvT:
                {
                    Dialogs_TOpenDialog_SetInitialDir(AlliedVariables.s_AlliedForm1Window!.OpenDialog1, AlliedVariables.s_XvTDirLabSetting);
                    break;
                }

            case GameVersionEnum.BoP:
                {
                    Dialogs_TOpenDialog_SetInitialDir(AlliedVariables.s_AlliedForm1Window!.OpenDialog1, AlliedVariables.s_BoPDirLabSetting);
                    break;
                }
        }

        if (eax0 < GameVersionEnum.XWing)
        {
            AlliedVariables.s_AlliedForm1Window!.OpenDialog1.FilterIndex = 0;
        }
        else if (eax0 == GameVersionEnum.XWing)
        {
            AlliedVariables.s_AlliedForm1Window!.OpenDialog1.FilterIndex = 1;
        }
        else if (eax0 < GameVersionEnum.XWA)
        {
            AlliedVariables.s_AlliedForm1Window!.OpenDialog1.FilterIndex = 0;
        }

        if (AlliedVariables.s_AlliedForm1Window!.OpenDialog1.ShowDialog(AlliedVariables.s_AlliedForm1Window!) == true)
        {
            AlliedVariables.s_V0x00543BF8 = AlliedVariables.s_AlliedForm1Window!.OpenDialog1.FileName;
            AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);

            if (eax0 == GameVersionEnum.Unknown)
            {
                AlliedVariables.s_V0x00543C10 = Directory.GetCurrentDirectory();
            }

            TForm1_ReadTieMission(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x00543BF8);
        }
    }

    // L00529468
    private static void TForm1_ShipListDrawItem(Form1Window Form1, ListBox sender, int index)
    {
        if (index < AlliedVariables.s_FlightGroupObjectsList.Count)
        {
            ListBoxItem item = sender.GetItem(index);
            S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index);

            switch (eax2.FlightGroupStruct.ArrivalDifficulty)
            {
                case (ArrivalDifficultyEnum)0x08:
                case (ArrivalDifficultyEnum)0x09:
                    {
                        Graphics_TFont_SetColor(item, 0x00808080);
                        break;
                    }

                default:
                    {
                        S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index);

                        if (eax3.m001444[AlliedVariables.s_CurrentRegion - 1] == 0 && AlliedVariables.s_BlackenChkSetting)
                        {
                            Graphics_TFont_SetColor(item, 0x004F4E4F);
                        }
                        else
                        {
                            S0xFGObject eax4 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index);
                            uint edx1 = AlliedGetIffColor(eax4.FlightGroupStruct.Iff, 0);
                            Graphics_TFont_SetColor(item, edx1);
                        }

                        break;
                    }
            }
        }
    }

    // L0052CF90
    private static void TForm1_SelectionBoxDrawItem(Form1Window Form1, ComboBox sender, int index)
    {
        if (index < AlliedVariables.s_FlightGroupObjectsList.Count)
        {
            ComboBoxItem item = sender.GetItem(index);
            S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index);

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
                        S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index);

                        if (eax3.m001444[AlliedVariables.s_CurrentRegion - 1] == 0)
                        {
                            Graphics_TFont_SetColor(item, 0x004F4E4F);
                        }
                        else
                        {
                            S0xFGObject eax4 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, index);
                            Graphics_TFont_SetColor(item, AlliedGetIffColor(eax4.FlightGroupStruct.Iff, 0));
                        }

                        break;
                    }
            }
        }
    }

    // L0051C83C
    private static void Unit_00513838_Proc_0051C83C()
    {
        if (File.Exists(AlliedVariables.s_AlliedDirectoryPath + "\\Conds.clp"))
        {
            TFileRec ebp154 = new();
            ebp154.Assign(AlliedVariables.s_AlliedDirectoryPath + "\\Conds.clp");
            ebp154.OpenFileForRead(0x06);
            System_L004028C4_CheckError();
            byte[] ebp06 = new byte[S0xTieTrigger.Size];

            while (true)
            {
                bool al1 = ebp154.EofFile();
                System_L004028C4_CheckError();

                if (al1)
                {
                    break;
                }

                ebp154.ReadRec(ebp06);
                System_L004028C4_CheckError();

                S0xCondObjectStruct eax4 = new();
                eax4.m000004 = S0xTieTrigger.FromByteArray(ebp06);
                AlliedVariables.s_V0x00543CF8.Add(eax4);
            }

            ebp154.Close();
            System_L004028C4_CheckError();
        }

        if (File.Exists(AlliedVariables.s_AlliedDirectoryPath + "\\Orders.clp"))
        {
            TFileRec ebp2A0 = new();
            ebp2A0.Assign(AlliedVariables.s_AlliedDirectoryPath + "\\Orders.clp");
            ebp2A0.OpenFileForRead(0x94);
            System_L004028C4_CheckError();
            byte[] ebp334 = new byte[S0xTieFlightGroupOrder.Size];

            while (true)
            {
                bool al3 = ebp2A0.EofFile();
                System_L004028C4_CheckError();

                if (al3)
                {
                    break;
                }

                ebp2A0.ReadRec(ebp334);
                System_L004028C4_CheckError();

                S0xOrdObject eax2 = new();
                eax2.m000004 = S0xTieFlightGroupOrder.FromByteArray(ebp334);
                AlliedVariables.s_V0x00543CFC.Add(eax2);
            }

            ebp2A0.Close();
            System_L004028C4_CheckError();
        }
    }

    // L0051F674
    private static string Unit_00513838_Proc_0051F674(string eax0)
    {
        string ebp0C_1 = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", AlliedVariables.s_AlliedDirectoryPath, eax0);
        string ebp0C_0 = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", AlliedVariables.s_AlliedDirectoryDriveLetter, eax0);

        if (File.Exists(ebp0C_1))
        {
            return ebp0C_1;
        }

        if (File.Exists(ebp0C_0))
        {
            return ebp0C_0;
        }

        return "n";
    }

    // L0052D3F8
    public static void TForm1_Proc_0052D3F8(Form1Window Form1)
    {
        if (AlliedVariables.s_V0x005B704C != 0 && AlliedVariables.s_V0x00543D4C != 0)
        {
            AlliedVariables.s_V0x005AFCBC!.RenderOpen();

            TForm1_Proc_0052D4CC(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFCBC);
            TForm1_Proc_0052D968(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005AFCBC!);


            if (AlliedVariables.s_FlightGroupObjectsList.Count > AlliedVariables.s_V0x005B7050)
            {
                S0xFGObject eax0 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
                if (eax0.m00147A != 0)
                {
                    TForm1_Proc_0052DC20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B7050, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!);
                }
            }

            AlliedVariables.s_V0x005AFCBC!.RenderClose();

            PaintBoxControl eax1 = AlliedVariables.s_AlliedForm1Window!.PaintBox1;
            TForm1_Proc_0052D348(AlliedVariables.s_AlliedForm1Window!, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!, eax1);
        }
    }

    // L0052CDF0
    private static void TForm1_PaintBox1Paint(Form1Window Form1, PaintBoxControl Sender)
    {
        if (AlliedVariables.s_V0x00543D4C != 0)
        {
            TForm1_Proc_0052D3F8(Form1);
        }
    }

    // L0052C348
    private static void TForm1_ZoomBarChange(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C48 >= AlliedVariables.s_V0x00543D4C)
        {
            AlliedVariables.s_V0x005B7005 = 0x01;
        }
        else
        {
            AlliedVariables.s_V0x005B7005 = 0;
        }

        AlliedVariables.s_V0x00543D4C = (int)Form1.ZoomBar.Value;
        TMapForm_Proc_004F69A0(AlliedVariables.s_TMapForm_Instance);

        if (AlliedVariables.s_V0x005B7004 == 0)
        {
            return;
        }

        AlliedVariables.s_V0x005B7020 = Math.Round(MapWindowImpl.TMapForm_Proc_004F65C8(AlliedVariables.s_TMapForm_Instance!, 0, AlliedVariables.s_V0x005B7030));
        AlliedVariables.s_V0x005B7028 = Math.Round(MapWindowImpl.TMapForm_Proc_004F65C8(AlliedVariables.s_TMapForm_Instance!, 0x01, AlliedVariables.s_V0x005B7038));
        TForm1_Proc_0052D3F8(Form1);
        MapWindowImpl.TMapForm__PROC_004F6A80(AlliedVariables.s_TMapForm_Instance!);
    }

    // L004F69A0
    private static void TMapForm_Proc_004F69A0(MapWindow? MapForm)
    {
        string ebp04 = AlliedVariables.s_V0x00543D4C.ToString(CultureInfo.InvariantCulture);
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.Label5, ebp04);
        AlliedVariables.s_AlliedForm1Window!.Label5.Update();
    }

    // L0052CE1C
    public static void TForm1_FitBattleBtnClick(Form1Window Form1, object? edx0)
    {
        Unit_00513838_Proc_0051D614();
        MapWindowImpl.TMapForm_Proc_004F8A94(AlliedVariables.s_TMapForm_Instance!);

        int ebx = AlliedVariables.s_V0x00543C58[AlliedVariables.s_V0x005B705C];

        if (ebx == 0)
        {
            ebx = 0x7D0;
        }
        else
        {
            ebx = Math.Abs((int)Math.Round(121000.0f * AlliedVariables.s_V0x005B7010 / ebx));
        }

        int esi = AlliedVariables.s_V0x00543C58[AlliedVariables.s_V0x005B7058];

        if (esi == 0)
        {
            esi = 0x7D0;
        }
        else
        {
            esi = Math.Abs((int)Math.Round(76000.0f * AlliedVariables.s_V0x005B7018 / esi));
        }

        if (esi < ebx)
        {
            AlliedVariables.s_V0x00543C48 = esi;
        }
        else
        {
            AlliedVariables.s_V0x00543C48 = ebx;
        }

        if (AlliedVariables.s_V0x00543C48 > 0x4B0)
        {
            AlliedVariables.s_V0x00543C48 -= 0x32;
        }
        else if (AlliedVariables.s_V0x00543C48 > 0xC8)
        {
            AlliedVariables.s_V0x00543C48 -= 0x1E;
        }
        else if (AlliedVariables.s_V0x00543C48 > 0x19)
        {
            AlliedVariables.s_V0x00543C48 -= 0x03;
        }
        else if (AlliedVariables.s_V0x00543C48 > 0x11)
        {
            AlliedVariables.s_V0x00543C48 -= 0x01;
        }

        AlliedVariables.s_V0x005B7004 = 0;
        StdCtrls_TScrollBar_SetPosition(AlliedVariables.s_AlliedForm1Window!.ZoomBar, AlliedVariables.s_V0x00543C48);
        AlliedVariables.s_V0x005B7004 = 0x01;
        MapWindowImpl.TMapForm__PROC_004F68E8(AlliedVariables.s_TMapForm_Instance!, AlliedVariables.s_V0x00543C4C[AlliedVariables.s_V0x005B705C], -AlliedVariables.s_V0x00543C4C[AlliedVariables.s_V0x005B7058]);
        AlliedVariables.s_V0x005B7005 = 0x01;
    }

    // L0052D4CC
    private static void TForm1_Proc_0052D4CC(Form1Window eax0, TBitmap? edx0)
    {
        MapWindowImpl.TMapForm_PROC_004F894C(AlliedVariables.s_TMapForm_Instance!);

        TBitmap esi = Graphics_TBitmap_GetCanvas(edx0!)!;
        Graphics_TBrush_SetColor(esi, 0);
        Graphics_TCanvas_FillRect(esi, eax0.GetClientRect());
        Graphics_TBrush_SetStyle(esi, 0x01);

        if (AlliedVariables.s_V0x005B7047)
        {
            Graphics_TPen_SetStyle(Graphics_TBitmap_GetCanvas(edx0)!, 0);
            Graphics_TFont_SetColor(Graphics_TBitmap_GetCanvas(edx0)!, 0x00C0C0C0);

            int ebp10;

            if (AlliedVariables.s_V0x00543D4C > 0x41)
            {
                ebp10 = 0x01;
            }
            else if (AlliedVariables.s_V0x00543D4C > 0x14)
            {
                ebp10 = 0x05;
            }
            else if (AlliedVariables.s_V0x00543D4C > 0x08)
            {
                ebp10 = 0x0A;
            }
            else
            {
                ebp10 = 0x14;
            }

            int ebx0 = (int)Math.Round(AlliedVariables.s_V0x005B6FE0);
            int edi0 = (int)Math.Round(AlliedVariables.s_V0x005B6FE8);

            // upgrade
            int bottomPositionY;
            if (AlliedVariables.s_AlliedForm1Window!.PaletOn.IsChecked == true)
            {
                bottomPositionY = (int)AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualHeight - 0x28;
            }
            else
            {
                bottomPositionY = (int)AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualHeight;
            }

            for (int ebx = ebx0; ebx <= edi0; ebx++)
            {
                bool ebp15 = true;

                if (ebx == 0)
                {
                    Graphics_TPen_SetColor(esi, AlliedVariables.s_V0x005B6FF0);
                }
                else if (ebx % 0x05 == 0)
                {
                    Graphics_TPen_SetColor(esi, AlliedVariables.s_V0x005B6FF4);
                }
                else if (AlliedVariables.s_V0x00543D4C < 0x0A)
                {
                    ebp15 = false;
                }
                else
                {
                    Graphics_TPen_SetColor(esi, AlliedVariables.s_V0x005B6FF8);
                }

                if (!ebp15)
                {
                    continue;
                }

                int ebp08 = (int)Math.Round(ebx * AlliedVariables.s_V0x00543D4C + AlliedVariables.s_V0x005B6D68 + AlliedVariables.s_V0x005B7020);

                Graphics_TCanvas_MoveTo(esi, ebp08, 0);
                Graphics_TCanvas_LineTo(esi, ebp08, bottomPositionY);

                if (!AlliedVariables.s_ShowNumbersMapSetting)
                {
                    continue;
                }

                if (ebx % ebp10 == 0)
                {
                    string ebp14;

                    if (ebx < 0x0A)
                    {
                        ebp14 = " " + ebx.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        ebp14 = ebx.ToString(CultureInfo.InvariantCulture);
                    }

                    Graphics_TCanvas_TextOut(esi, ebp08 - 0x06, bottomPositionY - 0x14, ebp14);
                }
            }

            int ebx1 = (int)Math.Round(AlliedVariables.s_V0x005B6FD0);
            int edi1 = (int)Math.Round(AlliedVariables.s_V0x005B6FD8);

            for (int ebx = ebx1; ebx <= edi1; ebx++)
            {
                bool ebp15 = true;

                if (ebx == 0)
                {
                    Graphics_TPen_SetColor(esi, AlliedVariables.s_V0x005B6FF0);
                }
                else if (ebx % 0x05 == 0)
                {
                    Graphics_TPen_SetColor(esi, AlliedVariables.s_V0x005B6FF4);
                }
                else if (AlliedVariables.s_V0x00543D4C < 0x0A)
                {
                    ebp15 = false;
                }
                else
                {
                    Graphics_TPen_SetColor(esi, AlliedVariables.s_V0x005B6FF8);
                }

                if (!ebp15)
                {
                    continue;
                }

                int ebp08 = (int)Math.Round(AlliedVariables.s_V0x005B6D6C + AlliedVariables.s_V0x005B7028 + (ebx * AlliedVariables.s_V0x00543D4C));

                // upgrade
                if (ebp08 >= bottomPositionY)
                {
                    continue;
                }

                if (AlliedVariables.s_ShowNumbersMapSetting && ((int)eax0.ScrollBox1.ActualHeight - 0x14) <= ebp08)
                {
                    continue;
                }

                Graphics_TCanvas_MoveTo(esi, 0, ebp08);
                Graphics_TCanvas_LineTo(esi, (int)AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualWidth, ebp08);

                if (!AlliedVariables.s_ShowNumbersMapSetting)
                {
                    continue;
                }

                // upgrade
                if (ebp08 >= bottomPositionY - 0x14 / 2)
                {
                    continue;
                }

                if (ebx % ebp10 == 0)
                {
                    int ebp0C;

                    if (AlliedVariables.s_AlliedMapOrientation == MapOrientationEnum.XY)
                    {
                        ebp0C = ebx;
                    }
                    else
                    {
                        ebp0C = -ebx;
                    }

                    string ebp14;

                    if (ebp0C > -1)
                    {
                        ebp14 = " " + ebp0C.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        ebp14 = ebp0C.ToString(CultureInfo.InvariantCulture);
                    }

                    Graphics_TCanvas_TextOut(esi, 0, ebp08 - 0x06, ebp14);
                }
            }
        }

        if (AlliedVariables.s_AlliedForm1Window!.CurrOnly.IsChecked == true)
        {
            Graphics_TFont_SetColor(esi, 0x0000FFFF);
            Graphics_TCanvas_TextOut(esi, (int)AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualWidth / 2 - 0x54, 0x03, "[Current FG only]");
        }

        if (eax0.LockBtn.IsChecked == true)
        {
            Graphics_TFont_SetColor(esi, 0x0000FFFF);
            Graphics_TCanvas_TextOut(esi, (int)AlliedVariables.s_AlliedForm1Window!.ScrollBox1.ActualWidth / 2 + 0x05, 0x03, "[Start Pt 1 Locked]");
        }

        if (AlliedVariables.s_AlliedMapOrientation != MapOrientationEnum.XY)
        {
            AlliedVariables.s_V0x005B6FD0 = 0.0f - AlliedVariables.s_V0x005B6FD0;
            AlliedVariables.s_V0x005B6FD8 = 0.0f - AlliedVariables.s_V0x005B6FD8;
        }
    }

    // L0052D968
    public static void TForm1_Proc_0052D968(Form1Window eax0, TBitmap edx0)
    {
        TBitmap eax1 = Graphics_TBitmap_GetCanvas(edx0)!;
        Graphics_TBrush_SetStyle(eax1, 0x01);

        int eax2 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;

        if (eax2 < AlliedVariables.s_V0x005B7068)
        {
            AlliedVariables.s_V0x005B7068 = eax2;
        }

        int esi = AlliedVariables.s_V0x005B7064;

        for (int ebx0 = AlliedVariables.s_V0x005B7068; ebx0 >= esi; ebx0--)
        {
            if (ebx0 == AlliedVariables.s_V0x005B7050)
            {
                continue;
            }

            S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx0);

            if (eax3.m00147A == 0)
            {
                continue;
            }

            TBitmap eax = Graphics_TBitmap_GetCanvas(edx0)!;
            eax.RenderOpen();
            TForm1_Proc_0052DC20(eax0, ebx0, eax);
            eax.RenderClose();
        }

        if (AlliedVariables.s_AlliedForm1Window!.PaletOn.IsChecked == true)
        {
            Graphics_TBitmap_GetCanvas(edx0)!.Color = 0x00CC0020;
            TRect ebp14 = new(0, 0, 0x24F, 0x28);
            int ebx1 = (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight;
            TRect ebp24 = new(0, ebx1 - 0x28, 0x24F, ebx1);

            Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(edx0)!, ebp24, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC4!)!, ebp14);

            Graphics_TPen_SetStyle(Graphics_TBitmap_GetCanvas(edx0)!, 0);
            Graphics_TPen_SetColor(Graphics_TBitmap_GetCanvas(edx0)!, 0x00C0C0C0);
            Graphics_TCanvas_MoveTo(Graphics_TBitmap_GetCanvas(edx0)!, 0, (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight - 0x29);
            Graphics_TCanvas_LineTo(Graphics_TBitmap_GetCanvas(edx0)!, 0x250, (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight - 0x29);
            Graphics_TCanvas_LineTo(Graphics_TBitmap_GetCanvas(edx0)!, 0x250, (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight);
            Graphics_TFont_SetColor(Graphics_TBitmap_GetCanvas(edx0)!, 0x00C0C0C0);

            if (AlliedVariables.s_AlliedAplicationWidth == 0x280)
            {
                string ebp34_3 = " " + (AlliedVariables.s_V0x00543B18 + 1).ToString(CultureInfo.InvariantCulture);
                Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(edx0)!, 0x253, (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight - 0x19, ebp34_3);
            }
            else
            {
                string ebp34_1 = " Team " + (AlliedVariables.s_V0x00543B18 + 1).ToString(CultureInfo.InvariantCulture);
                Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(edx0)!, 0x253, (int)AlliedVariables.s_AlliedForm1Window!.PaintBox1.ActualHeight - 0x19, ebp34_1);
            }
        }
    }

    private static void TForm1_Proc_0052D348(Form1Window eax0, TBitmap edx0, PaintBoxControl ecx0)
    {
        TForm1_Proc_0052D348(eax0, edx0, ecx0.Bitmap!);
    }

    // L0052D348
    private static void TForm1_Proc_0052D348(Form1Window eax0, TBitmap edx0, TBitmap ecx0)
    {
        // todo
        //ecx0.Bitmap!.Color = 0x00CC0020;
        //TRect esp00 = eax0.GetClientRect();
        //TRect esp10 = eax0.GetClientRect();
        //Graphics_TCanvas_CopyRect(ecx0.Bitmap!, esp10, edx0, esp00);

        //ecx0.Bitmap!.Render(context =>
        //{
        //    //context.DrawImage(edx0.Source!, new Rect(0, 0, (int)ecx0.ActualWidth, (int)ecx0.ActualHeight));

        //    byte[] buffer = new byte[edx0.Source!.PixelWidth * 4 * edx0.Source!.PixelHeight];
        //    edx0.Source!.CopyPixels(buffer, edx0.Source!.PixelWidth * 4, 0);

        //    var source = BitmapFactory.New(edx0.Source!.PixelWidth, edx0.Source!.PixelHeight).FromByteArray(buffer);
        //    context.WriteableBitmap.Blit(new Rect(0, 0, (int)ecx0.ActualWidth, (int)ecx0.ActualHeight), source, new Rect(0, 0, edx0.Source!.PixelWidth, edx0.Source!.PixelHeight));
        //});

        ecx0.Render(context =>
        {
            byte[] buffer = new byte[edx0.Source!.PixelWidth * 4 * edx0.Source!.PixelHeight];
            edx0.Source!.CopyPixels(buffer, edx0.Source!.PixelWidth * 4, 0);

            var source = BitmapFactory.New(edx0.Source!.PixelWidth, edx0.Source!.PixelHeight).FromByteArray(buffer);
            context.WriteableBitmap.Blit(new Rect(0, 0, ecx0.Width, ecx0.Height), source, new Rect(0, 0, edx0.Source!.PixelWidth, edx0.Source!.PixelHeight));
        });
    }

    // L0052DC20
    public static void TForm1_Proc_0052DC20(Form1Window eax0, int edx0, TBitmap ecx0)
    {
        S0xFGObject esi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0);
        bool ebp41 = Unit_00513838_Proc_005208F8(edx0);
        double ebp40 = Unit_004B57A4_Proc_004B5844(esi.FlightGroupStruct.CraftId);

        if (!ebp41)
        {
            if (esi.FlightGroupStruct.PlayerNumber > 0)
            {
                Unit_00513838_Proc_0051FEF4(edx0);
            }
            else
            {
                Unit_00513838_Proc_00520198(edx0);
            }
        }

        double ebp38 = esi.m00147C[AlliedVariables.s_V0x005B705C].M000000[0] / 160.0f;

        bool edx1 = false;
        bool ebx0 = false;

        if (AlliedVariables.s_V0x005B6FE0 - ebp40 >= ebp38 || AlliedVariables.s_V0x005B6FE8 + ebp40 <= ebp38)
        {
            edx1 = false;
        }
        else
        {
            edx1 = true;
        }

        if (edx1)
        {
            ebp38 = esi.m00147C[AlliedVariables.s_V0x005B7058].M000000[0] / 160.0f;

            if (AlliedVariables.s_AlliedMapOrientation != MapOrientationEnum.XY)
            {
                if (AlliedVariables.s_V0x005B6FD0 + ebp40 <= ebp38 || AlliedVariables.s_V0x005B6FD8 - ebp40 >= ebp38)
                {
                    ebx0 = false;
                }
                else
                {
                    ebx0 = true;
                }
            }
            else
            {
                if (AlliedVariables.s_V0x005B6FD0 - ebp40 >= ebp38 || AlliedVariables.s_V0x005B6FD8 + ebp40 <= ebp38)
                {
                    ebx0 = false;
                }
                else
                {
                    ebx0 = true;
                }
            }
        }

        AlliedVariables.s_V0x005B7006 = edx1 && ebx0;

        if (AlliedVariables.s_V0x005B7006 || AlliedVariables.s_V0x005B7007)
        {
            AlliedVariables.s_V0x005B7000 = AlliedGetIffColor(esi.FlightGroupStruct.Iff, 0x01);
            AlliedVariables.s_V0x005B6FFC = AlliedGetIffColor(esi.FlightGroupStruct.Iff, 0);

            if (esi.FlightGroupStruct.Iff == 0)
            {
                AlliedVariables.s_V0x005B6FFC = GoalViewWindowImpl.TGoalViewForm_Proc_004B57A4(AlliedVariables.s_V0x005B6FFC, 0x50);
            }

            bool ebp23;

            if (eax0.Showdistances.IsChecked || eax0.Showtimes.IsChecked)
            {
                ebp23 = true;
            }
            else
            {
                ebp23 = false;
            }

            bool ebp21;

            if (!BtBitString((int)esi.FlightGroupStruct.CraftId, AlliedVariables.s_V0x00533C80))
            {
                ebp21 = true;
            }
            else
            {
                ebp21 = false;
            }

            if ((AlliedVariables.s_AlliedForm1Window!.AllWPS.IsChecked == true) || ((AlliedVariables.s_AlliedForm1Window!.CurrWPs.IsChecked == true) && (edx0 == AlliedVariables.s_V0x005B7050)))
            {
                AlliedVariables.s_V0x005B7070 = 0x0C;
            }
            else
            {
                AlliedVariables.s_V0x005B7070 = 0x01;
            }

            int ebp14 = 0;
            int ebp18 = 0;
            int ebp1C = 0;
            int ebp20 = 0;
            int ebp0C = 0;

            for (int ebx = AlliedVariables.s_V0x005B706C; ebx <= AlliedVariables.s_V0x005B7070; ebx++)
            {
                if (esi.IsWPEnabled[ebx - 1] != 0x01 && ebx != 0x01)
                {
                    continue;
                }

                bool ebp22;

                if (ebx > 0x01 && ebx <= 0x0C)
                {
                    ebp22 = true;
                }
                else
                {
                    ebp22 = false;
                }

                AlliedVariables.s_V0x005B6D60 = (int)Math.Round(MapWindowImpl.TMapForm_Proc_004F65C8(AlliedVariables.s_TMapForm_Instance!, 0, esi.m00147C[AlliedVariables.s_V0x005B705C].GetValue(ebx - 1)) + AlliedVariables.s_V0x005B6D68 + AlliedVariables.s_V0x005B7020);
                AlliedVariables.s_V0x005B6D64 = (int)Math.Round(MapWindowImpl.TMapForm_Proc_004F65C8(AlliedVariables.s_TMapForm_Instance!, 0x01, esi.m00147C[AlliedVariables.s_V0x005B7058].GetValue(ebx - 1)) + AlliedVariables.s_V0x005B6D6C + AlliedVariables.s_V0x005B7028);

                string ebp2C_1 = "  ";
                string ebp2C_0 = string.Empty;

                if (AlliedVariables.s_AlliedForm1Window!.NamesOn.IsChecked == true)
                {
                    switch (ebx)
                    {
                        case 0x01:
                        case 0x0F:
                        case 0x10:
                        case 0x11:
                        case 0x12:
                        case 0x13:
                        case 0x14:
                        case 0x15:
                        case 0x16:
                            if (AlliedVariables.s_AlliedForm1Window!.NamesOn.IsChecked == true)
                            {
                                ebp2C_1 += " " + System_LStrFromPCharLen(esi.FlightGroupStruct.Name, 0x14);
                            }

                            break;

                        case 0x02:
                        case 0x03:
                            ebp2C_1 += "Start " + ebx.ToString(CultureInfo.InvariantCulture);
                            break;

                        case 0x04:
                            ebp2C_1 += "Depart";
                            break;

                        case 0x0D:
                            ebp2C_1 += "RDVZ";
                            break;

                        case 0x0E:
                            ebp2C_1 += "HYP";
                            break;

                        default:
                            ebp2C_1 += "w " + (ebx - 0x04).ToString(CultureInfo.InvariantCulture);
                            break;
                    }
                }

                if (ebp22)
                {
                    if (AlliedVariables.s_AlliedForm1Window!.CurrOnly.IsChecked == false && AlliedVariables.s_AlliedForm1Window!.NamesOn.IsChecked == true)
                    {
                        ebp2C_1 += " " + System_LStrFromPCharLen(esi.FlightGroupStruct.Name, 0x14);
                    }

                    if (ebp23 && ebp21)
                    {
                        ebp0C = 0;

                        if (ebp41 || esi.FlightGroupStruct.PlayerNumber > 0)
                        {
                            if (ebx >= 0x05)
                            {
                                ebp0C = esi.m00147C[3].M000008[0];
                            }

                            if (ebx > 0x05)
                            {
                                ebp0C += esi.m00147C[3].M000008[1];
                            }
                        }
                        else
                        {
                            ebp0C = TForm1_Proc_0052AE38(AlliedVariables.s_AlliedForm1Window!, esi, 0x01, 0x06);
                        }

                        for (int ebp4C = 0x02; ebp4C <= ebx - 0x05; ebp4C++)
                        {
                            if (ebp4C < 4)
                            {
                                ebp0C += esi.m00147C[3].M000008[ebp4C];
                            }
                            else
                            {
                                ebp0C += esi.m00147C[3].M000010[ebp4C - 4];
                            }
                        }

                        if (ebx == 0x04)
                        {
                            for (int eax = 0x03; eax < 0x08; eax++)
                            {
                                if (eax < 4)
                                {
                                    ebp0C += esi.m00147C[3].M000000[eax];
                                }
                                else
                                {
                                    ebp0C += esi.m00147C[3].M000008[eax - 4];
                                }
                            }
                        }
                    }

                    if (AlliedVariables.s_AlliedForm1Window!.Showdistances.IsChecked && ebp21)
                    {
                        ebp2C_0 += Allied_FloatToText(0x02, 0x07, 0x02, ebp0C / 160.0f) + "km ";
                    }

                    if (AlliedVariables.s_AlliedForm1Window!.Showtimes.IsChecked && ebp21)
                    {
                        if (TForm1_Proc_0052AED4(AlliedVariables.s_AlliedForm1Window!, esi.FlightGroupStruct) == 0)
                        {
                            ebp2C_0 += "(Holding)";
                        }
                        else
                        {
                            int ebp6C = TForm1_Proc_0052AED4(AlliedVariables.s_AlliedForm1Window!, esi.FlightGroupStruct);
                            int ebp10 = (int)Math.Round((ebp0C / 160.0f) / (ebp6C * 0.0022094564717650002));
                            string ebp70 = Allied_TimeInSeconds_ToMinutesSecondsString(ebp10);
                            ebp2C_0 += "(" + ebp70 + ")";
                        }
                    }
                }

                if (AlliedVariables.s_AlliedForm1Window!.NamesOn.IsChecked == true)
                {
                    if (AlliedVariables.s_GhostMapChkSetting)
                    {
                        Graphics_TFont_SetColor(ecx0, 0x005C5C5C);
                    }
                    else
                    {
                        Graphics_TFont_SetColor(ecx0, AlliedVariables.s_V0x005B7000);
                    }

                    Graphics_TCanvas_TextOut(ecx0, AlliedVariables.s_V0x005B6D60, AlliedVariables.s_V0x005B6D64 - 0x06, ebp2C_1);
                }

                if (ebp23)
                {
                    if (ebx != 0x05 || ebp41 || esi.FlightGroupStruct.PlayerNumber > 0)
                    {
                        Graphics_TFont_SetColor(ecx0, AlliedVariables.s_V0x005B7000);
                        Graphics_TCanvas_TextOut(ecx0, AlliedVariables.s_V0x005B6D60 + 0x05, AlliedVariables.s_V0x005B6D64 + 0x07, ebp2C_0);
                    }
                }

                if (ebx == 0x01 || ebx > 0x0E)
                {
                    TForm1_Proc_0052FAF0(AlliedVariables.s_AlliedForm1Window!, edx0, ebx, ecx0, AlliedVariables.s_V0x005B6D64, AlliedVariables.s_V0x005B6D60);

                    ebp14 = AlliedVariables.s_V0x005B6D60;
                    ebp18 = AlliedVariables.s_V0x005B6D64;
                }
                else
                {
                    if (ebx == 0x04)
                    {
                        Graphics_TPen_SetColor(ecx0, 0x00C0C0C0);

                        ebp1C = AlliedVariables.s_V0x005B6D60;
                        ebp20 = AlliedVariables.s_V0x005B6D64;
                    }
                    else
                    {
                        Graphics_TPen_SetColor(ecx0, AlliedVariables.s_V0x005B7000);
                    }

                    TPoint[] ebp90 = new TPoint[4];
                    ebp90[0] = new((int)Math.Round((float)(AlliedVariables.s_V0x005B6D60 - 1)), (int)Math.Round((float)(AlliedVariables.s_V0x005B6D64 + 1)));
                    ebp90[1] = new((int)Math.Round((float)(AlliedVariables.s_V0x005B6D60 + 1)), (int)Math.Round((float)(AlliedVariables.s_V0x005B6D64 + 1)));
                    ebp90[2] = new((int)Math.Round((float)(AlliedVariables.s_V0x005B6D60 + 1)), (int)Math.Round((float)(AlliedVariables.s_V0x005B6D64 - 1)));
                    ebp90[3] = new((int)Math.Round((float)(AlliedVariables.s_V0x005B6D60 - 1)), (int)Math.Round((float)(AlliedVariables.s_V0x005B6D64 - 1)));
                    Graphics_TCanvas_Polygon_L00426B1C(ecx0, ebp90);

                    Graphics_TCanvas_MoveTo(ecx0, ebp14, ebp18);

                    if (ebx == 0x05)
                    {
                        if (ebp41 || esi.FlightGroupStruct.PlayerNumber > 0)
                        {
                            Graphics_TPen_SetStyle(ecx0, 0);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60, AlliedVariables.s_V0x005B6D64);

                            ebp14 = AlliedVariables.s_V0x005B6D60;
                            ebp18 = AlliedVariables.s_V0x005B6D64;
                        }
                        else if (AlliedVariables.s_V0x00543B0C == edx0)
                        {
                            Graphics_TPen_SetColor(ecx0, 0x00FFFFFF);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60, AlliedVariables.s_V0x005B6D64);

                            int x = (int)Math.Round(MapWindowImpl.TMapForm_Proc_004F65C8(AlliedVariables.s_TMapForm_Instance!, 0, AlliedVariables.s_V0x00543D30[AlliedVariables.s_V0x005B705C]) + AlliedVariables.s_V0x005B6D68 + AlliedVariables.s_V0x005B7020);
                            int y = (int)Math.Round(MapWindowImpl.TMapForm_Proc_004F65C8(AlliedVariables.s_TMapForm_Instance!, 0x01, AlliedVariables.s_V0x00543D30[AlliedVariables.s_V0x005B7058]) + AlliedVariables.s_V0x005B6D6C + AlliedVariables.s_V0x005B7028);
                            Graphics_TCanvas_LineTo(ecx0, x, y);
                        }
                    }
                    else if (ebx >= 0x05)
                    {
                        if (AlliedVariables.s_TMapForm_Instance!.WPsOn.IsChecked == true)
                        {
                            Graphics_TPen_SetStyle(ecx0, 0x02);

                            if (AlliedVariables.s_TMapForm_Instance!.WPsOn.IsChecked == true)
                            {
                                Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60, AlliedVariables.s_V0x005B6D64);
                            }

                            ebp14 = AlliedVariables.s_V0x005B6D60;
                            ebp18 = AlliedVariables.s_V0x005B6D64;

                            Graphics_TPen_SetStyle(ecx0, 0);
                        }
                    }
                }

                if (esi.FlightGroupStruct.PlayerNumber != 0)
                {
                    if (ebx == 0x01 || ebx > 0x0E)
                    {
                        Graphics_TFont_SetColor(ecx0, 0x00C0C0C0);
                        Graphics_TCanvas_TextOut(ecx0, AlliedVariables.s_V0x005B6D60 - 0x03, AlliedVariables.s_V0x005B6D64 + 0x06, esi.FlightGroupStruct.PlayerNumber.ToString(CultureInfo.InvariantCulture));
                    }
                }

                if (ebx == 0x01)
                {
                    if (edx0 == AlliedVariables.s_V0x005B7050 || Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0).m001479 != 0)
                    {
                        if (edx0 == AlliedVariables.s_V0x005B7050)
                        {
                            Graphics_TPen_SetColor(ecx0, 0x0000FFFF);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 - 0x05);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x04, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x04, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 - 0x04);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 + 0x05);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x03, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x05, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 + 0x04);
                        }
                        else
                        {
                            Graphics_TPen_SetColor(ecx0, 0x00C9C9C9);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 - 0x05);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x05, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x05, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 - 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 - 0x04);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 + 0x05);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x07, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 + 0x04, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_MoveTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x06, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 + 0x08);
                            Graphics_TCanvas_LineTo(ecx0, AlliedVariables.s_V0x005B6D60 - 0x08, AlliedVariables.s_V0x005B6D64 + 0x04);
                        }
                    }
                }
            }

            if (eax0.LinkHyppoint1.IsChecked)
            {
                if (eax0.AllWPS.IsChecked == true || ((eax0.CurrWPs.IsChecked == true) && edx0 == AlliedVariables.s_V0x005B7050))
                {
                    if (esi.IsWPEnabled[3] == 0x01)
                    {
                        Graphics_TPen_SetColor(ecx0, 0x00C0C0C0);
                        Graphics_TPen_SetStyle(ecx0, 0x01);
                        Graphics_TCanvas_LineTo(ecx0, ebp1C, ebp20);
                    }
                }
            }
        }
    }

    // L0052FAF0
    private static void TForm1_Proc_0052FAF0(Form1Window eax0, int edx0, int ecx0, TBitmap A4, int A8, int AC)
    {
        S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edx0);
        S0xTieFlightGroup ebpE42 = S0xTieFlightGroup.FromByteArray(eax1.FlightGroupStruct.ToByteArray());

        if (ebpE42.CraftId == CraftIdEnum._050_0_90_LightCalamariCruiser)
        {
            AlliedVariables.s_V0x005B6D28 = AlliedVariables.s_V0x00543D4C / 2574.25f;
        }
        else
        {
            AlliedVariables.s_V0x005B6D28 = AlliedVariables.s_V0x00543D4C / 1000.0f;
        }

        // todo
        if (AlliedVariables.s_LimitShrinkChkSetting)
        {
            var data = OptHelpers.GetCraftData((int)ebpE42.CraftId);
            AlliedVariables.s_V0x005B6D28 = Math.Max(AlliedVariables.s_V0x005B6D28, 10.0f / data.size);
        }

        if (AlliedVariables.s_V0x005B7006)
        {
            bool goto0052FBFA = false;

            if (!BtBitString((int)ebpE42.CraftId, AlliedVariables.s_V0x00533B20))
            {
                bool goto0052FBB1 = false;

                if (BtBitString((int)ebpE42.CraftId, AlliedVariables.s_V0x00533B40))
                {
                    goto0052FBB1 = true;
                }
                else if (BtBitString((int)ebpE42.CraftId, AlliedVariables.s_V0x00533B60))
                {
                    goto0052FBB1 = true;
                }

                if (!goto0052FBB1)
                {
                    goto0052FBFA = true;
                }
                else if (AlliedVariables.s_IconZoomEditSetting >= AlliedVariables.s_V0x00543D4C)
                {
                    goto0052FBFA = true;
                }
                else if (AlliedVariables.s_OnlyXYChkSetting && AlliedVariables.s_AlliedMapOrientation < (MapOrientationEnum)0x03)
                {
                    goto0052FBFA = true;
                }
            }

            if (!goto0052FBFA && !AlliedVariables.s_V0x005B7046)
            {
                AlliedVariables.s_V0x00543D38 = A4;
                A4.RenderOpen();
                MapWindowImpl.TMapForm__PROC_004F7E58(AlliedVariables.s_TMapForm_Instance!, edx0, A4);
                A4.RenderClose();
            }
            else if (AlliedVariables.s_V0x005B70A8 != 0 && ebpE42.CraftId > CraftIdEnum._000__1_0)
            {
                CraftIdEnum al = ebpE42.CraftId;
                int edx = (int)al - 1;
                int ecx = (int)((uint)edx & 0x8000000F);

                if (ecx < 0)
                {
                    ecx = (int)((uint)(ecx - 1) | 0xFFFFFFF0) + 1;
                }

                int ebp04 = ecx * 0x11;

                if (edx < 0)
                {
                    edx += 0x0F;
                }

                int esi = edx / 0x10 * 0x15;

                if (al == CraftIdEnum._086_1_11_AsteroidHR1)
                {
                    TBitmap eax = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005B7094)!;
                    Graphics_TBrush_SetColor(eax, 0x009D9D9D);
                }
                else
                {
                    uint color = BriefingWindowImpl.TBrfForm_GetIffColor(AlliedVariables.s_TBrfForm_Instance!, ebpE42.Iff);
                    TBitmap eax = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005B7094)!;
                    Graphics_TBrush_SetColor(eax, color);
                }

                Graphics_TCanvas_FillRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005B7094)!, eax0.GetClientRect());
                TBitmap eax2 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005B7094)!;
                eax2.Color = 0x008800C6;
                AlliedVariables.s_V0x005B7094!.RenderOpen();
                Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005B7094)!, AlliedVariables.s_V0x005B7098, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00543D40)!, new TRect(ebp04, esi, ebp04 + 0x10, esi + 0x14));
                AlliedVariables.s_V0x005B7094!.RenderClose();
                A4.Color = 0x00EE0086;
                TRect ebpE54 = new(
                    (int)Math.Round((float)(AlliedVariables.s_V0x005B6D60 - 0x08)),
                    (int)Math.Round((float)(AlliedVariables.s_V0x005B6D64 - 0x0A)),
                    (int)Math.Round((float)(AlliedVariables.s_V0x005B6D60 + 0x08)),
                    (int)Math.Round((float)(AlliedVariables.s_V0x005B6D64 + 0x0A)));
                Graphics_TCanvas_CopyRect(A4, ebpE54, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005B7094)!, AlliedVariables.s_V0x005B7098);
            }

            Graphics_TBrush_SetStyle(A4, 0x01);
        }
    }

    // L00526288
    public static string TForm1_Proc_00526288(Form1Window eax0, S0xTieFlightGroupOrder edx0)
    {
        S0xTieFlightGroupOrder ebp98 = S0xTieFlightGroupOrder.FromByteArray(edx0.ToByteArray());
        string ebp04;

        switch (ebp98.OrderId)
        {
            case TieOrderIdEnum._07_CapFree:
                ebp04 = "Attack";
                break;

            case TieOrderIdEnum._11_Disable:
                ebp04 = "Disable";
                break;

            default:
                ebp04 = AlliedVariables.s_Strings_Orders.GetText((int)ebp98.OrderId);
                break;
        }

        switch (ebp98.OrderId)
        {
            case TieOrderIdEnum._02_Form:
            case TieOrderIdEnum._21_StarshipForm:
                ebp04 += ", " + ebp98.Var0.ToString(CultureInfo.InvariantCulture) + " time";

                if (ebp98.Var0 > 0x01)
                {
                    ebp04 += "s";
                }

                break;

            case TieOrderIdEnum._50_Hyperspace:
                ebp04 += " #" + (ebp98.Var0 + 1).ToString(CultureInfo.InvariantCulture);
                break;
        }

        ebp04 += ":";

        if (ebp98.OrderId == TieOrderIdEnum._00_Stationary)
        {
            ebp04 = "---";
        }
        else
        {
            string ebpB0_3 = Unit_00513838_Proc_005175C0(ebp98.PrimaryTarget.ClassA, ebp98.PrimaryTarget.ParameterA);
            ebp04 += "  " + ebpB0_3;

            if (ebp98.PrimaryTarget.ClassB != TieClassEnum.None)
            {
                if (ebp98.PrimaryTarget.Operator != 0)
                {
                    ebp04 += " or";
                }
                else
                {
                    ebp04 += " only if";
                }
            }

            string ebpB0_2 = Unit_00513838_Proc_005175C0(ebp98.PrimaryTarget.ClassB, ebp98.PrimaryTarget.ParameterB);
            ebp04 += " " + ebpB0_2;
        }

        if (ebp98.SecondaryTarget.ClassA != TieClassEnum.None)
        {
            string ebpB0_1 = Unit_00513838_Proc_005175C0(ebp98.SecondaryTarget.ClassA, ebp98.SecondaryTarget.ParameterA);
            ebp04 += "  /  " + ebpB0_1;

            if (ebp98.SecondaryTarget.ClassB != TieClassEnum.None)
            {
                if (ebp98.SecondaryTarget.Operator != 0)
                {
                    ebp04 += " or";
                }
                else
                {
                    ebp04 += " only if";
                }
            }

            string ebpB0_0 = Unit_00513838_Proc_005175C0(ebp98.SecondaryTarget.ClassB, ebp98.SecondaryTarget.ParameterB);
            ebp04 += " " + ebpB0_0;
        }

        return ebp04;
    }

    // L00525ECC
    public static string TForm1_GetCraftString_NameAndShort(Form1Window TForm1, S0xTieFlightGroup edx0)
    {
        S0xTieFlightGroup ebpE3E = S0xTieFlightGroup.FromByteArray(edx0.ToByteArray());
        string ebpE4C_2 = ebpE3E.CraftsCount.ToString(CultureInfo.InvariantCulture);
        string ebpE4C_1 = AlliedGetCraftShortString(ebpE3E.CraftId);
        string ebpE4C_0 = System_LStrFromPCharLen(ebpE3E.Name, 0x14);
        string ecx0 = "[" + ebpE4C_2 + "] " + ebpE4C_1 + " " + ebpE4C_0;
        return ecx0;
    }

    // L0052F414
    public static void TForm1_Proc_0052F414(Form1Window eax0, bool edx0)
    {
        if (edx0 == AlliedVariables.s_TDatapad_Instance!.M000057())
        {
            return;
        }

        if (edx0 == eax0.ShowDatapad.IsChecked)
        {
            return;
        }

        ComCtrls_TToolButton_SetDown(eax0.ShowDatapad, edx0);
        TForm1_ShowDatapadClick(eax0, eax0.ShowDatapad);

        if (edx0)
        {
            AlliedVariables.s_TDatapad_Instance!.Update();
        }
    }

    // L0052BD6C
    public static void TForm1_Proc_0052BD6C(Form1Window eax0, Control edx0, S0x005342B0_00000C ecx0)
    {
        int ebx;

        ebx = ecx0.Width;

        if (ebx > TApplication_GetWidth() - 0x14)
        {
            ebx = TApplication_GetWidth() - 0x14;
        }

        Controls_TControl_SetWidth(edx0, ebx);

        ebx = ecx0.Top;

        if (ebx < 0x05 || ebx > TApplication_GetHeight() - 0x14)
        {
            ebx = 0x05;
        }

        Controls_TControl_SetTop(edx0, ebx);

        ebx = ecx0.Height;

        if (ebx > TApplication_GetHeight() - edx0.GetTop())
        {
            ebx = TApplication_GetHeight() - edx0.GetTop();
        }

        Controls_TControl_SetHeight(edx0, ebx);

        ebx = ecx0.Left;

        if (ebx > TApplication_GetWidth() - (int)edx0.ActualWidth)
        {
            ebx = TApplication_GetWidth() - (int)edx0.ActualWidth;
        }

        Controls_TControl_SetLeft(edx0, ebx);
    }

    // L0052F464
    public static void TForm1_Proc_0052F464(Form1Window eax0, bool edx0)
    {
        if (edx0 == eax0.ShowOrderSel.IsChecked)
        {
            return;
        }

        ComCtrls_TToolButton_SetDown(eax0.ShowOrderSel, edx0);
        TForm1_ShowOrderSelClick(eax0, eax0.ShowOrderSel);

        if (edx0)
        {
            AlliedVariables.s_TOrderSel_Instance!.Update();
        }
    }

    // L0052CE00
    private static void TForm1_ScrollBox1Resize(Form1Window Form1, object? Sender)
    {
        Controls_TControl_SetAlign(Form1.PaintBox1, TAlignEnum.Client);
        MapWindowImpl.TMapForm_Proc_004F8A94(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052EAF4
    private static void TForm1_OverallPagesResize(Form1Window Form1, object? Sender)
    {
        Controls_TControl_SetAlign(Form1.ScrollBox1, TAlignEnum.Client);
        TabControl eax1 = Form1.OverallPages;
        AlliedVariables.s_V0x005B7088[Convert.ToInt32(eax1.GetActivePage().Tag)] = (int)eax1.ActualWidth;
    }

    // L0052D100
    private static void TForm1_NamesOnClick(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052D298
    private static void TForm1_XYClick(Form1Window Form1, object? Sender)
    {
        MapWindowImpl.TMapForm__PROC_004F6F68(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052AE38
    public static short TForm1_Proc_0052AE38(Form1Window eax0, S0xFGObject edx0, byte ecx0, byte A4)
    {
        int ebp04 = Math.Abs(edx0.m00147C[0].GetValue(ecx0 - 0x01) - edx0.m00147C[0].GetValue(A4 - 0x01));
        int ebp08 = Math.Abs(edx0.m00147C[1].GetValue(ecx0 - 0x01) - edx0.m00147C[1].GetValue(A4 - 0x01));
        int ebx = edx0.m00147C[2].GetValue(ecx0 - 0x01) - edx0.m00147C[2].GetValue(A4 - 0x01);

        short eax = (short)Math.Round(Math.Sqrt(ebp04 * ebp04 + ebp08 * ebp08 + ebx * ebx));
        return eax;
    }

    // L0052AED4
    private static short TForm1_Proc_0052AED4(Form1Window eax0, S0xTieFlightGroup edx0)
    {
        short esi = 0;
        bool bl = true;

        if (edx0.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].OrderId == TieOrderIdEnum._00_Stationary)
        {
            esi = 0;
            bl = false;
        }
        else
        {
            if (edx0.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Throttle == 0x0A)
            {
                if (edx0.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SpeedMph != 0)
                {
                    esi = edx0.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SpeedMph;
                    bl = false;
                }
            }
        }

        if (bl)
        {
            if (AlliedVariables.s_Strings_Speeds.GetCount() > 0)
            {
                int ebpE44 = int.Parse(AlliedVariables.s_Strings_Speeds.GetText((int)edx0.CraftId), CultureInfo.InvariantCulture);

                esi = (short)Math.Round((ebpE44 * edx0.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Throttle) / 10.0f);
            }
        }

        return esi;
    }

    // L0052F6EC
    private static void TForm1_XYView1Click(Form1Window Form1, MenuItem Sender)
    {
        switch ((MapOrientationEnum)(Convert.ToInt32(Sender.Tag) - 1))
        {
            case MapOrientationEnum.XY:
                ComCtrls_TToolButton_SetDown(Form1.XY, true);
                break;

            case MapOrientationEnum.XZ:
                ComCtrls_TToolButton_SetDown(Form1.XZ, true);
                break;

            case MapOrientationEnum.YZ:
                ComCtrls_TToolButton_SetDown(Form1.YZ, true);
                break;
        }

        MapWindowImpl.TMapForm__PROC_004F6F68(AlliedVariables.s_TMapForm_Instance!);
        Menus_TMenuItem_SetChecked(Sender, true);
    }

    // L00529808
    //private static void TForm1_PrefButClick(Form1Window Form1, object? Sender)
    //{
    //    try
    //    {
    //        Unit_00507130_Proc_005077D4(true);
    //    }
    //    catch
    //    {
    //        MessageBox_ShowInformation("WHATT??");
    //    }
    //}

    // L0052F3C0
    private static void TForm1_Preferences1Click(Form1Window Form1, object? Sender)
    {
        Unit_00507130_Proc_005077D4(true);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L00529744
    private static void TForm1_ErrBtnClick(Form1Window Form1, object? Sender)
    {
        ErrorBoxImpl.TErrForm_L00510F18(AlliedVariables.s_TErrForm_Instance, true);
    }

    // L005302D8
    private static void TForm1_GoalView1Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_TGoalViewForm_Instance = MainImpl.CreateGoalViewWindow();
        AlliedVariables.s_TGoalViewForm_Instance.Owner = Form1;
        AlliedVariables.s_TGoalViewForm_Instance.ShowDialog();
        AlliedVariables.s_TGoalViewForm_Instance = null;
    }

    // L0052D0F8
    private static void TForm1_PaletOnClick(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052D0D8
    private static void TForm1_TeamDropClick(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x00543B18 += 1;

        if (AlliedVariables.s_V0x00543B18 > 0x09)
        {
            AlliedVariables.s_V0x00543B18 = 0;
        }

        Unit_00513838_Proc_0051F42C();
    }

    // L0052F9F0
    private static void TForm1_TeamMenuPopup(Form1Window Form1, object? Sender)
    {
        TForm1_TeamMenuPopup(Form1.TeamDrop);
        TForm1_TeamMenuPopup(Form1.TabSheet14.ContextMenu);
    }

    // L0052F9F0
    private static void TForm1_TeamMenuPopup(ItemsControl menu)
    {
        for (int ebx = 0; ebx < 0x0A; ebx++)
        {
            string ebp04 = AlliedVariables.s_Strings_Teams.GetText(ebx);
            Menus_TMenuItem_SetCaption(Menus_TMenuItem_GetItem(menu, ebx), ebp04);
            Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(menu, ebx), false);
        }

        Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(menu, AlliedVariables.s_V0x00543B18), true);
    }

    // L0052FA94
    private static void TForm1_N11Click(Form1Window Form1, MenuItem Sender)
    {
        AlliedVariables.s_V0x00543B18 = Menus_TMenuItem_GetMenuIndex(Sender);
        Unit_00513838_Proc_0051F42C();
    }

    // L0052CD40
    private static void TForm1_PaintBox1DblClick(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_DblClickLinkRadioIndexSetting != 0 && AlliedVariables.s_V0x005B7005 == 0)
        {
            TForm1_FitBattleBtnClick(Form1, Form1.FitBattleBtn);
            return;
        }

        ScrollBar eax = AlliedVariables.s_AlliedForm1Window!.ZoomBar;
        int edx = (int)eax.Value;

        if (edx > 0x3E8)
        {
            StdCtrls_TScrollBar_SetPosition(eax, 0x1F4);
        }
        else if (edx > 0x1F3)
        {
            StdCtrls_TScrollBar_SetPosition(eax, 0xC8);
        }
        else if (edx > 0xC7)
        {
            StdCtrls_TScrollBar_SetPosition(eax, 0x40);
        }
        else if (edx > 0x24)
        {
            StdCtrls_TScrollBar_SetPosition(eax, 0x20);
        }
        else if (edx > 0x14)
        {
            StdCtrls_TScrollBar_SetPosition(eax, 0x10);
        }
        else if (edx > 0x0A)
        {
            StdCtrls_TScrollBar_SetPosition(eax, 0x08);
        }
        else
        {
            StdCtrls_TScrollBar_SetPosition(eax, 0x04);
        }
    }

    // L0052C72C
    private static void TForm1_PaintBox1MouseMove(Form1Window Form1, object? Sender, TShiftState ecx0, int A4, int A8)
    {
        int ebp20 = A8 - AlliedVariables.s_V0x005B707C;
        int ebp24 = A4 - AlliedVariables.s_V0x005B7080;

        if (AlliedVariables.s_V0x005B7008 != 0 && ((TShiftState)16 == ecx0 || (TShiftState)17 == ecx0 || (TShiftState)20 == ecx0 || (TShiftState)21 == ecx0))
        {
            StdCtrls_TScrollBar_SetPosition(Form1.XScroll, (int)Form1.XScroll.Value - ebp20);
            StdCtrls_TScrollBar_SetPosition(Form1.YScroll, (int)Form1.YScroll.Value - ebp24);
            TForm1_Proc_0052D3F8(Form1);
        }
        else if ((TShiftState)20 != ecx0)
        {
            double ebp10 = (long)Math.Round(MapWindowImpl.TMapForm_Proc_004F6640(AlliedVariables.s_TMapForm_Instance!, 0, A8)) / AlliedVariables.s_V0x00543D44 * AlliedVariables.s_V0x00543D44;
            double ebp18 = (long)Math.Round(MapWindowImpl.TMapForm_Proc_004F6640(AlliedVariables.s_TMapForm_Instance!, 0x01, A4)) / AlliedVariables.s_V0x00543D48 * AlliedVariables.s_V0x00543D48;

            if (AlliedVariables.s_V0x005B7040 != 0 && (AlliedVariables.s_V0x005B7054 != 0x01 || Form1.LockBtn.IsChecked != true))
            {
                if (Form1.LockX.IsChecked != true)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
                    AlliedVariables.s_V0x005B7084 = eax1.m00147C[AlliedVariables.s_V0x005B705C].M000000[AlliedVariables.s_V0x005B7054 - 1] - (int)Math.Round(ebp10);
                    TForm1_Proc_005303D8(Form1, ecx0, AlliedVariables.s_V0x005B705C, (int)Math.Round(ebp10), AlliedVariables.s_V0x005B7054, AlliedVariables.s_V0x005B7050);
                }

                if (Form1.LockY.IsChecked != true)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
                    AlliedVariables.s_V0x005B7084 = eax1.m00147C[AlliedVariables.s_V0x005B7058].M000000[AlliedVariables.s_V0x005B7054 - 1] - (int)Math.Round(ebp18);
                    TForm1_Proc_005303D8(Form1, ecx0, AlliedVariables.s_V0x005B7058, (int)Math.Round(ebp18), AlliedVariables.s_V0x005B7054, AlliedVariables.s_V0x005B7050);
                }

                Unit_00513838_Proc_005146A4();
                TForm1_Proc_0052D384(Form1);
            }
            else if (((TShiftState)9 == ecx0 || (TShiftState)13 == ecx0) && AlliedVariables.s_V0x005B7054 == 0x01 && AlliedVariables.s_V0x005B7041 != 0)
            {
                if (Form1.LockX.IsChecked != true)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
                    AlliedVariables.s_V0x005B7084 = eax1.m00147C[AlliedVariables.s_V0x005B705C].M000000[AlliedVariables.s_V0x005B7054 - 1] - (int)Math.Round(ebp10);

                    int ebp28 = AlliedVariables.s_FlightGroupObjectsList.Count;

                    for (int ebp1C = 0; ebp1C < ebp28; ebp1C++)
                    {
                        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp1C);

                        if (eax2.m001479 == 0)
                        {
                            continue;
                        }

                        if (!Unit_00513838_Proc_005208F8(ebp1C))
                        {
                            continue;
                        }

                        TForm1_Proc_005303D8(Form1, ecx0, AlliedVariables.s_V0x005B705C, (int)Math.Round(ebp10), AlliedVariables.s_V0x005B7054, ebp1C);
                    }
                }

                if (Form1.LockY.IsChecked != true)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
                    AlliedVariables.s_V0x005B7084 = eax1.m00147C[AlliedVariables.s_V0x005B7058].M000000[AlliedVariables.s_V0x005B7054 - 1] - (int)Math.Round(ebp18);

                    int ebp28 = AlliedVariables.s_FlightGroupObjectsList.Count;

                    for (int ebp1C = 0; ebp1C < ebp28; ebp1C++)
                    {
                        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp1C);

                        if (eax2.m001479 == 0)
                        {
                            continue;
                        }

                        if (!Unit_00513838_Proc_005208F8(ebp1C))
                        {
                            continue;
                        }

                        TForm1_Proc_005303D8(Form1, ecx0, AlliedVariables.s_V0x005B7058, (int)Math.Round(ebp18), AlliedVariables.s_V0x005B7054, ebp1C);
                    }
                }

                Unit_00513838_Proc_005146A4();
                TForm1_Proc_0052D3F8(Form1);
            }

            if (AlliedVariables.s_V0x005B705C == 0)
            {
                string ebp54_7 = "X: " + MapWindowImpl.TMapForm_Proc_004F66F0(AlliedVariables.s_TMapForm_Instance!, ebp10);
                Controls_TControl_SetText(Form1.Label1, ebp54_7);
            }
            else
            {
                string ebp54_5 = "Y: " + MapWindowImpl.TMapForm_Proc_004F66F0(AlliedVariables.s_TMapForm_Instance!, ebp10);
                Controls_TControl_SetText(Form1.Label1, ebp54_5);
            }

            if (AlliedVariables.s_V0x005B7058 == 0x01)
            {
                string ebp54_3 = "Y: " + MapWindowImpl.TMapForm_Proc_004F66F0(AlliedVariables.s_TMapForm_Instance!, ebp18);
                Controls_TControl_SetText(Form1.Label2, ebp54_3);
            }
            else
            {
                string ebp54_1 = "Z: " + MapWindowImpl.TMapForm_Proc_004F66F0(AlliedVariables.s_TMapForm_Instance!, ebp18);
                Controls_TControl_SetText(Form1.Label2, ebp54_1);
            }
        }

        Form1.Label1.Update();
        Form1.Label2.Update();

        AlliedVariables.s_V0x005B707C = A8;
        AlliedVariables.s_V0x005B7080 = A4;
    }

    // L0052C410
    private static void TForm1_PaintBox1MouseDown(Form1Window Form1, object? Sender, int A4, int A8, TShiftState AC)
    {
        TApplication_L00468A40(Form1, Form1.ScrollBox1);

        byte ebp02 = 0;
        byte ebp01 = 0x01;
        byte ebp03 = 0;
        AlliedVariables.s_V0x005B7040 = 0;
        AlliedVariables.s_V0x005B7041 = 0;

        if ((TShiftState)24 == AC || (TShiftState)32 == AC)
        {
            if (AlliedVariables.s_ResChkSetting)
            {
                TForm1_Proc_0052F414(Form1, !AlliedVariables.s_TDatapad_Instance!.M000057());
            }

            AlliedVariables.s_V0x005B7008 = 0;
        }
        else if (Form1.PaintBox1.ActualHeight - 0x28 < A4 && A8 < 0x24F && Form1.PaletOn.IsChecked == true)
        {
            TForm1_Proc_0052EBA8(Form1, A8, A4);
        }
        else if (TForm1_Proc_0052EE50(Form1, A8, A4, AC) != 0)
        {
            if (AlliedVariables.s_V0x005B7054 == 0x01 && Form1.LockBtn.IsChecked == true)
            {
                TForm1_Proc_0052D3F8(Form1);
                Graphics_TBrush_SetStyle(Form1.PaintBox1.Bitmap!, 0x01);
                Graphics_TFont_SetColor(Form1.PaintBox1.Bitmap!, 0x0000FFFF);
                Graphics_TCanvas_TextOut(Form1.PaintBox1.Bitmap!, A8 + 0x0F, A4 - 0x0A, "Locked!");
            }
            else
            {
                if ((TShiftState)16 == AC || (TShiftState)17 == AC || (TShiftState)20 == AC)
                {
                    AlliedVariables.s_V0x005B7008 = 0x01;
                    //TApplication_SetCursor(AlliedVariables.s_Allied_TScreen->Application, 0xFFEB);
                }
                else if ((TShiftState)9 == AC || (TShiftState)13 == AC)
                {
                    AlliedVariables.s_V0x005B7041 = 0x01;
                }
                else
                {
                    AlliedVariables.s_V0x005B7040 = 0x01;
                }

                ebp02 = 0x01;
                ebp03 = 0x01;

                if (AlliedVariables.s_V0x00543B0C != AlliedVariables.s_V0x005B7050 && AlliedVariables.s_V0x00543B54 != 0)
                {
                    AlliedVariables.s_FlightGroupObjectsList[AlliedVariables.s_V0x00543B0C] = AlliedVariables.s_V0x005AFE90.Clone();
                    Unit_00513838_Proc_005146A4();
                }
            }
        }
        else
        {
            if ((TShiftState)16 == AC || (TShiftState)17 == AC || (TShiftState)20 == AC || (TShiftState)21 == AC)
            {
                AlliedVariables.s_V0x005B7074 = 0;
                AlliedVariables.s_V0x005B7008 = 0x01;
                //TApplication_SetCursor(AlliedVariables.s_Allied_TScreenPtr->Application, 0x0005);
            }
            else if ((ebp01 != 0 && (TShiftState)8 == AC) || (TShiftState)9 == AC || (TShiftState)12 == AC || (TShiftState)13 == AC)
            {
                AlliedVariables.s_V0x005B7074 = 0;
                AlliedVariables.s_V0x005B7009 = 0x01;
                AlliedVariables.s_TMapForm_Instance!.EnableTimer1();
            }
        }

        if ((TShiftState)16 == AC)
        {
        }

        if (ebp02 != 0)
        {
            bool dl;

            if ((TShiftState)20 == AC || (TShiftState)17 == AC || (TShiftState)9 == AC || (TShiftState)13 == AC)
            {
                dl = false;
            }
            else
            {
                dl = true;
            }

            TForm1_Proc_0052ED7C(Form1, dl);
        }

        if (ebp03 != 0)
        {
            AlliedVariables.s_V0x005AFCC0!.RenderOpen();
            TForm1_Proc_0052D4CC(Form1, AlliedVariables.s_V0x005AFCC0!);
            TForm1_Proc_0052D968(Form1, AlliedVariables.s_V0x005AFCC0!);
            AlliedVariables.s_V0x005AFCC0!.RenderClose();
            TForm1_Proc_0052D384(Form1);
        }

        Form1.SelectionBox.SelectedIndex = AlliedVariables.s_V0x005B7050;
    }

    // L0052CCD8
    private static void TForm1_PaintBox1MouseUp(Form1Window Form1, object? Sender, int A4, int A8, TShiftState AC)
    {
        AlliedVariables.s_V0x005B7040 = 0;
        AlliedVariables.s_V0x005B7041 = 0;
        AlliedVariables.s_V0x005B7009 = 0;
        AlliedVariables.s_TMapForm_Instance!.DisableTimer1();
        AlliedVariables.s_V0x005B7008 = 0;
        //TApplication_SetCursor(s_Allied_TScreenPtr->Application, 0);
        TForm1_Proc_0052D3F8(Form1);

        if (AlliedVariables.s_V0x005B6D18 != 0)
        {
            WaypointsWindowImpl.TWPform_PROC_0050BA2C(AlliedVariables.s_TWPform_Instance!);
        }
    }

    // L0052EE50
    private static byte TForm1_Proc_0052EE50(Form1Window Form1, int edx0, int ecx0, TShiftState A4)
    {
        byte ebp0D = 0;

        int ebp14 = 0;

        if (AlliedVariables.s_V0x00543D4C < 0xC8)
        {
            ebp14 = 0x5DC / AlliedVariables.s_V0x00543D4C;
        }
        else if (AlliedVariables.s_V0x00543D4C < 0x1F4)
        {
            ebp14 = 0x4B0 / AlliedVariables.s_V0x00543D4C;
        }
        else if (AlliedVariables.s_V0x00543D4C < 0x5DC)
        {
            ebp14 = 0x960 / AlliedVariables.s_V0x00543D4C;
        }
        else
        {
            ebp14 = 0xE10 / AlliedVariables.s_V0x00543D4C;
        }

        bool bl = false;
        int edi = 0;
        int esi = AlliedVariables.s_V0x005B7064 - 1;

        while (true)
        {
            esi++;

            S0xFGObject ebp18 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

            edi = AlliedVariables.s_V0x005B706C - 1;

            while (true)
            {
                edi++;

                if (Form1.AllWPS.IsChecked == true || (Form1.CurrWPs.IsChecked == true && esi == AlliedVariables.s_V0x005B7050))
                {
                    AlliedVariables.s_V0x005B7070 = 0x0C;
                }
                else
                {
                    AlliedVariables.s_V0x005B7070 = 0x01;
                }

                bl = false;

                if (ebp18.IsWPEnabled[edi - 1] == 0x01 || edi == 0x01)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);

                    if (eax1.m00147A != 0)
                    {
                        if (ebp14 > Math.Abs(MapWindowImpl.TMapForm_Proc_004F6640(AlliedVariables.s_TMapForm_Instance!, 0, edx0) - ebp18.m00147C[AlliedVariables.s_V0x005B705C].GetValue(edi - 1))
                            && ebp14 > Math.Abs(MapWindowImpl.TMapForm_Proc_004F6640(AlliedVariables.s_TMapForm_Instance!, 0x01, ecx0) - ebp18.m00147C[AlliedVariables.s_V0x005B7058].GetValue(edi - 1)))
                        {
                            if (edi != 0x01 || (
                                (TShiftState)16 != A4
                                && (TShiftState)17 != A4
                                && (TShiftState)20 != A4
                                && (TShiftState)21 != A4))
                            {
                                bl = true;
                            }
                        }
                    }
                }

                if (edi >= AlliedVariables.s_V0x005B7070 || bl)
                {
                    break;
                }
            }

            if (esi >= AlliedVariables.s_V0x005B7068 || bl)
            {
                break;
            }
        }

        if (bl)
        {
            if (((TShiftState)16 == A4 && edi == 0x01)
            || (TShiftState)8 == A4
            || ((TShiftState)12 == A4 && edi == 0x01)
            || ((TShiftState)20 == A4 && edi == 0x01)
            || ((TShiftState)17 == A4 && edi == 0x01)
            || ((TShiftState)9 == A4 && edi == 0x01)
            || ((TShiftState)13 == A4 && edi == 0x01))
            {
                AlliedVariables.s_V0x005B7050 = esi;
                AlliedVariables.s_V0x005B7054 = edi;
                ebp0D = 0x01;

                if ((TShiftState)12 == A4)
                {
                    edi--;

                    if (edi == 0)
                    {
                        if (Form1.AllWPS.IsChecked != true && Form1.CurrWPs.IsChecked != true)
                        {
                            ComCtrls_TToolButton_SetDown(Form1.CurrWPs, true);
                        }
                    }
                }
            }
            else
            {
                ebp0D = 0;
            }
        }

        return ebp0D;
    }

    // L0052ED7C
    private static void TForm1_Proc_0052ED7C(Form1Window Form1, bool edx0)
    {
        if (AlliedVariables.s_V0x00543B0C != AlliedVariables.s_V0x005B7050)
        {
            AlliedVariables.s_V0x00543B0C = AlliedVariables.s_V0x005B7050;
            Form1.ShipList.SelectedIndex = AlliedVariables.s_V0x00543B0C;
            Unit_00513838_Proc_0051950C(true);
        }

        if (edx0)
        {
            int esi = AlliedVariables.s_FlightGroupObjectsList.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                StdCtrls_TCustomListBox_SetSelected(Form1.ShipList, ebx, false);

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.m001479 = 0;
            }
        }

        StdCtrls_TCustomListBox_SetSelected(Form1.ShipList, AlliedVariables.s_V0x00543B0C, true);

        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
        eax2.m001479 = 0x01;
    }

    // L005303D8
    private static void TForm1_Proc_005303D8(Form1Window Form1, TShiftState edx0, int ecx0, int A4, int A8, int AC)
    {
        S0xFGObject eax1;

        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
        byte ebp40_DepartureDelayMinutes = eax1.FlightGroupStruct.DepartureDelayMinutes;
        byte ebp40_DepartureDelaySeconds = eax1.FlightGroupStruct.DepartureDelaySeconds;
        byte ebp40_AbortCondition = eax1.FlightGroupStruct.AbortCondition;
        byte ebp40_ArrivalRandomDelaySeconds = eax1.FlightGroupStruct.ArrivalRandomDelaySeconds;
        short ebp40_CurStartFg = eax1.FlightGroupStruct.CurStartFg;
        byte ebp40_StartFg = eax1.FlightGroupStruct.StartFg;
        byte ebp40_StartFgUsed = eax1.FlightGroupStruct.StartFgUsed;
        byte ebp40_PrimaryStopFg = eax1.FlightGroupStruct.PrimaryStopFg;
        byte ebp40_PrimaryStopFgUsed = eax1.FlightGroupStruct.PrimaryStopFgUsed;
        byte ebp40_SecondaryStopFg = eax1.FlightGroupStruct.SecondaryStopFg;
        byte ebp40_SecondaryStopFgUsed = eax1.FlightGroupStruct.SecondaryStopFgUsed;
        byte ebp40_CaptureFg = eax1.FlightGroupStruct.CaptureFg;
        byte ebp40_CaptureFgUsed = eax1.FlightGroupStruct.CaptureFgUsed;

        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
        TieOrderIdEnum ebp54_OrderId = eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].OrderId;
        byte ebp54_Throttle = eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Throttle;
        byte ebp54_Parameters0 = eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var0;
        byte ebp54_Parameters1 = eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var1;
        short ebp54_Parameters2 = eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var2;
        S0xTieFlightGroupOrderSecondaryTarget ebp54_SecondaryTarget = S0xTieFlightGroupOrderSecondaryTarget.FromByteArray(eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SecondaryTarget.ToByteArray());
        S0xTieFlightGroupOrderPrimaryTarget ebp54_PrimaryTarget = S0xTieFlightGroupOrderPrimaryTarget.FromByteArray(eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].PrimaryTarget.ToByteArray());
        short ebp54_Speed = eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SpeedMph;

        if (A8 == 0x01 && !Unit_00513838_Proc_005208F8(AC))
        {
            return;
        }

        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
        eax2.m00147C[ecx0].M000000[A8 - 1] -= (short)AlliedVariables.s_V0x005B7084;

        if (A8 == 0x01)
        {
            eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
            eax2.FlightGroupStruct.StartPoints[0].Position[ecx0] -= (short)AlliedVariables.s_V0x005B7084;
        }
        else
        {
            eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
            eax2.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[A8 - 5].Position[ecx0] -= (short)AlliedVariables.s_V0x005B7084;
        }

        if ((TShiftState)12 == edx0 || ((TShiftState)13 == edx0 && A8 == 0x01))
        {
            for (int ebx = 0x01; ebx < 0x09; ebx++)
            {
                S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);

                if (eax3.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[ebx - 1].IsUsed == 0)
                {
                    continue;
                }

                eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
                eax3.m00147C[ecx0].M000008[ebx - 1] -= (short)AlliedVariables.s_V0x005B7084;

                eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
                eax3.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints[ebx - 1].Position[ecx0] -= (short)AlliedVariables.s_V0x005B7084;
            }
        }

        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
        eax1.FlightGroupStruct.DepartureDelayMinutes = ebp40_DepartureDelayMinutes;
        eax1.FlightGroupStruct.DepartureDelaySeconds = ebp40_DepartureDelaySeconds;
        eax1.FlightGroupStruct.AbortCondition = ebp40_AbortCondition;
        eax1.FlightGroupStruct.ArrivalRandomDelaySeconds = ebp40_ArrivalRandomDelaySeconds;
        eax1.FlightGroupStruct.CurStartFg = ebp40_CurStartFg;
        eax1.FlightGroupStruct.StartFg = ebp40_StartFg;
        eax1.FlightGroupStruct.StartFgUsed = ebp40_StartFgUsed;
        eax1.FlightGroupStruct.PrimaryStopFg = ebp40_PrimaryStopFg;
        eax1.FlightGroupStruct.PrimaryStopFgUsed = ebp40_PrimaryStopFgUsed;
        eax1.FlightGroupStruct.SecondaryStopFg = ebp40_SecondaryStopFg;
        eax1.FlightGroupStruct.SecondaryStopFgUsed = ebp40_SecondaryStopFgUsed;
        eax1.FlightGroupStruct.CaptureFg = ebp40_CaptureFg;
        eax1.FlightGroupStruct.CaptureFgUsed = ebp40_CaptureFgUsed;

        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AC);
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].OrderId = ebp54_OrderId;
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Throttle = ebp54_Throttle;
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var0 = ebp54_Parameters0;
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var1 = ebp54_Parameters1;
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Var2 = ebp54_Parameters2;
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SecondaryTarget = ebp54_SecondaryTarget;
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].PrimaryTarget = ebp54_PrimaryTarget;
        eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].SpeedMph = ebp54_Speed;

        Unit_00513838_Proc_0051D53C(AC);
    }

    // L0052EBA8
    private static void TForm1_Proc_0052EBA8(Form1Window Form1, int edx0, int ecx0)
    {
        List<S0xFGObject> edi = AlliedVariables.s_FlightGroupObjectsList;

        Unit_00513838_Proc_00519DA8();

        if (Form1.PaintBox1.ActualHeight - 0x14 < ecx0)
        {
            int edx1 = edx0;

            if (edx1 < 0)
            {
                edx1 += 0x0F;
            }

            edx1 = edx1 / 0x10 + 0x26;

            S0xFGObject eax1 = Classes_TList_Get(edi, edi.Count - 1);
            eax1.FlightGroupStruct.CraftId = (CraftIdEnum)MapWindowImpl.TMapForm__PROC_004F7D0C(AlliedVariables.s_TMapForm_Instance!, (byte)edx1);
        }
        else
        {
            int edx1 = edx0;

            if (edx1 < 0)
            {
                edx1 += 0x0F;
            }

            edx1 = edx1 / 0x10 + 1;

            S0xFGObject eax1 = Classes_TList_Get(edi, edi.Count - 1);
            eax1.FlightGroupStruct.CraftId = (CraftIdEnum)MapWindowImpl.TMapForm__PROC_004F7D0C(AlliedVariables.s_TMapForm_Instance!, (byte)edx1);
        }

        S0xFGObject eax2 = Classes_TList_Get(edi, edi.Count - 1);
        eax2.FlightGroupStruct.Iff = (byte)AlliedVariables.s_V0x005B7078;
        eax2 = Classes_TList_Get(edi, edi.Count - 1);
        eax2.FlightGroupStruct.Team = (byte)AlliedVariables.s_V0x00543B18;

        eax2 = Classes_TList_Get(edi, edi.Count - 1);
        byte al3 = eax2.FlightGroupStruct.CraftId - CraftIdEnum._024_0_32_Tug;

        if (al3 <= 0x47 && BtBitString(al3 & 0x7F, AlliedVariables.s_V0x0052ED70))
        {
            eax2 = Classes_TList_Get(edi, edi.Count - 1);
            eax2.IsWPEnabled[4] = 0;
        }

        Unit_00513838_Proc_0051DB68();
        AlliedVariables.s_V0x005B7050 = edi.Count - 1;
        AlliedVariables.s_V0x005B7054 = 0x01;
        Form1.SelectionBox.SetItems(AlliedVariables.s_V0x00543BC0);
        Form1.SelectionBox.SelectedIndex = AlliedVariables.s_V0x005B7050;
        Form1.SelectionBox.Update();
        AlliedVariables.s_V0x005B7068 = edi.Count - 1;
        AlliedVariables.s_V0x005B7044 = 0x01;
        eax2 = Classes_TList_Get(edi, edi.Count - 1);
        eax2.m00147A = 0x01;
        AlliedVariables.s_V0x005B7040 = 0x01;
        AlliedVariables.s_V0x005B7043 = 0;
        TForm1_CurrOnlyClick(Form1, Form1.CurrOnly);
        AlliedVariables.s_V0x005B7043 = 0x01;

        AlliedVariables.s_V0x005AFCC0!.RenderOpen();
        TForm1_Proc_0052D4CC(Form1, AlliedVariables.s_V0x005AFCC0);
        TForm1_Proc_0052D968(Form1, AlliedVariables.s_V0x005AFCC0!);
        AlliedVariables.s_V0x005AFCC0!.RenderClose();
    }

    // L0052D384
    private static void TForm1_Proc_0052D384(Form1Window Form1)
    {
        if (AlliedVariables.s_FlightGroupObjectsList.Count <= AlliedVariables.s_V0x005B7050)
        {
            return;
        }

        AlliedVariables.s_V0x005AFCBC!.RenderOpen();
        TForm1_Proc_0052D348(Form1, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCC0)!, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!);
        TForm1_Proc_0052DC20(Form1, AlliedVariables.s_V0x005B7050, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!);
        AlliedVariables.s_V0x005AFCBC!.RenderClose();
        TForm1_Proc_0052D348(Form1, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!, AlliedVariables.s_AlliedForm1Window!.PaintBox1);
    }

    // L00519DA8
    private static void Unit_00513838_Proc_00519DA8()
    {
        List<S0xFGObject> edi = AlliedVariables.s_FlightGroupObjectsList;
        Form1Window ebp = AlliedVariables.s_AlliedForm1Window!;
        S0xFGObject ebx = new();
        ebx.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
        ebx.FlightGroupStruct.Iff = AlliedVariables.s_V0x00543C78[0];
        ebx.IsWPEnabled[0] = 0x01;
        ebx.FlightGroupStruct.StartPointRegions[0] = (byte)(AlliedVariables.s_CurrentRegion - 1);

        if (AlliedVariables.s_UseAutoChkSetting)
        {
            ebx.AutoLink = true;
        }

        ebx.m001448 = 1.0;
        ebx.m001450 = 0;
        ebx.m001458 = 0;
        ebx.m001460 = -1.0;
        ebx.m001470 = 1.0;
        ebx.m001468 = 0;
        Unit_00513838_Proc_0051D614();
        ebx.m001444 = BitConverter.GetBytes(Unit_00511CD0_Proc_00512568(ebx.FlightGroupStruct));
        edi.Add(ebx);
        AlliedVariables.s_TieFileHeader.FlightGroupsCount++;
        Unit_00513838_Proc_0051950C(false);
        Unit_00513838_Proc_0051467C();
        Unit_00513838_Proc_0051DB68();

        int esi = ebp.ShipList.Items.Count;

        for (int ebx1 = 0; ebx1 < esi; ebx1++)
        {
            StdCtrls_TCustomListBox_SetSelected(ebp.ShipList, ebx1, false);

            S0xFGObject eax1 = Classes_TList_Get(edi, ebx1);
            eax1.m001479 = 0;
        }

        StdCtrls_TCustomListBox_SetSelected(ebp.ShipList, AlliedVariables.s_V0x00543B0C, true);
        S0xFGObject eax2 = Classes_TList_Get(edi, AlliedVariables.s_V0x00543B0C);
        eax2.m001479 = 0x01;
    }

    // L0051950C
    private static void Unit_00513838_Proc_0051950C(bool eax0)
    {
        if (eax0)
        {
            AlliedVariables.s_V0x00543B0C = AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex;
        }
        else
        {
            AlliedVariables.s_V0x00543B0C = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        }

        AlliedVariables.s_V0x00543C9C = 0x01;
        AlliedVariables.s_V0x005AFE90 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
        AlliedVariables.s_V0x00543B24 = 0;
        AlliedVariables.s_V0x00543B40 = 0;
        AlliedVariables.s_V0x00543B44 = 0;
        AlliedVariables.s_V0x00543B48 = 0;
        AlliedVariables.s_V0x005B6B64 = S0xTieFlightGroupGoal.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Goals[0].ToByteArray());

        if ((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag) == DatapadFGPageEnum.Options)
        {
            DatapadWindowImpl.Unit_00513838_Proc_0051B1C8();
        }

        DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);

        if (AlliedVariables.s_V0x005B6D18 != 0)
        {
            WaypointsWindowImpl.TWPform_PROC_0050BA2C(AlliedVariables.s_TWPform_Instance!);
        }

        AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex = AlliedVariables.s_V0x00543B0C;
        DatapadWindowImpl.TDatapad__PROC_004C05F0(AlliedVariables.s_TDatapad_Instance!);
        AlliedVariables.s_V0x00543B54 = 0;
        AlliedVariables.s_V0x00543B28 = 0x01;
        AlliedVariables.s_V0x00543B2C = 0x01;
        AlliedVariables.s_V0x00543B4C = 0x01;
        OrderSelWindowImpl.TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);
        CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, 0x01);
    }

    // L0052D108
    public static void TForm1_CurrOnlyClick(Form1Window Form1, object? Sender)
    {
        if (Form1.CurrOnly.IsChecked != true)
        {
            AlliedVariables.s_V0x005B7064 = 0;
            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
            AlliedVariables.s_V0x005B7044 = 0x01;
        }
        else
        {
            AlliedVariables.s_V0x005B7050 = Form1.SelectionBox.SelectedIndex;

            if (AlliedVariables.s_CenteringSetting)
            {
                MapWindowImpl.TMapForm__PROC_004F7A4C(AlliedVariables.s_TMapForm_Instance!);
            }

            AlliedVariables.s_V0x005B7064 = Form1.SelectionBox.SelectedIndex;
            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_V0x005B7064;
            AlliedVariables.s_V0x005B7044 = 0;
        }

        if (AlliedVariables.s_V0x005B7043 != 0)
        {
            TForm1_Proc_0052D3F8(Form1);
        }
    }

    // L0052BEB4
    private static void TForm1_AddHyperbuoys1Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_THyperForm_Instance = MainImpl.CreateHyperBox();
        AlliedVariables.s_THyperForm_Instance.Owner = Form1;
        AlliedVariables.s_THyperForm_Instance.ShowDialog();
        AlliedVariables.s_THyperForm_Instance = null;
        Unit_00513838_Proc_0051DB68();
        MapWindowImpl.TMapForm__PROC_004F8CC4(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052BF9C
    private static void TForm1_AddBackdrop1Click(Form1Window Form1, object? Sender)
    {
        S0xFGObject ebx0 = new();
        ebx0.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
        ebx0.FlightGroupStruct.StartPointRegions[0] = (byte)(AlliedVariables.s_CurrentRegion - 1);
        ebx0.m001444[AlliedVariables.s_CurrentRegion - 1] = 0x01;
        ebx0.FlightGroupStruct.CraftId = CraftIdEnum._183_9001_1100_ResData_Backdrop;
        ebx0.FlightGroupStruct.PlanetId = 0x01;
        ebx0.FlightGroupStruct.Name = Unit_00511CD0_Proc_00511EB8("1.0 1.0 1.0");
        ebx0.FlightGroupStruct.Cargo = Unit_00511CD0_Proc_00511EB8("1.0");
        ebx0.FlightGroupStruct.SpecialCargo = Unit_00511CD0_Proc_00511EB8("1.0");
        ebx0.m001448 = 1.0;
        ebx0.m001450 = 0.0;
        ebx0.m001458 = 0.0;
        ebx0.m001460 = -1.0;
        ebx0.m001468 = 0.0;
        ebx0.m001470 = 1.0;
        AlliedVariables.s_FlightGroupObjectsList.Add(ebx0);
        AlliedVariables.s_TieFileHeader.FlightGroupsCount++;

        Unit_00513838_Proc_0051950C(false);
        Unit_00513838_Proc_0051467C();
        Unit_00513838_Proc_0051DB68();

        if (Form1.CurrOnly.IsChecked != true)
        {
            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_FlightGroupObjectsList.Count - 1);
            eax.m00147A = 0x01;
            AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        }

        int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, ebx, false);

            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            eax.m001479 = 0;
        }

        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543B0C, true);

        S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
        eax1.m001479 = 0x01;

        MapWindowImpl.TMapForm__PROC_004F8CC4(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052C1B8
    private static void TForm1_XScrollChange(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x005B7042 == 0)
        {
            return;
        }

        AlliedVariables.s_V0x005B7020 = Integer_Negate_L0051C034((int)Form1.XScroll.Value);
        AlliedVariables.s_V0x005B7030 = Math.Round(0.0f - MapWindowImpl.TMapForm_Proc_004F6640(AlliedVariables.s_TMapForm_Instance!, 0, AlliedVariables.s_V0x005B6D68));

        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052EA34
    private static void TForm1_YScrollChange(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x005B7042 == 0)
        {
            return;
        }

        AlliedVariables.s_V0x005B7028 = Integer_Negate_L0051C034((int)Form1.YScroll.Value);
        AlliedVariables.s_V0x005B7038 = 0.0f - MapWindowImpl.TMapForm_Proc_004F6640(AlliedVariables.s_TMapForm_Instance!, 0x01, AlliedVariables.s_V0x005B6D6C);

        TForm1_Proc_0052D3F8(Form1);
    }

    // L005285C4
    private static void TForm1_NewMissionButClick(Form1Window Form1, object? Sender)
    {
        bool bl = false;

        if (Unit_00513838_Proc_0051710C())
        {
            TModalResultEnum ax1 = MessageBox_ShowConfirmation("Current Mission has been changed. Save it?", "cancel");

            if (ax1 == TModalResultEnum.Yes)
            {
                if (AlliedVariables.s_V0x00543B50 != 0)
                {
                    Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0x01, 0);
                }
                else
                {
                    TForm1_SaveAsBtnClick(Form1, Sender);
                }

                bl = true;
            }
            else if (ax1 == TModalResultEnum.No)
            {
                bl = true;
            }
        }
        else
        {
            Unit_00513838_Proc_00518694();
        }

        if (!Unit_00513838_Proc_0051710C())
        {
            bl = true;
        }

        if (bl)
        {
            Unit_00513838_Proc_00517128();
            Unit_00513838_Proc_00520B74();
        }

        AlliedVariables.s_V0x005B7064 = 0;
        AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
        AlliedVariables.s_V0x005B7044 = 0x01;
        AlliedVariables.s_V0x005B7050 = 0;
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        MapWindowImpl.TMapForm_Proc_004F6B48(AlliedVariables.s_TMapForm_Instance!);
        OrderSelWindowImpl.TOrderSel__PROC_0050CA68(AlliedVariables.s_TOrderSel_Instance!);

        if (AlliedVariables.s_V0x005B6D18 != 0)
        {
            WaypointsWindowImpl.TWPform_PROC_0050BA2C(AlliedVariables.s_TWPform_Instance!);
        }

        Unit_00513838_Proc_00520924();
    }

    // L0051710C
    public static bool Unit_00513838_Proc_0051710C()
    {
        return AlliedVariables.s_V0x00543B54 != 0 || AlliedVariables.s_V0x00543B55 != 0 || AlliedVariables.s_V0x00543B53;
    }

    // L00518694
    public static void Unit_00513838_Proc_00518694()
    {
        if (AlliedVariables.s_V0x00543B59 == 0)
        {
            return;
        }

        if (!File.Exists(AlliedVariables.s_V0x00543C04))
        {
            return;
        }

        if (string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
        {
            return;
        }

        TModalResultEnum ax0 = MessageBox_ShowConfirmation("WAV .lst file has been changed. Save it?", null);

        if (ax0 == TModalResultEnum.Yes)
        {
            TForm1_SaveWAVClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.SaveWAV);
        }
    }

    // L00517128
    private static void Unit_00513838_Proc_00517128()
    {
        Unit_00513838_Proc_005144F8();
        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.MsgtextEd, null);
        Unit_00513838_Proc_005196CC();
        Unit_00513838_Proc_00513F30();
        AlliedVariables.s_V0x00543BF8 = "Unnamed";
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!, ProductVersionHelpers.GetNameAndVersion() + " - [Unnamed]");
        AlliedVariables.s_V0x00543C04 = "none";
        Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!.WAVfileEd, "No matching .lst file");
        AlliedVariables.s_V0x00543C3C.Clear();
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.Stop();
        AlliedVariables.s_TErrForm_OmitWarns = false;
    }

    // L00527648
    public static void TForm1_SaveBtnClick(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543B50 != 0)
        {
            if (!string.Equals(AlliedVariables.s_V0x00543BF8, "Unnamed", StringComparison.Ordinal))
            {
                Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0x01, 0);
                return;
            }
        }

        TForm1_SaveAsBtnClick(Form1, Sender);
    }

    // L00525610
    private static void TForm1_LoadBtnClick(Form1Window Form1, object? ToolButton)
    {
        if (!Unit_00513838_Proc_0051710C())
        {
            Unit_00513838_Proc_00518694();
            Unit_00513838_Proc_00515884(GameVersionEnum.Unknown);
        }
        else
        {
            TModalResultEnum ax0 = MessageBox_ShowConfirmation("Current Mission has been changed. Save it?", "cancel");

            if (ax0 == TModalResultEnum.Yes)
            {
                if (AlliedVariables.s_V0x00543B50 != 0)
                {
                    Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0, 0x01);
                }
                else
                {
                    TForm1_SaveAsBtnClick(Form1, ToolButton);
                }

                Unit_00513838_Proc_00518694();
                Unit_00513838_Proc_00515884(GameVersionEnum.Unknown);
            }
            else if (ax0 == TModalResultEnum.No)
            {
                Unit_00513838_Proc_00518694();
                Unit_00513838_Proc_00515884(GameVersionEnum.Unknown);
            }
        }
    }

    // L005256D0
    public static void TForm1_SaveAsBtnClick(Form1Window Form1, object? Sender)
    {
        Dialogs_TOpenDialog_SetInitialDir(Form1.SaveDialog1, AlliedVariables.s_StartDirLabSetting);
        string ebp04 = AlliedVariables.s_V0x00543BF8;

        if (Form1.SaveDialog1.ShowDialog(Form1) != true)
        {
            AlliedVariables.s_V0x00543B5B = 0x01;
        }
        else
        {
            AlliedVariables.s_V0x00543BF8 = Form1.SaveDialog1.FileName;
            AlliedVariables.s_V0x00543B59 = 0;
            AlliedVariables.s_V0x00543BF8 = Form1.SaveDialog1.FileName;
            AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
            Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0x01, 0);

            bool al1 = true;

            if (al1)
            {
                if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal) && AlliedVariables.s_V0x00543C3C.GetCount() > 0)
                {
                    if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.Bop)
                    {
                        string ebp14_1 = Allied_GetFileNameWithLstExtension(AlliedVariables.s_V0x00543BF8);
                        AlliedVariables.s_V0x00543C04 = AlliedVariables.s_BoPDirLabSetting + "\\wave\\" + ebp14_1;
                    }
                    else
                    {
                        string ebp14_0 = Allied_GetFileNameWithLstExtension(AlliedVariables.s_V0x00543BF8);
                        AlliedVariables.s_V0x00543C04 = AlliedVariables.s_XWADirLabSetting + "\\wave\\Missionvoice\\" + ebp14_0;
                    }

                    TForm1_SaveWAVClick(Form1, Sender);
                }
            }
        }
    }

    // L0052B418
    private static void TForm1_Reopen1Click(Form1Window Form1, object? Sender)
    {
        string caption = (Sender as MenuItem)?.Header as string ?? string.Empty;

        if (string.Equals(caption, "_Reopen", StringComparison.Ordinal))
        {
            return;
        }

        bool bl0 = true;

        if (Unit_00513838_Proc_0051710C())
        {
            TModalResultEnum ax1 = MessageBox_ShowConfirmation("Current Mission has been changed. Save it?", "cancel");

            switch (ax1)
            {
                case TModalResultEnum.Cancel:
                    bl0 = false;
                    break;

                case TModalResultEnum.Yes:
                    if (AlliedVariables.s_V0x00543B50 != 0)
                    {
                        if (!string.Equals(AlliedVariables.s_V0x00543BF8, "Unnamed", StringComparison.Ordinal))
                        {
                            Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0x01, 0);
                            break;
                        }
                    }

                    AlliedVariables.s_V0x00543B5B = 0;
                    TForm1_SaveAsBtnClick(Form1, Sender);

                    if (AlliedVariables.s_V0x00543B5B != 0)
                    {
                        bl0 = false;
                    }

                    break;

                case TModalResultEnum.No:
                    AlliedVariables.s_V0x00543B53 = false;
                    break;
            }
        }

        if (bl0)
        {
            Unit_00513838_Proc_00518694();

            //if (string.Equals(caption, "_Load by lst...", StringComparison.Ordinal))
            if (Sender is Window)
            {
                AlliedVariables.s_TLBLForm_Instance = MainImpl.CreateLblBox();
                AlliedVariables.s_TLBLForm_Instance.Owner = Form1;
                TForm1_Proc_0052BD6C(Form1, AlliedVariables.s_TLBLForm_Instance!, AlliedVariables.s_V0x005AFC74.m00003C);
                AlliedVariables.s_TLBLForm_Instance!.BrowseChk.IsChecked = AlliedVariables.s_V0x005AFC74.m00003C.m000008 != 0;
                Menus_TMenuItem_SetEnabled(Form1.Loadbylst2, false);
                TApplication_BringToFront(AlliedVariables.s_TLBLForm_Instance!);
            }
            else
            {
                int edx1 = int.Parse(caption[1].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                string ebp14_3 = AlliedVariables.s_Allied_FilenamesHistory.GetText(edx1);

                if (File.Exists(ebp14_3))
                {
                    AlliedVariables.s_V0x00543BF8 = ebp14_3;
                    AlliedHistoryAddStr(AlliedVariables.s_Allied_FilenamesHistory, AlliedVariables.s_V0x00543BF8);
                    TForm1_ReadTieMission(Form1, AlliedVariables.s_V0x00543BF8);
                }
                else
                {
                    MessageBox_ShowError(ebp14_3 + " does not exist");
                }
            }
        }
    }

    // L0052F31C
    private static void TForm1_ShowOrderSelClick(Form1Window eax0, object? edx0)
    {
        Buttons_L00467140_SetVisible(AlliedVariables.s_TOrderSel_Instance!, eax0.ShowOrderSel.IsChecked == true);
        Menus_TMenuItem_SetChecked(eax0.OrderRegionSelect1, eax0.ShowOrderSel.IsChecked == true);

        if (Convert.ToInt32(AlliedVariables.s_AlliedForm1Window!.OverallPages.GetActivePage().Tag) == (int)Form1OverallPagesEnum.FlightGroups)
        {
            AlliedVariables.s_V0x005B704A = eax0.ShowOrderSel.IsChecked == true;
        }
    }

    // L0052356C
    private static void TForm1_FormClose(Form1Window Form1, object? edx0, CancelEventArgs ecx0)
    {
        bool bl = true;

        if (Unit_00513838_Proc_0051710C())
        {
            TModalResultEnum ax1 = MessageBox_ShowConfirmation("Current Mission has been changed. Save it?", "cancel");

            if (ax1 == TModalResultEnum.Cancel)
            {
                bl = false;
            }
            else if (ax1 == TModalResultEnum.Yes)
            {
                bool skip = false;

                if (AlliedVariables.s_V0x00543B50 == 0)
                {
                    skip = true;
                }

                if (!skip)
                {
                    if (string.Equals(AlliedVariables.s_V0x00543BF8, "Unnamed", StringComparison.Ordinal))
                    {
                        skip = true;
                    }
                }

                if (!skip)
                {
                    Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0x01, 0);
                }
                else
                {
                    AlliedVariables.s_V0x00543B5B = 0;
                    TForm1_SaveAsBtnClick(Form1, edx0);

                    if (AlliedVariables.s_V0x00543B5B != 0)
                    {
                        bl = false;
                    }
                }
            }
        }

        if (!bl)
        {
            ecx0.Cancel = true;
        }
        else
        {
            Unit_00513838_Proc_00518694();
            LibWindowImpl.TLibForm_ShowSaveIfChanged(AlliedVariables.s_TLibForm_Instance, null);

            try
            {
                if (AlliedVariables.s_Allied_FilenamesHistory.GetCount() != 0)
                {
                    Unit_00507130_Proc_00507228();
                }
            }
            catch
            {
                MessageBox_ShowError("Error saving History.");
            }

            if (AlliedVariables.s_V0x005439A8.m000004 == 0 && AlliedVariables.s_V0x00543948 == 0)
            {
                if (AlliedVariables.s_V0x00535E8C != 0)
                {
                    TForm1_L0052BD20(Form1, AlliedVariables.s_TMemoForm_Instance!, AlliedVariables.s_V0x005AFC74.m000000);
                }

                if (AlliedVariables.s_V0x005B6D18 != 0)
                {
                    TForm1_L0052BD20(Form1, AlliedVariables.s_TWPform_Instance!, AlliedVariables.s_V0x005AFC74.m000030);
                }

                Unit_00507130_Proc_00508BF4();
            }

            Unit_00513838_Proc_005144F8();
            LibWindowImpl.TLibForm_Proc_005132CC(AlliedVariables.s_TLibForm_Instance);
            ecx0.Cancel = false;
            AlliedVariables.s_V0x00543D40?.Close();
            AlliedVariables.s_V0x00543D40 = null;
            AlliedVariables.s_V0x005B7094?.Close();
            AlliedVariables.s_V0x005B7094 = null;

            if (AlliedVariables.s_V0x005B6D14 != 0)
            {
                AlliedVariables.s_V0x00543B52 = 0x01;
                //AlliedVariables.s_TMapForm_Instance!.Visibility = Visibility.Visible;
                MapWindowImpl.TMapForm__PROC_004F6A2C(AlliedVariables.s_TMapForm_Instance!);
                AlliedVariables.s_TMapForm_Instance!.Close();
                AlliedVariables.s_TMapForm_Instance = null;
            }

            AlliedVariables.s_Strings_Backdrops.Clear();
            AlliedVariables.s_Strings_Ships.Clear();
            AlliedVariables.s_V0x00543B64.Clear();
            AlliedVariables.s_Strings_FGNames.Clear();
            AlliedVariables.s_Strings_Players.Clear();
            AlliedVariables.s_Strings_Status.Clear();
            AlliedVariables.s_Strings_AI.Clear();
            AlliedVariables.s_Strings_IFF.Clear();
            AlliedVariables.s_Strings_Colors.Clear();
            AlliedVariables.s_Strings_Radio.Clear();
            AlliedVariables.s_Strings_Form.Clear();
            AlliedVariables.s_Strings_Missiles.Clear();
            AlliedVariables.s_Strings_Teams.Clear();
            AlliedVariables.s_Strings_Counters.Clear();
            AlliedVariables.s_Strings_Beams.Clear();
            AlliedVariables.s_Strings_CMD.Clear();
            AlliedVariables.s_Strings_OpShips.Clear();
            AlliedVariables.s_Strings_Orders.Clear();
            AlliedVariables.s_V0x00543BA8.Clear();
            AlliedVariables.s_Strings_Speeds.Clear();
            AlliedVariables.s_Allied_FilenamesHistory.Clear();
            AlliedVariables.s_Allied_Numbers_NoneTo255.Clear();
            AlliedVariables.s_Strings_Musts.Clear();
            AlliedVariables.s_Strings_ObjCats.Clear();
            AlliedVariables.s_Strings_ShipCats.Clear();
            AlliedVariables.s_V0x00543BC0.Clear();
            AlliedVariables.s_Strings_Regions.Clear();
            AlliedVariables.s_V0x00543BC8.Clear();
            AlliedVariables.s_V0x00543BCC.Clear();
            AlliedVariables.s_Strings_Short.Clear();
            AlliedVariables.s_V0x00543BD4.Clear();
            AlliedVariables.s_Strings_Planets.Clear();
            AlliedVariables.s_V0x00543C3C.Clear();
            AlliedVariables.s_Strings_WPEnable.Clear();
            AlliedVariables.s_Strings_When.Clear();
            AlliedVariables.s_Strings_DepWhen.Clear();
            AlliedVariables.s_V0x00543BE8.Clear();
            AlliedVariables.s_Strings_OrdTexts.Clear();
            AlliedVariables.s_Strings_ShipSeq.Clear();
            AlliedVariables.s_TDatapad_T1ClassStrings.Clear();
        }
    }

    // L00507228
    private static void Unit_00507130_Proc_00507228()
    {
        if (AlliedVariables.s_V0x00543948 != 0)
        {
            return;
        }

        if (AlliedVariables.s_Allied_FilenamesHistory.GetCount() > 0)
        {
            using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            using RegistryKey? key = baseKey.OpenSubKey("SOFTWARE\\Troy's Editors\\AlliED", true);

            RegistryCreateKeyInteger(key, "History", "Count", AlliedVariables.s_Allied_FilenamesHistory.GetCount());

            int esi = AlliedVariables.s_Allied_FilenamesHistory.GetCount();

            for (int ebx = 0; ebx < esi; ebx++)
            {
                string ebp0C_1 = AlliedVariables.s_Allied_FilenamesHistory.GetText(ebx);
                string ebp0C_0 = string.Format(CultureInfo.InvariantCulture, "file{0}", ebx + 1);
                RegistryCreateKeyString(key, "History", ebp0C_0, ebp0C_1);
            }
        }
    }

    // L00508BF4
    private static void Unit_00507130_Proc_00508BF4()
    {
        using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
        using RegistryKey? key = baseKey.CreateSubKey("SOFTWARE\\Troy's Editors\\AlliED", true);

        if (!string.IsNullOrEmpty(AlliedVariables.s_StartDirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "Missions", AlliedVariables.s_StartDirLabSetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_BriefingsDirectorySetting))
        {
            RegistryCreateKeyString(key, "Directories", "Briefings", AlliedVariables.s_BriefingsDirectorySetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_XWADirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "XWA", AlliedVariables.s_XWADirLabSetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_AlliedDirectoryPath))
        {
            RegistryCreateKeyString(key, "Directories", "AlliED", AlliedVariables.s_AlliedDirectoryPath);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_WaveDirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "Wave", AlliedVariables.s_WaveDirLabSetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_OPTDirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "OPT", AlliedVariables.s_OPTDirLabSetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_XWDirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "XWing", AlliedVariables.s_XWDirLabSetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_TFDirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "T/F", AlliedVariables.s_TFDirLabSetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_XvTDirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "XvT", AlliedVariables.s_XvTDirLabSetting);
        }

        if (!string.IsNullOrEmpty(AlliedVariables.s_BoPDirLabSetting))
        {
            RegistryCreateKeyString(key, "Directories", "BoP", AlliedVariables.s_BoPDirLabSetting);
        }

        RegistryCreateKeyBool(key, "Options", "AutoLink", AlliedVariables.s_UseAutoChkSetting);
        RegistryCreateKeyBool(key, "Options", "CheckPlayable", AlliedVariables.s_PlayableChkSetting);
        RegistryCreateKeyBool(key, "Options", "ConfirmDeletes", AlliedVariables.s_ConfDeletesChkSetting);
        RegistryCreateKeyBool(key, "Options", "Blacken", AlliedVariables.s_BlackenChkSetting);
        RegistryCreateKeyBool(key, "Options", "EnableWPs", AlliedVariables.s_EnableWPsChkSetting);
        RegistryCreateKeyBool(key, "Options", "LinkColor", AlliedVariables.s_LinkColorChkSetting);
        RegistryCreateKeyBool(key, "Options", "DPToggle", AlliedVariables.s_ResChkSetting);
        RegistryCreateKeyBool(key, "Options", "UseWiz", AlliedVariables.s_UseWizChkSetting);
        RegistryCreateKeyInteger(key, "Options", "DefaultFormat", AlliedVariables.s_MissionFormatRadioIndexSetting);
        RegistryCreateKeyInteger(key, "Options", "OpenProgWith", (int)AlliedVariables.s_OpenWithRadioIndexSetting);
        RegistryCreateKeyBool(key, "Options", "DirInReopen", AlliedVariables.s_DirInHistChkSetting);
        RegistryCreateKeyInteger(key, "Options", "DefaultShip", (int)AlliedVariables.s_DefaultShipBoxSettingIndex);
        RegistryCreateKeyInteger(key, "Options", "DefaultAI", AlliedVariables.s_DefaultAIBoxSettingIndex);
        RegistryCreateKeyBool(key, "Options", "FileSavedMsg", AlliedVariables.s_ConfSaveChkSetting);
        RegistryCreateKeyBool(key, "Options", "DoBackups", AlliedVariables.s_DoBackupsChkSetting);
        RegistryCreateKeyBool(key, "Options", "LockOrdToReg", AlliedVariables.s_LockOrdToRegOptionSetting);
        RegistryCreateKeyBool(key, "Options", "CheckFilename", AlliedVariables.s_CheckFilenameOptionSetting);
        RegistryCreateKeyBool(key, "Options", "ErrorCheck", AlliedVariables.s_AlliedForm1Window!.ErrCheckOn1.IsChecked);
        RegistryCreateKeyBool(key, "Options", "OverwriteBKUP", AlliedVariables.s_AlliedForm1Window!.Overwriteprevious1.IsChecked);
        RegistryCreateKeyBool(key, "Options", "BrfOpt1", AlliedVariables.s_V0x00543938.ShowCurrDown);
        RegistryCreateKeyBool(key, "Options", "BrfOpt2", AlliedVariables.s_V0x00543938.ShowNumsDown);
        RegistryCreateKeyBool(key, "Options", "BrfOpt3", AlliedVariables.s_V0x00543938.StopAtStopDown);
        RegistryCreateKeyBool(key, "Options", "BrfOpt4", AlliedVariables.s_V0x00543938.FastPlaybackBtnDown);
        RegistryCreateKeyInteger(key, "Options", "IconSpeed", AlliedVariables.s_IconSpeedOptionSetting);

        for (int esi = 0x01; esi < 0x04; esi++)
        {
            S0x00534058_000000 ebp04 = AlliedVariables.s_V0x00543C84.M000000[esi - 1];
            RegistryCreateKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", esi, 0), ebp04.M000000 ? 1 : 0);
            RegistryCreateKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", esi, 1), ebp04.M000001);
            RegistryCreateKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", esi, 2), ebp04.M000002);
            RegistryCreateKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", esi, 3), ebp04.M000003);
            RegistryCreateKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", esi, 4), ebp04.M000004);
            RegistryCreateKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", esi, 5), ebp04.M000005);
            RegistryCreateKeyInteger(key, "Options", string.Format(CultureInfo.InvariantCulture, "ImportOpn{0}{1}", esi, 6), ebp04.M000006 ? 1 : 0);
        }

        RegistryCreateKeyBool(key, "Map", "GhostText", AlliedVariables.s_GhostMapChkSetting);
        RegistryCreateKeyBool(key, "Map", "17.6km SSD", AlliedVariables.s_SSD17chkSetting);
        RegistryCreateKeyBool(key, "Map", "Centre", AlliedVariables.s_CenteringSetting);
        RegistryCreateKeyBool(key, "Map", "ConfirmClose", AlliedVariables.s_ConfirmCloseMapSetting);
        RegistryCreateKeyInteger(key, "Map", "DefaultWPs", (int)AlliedVariables.s_WPsDefaultRadioIndexSetting);
        RegistryCreateKeyInteger(key, "Map", "AutoCtr", (int)AlliedVariables.s_AutoCtrGrpIndexSetting);
        RegistryCreateKeyInteger(key, "Map", "ZoomSpeed", AlliedVariables.s_ZoomSpeedScrollPositionSetting);
        RegistryCreateKeyInteger(key, "Map", "DblClickLink", AlliedVariables.s_DblClickLinkRadioIndexSetting);
        RegistryCreateKeyBool(key, "Map", "DefaultName", AlliedVariables.s_NamesOnChkSetting);
        RegistryCreateKeyBool(key, "Map", "DefaultWPtime", AlliedVariables.s_AlliedForm1Window!.Showtimes.IsChecked);
        RegistryCreateKeyBool(key, "Map", "DefaultWPdist", AlliedVariables.s_AlliedForm1Window!.Showdistances.IsChecked);
        RegistryCreateKeyBool(key, "Map", "DefaultPalet", AlliedVariables.s_DefaultPalletOnChkSetting);
        RegistryCreateKeyBool(key, "Map", "DefaultFGList", AlliedVariables.s_DefaultFGListMapSetting);
        RegistryCreateKeyBool(key, "Map", "DefaultHyp", AlliedVariables.s_DefaultHypOnChkSetting);
        RegistryCreateKeyBool(key, "Map", "DefaultOptions", AlliedVariables.s_DefaultOptionsOnChkSetting);
        RegistryCreateKeyBool(key, "Map", "LimitShrink", AlliedVariables.s_LimitShrinkChkSetting);
        RegistryCreateKeyBool(key, "Map", "GhostGrid", AlliedVariables.s_DarkGridChkSetting);
        RegistryCreateKeyBool(key, "Map", "ShowNumbers", AlliedVariables.s_ShowNumbersMapSetting);
        RegistryCreateKeyBool(key, "Map", "OnlyXY", AlliedVariables.s_OnlyXYChkSetting);
        RegistryCreateKeyInteger(key, "Map", "IconZoomSmall", AlliedVariables.s_IconZoomEditSetting);

        for (int ebx = 0; ebx < 0x03; ebx++)
        {
            RegistryCreateKeyInteger(key, "Layout", "lw" + ebx.ToString(CultureInfo.InvariantCulture), AlliedVariables.s_V0x005B7088[ebx]);
        }

        RegistryCreateKeyBool(key, "Layout", "wmx", AlliedVariables.s_AlliedForm1Window!.M00022B() == 0x02);
        RegistryCreateKeyBool(key, "Layout", "dpo", AlliedVariables.s_V0x005B7048);
        RegistryCreateKeyBool(key, "Layout", "mpo", AlliedVariables.s_AlliedForm1Window!.ShowFGList.IsChecked == true);

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            TForm1_L0052BD20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_V0x005AFC74.m000018);
        }

        if (AlliedVariables.s_V0x005B6D17 != 0)
        {
            TForm1_L0052BD20(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_TOrderSel_Instance!, AlliedVariables.s_V0x005AFC74.m00000C);
        }

        AlliedVariables.s_V0x005AFC74.m00000C.m000008 = AlliedVariables.s_V0x005B704A ? (short)1 : (short)0;

        static void S0x005342B0_00000C_Write(RegistryKey? key, S0x005342B0_00000C esi, int ebx)
        {
            RegistryCreateKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "1", esi.Top);
            RegistryCreateKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "2", esi.Left);
            RegistryCreateKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "3", esi.Height);
            RegistryCreateKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "4", esi.Width);
            RegistryCreateKeyInteger(key, "Layout", "fd" + ebx.ToString(CultureInfo.InvariantCulture) + "5", esi.m000008);
        }

        S0x005342B0_00000C_Write(key, AlliedVariables.s_V0x005AFC74.m000000, 1);
        S0x005342B0_00000C_Write(key, AlliedVariables.s_V0x005AFC74.m00000C, 2);
        S0x005342B0_00000C_Write(key, AlliedVariables.s_V0x005AFC74.m000018, 3);
        S0x005342B0_00000C_Write(key, AlliedVariables.s_V0x005AFC74.m000024, 4);
        S0x005342B0_00000C_Write(key, AlliedVariables.s_V0x005AFC74.m000030, 5);
        S0x005342B0_00000C_Write(key, AlliedVariables.s_V0x005AFC74.m00003C, 6);

        AlliedVariables.s_V0x00543C64[0x09] = AlliedVariables.s_AlliedForm1Window!.ShowWAVman.IsChecked == true;

        for (int ebx = 0; ebx < 0x0C; ebx++)
        {
            RegistryCreateKeyBool(key, "Layout", "seo" + ebx.ToString(CultureInfo.InvariantCulture), AlliedVariables.s_V0x00543C64[ebx]);
        }
    }

    // L0052EAA8
    public static void TForm1_ShowFGListClick(Form1Window Form1, object? edx0)
    {
        bool showFGList = Form1.ShowFGList.IsChecked == true;

        // todo: upgrade
        //Controls_TControl_SetVisible(Form1.OverallPages, showFGList);
        if (showFGList)
        {
            Form1.OverallPages.Visibility = Visibility.Visible;
            Form1.mainGrid.ColumnDefinitions[0].Width = (GridLength)Form1.mainGrid.ColumnDefinitions[0].Tag;
        }
        else
        {
            Form1.OverallPages.Visibility = Visibility.Collapsed;
            Form1.mainGrid.ColumnDefinitions[0].Tag = Form1.mainGrid.ColumnDefinitions[0].Width;
            Form1.mainGrid.ColumnDefinitions[0].Width = new GridLength(0);
        }

        ExtCtrls_TSplitter_SetBeveled(Form1.Splitter1, showFGList);
        Menus_TMenuItem_SetChecked(Form1.ShipList1, showFGList);
    }

    // L0052F1E4
    private static void TForm1_ShipList1Click(Form1Window Form1, object? Sender)
    {
        ComCtrls_TToolButton_SetDown(Form1.ShowFGList, Form1.ShowFGList.IsChecked != true);
        TForm1_ShowFGListClick(Form1, Form1.ShowFGList);
        Menus_TMenuItem_SetChecked(Form1.ShipList1, Form1.ShowFGList.IsChecked == true);
    }

    // L0052F24C
    private static void TForm1_ShowDatapadClick(Form1Window eax0, object? edx0)
    {
        Buttons_L00467140_SetVisible(AlliedVariables.s_TDatapad_Instance!, eax0.ShowDatapad.IsChecked == true);
        Menus_TMenuItem_SetChecked(eax0.Datapad1, eax0.ShowDatapad.IsChecked == true);

        if (Convert.ToInt32(AlliedVariables.s_AlliedForm1Window!.OverallPages.GetActivePage().Tag) == (int)Form1OverallPagesEnum.FlightGroups)
        {
            AlliedVariables.s_V0x005B7048 = eax0.ShowDatapad.IsChecked == true;
        }
    }

    // L0052F2D4
    private static void TForm1_Datapad1Click(Form1Window Form1, object? Sender)
    {
        ComCtrls_TToolButton_SetDown(Form1.ShowDatapad, Form1.ShowDatapad.IsChecked != true);
        Buttons_L00467140_SetVisible(AlliedVariables.s_TDatapad_Instance!, Form1.ShowDatapad.IsChecked == true);
        Menus_TMenuItem_SetChecked(Form1.Datapad1, Form1.ShowDatapad.IsChecked == true);
    }

    // L0052F378
    private static void TForm1_OrderRegionSelect1Click(Form1Window Form1, object? Sender)
    {
        ComCtrls_TToolButton_SetDown(Form1.ShowOrderSel, Form1.ShowOrderSel.IsChecked != true);
        Buttons_L00467140_SetVisible(AlliedVariables.s_TOrderSel_Instance!, Form1.ShowOrderSel.IsChecked == true);
        Menus_TMenuItem_SetChecked(Form1.OrderRegionSelect1, Form1.ShowOrderSel.IsChecked == true);
    }

    // L0052B6AC
    private static void TForm1_OverallPagesChange(Form1Window Form1, object? Sender)
    {
        int esi0 = AlliedVariables.s_V0x005B7088[Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag)];

        if (esi0 > TApplication_GetWidth() - 0x32)
        {
            esi0 = TApplication_GetWidth() - 0x32;
        }

        // todo: upgrade
        //Controls_TControl_SetWidth(Form1.OverallPages, esi0);
        Form1.OverallPages.MinWidth = esi0;
        Form1.OverallPages.Update();

        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                Form1.LibShowBut.IsEnabled = true;
                Form1.PutIntoLib.IsEnabled = true;
                Form1.ShowWAVman.IsEnabled = false;
                Controls_TControl_SetVisible(Form1.ControlBar2, true);
                Form1.ControlBar2.Update();
                Controls_TControl_SetVisible(Form1.MsgStrList, true);
                Controls_TControl_SetVisible(Form1.ShipList, true);
                break;

            case Form1OverallPagesEnum.Messages:
                Form1.LibShowBut.IsEnabled = false;
                Form1.PutIntoLib.IsEnabled = false;
                Form1.ShowWAVman.IsEnabled = true;
                Controls_TControl_SetVisible(Form1.ControlBar2, true);
                Form1.ControlBar2.Update();
                Controls_TControl_SetVisible(Form1.MsgStrList, true);
                Controls_TControl_SetVisible(Form1.ShipList, true);
                break;

            case Form1OverallPagesEnum.Teams:
                Controls_TControl_SetVisible(Form1.ControlBar2, false);
                break;
        }

        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                {
                    if (AlliedVariables.s_V0x005B704B)
                    {
                        TForm1_Proc_0052F518(Form1, true);
                    }

                    if (AlliedVariables.s_V0x005B704A)
                    {
                        TForm1_Proc_0052F464(Form1, true);
                    }

                    if (AlliedVariables.s_V0x005B7048)
                    {
                        TForm1_Proc_0052F414(Form1, true);
                        AlliedVariables.s_TDatapad_Instance!.Update();
                    }

                    if (!AlliedVariables.s_V0x005B7048)
                    {
                        TForm1_Proc_0052F414(Form1, false);
                    }

                    if (!AlliedVariables.s_V0x005B704A)
                    {
                        TForm1_Proc_0052F464(Form1, false);
                    }

                    if (!AlliedVariables.s_V0x005B704B)
                    {
                        TForm1_Proc_0052F518(Form1, false);
                    }

                    if (AlliedVariables.s_TDatapad_Instance is not null)
                    {
                        ComCtrls_TPageControl_SetActivePage(AlliedVariables.s_TDatapad_Instance!.FGPages, ComCtrls_TPageControl_GetPage(AlliedVariables.s_TDatapad_Instance!.FGPages, AlliedVariables.s_V0x00543CC4));
                        ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(AlliedVariables.s_TDatapad_Instance!.FGPages, 0x08), true);
                        ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(AlliedVariables.s_TDatapad_Instance!.FGPages, 0x09), false);
                        Unit_00513838_Proc_0051C03C((DatapadFGPageEnum)Convert.ToInt32(AlliedVariables.s_TDatapad_Instance!.FGPages.GetActivePage().Tag));
                        DatapadWindowImpl.TDatapad_FGPagesChange(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_TDatapad_Instance!.FGPages);
                        AlliedVariables.s_TDatapad_Instance!.FGPages.Update();
                    }

                    break;
                }

            case Form1OverallPagesEnum.Messages:
                {
                    TForm1_Proc_0052F518(Form1, false);
                    ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(AlliedVariables.s_TDatapad_Instance!.FGPages, 0x09), true);
                    ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(AlliedVariables.s_TDatapad_Instance!.FGPages, 0x08), false);
                    ComCtrls_TPageControl_SetActivePage(AlliedVariables.s_TDatapad_Instance!.FGPages, AlliedVariables.s_TDatapad_Instance!.Messag);
                    Form1.OverallPages.GetActivePage().Update();
                    TForm1_Proc_00526E4C(Form1, AlliedVariables.s_V0x005B6B58.RadioMessage);
                    AlliedVariables.s_V0x00543B3C = 0x01;
                    CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, 0x01);
                    Unit_00513838_Proc_0051C0F0((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag));
                    DatapadWindowImpl.TDatapad_FGPagesChange(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_TDatapad_Instance!.FGPages);

                    if (Form1.EndMsgWav.IsChecked != true)
                    {
                        TForm1_Proc_0052F414(Form1, true);
                        AlliedVariables.s_TDatapad_Instance!.Update();
                    }
                    else
                    {
                        TForm1_Proc_0052F414(Form1, false);
                    }

                    TForm1_Proc_0052F464(Form1, false);
                    AlliedVariables.s_TDatapad_Instance!.FGPages.Update();
                    break;
                }

            case Form1OverallPagesEnum.Teams:
                {
                    TForm1_Proc_0052F518(Form1, false);
                    ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(AlliedVariables.s_TDatapad_Instance!.FGPages, 0x08), true);
                    ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(AlliedVariables.s_TDatapad_Instance!.FGPages, 0x09), false);
                    ComCtrls_TPageControl_SetActivePage(AlliedVariables.s_TDatapad_Instance!.FGPages, AlliedVariables.s_TDatapad_Instance!.GGoals);
                    DatapadWindowImpl.TDatapad_FGPagesChange(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_TDatapad_Instance!.FGPages);
                    Form1.TeamIFFBox1.SelectedIndex = AlliedVariables.s_V0x00543C78[AlliedVariables.s_V0x00543B18];
                    TForm1_Proc_0052F414(Form1, true);
                    TForm1_Proc_0052F464(Form1, false);
                    AlliedVariables.s_TDatapad_Instance!.FGPages.Update();
                    break;
                }
        }

        AlliedVariables.s_V0x005B704C = 0x01;
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052F518
    private static void TForm1_Proc_0052F518(Form1Window Form1, bool dl0)
    {
        if (dl0 != (Form1.MapEditWPBtn.IsChecked != true))
        {
            return;
        }

        ComCtrls_TToolButton_SetDown(Form1.MapEditWPBtn, dl0);
        TForm1_MapEditWPBtnClick(Form1, Form1.MapEditWPBtn);

        if (dl0)
        {
            AlliedVariables.s_TWPform_Instance!.Update();
        }
    }

    // L0051C03C
    private static void Unit_00513838_Proc_0051C03C(DatapadFGPageEnum eax0)
    {
        Controls_TControl_SetTop(AlliedVariables.s_TCondToolForm_Instance!, (int)AlliedVariables.s_AlliedForm1Window!.Top + 0xB4);
        Controls_TControl_SetLeft(AlliedVariables.s_TCondToolForm_Instance!, (int)AlliedVariables.s_AlliedForm1Window!.Left + 0x167);

        switch (eax0)
        {
            case DatapadFGPageEnum.Arrival:
                Controls_TControl_SetHeight(AlliedVariables.s_TCondToolForm_Instance!, 0x118);
                Buttons_L00467140_SetVisible(AlliedVariables.s_TCondToolForm_Instance!, true);
                break;

            case DatapadFGPageEnum.Departure:
                Controls_TControl_SetHeight(AlliedVariables.s_TCondToolForm_Instance!, 0xB7);
                Buttons_L00467140_SetVisible(AlliedVariables.s_TCondToolForm_Instance!, true);
                break;

            case DatapadFGPageEnum.Jump:
                Controls_TControl_SetHeight(AlliedVariables.s_TCondToolForm_Instance!, 0xB7);
                Buttons_L00467140_SetVisible(AlliedVariables.s_TCondToolForm_Instance!, true);
                break;
        }
    }

    // L0052F744
    private static void TForm1_Region11Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x00543C9A = 0;
        AlliedVariables.s_CurrentRegion = AlliedGetControlTag(Sender);

        if (AlliedVariables.s_V0x005B6D17 != 0)
        {
            AlliedVariables.s_TOrderSel_Instance!.RegionTabs.SelectedIndex = AlliedVariables.s_CurrentRegion - 1;
        }

        switch (AlliedVariables.s_CurrentRegion)
        {
            case 0x01:
                ComCtrls_TToolButton_SetDown(Form1.R1btn, true);
                break;

            case 0x02:
                ComCtrls_TToolButton_SetDown(Form1.R2btn, true);
                break;

            case 0x03:
                ComCtrls_TToolButton_SetDown(Form1.R3Btn, true);
                break;

            case 0x04:
                ComCtrls_TToolButton_SetDown(Form1.R4Btn, true);
                break;
        }

        switch (AlliedVariables.s_CurrentRegion)
        {
            case 0x01:
                Menus_TMenuItem_SetChecked(Form1.Region11, true);
                break;

            case 0x02:
                Menus_TMenuItem_SetChecked(Form1.Region21, true);
                break;

            case 0x03:
                Menus_TMenuItem_SetChecked(Form1.Region31, true);
                break;

            case 0x04:
                Menus_TMenuItem_SetChecked(Form1.Region41, true);
                break;
        }

        byte bl = AlliedVariables.s_V0x00543B54;
        DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_CurrentOrderInRegion);
        AlliedVariables.s_V0x00543B54 = bl;
        Unit_00513838_Proc_0051E43C(false, AlliedVariables.s_CurrentRegion);
        AlliedVariables.s_V0x00543C9A = 0x01;
        TForm1_Proc_0052D968(Form1, AlliedVariables.s_V0x005AFCBC!);

        if (AlliedVariables.s_FlightGroupObjectsList.Count > AlliedVariables.s_V0x005B7050)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x005B7050);
            if (eax1.m00147A != 0)
            {
                AlliedVariables.s_V0x005AFCBC!.RenderOpen();
                TForm1_Proc_0052DC20(Form1, AlliedVariables.s_V0x005B7050, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005AFCBC)!);
                AlliedVariables.s_V0x005AFCBC!.RenderClose();
            }
        }

        TForm1_FitBattleBtnClick(Form1, Form1.FitBattleBtn);

        if (AlliedVariables.s_BlackenChkSetting)
        {
            Unit_00513838_Proc_0051DB68();
        }

        AlliedVariables.s_V0x00543C9A = 0x01;
    }

    // L0052D1FC
    private static void TForm1_SelectionBoxChange(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x005B7050 = AlliedVariables.s_AlliedForm1Window!.SelectionBox.SelectedIndex;

        if (Sender == Form1.SelectionBox)
        {
            TForm1_Proc_0052ED7C(Form1, true);
        }

        if (Form1.CurrOnly.IsChecked == true)
        {
            TForm1_CurrOnlyClick(Form1, Form1.CurrOnly);
        }

        if (AlliedVariables.s_CenteringSetting)
        {
            MapWindowImpl.TMapForm__PROC_004F7A4C(AlliedVariables.s_TMapForm_Instance!);
        }
        else
        {
            TForm1_Proc_0052D3F8(Form1);
        }
    }

    // L0052BB40
    private static void TForm1_OverallPagesChanging(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x005B704C = 0;

        Form1OverallPagesEnum edx1 = (Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag);
        AlliedVariables.s_V0x005B7088[(int)edx1] = (int)Form1.OverallPages.ActualWidth;

        switch (edx1)
        {
            case Form1OverallPagesEnum.FlightGroups:
                Controls_TControl_SetVisible(Form1.ShipList, false);
                break;

            case Form1OverallPagesEnum.Messages:
                Controls_TControl_SetVisible(Form1.MsgStrList, false);
                break;

            case Form1OverallPagesEnum.Teams:
                Controls_TControl_SetVisible(Form1.ControlBar2, true);
                break;
        }

        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                {
                    AlliedVariables.s_V0x005B7048 = AlliedVariables.s_TDatapad_Instance!.M000057();
                    AlliedVariables.s_V0x005B704A = AlliedVariables.s_TOrderSel_Instance!.M000057();
                    AlliedVariables.s_V0x005B704B = Form1.MapEditWPBtn.IsChecked == true;
                    break;
                }

            case Form1OverallPagesEnum.Teams:
                {
                    bool bl = AlliedVariables.s_V0x00543B53;
                    byte esp00 = AlliedVariables.s_V0x00543B55;
                    Unit_00513838_Proc_005183C0();
                    Unit_00513838_Proc_005187A0();
                    Unit_00513838_Proc_00518460();

                    if (Form1.EndMsgWav.IsChecked == true)
                    {
                        Unit_00513838_Proc_0051477C();
                    }

                    AlliedVariables.s_V0x00543B53 = bl;
                    Form1.SaveBtn.IsEnabled = bl;
                    AlliedVariables.s_V0x00543B55 = esp00;
                    break;
                }
        }
    }

    // L0052F3D4
    private static void TForm1_TextSections1Click(Form1Window Form1, object? Sender)
    {
        ComCtrls_TToolButton_SetDown(Form1.DescBtn, Form1.DescBtn.IsChecked != true);
        TForm1_DescBtnClick(Form1, Form1.DescBtn);
        Menus_TMenuItem_SetChecked(Form1.TextSections1, Form1.DescBtn.IsChecked == true);
    }

    // L0052BCAC
    private static void TForm1_DescBtnClick(Form1Window Form1, object? Sender)
    {
        if (Form1.DescBtn.IsChecked == true)
        {
            if (AlliedVariables.s_V0x00535E8C == 0)
            {
                AlliedVariables.s_TMemoForm_Instance = MainImpl.CreateMemoWindow();
                AlliedVariables.s_TMemoForm_Instance.Owner = Application.Current.MainWindow;
            }

            TForm1_Proc_0052BD6C(Form1, AlliedVariables.s_TMemoForm_Instance!, AlliedVariables.s_V0x005AFC74.m000000);
            TApplication_BringToFront(AlliedVariables.s_TMemoForm_Instance!);
        }
        else
        {
            if (AlliedVariables.s_V0x00535E8C != 0)
            {
                AlliedVariables.s_TMemoForm_Instance?.Close();
                AlliedVariables.s_TMemoForm_Instance = null;
            }
        }

        // tood: upgraded
        Menus_TMenuItem_SetChecked(Form1.TextSections1, Form1.DescBtn.IsChecked == true);
    }

    // L0052A600
    public static void TForm1_Proc_0052A600(Form1Window eax0, TFixedString edx0, TextBox ecx0)
    {
        if (string.IsNullOrEmpty(edx0.Text))
        {
            ecx0.Text = string.Empty;
            return;
        }

        char[] ebp08 = new char[edx0.MaxLength];

        int esi = 0;
        int length = Math.Min(edx0.Text.Length, 0xFFF);

        for (int edx = 0; edx < length; edx++)
        {
            char cl = edx0.Text[edx];

            if (cl == '$')
            {
                if (esi < 0xFFE)
                {
                    ebp08[esi] = '\r';

                    if (esi < 0xFFD)
                    {
                        ebp08[esi + 1] = '\n';
                    }

                    esi += 2;
                }
            }
            else
            {
                if (esi <= 0xFFE)
                {
                    ebp08[esi] = cl;
                    esi++;
                }
            }
        }

        ebp08[esi] = '\0';
        ecx0.Text = new string(ebp08, 0, esi);
    }

    // L0052BD20
    public static void TForm1_L0052BD20(Form1Window Form1, Window A8, S0x005342B0_00000C AC)
    {
        AC.Top = (short)A8.Top;
        AC.Left = (short)A8.Left;
        AC.Height = (short)A8.Height;
        AC.Width = (short)A8.Width;
    }

    // L0052EB20
    private static void TForm1_MapEditWPBtnClick(Form1Window Form1, object? Sender)
    {
        if (Form1.MapEditWPBtn.IsChecked == true)
        {
            if (AlliedVariables.s_V0x005B6D18 == 0)
            {
                AlliedVariables.s_TWPform_Instance = MainImpl.CreateWaypointsWindow();
                AlliedVariables.s_TWPform_Instance.Owner = Application.Current.MainWindow;
            }

            TForm1_Proc_0052BD6C(Form1, AlliedVariables.s_TWPform_Instance!, AlliedVariables.s_V0x005AFC74.m000030);
            TApplication_BringToFront(AlliedVariables.s_TWPform_Instance!);
        }
        else
        {
            if (AlliedVariables.s_V0x005B6D18 != 0)
            {
                AlliedVariables.s_TWPform_Instance?.Close();
                AlliedVariables.s_TWPform_Instance = null;
            }
        }

        Menus_TMenuItem_SetChecked(Form1.WPEditor1, Form1.MapEditWPBtn.IsChecked == true);
    }

    // L0052F9A0
    private static void TForm1_WPEditor1Click(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_0052F518(Form1, Form1.MapEditWPBtn.IsChecked != true);
    }

    // L00526C0C
    private static void TForm1_ShipListClick(Form1Window Form1, object? Sender)
    {
        if ((Form1.ShipList.SelectedIndex != AlliedVariables.s_V0x00543B0C && AlliedVariables.s_V0x00543B0C < AlliedVariables.s_FlightGroupObjectsList.Count)
            || (Form1.ShipList.SelectedIndex == 0 && AlliedVariables.s_V0x00543B0C == 0))
        {
            if (AlliedVariables.s_V0x00543B54 != 0)
            {
                AlliedVariables.s_FlightGroupObjectsList[AlliedVariables.s_V0x00543B0C] = AlliedVariables.s_V0x005AFE90.Clone();
                Unit_00513838_Proc_005146A4();
                AlliedVariables.s_V0x00543B0C = Form1.ShipList.SelectedIndex;
            }

            Unit_00513838_Proc_0051950C(true);
            Form1.SelectionBox.SelectedIndex = AlliedVariables.s_V0x00543B0C;
            TForm1_SelectionBoxChange(Form1, Form1.ShipList);
        }
    }

    // L0052C1A4
    private static void TForm1_ShipListDblClick(Form1Window Form1, object? Sender)
    {
        if (!AlliedVariables.s_ResChkSetting)
        {
            return;
        }

        TForm1_Proc_0052F414(Form1, true);
    }

    // L00530368
    private static void TForm1_ShipListMouseUp(Form1Window Form1, object? Sender, int A4, int A8, TShiftState AC)
    {
        int esi = Form1.ShipList.Items.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            if (StdCtrls_TCustomListBox_GetSelected(Form1.ShipList, ebx))
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.m001479 = 0x01;
            }
            else
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.m001479 = 0;
            }
        }

        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052B6A8
    public static void TForm1_FGPagesChange(Form1Window Form1, object? Sender)
    {
    }

    // L0052769C
    public static void TForm1_Proc_0052769C(Form1Window eax0, S0xTieFlightGroup edx0)
    {
        DatapadWindow edi = AlliedVariables.s_TDatapad_Instance!;

        if (AlliedVariables.s_AlliedDatapadCurrentPage != DatapadFGPageEnum.Options)
        {
            edi.OpShipBox.SetItems(AlliedVariables.s_Strings_Ships);
            DatapadWindowImpl.Unit_00513838_Proc_0051B1C8();
        }

        edi.OrientationBox.SelectedIndex = edx0.Pitch;

        int ebx1 = edi.MissleSelBox.Items.Count;

        for (int esi = 0; esi < ebx1; esi++)
        {
            StdCtrls_TCustomListBox_SetSelected(edi.MissleSelBox, esi, false);
        }

        int ebx2 = edi.MissleSelBox.Items.Count;

        for (int esi = 0; esi < ebx2; esi++)
        {
            byte al = edx0.OptionalWarheads[esi];

            if (al == 0)
            {
                continue;
            }

            StdCtrls_TCustomListBox_SetSelected(edi.MissleSelBox, al - 1, true);
        }

        int ebx3 = edi.BeamSelBox.Items.Count;

        for (int esi = 0; esi < ebx3; esi++)
        {
            StdCtrls_TCustomListBox_SetSelected(edi.BeamSelBox, esi, false);
        }

        int ebx4 = edi.BeamSelBox.Items.Count;

        for (int esi = 0; esi < ebx4; esi++)
        {
            byte al = edx0.OptionalBeams[esi];

            if (al == 0)
            {
                continue;
            }

            StdCtrls_TCustomListBox_SetSelected(edi.BeamSelBox, al - 1, true);
        }

        int ebx5 = edi.CounterSelbox.Items.Count;

        for (int esi = 0; esi < ebx5; esi++)
        {
            StdCtrls_TCustomListBox_SetSelected(edi.CounterSelbox, esi, false);
        }

        int ebx6 = edi.CounterSelbox.Items.Count;

        for (int esi = 0; esi < ebx6; esi++)
        {
            byte al = edx0.OptionalCounterMeasures[esi];

            if (al == 0)
            {
                continue;
            }

            StdCtrls_TCustomListBox_SetSelected(edi.CounterSelbox, al - 1, true);
        }

        Allied_ComboBox_SetSelectedIndex(edi.OpShipTypes, edx0.OptionalCraftCategory);

        edi.OpFGList.Clear();

        if (edx0.OptionalCraftCategory == 0x04)
        {
            DatapadWindowImpl.Unit_00513838_Proc_00515E38();
        }
    }

    // L005278A0
    public static void TForm1_Proc_005278A0(Form1Window eax0, S0xTieFlightGroup edx0)
    {
        DatapadWindow ebx = AlliedVariables.s_TDatapad_Instance!;

        if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftId == CraftIdEnum._183_9001_1100_ResData_Backdrop)
        {
            Controls_TControl_SetText(ebx.Label20, "(shadow)");
        }
        else if (AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftId != CraftIdEnum._183_9001_1100_ResData_Backdrop)
        {
            Controls_TControl_SetText(ebx.Label20, "(cargo)");
        }

        ebx.UseRole1.IsChecked = edx0.TacticalRoleUsed0 == TacticalRoleUsedEnum.NoTC;
        ebx.UseRole2.IsChecked = edx0.TacticalRoleUsed1 == TacticalRoleUsedEnum.NoTC;
        ebx.RoleBox.SelectedIndex = (int)edx0.TacticalRole0;
        Controls_TControl_SetText(ebx.Edit1, ((int)edx0.TacticalRoleUsed0).ToString(CultureInfo.InvariantCulture));
        ebx.UseRole1.IsChecked = edx0.TacticalRoleUsed0 == TacticalRoleUsedEnum.NoTC;
        Controls_TControl_SetText(ebx.Edit2, ((int)AlliedVariables.s_V0x005AFE90.FlightGroupStruct.TacticalRoleUsed1).ToString(CultureInfo.InvariantCulture));
        ebx.Role2Box.SelectedIndex = (int)edx0.TacticalRole1;
        Controls_TControl_SetText(ebx.Edit5, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Comm.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(ebx.Edit6, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.GlobalCargoIndex.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(ebx.Edit7, AlliedVariables.s_V0x005AFE90.FlightGroupStruct.SpecialCargoIndex.ToString(CultureInfo.InvariantCulture));

        if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XvT || AlliedVariables.s_TieFileVersion == TieFileVersionEnum.Bop)
        {
            Controls_TControl_SetText(ebx.OrderDescEd, edx0.Role.WithMaxLength(0x14));
        }
        else if (AlliedVariables.s_TieFileVersion == TieFileVersionEnum.XWA)
        {
            Controls_TControl_SetText(ebx.OrderDescEd, edx0.PilotVoice.WithMaxLength(0x10));
        }
    }

    // L00526E4C
    public static void TForm1_Proc_00526E4C(Form1Window eax0, S0xTieRadioMessage edx0)
    {
        S0xTieRadioMessage ebpA6 = S0xTieRadioMessage.FromByteArray(edx0.ToByteArray());
        DatapadWindow esi = AlliedVariables.s_TDatapad_Instance!;

        if (AlliedVariables.s_V0x005B6D15 != 0)
        {
            bool ebx = AlliedVariables.s_V0x00543B53;

            AlliedVariables.s_V0x00543C9B = 0;
            string ebp04 = Unit_00513838_Proc_005157D0(System_LStrFromPCharLen(ebpA6.Message, 0x40), 0x79);
            Controls_TControl_SetText(esi.MsgtextEd, ebp04);
            esi.MsgtextEd.Update();
            Graphics_TFont_SetColor(esi.MsgtextEd, AlliedGetIffColor(ebpA6.Side, 0));
            Allied_ComboBox_SetSelectedIndex(esi.MsgColorBox, ebpA6.Side);
            esi.MsgDelaySpin.Value = ebpA6.TimePassed;
            string ebpC4_4 = Allied_TimeInSeconds_ToMinutesSecondsString(Allied_Time_ToSeconds_L0051F534(ebpA6.TimePassed));
            Controls_TControl_SetText(esi.MsgDelayLab, "Delay:  " + ebpC4_4);
            Unit_00513838_Proc_00518460();

            if ((DatapadFGPageEnum)Convert.ToInt32(esi.FGPages.GetActivePage().Tag) == DatapadFGPageEnum.Messages)
            {
                CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, ebpA6.Condition.Trigger_0[0]);
                CondToolUserControlImpl.TCondToolForm__PROC_0051009C(AlliedVariables.s_TCondToolForm_Instance!, ebpA6.Operator, ebpA6.Condition.Operator_0, ebpA6.Condition.Operator_1);
                CondToolUserControlImpl.TCondToolForm_Proc_0050F450(AlliedVariables.s_TCondToolForm_Instance!, ebpA6.Condition);
                DatapadWindowImpl.TDatapad_SetTitle(esi);
            }

            AlliedVariables.s_V0x00543B55 = 0;
            AlliedVariables.s_V0x00543B53 = ebx;

            Controls_TControl_SetText(esi.MsgUnk3, ebpA6.Fg.ToString(CultureInfo.InvariantCulture));
            Controls_TControl_SetText(esi.Cond5Lab, Unit_00511CD0_Proc_005122D0(ebpA6.Trigger1));
            Controls_TControl_SetText(esi.Cond6Lab, Unit_00511CD0_Proc_005122D0(ebpA6.Trigger2));
            esi.and5And.IsChecked = ebpA6.TriggersOperator != 0;
            esi.And5Or.IsChecked = !esi.and5And.IsChecked;
            Controls_TControl_SetText(esi.MsgID, System_LStrFromPCharLen(ebpA6.M00007C, 0x08));

            AlliedVariables.s_V0x00543C9B = 0x01;
        }
    }

    // L00527B64
    public static void TForm1_Proc_00527B64(Form1Window eax0, S0xTieFlightGroup edx0)
    {
        DatapadWindow esi = AlliedVariables.s_TDatapad_Instance!;

        if (AlliedVariables.s_AlliedDatapadCurrentPage != DatapadFGPageEnum.Arrival)
        {
            DatapadWindowImpl.TDatapad_Proc_004C2F30(esi);
        }

        if (edx0.StartFgUsed == 0)
        {
            esi.ArrMotherBox.SelectedIndex = 0;
        }
        else
        {
            esi.ArrMotherBox.SelectedIndex = edx0.StartFg + 1;
        }

        if (edx0.SecondaryStopFgUsed == 0)
        {
            esi.ArrAltmotherBox.SelectedIndex = 0;
        }
        else
        {
            esi.ArrAltmotherBox.SelectedIndex = edx0.SecondaryStopFg + 1;
        }

        esi.ArrIfPlrChk.IsChecked = edx0.ArriveOnlyIfPlayer;
        AlliedVariables.s_V0x00543B28 = 0x01;
        CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, edx0.ArrivalTrigger1.Triggers[0]);
        Controls_TControl_SetText(esi.ArrMinSpin, edx0.ArrivalDelayMinutes.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(esi.ArrSecSpin, edx0.ArrivalDelaySeconds.ToString(CultureInfo.InvariantCulture));
        TForm1_Proc_00528D70(eax0);
        CondToolUserControlImpl.TCondToolForm__PROC_0051009C(AlliedVariables.s_TCondToolForm_Instance!, edx0.ArrivalTriggersOperator, edx0.ArrivalTrigger1.Operator, edx0.ArrivalTrigger2.Operator);
        CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x00543B28);
        esi.DifficultyBox.SelectedIndex = (int)edx0.ArrivalDifficulty;
    }

    // L00528D70
    public static void TForm1_Proc_00528D70(Form1Window eax0)
    {
        if (AlliedVariables.s_V0x00543990 == 0)
        {
            return;
        }

        CondToolUserControlImpl.TCondToolForm_Proc_00510018(
            AlliedVariables.s_TCondToolForm_Instance!,
            AlliedVariables.s_TCondToolForm_Instance!.Cond1Lab,
            AlliedVariables.s_TCondToolForm_Instance!.Cond2Lab,
            AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ArrivalTrigger1
            );

        CondToolUserControlImpl.TCondToolForm_Proc_00510018(
            AlliedVariables.s_TCondToolForm_Instance!,
            AlliedVariables.s_TCondToolForm_Instance!.Cond3Lab,
            AlliedVariables.s_TCondToolForm_Instance!.Cond4Lab,
            AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ArrivalTrigger2
            );
    }

    // L00527D44
    public static void TForm1_Proc_00527D44(Form1Window eax0, S0xTieFlightGroup edx0)
    {
        DatapadWindow ebx = AlliedVariables.s_TDatapad_Instance!;

        if (AlliedVariables.s_AlliedDatapadCurrentPage != DatapadFGPageEnum.Departure)
        {
            DatapadWindowImpl.TDatapad_Proc_004C2F30(ebx);
        }

        if (edx0.PrimaryStopFgUsed == 0)
        {
            ebx.ArrMotherBox.SelectedIndex = 0;
        }
        else
        {
            ebx.ArrMotherBox.SelectedIndex = edx0.PrimaryStopFg + 1;
        }

        if (edx0.CaptureFgUsed == 0)
        {
            ebx.ArrAltmotherBox.SelectedIndex = 0;
        }
        else
        {
            ebx.ArrAltmotherBox.SelectedIndex = edx0.CaptureFg + 1;
        }

        Spin_TSpinEdit_SetValue(ebx.ExpTimeSpin, edx0.CraftExplosionTime);
        Controls_TControl_SetText(ebx.ExpLabel, edx0.CraftExplosionTime.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(ebx.DepMin, edx0.DepartureDelayMinutes.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(ebx.DepSec, edx0.DepartureDelaySeconds.ToString(CultureInfo.InvariantCulture));
        AlliedVariables.s_V0x00543B2C = 0x01;
        CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, edx0.DepartureTrigger.Triggers[0]);

        if (AlliedVariables.s_V0x00543990 != 0)
        {
            CondToolUserControlImpl.TCondToolForm_Proc_00510018(
                AlliedVariables.s_TCondToolForm_Instance!,
                AlliedVariables.s_TCondToolForm_Instance!.Cond1Lab,
                AlliedVariables.s_TCondToolForm_Instance!.Cond2Lab,
                edx0.DepartureTrigger
                );

            CondToolUserControlImpl.Unit_00513838_Proc_005143FC(
                AlliedVariables.s_TCondToolForm_Instance!.and2Or,
                AlliedVariables.s_TCondToolForm_Instance!.and2And,
                edx0.DepartureTrigger.Operator
                );
        }

        ebx.DepartWhenBox.SelectedIndex = edx0.AbortCondition;
        CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x00543B2C);
    }

    // L00527F7C
    public static void TForm1_Proc_00527F7C(Form1Window eax0, S0xTieFlightGroup edx0)
    {
        CondToolUserControl esi = AlliedVariables.s_TCondToolForm_Instance!;

        if (AlliedVariables.s_AlliedDatapadCurrentPage != DatapadFGPageEnum.FGGoals && AlliedVariables.s_V0x00543990 != 0)
        {
            AlliedVariables.s_TDatapad_Instance!.FGGoalTeamBox.SetItems(AlliedVariables.s_Strings_Teams);
            esi.PercentBox.SetItems(AlliedVariables.s_V0x00543BC8);
            esi.IndexBox.SetItems(AlliedVariables.s_Strings_Musts);
            esi.CondBox.SetItems(AlliedVariables.s_V0x00543BD4);
        }

        Graphics_TFont_SetColor(esi.FGStrIncomp, 0x0000FFFF);
        Graphics_TFont_SetColor(esi.FGStrSucc, 0x0000FF00);
        Controls_TControl_SetText(esi.Label63, "Success");
        Controls_TControl_SetVisible(esi.FGStrIncomp, true);
        Controls_TControl_SetVisible(esi.Label62, true);
        Controls_TControl_SetVisible(esi.FGStrFail, true);
        Controls_TControl_SetVisible(esi.Label73, true);
        TForm1_Proc_0052870C(eax0);

        esi.FGStrIncomp.Update();
        esi.FGStrFail.Update();
        esi.Label73.Update();
        esi.Label63.Update();
        esi.Label62.Update();

        DatapadWindowImpl.TDatapad__PROC_004C191C(AlliedVariables.s_TDatapad_Instance!);
    }

    // L0052870C
    public static void TForm1_Proc_0052870C(Form1Window eax0)
    {
        if (AlliedVariables.s_V0x00543990 != 0)
        {
            byte ebp01 = AlliedVariables.s_V0x00543B54;
            AlliedVariables.s_V0x005B6B64 = S0xTieFlightGroupGoal.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].ToByteArray());
            AlliedVariables.s_TCondToolForm_Instance!.ClassBox.SelectedIndex = AlliedVariables.s_V0x00543B0C;
            Allied_ComboBox_SetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.FGGoalTeamBox, AlliedVariables.s_V0x005B6B64.AppliesToTeams[1 + AlliedVariables.s_V0x00543B48]);
            Allied_ComboBox_SetSelectedIndex(AlliedVariables.s_TCondToolForm_Instance!.PercentBox, (int)AlliedVariables.s_V0x005B6B64.Amount);
            Allied_ComboBox_SetSelectedIndex(AlliedVariables.s_TCondToolForm_Instance!.IndexBox, (int)AlliedVariables.s_V0x005B6B64.GoalType);
            Allied_ComboBox_SetSelectedIndex(AlliedVariables.s_TCondToolForm_Instance!.CondBox, (int)AlliedVariables.s_V0x005B6B64.Condition);

            switch (AlliedVariables.s_V0x005B6B64.Condition)
            {
                case TieConditionEnum.Within:
                case TieConditionEnum.Beyond:
                    AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.SetItems(AlliedVariables.s_Strings_Regions);
                    AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.SelectedIndex = AlliedVariables.s_V0x005B6B64.Time;
                    break;

                default:
                    AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                    AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.PutItem(0, "0");
                    AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.SelectedIndex = AlliedVariables.s_V0x005B6B64.Time;
                    break;
            }

            AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.SelectedIndex = AlliedVariables.s_V0x005B6B64.Time;

            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
            Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.CondUnk2, eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].SequenceNumber.ToString(CultureInfo.InvariantCulture));
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
            Spin_TSpinEdit_SetValue(AlliedVariables.s_TDatapad_Instance!.FGGoalPointsSpin, eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].Points * 0x19);
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
            AlliedVariables.s_TDatapad_Instance!.FGGoalUnk.IsChecked = eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[0] != 0;
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
            Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.FGGoalTimeSpin, eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[9].ToString(CultureInfo.InvariantCulture));

            if (AlliedVariables.s_V0x00543990 != 0)
            {
                Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.FGStrIncomp, AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000000.WithMaxLength(0x40));
                Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.FGStrSucc, AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000040.WithMaxLength(0x40));
                Controls_TControl_SetText(AlliedVariables.s_TCondToolForm_Instance!.FGStrFail, AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000080.WithMaxLength(0x40));
            }

            //AlliedVariables.s_V0x00543B48 = AlliedVariables.s_V0x00543B48;
            AlliedVariables.s_V0x00543B54 = ebp01;
        }
    }

    // L00525FEC
    public static string TForm1_Proc_00525FEC(Form1Window eax0, S0xTieFlightGroupGoal edx0)
    {
        string ebp04 = string.Empty;

        if (AlliedVariables.s_V0x005B6D15 != 0 && AlliedVariables.s_V0x00543BC8.GetCount() > 0)
        {
            // todo
            if (edx0.Condition == TieConditionEnum.Never || edx0.Condition == TieConditionEnum.Always)
            {
                ebp04 = "<None>";
            }
            else
            {
                ebp04 = AlliedVariables.s_V0x00543BC8.GetText((int)edx0.Amount) + " of it " + AlliedVariables.s_Strings_Musts.GetText((byte)edx0.GoalType) + " ";
                ebp04 += AlliedVariables.s_V0x00543BD4.GetText((int)edx0.Condition);

                if (edx0.Condition == TieConditionEnum.Arrival || edx0.Condition == TieConditionEnum.Departure)
                {
                    ebp04 += edx0.Time.ToString(CultureInfo.InvariantCulture);
                }

                if (edx0.Condition < TieConditionEnum.Carried)
                {
                    ebp04 += " " + AlliedVariables.s_Strings_Regions.GetText(edx0.Time);
                }

                if (edx0.Points != 0)
                {
                    string ebp84_2 = Runtime_L0040A8D4_FloatToText(0, (double)edx0.Points * 25.0f);
                    ebp04 += " : " + ebp84_2 + " Pts";
                }

                ebp04 += "  - Team " + AlliedVariables.s_Strings_Teams.GetText(edx0.AppliesToTeams[1]);
            }
        }

        return ebp04;
    }

    // L00528118
    public static void TForm1_Proc_00528118(Form1Window eax0, S0xTieFlightGroup edx0)
    {
        CondToolUserControl ebx = AlliedVariables.s_TCondToolForm_Instance!;

        string ebpE44 = string.Format(CultureInfo.InvariantCulture, "...to order {0} in R{1}", AlliedVariables.s_CurrentOrderInRegion, AlliedVariables.s_CurrentRegion);
        Controls_TControl_SetText(AlliedVariables.s_TDatapad_Instance!.JumpLab, ebpE44);

        CondToolUserControlImpl.TCondToolForm_Proc_00510018(
            ebx,
            ebx.Cond1Lab,
            ebx.Cond2Lab,
            edx0.JumpTriggers[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)]);

        CondToolUserControlImpl.Unit_00513838_Proc_005143FC(
            ebx.and2Or,
            ebx.and2And,
            edx0.JumpTriggers[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Operator);

        if (AlliedVariables.s_AlliedDatapadCurrentPage == DatapadFGPageEnum.Jump)
        {
        }

        AlliedVariables.s_V0x00543B4C = 0x01;

        CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(ebx, edx0.JumpTriggers[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Triggers[0]);
        CondToolUserControlImpl.TCondToolForm_PROC_0051015C(ebx, 0x01);
        Unit_00513838_Proc_00519644();
    }

    // L00519644
    private static void Unit_00513838_Proc_00519644()
    {
        string ebp08 = string.Empty;
        byte[] array = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ToByteArray();

        for (int ebx = 0; ebx < 0x14; ebx++)
        {
            ebp08 += array[0x14 + ebx].ToString(CultureInfo.InvariantCulture) + ", ";
        }
    }

    // L0051C0F0
    private static void Unit_00513838_Proc_0051C0F0(Form1OverallPagesEnum al0)
    {
        switch (al0)
        {
            case Form1OverallPagesEnum.Messages:
                Controls_TControl_SetTop(AlliedVariables.s_TCondToolFormWindow_Instance!, AlliedVariables.s_AlliedForm1Window!.GetTop() + 0x91);
                Controls_TControl_SetLeft(AlliedVariables.s_TCondToolFormWindow_Instance!, AlliedVariables.s_AlliedForm1Window!.GetLeft() + 0x186);
                Controls_TControl_SetHeight(AlliedVariables.s_TCondToolFormWindow_Instance!, 0x118);
                Buttons_L00467140_SetVisible(AlliedVariables.s_TCondToolFormWindow_Instance!, true);
                break;

            case Form1OverallPagesEnum.Teams:
                Controls_TControl_SetTop(AlliedVariables.s_TCondToolFormWindow_Instance!, AlliedVariables.s_AlliedForm1Window!.GetTop() + 0xA0);
                Controls_TControl_SetLeft(AlliedVariables.s_TCondToolFormWindow_Instance!, AlliedVariables.s_AlliedForm1Window!.GetLeft() + 0x19);
                Controls_TControl_SetHeight(AlliedVariables.s_TCondToolFormWindow_Instance!, 0x118);
                Buttons_L00467140_SetVisible(AlliedVariables.s_TCondToolFormWindow_Instance!, true);
                break;
        }
    }

    // L005183C0
    public static void Unit_00513838_Proc_005183C0()
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

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < esi; AlliedVariables.s_V0x00543CC0 += 1)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
            {
                continue;
            }

            for (int ebx = 0; ebx < 0x08; ebx++)
            {
                S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                eax1.RadioMessage.ForTeam[ebx] = StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_TDatapad_Instance!.SeenByList, ebx) ? (byte)1 : (byte)0;
            }
        }
    }

    // L00528DFC
    public static void TForm1_Proc_00528DFC(Form1Window Form1)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            int esi = Form1.ShipList.Items.Count;

            for (int ebx = 0; ebx < esi; ebx++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(Form1.ShipList, ebx))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].Amount = (TieAmountEnum)AlliedVariables.s_TCondToolForm_Instance!.PercentBox.SelectedIndex;
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].GoalType = (TieFGGoalTypeEnum)AlliedVariables.s_TCondToolForm_Instance!.IndexBox.SelectedIndex;
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].Condition = (TieConditionEnum)AlliedVariables.s_TCondToolForm_Instance!.CondBox.SelectedIndex;
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[1] = (byte)AlliedVariables.s_TDatapad_Instance!.FGGoalTeamBox.SelectedIndex;

                switch ((TieConditionEnum)AlliedVariables.s_TCondToolForm_Instance!.CondBox.SelectedIndex)
                {
                    case TieConditionEnum.Within:
                    case TieConditionEnum.Beyond:
                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].Time = (byte)AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.SelectedIndex;
                        break;

                    default:
                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                        eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].Time = (byte)AlliedVariables.s_TCondToolForm_Instance!.RegionFGBox.SelectedIndex;
                        break;
                }

                string ebp04 = Controls_TControl_GetText(AlliedVariables.s_TCondToolForm_Instance!.CondUnk2);
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].SequenceNumber = (byte)StrRec_try_to_int_L0051E3BC(ebp04);

                AlliedVariables.s_V0x00543C9A = 0;

                if (AlliedVariables.s_TDatapad_Instance!.FGGoalTeamBox.SelectedIndex == 0)
                {
                    eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[0] = 0x01;
                    AlliedVariables.s_TDatapad_Instance!.FGGoalUnk.IsChecked = true;
                }
                else
                {
                    eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                    eax1.FlightGroupStruct.Goals[AlliedVariables.s_V0x00543B48].AppliesToTeams[0] = 0;
                    AlliedVariables.s_TDatapad_Instance!.FGGoalUnk.IsChecked = false;
                }

                AlliedVariables.s_V0x00543C9A = 0x01;
            }

            DatapadWindowImpl.TDatapad__PROC_004C191C(AlliedVariables.s_TDatapad_Instance!);
            Unit_00513838_Proc_0051467C();
        }
    }

    // L0051BD20
    private static void Unit_00513838_Proc_0051BD20(Form1Window ebx)
    {
        if (AlliedVariables.s_AlliedAplicationWidth == 0x280 || AlliedVariables.s_AlliedAplicationHeight == 0x1E0)
        {
            Controls_TControl_SetAlign(AlliedVariables.s_AlliedForm1Window!.OverallPages, TAlignEnum.Client);
            Controls_TControl_SetLeft(AlliedVariables.s_AlliedForm1Window!.OverallPages, AlliedVariables.s_AlliedForm1Window.OverallPages.GetLeft() + 0x0C);
            Graphics_TFont_SetName(AlliedVariables.s_AlliedForm1Window!.OverallPages, "MS Serif");
            Graphics_TFont_SetSize(AlliedVariables.s_AlliedForm1Window!.OverallPages, 0x09);
            Graphics_TFont_SetPitch(AlliedVariables.s_AlliedForm1Window!.OverallPages, 0);
            return;
        }

        if (AlliedVariables.s_AlliedAplicationWidth == 0x400 || AlliedVariables.s_AlliedAplicationHeight == 0x300)
        {
            TApplication_L00468080(ebx, 0x01);
            StdCtrls_TCustomListBox_SetItemHeight(AlliedVariables.s_AlliedForm1Window!.ShipList, 0x0E);
            StdCtrls_TCustomListBox_SetItemHeight(AlliedVariables.s_AlliedForm1Window!.MsgStrList, 0x0E);
            StdCtrls_TCustomListBox_SetItemHeight(AlliedVariables.s_TDatapad_Instance!.FGgoalList, 0x10);
            Graphics_TFont_SetSize(AlliedVariables.s_AlliedForm1Window!.ShipList, 0x07);
            Graphics_TFont_SetSize(AlliedVariables.s_AlliedForm1Window!.MsgStrList, 0x07);
            Controls_TControl_SetHeight(ebx, Unit_00513838_Proc_0051C570((int)ebx.ActualHeight, 0));
            Controls_TControl_SetWidth(ebx, Unit_00513838_Proc_0051C570((int)ebx.ActualHeight, 0x01));
            Controls_TWinControl_ScaleBy(ebx, AlliedVariables.s_AlliedAplicationWidth, 0x320);
        }
    }

    // L0052BC44
    private static void TForm1_MissionBtnClick(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_THeaderForm_Instance = MainImpl.CreateHeaderBox();
        AlliedVariables.s_THeaderForm_Instance.Owner = Form1;
        AlliedVariables.s_THeaderForm_Instance.ShowDialog();
        AlliedVariables.s_THeaderForm_Instance = null;
    }

    // L0052B408
    private static void TForm1_AboutBtnClick(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_0052F114(Form1, 0x01);
    }

    // L0052B414
    private static void TForm1_OptClick(Form1Window Form1, object? Sender)
    {
    }

    // L0052BC40
    private static void TForm1_OrderSelectChange(Form1Window Form1, object? Sender)
    {
    }

    // L0052BCA4
    private static void TForm1_Edit1Change(Form1Window Form1, object? Sender)
    {
        DatapadWindowImpl.Unit_00513838_Proc_0051E2AC(Sender);
    }

    // L00528AE8
    public static void TForm1_Proc_00528AE8(Form1Window Form1)
    {
        int esp08 = Form1.ShipList.Items.Count;

        for (int esi = 0; esi < esp08; esi++)
        {
            if (!StdCtrls_TCustomListBox_GetSelected(Form1.ShipList, esi))
            {
                continue;
            }

            byte esp05 = 0;
            byte esp06 = 0;
            byte esp07 = 0;

            byte bl = (byte)AlliedVariables.s_TDatapad_Instance!.MissleSelBox.Items.Count;

            for (byte esp04 = 0; esp04 < bl; esp04++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_TDatapad_Instance!.MissleSelBox, esp04))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.FlightGroupStruct.OptionalWarheads[esp05] = (byte)(esp04 + 1);
                esp05++;
            }

            bl = (byte)AlliedVariables.s_TDatapad_Instance!.MissleSelBox.Items.Count;

            for (byte esp04 = esp05; esp04 < bl; esp04++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.FlightGroupStruct.OptionalWarheads[esp04] = 0;
            }

            bl = (byte)AlliedVariables.s_TDatapad_Instance!.BeamSelBox.Items.Count;

            for (byte esp04 = 0; esp04 < bl; esp04++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_TDatapad_Instance!.BeamSelBox, esp04))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.FlightGroupStruct.OptionalBeams[esp06] = (byte)(esp04 + 1);
                esp06++;
            }

            bl = (byte)AlliedVariables.s_TDatapad_Instance!.BeamSelBox.Items.Count;

            for (byte esp04 = esp06; esp04 < bl; esp04++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.FlightGroupStruct.OptionalBeams[esp04] = 0;
            }

            bl = (byte)AlliedVariables.s_TDatapad_Instance!.CounterSelbox.Items.Count;

            for (byte esp04 = 0; esp04 < bl; esp04++)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_TDatapad_Instance!.CounterSelbox, esp04))
                {
                    continue;
                }

                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.FlightGroupStruct.OptionalCounterMeasures[esp07] = (byte)(esp04 + 1);
                esp07++;
            }

            bl = (byte)AlliedVariables.s_TDatapad_Instance!.CounterSelbox.Items.Count;

            for (byte esp04 = esp07; esp04 < bl; esp04++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                eax1.FlightGroupStruct.OptionalCounterMeasures[esp04] = 0;
            }
        }
    }

    // L0052B144
    public static S0xTieFlightGroupOrder TForm1_Proc_0052B144(Form1Window Form1)
    {
        S0xTieFlightGroupOrder ebp98 = new();

        ebp98.OrderId = (TieOrderIdEnum)AlliedVariables.s_TDatapad_Instance!.OrderBox.SelectedIndex;
        ebp98.Throttle = (byte)AlliedVariables.s_TDatapad_Instance!.OrderSpeedBox.SelectedIndex;

        if (AlliedVariables.s_TDatapad_Instance!.OrderBox.SelectedIndex == 0x32)
        {
            ebp98.Var0 = (byte)(Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.OrderP1) - 1);
        }
        else
        {
            ebp98.Var0 = (byte)Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.OrderP1);
        }

        ebp98.Var1 = (byte)Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.OrderP2);
        ebp98.Var2 = (byte)Spin_TSpinEdit_GetValue(AlliedVariables.s_TDatapad_Instance!.OrderP3);
        ebp98.SecondaryTarget.ClassA = (TieClassEnum)AlliedVariables.s_TDatapad_Instance!.T3Class.SelectedIndex;

        if (AlliedVariables.s_TDatapad_Instance!.T3Class.SelectedIndex == 0x02)
        {
            ebp98.SecondaryTarget.ParameterA = (byte)AlliedConvertShipSeqToCraftId((ShipSeqEnum)AlliedVariables.s_TDatapad_Instance!.T3Index.SelectedIndex);
        }
        else
        {
            ebp98.SecondaryTarget.ParameterA = (byte)AlliedVariables.s_TDatapad_Instance!.T3Index.SelectedIndex;
        }

        ebp98.SecondaryTarget.ClassB = (TieClassEnum)AlliedVariables.s_TDatapad_Instance!.T4Class.SelectedIndex;

        if (AlliedVariables.s_TDatapad_Instance!.T4Class.SelectedIndex == 0x02)
        {
            ebp98.SecondaryTarget.ParameterB = (byte)AlliedConvertShipSeqToCraftId((ShipSeqEnum)AlliedVariables.s_TDatapad_Instance!.T4Index.SelectedIndex);
        }
        else
        {
            ebp98.SecondaryTarget.ParameterB = (byte)AlliedVariables.s_TDatapad_Instance!.T4Index.SelectedIndex;
        }

        ebp98.SecondaryTarget.Operator = AlliedVariables.s_TDatapad_Instance!.ThreeAnd4Chk.IsChecked != true ? (byte)1 : (byte)0;
        ebp98.SecondaryTarget.m000005 = 0;
        ebp98.PrimaryTarget.ClassA = (TieClassEnum)AlliedVariables.s_TDatapad_Instance!.T1Class.SelectedIndex;

        if (AlliedVariables.s_TDatapad_Instance!.T1Class.SelectedIndex == 0x02)
        {
            ebp98.PrimaryTarget.ParameterA = (byte)AlliedConvertShipSeqToCraftId((ShipSeqEnum)AlliedVariables.s_TDatapad_Instance!.T1Index.SelectedIndex);
        }
        else
        {
            ebp98.PrimaryTarget.ParameterA = (byte)AlliedVariables.s_TDatapad_Instance!.T1Index.SelectedIndex;
        }

        ebp98.PrimaryTarget.ClassB = (TieClassEnum)AlliedVariables.s_TDatapad_Instance!.T2Class.SelectedIndex;

        if (AlliedVariables.s_TDatapad_Instance!.T2Class.SelectedIndex == 0x02)
        {
            ebp98.PrimaryTarget.ParameterB = (byte)AlliedConvertShipSeqToCraftId((ShipSeqEnum)AlliedVariables.s_TDatapad_Instance!.T2Index.SelectedIndex);
        }
        else
        {
            ebp98.PrimaryTarget.ParameterB = (byte)AlliedVariables.s_TDatapad_Instance!.T2Index.SelectedIndex;
        }

        ebp98.PrimaryTarget.Operator = AlliedVariables.s_TDatapad_Instance!.OneAnd2Chk.IsChecked != true ? (byte)1 : (byte)0;
        ebp98.PrimaryTarget.m000005 = 0;
        ebp98.SpeedMph = (byte)AlliedVariables.s_TDatapad_Instance!.MGLTBox.SelectedIndex;

        ebp98.Waypoints = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].Waypoints.ToArray();
        ebp98.m000054 = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (AlliedVariables.s_CurrentOrderInRegion - 1)].m000054.ToArray();

        return ebp98;
    }

    // L0051FEF4
    private static void Unit_00513838_Proc_0051FEF4(int eax0)
    {
        bool esp10 = false;
        bool bl = false;

        S0xFGObject edi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
        int esi = 0x01;

        for (; esi <= 0x04; esi++)
        {
            for (int eax = 0x01; eax <= 0x04; eax++)
            {
                if (edi.FlightGroupStruct.Orders[(esi - 1) * 4 + (eax - 1)].Var2 == 0x32)
                {
                    if (edi.FlightGroupStruct.Orders[(esi - 1) * 4 + (eax - 1)].SecondaryTarget.ClassA == (TieClassEnum)(AlliedVariables.s_CurrentRegion - 1))
                    {
                        bl = true;
                    }
                }

                if (bl)
                {
                    break;
                }
            }

            if (bl)
            {
                break;
            }
        }

        for (int ebp = 0; ebp < AlliedVariables.s_FlightGroupObjectsList.Count && !esp10; ebp++)
        {
            edi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp);

            if (edi.m001444[AlliedVariables.s_CurrentRegion - 1] == 0)
            {
                continue;
            }

            switch (edi.FlightGroupStruct.CraftId)
            {
                case CraftIdEnum._083_1_47_BuoyC:
                case CraftIdEnum._084_1_48_BuoyB:
                case CraftIdEnum._085_1_8_BuoyFaux:
                    break;

                default:
                    continue;
            }

            BitArray esp14 = (BitArray)AlliedVariables.s_V0x00520170.Clone();

            if (esi + 0x0B <= 0xFF)
            {
                BtsBitString(esi + 0x0B, esp14);
            }

            if (!BtBitString((int)edi.FlightGroupStruct.TacticalRole0, esp14) || (edi.FlightGroupStruct.TacticalRoleUsed0 != TacticalRoleUsedEnum.NoTC && edi.FlightGroupStruct.TacticalRoleUsed0 != TacticalRoleUsedEnum.Team8))
            {
                BitArray esp34 = (BitArray)AlliedVariables.s_V0x00520170.Clone();

                if (esi + 0x0B <= 0xFF)
                {
                    BtsBitString(esi + 0x0B, esp34);
                }

                if (!BtBitString((int)edi.FlightGroupStruct.TacticalRole1, esp34) || (edi.FlightGroupStruct.TacticalRoleUsed1 != TacticalRoleUsedEnum.NoTC && edi.FlightGroupStruct.TacticalRoleUsed1 != TacticalRoleUsedEnum.Team8))
                {
                    continue;
                }
            }

            esp10 = true;

            for (int ebx = 0; ebx < 0x03; ebx++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
                eax1.m00147C[ebx].M000000[0] = edi.m00147C[ebx].M000000[0];
            }
        }

        if (!esp10)
        {
            for (int ebx = 0; ebx < 0x03; ebx++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
                eax1.m00147C[ebx].M000000[0] = 0;
            }
        }

        Unit_00513838_Proc_00520528(eax0);

        edi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);

        double st2 = edi.m001458;
        double st1 = edi.m001460;
        double st0 = edi.m001450 / edi.m001448;
        double esp08 = Math.Sqrt(st2 * st2 + st1 * st1 + st0 * st0);

        edi.m00147C[0].M000000[0] = (short)Math.Round(edi.m00147C[0].M000000[0] - edi.m001458 / esp08 * 100.0f);
        edi.m00147C[1].M000000[0] = (short)Math.Round(edi.m00147C[1].M000000[0] - edi.m001460 / esp08 * 100.0f);

        if (edi.m001448 != 0.0f)
        {
            edi.m00147C[2].M000000[0] = (short)Math.Round(edi.m00147C[2].M000000[0] - edi.m001450 / edi.m001448 / esp08 * 100.0f);
        }

        Unit_00513838_Proc_0051D53C(eax0);
    }

    // L00520198
    private static void Unit_00513838_Proc_00520198(int eax0)
    {
        bool esp08 = false;
        short[] esp0A = new short[3];

        S0xFGObject esi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);

        bool bl1 = false;
        int esp04 = 1;
        int edi = 1;

        for (; !bl1 && esp04 <= 0x04; esp04++)
        {
            edi = 1;

            for (; !bl1 && edi <= 0x04; edi++)
            {
                if (esi.FlightGroupStruct.Orders[(esp04 - 1) * 4 + (edi - 1)].OrderId == TieOrderIdEnum._50_Hyperspace)
                {
                    if (esi.FlightGroupStruct.Orders[(esp04 - 1) * 4 + (edi - 1)].Var0 == AlliedVariables.s_CurrentRegion - 1)
                    {
                        bl1 = true;
                    }
                }
            }
        }

        esp04--;
        edi--;

        for (int ebp = 0; true;)
        {
            esi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp);

            if (esi.m001444[esp04 - 1] != 0)
            {
                BitArray esp14;

                switch (esi.FlightGroupStruct.CraftId)
                {
                    case CraftIdEnum._083_1_47_BuoyC:
                    case CraftIdEnum._084_1_48_BuoyB:
                    case CraftIdEnum._085_1_8_BuoyFaux:
                        esp14 = (BitArray)AlliedVariables.s_V0x00520508.Clone();

                        if (AlliedVariables.s_CurrentRegion + 0x0F <= 0xFF)
                        {
                            BtsBitString(AlliedVariables.s_CurrentRegion + 0x0F, esp14);
                        }

                        if (!BtBitString((int)esi.FlightGroupStruct.TacticalRole0, esp14) || (esi.FlightGroupStruct.TacticalRoleUsed0 != TacticalRoleUsedEnum.NoTC && esi.FlightGroupStruct.TacticalRoleUsed0 != TacticalRoleUsedEnum.Team8))
                        {
                            BitArray esp34 = (BitArray)AlliedVariables.s_V0x00520508.Clone();

                            if (AlliedVariables.s_CurrentRegion + 0x0F <= 0xFF)
                            {
                                BtsBitString(AlliedVariables.s_CurrentRegion + 0x0F, esp34);
                            }

                            if (!BtBitString((int)esi.FlightGroupStruct.TacticalRole1, esp34) || (esi.FlightGroupStruct.TacticalRoleUsed1 != TacticalRoleUsedEnum.NoTC && esi.FlightGroupStruct.TacticalRoleUsed1 != TacticalRoleUsedEnum.Team8))
                            {
                                break;
                            }
                        }

                        esp08 = true;

                        for (int ebx = 0; ebx < 0x03; ebx++)
                        {
                            esp0A[ebx] = esi.m00147C[ebx].M000000[0];
                        }

                        break;
                }
            }

            ebp++;

            if (ebp >= AlliedVariables.s_FlightGroupObjectsList.Count || esp08)
            {
                break;
            }
        }

        if (!esp08)
        {
            for (int ebx = 0; ebx < 0x03; ebx++)
            {
                esp0A[ebx] = 0;
            }
        }

        bool esp09 = false;
        esi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);

        while (true)
        {
            for (int ebp = 0x08; true;)
            {
                if (esi.FlightGroupStruct.Orders[(esp04 - 1) * 4 + (edi - 1)].Waypoints[ebp - 1].IsUsed == 0x01)
                {
                    esp09 = true;

                    for (int ebx = 0; ebx < 0x03; ebx++)
                    {
                        esp0A[ebx] -= esi.FlightGroupStruct.Orders[(esp04 - 1) * 4 + (edi - 1)].Waypoints[ebp - 1].Position[ebx];
                    }
                }

                ebp--;

                if (ebp <= 0 || esp09)
                {
                    break;
                }
            }

            edi--;

            if (edi <= 0 || esp09)
            {
                break;
            }
        }

        esp08 = false;

        for (int ebp = 0; true;)
        {
            esi = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp);

            if (esi.m001444[AlliedVariables.s_CurrentRegion - 1] != 0)
            {
                BitArray esp14;

                switch (esi.FlightGroupStruct.CraftId)
                {
                    case CraftIdEnum._083_1_47_BuoyC:
                    case CraftIdEnum._084_1_48_BuoyB:
                    case CraftIdEnum._085_1_8_BuoyFaux:
                        esp14 = (BitArray)AlliedVariables.s_V0x00520508.Clone();

                        if (esp04 + 0x0B <= 0xFF)
                        {
                            BtsBitString(esp04 + 0x0B, esp14);
                        }

                        if (!BtBitString((int)esi.FlightGroupStruct.TacticalRole0, esp14) || (esi.FlightGroupStruct.TacticalRoleUsed0 != TacticalRoleUsedEnum.NoTC && esi.FlightGroupStruct.TacticalRoleUsed0 != TacticalRoleUsedEnum.Team8))
                        {
                            BitArray esp34 = (BitArray)AlliedVariables.s_V0x00520508.Clone();

                            if (esp04 + 0x0B <= 0xFF)
                            {
                                BtsBitString(esp04 + 0x0B, esp34);
                            }

                            if (!BtBitString((int)esi.FlightGroupStruct.TacticalRole1, esp34) || (esi.FlightGroupStruct.TacticalRoleUsed1 != TacticalRoleUsedEnum.NoTC && esi.FlightGroupStruct.TacticalRoleUsed1 != TacticalRoleUsedEnum.Team8))
                            {
                                break;
                            }
                        }

                        esp08 = true;

                        for (int ebx = 0; ebx < 0x03; ebx++)
                        {
                            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
                            eax1.m00147C[ebx].M000000[0] = (short)(esi.m00147C[ebx].M000000[0] - esp0A[ebx]);
                            AlliedVariables.s_V0x00543D30[ebx] = esi.m00147C[ebx].M000000[0];
                        }

                        break;
                }
            }

            ebp++;

            if (ebp >= AlliedVariables.s_FlightGroupObjectsList.Count || esp08)
            {
                break;
            }
        }

        if (!esp08)
        {
            for (int ebx = 0; ebx < 0x03; ebx++)
            {
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, eax0);
                eax1.m00147C[ebx].M000000[0] = 0;
            }
        }

        Unit_00513838_Proc_0051D53C(eax0);
    }

    // L0052A404
    private static void TForm1_BrfBtnClick(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_TBrfForm_Instance = MainImpl.CreateBriefingWindow();
        AlliedVariables.s_TBrfForm_Instance!.Owner = Form1;
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_TBrfForm_Instance.ShowCurr, AlliedVariables.s_V0x00543938.ShowCurrDown);
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_TBrfForm_Instance.ShowNums, AlliedVariables.s_V0x00543938.ShowNumsDown);
        ComCtrls_TToolButton_SetDown(AlliedVariables.s_TBrfForm_Instance.StopAtStop, AlliedVariables.s_V0x00543938.StopAtStopDown);
        Buttons_TSpeedButton_SetDown(AlliedVariables.s_TBrfForm_Instance.FastPlaybackBtn, AlliedVariables.s_V0x00543938.FastPlaybackBtnDown);

        if (AlliedVariables.s_V0x00543938.FastPlaybackBtnDown)
        {
            AlliedVariables.s_TBrfForm_Instance.Timer1.Interval = 15;
        }

        Spin_TSpinEdit_SetValue(AlliedVariables.s_TBrfForm_Instance.SpeedSpin, AlliedVariables.s_IconSpeedOptionSetting);
        AlliedVariables.s_TBrfForm_Instance.ShowDialog();
        TApplication_PostMessage_B021(AlliedVariables.s_TBrfForm_Instance);
    }

    // L00530A00
    private static void TForm1_Backup1Click(Form1Window Form1, object? Sender)
    {
        string ebp0C_2 = AlliedVariables.s_V0x00543BF8;
        ebp0C_2 = ebp0C_2.Insert(ebp0C_2.Length - 0x03, "_BK");

        if (Form1.Incremental1.IsChecked == true && File.Exists(ebp0C_2))
        {
            int ebx = 0;

            while (true)
            {
                ebx++;

                string ebp0C_1 = AlliedVariables.s_V0x00543BF8;
                ebp0C_1 = ebp0C_1.Insert(ebp0C_1.Length - 0x03, string.Format(CultureInfo.InvariantCulture, "_BK{0}", ebx));

                if (!File.Exists(ebp0C_1))
                {
                    ebp0C_2 = ebp0C_1;
                    break;
                }
            }
        }

        Allied_WriteTieMission(ebp0C_2, 0, 0x01);
    }

    // L0053082C
    private static void TForm1_Browse1Click(Form1Window Form1, object? Sender)
    {
        //TBrowseFileForm** edi = AlliedVariables.s_TBrowseFileForm_Instance;
        //bool bl = AlliedVariables.s_V0x00543C9A;
        //*s_V0x00543C9APtr = 0;
        //*edi = (TBrowseFileForm*)TApplication_CreateInstanceOfClass(s_TBrowseFileFormClass, 0x01, s_TApplicationPtr->Component);
        //TForm1_Proc_0052BD6C(Form1, *edi, &s_V0x005AFC74.m000024);

        //if (System_FileExists(*s_V0x00543BF8Ptr) != 0)
        //{
        //    CALL_FUNCTION_ptr_1((*edi)->FilterComboBox1, mVtbl->SetItemIndex, s_V0x005AFC74.m000024.m000008);
        //    FileCtrl_TFilterComboBox_GetMask((*edi)->FilterComboBox1, &ebp08[0]);
        //    FileCtrl_TFileListBox_SetMask((*edi)->FileListBox1, ebp08[0]);
        //    Runtime_GetDriveLetter(*s_V0x00543BF8Ptr, &ebp08[1]);
        //    FileCtrl_TDriveComboBox_SetDrive((*edi)->DriveComboBox1, *(char*)(ebp08[1]));
        //    CALL_FUNCTION_ptr_1((*edi)->DirectoryListBox1, mVtbl->SetDirectory, ebp08[1]);
        //    CALL_FUNCTION_ptr_1((*edi)->FileListBox1, mVtbl->ApplyFilePath, *s_V0x00543BF8Ptr);
        //}
        //else
        //{
        //    FileCtrl_TDriveComboBox_SetDrive((*edi)->DriveComboBox1, 'C');
        //    // "C:\"
        //    /* 0x0053098C = "C:\\" */
        //    CALL_FUNCTION_ptr_1((*edi)->DirectoryListBox1, mVtbl->SetDirectory, (StrRec*)0x0053098C);
        //}

        //TApplication_BringToFront(*edi);
        //Menus_TMenuItem_SetEnabled(Form1.Browse1, false);
        //AlliedVariables.s_V0x00543C9A = bl;
    }

    // L005301D0
    private static void TForm1_XWing1Click(Form1Window Form1, MenuItem Sender)
    {
        MenuItem item = (MenuItem)Sender!;

        if (Unit_00513838_Proc_0051710C())
        {
            switch (MessageBox_ShowConfirmation("Current Mission has been changed. Save it?", "cancel"))
            {
                case TModalResultEnum.Yes:
                    if (AlliedVariables.s_V0x00543B50 != 0)
                    {
                        Allied_WriteTieMission(AlliedVariables.s_V0x00543BF8, 0x01, 0);
                    }
                    else
                    {
                        TForm1_SaveAsBtnClick(Form1, Sender);
                    }

                    Unit_00513838_Proc_00518694();
                    Unit_00513838_Proc_00515884((GameVersionEnum)Convert.ToInt32(item.Tag));
                    break;

                case TModalResultEnum.No:
                    Unit_00513838_Proc_00518694();
                    Unit_00513838_Proc_00515884((GameVersionEnum)Convert.ToInt32(item.Tag));
                    break;
            }
        }
        else
        {
            Unit_00513838_Proc_00518694();
            Unit_00513838_Proc_00515884((GameVersionEnum)Convert.ToInt32(item.Tag));
        }
    }

    // L005307DC
    private static void TForm1_XvT1Click(Form1Window Form1, MenuItem Sender)
    {
        MenuItem item = (MenuItem)Sender!;
        XvTBoxImpl.TXvTForm_Proc_004CCFD8(AlliedVariables.s_TXvTForm_Instance!, (MissionExportTypeEnum)Convert.ToInt32(item.Tag));
    }

    // L0052F8FC
    private static void TForm1_Order11Click(Form1Window Form1, MenuItem Sender)
    {
        byte bl = AlliedVariables.s_V0x00543B54;
        AlliedVariables.s_V0x00543C9A = 0;
        AlliedVariables.s_CurrentOrderInRegion = Convert.ToInt32(Sender.Tag);
        Menus_TMenuItem_SetChecked(Sender, true);
        DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, AlliedVariables.s_CurrentOrderInRegion);
        Unit_00513838_Proc_00516414((TieOrderIdEnum)AlliedVariables.s_CurrentOrderInRegion);
        AlliedVariables.s_V0x00543C9A = 0x01;
        AlliedVariables.s_V0x00543B54 = bl;
        Unit_00513838_Proc_0051E43C(false, AlliedVariables.s_CurrentRegion);
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L00517248
    private static void Unit_00513838_Proc_00517248()
    {
    }

    // L005297F0
    private static void TForm1_BoPRadioClick(Form1Window Form1, object? Sender)
    {
        Allied_SetTieFileVersion_To_0x12();
        Unit_00513838_Proc_005146A4();
    }

    // L005297FC
    private static void TForm1_XvTRadioClick(Form1Window Form1, object? Sender)
    {
        Unit_00513838_Proc_00517248();
        Unit_00513838_Proc_005146A4();
    }

    // L00529298
    private static void TForm1_NewFGButClick(Form1Window Form1, object? Sender)
    {
        Form1OverallPagesEnum edx1 = (Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag);

        if (edx1 == Form1OverallPagesEnum.FlightGroups)
        {
            Unit_00513838_Proc_00519DA8();
            MapWindowImpl.TMapForm__PROC_004F8CC4(AlliedVariables.s_TMapForm_Instance!);
        }
        else if (edx1 == Form1OverallPagesEnum.Messages)
        {
            if (AlliedVariables.s_RadioMessagesObjectsList.Count < 0x40)
            {
                Unit_00513838_Proc_00518A44();
            }
            else
            {
                MessageBox_ShowError("Limit of 64 custom messages in XWA.");
            }
        }
    }

    // L00526A78
    private static void TForm1_DeleteButClick(Form1Window Form1, object? Sender)
    {
        // todo
        //if (AlliedVariables.s_AlliedForm1Window.m00022E == 0)
        //{
        //    return;
        //}

        //if (Form1.ActiveControl != Form1.ShipList && Form1.ActiveControl != Form1.MsgStrList && Form1.ActiveControl != Form1.ScrollBox1)
        //{
        //    return;
        //}

        bool bl = AlliedVariables.s_ConfDeletesChkSetting;

        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                if (bl)
                {
                    if (MessageBox_ShowConfirmation("Delete all selected Flight Groups - Are You Sure?", null) == TModalResultEnum.Yes)
                    {
                        Unit_00513838_Proc_00518CD8();
                    }
                }
                else
                {
                    Unit_00513838_Proc_00518CD8();
                }

                break;

            case Form1OverallPagesEnum.Messages:
                if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0 && Form1.EndMsgWav.IsChecked != true)
                {
                    if (bl)
                    {
                        if (MessageBox_ShowConfirmation("Delete all selected Messages - Are You Sure?", null) == TModalResultEnum.Yes)
                        {
                            Unit_00513838_Proc_00519008();
                        }
                    }
                    else
                    {
                        Unit_00513838_Proc_00519008();
                    }
                }

                break;

            case (Form1OverallPagesEnum)0x06:
                if (bl)
                {
                    if (MessageBox_ShowConfirmation("Delete Briefing - Are You Sure?", null) == TModalResultEnum.Yes)
                    {
                        BriefingWindowImpl.TBrfForm__PROC_004FC2F8(AlliedVariables.s_TBrfForm_Instance!);
                    }
                }

                break;
        }

        if (bl)
        {
            MapWindowImpl.TMapForm__PROC_004F8CC4(AlliedVariables.s_TMapForm_Instance!);
        }
    }

    // L0052687C
    private static void TForm1_CopyBtnClick(Form1Window Form1, object? Sender)
    {
        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                {
                    AlliedVariables.s_V0x005AFE94 = new();
                    AlliedVariables.s_V0x005AFE94.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ToByteArray());
                    AlliedVariables.s_V0x005AFE94.m000E42 = S0xFGObject_000E42.FromByteArray(AlliedVariables.s_V0x005AFE90.m000E42.ToByteArray());
                    AlliedVariables.s_V0x005AFE94.AutoLink = AlliedVariables.s_V0x005AFE90.AutoLink;
                    AlliedVariables.s_V0x005AFE94.m001443 = AlliedVariables.s_V0x005AFE90.m001443;
                    AlliedVariables.s_V0x005AFE94.m001444 = (byte[])AlliedVariables.s_V0x005AFE90.m001444.Clone();
                    AlliedVariables.s_V0x005AFE94.m001478 = AlliedVariables.s_V0x005AFE90.m001478;
                    for (int i = 0; i < AlliedVariables.s_V0x005AFE90.m00147C.Length; i++)
                    {
                        AlliedVariables.s_V0x005AFE94.m00147C[i] = S0xFGObject_00147C.FromByteArray(AlliedVariables.s_V0x005AFE90.m00147C[i].ToByteArray());
                    }
                    AlliedVariables.s_V0x005AFE94.IsWPEnabled = (short[])AlliedVariables.s_V0x005AFE90.IsWPEnabled.Clone();
                    AlliedVariables.s_V0x005AFE94.m001448 = AlliedVariables.s_V0x005AFE90.m001448;
                    AlliedVariables.s_V0x005AFE94.m001450 = AlliedVariables.s_V0x005AFE90.m001450;
                    AlliedVariables.s_V0x005AFE94.m001458 = AlliedVariables.s_V0x005AFE90.m001458;
                    AlliedVariables.s_V0x005AFE94.m001460 = AlliedVariables.s_V0x005AFE90.m001460;
                    AlliedVariables.s_V0x005AFE94.m001470 = AlliedVariables.s_V0x005AFE90.m001470;
                    AlliedVariables.s_V0x005AFE94.m001468 = AlliedVariables.s_V0x005AFE90.m001468;
                    break;
                }

            case Form1OverallPagesEnum.Messages:
                {
                    AlliedVariables.s_V0x005B6B5C.RadioMessage = S0xTieRadioMessage.FromByteArray(AlliedVariables.s_V0x005B6B58.RadioMessage.ToByteArray());
                    AlliedVariables.s_V0x005B6B5C.M0000A6 = AlliedVariables.s_V0x005B6B58.M0000A6;
                    break;
                }
        }
    }

    // L005273D8
    private static void TForm1_PasteBtnClick(Form1Window Form1, object? Sender)
    {
        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                {
                    S0xFGObject eax1 = new();
                    eax1.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005AFE94.FlightGroupStruct.ToByteArray());
                    eax1.m000E42 = S0xFGObject_000E42.FromByteArray(AlliedVariables.s_V0x005AFE94.m000E42.ToByteArray());
                    eax1.AutoLink = AlliedVariables.s_V0x005AFE94.AutoLink;
                    eax1.m001443 = AlliedVariables.s_V0x005AFE94.m001443;
                    eax1.m001444 = (byte[])AlliedVariables.s_V0x005AFE94.m001444.Clone();
                    eax1.m001478 = AlliedVariables.s_V0x005AFE94.m001478;
                    for (int i = 0; i < AlliedVariables.s_V0x005AFE94.m00147C.Length; i++)
                    {
                        eax1.m00147C[i] = S0xFGObject_00147C.FromByteArray(AlliedVariables.s_V0x005AFE94.m00147C[i].ToByteArray());
                    }
                    eax1.IsWPEnabled = (short[])AlliedVariables.s_V0x005AFE94.IsWPEnabled.Clone();
                    eax1.m001448 = AlliedVariables.s_V0x005AFE94.m001448;
                    eax1.m001450 = AlliedVariables.s_V0x005AFE94.m001450;
                    eax1.m001458 = AlliedVariables.s_V0x005AFE94.m001458;
                    eax1.m001460 = AlliedVariables.s_V0x005AFE94.m001460;
                    eax1.m001470 = AlliedVariables.s_V0x005AFE94.m001470;
                    eax1.m001468 = AlliedVariables.s_V0x005AFE94.m001468;
                    AlliedVariables.s_FlightGroupObjectsList.Add(eax1);
                    AlliedVariables.s_TieFileHeader.FlightGroupsCount++;
                    Unit_00513838_Proc_0051950C(false);
                    Unit_00513838_Proc_0051467C();
                    Unit_00513838_Proc_0051DB68();

                    int edi = Form1.ShipList.Items.Count;

                    for (int esi = 0; esi < edi; esi++)
                    {
                        StdCtrls_TCustomListBox_SetSelected(Form1.ShipList, esi, false);
                        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                        eax2.m001479 = 0;
                    }

                    StdCtrls_TCustomListBox_SetSelected(Form1.ShipList, AlliedVariables.s_V0x00543B0C, true);
                    S0xFGObject eax3 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
                    eax3.m001479 = 0x01;

                    if (Form1.CurrOnly.IsChecked != true)
                    {
                        S0xFGObject eax4 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_FlightGroupObjectsList.Count - 1);
                        eax4.m00147A = 0x01;
                        AlliedVariables.s_V0x005B7068 = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
                    }

                    TForm1_Proc_0052D3F8(Form1);
                    Unit_00513838_Proc_00520924();
                    break;
                }

            case Form1OverallPagesEnum.Messages:
                {
                    Unit_00513838_Proc_00518B88();
                    break;
                }
        }
    }

    // L00526624
    private static void TForm1_UpBtnClick(Form1Window Form1, object? Sender)
    {
        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                if (Form1.ShipList.SelectedIndex > 0 && !StdCtrls_TCustomListBox_GetSelected(Form1.ShipList, 0))
                {
                    Unit_00513838_Proc_00517D44("Up", Form1.ShipList.SelectedIndex);
                }

                break;

            case Form1OverallPagesEnum.Messages:
                if (Form1.MsgStrList.SelectedIndex > 0 && !StdCtrls_TCustomListBox_GetSelected(Form1.MsgStrList, 0))
                {
                    Unit_00513838_Proc_00517F80("Up", Form1.MsgStrList.SelectedIndex);
                }

                break;
        }
    }

    // L005266CC
    private static void TForm1_DownBtnClick(Form1Window Form1, object? Sender)
    {
        switch ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag))
        {
            case Form1OverallPagesEnum.FlightGroups:
                if (Form1.ShipList.SelectedIndex < AlliedVariables.s_FlightGroupObjectsList.Count - 1 && !StdCtrls_TCustomListBox_GetSelected(Form1.ShipList, AlliedVariables.s_FlightGroupObjectsList.Count - 1))
                {
                    Unit_00513838_Proc_00517D44("Down", Form1.ShipList.SelectedIndex);
                }

                break;

            case Form1OverallPagesEnum.Messages:
                if (Form1.MsgStrList.SelectedIndex < Form1.MsgStrList.Items.Count - 1 && !StdCtrls_TCustomListBox_GetSelected(Form1.MsgStrList, Form1.MsgStrList.Items.Count - 1))
                {
                    Unit_00513838_Proc_00517F80("Down", Form1.MsgStrList.SelectedIndex);
                }

                break;
        }
    }

    // L0052F5B4
    private static void TForm1_ClassicFGEditview1Click(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_AlliedAplicationWidth > 0x280)
        {
            TForm1_Proc_0052F4A8(Form1, true);
            DatapadWindowImpl.TDatapad_Proc_004C32C8(AlliedVariables.s_TDatapad_Instance!, out int esp00, out int esp04);
            Controls_TControl_SetTop(AlliedVariables.s_TDatapad_Instance!, esp04);
            Controls_TControl_SetLeft(AlliedVariables.s_TDatapad_Instance!, esp00);
            TForm1_Proc_0052F414(Form1, true);

            if ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag) == Form1OverallPagesEnum.FlightGroups)
            {
                Controls_TControl_SetTop(AlliedVariables.s_TOrderSel_Instance!, AlliedPixelsScaleDiv(0x19C) + (int)AlliedVariables.s_TDatapad_Instance!.Top);
                Controls_TControl_SetLeft(AlliedVariables.s_TOrderSel_Instance!, (int)AlliedVariables.s_TDatapad_Instance!.Left);
                TForm1_Proc_0052F464(Form1, true);
            }

            TForm1_Proc_0052F4D4(Form1, false);
            TForm1_Proc_0052F518(Form1, false);
        }
        else
        {
            Controls_TControl_SetTop(AlliedVariables.s_TDatapad_Instance!, 0);
            Controls_TControl_SetLeft(AlliedVariables.s_TDatapad_Instance!, 0x32);
            Controls_TControl_SetTop(AlliedVariables.s_TOrderSel_Instance!, 0x190);
            Controls_TControl_SetLeft(AlliedVariables.s_TOrderSel_Instance!, 0x32);
        }
    }

    // L0052F6B8
    private static void TForm1_ClassicMapview1Click(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_0052F4A8(Form1, false);
        TForm1_Proc_0052F414(Form1, false);
        TForm1_Proc_0052F464(Form1, false);
        TForm1_Proc_0052F4D4(Form1, false);
        TForm1_Proc_0052F518(Form1, false);
    }

    // L0052F55C
    private static void TForm1_Docklefttoolbartotop1Click(Form1Window Form1, object? Sender)
    {
        Controls_TControl_SetHeight(Form1.ToolBarAxisView, 0x38);
        Controls_TControl_SetWidth(Form1.ToolBarAxisView, 0x113);
        Controls_TControl_SetTop(Form1.ToolBarAxisView, 0x19);
        // todo
        //Form1.ToolBarAxisView.Parent = Form1.ControlBar1;
        Controls_TControl_SetTop(Form1.ToolBarAxisView, 0x19);
    }

    // L00530650
    private static void TForm1_Lockorders1Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_LockOrdToRegOptionSetting = !AlliedVariables.s_LockOrdToRegOptionSetting;
    }

    // L005309E8
    private static void TForm1_ErrCheckOn1Click(Form1Window Form1, object? Sender)
    {
        Menus_TMenuItem_SetChecked(Form1.ErrCheckOn1, Form1.ErrCheckOn1.IsChecked != true);
    }

    // L005309BC
    private static void TForm1_CheckFilename1Click(Form1Window Form1, MenuItem Sender)
    {
        AlliedVariables.s_CheckFilenameOptionSetting = Form1.CheckFilename1.IsChecked != true;
        Menus_TMenuItem_SetChecked(Sender, AlliedVariables.s_CheckFilenameOptionSetting);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L00530B0C
    private static void TForm1_Overwriteprevious1Click(Form1Window Form1, MenuItem Sender)
    {
        Menus_TMenuItem_SetChecked(Sender, Sender.IsChecked != true);
    }

    // L00530664
    private static void TForm1_ShipListSequence1Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_TClipForm_Instance = MainImpl.CreateClipWindow();
        Controls_TControl_SetText(AlliedVariables.s_TClipForm_Instance, "Shiplist Sequence");
        Controls_TControl_SetVisible(AlliedVariables.s_TClipForm_Instance.CancelBtn, false);
        Controls_TControl_SetText(AlliedVariables.s_TClipForm_Instance.OKBtn, "OK");
        Controls_TControl_SetVisible(AlliedVariables.s_TClipForm_Instance.InsBtn, true);
        AlliedVariables.s_TClipForm_Instance.ListBox1.M0000F0(true);
        StdCtrls_TCustomListBox_SetExtendedSelect(AlliedVariables.s_TClipForm_Instance.ListBox1, true);
        Controls_TControl_SetVisible(AlliedVariables.s_TClipForm_Instance.Label1, true);
        Controls_TControl_SetVisible(AlliedVariables.s_TClipForm_Instance.SpeedEd, true);
        AlliedVariables.s_ClipboardType = ClipboardTypeEnum.ShiplistSequence;
        AlliedVariables.s_TClipForm_Instance.Owner = Form1;
        AlliedVariables.s_TClipForm_Instance.ShowDialog();
        AlliedVariables.s_TClipForm_Instance = null;
        CraftIdEnum esi = AlliedVariables.s_V0x005AFE90.FlightGroupStruct.CraftId;
        AlliedVariables.s_TDatapad_Instance!.ShipBox.SetItems(AlliedVariables.s_Strings_Ships);
        AlliedVariables.s_TDatapad_Instance!.ShipBox.SelectedIndex = (int)AlliedConvertCraftIdToShipSeq(esi);
    }

    // L00530624
    private static void TForm1_Options1Click(Form1Window Form1, object? Sender)
    {
        Menus_TMenuItem_SetChecked(Form1.Lockorders1, AlliedVariables.s_LockOrdToRegOptionSetting);
        Menus_TMenuItem_SetChecked(Form1.CheckFilename1, AlliedVariables.s_CheckFilenameOptionSetting);
    }

    // L005300B8
    private static void TForm1_XsnapPopPopup(Form1Window Form1, object? Sender)
    {
        for (int ebx = 0; ebx < 5; ebx++)
        {
            Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.XsnapPop, ebx), false);
        }

        Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.XsnapPop, Unit_00513838_Proc_0051F630(AlliedVariables.s_V0x00543D44)), true);
    }

    // L00530100
    private static void TForm1_YSnapPopPopup(Form1Window Form1, object? Sender)
    {
        for (int ebx = 0; ebx < 5; ebx++)
        {
            Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.YSnapPop, ebx), false);
        }

        Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.YSnapPop, Unit_00513838_Proc_0051F630(AlliedVariables.s_V0x00543D48)), true);
    }

    // L00530148
    private static void TForm1_XSnap1Click(Form1Window Form1, object? Sender)
    {
        for (int ebx = 0; ebx < 5; ebx++)
        {
            Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.XSnap1, ebx), false);
        }

        Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.XSnap1, Unit_00513838_Proc_0051F630(AlliedVariables.s_V0x00543D44)), true);
    }

    // L0052F1B4
    private static void TForm1_Off1Click(Form1Window Form1, string tag)
    {
        AlliedVariables.s_V0x00543D44 = Convert.ToInt32(tag);
    }

    // L0053018C
    private static void TForm1_YSnap1Click(Form1Window Form1, object? Sender)
    {
        for (int ebx = 0; ebx < 5; ebx++)
        {
            Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.YSnap1, ebx), false);
        }

        Menus_TMenuItem_SetChecked(Menus_TMenuItem_GetItem(Form1.YSnap1, Unit_00513838_Proc_0051F630(AlliedVariables.s_V0x00543D48)), true);
    }

    // L0052F1C0
    private static void TForm1_Off2Click(Form1Window Form1, string tag)
    {
        AlliedVariables.s_V0x00543D48 = Convert.ToInt32(tag);
    }

    // L0052F1CC
    private static void TForm1_AllSnapOff1Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x00543D44 = 0x01;
        AlliedVariables.s_V0x00543D48 = 0x01;
    }

    // L0052FDF4
    private static void TForm1_ToggleGrid1Click(Form1Window Form1, MenuItem Sender)
    {
        AlliedVariables.s_V0x005B7047 = !AlliedVariables.s_V0x005B7047;
        Menus_TMenuItem_SetChecked(Sender, AlliedVariables.s_V0x005B7047);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L00530990
    private static void TForm1_Numbergrid1Click(Form1Window Form1, MenuItem Sender)
    {
        AlliedVariables.s_ShowNumbersMapSetting = Form1.Numbergrid1.IsChecked != true;
        Menus_TMenuItem_SetChecked(Sender, AlliedVariables.s_ShowNumbersMapSetting);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052F9B8
    private static void TForm1_Allwaypoints1Click(Form1Window Form1, object? Sender)
    {
        ComCtrls_TToolButton_SetDown(Form1.AllWPS, Form1.AllWPS.IsChecked != true);
        TForm1_Proc_0052D3F8(Form1);
        Menus_TMenuItem_SetChecked(Form1.Allwaypoints1, Form1.AllWPS.IsChecked == true);
    }

    // L00530088
    private static void TForm1_LinkHyppoint1Click(Form1Window Form1, object? Sender)
    {
        Menus_TMenuItem_SetChecked(Form1.LinkHyppoint1, Form1.LinkHyppoint1.IsChecked != true);
        AlliedVariables.s_DefaultHypOnChkSetting = Form1.LinkHyppoint1.IsChecked == true;
        TForm1_Proc_0052D3F8(Form1);
    }

    // L005307EC
    private static void TForm1_ShowdistancesClick(Form1Window Form1, object? Sender)
    {
        Menus_TMenuItem_SetChecked(Form1.Showdistances, Form1.Showdistances.IsChecked != true);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0053080C
    private static void TForm1_ShowtimesClick(Form1Window Form1, object? Sender)
    {
        Menus_TMenuItem_SetChecked(Form1.Showtimes, Form1.Showtimes.IsChecked != true);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L00530338
    private static void TForm1_MinimumWireframesSizes1Click(Form1Window Form1, object? Sender)
    {
        Menus_TMenuItem_SetChecked(Form1.MinimumWireframesSizes1, Form1.MinimumWireframesSizes1.IsChecked != true);
        AlliedVariables.s_LimitShrinkChkSetting = Form1.MinimumWireframesSizes1.IsChecked == true;
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052FDCC
    private static void TForm1_ToggleIconsOnly1Click(Form1Window Form1, MenuItem Sender)
    {
        AlliedVariables.s_V0x005B7046 = !AlliedVariables.s_V0x005B7046;
        Menus_TMenuItem_SetChecked(Sender, AlliedVariables.s_V0x005B7046);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052FAB0
    private static void TForm1_YX1Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x005B705C = 0x01;
        AlliedVariables.s_V0x005B7058 = 0;
        AlliedVariables.s_V0x005B7060 = 0x02;
        AlliedVariables.s_AlliedMapOrientation = MapOrientationEnum.YX;
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052FF64
    private static void TForm1_MyPage1Click(Form1Window Form1, object? Sender)
    {
        //Allied_ShellExecute_StrRec("http://troyed.com/");
    }

    // L0052FF8C
    private static void TForm1_LucasArts1Click(Form1Window Form1, object? Sender)
    {
        Allied_ShellExecute_StrRec("http://www.lucasarts.com/");
    }

    // L0052FFBC
    private static void TForm1_TotallyGames1Click(Form1Window Form1, object? Sender)
    {
        //Allied_ShellExecute_StrRec("http://www.totallygames.com/");
    }

    // L0052FFF0
    private static void TForm1_DatamastersWebSite1Click(Form1Window Form1, object? Sender)
    {
        Allied_ShellExecute_StrRec("http://www.xwaupgrade.com/");
    }

    // L00530020
    private static void TForm1_XWingAllianceNet1Click(Form1Window Form1, object? Sender)
    {
        //Allied_ShellExecute_StrRec("http://www.darksaber.gaylenol.com/");
    }

    // L00530058
    private static void TForm1_StarWarscom1Click(Form1Window Form1, object? Sender)
    {
        Allied_ShellExecute_StrRec("http://www.starwars.com/");
    }

    // L0052A38C
    private static void TForm1_LibShowButClick(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_TLibForm_Instance = MainImpl.CreateLibWindow();
        AlliedVariables.s_TLibForm_Instance.Owner = Form1;
        AlliedVariables.s_TLibForm_Instance.ShowDialog();
        AlliedVariables.s_TLibForm_Instance = null;
    }

    // L0052A3EC
    private static void TForm1_PutIntoLibClick(Form1Window Form1, object? Sender)
    {
        LibWindowImpl.TLibForm_Proc_0051330C(AlliedVariables.s_TLibForm_Instance!);
        AlliedVariables.s_LibFormHasChanged = true;
    }

    // L00518A44
    public static void Unit_00513838_Proc_00518A44()
    {
        if (AlliedVariables.s_AlliedForm1Window!.WAVfileEd.M000057())
        {
            AlliedVariables.s_V0x00543B59 = 0x01;
        }

        S0xTieRadioMessageObject eax0 = new();
        eax0.RadioMessage = S0xTieRadioMessage.FromByteArray(AlliedVariables.s_V0x005B6AB4.ToByteArray());
        eax0.RadioMessage.Id = (short)AlliedVariables.s_RadioMessagesObjectsList.Count;
        eax0.RadioMessage.ForTeam[0] = 0x01;
        eax0.M0000A6 = 0x01;
        AlliedVariables.s_RadioMessagesObjectsList.Add(eax0);

        if (AlliedVariables.s_RadioMessagesObjectsList.Count == 0x01)
        {
            AlliedVariables.s_V0x00543B10 = 0;
            AlliedVariables.s_V0x005B6B58 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, 0);
            TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6B58.RadioMessage);
        }

        AlliedVariables.s_TieFileHeader.RadioMessagesCount++;
        Unit_00513838_Proc_005146A4();
        Unit_00513838_Proc_0051477C();

        int ebx0 = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx0; AlliedVariables.s_V0x00543CC0 += 1)
        {
            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0, false);
        }

        int edx0 = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count - 1;
        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, edx0, true);
        AlliedVariables.s_AlliedForm1Window!.MsgStrList.SelectedIndex = AlliedVariables.s_RadioMessagesObjectsList.Count - 1;
        TForm1_MsgStrListClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.MsgStrList);
    }

    // L00516D84
    private static void Unit_00513838_Proc_00516D84(ComboBox eax0, ComboBox edx0, ComboBox ecx0, ComboBox A4)
    {
        eax0.SetItems(AlliedVariables.s_V0x00543BC8);
        ecx0.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
        edx0.SetItems(AlliedVariables.s_V0x00543BCC);
        A4.SetItems(AlliedVariables.s_V0x00543BD4);
    }

    // L00530294
    private static void TForm1_SpeedButton1Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x00543B18 += 1;

        if (AlliedVariables.s_V0x00543B18 > 0x09)
        {
            AlliedVariables.s_V0x00543B18 = 0;
        }

        Unit_00513838_Proc_0051F42C();
    }

    // L005302B4
    private static void TForm1_SpeedButton2Click(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x00543B18 -= 1;

        if (AlliedVariables.s_V0x00543B18 < 0)
        {
            AlliedVariables.s_V0x00543B18 = 0x09;
        }

        Unit_00513838_Proc_0051F42C();
    }

    // L0052560C
    public static void L0052560C(Form1Window eax0)
    {
    }

    // L0052FE8C
    private static void TForm1_EndMsgWavClick(Form1Window Form1, object? Sender)
    {
        Buttons_L00467140_SetVisible(AlliedVariables.s_TDatapad_Instance!, Form1.EndMsgWav.IsChecked != true);
        TForm1_Proc_0052F414(Form1, AlliedVariables.s_TDatapad_Instance!.M000057());

        if (Form1.EndMsgWav.IsChecked == true)
        {
            Form1.MsgStrList.SelectedIndex = 0;
        }

        Unit_00513838_Proc_0051477C();

        if (Form1.EndMsgWav.IsChecked != true)
        {
            int esi = Form1.MsgStrList.Items.Count;

            for (int edi = 0; edi < esi; edi++)
            {
                StdCtrls_TCustomListBox_SetSelected(Form1.MsgStrList, edi, false);
            }

            StdCtrls_TCustomListBox_SetSelected(Form1.MsgStrList, AlliedVariables.s_V0x00543B10, true);
        }

        TForm1_Proc_00527128(Form1);
    }

    // L00517D44
    private static void Unit_00513838_Proc_00517D44(string eax0, int edx0)
    {
        if (string.Equals(eax0, "Up", StringComparison.Ordinal))
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < esi; AlliedVariables.s_V0x00543CC0 += 1)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
                {
                    continue;
                }

                L005192A0(AlliedVariables.s_V0x00543CC0, 0x55);
                Classes_TList_Exchange(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0, AlliedVariables.s_V0x00543CC0 - 1);
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0, false);
                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                eax1.m001479 = 0;
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0 - 1, true);
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0 - 1);
                eax1.m001479 = 0x01;

                if (AlliedVariables.s_V0x00543B0C == AlliedVariables.s_V0x00543CC0)
                {
                    AlliedVariables.s_V0x00543B0C -= 1;
                }
            }
        }

        if (string.Equals(eax0, "Down", StringComparison.Ordinal))
        {
            int eax1 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count - 1;

            for (AlliedVariables.s_V0x00543CC0 = eax1; AlliedVariables.s_V0x00543CC0 >= 0; AlliedVariables.s_V0x00543CC0 -= 1)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
                {
                    continue;
                }

                L005192A0(AlliedVariables.s_V0x00543CC0, 0x44);
                Classes_TList_Exchange(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0, AlliedVariables.s_V0x00543CC0 + 1);
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0, false);
                S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                eax2.m001479 = 0;
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0 + 1, true);
                eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0 + 1);
                eax2.m001479 = 0x01;

                if (AlliedVariables.s_V0x00543B0C == AlliedVariables.s_V0x00543CC0)
                {
                    AlliedVariables.s_V0x00543B0C += 1;
                }
            }
        }

        Unit_00513838_Proc_0051DB68();
        L00517568((byte)AlliedVariables.s_V0x00543B0C);
        AlliedVariables.s_AlliedForm1Window!.ShipList.IsEnabled = false;
        AlliedVariables.s_AlliedForm1Window!.ShipList.IsEnabled = true;
        Unit_00513838_Proc_005146A4();
        DatapadWindowImpl.TDatapad_SetTitle(AlliedVariables.s_TDatapad_Instance!);
    }

    // L00517F80
    private static void Unit_00513838_Proc_00517F80(string eax0, int edx0)
    {
        if (string.Equals(eax0, "Up", StringComparison.Ordinal))
        {
            int esi = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

            for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < esi; AlliedVariables.s_V0x00543CC0 += 1)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
                {
                    continue;
                }

                if (AlliedVariables.s_V0x00543B10 == AlliedVariables.s_V0x00543CC0)
                {
                    AlliedVariables.s_V0x00543B10 -= 1;
                }

                Classes_TList_Exchange(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0, AlliedVariables.s_V0x00543CC0 - 1);

                if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
                {
                    if (AlliedVariables.s_V0x00543C3C.GetCount() >= AlliedVariables.s_V0x00543CC0 && AlliedVariables.s_V0x00543C3C.GetCount() > AlliedVariables.s_V0x00543CC0 - 1)
                    {
                        if (AlliedVariables.s_V0x00543CC0 < 0x40)
                        {
                            AlliedVariables.s_V0x00543C3C.Exchange(AlliedVariables.s_V0x00543CC0, AlliedVariables.s_V0x00543CC0 - 1);
                        }
                    }
                }

                S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0 - 1);
                eax1.RadioMessage.Id = (short)(AlliedVariables.s_V0x00543CC0 - 1);
                eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                eax1.RadioMessage.Id = (short)AlliedVariables.s_V0x00543CC0;
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0, false);
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0 - 1, true);
            }
        }

        if (string.Equals(eax0, "Down", StringComparison.Ordinal))
        {
            int eax1 = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

            for (AlliedVariables.s_V0x00543CC0 = eax1 - 1; AlliedVariables.s_V0x00543CC0 >= 0; AlliedVariables.s_V0x00543CC0 -= 1)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
                {
                    continue;
                }

                if (AlliedVariables.s_V0x00543B10 == AlliedVariables.s_V0x00543CC0)
                {
                    AlliedVariables.s_V0x00543B10 += 1;
                }

                Classes_TList_Exchange(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0, AlliedVariables.s_V0x00543CC0 + 1);

                if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
                {
                    if (AlliedVariables.s_V0x00543C3C.GetCount() > AlliedVariables.s_V0x00543CC0 && AlliedVariables.s_V0x00543C3C.GetCount() > AlliedVariables.s_V0x00543CC0 + 1)
                    {
                        if (AlliedVariables.s_V0x00543CC0 < 0x3F)
                        {
                            AlliedVariables.s_V0x00543C3C.Exchange(AlliedVariables.s_V0x00543CC0, AlliedVariables.s_V0x00543CC0 + 1);
                        }
                    }
                }

                S0xTieRadioMessageObject eax2 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0 + 1);
                eax2.RadioMessage.Id = (short)(AlliedVariables.s_V0x00543CC0 + 1);
                eax2 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);
                eax2.RadioMessage.Id = (short)AlliedVariables.s_V0x00543CC0;
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0, false);
                StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0 + 1, true);
            }
        }

        Unit_00513838_Proc_0051477C();
        AlliedVariables.s_V0x005B6B58 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543B10);
        TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6B58.RadioMessage);
        TForm1_Proc_00527128(AlliedVariables.s_AlliedForm1Window!);
        AlliedVariables.s_V0x00543B3C = 0x01;
        CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, 0x01);
        Unit_00513838_Proc_005146A4();
        AlliedVariables.s_V0x00543B59 = 0x01;
    }

    // L00518B88
    private static void Unit_00513838_Proc_00518B88()
    {
        if (AlliedVariables.s_AlliedForm1Window!.WAVfileEd.M000057())
        {
            AlliedVariables.s_V0x00543B59 = 0x01;
        }

        S0xTieRadioMessageObject eax1 = new();
        eax1.RadioMessage = S0xTieRadioMessage.FromByteArray(AlliedVariables.s_V0x005B6B5C.RadioMessage.ToByteArray());
        eax1.RadioMessage.Id = (short)AlliedVariables.s_RadioMessagesObjectsList.Count;
        eax1.M0000A6 = AlliedVariables.s_V0x005B6B5C.M0000A6;
        AlliedVariables.s_RadioMessagesObjectsList.Add(eax1);

        if (AlliedVariables.s_RadioMessagesObjectsList.Count == 0x01)
        {
            AlliedVariables.s_V0x00543B10 = 0;
            AlliedVariables.s_V0x005B6B58 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, 0);
            TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6B58.RadioMessage);
        }

        AlliedVariables.s_TieFileHeader.RadioMessagesCount++;
        Unit_00513838_Proc_005146A4();
        Unit_00513838_Proc_0051477C();

        int ebx = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

        for (AlliedVariables.s_V0x00543CC0 = 0; AlliedVariables.s_V0x00543CC0 < ebx; AlliedVariables.s_V0x00543CC0 += 1)
        {
            StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0, false);
        }

        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count - 1, true);
        AlliedVariables.s_AlliedForm1Window!.MsgStrList.SelectedIndex = AlliedVariables.s_RadioMessagesObjectsList.Count - 1;
        TForm1_MsgStrListClick(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_AlliedForm1Window!.MsgStrList);
    }

    // L0052F4D4
    private static void TForm1_Proc_0052F4D4(Form1Window Form1, bool edx0)
    {
        if (edx0 != (Form1.DescBtn.IsChecked != true))
        {
            return;
        }

        ComCtrls_TToolButton_SetDown(Form1.DescBtn, edx0);
        TForm1_DescBtnClick(Form1, Form1.DescBtn);

        if (edx0)
        {
            AlliedVariables.s_TMemoForm_Instance!.Update();
        }
    }

    // L0052F4A8
    private static void TForm1_Proc_0052F4A8(Form1Window Form1, bool edx0)
    {
        if (edx0 == (Form1.ShowFGList.IsChecked != true))
        {
            ComCtrls_TToolButton_SetDown(Form1.ShowFGList, edx0);
            TForm1_ShowFGListClick(Form1, Form1.ShowFGList);
        }
    }

    // L0052EA98
    private static void TForm1_LockBattleCtrClick(Form1Window Form1, object? Sender)
    {
        MapWindowImpl.TMapForm__PROC_004F7DD0(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052D2A8
    private static void TForm1_ShowR1Click(Form1Window Form1, Button Sender)
    {
        AlliedVariables.s_V0x00543C9A = 0;
        AlliedVariables.s_CurrentRegion = Convert.ToInt32(Sender.Tag);

        switch (AlliedVariables.s_CurrentRegion)
        {
            case 0x01:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R1btn, true);
                break;

            case 0x02:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R2btn, true);
                break;

            case 0x03:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R3Btn, true);
                break;

            case 0x04:
                ComCtrls_TToolButton_SetDown(AlliedVariables.s_AlliedForm1Window!.R4Btn, true);
                break;
        }

        Unit_00513838_Proc_0051E43C(true, AlliedVariables.s_CurrentRegion);
        AlliedVariables.s_V0x00543C9A = 0x01;
        TForm1_Proc_0052D3F8(Form1);
        Unit_00513838_Proc_0051DB68();
    }

    // L00518CD8
    private static void Unit_00513838_Proc_00518CD8()
    {
        bool bl = AlliedVariables.s_V0x00543B0C == 0;

        if (AlliedVariables.s_FlightGroupObjectsList.Count > 0x01 && AlliedVariables.s_FlightGroupObjectsList.Count > AlliedVariables.s_V0x00543B0C)
        {
            int eax1 = AlliedVariables.s_AlliedForm1Window!.ShipList.Items.Count;

            for (AlliedVariables.s_V0x00543CC0 = eax1 - 1; AlliedVariables.s_V0x00543CC0 >= 0; AlliedVariables.s_V0x00543CC0 -= 1)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543CC0))
                {
                    continue;
                }

                L005192A0(AlliedVariables.s_V0x00543CC0, 0x58);
                Classes_TList_Delete(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543CC0);
                AlliedVariables.s_AlliedForm1Window!.ShipList.DeleteItem(AlliedVariables.s_V0x00543CC0);
                AlliedVariables.s_TieFileHeader.FlightGroupsCount--;

                if (AlliedVariables.s_V0x00543B0C > 0)
                {
                    AlliedVariables.s_V0x00543B0C -= 1;
                }
            }

            if (!bl)
            {
                AlliedVariables.s_V0x00543B0C += 1;
            }

            if (AlliedVariables.s_FlightGroupObjectsList.Count <= AlliedVariables.s_V0x00543B0C)
            {
                AlliedVariables.s_V0x00543B0C = AlliedVariables.s_FlightGroupObjectsList.Count - 1;
            }

            DatapadWindowImpl.TDatapad__PROC_004C05F0(AlliedVariables.s_TDatapad_Instance!);

            if (AlliedVariables.s_FlightGroupObjectsList.Count > 0)
            {
                AlliedVariables.s_V0x005AFE90 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
                DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
                AlliedVariables.s_AlliedForm1Window!.ShipList.SelectedIndex = AlliedVariables.s_V0x00543B0C;
            }

            Unit_00513838_Proc_0051DB68();
        }
        else
        {
            AlliedVariables.s_V0x005AFE90.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005B5C74.ToByteArray());
            Unit_00513838_Proc_0051DB68();
            DatapadWindowImpl.TDatapad_Proc_004BF834(AlliedVariables.s_TDatapad_Instance!);
        }

        StdCtrls_TCustomListBox_SetSelected(AlliedVariables.s_AlliedForm1Window!.ShipList, AlliedVariables.s_V0x00543B0C, true);
        S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, AlliedVariables.s_V0x00543B0C);
        eax2.m001479 = 0x01;
        Unit_00513838_Proc_005146A4();
    }

    // L0051F630
    private static int Unit_00513838_Proc_0051F630(int eax0)
    {
        return eax0 switch
        {
            0x00 => 0,
            0x10 => 0x01,
            0x20 => 0x02,
            0x50 => 0x03,
            0xA0 => 0x04,
            _ => 0,
        };
    }

    // L005284A0
    private static void L005284A0(Form1Window Form1, int edx0, byte ecx0, S0xTieTrigger A4)
    {
        if (A4.VariableType != TieClassEnum.FlightGroup)
        {
            return;
        }

        switch (ecx0)
        {
            case 0x44:
                if (A4.Variable == edx0)
                {
                    A4.Variable++;
                }
                else if (A4.Variable == edx0 + 1)
                {
                    A4.Variable--;
                }

                break;

            case 0x55:
                if (A4.Variable == edx0)
                {
                    A4.Variable--;
                }
                else if (A4.Variable == edx0 - 1)
                {
                    A4.Variable++;
                }

                break;

            case 0x58:
                if (A4.Variable >= edx0)
                {
                    A4.Variable--;
                }

                break;
        }
    }

    // L0052D198
    private static void TForm1_AllWPsClick(Form1Window Form1, object? Sender)
    {
        MapWindowImpl.TMapForm_Proc_004F710C(AlliedVariables.s_TMapForm_Instance!);
        Menus_TMenuItem_SetChecked(Form1.Allwaypoints1, Form1.AllWPS.IsChecked == true);

        if (Form1.AllWPS.IsChecked == true)
        {
            AlliedVariables.s_WPsDefaultRadioIndexSetting = WPsDefaultRadioEnum.AllWPs;
        }
        else if (Form1.CurrWPs.IsChecked == true)
        {
            AlliedVariables.s_WPsDefaultRadioIndexSetting = WPsDefaultRadioEnum.CurrentWPs;
        }
        else
        {
            AlliedVariables.s_WPsDefaultRadioIndexSetting = WPsDefaultRadioEnum.None;
        }
    }

    // L0052D268
    public static void TForm1_CentreMapBtnClick(Form1Window Form1, object? Sender)
    {
        MapWindowImpl.TMapForm__PROC_004F68E8(AlliedVariables.s_TMapForm_Instance!, 0, 0);
    }

    // L0052D27C
    public static void TForm1_CtrFGBtnClick(Form1Window Form1, object? Sender)
    {
        MapWindowImpl.TMapForm__PROC_004F7A4C(AlliedVariables.s_TMapForm_Instance!);
        MapWindowImpl.TMapForm__PROC_004F6A80(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052C324
    private static void TForm1_ZoomTo800BtnClick(Form1Window Form1, object? Sender)
    {
        StdCtrls_TScrollBar_SetPosition(Form1.ZoomBar, 0x320);
    }

    // L0052C310
    public static void TForm1_ZoomTo16BtnClick(Form1Window Form1, object? Sender)
    {
        StdCtrls_TScrollBar_SetPosition(Form1.ZoomBar, 0x10);
    }

    // L0052C230
    public static void TForm1_AllIFFClick(Form1Window Form1, object? Sender)
    {
        Buttons_TSpeedButton_SetDown(Form1.RebIFF, true);
        Buttons_TSpeedButton_SetDown(Form1.ImpIFF, true);
        Buttons_TSpeedButton_SetDown(Form1.BluIFF, true);
        Buttons_TSpeedButton_SetDown(Form1.Red2IFF, true);
        Buttons_TSpeedButton_SetDown(Form1.YellIFF, true);
        Buttons_TSpeedButton_SetDown(Form1.PurpIFF, true);
        Buttons_TSpeedButton_SetDown(Form1.CapShipsOn, true);
        Buttons_TSpeedButton_SetDown(Form1.FightersOn, true);
        Buttons_TSpeedButton_SetDown(Form1.TransportsOn, true);
        Buttons_TSpeedButton_SetDown(Form1.FRTsOn, true);
        Buttons_TSpeedButton_SetDown(Form1.PlatformsOn, true);
        Buttons_TSpeedButton_SetDown(Form1.ObjectsOn, true);
        Buttons_TSpeedButton_SetDown(Form1.ShowStartBtn, false);

        int esi = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int edi = 0; edi < esi; edi++)
        {
            S0xFGObject eax = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, edi);
            eax.m00147A = 0x01;
        }

        TForm1_Proc_0052D3F8(Form1);
    }

    // L0052C338
    private static void TForm1_RebIFFClick(Form1Window Form1, object? Sender)
    {
        MapWindowImpl.TMapForm_Proc_004F6B48(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052A560
    private static void TForm1_BattleClick(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_TlstForm_Instance = MainImpl.CreateLstWindow();
        AlliedVariables.s_TlstForm_Instance.Owner = Form1;
        AlliedVariables.s_TlstForm_Instance.ShowDialog();
        AlliedVariables.s_TlstForm_Instance = null;
    }

    // L00529348
    private static void TForm1_Succ1EdChange(Form1Window Form1, TextBox Sender)
    {
        AlliedVariables.s_V0x005B6BB4 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, AlliedVariables.s_V0x00543B18);
        string ebp40 = Unit_00511CD0_Proc_0051213C(Controls_TControl_GetText(Sender));

        switch (Convert.ToInt32(Sender.Tag) - 1)
        {
            case 0:
                AlliedVariables.s_V0x005B6BB4.Team.PrimarySuccessMessage1 = ebp40;
                break;

            case 1:
                AlliedVariables.s_V0x005B6BB4.Team.PrimarySuccessMessage2 = ebp40;
                break;

            case 2:
                AlliedVariables.s_V0x005B6BB4.Team.PrimaryFailureMessage1 = ebp40;
                break;

            case 3:
                AlliedVariables.s_V0x005B6BB4.Team.PrimaryFailureMessage2 = ebp40;
                break;

            case 4:
                AlliedVariables.s_V0x005B6BB4.Team.SecondarySuccessMessage1 = ebp40;
                break;

            case 5:
                AlliedVariables.s_V0x005B6BB4.Team.SecondarySuccessMessage2 = ebp40;
                break;
        }

        Unit_00513838_Proc_005146A4();
    }

    // L0052B038
    private static void TForm1_JoinByRadioClick(Form1Window Form1, object? Sender)
    {
        int esi = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int ebx = 0; ebx < esi; ebx++)
        {
            S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

            if (eax1.FlightGroupStruct.Team != AlliedVariables.s_V0x00543B18)
            {
                continue;
            }

            S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
            eax2.FlightGroupStruct.Radio = (byte)(AlliedVariables.s_V0x00543B18 + 1);
        }

        Unit_00513838_Proc_0051442C();
        Allied_ShowMessageWithTimer(0x3E8, "Team " + AlliedVariables.s_Strings_Teams.GetText(AlliedVariables.s_V0x00543B18) + " has been linked by Radio");
    }

    // L00529858
    private static void TForm1_TeamIFFBox1Change(Form1Window Form1, object? Sender)
    {
        AlliedVariables.s_V0x00543C78[AlliedVariables.s_V0x00543B18] = (byte)Form1.TeamIFFBox1.SelectedIndex;

        switch (Form1.TeamIFFBox1.SelectedIndex)
        {
            case 0x00:
                {
                    S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, AlliedVariables.s_V0x00543B18);
                    eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Rebel");
                    break;
                }

            case 0x01:
                {
                    S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, AlliedVariables.s_V0x00543B18);
                    eax1.Team.Name = Unit_00511CD0_Proc_00511E04("Imperial");
                    break;
                }
        }

        Unit_00513838_Proc_00515BE0(AlliedVariables.s_V0x00543B18);
    }

    // L00529920
    private static void TForm1_LstManagerClick(Form1Window Form1, object? Sender)
    {
    }

    // L0052A5C0
    private static void TForm1_FriendsListClick(Form1Window Form1, object? Sender)
    {
        Unit_00513838_Proc_005146A4();

        for (int ebx = 0; ebx < 8; ebx++)
        {
            bool edx1 = ebx >= Form1.FriendsList.Items.Count ? false : StdCtrls_TCustomListBox_GetSelected(Form1.FriendsList, ebx);
            S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, AlliedVariables.s_V0x00543B18);
            eax1.Team.TeamAllied[ebx] = edx1;
        }
    }

    // L0052A5FC
    private static void TForm1_ThreeDecClick(Form1Window Form1, object? Sender)
    {
    }

    // L0052975C
    private static void TForm1_TeamName1EdChange(Form1Window Form1, object? Sender)
    {
        S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, AlliedVariables.s_V0x00543B18);
        eax1.Team.Name = Unit_00511CD0_Proc_00511E04(Controls_TControl_GetText(Form1.TeamName1Ed));

        if ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag) == (Form1OverallPagesEnum)0x05)
        {
            Unit_00513838_Proc_005187A0();
        }

        Unit_00513838_Proc_005146A4();
    }

    // L00529754
    private static void TForm1_MissMinSpinChange(Form1Window Form1, object? Sender)
    {
        Unit_00513838_Proc_005146A4();
    }

    // L00529948
    private static void TForm1_ShowMapClick(Form1Window Form1, object? Sender)
    {
        TApplication_BringToFront(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052BF24
    private static void TForm1_Ord1Click(Form1Window Form1, Button Sender)
    {
        int esi = Convert.ToInt32(Sender.Tag);
        byte bl = AlliedVariables.s_V0x00543B54;
        AlliedVariables.s_V0x00543C9A = 0;
        AlliedVariables.s_CurrentOrderInRegion = esi;
        Unit_00513838_Proc_00519B48(esi);
        DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, esi);
        Unit_00513838_Proc_00516414((TieOrderIdEnum)Allied_ComboBox_GetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.OrderBox));
        AlliedVariables.s_V0x00543C9A = 0x01;
        AlliedVariables.s_V0x00543B54 = bl;
        Unit_00513838_Proc_0051E43C(false, AlliedVariables.s_CurrentRegion);
        MapWindowImpl.TMapForm_Proc_004F8BBC(AlliedVariables.s_TMapForm_Instance!);
    }

    // L0052BE68
    private static void TForm1_SetRect_L0052BE68(Form1Window Form1, S0x005342B0_00000C edx0, int ecx0, int A4, int A8, int AC)
    {
        edx0.Top = (short)ecx0;
        edx0.Left = (short)AC;
        edx0.Height = (short)A8;
        edx0.Width = (short)A4;
    }

    // L00529CB4
    public static void TForm1_CreateLstClick(Form1Window Form1, object? Sender)
    {
        if (string.Equals(AlliedVariables.s_V0x00543BF8, "Unnamed", StringComparison.Ordinal))
        {
            MessageBox_ShowError("Mission File is unnamed. Can't create matching .lst file until it is named.");
        }
        else if (File.Exists(AlliedVariables.s_V0x00543C04) && !string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
        {
            MessageBox_ShowError(AlliedVariables.s_V0x00543C04 + " already exists!");
        }
        else
        {
            AlliedVariables.s_V0x00543C3C.Clear();

            for (int ebx = 0; ebx < 0x46; ebx++)
            {
                AlliedVariables.s_V0x00543C3C.Add("dummy.wav");
            }

            Controls_TControl_SetVisible(Form1.WAVfileEd, true);

            switch (AlliedVariables.s_TieFileVersion)
            {
                case TieFileVersionEnum.Bop:
                    AlliedVariables.s_V0x00543C04 = AlliedVariables.s_BoPDirLabSetting + "\\wave\\" + Allied_GetFileNameWithLstExtension(AlliedVariables.s_V0x00543BF8);
                    break;

                case TieFileVersionEnum.XWA:
                    if (!Directory.Exists(AlliedVariables.s_XWADirLabSetting + "\\Wave\\MissionVoice\\"))
                    {
                        Directory.Exists(AlliedVariables.s_XWADirLabSetting + "\\Wave\\MissionVoice\\");
                    }

                    AlliedVariables.s_V0x00543C04 = AlliedVariables.s_XWADirLabSetting + "\\Wave\\MissionVoice\\" + Allied_GetFileNameWithLstExtension(AlliedVariables.s_V0x00543BF8);
                    break;
            }

            if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
            {
                Controls_TControl_SetText(Form1.WAVfileEd, AlliedVariables.s_V0x00543C3C.GetText(AlliedVariables.s_V0x00543B10));
                Form1.WAVplayer.SoundLocation = AlliedVariables.s_XWADirLabSetting + "\\Wave\\" + AlliedVariables.s_V0x00543C3C.GetText(AlliedVariables.s_V0x00543B10);
            }

            AlliedVariables.s_V0x00543C3C.SaveToFile(AlliedVariables.s_V0x00543C04);
            Allied_ShowMessageWithTimer(0x320, AlliedVariables.s_V0x00543C04 + " has been created.");

            AlliedVariables.s_V0x00543B59 = 0x01;
        }
    }

    // L0052A1C4
    private static void TForm1_AutoPlayClick(Form1Window Form1, object? Sender)
    {
    }

    // L0052A014
    private static void TForm1_SaveWAVClick(Form1Window Form1, object? Sender)
    {
        TieFileVersionEnum eax0 = AlliedVariables.s_TieFileVersion;

        int ebx0;

        if (eax0 == TieFileVersionEnum.XvT || eax0 == TieFileVersionEnum.Bop)
        {
            ebx0 = 0x13;
        }
        else if (eax0 == TieFileVersionEnum.XWA)
        {
            ebx0 = 0x46;
        }
        else
        {
            ebx0 = 0x13;
        }

        if (string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
        {
            MessageBox_ShowError("There is no matching .lst file to save!");
        }
        else
        {
            if (ebx0 < AlliedVariables.s_V0x00543C3C.GetCount())
            {
                for (int esi = AlliedVariables.s_V0x00543C3C.GetCount() - 1; esi >= ebx0; esi--)
                {
                    AlliedVariables.s_V0x00543C3C.Delete(esi);
                }
            }

            AlliedVariables.s_V0x00543C3C.SaveToFile(AlliedVariables.s_V0x00543C04);

            AlliedVariables.s_V0x00543B59 = 0;

            if (AlliedVariables.s_ConfSaveChkSetting)
            {
                Allied_ShowMessageWithTimer(0x2BC, "WAV .lst file " + Path.GetFileName(AlliedVariables.s_V0x00543C04) + "saved.");
            }
        }
    }

    // L0052A1C8
    private static void TForm1_ClearWAVButClick(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C3C.GetCount() > 0)
        {
            int esi0;

            if (Form1.EndMsgWav.IsChecked == true)
            {
                esi0 = AlliedVariables.s_V0x00543B10 + 0x40;
            }
            else
            {
                esi0 = AlliedVariables.s_V0x00543B10;
            }

            AlliedVariables.s_V0x00543C3C.Put(esi0, "dummy.wav");
            Controls_TControl_SetText(Form1.WAVfileEd, AlliedVariables.s_V0x00543C3C.GetText(esi0));
            Form1.WAVplayer.Stop();
            AlliedVariables.s_V0x00543B59 = 0x01;

            if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
            {
                Unit_00513838_Proc_0051477C();
            }
        }
    }

    // L00529984
    private static void TForm1_LoadWAVClick(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543B10 >= 0x40)
        {
            MessageBox_ShowError("Limit of 64 wavs for custom messages");
        }
        else
        {
            Dialogs_TOpenDialog_SetInitialDir(Form1.OpenDialog2, AlliedVariables.s_WaveDirLabSetting);

            if (AlliedVariables.s_RadioMessagesObjectsList.Count <= 0)
            {
                MessageBox_ShowError("There are no messages to add sounds to!");
            }
            else
            {
                if (string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
                {
                    if (MessageBox_ShowConfirmation("There is no .lst file for this mission to assign WAVs to. Create one?", null) == TModalResultEnum.Yes)
                    {
                        TForm1_CreateLstClick(Form1, Sender);
                    }
                }

                if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal) && Form1.OpenDialog2.ShowDialog(Form1) == true)
                {
                    string ebp08_1 = Form1.OpenDialog2.FileName;
                    int eax1 = ebp08_1.ToUpperInvariant().IndexOf("\\WAVE\\");

                    if (eax1 != 0)
                    {
                        System_LStrDelete(ref ebp08_1, 0x01, eax1 + 0x05);
                    }

                    if (Unit_00513838_Proc_0051D348_Returns_0x01(AlliedVariables.s_V0x00543C3C, ebp08_1))
                    {
                        if (Form1.EndMsgWav.IsChecked == true)
                        {
                            int edx1 = Form1.MsgStrList.SelectedIndex + 0x40;
                            AlliedVariables.s_V0x00543C3C.Put(edx1, ebp08_1);
                        }
                        else
                        {
                            AlliedVariables.s_V0x00543C3C.Put(AlliedVariables.s_V0x00543B10, ebp08_1);
                        }

                        Controls_TControl_SetText(Form1.WAVfileEd, ebp08_1);

                        if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
                        {
                            Unit_00513838_Proc_0051477C();
                        }

                        Form1.WAVplayer.SoundLocation = Form1.OpenDialog2.FileName;

                        if (File.Exists(Form1.OpenDialog2.FileName))
                        {
                            Form1.WAVplayer.Load();
                            Form1.WAVplayer.Play();
                        }

                        System_LGetDir(0, AlliedVariables.s_WaveDirLabSetting);
                        AlliedVariables.s_V0x00543B59 = 0x01;
                    }
                }
            }
        }
    }

    // L0052A2BC
    private static void TForm1_WAVlistButClick(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_RadioMessagesObjectsList.Count <= 0)
        {
            MessageBox_ShowError("There are no messages to add sounds to!");
            return;
        }

        AlliedVariables.s_TWAVListForm_Instance = MainImpl.CreateWavListBox();
        AlliedVariables.s_TWAVListForm_Instance.Owner = Form1;
        AlliedVariables.s_TWAVListForm_Instance.ShowDialog();
        AlliedVariables.s_TWAVListForm_Instance = null;

        if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal))
        {
            Unit_00513838_Proc_0051477C();
        }
    }

    // L005295E0
    private static void TForm1_MsgStrListDrawItem(Form1Window Form1, ListBox sender, int index, object? A4, object? rect)
    {
        ListBoxItem item = sender.GetItem(index);

        if (Form1.EndMsgWav.IsChecked == true)
        {
            Graphics_TFont_SetColor(item, Form1.Succ1Ed.GetFontColor());
        }
        else if (AlliedVariables.s_RadioMessagesObjectsList.Count > index)
        {
            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, index);

            if (eax1.M0000A6 == 0)
            {
                Graphics_TFont_SetColor(item, 0x00808080);
            }
            else
            {
                S0xTieRadioMessageObject eax2 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, index);
                Graphics_TFont_SetColor(item, AlliedGetIffColor(eax2.RadioMessage.Side, 0));
            }
        }
    }

    // L00526D0C
    private static void TForm1_MsgStrListClick(Form1Window Form1, object? edx0)
    {
        if (Form1.EndMsgWav.IsChecked == true)
        {
            if (Form1.WAVPanel.M000057())
            {
                TForm1_Proc_00527128(Form1);
            }
        }
        else
        {
            if (AlliedVariables.s_V0x00543B10 < AlliedVariables.s_RadioMessagesObjectsList.Count || (Form1.MsgStrList.SelectedIndex == 0 && AlliedVariables.s_V0x00543B10 == 0))
            {
                if (AlliedVariables.s_V0x00543B55 != 0)
                {
                    Unit_00513838_Proc_005146A4();
                }

                AlliedVariables.s_V0x00543B10 = Form1.MsgStrList.SelectedIndex;

                if (Form1.MsgStrList.SelectedIndex < AlliedVariables.s_TieFileHeader.RadioMessagesCount)
                {
                    AlliedVariables.s_V0x005B6B58 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, Form1.MsgStrList.SelectedIndex);
                }

                TForm1_Proc_00526E4C(Form1, AlliedVariables.s_V0x005B6B58.RadioMessage);

                if (Form1.WAVPanel.M000057() && AlliedVariables.s_V0x00543B10 < 0x40)
                {
                    TForm1_Proc_00527128(Form1);
                }

                AlliedVariables.s_V0x00543B3C = 0x01;
                CondToolUserControlImpl.TCondToolForm_PROC_0051015C(AlliedVariables.s_TCondToolForm_Instance!, 0x01);
            }

            AlliedVariables.s_V0x00543B55 = 0;
        }
    }

    // L0052A6D0
    private static void TForm1_MsgStrListDblClick(Form1Window Form1, object? Sender)
    {
        TForm1_WAVlistButClick(Form1, Form1.WAVlistBut);
    }

    // L0052A534
    private static void TForm1_Status2BoxChange(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedGetControlTag(Sender);
        int edx = DatapadWindowImpl.Unit_00513838_Proc_0051E8C4(Sender);
        Unit_00513838_Proc_0051EF8C(esi, edx);
    }

    // L00529958
    private static void TForm1_SeenByListClick(Form1Window Form1, object? Sender)
    {
        Unit_00513838_Proc_005183C0();
        AlliedVariables.s_V0x00543B55 = 0x01;
        AlliedVariables.s_V0x005B6B58.M0000A6 = (byte)Unit_00513838_Proc_0051D874(AlliedVariables.s_V0x00543B10);
        Unit_00513838_Proc_0051477C();
    }

    // L005293D8
    private static void TForm1_FGStrIncompChange(Form1Window Form1, TextBox Sender)
    {
        if (AlliedVariables.s_V0x00543C9A != 0)
        {
            string ebp40 = Unit_00511CD0_Proc_0051213C(Controls_TControl_GetText(Sender));

            switch (Convert.ToInt32(Sender.Tag) - 1)
            {
                case 0:
                    AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000000 = ebp40;
                    break;

                case 1:
                    AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000040 = ebp40;
                    break;

                case 2:
                    AlliedVariables.s_V0x005AFE90.m000E42.M000000[AlliedVariables.s_V0x00543B48].M000080 = ebp40;
                    break;
            }
        }
    }

    // L005291A4
    public static void L005291A4(Form1Window Form1)
    {
        if (AlliedVariables.s_RadioMessagesObjectsList.Count <= 0)
        {
            Unit_00513838_Proc_00518A44();
        }
        else
        {
            switch (AlliedVariables.s_V0x00543B3C)
            {
                case 0x01:
                case 0x02:
                    CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6B58.RadioMessage.Condition.Trigger_0[AlliedVariables.s_V0x00543B3C - 1]);
                    break;

                case 0x03:
                    CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6B58.RadioMessage.Condition.Trigger_1[0]);
                    break;

                case 0x04:
                    CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6B58.RadioMessage.Condition.Trigger_1[1]);
                    break;

                case 0x05:
                    CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6B58.RadioMessage.Trigger1);
                    break;

                case 0x06:
                    CondToolUserControlImpl.TCondToolForm_Proc_0050F2E8(AlliedVariables.s_TCondToolForm_Instance!, AlliedVariables.s_V0x005B6B58.RadioMessage.Trigger2);
                    break;
            }
        }
    }

    // L00528DE0
    private static void TForm1_FGGoalPercentChange(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_00528DFC(Form1);
    }

    // L00528DE8
    private static void TForm1_FGMustBoxChange(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_00528DFC(Form1);
    }

    // L00528DF0
    private static void TForm1_FGGoalCondChange(Form1Window Form1, object? Sender)
    {
        TForm1_Proc_00528DFC(Form1);
        Unit_00513838_Proc_00517258();
    }

    // L00528D60
    private static void TForm1_MissionTypeBoxChange(Form1Window Form1, object? Sender)
    {
    }

    // L00528D64
    private static void TForm1_Order1LabClick(Form1Window Form1, object? Sender)
    {
        Unit_00513838_Proc_0051D378(0x01);
    }

    // L00528500
    private static void TForm1_PrevTeamBtnClick(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543B18 <= 0)
        {
            return;
        }

        AlliedVariables.s_V0x00543B18 -= 1;
        Unit_00513838_Proc_00515BE0(AlliedVariables.s_V0x00543B18);
    }

    // L00528520
    private static void TForm1_NextTeamBtnClick(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543B18 >= 0x09)
        {
            return;
        }

        AlliedVariables.s_V0x00543B18 += 1;
        Unit_00513838_Proc_00515BE0(AlliedVariables.s_V0x00543B18);
    }

    // L00528540
    private static void TForm1_AIBoxChange(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_V0x00543C9A == 0)
        {
            return;
        }

        int esi = AlliedGetControlTag(Sender);
        int edx1 = DatapadWindowImpl.Unit_00513838_Proc_0051E8C4(Sender);
        Unit_00513838_Proc_0051E614(esi, edx1);
    }

    // L00528D4C
    private static void TForm1_ShortIDEdChange(Form1Window Form1, object? Sender)
    {
        if (AlliedVariables.s_RadioMessagesObjectsList.Count <= 0)
        {
            return;
        }

        Unit_00513838_Proc_0051442C();
    }

    // L00528414
    private static void L00528414(Form1Window Form1, int edx0, byte ecx0, S0xTieFlightGroupTriggerPair A4)
    {
        L005284A0(Form1, edx0, ecx0, A4.Triggers[0]);
        L005284A0(Form1, edx0, ecx0, A4.Triggers[1]);
    }

    // L00528448
    private static void L00528448(Form1Window Form1, int edx0, byte ecx0, S0xTieTriggers A4)
    {
        L005284A0(Form1, edx0, ecx0, A4.Trigger_0[0]);
        L005284A0(Form1, edx0, ecx0, A4.Trigger_0[1]);
        L005284A0(Form1, edx0, ecx0, A4.Trigger_1[0]);
        L005284A0(Form1, edx0, ecx0, A4.Trigger_1[1]);
    }

    // L005192A0
    public static void L005192A0(int eax0, byte edx0)
    {
        int ebp10a = AlliedVariables.s_FlightGroupObjectsList.Count;

        for (int ebp0C = 0; ebp0C < ebp10a; ebp0C++)
        {
            S0xFGObject eax1;

            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
            L00528414(AlliedVariables.s_AlliedForm1Window!, eax0, edx0, eax1.FlightGroupStruct.ArrivalTrigger1);
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
            L00528414(AlliedVariables.s_AlliedForm1Window!, eax0, edx0, eax1.FlightGroupStruct.ArrivalTrigger2);
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
            L00528414(AlliedVariables.s_AlliedForm1Window!, eax0, edx0, eax1.FlightGroupStruct.DepartureTrigger);

            for (int esi = 0x01; esi < 0x05; esi++)
            {
                for (int ebx = 0x01; ebx < 0x05; ebx++)
                {
                    eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
                    L00528414(AlliedVariables.s_AlliedForm1Window!, eax0, edx0, eax1.FlightGroupStruct.JumpTriggers[(esi - 1) * 4 + (ebx - 1)]);
                }
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
            L00517520(eax0, edx0, ref eax1.FlightGroupStruct.StartFg);
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
            L00517520(eax0, edx0, ref eax1.FlightGroupStruct.SecondaryStopFg);
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
            L00517520(eax0, edx0, ref eax1.FlightGroupStruct.PrimaryStopFg);
            eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
            L00517520(eax0, edx0, ref eax1.FlightGroupStruct.CaptureFg);

            for (int ebx = 0x01; ebx < 0x05; ebx++)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebp0C);
                L005282D4(AlliedVariables.s_AlliedForm1Window!, eax0, edx0, eax1.FlightGroupStruct.Orders[(AlliedVariables.s_CurrentRegion - 1) * 4 + (ebx - 1)]);
            }
        }

        int ebp10b = AlliedVariables.s_RadioMessagesObjectsList.Count;

        for (int ebp0C = 0; ebp0C < ebp10b; ebp0C++)
        {
            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebp0C);
            L00528448(AlliedVariables.s_AlliedForm1Window!, eax0, edx0, eax1.RadioMessage.Condition);
        }

        for (int ebp0C = 0; ebp0C < 0x0A; ebp0C++)
        {
            for (int ebx = 0; ebx < 0x03; ebx++)
            {
                S0xTieGlobalGoalObject eax1 = Classes_TList_Get(AlliedVariables.s_GlobalGoalsObjectsList, ebp0C);
                L00528448(AlliedVariables.s_AlliedForm1Window!, eax0, edx0, eax1.GlobalGoal.GlobalGoals[ebx].Triggers);
            }
        }
    }

    // L005282D4
    private static void L005282D4(Form1Window Form1, int edx0, byte ecx0, S0xTieFlightGroupOrder A4)
    {
        if (A4.PrimaryTarget.ClassA == TieClassEnum.FlightGroup)
        {
            byte esi = A4.PrimaryTarget.ParameterA;

            switch (ecx0)
            {
                case 0x44:
                    if (esi == edx0)
                    {
                        A4.PrimaryTarget.ParameterA++;
                    }
                    else if (esi == edx0 + 1)
                    {
                        A4.PrimaryTarget.ParameterA--;
                    }

                    break;

                case 0x55:
                    if (esi == edx0)
                    {
                        A4.PrimaryTarget.ParameterA--;
                    }
                    else if (esi == edx0 - 1)
                    {
                        A4.PrimaryTarget.ParameterA++;
                    }

                    break;

                case 0x58:
                    if (esi >= edx0)
                    {
                        A4.PrimaryTarget.ParameterA--;
                    }

                    break;
            }
        }

        if (A4.PrimaryTarget.ClassB == TieClassEnum.FlightGroup)
        {
            byte esi = A4.PrimaryTarget.ParameterB;

            switch (ecx0)
            {
                case 0x44:
                    if (esi == edx0)
                    {
                        A4.PrimaryTarget.ParameterB++;
                    }
                    else if (esi == edx0 + 1)
                    {
                        A4.PrimaryTarget.ParameterB--;
                    }

                    break;

                case 0x55:
                    if (esi == edx0)
                    {
                        A4.PrimaryTarget.ParameterB--;
                    }
                    else if (esi == edx0 - 1)
                    {
                        A4.PrimaryTarget.ParameterB++;
                    }

                    break;

                case 0x58:
                    if (esi >= edx0)
                    {
                        A4.PrimaryTarget.ParameterB--;
                    }

                    break;
            }
        }

        if (A4.SecondaryTarget.ClassA == TieClassEnum.FlightGroup)
        {
            byte esi = A4.SecondaryTarget.ParameterA;

            switch (ecx0)
            {
                case 0x44:
                    if (esi == edx0)
                    {
                        A4.SecondaryTarget.ParameterA++;
                    }
                    else if (esi == edx0 + 1)
                    {
                        A4.SecondaryTarget.ParameterA--;
                    }

                    break;

                case 0x55:
                    if (esi == edx0)
                    {
                        A4.SecondaryTarget.ParameterA--;
                    }
                    else if (esi == edx0 - 1)
                    {
                        A4.SecondaryTarget.ParameterA++;
                    }

                    break;

                case 0x58:
                    if (esi >= edx0)
                    {
                        A4.SecondaryTarget.ParameterA--;
                    }

                    break;
            }
        }

        if (A4.SecondaryTarget.ClassB == TieClassEnum.FlightGroup)
        {
            byte esi = A4.SecondaryTarget.ParameterB;

            switch (ecx0)
            {
                case 0x44:
                    if (esi == edx0)
                    {
                        A4.SecondaryTarget.ParameterB++;
                    }
                    else if (esi == edx0 + 1)
                    {
                        A4.SecondaryTarget.ParameterB--;
                    }

                    break;

                case 0x55:
                    if (esi == edx0)
                    {
                        A4.SecondaryTarget.ParameterB--;
                    }
                    else if (esi == edx0 - 1)
                    {
                        A4.SecondaryTarget.ParameterB++;
                    }

                    break;

                case 0x58:
                    if (esi >= edx0)
                    {
                        A4.SecondaryTarget.ParameterB--;
                    }

                    break;
            }
        }
    }

    // L00519008
    private static void Unit_00513838_Proc_00519008()
    {
        if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0 && AlliedVariables.s_RadioMessagesObjectsList.Count > AlliedVariables.s_V0x00543B10)
        {
            int eax1 = AlliedVariables.s_AlliedForm1Window!.MsgStrList.Items.Count;

            for (AlliedVariables.s_V0x00543CC0 = eax1 - 1; AlliedVariables.s_V0x00543CC0 >= 0; AlliedVariables.s_V0x00543CC0 -= 1)
            {
                if (!StdCtrls_TCustomListBox_GetSelected(AlliedVariables.s_AlliedForm1Window!.MsgStrList, AlliedVariables.s_V0x00543CC0))
                {
                    continue;
                }

                Classes_TList_Delete(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543CC0);

                if (AlliedVariables.s_V0x00543C3C.GetCount() > AlliedVariables.s_V0x00543CC0)
                {
                    AlliedVariables.s_V0x00543C3C.Delete(AlliedVariables.s_V0x00543CC0);

                    if (AlliedVariables.s_V0x00543C3C.GetCount() > 0x3F)
                    {
                        AlliedVariables.s_V0x00543C3C.Insert(0x3F, "dummy.wav");
                    }
                }

                AlliedVariables.s_TieFileHeader.RadioMessagesCount--;
            }

            if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0 && AlliedVariables.s_RadioMessagesObjectsList.Count <= AlliedVariables.s_V0x00543B10)
            {
                AlliedVariables.s_V0x00543B10 = AlliedVariables.s_RadioMessagesObjectsList.Count - 1;
            }

            if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
            {
                AlliedVariables.s_V0x005B6B58 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543B10);
                AlliedVariables.s_AlliedForm1Window!.MsgStrList.SelectedIndex = AlliedVariables.s_V0x00543B10;
            }

            if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
            {
                TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6B58.RadioMessage);
                TForm1_Proc_00527128(AlliedVariables.s_AlliedForm1Window!);
            }
            else
            {
                TForm1_Proc_00526E4C(AlliedVariables.s_AlliedForm1Window!, AlliedVariables.s_V0x005B6AB4);
            }
        }
        else
        {
            AlliedVariables.s_V0x005B6B58.RadioMessage = S0xTieRadioMessage.FromByteArray(AlliedVariables.s_V0x005B6AB4.ToByteArray());
            Unit_00513838_Proc_0051477C();
            AlliedVariables.s_V0x00543C3C.Clear();
            AlliedVariables.s_V0x00543C3C.Add("dummy.wav");
        }

        if (AlliedVariables.s_RadioMessagesObjectsList.Count - 1 == AlliedVariables.s_V0x00543B10 && AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, AlliedVariables.s_V0x00543B10);
            eax1.RadioMessage.Id = (short)(AlliedVariables.s_RadioMessagesObjectsList.Count - 1);
        }
        else if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            byte al = (byte)AlliedVariables.s_V0x00543B10;
            byte bl = (byte)AlliedVariables.s_RadioMessagesObjectsList.Count;

            for (byte ebp01 = al; ebp01 < bl; ebp01++)
            {
                S0xTieRadioMessageObject eax1 = Classes_TList_Get(AlliedVariables.s_RadioMessagesObjectsList, ebp01);
                eax1.RadioMessage.Id--;
            }
        }

        AlliedVariables.s_V0x00543B55 = 0;
        Unit_00513838_Proc_005146A4();

        if (AlliedVariables.s_AlliedForm1Window!.WAVfileEd.M000057())
        {
            AlliedVariables.s_V0x00543B59 = 0x01;
        }
    }

    // L0051D378
    private static void Unit_00513838_Proc_0051D378(int eax0)
    {
        Unit_00513838_Proc_0051E43C(false, AlliedVariables.s_CurrentRegion);
        byte bl = AlliedVariables.s_V0x00543B54;
        AlliedVariables.s_V0x00543C9A = 0;
        AlliedVariables.s_CurrentOrderInRegion = eax0;
        Unit_00513838_Proc_00519B48(eax0);
        DatapadWindowImpl.TDatapad_Proc_004C0774(AlliedVariables.s_TDatapad_Instance!, eax0);
        Unit_00513838_Proc_00516414((TieOrderIdEnum)Allied_ComboBox_GetSelectedIndex(AlliedVariables.s_TDatapad_Instance!.OrderBox));
        AlliedVariables.s_V0x00543C9A = 0x01;
        AlliedVariables.s_V0x00543B54 = bl;
    }

    // L0051DAEC
    private static void AlliedWavPlayerOpenAndPlay(string eax0)
    {
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.SoundLocation = eax0;
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.Load();
        AlliedVariables.s_AlliedForm1Window!.WAVplayer.Play();
    }

    // L005267AC
    private static void TForm1_CutBtnClick(Form1Window Form1, object? Sender)
    {
        if ((Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag) != Form1OverallPagesEnum.FlightGroups)
        {
            return;
        }

        if (AlliedVariables.s_FlightGroupObjectsList.Count > 0 && MessageBox_ShowConfirmation("Cut this Flight Group. Are You Sure?", null) == TModalResultEnum.Yes)
        {
            AlliedVariables.s_V0x005AFE94.FlightGroupStruct = S0xTieFlightGroup.FromByteArray(AlliedVariables.s_V0x005AFE90.FlightGroupStruct.ToByteArray());

            Unit_00513838_Proc_00517518();
            Unit_00513838_Proc_00518CD8();
        }
        else if (AlliedVariables.s_RadioMessagesObjectsList.Count > 0)
        {
            MessageBox_ShowConfirmation("Cut this Message. Are You Sure?", null);
        }
    }

    // L00525C14
    public static string L00525C14(Form1Window eax0, S0xFGObject edx0)
    {
        string ebp1C_6 = string.Empty;

        if (edx0.FlightGroupStruct.PlayerNumber != 0)
        {
            ebp1C_6 += "Plr: " + edx0.FlightGroupStruct.PlayerNumber.ToString(CultureInfo.InvariantCulture) + "    ";
        }
        else
        {
            if (edx0.FlightGroupStruct.ArrivalDifficulty != (ArrivalDifficultyEnum)0x00)
            {
                switch (edx0.FlightGroupStruct.ArrivalDifficulty)
                {
                    case (ArrivalDifficultyEnum)0x01:
                    case (ArrivalDifficultyEnum)0x08:
                        {
                            ebp1C_6 += "<= Easy  ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x02:
                    case (ArrivalDifficultyEnum)0x09:
                        {
                            ebp1C_6 += "   Med     ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x03:
                    case (ArrivalDifficultyEnum)0x0A:
                        {
                            ebp1C_6 += "   hard     ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x04:
                        {
                            ebp1C_6 += ">= Med   ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x05:
                        {
                            ebp1C_6 += "<= Med   ";
                            break;
                        }

                    case (ArrivalDifficultyEnum)0x06:
                    case (ArrivalDifficultyEnum)0x07:
                        {
                            ebp1C_6 += "--never--  ";
                            break;
                        }
                }
            }
            else
            {
                ebp1C_6 += "               ";
            }
        }

        ebp1C_6 += (edx0.FlightGroupStruct.WavesCount + 1).ToString(CultureInfo.InvariantCulture) + "  x  [ " + edx0.FlightGroupStruct.CraftsCount.ToString(CultureInfo.InvariantCulture) + " ]  " + AlliedGetCraftShortString(edx0.FlightGroupStruct.CraftId) + "  " + System_LStrFromPCharLen(edx0.FlightGroupStruct.Name, 0x14);

        return ebp1C_6;
    }

    // L00527128
    private static void TForm1_Proc_00527128(Form1Window Form1)
    {
        if (Form1.EndMsgWav.IsChecked == true && AlliedVariables.s_V0x00543C3C.GetCount() >= 0x46)
        {
            int eax1 = Form1.MsgStrList.SelectedIndex;
            string ebp1C_6 = AlliedVariables.s_V0x00543C3C.GetText(eax1 + 0x40);
            Controls_TControl_SetText(Form1.WAVfileEd, ebp1C_6);

            string ebp1C_3 = AlliedVariables.s_XWADirLabSetting + "\\wave\\" + ebp1C_6;

            if (!File.Exists(ebp1C_3))
            {
                ebp1C_3 = System_LStrFromChar(AlliedVariables.s_AlliedDriveLetter) + ":\\Wave\\" + ebp1C_6;
            }

            if (File.Exists(ebp1C_3))
            {
                AlliedWavPlayerOpenAndPlay(ebp1C_3);
            }
            else
            {
                Form1.WAVplayer.Stop();
            }
        }
        else if (AlliedVariables.s_V0x00543C9E != 0 && (Form1OverallPagesEnum)Convert.ToInt32(Form1.OverallPages.GetActivePage().Tag) == Form1OverallPagesEnum.Messages)
        {
            if (!string.Equals(AlliedVariables.s_V0x00543C04, "none", StringComparison.Ordinal) && AlliedVariables.s_V0x00543C3C.GetCount() > AlliedVariables.s_V0x00543B10)
            {
                if (!string.IsNullOrEmpty(AlliedVariables.s_V0x00543C3C.GetText(AlliedVariables.s_V0x00543B10)))
                {
                    string ebp1C_6 = AlliedVariables.s_V0x00543C3C.GetText(AlliedVariables.s_V0x00543B10);
                    Controls_TControl_SetText(Form1.WAVfileEd, ebp1C_6);

                    string ebp1C_5 = AlliedVariables.s_BoPDirLabSetting + "\\wave\\" + ebp1C_6;
                    string ebp1C_4 = AlliedVariables.s_XWADirLabSetting + "\\wave\\" + ebp1C_6;
                    string ebp1C_3 = AlliedVariables.s_XWADirLabSetting + "\\wave\\" + ebp1C_6;

                    if (!File.Exists(ebp1C_3))
                    {
                        ebp1C_3 = System_LStrFromChar(AlliedVariables.s_AlliedDriveLetter) + ":\\Wave\\" + ebp1C_6;
                    }

                    if (File.Exists(ebp1C_3))
                    {
                        AlliedWavPlayerOpenAndPlay(ebp1C_3);
                    }
                    else
                    {
                        Form1.WAVplayer.Stop();
                    }
                }
            }
        }
    }
}
