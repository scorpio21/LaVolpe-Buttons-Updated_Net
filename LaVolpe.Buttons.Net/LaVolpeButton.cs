using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LaVolpe.Buttons.Net;

// El control conserva los nombres C# para código existente, pero el PropertyGrid
// muestra los miembros canónicos definidos por lvButtons.ctl (lv_* / vb*).
internal sealed class VB6EnumConverter : EnumConverter
{
    public VB6EnumConverter() : base(typeof(Enum)) { }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
    {
        var type = context?.PropertyDescriptor?.PropertyType;
        if (type?.IsEnum != true)
            return new StandardValuesCollection(Array.Empty<object>());

        // VB6EnumConverter exposes several aliases for the same numeric value
        // (WindowsXp, XP and lv_XP). The PropertyGrid must show one item only.
        var values = Enum.GetValues(type)
            .Cast<Enum>()
            .GroupBy(value => Convert.ToInt64(value))
            .Select(group => group.First())
            .ToArray();
        return new StandardValuesCollection(values);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is Enum e)
        {
            var field = e.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.Name.StartsWith("lv_", StringComparison.Ordinal) || f.Name.StartsWith("vb", StringComparison.Ordinal))
                .FirstOrDefault(f => Convert.ToInt64(f.GetValue(null)) == Convert.ToInt64(e));
            if (field is not null) return field.Name;
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
    {
        if (value is string text && context?.PropertyDescriptor?.PropertyType is { IsEnum: true } type)
        {
            var field = type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(f => string.Equals(f.Name, text, StringComparison.Ordinal));
            if (field is not null) return field.GetValue(null);
        }
        return base.ConvertFrom(context, culture, value);
    }
}

[TypeConverter(typeof(VB6EnumConverter))] public enum ImagePlacement { LeftEdge=0, LeftOfCaption=1, RightEdge=2, RightOfCaption=3, TopCenter=4, BottomCenter=5, lv_LeftEdge=LeftEdge, lv_LeftOfCaption=LeftOfCaption, lv_RightEdge=RightEdge, lv_RightOfCaption=RightOfCaption, lv_TopCenter=TopCenter, lv_BottomCenter=BottomCenter }
[TypeConverter(typeof(VB6EnumConverter))] public enum ImageSize { Size16x16=0, Size24x24=1, Size32x32=2, FillStretch=3, FillScaleUpDown=4, lv_16x16=Size16x16, lv_24x24=Size24x24, lv_32x32=Size32x32, lv_Fill_Stretch=FillStretch, lv_Fill_ScaleUpDown=FillScaleUpDown }
[TypeConverter(typeof(VB6EnumConverter))] public enum ButtonMode { CommandButton=0, CheckBox=1, OptionButton=2, lv_CommandButton=CommandButton, lv_CheckBox=CheckBox, lv_OptionButton=OptionButton }
[TypeConverter(typeof(VB6EnumConverter))] public enum ButtonShape { Rectangular=0, LeftDiagonal=1, RightDiagonal=2, FullDiagonal=3, Round3D=4, Round3DFixed=5, RoundFlat=6, CustomFlat=7, Custom3DBorder=8, lv_Rectangular=Rectangular, lv_LeftDiagonal=LeftDiagonal, lv_RightDiagonal=RightDiagonal, lv_FullDiagonal=FullDiagonal, lv_Round3D=Round3D, lv_Round3DFixed=Round3DFixed, lv_RoundFlat=RoundFlat, lv_CustomFlat=CustomFlat, lv_Custom3DBorder=Custom3DBorder }
[TypeConverter(typeof(VB6EnumConverter))] public enum HoverColorLocks { TextAndBackColor=0, TextColorOnly=1, BackColorOnly=2, NoLocks=3, LockTextandBackColor=TextAndBackColor, LockTextColorOnly=TextColorOnly, LockBackColorOnly=BackColorOnly, lv_LockTextandBackColor=TextAndBackColor, lv_LockTextColorOnly=TextColorOnly, lv_LockBackColorOnly=BackColorOnly, lv_NoLocks=NoLocks }
[TypeConverter(typeof(VB6EnumConverter))] public enum CustomClick { Default=0, North=1, NorthEast=2, NorthWest=3, SouthEast=4, SouthWest=5, East=6, South=7, West=8, lv_cDefault=Default, lv_cNorth=North, lv_cNorthEast=NorthEast, lv_cNorthWest=NorthWest, lv_cSouthEast=SouthEast, lv_cSouthWest=SouthWest, lv_cEast=East, lv_cSouth=South, lv_cWest=West, lv_Default=Default, lv_North=North, lv_NorthEast=NorthEast, lv_NorthWest=NorthWest, lv_SouthEast=SouthEast, lv_SouthWest=SouthWest, lv_East=East, lv_South=South, lv_West=West }
[TypeConverter(typeof(VB6EnumConverter))] public enum GradientMode { None=0, LeftToRight=1, RightToLeft=2, TopToBottom=3, BottomToTop=4, NoGradient=None, Left2Right=LeftToRight, Right2Left=RightToLeft, Top2Bottom=TopToBottom, Bottom2Top=BottomToTop }
[TypeConverter(typeof(VB6EnumConverter))] public enum CaptionStyle { Default=0, Sunken=1, Raised=2, lv_Default=Default, lv_Sunken=Sunken, lv_Raised=Raised }
[Flags, TypeConverter(typeof(VB6EnumConverter))] public enum FontStyles { PlainStyle=0, Bold=2, Italic=4, Underline=8, BoldItalic=6, BoldUnderline=10, ItalicUnderline=12, BoldItalicUnderline=14, lv_PlainStyle=PlainStyle, lv_Bold=Bold, lv_Italic=Italic, lv_Underline=Underline, lv_BoldItalic=BoldItalic, lv_BoldUnderline=BoldUnderline, lv_ItalicUnderline=ItalicUnderline, lv_BoldItalicUnderline=BoldItalicUnderline }
[TypeConverter(typeof(VB6EnumConverter))] public enum BackStyle { Windows95=0, Windows31=1, WindowsXp=2, Java=3, Flat=4, Hover=5, Netscape=6, Macintosh=7, w95=Windows95, w31=Windows31, XP=WindowsXp, hover=Hover, lv_w95=Windows95, lv_w31=Windows31, lv_XP=WindowsXp, lv_Java=Java, lv_Flat=Flat, lv_hover=Hover, lv_Netscape=Netscape, lv_Macintosh=Macintosh }
[TypeConverter(typeof(VB6EnumConverter))] public enum MousePointerConstants { Default=0, Arrow=1, Crosshair=2, IBeam=3, SizeNESW=6, SizeNS=7, SizeNWSE=8, SizeWE=9, UpArrow=10, Hourglass=11, NoDrop=12, ArrowHourglass=13, ArrowQuestion=14, SizeAll=15, Custom=99, vbDefault=Default, vbArrow=Arrow, vbCrosshair=Crosshair, vbIBeam=IBeam, vbSizeNESW=SizeNESW, vbSizeNS=SizeNS, vbSizeNWSE=SizeNWSE, vbSizeWE=SizeWE, vbUpArrow=UpArrow, vbHourglass=Hourglass, vbNoDrop=NoDrop, vbArrowHourglass=ArrowHourglass, vbArrowQuestion=ArrowQuestion, vbSizeAll=SizeAll, vbCustom=Custom }
[TypeConverter(typeof(VB6EnumConverter))] public enum DragModeConstants { vbManual=0, vbAutomatic=1, Manual=vbManual, Automatic=vbAutomatic }

public delegate void LvMouseEventHandler(object sender, MouseEventArgs e);
public delegate void LvKeyEventHandler(object sender, KeyEventArgs e);

[ToolboxItem(true), DesignTimeVisible(true), DesignerCategory("Component"), DefaultEvent(nameof(Click)), DefaultProperty(nameof(Caption))]
public class LaVolpeButton : Control, IButtonControl
{
    private bool _hovered, _pressed, _value, _delayDrawing, _resetDefaultColors, _explicitBackColor;
    private string _caption = "";
    private Color _normalBack = SystemColors.Control, _hoverBack = Color.Empty, _gradientColor = SystemColors.ControlLight, _hoverFore = SystemColors.ControlText;
    private Image? _picture, _mouseIcon, _dragIcon;
    private MousePointerConstants _mousePointer = MousePointerConstants.Default;
    private ButtonMode _mode; private ButtonShape _shape; private BackStyle _style = BackStyle.Windows95; private GradientMode _gradientMode;
    private FontStyles _fontStyle;
    private ImagePlacement _pictureAlign = ImagePlacement.LeftOfCaption;
    private ImageSize _pictureSize = ImageSize.Size16x16;
    private ContentAlignment _captionAlign = ContentAlignment.MiddleCenter;
    private CaptionStyle _captionStyle = CaptionStyle.Default;
    private HoverColorLocks _hoverColorLocks = HoverColorLocks.TextAndBackColor;
    private CustomClick _customClick = CustomClick.Default;
    private bool _showFocusRect = true, _cancel, _default, _dragMode;
    private DialogResult _dialogResult = DialogResult.None;
    private Point _dragStart;
    private int _helpContextId, _index, _whatsThisHelpId;
    private string _toolTipText = "";
    private ToolTip? _toolTip;
    private Cursor? _mouseIconCursor, _dragIconCursor;
    private IntPtr _mouseIconHandle, _dragIconHandle;
    private readonly System.Windows.Forms.Timer _hoverTimer;
    private Region? _region;
    private Graphics? _hdcGraphics;
    private IntPtr _hdc;

    public event EventHandler<bool>? MouseOnButton;
    public event EventHandler? ValueChanged;
    public event EventHandler? CheckedChanged;
    public event EventHandler? SelectedChanged;
    public event LvMouseEventHandler? LvMouseDown, LvMouseUp, LvMouseMove;
    public event LvKeyEventHandler? LvKeyDown, LvKeyUp;
    public event KeyPressEventHandler? LvKeyPress;
    public event EventHandler<int>? ButtonDoubleClick;
    public event DragEventHandler? OLEDragDrop, OLEDragOver, OLEStartDrag;
    public event GiveFeedbackEventHandler? OLEGiveFeedback;
    public event EventHandler? OLECompleteDrag, OLESetData;

    public LaVolpeButton() { SetStyle(ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer|ControlStyles.ResizeRedraw|ControlStyles.Selectable,true); Size=new Size(90,33); TabStop=true; AllowDrop=true; AccessibleRole=AccessibleRole.PushButton; _hoverTimer=new System.Windows.Forms.Timer { Interval=50 }; _hoverTimer.Tick += (_,_) => TimerUpdate(0); }
    [Category("LaVolpe"), DefaultValue(""), Description("Texto visible del botón; admite && para aceleradores y || para salto de línea.")] public string Caption { get=>_caption; set{ if(_caption==(value??""))return; _caption=value??""; base.Text=_caption; if(string.IsNullOrWhiteSpace(AccessibleName))AccessibleName=_caption.Replace("&",""); InvalidateIfNeeded();} }
    // VB6: Caption es el miembro por defecto; Text debe reflejarlo para accesibilidad y diseñador.
    [AllowNull, Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text { get=>_caption; set=>Caption=value??string.Empty; }
    [Category("LaVolpe"), DefaultValue(ButtonMode.CommandButton)] public ButtonMode Mode { get=>_mode; set{ if(_mode==value)return; _mode=value; if(value==ButtonMode.CommandButton&&_value){_value=false;ValueChanged?.Invoke(this,EventArgs.Empty);} InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(ButtonShape.Rectangular), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public ButtonShape ButtonShape { get=>_shape; set{_shape=value; UpdateRegion(); InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(BackStyle.Windows95), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public BackStyle ButtonStyle { get=>_style; set{_style=value;UpdateRegion();InvalidateIfNeeded();} }
    // VB6 rechaza Value = True en modo CommandButton.
    [Category("LaVolpe"), DefaultValue(false)] public bool Value { get=>_value; set{ if(_mode==ButtonMode.CommandButton)return; if(value)SelectThis(); else SetValue(false); } }
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool Checked { get=>_value; set=>Value=value; }
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool Selected { get=>_value; set=>Value=value; }
    [Category("Appearance")]
    public override Color BackColor { get => _normalBack; set { _normalBack = value; _explicitBackColor = true; base.BackColor = value; InvalidateIfNeeded(); } }
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public Color BackColorButton { get => BackColor; set => BackColor = value; }
    [Category("LaVolpe")] public Color HoverBackColor { get=>HoverBackEffective; set{_hoverBack=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Color HoverForeColor { get=>_hoverFore; set{_hoverFore=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Color GradientColor { get=>_gradientColor; set{_gradientColor=value; InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(GradientMode.None)] public GradientMode GradientMode { get=>_gradientMode; set{_gradientMode=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Image? Picture { get=>_picture; set{_picture=value; if(value is not null&&IsCustomShape) _pictureSize=ImageSize.FillScaleUpDown; UpdateRegion(); InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(ImagePlacement.LeftOfCaption)] public ImagePlacement PictureAlign { get=>_pictureAlign; set{_pictureAlign=value;InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(ImageSize.Size16x16)] public ImageSize PictureSize { get=>_pictureSize; set{_pictureSize=value;InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(ContentAlignment.MiddleCenter)] public ContentAlignment CaptionAlign { get=>_captionAlign; set{_captionAlign=value;InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(CaptionStyle.Default)] public CaptionStyle CaptionStyle { get=>_captionStyle; set{_captionStyle=value;InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(HoverColorLocks.TextAndBackColor)] public HoverColorLocks HoverColorLocks { get=>_hoverColorLocks; set{_hoverColorLocks=value;InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(CustomClick.Default)] public CustomClick CustomClick { get=>_customClick; set{_customClick=value;InvalidateIfNeeded();} }
    [Category("LaVolpe"), DefaultValue(FontStyles.PlainStyle)] public FontStyles FontStyle { get=>_fontStyle; set{_fontStyle=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Image? MouseIcon { get=>_mouseIcon; set{_mouseIcon=value;if(value is not null)_mousePointer=MousePointerConstants.Custom;ApplyMouseIcon();InvalidateIfNeeded();} }
    // VB6: MousePointerConstants, no un Cursor arbitrario. Custom usa MouseIcon.
    [Category("LaVolpe"), DefaultValue(MousePointerConstants.Default)]
    public MousePointerConstants MousePointer { get=>_mousePointer; set{_mousePointer=value;ApplyMousePointer();} }
    // VB6/UserControl: imagen de arrastre usada durante OLEDrag.
    [Category("LaVolpe")] public Image? DragIcon { get=>_dragIcon; set{_dragIcon=value;BuildDragCursor();} }
    [Category("LaVolpe"), DefaultValue("")] public string ToolTipText { get=>_toolTipText; set{_toolTipText=value??"";(_toolTip ??= new ToolTip()).SetToolTip(this,_toolTipText); }}
    [Category("LaVolpe")] public bool ShowFocusRect { get=>_showFocusRect; set{_showFocusRect=value;InvalidateIfNeeded();} }
    [Browsable(false), Obsolete("Use DelayDrawing(bool) como en VB6.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DelayDrawingEnabled { get=>_delayDrawing; set{_delayDrawing=value; if(!value) Invalidate();} }
    // VB6: DelayDrawing True / DelayDrawing False
    public void DelayDrawing(bool delay){ _delayDrawing=delay; if(!delay) RedrawButton(); }
    [Category("Behavior"), DefaultValue(false)] public bool Cancel { get=>_cancel; set{if(_cancel==value)return;_cancel=value;UpdateFormButtonBindings();} }
    [Category("Behavior"), DefaultValue(false)] public bool Default { get=>_default; set{if(_default==value)return;_default=value;UpdateFormButtonBindings();InvalidateIfNeeded();} }
    [Category("Behavior"), DefaultValue(DragModeConstants.vbManual)] public DragModeConstants DragMode { get=>_dragMode ? DragModeConstants.vbAutomatic : DragModeConstants.vbManual; set=>_dragMode=value==DragModeConstants.vbAutomatic; }
    [Category("Behavior"), DefaultValue(DialogResult.None)] public DialogResult DialogResult { get=>_dialogResult; set=>_dialogResult=value; }
    [Category("Design"), DefaultValue(0)] public int Index { get=>_index; set=>_index=value; }
    [Category("Help"), DefaultValue(0)] public int HelpContextID { get=>_helpContextId; set=>_helpContextId=value; }
    [Category("Help"), DefaultValue(0)] public int WhatsThisHelpID { get=>_whatsThisHelpId; set=>_whatsThisHelpId=value; }
    public bool ShouldSerializeButtonShape() => _shape != ButtonShape.Rectangular;
    public bool ShouldSerializeButtonStyle() => _style != BackStyle.Windows95;
    [Browsable(false)] public bool MouseOver=>_hovered;
    [Browsable(false)] public IntPtr hwnd=>Handle;
    // HDC válido mientras el control exista. Se libera al destruir el handle o al hacer Dispose.
    [Browsable(false)] public IntPtr hDC { get { if(_hdc==IntPtr.Zero){ _hdcGraphics=Graphics.FromHwnd(Handle); _hdc=_hdcGraphics.GetHdc(); } return _hdc; } }
    [Category("LaVolpe")] public bool ResetDefaultColors { get => _resetDefaultColors; set { _resetDefaultColors = value; if (value) ResetDefaultColorsNow(); } }
    public void ResetDefaultColorsNow(){_normalBack=SystemColors.Control;_hoverBack=Color.Empty;_gradientColor=SystemColors.ControlLight;_hoverFore=SystemColors.ControlText;_explicitBackColor=false;base.BackColor=_normalBack;_resetDefaultColors=false;InvalidateIfNeeded();}
    private Color HoverBackEffective => _hoverBack.IsEmpty ? _normalBack : _hoverBack;
    private bool IsCustomShape => _shape is ButtonShape.CustomFlat or ButtonShape.Custom3DBorder;
    private void ReleaseHdc()
    {
        if(_hdcGraphics is null) return;
        try { if(_hdc!=IntPtr.Zero) _hdcGraphics.ReleaseHdc(_hdc); }
        finally { _hdc=IntPtr.Zero; _hdcGraphics.Dispose(); _hdcGraphics=null; }
    }
    private void UpdateRegion()
    {
        var old=_region;
        Region? region=null;
        if(IsCustomShape && _picture is not null)
        {
            try { region=BuildImageRegion(_picture, ClientRectangle); }
            catch(ArgumentException) { region=null; }      // imagen ya liberada por el usuario
            catch(ExternalException) { region=null; }
        }
        else if(_shape!=ButtonShape.Rectangular)
        {
            using var path=CreatePath(ClientRectangle);
            region=new Region(path);
            LopOffCorners(region, ClientRectangle);
        }
        else
        {
            // VB6 recorta 1-3 px en las esquinas también en botones rectangulares (LopOffCorners1..4).
            var rectangular=new Region(ClientRectangle);
            if(LopOffCorners(rectangular, ClientRectangle)) region=rectangular; else rectangular.Dispose();
        }
        _region=region;
        Region=region;
        old?.Dispose();
    }
    // Recorta las esquinas como LopOffCorners1..4 de lvButtons.ctl. Devuelve true si aplicó algún recorte.
    private bool LopOffCorners(Region region, Rectangle r)
    {
        bool left, right;
        switch(_shape)
        {
            case ButtonShape.LeftDiagonal: left=true; right=false; break;
            case ButtonShape.RightDiagonal: left=false; right=true; break;
            case ButtonShape.FullDiagonal: return false;
            default: left=true; right=true; break;
        }
        switch(_style)
        {
            case BackStyle.Windows31:
            case BackStyle.Java:
                if(_style==BackStyle.Java && !Enabled) return false;
                if(left){ region.Exclude(new Rectangle(r.Left, r.Top, 1, 1)); region.Exclude(new Rectangle(r.Left, r.Bottom-1, 1, 1)); }
                if(right){ region.Exclude(new Rectangle(r.Right-1, r.Top, 1, 1)); region.Exclude(new Rectangle(r.Right-1, r.Bottom-1, 1, 1)); }
                return true;
            case BackStyle.WindowsXp:
            case BackStyle.Macintosh:
                if(left){ ExcludeTriangle(region, new Point(r.Left, r.Top), new Point(r.Left+2, r.Top), new Point(r.Left, r.Top+2));
                          ExcludeTriangle(region, new Point(r.Left, r.Bottom), new Point(r.Left+3, r.Bottom), new Point(r.Left, r.Bottom-3)); }
                if(right){ ExcludeTriangle(region, new Point(r.Right, r.Top), new Point(r.Right-2, r.Top), new Point(r.Right, r.Top+2));
                           ExcludeTriangle(region, new Point(r.Right, r.Bottom), new Point(r.Right-3, r.Bottom), new Point(r.Right, r.Bottom-3)); }
                return true;
            default:
                return false;
        }
    }
    private static void ExcludeTriangle(Region region, Point a, Point b, Point c)
    {
        using var path=new GraphicsPath();
        path.AddPolygon(new[]{a,b,c});
        region.Exclude(path);
    }
    // NormalizeImage de VB6: la región sale de la imagen usando el píxel superior izquierdo como máscara.
    private static Region BuildImageRegion(Image image, Rectangle bounds)
    {
        var w=Math.Max(1, bounds.Width); var h=Math.Max(1, bounds.Height);
        if((long)w*h>250_000) return new Region(bounds);
        using var scaled=new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using(var g=Graphics.FromImage(scaled)){ g.InterpolationMode=InterpolationMode.HighQualityBicubic; g.PixelOffsetMode=PixelOffsetMode.HighQuality; g.DrawImage(image, 0, 0, w, h); }
        var data=scaled.LockBits(new Rectangle(0,0,w,h), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        var bytes=new byte[data.Stride*h];
        try { System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length); }
        finally { scaled.UnlockBits(data); }
        int maskIndex=0;   // píxel (0,0) = color transparente
        int maskB=bytes[maskIndex], maskG=bytes[maskIndex+1], maskR=bytes[maskIndex+2];
        var region=new Region(); region.MakeEmpty();
        for(int y=0;y<h;y++)
        {
            int start=-1;
            for(int x=0;x<w;x++)
            {
                int i=y*data.Stride+x*4;
                bool transparent=bytes[i+3]<20 || (Math.Abs(bytes[i]-maskB)<=10 && Math.Abs(bytes[i+1]-maskG)<=10 && Math.Abs(bytes[i+2]-maskR)<=10);
                if(!transparent && start<0) start=x;
                else if(transparent && start>=0){ region.Union(new Rectangle(start, y, x-start, 1)); start=-1; }
            }
            if(start>=0) region.Union(new Rectangle(start, y, w-start, 1));
        }
        bool empty;
        using(var gg=Graphics.FromImage(scaled)) empty=region.IsEmpty(gg);
        if(empty) { region.Dispose(); return new Region(bounds); }
        return region;
    }
    private void ApplyMouseIcon()
    {
        _mouseIconCursor?.Dispose(); _mouseIconCursor=null;
        if(_mouseIconHandle!=IntPtr.Zero){ DestroyIcon(_mouseIconHandle); _mouseIconHandle=IntPtr.Zero; }
        if(_mouseIcon is not null)
        {
            // VB6 usa la imagen tal cual, sin escalarla, con hotspot centrado.
            (_mouseIconCursor,_mouseIconHandle)=CreateCursorFromImage(_mouseIcon);
        }
        ApplyMousePointer();
    }
    private void ApplyMousePointer()
    {
        if(_mousePointer==MousePointerConstants.Custom)
        {
            Cursor=_mouseIconCursor ?? Cursors.Default;
            return;
        }
        Cursor=_mousePointer switch
        {
            MousePointerConstants.Arrow => Cursors.Arrow,
            MousePointerConstants.Crosshair => Cursors.Cross,
            MousePointerConstants.IBeam => Cursors.IBeam,
            MousePointerConstants.SizeNWSE => Cursors.SizeNWSE,
            MousePointerConstants.SizeNESW => Cursors.SizeNESW,
            MousePointerConstants.SizeWE => Cursors.SizeWE,
            MousePointerConstants.SizeNS => Cursors.SizeNS,
            MousePointerConstants.SizeAll => Cursors.SizeAll,
            MousePointerConstants.NoDrop => Cursors.No,
            MousePointerConstants.ArrowHourglass => Cursors.AppStarting,
            MousePointerConstants.ArrowQuestion => Cursors.Help,
            MousePointerConstants.Hourglass => Cursors.WaitCursor,
            MousePointerConstants.UpArrow => Cursors.UpArrow,
            _ => Cursors.Default
        };
    }
    private void BuildDragCursor()
    {
        _dragIconCursor?.Dispose(); _dragIconCursor=null;
        if(_dragIconHandle!=IntPtr.Zero){ DestroyIcon(_dragIconHandle); _dragIconHandle=IntPtr.Zero; }
        if(_dragIcon is not null) (_dragIconCursor,_dragIconHandle)=CreateCursorFromImage(_dragIcon);
    }
    private static (Cursor? cursor, IntPtr handle) CreateCursorFromImage(Image image)
    {
        try
        {
            using var bitmap=new Bitmap(image);
            var size=bitmap.Size;
            if(size.Width<=0||size.Height<=0) return (null,IntPtr.Zero);
            var hColor=bitmap.GetHbitmap(Color.FromArgb(0));
            var hMask=CreateBitmap(size.Width, size.Height, 1, 1, IntPtr.Zero);
            try
            {
                var info=new ICONINFO { fIcon=false, xHotspot=size.Width/2, yHotspot=size.Height/2, hbmMask=hMask, hbmColor=hColor };
                var handle=CreateIconIndirect(ref info);
                return handle==IntPtr.Zero ? (null,IntPtr.Zero) : (new Cursor(handle),handle);
            }
            finally { DeleteObject(hColor); DeleteObject(hMask); }
        }
        catch(ArgumentException) { return (null,IntPtr.Zero); }   // imagen ya liberada
        catch(ExternalException) { return (null,IntPtr.Zero); }
    }
    private void SetValue(bool value){if(_value==value)return;_value=value;ValueChanged?.Invoke(this,EventArgs.Empty);if(_mode==ButtonMode.CheckBox)CheckedChanged?.Invoke(this,EventArgs.Empty);if(_mode==ButtonMode.OptionButton)SelectedChanged?.Invoke(this,EventArgs.Empty);InvalidateIfNeeded();}
    private void SelectThis(){ if(_mode==ButtonMode.OptionButton) DeselectSiblings(); SetValue(true); }
    private void DeselectSiblings()
    {
        if(Parent is null) return;
        foreach(Control c in Parent.Controls)
            if(c is LaVolpeButton b && b!=this && b.Mode==ButtonMode.OptionButton) b.SetValue(false);
    }
    private void InvalidateIfNeeded(){if(!_delayDrawing) Invalidate();}
    public void RedrawButton(){Invalidate(); Update();}
    public void TimerUpdate(long timerId)
    {
        if (!IsHandleCreated || !Visible) return;
        var point = PointToClient(Cursor.Position);
        using var path = CreatePath(ClientRectangle);
        var inside = path.IsVisible(point);
        if (inside != _hovered) { _hovered = inside; MouseOnButton?.Invoke(this, inside); InvalidateIfNeeded(); }
    }
    public void BeginDelayDrawing(){_delayDrawing=true;}
    public void EndDelayDrawing(){_delayDrawing=false; RedrawButton();}
    public void RaiseOLECompleteDrag(){OLECompleteDrag?.Invoke(this, EventArgs.Empty);}
    public void RaiseOLESetData(){OLESetData?.Invoke(this, EventArgs.Empty);}

    private Form? _boundForm;
    private void UpdateFormButtonBindings()
    {
        if (_boundForm is not null)
        {
            if (!_default && ReferenceEquals(_boundForm.AcceptButton, this)) _boundForm.AcceptButton = null;
            if (!_cancel && ReferenceEquals(_boundForm.CancelButton, this)) _boundForm.CancelButton = null;
        }
        var form = FindForm();
        if (form is null) return;
        if (_default) form.AcceptButton = this;
        if (_cancel) form.CancelButton = this;
        if (_default || _cancel) _boundForm = form;
    }
    public void NotifyDefault(bool value){ if(_default==value) return; _default=value; InvalidateIfNeeded(); }
    protected override void OnEnabledChanged(EventArgs e){base.OnEnabledChanged(e);UpdateRegion();InvalidateIfNeeded();}
    protected override void OnResize(EventArgs e){base.OnResize(e);UpdateRegion();InvalidateIfNeeded();}
    protected override void OnDpiChangedAfterParent(EventArgs e){base.OnDpiChangedAfterParent(e);UpdateRegion();InvalidateIfNeeded();}
    protected override void OnHandleCreated(EventArgs e){base.OnHandleCreated(e);UpdateRegion();}
    protected override void OnHandleDestroyed(EventArgs e){_hoverTimer.Stop();ReleaseHdc();base.OnHandleDestroyed(e);}
    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);
        // VB6 toma el BackColor del contenedor al crear el control.
        if(!_explicitBackColor && Parent is not null){ _normalBack=Parent.BackColor; base.BackColor=_normalBack; InvalidateIfNeeded(); }
        UpdateFormButtonBindings();
    }
    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        if(_mode==ButtonMode.OptionButton && !_value) SelectThis();   // VB6: GotFocus selecciona la opción
        InvalidateIfNeeded();
    }
    protected override void OnLostFocus(EventArgs e){base.OnLostFocus(e);InvalidateIfNeeded();}
    protected override bool ProcessDialogKey(Keys keyData){
        var key = keyData & Keys.KeyCode;
        if(Mode==ButtonMode.CommandButton && key==Keys.Escape && Cancel){PerformClick();return true;}
        if(Mode==ButtonMode.CommandButton && key==Keys.Enter && Default){PerformClick();return true;}
        if(key is Keys.Right or Keys.Down) return MoveFocus(true);
        if(key is Keys.Left or Keys.Up) return MoveFocus(false);
        return base.ProcessDialogKey(keyData);
    }
    private bool MoveFocus(bool forward)
    {
        // En OptionButton las flechas recorren el grupo y seleccionan, como en VB6.
        if(_mode==ButtonMode.OptionButton && Parent is not null)
        {
            var options=Parent.Controls.OfType<LaVolpeButton>().Where(b=>b.Mode==ButtonMode.OptionButton).ToList();
            var index=options.IndexOf(this);
            if(index>=0 && options.Count>1)
            {
                var next=options[(index+(forward?1:options.Count-1))%options.Count];
                next.Focus(); next.SelectThis(); return true;
            }
        }
        Parent?.SelectNextControl(this,forward,true,true,true);
        return true;
    }
    protected override bool ProcessMnemonic(char charCode){if(!CanSelect)return false;var mnemonic=FindMnemonic(Caption);if(mnemonic != '\0' && char.ToUpperInvariant(mnemonic)==char.ToUpperInvariant(charCode)){Focus();PerformClick();return true;}return base.ProcessMnemonic(charCode);}
    private static char FindMnemonic(string text){for(int i=0;i<text.Length-1;i++){if(text[i]=='&'){if(text[i+1]=='&'){i++;continue;}return text[i+1];}}return '\0';}
    protected override void Dispose(bool disposing)
    {
        if(disposing)
        {
            ReleaseHdc();
            if(_region is not null)
            {
                if(IsHandleCreated && ReferenceEquals(Region, _region)) Region = null;
                _region.Dispose();
                _region = null;
            }
            _hoverTimer.Stop(); _hoverTimer.Dispose(); _toolTip?.Dispose(); _mouseIconCursor?.Dispose(); _dragIconCursor?.Dispose();
            if(_mouseIconHandle!=IntPtr.Zero){ DestroyIcon(_mouseIconHandle); _mouseIconHandle=IntPtr.Zero; }
            if(_dragIconHandle!=IntPtr.Zero){ DestroyIcon(_dragIconHandle); _dragIconHandle=IntPtr.Zero; }
        }
        base.Dispose(disposing);
    }
    protected override void OnMouseEnter(EventArgs e){base.OnMouseEnter(e);_hoverTimer.Start();TimerUpdate(0);}
    protected override void OnMouseLeave(EventArgs e){base.OnMouseLeave(e);_hoverTimer.Stop();if(_hovered){_hovered=false;MouseOnButton?.Invoke(this,false);} _pressed=false;InvalidateIfNeeded();}
    protected override void OnMouseDown(MouseEventArgs e){base.OnMouseDown(e);LvMouseDown?.Invoke(this,e);if(e.Button==MouseButtons.Left&&Enabled){_dragStart=e.Location;_pressed=true;Focus();InvalidateIfNeeded();}}
    protected override void OnMouseUp(MouseEventArgs e){base.OnMouseUp(e);LvMouseUp?.Invoke(this,e);if(e.Button==MouseButtons.Left&&_pressed){_pressed=false;using(var path=CreatePath(ClientRectangle)) if(path.IsVisible(e.Location))PerformClick();InvalidateIfNeeded();}}
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        LvMouseMove?.Invoke(this, e);
        if (!_dragMode || e.Button != MouseButtons.Left || !Enabled) return;
        var dx = Math.Abs(e.X - _dragStart.X);
        var dy = Math.Abs(e.Y - _dragStart.Y);
        if (dx < SystemInformation.DragSize.Width / 2 && dy < SystemInformation.DragSize.Height / 2) return;

        _pressed = false;
        var data = CreateDragData(Caption);
        OLEStartDrag?.Invoke(this, new DragEventArgs(data, 0, e.X, e.Y, DragDropEffects.Copy, DragDropEffects.Copy));
        try
        {
            DoDragDrop(data, DragDropEffects.Copy);
        }
        finally
        {
            OLECompleteDrag?.Invoke(this, EventArgs.Empty);
            ApplyMousePointer();
            InvalidateIfNeeded();
        }
    }
    private static DataObject CreateDragData(string caption)
    {
        var data = new DataObject();
        data.SetData(DataFormats.UnicodeText, caption);
        data.SetData(DataFormats.Text, caption);
        return data;
    }
    protected override void OnMouseDoubleClick(MouseEventArgs e){base.OnMouseDoubleClick(e);ButtonDoubleClick?.Invoke(this,(int)e.Button);}
    protected override void OnKeyDown(KeyEventArgs e){base.OnKeyDown(e);LvKeyDown?.Invoke(this,e);if(e.KeyCode is Keys.Space or Keys.Enter){_pressed=true;InvalidateIfNeeded();}}
    protected override void OnKeyUp(KeyEventArgs e){base.OnKeyUp(e);LvKeyUp?.Invoke(this,e);if(e.KeyCode is Keys.Space or Keys.Enter){_pressed=false;PerformClick();InvalidateIfNeeded();}}
    protected override void OnKeyPress(KeyPressEventArgs e){base.OnKeyPress(e);LvKeyPress?.Invoke(this,e);}
    protected override void OnClick(EventArgs e)
    {
        if(_mode==ButtonMode.CheckBox) SetValue(!_value);
        else if(_mode==ButtonMode.OptionButton)
        {
            if(_value) return;    // VB6 suprime el Click sobre la opción ya seleccionada
            SelectThis();
        }
        base.OnClick(e);
        if(_dialogResult!=DialogResult.None && TopLevelControl is Form form) form.DialogResult=_dialogResult;
        InvalidateIfNeeded();
    }
    public void PerformClick(){if(Enabled)OnClick(EventArgs.Empty);}
    protected override void OnDragDrop(DragEventArgs drgevent){base.OnDragDrop(drgevent);OLEDragDrop?.Invoke(this,drgevent);}
    protected override void OnDragOver(DragEventArgs drgevent){base.OnDragOver(drgevent);OLEDragOver?.Invoke(this,drgevent);}
    protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
    {
        base.OnGiveFeedback(e);
        OLEGiveFeedback?.Invoke(this,e);
        // DragIcon de VB6 se muestra mientras se arrastra.
        if(_dragIconCursor is not null) { Cursor=_dragIconCursor; e.UseDefaultCursors=false; }
    }
    protected override void OnDragEnter(DragEventArgs e){base.OnDragEnter(e);}

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e); var g=e.Graphics; g.SmoothingMode=SmoothingMode.AntiAlias; g.InterpolationMode=InterpolationMode.HighQualityBicubic; g.PixelOffsetMode=PixelOffsetMode.HighQuality;
        var full=ClientRectangle; var r=full; r.Width--; r.Height--;
        using var fillPath=CreatePath(full);      // área completa: se recorta con la Region de la ventana
        var selected = _mode!=ButtonMode.CommandButton && _value;
        var down = _pressed || selected;
        var hot = _hovered && !down;
        var back = GetStateBack(down,hot,selected);
        DrawButtonBackground(g,full,fillPath,back,down,hot,selected);
        var useHoverFore = _hovered && _hoverColorLocks is not (HoverColorLocks.TextColorOnly or HoverColorLocks.TextAndBackColor);
        var contentColors = GetCaptionColors(useHoverFore,back);
        var clipState = g.Save();
        g.SetClip(fillPath, CombineMode.Intersect);
        DrawContent(g,ClientRectangle,contentColors,selected,back);
        g.Restore(clipState);
        if(Focused&&ShowFocusRect) DrawFocusRing(g,r);
    }
    private Color GetStateBack(bool down, bool hot, bool selected)
    {
        // VB6: sólo el CommandButton XP deshabilitado oscurece el fondo (-0x18).
        if(!Enabled && _style==BackStyle.WindowsXp && _mode==ButtonMode.CommandButton) return Shade(GetUpBack(),-0x18,true);
        if(selected) return GetCheckBack();
        var up=GetUpBack();
        if(down) return GetDownBack(up);
        if(hot && _hoverColorLocks is not (HoverColorLocks.BackColorOnly or HoverColorLocks.TextAndBackColor)) return GetHoverBack(up);
        return up;
    }
    // adjBackColorUp/adjBackColorDn/cCheckBox se calculan por estilo en lvButtons.ctl.
    private Color GetUpBack() => _style switch
    {
        BackStyle.Macintosh => Shade(_normalBack,0x1F),
        BackStyle.WindowsXp => Shade(_normalBack,0x30,true),
        _ => _normalBack
    };
    private Color GetHoverBack(Color up) { var h=HoverBackEffective; return _style switch
    {
        BackStyle.Macintosh => Shade(h,0x1F),
        BackStyle.WindowsXp => Shade(h,0x30,true),
        _ => h
    }; }
    private Color GetDownBack(Color up) => _style switch
    {
        BackStyle.Macintosh => Shade(Color.FromArgb(128,128,128),-0x10),
        BackStyle.WindowsXp => Shade(up,-0x20,true),
        BackStyle.Java => Shade(Color.FromArgb(128,128,128),0x0C),
        _ => up
    };
    private Color GetCheckBack() => _style switch
    {
        BackStyle.Macintosh => Shade(Color.FromArgb(128,128,128),0x10,true),
        BackStyle.WindowsXp => Shade(Color.White,-0x20,true),
        BackStyle.Java => Shade(Shade(Color.FromArgb(128,128,128),0x0C),0x1F),
        _ => Shade(Color.White,-0x20)
    };
    // DrawButtonBackground: relleno + tres anillos concéntricos de borde (DrawButtonBorder).
    private void DrawButtonBackground(Graphics g, Rectangle full, GraphicsPath fillPath, Color back, bool down, bool hot, bool selected)
    {
        if(_gradientMode!=GradientMode.None && !selected && _shape<ButtonShape.CustomFlat)
        {
            using var brush=CreateBrush(full,back); g.FillPath(brush,fillPath);
        }
        else if(_style==BackStyle.WindowsXp && (Enabled || _mode!=ButtonMode.CommandButton))
        {
            // VB6: los botones XP llevan un degradado vertical fijo de 25 tonos.
            using var xp=new LinearGradientBrush(full,back,Shade(back,-25,true),LinearGradientMode.Vertical);
            g.FillPath(xp,fillPath);
        }
        else
        {
            using var brush=new SolidBrush(back); g.FillPath(brush,fillPath);
        }
        var rings=GetBorders(back,down,hot,selected);
        for(int i=0;i<rings.Length;i++)
        {
            if(rings[i].IsEmpty) continue;
            var rr=Rectangle.Inflate(full,-i,-i);
            if(rr.Width<=2||rr.Height<=2) continue;
            using var path=CreatePath(rr);
            DrawRing(g,path,rr,rings[i]);
        }
        if (_style == BackStyle.WindowsXp && _shape is ButtonShape.LeftDiagonal or ButtonShape.RightDiagonal or ButtonShape.FullDiagonal)
        {
            // VB6 deja una línea exterior azul continua en las diagonales; los anillos
            // interiores de WinForms pueden suavizarla si se dibuja antes.
            var edgeState = g.Save();
            g.SmoothingMode = SmoothingMode.None;
            g.PixelOffsetMode = PixelOffsetMode.Default;
            using var edge = new Pen(Color.FromArgb(0, 60, 115), 1f);
            var edgeRect = full;
            edgeRect.Width--;
            edgeRect.Height--;
            using var edgePath = CreatePath(edgeRect);
            g.DrawPath(edge, edgePath);
            g.Restore(edgeState);
        }
    }
    // Cada anillo guarda los cuatro lados por separado (polyColors 1..4, 5..8 y 9..12).
    private readonly struct RingBorder
    {
        public readonly Color? Left, Top, Right, Bottom;
        public RingBorder(Color? left, Color? top, Color? right, Color? bottom){Left=left;Top=top;Right=right;Bottom=bottom;}
        public static readonly RingBorder None = new(null,null,null,null);
        public static RingBorder Same(Color? c) => new(c,c,c,c);
        public static RingBorder Pair(Color? leftTop, Color? rightBottom) => new(leftTop,leftTop,rightBottom,rightBottom);
        public bool IsEmpty => Left is null && Top is null && Right is null && Bottom is null;
    }
    private static void DrawRing(Graphics g, GraphicsPath path, Rectangle b, RingBorder ring)
    {
        var fallback = ring.Top ?? ring.Left ?? ring.Right ?? ring.Bottom;
        if(fallback is null) return;
        var f = fallback.Value;
        var bandT=Math.Max(2,b.Height/3);
        DrawClipped(g,path,new Rectangle(b.Left,b.Top,b.Width,bandT+1),ring.Top ?? f);
        DrawClipped(g,path,new Rectangle(b.Left,b.Bottom-bandT-1,b.Width,bandT+1),ring.Bottom ?? f);
        var bandL=Math.Max(2,b.Width/3);
        DrawClipped(g,path,new Rectangle(b.Left,b.Top+bandT,bandL,Math.Max(1,b.Height-2*bandT)),ring.Left ?? f);
        DrawClipped(g,path,new Rectangle(b.Right-bandL,b.Top+bandT,bandL,Math.Max(1,b.Height-2*bandT)),ring.Right ?? f);
    }
    private static void DrawClipped(Graphics g, GraphicsPath path, Rectangle clip, Color color)
    {
        if(clip.Width<=0||clip.Height<=0) return;
        var state=g.Save();
        g.SetClip(clip,CombineMode.Intersect);
        using(var pen=new Pen(color,1f)) g.DrawPath(pen,path);
        g.Restore(state);
    }
    private RingBorder[] GetBorders(Color back, bool down, bool hot, bool selected)
    {
        var rings=new RingBorder[3];
        var gray=Color.FromArgb(128,128,128); var white=Color.White; var black=Color.Black;
        bool focused=Focused;
        switch(_style)
        {
            case BackStyle.Windows95:
            {
                // midShade es el borde pulsado; con Value/tecla vuelve a RGB(233,233,233).
                var mid = (down && !selected) ? back : Color.FromArgb(233,233,233);
                var st = (down||selected) ? 3 : (focused?1:0);
                rings[2] = st==1 ? RingBorder.Pair(mid,gray) : RingBorder.None;
                rings[1] = st switch
                {
                    0 => RingBorder.Pair(mid,gray),
                    1 => RingBorder.Pair(white,black),
                    3 => new RingBorder(null,null,mid,mid),
                    _ => RingBorder.None
                };
                rings[0] = selected
                    ? new RingBorder(black,black,white,white)
                    : new RingBorder(focused?black:white,focused?black:white,black,black);
                break;
            }
            case BackStyle.Windows31:
            {
                var st = (down||selected) ? 2 : 1;
                var t = st==2 ? gray : white;
                var b = st==2 ? white : gray;
                rings[2]=RingBorder.Pair(t,b); rings[1]=RingBorder.Pair(t,b); rings[0]=RingBorder.Same(black);
                break;
            }
            case BackStyle.WindowsXp:
            {
                var lColor=selected?GetCheckBack():GetUpBack();
                if(!Enabled)
                {
                    rings[0]=RingBorder.Same(Shade(lColor,-0x54,true));
                    break;
                }
                int st = (selected||down) ? (focused?1:2) : (hot?3:(focused?1:0));
                if(st==1 && !ShowFocusRect) st=0;
                Color C(int offset)=>Shade(lColor,offset,true);
                Color c1,c2,c3,c4,c5,c6,c7,c8;
                switch(st)
                {
                    case 1:   // foco: tonos tostados
                        c1=Color.FromArgb(181,209,240); c2=Color.FromArgb(189,215,247); c3=Color.FromArgb(181,209,240); c4=Color.FromArgb(140,174,231);
                        c5=Color.FromArgb(140,174,231); c6=Color.FromArgb(191,223,255); c7=Color.FromArgb(140,174,231); c8=Color.FromArgb(107,130,239);
                        break;
                    case 2:   // pulsado / marcado
                        c1=C(-0x16); c2=C(-0x18); c3=lColor; c4=C(0xA);
                        c5=C(-0x20); c6=C(-0x20); c7=C(0x5); c8=C(0x10);
                        break;
                    case 3:   // hover: tonos azules
                        c1=Color.FromArgb(255,203,107); c2=Color.FromArgb(255,219,140); c3=Color.FromArgb(255,203,107); c4=Color.FromArgb(255,178,49);
                        c5=Color.FromArgb(255,178,49); c6=Color.FromArgb(255,233,166); c7=Color.FromArgb(255,178,49); c8=Color.FromArgb(231,150,0);
                        break;
                    default:  // normal
                        c1=C(-0xA); c2=C(0xA); c3=C(-0x18); c4=C(-0x20);
                        c5=C(-0x5); c6=C(0x10); c7=C(-0x24); c8=C(-0x30);
                        break;
                }
                rings[2]=new RingBorder(c1,c2,c3,c4);
                rings[1]=new RingBorder(c5,c6,c7,c8);
                rings[0]=RingBorder.Same(Color.FromArgb(0,60,115));   // lColor = &H733C00
                break;
            }
            case BackStyle.Macintosh:
            {
                var st = (down||selected) ? 2 : 1;
                Color baseColor, lite, mid, dark;
                if(st==2)
                {
                    // VB6 parte de vbGray cuando no hay degradado, no del fondo pulsado.
                    baseColor = _gradientMode==GradientMode.None ? gray : GetUpBack();
                    lite=Shade(baseColor,-0x20);
                    mid=Shade(baseColor,-0x40);
                    dark=Shade(baseColor,-0x10);
                }
                else
                {
                    baseColor=back; lite=white;
                    mid=Shade(back,0x1F,true);
                    dark=Shade(back,-0x40,true);
                }
                rings[2]=RingBorder.Pair(lite,baseColor);
                rings[1]=RingBorder.Pair(mid,dark);
                rings[0]=RingBorder.Same(black);
                break;
            }
            case BackStyle.Flat:
            {
                var st = (down||selected) ? 2 : 1;
                rings[0] = st==1 ? RingBorder.Pair(white,gray) : RingBorder.Pair(gray,white);
                break;
            }
            case BackStyle.Hover:
            {
                if(!hot && !down && !selected) break;     // sin borde hasta que el ratón entra
                var st = (down||selected) ? 2 : 1;
                rings[0] = st==1 ? RingBorder.Pair(white,gray) : RingBorder.Pair(gray,white);
                break;
            }
            case BackStyle.Netscape:
            {
                var lite=Color.FromArgb(231,231,231);
                var st = (down||selected) ? 1 : 0;
                var t = st==0 ? lite : gray;
                var b = st==0 ? gray : lite;
                rings[1]=RingBorder.Pair(t,b); rings[0]=RingBorder.Pair(t,b);
                break;
            }
            case BackStyle.Java:
            {
                var dark=Shade(gray,-0x1A);
                if(!Enabled)
                {
                    // VB6 deshabilitado: todo el anillo medio con el fondo y el exterior oscuro.
                    rings[1]=RingBorder.Same(back);
                    rings[0]=RingBorder.Same(dark);
                    break;
                }
                int st = _mode!=ButtonMode.CommandButton ? ((selected||_pressed)?2:3) : (down?2:1);
                if(st<2) st=1;
                var t = st==2 ? back : white;
                rings[1]=new RingBorder(t,t,dark,dark);
                rings[0]=new RingBorder(dark,dark,white,white);
                break;
            }
        }
        return rings;
    }
    // VB6 cambia el foco según el estilo; las formas personalizadas no lo dibujan.
    private void DrawFocusRing(Graphics g, Rectangle r)
    {
        if(IsCustomShape) return;
        var inner=Rectangle.Inflate(r,-4,-4);
        if(_shape is ButtonShape.Round3D or ButtonShape.Round3DFixed or ButtonShape.RoundFlat)
        {
            using var pen=new Pen(Color.Black,1) { DashStyle=DashStyle.Dot };
            g.DrawEllipse(pen,inner);
            return;
        }
        if(_style is BackStyle.WindowsXp or BackStyle.Java)
        {
            using var pen=new Pen(_style==BackStyle.WindowsXp?Color.FromArgb(107,130,239):Color.FromArgb(204,153,153),1);
            g.DrawRectangle(pen,inner);
            return;
        }
        ControlPaint.DrawFocusRectangle(g,inner);
    }
    private Point CustomClickOffset() => CustomClick switch { CustomClick.North => new Point(0,-1), CustomClick.NorthEast => new Point(1,-1), CustomClick.NorthWest => new Point(-1,-1), CustomClick.SouthEast => new Point(1,1), CustomClick.SouthWest => new Point(-1,1), CustomClick.East => new Point(1,0), CustomClick.South => new Point(0,1), CustomClick.West => new Point(-1,0), _ => new Point(1,1) };
    // ShadeColor de lvButtons.ctl: desplaza R y G; con lessBlue el azul se mueve proporcionalmente.
    private static Color Shade(Color c, int offset, bool lessBlue = false)
    {
        int r = Math.Clamp(c.R + offset, 0, 255);
        int g = Math.Clamp(c.G + offset, 0, 255);
        int b = lessBlue
            ? Math.Clamp(c.B + (int)(((long)c.B * offset) / 192), 0, 255)
            : Math.Clamp(c.B + offset, 0, 255);
        return Color.FromArgb(r, g, b);
    }
    // Diagonal de VB6: lEdge = 4 * ((Height + 1) / 4), dejando al menos 5 px de ancho.
    private static int DiagonalEdge(Rectangle r)
    {
        var edge = (int)Math.Round(4.0 * ((r.Height + 1) / 4.0));
        return Math.Max(4, Math.Min(edge, r.Width - 5));
    }
    private GraphicsPath CreatePath(Rectangle r)
    {
        var p = new GraphicsPath();
        switch (_shape)
        {
            case ButtonShape.LeftDiagonal:
                p.AddPolygon(new[] { new Point(r.Left, r.Top), new Point(r.Left + DiagonalEdge(r), r.Bottom), new Point(r.Right, r.Bottom), new Point(r.Right, r.Top) });
                break;
            case ButtonShape.RightDiagonal:
                p.AddPolygon(new[] { new Point(r.Left, r.Top), new Point(r.Left, r.Bottom), new Point(r.Right, r.Bottom), new Point(r.Right - DiagonalEdge(r), r.Top) });
                break;
            case ButtonShape.FullDiagonal:
                p.AddPolygon(new[] { new Point(r.Left, r.Top), new Point(r.Left + DiagonalEdge(r), r.Bottom), new Point(r.Right, r.Bottom), new Point(r.Right - DiagonalEdge(r), r.Top) });
                break;
            // Elipse completa, como CreateEllipticRgn(0, 0, ScaleWidth, ScaleHeight) en VB6.
            case ButtonShape.Round3D:
            case ButtonShape.Round3DFixed:
            case ButtonShape.RoundFlat:
                p.AddEllipse(r);
                break;
            default:
                p.AddRectangle(r);
                break;
        }
        return p;
    }
    private Brush CreateBrush(Rectangle r,Color back){if(_gradientMode==GradientMode.None)return new SolidBrush(back);var mode=_gradientMode is GradientMode.LeftToRight or GradientMode.RightToLeft?LinearGradientMode.Horizontal:LinearGradientMode.Vertical;var b=new LinearGradientBrush(r,back,_gradientColor,mode);if(_gradientMode is GradientMode.RightToLeft or GradientMode.BottomToTop)b.RotateTransform(180);return b;}
    private readonly record struct CaptionColors(Color Color, Color? Shadow, Point ShadowOffset);
    // Offsets de DrawCaptionIcon: pulsado/marcado desplaza 1 px (2 en formas redondas), Java nunca.
    private Point ContentOffset(bool selected)
    {
        if(_style==BackStyle.Java) return Point.Empty;
        if(_pressed) return CustomClickOffset();
        if(selected) return new Point(1 + (_shape>ButtonShape.FullDiagonal?1:0), 1);
        return Point.Empty;
    }
    private CaptionColors GetCaptionColors(bool useHoverFore, Color back)
    {
        var gray=Color.FromArgb(128,128,128);
        if(!Enabled)
        {
            if(_style==BackStyle.WindowsXp)
            {
                // cDisabled = ShadeColor(backShade, -0x68, True), color único.
                var baseColor = (_mode!=ButtonMode.CommandButton && _value) ? GetCheckBack() : GetUpBack();
                if(_mode==ButtonMode.CommandButton) baseColor=Shade(baseColor,-0x18,true);
                return new CaptionColors(Shade(baseColor,-0x68,true),null,Point.Empty);
            }
            if(_style==BackStyle.Java) return new CaptionColors(Shade(gray,-0x10),null,Point.Empty);
            if(_mode!=ButtonMode.CommandButton)
            {
                if(_style==BackStyle.Macintosh && _value) return new CaptionColors(Shade(GetCheckBack(),-0x20,true),null,Point.Empty);
                return new CaptionColors(gray,null,Point.Empty);
            }
            // Estilos clásicos: blanco en +1,+1 y luego gris en la posición original.
            return new CaptionColors(gray,Color.White,new Point(1,1));
        }
        if(_style==BackStyle.Java) return new CaptionColors(gray,null,Point.Empty);
        return new CaptionColors(useHoverFore ? _hoverFore : ForeColor,null,Point.Empty);
    }
    private int HorizontalAlignClass() => _captionAlign switch
    {
        ContentAlignment.TopLeft or ContentAlignment.MiddleLeft or ContentAlignment.BottomLeft => 0,
        ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight => 1,
        _ => 2
    };
    // Traduce CalculateBoundingRects de lvButtons.ctl (texto e imagen).
    private (Rectangle Text, Rectangle Image, bool ShowImage) ComputeLayout(Graphics g, Font f, Rectangle client, string text)
    {
        int w=client.Width, h=client.Height;
        int diag=DiagonalEdge(client);
        int lEdge=0, rEdge=w, adjWidth=w;
        switch(_shape)
        {
            case ButtonShape.LeftDiagonal:
                lEdge=diag-diag/3+3; rEdge=w; adjWidth=w-lEdge; break;
            case ButtonShape.RightDiagonal:
                rEdge=(w-diag)+(diag/3); adjWidth=rEdge; break;
            case ButtonShape.FullDiagonal:
                lEdge=diag-diag/3+3; rEdge=(w-diag)+(diag/3); adjWidth=rEdge-lEdge; break;
            case ButtonShape.CustomFlat:
            case ButtonShape.Custom3DBorder:
                adjWidth=w-3; rEdge=w-3; lEdge=3; break;
        }
        bool hasImage=_picture is not null;
        int size = PictureSize switch { ImageSize.Size24x24=>24, ImageSize.Size32x32=>32, _=>16 };
        bool reserve = hasImage && PictureSize is not (ImageSize.FillStretch or ImageSize.FillScaleUpDown);
        bool imgLeft=reserve && _pictureAlign is ImagePlacement.LeftEdge or ImagePlacement.LeftOfCaption;
        bool imgRight=reserve && _pictureAlign is ImagePlacement.RightEdge or ImagePlacement.RightOfCaption;
        bool imgBottom=reserve && _pictureAlign==ImagePlacement.BottomCenter;
        bool hasText=text.Length>0;
        Rectangle trect=Rectangle.Empty;
        int ch=0;
        if(hasText)
        {
            int maxW=Math.Max(1,adjWidth-8-(imgLeft||imgRight?size:0));
            var textFlags = TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.NoPadding;
            var ms=TextRenderer.MeasureText(g,text,f,new Size(maxW,10000),textFlags);
            int cw=ms.Width, chh=ms.Height;
            if(_captionStyle!=CaptionStyle.Default){cw+=2; chh+=2;}
            ch=chh;
            int left;
            switch(HorizontalAlignClass())
            {
                case 0: left=(imgLeft?size:0)+lEdge+4+(imgLeft?4:0); break;
                case 1: left=rEdge-(imgRight?size:0)-4-cw-(imgRight?4:0); break;
                default:
                    if(imgLeft && _pictureAlign==ImagePlacement.LeftOfCaption)
                        left=(adjWidth-(size+cw+4))/2+lEdge+4+size;
                    else if(imgRight && _pictureAlign==ImagePlacement.RightOfCaption)
                        left=(adjWidth-(size+cw+4))/2+lEdge;
                    else
                        left=((adjWidth-(imgLeft?size:0)-(imgRight?size:0))-cw)/2+lEdge+(imgLeft?size:0);
                    break;
            }
            trect=new Rectangle(left,(h-chh)/2,cw,chh);
        }
        Rectangle irect=Rectangle.Empty;
        bool show=false;
        if(hasImage)
        {
            show=true;
            if(PictureSize==ImageSize.FillStretch || PictureSize==ImageSize.FillScaleUpDown)
            {
                if(PictureSize==ImageSize.FillStretch)
                    irect=_shape==ButtonShape.RoundFlat ? Rectangle.Inflate(client,-1,-1) : Rectangle.Inflate(client,-3,-3);
                else
                {
                    var availW=Math.Max(1,adjWidth-12); var availH=Math.Max(1,h-12);
                    var scale=Math.Min((double)availW/_picture!.Width,(double)availH/_picture.Height);
                    int iw=Math.Max(1,(int)Math.Round(_picture.Width*scale)), ih=Math.Max(1,(int)Math.Round(_picture.Height*scale));
                    irect=new Rectangle((adjWidth-iw)/2+lEdge,(h-ih)/2,iw,ih);
                }
            }
            else
            {
                int ix=lEdge+4, iy=(h-size)/2;
                switch(_pictureAlign)
                {
                    case ImagePlacement.LeftEdge: ix=lEdge+4; break;
                    case ImagePlacement.LeftOfCaption: ix=hasText ? trect.Left-4-size : lEdge+4; break;
                    case ImagePlacement.RightOfCaption: ix=hasText ? trect.Right+4 : rEdge-4-size; break;
                    case ImagePlacement.RightEdge: ix=rEdge-4-size; break;
                    case ImagePlacement.TopCenter:
                        iy=(h-(ch+size))/2; ix=(adjWidth-size)/2+lEdge;
                        if(hasText) trect.Y=iy+2+size;
                        break;
                    case ImagePlacement.BottomCenter:
                        iy=(h-(ch+size))/2+ch+4; ix=(adjWidth-size)/2+lEdge;
                        if(hasText) trect.Y=iy-2-ch;
                        break;
                }
                if(hasText && _pictureAlign<ImagePlacement.TopCenter) trect.Y=(h-ch)/2;
                irect=new Rectangle(ix,iy,size,size);
            }
        }
        else if(hasText) trect.Y=(h-ch)/2;
        // Comprobaciones de sanidad finales de VB6.
        if(hasText)
        {
            if(trect.Top<4) trect.Y=4;
            if(trect.Left<4+lEdge) trect.X=4+lEdge;
            if(trect.Right>rEdge-4) trect.X=rEdge-4-trect.Width;
            if(trect.Bottom>h-5) trect.Y=h-5-trect.Height;
        }
        if(show && PictureSize is not (ImageSize.FillStretch or ImageSize.FillScaleUpDown))
        {
            if(irect.Top<4) irect.Y=4;
            if(irect.Left<4+lEdge) irect.X=4+lEdge;
            if(irect.Right>rEdge-4) irect.X=rEdge-4-irect.Width;
            if(irect.Bottom>h-5) irect.Y=h-5-irect.Height;
        }
        return (trect,irect,show);
    }
    private void DrawContent(Graphics g, Rectangle client, CaptionColors colors, bool selected, Color back)
    {
        var fs = _fontStyle switch { FontStyles.Bold => System.Drawing.FontStyle.Bold, FontStyles.Italic => System.Drawing.FontStyle.Italic, FontStyles.Underline => System.Drawing.FontStyle.Underline, FontStyles.BoldItalic => System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic, FontStyles.BoldUnderline => System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Underline, FontStyles.ItalicUnderline => System.Drawing.FontStyle.Italic|System.Drawing.FontStyle.Underline, FontStyles.BoldItalicUnderline => System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic|System.Drawing.FontStyle.Underline, _ => System.Drawing.FontStyle.Regular };
        using var f = new Font(Font, fs);
        var text = Caption.Replace("||", Environment.NewLine);
        var offset=ContentOffset(selected);
        if (IsCustomShape)
        {
            // VB6: la imagen define la forma; sólo hay caption en modo CheckBox/OptionButton con Value=True.
            if (selected && _mode!=ButtonMode.CommandButton)
            {
                var custom=ComputeLayout(g,f,client,text);
                var tr=custom.Text; tr.Offset(offset);
                if(text.Length>0) DrawCaption(g,tr,f,text,colors,back);
                return;
            }
            if (_picture is null) return;
            try
            {
                // CustomClick=Default encoge la imagen al presionar; el resto desplaza el contenido.
                var dest = _pressed && _customClick==CustomClick.Default ? Rectangle.Inflate(client,-1,-1) : client;
                g.DrawImage(_picture,dest);
            }
            catch (ArgumentException) { }
            catch (ExternalException) { }
            return;
        }
        var layout=ComputeLayout(g,f,client,text);
        var textRect=layout.Text; textRect.Offset(offset);
        var imageRect=layout.Image; imageRect.Offset(offset);
        if (layout.ShowImage && _picture is not null)
        {
            // Si el usuario liberó la imagen, se omite el dibujo en vez de tumbar la aplicación.
            try { g.DrawImage(_picture,imageRect); }
            catch (ArgumentException) { }
            catch (ExternalException) { }
        }
        if(text.Length>0) DrawCaption(g,textRect,f,text,colors,back);
    }
    private void DrawCaption(Graphics g, Rectangle r, Font f, string text, CaptionColors colors, Color back)
    {
        var flags=TextFormatFlags.Left|TextFormatFlags.Top|TextFormatFlags.WordBreak|TextFormatFlags.NoPadding;
        if(Enabled && _captionStyle is CaptionStyle.Raised or CaptionStyle.Sunken)
        {
            // VB6: ShadeColor(bColor, +/-40) a -1,0 y +1,+1 antes del texto definitivo.
            int shadeOffset=_captionStyle==CaptionStyle.Raised?40:-40;
            var light=r; light.Offset(-1,0);
            TextRenderer.DrawText(g,text,f,light,Shade(back,shadeOffset),flags);
            var dark=r; dark.Offset(1,1);
            TextRenderer.DrawText(g,text,f,dark,Shade(back,-shadeOffset),flags);
        }
        if(colors.Shadow is Color shadow)
        {
            var sr=r; sr.Offset(colors.ShadowOffset);
            TextRenderer.DrawText(g,text,f,sr,shadow,flags);
        }
        TextRenderer.DrawText(g,text,f,r,colors.Color,flags);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ICONINFO { public bool fIcon; public int xHotspot; public int yHotspot; public IntPtr hbmMask; public IntPtr hbmColor; }
    [DllImport("user32.dll", SetLastError=true)] private static extern IntPtr CreateIconIndirect(ref ICONINFO info);
    [DllImport("user32.dll", SetLastError=true)] private static extern bool DestroyIcon(IntPtr hIcon);
    [DllImport("gdi32.dll")] private static extern IntPtr CreateBitmap(int width, int height, uint planes, uint bitsPerPixel, IntPtr bits);
    [DllImport("gdi32.dll")] private static extern bool DeleteObject(IntPtr hObject);
}
