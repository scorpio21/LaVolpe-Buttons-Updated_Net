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

### Temporizador

`modLvTimer.bas` expone `lv_TimerCallBack` y usa `CopyMemory`/`GetProp`. El VB6 emplea callback global y mensajes de ventana. El port actual conserva `TimerUpdate(long)` y ahora mantiene un `System.Windows.Forms.Timer` de 50 ms por control, con limpieza en `Dispose`. No reproduce el callback global Win32 ni `GetProp`. La detección administrada usa `GraphicsPath.IsVisible` para las formas actualmente soportadas y el timer la consulta cada 50 ms; las máscaras bitmap de `CustomFlat`/`Custom3DBorder` aún no se derivan de una imagen.

### Formulario de ejemplo

`frmSample.frm` usa un array de tres `lvButtons1`, tres `OptMode`, controles de selección de tamaño/alineación, casillas de deshabilitar/gradientes/hover, imágenes `Image1(0..2)`, selección de estilo y handlers para cambiar `Mode`, `PictureSize`, `CaptionAlign`, `PictureAlign` y `MouseOnButton`.

## Inventario .NET

El control `LaVolpeButton` contiene enums compatibles `lv_*`, propiedades de apariencia, imagen, modo, foco, cursor, compatibilidad (`hwnd`, `hDC`), eventos de mouse/teclado/OLE, estados CheckBox/OptionButton, tooltips, accesibilidad, `PerformClick`, `Cancel`, `Default`, `DragMode`, `Index`, `HelpContextID` y `WhatsThisHelpID`.

El ejemplo C# es funcional y usa recursos extraídos del FRX. No es todavía un formulario generado por diseñador: está construido en `SampleForm.cs`. El control incorpora metadatos `ToolboxItem`, `DesignTimeVisible`, `DesignerCategory`, `DefaultEvent` y `DefaultProperty`; la validación real de Visual Studio Designer requiere un entorno VS con WinForms.

## Teclado y navegación

El VB6 trata Space como pulsación visual seguida de Click, remite flechas al contenedor para navegación, y evita Enter/Escape automáticos en CheckBox/OptionButton. El port ahora aplica Enter/Escape únicamente a `CommandButton` cuando `Default`/`Cancel` están activos, conserva el estado visual de Space y usa `SelectNextControl` para flechas. La navegación depende del orden/tabulación del contenedor WinForms, no de `PostMessage`/`Tag` como en VB6.

## Hallazgos críticos

1. El control original es un ActiveX OCX; el control .NET no puede conservar CLSID, registro COM, `DataObject` VB6 ni persistencia binaria OLE sin una capa COM adicional.
2. `hDC` y `hwnd` son adaptaciones de interoperabilidad, no el mismo ciclo de vida del UserControl VB6.
3. `MousePointer`/`MouseIcon` necesitan pruebas reales de hotspot, transparencia y liberación de cursor.
4. Drag-and-drop ahora emite `OLEStartDrag` antes de `DoDragDrop` y `OLECompleteDrag` después; las firmas siguen siendo adaptaciones WinForms y no `DataObject` VB6.
4. `DragMode` WinForms y OLE DragDrop no tienen equivalencia exacta de firmas.
5. El temporizador global VB6 aún no está reproducido literalmente.
6. Las formas y estilos están implementados conceptualmente, pero requieren capturas comparativas contra VB6 para determinar equivalencia.
7. Los arrays/indexados del formulario VB6 se representan como controles independientes con nombres explícitos.
8. El ejemplo clona las imágenes al cargar y las libera al cerrar, evitando el bloqueo de archivos que produciría `Image.FromFile` directo.

## Evidencia de compilación

Última compilación realizada:

```powershell
dotnet build .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj --configuration Release
```

Resultado: **0 errores, 0 advertencias** al compilar el proyecto de ejemplo directamente. La compilación de la solución completa con el SDK instalado 10.0.403 queda afectada por la resolución de `Microsoft.NET.SDK.WorkloadAutoImportPropsLocator`/`WorkloadManifestTargetsLocator`; los proyectos individuales sí compilan correctamente.

## Distribución

Se realizó `dotnet publish` en `artifacts/publish`. El resultado contiene la biblioteca, ejecutable, archivos de runtime/dependencias, símbolos PDB y los tres recursos PNG. La publicación depende del .NET 8 Desktop Runtime y no instala ni registra el OCX VB6.

## Conclusión

El port es funcional para WinForms y tiene una cobertura amplia de la API superficial. No debe declararse equivalente al 100 % hasta probar visualmente cada forma/estado, revisar el temporizador y verificar diseñador, persistencia, OLE y formulario completo.
