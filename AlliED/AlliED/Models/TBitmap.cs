using AlliED.Helpers;
using JeremyAnsel.DirectX.D2D1;
using JeremyAnsel.DirectX.DWrite;
using JeremyAnsel.DirectX.DXCommon;
using JeremyAnsel.DirectX.Dxgi;
using JeremyAnsel.DirectX.WinCodec;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AlliED.Models;

public class TBitmap
{
    private BitmapSource? _bitmap;

    public TBitmap()
    {
    }

    public TBitmap(int width, int height)
    {
        CreateWriteable(width, height);
    }

    public TBitmap(string filename)
    {
        _bitmap = new BitmapImage(new Uri(filename));
    }

    public TBitmap(BitmapFrame frame)
    {
        _bitmap = frame;
    }

    ~TBitmap()
    {
        Close();
    }

    public BitmapSource? Source => _bitmap;

    public int Width => _bitmap is null ? 0 : _bitmap.PixelWidth;

    public int Height => _bitmap is null ? 0 : _bitmap.PixelHeight;

    public int BrushStyle { get; set; }

    public uint BrushColor { get; set; }

    public uint Color { get; set; }

    public string FontFace { get; set; } = string.Empty;

    public int FontSize { get; set; }

    public uint FontColor { get; set; }

    public int PenStyle { get; set; }

    public uint PenColor { get; set; }

    public int CurrentPositionX { get; set; }

    public int CurrentPositionY { get; set; }

    public void Resize(int width, int height)
    {
        if (_bitmap is not WriteableBitmap)
        {
            throw new InvalidOperationException();
        }

        Close();
        CreateWriteable(width, height);
    }

    private void CreateWriteable(int width, int height)
    {
        if (width == 0)
        {
            width = 1;
        }

        if (height == 0)
        {
            height = 1;
        }

        _bitmap = BitmapFactory.New(width, height);
        //_buffer = new byte[width * 4 * height];
        _wicFactory = WicImagingFactory.Create();
        _d2dFactory = D2D1Factory.Create(D2D1FactoryType.SingleThreaded);
        _dwriteFactory = DWriteFactory.Create();

        _wicBitmap = _wicFactory.CreateBitmap((uint)width, (uint)height, WicGuids.GUID_WICPixelFormat32bppPBGRA, WicBitmapCreateCacheOption.WICBitmapCacheOnLoad);

        _d2dRenderTarget = _d2dFactory.CreateWicBitmapRenderTarget(_wicBitmap, new D2D1RenderTargetProperties
        {
            DpiX = 96.0f,
            DpiY = 96.0f,
            Usage = D2D1RenderTargetUsages.None,
            PixelFormat = new D2D1PixelFormat(DxgiFormat.B8G8R8A8UNorm, D2D1AlphaMode.Premultiplied),
            RenderTargetType = D2D1RenderTargetType.Default,
            MinLevel = D2D1FeatureLevel.Default
        });
    }

    public void Close()
    {
        // todo

        if (_bitmap is not WriteableBitmap)
        {
            return;
        }

        DXUtils.DisposeAndNull(ref _d2dRenderTarget);
        DXUtils.DisposeAndNull(ref _wicBitmap);
        DXUtils.DisposeAndNull(ref _wicFactory);
        DXUtils.DisposeAndNull(ref _d2dFactory);
        DXUtils.DisposeAndNull(ref _dwriteFactory);
    }

    public long State => _state;

    private long _state = 0;
    private BitmapContext? _bitmapContext;
    //private byte[]? _buffer;
    private WicImagingFactory? _wicFactory;
    private D2D1Factory? _d2dFactory;
    private DWriteFactory? _dwriteFactory;
    private WicBitmap? _wicBitmap;
    private D2D1RenderTarget? _d2dRenderTarget;

    private record TextItem(string text, int x, int y, float fontSize, uint color);
    private readonly List<TextItem> _textItems = new();

    public void RenderLine(uint color, int x0, int y0, int x1, int y1)
    {
        if (_bitmapContext is null)
        {
            throw new InvalidOperationException("Missing Render Open/Close");
        }

        color = ColorHelpers.FromUInt32Invert(color);
        WriteableBitmapExtensions.DrawLine(_bitmapContext.Value, Width, Height, x0, y0, x1, y1, (int)color);
    }

    public void RenderText(string text, int x, int y, float fontSize, uint color)
    {
        if (string.IsNullOrEmpty(text) || fontSize == 0)
        {
            return;
        }

        _textItems.Add(new(text, x, y, fontSize, color));
    }

    public void RenderOpen()
    {
        if (_bitmap is not WriteableBitmap target)
        {
            throw new InvalidOperationException();
        }

        if (_state < 0)
        {
            throw new InvalidOperationException();
        }

        if (_state == 0)
        {
            // open

            _bitmapContext = target.GetBitmapContext();
            //target.Clear(Colors.Black);
            if (PenColor == 0)
            {
                target.Clear();
            }
            _textItems.Clear();
        }

        _state++;
    }

    public void RenderClose()
    {
        if (_bitmap is not WriteableBitmap target)
        {
            throw new InvalidOperationException();
        }

        if (_state < 1)
        {
            throw new InvalidOperationException();
        }

        _state--;

        if (_state == 0)
        {
            // close

            _bitmapContext?.Dispose();
            _bitmapContext = null;

            RenderTextItems();
        }
    }

    private void RenderTextItems()
    {
        if (_textItems.Count == 0)
        {
            return;
        }

        if (_bitmap is not WriteableBitmap target)
        {
            throw new InvalidOperationException();
        }

        unsafe
        {
            using WicBitmapLock bitmapLock = _wicBitmap!.Lock(new WicRect(0, 0, Width, Height), WicBitmapLockFlags.WICBitmapLockWrite);
            bitmapLock.GetDataPointer(out uint size, out nint data);
            Buffer.MemoryCopy((void*)target.BackBuffer, (void*)data, size, size);
        }

        _d2dRenderTarget!.BeginDraw();
        DWriteTextFormat? format = null;
        float fontSize = 0;
        D2D1SolidColorBrush? brush = null;
        uint color = 0;

        foreach (var textItem in _textItems)
        {
            if (textItem.fontSize != fontSize)
            {
                fontSize = textItem.fontSize;

                DXUtils.DisposeAndNull(ref format);
                format = _dwriteFactory!.CreateTextFormat(
                    "MS Sans Serif",
                    null,
                    DWriteFontWeight.Normal,
                    DWriteFontStyle.Normal,
                    DWriteFontStretch.Normal,
                    textItem.fontSize,
                    string.Empty);
            }

            if (textItem.color != color)
            {
                color = textItem.color;

                DXUtils.DisposeAndNull(ref brush);
                brush = _d2dRenderTarget!.CreateSolidColorBrush(new D2D1ColorF(ColorHelpers.FromUInt32Invert(color)));
            }

            _d2dRenderTarget!.DrawText(textItem.text, format, new D2D1RectF(textItem.x, textItem.y, Width, Height), brush);
        }

        DXUtils.DisposeAndNull(ref format);
        DXUtils.DisposeAndNull(ref brush);
        _d2dRenderTarget!.EndDraw();

        unsafe
        {
            using WicBitmapLock bitmapLock = _wicBitmap!.Lock(new WicRect(0, 0, Width, Height), WicBitmapLockFlags.WICBitmapLockWrite);
            bitmapLock.GetDataPointer(out uint size, out nint data);
            Buffer.MemoryCopy((void*)data, (void*)target.BackBuffer, size, size);
        }
    }

    public void RenderEllipse(int x1, int y1, int x2, int y2, uint color)
    {
        int left = Math.Min(x1, x2);
        int top = Math.Min(y1, y2);
        int right = Math.Max(x1, x2);
        int bottom = Math.Max(y1, y2);
        _bitmapContext!.Value.WriteableBitmap.DrawEllipse(left, top, right, bottom, (int)color);
    }

    public void Render(Action<BitmapContext> render)
    {
        RenderOpen();
        render(_bitmapContext!.Value);
        RenderClose();
    }

    public void Save(string filename)
    {
        if (_bitmap is null)
        {
            throw new InvalidOperationException();
        }

        using var file = new FileStream(filename, FileMode.Create);
        BitmapEncoder encoder = new BmpBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(_bitmap));
        encoder.Save(file);
    }

    public void CopyDirectBitmap(TBitmap src)
    {
        Render(context =>
        {
            byte[] buffer = new byte[src.Source!.PixelWidth * 4 * src.Source!.PixelHeight];
            src.Source!.CopyPixels(buffer, src.Source!.PixelWidth * 4, 0);

            var source = BitmapFactory.New(src.Source!.PixelWidth, src.Source!.PixelHeight).FromByteArray(buffer);
            context.WriteableBitmap.Blit(new Rect(0, 0, Width, Height), source, new Rect(0, 0, src.Source!.PixelWidth, src.Source!.PixelHeight));
        });
    }

    public uint GetPixel(int x, int y)
    {
        byte[] pixel = new byte[4];

        if (_bitmap is null)
        {
            throw new InvalidOperationException();
        }

        if (_bitmap.Format.BitsPerPixel == 8)
        {
            _bitmap.CopyPixels(new Int32Rect(x, y, 1, 1), pixel, Width, 0);
            byte p = pixel[0];
            var c = _bitmap.Palette.Colors[p];
            pixel[0] = c.R;
            pixel[1] = c.G;
            pixel[2] = c.B;
            pixel[3] = 0xff;
        }
        else
        {
            _bitmap.CopyPixels(new Int32Rect(x, y, 1, 1), pixel, Width * 4, 0);
        }

        return BitConverter.ToUInt32(pixel, 0);
    }

    public void SetPixel(int x, int y, uint color)
    {
        if (!_bitmapContext.HasValue)
        {
            throw new InvalidOperationException();
        }

        var context = _bitmapContext.Value;
        context.WriteableBitmap.SetPixel(x, y, (int)color);
    }
}
