# Auditoría VB6 → C# .NET 8

## Alcance

Se revisaron los fuentes VB6 `lvButtons.ctl`, `modLvTimer.bas`, `frmSample.frm`, los proyectos `.vbp`, `Readme.htm`, el recurso `frmSample.frx`, y los proyectos/fuentes C# actuales.

La auditoría distingue **compilación** de **equivalencia de comportamiento**. Compilar no implica paridad visual ni ActiveX.

## Inventario VB6

### API pública

Propiedades declaradas en `lvButtons.ctl`: `ButtonStyle`, `Mode`, `Caption`, `CaptionAlign`, `CaptionStyle`, `CustomClick`, `ButtonShape`, `Picture`, `PictureAlign`, `Enabled`, `ShowFocusRect`, `Value`, `PictureSize`, `MousePointer`, `MouseIcon`, `Font`, `FontStyle`, `ForeColor`, `BackColor`, `GradientColor`, `GradientMode`, `ResetDefaultColors`, `HoverColorLocks`, `HoverForeColor`, `HoverBackColor`, `hDC`, `hwnd`.

El formulario VB6 usa además propiedades de contenedor estándar (`Name`, `Index`, `Left`, `Top`, `Width`, `Height`, `TabIndex`, `Tag`, `ToolTipText`) y `DefaultCancel` en el UserControl.

### Eventos

`MouseDown`, `MouseUp`, `MouseMove`, `KeyDown`, `KeyPress`, `KeyUp`, `MouseOnButton`, `Click`, `DoubleClick`, `OLECompleteDrag`, `OLEDragDrop`, `OLEDragOver`, `OLEGiveFeedback`, `OLESetData`, `OLEStartDrag`.

### Enums

`ImagePlacementConstants`, `ImageSizeConstants`, `ButtonModeConstants`, `ButtonStyleConstants`, `HoverLockConstants`, `CustomCickConstants` (sic en fuente original), `GradientConstants`, `CaptionEffectConstants`, `FontStyles`, `BackStyleConstants`.

### Win32/GDI

El OCX usa declaraciones GDI32/USER32 para DC, bitmaps, fuentes, regiones, clipping, paletas, colores, texto, temporizador y cursor. El port usa principalmente `Graphics`, `Pen`, `Brush`, `LinearGradientBrush`, `TextRenderer`, `GraphicsPath` y APIs de WinForms; por tanto no es una sustitución binariamente equivalente.

### Pintado

Funciones relevantes VB6: `DrawButtonBackground`, `DrawButtonBorder`, `DrawCustomBorders`, `DrawButtonIcon`, `DrawCaptionIcon`, `DrawGradient`, `DrawFocusRectangle`, `CreateButtonRegion`, `ScaleImage`, `NormalizeImage`, y rutinas de estilo `DrawButton_Win95`, `DrawButton_Win31`, `DrawButton_WinXP`, `DrawButton_Macintosh`, `DrawButton_Flat`, `DrawButton_Hover`, `DrawButton_Netscape`, `DrawButton_Java`.

El port no copia literalmente esas rutinas: usa un único `OnPaint` con `GraphicsPath` y `GetBorders()`, que devuelve tres anillos con una pareja de color claro/oscuro por estado. Desde el 2026-11-05 la paleta se deriva de `BackColor`/`HoverBackColor` con la fórmula `ShadeColor` de VB6 (`Shade()`), por lo que `BackColor` ya afecta a los 8 estilos; también se añadieron `LopOffCorners1..4`, anillo de foco por familia y máscaras de imagen. Desde el 2026-11-06 hay tres anillos concéntricos, degradado vertical XP y estados normal/foco/pulsado/marcado. Aun así faltan los 8+ colores por lado y anillo, `FrameRgn`, los offsets de texto por estilo y el sombreado exacto del deshabilitado.

### Temporizador

`modLvTimer.bas` expone `lv_TimerCallBack` y usa `CopyMemory`/`GetProp`. El VB6 emplea callback global y mensajes de ventana. El port actual conserva `TimerUpdate(long)` y mantiene un `System.Windows.Forms.Timer` de 50 ms **por control**, activo solo entre `OnMouseEnter` y `OnMouseLeave` y con limpieza en `Dispose`. No reproduce el callback global Win32 ni `GetProp`. La detección administrada usa `GraphicsPath.IsVisible` para las formas soportadas; las máscaras `CustomFlat`/`Custom3DBorder` afectan a la `Region`, pero no al cálculo de hover.

### Formulario de ejemplo

`frmSample.frm` usa un array de tres `lvButtons1`, tres `OptMode`, controles de selección de tamaño/alineación, casillas de deshabilitar/gradientes/hover, imágenes `Image1(0..2)`, selección de estilo y handlers para cambiar `Mode`, `PictureSize`, `CaptionAlign`, `PictureAlign` y `MouseOnButton`.

## Inventario .NET

El control `LaVolpeButton` contiene enums compatibles `lv_*`, propiedades de apariencia, imagen, modo, foco, cursor, compatibilidad (`hwnd`, `hDC`), eventos de mouse/teclado/OLE, estados CheckBox/OptionButton, tooltips, accesibilidad, `IButtonControl` (`DialogResult`/`NotifyDefault`), `PerformClick`, `Cancel`, `Default`, `DragMode`, `DelayDrawing(bool)`, `Index`, `HelpContextID` y `WhatsThisHelpID`.

El ejemplo C# es funcional y usa recursos extraídos del FRX. No es todavía un formulario generado por diseñador: está construido en `SampleForm.cs`. El control incorpora metadatos `ToolboxItem`, `DesignTimeVisible`, `DesignerCategory`, `DefaultEvent` y `DefaultProperty`; la validación real de Visual Studio Designer requiere un entorno VS con WinForms.

## Teclado y navegación

El VB6 trata Space como pulsación visual seguida de Click, remite flechas al contenedor para navegación, y evita Enter/Escape automáticos en CheckBox/OptionButton. El port aplica Enter/Escape únicamente a `CommandButton` cuando `Default`/`Cancel` están activos, conserva el estado visual de Space y usa `SelectNextControl` para flechas. En `OptionButton`, las flechas recorren el grupo y seleccionan la opción siguiente. La navegación depende del orden/tabulación del contenedor WinForms, no de `PostMessage`/`Tag` como en VB6.

## Hallazgos críticos

1. El control original es un ActiveX OCX; el control .NET no puede conservar CLSID, registro COM, `DataObject` VB6 ni persistencia binaria OLE sin una capa COM adicional.
2. `hDC` y `hwnd` son adaptaciones de interoperabilidad, no el mismo ciclo de vida del UserControl VB6. El `hDC` del port ahora se obtiene de `Graphics.FromHwnd` y se conserva hasta `OnHandleDestroyed`/`Dispose` (antes se liberaba al salir del `get`).
3. `MousePointer` usa `MousePointerConstants` (15 valores VB6) y se mapea a `Cursor`; `MouseIcon` implica `Custom`. `DragIcon` crea su propio cursor/HICON. Faltan pruebas de transparencia real de ambos cursores.
4. Drag-and-drop emite `OLEStartDrag` antes de `DoDragDrop` y `OLECompleteDrag` después; las firmas siguen siendo adaptaciones WinForms y no `DataObject` VB6. `DragIcon` ya se aplica en `OnGiveFeedback`.
5. El temporizador global VB6 aún no está reproducido literalmente: hay un `Timer` de 50 ms por control.
6. Las formas usan geometría real (diagonales con `lEdge`, elipses para `Round*`), el control define `Region`, aplica `LopOffCorners1..4` y deriva la región de `CustomFlat`/`Custom3DBorder` desde la imagen. Queda la diferencia de que VB6 redimensiona el control a la imagen y el port estira la imagen al control.
7. Los estilos ya usan tres anillos concéntricos con colores por estado, pero cada anillo colapsa sus cuatro lados en una pareja claro/oscuro; no son aún las ocho rutinas `DrawButton_*` completas.
8. Los arrays/indexados del formulario VB6 se representan como controles independientes con nombres explícitos.
9. El ejemplo clona las imágenes al cargar y las libera al cerrar, evitando el bloqueo de archivos que produciría `Image.FromFile` directo.
10. `Text` ya refleja `Caption`, `Value` aplica la semántica de OptionButton/CommandButton y existe `IButtonControl` con `DialogResult`/`NotifyDefault`. Queda conectar `Default`/`Cancel` a `Form.AcceptButton`/`Form.CancelButton` sin depender del foco.

## Estado de los hallazgos de pintura (2026-11-05)

| Hallazgo | Estado |
|---|---|
| Fondo sin limpiar y `Region` nula | **Corregido**: `UpdateRegion()` asigna `Region` en las formas no rectangulares y el relleno cubre el rectángulo completo |
| Diagonales rectas | **Corregido**: polígonos con `lEdge = 4 * ((Height + 1) / 4)`, mínimo 5 px de ancho |
| `Round*` como rectángulo redondeado | **Corregido**: `AddEllipse`, equivalente a `CreateEllipticRgn(0,0,W,H)` |
| `BackColor` ignorado | **Corregido**: la paleta se deriva con `Shade()` (`adjBackColorUp/Dn`, `lessBlue`) |
| `hDC` liberado | **Corregido**: se cachea y se libera en `OnHandleDestroyed`/`Dispose` |
| Foco no repinta | **Corregido**: `OnGotFocus`/`OnLostFocus` invalidan |
| Recorte de esquinas por estilo | **Corregido**: `LopOffCorners1..4` para Win31/Java/XP/Macintosh |
| Máscara de `CustomFlat`/`Custom3DBorder` | **Corregido**: región desde `LockBits`, píxel superior izquierdo, límite 250 000 px |
| `Text`/`Caption` | **Corregido**: sincronizados en ambos sentidos y `null` normalizado |
| OptionButton/CheckBox | **Corregido**: exclusividad, `GotFocus`, flechas y supresión de clic repetido |
| `MouseIcon` | **Corregido**: hotspot centrado, `DestroyIcon` y `MousePointer = Custom` |
| `DragIcon` / `MousePointer` | **Corregido**: `MousePointerConstants` y cursor de arrastre propio |
| `Default`/`Cancel` | **Parcial**: `IButtonControl` y teclas con foco; falta `AcceptButton`/`CancelButton` |
| Indicador CheckBox/OptionButton | **Corregido**: se eliminó el glifo; toda la superficie usa `cCheckBox` |
| Ocho rutinas `DrawButton_*` | **Parcial**: tres anillos con colores por estilo; faltan los 4 lados independientes |

## Evidencia de compilación

Última compilación realizada (2026-11-06):

```powershell
dotnet build .\LaVolpe.Buttons.Net.sln --configuration Release
```

Resultado: **0 errores, 0 advertencias** para la biblioteca, el proyecto de ejemplo y el proyecto de pruebas. `LaVolpe.Buttons.Tests` ejecuta 25 casos y devuelve `25 correctas, 0 fallidas`.

> Corrección: la versión anterior de esta auditoría afirmaba que la solución completa fallaba por
> `Microsoft.NET.SDK.WorkloadAutoImportPropsLocator` / `WorkloadManifestTargetsLocator` con el SDK
> 10.0.403. Esa observación ya no aplica: con el `global.json` del repositorio (SDK 8.0.427) la
> solución compila entera. El fallo era del entorno (SDK preview sin workloads), no del código.

## Distribución

Se realizó `dotnet publish` en `artifacts/publish`. El resultado contiene la biblioteca, ejecutable, archivos de runtime/dependencias, símbolos PDB y los tres recursos PNG. La publicación depende del .NET 8 Desktop Runtime y no instala ni registra el OCX VB6.

## Conclusión

El port es funcional para WinForms y tiene una cobertura amplia de la API superficial. No debe declararse equivalente al 100 % hasta probar visualmente cada forma/estado, revisar el temporizador y verificar diseñador, persistencia, OLE y formulario completo.
