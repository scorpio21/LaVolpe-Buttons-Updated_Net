namespace LaVolpe.Buttons.DesignerSample2;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        laVolpeButton1 = new LaVolpe.Buttons.Net.LaVolpeButton();
        oleInstructionLabel = new Label();
        oleStatusLabel = new Label();
        laVolpeButton2 = new LaVolpe.Buttons.Net.LaVolpeButton();
        laVolpeButton3 = new LaVolpe.Buttons.Net.LaVolpeButton();
        laVolpeButton4 = new LaVolpe.Buttons.Net.LaVolpeButton();
        laVolpeButton6 = new LaVolpe.Buttons.Net.LaVolpeButton();
        SuspendLayout();
        // 
        // laVolpeButton1
        // 
        laVolpeButton1.AccessibleName = "LOAD";
        laVolpeButton1.AccessibleRole = AccessibleRole.PushButton;
        laVolpeButton1.AllowDrop = true;
        laVolpeButton1.BackColor = SystemColors.Control;
        laVolpeButton1.ButtonShape = Net.ButtonShape.RightDiagonal;
        laVolpeButton1.Caption = "LOAD";
        laVolpeButton1.DragIcon = null;
        laVolpeButton1.GradientColor = SystemColors.ControlLight;
        laVolpeButton1.HoverBackColor = SystemColors.Control;
        laVolpeButton1.HoverForeColor = SystemColors.ControlText;
        laVolpeButton1.Location = new Point(315, 179);
        laVolpeButton1.MouseIcon = null;
        laVolpeButton1.Name = "laVolpeButton1";
        laVolpeButton1.Picture = null;
        laVolpeButton1.ResetDefaultColors = false;
        laVolpeButton1.ShowFocusRect = true;
        laVolpeButton1.Size = new Size(121, 43);
        laVolpeButton1.TabIndex = 0;
        // 
        // oleInstructionLabel
        // 
        oleInstructionLabel.AutoSize = true;
        oleInstructionLabel.Location = new Point(24, 24);
        oleInstructionLabel.Name = "oleInstructionLabel";
        oleInstructionLabel.Size = new Size(468, 15);
        oleInstructionLabel.TabIndex = 9;
        oleInstructionLabel.Text = "Arrastre texto desde Notepad sobre LOAD para comprobar OLEDragOver/OLEDragDrop.";
        // 
        // oleStatusLabel
        // 
        oleStatusLabel.AutoEllipsis = true;
        oleStatusLabel.BorderStyle = BorderStyle.Fixed3D;
        oleStatusLabel.Location = new Point(24, 320);
        oleStatusLabel.Name = "oleStatusLabel";
        oleStatusLabel.Size = new Size(752, 36);
        oleStatusLabel.TabIndex = 8;
        oleStatusLabel.Text = "OLE: esperando una operación de arrastre.";
        oleStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // laVolpeButton2
        // 
        laVolpeButton2.AccessibleRole = AccessibleRole.CheckButton;
        laVolpeButton2.BackColor = SystemColors.Control;
        laVolpeButton2.DragIcon = null;
        laVolpeButton2.GradientColor = SystemColors.ControlLight;
        laVolpeButton2.HoverBackColor = SystemColors.Control;
        laVolpeButton2.HoverForeColor = SystemColors.ControlText;
        laVolpeButton2.Location = new Point(519, 193);
        laVolpeButton2.Mode = Net.ButtonMode.CheckBox;
        laVolpeButton2.MouseIcon = null;
        laVolpeButton2.Name = "laVolpeButton2";
        laVolpeButton2.Palette.CheckedBackColor = Color.FromArgb(255, 128, 0);
        laVolpeButton2.Palette.DisabledBackColor = Color.Empty;
        laVolpeButton2.Palette.DisabledTextColor = Color.Empty;
        laVolpeButton2.Palette.FocusColor = Color.Yellow;
        laVolpeButton2.Palette.HoverBackColor = Color.Lime;
        laVolpeButton2.Palette.HoverTextColor = Color.Empty;
        laVolpeButton2.Palette.NormalBackColor = Color.Blue;
        laVolpeButton2.Palette.PressedBackColor = Color.Red;
        laVolpeButton2.Palette.TextColor = Color.Empty;
        laVolpeButton2.Picture = null;
        laVolpeButton2.ResetDefaultColors = false;
        laVolpeButton2.ShowFocusRect = true;
        laVolpeButton2.Size = new Size(90, 33);
        laVolpeButton2.TabIndex = 3;
        laVolpeButton2.Value = true;
        // 
        // laVolpeButton3
        // 
        laVolpeButton3.AccessibleRole = AccessibleRole.RadioButton;
        laVolpeButton3.BackColor = SystemColors.Control;
        laVolpeButton3.DragIcon = null;
        laVolpeButton3.GradientColor = SystemColors.ControlLight;
        laVolpeButton3.HoverBackColor = SystemColors.Control;
        laVolpeButton3.HoverForeColor = SystemColors.ControlText;
        laVolpeButton3.Location = new Point(535, 102);
        laVolpeButton3.Mode = Net.ButtonMode.OptionButton;
        laVolpeButton3.MouseIcon = null;
        laVolpeButton3.Name = "laVolpeButton3";
        laVolpeButton3.Picture = null;
        laVolpeButton3.ResetDefaultColors = false;
        laVolpeButton3.ShowFocusRect = true;
        laVolpeButton3.Size = new Size(90, 33);
        laVolpeButton3.TabIndex = 4;
        laVolpeButton3.Value = true;
        // 
        // laVolpeButton4
        // 
        laVolpeButton4.AccessibleName = "Lopera";
        laVolpeButton4.AccessibleRole = AccessibleRole.PushButton;
        laVolpeButton4.BackColor = SystemColors.Control;
        laVolpeButton4.ButtonShape = Net.ButtonShape.Round3D;
        laVolpeButton4.ButtonStyle = Net.BackStyle.XP;
        laVolpeButton4.Caption = "Lopera";
        laVolpeButton4.CaptionStyle = Net.CaptionStyle.Sunken;
        laVolpeButton4.CustomClick = Net.CustomClick.lv_cSouthWest;
        laVolpeButton4.DragIcon = null;
        laVolpeButton4.DragMode = Net.DragModeConstants.vbAutomatic;
        laVolpeButton4.FontStyle = Net.FontStyles.lv_Bold | Net.FontStyles.lv_Italic;
        laVolpeButton4.GradientColor = SystemColors.ControlLight;
        laVolpeButton4.HoverBackColor = Color.Red;
        laVolpeButton4.HoverColorLocks = Net.HoverColorLocks.TextColorOnly;
        laVolpeButton4.HoverForeColor = SystemColors.ControlText;
        laVolpeButton4.Location = new Point(128, 114);
        laVolpeButton4.MouseIcon = null;
        laVolpeButton4.Name = "laVolpeButton4";
        laVolpeButton4.Picture = null;
        laVolpeButton4.ResetDefaultColors = false;
        laVolpeButton4.ShowFocusRect = true;
        laVolpeButton4.Size = new Size(90, 33);
        laVolpeButton4.TabIndex = 5;
        laVolpeButton4.Click += laVolpeButton4_Click;
        // 
        // laVolpeButton6
        // 
        laVolpeButton6.AccessibleRole = AccessibleRole.PushButton;
        laVolpeButton6.BackColor = SystemColors.Control;
        laVolpeButton6.DragIcon = null;
        laVolpeButton6.GradientColor = Color.FromArgb(192, 0, 0);
        laVolpeButton6.GradientMode = Net.GradientMode.TopToBottom;
        laVolpeButton6.HoverBackColor = Color.FromArgb(192, 0, 0);
        laVolpeButton6.HoverColorLocks = Net.HoverColorLocks.TextColorOnly;
        laVolpeButton6.HoverForeColor = SystemColors.ControlText;
        laVolpeButton6.Location = new Point(51, 211);
        laVolpeButton6.MouseIcon = null;
        laVolpeButton6.Name = "laVolpeButton6";
        laVolpeButton6.Picture = null;
        laVolpeButton6.ResetDefaultColors = false;
        laVolpeButton6.ShowFocusRect = true;
        laVolpeButton6.Size = new Size(90, 33);
        laVolpeButton6.TabIndex = 7;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(laVolpeButton6);
        Controls.Add(laVolpeButton4);
        Controls.Add(laVolpeButton3);
        Controls.Add(laVolpeButton2);
        Controls.Add(oleStatusLabel);
        Controls.Add(oleInstructionLabel);
        Controls.Add(laVolpeButton1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    private LaVolpe.Buttons.Net.LaVolpeButton laVolpeButton1;
    private System.Windows.Forms.Label oleInstructionLabel;
    private System.Windows.Forms.Label oleStatusLabel;
    private Net.LaVolpeButton laVolpeButton2;
    private Net.LaVolpeButton laVolpeButton3;
    private Net.LaVolpeButton laVolpeButton4;
    private Net.LaVolpeButton laVolpeButton6;
}
