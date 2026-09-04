using AlliED.Helpers;

namespace AlliED.Impl.ViewsImpl;

internal static class CondToolWindowImpl
{
    public static void Register(CondToolWindow window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(CondToolWindow window)
    {
    }

    // L00510298
    private static void FormCreate(CondToolWindow CondToolForm)
    {
        ControlHelpers.SetParent(AlliedVariables.s_TCondToolFormWindow_Instance!, AlliedVariables.s_TDatapad_Instance!.Panel2);
        AlliedVariables.s_TCondToolForm_Instance = (CondToolUserControl)AlliedVariables.s_TDatapad_Instance!.Panel2.Content;
        Controls_TControl_SetAlign(AlliedVariables.s_TCondToolForm_Instance!, TAlignEnum.Top);
        TApplication_BringToFront(AlliedVariables.s_TCondToolForm_Instance!);
        AlliedVariables.s_TCondToolFormWindow_Instance?.Close();
        AlliedVariables.s_TCondToolFormWindow_Instance = null;
    }
}
