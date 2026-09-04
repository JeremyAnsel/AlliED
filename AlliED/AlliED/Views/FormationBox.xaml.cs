using AlliED.Controls;
using AlliED.Helpers;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour FormationBox.xaml
/// </summary>
public partial class FormationBox : Window
{
    public FormationBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        Image1Bitmap = new TBitmap(StringImageHelpers.CreateFrame(FormationBoxResources.Image1)!);

        PaintBox1.Paint += PaintBox1_Paint;
    }

    public TBitmap Image1Bitmap;

    private void PaintBox1_Paint(PaintBoxControl sender, BitmapContext bitmapContext)
    {
        double width = sender.ActualWidth;
        double height = sender.ActualHeight;

        bitmapContext.WriteableBitmap.FillRectangle(0, 0, (int)width, (int)height, 0);
    }

    private void PrevForm_Click(object sender, RoutedEventArgs e)
    {
        PaintBox1.InvalidateVisual();
    }

    private void NextForm_Click(object sender, RoutedEventArgs e)
    {
        PaintBox1.InvalidateVisual();
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void Button2_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }
}

