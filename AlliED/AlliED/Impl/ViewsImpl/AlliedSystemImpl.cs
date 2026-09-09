using AlliED.Controls;
using AlliED.Extensions;
using AlliED.Helpers;
using Microsoft.Win32;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AlliED.Impl.ViewsImpl;

internal static class AlliedSystemImpl
{
    public static int TApplication_GetWidth()
    {
        double width = SystemParameters.PrimaryScreenWidth;
        return (int)width;
    }

    public static int TApplication_GetHeight()
    {
        double height = SystemParameters.PrimaryScreenHeight;
        return (int)height;
    }

    public static int Application_GetPixelsPerInch()
    {
        Window main = Application.Current.MainWindow;
        var dpiScale = VisualTreeHelper.GetDpi(main);
        return (int)dpiScale.PixelsPerInchX;
    }

    // L00512600
    public static int AlliedPixelsScaleMul(int eax0)
    {
        return (int)Math.Round(eax0 * 1.22);
    }

    // L0051F5F8
    public static int AlliedPixelsScaleDiv(int eax0)
    {
        if (Application_GetPixelsPerInch() == 0x60)
        {
            return eax0;
        }

        return (int)Math.Round(eax0 / 1.22);
    }

    public static TBitmap Graphics_TBitmap_Create()
    {
        return new TBitmap();
    }

    public static TBitmap Graphics_TBitmap_Create(int width, int height)
    {
        return new TBitmap(width, height);
    }

    public static void StdCtrls_TCustomListBox_SetItemHeight(Control control, int size)
    {
        control.FontSize = size;
    }

    public static string TApplication_GetProgramFileName()
    {
        return Assembly.GetEntryAssembly().Location;
    }

    public static string Runtime_GetDriveLetter(string filename)
    {
        return Path.GetDirectoryName(filename) + "\\";
    }

    public static void Controls_TSizeConstraints_SetConstraints(FrameworkElement control, int type, int value)
    {
        switch (type)
        {
            case 0:
                control.MaxHeight = value;
                break;

            case 1:
                control.MaxWidth = value;
                break;

            case 2:
                control.MinHeight = value;
                break;

            case 3:
                control.MinWidth = value;
                break;
        }
    }

    public static void Controls_TControl_SetLeft(UIElement control, int value)
    {
        if (control is null)
        {
            return;
        }

        Canvas.SetLeft(control, value);
    }

    public static void Controls_TControl_SetTop(UIElement control, int value)
    {
        if (control is null)
        {
            return;
        }

        Canvas.SetTop(control, value);
    }

    public static void TApplication_ShowWindow(Window window, int value)
    {
        if (value == 2)
        {
            window.WindowState = WindowState.Maximized;
        }

        window.Show();
    }

    public static void Controls_TControl_SetText(Window window, string? text)
    {
        window.Title = text ?? string.Empty;
    }

    public static string Controls_TControl_GetText(Window window)
    {
        return window.Title;
    }

    public static void Controls_TControl_SetText(Button box, string? text)
    {
        box.Content = text ?? string.Empty;
    }

    public static void Controls_TControl_SetText(ComboBox box, string? text)
    {
        box.Text = text ?? string.Empty;
    }

    public static string Controls_TControl_GetText(Xceed.Wpf.Toolkit.Primitives.InputBase control)
    {
        return control.Text;
    }

    public static string Controls_TControl_GetText(ComboBox box)
    {
        return box.Text;
    }

    public static void Controls_TControl_SetText(TextBox box, string? text)
    {
        box.Text = text ?? string.Empty;
    }

    public static string Controls_TControl_GetText(TextBox box)
    {
        return box.Text;
    }

    public static void Controls_TControl_SetText(TextBlock block, string? text)
    {
        block.Text = text ?? string.Empty;
    }

    public static string Controls_TControl_GetText(TextBlock block)
    {
        return block.Text;
    }

    public static void Controls_TControl_SetText(GroupBox block, string? text)
    {
        block.Header = text ?? string.Empty;
    }

    public static void Controls_TControl_SetText(Xceed.Wpf.Toolkit.Primitives.InputBase button, string? text)
    {
        button.Text = text ?? string.Empty;
    }

    public static int Spin_TSpinEdit_GetValue(Xceed.Wpf.Toolkit.IntegerUpDown control)
    {
        return control.Value.GetValueOrDefault();
    }

    public static void Controls_TControl_SetWidth(Control control, int width)
    {
        if (control is null)
        {
            return;
        }

        control.Width = width;
    }

    public static void Controls_TControl_SetHeight(FrameworkElement control, int height)
    {
        if (control is null)
        {
            return;
        }

        control.Height = height;
    }

    public static void Graphics_TFont_SetColor(Control control, uint color)
    {
        control.Foreground = ColorHelpers.CreateBrush(color);
    }

    public static void Graphics_TFont_SetColor(Control control, Color color)
    {
        control.Foreground = new SolidColorBrush(color);
    }

    public static void Graphics_TFont_SetColor(TextBlock control, uint color)
    {
        control.Foreground = ColorHelpers.CreateBrush(color);
    }

    public static void Graphics_TFont_SetColor(TBitmap bitmap, uint color)
    {
        bitmap.FontColor = color;
    }

    public static void Graphics_TFont_SetName(Control control, string fontName)
    {
        control.FontFamily = new FontFamily(fontName);
    }

    public static void Graphics_TFont_SetName(TBitmap control, string fontName)
    {
        control.FontFace = fontName;
    }

    public static void Graphics_TFont_SetSize(Control control, double size)
    {
        control.FontSize = size;
    }

    public static void Graphics_TFont_SetSize(TBitmap control, double size)
    {
        control.FontSize = (int)size;
    }

    public static void Graphics_TFont_SetPitch(Control control, double pitch)
    {
    }

    public static void ComCtrls_TToolButton_SetDown(ToggleButton button, bool isDown)
    {
        button.IsChecked = isDown;
    }

    public static void Buttons_TSpeedButton_SetDown(ToggleButton button, bool isDown)
    {
        button.IsChecked = isDown;
    }

    public static void Menus_TMenuItem_SetChecked(MenuItem item, bool isChecked)
    {
        item.IsChecked = isChecked;
    }

    public static void Dialogs_TOpenDialog_SetInitialDir(FileDialog dialog, string directory)
    {
        if (Directory.Exists(directory))
        {
            dialog.InitialDirectory = directory;
        }
    }

    public static T Classes_TList_Get<T>(List<T> list, int index) where T : new()
    {
        if (index == -1)
        {
            index = 0;
        }

        if (index >= list.Count)
        {
            return new();
        }

        return list[index];
    }

    public static void TApplication_L00468A40_SetActiveControl(Window window, Control control)
    {
        // todo
        window.Dispatcher.Invoke(() => control.Focus());
    }

    public static void ComCtrls_TPageControl_SetActivePage(TabControl tabControl, Control control)
    {
        tabControl.Dispatcher.Invoke(() =>
        {
            if (tabControl.SelectedItem == control)
            {
                return;
            }

            tabControl.SelectedItem = control;
        });
    }

    public static void Controls_TControl_SetVisible(UIElement control, bool visible)
    {
        control.Dispatcher.Invoke(() =>
        {
            Visibility visibility = visible ? Visibility.Visible : Visibility.Collapsed;

            if (control.Visibility == visibility)
            {
                return;
            }

            control.Visibility = visibility;
        });
    }

    public static void Buttons_L00467140_SetVisible(UIElement? window, bool visible)
    {
        if (window is null)
        {
            return;
        }

        window.Dispatcher.Invoke(() =>
        {
            window.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        });
    }

    public static int System_ParamCount()
    {
        return Environment.GetCommandLineArgs().Length - 1;
    }

    public static string System_ParamStr(int index)
    {
        string[] args = Environment.GetCommandLineArgs();

        if (index >= args.Length)
        {
            return string.Empty;
        }

        return args[index];
    }

    public static string RegistryReadKeyString(RegistryKey? key, string name, string defaultValue = "")
    {
        if (key is null)
        {
            return defaultValue;
        }

        object? value = key.GetValue(name);

        if (value is null)
        {
            return defaultValue;
        }

        return (string)value;
    }

    public static string RegistryReadKeyString(RegistryKey? key, string subkey, string name, string defaultValue = "")
    {
        if (key is null)
        {
            return defaultValue;
        }

        using RegistryKey? subKey = key.OpenSubKey(subkey);
        return RegistryReadKeyString(subKey, name, defaultValue);
    }

    public static void RegistryCreateKeyString(RegistryKey? key, string name, string value)
    {
        if (key is null)
        {
            return;
        }

        key.SetValue(name, value, RegistryValueKind.String);
    }

    public static void RegistryCreateKeyString(RegistryKey? key, string subkey, string name, string value)
    {
        if (key is null)
        {
            return;
        }

        using RegistryKey? subKey = key.CreateSubKey(subkey);
        RegistryCreateKeyString(subKey, name, value);
    }

    public static bool RegistryReadKeyBool(RegistryKey? key, string name, bool defaultValue)
    {
        if (key is null)
        {
            return defaultValue;
        }

        object? value = key.GetValue(name);

        if (value is null)
        {
            return defaultValue;
        }

        if (!int.TryParse((string)value, out int v))
        {
            return defaultValue;
        }

        return v != 0;
    }

    public static bool RegistryReadKeyBool(RegistryKey? key, string subkey, string name, bool defaultValue)
    {
        if (key is null)
        {
            return defaultValue;
        }

        using RegistryKey? subKey = key.OpenSubKey(subkey);
        return RegistryReadKeyBool(subKey, name, defaultValue);
    }

    public static void RegistryCreateKeyBool(RegistryKey? key, string name, bool value)
    {
        if (key is null)
        {
            return;
        }

        key.SetValue(name, value ? "1" : "0", RegistryValueKind.String);
    }

    public static void RegistryCreateKeyBool(RegistryKey? key, string subkey, string name, bool value)
    {
        if (key is null)
        {
            return;
        }

        using RegistryKey? subKey = key.CreateSubKey(subkey, true);
        RegistryCreateKeyBool(subKey, name, value);
    }

    public static int RegistryReadKeyInteger(RegistryKey? key, string name, int defaultValue)
    {
        if (key is null)
        {
            return defaultValue;
        }

        object? value = key.GetValue(name);

        if (value is null)
        {
            return defaultValue;
        }

        if (!int.TryParse((string)value, out int v))
        {
            return defaultValue;
        }

        return v;
    }

    public static int RegistryReadKeyInteger(RegistryKey? key, string subkey, string name, int defaultValue)
    {
        if (key is null)
        {
            return defaultValue;
        }

        using RegistryKey? subKey = key.OpenSubKey(subkey);
        return RegistryReadKeyInteger(subKey, name, defaultValue);
    }

    public static void RegistryCreateKeyInteger(RegistryKey? key, string name, int value)
    {
        if (key is null)
        {
            return;
        }

        key.SetValue(name, value.ToString(CultureInfo.InvariantCulture), RegistryValueKind.String);
    }

    public static void RegistryCreateKeyInteger(RegistryKey? key, string subkey, string name, int value)
    {
        if (key is null)
        {
            return;
        }

        using RegistryKey? subKey = key.CreateSubKey(subkey, true);
        RegistryCreateKeyInteger(subKey, name, value);
    }

    public static void StdCtrls_TCustomListBox_SetItems(ListBox box, IList collection)
    {
        box.Items.Clear();

        foreach (object item in collection)
        {
            if (item is string s)
            {
                box.AddItem(s);
            }
            else if (item is ListBoxItem listBoxItem)
            {
                box.AddItem((string)listBoxItem.Content);
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }

    public static bool StdCtrls_TCustomListBox_GetSelected(ObservableItemsControl box, int index)
    {
        CollectionSelectableTextItem item = (CollectionSelectableTextItem)box.Items[index];
        return item.IsSelected;
    }

    public static bool StdCtrls_TCustomListBox_GetSelected(ListBox box, int index)
    {
        object item = box.Items[index];
        return box.SelectedItems.Contains(item);
    }

    public static void StdCtrls_TCustomListBox_SetSelected(ListBox box, int index, bool selected)
    {
        object item = box.Items[index];

        if (item is ListBoxItem listItem)
        {
            listItem.IsSelected = selected;
            return;
        }

        if (selected)
        {
            if (!box.SelectedItems.Contains(item))
            {
                box.SelectedItems.Add(item);
            }
        }
        else
        {
            if (box.SelectedItems.Contains(item))
            {
                box.SelectedItems.Remove(item);
            }
        }
    }

    public static string System_LStrFromPCharLen(string str, int maxLength)
    {
        return str.WithMaxLength(maxLength);
    }

    public static string System_LStrFromPCharLen(byte[] array, int maxLength)
    {
        string str = Encoding.ASCII.GetString(array);
        int index = str.IndexOf('\0');

        if (index != -1)
        {
            str = str[..index];
        }

        return str.WithMaxLength(maxLength);
    }

    public static bool BtBitString(int index, BitArray array)
    {
        return array[index];
    }

    public static void BtsBitString(int index, BitArray array)
    {
        array.Set(index, !array.Get(index));
    }

    public static string Allied_UIntToHexString(int value, int precision)
    {
        return Allied_UIntToHexString((uint)value, precision);
    }

    public static string Allied_UIntToHexString(uint value, int precision)
    {
        string format = "X" + precision.ToString(CultureInfo.InvariantCulture);
        return value.ToString(format, CultureInfo.InvariantCulture);
    }

    public static void System_LStrDelete(ref string str, int edx0, int ecx0)
    {
        //int index1 = edx0;
        //str = str[..index1];

        //int index2 = edx0 + ecx0;
        //if (index2 < str.Length)
        //{
        //    str = str[index2..];
        //}

        string left = str[..Math.Min(edx0 - 1, str.Length)];
        string right = str[Math.Min(edx0 - 1 + ecx0, str.Length)..];
        str = left + right;
    }

    // L005142A8
    public static void Allied_ComboBox_SetSelectedIndex(ComboBox eax0, int edx0)
    {
        if (eax0.SelectedIndex == edx0)
        {
            return;
        }

        eax0.SelectedIndex = edx0;
    }

    public static MenuItem Menus_TMenuItem_GetItem(ItemsControl control, int index)
    {
        return (MenuItem)control.Items[index];
    }

    public static void Menus_TMenuItem_SetVisible(MenuItem menu, bool value)
    {
        menu.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
    }

    public static void Menus_TMenuItem_SetCaption(MenuItem menu, string text)
    {
        menu.Header = text;
    }

    public static void System_L004028C4_CheckError()
    {
    }

    public static TBitmap? Graphics_TBitmap_GetCanvas(TBitmap? bitmap)
    {
        return bitmap;
    }

    public static TBitmap? ExtCtrls_TImage_GetCanvas(TBitmap? bitmap)
    {
        return bitmap;
    }

    public static void Graphics_TBrush_SetColor(TBitmap bitmap, uint color)
    {
        bitmap.BrushColor = color;
    }

    public static void Graphics_TBrush_SetStyle(TBitmap bitmap, int style)
    {
        bitmap.BrushStyle = style;
    }

    public static void Graphics_TPen_SetStyle(TBitmap bitmap, int style)
    {
        bitmap.PenStyle = style;
    }

    public static void Graphics_TPen_SetColor(TBitmap bitmap, uint color)
    {
        bitmap.PenColor = color;
    }

    public static void StdCtrls_TScrollBar_SetPosition(RangeBase bar, int value)
    {
        bar.Value = value;
    }

    public static void Graphics_TCanvas_CopyRect(TBitmap dstBitmap, TRect dstRect, TBitmap srcBitmap, TRect srcRect)
    {
        dstBitmap.Render(context =>
        {
            uint filter = 0xffffff;

            if (srcBitmap.BrushColor != 0)
            {
                filter = srcBitmap.BrushColor;
            }

            context.DrawImage(dstRect, srcBitmap, srcRect, filter);
        });
    }

    public static void Graphics_TCanvas_CopyRect_Solid(TBitmap dstBitmap, TRect dstRect, TBitmap srcBitmap, TRect srcRect)
    {
        dstBitmap.Render(context =>
        {
            context.DrawImageSolid(dstRect, srcBitmap, srcRect, ColorHelpers.FromUInt32Invert(dstBitmap.BrushColor));
        });
    }

    public static void Graphics_TCanvas_FillRect(TBitmap eax0, TRect edx0)
    {
        eax0.RenderOpen();
        eax0.Render(context =>
        {
            context.WriteableBitmap.FillRectangle(edx0.Left, edx0.Top, edx0.Right, edx0.Bottom, ColorHelpers.FromUInt32(eax0.BrushColor));
        });
        eax0.RenderClose();
    }

    public static void Graphics_TCanvas_MoveTo(TBitmap eax0, int x, int y)
    {
        eax0.CurrentPositionX = x;
        eax0.CurrentPositionY = y;
    }

    public static void Graphics_TCanvas_LineTo(TBitmap eax0, int x, int y)
    {
        if (x == eax0.CurrentPositionX)
        {
            x++;
        }

        if (y == eax0.CurrentPositionY)
        {
            y++;
        }

        eax0.RenderLine(eax0.PenColor, eax0.CurrentPositionX, eax0.CurrentPositionY, x, y);

        eax0.CurrentPositionX = x;
        eax0.CurrentPositionY = y;
    }

    public static void Graphics_TCanvas_Polygon_L00426B1C(TBitmap eax0, TPoint[] points)
    {
        if (points.Length < 2)
        {
            return;
        }

        eax0.RenderOpen();

        int oldPositionX = eax0.CurrentPositionX;
        int oldPositionY = eax0.CurrentPositionY;

        Graphics_TCanvas_MoveTo(eax0, points[0].X, points[0].Y);

        for (int i = 1; i < points.Length; i++)
        {
            Graphics_TCanvas_LineTo(eax0, points[i].X, points[i].Y);
        }

        eax0.CurrentPositionX = oldPositionX;
        eax0.CurrentPositionY = oldPositionY;

        eax0.RenderClose();
    }

    public static void Graphics_TCanvas_Polygon_L00426B54(TBitmap eax0, TPoint[] points)
    {
        if (points.Length < 2)
        {
            return;
        }

        eax0.RenderOpen();

        int oldPositionX = eax0.CurrentPositionX;
        int oldPositionY = eax0.CurrentPositionY;

        Graphics_TCanvas_MoveTo(eax0, points[0].X, points[0].Y);

        for (int i = 1; i < points.Length; i++)
        {
            Graphics_TCanvas_LineTo(eax0, points[i].X, points[i].Y);
        }

        eax0.CurrentPositionX = oldPositionX;
        eax0.CurrentPositionY = oldPositionY;

        eax0.RenderClose();
    }

    public static void Graphics_TCanvas_Ellipse_L004269D0(TBitmap bitmap, int x1, int y1, int x2, int y2)
    {
        bitmap.RenderEllipse(x1, y1, x2, y2, ColorHelpers.FromUInt32Invert(bitmap.PenColor));
    }

    public static void Graphics_TCanvas_TextOut(TBitmap eax0, int x, int y, string text)
    {
        // todo
        //eax0.Render(context =>
        //{
        //    var formatedText = new FormattedText(
        //        text,
        //        CultureInfo.InvariantCulture,
        //        FlowDirection.LeftToRight,
        //        new Typeface(eax0.FontFace),
        //        eax0.FontSize * 1.5,
        //        ColorHelpers.CreateBrush(eax0.FontColor),
        //        2.0);

        //    context.WriteableBitmap.FillText(formatedText, x, y, ColorHelpers.FromUInt32(eax0.FontColor));
        //});

        eax0.RenderText(text, x, y, eax0.FontSize * 1.5f, eax0.FontColor);
    }

    public static string Allied_FloatToText(byte digits, int precision, int format, double value)
    {
        //if (value == 0.0)
        //{
        //    return digits == 0 ? "0" : ("0." + new string('0', digits));
        //}

        string f = "F" + digits.ToString(CultureInfo.InvariantCulture);
        return value.ToString(f, CultureInfo.InvariantCulture);
    }

    public static string Runtime_L0040A8D4_FloatToText(byte digits, double value)
    {
        //if (value == 0.0)
        //{
        //    return digits == 0 ? "0" : ("0." + new string('0', digits));
        //}

        digits = 2;
        string f = "F" + digits.ToString(CultureInfo.InvariantCulture);
        return value.ToString(f, CultureInfo.InvariantCulture);
    }

    public static uint Runtime_L00407BEC_Return_eax(uint eax0)
    {
        return eax0;
    }

    public static uint Runtime_L00407BF0_Div_by_0x100(uint eax0)
    {
        return eax0 / 0x100;
    }

    public static uint Runtime_L00407BF4_Div_by_0x10000(uint eax0)
    {
        return eax0 / 0x10000;
    }

    public static uint Runtime_L00407BD0_Combine(byte eax0, byte edx0, byte ecx0)
    {
        return eax0 | ((uint)edx0 << 8) | ((uint)ecx0 << 16);
    }

    public static TabItem ComCtrls_TPageControl_GetPage(TabControl tabControl, int index)
    {
        return (TabItem)tabControl.Items[index];
    }

    public static void ComCtrls_TTabSheet_SetTabVisible(Control control, bool visible)
    {
        Visibility visibility = visible ? Visibility.Visible : Visibility.Collapsed;

        if (control.Visibility == visibility)
        {
            return;
        }

        control.Visibility = visibility;
    }

    public static void Controls_TControl_SetAlign(FrameworkElement control, TAlignEnum align)
    {
        // todo
    }

    public static void TApplication_BringToFront(FrameworkElement window)
    {
        // todo
        //window.Show();
        window.BringIntoView();
    }

    public static void TApplication_BringToFront(Window window)
    {
        window.Show();
        window.Activate();
    }

    public static void TApplication_PostMessage_B021(Window window)
    {
        // todo
    }

    public static void TApplication_PostMessage_B021(Control window)
    {
        // todo
    }

    public static void Spin_TSpinEdit_SetValue(Xceed.Wpf.Toolkit.IntegerUpDown control, int value)
    {
        control.Value = value;
    }

    public static void Controls_TControl_SetColor(Control control, uint color)
    {
        control.Background = ColorHelpers.CreateBrush(color);
    }

    public static void Controls_TControl_SetColor(Control control, Color? color)
    {
        control.Background = new SolidColorBrush(color.HasValue ? color.Value : Colors.White);
    }

    public static void Menus_TMenuItem_SetEnabled(Control control, bool isEnabled)
    {
        control.IsEnabled = isEnabled;
    }

    public static int Menus_TMenuItem_GetCount(ItemsControl parent)
    {
        return parent.Items.Count;
    }

    public static void Menus_TMenuItem_Add(ItemsControl parent, Control child)
    {
        parent.Items.Add(child);
    }

    // L0051AF4C
    public static void AlliedLoadTStringsItemsFromFileAndFillComboBox(string eax0, TStrings edx0, ComboBox ecx0, bool fillComboBox)
    {
        if (File.Exists(AlliedVariables.s_AlliedDirectoryPath + "\\Data\\" + eax0 + ".txt"))
        {
            edx0.LoadFromFile(AlliedVariables.s_AlliedDirectoryPath + "\\Data\\" + eax0 + ".txt");

            if (fillComboBox)
            {
                ecx0.SetItems(edx0);
            }
        }
    }

    // L00518600
    public static string Allied_GetFileNameWithLstExtension(string eax0)
    {
        string ebp08_0 = Path.GetFileName(eax0);
        System_LStrDelete(ref ebp08_0, ebp08_0.Length - 0x03, 0x0A);
        return ebp08_0 + ".lst";
    }

    public static string System_LStrFromChar(char c)
    {
        return new string(c, 1);
    }

    // L0051213C
    public static string Unit_00511CD0_Proc_0051213C(string eax0)
    {
        return eax0.WithMaxLength(0x40);
    }

    public static void ExtCtrls_TCustomRadioGroup_SetItemIndex(GroupBox group, int index)
    {
        group.SetItemIndex(index);
    }

    public static int Menus_TMenuItem_GetMenuIndex(MenuItem child)
    {
        ItemsControl parent = (ItemsControl)child.Parent;

        for (int index = 0; index < parent.Items.Count; index++)
        {
            if (parent.Items[index] == child)
            {
                return index;
            }
        }

        return -1;
    }

    public static void TApplication_L00468A40(Window window, Visual control)
    {
        // todo
    }

    public static void ExtCtrls_TSplitter_SetBeveled(GridSplitter splitter, bool value)
    {
        splitter.IsEnabled = value;
    }

    // L005142B4
    public static int Allied_ComboBox_GetSelectedIndex(ComboBox eax0)
    {
        return eax0.SelectedIndex;
    }

    public static void Classes_TList_Delete<T>(List<T> list, int index)
    {
        list.RemoveAt(index);
    }

    public static void Classes_TList_Exchange<T>(List<T> box, int index1, int index2)
    {
        (box[index1], box[index2]) = (box[index2], box[index1]);
    }

    public static void Grids_TStringGrid_SetCells(ObservableItemsControl control, int column, int row, string text)
    {
        var itemsSource = (ObservableCollection<CollectionTextItem>)control.ItemsSource;
        int columnCount = Convert.ToInt32(control.Tag);

        if (columnCount == 0)
        {
            throw new IndexOutOfRangeException();
        }

        itemsSource[column + row * columnCount].Text = text;
    }

    public static void Grids_TStringGrid_SetCells(ListView control, int column, int row, string text)
    {
        var itemsSource = (ObservableCollection<CollectionTextItem>)control.ItemsSource;

        if (control.View is not GridView)
        {
            throw new InvalidOperationException();
        }

        if (control.Tag is null)
        {
            throw new InvalidOperationException();
        }

        int textColumn = Convert.ToInt32(control.Tag);

        if (column != textColumn)
        {
            return;
        }

        itemsSource[row].Text = text;
    }

    public static string Grids_TStringGrid_GetCells(ObservableItemsControl control, int column, int row)
    {
        var itemsSource = (ObservableCollection<CollectionTextItem>)control.ItemsSource;
        int columnCount = Convert.ToInt32(control.Tag);

        if (columnCount == 0)
        {
            throw new IndexOutOfRangeException();
        }

        return itemsSource[column + row * columnCount].Text;
    }

    public static string Grids_TStringGrid_GetCells(ListView control, int column, int row)
    {
        var itemsSource = (ObservableCollection<CollectionTextItem>)control.ItemsSource;

        if (control.View is not GridView)
        {
            throw new InvalidOperationException();
        }

        if (control.Tag is null)
        {
            throw new InvalidOperationException();
        }

        int textColumn = Convert.ToInt32(control.Tag);

        if (column != textColumn)
        {
            return string.Empty;
        }

        return itemsSource[row].Text;
    }

    public static bool StdCtrls_TCustomListBox_GetSelected(ItemsControl control, int index)
    {
        var itemsSource = (ObservableCollection<CollectionSelectableTextItem>)control.ItemsSource;
        var item = itemsSource[index];
        return item.IsSelected;
    }

    public static void StdCtrls_TCustomListBox_SetSelected(ItemsControl control, int index, bool selected)
    {
        var itemsSource = (ObservableCollection<CollectionSelectableTextItem>)control.ItemsSource;
        var item = itemsSource[index];
        item.IsSelected = selected;
    }

    public static int StrRec_try_to_int_L0051E3BC(string eax0)
    {
        if (int.TryParse(eax0, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
        {
            return value;
        }

        return 0;
    }

    public static int Allied_StrRec_to_int(string eax0)
    {
        return StrRec_try_to_int_L0051E3BC(eax0);
    }

    public static void TApplication_L00468080(Window window, int value)
    {
        // todo
    }

    public static void Controls_TWinControl_ScaleBy(Window window, int width, int height)
    {
        // todo
        window.Width = width;
        window.Height = height;
    }

    public static void TApplication_RecreateWnd(Window window, int value)
    {
        // todo
    }

    public static void Grids_TCustomGrid_SetDefaultRowHeight(ListView control, double height)
    {
        // todo
    }

    public static void Grids_TCustomGrid_SetDefaultColWidth(ListView control, double width)
    {
        // todo
    }

    public static void Grids_TCustomGrid_SetColWidths(ListView control, int column, double width)
    {
        // todo
    }

    private static Random _rand = new();

    public static void System_Randomize()
    {
        _rand = new Random();
    }

    public static int System_RandInt(int maxValue)
    {
        return _rand.Next(maxValue);
    }

    public static void StdCtrls_TScrollBar_SetMax(ScrollBar scrollbar, int max)
    {
        scrollbar.Maximum = max;
    }

    public static void TLMDHiTimer__PROC_004B08E4(TimerEx timer, bool value)
    {
        // todo
        if (value)
        {
            timer.Start();
        }
        else
        {
            timer.Stop();
        }
    }

    public static void TLMDHiTimer__PROC_004B08F4(TimerEx timer, int value)
    {
        // todo
        timer.Interval = value;
    }

    public static double Runtime_L0040AA10__StrRecToFloat(string str)
    {
        if (double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out double d))
        {
            return d;
        }

        return 0.0;
    }

    public static void System_LGetDir(int value, string str)
    {
        // todo
    }

    public static void ComCtrls_TToolButton_SetImageIndex(Button button, int index)
    {
        if (button.Content is not Image image)
        {
            throw new InvalidOperationException();
        }

        Binding imageBinding = BindingOperations.GetBinding(image, Image.SourceProperty);
        Binding newBinding = new()
        {
            Source = imageBinding.Source,
            Converter = imageBinding.Converter,
            ConverterParameter = index.ToString(CultureInfo.InvariantCulture)
        };
        BindingOperations.SetBinding(image, Image.SourceProperty, newBinding);
    }

    public static void StdCtrls_TCustomListBox_SetExtendedSelect(ListBox box, bool value)
    {
        box.SelectionMode = value ? SelectionMode.Extended : SelectionMode.Single;
    }

    // L0051E580
    public static void Allied_ShellExecute_StrRec(string str)
    {
        Process.Start(str);
    }

    // L0051E5D4
    public static void Allied_ShellExecute_pchar(string pchar)
    {
        Process.Start(pchar);
    }
}
