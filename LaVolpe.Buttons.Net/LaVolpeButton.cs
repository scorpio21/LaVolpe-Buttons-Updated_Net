using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace LaVolpe.Buttons.Net;

public enum ImagePlacement { LeftEdge=0, LeftOfCaption=1, RightEdge=2, RightOfCaption=3, TopCenter=4, BottomCenter=5, lv_LeftEdge=LeftEdge, lv_LeftOfCaption=LeftOfCaption, lv_RightEdge=RightEdge, lv_RightOfCaption=RightOfCaption, lv_TopCenter=TopCenter, lv_BottomCenter=BottomCenter }
public enum ImageSize { Size16x16=0, Size24x24=1, Size32x32=2, FillStretch=3, FillScaleUpDown=4, lv_16x16=Size16x16, lv_24x24=Size24x24, lv_32x32=Size32x32, lv_Fill_Stretch=FillStretch, lv_Fill_ScaleUpDown=FillScaleUpDown }
public enum ButtonMode { CommandButton=0, CheckBox=1, OptionButton=2, lv_CommandButton=CommandButton, lv_CheckBox=CheckBox, lv_OptionButton=OptionButton }
public enum ButtonShape { Rectangular=0, LeftDiagonal=1, RightDiagonal=2, FullDiagonal=3, Round3D=4, Round3DFixed=5, RoundFlat=6, CustomFlat=7, Custom3DBorder=8, lv_Rectangular=Rectangular, lv_LeftDiagonal=LeftDiagonal, lv_RightDiagonal=RightDiagonal, lv_FullDiagonal=FullDiagonal, lv_Round3D=Round3D, lv_Round3DFixed=Round3DFixed, lv_RoundFlat=RoundFlat, lv_CustomFlat=CustomFlat, lv_Custom3DBorder=Custom3DBorder }
public enum HoverColorLocks { TextAndBackColor=0, TextColorOnly=1, BackColorOnly=2, NoLocks=3, LockTextandBackColor=TextAndBackColor, LockTextColorOnly=TextColorOnly, LockBackColorOnly=BackColorOnly }
public enum CustomClick { Default=0, North=1, NorthEast=2, NorthWest=3, SouthEast=4, SouthWest=5, East=6, South=7, West=8, lv_Default=Default, lv_North=North, lv_NorthEast=NorthEast, lv_NorthWest=NorthWest, lv_SouthEast=SouthEast, lv_SouthWest=SouthWest, lv_East=East, lv_South=South, lv_West=West }
public enum GradientMode { None=0, LeftToRight=1, RightToLeft=2, TopToBottom=3, BottomToTop=4, NoGradient=None, Left2Right=LeftToRight, Right2Left=RightToLeft, Top2Bottom=TopToBottom, Bottom2Top=BottomToTop }
public enum CaptionStyle { Default=0, Sunken=1, Raised=2, lv_Default=Default, lv_Sunken=Sunken, lv_Raised=Raised }
[Flags] public enum FontStyles { PlainStyle=0, Bold=2, Italic=4, Underline=8, BoldItalic=6, BoldUnderline=10, ItalicUnderline=12, BoldItalicUnderline=14 }
public enum BackStyle { Windows95=0, Windows31=1, WindowsXp=2, Java=3, Flat=4, Hover=5, Netscape=6, Macintosh=7, w95=Windows95, w31=Windows31, XP=WindowsXp, hover=Hover }

public delegate void LvMouseEventHandler(object sender, MouseEventArgs e);
public delegate void LvKeyEventHandler(object sender, KeyEventArgs e);

[ToolboxItem(true), DesignTimeVisible(true), DesignerCategory("Component"), DefaultEvent(nameof(Click)), DefaultProperty(nameof(Caption))]
public class LaVolpeButton : Control
{
    private bool _hovered, _pressed, _value, _delayDrawing, _resetDefaultColors;
    private string _caption = "";
    private Color _normalBack = SystemColors.Control, _hoverBack = Color.FromArgb(220,230,245), _gradientColor = SystemColors.ControlLight, _hoverFore = Color.Black;
    private Image? _picture, _mouseIcon;
    private ButtonMode _mode; private ButtonShape _shape; private BackStyle _style = BackStyle.WindowsXp; private GradientMode _gradientMode;
    private FontStyles _fontStyle;
    private ImagePlacement _pictureAlign = ImagePlacement.LeftOfCaption;
    private ImageSize _pictureSize = ImageSize.Size16x16;
    private ContentAlignment _captionAlign = ContentAlignment.MiddleCenter;
    private CaptionStyle _captionStyle = CaptionStyle.Default;
    private HoverColorLocks _hoverColorLocks = HoverColorLocks.NoLocks;
    private CustomClick _customClick = CustomClick.Default;
    private bool _showFocusRect = true, _cancel, _default, _dragMode;
    private Point _dragStart;
    private int _helpContextId, _index, _whatsThisHelpId;
    private string _toolTipText = "";
    private ToolTip? _toolTip;
    private Cursor? _mouseIconCursor;
    private readonly System.Windows.Forms.Timer _hoverTimer;

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

    public LaVolpeButton() { SetStyle(ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer|ControlStyles.ResizeRedraw|ControlStyles.Selectable,true); Size=new Size(90,33); Font=new Font("Times New Roman",9f); TabStop=true; AllowDrop=true; AccessibleRole=AccessibleRole.PushButton; _hoverTimer=new System.Windows.Forms.Timer { Interval=50 }; _hoverTimer.Tick += (_,_) => TimerUpdate(0); _hoverTimer.Start(); }
    [Category("LaVolpe"), DefaultValue("")] public string Caption { get=>_caption; set{_caption=value??""; if(string.IsNullOrWhiteSpace(AccessibleName))AccessibleName=_caption.Replace("&",""); InvalidateIfNeeded();} }
    [Category("LaVolpe")] public ButtonMode Mode { get=>_mode; set{_mode=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public ButtonShape ButtonShape { get=>_shape; set{_shape=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public BackStyle ButtonStyle { get=>_style; set{_style=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public bool Value { get=>_value; set=>SetValue(value); }
    [Browsable(false)] public bool Checked { get=>_value; set=>SetValue(value); }
    [Browsable(false)] public bool Selected { get=>_value; set=>SetValue(value); }
    [Category("Appearance")]
    public override Color BackColor { get => _normalBack; set { _normalBack = value; base.BackColor = value; InvalidateIfNeeded(); } }
    [Browsable(false)] public Color BackColorButton { get => BackColor; set => BackColor = value; }
    [Category("LaVolpe")] public Color HoverBackColor { get=>_hoverBack; set{_hoverBack=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Color HoverForeColor { get=>_hoverFore; set{_hoverFore=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Color GradientColor { get=>_gradientColor; set{_gradientColor=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public GradientMode GradientMode { get=>_gradientMode; set{_gradientMode=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Image? Picture { get=>_picture; set{_picture=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public ImagePlacement PictureAlign { get=>_pictureAlign; set{_pictureAlign=value;InvalidateIfNeeded();} }
    [Category("LaVolpe")] public ImageSize PictureSize { get=>_pictureSize; set{_pictureSize=value;InvalidateIfNeeded();} }
    [Category("LaVolpe")] public ContentAlignment CaptionAlign { get=>_captionAlign; set{_captionAlign=value;InvalidateIfNeeded();} }
    [Category("LaVolpe")] public CaptionStyle CaptionStyle { get=>_captionStyle; set{_captionStyle=value;InvalidateIfNeeded();} }
    [Category("LaVolpe")] public HoverColorLocks HoverColorLocks { get=>_hoverColorLocks; set{_hoverColorLocks=value;InvalidateIfNeeded();} }
    [Category("LaVolpe")] public CustomClick CustomClick { get=>_customClick; set{_customClick=value;InvalidateIfNeeded();} }
    [Category("LaVolpe")] public FontStyles FontStyle { get=>_fontStyle; set{_fontStyle=value; InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Image? MouseIcon { get=>_mouseIcon; set{_mouseIcon=value;ApplyMouseIcon();InvalidateIfNeeded();} }
    [Category("LaVolpe")] public Cursor MousePointer { get => Cursor; set => Cursor = value ?? Cursors.Default; }
    [Category("LaVolpe"), DefaultValue("")] public string ToolTipText { get=>_toolTipText; set{_toolTipText=value??"";(_toolTip ??= new ToolTip()).SetToolTip(this,_toolTipText); }}
    [Category("LaVolpe")] public bool ShowFocusRect { get=>_showFocusRect; set{_showFocusRect=value;InvalidateIfNeeded();} }
    [Category("LaVolpe")] public bool DelayDrawing { get=>_delayDrawing; set{_delayDrawing=value; if(!value) Invalidate();} }
    [Category("Behavior"), DefaultValue(false)] public bool Cancel { get=>_cancel; set=>_cancel=value; }
    [Category("Behavior"), DefaultValue(false)] public bool Default { get=>_default; set=>_default=value; }
    [Category("Behavior"), DefaultValue(false)] public bool DragMode { get=>_dragMode; set=>_dragMode=value; }
    [Category("Design"), DefaultValue(0)] public int Index { get=>_index; set=>_index=value; }
    [Category("Help"), DefaultValue(0)] public int HelpContextID { get=>_helpContextId; set=>_helpContextId=value; }
    [Category("Help"), DefaultValue(0)] public int WhatsThisHelpID { get=>_whatsThisHelpId; set=>_whatsThisHelpId=value; }
    [Browsable(false)] public bool MouseOver=>_hovered;
    [Browsable(false)] public IntPtr hwnd=>Handle;
    [Browsable(false)] public IntPtr hDC { get { using var g=CreateGraphics(); return g.GetHdc(); } }
    [Category("LaVolpe")] public bool ResetDefaultColors { get => _resetDefaultColors; set { _resetDefaultColors = value; if (value) ResetDefaultColorsNow(); } }
    public void ResetDefaultColorsNow(){_normalBack=SystemColors.Control;_hoverBack=Color.FromArgb(220,230,245);_gradientColor=SystemColors.ControlLight;_hoverFore=Color.Black;_resetDefaultColors=false;InvalidateIfNeeded();}
    private void ApplyMouseIcon(){
        if(_mouseIcon is null){_mouseIconCursor?.Dispose();_mouseIconCursor=null;return;}
        using var bitmap = new Bitmap(_mouseIcon);
        _mouseIconCursor?.Dispose();
        _mouseIconCursor = new Cursor(bitmap.GetHicon());
        Cursor = _mouseIconCursor;
    }
    private void SetValue(bool value){if(_value==value)return;_value=value;ValueChanged?.Invoke(this,EventArgs.Empty);if(_mode==ButtonMode.CheckBox)CheckedChanged?.Invoke(this,EventArgs.Empty);if(_mode==ButtonMode.OptionButton)SelectedChanged?.Invoke(this,EventArgs.Empty);InvalidateIfNeeded();}
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

    protected override void OnEnabledChanged(EventArgs e){base.OnEnabledChanged(e);InvalidateIfNeeded();}
    protected override void OnResize(EventArgs e){base.OnResize(e);InvalidateIfNeeded();}
    protected override bool ProcessDialogKey(Keys keyData){
        var key = keyData & Keys.KeyCode;
        if(Mode==ButtonMode.CommandButton && key==Keys.Escape && Cancel){PerformClick();return true;}
        if(Mode==ButtonMode.CommandButton && key==Keys.Enter && Default){PerformClick();return true;}
        if(key is Keys.Right or Keys.Down){Parent?.SelectNextControl(this,true,true,true,true);return true;}
        if(key is Keys.Left or Keys.Up){Parent?.SelectNextControl(this,false,true,true,true);return true;}
        return base.ProcessDialogKey(keyData);
    }
    protected override bool ProcessMnemonic(char charCode){if(!CanSelect)return false;var mnemonic=FindMnemonic(Caption);if(mnemonic != '\0' && char.ToUpperInvariant(mnemonic)==char.ToUpperInvariant(charCode)){Focus();PerformClick();return true;}return base.ProcessMnemonic(charCode);}
    private static char FindMnemonic(string text){for(int i=0;i<text.Length-1;i++){if(text[i]=='&'){if(text[i+1]=='&'){i++;continue;}return text[i+1];}}return '\0';}
    protected override void Dispose(bool disposing){if(disposing){_hoverTimer.Stop();_hoverTimer.Dispose();_toolTip?.Dispose();_mouseIconCursor?.Dispose();}base.Dispose(disposing);}
    protected override void OnMouseEnter(EventArgs e){base.OnMouseEnter(e);TimerUpdate(0);}
    protected override void OnMouseLeave(EventArgs e){base.OnMouseLeave(e);if(_hovered){_hovered=false;MouseOnButton?.Invoke(this,false);} _pressed=false;InvalidateIfNeeded();}
    protected override void OnMouseDown(MouseEventArgs e){base.OnMouseDown(e);LvMouseDown?.Invoke(this,e);if(e.Button==MouseButtons.Left&&Enabled){_dragStart=e.Location;_pressed=true;Focus();InvalidateIfNeeded();}}
    protected override void OnMouseUp(MouseEventArgs e){base.OnMouseUp(e);LvMouseUp?.Invoke(this,e);if(e.Button==MouseButtons.Left&&_pressed){_pressed=false;using(var path=CreatePath(ClientRectangle)) if(path.IsVisible(e.Location))PerformClick();InvalidateIfNeeded();}}
    protected override void OnMouseMove(MouseEventArgs e){base.OnMouseMove(e);LvMouseMove?.Invoke(this,e);if(_dragMode&&e.Button==MouseButtons.Left&&Enabled){var dx=Math.Abs(e.X-_dragStart.X);var dy=Math.Abs(e.Y-_dragStart.Y);if(dx>=SystemInformation.DragSize.Width/2||dy>=SystemInformation.DragSize.Height/2){_pressed=false;OLEStartDrag?.Invoke(this,new DragEventArgs(new DataObject(Caption),0,e.X,e.Y,DragDropEffects.Copy,DragDropEffects.Copy));var effect=DoDragDrop(Caption,DragDropEffects.Copy);OLECompleteDrag?.Invoke(this,EventArgs.Empty);InvalidateIfNeeded();}}}
    protected override void OnMouseDoubleClick(MouseEventArgs e){base.OnMouseDoubleClick(e);ButtonDoubleClick?.Invoke(this,(int)e.Button);}
    protected override void OnKeyDown(KeyEventArgs e){base.OnKeyDown(e);LvKeyDown?.Invoke(this,e);if(e.KeyCode is Keys.Space or Keys.Enter){_pressed=true;InvalidateIfNeeded();}}
    protected override void OnKeyUp(KeyEventArgs e){base.OnKeyUp(e);LvKeyUp?.Invoke(this,e);if(e.KeyCode is Keys.Space or Keys.Enter){_pressed=false;PerformClick();InvalidateIfNeeded();}}
    protected override void OnKeyPress(KeyPressEventArgs e){base.OnKeyPress(e);LvKeyPress?.Invoke(this,e);}
    protected override void OnClick(EventArgs e){if(_mode==ButtonMode.CheckBox)SetValue(!_value);else if(_mode==ButtonMode.OptionButton&&Parent!=null){foreach(Control c in Parent.Controls)if(c is LaVolpeButton b&&b!=this&&b.Mode==ButtonMode.OptionButton)b.SetValue(false);SetValue(true);}base.OnClick(e);InvalidateIfNeeded();}
    public void PerformClick(){if(Enabled)OnClick(EventArgs.Empty);}
    protected override void OnDragDrop(DragEventArgs drgevent){base.OnDragDrop(drgevent);OLEDragDrop?.Invoke(this,drgevent);}
    protected override void OnDragOver(DragEventArgs drgevent){base.OnDragOver(drgevent);OLEDragOver?.Invoke(this,drgevent);}
    protected override void OnGiveFeedback(GiveFeedbackEventArgs e){base.OnGiveFeedback(e);OLEGiveFeedback?.Invoke(this,e);}
    protected override void OnDragEnter(DragEventArgs e){base.OnDragEnter(e);}

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e); var g=e.Graphics; g.SmoothingMode=SmoothingMode.AntiAlias; g.InterpolationMode=InterpolationMode.HighQualityBicubic; g.PixelOffsetMode=PixelOffsetMode.HighQuality;
        var r=ClientRectangle; r.Width--; r.Height--; using var path=CreatePath(r);
        var palette=GetStylePalette();
        var hovering = _hovered || _value;
        var useHoverBack = hovering && _hoverColorLocks is not (HoverColorLocks.BackColorOnly or HoverColorLocks.TextAndBackColor);
        var back = useHoverBack ? (_style==BackStyle.Hover ? palette.hover : _hoverBack) : palette.normal;
        if(_pressed) back=ControlPaint.Dark(back,.15f); if(!Enabled) back=ControlPaint.Light(back,.25f);
        using var brush=CreateBrush(r,back); g.FillPath(brush,path);
        using var pen=new Pen(palette.border, _style is BackStyle.Flat or BackStyle.Hover || _shape==ButtonShape.RoundFlat ? 1 : 2); g.DrawPath(pen,path);
        if(_pressed) using(var pressedPen=new Pen(palette.dark,1)) g.DrawPath(pressedPen,RectanglePath(r));
        var contentRect = r;
        if (_pressed) contentRect.Offset(CustomClickOffset());
        var useHoverFore = hovering && _hoverColorLocks is not (HoverColorLocks.TextColorOnly or HoverColorLocks.TextAndBackColor);
        var contentColor = !Enabled ? SystemColors.GrayText : (useHoverFore ? _hoverFore : ForeColor);
        var clipState = g.Save();
        g.SetClip(path, CombineMode.Intersect);
        DrawContent(g,contentRect,contentColor);
        g.Restore(clipState);
        if(Focused&&ShowFocusRect) ControlPaint.DrawFocusRectangle(g,Rectangle.Inflate(r,-4,-4));
    }
    private Point CustomClickOffset() => CustomClick switch { CustomClick.North => new Point(0,-1), CustomClick.NorthEast => new Point(1,-1), CustomClick.NorthWest => new Point(-1,-1), CustomClick.SouthEast => new Point(1,1), CustomClick.SouthWest => new Point(-1,1), CustomClick.East => new Point(1,0), CustomClick.South => new Point(0,1), CustomClick.West => new Point(-1,0), _ => new Point(1,1) };
    private GraphicsPath RectanglePath(Rectangle r){var p=new GraphicsPath();p.AddRectangle(Rectangle.Inflate(r,-2,-2));return p;}
    private (Color normal, Color hover, Color border, Color dark) GetStylePalette() => _style switch
    {
        BackStyle.Windows95 => (SystemColors.Control, Color.FromArgb(210,220,235), Color.Black, SystemColors.ControlDark),
        BackStyle.Windows31 => (Color.FromArgb(192,192,192), Color.FromArgb(210,210,210), Color.Black, Color.FromArgb(96,96,96)),
        BackStyle.WindowsXp => (Color.FromArgb(236,242,254), Color.FromArgb(205,225,252), Color.FromArgb(49,106,197), Color.FromArgb(31,78,121)),
        BackStyle.Java => (Color.FromArgb(238,238,238), Color.FromArgb(250,250,250), Color.FromArgb(128,128,128), Color.FromArgb(80,80,80)),
        BackStyle.Flat => (_normalBack, _hoverBack, Color.Black, ControlPaint.Dark(_normalBack)),
        BackStyle.Hover => (_normalBack, _hoverBack, ControlPaint.Dark(_hoverBack), ControlPaint.Dark(_normalBack)),
        BackStyle.Netscape => (Color.FromArgb(214,214,214), Color.FromArgb(235,235,235), Color.FromArgb(90,90,90), Color.FromArgb(120,120,120)),
        BackStyle.Macintosh => (Color.FromArgb(221,221,221), Color.FromArgb(245,245,245), Color.FromArgb(75,75,75), Color.FromArgb(130,130,130)),
        _ => (_normalBack, _hoverBack, ControlPaint.Dark(_normalBack), ControlPaint.DarkDark(_normalBack))
    };
    private GraphicsPath CreatePath(Rectangle r){var p=new GraphicsPath();switch(_shape){case ButtonShape.LeftDiagonal:p.AddPolygon(new[]{new Point(r.Left+8,r.Top),new Point(r.Right,r.Top),new Point(r.Right,r.Bottom),new Point(r.Left+8,r.Bottom)});break;case ButtonShape.RightDiagonal:p.AddPolygon(new[]{new Point(r.Left,r.Top),new Point(r.Right-8,r.Top),new Point(r.Right-8,r.Bottom),new Point(r.Left,r.Bottom)});break;case ButtonShape.FullDiagonal:p.AddPolygon(new[]{new Point(r.Left+8,r.Top),new Point(r.Right-8,r.Top),new Point(r.Right,r.Bottom),new Point(r.Left,r.Bottom)});break;case ButtonShape.Round3D:case ButtonShape.Round3DFixed:case ButtonShape.RoundFlat:p.AddArc(r.X,r.Y,12,12,180,90);p.AddArc(r.Right-12,r.Y,12,12,270,90);p.AddArc(r.Right-12,r.Bottom-12,12,12,0,90);p.AddArc(r.X,r.Bottom-12,12,12,90,90);p.CloseFigure();break;default:p.AddRectangle(r);break;}return p;}
    private Brush CreateBrush(Rectangle r,Color back){if(_gradientMode==GradientMode.None)return new SolidBrush(back);var mode=_gradientMode is GradientMode.LeftToRight or GradientMode.RightToLeft?LinearGradientMode.Horizontal:LinearGradientMode.Vertical;var b=new LinearGradientBrush(r,back,_gradientColor,mode);if(_gradientMode is GradientMode.RightToLeft or GradientMode.BottomToTop)b.RotateTransform(180);return b;}
    private void DrawContent(Graphics g, Rectangle r, Color color)
    {
        var fs = _fontStyle switch { FontStyles.Bold => System.Drawing.FontStyle.Bold, FontStyles.Italic => System.Drawing.FontStyle.Italic, FontStyles.Underline => System.Drawing.FontStyle.Underline, FontStyles.BoldItalic => System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic, FontStyles.BoldUnderline => System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Underline, FontStyles.ItalicUnderline => System.Drawing.FontStyle.Italic|System.Drawing.FontStyle.Underline, FontStyles.BoldItalicUnderline => System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic|System.Drawing.FontStyle.Underline, _ => System.Drawing.FontStyle.Regular };
        using var f = new Font(Font, fs);
        var flags = CaptionAlign switch { ContentAlignment.TopLeft => TextFormatFlags.Top|TextFormatFlags.Left, ContentAlignment.TopCenter => TextFormatFlags.Top|TextFormatFlags.HorizontalCenter, ContentAlignment.TopRight => TextFormatFlags.Top|TextFormatFlags.Right, ContentAlignment.MiddleLeft => TextFormatFlags.VerticalCenter|TextFormatFlags.Left, ContentAlignment.MiddleRight => TextFormatFlags.VerticalCenter|TextFormatFlags.Right, ContentAlignment.BottomLeft => TextFormatFlags.Bottom|TextFormatFlags.Left, ContentAlignment.BottomRight => TextFormatFlags.Bottom|TextFormatFlags.Right, _ => TextFormatFlags.VerticalCenter|TextFormatFlags.HorizontalCenter } | TextFormatFlags.EndEllipsis;
        if (_mode is ButtonMode.CheckBox or ButtonMode.OptionButton)
        {
            const int indicator = 14;
            var box = new Rectangle(r.X + 8, r.Y + (r.Height - indicator) / 2, indicator, indicator);
            using var indicatorPen = new Pen(color, 1.5f);
            if (_mode == ButtonMode.CheckBox)
            {
                g.DrawRectangle(indicatorPen, box);
                if (_value) { using var checkPen = new Pen(color, 2f); g.DrawLine(checkPen, box.Left + 3, box.Top + 7, box.Left + 6, box.Bottom - 3); g.DrawLine(checkPen, box.Left + 6, box.Bottom - 3, box.Right - 2, box.Top + 3); }
            }
            else
            {
                g.DrawEllipse(indicatorPen, box);
                if (_value) using (var dot = new SolidBrush(color)) g.FillEllipse(dot, Rectangle.Inflate(box, -4, -4));
            }
            r.X += indicator + 14;
            r.Width -= indicator + 14;
        }
        if (_picture != null)
        {
            int s = PictureSize switch { ImageSize.Size24x24 => 24, ImageSize.Size32x32 => 32, _ => 16 };
            if (PictureSize == ImageSize.FillStretch) { g.DrawImage(_picture, r); }
            else
            {
                int iw=s, ih=s;
                if (PictureSize == ImageSize.FillScaleUpDown)
                {
                    var scale = Math.Min((double)r.Width / _picture.Width, (double)r.Height / _picture.Height);
                    iw = Math.Max(1, (int)Math.Round(_picture.Width * scale));
                    ih = Math.Max(1, (int)Math.Round(_picture.Height * scale));
                }
                var ir = PictureAlign switch { ImagePlacement.RightEdge => new Rectangle(r.Right-iw-5, r.Y+(r.Height-ih)/2, iw, ih), ImagePlacement.TopCenter => new Rectangle(r.X+(r.Width-iw)/2, r.Y+3, iw, ih), ImagePlacement.BottomCenter => new Rectangle(r.X+(r.Width-iw)/2, r.Bottom-ih-3, iw, ih), _ => new Rectangle(r.X+5, r.Y+(r.Height-ih)/2, iw, ih) };
                g.DrawImage(_picture, ir);
                if (PictureAlign is ImagePlacement.LeftOfCaption or ImagePlacement.LeftEdge) { r.X += iw+8; r.Width -= iw+8; }
                else if (PictureAlign == ImagePlacement.RightOfCaption) r.Width -= iw+8;
            }
        }
        var text = Caption.Replace("||", Environment.NewLine);
        if (CaptionStyle == CaptionStyle.Raised)
        {
            var shadow = r; shadow.Offset(1, 1);
            TextRenderer.DrawText(g, text, f, shadow, Color.White, flags);
        }
        else if (CaptionStyle == CaptionStyle.Sunken)
        {
            var shadow = r; shadow.Offset(1, 1);
            TextRenderer.DrawText(g, text, f, shadow, ControlPaint.Dark(color), flags);
        }
        TextRenderer.DrawText(g, text, f, r, color, flags);
    }
}
