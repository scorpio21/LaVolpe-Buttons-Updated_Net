using LaVolpe.Buttons.Net;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LaVolpe.Buttons.Sample;

/// <summary>
/// WinForms reproduction of frmSample.frm using the VB6 pixel coordinates.
/// </summary>
public sealed class SampleForm : Form
{
    private readonly List<Image> _ownedImages = new();
    private readonly LaVolpeButton[] _source = new LaVolpeButton[8];
    private readonly LaVolpeButton[] _sample = new LaVolpeButton[4];
    private readonly LaVolpeButton[] _custom = new LaVolpeButton[4];
    private readonly RadioButton[] _mode = new RadioButton[3];
    private readonly ComboBox _captionAlign = CreateCombo(18, 296, 177);
    private readonly ComboBox _pictureAlign = CreateCombo(18, 321, 177);
    private readonly ComboBox _pictureSize = CreateCombo(18, 344, 177);
    private readonly CheckBox _disable = CreateCheck("Disable above three buttons?", 316, 302, 166);
    private readonly CheckBox _gradients = CreateCheck("Use gradeint colors?", 358, 324, 124);
    private readonly CheckBox _hover = CreateCheck("Highlight hover with other color?", 303, 347, 179);
    private readonly Panel _samplePanel = new() { Location = new Point(8, 206), Size = new Size(494, 163), BorderStyle = BorderStyle.FixedSingle, BackColor = SystemColors.Control };
    private readonly List<Image?> _images = new();
    private readonly List<Image?> _vb6SampleImages = new();

    public SampleForm()
    {
        Text = "Sample for PSC - Vertical Button Version coming soon.";
        ClientSize = new Size(508, 374);
        AutoScaleMode = AutoScaleMode.None;
        BackColor = SystemColors.Control;
        Font = new Font("Times New Roman", 8.25f);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        LoadImages();
        BuildSourceButtons();
        BuildCustomButtons();
        BuildModeOptions();
        BuildSampleButtons();
        BuildOptions();
        ApplyInitialState();
        FormClosed += (_, _) => _ownedImages.ForEach(image => image.Dispose());
    }

    private void BuildSourceButtons()
    {
        var styles = new[] { BackStyle.Windows95, BackStyle.WindowsXp, BackStyle.Flat, BackStyle.Netscape, BackStyle.Windows31, BackStyle.Java, BackStyle.Hover, BackStyle.Macintosh };
        var captions = new[] { "Win 9.x", "Win XP", "Flat", "Netscape", "Win 3.x", "Java", "Hover", "Macintosh" };
        var positions = new[] { new Point(25, 13), new Point(147, 13), new Point(266, 13), new Point(384, 13), new Point(25, 60), new Point(147, 60), new Point(266, 60), new Point(384, 60) };
        for (var i = 0; i < _source.Length; i++)
        {
            var index = i;
            _source[i] = CreateButton(captions[i], positions[i], new Size(103, 43), styles[i], ButtonShape.Rectangular, "Tahoma", 9.75f, true);
            _source[i].Click += (_, _) => SelectSource(index);
        }
    }

    private void BuildCustomButtons()
    {
        _custom[0] = CreateButton("Yes?", new Point(44, 106), new Size(52, 43), BackStyle.WindowsXp, ButtonShape.Round3D, "Tahoma", 8.25f, true);
        _custom[0].BackColor = Color.FromArgb(192, 255, 255);
        _custom[0].GradientColor = Color.FromArgb(0, 128, 128);
        _custom[0].GradientMode = GradientMode.TopToBottom;

        _custom[1] = CreateButton("Pats  ||", new Point(128, 107), new Size(100, 52), BackStyle.WindowsXp, ButtonShape.CustomFlat, "Tahoma", 9f, false);
        _custom[1].Picture = _images[0];
        _custom[1].PictureSize = ImageSize.FillScaleUpDown;
        _custom[1].CaptionAlign = ContentAlignment.MiddleRight;
        _custom[1].HoverBackColor = Color.Yellow;
        _custom[1].HoverForeColor = Color.FromArgb(0, 0, 192);

        _custom[2] = CreateButton("La ||Volpe", new Point(316, 106), new Size(59, 59), BackStyle.WindowsXp, ButtonShape.Custom3DBorder, "Times New Roman", 9f, false);
        _custom[2].Picture = _images[1];
        _custom[2].PictureSize = ImageSize.Size32x32;
        _custom[2].CustomClick = CustomClick.SouthEast;
        _custom[2].CaptionStyle = CaptionStyle.Raised;

        _custom[3] = CreateButton(string.Empty, new Point(407, 109), new Size(58, 54), BackStyle.WindowsXp, ButtonShape.RoundFlat, "Tahoma", 8.25f, true);
        _custom[3].Picture = _images[1];
        _custom[3].PictureSize = ImageSize.Size32x32;
        _custom[3].PictureAlign = ImagePlacement.TopCenter;
        _custom[3].BackColor = Color.Magenta;
        _custom[3].MouseOnButton += (_, inside) =>
        {
            _custom[3].DelayDrawing(true);
            _custom[3].BackColor = inside ? Color.Cyan : Color.Magenta;
            _custom[3].Picture = inside ? _images[2] : _images[1];
            _custom[3].DelayDrawing(false);
        };
    }

    private void BuildModeOptions()
    {
        var captions = new[] { "Above 3 check boxes", "Above 3 option buttons", "Above 3 are command buttons" };
        var positions = new[] { new Point(37, 180), new Point(167, 180), new Point(310, 180) };
        var widths = new[] { 131, 135, 174 };
        for (var i = 0; i < _mode.Length; i++)
        {
            var index = i;
            _mode[i] = new RadioButton { Text = captions[i], Location = positions[i], Size = new Size(widths[i], 14), ForeColor = Color.Blue, Font = new Font("MS Sans Serif", 8.25f), AutoSize = false };
            _mode[i].CheckedChanged += (_, _) => { if (_mode[index].Checked) ApplyMode(index); };
            Controls.Add(_mode[i]);
        }
        _mode[2].Checked = true;
    }

    private void BuildSampleButtons()
    {
        _sample[0] = CreateButton("Win XP", new Point(17, 222), new Size(194, 70), BackStyle.WindowsXp, ButtonShape.RightDiagonal, "Arial", 9.75f, true);
        _sample[1] = CreateButton("Win XP", new Point(141, 222), new Size(225, 70), BackStyle.WindowsXp, ButtonShape.FullDiagonal, "Arial", 9.75f, true);
        _sample[2] = CreateButton("Win XP", new Point(296, 222), new Size(193, 70), BackStyle.WindowsXp, ButtonShape.LeftDiagonal, "Arial", 9.75f, true);
        _sample[0].Picture = _vb6SampleImages[0];
        _sample[1].Picture = _vb6SampleImages[1];
        _sample[2].Picture = _vb6SampleImages[2];
        foreach (var button in _sample.Take(3))
        {
            button.PictureSize = ImageSize.Size32x32;
            button.PictureAlign = ImagePlacement.LeftOfCaption;
        }
        _sample[3] = CreateButton("Round", new Point(208, 302), new Size(96, 56), BackStyle.WindowsXp, ButtonShape.Round3D, "MS Sans Serif", 8.25f, true);
        _sample[3].Picture = _vb6SampleImages[3];
        _sample[3].PictureSize = ImageSize.Size32x32;
        _sample[3].PictureAlign = ImagePlacement.LeftOfCaption;
        _sample[3].GradientColor = Color.FromArgb(0, 128, 128);
        _sample[3].GradientMode = GradientMode.TopToBottom;
    }

    private void BuildOptions()
    {
        Controls.Add(new Label { Text = "Note how gradients on circular button has nice 3D effect", Location = new Point(51, 159), Size = new Size(277, 18), Font = new Font("MS Sans Serif", 8.25f) });
        Controls.Add(new Label { Text = "< Custom Buttons >", Location = new Point(238, 121), Size = new Size(58, 27), ForeColor = Color.FromArgb(192, 0, 0), Font = new Font("Times New Roman", 9f, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter });
        Controls.Add(new Label { Text = "Click above rectangular buttons to see their style as a diagonal shape.", Location = new Point(18, 200), AutoSize = true, Font = new Font("MS Sans Serif", 8.25f) });
        Controls.Add(_samplePanel);
        Controls.Add(_captionAlign);
        Controls.Add(_pictureAlign);
        Controls.Add(_pictureSize);
        Controls.Add(_disable);
        Controls.Add(_gradients);
        Controls.Add(_hover);
        _captionAlign.Items.AddRange(new object[] { "Caption Left", "Caption Right", "Caption Center", "Caption Top", "Caption Bottom" });
        _pictureAlign.Items.AddRange(new object[] { "Image at Left", "Image Left of Caption", "Image Right of Caption", "Image at Right", "Image Top", "Image Bottom" });
        _pictureSize.Items.AddRange(new object[] { "Image 16 x 16", "Image 24 x 24", "Image 32 x 32", "Fill Stretch", "Fill Scale Up/Down" });
        _captionAlign.SelectedIndexChanged += (_, _) => ApplyCaptionAlignment();
        _pictureAlign.SelectedIndexChanged += (_, _) => ApplyPictureAlignment();
        _pictureSize.SelectedIndexChanged += (_, _) => ApplyPictureSize();
        _disable.CheckedChanged += (_, _) => SetSamplesEnabled(!_disable.Checked);
        _gradients.CheckedChanged += (_, _) => ApplyGradients();
        _hover.CheckedChanged += (_, _) => ApplyHover();

        // The VB6 frame is a background container. Keep every child control above it.
        _samplePanel.SendToBack();
        foreach (var control in _sample.Concat(_custom).Concat<Control>(_mode).Append(_captionAlign).Append(_pictureAlign).Append(_pictureSize).Append(_disable).Append(_gradients).Append(_hover))
            control.BringToFront();
    }

    private void ApplyInitialState()
    {
        _captionAlign.SelectedIndex = 2;
        _pictureAlign.SelectedIndex = 1;
        _pictureSize.SelectedIndex = 2;
        ApplyMode(2);
        ApplyCaptionAlignment();
        ApplyPictureAlignment();
        ApplyPictureSize();
    }

    private void SelectSource(int index)
    {
        foreach (var button in _sample.Concat(_custom)) button.ButtonStyle = (BackStyle)index;
        var caption = _source[index].Caption;
        foreach (var button in _sample.Take(3)) button.Caption = caption;
    }

    private void ApplyMode(int index)
    {
        var mode = index switch { 0 => ButtonMode.CheckBox, 1 => ButtonMode.OptionButton, _ => ButtonMode.CommandButton };
        for (var i = 0; i < 3; i++) _custom[i].Mode = mode;
        _custom[2].ShowFocusRect = index != 2;
    }

    private void ApplyCaptionAlignment()
    {
        var value = _captionAlign.SelectedIndex switch { 0 => ContentAlignment.MiddleLeft, 1 => ContentAlignment.MiddleRight, 2 => ContentAlignment.MiddleCenter, 3 => ContentAlignment.TopCenter, _ => ContentAlignment.BottomCenter };
        foreach (var button in _sample) button.CaptionAlign = value;
    }

    private void ApplyPictureAlignment()
    {
        var value = _pictureAlign.SelectedIndex switch { 0 => ImagePlacement.LeftEdge, 1 => ImagePlacement.LeftOfCaption, 2 => ImagePlacement.RightOfCaption, 3 => ImagePlacement.RightEdge, 4 => ImagePlacement.TopCenter, _ => ImagePlacement.BottomCenter };
        foreach (var button in _sample) button.PictureAlign = value;
    }

    private void ApplyPictureSize()
    {
        var value = (ImageSize)Math.Max(0, _pictureSize.SelectedIndex);
        foreach (var button in _sample) button.PictureSize = value;
    }

    private void SetSamplesEnabled(bool enabled)
    {
        foreach (var button in _sample) button.Enabled = enabled;
    }

    private void ApplyHover()
    {
        foreach (var button in _sample)
        {
            button.HoverBackColor = _hover.Checked ? Color.Cyan : button.BackColor;
            button.HoverForeColor = Color.Blue;
            button.HoverColorLocks = _hover.Checked ? HoverColorLocks.NoLocks : HoverColorLocks.TextAndBackColor;
        }
    }

    private void ApplyGradients()
    {
        foreach (var button in _sample)
        {
            if (_gradients.Checked)
            {
                button.BackColor = Color.Cyan;
                button.GradientColor = Color.Blue;
                button.GradientMode = GradientMode.BottomToTop;
                button.CaptionStyle = CaptionStyle.Default;
            }
            else
            {
                button.BackColor = SystemColors.Control;
                button.GradientMode = GradientMode.None;
            }
        }
        ApplyHover();
    }

    private LaVolpeButton CreateButton(string caption, Point location, Size size, BackStyle style, ButtonShape shape, string fontName, float fontSize, bool bold)
    {
        var button = new LaVolpeButton { Caption = caption, Location = location, Size = size, ButtonStyle = style, ButtonShape = shape, Font = new Font(fontName, fontSize), FontStyle = bold ? FontStyles.Bold : FontStyles.PlainStyle, BackColor = SystemColors.Control, ForeColor = Color.Black };
        Controls.Add(button);
        return button;
    }

    private void LoadImages()
    {
        foreach (var name in new[] { "image0.png", "image1.png", "image2.png" })
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Resources", name);
            if (!File.Exists(path)) { _images.Add(null); continue; }
            using var source = Image.FromFile(path);
            var copy = new Bitmap(source);
            _ownedImages.Add(copy);
            _images.Add(copy);
        }

        foreach (var name in new[] { "vb6-diagonal-cd.png", "vb6-diagonal-laptop.png", "vb6-diagonal-floppy.png", "vb6-round-mouse.png" })
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Resources", name);
            if (!File.Exists(path)) { _vb6SampleImages.Add(null); continue; }
            using var source = Image.FromFile(path);
            var copy = new Bitmap(source);
            _ownedImages.Add(copy);
            _vb6SampleImages.Add(copy);
        }
    }

    private static ComboBox CreateCombo(int x, int y, int width) => new() { Location = new Point(x, y), Size = new Size(width, 21), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("MS Sans Serif", 8.25f) };
    private static CheckBox CreateCheck(string text, int x, int y, int width) => new() { Text = text, Location = new Point(x, y), Size = new Size(width, 17), AutoSize = false, CheckAlign = ContentAlignment.MiddleRight, Font = new Font("MS Sans Serif", 8.25f) };
}
