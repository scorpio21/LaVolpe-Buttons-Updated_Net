using LaVolpe.Buttons.Net;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LaVolpe.Buttons.Sample;

public sealed class SampleForm : Form
{
    private readonly Label _status = new()
    {
        AutoSize = false,
        Dock = DockStyle.Bottom,
        Height = 46,
        Padding = new Padding(12, 6, 12, 6),
        BackColor = SystemColors.Control,
        ForeColor = Color.FromArgb(60, 60, 60),
        Text = "Pase el ratón por un botón o haga clic para ver los eventos."
    };
    private readonly List<Image> _ownedImages = new();

    public SampleForm()
    {
        Text = "Sample for PSC - Vertical Button Version coming soon.";
        ClientSize = new Size(760, 470);
        BackColor = SystemColors.Control;
        Font = new Font("Times New Roman", 8.25f);
        StartPosition = FormStartPosition.CenterScreen;
        AutoScroll = true;

        Controls.Add(new Label { Text = "ButtonStyle", Location = new Point(20, 12), AutoSize = true, ForeColor = Color.DimGray });
        var styles = new[] { BackStyle.Windows95, BackStyle.WindowsXp, BackStyle.Flat, BackStyle.Netscape, BackStyle.Windows31, BackStyle.Java, BackStyle.Hover, BackStyle.Macintosh };
        var styleNames = new[] { "Win 9.x", "Win XP", "Flat", "Netscape", "Win 3.x", "Java", "Hover", "Macintosh" };
        for (int i = 0; i < styles.Length; i++)
            Add(Make(styleNames[i], new Point(20 + i % 4 * 185, 32 + i / 4 * 46), new Size(170, 40), BackStyle: styles[i]));

        Controls.Add(new Label { Text = "ButtonShape", Location = new Point(20, 132), AutoSize = true, ForeColor = Color.DimGray });
        var shapes = new[] { ButtonShape.Rectangular, ButtonShape.LeftDiagonal, ButtonShape.RightDiagonal, ButtonShape.FullDiagonal, ButtonShape.Round3D, ButtonShape.Round3DFixed, ButtonShape.RoundFlat };
        for (int i = 0; i < shapes.Length; i++)
            Add(Make(shapes[i].ToString(), new Point(20 + i % 4 * 185, 152 + i / 4 * 52), new Size(170, 44), ButtonShape: shapes[i]));

        Controls.Add(new Label { Text = "Mode", Location = new Point(20, 262), AutoSize = true, ForeColor = Color.DimGray });
        var check = Make("Activar (CheckBox)", new Point(20, 282), new Size(170, 40), Mode: ButtonMode.CheckBox);
        check.CheckedChanged += (_, _) => _status.Text = $"CheckBox: {(check.Checked ? "activado" : "desactivado")}";
        Add(check);

        var option1 = Make("Opción 1", new Point(200, 282), new Size(170, 40), Mode: ButtonMode.OptionButton);
        var option2 = Make("Opción 2", new Point(380, 282), new Size(170, 40), Mode: ButtonMode.OptionButton);
        option1.SelectedChanged += (_, _) => _status.Text = $"Opción 1: {(option1.Selected ? "seleccionada" : "libre")}";
        option2.SelectedChanged += (_, _) => _status.Text = $"Opción 2: {(option2.Selected ? "seleccionada" : "libre")}";
        Add(option1); Add(option2);

        Controls.Add(new Label { Text = "Default / Cancel / imágenes / gradiente", Location = new Point(20, 332), AutoSize = true, ForeColor = Color.DimGray });
        var accept = Make("Aceptar (Default)", new Point(20, 352), new Size(170, 40), BackStyle: BackStyle.WindowsXp);
        accept.DialogResult = DialogResult.OK;
        Add(accept);

        var cancel = Make("Cancelar", new Point(200, 352), new Size(170, 40), BackStyle: BackStyle.WindowsXp);
        cancel.Cancel = true;
        Add(cancel);

        // Integración WinForms real: Enter/Escape delegan en IButtonControl.
        AcceptButton = accept;
        CancelButton = cancel;

        var gradient = Make("Gradiente", new Point(380, 352), new Size(170, 40), BackStyle: BackStyle.WindowsXp);
        gradient.GradientMode = GradientMode.TopToBottom;
        gradient.GradientColor = Color.White;
        Add(gradient);

        var images = LoadImages();
        var picture = Make("Con imagen", new Point(560, 352), new Size(170, 40), BackStyle: BackStyle.WindowsXp);
        picture.Picture = images[0];
        picture.PictureSize = ImageSize.Size24x24;
        Add(picture);

        // CustomFlat: la región de la ventana sale de la máscara de la imagen (píxel superior izquierdo).
        var custom = Make("", new Point(560, 282), new Size(48, 48), ButtonShape: ButtonShape.CustomFlat);
        custom.Picture = images[2];
        custom.PictureSize = ImageSize.FillScaleUpDown;
        custom.ToolTipText = "CustomFlat: región generada desde la imagen";
        Add(custom);

        accept.Click += (_, _) => _status.Text = $"Click en Aceptar (DialogResult = {accept.DialogResult})";
        cancel.Click += (_, _) => _status.Text = "Click en Cancelar";
        Controls.Add(_status);

        FormClosed += (_, _) => { foreach (var image in _ownedImages) image.Dispose(); };
    }

    private LaVolpeButton Make(string caption, Point location, Size size, BackStyle? BackStyle = null,
        ButtonShape? ButtonShape = null, ButtonMode? Mode = null)
    {
        var button = new LaVolpeButton
        {
            Caption = caption,
            Location = location,
            Size = size,
            Font = new Font("Tahoma", 9.75f),
            FontStyle = FontStyles.Bold,
            ForeColor = Color.Black,
            BackColor = SystemColors.Control
        };
        if (BackStyle is not null) button.ButtonStyle = BackStyle.Value;
        if (ButtonShape is not null) button.ButtonShape = ButtonShape.Value;
        if (Mode is not null) button.Mode = Mode.Value;
        button.MouseOnButton += (_, inside) => { if (inside) _status.Text = $"MouseOnButton: {caption}"; };
        button.Click += (_, _) => _status.Text = $"Click: {caption}";
        return button;
    }

    private void Add(Control control) => Controls.Add(control);

    private List<Image?> LoadImages()
    {
        var result = new List<Image?>();
        foreach (var name in new[] { "image0.png", "image1.png", "image2.png" })
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Resources", name);
            if (!File.Exists(path)) { result.Add(null); continue; }
            using var source = Image.FromFile(path);
            var copy = new Bitmap(source);
            _ownedImages.Add(copy);
            result.Add(copy);
        }
        return result;
    }
}
