namespace AlliED.Impl.ViewsImpl;

internal static class ChoiceBoxImpl
{
    public static void Register(ChoiceBox window)
    {
        SetBindings(window);
        FormCreate(window);
    }

    private static void SetBindings(ChoiceBox window)
    {
    }

    // L004B119C
    private static void FormCreate(ChoiceBox window)
    {
        if (TApplication_GetWidth() == 0x280 || TApplication_GetHeight() == 0x1E0)
        {
            window.Top = window.Top - 0x0A;
        }
    }
}
