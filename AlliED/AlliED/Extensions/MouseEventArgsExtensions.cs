using System.Windows.Input;

namespace AlliED.Extensions;

internal static class MouseEventArgsExtensions
{
    public static TShiftState ToTShiftState(this MouseEventArgs e)
    {
        TShiftState state = TShiftState.None;

        ModifierKeys modifiers = Keyboard.Modifiers;

        if (modifiers.HasFlag(ModifierKeys.Shift))
        {
            state |= TShiftState.Shift;
        }

        if (modifiers.HasFlag(ModifierKeys.Alt))
        {
            state |= TShiftState.Alt;
        }

        if (modifiers.HasFlag(ModifierKeys.Control))
        {
            state |= TShiftState.Control;
        }

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            state |= TShiftState.LeftButton;
        }

        if (e.RightButton == MouseButtonState.Pressed)
        {
            state |= TShiftState.LeftButton;
        }

        return state;
    }
}
