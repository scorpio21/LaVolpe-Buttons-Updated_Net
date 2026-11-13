using Xunit;

namespace LaVolpe.Buttons.XunitTests;

public sealed class CompatibilityTests
{
    public static IEnumerable<object[]> Cases => Names.Select(name => new object[] { name });

    [Theory(DisplayName = "VB6 compatibility: {0}")]
    [MemberData(nameof(Cases))]
    public void Compatibility_case_runs_in_sta(string name)
    {
        Assert.Equal(0, LaVolpe.Buttons.Tests.Program.RunSingleTest(name));
    }

    private static readonly string[] Names =
    {
        "Text y Caption sincronizados", "Caption nulo se normaliza a cadena vacía", "Valores por defecto de VB6",
        "Opciones de diseñador WinForms", "Setters ignoran enums fuera del rango VB6", "PictureAlign vertical centra el caption como VB6",
        "Caption layout admite longitudes y saltos VB6", "BackColor hereda del contenedor", "ResetDefaultColors restaura colores y gradiente",
        "HoverColorLocks sincroniza colores como VB6", "CommandButton rechaza Value", "PerformClick dispara Click y DialogResult",
        "PerformClick respeta Enabled", "OptionButton deselecciona hermanos", "Clic en opción ya elegida no dispara Click",
        "GotFocus selecciona OptionButton", "CheckBox alterna Value", "Clic físico no duplica Click ni Value",
        "Teclas Space/Enter notifican y activan el botón", "Escape y flechas reproducen la navegación VB6", "IButtonControl y NotifyDefault",
        "Default y Cancel enlazan el formulario", "Región para formas no rectangulares", "Regiones conservadas en escalas equivalentes a DPI",
        "Rectangular XP recorta las esquinas", "CustomFlat usa la imagen como máscara", "Custom* redimensiona proporcionalmente como VB6",
        "Custom3DBorder dibuja el anillo blanco a gris de VB6", "Imagen liberada no derriba el control", "Timer de hover solo vive mientras el puntero está dentro",
        "MouseIcon crea y libera el cursor", "DelayDrawing(bool) no lanza y repinta", "Refresh público redibuja el control",
        "hDC válido a 32 bpp", "MousePointer asigna cursores estándar", "MouseIcon implica MousePointer vbCustom",
        "DragIcon crea y libera su cursor", "OLEStartDrag publica texto en el mismo DataObject", "OLECompleteDrag y OLESetData siguen notificándose",
        "OLEDragEnter permite soltar texto externo", "OLEDragEnter acepta texto sin manejador", "OLEDragEnter respeta rechazo explícito",
        "OLEDragDrop entrega el texto externo", "CheckBox/OptionButton marcan toda la superficie", "Los 8 estilos se pintan sin excepciones",
        "Java conserva ForeColor", "ShadeColor reproduce la conversión VB6 de G y B", "ShadeColor respeta los límites VB6 de los canales",
        "XP usa cuatro colores por anillo", "XP deshabilitado oscurece el fondo", "XP reproduce el sombreado por franjas de VB6",
        "GradientMode reproduce extremos inclusivos de VB6", "GradientMode no genera canales fuera de rango", "Offset de contenido por estado",
        "CustomClick desplaza y restaura el control VB6", "Custom marcado pinta el caption"
    };
}
