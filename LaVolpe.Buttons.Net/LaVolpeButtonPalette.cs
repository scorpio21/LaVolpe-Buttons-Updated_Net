using System.ComponentModel;
using System.Drawing;

namespace LaVolpe.Buttons.Net;

/// <summary>
/// Optional state palette for <see cref="LaVolpeButton"/>.
/// Empty colors keep the original VB6-derived renderer behavior.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public sealed class LaVolpeButtonPalette
{
    private Color _normalBackColor;
    private Color _hoverBackColor;
    private Color _pressedBackColor;
    private Color _checkedBackColor;
    private Color _disabledBackColor;
    private Color _textColor;
    private Color _hoverTextColor;
    private Color _disabledTextColor;
    private Color _focusColor;

    public event EventHandler? Changed;

    [Category("Normal"), Description("Color de fondo del estado normal."), DefaultValue(typeof(Color), "Empty")]
    public Color NormalBackColor { get => _normalBackColor; set => Set(ref _normalBackColor, value); }

    [Category("Hover"), Description("Color de fondo cuando el puntero está sobre el botón."), DefaultValue(typeof(Color), "Empty")]
    public Color HoverBackColor { get => _hoverBackColor; set => Set(ref _hoverBackColor, value); }

    [Category("Pressed"), Description("Color de fondo mientras el botón está pulsado."), DefaultValue(typeof(Color), "Empty")]
    public Color PressedBackColor { get => _pressedBackColor; set => Set(ref _pressedBackColor, value); }

    [Category("Checked"), Description("Color de fondo de CheckBox u OptionButton marcado."), DefaultValue(typeof(Color), "Empty")]
    public Color CheckedBackColor { get => _checkedBackColor; set => Set(ref _checkedBackColor, value); }

    [Category("Disabled"), Description("Color de fondo cuando el control está deshabilitado."), DefaultValue(typeof(Color), "Empty")]
    public Color DisabledBackColor { get => _disabledBackColor; set => Set(ref _disabledBackColor, value); }

    [Category("Text"), Description("Color del texto en el estado normal."), DefaultValue(typeof(Color), "Empty")]
    public Color TextColor { get => _textColor; set => Set(ref _textColor, value); }

    [Category("Text"), Description("Color del texto durante hover."), DefaultValue(typeof(Color), "Empty")]
    public Color HoverTextColor { get => _hoverTextColor; set => Set(ref _hoverTextColor, value); }

    [Category("Text"), Description("Color del texto cuando el control está deshabilitado."), DefaultValue(typeof(Color), "Empty")]
    public Color DisabledTextColor { get => _disabledTextColor; set => Set(ref _disabledTextColor, value); }

    [Category("Focus"), Description("Color del indicador de foco."), DefaultValue(typeof(Color), "Empty")]
    public Color FocusColor { get => _focusColor; set => Set(ref _focusColor, value); }

    [Browsable(false)]
    public bool IsDefault => _normalBackColor.IsEmpty && _hoverBackColor.IsEmpty && _pressedBackColor.IsEmpty
        && _checkedBackColor.IsEmpty && _disabledBackColor.IsEmpty && _textColor.IsEmpty
        && _hoverTextColor.IsEmpty && _disabledTextColor.IsEmpty && _focusColor.IsEmpty;

    public void Reset()
    {
        _normalBackColor = Color.Empty;
        _hoverBackColor = Color.Empty;
        _pressedBackColor = Color.Empty;
        _checkedBackColor = Color.Empty;
        _disabledBackColor = Color.Empty;
        _textColor = Color.Empty;
        _hoverTextColor = Color.Empty;
        _disabledTextColor = Color.Empty;
        _focusColor = Color.Empty;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString() => IsDefault ? "(predeterminada)" : "(personalizada)";

    private void Set(ref Color field, Color value)
    {
        if (field == value) return;
        field = value;
        Changed?.Invoke(this, EventArgs.Empty);
    }
}
