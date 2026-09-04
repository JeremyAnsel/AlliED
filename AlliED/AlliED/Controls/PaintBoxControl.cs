using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AlliED.Controls;

public class PaintBoxControl : Control
{
    static PaintBoxControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PaintBoxControl), new FrameworkPropertyMetadata(typeof(PaintBoxControl)));
    }

    public delegate void PaintEventHandler(PaintBoxControl sender, BitmapContext bitmapContext);

    public event PaintEventHandler? Paint;

    private TBitmap? bitmap;

    public TBitmap? Bitmap => bitmap;

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);

        bitmap = new TBitmap((int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height);

        InvalidateVisual();
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        //Paint?.Invoke(this, drawingContext);

        if (bitmap?.Source is not null)
        {
            drawingContext.DrawImage(bitmap.Source, new Rect(0, 0, (int)this.RenderSize.Width, (int)this.RenderSize.Height));

            bitmap.Render(context =>
            {
                Paint?.Invoke(this, context);
            });
        }
    }
}
