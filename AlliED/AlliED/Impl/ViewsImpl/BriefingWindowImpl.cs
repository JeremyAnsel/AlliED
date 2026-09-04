using AlliED.Extensions;
using AlliED.Helpers;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlliED.Impl.ViewsImpl;

internal static class BriefingWindowImpl
{
    public static void Register(BriefingWindow window)
    {
        SetBindings(window);

        window.Closing += (s, e) =>
        {
            window.Timer1.Stop();
            window.Timer2.Stop();
        };

        FormCreate(window);
    }

    private static void SetBindings(BriefingWindow window)
    {
        window.Loaded += (s, e) => TBrfForm_FormActivate(window, s);
        window.PaintBox1.Paint += (sender, context) => TBrfForm_PaintBox1Paint(window);
        window.Closed += (s, e) => TBrfForm_FormClose(window, s);
        window.BrfPages.SelectionChanged += (s, e) => TBrfForm_BrfPagesChange(window, s, e);
        window.Stop.Click += (s, e) => TBrfForm_StopClick(window, s);
        window.Stop1.Click += (s, e) => TBrfForm_StopClick(window, s);
        window.BrfSeenBy.ObservableItemChanged += (element, index) => TBrfForm_BrfSeenByClick(window, element);
        window.ComDisplay.MouseLeftButtonUp += (s, e) => TBrfForm_ComDisplayClick(window, s);
        window.ColorBox.SelectionChanged += (s, e) => TBrfForm_ColorBoxChange(window, s);
        window.RadioGroup1.SetClickHandler((s, e) => TBrfForm_RadioGroup1Click(window, s));
        window.KeyDown += (s, e) => TBrfForm_FormKeyDown(window, s, e.Key, Keyboard.Modifiers);
        window.PaintBox1.MouseDown += (s, e) =>
        {
            if (e.ClickCount != 1)
            {
                return;
            }
            Point position = e.GetPosition((Control)s);
            TBrfForm_PaintBox1MouseDown(window, s, (int)position.Y, (int)position.X, e.ToTShiftState());
        };
        window.PaintBox1.MouseUp += (s, e) =>
        {
            Point position = e.GetPosition((Control)s);
            TBrfForm_PaintBox1MouseUp(window, s, (int)position.Y, (int)position.X, e.ToTShiftState());
            TBrfForm_PaintBox1MouseMove(window, s, e.ToTShiftState(), (int)position.Y, (int)position.X);
        };
        window.PaintBox1.MouseMove += (s, e) =>
        {
            Point position = e.GetPosition((Control)s);
            TBrfForm_PaintBox1MouseMove(window, s, e.ToTShiftState(), (int)position.Y, (int)position.X);
        };
        window.Acc.Click += (s, e) => TBrfForm_AccClick(window, s);
        window.Acc2.Click += (s, e) => TBrfForm_AccClick(window, s);
        window.Dee.Click += (s, e) => TBrfForm_AccClick(window, s);
        window.Dee2.Click += (s, e) => TBrfForm_AccClick(window, s);
        window.AutoBrfBtn.Click += (s, e) => TBrfForm_AutoBrfBtnClick(window, s);
        window.MapTagGrid.LostFocus += (s, e) => TBrfForm_MapTagGridSetEditText(window, null, 0, string.Empty, window.MapTagGrid.SelectedIndex);
        window.BrfStrGrid.LostFocus += (s, e) => TBrfForm_BrfStrGridSetEditText(window, null, 0, string.Empty, window.BrfStrGrid.SelectedIndex);
        window.IndexBox.ItemsChanged += (s, e) => TBrfForm_IndexBoxChange(window);
        window.CommandBox.SelectionChanged += (s, e) => TBrfForm_CommandBoxChange(window);
        window.BrfTimeEd.TextChanged += (s, e) => TBrfForm_BrfTimeEdChange(window, s);
        window.BrfUnk1Ed.TextChanged += (s, e) => TBrfForm_BrfUnk1EdChange(window, s);
        window.BrfUnk2Ed.TextChanged += (s, e) => TBrfForm_BrfUnk2EdChange(window, s);
        window.BrfUnk3Ed.TextChanged += (s, e) => TBrfForm_BrfUnk3EdChange(window, s);
        window.DeleteCom.Click += (s, e) => TBrfForm_DeleteComClick(window, s);
        window.Delete1.Click += (s, e) => TBrfForm_DeleteComClick(window, s);
        window.InsertStop.Click += (s, e) => TBrfForm_InsertStopClick(window, s);
        window.InsertStop1.Click += (s, e) => TBrfForm_InsertStopClick(window, s);
        window.ToolButton7.Click += (s, e) => TBrfForm_InsertStopClick(window, s);
        window.MoveUpBtn.Click += (s, e) => TBrfForm_MoveUpBtnClick(window, s);
        window.Moveup1.Click += (s, e) => TBrfForm_MoveUpBtnClick(window, s);
        window.MoveDownBtn.Click += (s, e) => TBrfForm_MoveDownBtnClick(window, s);
        window.Movedown1.Click += (s, e) => TBrfForm_MoveDownBtnClick(window, s);
        window.XEd.TextChanged += (s, e) => TBrfForm_XEdChange(window, s);
        window.YEd.TextChanged += (s, e) => TBrfForm_YEdChange(window, s);
        window.TimeSpin.ValueChanged += (s, e) => TBrfForm_TimeSpinChange(window, s);
        window.InsertMove.Click += (s, e) => TBrfForm_InsertMoveClick(window, s);
        window.ToolButton8.Click += (s, e) => TBrfForm_InsertMoveClick(window, s);
        window.InsertMove1.Click += (s, e) => TBrfForm_InsertMoveClick(window, s);
        window.InsertZoom.Click += (s, e) => TBrfForm_InsertZoomClick(window, s);
        window.ToolButton9.Click += (s, e) => TBrfForm_InsertZoomClick(window, s);
        window.InsertZoom1.Click += (s, e) => TBrfForm_InsertZoomClick(window, s);
        window.InsertText.Click += (s, e) => TBrfForm_InsertTextClick(window, s);
        window.ToolButton10.Click += (s, e) => TBrfForm_InsertTextClick(window, s);
        window.InsertText1.Click += (s, e) => TBrfForm_InsertTextClick(window, s);
        window.PopupMenu1.Opened += (s, e) => TBrfForm_PopupMenu1Popup(window, s);
        window.InsertFGBox.Click += (s, e) => TBrfForm_InsertFGBoxClick(window, s);
        window.ToolButton11.Click += (s, e) => TBrfForm_InsertFGBoxClick(window, s);
        window.InsertFGBox1.Click += (s, e) => TBrfForm_InsertFGBoxClick(window, s);
        window.InsertClearBox.Click += (s, e) => TBrfForm_InsertClearBoxClick(window, s);
        window.ToolButton12.Click += (s, e) => TBrfForm_InsertClearBoxClick(window, s);
        window.Insert1.Click += (s, e) => TBrfForm_InsertClearBoxClick(window, s);
        window.InsertTag.Click += (s, e) => TBrfForm_InsertTagClick(window, s);
        window.ToolButton13.Click += (s, e) => TBrfForm_InsertTagClick(window, s);
        window.InsertMapTag1.Click += (s, e) => TBrfForm_InsertTagClick(window, s);
        window.InsertClearTags.Click += (s, e) => TBrfForm_InsertClearTagsClick(window, s);
        window.ToolButton14.Click += (s, e) => TBrfForm_InsertClearTagsClick(window, s);
        window.ClearText1.Click += (s, e) => TBrfForm_InsertClearTagsClick(window, s);
        window.InsertRegion.Click += (s, e) => TBrfForm_InsertRegionClick(window, s);
        window.InsertNew.Click += (s, e) => TBrfForm_InsertNewClick(window, s);
        window.ToolButton4.Click += (s, e) => TBrfForm_InsertNewClick(window, s);
        window.InsertMoveIcon.Click += (s, e) => TBrfForm_InsertMoveIconClick(window, s);
        window.InsertRotate.Click += (s, e) => TBrfForm_InsertRotateClick(window, s);
        window.InsertInfo.Click += (s, e) => TBrfForm_InsertInfoClick(window, s);
        window.InsertChangeRegion.Click += (s, e) => TBrfForm_InsertChangeRegionClick(window, s);
        window.ToolButton15.Click += (s, e) => TBrfForm_ToolButton15Click(window, s);
        window.SetalltoStart11.Click += (s, e) => TBrfForm_Button1Click(window, s);
        window.FastPlaybackBtn.Click += (s, e) => TBrfForm_FastPlaybackBtnClick(window, s);
        window.SaveBtn.Click += (s, e) => TBrfForm_SaveBtnClick(window, s);
        window.Play.Click += (s, e) => TBrfForm_PlayClick(window, s);
        window.Play1.Click += (s, e) => TBrfForm_PlayClick(window, s);
        window.Pause.Click += (s, e) => TBrfForm_PauseClick(window, s);
        window.Pause1.Click += (s, e) => TBrfForm_PauseClick(window, s);
        window.Step.Click += (s, e) => TBrfForm_StepClick(window, s);
        window.NextStop.Click += (s, e) => TBrfForm_NextStopClick(window, s);
        window.ZoomScroll.ValueChanged += (s, e) => TBrfForm_ZoomScrollChange(window, s);
        window.XScroll.ValueChanged += (s, e) => TBrfForm_XScrollChange(window, s);
        window.YScroll.ValueChanged += (s, e) => TBrfForm_YScrollChange(window, s);
        window.PrevInstr.Click += (s, e) => TBrfForm_PrevInstrClick(window, s);
        window.Return.Click += (s, e) => TBrfForm_ReturnClick(window, s);
        window.Timer1.ElapsedEvent += (s, e) => TBrfForm_Timer1Timer(window, s);
        window.Timer2.ElapsedEvent += (s, e) => TBrfForm_Timer2Timer(window, s);
        window.ShipBox.SelectionChanged += (s, e) => TBrfForm_ShipBoxChange(window, s);
        window.CancelInsert.Click += (s, e) => TBrfForm_CancelInsertClick(window, s);
        window.MapIndex.SelectionChanged += (s, e) => TBrfForm_MapIndexChange(window, s);
        window.DoneBtn.Click += (s, e) => TBrfForm_DoneBtnClick(window, s);
        window.FF.Click += (s, e) => TBrfForm_FFClick(window, s);
        window.TimeScroll.ValueChanged += (s, e) => TBrfForm_TimeScrollChange(window, s);
        window.SpeedButton6.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.SpeedButton3.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.SpeedButton2.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.SpeedButton5.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.IconRight.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.SpeedButton7.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.SpeedButton4.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.SpeedButton8.Click += (s, e) => TBrfForm_IconRightClick(window, (Button)s);
        window.ShowCurr.Click += (s, e) => TBrfForm_ShowNumsClick(window, s);
        window.ShowNums.Click += (s, e) => TBrfForm_ShowNumsClick(window, s);
        window.ImportBtn.Click += (s, e) => TBrfForm_ImportBtnClick(window, s);
        window.XSnapBtn.Click += (s, e) => TBrfForm_XSnapBtnClick(window, s);
        window.YSnapBtn.Click += (s, e) => TBrfForm_YSnapBtnClick(window, s);
        window.Rot0.Click += (s, e) => TBrfForm_Rot0Click(window, (Button)s);
        window.Rot90.Click += (s, e) => TBrfForm_Rot0Click(window, (Button)s);
        window.Rot180.Click += (s, e) => TBrfForm_Rot0Click(window, (Button)s);
        window.Rot270.Click += (s, e) => TBrfForm_Rot0Click(window, (Button)s);
        window.RotMirr.Click += (s, e) => TBrfForm_Rot0Click(window, (Button)s);
        window.ClearIcon.Click += (s, e) => TBrfForm_ClearIconClick(window, s);
        window.IconInfo.Click += (s, e) => TBrfForm_IconInfoClick(window, s);
    }

    // L004FB520
    private static void FormCreate(BriefingWindow BrfForm)
    {
        Controls_TControl_SetTop(BrfForm.Guidememo, 0x1BF);
        Controls_TControl_SetLeft(BrfForm.GroupBox4, 0x266);
        AlliedVariables.s_V0x00542ECE = 0;
        AlliedVariables.s_V0x00542ED1 = 0x01;
        AlliedVariables.s_BrfFormXSnapBtnStep = 0x01;
        AlliedVariables.s_BrfFormYSnapBtnStep = 0x01;
        AlliedVariables.s_V0x005433EC = 0x13;
        AlliedVariables.s_V0x005433F0 = 0x16;
        AlliedVariables.s_V0x005433F4 = 0x1C;
        TBrfForm_Proc_00502A20(BrfForm);
        AlliedVariables.s_V0x005424C8 = 0;
        AlliedVariables.s_V0x00543408 = 0x01;
        ComCtrls_TTabSheet_SetTabVisible(ComCtrls_TPageControl_GetPage(BrfForm.BrfPages, 0x03), false);
        AlliedVariables.s_V0x0054248C = new();
        BrfForm.ColorBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_V0x0054248C);
        AlliedVariables.s_V0x00542490 = new();
        BrfForm.IndexBox.AlliedCopyComboxBoxItemsToTStrings(AlliedVariables.s_V0x00542490);

        for (int ebp0C = 0; ebp0C < 0x33; ebp0C++)
        {
            AlliedVariables.s_V0x005432D8[ebp0C] = new();
        }

        AlliedVariables.s_AlliedMapBitmaps = new AlliedMapBitmap[232];
        for (int i = 0; i < 232; i++)
        {
            AlliedVariables.s_AlliedMapBitmaps[i] = new()
            {
                IsLoaded = 0
            };
        }

        BrfForm.BrfSeenBy.CreateCollection(true);
        for (int ebp0C = 0; ebp0C < 0x08; ebp0C++)
        {
            string ebp14 = AlliedVariables.s_Strings_Teams.GetText(ebp0C);
            BrfForm.BrfSeenBy.AddItem(ebp14, false);
        }

        if (AlliedVariables.s_AlliedAplicationWidth == 0x280 || AlliedVariables.s_AlliedAplicationWidth == 0x1E0)
        {
            TApplication_L00468080(BrfForm, 0x01);
            Graphics_TFont_SetName(BrfForm, "MS Serif");
            Graphics_TFont_SetSize(BrfForm, 0x09);
            Graphics_TFont_SetName(BrfForm.BrfPages, "MS Serif");
            Graphics_TFont_SetSize(BrfForm.BrfPages, 0x09);
            Graphics_TFont_SetName(BrfForm.Memo1, "MS Serif");
            Graphics_TFont_SetSize(BrfForm.Memo1, 0x08);
            Graphics_TFont_SetName(BrfForm.Memo2, "MS Serif");
            Graphics_TFont_SetSize(BrfForm.Memo2, 0x08);
            Controls_TControl_SetHeight(BrfForm.BrfSeenBy, 0x78);
            BrfForm.MapIndex.SetMaxDropDownCount(0x14);
            Grids_TCustomGrid_SetColWidths(BrfForm.MapTagGrid, 0x01, 0xC8);
            Grids_TCustomGrid_SetColWidths(BrfForm.BrfStrGrid, 0x01, 0x258);
            Controls_TWinControl_ScaleBy(BrfForm, AlliedVariables.s_AlliedAplicationWidth, 0x32A);
            Controls_TControl_SetHeight(BrfForm, 0x1CD);
            Controls_TControl_SetWidth(BrfForm, 0x280);
            TApplication_RecreateWnd(BrfForm, 0x04);
        }
        else if (AlliedVariables.s_AlliedAplicationWidth == 0x320 || AlliedVariables.s_AlliedAplicationHeight == 0x258)
        {
            Grids_TCustomGrid_SetColWidths(BrfForm.MapTagGrid, 0x01, 0xC8);
            Grids_TCustomGrid_SetColWidths(BrfForm.BrfStrGrid, 0x01, 0x320);
        }
        else
        {
            Grids_TCustomGrid_SetColWidths(BrfForm.MapTagGrid, 0x01, (int)Math.Round(AlliedVariables.s_AlliedAplicationWidth / 3.0f));
            Grids_TCustomGrid_SetColWidths(BrfForm.BrfStrGrid, 0x01, AlliedVariables.s_AlliedAplicationWidth);
        }

        AlliedVariables.s_V0x005433A4 = new();
        AlliedVariables.s_V0x005433A8 = new();
        AlliedVariables.s_V0x00542ED2 = 0;
        string ebp08 = AlliedVariables.s_XWADirLabSetting + "\\Frontres\\MapIcons\\LIcon.bmp";

        if (File.Exists(ebp08))
        {
            AlliedVariables.s_V0x005433A4 = new(ebp08);
            AlliedVariables.s_V0x00542ED2 = 0x01;
        }
        else
        {
            ebp08 = AlliedVariables.s_AlliedDirectoryPath + "\\LIcon.bmp";

            if (File.Exists(ebp08))
            {
                AlliedVariables.s_V0x005433A4 = new(ebp08);
                AlliedVariables.s_V0x00542ED2 = 0x01;
            }
            else
            {
                MessageBox_ShowError("Licon File not found!\n\n\rCannot draw icons.");
            }
        }

        AlliedVariables.s_V0x005433AC = new(0, 0, 0x26, 0x2C);
        AlliedVariables.s_V0x005433BC = new(0, 0, 0x26, 0x38);
        AlliedVariables.s_V0x005433CC = new(0, 0, 0x2C, 0x26);
        AlliedVariables.s_V0x005433DC = new(0, 0, 0x38, 0x26);
        AlliedVariables.s_V0x00542EB8 = new((int)BrfForm.PaintBox1.Width, (int)BrfForm.PaintBox1.Height);
        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
        Graphics_TFont_SetName(eax1, "Verdana");
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
        Graphics_TFont_SetSize(eax1, 0x09);
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
        Graphics_TBrush_SetStyle(eax1, 0x01);
        AlliedVariables.s_V0x00542EBC = new((int)BrfForm.PaintBox1.Width, (int)BrfForm.PaintBox1.Height);
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
        Graphics_TBrush_SetStyle(eax1, 0x01);
        AlliedVariables.s_V0x00542EC5 = 0;
        AlliedVariables.s_V0x00542EC6 = 0;
        AlliedVariables.s_V0x00542EC7 = 0;
        AlliedVariables.s_V0x00542EC8 = 0;
        AlliedVariables.s_V0x00542EC9 = 0;
        AlliedVariables.s_V0x00542ECA = 0;
        AlliedVariables.s_V0x00542ECB = 0;
        AlliedVariables.s_V0x005424A8 = (int)BrfForm.ScrollBox1.Width / 2;
        AlliedVariables.s_V0x005424AC = (int)BrfForm.ScrollBox1.Height / 2;
        Grids_TCustomGrid_SetColWidths(BrfForm.MapTagGrid, 0, 0x14);
        Grids_TCustomGrid_SetColWidths(BrfForm.BrfStrGrid, 0, 0x14);
        BrfForm.ShipBox.SetItems(AlliedVariables.s_Strings_Ships);
        TBrfForm_Proc_005029DC(BrfForm);
        TBrfForm_Proc_005029F8(BrfForm);

        if (AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.CodeSize < 0x07
            && AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Length != 0x258)
        {
            int ebp0C = 0x1B59;

            while (true)
            {
                ebp0C--;

                if (AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebp0C - 1] != 0)
                {
                    break;
                }

                if (ebp0C <= 0x01)
                {
                    break;
                }
            }

            if (ebp0C > 0x01 && AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.CodeSize != ebp0C)
            {
                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.CodeSize = (short)ebp0C;
                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebp0C - 2] = 0x270F;
                MessageBox_ShowInformation("Briefing structure is inconsistent, attempting to correct it...");
            }
        }
    }

    // L00506140
    public static uint TBrfForm_GetIffColor(BriefingWindow eax0, byte iff)
    {
        var eax = iff switch
        {
            0x00 => (uint)0x0000FF00,
            0x01 => (uint)0x003C3CFF,
            0x02 => (uint)0x00FFC600,
            0x03 => (uint)0x0000FFFF,
            0x04 => (uint)0x000000FF,
            0x05 => (uint)0x00FF00FF,
            _ => (uint)0x00FF8000,
        };
        return eax;
    }

    // L00506194
    public static uint TBrfForm_GetColorIndexColor(BriefingWindow BrfForm, byte edx0)
    {
        return edx0 switch
        {
            0x00 => 0x0000FF00,
            0x01 => 0x002020FF,
            0x02 => 0x0000FFFF,
            0x03 => 0x00FFC600,
            0x04 => 0x00FF00FF,
            _ => 0,
        };
    }

    // L00506028
    private static bool TBrfForm_BrfPagesChanging(BriefingWindow BrfForm, object? Sender, SelectionChangedEventArgs e)
    {
        bool AllowChange = true;

        TabItem old = (TabItem)e.RemovedItems[0];

        if ((BrfPageEnum)Convert.ToInt32(old.Tag) == BrfPageEnum.WYSIWYG)
        {
            if (AlliedVariables.s_V0x00542EC8 != 0 || AlliedVariables.s_V0x00542EC9 != 0 || AlliedVariables.s_V0x00542EC7 != 0 || AlliedVariables.s_V0x00542EC6 != 0 || AlliedVariables.s_V0x00542EC5 != 0)
            {
                AllowChange = false;
            }
        }

        switch ((BrfPageEnum)Convert.ToInt32(old.Tag))
        {
            case BrfPageEnum.WYSIWYG:
            case BrfPageEnum.Strings:
                AlliedVariables.s_V0x005424F4 = AlliedVariables.s_V0x00542498;
                BrfForm.IndexBox.SetItems(AlliedVariables.s_V0x00542480);
                TBrfForm__PROC_004FCA50(BrfForm);
                BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
                TBrfForm__PROC_004FDECC(BrfForm, AlliedVariables.s_V0x00542498);
                break;
        }

        if (!AllowChange)
        {
            BrfForm.BrfPages.SelectedItem = old;
        }

        return AllowChange;
    }

    // L00505ED4
    private static void TBrfForm_BrfPagesChange(BriefingWindow BrfForm, object? Sender, SelectionChangedEventArgs e)
    {
        if (e.Source != Sender || e.RemovedItems.Count == 0)
        {
            return;
        }

        bool allowChange = TBrfForm_BrfPagesChanging(BrfForm, Sender, e);
        if (!allowChange)
        {
            return;
        }

        switch ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag))
        {
            case BrfPageEnum.WYSIWYG:
                StdCtrls_TScrollBar_SetMax(BrfForm.TimeScroll, AlliedVariables.s_V0x00542ED8 - 1);
                TApplication_L00468A40(AlliedVariables.s_TBrfForm_Instance!, BrfForm.ScrollBox1);
                break;

            case BrfPageEnum.InstructionList:
                TBrfForm_Proc_00503298(BrfForm);

                if (BrfForm.Pause.IsChecked != true)
                {
                    Buttons_TSpeedButton_SetDown(BrfForm.Play, false);
                }

                break;

            case BrfPageEnum.Strings:
                TBrfForm_Proc_00503298(BrfForm);

                if (BrfForm.Pause.IsChecked != true)
                {
                    Buttons_TSpeedButton_SetDown(BrfForm.Play, false);
                }

                break;
        }

        if ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag) == BrfPageEnum.WYSIWYG)
        {
            if (AlliedVariables.s_V0x00542EC8 != 0 || AlliedVariables.s_V0x00542EC9 != 0 || AlliedVariables.s_V0x00542EC7 != 0 || AlliedVariables.s_V0x00542EC6 != 0 || AlliedVariables.s_V0x00542EC5 != 0)
            {
                ComCtrls_TPageControl_SetActivePage(BrfForm.BrfPages, BrfForm.WYSIWYG);
            }
            else
            {
                switch ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag))
                {
                    case BrfPageEnum.WYSIWYG:
                    case BrfPageEnum.Strings:
                        AlliedVariables.s_V0x005424F4 = AlliedVariables.s_V0x00542498;
                        BrfForm.IndexBox.SetItems(AlliedVariables.s_V0x00542480);
                        TBrfForm__PROC_004FCA50(BrfForm);
                        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
                        TBrfForm__PROC_004FDECC(BrfForm, AlliedVariables.s_V0x00542498);
                        break;
                }
            }
        }

        if ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag) == BrfPageEnum.WYSIWYG)
        {
            TBrfForm_Proc_00502248(BrfForm);
        }
    }

    // L004FB220
    private static int TBrfForm_Proc_004FB220(BriefingWindow BrfForm, int edx0)
    {
        return (int)Math.Round(((float)AlliedVariables.s_V0x0054249C / (float)AlliedVariables.s_V0x005424F0) * edx0);
    }

    // L004FB244
    private static int TBrfForm__PROC_004FB244(BriefingWindow BrfForm, int edx0, int ecx0)
    {
        if (AlliedVariables.s_V0x0054249C == 0)
        {
            AlliedVariables.s_V0x0054249C = 0x04;
        }

        int eax1;

        if (ecx0 == 0)
        {
            int ebx = edx0 - AlliedVariables.s_V0x005424A8 - AlliedVariables.s_V0x005424A0;
            eax1 = (int)Math.Round((float)ebx / (float)AlliedVariables.s_V0x0054249C * (float)AlliedVariables.s_V0x005424F0);
        }
        else
        {
            int ebx = edx0 - AlliedVariables.s_V0x005424AC - AlliedVariables.s_V0x005424A4;
            eax1 = (int)Math.Round((float)ebx / (float)AlliedVariables.s_V0x0054249C * (float)AlliedVariables.s_V0x005424F0);
        }

        return eax1;
    }

    // L00502A20
    private static void TBrfForm_Proc_00502A20(BriefingWindow BrfForm)
    {
        for (int edx = 0; edx < 0x33; edx++)
        {
            S0x00542EDC eax = AlliedVariables.s_V0x00542EDC[edx];
            eax.m000006 = 0;
            eax.Rotation = IconRotationEnum.Rotate0;
            eax.m000008 = 0;
            eax.m00000C = 0;
        }

        AlliedVariables.s_V0x00542500 = 0;
        Controls_TControl_SetText(BrfForm.Label22, AlliedVariables.s_V0x00542500.ToString(CultureInfo.InvariantCulture));
    }

    // L005029DC
    private static void TBrfForm_Proc_005029DC(BriefingWindow BrfForm)
    {
        for (int edx = 0; edx < 0x08; edx++)
        {
            AlliedVariables.s_V0x00542E58[edx].m000002 = -1;
            AlliedVariables.s_V0x00542E58[edx].m000001 = 0;
        }
    }

    // L005029F8
    private static void TBrfForm_Proc_005029F8(BriefingWindow BrfForm)
    {
        for (int ecx = 0; ecx < 0x08; ecx++)
        {
            AlliedVariables.s_V0x00542E78[ecx] = -1;
            AlliedVariables.s_V0x00542E98[ecx] = 0;
        }
    }

    // L004FBD20
    private static void TBrfForm_FormActivate(BriefingWindow BrfForm, object? Sender)
    {
        Controls_TControl_SetTop(BrfForm, (int)BrfForm.Top - 0x0F);
        AlliedVariables.s_V0x005424F0 = 0xDF;

        for (int ebx = 0; ebx < 0x20; ebx++)
        {
            Grids_TStringGrid_SetCells(BrfForm.MapTagGrid, 0, ebx, ebx.ToString(CultureInfo.InvariantCulture));
        }

        for (int ebx = 0; ebx < 0x20; ebx++)
        {
            Grids_TStringGrid_SetCells(BrfForm.BrfStrGrid, 0, ebx, ebx.ToString(CultureInfo.InvariantCulture));
        }

        AlliedVariables.s_V0x00543B20 = 0;
        AlliedVariables.s_V0x0054247C = new();
        AlliedVariables.s_V0x00542480 = new();
        AlliedVariables.s_V0x00542484 = new();
        AlliedVariables.s_V0x00542488 = new();
        AlliedVariables.s_V0x00542EC0 = "<Mission Description from .lst file>";
        string ebp04 = "none";
        ebp04 = AlliedVariables.s_XWADirLabSetting + "\\Missions\\Mission.lst";

        if (!File.Exists(ebp04))
        {
            ebp04 = "none";
        }

        if (!string.Equals(ebp04, "none", StringComparison.Ordinal))
        {
            AlliedVariables.s_V0x00542488.LoadFromFile(ebp04);
            int ebp08 = AlliedVariables.s_V0x00542488.GetCount();

            for (int ebx = 0; ebx < ebp08; ebx++)
            {
                if (AlliedVariables.s_V0x00542488.GetText(ebx).ToUpperInvariant().IndexOf(Path.GetFileName(AlliedVariables.s_V0x00543BF8).ToUpperInvariant()) <= 0)
                {
                    continue;
                }

                AlliedVariables.s_V0x00542EC0 = LblBoxImpl.Unit_00513838_Proc_0051F4B8(AlliedVariables.s_V0x00542488.GetText(ebx + 1));
            }
        }

        BrfForm.Memo2.Clear();
        BrfForm.Memo2.AddLine(AlliedVariables.s_V0x00542EC0);

        AlliedVariables.s_V0x00542484.Add("-----");
        AlliedVariables.s_V0x00542484.Add("16   -   36 km  across");
        AlliedVariables.s_V0x00542484.Add("24   -   24 km  across");
        AlliedVariables.s_V0x00542484.Add("32   -   18 km  across (default)");
        AlliedVariables.s_V0x00542484.Add("48   -   12 km  across");
        AlliedVariables.s_V0x00542484.Add("64   -   9 km  across");
        AlliedVariables.s_V0x00542484.Add("80   -   7 km  across");
        AlliedVariables.s_V0x00542484.Add("100 -   6 km  across");
        AlliedVariables.s_V0x00542484.Add("168 -   3 km  across");

        TBrfForm_Proc_004FD968(BrfForm, AlliedVariables.s_V0x00543B20);
        TBrfForm_Proc_004FC44C(BrfForm, AlliedVariables.s_V0x00543B20);
        AlliedVariables.s_V0x00542504 = 0;
        AlliedVariables.s_V0x00542505 = 0x01;
        BrfForm.BeginOnFGBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
        BrfForm.BeginOnFGBox.SelectedIndex = 0;
        BrfForm.BeginZoomBox.SetItems(AlliedVariables.s_V0x00542484);
        BrfForm.BeginZoomBox.SelectedIndex = 0x03;
        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
        AlliedVariables.s_V0x00542ECF = 0x01;
        AlliedVariables.s_V0x00542ED0 = 0x01;
        StdCtrls_TScrollBar_SetMax(BrfForm.TimeScroll, AlliedVariables.s_V0x00542ED8);
        AlliedVariables.s_V0x00542498 = 0;
        AlliedVariables.s_V0x005424F4 = 0;
        AlliedVariables.s_V0x005424F8 = 0;
        TBrfForm_Proc_00502248(BrfForm);
    }

    // L004FD968
    private static void TBrfForm_Proc_004FD968(BriefingWindow BrfForm, int edx0)
    {
        S0xTieBriefing briefing = AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].Clone();
        AlliedVariables.s_TieBriefingData_Instance = briefing.BriefingData;
        AlliedVariables.s_V0x0054247C = briefing.BriefingTags;
        AlliedVariables.s_V0x00542480 = briefing.BriefingStrings;
    }

    // L004FC44C
    private static void TBrfForm_Proc_004FC44C(BriefingWindow BrfForm, int edx0)
    {
        TBrfForm__PROC_004FC338(BrfForm, edx0);
        double ebp10 = AlliedVariables.s_TieBriefingData_Instance.BriefingCode.Length;
        AlliedVariables.s_V0x00542ED8 = (int)Math.Round(ebp10);
        Controls_TControl_SetText(BrfForm.BrfTimeEd, Runtime_L0040A8D4_FloatToText(0, ebp10 / 25.0f));
        Controls_TControl_SetText(BrfForm.BrfUnk1Ed, AlliedVariables.s_TieBriefingData_Instance.BriefingCode.Time.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(BrfForm.BrfUnk2Ed, AlliedVariables.s_TieBriefingData_Instance.BriefingCode.Index.ToString(CultureInfo.InvariantCulture));
        Controls_TControl_SetText(BrfForm.BrfUnk3Ed, AlliedVariables.s_TieBriefingData_Instance.BriefingCode.Title.ToString(CultureInfo.InvariantCulture));

        for (int esi = 0; esi < 0x08; esi++)
        {
            StdCtrls_TCustomListBox_SetSelected(BrfForm.BrfSeenBy, esi, AlliedVariables.s_TieBriefingData_Instance.BriefingCode.ForTeam[esi] != 0);
        }

        TBrfForm_Proc_004FC5C8(BrfForm);
        AlliedVariables.s_V0x00542498 = 0;
        TBrfForm__PROC_004FDECC(BrfForm, AlliedVariables.s_V0x00542498);
        BrfForm.ComDisplay.SelectedIndex = 0;
        AlliedVariables.s_V0x00542504 = 0;
    }

    // L004FC338
    private static void TBrfForm__PROC_004FC338(BriefingWindow BrfForm, int edx0)
    {
        bool bl = AlliedVariables.s_V0x00543B53;
        TBrfForm_Proc_004FC1F0(BrfForm);
        TBrfForm__PROC_004FC290(BrfForm);
        string ebp0C_1 = Path.GetFileName(AlliedVariables.s_V0x00543BF8);
        string ebp0C_0 = AlliedVariables.s_Strings_Teams.GetText(AlliedVariables.s_V0x00543B20);
        string ebp0C_2 = "Briefing Editor - (" + ebp0C_1 + ")           " + "                              " + ebp0C_0;
        Controls_TControl_SetText(AlliedVariables.s_TBrfForm_Instance!, ebp0C_2);
        AlliedVariables.s_V0x00543B53 = bl;
    }

    // L004FC1F0
    private static void TBrfForm_Proc_004FC1F0(BriefingWindow BrfForm)
    {
        for (int ebp08 = 0; ebp08 < 0x20; ebp08++)
        {
            string ebp0C = AlliedVariables.s_V0x0054247C.GetText(ebp08);
            Grids_TStringGrid_SetCells(BrfForm.MapTagGrid, 0x01, ebp08, ebp0C);
        }
    }

    // L004FC290
    private static void TBrfForm__PROC_004FC290(BriefingWindow BrfForm)
    {
        for (int ebx = 0; ebx < 0x20; ebx++)
        {
            string ebp04 = AlliedVariables.s_V0x00542480.GetText(ebx);
            Grids_TStringGrid_SetCells(BrfForm.BrfStrGrid, 0x01, ebx, ebp04);
        }
    }

    // L004FC2F8
    public static void TBrfForm__PROC_004FC2F8(BriefingWindow BrfForm)
    {
        TBrfForm_Proc_004FD968(BrfForm, AlliedVariables.s_V0x00543B20);

        for (byte bl = 0; bl < 0x20; bl++)
        {
            AlliedVariables.s_V0x00542480.Put(bl, string.Empty);
            AlliedVariables.s_V0x0054247C.Put(bl, string.Empty);
        }
    }

    // L004FC5C8
    private static void TBrfForm_Proc_004FC5C8(BriefingWindow BrfForm)
    {
        S0xTieBriefingData esi = AlliedVariables.s_TieBriefingData_Instance;

        esi.BriefingCode.m00000A = (short[])AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A.Clone();
        esi.BriefingCode.CodeSize = AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.CodeSize;
        esi.BriefingCode.Length = AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Length;
        TBrfForm__PROC_004FC9F8(BrfForm);

        int ebx = 0;

        while (true)
        {
            TCommandObject eax1 = new();
            TCommand ebp4A8 = new();

            ebp4A8.Time = esi.BriefingCode.m00000A[ebx];
            ebx++;
            ebp4A8.BriefingCommand = (BriefingCommandEnum)esi.BriefingCode.m00000A[ebx];
            ebx++;
            ebp4A8.Parameter = 0;

            switch (ebp4A8.BriefingCommand)
            {
                case BriefingCommandEnum.None:
                    ebp4A8.BriefingCommand = BriefingCommandEnum.BriefingEnd;
                    ebp4A8.Time = 0x270F;
                    break;

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
                case BriefingCommandEnum.ChangeRegion:
                    ebp4A8.Parameter = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.X = 0;
                    ebp4A8.Y = 0;
                    break;

                case BriefingCommandEnum.MoveMap:
                case BriefingCommandEnum.ScaleMap:
                    ebp4A8.X = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.Y = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    break;

                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    ebp4A8.Parameter = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.X = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.Y = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.ColorIndex = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    break;

                case BriefingCommandEnum.NewIcon:
                    ebp4A8.Parameter = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.CraftId = (CraftIdEnum)esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.ColorIndex = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    break;

                case BriefingCommandEnum.ShowShipData:
                    ebp4A8.Parameter = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.IconIndex = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    break;

                case BriefingCommandEnum.MoveIcon:
                    ebp4A8.Parameter = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.X = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.Y = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    break;

                case BriefingCommandEnum.RotateIcon:
                    ebp4A8.Parameter = esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    ebp4A8.Rotation = (IconRotationEnum)esi.BriefingCode.m00000A[ebx];
                    ebx++;
                    break;
            }

            eax1.m000004 = ebp4A8;
            AlliedVariables.s_V0x00543CF4.Add(eax1);

            // todo
            if (ebx >= esi.BriefingCode.CodeSize - 1 || ebx >= 0x1B58 - 1)
            {
                break;
            }
        }

        int count = AlliedVariables.s_V0x00543CF4.Count;

        TBrfForm__PROC_004FCA50(BrfForm);
    }

    // L004FC9F8
    public static void TBrfForm__PROC_004FC9F8(BriefingWindow BrfForm)
    {
        AlliedVariables.s_V0x00543CF4.Clear();
    }

    // L004FCA50
    private static void TBrfForm__PROC_004FCA50(BriefingWindow BrfForm)
    {
        TBrfForm_Proc_005049B8(BrfForm);
        BrfForm.ComDisplay.Clear();
        AlliedVariables.s_V0x005424FC = 0;

        int ebp14 = AlliedVariables.s_V0x00543CF4.Count;

        for (int ebp08 = 0; ebp08 < ebp14; ebp08++)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebp08);
            TCommand ebp4B8 = eax1.m000004.Clone();

            if (ebp4B8.Parameter == -1)
            {
                ebp4B8.Parameter = 0;
            }

            StringBuilder ebp10_1 = new();

            if (ebp4B8.Time == 0x270F && ebp4B8.BriefingCommand == BriefingCommandEnum.BriefingEnd)
            {
                ebp10_1.Append("<end marker>");
                AlliedVariables.s_V0x005424FC += 0x02;
            }
            else
            {
                ebp10_1.Append(Runtime_L0040A8D4_FloatToText(0, ebp4B8.Time / 25.0f));

                switch (ebp10_1.Length)
                {
                    case 0x01:
                        ebp10_1.Append("            ");
                        break;

                    case 0x02:
                        ebp10_1.Append("          ");
                        break;

                    case 0x03:
                        ebp10_1.Append("         ");
                        break;

                    case 0x04:
                        ebp10_1.Append("       ");
                        break;

                    case 0x05:
                        ebp10_1.Append("     ");
                        break;

                    case 0x06:
                        ebp10_1.Append("   ");
                        break;
                }

                switch (ebp4B8.BriefingCommand)
                {
                    case BriefingCommandEnum.PageBreak:
                        ebp10_1.Append("===========================================" + "=================");
                        break;

                    case BriefingCommandEnum.ClearFlightGroupTags:
                        ebp10_1.Append("xxxx [  ]");
                        break;

                    case BriefingCommandEnum.FlightGroupTags1:
                    case BriefingCommandEnum.FlightGroupTags2:
                    case BriefingCommandEnum.FlightGroupTags3:
                    case BriefingCommandEnum.FlightGroupTags4:
                    case BriefingCommandEnum.FlightGroupTags5:
                    case BriefingCommandEnum.FlightGroupTags6:
                    case BriefingCommandEnum.FlightGroupTags7:
                    case BriefingCommandEnum.FlightGroupTags8:
                        ebp10_1.Append(string.Format(CultureInfo.InvariantCulture, " [ {0} ]", (int)ebp4B8.BriefingCommand - 0x08));
                        break;

                    case BriefingCommandEnum.ClearTextTags:
                        ebp10_1.Append("xxxx  ^");
                        break;

                    case BriefingCommandEnum.TextTag1:
                    case BriefingCommandEnum.TextTag2:
                    case BriefingCommandEnum.TextTag3:
                    case BriefingCommandEnum.TextTag4:
                    case BriefingCommandEnum.TextTag5:
                    case BriefingCommandEnum.TextTag6:
                    case BriefingCommandEnum.TextTag7:
                    case BriefingCommandEnum.TextTag8:
                        ebp10_1.Append(string.Format(CultureInfo.InvariantCulture, " ^{0}", (int)ebp4B8.BriefingCommand - 0x11));
                        break;

                    case BriefingCommandEnum.NewIcon:
                        ebp10_1.Append(" + ");
                        break;

                    case BriefingCommandEnum.ShowShipData:
                        ebp10_1.Append(" i_");

                        if (ebp4B8.Parameter == 0)
                        {
                            ebp10_1.Append("OFF");
                        }
                        else
                        {
                            ebp10_1.Append("ON");
                        }

                        ebp10_1.Append(string.Format(CultureInfo.InvariantCulture, " for Icon #{0}", ebp4B8.IconIndex));
                        break;

                    case BriefingCommandEnum.MoveIcon:
                        ebp10_1.Append("<->");
                        break;

                    case BriefingCommandEnum.RotateIcon:
                        ebp10_1.Append(" @ ");
                        break;

                    case BriefingCommandEnum.ChangeRegion:
                        ebp10_1.Append(" ~~~~~~~~  ");
                        break;

                    default:
                        ebp10_1.Append(BrfForm.CommandBox.GetItemText((int)ebp4B8.BriefingCommand));
                        break;
                }

                AlliedVariables.s_V0x005424FC += 0x02;

                switch (ebp4B8.BriefingCommand)
                {
                    case BriefingCommandEnum.BriefingTitle:
                        {
                            ebp10_1.Append("           \"");

                            if (AlliedVariables.s_V0x00542480.GetText(ebp4B8.Parameter).Length > 0x1F)
                            {
                                ebp10_1.Append(TBrfForm_Proc_004FD8C0(BrfForm, AlliedVariables.s_V0x00542480.GetText(ebp4B8.Parameter), 0x28));
                            }
                            else
                            {
                                ebp10_1.Append(AlliedVariables.s_V0x00542480.GetText(ebp4B8.Parameter));
                            }

                            ebp10_1.Append("\"");
                            AlliedVariables.s_V0x005424FC += 1;
                            break;
                        }

                    case BriefingCommandEnum.BriefingText:
                        {
                            ebp10_1.Append("      \"");

                            if (AlliedVariables.s_V0x00542480.GetText(ebp4B8.Parameter).Length > 0x33)
                            {
                                ebp10_1.Append(TBrfForm_Proc_004FD8C0(BrfForm, AlliedVariables.s_V0x00542480.GetText(ebp4B8.Parameter), 0x37));
                            }
                            else
                            {
                                ebp10_1.Append(AlliedVariables.s_V0x00542480.GetText(ebp4B8.Parameter));
                            }

                            ebp10_1.Append("\"");
                            AlliedVariables.s_V0x005424FC += 1;
                            break;
                        }

                    case BriefingCommandEnum.MoveMap:
                        {
                            string ebp51C_9 = Allied_FloatToText(0x02, 0x07, 0x02, ebp4B8.X / 160.0f);
                            string ebp51C_8 = Allied_FloatToText(0x02, 0x07, 0x02, ebp4B8.Y / 160.0f);
                            ebp10_1.Append("        X: " + ebp51C_9 + ",   Y: " + ebp51C_8);
                            AlliedVariables.s_V0x005424FC += 0x02;
                            break;
                        }

                    case BriefingCommandEnum.ScaleMap:
                        {
                            string ebp51C_7 = ebp4B8.X.ToString(CultureInfo.InvariantCulture);
                            string ebp51C_6 = ebp4B8.Y.ToString(CultureInfo.InvariantCulture);
                            ebp10_1.Append("        X: " + ebp51C_7 + ",   Y: " + ebp51C_6);
                            AlliedVariables.s_V0x005424FC += 0x02;
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
                        ebp10_1.Append("        Icon#" + ebp4B8.Parameter.ToString(CultureInfo.InvariantCulture));
                        AlliedVariables.s_V0x005424FC += 1;
                        break;

                    case BriefingCommandEnum.TextTag1:
                    case BriefingCommandEnum.TextTag2:
                    case BriefingCommandEnum.TextTag3:
                    case BriefingCommandEnum.TextTag4:
                    case BriefingCommandEnum.TextTag5:
                    case BriefingCommandEnum.TextTag6:
                    case BriefingCommandEnum.TextTag7:
                    case BriefingCommandEnum.TextTag8:
                        {
                            ebp10_1.Append("      \"" + AlliedVariables.s_V0x0054247C.GetText(ebp4B8.Parameter) + "\"");
                            string ebp51C_3 = Allied_FloatToText(0x02, 0x07, 0x02, ebp4B8.X / 160.0f);
                            string ebp51C_2 = Allied_FloatToText(0x02, 0x07, 0x02, ebp4B8.Y / 160.0f);
                            ebp10_1.Append("    at    X: " + ebp51C_3 + ",   Y: " + ebp51C_2);
                            ebp10_1.Append("     Color: " + AlliedVariables.s_V0x0054248C.GetText(ebp4B8.ColorIndex));
                            AlliedVariables.s_V0x005424FC += 0x04;
                            break;
                        }

                    case BriefingCommandEnum.NewIcon:
                        {
                            string ebp548_2 = AlliedVariables.s_Strings_Ships.GetText((int)AlliedConvertCraftIdToShipSeq(ebp4B8.CraftId));
                            string ebp548_1 = AlliedVariables.s_Strings_IFF.GetText(ebp4B8.ColorIndex);
                            ebp10_1.Append(string.Format(CultureInfo.InvariantCulture, " #{0}:    {1}     IFF: {2}", ebp4B8.Parameter, ebp548_2, ebp548_1));
                            break;
                        }

                    case BriefingCommandEnum.MoveIcon:
                        {
                            string ebp564_2 = Allied_FloatToText(0x02, 0x07, 0x02, ebp4B8.X / 160.0f);
                            string ebp564_1 = Allied_FloatToText(0x02, 0x07, 0x02, ebp4B8.Y / 160.0f);
                            ebp10_1.Append(string.Format(CultureInfo.InvariantCulture, " #{0}", ebp4B8.Parameter) + "   to    X: " + ebp564_2 + ",   Y: " + ebp564_1);
                            break;
                        }

                    case BriefingCommandEnum.RotateIcon:
                        {
                            string ebp10_0 = ebp4B8.Rotation switch
                            {
                                IconRotationEnum.Rotate0 => "0 deg",
                                IconRotationEnum.Rotate270 => "270 deg",
                                IconRotationEnum.Rotate180 => "180 deg",
                                IconRotationEnum.Rotate90 => "90 deg",
                                IconRotationEnum.Mirror => "mirror",
                                _ => ((int)ebp4B8.Rotation).ToString(CultureInfo.InvariantCulture),
                            };

                            ebp10_1.Append(string.Format(CultureInfo.InvariantCulture, " #{0}   by   {1}", ebp4B8.Parameter, ebp10_0));
                            break;
                        }

                    case BriefingCommandEnum.ChangeRegion:
                        {
                            string ebp58C_2 = System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Regions[ebp4B8.Parameter].Name, 0x84);
                            ebp10_1.Append(string.Format(CultureInfo.InvariantCulture, " Region#{0}: \"{1}\"  ~~~~~~~~~~~~", ebp4B8.Parameter, ebp58C_2));
                            break;
                        }
                }
            }

            // ebp10_1.Append(" *error");
            // System_DoneExcept();

            BrfForm.ComDisplay.AddItem(ebp10_1.ToString());
        }

        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
        Controls_TControl_SetText(BrfForm.Label25, AlliedVariables.s_V0x005424FC.ToString(CultureInfo.InvariantCulture));
    }

    // L005049B8
    private static void TBrfForm_Proc_005049B8(BriefingWindow BrfForm)
    {
        int esp00 = AlliedVariables.s_V0x00543CF4.Count - 0x01;

        for (int esi = 0; esi < esp00; esi++)
        {
            int edi = AlliedVariables.s_V0x00543CF4.Count;

            for (int ebx = esi + 1; ebx < edi; ebx++)
            {
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);
                TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

                if (eax1.m000004.Time >= eax2.m000004.Time)
                {
                    continue;
                }

                if (esi == AlliedVariables.s_V0x00542498)
                {
                    AlliedVariables.s_V0x00542498 = ebx;
                }
                else if (ebx == AlliedVariables.s_V0x00542498)
                {
                    AlliedVariables.s_V0x00542498 = esi;
                }

                eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);
                TCommand esp04 = eax2.m000004.Clone();
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);
                eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);
                eax2.m000004 = eax1.m000004.Clone();
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);
                eax1.m000004 = esp04;
            }
        }
    }

    // L004FDECC
    private static void TBrfForm__PROC_004FDECC(BriefingWindow BrfForm, int edx0)
    {
        if (edx0 < 0)
        {
            return;
        }

        AlliedVariables.s_V0x00542505 = 0;
        Controls_TControl_SetText(BrfForm.Label14, string.Format(CultureInfo.InvariantCulture, "Instruction #{0}", edx0 + 1));

        if (edx0 < AlliedVariables.s_V0x00543CF4.Count)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, edx0);
            AlliedVariables.s_V0x00542510 = eax1.m000004.Clone();

            switch (AlliedVariables.s_V0x00542510.BriefingCommand)
            {
                case BriefingCommandEnum.BriefingTitle:
                case BriefingCommandEnum.BriefingText:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_V0x00542480);
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;

                case BriefingCommandEnum.MoveMap:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;

                case BriefingCommandEnum.ScaleMap:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_V0x00542484);
                    BrfForm.IndexBox.SelectedIndex = 0;
                    break;

                case BriefingCommandEnum.FlightGroupTags1:
                case BriefingCommandEnum.FlightGroupTags2:
                case BriefingCommandEnum.FlightGroupTags3:
                case BriefingCommandEnum.FlightGroupTags4:
                case BriefingCommandEnum.FlightGroupTags5:
                case BriefingCommandEnum.FlightGroupTags6:
                case BriefingCommandEnum.FlightGroupTags7:
                case BriefingCommandEnum.FlightGroupTags8:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                    BrfForm.IndexBox.PutItem(0, "0");
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;

                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_V0x0054247C);
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;

                case BriefingCommandEnum.NewIcon:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                    BrfForm.IndexBox.PutItem(0, "0");
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    BrfForm.ShipBox.SelectedIndex = (int)AlliedConvertCraftIdToShipSeq(AlliedVariables.s_V0x00542510.CraftId);
                    break;

                case BriefingCommandEnum.ShowShipData:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                    BrfForm.IndexBox.PutItem(0, "0");
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.IconIndex;
                    BrfForm.ColorBox.Clear();
                    BrfForm.ColorBox.AddItem("Off");
                    BrfForm.ColorBox.AddItem("On");
                    BrfForm.ColorBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;

                case BriefingCommandEnum.MoveIcon:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                    BrfForm.IndexBox.PutItem(0, "0");
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;

                case BriefingCommandEnum.RotateIcon:
                    BrfForm.IndexBox.SetItems(AlliedVariables.s_Allied_Numbers_NoneTo255);
                    BrfForm.IndexBox.PutItem(0, "0");
                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;

                case BriefingCommandEnum.ChangeRegion:
                    BrfForm.IndexBox.Clear();

                    for (int edi = 0; edi < 0x04; edi++)
                    {
                        BrfForm.IndexBox.AddItem(System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Regions[edi].Name, 0x84));
                    }

                    BrfForm.IndexBox.SelectedIndex = AlliedVariables.s_V0x00542510.Parameter;
                    break;
            }

            switch (AlliedVariables.s_V0x00542510.BriefingCommand)
            {
                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    BrfForm.ColorBox.SetItems(AlliedVariables.s_V0x0054248C);
                    BrfForm.ColorBox.SelectedIndex = AlliedVariables.s_V0x00542510.ColorIndex;
                    break;

                case BriefingCommandEnum.NewIcon:
                    BrfForm.ColorBox.SetItems(AlliedVariables.s_Strings_IFF);
                    BrfForm.ColorBox.SelectedIndex = AlliedVariables.s_V0x00542510.ColorIndex;
                    break;

                case BriefingCommandEnum.RotateIcon:
                    BrfForm.ColorBox.SetItems(AlliedVariables.s_V0x00542490);
                    BrfForm.ColorBox.SelectedIndex = (int)AlliedVariables.s_V0x00542510.Rotation;
                    break;
            }

            BrfForm.CommandBox.SelectedIndex = (int)AlliedVariables.s_V0x00542510.BriefingCommand;

            if (AlliedVariables.s_V0x00542510.BriefingCommand == BriefingCommandEnum.BriefingEnd)
            {
                BrfForm.TimeSpin.IsEnabled = false;
                BrfForm.CommandBox.IsEnabled = false;
                Controls_TControl_SetVisible(BrfForm.Label15, false);
            }
            else
            {
                BrfForm.TimeSpin.IsEnabled = true;
                BrfForm.CommandBox.IsEnabled = true;
                Controls_TControl_SetVisible(BrfForm.Label15, true);
            }

            if (AlliedVariables.s_V0x00542510.BriefingCommand == BriefingCommandEnum.ScaleMap)
            {
                Controls_TControl_SetText(BrfForm.XEd, AlliedVariables.s_V0x00542510.X.ToString(CultureInfo.InvariantCulture));
                Controls_TControl_SetText(BrfForm.YEd, AlliedVariables.s_V0x00542510.Y.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                Controls_TControl_SetText(BrfForm.XEd, Allied_FloatToText(0x02, 0x07, 0x02, AlliedVariables.s_V0x00542510.X / 160.0f));
                Controls_TControl_SetText(BrfForm.YEd, Allied_FloatToText(0x02, 0x07, 0x02, AlliedVariables.s_V0x00542510.Y / 160.0f));
            }

            Spin_TSpinEdit_SetValue(BrfForm.TimeSpin, AlliedVariables.s_V0x00542510.Time);
            AlliedVariables.s_V0x005424D8 = AlliedVariables.s_V0x00542510.Time;
            TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            Controls_TControl_SetText(BrfForm.Label15, "Time (" + Runtime_L0040A8D4_FloatToText(0, eax2.m000004.Time / 25.0f) + " sec)");
            TBrfForm__PROC_004FF548(BrfForm, AlliedVariables.s_V0x00542510.BriefingCommand);
        }

        AlliedVariables.s_V0x00542505 = 0x01;
    }

    // L004FF548
    private static void TBrfForm__PROC_004FF548(BriefingWindow BrfForm, BriefingCommandEnum edx0)
    {
        switch (edx0)
        {
            case BriefingCommandEnum.ScaleMap:
                Controls_TControl_SetText(BrfForm.XLab, "Zoom:");
                break;

            case BriefingCommandEnum.NewIcon:
                Controls_TControl_SetText(BrfForm.ColorLab, "         IFF:");
                break;

            case BriefingCommandEnum.ShowShipData:
                Controls_TControl_SetText(BrfForm.ColorLab, "      Info:");
                break;

            case BriefingCommandEnum.RotateIcon:
                Controls_TControl_SetText(BrfForm.ColorLab, "Rotation:");
                break;

            default:
                Controls_TControl_SetText(BrfForm.XLab, "      X:");
                Controls_TControl_SetText(BrfForm.ColorLab, "      Color:");
                break;
        }

        switch (edx0)
        {
            case BriefingCommandEnum.PageBreak:
            case BriefingCommandEnum.ClearFlightGroupTags:
            case BriefingCommandEnum.ClearTextTags:
            case BriefingCommandEnum.BriefingEnd:
                TBrfForm_Proc_004FF9C0(BrfForm, false, false, "                  Unused", false, false, false);
                break;

            case BriefingCommandEnum.BriefingTitle:
            case BriefingCommandEnum.BriefingText:
                if (edx0 == BriefingCommandEnum.BriefingTitle)
                {
                    TBrfForm_Proc_004FF9C0(BrfForm, true, false, "  Title (won't show up in Briefing):", false, false, false);
                }
                else
                {
                    TBrfForm_Proc_004FF9C0(BrfForm, true, false, "             Text to show:", false, false, false);
                }

                break;

            case BriefingCommandEnum.MoveMap:
            case BriefingCommandEnum.ScaleMap:
                if (edx0 == BriefingCommandEnum.MoveMap)
                {
                    TBrfForm_Proc_004FF9C0(BrfForm, false, false, "        Option: Center on Icon:", false, true, true);
                }
                else
                {
                    TBrfForm_Proc_004FF9C0(BrfForm, false, false, "             Zoom 'presets':", false, false, true);
                }

                break;

            case BriefingCommandEnum.FlightGroupTags1:
            case BriefingCommandEnum.FlightGroupTags2:
            case BriefingCommandEnum.FlightGroupTags3:
            case BriefingCommandEnum.FlightGroupTags4:
            case BriefingCommandEnum.FlightGroupTags5:
            case BriefingCommandEnum.FlightGroupTags6:
            case BriefingCommandEnum.FlightGroupTags7:
            case BriefingCommandEnum.FlightGroupTags8:
                TBrfForm_Proc_004FF9C0(BrfForm, true, false, "                   Icon # to Box:", false, false, false);
                break;

            case BriefingCommandEnum.TextTag1:
            case BriefingCommandEnum.TextTag2:
            case BriefingCommandEnum.TextTag3:
            case BriefingCommandEnum.TextTag4:
            case BriefingCommandEnum.TextTag5:
            case BriefingCommandEnum.TextTag6:
            case BriefingCommandEnum.TextTag7:
            case BriefingCommandEnum.TextTag8:
                TBrfForm_Proc_004FF9C0(BrfForm, true, true, "                   Map Tag:", false, true, true);
                break;

            case BriefingCommandEnum.NewIcon:
                TBrfForm_Proc_004FF9C0(BrfForm, true, true, "            Icon # to Add:", true, false, false);
                break;

            case BriefingCommandEnum.ShowShipData:
                TBrfForm_Proc_004FF9C0(BrfForm, true, true, "          Ship info for Icon:", false, false, false);
                break;

            case BriefingCommandEnum.MoveIcon:
                TBrfForm_Proc_004FF9C0(BrfForm, true, false, "           Icon # to move:", false, true, true);
                break;

            case BriefingCommandEnum.RotateIcon:
                TBrfForm_Proc_004FF9C0(BrfForm, true, true, "               Icon # to Rotate:", false, false, false);
                break;

            case BriefingCommandEnum.ChangeRegion:
                TBrfForm_Proc_004FF9C0(BrfForm, true, false, "               Region:", false, false, false);
                break;
        }
    }

    // L004FF9C0
    private static void TBrfForm_Proc_004FF9C0(BriefingWindow BrfForm, bool edx0, bool ecx0, string A4, bool A8, bool AC, bool A10)
    {
        Controls_TControl_SetVisible(BrfForm.IndexBox, edx0);
        BrfForm.IndexLab.IsEnabled = edx0;
        Controls_TControl_SetVisible(BrfForm.ColorBox, ecx0);
        BrfForm.ColorLab.IsEnabled = ecx0;
        Controls_TControl_SetVisible(BrfForm.XEd, A10);
        BrfForm.XLab.IsEnabled = A10;
        Controls_TControl_SetVisible(BrfForm.YEd, AC);
        BrfForm.YLab.IsEnabled = AC;
        Controls_TControl_SetVisible(BrfForm.ShipBox, A8);
        Controls_TControl_SetText(BrfForm.IndexLab, A4);
    }

    // L00502624
    private static void TBrfForm_Proc_00502624(BriefingWindow BrfForm)
    {
        if (AlliedVariables.s_V0x00543408 == 0)
        {
            return;
        }

        if (AlliedVariables.s_V0x00542ED3 != 0)
        {
            string ebp10 = AlliedVariables.s_Strings_Ships.GetText((int)AlliedConvertCraftIdToShipSeq(AlliedVariables.s_V0x00542ED4));
            TBrfForm_Proc_005027D0(BrfForm, string.Format(CultureInfo.InvariantCulture, "Ship Info Displayed for {0}", ebp10));
        }
        else if (AlliedVariables.s_V0x0054249C != 0 && AlliedVariables.s_V0x005429B4.BriefingCommand != BriefingCommandEnum.ChangeRegion)
        {
            if (AlliedVariables.s_V0x0054250C != 0)
            {
                TBrfForm_Proc_00502590(BrfForm);
            }

            //TRect ebp20 = BrfForm.GetClientRect();
            //TRect ebp30 = BrfForm.GetClientRect();
            //Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!, ebp30, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp20);
            AlliedVariables.s_V0x00542EB8?.CopyDirectBitmap(AlliedVariables.s_V0x00542EBC!);

            TBrfForm_Proc_00502A9C(BrfForm);
            TBrfForm_Proc_00502CC0(BrfForm);

            if (AlliedVariables.s_V0x00542EC4 != 0)
            {
                //ebp20 = BrfForm.GetClientRect();
                //ebp30 = BrfForm.GetClientRect();
                //Graphics_TCanvas_CopyRect(, ebp30, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!, ebp20);
                BrfForm.PaintBox1.Bitmap!.CopyDirectBitmap(AlliedVariables.s_V0x00542EB8!);
            }

            TBrfForm_Proc_00503D98(BrfForm);
        }
    }

    // L00502248
    private static void TBrfForm_Proc_00502248(BriefingWindow BrfForm)
    {
        AlliedVariables.s_V0x00542498 = 0;
        AlliedVariables.s_V0x00542494 = 0;
        AlliedVariables.s_V0x005424A0 = 0;
        AlliedVariables.s_V0x005424B4 = 0;
        AlliedVariables.s_V0x005424BC = 0;
        AlliedVariables.s_V0x005424B8 = 0;
        AlliedVariables.s_V0x005424A4 = 0;
        AlliedVariables.s_V0x005424C0 = 0;
        AlliedVariables.s_V0x0054249C = 0x20;
        AlliedVariables.s_V0x00542506 = 0;
        AlliedVariables.s_V0x00542507 = 0;
        AlliedVariables.s_V0x005424B4 = AlliedVariables.s_V0x005424A0;
        AlliedVariables.s_V0x005424B8 = AlliedVariables.s_V0x005424A4;
        AlliedVariables.s_V0x00542508 = 0;
        AlliedVariables.s_V0x00542509 = 0;
        AlliedVariables.s_V0x0054250A = 0;
        AlliedVariables.s_V0x0054250B = 0;
        AlliedVariables.s_V0x005424B0 = AlliedVariables.s_V0x0054249C;
        TBrfForm_Proc_005029DC(BrfForm);
        TBrfForm_Proc_005029F8(BrfForm);
        TBrfForm_Proc_00502A20(BrfForm);
        AlliedVariables.s_V0x00542500 = 0;
        Controls_TControl_SetText(BrfForm.Label22, AlliedVariables.s_V0x00542500.ToString(CultureInfo.InvariantCulture));
        AlliedVariables.s_V0x005424C8 = 0;
        AlliedVariables.s_V0x00542ED3 = 0;
        Controls_TControl_SetText(BrfForm.TimeLab, " Time: 0.00");
        Controls_TControl_SetText(BrfForm.Label20, "Instruction #" + (AlliedVariables.s_V0x00542498 + 1).ToString(CultureInfo.InvariantCulture) + " :");

        if (AlliedVariables.s_V0x00543CF4.Count > 0)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            TBrfForm_Proc_00506BD8(BrfForm, eax1.m000004);
        }

        AlliedVariables.s_V0x00542EC4 = 0;

        if (AlliedVariables.s_V0x00543CF4.Count > 0)
        {
            TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            AlliedVariables.s_V0x005429B4 = eax2.m000004.Clone();
        }
        else
        {
            AlliedVariables.s_V0x005429B4 = new();
        }

        if (BrfForm.Timer1.GetM000048() && AlliedVariables.s_V0x00543CF4.Count > 0x01)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, 0);
            TBrfForm_Proc_00501570(BrfForm, eax1.m000004);
        }
        else if (AlliedVariables.s_V0x00542ED0 != 0 && !BrfForm.Timer1.GetM000048())
        {
            int edi0 = AlliedVariables.s_V0x00543CF4.Count;

            for (int esi = 0; esi < edi0; esi++)
            {
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

                if (eax1.m000004.Time != 0)
                {
                    continue;
                }

                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

                if (eax1.m000004.BriefingCommand != BriefingCommandEnum.ScaleMap)
                {
                    continue;
                }

                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);
                TBrfForm_Proc_00501570(BrfForm, eax1.m000004);
            }

            int edi1 = AlliedVariables.s_V0x00543CF4.Count;

            for (int esi = 0; esi < edi1; esi++)
            {
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

                if (eax1.m000004.Time != 0)
                {
                    continue;
                }

                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

                if (eax1.m000004.BriefingCommand == BriefingCommandEnum.ScaleMap)
                {
                    continue;
                }

                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);
                TBrfForm_Proc_00501570(BrfForm, eax1.m000004);
            }
        }

        AlliedVariables.s_V0x005424C4 = 0x01;
        AlliedVariables.s_V0x00542EC4 = 0x01;
        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
        AlliedVariables.s_V0x00542ECF = 0;
        StdCtrls_TScrollBar_SetPosition(BrfForm.TimeScroll, AlliedVariables.s_V0x00542494);
        AlliedVariables.s_V0x00542ECF = 0x01;
    }

    // L00506BD8
    private static void TBrfForm_Proc_00506BD8(BriefingWindow BrfForm, TCommand edx0)
    {
        TCommand ebp4A8 = edx0.Clone();
        string ebp04 = BrfForm.CommandBox.GetItemText((int)ebp4A8.BriefingCommand);

        switch (ebp4A8.BriefingCommand)
        {
            case BriefingCommandEnum.NewIcon:
            case BriefingCommandEnum.MoveIcon:
            case BriefingCommandEnum.RotateIcon:
                {
                    ebp04 += string.Format(CultureInfo.InvariantCulture, " #{0}", ebp4A8.Parameter);
                    break;
                }

            case BriefingCommandEnum.ShowShipData:
                {
                    ebp04 += string.Format(CultureInfo.InvariantCulture, " for Icon #{0}", ebp4A8.IconIndex);
                    break;
                }
        }

        Controls_TControl_SetText(BrfForm.InstrLabel, ebp04);
    }

    // L00502590
    private static void TBrfForm_Proc_00502590(BriefingWindow BrfForm)
    {
        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
        eax1.RenderOpen();
        Graphics_TBrush_SetColor(eax1, 0x0029121A);
        TRect ebp10 = BrfForm.GetClientRect();
        Graphics_TCanvas_FillRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp10);
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
        Graphics_TBrush_SetStyle(eax1, 0x01);
        TBrfForm__PROC_004FB2B4(BrfForm);
        TBrfForm_Proc_00501E40_Draw(BrfForm);
        eax1.RenderClose();
        AlliedVariables.s_V0x0054250C = 0;
    }

    // L004FB2B4
    private static void TBrfForm__PROC_004FB2B4(BriefingWindow BrfForm)
    {
        int edi;

        if (AlliedVariables.s_AlliedAplicationWidth < 0x320)
        {
            edi = (int)Math.Round(Unit_00513838_Proc_0051C570(0x73, 0) / 100.0f * AlliedVariables.s_V0x0054249C);
        }
        else
        {
            edi = (int)Math.Round(AlliedVariables.s_V0x0054249C * 1.15);
        }

        TBitmap esi = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;

        for (int ebx = -0x3C; ebx < 0x3D; ebx++)
        {
            int ebp08 = (int)Math.Round((float)(ebx * edi)) + AlliedVariables.s_V0x005424A8 + AlliedVariables.s_V0x005424A0;
            int ebp0C = (int)Math.Round((float)(ebx * edi)) + AlliedVariables.s_V0x005424AC + AlliedVariables.s_V0x005424A4;

            if (ebx % 0x04 == 0)
            {
                Graphics_TPen_SetColor(esi, 0x9F);
            }
            else
            {
                Graphics_TPen_SetColor(esi, 0x57);
            }

            if ((ebx % 0x02 == 0 && AlliedVariables.s_V0x0054249C > 0x0F && AlliedVariables.s_V0x0054249C < 0x20) || AlliedVariables.s_V0x0054249C >= 0x20 || (AlliedVariables.s_V0x0054249C < 0x10 && ebx % 0x04 == 0))
            {
                Graphics_TCanvas_MoveTo(esi, ebp08, 0);
                Graphics_TCanvas_LineTo(esi, ebp08, (int)BrfForm.ScrollBox1.Height);
                Graphics_TCanvas_MoveTo(esi, 0, ebp0C);
                Graphics_TCanvas_LineTo(esi, (int)BrfForm.ScrollBox1.Width, ebp0C);
            }
        }

        Graphics_TFont_SetColor(esi, 0x00FFFFFF);
        Graphics_TFont_SetName(esi, "Verdana");
        Graphics_TFont_SetSize(esi, 0x09);
        Graphics_TCanvas_TextOut(esi, 0x05, 0x02, "Briefing: ");
        Graphics_TFont_SetColor(esi, 0x0000FFFF);
        S0xTieTeamObject eax1 = Classes_TList_Get(AlliedVariables.s_TeamsObjectsList, AlliedVariables.s_V0x00543B20);
        Graphics_TCanvas_TextOut(esi, 0x3C, 0x02, System_LStrFromPCharLen(eax1.Team.Name, 0x10));
    }

    // L00501E40
    private static void TBrfForm_Proc_00501E40_Draw(BriefingWindow BrfForm)
    {
        if (AlliedVariables.s_V0x00542ED2 == 0)
        {
            return;
        }

        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
        Graphics_TBrush_SetStyle(eax1, 0x01);

        for (int ebp08 = 0; ebp08 < 0x33; ebp08++)
        {
            if (AlliedVariables.s_V0x00542EDC[ebp08].m000006 == 0)
            {
                continue;
            }

            switch (AlliedVariables.s_V0x00542EDC[ebp08].CraftId)
            {
                case CraftIdEnum._000__1_0:
                case CraftIdEnum._086_1_11_AsteroidHR1:
                case CraftIdEnum._087_1_11_AsteroidHR1:
                case CraftIdEnum._164_0_136_CrewCabinFront:
                case CraftIdEnum._169_0_139_EngineFront:
                case CraftIdEnum._183_9001_1100_ResData_Backdrop:
                    continue;
            }

            int ebp0C = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x00542EDC[ebp08].X) + AlliedVariables.s_V0x005424A8 + AlliedVariables.s_V0x005424A0;
            int ebp10 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x00542EDC[ebp08].Y) + AlliedVariables.s_V0x005424AC + AlliedVariables.s_V0x005424A4;

            TBitmap eax2 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
            eax2.Color = 0x00EE0086;

            if (AlliedVariables.s_V0x00542EDC[ebp08].CraftId == CraftIdEnum._227__1_0)
            {
                TBitmap eax3 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
                Graphics_TPen_SetColor(eax3, TBrfForm_GetIffColor(BrfForm, AlliedVariables.s_V0x00542EDC[ebp08].Iff));
                Graphics_TCanvas_Ellipse_L004269D0(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp0C + 0x23, ebp10 + 0x23, ebp0C - 0x23, ebp10 - 0x23);
            }
            else
            {
                TRect ebp2C;

                switch (AlliedVariables.s_V0x00542EDC[ebp08].CraftId)
                {
                    case CraftIdEnum._049_0_89_CalamariCruiserNew:
                    case CraftIdEnum._051_0_91_Interdictor2:
                    case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
                    case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
                    case CraftIdEnum._054_0_94_SuperStarDestroyer:
                    case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
                    case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                        switch (AlliedVariables.s_V0x00542EDC[ebp08].Rotation)
                        {
                            case IconRotationEnum.Rotate0:
                            case IconRotationEnum.Rotate180:
                            case IconRotationEnum.Mirror:
                                ebp2C = AlliedVariables.s_V0x005433BC;
                                break;

                            default:
                                ebp2C = AlliedVariables.s_V0x005433DC;
                                break;
                        }

                        break;

                    default:
                        switch (AlliedVariables.s_V0x00542EDC[ebp08].Rotation)
                        {
                            case IconRotationEnum.Rotate0:
                            case IconRotationEnum.Rotate180:
                            case IconRotationEnum.Mirror:
                                ebp2C = AlliedVariables.s_V0x005433AC;
                                break;

                            default:
                                ebp2C = AlliedVariables.s_V0x005433CC;
                                break;
                        }

                        break;
                }

                int ebx1 = AlliedVariables.s_V0x005432D8[ebp08]!.Height / 2;
                int esi1 = AlliedVariables.s_V0x005432D8[ebp08]!.Width / 2;
                TRect ebp3C = new(0, 0, esi1 * 2, ebx1 * 2);
                TRect ebp4C = new(ebp0C - esi1, ebp10 - ebx1, ebp0C + esi1, ebp10 + ebx1);
                Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp4C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[ebp08])!, ebp3C);
            }

            //// "error drawing"
            /* 0x00502238 = "error drawing" */
            //Graphics_TCanvas_TextOut( Graphics_TBitmap_GetCanvas( s_V0x00542EBC ), ebp0C, ebp10, (StrRec*)0x00502238 );
            //// System_DoneExcept();

            if (BrfForm.ShowNums.IsChecked == true)
            {
                TBitmap eax4 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
                Graphics_TFont_SetColor(eax4, 0x00C0C0C0);
                Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp0C + 0x0A, ebp10 + 0x0A, ebp08.ToString(CultureInfo.InvariantCulture));
            }

            if (BrfForm.ShowCurr.IsChecked == true && ebp08 == AlliedVariables.s_V0x005424C8)
            {
                TBitmap ebx = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
                Graphics_TPen_SetColor(ebx, 0x0000FFFF);
                int esi = ebp0C - 0x08;
                Graphics_TCanvas_MoveTo(ebx, esi, ebp10 - 0x05);
                int edi1 = ebp10 - 0x08;
                Graphics_TCanvas_LineTo(ebx, esi, edi1);
                Graphics_TCanvas_LineTo(ebx, ebp0C - 0x05, edi1);
                Graphics_TCanvas_MoveTo(ebx, ebp0C + 0x05, edi1);
                Graphics_TCanvas_LineTo(ebx, ebp0C + 0x07, edi1);
                Graphics_TCanvas_LineTo(ebx, ebp0C + 0x07, ebp10 - 0x04);
                Graphics_TCanvas_MoveTo(ebx, ebp0C + 0x07, ebp10 + 0x05);
                int edi = ebp10 + 0x08;
                Graphics_TCanvas_LineTo(ebx, ebp0C + 0x07, edi);
                Graphics_TCanvas_LineTo(ebx, ebp0C + 0x04, edi);
                Graphics_TCanvas_MoveTo(ebx, ebp0C - 0x06, edi);
                Graphics_TCanvas_LineTo(ebx, esi, edi);
                Graphics_TCanvas_LineTo(ebx, esi, ebp10 + 0x04);
            }
        }
    }

    // L00502A9C
    private static void TBrfForm_Proc_00502A9C(BriefingWindow BrfForm)
    {
        for (int ebp0C = 0; ebp0C < 0x08; ebp0C++)
        {
            if (AlliedVariables.s_V0x00542E58[ebp0C].m000002 <= -1)
            {
                continue;
            }

            if (AlliedVariables.s_V0x00542E58[ebp0C].m000001 > 0x07)
            {
                continue;
            }

            TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
            Graphics_TPen_SetColor(eax1, TBrfForm_GetIffColor(BrfForm, AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x00542E58[ebp0C].m000002].Iff));

            int esi = 0;
            int ebx = 0;

            switch (AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x00542E58[ebp0C].m000002].CraftId)
            {
                case CraftIdEnum._049_0_89_CalamariCruiserNew:
                case CraftIdEnum._051_0_91_Interdictor2:
                case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
                case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
                case CraftIdEnum._054_0_94_SuperStarDestroyer:
                case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
                case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                    switch (AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x00542E58[ebp0C].m000002].Rotation)
                    {
                        case IconRotationEnum.Rotate0:
                        case IconRotationEnum.Rotate180:
                        case IconRotationEnum.Mirror:
                            esi = 0x38;
                            ebx = 0x26;
                            break;

                        default:
                            esi = 0x26;
                            ebx = 0x38;
                            break;
                    }

                    break;

                default:
                    switch (AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x00542E58[ebp0C].m000002].Rotation)
                    {
                        case IconRotationEnum.Rotate0:
                        case IconRotationEnum.Rotate180:
                        case IconRotationEnum.Mirror:
                            esi = 0x2C;
                            ebx = 0x26;
                            break;

                        default:
                            esi = 0x26;
                            ebx = 0x2C;
                            break;
                    }

                    break;
            }

            esi = (esi + AlliedVariables.s_V0x00542E58[ebp0C].m000001 - 0x06) / 2;
            ebx = (ebx + AlliedVariables.s_V0x00542E58[ebp0C].m000001 - 0x06) / 2;
            int edi = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x00542E58[ebp0C].m000002].X) + AlliedVariables.s_V0x005424A8 + AlliedVariables.s_V0x005424A0;
            int ebp08 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x00542E58[ebp0C].m000002].Y) + AlliedVariables.s_V0x005424AC + AlliedVariables.s_V0x005424A4;

            TPoint[] ebp38 = new TPoint[5];
            ebp38[0] = new(edi - ebx, ebp08 - esi);
            ebp38[1] = new(edi + ebx, ebp08 - esi);
            ebp38[2] = new(edi + ebx, ebp08 + esi);
            ebp38[3] = new(edi - ebx, ebp08 + esi);
            ebp38[4] = new(edi - ebx, ebp08 - esi);
            Graphics_TCanvas_Polygon_L00426B54(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!, ebp38);
        }
    }

    // L00502CC0
    private static void TBrfForm_Proc_00502CC0(BriefingWindow BrfForm)
    {
        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
        Graphics_TFont_SetName(eax1, "Verdana");
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
        Graphics_TFont_SetSize(eax1, 0x09);

        for (int ebp08 = 0; ebp08 < 0x08; ebp08++)
        {
            if (AlliedVariables.s_V0x00542E78[ebp08] <= -1)
            {
                continue;
            }

            TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp08]);
            eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
            Graphics_TFont_SetColor(eax1, TBrfForm_GetColorIndexColor(BrfForm, (byte)eax2.m000004.ColorIndex));
            eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp08]);
            string ebp18 = AlliedVariables.s_V0x0054247C.GetText(eax2.m000004.Parameter);
            string ebp0C = ebp18[..AlliedVariables.s_V0x00542E98[ebp08]];

            TCommandObject eax3 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp08]);
            TCommandObject eax4 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp08]);
            Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!, TBrfForm_Proc_004FB220(BrfForm, eax4.m000004.X) + AlliedVariables.s_V0x005424A8 + AlliedVariables.s_V0x005424A0, TBrfForm_Proc_004FB220(BrfForm, eax3.m000004.Y) + AlliedVariables.s_V0x005424AC + AlliedVariables.s_V0x005424A4, ebp0C);

            // System_DoneExcept();
        }

        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
        Graphics_TFont_SetName(eax1, "Verdana");
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
        Graphics_TFont_SetSize(eax1, 0x09);
    }

    // L00503D98
    private static void TBrfForm_Proc_00503D98(BriefingWindow BrfForm)
    {
    }

    // L0050440C
    private static void TBrfForm_PaintBox1Paint(BriefingWindow BrfForm)
    {
        TBrfForm_Proc_00502624(BrfForm);
    }

    // L004FD8C0
    private static string TBrfForm_Proc_004FD8C0(BriefingWindow BrfForm, string edx0, int ecx0)
    {
        if (ecx0 < edx0.Length)
        {
            return edx0[..ecx0] + "....";
        }
        else
        {
            return edx0;
        }
    }

    // L00501570
    private static void TBrfForm_Proc_00501570(BriefingWindow BrfForm, TCommand edx0)
    {
        TCommand ebp4A8 = edx0.Clone();

        TRect ebp4B8 = BrfForm.GetClientRect();
        TRect ebp4C8 = BrfForm.GetClientRect();
        //Graphics_TCanvas_CopyRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!, ebp4C8, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp4B8);
        AlliedVariables.s_V0x00542EB8?.CopyDirectBitmap(AlliedVariables.s_V0x00542EBC!);

        switch (ebp4A8.BriefingCommand)
        {
            case BriefingCommandEnum.PageBreak:
                if (BrfForm.StopAtStop.IsChecked == true)
                {
                    TBrfForm_Proc_00503298(BrfForm);
                }

                break;

            case BriefingCommandEnum.BriefingText:
                if (AlliedVariables.s_V0x00542494 != 0)
                {
                    AlliedVariables.s_V0x005424C4 += 1;
                }

                AlliedVariables.s_V0x005424F8 = ebp4A8.Parameter;
                BrfForm.Memo1.Clear();
                BrfForm.Memo1.AddLine(AlliedVariables.s_V0x00542480.GetText(ebp4A8.Parameter));
                break;

            case BriefingCommandEnum.MoveMap:
                AlliedVariables.s_V0x0054250C = 0x01;
                AlliedVariables.s_V0x005424BC = Integer_Negate_L0051C034(ebp4A8.X);
                AlliedVariables.s_V0x005424C0 = Integer_Negate_L0051C034(ebp4A8.Y);
                AlliedVariables.s_V0x005424B4 = Integer_Negate_L0051C034(TBrfForm_Proc_004FB220(BrfForm, ebp4A8.X));
                AlliedVariables.s_V0x005424B8 = Integer_Negate_L0051C034(TBrfForm_Proc_004FB220(BrfForm, ebp4A8.Y));

                if (AlliedVariables.s_V0x005424B4 > AlliedVariables.s_V0x005424A0)
                {
                    AlliedVariables.s_V0x00542508 = 0x01;
                }

                if (AlliedVariables.s_V0x005424B4 < AlliedVariables.s_V0x005424A0)
                {
                    AlliedVariables.s_V0x00542509 = 0x01;
                }

                if (AlliedVariables.s_V0x005424B8 > AlliedVariables.s_V0x005424A4)
                {
                    AlliedVariables.s_V0x0054250A = 0x01;
                }

                if (AlliedVariables.s_V0x005424B8 < AlliedVariables.s_V0x005424A4)
                {
                    AlliedVariables.s_V0x0054250B = 0x01;
                }

                if (ebp4A8.Time == 0)
                {
                    AlliedVariables.s_V0x005424A0 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424BC);
                    AlliedVariables.s_V0x005424A4 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424C0);
                    AlliedVariables.s_V0x00542508 = 0;
                    AlliedVariables.s_V0x00542509 = 0;
                    AlliedVariables.s_V0x0054250A = 0;
                    AlliedVariables.s_V0x0054250B = 0;
                }

                break;

            case BriefingCommandEnum.ScaleMap:
                AlliedVariables.s_V0x0054250C = 0x01;

                if (ebp4A8.Time != 0)
                {
                    AlliedVariables.s_V0x005424B0 = ebp4A8.X;

                    if (AlliedVariables.s_V0x005424B0 > AlliedVariables.s_V0x0054249C)
                    {
                        AlliedVariables.s_V0x00542506 = 0x01;
                    }

                    if (AlliedVariables.s_V0x005424B0 < AlliedVariables.s_V0x0054249C)
                    {
                        AlliedVariables.s_V0x00542507 = 0x01;
                    }
                }
                else
                {
                    AlliedVariables.s_V0x0054249C = ebp4A8.X;
                    AlliedVariables.s_V0x005424B0 = AlliedVariables.s_V0x0054249C;
                    AlliedVariables.s_V0x00542506 = 0;
                    AlliedVariables.s_V0x00542507 = 0;
                }

                break;

            case BriefingCommandEnum.ClearFlightGroupTags:
                TBrfForm_Proc_005029DC(BrfForm);
                break;

            case BriefingCommandEnum.FlightGroupTags1:
            case BriefingCommandEnum.FlightGroupTags2:
            case BriefingCommandEnum.FlightGroupTags3:
            case BriefingCommandEnum.FlightGroupTags4:
            case BriefingCommandEnum.FlightGroupTags5:
            case BriefingCommandEnum.FlightGroupTags6:
            case BriefingCommandEnum.FlightGroupTags7:
            case BriefingCommandEnum.FlightGroupTags8:
                AlliedVariables.s_V0x00542E58[(int)ebp4A8.BriefingCommand - 0x09].m000002 = ebp4A8.Parameter;
                AlliedVariables.s_V0x00542E58[(int)ebp4A8.BriefingCommand - 0x09].m000001 = 0x07;

                if (AlliedVariables.s_V0x00542ED0 != 0)
                {
                    TBrfForm_Proc_00502A9C(BrfForm);
                }

                break;

            case BriefingCommandEnum.ClearTextTags:
                TBrfForm_Proc_005029F8(BrfForm);
                break;

            case BriefingCommandEnum.TextTag1:
            case BriefingCommandEnum.TextTag2:
            case BriefingCommandEnum.TextTag3:
            case BriefingCommandEnum.TextTag4:
            case BriefingCommandEnum.TextTag5:
            case BriefingCommandEnum.TextTag6:
            case BriefingCommandEnum.TextTag7:
            case BriefingCommandEnum.TextTag8:
                AlliedVariables.s_V0x00542E78[(int)ebp4A8.BriefingCommand - 0x12] = AlliedVariables.s_V0x00542498;
                AlliedVariables.s_V0x00542E98[(int)ebp4A8.BriefingCommand - 0x12] = 0x01;

                if (AlliedVariables.s_V0x00542ED0 != 0)
                {
                    TBrfForm_Proc_00502CC0(BrfForm);
                }

                break;

            case BriefingCommandEnum.NewIcon:
                if (ebp4A8.Parameter > 0x32)
                {
                    break;
                }

                if (ebp4A8.Parameter == -1)
                {
                    ebp4A8.Parameter = 0;
                }

                AlliedVariables.s_V0x0054250C = 0x01;

                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].CraftId = ebp4A8.CraftId;
                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].Iff = (byte)ebp4A8.ColorIndex;
                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].X = 0;
                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].Y = 0;

                if (ebp4A8.CraftId > CraftIdEnum._000__1_0)
                {
                    AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].m000006 = 0x01;

                    TBrfForm_Proc_0050548C(
                        BrfForm,
                        ebp4A8.Parameter,
                        ebp4A8.CraftId,
                        AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].Rotation,
                        ebp4A8.ColorIndex
                        );
                }
                else
                {
                    AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].m000006 = 0;
                }

                AlliedVariables.s_V0x00542500 += 1;
                Controls_TControl_SetText(BrfForm.Label22, AlliedVariables.s_V0x00542500.ToString(CultureInfo.InvariantCulture));
                break;

            case BriefingCommandEnum.ShowShipData:
                if (ebp4A8.IconIndex > 0x32)
                {
                    break;
                }

                if (ebp4A8.Parameter == 0)
                {
                    AlliedVariables.s_V0x00542ED3 = 0;
                    AlliedVariables.s_V0x0054250C = 0x01;
                }
                else
                {
                    AlliedVariables.s_V0x00542ED3 = 0x01;
                    AlliedVariables.s_V0x00542ED4 = AlliedVariables.s_V0x00542EDC[ebp4A8.IconIndex].CraftId;
                }

                TBrfForm_Proc_00502624(BrfForm);
                break;

            case BriefingCommandEnum.MoveIcon:
                if (ebp4A8.Parameter > 0x32)
                {
                    break;
                }

                if (ebp4A8.Parameter == -1)
                {
                    ebp4A8.Parameter = 0;
                }

                AlliedVariables.s_V0x0054250C = 0x01;

                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].X = ebp4A8.X;
                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].Y = ebp4A8.Y;
                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].m000008 = AlliedVariables.s_V0x00542498;

                if (AlliedVariables.s_V0x00542498 != 0)
                {
                    AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].m00000C = AlliedVariables.s_V0x00542498;
                }

                break;

            case BriefingCommandEnum.RotateIcon:
                if (ebp4A8.Parameter > 0x32)
                {
                    break;
                }

                if (ebp4A8.Parameter == -1)
                {
                    ebp4A8.Parameter = 0;
                }

                TBrfForm_Proc_0050548C(
                    BrfForm,
                    ebp4A8.Parameter,
                    AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].CraftId,
                    ebp4A8.Rotation,
                    AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].Iff
                    );

                AlliedVariables.s_V0x0054250C = 0x01;
                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].Rotation = ebp4A8.Rotation;
                AlliedVariables.s_V0x00542EDC[ebp4A8.Parameter].m000006 = 0x01;
                break;

            case BriefingCommandEnum.ChangeRegion:
                AlliedVariables.s_V0x0054250E = BrfForm.Timer1.GetM000048();
                AlliedVariables.s_V0x0054250C = 0x01;
                TBrfForm_Proc_005029F8(BrfForm);
                TBrfForm_Proc_005029DC(BrfForm);

                if (AlliedVariables.s_V0x00542494 != 0)
                {
                    TBrfForm_Proc_00502A20(BrfForm);
                }

                TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, false);
                AlliedVariables.s_V0x0054340C = System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Regions[ebp4A8.Parameter].Name, 0x84);
                TBrfForm_Proc_005027D0(BrfForm, AlliedVariables.s_V0x0054340C);

                if (AlliedVariables.s_V0x0054250E)
                {
                    BrfForm.Timer2.SetM00000C(0);
                    TLMDHiTimer__PROC_004B08E4(BrfForm.Timer2!, true);
                    AlliedVariables.s_V0x00543400 = (int)BrfForm.PaintBox1.Height;
                }

                break;

            case BriefingCommandEnum.BriefingEnd:
                TBrfForm_Proc_00502248(BrfForm);
                break;
        }

        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        eax1.m000004.m000014 = AlliedVariables.s_V0x005424A0;
        eax1.m000004.m000018 = AlliedVariables.s_V0x005424A4;
        eax1.m000004.m00001C = AlliedVariables.s_V0x005424BC;
        eax1.m000004.m000020 = AlliedVariables.s_V0x005424C0;
        eax1.m000004.m000024 = AlliedVariables.s_V0x0054249C;
        eax1.m000004.m000028 = AlliedVariables.s_V0x005424B4;
        eax1.m000004.m00002C = AlliedVariables.s_V0x005424B8;
        eax1.m000004.m000030 = AlliedVariables.s_V0x005424B0;
        eax1.m000004.m000034 = AlliedVariables.s_V0x00542506;
        eax1.m000004.m000035 = AlliedVariables.s_V0x00542507;
        eax1.m000004.m000036 = AlliedVariables.s_V0x00542508;
        eax1.m000004.m000037 = AlliedVariables.s_V0x0054250A;
        eax1.m000004.m000038 = AlliedVariables.s_V0x00542509;
        eax1.m000004.m000039 = AlliedVariables.s_V0x0054250B;
        for (int i = 0; i < eax1.m000004.m000438.Length; i++)
        {
            eax1.m000004.m000438[i] = AlliedVariables.s_V0x00542E58[i].Clone();
        }
        eax1.m000004.m000458 = (int[])AlliedVariables.s_V0x00542E78.Clone();
        eax1.m000004.m000498 = AlliedVariables.s_V0x005424F8;
        eax1.m000004.m00049C = AlliedVariables.s_V0x00542ED3;
        eax1.m000004.m0004A0 = AlliedVariables.s_V0x00542ED4;
        for (int i = 0; i < eax1.m000004.m00003C.Length; i++)
        {
            eax1.m000004.m00003C[i] = AlliedVariables.s_V0x00542EDC[i].Clone();
        }

        Buttons_TSpeedButton_SetDown(BrfForm.Play, BrfForm.Timer1!.GetM000048());

        // TBrfForm_Proc_00503298( BrfForm );
    }

    // L0050548C
    private static void TBrfForm_Proc_0050548C(BriefingWindow BrfForm, int edx0, CraftIdEnum ecx0, IconRotationEnum A4, int A8)
    {
        if (edx0 > 0x32 || AlliedVariables.s_V0x00542EDC[edx0].CraftId == CraftIdEnum._000__1_0)
        {
            return;
        }

        TRect ebp1C;
        TRect ebp2C;

        int bitmapWidth;
        int bitmapHeight;

        switch (AlliedVariables.s_V0x00542EDC[edx0].CraftId)
        {
            case CraftIdEnum._049_0_89_CalamariCruiserNew:
            case CraftIdEnum._051_0_91_Interdictor2:
            case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
            case CraftIdEnum._054_0_94_SuperStarDestroyer:
            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                switch (A4)
                {
                    case IconRotationEnum.Rotate0:
                    case IconRotationEnum.Rotate180:
                    case IconRotationEnum.Mirror:
                        bitmapWidth = 0x26;
                        bitmapHeight = 0x38;
                        break;

                    default:
                        if (AlliedVariables.s_AlliedMapBitmaps[(int)AlliedVariables.s_V0x00542EDC[edx0].CraftId - 1].IsLoaded == 0)
                        {
                            TBrfForm_Proc_005059B8(BrfForm, AlliedVariables.s_V0x00542EDC[edx0].CraftId);
                        }

                        bitmapWidth = 0x38;
                        bitmapHeight = 0x26;
                        break;
                }

                break;

            default:
                switch (A4)
                {
                    case IconRotationEnum.Rotate0:
                    case IconRotationEnum.Rotate180:
                    case IconRotationEnum.Mirror:
                        bitmapWidth = 0x26;
                        bitmapHeight = 0x2C;
                        break;

                    default:
                        if (AlliedVariables.s_AlliedMapBitmaps[(int)AlliedVariables.s_V0x00542EDC[edx0].CraftId - 1].IsLoaded == 0)
                        {
                            TBrfForm_Proc_005059B8(BrfForm, AlliedVariables.s_V0x00542EDC[edx0].CraftId);
                        }

                        bitmapWidth = 0x2C;
                        bitmapHeight = 0x26;
                        break;
                }

                break;
        }

        if (AlliedVariables.s_V0x005432D8[edx0] is null
            || AlliedVariables.s_V0x005432D8[edx0]!.Width != bitmapWidth
            || AlliedVariables.s_V0x005432D8[edx0]!.Height != bitmapHeight)
        {
            AlliedVariables.s_V0x005432D8[edx0] = new(bitmapWidth, bitmapHeight);
        }

        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!;
        Graphics_TBrush_SetColor(eax1, TBrfForm_GetIffColor(BrfForm, AlliedVariables.s_V0x00542EDC[edx0].Iff));
        ebp1C = BrfForm.GetClientRect();
        Graphics_TCanvas_FillRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, ebp1C);
        eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!;
        eax1.Color = 0x008800C6;
        TPoint ebp0C = TBrfForm_Proc_00505B40(BrfForm, AlliedVariables.s_V0x00542EDC[edx0].CraftId);
        int ebp04 = ebp0C.X * 0x26;
        int esi = ebp0C.Y * 0x2C + 0x03;

        switch (AlliedVariables.s_V0x00542EDC[edx0].CraftId)
        {
            case CraftIdEnum._049_0_89_CalamariCruiserNew:
            case CraftIdEnum._051_0_91_Interdictor2:
            case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
            case CraftIdEnum._054_0_94_SuperStarDestroyer:
            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                switch (A4)
                {
                    case IconRotationEnum.Rotate0:
                        ebp1C = new(ebp04, esi + 0x0E, ebp04 + 0x26, esi + 0x46);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, AlliedVariables.s_V0x005433BC, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4)!, ebp1C);
                        break;

                    case IconRotationEnum.Rotate270:
                        ebp1C = new(0x37, 0x25, -1, -1);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, ebp1C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_AlliedMapBitmaps[(int)AlliedVariables.s_V0x00542EDC[edx0].CraftId - 1].pBitmap)!, AlliedVariables.s_V0x005433DC);

                        break;

                    case IconRotationEnum.Rotate180:
                        ebp1C = new(ebp04, esi + 0x0E, ebp04 + 0x26, esi + 0x46);
                        ebp2C = new(0, 0x37, 0x26, -1);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, ebp2C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4)!, ebp1C);
                        break;

                    case IconRotationEnum.Rotate90:
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, AlliedVariables.s_V0x005433DC, Graphics_TBitmap_GetCanvas(AlliedVariables.s_AlliedMapBitmaps[(int)AlliedVariables.s_V0x00542EDC[edx0].CraftId - 1].pBitmap)!, AlliedVariables.s_V0x005433DC);
                        break;

                    case IconRotationEnum.Mirror:
                        ebp1C = new(ebp04, esi + 0x0E, ebp04 + 0x26, esi + 0x46);
                        ebp2C = new(0x25, 0, -1, 0x38);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, ebp2C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4)!, ebp1C);
                        break;
                }

                break;

            default:
                switch (A4)
                {
                    case IconRotationEnum.Rotate0:
                        ebp1C = new(ebp04, esi, ebp04 + 0x26, esi + 0x2C);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, AlliedVariables.s_V0x005433AC, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4), ebp1C);
                        break;

                    case IconRotationEnum.Rotate270:
                        ebp1C = new(0x2B, 0x25, -1, -1);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, ebp1C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_AlliedMapBitmaps[(int)AlliedVariables.s_V0x00542EDC[edx0].CraftId - 1].pBitmap)!, AlliedVariables.s_V0x005433CC);
                        break;

                    case IconRotationEnum.Rotate180:
                        ebp1C = new(ebp04, esi, ebp04 + 0x26, esi + 0x2C);
                        ebp2C = new(0, 0x2B, 0x26, -1);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, ebp2C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4)!, ebp1C);
                        break;

                    case IconRotationEnum.Rotate90:
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, AlliedVariables.s_V0x005433CC, Graphics_TBitmap_GetCanvas(AlliedVariables.s_AlliedMapBitmaps[(int)AlliedVariables.s_V0x00542EDC[edx0].CraftId - 1].pBitmap)!, AlliedVariables.s_V0x005433CC);
                        break;

                    case IconRotationEnum.Mirror:
                        ebp1C = new(ebp04, esi, ebp04 + 0x26, esi + 0x2C);
                        ebp2C = new(0x25, 0, -1, 0x2C);
                        Graphics_TCanvas_CopyRect_Solid(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005432D8[edx0])!, ebp2C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4)!, ebp1C);
                        break;
                }

                break;
        }
    }

    // L00505B40
    private static TPoint TBrfForm_Proc_00505B40(BriefingWindow BrfForm, CraftIdEnum edx0)
    {
        int esp00 = 0;
        int esp04 = 0;

        switch (edx0)
        {
            case CraftIdEnum._000__1_0:
            case CraftIdEnum._001_0_0_Xwing:
            case CraftIdEnum._002_0_1_Ywing:
            case CraftIdEnum._003_0_2_Awing:
            case CraftIdEnum._004_0_3_Bwing:
            case CraftIdEnum._005_0_4_TieFighter:
            case CraftIdEnum._006_0_5_TieInterceptor:
            case CraftIdEnum._007_0_6_TieBomber:
            case CraftIdEnum._008_0_7_TieAdvanced:
            case CraftIdEnum._009_0_8_TieDefender:
            case CraftIdEnum._040_0_80_Corvette2:
            case CraftIdEnum._114_0_17_TieBizarro:
            case CraftIdEnum._115_0_18_TieBigGun:
            case CraftIdEnum._116_0_19_TieWarheads:
            case CraftIdEnum._117_0_20_TieBomb:
            case CraftIdEnum._118_0_21_TieBooster:
            case CraftIdEnum._132_0_115_DerilynPlatform:
                esp04 = 0;
                break;

            case CraftIdEnum._010_0_9_IrdFighter:
            case CraftIdEnum._030_0_34_HeavyLifter:
            case CraftIdEnum._050_0_90_LightCalamariCruiser:
            case CraftIdEnum._078_1_6_GunPlatform:
            case CraftIdEnum._093_0_96_LancerFrigate:
            case CraftIdEnum._097_0_48_ImpLandingCraft:
            case CraftIdEnum._101_0_101_ImpResearchShip:
            case CraftIdEnum._130_0_113_GolanTwo:
            case CraftIdEnum._131_0_114_GolanThree:
            case CraftIdEnum._144_0_127_ImpResearchCenter:
            case CraftIdEnum._148_0_131_IndustrialComplex:
            case CraftIdEnum._156_1_43_GunPad:
            case CraftIdEnum._157_1_44_GunWarheadPad:
            case CraftIdEnum._161_1_42_HomingMineB:
            case CraftIdEnum._162_1_40_LaserBat:
            case CraftIdEnum._163_1_39_IonBat:
                esp04 = 0x02;
                break;

            case CraftIdEnum._011_0_10_ToscanFighter:
            case CraftIdEnum._013_0_12_Twing:
            case CraftIdEnum._019_0_42_SystemPatrolCraft:
            case CraftIdEnum._021_0_39_StormtrooperTransport:
            case CraftIdEnum._024_0_32_Tug:
            case CraftIdEnum._036_0_72_TunaBoat2:
            case CraftIdEnum._046_0_86_StrikeCruiser:
            case CraftIdEnum._100_0_73_StarGalleon:
            case CraftIdEnum._112_0_142_Suprosa:
            case CraftIdEnum._122_0_25_SupaFighter:
            case CraftIdEnum._136_0_119_SpaceColony2:
            case CraftIdEnum._137_0_120_SpaceColony3:
            case CraftIdEnum._182_1_57_R2D2:
                esp04 = 0x0A;
                break;

            case CraftIdEnum._012_0_11_MissileBoat:
            case CraftIdEnum._031_0_35_MoleMiner:
            case CraftIdEnum._034_0_70_ModularConveyor:
            case CraftIdEnum._037_0_44_MuurianTransport:
            case CraftIdEnum._038_0_45_CorellianTransport2:
            case CraftIdEnum._039_0_46_MilleniumFalcon2:
            case CraftIdEnum._041_0_81_ModCorvette:
            case CraftIdEnum._043_0_83_ModFrigate:
            case CraftIdEnum._075_1_3_MineA:
            case CraftIdEnum._076_1_4_MineB:
            case CraftIdEnum._077_1_5_MineC:
            case CraftIdEnum._092_0_95_ModStrikeCruiser:
            case CraftIdEnum._102_0_50_LuxuryYacht:
            case CraftIdEnum._104_0_74_ModActionTransport:
            case CraftIdEnum._105_0_75_MobquetTransport:
            case CraftIdEnum._180_1_56_Marko:
                esp04 = 0x03;
                break;

            case CraftIdEnum._014_0_13_Z_95:
            case CraftIdEnum._178_1_54_ZeroGStormtrooper:
            case CraftIdEnum._179_1_55_ZeroGUtility:
                esp04 = 0x0B;
                break;

            case CraftIdEnum._015_0_14_R41:
            case CraftIdEnum._049_0_89_CalamariCruiserNew:
            case CraftIdEnum._051_0_91_Interdictor2:
            case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
            case CraftIdEnum._054_0_94_SuperStarDestroyer:
            case CraftIdEnum._070_1_0_SatB:
            case CraftIdEnum._081_1_50_ProbeCapsule:
            case CraftIdEnum._091_0_111_RepairYard:
            case CraftIdEnum._120_0_23_RazorFighter:
            case CraftIdEnum._142_0_125_ProcessingPlant:
            case CraftIdEnum._143_0_126_RebelPlatform:
            case CraftIdEnum._151_0_63_PropaneTank:
            case CraftIdEnum._158_1_45_ProximityMineA:
            case CraftIdEnum._159_1_46_ProximityMineB:
            case CraftIdEnum._181_1_57_R2D2:
            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                esp04 = 0x08;
                break;

            case CraftIdEnum._016_0_15_AssaultGunboat:
            case CraftIdEnum._022_0_40_AssaultTransport:
            case CraftIdEnum._066_0_108_AsteroidBase:
            case CraftIdEnum._067_0_133_AsteroidLaserBattery:
            case CraftIdEnum._095_0_98_AssaultFrigate:
            case CraftIdEnum._096_0_99_CorellianGunship:
            case CraftIdEnum._098_0_49_AssaultShuttle:
            case CraftIdEnum._099_0_100_MarauderCorvette:
            case CraftIdEnum._106_0_76_XiytiarTransport:
            case CraftIdEnum._119_0_22_CloakshapeFighter:
            case CraftIdEnum._140_0_123_CargoFacility2:
            case CraftIdEnum._145_0_128_FamilyBase:
                esp04 = 0x05;
                break;

            case CraftIdEnum._017_0_37_Shuttle:
            case CraftIdEnum._020_0_43_ScoutCraft:
            case CraftIdEnum._071_1_1_SatC:
            case CraftIdEnum._072_1_2_SatD:
            case CraftIdEnum._090_0_110_ShipYard:
            case CraftIdEnum._113_0_16_SkiprayBlastBoat:
            case CraftIdEnum._127_0_30_SlaveOne:
            case CraftIdEnum._128_0_31_SlaveTwo:
            case CraftIdEnum._133_0_116_SensorArray:
            case CraftIdEnum._135_0_118_SpaceColony1:
                esp04 = 0x09;
                break;

            case CraftIdEnum._018_0_38_EscortShuttle:
            case CraftIdEnum._023_0_41_EscortTransport:
            case CraftIdEnum._032_0_68_BulkFreighter:
            case CraftIdEnum._042_0_82_Frigate2:
            case CraftIdEnum._047_0_87_EscortCarrier:
            case CraftIdEnum._048_0_88_Dreadnaught2:
            case CraftIdEnum._069_0_109_Factory:
            case CraftIdEnum._103_0_51_FerryboatLiner:
            case CraftIdEnum._107_0_77_FreighterConB:
            case CraftIdEnum._108_0_78_FreighterConG:
            case CraftIdEnum._109_0_79_FreighterBox:
            case CraftIdEnum._129_0_112_GolanOne:
            case CraftIdEnum._146_0_129_FamilyRepairYard:
            case CraftIdEnum._150_0_36_EscapePod:
            case CraftIdEnum._174_0_153_EscapePodA:
                esp04 = 0x01;
                break;

            case CraftIdEnum._025_0_33_CombatUtilityVehicle:
            case CraftIdEnum._033_0_69_CargoFerry:
            case CraftIdEnum._045_0_85_CarrackCruiser:
            case CraftIdEnum._068_0_134_AsteroidWarheadLauncher:
            case CraftIdEnum._083_1_47_BuoyC:
            case CraftIdEnum._084_1_48_BuoyB:
            case CraftIdEnum._088_1_49_BuoyRendez:
            case CraftIdEnum._089_0_62_CargoCanister:
            case CraftIdEnum._094_0_97_BulkCruiser:
            case CraftIdEnum._134_0_117_CommRelay:
            case CraftIdEnum._138_0_121_Casino:
            case CraftIdEnum._139_0_122_CargoFacility1:
            case CraftIdEnum._141_0_124_AsteroidMiningUnit:
            case CraftIdEnum._153_0_65_ContainerBox:
            case CraftIdEnum._228_0_135_CalamariWinged:
                esp04 = 0x06;
                break;

            case CraftIdEnum._026_0_53_ContainerBrick:
            case CraftIdEnum._027_0_54_ContainerHexBox:
            case CraftIdEnum._028_0_55_ContainerTube:
            case CraftIdEnum._029_0_56_ContainerPronged:
            case CraftIdEnum._035_0_71_ContainerTransport:
            case CraftIdEnum._055_0_57_ContainerHemisphere:
            case CraftIdEnum._056_0_58_ContainerSlotted:
            case CraftIdEnum._057_0_59_ContainerHourglass:
            case CraftIdEnum._058_0_60_ContainerGem:
            case CraftIdEnum._059_0_61_ContainerYshaped:
            case CraftIdEnum._080_1_7_Probe:
            case CraftIdEnum._152_0_64_ContainerGrande:
            case CraftIdEnum._154_0_66_ContainerSphere:
            case CraftIdEnum._155_0_67_ContainerHanger:
                esp04 = 0x07;
                break;

            case CraftIdEnum._044_0_84_PassengerLiner:
            case CraftIdEnum._060_0_102_Platform1:
            case CraftIdEnum._061_0_103_Platform2:
            case CraftIdEnum._062_0_104_Platform3:
            case CraftIdEnum._063_0_105_Platform4:
            case CraftIdEnum._064_0_106_Platform5:
            case CraftIdEnum._065_0_107_Platform6:
            case CraftIdEnum._085_1_8_BuoyFaux:
            case CraftIdEnum._110_0_52_FamilyTransport:
            case CraftIdEnum._111_0_47_Outrider:
            case CraftIdEnum._121_0_24_PlanetaryFighter:
            case CraftIdEnum._123_0_26_Piggyback:
            case CraftIdEnum._125_0_28_PreybirdFighter:
            case CraftIdEnum._147_0_130_PirateShipyard:
            case CraftIdEnum._175_1_51_RebelPilot:
            case CraftIdEnum._176_1_52_ImperialPilot:
            case CraftIdEnum._177_1_53_CivilianPilot:
                esp04 = 0x04;
                break;
        }
        ;

        switch (edx0)
        {
            case CraftIdEnum._000__1_0:
            case CraftIdEnum._001_0_0_Xwing:
            case CraftIdEnum._014_0_13_Z_95:
            case CraftIdEnum._026_0_53_ContainerBrick:
            case CraftIdEnum._048_0_88_Dreadnaught2:
            case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
            case CraftIdEnum._085_1_8_BuoyFaux:
            case CraftIdEnum._102_0_50_LuxuryYacht:
            case CraftIdEnum._130_0_113_GolanTwo:
            case CraftIdEnum._136_0_119_SpaceColony2:
            case CraftIdEnum._141_0_124_AsteroidMiningUnit:
            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                esp00 = 0;
                break;

            case CraftIdEnum._002_0_1_Ywing:
            case CraftIdEnum._028_0_55_ContainerTube:
            case CraftIdEnum._051_0_91_Interdictor2:
            case CraftIdEnum._068_0_134_AsteroidWarheadLauncher:
            case CraftIdEnum._131_0_114_GolanThree:
            case CraftIdEnum._137_0_120_SpaceColony3:
            case CraftIdEnum._150_0_36_EscapePod:
            case CraftIdEnum._178_1_54_ZeroGStormtrooper:
                esp00 = 0x01;
                break;

            case CraftIdEnum._003_0_2_Awing:
            case CraftIdEnum._054_0_94_SuperStarDestroyer:
            case CraftIdEnum._058_0_60_ContainerGem:
            case CraftIdEnum._100_0_73_StarGalleon:
            case CraftIdEnum._111_0_47_Outrider:
            case CraftIdEnum._156_1_43_GunPad:
            case CraftIdEnum._157_1_44_GunWarheadPad:
            case CraftIdEnum._174_0_153_EscapePodA:
            case CraftIdEnum._179_1_55_ZeroGUtility:
            case CraftIdEnum._180_1_56_Marko:
                esp00 = 0x02;
                break;

            case CraftIdEnum._004_0_3_Bwing:
            case CraftIdEnum._021_0_39_StormtrooperTransport:
            case CraftIdEnum._038_0_45_CorellianTransport2:
            case CraftIdEnum._039_0_46_MilleniumFalcon2:
            case CraftIdEnum._044_0_84_PassengerLiner:
            case CraftIdEnum._047_0_87_EscortCarrier:
            case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._078_1_6_GunPlatform:
            case CraftIdEnum._094_0_97_BulkCruiser:
            case CraftIdEnum._106_0_76_XiytiarTransport:
            case CraftIdEnum._152_0_64_ContainerGrande:
            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
                esp00 = 0x03;
                break;

            case CraftIdEnum._005_0_4_TieFighter:
            case CraftIdEnum._015_0_14_R41:
            case CraftIdEnum._016_0_15_AssaultGunboat:
            case CraftIdEnum._017_0_37_Shuttle:
            case CraftIdEnum._036_0_72_TunaBoat2:
            case CraftIdEnum._043_0_83_ModFrigate:
            case CraftIdEnum._045_0_85_CarrackCruiser:
            case CraftIdEnum._063_0_105_Platform4:
            case CraftIdEnum._107_0_77_FreighterConB:
            case CraftIdEnum._154_0_66_ContainerSphere:
            case CraftIdEnum._163_1_39_IonBat:
                esp00 = 0x0B;
                break;

            case CraftIdEnum._006_0_5_TieInterceptor:
            case CraftIdEnum._010_0_9_IrdFighter:
            case CraftIdEnum._013_0_12_Twing:
            case CraftIdEnum._032_0_68_BulkFreighter:
            case CraftIdEnum._035_0_71_ContainerTransport:
            case CraftIdEnum._064_0_106_Platform5:
            case CraftIdEnum._092_0_95_ModStrikeCruiser:
            case CraftIdEnum._098_0_49_AssaultShuttle:
            case CraftIdEnum._113_0_16_SkiprayBlastBoat:
            case CraftIdEnum._120_0_23_RazorFighter:
            case CraftIdEnum._138_0_121_Casino:
                esp00 = 0x0C;
                break;

            case CraftIdEnum._007_0_6_TieBomber:
            case CraftIdEnum._011_0_10_ToscanFighter:
            case CraftIdEnum._020_0_43_ScoutCraft:
            case CraftIdEnum._057_0_59_ContainerHourglass:
            case CraftIdEnum._060_0_102_Platform1:
            case CraftIdEnum._089_0_62_CargoCanister:
            case CraftIdEnum._105_0_75_MobquetTransport:
            case CraftIdEnum._144_0_127_ImpResearchCenter:
            case CraftIdEnum._145_0_128_FamilyBase:
            case CraftIdEnum._146_0_129_FamilyRepairYard:
            case CraftIdEnum._158_1_45_ProximityMineA:
                esp00 = 0x08;
                break;

            case CraftIdEnum._008_0_7_TieAdvanced:
            case CraftIdEnum._018_0_38_EscortShuttle:
            case CraftIdEnum._046_0_86_StrikeCruiser:
            case CraftIdEnum._049_0_89_CalamariCruiserNew:
            case CraftIdEnum._075_1_3_MineA:
            case CraftIdEnum._083_1_47_BuoyC:
            case CraftIdEnum._123_0_26_Piggyback:
                esp00 = 0x04;
                break;

            case CraftIdEnum._009_0_8_TieDefender:
            case CraftIdEnum._033_0_69_CargoFerry:
            case CraftIdEnum._041_0_81_ModCorvette:
            case CraftIdEnum._056_0_58_ContainerSlotted:
            case CraftIdEnum._062_0_104_Platform3:
            case CraftIdEnum._090_0_110_ShipYard:
            case CraftIdEnum._095_0_98_AssaultFrigate:
            case CraftIdEnum._109_0_79_FreighterBox:
            case CraftIdEnum._148_0_131_IndustrialComplex:
            case CraftIdEnum._181_1_57_R2D2:
            case CraftIdEnum._182_1_57_R2D2:
                esp00 = 0x0A;
                break;

            case CraftIdEnum._012_0_11_MissileBoat:
            case CraftIdEnum._019_0_42_SystemPatrolCraft:
            case CraftIdEnum._027_0_54_ContainerHexBox:
            case CraftIdEnum._069_0_109_Factory:
            case CraftIdEnum._097_0_48_ImpLandingCraft:
            case CraftIdEnum._117_0_20_TieBomb:
            case CraftIdEnum._121_0_24_PlanetaryFighter:
            case CraftIdEnum._140_0_123_CargoFacility2:
            case CraftIdEnum._151_0_63_PropaneTank:
            case CraftIdEnum._228_0_135_CalamariWinged:
                esp00 = 0x07;
                break;

            case CraftIdEnum._022_0_40_AssaultTransport:
            case CraftIdEnum._025_0_33_CombatUtilityVehicle:
            case CraftIdEnum._034_0_70_ModularConveyor:
            case CraftIdEnum._065_0_107_Platform6:
            case CraftIdEnum._093_0_96_LancerFrigate:
            case CraftIdEnum._108_0_78_FreighterConG:
            case CraftIdEnum._116_0_19_TieWarheads:
            case CraftIdEnum._127_0_30_SlaveOne:
            case CraftIdEnum._143_0_126_RebelPlatform:
                esp00 = 0x0D;
                break;

            case CraftIdEnum._023_0_41_EscortTransport:
            case CraftIdEnum._030_0_34_HeavyLifter:
            case CraftIdEnum._071_1_1_SatC:
            case CraftIdEnum._076_1_4_MineB:
            case CraftIdEnum._081_1_50_ProbeCapsule:
            case CraftIdEnum._084_1_48_BuoyB:
            case CraftIdEnum._096_0_99_CorellianGunship:
            case CraftIdEnum._115_0_18_TieBigGun:
            case CraftIdEnum._122_0_25_SupaFighter:
            case CraftIdEnum._155_0_67_ContainerHanger:
            case CraftIdEnum._175_1_51_RebelPilot:
            case CraftIdEnum._176_1_52_ImperialPilot:
            case CraftIdEnum._177_1_53_CivilianPilot:
                esp00 = 0x05;
                break;

            case CraftIdEnum._024_0_32_Tug:
            case CraftIdEnum._029_0_56_ContainerPronged:
            case CraftIdEnum._061_0_103_Platform2:
            case CraftIdEnum._101_0_101_ImpResearchShip:
            case CraftIdEnum._103_0_51_FerryboatLiner:
            case CraftIdEnum._104_0_74_ModActionTransport:
            case CraftIdEnum._118_0_21_TieBooster:
            case CraftIdEnum._119_0_22_CloakshapeFighter:
            case CraftIdEnum._133_0_116_SensorArray:
            case CraftIdEnum._139_0_122_CargoFacility1:
            case CraftIdEnum._159_1_46_ProximityMineB:
                esp00 = 0x09;
                break;

            case CraftIdEnum._031_0_35_MoleMiner:
            case CraftIdEnum._040_0_80_Corvette2:
            case CraftIdEnum._042_0_82_Frigate2:
            case CraftIdEnum._059_0_61_ContainerYshaped:
            case CraftIdEnum._066_0_108_AsteroidBase:
            case CraftIdEnum._091_0_111_RepairYard:
            case CraftIdEnum._125_0_28_PreybirdFighter:
            case CraftIdEnum._128_0_31_SlaveTwo:
            case CraftIdEnum._134_0_117_CommRelay:
            case CraftIdEnum._162_1_40_LaserBat:
                esp00 = 0x0E;
                break;

            case CraftIdEnum._037_0_44_MuurianTransport:
            case CraftIdEnum._050_0_90_LightCalamariCruiser:
            case CraftIdEnum._067_0_133_AsteroidLaserBattery:
            case CraftIdEnum._070_1_0_SatB:
            case CraftIdEnum._080_1_7_Probe:
            case CraftIdEnum._110_0_52_FamilyTransport:
            case CraftIdEnum._129_0_112_GolanOne:
            case CraftIdEnum._132_0_115_DerilynPlatform:
            case CraftIdEnum._135_0_118_SpaceColony1:
            case CraftIdEnum._153_0_65_ContainerBox:
                esp00 = 0x0F;
                break;

            case CraftIdEnum._055_0_57_ContainerHemisphere:
            case CraftIdEnum._072_1_2_SatD:
            case CraftIdEnum._077_1_5_MineC:
            case CraftIdEnum._088_1_49_BuoyRendez:
            case CraftIdEnum._099_0_100_MarauderCorvette:
            case CraftIdEnum._112_0_142_Suprosa:
            case CraftIdEnum._114_0_17_TieBizarro:
            case CraftIdEnum._142_0_125_ProcessingPlant:
            case CraftIdEnum._147_0_130_PirateShipyard:
            case CraftIdEnum._161_1_42_HomingMineB:
                esp00 = 0x06;
                break;
        }

        return new TPoint(esp00, esp04);
    }

    // L005059B8
    private static void TBrfForm_Proc_005059B8(BriefingWindow BrfForm, CraftIdEnum edx0)
    {
        if (AlliedVariables.s_AlliedMapBitmaps[(int)edx0 - 1].IsLoaded != 0)
        {
            return;
        }

        int pBitmapHeight = 0x26;

        bool bl1 = false;

        switch (edx0)
        {
            case CraftIdEnum._049_0_89_CalamariCruiserNew:
            case CraftIdEnum._051_0_91_Interdictor2:
            case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
            case CraftIdEnum._054_0_94_SuperStarDestroyer:
            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                bl1 = true;
                break;

            default:
                bl1 = false;
                break;
        }

        int pBitmapWidth;

        if (bl1)
        {
            pBitmapWidth = 0x38;
        }
        else
        {
            pBitmapWidth = 0x2C;
        }

        AlliedVariables.s_AlliedMapBitmaps[(int)edx0 - 1].pBitmap = new(pBitmapWidth, pBitmapHeight);

        TPoint ebp0C = TBrfForm_Proc_00505B40(BrfForm, edx0);

        int ebp10 = ebp0C.X * 0x26;
        int ebp14 = ebp0C.Y * 0x2C + 0x03;
        int ebp18 = 0;

        bool al1 = false;

        switch (edx0)
        {
            case CraftIdEnum._049_0_89_CalamariCruiserNew:
            case CraftIdEnum._051_0_91_Interdictor2:
            case CraftIdEnum._052_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._053_0_93_ImperialStarDestroyer2:
            case CraftIdEnum._054_0_94_SuperStarDestroyer:
            case CraftIdEnum._229_0_92_VictoryStarDestroyer2:
            case CraftIdEnum._230_0_141_ImperialStarDestroyer2:
                al1 = true;
                break;

            default:
                al1 = false;
                break;
        }

        if (al1)
        {
            ebp14 += 0x0E;
            ebp18 = 0x38;
        }
        else
        {
            ebp18 = 0x2C;
        }

        Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4);

        TBitmap src = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x005433A4)!;
        TBitmap dst = Graphics_TBitmap_GetCanvas(AlliedVariables.s_AlliedMapBitmaps[(int)edx0 - 1].pBitmap)!;

        dst.RenderOpen();

        for (int edi = ebp10; edi < ebp10 + 0x26; edi++)
        {
            for (int ebx = ebp14; ebx < ebp14 + ebp18; ebx++)
            {
                dst.SetPixel(ebp14 + ebp18 - ebx - 1, edi - ebp10, src.GetPixel(edi, ebx));
            }
        }

        dst.RenderClose();

        AlliedVariables.s_AlliedMapBitmaps[(int)edx0 - 1].IsLoaded = 0x01;
    }

    // L005027D0
    private static void TBrfForm_Proc_005027D0(BriefingWindow BrfForm, string edx0)
    {
        System_Randomize();
        AlliedVariables.s_V0x00542EBC!.RenderOpen();
        TBitmap eax1 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
        Graphics_TBrush_SetColor(eax1, 0x0029121A);
        TRect ebp14 = BrfForm.GetClientRect();
        Graphics_TCanvas_FillRect(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp14);
        TBitmap eax2 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
        Graphics_TBrush_SetStyle(eax2, 0x01);
        TBitmap eax3 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
        Graphics_TFont_SetColor(eax3, 0x0000FFFF);
        Graphics_TCanvas_TextOut(
            Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!,
            (int)BrfForm.PaintBox1.Width / 2 - edx0.Length * 0x03,
            (int)BrfForm.PaintBox1.Height / 2 - 0x0A,
            edx0
            );

        if (BrfForm.Timer2.GetM000048() && AlliedVariables.s_V0x00543400 > 0)
        {
            TBitmap eax4 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!;
            Graphics_TPen_SetColor(eax4, 0x00FF9797);
            Graphics_TCanvas_MoveTo(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, 0, AlliedVariables.s_V0x00543400);
            Graphics_TCanvas_LineTo(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, AlliedVariables.s_V0x00542EBC!.Width, AlliedVariables.s_V0x00543400);

            for (int esi = 0; esi < 0x04; esi++)
            {
                Graphics_TCanvas_MoveTo(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, (int)BrfForm.PaintBox1.Width / 2, (int)BrfForm.PaintBox1.Height);
                Graphics_TCanvas_LineTo(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, System_RandInt((int)BrfForm.PaintBox1.Width), AlliedVariables.s_V0x00543400);
            }
        }

        AlliedVariables.s_V0x00542EBC!.RenderClose();
        ebp14 = BrfForm.GetClientRect();
        TRect ebp24 = BrfForm.GetClientRect();
        //Graphics_TCanvas_CopyRect(BrfForm.PaintBox1.Bitmap!, ebp24, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!, ebp14);
        BrfForm.PaintBox1.Bitmap?.CopyDirectBitmap(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EBC)!);
    }

    // L004FC844
    private static void TBrfForm_FormClose(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_StopClick(BrfForm, BrfForm.Stop);

        if (AlliedVariables.s_V0x00542504 != 0)
        {
            TBrfForm_Proc_004FD9C4(BrfForm);
            Unit_00513838_Proc_005146A4();
        }

        TBrfForm_Proc_00502A20(BrfForm);
        AlliedVariables.s_V0x00543CF4.Clear();
        TBrfForm__PROC_004FC9F8(BrfForm);
        AlliedVariables.s_V0x00542EB8 = null;
        AlliedVariables.s_V0x00542EBC = null;
        AlliedVariables.s_V0x005433A4 = null;
        AlliedVariables.s_V0x005433A8 = null;
        AlliedVariables.s_V0x00543938.ShowCurrDown = BrfForm.ShowCurr.IsChecked == true;
        AlliedVariables.s_V0x00543938.ShowNumsDown = BrfForm.ShowNums.IsChecked == true;
        AlliedVariables.s_V0x00543938.StopAtStopDown = BrfForm.StopAtStop.IsChecked == true;
        AlliedVariables.s_V0x00543938.FastPlaybackBtnDown = BrfForm.FastPlaybackBtn.IsChecked == true;
        AlliedVariables.s_IconSpeedOptionSetting = (short)Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin);

        for (int ebp04 = 0; ebp04 < 0x32; ebp04++)
        {
            AlliedVariables.s_V0x005432D8[ebp04] = null;
        }

        for (int ebp04 = 0; ebp04 < 0xA5; ebp04++)
        {
            if (AlliedVariables.s_AlliedMapBitmaps[ebp04].IsLoaded != 0)
            {
                AlliedVariables.s_AlliedMapBitmaps[ebp04].pBitmap = null;
            }
        }
    }

    // L00501DD0
    private static void TBrfForm_StopClick(BriefingWindow BrfForm, object? Sender)
    {
        if (BrfForm.Timer2.GetM000048())
        {
            BrfForm.Timer2.SetM00000C(0x29);
            TLMDHiTimer__PROC_004B08E4(BrfForm.Timer2!, false);
        }

        if (AlliedVariables.s_V0x00542ED3 != 0)
        {
            AlliedVariables.s_V0x00542ED3 = 0;
            AlliedVariables.s_V0x0054250C = 0x01;
        }

        AlliedVariables.s_V0x0054250E = false;
        TBrfForm_Proc_00503298(BrfForm);
        Buttons_TSpeedButton_SetDown(BrfForm.Play, false);
        Buttons_TSpeedButton_SetDown(BrfForm.Pause, false);
        TBrfForm_Proc_00502248(BrfForm);
    }

    // L00503298
    private static void TBrfForm_Proc_00503298(BriefingWindow BrfForm)
    {
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, false);
        StdCtrls_TScrollBar_SetPosition(BrfForm.XScroll, Integer_Negate_L0051C034(AlliedVariables.s_V0x005424A0));
        StdCtrls_TScrollBar_SetPosition(BrfForm.YScroll, Integer_Negate_L0051C034(AlliedVariables.s_V0x005424A4));
        AlliedVariables.s_V0x0054249C = AlliedVariables.s_V0x005424B0;
        TBrfForm_Proc_00502624(BrfForm);
    }

    // L004FD9C4
    private static void TBrfForm_Proc_004FD9C4(BriefingWindow BrfForm)
    {
        AlliedVariables.s_V0x00543B53 = true;
        //System_TObject_Create(s_TCommandObjectClass, true);

        int ebx = 0x01;
        int edi = AlliedVariables.s_V0x00543CF4.Count;

        for (int esi = 0; esi < edi; esi++)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);
            TCommand ebp4B4 = eax1.m000004.Clone();

            AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Time;
            ebx++;
            AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = (short)ebp4B4.BriefingCommand;
            ebx++;

            switch (ebp4B4.BriefingCommand)
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
                case BriefingCommandEnum.ChangeRegion:
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Parameter;
                    ebx++;
                    break;

                case BriefingCommandEnum.MoveMap:
                case BriefingCommandEnum.ScaleMap:
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.X;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Y;
                    ebx++;
                    break;

                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Parameter;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.X;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Y;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.ColorIndex;
                    ebx++;
                    break;

                case BriefingCommandEnum.NewIcon:
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Parameter;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = (short)ebp4B4.CraftId;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.ColorIndex;
                    ebx++;
                    break;

                case BriefingCommandEnum.ShowShipData:
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Parameter;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.IconIndex;
                    ebx++;
                    break;

                case BriefingCommandEnum.MoveIcon:
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Parameter;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.X;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Y;
                    ebx++;
                    break;

                case BriefingCommandEnum.RotateIcon:
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = ebp4B4.Parameter;
                    ebx++;
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[ebx - 1] = (short)ebp4B4.Rotation;
                    ebx++;
                    break;
            }
            ;
        }

        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.CodeSize = (short)(ebx - 1);

        for (int esi = ebx; esi < 0x1B59;)
        {
            AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0;
            esi++;
        }

        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Length = (short)Math.Round(Runtime_L0040AA10__StrRecToFloat(Controls_TControl_GetText(BrfForm.BrfTimeEd)) * 25.0f);
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Time = (short)Allied_StrRec_to_int(Controls_TControl_GetText(BrfForm.BrfUnk1Ed));
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Index = (short)Allied_StrRec_to_int(Controls_TControl_GetText(BrfForm.BrfUnk2Ed));
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Title = (short)Allied_StrRec_to_int(Controls_TControl_GetText(BrfForm.BrfUnk3Ed));
    }

    // L004FDE6C
    private static void TBrfForm_BrfSeenByClick(BriefingWindow BrfForm, object? Sender)
    {
        for (int esi = 0; esi < 0x08; esi++)
        {
            AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.ForTeam[esi] = StdCtrls_TCustomListBox_GetSelected(BrfForm.BrfSeenBy, esi) ? (byte)1 : (byte)0;
        }
    }

    // L004FDEA4
    private static void TBrfForm_ComDisplayClick(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x00542498 = Math.Max(BrfForm.ComDisplay.SelectedIndex, 0);
        TBrfForm__PROC_004FDECC(BrfForm, AlliedVariables.s_V0x00542498);
    }

    // L004FE5B0
    private static void TBrfForm_ColorBoxChange(BriefingWindow BrfForm, object? Sender)
    {
        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);

        switch (eax1.m000004.BriefingCommand)
        {
            case BriefingCommandEnum.ShowShipData:
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                eax1.m000004.Parameter = (short)BrfForm.ColorBox.SelectedIndex;
                break;

            case BriefingCommandEnum.RotateIcon:
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                eax1.m000004.Rotation = (IconRotationEnum)BrfForm.ColorBox.SelectedIndex;
                break;

            default:
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                eax1.m000004.ColorIndex = (short)BrfForm.ColorBox.SelectedIndex;
                break;
        }

        TBrfForm__PROC_004FCA50(BrfForm);
        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FFC68
    private static void TBrfForm__PROC_004FFC68(BriefingWindow BrfForm, int edx0)
    {
        for (int ecx = 0; ecx < 0x190; ecx++)
        {
            AlliedVariables.s_Allied_Briefing[edx0].BriefingData.BriefingCode.m00000A[ecx] = 0;
        }

        AlliedVariables.s_Allied_Briefing[edx0].BriefingData.BriefingCode.CodeSize = 0;
    }

    // L004FFCA0
    private static void TBrfForm__PROC_004FFCA0(BriefingWindow BrfForm)
    {
        AlliedVariables.s_V0x00542504 = 0x01;
    }

    // L00500F8C
    private static void TBrfForm_Proc_00500F8C(BriefingWindow BrfForm)
    {
        if (AlliedVariables.s_V0x00543CF4.Count <= 0x01)
        {
            return;
        }

        if (AlliedVariables.s_V0x00543CF4.Count - 1 <= AlliedVariables.s_V0x00542498)
        {
            return;
        }

        Classes_TList_Delete(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        Unit_00513838_Proc_005146A4();
        TBrfForm__PROC_004FFCA0(BrfForm);
        TBrfForm__PROC_004FCA50(BrfForm);
        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
        TBrfForm__PROC_004FDECC(BrfForm, AlliedVariables.s_V0x00542498);
    }

    // L00500FF8
    private static short TBrfForm_Proc_00500FF8(BriefingWindow BrfForm, ComboBox edx0)
    {
        short ebx = edx0.SelectedIndex switch
        {
            0x00 => 0x01,
            0x01 => 0x10,
            0x02 => 0x18,
            0x03 => 0x20,
            0x04 => 0x30,
            0x05 => 0x40,
            0x06 => 0x50,
            0x07 => 0x64,
            0x08 => 0xA8,
            _ => 0,
        };
        return ebx;
    }

    // L00500ED8
    private static void TBrfForm_RadioGroup1Click(BriefingWindow BrfForm, object? Sender)
    {
        switch ((TimeAdjustIncrementEnum)BrfForm.RadioGroup1.GetItemIndex())
        {
            case TimeAdjustIncrementEnum._1Div25:
                BrfForm.TimeSpin.Value = 0x01;
                break;

            case TimeAdjustIncrementEnum._1Div4:
                BrfForm.TimeSpin.Value = 0x06;
                break;

            case TimeAdjustIncrementEnum._1Div2:
                BrfForm.TimeSpin.Value = 0x0C;
                break;

            case TimeAdjustIncrementEnum._1:
                BrfForm.TimeSpin.Value = 0x19;
                break;

            case TimeAdjustIncrementEnum._5:
                BrfForm.TimeSpin.Value = 0x7D;
                break;
        }
    }

    // L005048C8
    private static void TBrfForm_FormKeyDown(BriefingWindow BrfForm, object? Sender, Key key, ModifierKeys modifiers)
    {
        if (key == Key.Delete)
        {
            if ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag) == BrfPageEnum.WYSIWYG)
            {
                if (BrfForm.Timer1.GetM000048())
                {
                    TBrfForm_Proc_00503298(BrfForm);
                    Buttons_TSpeedButton_SetDown(BrfForm.Pause, true);
                }
                else
                {
                    TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, true);
                    Buttons_TSpeedButton_SetDown(BrfForm.Pause, false);
                    Buttons_TSpeedButton_SetDown(BrfForm.Play, true);
                }
            }
        }
        else if (key == Key.C)
        {
            //if (AlliedVariables.s_TBrfForm_Instance.ActiveControl == BrfForm.ComDisplay)
            if (BrfForm.ComDisplay.IsFocused)
            {
                TBrfForm_DeleteComClick(BrfForm, BrfForm.DeleteCom);
            }
        }

        if (modifiers.HasFlag(ModifierKeys.Shift))
        {
            if (key == Key.V)
            {
                Form1WindowImpl.TForm1_SaveAsBtnClick(AlliedVariables.s_AlliedForm1Window!, BrfForm.Return);
            }
            else if (key == Key.NumPad9)
            {
                Form1WindowImpl.TForm1_SaveBtnClick(AlliedVariables.s_AlliedForm1Window!, BrfForm.Return);
            }
        }
    }

    // L005049A0
    private static void TBrfForm_PaintBox1MouseUp(BriefingWindow BrfForm, object? Sender, int A4, int A8, TShiftState AC)
    {
        AlliedVariables.s_V0x00543944 = 0;
        AlliedVariables.s_V0x0054250D = 0;
    }

    // L00503D9C
    private static void TBrfForm_PaintBox1MouseDown(BriefingWindow BrfForm, object? Sender, int A4, int A8, TShiftState AC)
    {
        TCommandObject eax1;
        TBitmap eax2;

        AlliedVariables.s_V0x005433F8 = TBrfForm__PROC_004FB244(BrfForm, A8, 0);
        AlliedVariables.s_V0x005433FC = TBrfForm__PROC_004FB244(BrfForm, A4, 0x01);

        if (AlliedVariables.s_V0x00542EC8 != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.X = (short)AlliedVariables.s_V0x005433F8;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Y = (short)AlliedVariables.s_V0x005433FC;
            TBrfForm_Proc_00502624(BrfForm);
            eax2 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
            Graphics_TBrush_SetStyle(eax2, 0x01);
            eax2 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
            Graphics_TFont_SetColor(eax2, TBrfForm_GetColorIndexColor(BrfForm, (byte)BrfForm.MapColor.SelectedIndex));
            eax2 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
            Graphics_TFont_SetName(eax2, "Verdana");
            eax2 = Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!;
            Graphics_TFont_SetSize(eax2, 0x09);
            int edx1 = BrfForm.MapIndex.SelectedIndex;
            string ebp0C = AlliedVariables.s_V0x0054247C.GetText(edx1);
            Graphics_TCanvas_TextOut(Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!, A8, A4, ebp0C);
            TRect ebp1C = BrfForm.GetClientRect();
            TRect ebp2C = BrfForm.GetClientRect();
            //Graphics_TCanvas_CopyRect(BrfForm.PaintBox1.Bitmap!, ebp2C, Graphics_TBitmap_GetCanvas(AlliedVariables.s_V0x00542EB8)!, ebp1C);
            BrfForm.PaintBox1.Bitmap?.CopyDirectBitmap(AlliedVariables.s_V0x00542EB8!);
        }
        else if (AlliedVariables.s_V0x00542ECA != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.X = (short)AlliedVariables.s_V0x005433F8;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Y = (short)AlliedVariables.s_V0x005433FC;
            TBrfForm_Proc_00502624(BrfForm);
            Graphics_TBrush_SetStyle(BrfForm.PaintBox1.Bitmap!, 0x01);
            Graphics_TFont_SetColor(BrfForm.PaintBox1, 0x00FFFFFF);
            int edx1 = BrfForm.MapIndex.SelectedIndex;
            string ebp30 = AlliedVariables.s_Strings_Ships.GetText(edx1);
            Graphics_TCanvas_TextOut(BrfForm.PaintBox1.Bitmap!, A8, A4 + 0x05, ebp30);
        }
        else
        {
            int esi1 = -1;
            byte ebp05 = 0;
            AlliedVariables.s_V0x00543944 = 0;

            while (true)
            {
                esi1++;

                int ecx1 = Math.Abs(TBrfForm__PROC_004FB244(BrfForm, A8, 0) - AlliedVariables.s_V0x00542EDC[esi1].X);

                if (ecx1 < 0x1194 / AlliedVariables.s_V0x0054249C)
                {
                    int ecx2 = Math.Abs(TBrfForm__PROC_004FB244(BrfForm, A4, 0x01) - AlliedVariables.s_V0x00542EDC[esi1].Y);

                    if (ecx2 < 0xBB8 / AlliedVariables.s_V0x0054249C)
                    {
                        ebp05 = 0x01;
                    }
                }

                if (ebp05 != 0)
                {
                    if (AC.HasFlag(TShiftState.LeftButton))
                    {
                        AlliedVariables.s_V0x00543944 = 0x01;
                    }

                    if (AlliedVariables.s_V0x00542494 == 0)
                    {
                        AlliedVariables.s_V0x00542EDC[esi1].m00000C = TBrfForm_Proc_00506B78(BrfForm, esi1);
                    }

                    AlliedVariables.s_V0x005424C8 = esi1;
                    AlliedVariables.s_V0x0054250C = 0x01;
                    TBrfForm_Proc_00502624(BrfForm);
                }

                if (esi1 > AlliedVariables.s_V0x00542500 || esi1 >= 0x32 || ebp05 != 0)
                {
                    break;
                }
            }

            if (ebp05 == 0)
            {
                AlliedVariables.s_V0x0054250D = 0;
                int esi2 = 0;

                for (int ebp04 = 0; ebp04 < 0x08; ebp04++)
                {
                    if (AlliedVariables.s_V0x00542E78[ebp04] <= -1)
                    {
                        continue;
                    }

                    eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp04]);
                    int ecx1 = Math.Abs(TBrfForm__PROC_004FB244(BrfForm, A8 - 0x14, 0) - eax1.m000004.X);

                    if (ecx1 < 0x1194 / AlliedVariables.s_V0x0054249C)
                    {
                        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp04]);
                        int ecx2 = Math.Abs(TBrfForm__PROC_004FB244(BrfForm, A4, 0x01) - eax1.m000004.Y);

                        if (ecx1 < 0xBB8 / AlliedVariables.s_V0x0054249C)
                        {
                            esi2 = ebp04 + 1;
                        }
                    }
                }

                if (esi2 > 0)
                {
                    AlliedVariables.s_V0x005424CC = AlliedVariables.s_V0x00542E78[esi2 - 1];
                    AlliedVariables.s_V0x0054250D = 0x01;
                    TBrfForm__PROC_004FFCA0(BrfForm);
                }
            }
        }

        if (AlliedVariables.s_V0x00543944 != 0 || AlliedVariables.s_V0x0054250D != 0)
        {
            TBrfForm__PROC_004FFCA0(BrfForm);
        }
    }

    // L00503B34
    private static void TBrfForm_PaintBox1MouseMove(BriefingWindow BrfForm, object? Sender, TShiftState ecx0, int A4, int A8)
    {
        int esi = TBrfForm__PROC_004FB244(BrfForm, A8, 0) / AlliedVariables.s_BrfFormXSnapBtnStep * AlliedVariables.s_BrfFormXSnapBtnStep;
        int edi = TBrfForm__PROC_004FB244(BrfForm, A4, 0x01) / AlliedVariables.s_BrfFormYSnapBtnStep * AlliedVariables.s_BrfFormYSnapBtnStep;

        if (AlliedVariables.s_V0x00543944 != 0)
        {
            if (BrfForm.LockX.IsChecked != true)
            {
                AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].X = (short)esi;
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].m00000C);
                eax1.m000004.X = (short)esi;
            }

            if (BrfForm.LockY.IsChecked != true)
            {
                AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].Y = (short)edi;
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].m00000C);
                eax1.m000004.Y = (short)edi;
            }

            AlliedVariables.s_V0x00543B53 = true;
            AlliedVariables.s_V0x0054250C = 0x01;
            TBrfForm_Proc_00502624(BrfForm);
            AlliedVariables.s_V0x0054250C = 0;
        }
        else if (AlliedVariables.s_V0x0054250D != 0)
        {
            if (BrfForm.LockX.IsChecked != true)
            {
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x005424CC);
                eax1.m000004.X = (short)esi;
            }

            if (BrfForm.LockY.IsChecked != true)
            {
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x005424CC);
                eax1.m000004.Y = (short)edi;
            }

            TBrfForm_Proc_00502624(BrfForm);
        }

        Controls_TControl_SetText(BrfForm.Label23, "X = " + Allied_FloatToText(0x02, 0x07, 0x02, esi / 160.0f));
        Controls_TControl_SetText(BrfForm.Label24, "Y = " + Allied_FloatToText(0x02, 0x07, 0x02, edi / 160.0f));
        BrfForm.Label23.Update();
        BrfForm.Label24.Update();
    }

    // L00506B78
    private static int TBrfForm_Proc_00506B78(BriefingWindow BrfForm, int edx0)
    {
        TCommandObject eax1;

        int esi = 0;
        bool bl = false;

        while (!bl)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

            if (eax1.m000004.Time != 0)
            {
                break;
            }

            if (esi >= AlliedVariables.s_V0x00543CF4.Count - 1)
            {
                break;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

            if (eax1.m000004.BriefingCommand == BriefingCommandEnum.MoveIcon)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

                if (eax1.m000004.Parameter == edx0)
                {
                    bl = true;
                }
            }

            if (!bl)
            {
                esi++;
            }
        }

        return esi;
    }

    // L00506B74
    private static void TBrfForm_AccClick(BriefingWindow BrfForm, object? Sender)
    {
    }

    // L004FE674
    private static void TBrfForm_AutoBrfBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x00542498 = 0;

        if (MessageBox_ShowConfirmation("Rewrite briefing - Are you sure?", null) == TModalResultEnum.Yes)
        {
            int ebp34 = 0;

            for (int ebp0C = 0x01; ;)
            {
                if (!string.IsNullOrEmpty(AlliedVariables.s_V0x00542480.GetText(ebp0C)))
                {
                    ebp34++;
                }

                ebp0C++;

                if (ebp0C >= 0x20)
                {
                    break;
                }

                if (string.IsNullOrEmpty(AlliedVariables.s_V0x00542480.GetText(ebp0C)))
                {
                    break;
                }
            }

            if (ebp34 == 0)
            {
                MessageBox_ShowInformation("There is no text to display!");
            }
            else
            {
                TBrfForm__PROC_004FC9F8(BrfForm);

                TStrings ebp2C = new();
                int ebp1C = 0x09;
                TBrfForm__PROC_004FFC68(BrfForm, AlliedVariables.s_V0x00543B20);
                int esi = 0x01;
                int ebp10 = 0;

                //ebp4F0[1] = Controls_TControl_GetText(BrfForm.AutoBrfTime);
                // if( ebp4F0[1] == 0 )
                int ebp14 = (int)Math.Round(Runtime_L0040AA10__StrRecToFloat(Controls_TControl_GetText(BrfForm.AutoBrfTime)) * 25.0f);

                if (ebp14 < 0x01)
                {
                    ebp14 = 0x64;
                }

                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebp10;
                esi++;

                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x06;
                esi++;

                if (BrfForm.BeginCenteredChk.IsChecked == true)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, BrfForm.BeginOnFGBox.SelectedIndex);
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = eax1.m00147C[1].M000000[2 + AlliedVariables.s_V0x00543B20];
                    esi++;

                    eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, BrfForm.BeginOnFGBox.SelectedIndex);
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = eax1.m00147C[2].M000000[2 + AlliedVariables.s_V0x00543B20];
                    esi++;
                }
                else
                {
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0;
                    esi++;

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0;
                    esi++;
                }

                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebp10;
                esi++;

                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0;
                esi++;

                if (BrfForm.BeginZoomChk.IsChecked == true)
                {
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = TBrfForm_Proc_00500FF8(BrfForm, BrfForm.BeginZoomBox);
                    esi++;

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = TBrfForm_Proc_00500FF8(BrfForm, BrfForm.BeginZoomBox);
                    esi++;
                }
                else
                {
                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x20;
                    esi++;

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x20;
                    esi++;
                }

                for (int ebp0C = 0x01; ebp0C <= ebp34; ebp0C++)
                {
                    ebp2C.Clear();
                    string ebp028_1 = AlliedVariables.s_V0x00542480.GetText(ebp0C);

                    for (int ebp30 = 0x01; ebp30 <= ebp028_1.Length; ebp30++)
                    {
                        if (ebp028_1[ebp30 - 1] != '[')
                        {
                            continue;
                        }

                        StringBuilder ebp028_0 = new();

                        int ebx = ebp30;
                        int edi = 0x01;

                        while (true)
                        {
                            ebp028_0.Append(System_LStrFromChar(ebp028_1[ebx - 1]));
                            ebx++;
                            edi++;

                            if (ebp028_1[ebx - 1] == ' ' || ebp028_1[ebx - 1] == ']')
                            {
                                break;
                            }

                            if (ebx > ebp028_1.Length - 1)
                            {
                                break;
                            }
                        }

                        //System_LStrDelete(&ebp028[0], 0x01, 0x01);
                        ebp028_0.Remove(0, 1);
                        ebp2C.Add(ebp028_0.ToString());
                    }

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebp10;
                    esi++;

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x05;
                    esi++;

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebp0C;
                    esi++;

                    if (BrfForm.AutoBox.IsChecked == true)
                    {
                        int ebp38 = ebp2C.GetCount();

                        for (int ebp30 = 0; ebp30 < ebp38; ebp30++)
                        {
                            int ebp18 = 0;
                            int ebp3C = AlliedVariables.s_FlightGroupObjectsList.Count;

                            for (int ebx = 0; ebx < ebp3C; ebx++)
                            {
                                S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);
                                if (!string.Equals(System_LStrFromPCharLen(eax1.FlightGroupStruct.Name, 0x14), ebp2C.GetText(ebp30), StringComparison.Ordinal))
                                {
                                    continue;
                                }

                                eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, ebx);

                                if (eax1.IsWPEnabled[0x0E + AlliedVariables.s_V0x00543B20] != 0x01)
                                {
                                    continue;
                                }

                                short ax2 = (short)((short)(ebp14 / ebp2C.GetCount() * ebp30) + (short)ebp10 + 0x0A + (short)ebp18 * 0x05);
                                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = ax2;
                                esi++;

                                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebp1C;
                                esi++;

                                ebp1C++;

                                if (ebp1C > 0x10)
                                {
                                    ebp1C = 0x09;
                                }

                                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebx;
                                esi++;

                                ebp18++;
                            }
                        }
                    }

                    ebp10 += ebp14;

                    if (ebp2C.GetCount() > 0)
                    {
                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebp10;
                        esi++;

                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x08;
                        esi++;
                    }

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)ebp10;
                    esi++;

                    AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x03;
                    esi++;
                }

                if (ebp14 > 0x3C)
                {
                    if (BrfForm.BeginCenteredChk.IsChecked == true)
                    {
                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)(ebp10 - 0x3C);
                        esi++;

                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x06;
                        esi++;

                        S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, BrfForm.BeginOnFGBox.SelectedIndex);
                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = eax1.m00147C[1].M000000[2 + AlliedVariables.s_V0x00543B20];
                        esi++;

                        eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, BrfForm.BeginOnFGBox.SelectedIndex);
                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = eax1.m00147C[2].M000000[2 + AlliedVariables.s_V0x00543B20];
                        esi++;
                    }

                    if (BrfForm.BeginZoomChk.IsChecked == true)
                    {
                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = (short)(ebp10 - 0x3C);
                        esi++;

                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x07;
                        esi++;

                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = TBrfForm_Proc_00500FF8(BrfForm, BrfForm.BeginZoomBox);
                        esi++;

                        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = TBrfForm_Proc_00500FF8(BrfForm, BrfForm.BeginZoomBox);
                        esi++;
                    }
                }

                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x270F;
                esi++;

                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[esi - 1] = 0x22;
                esi++;

                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.CodeSize = (short)(esi - 1);
                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Length = (short)ebp10;

                Controls_TControl_SetText(BrfForm.BrfTimeEd, Runtime_L0040A8D4_FloatToText(0, ebp10 / 25.0f));
                AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.ForTeam[AlliedVariables.s_V0x00543B20] = 0x01;
                TBrfForm_Proc_004FC5C8(BrfForm);

                AlliedVariables.s_V0x00543B53 = true;
            }
        }
    }

    // L004FEFD4
    private static void TBrfForm_MapTagGridSetEditText(BriefingWindow BrfForm, object? edx0, int ecx0, string A4, int A8)
    {
        string ebp04 = Grids_TStringGrid_GetCells(BrfForm.MapTagGrid, 0x01, A8);
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingTags.Put(A8, ebp04);
    }

    // L004FF04C
    private static void TBrfForm_BrfStrGridSetEditText(BriefingWindow BrfForm, object? edx0, int ecx0, string A4, int A8)
    {
        string ebp10_3 = Grids_TStringGrid_GetCells(BrfForm.BrfStrGrid, 0x01, A8);
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingStrings.Put(A8, ebp10_3);

        if (A8 == 0)
        {
            string ebp10_1 = Path.GetFileName(AlliedVariables.s_V0x00543BF8);
            string ebp10_0 = AlliedVariables.s_Allied_Briefing[0].BriefingStrings.GetText(0);
            string ebp10_2 = ProductVersionHelpers.GetNameAndVersion() + " - (" + ebp10_1 + ") - " + ebp10_0;
            Controls_TControl_SetText(AlliedVariables.s_AlliedForm1Window!, ebp10_2);
        }
    }

    // L004FF4BC
    private static void TBrfForm_CommandBoxChange(BriefingWindow BrfForm)
    {
        BriefingCommandEnum si = (BriefingCommandEnum)BrfForm.CommandBox.SelectedIndex;

        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        eax1.m000004.BriefingCommand = si;

        if (BrfForm.CommandBox.SelectedIndex == 0x22)
        {
            Spin_TSpinEdit_SetValue(BrfForm.TimeSpin, 0x270F);
        }

        TBrfForm__PROC_004FFCA0(BrfForm);
        TBrfForm__PROC_004FF548(BrfForm, (BriefingCommandEnum)BrfForm.CommandBox.SelectedIndex);
        TBrfForm__PROC_004FCA50(BrfForm);
        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
    }

    // L004FF200
    private static void TBrfForm_IndexBoxChange(BriefingWindow BrfForm)
    {
        AlliedVariables.s_V0x00542505 = 0;

        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);

        switch (eax1.m000004.BriefingCommand)
        {
            case BriefingCommandEnum.MoveMap:
                {
                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, BrfForm.IndexBox.SelectedIndex);
                    short si = eax2.m00147C[1].M000000[2 + AlliedVariables.s_V0x00543B20];
                    TCommandObject eax3 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    eax3.m000004.X = si;
                    eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, BrfForm.IndexBox.SelectedIndex);
                    si = eax2.m00147C[2].M000000[2 + AlliedVariables.s_V0x00543B20];
                    eax3 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    eax3.m000004.Y = si;
                    eax3 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    Controls_TControl_SetText(BrfForm.XEd, Allied_FloatToText(0x02, 0x07, 0x02, eax3.m000004.X / 160.0f));
                    eax3 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    Controls_TControl_SetText(BrfForm.YEd, Allied_FloatToText(0x02, 0x07, 0x02, eax3.m000004.Y / 160.0f));
                    break;
                }

            case BriefingCommandEnum.ScaleMap:
                {
                    short si = TBrfForm_Proc_00500FF8(BrfForm, BrfForm.IndexBox);
                    TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    eax2.m000004.X = si;
                    eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    si = eax2.m000004.X;
                    eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    eax2.m000004.Y = si;
                    eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    Controls_TControl_SetText(BrfForm.XEd, eax2.m000004.X.ToString(CultureInfo.InvariantCulture));
                    break;
                }

            case BriefingCommandEnum.NewIcon:
                {
                    short si = (short)BrfForm.IndexBox.SelectedIndex;
                    //short si = (short)Math.Max(BrfForm.IndexBox.SelectedIndex, 0);
                    TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    eax2.m000004.Parameter = si;
                    break;
                }

            case BriefingCommandEnum.ShowShipData:
                {
                    short si = (short)BrfForm.IndexBox.SelectedIndex;
                    TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    eax2.m000004.IconIndex = si;
                    break;
                }

            default:
                {
                    short si = (short)BrfForm.IndexBox.SelectedIndex;
                    TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    eax2.m000004.Parameter = si;
                    break;
                }
        }

        TBrfForm__PROC_004FCA50(BrfForm);
        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;

        TBrfForm__PROC_004FFCA0(BrfForm);

        AlliedVariables.s_V0x00542505 = 0x01;
    }

    // L004FF16C
    private static void TBrfForm_BrfTimeEdChange(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x00542ED8 = (int)Math.Round(Runtime_L0040AA10__StrRecToFloat(Controls_TControl_GetText(BrfForm.BrfTimeEd)) * 25.0f);
        StdCtrls_TScrollBar_SetMax(BrfForm.TimeScroll, AlliedVariables.s_V0x00542ED8);
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FF1E8
    private static void TBrfForm_BrfUnk1EdChange(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FF1F0
    private static void TBrfForm_BrfUnk2EdChange(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FF1F8
    private static void TBrfForm_BrfUnk3EdChange(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FFA94
    private static void TBrfForm_DeleteComClick(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_ConfDeletesChkSetting)
        {
            TModalResultEnum ax1 = MessageBox_ShowConfirmation("Delete Current Instruction - Are You Sure?", null);

            if (ax1 == TModalResultEnum.Yes)
            {
                TBrfForm_Proc_00500F8C(BrfForm);
            }
        }
        else
        {
            TBrfForm_Proc_00500F8C(BrfForm);
        }
    }

    // L004FFAFC
    private static void TBrfForm_InsertStopClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.PageBreak);
    }

    // L00500104
    private static void TBrfForm_AddCommand(BriefingWindow BrfForm, BriefingCommandEnum edx0)
    {
        AlliedVariables.s_V0x005424DC = AlliedVariables.s_V0x005424A0;
        AlliedVariables.s_V0x005424E0 = AlliedVariables.s_V0x005424A4;
        AlliedVariables.s_V0x005424E4 = AlliedVariables.s_V0x0054249C;
        AlliedVariables.s_V0x005424E8 = AlliedVariables.s_V0x005424BC;
        AlliedVariables.s_V0x005424EC = AlliedVariables.s_V0x005424C0;

        if (BrfForm.Timer1.GetM000048())
        {
            TBrfForm_Proc_00503298(BrfForm);
        }

        BriefingCommandEnum edi = edx0 switch
        {
            BriefingCommandEnum.FlightGroupTags1 => TBrfForm_Proc_005042E8(BrfForm, AlliedVariables.s_V0x00542498),
            BriefingCommandEnum.TextTag1 => TBrfForm_Proc_005041C4(BrfForm, AlliedVariables.s_V0x00542498 - 1),
            _ => edx0
        };

        TCommandObject ebx = new();
        ebx.m000004.BriefingCommand = edi;
        ebx.m000004.ColorIndex = 0;
        ebx.m000004.X = 0;
        ebx.m000004.Y = 0;
        ebx.m000004.CraftId = CraftIdEnum._001_0_0_Xwing;
        ebx.m000004.Parameter = 0;

        if (edi == BriefingCommandEnum.RotateIcon)
        {
            ebx.m000004.Rotation = AlliedVariables.s_BriefingCurrentIconRotation;
        }
        else
        {
            ebx.m000004.Rotation = IconRotationEnum.Rotate0;
        }

        ebx.m000004.IconIndex = 0x01;

        if (edi == BriefingCommandEnum.ScaleMap)
        {
            ebx.m000004.X = 0x20;
            ebx.m000004.Y = 0x20;
        }

        switch (edi)
        {
            case BriefingCommandEnum.PageBreak:
            case BriefingCommandEnum.ClearFlightGroupTags:
            case BriefingCommandEnum.ClearTextTags:
                ebx.m000004.m000014 = AlliedVariables.s_V0x005424DC;
                ebx.m000004.m000018 = AlliedVariables.s_V0x005424E0;
                ebx.m000004.m000024 = AlliedVariables.s_V0x005424E4;
                ebx.m000004.m00001C = AlliedVariables.s_V0x005424E8;
                ebx.m000004.m000020 = AlliedVariables.s_V0x005424EC;
                break;
        }

        if ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag) == BrfPageEnum.WYSIWYG)
        {
            ebx.m000004.Time = (short)AlliedVariables.s_V0x00542494;

            switch (edi)
            {
                case BriefingCommandEnum.FlightGroupTags1:
                case BriefingCommandEnum.FlightGroupTags2:
                case BriefingCommandEnum.FlightGroupTags3:
                case BriefingCommandEnum.FlightGroupTags4:
                case BriefingCommandEnum.FlightGroupTags5:
                case BriefingCommandEnum.FlightGroupTags6:
                case BriefingCommandEnum.FlightGroupTags7:
                case BriefingCommandEnum.FlightGroupTags8:
                    ebx.m000004.Parameter = (short)AlliedVariables.s_V0x005424C8;
                    break;
            }

            int ebp04 = AlliedVariables.s_V0x00542498;

            while (AlliedVariables.s_V0x00543CF4.Count - 1 > ebp04)
            {
                ebp04++;

                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebp04);

                if (eax1.m000004.Time > AlliedVariables.s_V0x00542494)
                {
                    break;
                }
            }

            AlliedVariables.s_V0x00542498 = ebp04;

            switch (edi)
            {
                case BriefingCommandEnum.ShowShipData:
                case BriefingCommandEnum.MoveIcon:
                    break;

                default:
                    AlliedVariables.s_V0x00543CF4.Insert(AlliedVariables.s_V0x00542498, ebx);
                    break;
            }

            AlliedVariables.s_V0x00542ECE = 0x01;

            switch (edi)
            {
                case BriefingCommandEnum.BriefingText:
                    Controls_TControl_SetText(BrfForm.GroupBox4, "Briefing Text:");
                    BrfForm.Guidememo.Clear();
                    BrfForm.Guidememo.AddLine("1. Choose the Text you want to show from the drop down list.\r\n2. Press the \"Done\" button to insert the instruction or \"Cancel\" to abort.");
                    BrfForm.MapIndex.SetItems(AlliedVariables.s_V0x00542480);
                    BrfForm.MapIndex.SelectedIndex = 0;
                    Controls_TControl_SetVisible(BrfForm.MapIndex, true);
                    AlliedVariables.s_V0x00542EC7 = 0x01;
                    TBrfForm_Proc_00503A20(BrfForm, false);
                    break;

                case BriefingCommandEnum.MoveMap:
                    Controls_TControl_SetText(BrfForm.GroupBox4, "Move Map");
                    BrfForm.Guidememo.Clear();
                    BrfForm.Guidememo.AddLine("1. Adjust the X and Y scrollbars to move the map to the desired postion.\r\n2. Press the \"Done\" button to insert the instruction or \"Cancel\" to abort.");
                    TBrfForm_Proc_00503A20(BrfForm, false);
                    Controls_TControl_SetVisible(BrfForm.XScroll, true);
                    BrfForm.XScroll.Update();
                    Controls_TControl_SetVisible(BrfForm.YScroll, true);
                    BrfForm.XScroll.Update();
                    AlliedVariables.s_V0x00542EC6 = 0x01;
                    break;

                case BriefingCommandEnum.ScaleMap:
                    Controls_TControl_SetText(BrfForm.GroupBox4, "Adjust Zoom");
                    BrfForm.Guidememo.Clear();
                    BrfForm.Guidememo.AddLine("1. Adjust the Zoom scrollbar to the desired zoom level.\r\n2. Press the \"Done\" button to insert the instruction or \"Cancel\" to abort.");
                    AlliedVariables.s_V0x00542EC5 = 0x01;
                    StdCtrls_TScrollBar_SetPosition(BrfForm.ZoomScroll, AlliedVariables.s_V0x0054249C);
                    Controls_TControl_SetVisible(BrfForm.ZoomScroll, true);
                    BrfForm.ZoomScroll.Update();
                    Controls_TControl_SetText(BrfForm.ZoomBarLab, "Zoom: " + AlliedVariables.s_V0x0054249C.ToString(CultureInfo.InvariantCulture));
                    Controls_TControl_SetVisible(BrfForm.ZoomBarLab, true);
                    BrfForm.ZoomBarLab.Update();
                    TBrfForm_Proc_00503A20(BrfForm, false);
                    break;

                case BriefingCommandEnum.TextTag1:
                case BriefingCommandEnum.TextTag2:
                case BriefingCommandEnum.TextTag3:
                case BriefingCommandEnum.TextTag4:
                case BriefingCommandEnum.TextTag5:
                case BriefingCommandEnum.TextTag6:
                case BriefingCommandEnum.TextTag7:
                case BriefingCommandEnum.TextTag8:
                    Controls_TControl_SetText(BrfForm.GroupBox4, "Map Tag:");
                    BrfForm.Guidememo.Clear();
                    BrfForm.Guidememo.AddLine("1. Choose the Tag you want to show, and its color, from the drop down lists.\r\n2. Click on the map to place it. You may click repeatedly until you are satisfied with the position.\r\n3. Press the \"Done\" button to insert the instruction or \"Cancel\" to abort.");
                    BrfForm.MapIndex.SetItems(AlliedVariables.s_V0x0054247C);
                    BrfForm.MapIndex.SelectedIndex = 0;
                    Controls_TControl_SetVisible(BrfForm.MapIndex, true);
                    BrfForm.MapColor.SetItems(AlliedVariables.s_V0x0054248C);
                    BrfForm.MapColor.SelectedIndex = 0;
                    Controls_TControl_SetVisible(BrfForm.MapColor, true);
                    AlliedVariables.s_V0x00542EC8 = 0x01;
                    TBrfForm_Proc_00503A20(BrfForm, false);
                    break;

                case BriefingCommandEnum.NewIcon:
                    Controls_TControl_SetText(BrfForm.GroupBox4, "Map Icon:");
                    BrfForm.Guidememo.Clear();
                    BrfForm.Guidememo.AddLine("1. Choose the Ship Icon you want to show, and its IFF, from the drop down lists.\r\n2. Click on the map to place it. You may click repeatedly until you are satisfied with the position.\r\n3. Press the \"Done\" button to insert the instruction or \"Cancel\" to abort.");
                    BrfForm.MapIndex.SetItems(AlliedVariables.s_Strings_Ships);
                    BrfForm.MapIndex.SelectedIndex = 0x01;
                    Controls_TControl_SetVisible(BrfForm.MapIndex, true);
                    BrfForm.MapColor.SetItems(AlliedVariables.s_Strings_IFF);
                    BrfForm.MapColor.SelectedIndex = 0;
                    Controls_TControl_SetVisible(BrfForm.MapColor, true);
                    AlliedVariables.s_V0x00542ECA = 0x01;
                    TBrfForm_Proc_00503A20(BrfForm, false);
                    break;

                case BriefingCommandEnum.RotateIcon:
                    ebx.m000004.Parameter = (short)AlliedVariables.s_V0x005424C8;
                    AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].Rotation = AlliedVariables.s_BriefingCurrentIconRotation;
                    TBrfForm_Proc_0050548C(
                        BrfForm,
                        AlliedVariables.s_V0x005424C8,
                        AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].CraftId,
                        AlliedVariables.s_BriefingCurrentIconRotation,
                        AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].Iff
                        );
                    AlliedVariables.s_V0x0054250C = 0x01;
                    TBrfForm_Proc_00502624(BrfForm);
                    break;

                case BriefingCommandEnum.ChangeRegion:
                    Controls_TControl_SetText(BrfForm.GroupBox4, "Choose Region");
                    BrfForm.Guidememo.Clear();
                    BrfForm.Guidememo.AddLine("1. Choose the new Region from the drop down list.\r\n2. Press the \"Done\" button to insert the instruction or \"Cancel\" to abort.");
                    BrfForm.MapIndex.Clear();

                    for (int ebp04a = 0; ebp04a < 0x04; ebp04a++)
                    {
                        BrfForm.MapIndex.AddItem(System_LStrFromPCharLen(AlliedVariables.s_TieFileHeader.Header.Regions[ebp04a].Name, 0x84));
                    }

                    BrfForm.MapIndex.SelectedIndex = 0;
                    Controls_TControl_SetVisible(BrfForm.MapIndex, true);
                    AlliedVariables.s_V0x00542ECB = 0x01;
                    TBrfForm_Proc_00503A20(BrfForm, false);
                    break;

                case BriefingCommandEnum.ClearIcon:
                    ebx.m000004.Parameter = (short)AlliedVariables.s_V0x005424C8;
                    ebx.m000004.BriefingCommand = BriefingCommandEnum.NewIcon;
                    ebx.m000004.CraftId = CraftIdEnum._000__1_0;
                    AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].m000006 = 0;
                    AlliedVariables.s_V0x0054250C = 0x01;
                    TBrfForm_Proc_00502624(BrfForm);
                    break;

                case BriefingCommandEnum.IconInfo:
                    ebx.m000004.BriefingCommand = BriefingCommandEnum.ShowShipData;
                    ebx.m000004.Parameter = 0x01;
                    ebx.m000004.IconIndex = (short)AlliedVariables.s_V0x005424C8;
                    ebx = new();
                    ebx.m000004.Time = (short)(AlliedVariables.s_V0x00542494 + 0x78);
                    ebx.m000004.BriefingCommand = BriefingCommandEnum.ShowShipData;
                    ebx.m000004.Parameter = 0;
                    ebx.m000004.IconIndex = (short)AlliedVariables.s_V0x005424C8;
                    AlliedVariables.s_V0x00543CF4.Insert(AlliedVariables.s_V0x00542498, ebx);
                    AlliedVariables.s_V0x00542ED3 = 0x01;
                    AlliedVariables.s_V0x00542ED4 = AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].CraftId;
                    AlliedVariables.s_V0x00542ECE = 0;
                    break;
            }
        }
        else if ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag) == BrfPageEnum.InstructionList)
        {
            if (AlliedVariables.s_V0x00543CF4.Count > 0x01 && AlliedVariables.s_V0x00542498 != 0)
            {
                if (AlliedVariables.s_V0x00543CF4.Count - 1 == AlliedVariables.s_V0x00542498)
                {
                    TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 - 1);
                    ebx.m000004.Time = eax1.m000004.Time;
                }
                else
                {
                    TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                    ebx.m000004.Time = eax1.m000004.Time;
                }
            }

            AlliedVariables.s_V0x00543CF4.Insert(AlliedVariables.s_V0x00542498, ebx);
        }

        int ebx1 = AlliedVariables.s_V0x00542498;
        TBrfForm__PROC_004FDECC(BrfForm, AlliedVariables.s_V0x00542498);
        TBrfForm__PROC_004FFCA0(BrfForm);
        TBrfForm__PROC_004FCA50(BrfForm);
        AlliedVariables.s_V0x00542498 = ebx1;

        Buttons_TSpeedButton_SetDown(BrfForm.Play, BrfForm.Timer1.GetM000048());
    }

    // L004FFB08
    private static void TBrfForm_MoveUpBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542498 <= 0)
        {
            return;
        }

        if (AlliedVariables.s_V0x00543CF4.Count - 1 <= AlliedVariables.s_V0x00542498)
        {
            return;
        }

        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        short si = eax1.m000004.Time;
        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 - 1);
        short di = eax1.m000004.Time;
        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        eax1.m000004.Time = di;
        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 - 1);
        eax1.m000004.Time = si;
        Classes_TList_Exchange(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498, AlliedVariables.s_V0x00542498 - 1);
        AlliedVariables.s_V0x00542498 -= 1;
        TBrfForm__PROC_004FFCA0(BrfForm);
        TBrfForm__PROC_004FCA50(BrfForm);
    }

    // L004FFBBC
    private static void TBrfForm_MoveDownBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00543CF4.Count - 0x02 <= AlliedVariables.s_V0x00542498)
        {
            return;
        }

        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        short si = eax1.m000004.Time;
        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 + 1);
        short di = eax1.m000004.Time;
        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        eax1.m000004.Time = di;
        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 + 1);
        eax1.m000004.Time = si;
        Classes_TList_Exchange(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498, AlliedVariables.s_V0x00542498 + 1);
        AlliedVariables.s_V0x00542498 += 1;
        TBrfForm__PROC_004FCA50(BrfForm);
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FFCA8
    private static void TBrfForm_XEdChange(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542505 == 0)
        {
            return;
        }

        if (string.IsNullOrEmpty(Controls_TControl_GetText(BrfForm.XEd)) || string.Equals(Controls_TControl_GetText(BrfForm.XEd), "-", StringComparison.Ordinal))
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.X = 0;
        }
        else
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);

            if (eax1.m000004.BriefingCommand == BriefingCommandEnum.ScaleMap)
            {
                short si = (short)Allied_StrRec_to_int(Controls_TControl_GetText(BrfForm.XEd));
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                eax1.m000004.X = si;
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                si = eax1.m000004.X;
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                eax1.m000004.Y = si;
            }
            else
            {
                short si = (short)Unit_00513838_Proc_0051443C(Controls_TControl_GetText(BrfForm.XEd));
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                eax1.m000004.X = si;
            }
        }

        TBrfForm__PROC_004FCA50(BrfForm);
        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FFE1C
    private static void TBrfForm_YEdChange(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542505 == 0)
        {
            return;
        }

        if (string.IsNullOrEmpty(Controls_TControl_GetText(BrfForm.YEd)) || string.Equals(Controls_TControl_GetText(BrfForm.YEd), "-", StringComparison.Ordinal))
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Y = 0;
        }
        else
        {
            short si = (short)Unit_00513838_Proc_0051443C(Controls_TControl_GetText(BrfForm.YEd));
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Y = si;
        }

        TBrfForm__PROC_004FCA50(BrfForm);
        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L004FFF14
    private static void TBrfForm_TimeSpinChange(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542505 == 0)
        {
            return;
        }

        if (BrfForm.CheckBox1.IsChecked == true)
        {
            if (Spin_TSpinEdit_GetValue(BrfForm.TimeSpin) > AlliedVariables.s_V0x005424D8)
            {
                int esi = AlliedVariables.s_V0x00543CF4.Count - 1;

                for (int edi = AlliedVariables.s_V0x00542498 + 1; edi < esi; edi++)
                {
                    TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, edi);
                    eax1.m000004.Time += (short)BrfForm.TimeSpin.Value!.Value;
                }
            }
            else
            {
                int esi = AlliedVariables.s_V0x00543CF4.Count - 1;

                for (int edi = AlliedVariables.s_V0x00542498 + 1; edi < esi; edi++)
                {
                    TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, edi);

                    if (eax1.m000004.Time <= BrfForm.TimeSpin.Value!.Value)
                    {
                        continue;
                    }

                    eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, edi);
                    eax1.m000004.Time -= (short)BrfForm.TimeSpin.Value!.Value;
                }
            }
        }

        AlliedVariables.s_V0x005424D8 = Spin_TSpinEdit_GetValue(BrfForm.TimeSpin);

        TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        eax2.m000004.Time = (short)Spin_TSpinEdit_GetValue(BrfForm.TimeSpin);

        eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        Controls_TControl_SetText(BrfForm.Label15, "Time (" + Runtime_L0040A8D4_FloatToText(0, eax2.m000004.Time / 25.0f) + " sec)");

        TBrfForm__PROC_004FCA50(BrfForm);
        BrfForm.ComDisplay.SelectedIndex = AlliedVariables.s_V0x00542498;
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L00500E08
    private static void TBrfForm_InsertMoveClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.MoveMap);
    }

    // L00500E14
    private static void TBrfForm_InsertZoomClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.ScaleMap);
    }

    // L00500E20
    private static void TBrfForm_InsertTextClick(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542494 != 0)
        {
            TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.PageBreak);
        }

        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.BriefingText);
    }

    // L00505294
    private static void TBrfForm_PopupMenu1Popup(BriefingWindow BrfForm, object? Sender)
    {
        switch ((BrfPageEnum)Convert.ToInt32(BrfForm.BrfPages.GetActivePage().Tag))
        {
            case BrfPageEnum.WYSIWYG:
                for (int ebx = 0x08; ebx < 0x0C; ebx++)
                {
                    Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, ebx), true);
                }

                if (BrfForm.Timer1.GetM000048())
                {
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x09), false);
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x11), false);
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x0A), true);
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x0B), true);
                }
                else
                {
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x09), true);
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x11), true);
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x0A), false);
                    Menus_TMenuItem_SetEnabled(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x0B), false);
                }

                for (int ebx = 0x0C; ebx < 0x10; ebx++)
                {
                    Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, ebx), false);
                }

                Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x10), true);
                Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x11), true);
                break;

            case BrfPageEnum.InstructionList:
                for (int ebx = 0x08; ebx < 0x0C; ebx++)
                {
                    Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, ebx), false);
                }

                for (int ebx = 0x0C; ebx < 0x10; ebx++)
                {
                    Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, ebx), true);
                }

                Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x10), false);
                Menus_TMenuItem_SetVisible(Menus_TMenuItem_GetItem(BrfForm.PopupMenu1, 0x11), false);
                break;
        }
    }

    // L00500E48
    private static void TBrfForm_InsertFGBoxClick(BriefingWindow BrfForm, object? Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.FlightGroupTags1);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
    }

    // L005042E8
    private static BriefingCommandEnum TBrfForm_Proc_005042E8(BriefingWindow BrfForm, int edx0)
    {
        TCommandObject eax1;
        BriefingCommandEnum edi = BriefingCommandEnum.None;

        for (int ebx1 = 0; ebx1 <= edx0; ebx1++)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (eax1.m000004.BriefingCommand < BriefingCommandEnum.ClearFlightGroupTags)
            {
                continue;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (eax1.m000004.BriefingCommand > BriefingCommandEnum.FlightGroupTags8)
            {
                continue;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (eax1.m000004.BriefingCommand == BriefingCommandEnum.ClearFlightGroupTags)
            {
                edi = BriefingCommandEnum.None;
                continue;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (edi < eax1.m000004.BriefingCommand)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);
                edi = eax1.m000004.BriefingCommand;
            }
        }

        BriefingCommandEnum ebx = BriefingCommandEnum.None;

        switch (edi)
        {
            case BriefingCommandEnum.None:
            case BriefingCommandEnum.ClearFlightGroupTags:
                ebx = BriefingCommandEnum.FlightGroupTags1;
                break;

            case BriefingCommandEnum.FlightGroupTags8:
                ebx = BriefingCommandEnum.FlightGroupTags1;
                MessageBox_ShowInformation("Seems like all FG Boxes are in use. It would be best to put in a \"Clear Boxes\" instruction before boxing more FGs");
                break;

            default:
                ebx = edi + 1;
                break;
        }

        return ebx;
    }

    // L00500E74
    private static void TBrfForm_InsertClearBoxClick(BriefingWindow BrfForm, object? Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.ClearFlightGroupTags);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
    }

    // L00500EA0
    private static void TBrfForm_InsertTagClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.TextTag1);
    }

    // L00500EAC
    private static void TBrfForm_InsertClearTagsClick(BriefingWindow BrfForm, object? Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.ClearTextTags);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
    }

    // L005041C4
    private static BriefingCommandEnum TBrfForm_Proc_005041C4(BriefingWindow BrfForm, int edx0)
    {
        TCommandObject eax1;
        BriefingCommandEnum edi = BriefingCommandEnum.None;

        for (int ebx1 = 0; ebx1 <= edx0; ebx1++)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (eax1.m000004.BriefingCommand < BriefingCommandEnum.ClearTextTags)
            {
                continue;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (eax1.m000004.BriefingCommand > BriefingCommandEnum.TextTag8)
            {
                continue;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (eax1.m000004.BriefingCommand == BriefingCommandEnum.ClearTextTags)
            {
                edi = BriefingCommandEnum.None;
                continue;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);

            if (edi < eax1.m000004.BriefingCommand)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx1);
                edi = eax1.m000004.BriefingCommand;
            }
        }

        BriefingCommandEnum ebx = BriefingCommandEnum.None;

        switch (edi)
        {
            case BriefingCommandEnum.None:
            case BriefingCommandEnum.ClearTextTags:
                ebx = BriefingCommandEnum.TextTag1;
                break;

            case BriefingCommandEnum.TextTag8:
                ebx = BriefingCommandEnum.TextTag1;
                MessageBox_ShowInformation("Seems like all Map Tags are in use. It would be best to put in a \"Clear Tags\" instruction before adding more Tags");
                break;

            default:
                ebx = edi + 1;
                break;
        }

        return ebx;
    }

    // L00506D30
    private static void TBrfForm_InsertRegionClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.ChangeRegion);
    }

    // L005068D4
    private static int TBrfForm_Proc_005068D4(BriefingWindow BrfForm)
    {
        int ebx = 0;
        int edi = 0;
        int ebp04 = 0;

        while (true)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);

            switch (eax1.m000004.BriefingCommand)
            {
                case BriefingCommandEnum.NewIcon:
                    eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);

                    if (edi < eax1.m000004.Parameter)
                    {
                        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);
                        edi = eax1.m000004.Parameter;
                    }

                    ebp04++;
                    break;

                case BriefingCommandEnum.ChangeRegion:
                    edi = 0;
                    ebp04 = 0;
                    break;
            }

            ebx++;

            if (ebx >= AlliedVariables.s_V0x00543CF4.Count - 1)
            {
                break;
            }

            int esi = ebx - 1;

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, esi);

            if (eax1.m000004.BriefingCommand == BriefingCommandEnum.ChangeRegion && esi > AlliedVariables.s_V0x00542498)
            {
                break;
            }
        }

        // edi = 0;
        // System_DoneExcept();

        if (edi == 0 && ebp04 == 0)
        {
            return 0;
        }

        return edi + 1;
    }

    // L005061DC
    private static void TBrfForm_InsertNewClick(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x005424C8 = TBrfForm_Proc_005068D4(BrfForm);
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.NewIcon);
    }

    // L005061FC
    private static void TBrfForm_InsertMoveIconClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.MoveIcon);
    }

    // L00506208
    private static void TBrfForm_InsertRotateClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.RotateIcon);
    }

    // L00506214
    private static void TBrfForm_InsertInfoClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.ShowShipData);
    }

    // L00506220
    private static void TBrfForm_InsertChangeRegionClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.ChangeRegion);
    }

    // L00506E80
    private static void TBrfForm_ToolButton15Click(BriefingWindow BrfForm, object? Sender)
    {
        if (MessageBox_ShowConfirmation("Clear all instructions. Are you sure?", null) != TModalResultEnum.Yes)
        {
            return;
        }

        TBrfForm_Proc_00506DF8(BrfForm);
        TBrfForm_Proc_004FC44C(BrfForm, AlliedVariables.s_V0x00543B20);
        BrfForm.Memo1.Clear();
    }

    // L00504AB0
    private static void TBrfForm_Button1Click(BriefingWindow BrfForm, object? Sender)
    {
        TModalResultEnum ax1 = MessageBox_ShowConfirmation("Set Briefing Points to Start Point 1 for Briefing " + (AlliedVariables.s_V0x00543B20 + 1).ToString(CultureInfo.InvariantCulture) + "?", null);

        if (ax1 == TModalResultEnum.Yes)
        {
            int ebp08 = AlliedVariables.s_FlightGroupObjectsList.Count;

            for (int esi = 0; esi < ebp08; esi++)
            {
                for (int ebx = 0; ebx < 0x03; ebx++)
                {
                    S0xFGObject eax1 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    S0xFGObject eax2 = Classes_TList_Get(AlliedVariables.s_FlightGroupObjectsList, esi);
                    eax2.m00147C[ebx + 1].M000000[2 + AlliedVariables.s_V0x00543B20] = eax1.m00147C[ebx].M000000[0];
                }
            }

            AlliedVariables.s_V0x0054250C = 0x01;
        }

        TBrfForm_Proc_00502624(BrfForm);
    }

    // L00506F18
    private static void TBrfForm_FastPlaybackBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, false);

        if (BrfForm.FastPlaybackBtn.IsChecked == true)
        {
            TLMDHiTimer__PROC_004B08F4(BrfForm.Timer1!, 0x0F);
        }
        else
        {
            TLMDHiTimer__PROC_004B08F4(BrfForm.Timer1!, 0x26);
        }

        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
    }

    // L00500F5C
    private static void TBrfForm_SaveBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542504 != 0)
        {
            TBrfForm_Proc_004FD9C4(BrfForm);
        }

        AlliedVariables.s_V0x00542504 = 0;

        Form1WindowImpl.TForm1_SaveBtnClick(AlliedVariables.s_AlliedForm1Window!, BrfForm.SaveBtn);
    }

    // L00501074
    private static void TBrfForm_PlayClick(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542ECE == 0)
        {
        }

        AlliedVariables.s_V0x00542ED8 = (int)Math.Round(Runtime_L0040AA10__StrRecToFloat(Controls_TControl_GetText(BrfForm.BrfTimeEd)) * 25.0f);

        if (AlliedVariables.s_TieBriefingData_Instance!.BriefingCode.CodeSize == 0)
        {
            MessageBox_ShowError("There are no instruction to playback");
        }
        else
        {
            if (string.Equals(Controls_TControl_GetText(BrfForm.BrfTimeEd), "0", StringComparison.Ordinal))
            {
                MessageBox_ShowError("This Brief's running time is set to zero seconds");
            }
            else
            {
                TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, true);
                Buttons_TSpeedButton_SetDown(BrfForm.Play, BrfForm.Timer1.GetM000048());
                Buttons_TSpeedButton_SetDown(BrfForm.Pause, false);
            }
        }
    }

    // L005011CC
    private static void TBrfForm_PauseClick(BriefingWindow BrfForm, object? Sender)
    {
        if (BrfForm.Timer1.GetM000048())
        {
            TBrfForm_Proc_00503298(BrfForm);
            Buttons_TSpeedButton_SetDown(BrfForm.Pause, true);
        }
        else if (BrfForm.Play.IsChecked == true)
        {
            TBrfForm_PlayClick(BrfForm, BrfForm.Pause);
        }
    }

    // L00501E38
    private static void TBrfForm_RewindClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_Proc_00502248(BrfForm);
    }

    // L00502EA0
    private static void TBrfForm_Proc_00502EA0(BriefingWindow BrfForm)
    {
        AlliedVariables.s_V0x00542ECC = 0;

        if (AlliedVariables.s_V0x00542506 != 0 || AlliedVariables.s_V0x00542507 != 0)
        {
            AlliedVariables.s_V0x0054250C = 0x01;
            AlliedVariables.s_V0x005424A0 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424BC);
            AlliedVariables.s_V0x005424A4 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424C0);

            if (AlliedVariables.s_V0x00542506 != 0)
            {
                if (Math.Abs(AlliedVariables.s_V0x0054249C - AlliedVariables.s_V0x005424B0) > 0x08)
                {
                    AlliedVariables.s_V0x0054249C += 0x07;
                }
                else if (Math.Abs(AlliedVariables.s_V0x0054249C - AlliedVariables.s_V0x005424B0) > 0x03)
                {
                    AlliedVariables.s_V0x0054249C += 0x02;
                }
                else if (AlliedVariables.s_V0x0054249C != AlliedVariables.s_V0x005424B0)
                {
                    AlliedVariables.s_V0x0054249C += 1;
                }

                if (AlliedVariables.s_V0x0054249C >= AlliedVariables.s_V0x005424B0)
                {
                    AlliedVariables.s_V0x00542506 = 0;
                }
            }

            if (AlliedVariables.s_V0x00542507 != 0)
            {
                if (Math.Abs(AlliedVariables.s_V0x0054249C - AlliedVariables.s_V0x005424B0) > 0x08)
                {
                    AlliedVariables.s_V0x0054249C -= 0x07;
                }
                else if (Math.Abs(AlliedVariables.s_V0x0054249C - AlliedVariables.s_V0x005424B0) > 0x03)
                {
                    AlliedVariables.s_V0x0054249C -= 0x02;
                }
                else if (AlliedVariables.s_V0x0054249C != AlliedVariables.s_V0x005424B0)
                {
                    AlliedVariables.s_V0x0054249C -= 1;
                }

                if (AlliedVariables.s_V0x0054249C <= AlliedVariables.s_V0x005424B0)
                {
                    AlliedVariables.s_V0x00542507 = 0;
                }
            }

            AlliedVariables.s_V0x00542ECC = 0x01;
        }

        if (AlliedVariables.s_V0x00542508 != 0)
        {
            AlliedVariables.s_V0x0054250C = 0x01;

            if (Math.Abs(AlliedVariables.s_V0x005424A0 - AlliedVariables.s_V0x005424B4) > 0x12)
            {
                AlliedVariables.s_V0x005424A0 += 0x0E;
            }
            else if (AlliedVariables.s_V0x005424A0 != AlliedVariables.s_V0x005424B4)
            {
                AlliedVariables.s_V0x005424A0 += 0x04;
            }

            if (AlliedVariables.s_V0x005424A0 >= AlliedVariables.s_V0x005424B4)
            {
                AlliedVariables.s_V0x00542508 = 0;
            }

            AlliedVariables.s_V0x00542ECC = 0x01;
        }

        if (AlliedVariables.s_V0x00542509 != 0)
        {
            AlliedVariables.s_V0x0054250C = 0x01;

            if (Math.Abs(AlliedVariables.s_V0x005424A0 - AlliedVariables.s_V0x005424B4) > 0x12)
            {
                AlliedVariables.s_V0x005424A0 -= 0x0E;
            }
            else if (AlliedVariables.s_V0x005424A0 != AlliedVariables.s_V0x005424B4)
            {
                AlliedVariables.s_V0x005424A0 -= 0x04;
            }

            if (AlliedVariables.s_V0x005424A0 <= AlliedVariables.s_V0x005424B4)
            {
                AlliedVariables.s_V0x00542509 = 0;
            }

            AlliedVariables.s_V0x00542ECC = 0x01;
        }

        if (AlliedVariables.s_V0x0054250A != 0)
        {
            AlliedVariables.s_V0x0054250C = 0x01;

            if (Math.Abs(AlliedVariables.s_V0x005424A4 - AlliedVariables.s_V0x005424B8) > 0x12)
            {
                AlliedVariables.s_V0x005424A4 += 0x0E;
            }
            else if (AlliedVariables.s_V0x005424A4 != AlliedVariables.s_V0x005424B8)
            {
                AlliedVariables.s_V0x005424A4 += 0x04;
            }

            if (AlliedVariables.s_V0x005424A4 >= AlliedVariables.s_V0x005424B8)
            {
                AlliedVariables.s_V0x0054250A = 0;
            }

            AlliedVariables.s_V0x00542ECC = 0x01;
        }

        if (AlliedVariables.s_V0x0054250B != 0)
        {
            AlliedVariables.s_V0x0054250C = 0x01;

            if (Math.Abs(AlliedVariables.s_V0x005424A4 - AlliedVariables.s_V0x005424B8) > 0x12)
            {
                AlliedVariables.s_V0x005424A4 -= 0x0E;
            }
            else if (AlliedVariables.s_V0x005424A4 != AlliedVariables.s_V0x005424B8)
            {
                AlliedVariables.s_V0x005424A4 -= 0x04;
            }

            if (AlliedVariables.s_V0x005424A4 <= AlliedVariables.s_V0x005424B8)
            {
                AlliedVariables.s_V0x0054250B = 0;
            }

            AlliedVariables.s_V0x00542ECC = 0x01;
        }

        for (int ebp08 = 0; ebp08 < 0x08; ebp08++)
        {
            if (AlliedVariables.s_V0x00542E58[ebp08].m000002 <= -1)
            {
                continue;
            }

            if (AlliedVariables.s_V0x00542E58[ebp08].m000001 > 0)
            {
                AlliedVariables.s_V0x00542ECC = 0x01;
                AlliedVariables.s_V0x00542E58[ebp08].m000001--;
            }
        }

        for (int ebp08 = 0; ebp08 < 0x08; ebp08++)
        {
            if (AlliedVariables.s_V0x00542E78[ebp08] <= -1)
            {
                continue;
            }

            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp08]);

            if (AlliedVariables.s_V0x0054247C.GetText(eax1.m000004.Parameter).Length <= AlliedVariables.s_V0x00542E98[ebp08])
            {
                continue;
            }

            AlliedVariables.s_V0x00542ECC = 0x01;
            AlliedVariables.s_V0x00542E98[ebp08] += 0x03;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp08]);

            if (AlliedVariables.s_V0x0054247C.GetText(eax1.m000004.Parameter).Length >= AlliedVariables.s_V0x00542E98[ebp08])
            {
                continue;
            }

            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542E78[ebp08]);
            AlliedVariables.s_V0x00542E98[ebp08] = AlliedVariables.s_V0x0054247C.GetText(eax1.m000004.Parameter).Length;
        }

        if (AlliedVariables.s_V0x00542ECC != 0 || AlliedVariables.s_V0x00542ED0 != 0)
        {
            TBrfForm_Proc_00502624(BrfForm);
        }
    }

    // L00503314
    private static void TBrfForm_StepClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_Proc_00503298(BrfForm);

        if (BrfForm.Pause.IsChecked != true)
        {
            Buttons_TSpeedButton_SetDown(BrfForm.Play, false);
        }

        TBrfForm_Timer1Timer(BrfForm, BrfForm.Step);
        TBrfForm_Proc_00503D98(BrfForm);
        AlliedVariables.s_V0x00542ECF = 0;
        StdCtrls_TScrollBar_SetPosition(BrfForm.TimeScroll, AlliedVariables.s_V0x00542494);
        AlliedVariables.s_V0x00542ECF = 0x01;
    }

    // L00504414
    private static void TBrfForm_NextStopClick(BriefingWindow BrfForm, object? Sender)
    {
        bool ebp01 = false;
        TCommandObject eax1;

        AlliedVariables.s_V0x00542ECD = BrfForm.Timer1.GetM000048();
        TBrfForm_Proc_00503298(BrfForm);

        if (AlliedVariables.s_V0x00543CF4.Count - 1 <= AlliedVariables.s_V0x00542498)
        {
            AlliedVariables.s_V0x00542494 = 0;
        }

        while (true)
        {
            AlliedVariables.s_V0x00542498 += 1;

            if (AlliedVariables.s_V0x00543CF4.Count - 1 >= AlliedVariables.s_V0x00542498)
            {
                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                AlliedVariables.s_V0x005429B4 = eax1.m000004.Clone();
                TBrfForm_Proc_00501570(BrfForm, AlliedVariables.s_V0x005429B4);
                AlliedVariables.s_V0x00542494 = AlliedVariables.s_V0x005429B4.Time;
                TBrfForm_Proc_00504714(BrfForm);
                TBrfForm_Proc_00502624(BrfForm);
            }

            if (AlliedVariables.s_V0x005429B4.BriefingCommand == BriefingCommandEnum.PageBreak || AlliedVariables.s_V0x005429B4.BriefingCommand == BriefingCommandEnum.BriefingEnd)
            {
                break;
            }

            if (AlliedVariables.s_V0x00543CF4.Count - 1 <= AlliedVariables.s_V0x00542498)
            {
                break;
            }

            if (AlliedVariables.s_V0x00542498 == 0)
            {
                break;
            }
        }

        if (AlliedVariables.s_V0x005429B4.BriefingCommand == BriefingCommandEnum.BriefingEnd && !AlliedVariables.s_V0x00542ECD)
        {
            AlliedVariables.s_V0x00542498 = -1;
            ebp01 = true;
        }

        AlliedVariables.s_V0x00542ECE = 0x01;
        AlliedVariables.s_V0x00542ECF = 0;
        StdCtrls_TScrollBar_SetPosition(BrfForm.TimeScroll, AlliedVariables.s_V0x00542494);
        AlliedVariables.s_V0x00542ECF = 0x01;
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, AlliedVariables.s_V0x00542ECD);
        AlliedVariables.s_V0x00542494 += 1;

        if (ebp01)
        {
            TBrfForm_StopClick(BrfForm, BrfForm.Stop);
            TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, false);
        }

        Buttons_TSpeedButton_SetDown(BrfForm.Play, BrfForm.Timer1.GetM000048());
    }

    // L00503370
    private static void TBrfForm_ZoomScrollChange(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x0054249C = (int)BrfForm.ZoomScroll.Value;
        Controls_TControl_SetText(BrfForm.ZoomBarLab, "Zoom: " + AlliedVariables.s_V0x0054249C.ToString(CultureInfo.InvariantCulture));
        AlliedVariables.s_V0x005424A0 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424BC);
        AlliedVariables.s_V0x005424A4 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424C0);
        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
    }

    // L00503430
    private static void TBrfForm_XScrollChange(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x005424A0 = Integer_Negate_L0051C034((int)BrfForm.XScroll.Value);
        AlliedVariables.s_V0x005424BC = Integer_Negate_L0051C034(TBrfForm__PROC_004FB244(BrfForm, AlliedVariables.s_V0x005424A8, 0));
        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
    }

    // L00503474
    private static void TBrfForm_YScrollChange(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x005424A4 = Integer_Negate_L0051C034((int)BrfForm.YScroll.Value);
        AlliedVariables.s_V0x005424C0 = Integer_Negate_L0051C034(TBrfForm__PROC_004FB244(BrfForm, AlliedVariables.s_V0x005424AC, 0x01));
        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
    }

    // L005051B4
    private static void TBrfForm_PrevInstrClick(BriefingWindow BrfForm, object? Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();

        TBrfForm_Proc_00503298(BrfForm);

        if (AlliedVariables.s_V0x00542498 > 0)
        {
            AlliedVariables.s_V0x00542498 -= 1;
        }

        if (AlliedVariables.s_V0x00542498 < 0)
        {
            AlliedVariables.s_V0x00542498 = 0;
        }

        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        AlliedVariables.s_V0x00542494 = eax1.m000004.Time;

        if (AlliedVariables.s_V0x00542498 == 0)
        {
            TBrfForm_Proc_00502248(BrfForm);
        }
        else
        {
            TBrfForm_Proc_00504EC0(BrfForm);
        }

        TBrfForm_Proc_00502624(BrfForm);
        TBrfForm_Proc_0050508C(BrfForm);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
        Buttons_TSpeedButton_SetDown(BrfForm.Play, BrfForm.Timer1.GetM000048());
        AlliedVariables.s_V0x00542ECF = 0;
        StdCtrls_TScrollBar_SetPosition(BrfForm.TimeScroll, AlliedVariables.s_V0x00542494);
        AlliedVariables.s_V0x00542ECF = 0x01;
    }

    // L0050488C
    private static void TBrfForm_ReturnClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_StopClick(BrfForm, BrfForm.Stop);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, true);
        Buttons_TSpeedButton_SetDown(BrfForm.Play, true);
        Buttons_TSpeedButton_SetDown(BrfForm.Pause, false);
    }

    // L00501210
    private static void TBrfForm_Timer1Timer(BriefingWindow BrfForm, object? Sender)
    {
        //if (!BrfForm.Timer1.GetM000048() && BrfForm.Timer1 == Sender)
        if (!BrfForm.Timer1.GetM000048())
        {
            return;
        }

        bool bl = false;

        if (AlliedVariables.s_V0x00542ECE != 0)
        {
            if (AlliedVariables.s_V0x00542494 < 0)
            {
                AlliedVariables.s_V0x00542494 = 0;
            }

            AlliedVariables.s_V0x00542ECE = 0;
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            TBrfForm_Proc_00501570(BrfForm, eax1.m000004);
        }

        AlliedVariables.s_V0x00542494 += 1;

        if (BrfForm.Timer1.GetM000048())
        {
            AlliedVariables.s_V0x00542ECF = 0;
            StdCtrls_TScrollBar_SetPosition(BrfForm.TimeScroll, AlliedVariables.s_V0x00542494);
            AlliedVariables.s_V0x00542ECF = 0x01;
        }

        TBrfForm_Proc_00502EA0(BrfForm);

        if (AlliedVariables.s_V0x00542494 != AlliedVariables.s_V0x00542ED8)
        {
            if (AlliedVariables.s_V0x00542494 > AlliedVariables.s_V0x00542ED8)
            {
                bl = BrfForm.Timer1.GetM000048();

                TBrfForm_StopClick(BrfForm, BrfForm.Stop);

                if (bl)
                {
                    TBrfForm_PlayClick(BrfForm, BrfForm.Play);
                }
            }
            else
            {
                Controls_TControl_SetText(BrfForm.TimeLab, " Time: " + Allied_FloatToText(0x02, 0x07, 0x02, AlliedVariables.s_V0x00542494 / 25.0f));

                if (AlliedVariables.s_V0x00543CF4.Count > 0x01)
                {
                    TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 + 1);

                    if (eax1.m000004.Time <= AlliedVariables.s_V0x00542494)
                    {
                        AlliedVariables.s_V0x00542498 += 1;

                        TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                        AlliedVariables.s_V0x005429B4 = eax2.m000004.Clone();
                        TBrfForm_Proc_00501570(BrfForm, AlliedVariables.s_V0x005429B4);
                        TCommandObject eax3 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                        AlliedVariables.s_V0x00542494 = eax3.m000004.Time;

                        while (true)
                        {
                            TCommandObject eax4 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 + 1);

                            if (eax4.m000004.Time == AlliedVariables.s_V0x00542494)
                            {
                                AlliedVariables.s_V0x00542498 += 1;
                                TCommandObject eax = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
                                AlliedVariables.s_V0x005429B4 = eax.m000004.Clone();
                                TBrfForm_Proc_00501570(BrfForm, AlliedVariables.s_V0x005429B4);
                                Controls_TControl_SetText(BrfForm.Label20, "Instruction #" + (AlliedVariables.s_V0x00542498 + 1).ToString(CultureInfo.InvariantCulture) + " :");
                                TBrfForm_Proc_00506BD8(BrfForm, AlliedVariables.s_V0x005429B4);
                            }

                            if (AlliedVariables.s_V0x005429B4.Time == 0x270F)
                            {
                                bl = true;
                            }

                            if (bl)
                            {
                                break;
                            }

                            TCommandObject eax5 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 + 1);

                            if (eax5.m000004.Time != AlliedVariables.s_V0x00542494)
                            {
                                break;
                            }

                            if (AlliedVariables.s_V0x00543CF4.Count <= AlliedVariables.s_V0x00542498)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (AlliedVariables.s_V0x00542ED0 != 0 && AlliedVariables.s_V0x005429B4.BriefingCommand != BriefingCommandEnum.ChangeRegion)
        {
            TBrfForm_Proc_00502624(BrfForm);
        }

        // TBrfForm_Proc_00503298( BrfForm );
        // System_DoneExcept();
    }

    // L005060C8
    private static void TBrfForm_Timer2Timer(BriefingWindow BrfForm, object? Sender)
    {
        if (!BrfForm.Timer2.GetM000048())
        {
            return;
        }

        BrfForm.Timer2.SetM00000C(BrfForm.Timer2!.GetM00000C() + 1);
        AlliedVariables.s_V0x00543400 -= 0x0F;
        TBrfForm_Proc_005027D0(BrfForm, AlliedVariables.s_V0x0054340C);

        if (BrfForm.Timer2.GetM00000C() < 0x1E)
        {
            return;
        }

        BrfForm.Timer2.SetM00000C(0);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer2!, false);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, AlliedVariables.s_V0x0054250E);
        BrfForm.VCRcontrols.IsEnabled = true;
        AlliedVariables.s_V0x0054250C = 0x01;
        AlliedVariables.s_V0x005429B4.BriefingCommand = BriefingCommandEnum.None;
        TBrfForm_Proc_00502624(BrfForm);
    }

    // L00506894
    private static void TBrfForm_ShipBoxChange(BriefingWindow BrfForm, object? Sender)
    {
        CraftIdEnum esi = AlliedConvertShipSeqToCraftId((ShipSeqEnum)BrfForm.ShipBox.SelectedIndex);
        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        eax1.m000004.CraftId = esi;
        TBrfForm__PROC_004FCA50(BrfForm);
        TBrfForm__PROC_004FFCA0(BrfForm);
    }

    // L00504BEC
    private static void TBrfForm_CancelInsertClick(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542EC5 != 0)
        {
            Controls_TControl_SetVisible(BrfForm.ZoomScroll, false);
            Controls_TControl_SetVisible(BrfForm.ZoomBarLab, false);
            AlliedVariables.s_V0x00542EC5 = 0;
        }
        else if (AlliedVariables.s_V0x00542EC6 != 0)
        {
            Controls_TControl_SetVisible(BrfForm.XScroll, false);
            Controls_TControl_SetVisible(BrfForm.YScroll, false);
            AlliedVariables.s_V0x00542EC6 = 0;
        }
        else if (AlliedVariables.s_V0x00542ECA != 0)
        {
            AlliedVariables.s_V0x00542500 -= 1;
        }

        Controls_TControl_SetText(BrfForm.Label22, AlliedVariables.s_V0x00542500.ToString(CultureInfo.InvariantCulture));
        TBrfForm_Proc_00503A20(BrfForm, true);
        Classes_TList_Delete(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);

        if (AlliedVariables.s_V0x00542EC7 != 0 && AlliedVariables.s_V0x00542494 != 0)
        {
            Classes_TList_Delete(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 - 1);
        }

        AlliedVariables.s_V0x00542EC9 = 0;
        AlliedVariables.s_V0x00542EC8 = 0;
        AlliedVariables.s_V0x00542EC7 = 0;
        AlliedVariables.s_V0x00542ECA = 0;
        AlliedVariables.s_V0x005424A0 = AlliedVariables.s_V0x005424DC;
        AlliedVariables.s_V0x005424A4 = AlliedVariables.s_V0x005424E0;
        AlliedVariables.s_V0x0054249C = AlliedVariables.s_V0x005424E4;
        AlliedVariables.s_V0x005424BC = AlliedVariables.s_V0x005424E8;
        AlliedVariables.s_V0x005424C0 = AlliedVariables.s_V0x005424EC;
        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
        Controls_TControl_SetVisible(BrfForm.MapIndex, false);
        Controls_TControl_SetVisible(BrfForm.MapColor, false);
        TBrfForm_Proc_00503D98(BrfForm);
    }

    // L00506D3C
    private static void TBrfForm_MapIndexChange(BriefingWindow BrfForm, object? Sender)
    {
        if (AlliedVariables.s_V0x00542ECA == 0)
        {
            return;
        }

        CraftIdEnum eax1 = AlliedConvertShipSeqToCraftId((ShipSeqEnum)BrfForm.MapIndex.SelectedIndex);

        if (eax1 < CraftIdEnum._231__1_0 && BtBitString((int)eax1, AlliedVariables.s_V0x00506DB8))
        {
            BrfForm.MapColor.SelectedIndex = 0x01;
        }
        else
        {
            eax1 = AlliedConvertShipSeqToCraftId((ShipSeqEnum)BrfForm.MapIndex.SelectedIndex);

            if (eax1 < CraftIdEnum._231__1_0 && BtBitString((int)eax1, AlliedVariables.s_V0x00506DD8))
            {
                BrfForm.MapColor.SelectedIndex = 0;
            }
        }
    }

    // L005034BC
    private static void TBrfForm_DoneBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);

        if (AlliedVariables.s_V0x00542EC5 != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.X = (short)BrfForm.ZoomScroll.Value;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Y = (short)BrfForm.ZoomScroll.Value;
            TBrfForm_Proc_00503A20(BrfForm, true);
            Controls_TControl_SetVisible(BrfForm.ZoomScroll, false);
            Controls_TControl_SetVisible(BrfForm.ZoomBarLab, false);
            AlliedVariables.s_V0x00542EC5 = 0;
        }
        else if (AlliedVariables.s_V0x00542EC6 != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.X = (short)Math.Round((float)AlliedVariables.s_V0x005424F0 / (float)AlliedVariables.s_V0x0054249C * (float)BrfForm.XScroll.Value);
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Y = (short)Math.Round((float)AlliedVariables.s_V0x005424F0 / (float)AlliedVariables.s_V0x0054249C * (float)BrfForm.YScroll.Value);
            TBrfForm_Proc_00503A20(BrfForm, true);
            Controls_TControl_SetVisible(BrfForm.XScroll, false);
            Controls_TControl_SetVisible(BrfForm.YScroll, false);
            AlliedVariables.s_V0x00542EC6 = 0;
        }
        else if (AlliedVariables.s_V0x00542EC9 != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.BriefingCommand = TBrfForm_Proc_005042E8(BrfForm, AlliedVariables.s_V0x00542498 - 1);
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Parameter = (short)BrfForm.MapIndex.SelectedIndex;
            TBrfForm_Proc_00503A20(BrfForm, true);
            AlliedVariables.s_V0x00542EC9 = 0;
        }
        else if (AlliedVariables.s_V0x00542EC8 != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.BriefingCommand = TBrfForm_Proc_005041C4(BrfForm, AlliedVariables.s_V0x00542498 - 1);
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Parameter = (short)BrfForm.MapIndex.SelectedIndex;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.ColorIndex = (short)BrfForm.MapColor.SelectedIndex;
            TBrfForm_Proc_00503A20(BrfForm, true);
            AlliedVariables.s_V0x00542EC8 = 0;
            TBrfForm_Proc_00502624(BrfForm);
        }
        else if (AlliedVariables.s_V0x00542ECA != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.BriefingCommand = BriefingCommandEnum.NewIcon;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Parameter = (short)AlliedVariables.s_V0x005424C8;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            int esi = eax1.m000004.Parameter;
            AlliedVariables.s_V0x00542EDC[esi].m000008 = AlliedVariables.s_V0x00542498;
            AlliedVariables.s_V0x00542EDC[esi].m00000C = AlliedVariables.s_V0x00542498 + 1;
            AlliedVariables.s_V0x00542EDC[esi].m000006 = 0x01;
            AlliedVariables.s_V0x00542EDC[esi].CraftId = AlliedConvertShipSeqToCraftId((ShipSeqEnum)BrfForm.MapIndex.SelectedIndex);
            AlliedVariables.s_V0x00542EDC[esi].Iff = (byte)BrfForm.MapColor.SelectedIndex;
            AlliedVariables.s_V0x00542EDC[esi].X = (short)AlliedVariables.s_V0x005433F8;
            AlliedVariables.s_V0x00542EDC[esi].Y = (short)AlliedVariables.s_V0x005433FC;
            TBrfForm_Proc_0050548C(BrfForm, esi, AlliedVariables.s_V0x00542EDC[esi].CraftId, AlliedVariables.s_V0x00542EDC[esi].Rotation, AlliedVariables.s_V0x00542EDC[esi].Iff);
            AlliedVariables.s_V0x00542500 += 1;
            Controls_TControl_SetText(BrfForm.Label22, AlliedVariables.s_V0x00542500.ToString(CultureInfo.InvariantCulture));
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.CraftId = AlliedConvertShipSeqToCraftId((ShipSeqEnum)BrfForm.MapIndex.SelectedIndex);
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.ColorIndex = (short)BrfForm.MapColor.SelectedIndex;
            TCommandObject edi = new();
            edi.m000004.BriefingCommand = BriefingCommandEnum.MoveIcon;
            edi.m000004.Time = (short)AlliedVariables.s_V0x00542494;
            edi.m000004.Parameter = (short)esi;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            edi.m000004.X = eax1.m000004.X;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            edi.m000004.Y = eax1.m000004.Y;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.X = 0;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.X = 0;
            AlliedVariables.s_V0x00543CF4.Insert(AlliedVariables.s_V0x00542498 + 1, edi);
            TBrfForm_Proc_00503A20(BrfForm, true);
            AlliedVariables.s_V0x00542ECA = 0;
            AlliedVariables.s_V0x0054250C = 0x01;
            TBrfForm_Proc_00502624(BrfForm);
        }
        else if (AlliedVariables.s_V0x00542EC7 != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Parameter = (short)BrfForm.MapIndex.SelectedIndex;
            TBrfForm_Proc_00503A20(BrfForm, true);
            AlliedVariables.s_V0x00542EC7 = 0;
        }
        else if (AlliedVariables.s_V0x00542ECB != 0)
        {
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            eax1.m000004.Parameter = (short)BrfForm.MapIndex.SelectedIndex;
            TBrfForm_Proc_00503A20(BrfForm, true);
            AlliedVariables.s_V0x00542ECB = 0;
        }

        Controls_TControl_SetVisible(BrfForm.MapIndex, false);
        Controls_TControl_SetVisible(BrfForm.MapColor, false);
        TBrfForm_Proc_00503D98(BrfForm);
    }

    // L00503A20
    private static void TBrfForm_Proc_00503A20(BriefingWindow BrfForm, bool edx0)
    {
        Controls_TControl_SetVisible(BrfForm.TimeScroll, edx0);
        Controls_TControl_SetVisible(BrfForm.Memo1, edx0);
        Controls_TControl_SetVisible(BrfForm.FastPlaybackBtn, edx0);
        BrfForm.Memo1.Update();
        Controls_TControl_SetVisible(BrfForm.Memo2, edx0);
        BrfForm.Memo2.Update();
        Controls_TControl_SetVisible(BrfForm.VCRcontrols, edx0);
        BrfForm.PopupMenu1.SetM000061(edx0);
        Controls_TControl_SetVisible(BrfForm.BrfSeenBy, edx0);
        Controls_TControl_SetVisible(BrfForm.Bevel1, edx0);
        Controls_TControl_SetVisible(BrfForm.Label5, edx0);
        Controls_TControl_SetVisible(BrfForm.Label6, edx0);
        Controls_TControl_SetVisible(BrfForm.BrfTimeEd, edx0);
        Controls_TControl_SetVisible(BrfForm.SelectedIconGroup, edx0);
        BrfForm.SelectedIconGroup.Update();
        BrfForm.ControlBar1.IsEnabled = edx0;
        Controls_TControl_SetVisible(BrfForm.GroupBox4, !edx0);
        BrfForm.GroupBox4.Update();
        Controls_TControl_SetVisible(BrfForm.Guidememo, !edx0);
        BrfForm.Guidememo.Update();
    }

    // L00504584
    private static void TBrfForm_FFClick(BriefingWindow BrfForm, object? Sender)
    {
        TCommandObject eax1;

        if (BrfForm.Timer2.GetM000048())
        {
            BrfForm.Timer2.SetM00000C(0x29);
            TBrfForm_Timer2Timer(BrfForm, BrfForm.Timer2!);
        }

        AlliedVariables.s_V0x00542ECD = BrfForm.Timer1.GetM000048();
        TBrfForm_Proc_00503298(BrfForm);

        if (AlliedVariables.s_V0x00542ECE != 0)
        {
            if (AlliedVariables.s_V0x00542494 < 0)
            {
                AlliedVariables.s_V0x00542494 = 0;
            }

            AlliedVariables.s_V0x00542ECE = 0;
            eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            TBrfForm_Proc_00501570(BrfForm, eax1.m000004);
        }

        AlliedVariables.s_V0x00542EC4 = 0;

        if (AlliedVariables.s_V0x00542498 < 0)
        {
            AlliedVariables.s_V0x00542498 = 0;
        }

        if (AlliedVariables.s_V0x00543CF4.Count - 0x02 > AlliedVariables.s_V0x00542498)
        {
            AlliedVariables.s_V0x00542498 += 1;
        }

        if (!AlliedVariables.s_V0x00542ECD && AlliedVariables.s_V0x005429B4.BriefingCommand == BriefingCommandEnum.BriefingEnd)
        {
            AlliedVariables.s_V0x00542498 = 0;
        }

        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        AlliedVariables.s_V0x005429B4 = eax1.m000004.Clone();
        TBrfForm_Proc_00501570(BrfForm, AlliedVariables.s_V0x005429B4);

        if (AlliedVariables.s_V0x00543CF4.Count - 0x02 > AlliedVariables.s_V0x00542498)
        {
            AlliedVariables.s_V0x00542494 = AlliedVariables.s_V0x005429B4.Time;
        }
        else
        {
            TBrfForm_Proc_00502248(BrfForm);
        }

        if (!AlliedVariables.s_V0x00542ECD)
        {
            TBrfForm_Proc_00504714(BrfForm);
        }

        AlliedVariables.s_V0x00542EC4 = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
        AlliedVariables.s_V0x00542ECF = 0;
        StdCtrls_TScrollBar_SetPosition(BrfForm.TimeScroll, AlliedVariables.s_V0x00542494);
        AlliedVariables.s_V0x00542ECF = 0x01;
        TBrfForm_Proc_0050508C(BrfForm);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, AlliedVariables.s_V0x00542ECD);
        Buttons_TSpeedButton_SetDown(BrfForm.Play, AlliedVariables.s_V0x00542ECD);
    }

    // L00504714
    private static void TBrfForm_Proc_00504714(BriefingWindow BrfForm)
    {
        TBrfForm_Proc_0050508C(BrfForm);
        AlliedVariables.s_V0x0054250C = 0x01;

        if (AlliedVariables.s_V0x00542508 != 0 || AlliedVariables.s_V0x00542509 != 0)
        {
            AlliedVariables.s_V0x005424A0 = AlliedVariables.s_V0x005424B4;
        }

        AlliedVariables.s_V0x00542508 = 0;
        AlliedVariables.s_V0x00542509 = 0;

        if (AlliedVariables.s_V0x0054250A != 0 || AlliedVariables.s_V0x0054250B != 0)
        {
            AlliedVariables.s_V0x005424A4 = AlliedVariables.s_V0x005424B8;
        }

        AlliedVariables.s_V0x0054250A = 0;
        AlliedVariables.s_V0x0054250B = 0;
        AlliedVariables.s_V0x0054249C = AlliedVariables.s_V0x005424B0;

        if (AlliedVariables.s_V0x00542506 != 0 || AlliedVariables.s_V0x00542507 != 0)
        {
            AlliedVariables.s_V0x005424A0 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424BC);
            AlliedVariables.s_V0x005424A4 = TBrfForm_Proc_004FB220(BrfForm, AlliedVariables.s_V0x005424C0);
            AlliedVariables.s_V0x00542506 = 0;
            AlliedVariables.s_V0x00542507 = 0;
        }

        for (int ebp04 = 0; ebp04 < 0x08; ebp04++)
        {
            int ebx = AlliedVariables.s_V0x00542E78[ebp04];

            if (ebx > -1)
            {
                TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);
                AlliedVariables.s_V0x00542E98[ebp04] = AlliedVariables.s_V0x0054247C.GetText(eax1.m000004.Parameter).Length;
            }

            if (AlliedVariables.s_V0x00542E58[ebp04].m000002 != -1)
            {
                AlliedVariables.s_V0x00542E58[ebp04].m000001 = 0;
            }
        }
    }

    // L00504D88
    private static void TBrfForm_TimeScrollChange(BriefingWindow BrfForm, object? Sender)
    {
        TCommandObject eax1;

        if (!BrfForm.Timer1.GetM000048() && AlliedVariables.s_V0x00542ECF != 0)
        {
            AlliedVariables.s_V0x00542ECF = 0;

            int esi = (int)BrfForm.TimeScroll.Value;

            if (esi >= AlliedVariables.s_V0x00542494)
            {
                AlliedVariables.s_V0x00542ED0 = 0;

                while (true)
                {
                    TBrfForm_Timer1Timer(BrfForm, BrfForm.TimeScroll);

                    if (esi == AlliedVariables.s_V0x00542494 || AlliedVariables.s_V0x00542494 == AlliedVariables.s_V0x00542ED8)
                    {
                        break;
                    }
                }

                AlliedVariables.s_V0x00542ED0 = 0x01;
                TBrfForm_Proc_00502624(BrfForm);
                AlliedVariables.s_V0x00542ECF = 0x01;
            }
            else if (esi <= 0 || AlliedVariables.s_V0x00542498 <= 0)
            {
                TBrfForm_Proc_00502248(BrfForm);
                AlliedVariables.s_V0x00542ECF = 0x01;
            }
            else
            {
                AlliedVariables.s_V0x00542ED0 = 0;

                if (AlliedVariables.s_V0x00542498 >= 0x02)
                {
                    while (true)
                    {
                        AlliedVariables.s_V0x00542498 -= 1;

                        eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 - 1);

                        if (esi > eax1.m000004.Time || AlliedVariables.s_V0x00542498 <= 0)
                        {
                            break;
                        }
                    }
                }

                eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498 - 1);
                AlliedVariables.s_V0x00542494 = eax1.m000004.Time;
                TBrfForm_Proc_00504EC0(BrfForm);

                while (true)
                {
                    TBrfForm_Timer1Timer(BrfForm, BrfForm.TimeScroll);

                    if (esi == AlliedVariables.s_V0x00542494 || AlliedVariables.s_V0x00542ED8 - 1 == AlliedVariables.s_V0x00542494)
                    {
                        break;
                    }
                }

                AlliedVariables.s_V0x00542ECF = 0x01;
                AlliedVariables.s_V0x00542ED0 = 0x01;
                TBrfForm_Proc_00502624(BrfForm);
            }
        }

        TBrfForm_Proc_0050508C(BrfForm);
    }

    // L00504EC0
    private static void TBrfForm_Proc_00504EC0(BriefingWindow BrfForm)
    {
        TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        TCommand ebp4AC = eax1.m000004.Clone();
        AlliedVariables.s_V0x005424A0 = ebp4AC.m000014;
        AlliedVariables.s_V0x005424A4 = ebp4AC.m000018;

        if (ebp4AC.m00001C != 0)
        {
            AlliedVariables.s_V0x005424BC = ebp4AC.m00001C;
        }

        if (ebp4AC.m000020 != 0)
        {
            AlliedVariables.s_V0x005424C0 = ebp4AC.m000020;
        }

        if (ebp4AC.m000024 != 0)
        {
            AlliedVariables.s_V0x0054249C = ebp4AC.m000024;
        }

        AlliedVariables.s_V0x005424B4 = ebp4AC.m000028;
        AlliedVariables.s_V0x005424B8 = ebp4AC.m00002C;
        AlliedVariables.s_V0x005424B0 = ebp4AC.m000030;
        AlliedVariables.s_V0x00542506 = ebp4AC.m000034;
        AlliedVariables.s_V0x00542507 = ebp4AC.m000035;
        AlliedVariables.s_V0x00542508 = ebp4AC.m000036;
        AlliedVariables.s_V0x00542509 = ebp4AC.m000038;
        AlliedVariables.s_V0x0054250A = ebp4AC.m000037;
        AlliedVariables.s_V0x0054250B = ebp4AC.m000039;
        for (int i = 0; i < AlliedVariables.s_V0x00542E58.Length; i++)
        {
            AlliedVariables.s_V0x00542E58[i] = ebp4AC.m000438[i].Clone();
        }
        AlliedVariables.s_V0x00542E78 = (int[])ebp4AC.m000458.Clone();
        AlliedVariables.s_V0x00542E98 = (int[])ebp4AC.m000478.Clone();
        AlliedVariables.s_V0x00542ED3 = ebp4AC.m00049C;
        AlliedVariables.s_V0x00542ED4 = ebp4AC.m0004A0;

        if (AlliedVariables.s_V0x00542500 > 0x32)
        {
            AlliedVariables.s_V0x00542500 = 0x32;
        }

        int edi = AlliedVariables.s_V0x00542500 + 1;

        for (int ebp08 = 0; ebp08 < edi; ebp08++)
        {
            S0x00542EDC esi = AlliedVariables.s_V0x00542EDC[ebp08];
            S0x00542EDC ebx = ebp4AC.m00003C[ebp08];

            if (esi.CraftId == ebx.CraftId && esi.Rotation == ebx.Rotation)
            {
                continue;
            }

            esi.Iff = ebx.Iff;
            esi.CraftId = ebx.CraftId;
            esi.Rotation = (IconRotationEnum)0x0A;
            TBrfForm_Proc_0050548C(BrfForm, ebp08, ebx.CraftId, ebx.Rotation, ebx.Iff);
        }

        for (int i = 0; i < AlliedVariables.s_V0x00542EDC.Length; i++)
        {
            AlliedVariables.s_V0x00542EDC[i] = ebp4AC.m00003C[i].Clone();
        }
        AlliedVariables.s_V0x0054250C = 0x01;
    }

    // L0050508C
    private static void TBrfForm_Proc_0050508C(BriefingWindow BrfForm)
    {
        Controls_TControl_SetText(BrfForm.TimeLab, " Time: " + Allied_FloatToText(0x02, 0x07, 0x02, AlliedVariables.s_V0x00542494 / 25.0f));
        BrfForm.TimeLab.Update();
        Controls_TControl_SetText(BrfForm.Label20, "Instruction #" + (AlliedVariables.s_V0x00542498 + 1).ToString(CultureInfo.InvariantCulture) + " :");

        if (AlliedVariables.s_V0x00542498 > -1)
        {
            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
            TBrfForm_Proc_00506BD8(BrfForm, eax1.m000004);
        }
    }

    // L0050622C
    private static void TBrfForm_IconRightClick(BriefingWindow BrfForm, Button Sender)
    {
        TBrfForm_Proc_00506238(BrfForm, (BrfIconRightEnum)Convert.ToInt32(Sender.Tag));
    }

    // L00506238
    private static void TBrfForm_Proc_00506238(BriefingWindow BrfForm, BrfIconRightEnum edx0)
    {
        int ebp0C = Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin);

        if (BrfForm.Timer1.GetM000048())
        {
            TBrfForm_Proc_00503298(BrfForm);
        }

        if (AlliedVariables.s_V0x00542494 == 0)
        {
            TBrfForm_StepClick(BrfForm, BrfForm.Step);
            AlliedVariables.s_V0x00542494 = 0x01;
        }

        AlliedVariables.s_V0x005424DC = AlliedVariables.s_V0x005424A0;
        AlliedVariables.s_V0x005424E0 = AlliedVariables.s_V0x005424A4;
        AlliedVariables.s_V0x005424E4 = AlliedVariables.s_V0x0054249C;
        AlliedVariables.s_V0x005424E8 = AlliedVariables.s_V0x005424BC;
        AlliedVariables.s_V0x005424EC = AlliedVariables.s_V0x005424C0;

        TCommandObject ebp08 = new();
        ebp08.m000004.BriefingCommand = BriefingCommandEnum.MoveIcon;

        int ebp10 = 0;
        int ebp14 = 0;
        int eax1 = (int)Math.Round(Math.Sqrt(ebp0C * ebp0C / 2.0f));

        switch (edx0)
        {
            case (BrfIconRightEnum)0x01:
                ebp14 = -ebp0C;
                break;

            case (BrfIconRightEnum)0x02:
                ebp10 = eax1;
                ebp14 = -eax1;
                break;

            case (BrfIconRightEnum)0x03:
                ebp10 = ebp0C;
                break;

            case (BrfIconRightEnum)0x05:
                ebp10 = eax1;
                ebp14 = eax1;
                break;

            case (BrfIconRightEnum)0x06:
                ebp14 = ebp0C;
                break;

            case (BrfIconRightEnum)0x07:
                ebp10 = -eax1;
                ebp14 = eax1;
                break;

            case (BrfIconRightEnum)0x09:
                ebp10 = -ebp0C;
                break;

            case (BrfIconRightEnum)0x0B:
                ebp10 = -eax1;
                ebp14 = -eax1;
                break;
        }

        AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].X += (short)ebp10;
        AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].Y += (short)ebp14;
        ebp08.m000004.X = AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].X;
        ebp08.m000004.Y = AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].Y;
        AlliedVariables.s_V0x00542EDC[AlliedVariables.s_V0x005424C8].m000008 = AlliedVariables.s_V0x00542498;
        ebp08.m000004.Parameter = (short)AlliedVariables.s_V0x005424C8;
        ebp08.m000004.Time = (short)AlliedVariables.s_V0x00542494;
        AlliedVariables.s_V0x00542498 += 1;

        if (AlliedVariables.s_V0x00542494 != 0)
        {
            AlliedVariables.s_V0x00543CF4.Insert(AlliedVariables.s_V0x00542498, ebp08);
        }
        else
        {
            AlliedVariables.s_V0x00543CF4.Insert(TBrfForm_Proc_00506B3C(BrfForm, 0), ebp08);
        }

        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
        TBrfForm__PROC_004FFCA0(BrfForm);
        TCommandObject eax2 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, AlliedVariables.s_V0x00542498);
        eax2.m000004.m000014 = AlliedVariables.s_V0x005424A0;
        eax2.m000004.m000018 = AlliedVariables.s_V0x005424A4;
        eax2.m000004.m00001C = AlliedVariables.s_V0x005424BC;
        eax2.m000004.m000020 = AlliedVariables.s_V0x005424C0;
        eax2.m000004.m000024 = AlliedVariables.s_V0x0054249C;
        eax2.m000004.m000028 = AlliedVariables.s_V0x005424B4;
        eax2.m000004.m00002C = AlliedVariables.s_V0x005424B8;
        eax2.m000004.m000030 = AlliedVariables.s_V0x005424B0;
        eax2.m000004.m000034 = AlliedVariables.s_V0x00542506;
        eax2.m000004.m000035 = AlliedVariables.s_V0x00542507;
        eax2.m000004.m000036 = AlliedVariables.s_V0x00542508;
        eax2.m000004.m000037 = AlliedVariables.s_V0x0054250A;
        eax2.m000004.m000038 = AlliedVariables.s_V0x00542509;
        eax2.m000004.m000039 = AlliedVariables.s_V0x0054250B;
        for (int i = 0; i < eax2.m000004.m000438.Length; i++)
        {
            eax2.m000004.m000438[i] = AlliedVariables.s_V0x00542E58[i].Clone();
        }
        eax2.m000004.m000458 = (int[])AlliedVariables.s_V0x00542E78.Clone();
        eax2.m000004.m000478 = (int[])AlliedVariables.s_V0x00542E98.Clone();
        eax2.m000004.m000498 = AlliedVariables.s_V0x005424F8;
        eax2.m000004.m00049C = AlliedVariables.s_V0x00542ED3;
        eax2.m000004.m0004A0 = AlliedVariables.s_V0x00542ED4;
        for (int i = 0; i < eax2.m000004.m00003C.Length; i++)
        {
            eax2.m000004.m00003C[i] = AlliedVariables.s_V0x00542EDC[i].Clone();
        }
        Buttons_TSpeedButton_SetDown(BrfForm.Play, false);
        TBrfForm__PROC_004FCA50(BrfForm);
        TBrfForm_StepClick(BrfForm, BrfForm.Step);

        if (AlliedVariables.s_V0x00542494 == 0)
        {
            AlliedVariables.s_V0x00542494 = 0x01;
        }

        if (BrfForm.Acc.IsChecked == true)
        {
            if (Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) < 0x3D)
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 1.2));
            }
            else
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 1.13));
            }
        }
        else if (BrfForm.Acc2.IsChecked == true)
        {
            if (Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) < 0x32)
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 1.2));
            }
            else
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 1.3));
            }
        }
        else if (BrfForm.Dee.IsChecked == true)
        {
            if (Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) > 0x64)
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 0.9));
            }
            else
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 0.85));
            }
        }
        else if (BrfForm.Dee2.IsChecked == true)
        {
            if (Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) > 0x64)
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 0.75f));
            }
            else
            {
                Spin_TSpinEdit_SetValue(BrfForm.SpeedSpin, (int)Math.Round(Spin_TSpinEdit_GetValue(BrfForm.SpeedSpin) * 0.8));
            }
        }
    }

    // L00506884
    private static void TBrfForm_ShowNumsClick(BriefingWindow BrfForm, object? Sender)
    {
        AlliedVariables.s_V0x0054250C = 0x01;
        TBrfForm_Proc_00502624(BrfForm);
    }

    // L00506F70
    private static void TBrfForm_ImportBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        TBrfForm_StopClick(BrfForm, BrfForm.Stop);
        Dialogs_TOpenDialog_SetInitialDir(BrfForm.BrfOpenDialog1, AlliedVariables.s_BriefingsDirectorySetting);

        if (BrfForm.BrfOpenDialog1.ShowDialog(BrfForm) == true)
        {
            TBrfForm_Proc_005029DC(BrfForm);
            TBrfForm_Proc_005029F8(BrfForm);

            if (BrfForm.BrfOpenDialog1.FileName.ToUpperInvariant().Contains(".BRF"))
            {
                Unit_004CEE18_Proc_004D15CC(BrfForm.BrfOpenDialog1.FileName);
            }
            else
            {
                Unit_00513838_Proc_0051F778(BrfForm.BrfOpenDialog1.FileName);
            }

            TBrfForm_Proc_004FD968(BrfForm, AlliedVariables.s_V0x00543B20);
            TBrfForm_Proc_004FC44C(BrfForm, AlliedVariables.s_V0x00543B20);
            TBrfForm_StopClick(BrfForm, BrfForm.Stop);
            System_LGetDir(0, AlliedVariables.s_BriefingsDirectorySetting);
            StdCtrls_TScrollBar_SetMax(BrfForm.TimeScroll, AlliedVariables.s_V0x00542ED8 - 1);
            TBrfForm__PROC_004FFCA0(BrfForm);
        }
    }

    // L005069A8
    private static void TBrfForm_XSnapBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        switch (AlliedVariables.s_BrfFormXSnapBtnStep)
        {
            case 0x01:
                AlliedVariables.s_BrfFormXSnapBtnStep = 0x1A;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.XSnapBtn, 0x01);
                break;

            case 0x1A:
                AlliedVariables.s_BrfFormXSnapBtnStep = 0x33;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.XSnapBtn, 0x02);
                break;

            case 0x33:
                AlliedVariables.s_BrfFormXSnapBtnStep = 0x80;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.XSnapBtn, 0x03);
                break;

            case 0x80:
                AlliedVariables.s_BrfFormXSnapBtnStep = 0x01;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.XSnapBtn, 0);
                break;
        }
    }

    // L00506A2C
    private static void TBrfForm_YSnapBtnClick(BriefingWindow BrfForm, object? Sender)
    {
        switch (AlliedVariables.s_BrfFormYSnapBtnStep)
        {
            case 0x01:
                AlliedVariables.s_BrfFormYSnapBtnStep = 0x1A;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.YSnapBtn, 0x01);
                break;

            case 0x1A:
                AlliedVariables.s_BrfFormYSnapBtnStep = 0x33;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.YSnapBtn, 0x02);
                break;

            case 0x33:
                AlliedVariables.s_BrfFormYSnapBtnStep = 0x80;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.YSnapBtn, 0x03);
                break;

            case 0x80:
                AlliedVariables.s_BrfFormYSnapBtnStep = 0x01;
                ComCtrls_TToolButton_SetImageIndex(BrfForm.YSnapBtn, 0);
                break;
        }
    }

    // L00506AB0
    private static void TBrfForm_Rot0Click(BriefingWindow BrfForm, Button Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();
        AlliedVariables.s_BriefingCurrentIconRotation = (IconRotationEnum)Convert.ToInt32(Sender.Tag);
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.RotateIcon);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
    }

    // L00506AE4
    private static void TBrfForm_ClearIconClick(BriefingWindow BrfForm, object? Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.ClearIcon);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
    }

    // L00506B10
    private static void TBrfForm_IconInfoClick(BriefingWindow BrfForm, object? Sender)
    {
        bool bl = BrfForm.Timer1.GetM000048();
        TBrfForm_AddCommand(BrfForm, BriefingCommandEnum.IconInfo);
        TLMDHiTimer__PROC_004B08E4(BrfForm.Timer1!, bl);
    }

    // L00506B3C
    private static int TBrfForm_Proc_00506B3C(BriefingWindow BrfForm, int edx0)
    {
        int ebx = -1;

        while (true)
        {
            ebx++;

            TCommandObject eax1 = Classes_TList_Get(AlliedVariables.s_V0x00543CF4, ebx);

            if (edx0 < eax1.m000004.Time)
            {
                break;
            }

            // AlliedVariables.s_V0x00543CF4.Clear
            if (ebx >= AlliedVariables.s_V0x00543CF4.Count)
            {
                break;
            }
        }

        // AlliedVariables.s_V0x00543CF4.Clear
        if (ebx >= AlliedVariables.s_V0x00543CF4.Count)
        {
            ebx--;
        }

        return ebx;
    }

    // L00506DF8
    private static void TBrfForm_Proc_00506DF8(BriefingWindow BrfForm)
    {
        for (int edx = 0; edx < 0x1B58; edx++)
        {
            AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[edx] = 0;
        }

        for (int edx = 0; edx < 0xD50; edx++)
        {
            AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m0036BA[edx] = 0;
        }

        for (int edx = 0; edx < 0x19C8; edx++)
        {
            AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.m004414[edx] = 0;
        }

        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.CodeSize = 0x02;
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[0] = 0x270F;
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.m00000A[1] = 0x22;
        AlliedVariables.s_Allied_Briefing[AlliedVariables.s_V0x00543B20].BriefingData.BriefingCode.Length = 0x258;

        TBrfForm_Proc_004FC5C8(BrfForm);
    }
}
