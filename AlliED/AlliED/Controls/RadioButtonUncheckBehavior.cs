using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AlliED.Controls;

public static class RadioButtonUncheckBehavior
{
    public static readonly DependencyProperty HandleUncheckOnClickProperty =
        DependencyProperty.RegisterAttached("HandleUncheckOnClick", typeof(bool), typeof(RadioButtonUncheckBehavior), new PropertyMetadata(false));

    public static bool GetHandleUncheckOnClick(DependencyObject obj)
    {
        return (bool)obj.GetValue(HandleUncheckOnClickProperty);
    }

    public static void SetHandleUncheckOnClick(DependencyObject obj, bool value)
    {
        obj.SetValue(HandleUncheckOnClickProperty, value);
    }

    static RadioButtonUncheckBehavior()
    {
        var metadata = new FrameworkPropertyMetadata();
        metadata.CoerceValueCallback = ForceChecked;
        ToggleButton.IsCheckedProperty.OverrideMetadata(typeof(RadioButton), metadata);
    }

    private static object ForceChecked(DependencyObject sender, object value)
    {
        if (!GetHandleUncheckOnClick(sender))
        {
            return value;
        }

        var radio = (RadioButton)sender;

        if (radio.IsChecked == true && (bool)value)
        {
            return false;
        }

        return value;
    }
}
