namespace LaVolpe.Buttons.DesignerSample2;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        laVolpeButton1 = new LaVolpe.Buttons.Net.LaVolpeButton();
        SuspendLayout();
        // 
        // laVolpeButton1
        // 
        laVolpeButton1.AccessibleName = "LaVolpe Button";
        laVolpeButton1.AccessibleRole = AccessibleRole.PushButton;
        laVolpeButton1.BackColor = SystemColors.Control;
        laVolpeButton1.ButtonShape = Net.ButtonShape.RightDiagonal;
        laVolpeButton1.ButtonStyle = Net.BackStyle.XP;
        laVolpeButton1.Caption = "LaVolpe Button";
        laVolpeButton1.DragIcon = null;
        laVolpeButton1.GradientColor = SystemColors.ControlLight;
        laVolpeButton1.HoverBackColor = SystemColors.Control;
        laVolpeButton1.HoverForeColor = SystemColors.ControlText;
        laVolpeButton1.Location = new Point(330, 190);
        laVolpeButton1.MouseIcon = null;
        laVolpeButton1.Name = "laVolpeButton1";
        laVolpeButton1.Picture = null;
        laVolpeButton1.ResetDefaultColors = false;
        laVolpeButton1.ShowFocusRect = true;
        laVolpeButton1.Size = new Size(140, 40);
        laVolpeButton1.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(laVolpeButton1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private LaVolpe.Buttons.Net.LaVolpeButton laVolpeButton1;
}
