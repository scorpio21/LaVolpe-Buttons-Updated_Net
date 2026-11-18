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
        this.components = new System.ComponentModel.Container();
        this.laVolpeButton1 = new LaVolpe.Buttons.Net.LaVolpeButton();
        this.SuspendLayout();
        // 
        // laVolpeButton1
        // 
        this.laVolpeButton1.Caption = "LaVolpe Button";
        this.laVolpeButton1.Location = new System.Drawing.Point(330, 190);
        this.laVolpeButton1.Name = "laVolpeButton1";
        this.laVolpeButton1.Size = new System.Drawing.Size(140, 40);
        this.laVolpeButton1.TabIndex = 0;
        // 
        // Form1
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.laVolpeButton1);
        this.Name = "Form1";
        this.Text = "Form1";
        this.ResumeLayout(false);
    }

    #endregion

    private LaVolpe.Buttons.Net.LaVolpeButton laVolpeButton1;
}
