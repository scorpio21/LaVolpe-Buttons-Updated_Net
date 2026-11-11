using LaVolpe.Buttons.Net;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LaVolpe.Buttons.Tests;

/// <summary>
/// Pruebas de compatibilidad ejecutables, sin dependencias externas.
/// Se ejecutan sobre WinForms real (STA) y devuelven código de salida 1 si algo falla.
/// </summary>
internal static class Program
{
    private static int _passed;
    private static readonly List<string> _failed = new();
    private static readonly List<IDisposable> _attached = new();
    private static string _current = "";

    [STAThread]
    private static int Main(string[] args)
    {
        if (args.Length == 2 && args[0].Equals("--visual-snapshot", StringComparison.OrdinalIgnoreCase))
            return GenerateVisualSnapshots(args[1]);

        if (args.Length == 3 && args[0].Equals("--visual-compare", StringComparison.OrdinalIgnoreCase))
            return CompareVisualSnapshots(args[1], args[2]);

        Application.EnableVisualStyles();
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("LaVolpe.Buttons.Tests - pruebas de compatibilidad");
        Console.WriteLine(new string('-', 70));

        Test("Text y Caption sincronizados", form =>
        {
            var button = Add(form, new LaVolpeButton { Caption = "Hola" });
            Check(button.Text == "Hola", $"Text='{button.Text}'");
            button.Text = "Adios";
            Check(button.Caption == "Adios", $"Caption='{button.Caption}'");
            button.Caption = "Otra vez";
            Check(button.Text == "Otra vez", $"Text='{button.Text}'");
        });

        Test("Caption nulo se normaliza a cadena vacía", form =>
        {
            var button = Add(form, new LaVolpeButton { Caption = "x" });
            button.Caption = null!;
            Check(button.Caption.Length == 0, $"Caption='{button.Caption}'");
        });

        Test("Valores por defecto de VB6", form =>
        {
            var button = Add(form, new LaVolpeButton());
            Check(button.ButtonStyle == BackStyle.Windows95, $"ButtonStyle={button.ButtonStyle}");
            Check(button.HoverColorLocks == HoverColorLocks.TextAndBackColor, $"HoverColorLocks={button.HoverColorLocks}");
            Check(button.HoverBackColor == button.BackColor, $"{button.HoverBackColor} != {button.BackColor}");
            Check(button.Mode == ButtonMode.CommandButton, $"Mode={button.Mode}");
        });

        Test("Opciones de diseñador WinForms", form =>
        {
            var button = Add(form, new LaVolpeButton());
            var properties = TypeDescriptor.GetProperties(button);
            var vb6Properties = new[]
            {
                "BackColor", "ButtonShape", "ButtonStyle", "Cancel", "Caption", "CaptionAlign", "CaptionStyle",
                "CustomClick", "Default", "DragIcon", "DragMode", "Enabled", "Font", "FontStyle", "ForeColor",
                "GradientColor", "GradientMode", "Height", "HelpContextID", "HoverBackColor", "HoverColorLocks",
                "HoverForeColor", "Index", "Left", "Mode", "MouseIcon", "MousePointer", "Picture", "PictureAlign",
                "PictureSize", "ResetDefaultColors", "ShowFocusRect", "TabIndex", "TabStop", "Tag", "ToolTipText",
                "Top", "Value", "Visible", "WhatsThisHelpID", "Width"
            };
            foreach (var name in vb6Properties)
                Check(properties[name] is not null, $"{name} no aparece en PropertyGrid");
            Check(properties[nameof(LaVolpeButton.Caption)] is not null, "Caption no aparece en PropertyGrid");
            Check(properties[nameof(LaVolpeButton.ButtonStyle)] is not null, "ButtonStyle no aparece en PropertyGrid");
            var styleText = properties[nameof(LaVolpeButton.ButtonStyle)]!.Converter.ConvertTo(BackStyle.WindowsXp, typeof(string));
            var modeText = properties[nameof(LaVolpeButton.Mode)]!.Converter.ConvertTo(ButtonMode.CheckBox, typeof(string));
            var shapeText = properties[nameof(LaVolpeButton.ButtonShape)]!.Converter.ConvertTo(ButtonShape.Custom3DBorder, typeof(string));
            var imageAlignText = properties[nameof(LaVolpeButton.PictureAlign)]!.Converter.ConvertTo(ImagePlacement.TopCenter, typeof(string));
            var imageSizeText = properties[nameof(LaVolpeButton.PictureSize)]!.Converter.ConvertTo(ImageSize.FillScaleUpDown, typeof(string));
            var pointerText = properties[nameof(LaVolpeButton.MousePointer)]!.Converter.ConvertTo(MousePointerConstants.Custom, typeof(string));
            Check(styleText?.ToString() == "lv_XP", $"BackStyle={styleText}");
            Check(modeText?.ToString() == "lv_CheckBox", $"Mode={modeText}");
            Check(shapeText?.ToString() == "lv_Custom3DBorder", $"ButtonShape={shapeText}");
            Check(imageAlignText?.ToString() == "lv_TopCenter", $"PictureAlign={imageAlignText}");
            Check(imageSizeText?.ToString() == "lv_Fill_ScaleUpDown", $"PictureSize={imageSizeText}");
            Check(pointerText?.ToString() == "vbCustom", $"MousePointer={pointerText}");
            Check(properties[nameof(LaVolpeButton.ButtonStyle)]!.Converter.GetStandardValuesSupported(), "ButtonStyle no tiene lista desplegable");
            var styleContext = new PropertyContext(button, properties[nameof(LaVolpeButton.ButtonStyle)]!);
            var styleValues = properties[nameof(LaVolpeButton.ButtonStyle)]!.Converter.GetStandardValues(styleContext)!.Cast<BackStyle>().ToArray();
            Check(styleValues.Length == 8, $"ButtonStyle muestra {styleValues.Length} valores en vez de 8");
            Check(styleValues.Select(value => (int)value).Distinct().Count() == styleValues.Length, "ButtonStyle contiene valores repetidos");
            var shapeContext = new PropertyContext(button, properties[nameof(LaVolpeButton.ButtonShape)]!);
            var shapeValues = properties[nameof(LaVolpeButton.ButtonShape)]!.Converter.GetStandardValues(shapeContext)!.Cast<ButtonShape>().ToArray();
            Check(shapeValues.Length == 9, $"ButtonShape muestra {shapeValues.Length} valores en vez de 9");
            Check(shapeValues.Select(value => (int)value).Distinct().Count() == shapeValues.Length, "ButtonShape contiene valores repetidos");
            Check(TypeDescriptor.GetDefaultProperty(button)?.Name == nameof(LaVolpeButton.Caption), "DefaultProperty no es Caption");
            Check(TypeDescriptor.GetDefaultEvent(button)?.Name == nameof(LaVolpeButton.Click), "DefaultEvent no es Click");
            Check(properties[nameof(LaVolpeButton.Caption)]!.SerializationVisibility == DesignerSerializationVisibility.Visible, "Caption no es serializable");
            Check(!properties[nameof(LaVolpeButton.Caption)]!.ShouldSerializeValue(button), "Caption predeterminado debe omitirse");
            button.Caption = "Designer button";
            Check(properties[nameof(LaVolpeButton.Caption)]!.ShouldSerializeValue(button), "Caption modificado no se serializa");
            Check(!properties[nameof(LaVolpeButton.ButtonStyle)]!.ShouldSerializeValue(button), "ButtonStyle predeterminado debe omitirse");
            button.ButtonStyle = BackStyle.WindowsXp;
            Check(properties[nameof(LaVolpeButton.ButtonStyle)]!.ShouldSerializeValue(button), "ButtonStyle modificado no se serializa");
            button.ButtonShape = ButtonShape.LeftDiagonal;
            Check(properties[nameof(LaVolpeButton.ButtonShape)]!.ShouldSerializeValue(button), "ButtonShape modificado no se serializa");
            Check(button.DragMode == DragModeConstants.vbManual, $"DragMode={button.DragMode}");
            button.DragMode = DragModeConstants.vbAutomatic;
            Check(button.DragMode == DragModeConstants.vbAutomatic, $"DragMode={button.DragMode}");
        });

        Test("BackColor hereda del contenedor", form =>
        {
            form.BackColor = Color.FromArgb(230, 230, 230);
            var button = Add(form, new LaVolpeButton());
            Check(button.BackColor == form.BackColor, $"{button.BackColor} != {form.BackColor}");
        });

        Test("CommandButton rechaza Value", form =>
        {
            var button = Add(form, new LaVolpeButton());
            button.Value = true;
            Check(!button.Value, $"Value={button.Value}");
        });

        Test("PerformClick dispara Click y DialogResult", form =>
        {
            var button = Add(form, new LaVolpeButton { DialogResult = DialogResult.OK });
            var clicks = 0;
            button.Click += (_, _) => clicks++;
            button.PerformClick();
            Check(clicks == 1, $"clicks={clicks}");
            Check(form.DialogResult == DialogResult.OK, $"DialogResult={form.DialogResult}");
        });

        Test("PerformClick respeta Enabled", form =>
        {
            var button = Add(form, new LaVolpeButton { Enabled = false });
            var clicks = 0;
            button.Click += (_, _) => clicks++;
            button.PerformClick();
            Check(clicks == 0, $"clicks={clicks}");
        });

        Test("OptionButton deselecciona hermanos", form =>
        {
            var first = Add(form, new LaVolpeButton { Mode = ButtonMode.OptionButton });
            var second = Add(form, new LaVolpeButton { Mode = ButtonMode.OptionButton });
            first.Value = true;
            second.Value = true;
            Check(!first.Value && second.Value, $"first={first.Value} second={second.Value}");
        });

        Test("Clic en opción ya elegida no dispara Click", form =>
        {
            var button = Add(form, new LaVolpeButton { Mode = ButtonMode.OptionButton });
            var clicks = 0;
            button.Click += (_, _) => clicks++;
            button.Value = true;
            button.PerformClick();
            Check(clicks == 0, $"clicks={clicks}");
        });

        Test("GotFocus selecciona OptionButton", form =>
        {
            var button = Add(form, new LaVolpeButton { Mode = ButtonMode.OptionButton });
            form.Activate();
            Application.DoEvents();
            button.Focus();
            Application.DoEvents();
            Check(button.Value, $"Value={button.Value} Focused={button.Focused}");
        });

        Test("CheckBox alterna Value", form =>
        {
            var button = Add(form, new LaVolpeButton { Mode = ButtonMode.CheckBox });
            button.PerformClick();
            Check(button.Value, "primer clic");
            button.PerformClick();
            Check(!button.Value, "segundo clic");
        });

        Test("IButtonControl y NotifyDefault", form =>
        {
            var button = Add(form, new LaVolpeButton { DialogResult = DialogResult.Yes });
            Check(((IButtonControl)button).DialogResult == DialogResult.Yes);
            button.NotifyDefault(true);
            Check(button.Default, "Default no se activó");
            button.NotifyDefault(false);
            Check(!button.Default, "Default no se desactivó");
        });

        Test("Default y Cancel enlazan el formulario", form =>
        {
            var button = Add(form, new LaVolpeButton { DialogResult = DialogResult.OK });
            button.Default = true;
            button.Cancel = true;
            Check(ReferenceEquals(form.AcceptButton, button), "AcceptButton no enlazado");
            Check(ReferenceEquals(form.CancelButton, button), "CancelButton no enlazado");
            button.Default = false;
            button.Cancel = false;
            Check(form.AcceptButton is null, "AcceptButton no desenlazado");
            Check(form.CancelButton is null, "CancelButton no desenlazado");
        });

        Test("Región para formas no rectangulares", form =>
        {
            var shapes = new[]
            {
                ButtonShape.LeftDiagonal, ButtonShape.RightDiagonal, ButtonShape.FullDiagonal,
                ButtonShape.Round3D, ButtonShape.Round3DFixed, ButtonShape.RoundFlat
            };
            foreach (var shape in shapes)
            {
                var button = Add(form, new LaVolpeButton { ButtonShape = shape, Size = new Size(120, 40) });
                Check(button.Region is not null, $"{shape}: Region=null");
            }
        });

        Test("Regiones conservadas en escalas equivalentes a DPI", form =>
        {
            foreach (var dpi in new[] { 96, 120, 144, 192 })
            {
                var scale = dpi / 96f;
                foreach (var shape in new[] { ButtonShape.LeftDiagonal, ButtonShape.RightDiagonal, ButtonShape.FullDiagonal, ButtonShape.Round3D })
                {
                    var size = new Size((int)Math.Round(120 * scale), (int)Math.Round(40 * scale));
                    var button = Add(form, new LaVolpeButton
                    {
                        ButtonShape = shape,
                        Size = size,
                        Font = new Font("Arial", 9.75f * scale),
                        Caption = $"DPI {dpi}"
                    });
                    using var bitmap = new Bitmap(size.Width, size.Height);
                    button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, size));
                    Check(button.Region is not null, $"DPI={dpi} {shape}: Region=null");
                    Check(button.Region!.IsVisible(size.Width / 2, size.Height / 2), $"DPI={dpi} {shape}: centro fuera de región");
                }
            }
        });

        Test("Rectangular XP recorta las esquinas", form =>
        {
            var button = Add(form, new LaVolpeButton { ButtonStyle = BackStyle.WindowsXp, Size = new Size(120, 40) });
            Check(button.Region is not null && !button.Region!.IsVisible(0, 0), button.Region is null ? "Region=null" : "esquina (0,0) visible");
        });

        Test("CustomFlat usa la imagen como máscara", form =>
        {
            var image = new Bitmap(60, 40);
            _attached.Add(image);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.Clear(Color.Magenta);
                using var brush = new SolidBrush(Color.RoyalBlue);
                graphics.FillEllipse(brush, 0, 0, 60, 40);
            }
            var button = Add(form, new LaVolpeButton { ButtonShape = ButtonShape.CustomFlat, Picture = image, Size = new Size(60, 40) });
            Check(button.PictureSize == ImageSize.FillScaleUpDown, $"PictureSize={button.PictureSize}");
            Check(button.Region is not null && !button.Region!.IsVisible(0, 0), button.Region is null ? "Region=null" : "máscara no aplicada");
        });

        Test("Imagen liberada no derriba el control", form =>
        {
            var image = new Bitmap(40, 40);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.Clear(Color.Magenta);
                graphics.FillEllipse(Brushes.RoyalBlue, 0, 0, 40, 40);
            }
            var button = Add(form, new LaVolpeButton { ButtonShape = ButtonShape.CustomFlat, Picture = image, Size = new Size(40, 40) });
            Application.DoEvents();
            image.Dispose();
            button.ButtonShape = ButtonShape.CustomFlat;   // fuerza a recalcular la región con la imagen liberada
            button.Invalidate();
            form.Refresh();
            Application.DoEvents();
            Check(true);
        });

        Test("MouseIcon crea y libera el cursor", form =>
        {
            var image = new Bitmap(24, 24);
            _attached.Add(image);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.Clear(Color.Transparent);
                graphics.FillEllipse(Brushes.Red, 2, 2, 20, 20);
            }
            var button = Add(form, new LaVolpeButton());
            button.MouseIcon = image;
            Check(button.Cursor != Cursors.Default && button.Cursor.Handle != IntPtr.Zero, "cursor no creado");
            button.MouseIcon = null;
            Check(button.Cursor == Cursors.Default, "cursor no restaurado");
        });

        Test("DelayDrawing(bool) no lanza y repinta", form =>
        {
            var button = Add(form, new LaVolpeButton());
            button.DelayDrawing(true);
            button.BackColor = Color.Red;
            button.DelayDrawing(false);
            Check(button.BackColor == Color.Red, $"BackColor={button.BackColor}");
        });

        Test("hDC válido a 32 bpp", form =>
        {
            var button = Add(form, new LaVolpeButton());
            var dc = button.hDC;
            Check(dc != IntPtr.Zero, "hDC=0");
            Check(GetDeviceCaps(dc, 12) == 32, $"BITSPIXEL={GetDeviceCaps(dc, 12)}");
        });

        Test("MousePointer asigna cursores estándar", form =>
        {
            // Los valores numéricos deben coincidir con VB6 para conservar .frm/.frx.
            Check((int)MousePointerConstants.SizeNESW == 6, $"SizeNESW={(int)MousePointerConstants.SizeNESW}");
            Check((int)MousePointerConstants.SizeNS == 7, $"SizeNS={(int)MousePointerConstants.SizeNS}");
            Check((int)MousePointerConstants.SizeNWSE == 8, $"SizeNWSE={(int)MousePointerConstants.SizeNWSE}");
            Check((int)MousePointerConstants.SizeWE == 9, $"SizeWE={(int)MousePointerConstants.SizeWE}");
            Check((int)MousePointerConstants.UpArrow == 10, $"UpArrow={(int)MousePointerConstants.UpArrow}");
            Check((int)MousePointerConstants.Hourglass == 11, $"Hourglass={(int)MousePointerConstants.Hourglass}");
            Check((int)MousePointerConstants.NoDrop == 12, $"NoDrop={(int)MousePointerConstants.NoDrop}");
            Check((int)MousePointerConstants.ArrowHourglass == 13, $"ArrowHourglass={(int)MousePointerConstants.ArrowHourglass}");
            Check((int)MousePointerConstants.ArrowQuestion == 14, $"ArrowQuestion={(int)MousePointerConstants.ArrowQuestion}");
            Check((int)MousePointerConstants.SizeAll == 15, $"SizeAll={(int)MousePointerConstants.SizeAll}");
            Check((int)MousePointerConstants.Custom == 99, $"Custom={(int)MousePointerConstants.Custom}");
            var button = Add(form, new LaVolpeButton());
            button.MousePointer = MousePointerConstants.Arrow;
            Check(button.Cursor == Cursors.Arrow, $"Arrow={button.Cursor}");
            button.MousePointer = MousePointerConstants.Crosshair;
            Check(button.Cursor == Cursors.Cross, $"Crosshair={button.Cursor}");
            button.MousePointer = MousePointerConstants.Hourglass;
            Check(button.Cursor == Cursors.WaitCursor, $"Hourglass={button.Cursor}");
            button.MousePointer = MousePointerConstants.SizeWE;
            Check(button.Cursor == Cursors.SizeWE, $"SizeWE={button.Cursor}");
            button.MousePointer = MousePointerConstants.Default;
            Check(button.Cursor == Cursors.Default, $"Default={button.Cursor}");
        });

        Test("MouseIcon implica MousePointer vbCustom", form =>
        {
            var image = new Bitmap(16, 16);
            _attached.Add(image);
            using (var graphics = Graphics.FromImage(image)) graphics.FillEllipse(Brushes.Blue, 2, 2, 12, 12);
            var button = Add(form, new LaVolpeButton { MouseIcon = image });
            Check(button.MousePointer == MousePointerConstants.Custom, $"MousePointer={button.MousePointer}");
            Check(button.Cursor != Cursors.Default, "cursor personalizado no aplicado");
        });

        Test("DragIcon crea y libera su cursor", form =>
        {
            var image = new Bitmap(20, 20);
            _attached.Add(image);
            using (var graphics = Graphics.FromImage(image)) graphics.FillRectangle(Brushes.Green, 0, 0, 20, 20);
            var button = Add(form, new LaVolpeButton());
            button.DragIcon = image;
            Check(button.DragIcon == image, "DragIcon no asignado");
            var field = typeof(LaVolpeButton).GetField("_dragIconCursor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Check(field?.GetValue(button) is Cursor, "cursor de arrastre no creado");
            button.DragIcon = null;
            Check(field?.GetValue(button) is null, "cursor de arrastre no liberado");
        });

        Test("OLEStartDrag publica texto en el mismo DataObject", form =>
        {
            var method = typeof(LaVolpeButton).GetMethod("CreateDragData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var data = (DataObject?)method?.Invoke(null, new object[] { "texto de prueba" });
            Check(data is not null, "DataObject no creado");
            Check(data!.GetDataPresent(DataFormats.UnicodeText), "UnicodeText ausente");
            Check(data.GetDataPresent(DataFormats.Text), "Text ausente");
            Check((string?)data.GetData(DataFormats.UnicodeText) == "texto de prueba", "texto Unicode incorrecto");
        });

        Test("OLEDragEnter permite soltar texto externo", form =>
        {
            var button = Add(form, new LaVolpeButton { AllowDrop = true });
            var data = new DataObject();
            data.SetData(DataFormats.UnicodeText, "texto externo");
            var fired = false;
            button.OLEDragOver += (_, e) =>
            {
                fired = true;
                e.Effect = e.Data?.GetDataPresent(DataFormats.UnicodeText) == true
                    ? DragDropEffects.Copy
                    : DragDropEffects.None;
            };
            var args = new DragEventArgs(data, 0, 1, 1, DragDropEffects.Copy, DragDropEffects.Copy);
            var method = typeof(LaVolpeButton).GetMethod("OnDragEnter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method?.Invoke(button, new object[] { args });
            Check(fired, "OLEDragOver no se notificó en DragEnter");
            Check(args.Effect == DragDropEffects.Copy, $"Effect={args.Effect}");
        });

        Test("OLEDragDrop entrega el texto externo", form =>
        {
            var button = Add(form, new LaVolpeButton { AllowDrop = true });
            var data = new DataObject();
            data.SetData(DataFormats.UnicodeText, "texto recibido");
            string? received = null;
            button.OLEDragDrop += (_, e) => received = e.Data?.GetData(DataFormats.UnicodeText) as string;
            var args = new DragEventArgs(data, 0, 2, 2, DragDropEffects.Copy, DragDropEffects.Copy);
            var method = typeof(LaVolpeButton).GetMethod("OnDragDrop", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method?.Invoke(button, new object[] { args });
            Check(received == "texto recibido", $"texto={received ?? "<null>"}");
        });

        Test("CheckBox/OptionButton marcan toda la superficie", form =>
        {
            var check = Add(form, new LaVolpeButton { Mode = ButtonMode.CheckBox, Size = new Size(100, 40), Value = true });
            var plain = Add(form, new LaVolpeButton { Mode = ButtonMode.CheckBox, Size = new Size(100, 40) });
            using var checkedBitmap = new Bitmap(100, 40);
            using var plainBitmap = new Bitmap(100, 40);
            check.DrawToBitmap(checkedBitmap, new Rectangle(0, 0, 100, 40));
            plain.DrawToBitmap(plainBitmap, new Rectangle(0, 0, 100, 40));
            var checkedColor = checkedBitmap.GetPixel(50, 20);
            var plainColor = plainBitmap.GetPixel(50, 20);
            Check(checkedColor != plainColor, $"sin cambio de estado: {checkedColor} vs {plainColor}");
            // VB6 usa cCheckBox = ShadeColor(blanco, -0x20) en estilos no XP/Mac/Java.
            Check(Math.Abs(checkedColor.R - 223) <= 3, $"color marcado={checkedColor}");
        });

        Test("Los 8 estilos se pintan sin excepciones", form =>
        {
            for (int i = 0; i < 8; i++)
            {
                var button = Add(form, new LaVolpeButton
                {
                    Caption = "Botón",
                    Size = new Size(100, 36),
                    ButtonStyle = (BackStyle)i,
                    Mode = ButtonMode.CheckBox
                });
                using (var bitmap = new Bitmap(100, 36))
                {
                    button.DrawToBitmap(bitmap, new Rectangle(0, 0, 100, 36));
                    button.Value = true;
                    button.DrawToBitmap(bitmap, new Rectangle(0, 0, 100, 36));
                    button.Value = false;
                }
            }
            Check(true);
        });

        Test("XP usa cuatro colores por anillo", form =>
        {
            var button = Add(form, new LaVolpeButton { ButtonStyle = BackStyle.WindowsXp, BackColor = Color.FromArgb(180,180,180), Size = new Size(120, 40) });
            var method = typeof(LaVolpeButton).GetMethod("GetBorders", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            var rings = (Array)method.Invoke(button, new object[] { button.BackColor, true, false, false })!;
            Color Side(object ring, string name) => (Color)ring.GetType().GetField(name)!.GetValue(ring)!;
            // Anillo interior: en VB6 los 4 lados se asignan por separado (polyColors 1..4).
            var inner = rings.GetValue(2)!;
            var left = Side(inner, "Left");
            var top = Side(inner, "Top");
            var right = Side(inner, "Right");
            var bottom = Side(inner, "Bottom");
            Check(left != top, $"izquierda={left} igual a superior={top}");
            Check(right != bottom, $"derecha={right} igual a inferior={bottom}");
        });

        Test("XP deshabilitado oscurece el fondo", form =>
        {
            var enabled = Add(form, new LaVolpeButton { ButtonStyle = BackStyle.WindowsXp, Size = new Size(120, 40) });
            var disabled = Add(form, new LaVolpeButton { ButtonStyle = BackStyle.WindowsXp, Size = new Size(120, 40), Enabled = false });
            using var a = new Bitmap(120, 40);
            using var b = new Bitmap(120, 40);
            enabled.DrawToBitmap(a, new Rectangle(0, 0, 120, 40));
            disabled.DrawToBitmap(b, new Rectangle(0, 0, 120, 40));
            var ca = a.GetPixel(60, 20);
            var cb = b.GetPixel(60, 20);
            Check(cb.GetBrightness() < ca.GetBrightness(), $"deshabilitado={cb} no es más oscuro que {ca}");
        });

        Test("Offset de contenido por estado", form =>
        {
            var method = typeof(LaVolpeButton).GetMethod("ContentOffset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            Point Offset(LaVolpeButton b, bool selected) => (Point)method.Invoke(b, new object[] { selected })!;
            var plain = Add(form, new LaVolpeButton());
            Check(Offset(plain, false) == Point.Empty, "sin estado no debe desplazar");
            Check(Offset(plain, true) == new Point(1, 1), $"Win95 marcado={Offset(plain, true)}");
            plain.ButtonShape = ButtonShape.Round3D;
            Check(Offset(plain, true) == new Point(2, 1), $"forma redonda marcada={Offset(plain, true)}");
            var java = Add(form, new LaVolpeButton { ButtonStyle = BackStyle.Java });
            Check(Offset(java, true) == Point.Empty, $"Java marcado={Offset(java, true)}");
        });

        Test("Custom marcado pinta el caption", form =>
        {
            var image = new Bitmap(48, 48);
            _attached.Add(image);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.Clear(Color.Magenta);
                graphics.FillEllipse(Brushes.RoyalBlue, 0, 0, 48, 48);
            }
            var button = Add(form, new LaVolpeButton
            {
                ButtonShape = ButtonShape.CustomFlat,
                Mode = ButtonMode.CheckBox,
                Caption = "OK",
                Picture = image,
                Size = new Size(48, 48),
                Value = true
            });
            using var bitmap = new Bitmap(48, 48);
            button.DrawToBitmap(bitmap, new Rectangle(0, 0, 48, 48));
            Check(button.Value, "Value no activado");
            bool dark = false;
            for (int y = 16; y < 34 && !dark; y++)
                for (int x = 8; x < 40 && !dark; x++)
                {
                    var c = bitmap.GetPixel(x, y);
                    if (c.R < 100 && c.G < 100 && c.B < 100) dark = true;
                }
            Check(dark, "no se encontraron píxeles del caption");
        });

        foreach (var failure in _failed)
            Console.WriteLine($"FAIL  {failure}");
        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"{_passed} correctas, {_failed.Count} fallidas");
        return _failed.Count == 0 ? 0 : 1;
    }

    private static int GenerateVisualSnapshots(string outputDirectory)
    {
        Application.EnableVisualStyles();
        Directory.CreateDirectory(outputDirectory);
        var styles = Enum.GetValues<BackStyle>().Distinct().ToArray();
        var states = new[]
        {
            (Name: "normal", Value: false, Enabled: true, Focused: false),
            (Name: "focused", Value: false, Enabled: true, Focused: true),
            (Name: "checked", Value: true, Enabled: true, Focused: false),
            (Name: "disabled", Value: false, Enabled: false, Focused: false)
        };

        using var form = new Form { ClientSize = new Size(160, 72), ShowInTaskbar = false, StartPosition = FormStartPosition.Manual, Location = new Point(-3000, 0) };
        using var button = new LaVolpeButton { Caption = "Botón", Size = new Size(120, 40), Location = new Point(20, 16), Mode = ButtonMode.CheckBox };
        form.Controls.Add(button);
        form.Show();
        Application.DoEvents();

        foreach (var style in styles)
        foreach (var state in states)
        {
            button.ButtonStyle = style;
            button.Value = state.Value;
            button.Enabled = state.Enabled;
            if (state.Focused) button.Focus();
            using var bitmap = new Bitmap(button.Width, button.Height);
            button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, bitmap.Size));
            bitmap.Save(Path.Combine(outputDirectory, $"{style}-{state.Name}.png"), System.Drawing.Imaging.ImageFormat.Png);
        }

        Console.WriteLine($"Snapshots visuales generados: {styles.Length * states.Length}");
        return 0;
    }

    private static int CompareVisualSnapshots(string referenceDirectory, string currentDirectory)
    {
        var failures = 0;
        foreach (var referencePath in Directory.EnumerateFiles(referenceDirectory, "*.png"))
        {
            var currentPath = Path.Combine(currentDirectory, Path.GetFileName(referencePath));
            if (!File.Exists(currentPath))
            {
                Console.WriteLine($"FALTA {Path.GetFileName(referencePath)}");
                failures++;
                continue;
            }

            using var reference = new Bitmap(referencePath);
            using var current = new Bitmap(currentPath);
            if (reference.Size != current.Size)
            {
                Console.WriteLine($"TAMAÑO {Path.GetFileName(referencePath)}: VB6={reference.Size}, .NET={current.Size}");
                failures++;
                continue;
            }

            long differentPixels = 0;
            long totalChannelDelta = 0;
            for (var y = 0; y < reference.Height; y++)
            for (var x = 0; x < reference.Width; x++)
            {
                var expected = reference.GetPixel(x, y);
                var actual = current.GetPixel(x, y);
                var delta = Math.Abs(expected.R - actual.R) + Math.Abs(expected.G - actual.G) + Math.Abs(expected.B - actual.B) + Math.Abs(expected.A - actual.A);
                if (delta != 0) differentPixels++;
                totalChannelDelta += delta;
            }

            var totalPixels = (long)reference.Width * reference.Height;
            var percentage = totalPixels == 0 ? 0 : differentPixels * 100.0 / totalPixels;
            Console.WriteLine($"{Path.GetFileName(referencePath)}: {differentPixels}/{totalPixels} píxeles ({percentage:F2}%), delta={totalChannelDelta}");
        }

        return failures == 0 ? 0 : 1;
    }

    private static T Add<T>(Form form, T control) where T : Control
    {
        form.Controls.Add(control);
        return control;
    }

    private static void Test(string name, Action<Form> body)
    {
        _current = name;
        var form = new Form
        {
            ClientSize = new Size(640, 480),
            StartPosition = FormStartPosition.Manual,
            Location = new Point(-3000, 0),
            ShowInTaskbar = false,
            BackColor = Color.FromArgb(240, 240, 240)
        };
        try
        {
            form.Show();
            Application.DoEvents();
            body(form);
            Application.DoEvents();
            Console.WriteLine($"PASS  {name}");
            _passed++;
        }
        catch (AssertFailedException ex)
        {
            _failed.Add($"{name} -> {ex.Message}");
        }
        catch (Exception ex)
        {
            _failed.Add($"{name} -> {ex.GetType().Name}: {ex.Message}");
        }
        finally
        {
            form.Close();
            form.Dispose();
            foreach (var disposable in _attached)
                disposable.Dispose();
            _attached.Clear();
        }
    }

    private static void Check(bool condition, string detail = "")
    {
        if (!condition)
            throw new AssertFailedException($"{_current}: {detail}");
    }

    [DllImport("gdi32.dll")]
    private static extern int GetDeviceCaps(IntPtr hdc, int index);

    private sealed class PropertyContext(object instance, PropertyDescriptor property) : ITypeDescriptorContext
    {
        public IContainer? Container => null;
        public object Instance => instance;
        public PropertyDescriptor PropertyDescriptor => property;
        public object? GetService(Type serviceType) => null;
        public bool OnComponentChanging() => true;
        public void OnComponentChanged() { }
    }

    private sealed class AssertFailedException(string message) : Exception(message);
}
