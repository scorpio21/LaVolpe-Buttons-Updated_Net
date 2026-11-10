# LaVolpe Buttons .NET

Port de `lvButtons_H` (VB6/ActiveX) a un control personalizado **C# .NET 8 WinForms**.

> Busca compatibilidad funcional y visual con el control original, pero **no** es un reemplazo binario del OCX ni afirma paridad del 100 %. El porcentaje estimado y la evidencia de cada área están en [ESTADO-PORT-NET.md](ESTADO-PORT-NET.md).

## Estado actual

El control compila sin errores ni advertencias y cuenta con una suite de pruebas ejecutable. Las funciones verificadas por ejecución incluyen:

- Modos `CommandButton`, `CheckBox` y `OptionButton` con la semántica de VB6 para `Value`, selección de hermanos, `GotFocus` y supresión del clic sobre una opción ya elegida.
- `Caption`/`Text` sincronizados, `IButtonControl`, `DialogResult`, `Default`, `Cancel` y navegación con teclado.
- 8 estilos de fondo, 9 formas, regiones reales de ventana (incluidas diagonales, elipses y máscaras de imagen), bordes de tres anillos por estilo, degradado XP, foco, hover y estados pulsado/marcado/deshabilitado.
- Gradientes en cuatro direcciones y paleta derivada de `BackColor`.
- Imágenes con las colocaciones y tamaños del original, `MouseIcon` con hotspot centrado, `MousePointerConstants` y `ToolTipText`.
- Compatibilidad de API: `hDC`, `hwnd`, `Index`, `DelayDrawing(bool)`, `RedrawButton()`, `DragIcon`, alias `lv_*` de los enums y eventos `Lv*`.
- 29 pruebas automáticas de compatibilidad (ver [Pruebas](#pruebas)).

Todavía hay diferencias conocidas; consulte [Limitaciones](#limitaciones-y-diferencias-conocidas).

## Estructura

| Ruta | Contenido |
| --- | --- |
| `LaVolpe.Buttons.Net/` | Biblioteca WinForms con el control `LaVolpeButton`. |
| `LaVolpe.Buttons.Sample/` | Aplicación de ejemplo con estilos, formas, modos, imágenes y gradientes. |
| `LaVolpe.Buttons.Tests/` | Pruebas de compatibilidad sin dependencias externas. |
| `LaVolpe.Buttons.Net.sln` | Solución .NET (biblioteca, ejemplo y pruebas). |
| `LaVolpe-Buttons-Updated-/` | Código VB6 original de referencia (no forma parte de la compilación). |
| `ESTADO-PORT-NET.md` | Estado del port, porcentaje estimado y evidencia por área. |
| `COMPATIBILIDAD.md` | Matriz de compatibilidad por propiedad, método y evento. |
| `PRUEBAS_COMPATIBILIDAD.md` | Casos ejecutados, resultados y pendientes. |
| `AUDITORIA_VB6_NET8.md` | Auditoría del control VB6 y del port .NET. |
| `EVIDENCIA_Y_DIFERENCIAS.md` | Evidencia y diferencias conocidas. |

## Requisitos

- Windows.
- .NET 8 SDK con soporte `net8.0-windows`.
- Visual Studio 2022 o la CLI de `dotnet`.

El repositorio incluye `global.json` para seleccionar el SDK .NET 8.0.427 cuando está instalado.

## Compilar

```powershell
dotnet restore .\LaVolpe.Buttons.Net.sln
dotnet build .\LaVolpe.Buttons.Net.sln --configuration Release --no-restore -m:1
dotnet build .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj --configuration Release
dotnet build .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj --configuration Release
dotnet build .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj --configuration Release
```

`-m:1` mantiene la compilación de la solución estable en entornos donde el SDK cancela procesos Roslyn al compilar los proyectos WinForms en paralelo.

## Ejecutar el ejemplo

```powershell
dotnet run --project .\LaVolpe.Buttons.Sample --configuration Release
```

## Usarlo en una aplicación WinForms

Añade una referencia a `LaVolpe.Buttons.Net.csproj` o a la DLL publicada y crea el control desde código:

```csharp
var aceptar = new LaVolpeButton
{
    Caption = "Aceptar",
    ButtonStyle = BackStyle.WindowsXp,
    Mode = ButtonMode.CommandButton,
    DialogResult = DialogResult.OK,
    Size = new Size(120, 36)
};

aceptar.Click += (_, _) => MessageBox.Show("Pulsado");
Controls.Add(aceptar);
AcceptButton = aceptar;
```

El control es WinForms (`net8.0-windows`); no se puede colocar directamente en WPF, ASP.NET o MAUI.

El ejemplo recorre los 8 `ButtonStyle`, las 7 formas no personalizadas, los tres modos, los botones `Default`/`Cancel`, un gradiente, una imagen y una forma `CustomFlat` generada desde la máscara de una imagen.

## Pruebas

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests --configuration Release
```

El ejecutable crea formularios reales en un hilo STA, comprueba la API y las regiones, imprime `PASS`/`FAIL` por caso y devuelve código de salida `1` si algo falla. No requiere xUnit, NUnit ni paquetes externos.

## Publicar

```powershell
dotnet publish .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj `
  --configuration Release `
  --output .\artifacts\publish
```

## Características

### Apariencia

- `BackStyle`: `Windows95`, `Windows31`, `WindowsXp`, `Java`, `Flat`, `Hover`, `Netscape` y `Macintosh`.
- `ButtonShape`: `Rectangular`, `LeftDiagonal`, `RightDiagonal`, `FullDiagonal`, `Round3D`, `Round3DFixed`, `RoundFlat`, `CustomFlat` y `Custom3DBorder`.
- Regiones de ventana reales: diagonales calculadas como el VB6, elipses completas en las formas redondeadas y recorte de esquinas según el estilo (`LopOffCorners`).
- En `CustomFlat`/`Custom3DBorder` la región se genera desde la imagen: el píxel superior izquierdo actúa como color transparente (también se descarta alpha < 20).
- Colores derivados de `BackColor`/`HoverBackColor` mediante el mismo desplazamiento de canales que `ShadeColor`; bordes de tres anillos concéntricos con colores por estilo y estado, más el degradado vertical fijo de XP.
- `GradientMode`: `LeftToRight`, `RightToLeft`, `TopToBottom`, `BottomToTop`.
- `CaptionStyle` (`Default`, `Sunken`, `Raised`), `CaptionAlign`, `FontStyle` y `ShowFocusRect`.
- `Picture`, `PictureAlign` y `PictureSize` (`16x16`, `24x24`, `32x32`, `FillStretch`, `FillScaleUpDown`).

### Comportamiento

- `Mode`: `CommandButton`, `CheckBox`, `OptionButton`.
- `Value`/`Checked`/`Selected`; en `CommandButton` se rechaza `Value = true`, como en VB6.
- `OptionButton`: deselecciona hermanos, se selecciona al recibir foco y no dispara `Click` si ya estaba elegido.
- CheckBox/OptionButton marcan toda la superficie con `cCheckBox`, igual que `Style = Graphical`; no hay glifo independiente.
- `IButtonControl`: `DialogResult`, `NotifyDefault`, `Default` y `Cancel`.
- Hover con temporizador administrado de 50 ms **por control**, activo solo mientras el puntero está sobre el botón; evento `MouseOnButton`.
- `MouseIcon` con hotspot centrado y liberación del `HICON`; `MousePointer` usa `MousePointerConstants` y `MouseIcon` implica `Custom`.
- `DragIcon` crea un cursor propio con `CreateIconIndirect` y lo usa durante el arrastre.
- `DelayDrawing(bool)`, `BeginDelayDrawing()`, `EndDelayDrawing()` y `RedrawButton()`.
- Eventos `ValueChanged`, `CheckedChanged`, `SelectedChanged`, `ButtonDoubleClick`, `LvMouseDown/Up/Move`, `LvKeyDown/Up`, `LvKeyPress`, además de los eventos WinForms habituales.
- Propiedades de compatibilidad: `hDC`, `hwnd`, `Index`, `HelpContextID`, `WhatsThisHelpID`, `BackColorButton`, `ResetDefaultColors`.
- Alias `lv_*` en los enums para facilitar el traslado de código VB6.

## Limitaciones y diferencias conocidas

- **No hay compatibilidad COM/ActiveX**: no se conservan CLSID, PropertyBag, registro del OCX ni el diseñador de VB6.
- **Pintado aproximado**: los tres anillos, focos y paletas se reproducen con GDI+, pero cada anillo colapsa sus cuatro lados en una pareja claro/oscuro; no se garantiza igualdad píxel a píxel con el control original.
- **Formas personalizadas**: VB6 redimensiona el control para ajustarlo a la imagen; este port estira la imagen al tamaño actual del control para que coincida con la región calculada. El caption sólo aparece en modo CheckBox/OptionButton con `Value = True`.
- **Máscara de imagen**: se usa el píxel superior izquierdo (±10 por canal) o alpha < 20. Si el control supera 250 000 píxeles, se usa un rectángulo para evitar regiones costosas. Una imagen ya liberada se ignora al recalcular la región.
- **`DrawToBitmap` no respeta `Region`**: para comprobar el recorte de ventana hay que usar `Region.IsVisible` o una captura real de pantalla.
- **Eventos OLE adaptados**: `OLEDragDrop`, `OLEStartDrag`, etc. son equivalentes WinForms, no OLE real.
- **`Index` no crea un arreglo de controles**: es solo una propiedad de compatibilidad.
- **Ayuda**: `HelpContextID` y `WhatsThisHelpID` se almacenan pero no abren ayuda por sí solos.
- **Timer**: es un `System.Windows.Forms.Timer` de 50 ms, no el timer Win32 nativo del OCX.

## Recursos e imágenes

`LaVolpe.Buttons.Net/Resources/*.png` se extrajeron de `frmSample.frx` con `extract_frx_images.py` (requiere Pillow, no es dependencia de compilación):

```powershell
python .\extract_frx_images.py
```

La muestra reutiliza esos PNG tal como venían del proyecto original. Algunos pueden ser logotipos de terceros: si va a distribuir el proyecto, revise su licencia o reemplácelos por recursos propios. El código del port es MIT, pero eso no concede derechos sobre esas imágenes.

## Documentación

- [ESTADO-PORT-NET.md](ESTADO-PORT-NET.md) — estado y porcentaje estimado por área.
- [COMPATIBILIDAD.md](COMPATIBILIDAD.md) — matriz de compatibilidad.
- [PRUEBAS_COMPATIBILIDAD.md](PRUEBAS_COMPATIBILIDAD.md) — pruebas ejecutadas y pendientes.
- [AUDITORIA_VB6_NET8.md](AUDITORIA_VB6_NET8.md) — auditoría técnica.
- [EVIDENCIA_Y_DIFERENCIAS.md](EVIDENCIA_Y_DIFERENCIAS.md) — evidencia y diferencias.

## Licencia

MIT. Consulte [LICENSE](LICENSE).
