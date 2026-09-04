using System.Windows;

namespace AlliED.Impl;

internal static class MainImpl
{
    public static bool IsMainOpened { get; private set; }

    public static void Run()
    {
        var main = Application.Current.MainWindow;
        AlliedVariables = new GlobalVariables();
        var window = CreateForm1Window(null);
        Application.Current.MainWindow = window;
        main.Close();
        window.Loaded += (s, e) => CreateAlliedWindows(window);
        window.Closed += (s, e) => CloseAlliedWindows();
        window.Show();
        SetAlliedWindowsOwner();
    }

    public static void Run(Form1Window window)
    {
        AlliedVariables = new GlobalVariables();
        CreateForm1Window(window);
        window.Loaded += (s, e) => CreateAlliedWindows(window);
        window.Closed += (s, e) => CloseAlliedWindows();
        window.Show();
        SetAlliedWindowsOwner();
    }

    private static void CreateAlliedWindows(Form1Window window)
    {
        IsMainOpened = true;
        CreateDatapadWindow();
        CreateCondToolWindow();
        CreateMapWindow();
        CreateOrderSelWindow();
    }

    private static void SetAlliedWindowsOwner()
    {
        SetAlliedWindowOwner(AlliedVariables.s_TDatapad_Instance);
        SetAlliedWindowOwner(AlliedVariables.s_TCondToolFormWindow_Instance);
        SetAlliedWindowOwner(AlliedVariables.s_TMapForm_Instance);
        SetAlliedWindowOwner(AlliedVariables.s_TOrderSel_Instance);
    }

    private static void SetAlliedWindowOwner(Window? window)
    {
        if (window is not null)
        {
            window.Owner = Application.Current.MainWindow;
        }
    }

    private static void CloseAlliedWindows()
    {
        IsMainOpened = false;
        AlliedVariables.s_TDatapad_Instance?.Close();
        AlliedVariables.s_TDatapad_Instance = null;
        AlliedVariables.s_TCondToolForm_Instance = null;
        AlliedVariables.s_TCondToolFormWindow_Instance?.Close();
        AlliedVariables.s_TCondToolFormWindow_Instance = null;
        AlliedVariables.s_TMapForm_Instance?.Close();
        AlliedVariables.s_TMapForm_Instance = null;
        AlliedVariables.s_TOrderSel_Instance?.Close();
        AlliedVariables.s_TOrderSel_Instance = null;
    }

    public static Form1Window CreateForm1Window(Form1Window? window)
    {
        window ??= new Form1Window();
        AlliedVariables.s_AlliedForm1Window = window;
        Form1WindowImpl.Register(window);
        return window;
    }

    public static AboutBox CreateAboutBox()
    {
        var window = new AboutBox();
        AboutBoxImpl.Register(window);
        return window;
    }

    public static BackdropBox CreateBackdropBox()
    {
        var window = new BackdropBox();
        BackdropBoxImpl.Register(window);
        return window;
    }

    public static BriefingWindow CreateBriefingWindow()
    {
        var window = new BriefingWindow();
        BriefingWindowImpl.Register(window);
        return window;
    }

    public static ChoiceBox CreateChoiceBox()
    {
        var window = new ChoiceBox();
        ChoiceBoxImpl.Register(window);
        return window;
    }

    public static ClipWindow CreateClipWindow()
    {
        var window = new ClipWindow();
        ClipWindowImpl.Register(window);
        return window;
    }

    public static CondToolWindow CreateCondToolWindow()
    {
        var window = new CondToolWindow();
        AlliedVariables.s_TCondToolFormWindow_Instance = window;
        AlliedVariables.s_TCondToolForm_Instance = (CondToolUserControl)window.Content;
        CondToolUserControlImpl.Register(AlliedVariables.s_TCondToolForm_Instance);
        CondToolWindowImpl.Register(AlliedVariables.s_TCondToolFormWindow_Instance);
        return window;
    }

    public static DatapadWindow CreateDatapadWindow()
    {
        var window = new DatapadWindow();
        AlliedVariables.s_TDatapad_Instance = window;
        DatapadWindowImpl.Register(window);
        return window;
    }

    public static ErrorBox CreateErrorBox()
    {
        var window = new ErrorBox();
        ErrorBoxImpl.Register(window);
        return window;
    }

    public static FormationBox CreateFormationBox()
    {
        var window = new FormationBox();
        FormationBoxImpl.Register(window);
        return window;
    }

    public static GoalViewWindow CreateGoalViewWindow()
    {
        var window = new GoalViewWindow();
        GoalViewWindowImpl.Register(window);
        return window;
    }

    public static HeaderBox CreateHeaderBox()
    {
        var window = new HeaderBox();
        HeaderBoxImpl.Register(window);
        return window;
    }

    public static HyperBox CreateHyperBox()
    {
        var window = new HyperBox();
        HyperBoxImpl.Register(window);
        return window;
    }

    public static ImportBox CreateImportBox()
    {
        var window = new ImportBox();
        ImportBoxImpl.Register(window);
        return window;
    }

    public static LblBox CreateLblBox()
    {
        var window = new LblBox();
        LblBoxImpl.Register(window);
        return window;
    }

    public static LibWindow CreateLibWindow()
    {
        var window = new LibWindow();
        LibWindowImpl.Register(window);
        return window;
    }

    public static LstWindow CreateLstWindow()
    {
        var window = new LstWindow();
        LstWindowImpl.Register(window);
        return window;
    }

    public static MapWindow CreateMapWindow()
    {
        var window = new MapWindow();
        AlliedVariables.s_TMapForm_Instance = window;
        MapWindowImpl.Register(window);
        return window;
    }

    public static MemoWindow CreateMemoWindow()
    {
        var window = new MemoWindow();
        MemoWindowImpl.Register(window);
        return window;
    }

    public static OrderSelWindow CreateOrderSelWindow()
    {
        var window = new OrderSelWindow();
        AlliedVariables.s_TOrderSel_Instance = window;
        OrderSelWindowImpl.Register(window);
        return window;
    }

    public static PreferencesWindow CreatePreferencesWindow()
    {
        var window = new PreferencesWindow();
        PreferencesWindowImpl.Register(window);
        return window;
    }

    public static ShipExtWindow CreateShipExtWindow()
    {
        var window = new ShipExtWindow();
        ShipExtWindowImpl.Register(window);
        return window;
    }

    public static UnknownsWindow CreateUnknownsWindow()
    {
        var window = new UnknownsWindow();
        UnknownsWindowImpl.Register(window);
        return window;
    }

    public static WavListBox CreateWavListBox()
    {
        var window = new WavListBox();
        WavListBoxImpl.Register(window);
        return window;
    }

    public static WaypointsWindow CreateWaypointsWindow()
    {
        var window = new WaypointsWindow();
        WaypointsWindowImpl.Register(window);
        return window;
    }

    public static XvTBox CreateXvTBox()
    {
        var window = new XvTBox();
        XvTBoxImpl.Register(window);
        return window;
    }
}
