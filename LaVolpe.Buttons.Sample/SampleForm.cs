using LaVolpe.Buttons.Net;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LaVolpe.Buttons.Sample;

public sealed class SampleForm : Form
{
    public SampleForm()
    {
        Text = "LaVolpe Buttons - .NET sample";
        ClientSize = new Size(560, 260);
        BackColor = Color.WhiteSmoke;
        AutoScroll = true;
        StartPosition = FormStartPosition.CenterScreen;

        var resourcePath = (string name) => Path.Combine(AppContext.BaseDirectory, "Resources", name);
        static Image? LoadImage(string path)
        {
            if (!File.Exists(path)) return null;
            using var source = Image.FromFile(path);
            return new Bitmap(source);
        }
        var image0Path = resourcePath("image0.png");
        var image1Path = resourcePath("image1.png");
        var image2Path = resourcePath("image2.png");
        var normal = new LaVolpeButton { ToolTipText = "Ejecuta la acción Aceptar", Caption = "Aceptar", Location = new Point(30, 30), Size = new Size(190, 45), ButtonStyle = BackStyle.WindowsXp, ButtonShape = ButtonShape.Round3D, GradientMode = GradientMode.TopToBottom, GradientColor = Color.White, Picture = LoadImage(image0Path), PictureSize = ImageSize.FillScaleUpDown };
        var check = new LaVolpeButton { ToolTipText = "Activa o desactiva la opción", Name = "checkBoxActivar", Caption = "Activar (CheckBox)", Mode = ButtonMode.CheckBox, Location = new Point(30, 95), Size = new Size(190, 45), ButtonStyle = BackStyle.Flat, ButtonShape = ButtonShape.RoundFlat, BackColor = Color.White, ForeColor = Color.Black, HoverBackColor = Color.LightGreen, Picture = LoadImage(image1Path), PictureSize = ImageSize.Size24x24, Visible = true };
        var option1 = new LaVolpeButton { ToolTipText = "Selecciona la opción 1", Name = "optionButton1", Caption = "Opción 1 (OptionButton)", Mode = ButtonMode.OptionButton, Location = new Point(260, 30), Size = new Size(220, 45), ButtonShape = ButtonShape.LeftDiagonal, Picture = LoadImage(image2Path), PictureSize = ImageSize.Size24x24, BackColor = Color.White, ForeColor = Color.Black, Visible = true };
        var option2 = new LaVolpeButton { ToolTipText = "Selecciona la opción 2", Name = "optionButton2", Caption = "Opción 2 (OptionButton)", Mode = ButtonMode.OptionButton, Location = new Point(260, 95), Size = new Size(220, 45), ButtonShape = ButtonShape.RightDiagonal, BackColor = Color.White, ForeColor = Color.Black, Visible = true };
        normal.Click += (_, _) => MessageBox.Show("Click recibido", "LaVolpe");
        check.MouseOnButton += (_, inside) => Text = inside ? "LaVolpe Buttons - dentro" : "LaVolpe Buttons - .NET sample";
        check.Click += (_, _) => Text = $"CheckBox: {(check.Value ? "activado" : "desactivado")}";
        option1.Click += (_, _) => Text = "OptionButton 1 seleccionado";
        option2.Click += (_, _) => Text = "OptionButton 2 seleccionado";
        check.CheckedChanged += (_, _) => check.AccessibleDescription = check.Checked ? "Casilla activada" : "Casilla desactivada";
        option1.SelectedChanged += (_, _) => option1.AccessibleDescription = option1.Selected ? "Opción 1 seleccionada" : "Opción 1 no seleccionada";
        option2.SelectedChanged += (_, _) => option2.AccessibleDescription = option2.Selected ? "Opción 2 seleccionada" : "Opción 2 no seleccionada";
        Controls.AddRange(new Control[] { normal, check, option1, option2 });
        FormClosed += (_, _) =>
        {
            foreach (Control control in Controls)
                if (control is LaVolpeButton button && button.Picture is IDisposable ownedImage)
                    ownedImage.Dispose();
        };
    }
}
