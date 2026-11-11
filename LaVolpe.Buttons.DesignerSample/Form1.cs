namespace LaVolpe.Buttons.DesignerSample;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        laVolpeButton1.OLEDragOver += LaVolpeButton1_OLEDragOver;
        laVolpeButton1.OLEDragDrop += LaVolpeButton1_OLEDragDrop;
        laVolpeButton1.OLEStartDrag += LaVolpeButton1_OLEStartDrag;
        laVolpeButton1.OLECompleteDrag += LaVolpeButton1_OLECompleteDrag;
        laVolpeButton1.OLEGiveFeedback += LaVolpeButton1_OLEGiveFeedback;
    }

    private void LaVolpeButton1_OLEDragOver(object? sender, DragEventArgs e)
    {
        bool hasText = e.Data?.GetDataPresent(DataFormats.UnicodeText) == true
            || e.Data?.GetDataPresent(DataFormats.Text) == true;
        e.Effect = hasText ? DragDropEffects.Copy : DragDropEffects.None;
        oleStatusLabel.Text = hasText
            ? "OLEDragOver: texto listo para soltar"
            : "OLEDragOver: formato no compatible";
    }

    private void LaVolpeButton1_OLEDragDrop(object? sender, DragEventArgs e)
    {
        string text = Convert.ToString(e.Data?.GetData(DataFormats.UnicodeText)
            ?? e.Data?.GetData(DataFormats.Text)) ?? string.Empty;
        oleStatusLabel.Text = $"OLEDragDrop: {text}";
    }

    private void LaVolpeButton1_OLEStartDrag(object? sender, DragEventArgs e)
    {
        oleStatusLabel.Text = "OLEStartDrag: arrastre iniciado";
    }

    private void LaVolpeButton1_OLECompleteDrag(object? sender, EventArgs e)
    {
        oleStatusLabel.Text = "OLECompleteDrag: arrastre finalizado";
    }

    private void LaVolpeButton1_OLEGiveFeedback(object? sender, GiveFeedbackEventArgs e)
    {
        oleStatusLabel.Text = "OLEGiveFeedback: cursor de arrastre activo";
    }
}
