using AlliED.Helpers;
using Microsoft.Win32;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour Form1Window.xaml
/// </summary>
public partial class Form1Window : Window
{
    public Form1Window()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        Closed += (s, e) => WAVplayer.Dispose();

        Title = ProductVersionHelpers.GetNameAndVersion();

        OpenDialog1 = GetOpenDialog1();
        SaveDialog1 = GetSaveDialog1();
        OpenDialog2 = GetOpenDialog2();
        OpenDialog3 = GetOpenDialog3();
        SaveDialog2 = GetSaveDialog2();
        ExportSaveDlg = GetExportSaveDlg();

        //PaintBox1.Paint += PaintBox1_Paint;

        InitializeCommandBindings();

        //NewMission += (s, e) => MessageBox.Show(((RoutedUICommand)e.Command).Text);

        if (Application.Current.MainWindow == this)
        {
            AlliedCommands.RegisterCommand(this, AlliedCommands.ShowViewWindows, (s, e) =>
            {
                var window = new MainWindow
                {
                    Owner = this
                };
                window.ShowDialog();
            });

            MainImpl.Run(this);
        }
    }

    public SoundPlayer WAVplayer { get; } = new();

    //public event PaintBoxControl.PaintEventHandler? Paint;

    //private void PaintBox1_Paint(PaintBoxControl sender, DrawingContext drawingContext)
    //{
    //    double width = sender.ActualWidth;
    //    double height = sender.ActualHeight;

    //    drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, width, height));

    //    //Paint?.Invoke(sender, drawingContext);
    //}

    private void InitializeCommandBindings()
    {
        AlliedCommands.RegisterCommand(this, AlliedCommands.NewMission, (s, e) => NewMission?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.OpenMission, (s, e) => OpenMission?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.SaveMission, (s, e) => SaveMission?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.SaveAsMission, (s, e) => SaveAsMission?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.BackupMission, (s, e) => BackupMission?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.BrowseMissions, (s, e) => BrowseMissions?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.LoadMissionByLst, (s, e) => LoadMissionByLst?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ImportXWing, (s, e) => ImportXWing?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ImportTieFighter, (s, e) => ImportTieFighter?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ImportXvT, (s, e) => ImportXvT?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ImportBoP, (s, e) => ImportBoP?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ExportXvT, (s, e) => ExportXvT?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ExportBoP, (s, e) => ExportBoP?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.AddItem, (s, e) => AddItem?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.DeleteItem, (s, e) => DeleteItem?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.EditCopy, (s, e) => EditCopy?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.EditPaste, (s, e) => EditPaste?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.EditMoveUp, (s, e) => EditMoveUp?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.EditMoveDown, (s, e) => EditMoveDown?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ClassicEditorView, (s, e) => ClassicEditorView?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ClassicMapView, (s, e) => ClassicMapView?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ViewWaypointsEditor, ViewWaypointsEditor_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ViewMainPages, ViewMainPages_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ViewDatapad, ViewDatapad_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ViewOrderRegionSelector, ViewOrderRegionSelector_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ViewTextSections, ViewTextSections_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ViewWavFileManager, ViewWavFileManager_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.OptionsPreferences, (s, e) => OptionsPreferences?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.OptionsLockOrdersToRegions, OptionsLockOrdersToRegions_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ErrorCheckingAutoOnSaving, ErrorCheckingAutoOnSaving_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ErrorCheckingFilenameFormat, ErrorCheckingFilenameFormat_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.BackingUpOverwrite, BackingUpOverwrite_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.BackingUpIncremental, BackingUpIncremental_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.ToolsHyperbuoyWizard, (s, e) => ToolsHyperbuoyWizard?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ToolsAddDefaultBackdrop, (s, e) => ToolsAddDefaultBackdrop?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.GoalSummary, (s, e) => GoalSummary?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.ToolsShiplistSequence, (s, e) => ToolsShiplistSequence?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapXYView, MapXYView_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapXZView, MapXZView_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapYZView, MapYZView_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapXOff, SnapXOff_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapX01, SnapX01_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapX02, SnapX02_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapX05, SnapX05_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapX10, SnapX10_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapYOff, SnapYOff_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapY01, SnapY01_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapY02, SnapY02_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapY05, SnapY05_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.SnapY10, SnapY10_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapAllSnapOff, (s, e) => MapAllSnapOff?.Invoke(s, e));
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapGridOn, MapGridOn_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapNumberGrid, MapNumberGrid_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapAllWaypoints, MapAllWaypoints_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapLinkHypPoint, MapLinkHypPoint_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapShowDistances, MapShowDistances_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapShowTimes, MapShowTimes_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapMinWireframesSizes, MapMinWireframesSizes_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.MapIconsOnly, MapIconsOnly_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Region1, Region1_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Region2, Region2_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Region3, Region3_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Region4, Region4_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Order1, Order1_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Order2, Order2_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Order3, Order3_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.Order4, Order4_Executed);
        AlliedCommands.RegisterCommand(this, AlliedCommands.About, (s, e) => About?.Invoke(s, e));
    }

    public event ExecutedRoutedEventHandler? NewMission;
    public event ExecutedRoutedEventHandler? OpenMission;
    public event ExecutedRoutedEventHandler? SaveMission;
    public event ExecutedRoutedEventHandler? SaveAsMission;
    public event ExecutedRoutedEventHandler? BackupMission;
    public event ExecutedRoutedEventHandler? BrowseMissions;
    public event ExecutedRoutedEventHandler? LoadMissionByLst;
    public event ExecutedRoutedEventHandler? ImportXWing;
    public event ExecutedRoutedEventHandler? ImportTieFighter;
    public event ExecutedRoutedEventHandler? ImportXvT;
    public event ExecutedRoutedEventHandler? ImportBoP;
    public event ExecutedRoutedEventHandler? ExportXvT;
    public event ExecutedRoutedEventHandler? ExportBoP;
    public event ExecutedRoutedEventHandler? AddItem;
    public event ExecutedRoutedEventHandler? DeleteItem;
    public event ExecutedRoutedEventHandler? EditCopy;
    public event ExecutedRoutedEventHandler? EditPaste;
    public event ExecutedRoutedEventHandler? EditMoveUp;
    public event ExecutedRoutedEventHandler? EditMoveDown;
    public event ExecutedRoutedEventHandler? ClassicEditorView;
    public event ExecutedRoutedEventHandler? ClassicMapView;
    public event ExecutedRoutedEventHandler? ViewWaypointsEditor;
    public event ExecutedRoutedEventHandler? ViewMainPages;
    public event ExecutedRoutedEventHandler? ViewDatapad;
    public event ExecutedRoutedEventHandler? ViewOrderRegionSelector;
    public event ExecutedRoutedEventHandler? ViewTextSections;
    public event ExecutedRoutedEventHandler? ViewWavFileManager;
    public event ExecutedRoutedEventHandler? OptionsPreferences;
    public event ExecutedRoutedEventHandler? OptionsLockOrdersToRegions;
    public event ExecutedRoutedEventHandler? ErrorCheckingAutoOnSaving;
    public event ExecutedRoutedEventHandler? ErrorCheckingFilenameFormat;
    public event ExecutedRoutedEventHandler? BackingUpOverwrite;
    public event ExecutedRoutedEventHandler? BackingUpIncremental;
    public event ExecutedRoutedEventHandler? ToolsHyperbuoyWizard;
    public event ExecutedRoutedEventHandler? ToolsAddDefaultBackdrop;
    public event ExecutedRoutedEventHandler? GoalSummary;
    public event ExecutedRoutedEventHandler? ToolsShiplistSequence;
    public event ExecutedRoutedEventHandler? MapXYView;
    public event ExecutedRoutedEventHandler? MapXZView;
    public event ExecutedRoutedEventHandler? MapYZView;
    public event ExecutedRoutedEventHandler? SnapXOff;
    public event ExecutedRoutedEventHandler? SnapX01;
    public event ExecutedRoutedEventHandler? SnapX02;
    public event ExecutedRoutedEventHandler? SnapX05;
    public event ExecutedRoutedEventHandler? SnapX10;
    public event ExecutedRoutedEventHandler? SnapYOff;
    public event ExecutedRoutedEventHandler? SnapY01;
    public event ExecutedRoutedEventHandler? SnapY02;
    public event ExecutedRoutedEventHandler? SnapY05;
    public event ExecutedRoutedEventHandler? SnapY10;
    public event ExecutedRoutedEventHandler? MapAllSnapOff;
    public event ExecutedRoutedEventHandler? MapGridOn;
    public event ExecutedRoutedEventHandler? MapNumberGrid;
    public event ExecutedRoutedEventHandler? MapAllWaypoints;
    public event ExecutedRoutedEventHandler? MapLinkHypPoint;
    public event ExecutedRoutedEventHandler? MapShowDistances;
    public event ExecutedRoutedEventHandler? MapShowTimes;
    public event ExecutedRoutedEventHandler? MapMinWireframesSizes;
    public event ExecutedRoutedEventHandler? MapIconsOnly;
    public event ExecutedRoutedEventHandler? Region1;
    public event ExecutedRoutedEventHandler? Region2;
    public event ExecutedRoutedEventHandler? Region3;
    public event ExecutedRoutedEventHandler? Region4;
    public event ExecutedRoutedEventHandler? Order1;
    public event ExecutedRoutedEventHandler? Order2;
    public event ExecutedRoutedEventHandler? Order3;
    public event ExecutedRoutedEventHandler? Order4;
    public event ExecutedRoutedEventHandler? About;

    private void ViewWaypointsEditor_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != WPEditor1)
        {
            WPEditor1.IsChecked = !WPEditor1.IsChecked;
        }

        ViewWaypointsEditor?.Invoke(sender, e);
    }

    private void ViewMainPages_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != ShipList1)
        {
            ShipList1.IsChecked = !ShipList1.IsChecked;
        }

        ViewMainPages?.Invoke(sender, e);
    }

    private void ViewDatapad_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != Datapad1)
        {
            Datapad1.IsChecked = !Datapad1.IsChecked;
        }

        ViewDatapad?.Invoke(sender, e);
    }

    private void ViewOrderRegionSelector_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != OrderRegionSelect1)
        {
            OrderRegionSelect1.IsChecked = !OrderRegionSelect1.IsChecked;
        }

        ViewOrderRegionSelector?.Invoke(sender, e);
    }

    private void ViewTextSections_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != TextSections1)
        {
            TextSections1.IsChecked = !TextSections1.IsChecked;
        }

        ViewTextSections?.Invoke(sender, e);
    }

    private void ViewWavFileManager_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != WAVfilemanager1)
        {
            WAVfilemanager1.IsChecked = !WAVfilemanager1.IsChecked;
        }

        ViewWavFileManager?.Invoke(sender, e);
    }

    private void OptionsLockOrdersToRegions_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != Lockorders1)
        {
            Lockorders1.IsChecked = !Lockorders1.IsChecked;
        }

        OptionsLockOrdersToRegions?.Invoke(sender, e);
    }

    private void ErrorCheckingAutoOnSaving_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != ErrCheckOn1)
        {
            ErrCheckOn1.IsChecked = !ErrCheckOn1.IsChecked;
        }

        ErrorCheckingAutoOnSaving?.Invoke(sender, e);
    }

    private void ErrorCheckingFilenameFormat_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != CheckFilename1)
        {
            CheckFilename1.IsChecked = !CheckFilename1.IsChecked;
        }

        ErrorCheckingFilenameFormat?.Invoke(sender, e);
    }

    private void BackingUpOverwrite_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Overwriteprevious1.IsChecked = true;
        Incremental1.IsChecked = false;
        BackingUpOverwrite?.Invoke(sender, e);
    }

    private void BackingUpIncremental_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Overwriteprevious1.IsChecked = false;
        Incremental1.IsChecked = true;
        BackingUpIncremental?.Invoke(sender, e);
    }

    private void MapXYView_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        XYView1_Click(XYView1);
        MapXYView?.Invoke(sender, e);
    }

    private void MapXZView_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        XYView1_Click(XZView1);
        MapXZView?.Invoke(sender, e);
    }

    private void MapYZView_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        XYView1_Click(YZView1);
        MapYZView?.Invoke(sender, e);
    }

    private void XYView1_Click(MenuItem item)
    {
        XYView1.IsChecked = false;
        XZView1.IsChecked = false;
        YZView1.IsChecked = false;
        item.IsChecked = true;
    }

    private void SnapXOff_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off1_Click(Off1, XsnapOff);
        SnapXOff?.Invoke(sender, e);
    }

    private void SnapX01_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off1_Click(N01km1, Xsnap01);
        SnapX01?.Invoke(sender, e);
    }

    private void SnapX02_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off1_Click(N02km1, Xsnap02);
        SnapX02?.Invoke(sender, e);
    }

    private void SnapX05_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off1_Click(N05km1, Xsnap05);
        SnapX05?.Invoke(sender, e);
    }

    private void SnapX10_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off1_Click(N10km2, Xsnap10);
        SnapX10?.Invoke(sender, e);
    }

    private void Off1_Click(MenuItem item, MenuItem item2)
    {
        Off1.IsChecked = false;
        N01km1.IsChecked = false;
        N02km1.IsChecked = false;
        N05km1.IsChecked = false;
        N10km2.IsChecked = false;

        XsnapOff.IsChecked = false;
        Xsnap01.IsChecked = false;
        Xsnap02.IsChecked = false;
        Xsnap05.IsChecked = false;
        Xsnap10.IsChecked = false;

        item.IsChecked = true;
        item2.IsChecked = true;
    }

    private void SnapYOff_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off2_Click(Off2, YsnapOff);
        SnapYOff?.Invoke(sender, e);
    }

    private void SnapY01_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off2_Click(N01km2, Ysnap01);
        SnapY01?.Invoke(sender, e);
    }

    private void SnapY02_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off2_Click(N02km2, Ysnap02);
        SnapY02?.Invoke(sender, e);
    }

    private void SnapY05_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off2_Click(N05km2, Ysnap05);
        SnapY05?.Invoke(sender, e);
    }

    private void SnapY10_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Off2_Click(N10km1, Ysnap10);
        SnapY10?.Invoke(sender, e);
    }

    private void Off2_Click(MenuItem item, MenuItem item2)
    {
        Off2.IsChecked = false;
        N01km2.IsChecked = false;
        N02km2.IsChecked = false;
        N05km2.IsChecked = false;
        N10km1.IsChecked = false;

        YsnapOff.IsChecked = false;
        Ysnap01.IsChecked = false;
        Ysnap02.IsChecked = false;
        Ysnap05.IsChecked = false;
        Ysnap10.IsChecked = false;

        item.IsChecked = true;
        item2.IsChecked = true;
    }

    private void MapGridOn_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != ToggleGrid1)
        {
            ToggleGrid1.IsChecked = !ToggleGrid1.IsChecked;
        }

        MapGridOn?.Invoke(sender, e);
    }

    private void MapNumberGrid_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != Numbergrid1)
        {
            Numbergrid1.IsChecked = !Numbergrid1.IsChecked;
        }

        MapNumberGrid?.Invoke(sender, e);
    }

    private void MapAllWaypoints_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != Allwaypoints1)
        {
            Allwaypoints1.IsChecked = !Allwaypoints1.IsChecked;
        }

        MapAllWaypoints?.Invoke(sender, e);
    }

    private void MapLinkHypPoint_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != LinkHyppoint1)
        {
            LinkHyppoint1.IsChecked = !LinkHyppoint1.IsChecked;
        }

        MapLinkHypPoint?.Invoke(sender, e);
    }

    private void MapShowDistances_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != Showdistances)
        {
            Showdistances.IsChecked = !Showdistances.IsChecked;
        }

        MapShowDistances?.Invoke(sender, e);
    }

    private void MapShowTimes_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != Showtimes)
        {
            Showtimes.IsChecked = !Showtimes.IsChecked;
        }

        MapShowTimes?.Invoke(sender, e);
    }

    private void MapMinWireframesSizes_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != MinimumWireframesSizes1)
        {
            MinimumWireframesSizes1.IsChecked = !MinimumWireframesSizes1.IsChecked;
        }

        MapMinWireframesSizes?.Invoke(sender, e);
    }

    private void MapIconsOnly_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Source != ToggleIconsOnly1)
        {
            ToggleIconsOnly1.IsChecked = !ToggleIconsOnly1.IsChecked;
        }

        MapIconsOnly?.Invoke(sender, e);
    }

    private void Region1_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Region11_Click(Region11);
        Region1?.Invoke(sender, e);
    }

    private void Region2_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Region11_Click(Region21);
        Region2?.Invoke(sender, e);
    }

    private void Region3_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Region11_Click(Region31);
        Region3?.Invoke(sender, e);
    }

    private void Region4_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Region11_Click(Region41);
        Region4?.Invoke(sender, e);
    }

    private void Region11_Click(MenuItem item)
    {
        Region11.IsChecked = false;
        Region21.IsChecked = false;
        Region31.IsChecked = false;
        Region41.IsChecked = false;
        item.IsChecked = true;
    }

    private void Order1_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Order11_Click(Order11);
        Order1?.Invoke(sender, e);
    }

    private void Order2_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Order11_Click(Order21);
        Order2?.Invoke(sender, e);
    }

    private void Order3_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Order11_Click(Order31);
        Order3?.Invoke(sender, e);
    }

    private void Order4_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Order11_Click(Order41);
        Order4?.Invoke(sender, e);
    }

    private void Order11_Click(MenuItem item)
    {
        Order11.IsChecked = false;
        Order21.IsChecked = false;
        Order31.IsChecked = false;
        Order41.IsChecked = false;
        item.IsChecked = true;
    }

    public OpenFileDialog OpenDialog1 { get; }

    public SaveFileDialog SaveDialog1 { get; }

    public OpenFileDialog OpenDialog2 { get; }

    public OpenFileDialog OpenDialog3 { get; }

    public SaveFileDialog SaveDialog2 { get; }

    public SaveFileDialog ExportSaveDlg { get; }

    private OpenFileDialog GetOpenDialog1()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "XWA, XvT, T/F mission files (.tie)|*.tie|X-Wing mission files (.xwi)|*.xwi|All Files (*.*)|*.*"
        };

        return dialog;
    }

    private SaveFileDialog GetSaveDialog1()
    {
        var dialog = new SaveFileDialog
        {
            DefaultExt = "tie",
            Filter = "XWA Mission Files|*.tie"
        };

        return dialog;
    }

    private OpenFileDialog GetOpenDialog2()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Wave sound files (*wav)|*.wav"
        };

        return dialog;
    }

    private OpenFileDialog GetOpenDialog3()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Flight Group Library|*.fgp"
        };

        return dialog;
    }

    private SaveFileDialog GetSaveDialog2()
    {
        var dialog = new SaveFileDialog
        {
            DefaultExt = "fgp",
            Filter = "Flight Group Library|*.fgp"
        };

        return dialog;
    }

    private SaveFileDialog GetExportSaveDlg()
    {
        var dialog = new SaveFileDialog
        {
            DefaultExt = ".tie",
            Filter = "XvT, BoP mission files (*.tie)|*.tie"
        };

        return dialog;
    }

    public TBitmap Icons { get; } = new TBitmap(StringImageHelpers.CreateFrame(Form1WindowResources.Icons)!);

    private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement element)
        {
            return;
        }

        element.ContextMenu.IsOpen = true;
    }

    private void OpenSubMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem item)
        {
            return;
        }

        LoadMissionByLst?.Invoke(item, null);
    }

    private void ShowWAVman_Click(object sender, RoutedEventArgs e)
    {
        ViewWavFileManager?.Invoke(sender, null);
    }
}
