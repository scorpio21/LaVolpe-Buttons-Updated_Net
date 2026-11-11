namespace LaVolpe.Buttons.DesignerSample;

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
        this.components = new System.ComponentModel.Container();
        this.laVolpeButton1 = new LaVolpe.Buttons.Net.LaVolpeButton();
        this.SuspendLayout();
        //
        // laVolpeButton1
        //
        this.laVolpeButton1.Caption = "LOAD";
        this.laVolpeButton1.Location = new System.Drawing.Point(315, 179);
        this.laVolpeButton1.Name = "laVolpeButton1";
        this.laVolpeButton1.Size = new System.Drawing.Size(121, 43);
        this.laVolpeButton1.TabIndex = 0;
        this.laVolpeButton1.ButtonShape = LaVolpe.Buttons.Net.ButtonShape.lv_LeftDiagonal;
        this.laVolpeButton1.ButtonStyle = LaVolpe.Buttons.Net.BackStyle.lv_XP;
        this.laVolpeButton1.CaptionAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.laVolpeButton1.PictureSize = LaVolpe.Buttons.Net.ImageSize.lv_32x32;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.laVolpeButton1);
        this.Text = "Form1";
        this.ResumeLayout(false);
    }

    #endregion

    private LaVolpe.Buttons.Net.LaVolpeButton laVolpeButton1;
}
