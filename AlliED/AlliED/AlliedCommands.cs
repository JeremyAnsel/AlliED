using System.Windows;
using System.Windows.Input;

namespace AlliED;

public static class AlliedCommands
{
    private static RoutedUICommand CreateCommand(string text, string name, Key key, ModifierKeys modifiers)
    {
        RoutedUICommand command = new(
            text,
            name,
            typeof(AlliedCommands),
            new InputGestureCollection
            {
                new KeyGesture(key, modifiers)
            });

        return command;
    }

    private static CanExecuteRoutedEventHandler CanExecute = (s, e) =>
    {
        e.CanExecute = true;
    };

    public static void RegisterCommand(UIElement element, ICommand command, ExecutedRoutedEventHandler executed)
    {
        element.CommandBindings.Add(new CommandBinding(command, executed, CanExecute));
    }

    public static RoutedUICommand ShowViewWindows { get; } = CreateCommand("Show View Windows", "ShowViewWindows", Key.F12, ModifierKeys.Control);

    public static RoutedUICommand NewMission { get; } = CreateCommand("New Mission", "NewMission", Key.N, ModifierKeys.Control);
    public static RoutedUICommand OpenMission { get; } = CreateCommand("Open Mission", "OpenMission", Key.O, ModifierKeys.Control);
    public static RoutedUICommand SaveMission { get; } = CreateCommand("Save Mission", "SaveMission", Key.S, ModifierKeys.Control);
    public static RoutedUICommand SaveAsMission { get; } = CreateCommand("Save Mission As", "SaveAsMission", Key.A, ModifierKeys.Control);
    public static RoutedUICommand BackupMission { get; } = CreateCommand("Backup Mission", "BackupMission", Key.B, ModifierKeys.Control);
    public static RoutedUICommand BrowseMissions { get; } = CreateCommand("Browse Missions", "BrowseMissions", Key.B, ModifierKeys.Alt);
    public static RoutedUICommand LoadMissionByLst { get; } = CreateCommand("Load Mission By Lst", "LoadMissionByLst", Key.L, ModifierKeys.Alt);
    public static RoutedUICommand ImportXWing { get; } = CreateCommand("Import X-Wing", "ImportXWing", Key.None, ModifierKeys.None);
    public static RoutedUICommand ImportTieFighter { get; } = CreateCommand("Import Tie Fighter", "ImportTieFighter", Key.None, ModifierKeys.None);
    public static RoutedUICommand ImportXvT { get; } = CreateCommand("Import XvT", "ImportXvT", Key.None, ModifierKeys.None);
    public static RoutedUICommand ImportBoP { get; } = CreateCommand("Import BoP", "ImportBoP", Key.None, ModifierKeys.None);
    public static RoutedUICommand ExportXvT { get; } = CreateCommand("Export XvT", "ExportXvT", Key.None, ModifierKeys.None);
    public static RoutedUICommand ExportBoP { get; } = CreateCommand("Export BoP", "ExportBoP", Key.None, ModifierKeys.None);
    public static RoutedUICommand AddItem { get; } = CreateCommand("Add Item", "AddItem", Key.Insert, ModifierKeys.None);
    public static RoutedUICommand DeleteItem { get; } = CreateCommand("Delete Item", "DeleteItem", Key.Delete, ModifierKeys.Control);
    public static RoutedUICommand EditCopy { get; } = CreateCommand("Edit Copy", "EditCopy", Key.None, ModifierKeys.None);
    public static RoutedUICommand EditPaste { get; } = CreateCommand("Edit Paste", "EditPaste", Key.None, ModifierKeys.None);
    public static RoutedUICommand EditMoveUp { get; } = CreateCommand("Edit Move Up", "EditMoveUp", Key.None, ModifierKeys.None);
    public static RoutedUICommand EditMoveDown { get; } = CreateCommand("Edit Move Down", "EditMoveDown", Key.None, ModifierKeys.None);
    public static RoutedUICommand ClassicEditorView { get; } = CreateCommand("Classic Editor View", "ClassicEditorView", Key.F9, ModifierKeys.None);
    public static RoutedUICommand ClassicMapView { get; } = CreateCommand("Classic Map View", "ClassicMapView", Key.F10, ModifierKeys.None);
    public static RoutedUICommand ViewWaypointsEditor { get; } = CreateCommand("View Waypoints Editor", "ViewWaypointsEditor", Key.F4, ModifierKeys.None);
    public static RoutedUICommand ViewMainPages { get; } = CreateCommand("View Main Pages", "ViewMainPages", Key.F5, ModifierKeys.None);
    public static RoutedUICommand ViewDatapad { get; } = CreateCommand("View Datapad", "ViewDatapad", Key.F6, ModifierKeys.None);
    public static RoutedUICommand ViewOrderRegionSelector { get; } = CreateCommand("View Order Region Selector", "ViewOrderRegionSelector", Key.F7, ModifierKeys.None);
    public static RoutedUICommand ViewTextSections { get; } = CreateCommand("View Text Sections", "ViewTextSections", Key.F8, ModifierKeys.None);
    public static RoutedUICommand ViewWavFileManager { get; } = CreateCommand("View Wav File Manager", "ViewWavFileManager", Key.None, ModifierKeys.None);
    public static RoutedUICommand OptionsPreferences { get; } = CreateCommand("Options Preferences", "OptionsPreferences", Key.None, ModifierKeys.None);
    public static RoutedUICommand OptionsLockOrdersToRegions { get; } = CreateCommand("Options Lock Orders To Regions", "OptionsLockOrdersToRegions", Key.None, ModifierKeys.None);
    public static RoutedUICommand ErrorCheckingAutoOnSaving { get; } = CreateCommand("Options Lock Orders To Regions", "OptionsLockOrdersToRegions", Key.None, ModifierKeys.None);
    public static RoutedUICommand ErrorCheckingFilenameFormat { get; } = CreateCommand("Options Lock Orders To Regions", "OptionsLockOrdersToRegions", Key.None, ModifierKeys.None);
    public static RoutedUICommand BackingUpOverwrite { get; } = CreateCommand("Backing Up Overwrite", "BackingUpOverwrite", Key.None, ModifierKeys.None);
    public static RoutedUICommand BackingUpIncremental { get; } = CreateCommand("Backing Up Incremental", "BackingUpIncremental", Key.None, ModifierKeys.None);
    public static RoutedUICommand ToolsHyperbuoyWizard { get; } = CreateCommand("Tools Hyperbuoy Wizard", "ToolsHyperbuoyWizard", Key.None, ModifierKeys.None);
    public static RoutedUICommand ToolsAddDefaultBackdrop { get; } = CreateCommand("Tools Add Default Backdrop", "ToolsAddDefaultBackdrop", Key.None, ModifierKeys.None);
    public static RoutedUICommand GoalSummary { get; } = CreateCommand("Goal Summary", "GoalSummary", Key.G, ModifierKeys.Control);
    public static RoutedUICommand ToolsShiplistSequence { get; } = CreateCommand("Tools Shiplist Sequence", "ToolsShiplistSequence", Key.None, ModifierKeys.None);
    public static RoutedUICommand MapXYView { get; } = CreateCommand("Map XY View", "MapXYView", Key.F1, ModifierKeys.None);
    public static RoutedUICommand MapXZView { get; } = CreateCommand("Map XZ View", "MapXZView", Key.F2, ModifierKeys.None);
    public static RoutedUICommand MapYZView { get; } = CreateCommand("Map YZ View", "MapYZView", Key.F3, ModifierKeys.None);
    public static RoutedUICommand SnapXOff { get; } = CreateCommand("Snap X Off", "SnapXOff", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapX01 { get; } = CreateCommand("Snap X 0.1 km", "SnapX01", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapX02 { get; } = CreateCommand("Snap X 0.2 km", "SnapX02", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapX05 { get; } = CreateCommand("Snap X 0.5 km", "SnapX05", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapX10 { get; } = CreateCommand("Snap X 1.0 km", "SnapX10", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapYOff { get; } = CreateCommand("Snap Y Off", "SnapYOff", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapY01 { get; } = CreateCommand("Snap Y 0.1 km", "SnapY01", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapY02 { get; } = CreateCommand("Snap Y 0.2 km", "SnapY02", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapY05 { get; } = CreateCommand("Snap Y 0.5 km", "SnapY05", Key.None, ModifierKeys.None);
    public static RoutedUICommand SnapY10 { get; } = CreateCommand("Snap Y 1.0 km", "SnapY10", Key.None, ModifierKeys.None);
    public static RoutedUICommand MapAllSnapOff { get; } = CreateCommand("Map All Snap Off", "MapAllSnapOff", Key.S, ModifierKeys.Alt);
    public static RoutedUICommand MapGridOn { get; } = CreateCommand("Map Grid On", "MapGridOn", Key.G, ModifierKeys.Alt);
    public static RoutedUICommand MapNumberGrid { get; } = CreateCommand("Map Number Grid", "MapNumberGrid", Key.None, ModifierKeys.None);
    public static RoutedUICommand MapAllWaypoints { get; } = CreateCommand("Map All Waypoints", "MapAllWaypoints", Key.W, ModifierKeys.Control);
    public static RoutedUICommand MapLinkHypPoint { get; } = CreateCommand("Map Link Hyp Point", "MapLinkHypPoint", Key.H, ModifierKeys.Alt);
    public static RoutedUICommand MapShowDistances { get; } = CreateCommand("Map Show Distances", "MapShowDistances", Key.None, ModifierKeys.None);
    public static RoutedUICommand MapShowTimes { get; } = CreateCommand("Map Show Times", "MapShowTimes", Key.None, ModifierKeys.None);
    public static RoutedUICommand MapMinWireframesSizes { get; } = CreateCommand("Map Min Wireframes Sizes", "MapMinWireframesSizes", Key.Z, ModifierKeys.Alt);
    public static RoutedUICommand MapIconsOnly { get; } = CreateCommand("Map Icons Only", "MapIconsOnly", Key.I, ModifierKeys.Alt);
    public static RoutedUICommand Region1 { get; } = CreateCommand("Region 1", "Region1", Key.F1, ModifierKeys.Shift);
    public static RoutedUICommand Region2 { get; } = CreateCommand("Region 2", "Region2", Key.F2, ModifierKeys.Shift);
    public static RoutedUICommand Region3 { get; } = CreateCommand("Region 3", "Region3", Key.F3, ModifierKeys.Shift);
    public static RoutedUICommand Region4 { get; } = CreateCommand("Region 4", "Region4", Key.F4, ModifierKeys.Shift);
    public static RoutedUICommand Order1 { get; } = CreateCommand("Order 1", "Order1", Key.F1, ModifierKeys.Control);
    public static RoutedUICommand Order2 { get; } = CreateCommand("Order 2", "Order2", Key.F2, ModifierKeys.Control);
    public static RoutedUICommand Order3 { get; } = CreateCommand("Order 3", "Order3", Key.F3, ModifierKeys.Control);
    public static RoutedUICommand Order4 { get; } = CreateCommand("Order 4", "Order4", Key.F4, ModifierKeys.Control);
    public static RoutedUICommand About { get; } = CreateCommand("About", "About", Key.None, ModifierKeys.None);
}
