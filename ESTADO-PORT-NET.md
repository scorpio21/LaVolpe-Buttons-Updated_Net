# Estado del port de LaVolpe Buttons a .NET

**Fecha de esta revisión:** 2026-11-06 (tercera ronda)
**Revisión anterior:** 2026-11-05 (segunda ronda) - 70 %
**Estado general estimado:** **76 %**

Archivos originales: `k:\Proyectos\LaVolpe-Buttons-Updated\LaVolpe-Buttons-Updated-\`
Archivos .NET: `k:\Proyectos\LaVolpe-Buttons-Updated\LaVolpe.Buttons.Net\`
Archivos .NET Sample: `k:\Proyectos\LaVolpe-Buttons-Updated\LaVolpe.Buttons.Sample\`
Pruebas: `k:\Proyectos\LaVolpe-Buttons-Updated\LaVolpe.Buttons.Tests\`

> Los porcentajes son estimaciones técnicas, no equivalencia binaria ni comparación píxel a píxel.
> La revisión del 2026-11-04 **bajó** las cifras porque se midieron ejecutando el control. Las dos
> rondas del 2026-11-05 suben las áreas realmente corregidas. La tercera ronda (2026-11-06) porta los
> tres anillos de borde por estilo, el degradado XP, el estado marcado de CheckBox/OptionButton,
> `MousePointerConstants` y `DragIcon`; el pintado fino y la comparación píxel a píxel siguen abiertos.

## Resumen por área

| Área | 2026-11-02 | 2026-11-04 | 2026-11-05 ronda 1 | 2026-11-05 ronda 2 | 2026-11-06 ronda 3 | Motivo del último cambio |
|---|---:|---:|---:|---:|---:|---|
| Control WinForms funcional | 90 % | 75 % | 82 % | 85 % | 88 % | `MousePointerConstants`, `DragIcon` y ciclo de vida de cursores |
| Propiedades y enums | 80 % | 65 % | 67 % | 82 % | 90 % | Enum de puntero, `DragIcon`, `Value` y defaults cubiertos por pruebas |
| Eventos de mouse y teclado | 80 % | 65 % | 66 % | 70 % | 70 % | Sin cambios: OLE y arrastre siguen adaptados |
| CheckBox y OptionButton | 80 % | 45 % | 45 % | 78 % | 90 % | Toda la superficie cambia de estado; se eliminó el glifo de 14 px |
| Eventos OLE | 50 % | 40 % | 40 % | 40 % | 40 % | Sin cambios: adaptaciones WinForms |
| Estados visuales | 80 % | 50 % | 55 % | 66 % | 82 % | Estados normal/foco/pulsado/marcado con colores por estilo |
| Formas gráficas | 70 % | 30 % | 55 % | 80 % | 82 % | Imagen encogida al pulsar y reglas de caption para `Custom*` |
| Estilos visuales | 65 % | 25 % | 32 % | 42 % | 72 % | Tres anillos concéntricos y parejas de color por estilo |
| Imágenes y escalado | 80 % | 55 % | 55 % | 70 % | 72 % | Reglas de forma personalizada; falta redimensionar el control |
| Recursos `.frx` | 85 % | 75 % | 75 % | 80 % | 80 % | Sin cambios |
| Formulario de ejemplo | 85 % | 35 % | 35 % | 58 % | 58 % | Sin cambios: la demo sigue siendo un subconjunto |
| Proyecto de ejemplo | 90 % | 60 % | 60 % | 65 % | 65 % | Sin cambios |
| Diseñador de Visual Studio | 60 % | 45 % | 46 % | 50 % | 50 % | Sin validación real en el diseñador |
| Instalación/distribución | 30 % | 35 % | 35 % | 35 % | 35 % | Sin NuGet ni instalador |
| Pruebas automatizadas | 25 % | 0 % | 0 % | 50 % | 62 % | 25 casos: puntero, `DragIcon` y superficie marcada |
| Equivalencia visual con VB6 | 50 % | 30 % | 38 % | 45 % | 60 % | Anillos y degradado XP; falta comparación píxel a píxel |
| **Aplicación completa exportada** | **75 %** | **53 %** | **58 %** | **70 %** | **76 %** | Media ponderada de las áreas (ver metodología) |

### Metodología

El porcentaje global es la media ponderada de las filas anteriores con estos pesos aproximados:
control funcional 20, propiedades 12, checkbox/option 10, eventos mouse/teclado 8, estados 8,
formas 8, estilos 8, OLE 5, imágenes 5, formulario 5, recursos 3, diseñador 3, proyecto 2,
distribución 2, pruebas 1. Resultado 2026-11-06 (ronda 3): **76,0 %**.

## Evidencia de la revisión 2026-11-04 (detección)

Todo lo siguiente se ejecutó el 2026-11-04 sobre el repositorio, antes de las correcciones:

| Comprobación | Resultado |
|---|---|
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | **0 errores, 0 advertencias** (SDK 8.0.427) |
| `dotnet build .\LaVolpe.Buttons.Sample\...csproj -c Release` (desde `bin`/`obj` limpios) | 0 errores; los 3 PNG llegan a `bin\Release\net8.0-windows\Resources` |
| Ejecutar `LaVolpe.Buttons.Sample.exe` | Arranca y dibuja los 4 botones |
| `BackColor = Red` + `ButtonStyle = WindowsXp` | Pinta `#ECF2FE`: el `BackColor` **se ignora** |
| Gradientes en 4 direcciones | Correctos (izq→der, der→izq, arriba→abajo, abajo→arriba) |
| `b.Value = true; c.Value = true` con dos OptionButton | Ambos quedan `True` (VB6 deja `b = False`) |
| `PerformClick()` sobre un OptionButton ya seleccionado | Dispara `Click` (VB6 lo suprime) |
| `Value = true` en modo CommandButton | Aceptado (VB6 lo rechaza) |
| Mover/recuperar el foco | **0** eventos `Invalidated`: el rectángulo de foco no se repinta |
| `Caption = "Hola"` | `Text` sigue vacío |
| `ButtonShape = CustomFlat` + `PictureSize = Size16x16` | No se fuerza `Fill_ScaleUpDown` |
| `Region` de los controles | `null` en las 8 formas (VB6 usa `SetWindowRgn`) |
| `hDC` | Devuelve un HDC de un `Graphics` ya liberado por el `using` |
| `PerformClick()` con `Enabled = false` | 0 clics (correcto) |

> Nota: la afirmación de `AUDITORIA_VB6_NET8.md` de que la solución **no** compila por los
> resolvers de workload está obsoleta. Con `global.json` a 8.0.427 la solución compila entera.

## Evidencia de la revisión 2026-11-05 (ronda 1)

| Comprobación | Resultado |
|---|---|
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | **0 errores, 0 advertencias** |
| `Region` en formas no rectangulares | `LeftDiagonal`, `RightDiagonal`, `FullDiagonal`, `Round3D`, `Round3DFixed`, `RoundFlat` → `Region != null` |
| Recorte de esquinas de las diagonales | `IsVisible` fuera/dentro correcto en los tres casos |
| `Round3D` es elipse | `IsVisible(2, 2) == false`, `IsVisible(85, 30) == true` |
| `BackColor = Red` + estilo XP | Píxel central `ARGB(255,255,48,0)`; ya **no** pinta `#ECF2FE` |
| `BackColor = Gold` + estilo Flat | Píxel central `ARGB(255,255,215,0)` |
| Foco | `Focus()` → 1 evento `Invalidated`; `Focused == true` |
| `hDC` tras `Refresh()` | `BITSPIXEL == 32`; el handle no cambia entre llamadas |
| Captura de pantalla real | Diagonales inclinadas y elipses recortadas contra el fondo |
| Gradientes en 4 direcciones | Siguen correctos |
| `PerformClick()` con `Enabled = false` | 0 clics (correcto) |

## Evidencia de la revisión 2026-11-05 (ronda 2)

Sonda integral ejecutada fuera del repositorio y, después, **portada al proyecto
`LaVolpe.Buttons.Tests`** (19 casos, todos en verde):

| Comprobación | Resultado |
|---|---|
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | **0 errores, 0 advertencias** |
| `LaVolpe.Buttons.Tests.exe` | **19 correctas, 0 fallidas** (código de salida 0) |
| `Caption` → `Text` y `Text` → `Caption` | Sincronizados; `null` se normaliza a cadena vacía |
| Defaults de VB6 | `ButtonStyle = Windows95`, `HoverColorLocks = TextAndBackColor`, hover = `BackColor`, `BackColor` heredado del contenedor |
| `Value = true` en CommandButton | Rechazado |
| `PerformClick` + `DialogResult` | Dispara `Click` y asigna `Form.DialogResult`; con `Enabled = false` no dispara |
| OptionButton | Deselecciona hermanos, `GotFocus` selecciona y el clic sobre la opción ya elegida no dispara `Click` |
| CheckBox | Alterna `Value` en cada clic |
| `IButtonControl` / `NotifyDefault` | `DialogResult` y `Default` sincronizados |
| Regiones | 6 formas no rectangulares con `Region`; rectangular XP recorta la esquina `(0,0)` |
| `CustomFlat` | Fuerza `FillScaleUpDown` y genera `Region` desde la máscara de la imagen |
| Imagen liberada por el usuario | No derriba el control: se ignora al calcular la región y al pintar |
| `MouseIcon` | Crea un cursor propio y lo restaura al asignar `null` |
| `DelayDrawing(bool)` | Acepta el cambio de color sin lanzar y repinta |
| `hDC` | `BITSPIXEL == 32` |
| Captura del ejemplo | `sample.png` renderiza los 8 estilos, las 7 formas, los 3 modos, gradiente, imagen y `CustomFlat` |
| Captura de estilos | `styles.png` muestra bordes, biseles y anillo de foco por estilo |

## Evidencia de la revisión 2026-11-06 (ronda 3)

| Comprobación | Resultado |
|---|---|
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | **0 errores, 0 advertencias** |
| `dotnet run --project .\LaVolpe.Buttons.Tests -c Release` | **25 correctas, 0 fallidas** (código de salida 0) |
| Tres anillos de borde | `DrawRing` dibuja los anillos 0/1/2 con parejas de color por estilo y estado |
| Degradado XP | Fondo vertical `BackColor → Shade(back, -25, true)` cuando `GradientMode = None` |
| Estado marcado | CheckBox/OptionButton con `Value = True` usan `cCheckBox` en toda la superficie; prueba de píxel central |
| Glifo de 14 px | Eliminado; ya no se reserva espacio horizontal en el caption |
| `MousePointerConstants` | `Arrow`, `Crosshair`, `Hourglass`, `SizeWE`, `Default` mapeados y probados |
| `MouseIcon` | Implica `MousePointer = Custom`; cursor creado y restaurado |
| `DragIcon` | Cursor creado con `CreateIconIndirect` y liberado al asignar `null` |
| `CustomFlat`/`Custom3DBorder` | Caption sólo con `Mode > CommandButton` y `Value = True`; imagen oculta en ese caso |
| Pulsado en `Custom*` | Con `CustomClick = Default` la imagen se encoge 1 px por lado |
| Capturas | `sample.png` y `styles.png` renderizadas y revisadas: 8 estilos, estados normal/marcado y 4 formas |

## Porcentaje por propiedad VB6

| Propiedad | 2026-11-02 | 2026-11-05 (ronda 2) | Estado |
|---|---:|---:|---|
| `Name` | 100 % | 100 % | Heredada de `Control` |
| `BackColor` | 100 % | 92 % | Se aplica en los 8 estilos; los tonos se derivan con `Shade()` |
| `ButtonShape` | 100 % | 85 % | Diagonales, elipses, `LopOffCorners` y máscaras; `Custom*` estira en vez de redimensionar |
| `ButtonStyle` | 100 % | 75 % | Tres anillos de borde y colores por estado según `DrawButton_*` |
| `Cancel` | 65 % | 60 % | `Escape` con foco; no se conecta solo a `Form.CancelButton` |
| `Caption` | 100 % | 95 % | `Text` sincronizado y mnemónico resuelto |
| `CaptionAlign` | 100 % | 75 % | Se expone `ContentAlignment` (9 valores) en vez de 3 alineaciones |
| `CaptionStyle` | 100 % | 65 % | Sombra aproximada, sin offsets/colores por estilo |
| `CausesValidation` | 100 % | 100 % | Heredada |
| `CustomClick` | 100 % | 65 % | Desplazamiento implementado; no se restringe a formas custom |
| `Default` | 65 % | 60 % | `Enter` con foco y `NotifyDefault`; falta uso automático del `AcceptButton` |
| `DragIcon` | 50 % | 85 % | `Image` propia, cursor/HICON creado y liberado; usada en `OnGiveFeedback` |
| `DragMode` | 65 % | 45 % | `DoDragDrop`; efectos y datos OLE distintos |
| `Enabled` | 100 % | 95 % | Heredada y con pintura deshabilitada |
| `Font` | 100 % | 95 % | Hereda la fuente del contenedor, como VB6 |
| `FontStyle` | 100 % | 95 % | Alias `lv_Bold`…`lv_BoldItalicUnderline` |
| `ForeColor` | 100 % | 90 % | Heredada |
| `GradientColor` | 100 % | 85 % | Funciona; no usa todas las fórmulas de VB6 |
| `GradientMode` | 100 % | 90 % | Las 4 direcciones verificadas |
| `Height` | 100 % | 100 % | Heredada |
| `HelpContextID` | 30 % | 20 % | Almacenada; sin `HelpProvider` |
| `HoverBackColor` | 100 % | 85 % | Default = `BackColor`; locks respetados |
| `HoverColorLocks` | 100 % | 90 % | Semántica y default de VB6 |
| `HoverForeColor` | 100 % | 85 % | Igual que el anterior |
| `Index` | 60 % | 40 % | Propiedad suelta; no recrea control arrays |
| `Left` | 100 % | 100 % | Heredada |
| `Mode` | 100 % | 90 % | Transiciones de VB6 para `Value` y selección |
| `MouseIcon` | 55 % | 85 % | Hotspot centrado, `HICON` liberado e integrado con `MousePointer` |
| `MousePointer` | 100 % | 100 % | `MousePointerConstants` con mapa a `Cursor`; `MouseIcon` implica `vbCustom` |
| `Picture` | 100 % | 82 % | Máscara por color/alfa y escalado; falta transparencia exacta |
| `PictureAlign` | 100 % | 82 % | Cálculo aproximado en casos extremos |
| `PictureSize` | 100 % | 85 % | `Fill_ScaleUpDown`, `FillStretch` y encogido al pulsar en `Custom*`; falta redimensionar el control |
| `ResetDefaultColors` | 100 % | 75 % | Restaura colores y vuelve a heredar del contenedor al cambiar de padre |
| `ShowFocusRect` | 100 % | 92 % | Anillo por familia de estilo; las formas `Custom*` no lo dibujan |
| `TabIndex` / `TabStop` | 100 % | 100 % | Heredadas |
| `Tag` | 100 % | 100 % | Heredada |
| `ToolTipText` | 85 % | 85 % | Componente `ToolTip` externo |
| `Top` | 100 % | 100 % | Heredada |
| `Value` | 100 % | 95 % | CommandButton lo rechaza; CheckBox/OptionButton marcan toda la superficie |
| `Visible` | 100 % | 100 % | Heredada |
| `WhatsThisHelpID` | 30 % | 20 % | Almacenada; sin integración |
| `Width` | 100 % | 100 % | Heredada |

**Promedio de estas propiedades: ~84 %** (80 % el 2026-11-02, 67 % el 2026-11-04, ~70 % en la
primera ronda y ~79 % en la segunda ronda del 2026-11-05).

## Ya implementado

* Proyecto C# `net8.0-windows`, solución y `global.json` funcionando.
* Control `LaVolpeButton` basado en `System.Windows.Forms.Control`, con doble buffer y `UserPaint`.
* `IButtonControl` (`DialogResult`, `NotifyDefault`) y teclas `Enter`/`Escape`/flechas.
* `Region` de ventana para formas no rectangulares, recalculada al redimensionar y al cambiar de forma.
* Diagonales reales (`DiagonalEdge`), elipses en `Round3D`/`Round3DFixed`/`RoundFlat` y
  `LopOffCorners` para Win31/Java/XP/Macintosh.
* Máscara de imagen para `CustomFlat`/`Custom3DBorder`: región construida con `LockBits`, píxel
  superior izquierdo como color transparente, límite de 250 000 píxeles y tolerancia a imágenes liberadas.
* Paleta de los 8 estilos derivada de `BackColor`/`HoverBackColor` con la fórmula `ShadeColor`,
  incluidos `adjBackColorUp`, `adjBackColorDn` y `cCheckBox`.
* `DrawButtonBackground`/`DrawButtonBorder`: tres anillos concéntricos con parejas de color por
  estilo y estado, degradado vertical fijo en XP y borde sólo al pasar el ratón en `Hover`.
* Bisel por estilo, foco rectangular/elíptico y desplazamiento al pulsar.
* `hDC` cacheado y liberado en `OnHandleDestroyed`/`Dispose`; repintado en foco y hover.
* Modos CommandButton, CheckBox y OptionButton con exclusividad entre hermanos, auto-selección al
  recibir foco, navegación con flechas, supresión del clic repetido y estado marcado en toda la
  superficie (sin glifo independiente, como el `Style = Graphical` de VB6).
* `Text` ↔ `Caption`, defaults de VB6 y alias `lv_*` para `BackStyle`, `ButtonShape`, `ButtonMode`,
  `FontStyles`, `HoverColorLocks`, `ImagePlacement`, `ImageSize`, `CustomClick`, `CaptionStyle` y
  `GradientMode`.
* Imágenes con `PictureAlign`/`PictureSize`; `FillScaleUpDown` conserva proporción.
* `MouseIcon` con `CreateIconIndirect`, hotspot centrado y `DestroyIcon`; `MousePointerConstants`
  con mapa a `Cursor`; `DragIcon` con cursor/HICON propio usado durante el arrastre.
* `DelayDrawing(bool)`, `BeginDelayDrawing()`, `EndDelayDrawing()`, `RedrawButton()` y
  `DelayDrawingEnabled` obsoleto.
* Eventos `MouseOnButton`, `ValueChanged`, `CheckedChanged`, `SelectedChanged`, `ButtonDoubleClick`,
  `LvMouseDown/Up/Move`, `LvKeyDown/Up`, `LvKeyPress` y OLE adaptados.
* Timer administrado de 50 ms por control, activo solo mientras el puntero está encima.
* Proyecto de pruebas `LaVolpe.Buttons.Tests` sin dependencias externas (25 casos, STA real).
* Ejemplo ampliado con los 8 estilos, 7 formas, 3 modos, `Default`/`Cancel`, gradiente, imagen y
  `CustomFlat`.
* Recursos del `.frx` extraídos con script de rutas relativas y copiados al output del ejemplo.
* Documentación de estado, compatibilidad, auditoría y pruebas.

## Fidelidad visual

El port reproduce ya el modelo de tres anillos (`GetBorders`) con parejas de color
superior/izquierda e inferior/derecha por estilo y estado, más el degradado vertical de XP. VB6 tiene
ocho rutinas `DrawButton_*` de ~100 líneas cada una, con colores independientes para los cuatro lados
de cada anillo, `DrawGradient`, `FrameRgn`, recorte de esquinas por estilo, variantes de foco por
estilo y sombreado propio del deshabilitado.

Diferencias concretas (estado 2026-11-06, ronda 3):

* **Formas:** implementadas. `Round*` es elipse (`AddEllipse`, equivalente a
  `CreateEllipticRgn`, `lvButtons.ctl:1394`), las diagonales usan
  `lEdge = 4 * ((Height + 1) / 4)` (`:1433`) y las esquinas se recortan con `LopOffCorners1..4`.
* **Región de ventana:** implementada para formas y máscaras. En `Custom*`, VB6 redimensiona el
  control para ajustarlo a la imagen; el port estira la imagen al control actual.
* **Transparencia:** el port usa el píxel superior izquierdo (±10 por canal) o alpha < 20, con el
  mismo espíritu que `NormalizeImage`, pero sin la alineación exacta de regiones del OCX.
* **Texto:** VB6 calcula `CalculateBoundingRects` y aplica offsets/colores por estilo y por efecto
  raised/sunken; el port hace una sola pasada de `TextRenderer.DrawText`.
* **CheckBox/OptionButton:** el port ya no dibuja glifo; marca toda la superficie con `cCheckBox`,
  como el `Style = Graphical` de VB6. Faltan los matices de borde exactos por estilo.
* **Colores:** `GetBorders` colapsa los cuatro lados de cada anillo en una pareja claro/oscuro; VB6
  usa hasta cuatro colores distintos por anillo (por ejemplo XP). El resultado es cercano, no
  idéntico.
* **Deshabilitado:** el sombreado es aproximado; VB6 aplica `ShadeColor(..., -&H68, True)` y variantes
  por estilo.

## Bloqueos principales (prioridad alta)

Cerrados en las tres rondas: fondo/`Region`, geometría, `BackColor`, `hDC`, foco,
OptionButton/CheckBox, `Text`/`Caption`, `MouseIcon`, `MousePointerConstants`, `DragIcon`, defaults,
alias, `DelayDrawing`, máscaras, recorte de esquinas, anillos de borde y degradado XP. Quedan:

1. **Pintado fino de los 8 estilos**: colores independientes por lado y anillo, `FrameRgn`,
   offsets de texto y sombreado deshabilitado exacto.
2. **Formas personalizadas**: VB6 redimensiona el control a la imagen; decidir si se replica ese
   comportamiento (hoy se estira la imagen).
3. **OLE real**: los eventos `OLEDragDrop`, `OLEStartDrag`, etc. son adaptaciones WinForms.
4. **Diseñador de Visual Studio**: validar Toolbox, serialización, reapertura y edición.
5. **DPI**: probar 96/120/144/192 y el redimensionado de formas.
6. **Formulario VB6 completo**: la demo .NET sigue siendo un subconjunto de los 16 botones de
   `frmSample.frm`.
7. **Distribución**: no hay paquete NuGet ni instalador.

## Pendiente para llegar al 100 %

### 1. Pintado

* Completar las ocho rutinas `DrawButton_*` (`Win95`, `Win31`, `WinXP`, `Macintosh`, `Flat`, `Hover`,
  `Netscape`, `Java`) con colores independientes por lado de cada anillo.
* Igualar `FrameRgn`, los offsets de texto por estilo y el sombreado del estado deshabilitado.
* Reproducir la transparencia exacta y la alineación de regiones de `NormalizeImage`.

### 2. API y compatibilidad de código fuente

* Revisar orden y firmas de eventos VB6, incluido el `DoubleClick` que simula un segundo click.
* `Index` como control array real o documentar definitivamente que es una propiedad suelta.
* Integración de `HelpContextID`/`WhatsThisHelpID` con `HelpProvider`.

### 3. Formulario y proyecto VB6

* Convertir `frmSample.frm` completo: 16 `lvButtons_H`, 3 `OptMode`, combos de tamaño/alineación,
  casillas de gradiente/hover/deshabilitado y las imágenes `Image1(0..2)`.
* Convertir `prjSampleButtons.vbp`.
* Documentar formalmente la sustitución de `modLvTimer.bas` (hoy es un `Timer` por control).

### 4. Diseñador y distribución

* Validar en Visual Studio: Toolbox, inserción, edición, reapertura y serialización.
* Completar `[Description]` y `[DefaultValue]` en las propiedades restantes.
* Paquete NuGet o instalador con DLL, docs y ejemplo.

### 5. Pruebas

* Ampliar `LaVolpe.Buttons.Tests` con DPI 96/120/144/192, redimensionado y mnemónicos.
* Pruebas de `MouseOnButton` con movimiento real del puntero.
* Capturas de referencia y comparación contra el OCX original.

## Verificación actual

```powershell
dotnet build .\LaVolpe.Buttons.Net.sln --configuration Release
dotnet run --project .\LaVolpe.Buttons.Tests --configuration Release
```

Resultado (2026-11-06, ronda 3):

```text
Compilación correcta.
    0 Advertencia(s)
    0 Errores

LaVolpe.Buttons.Tests - pruebas de compatibilidad
25 correctas, 0 fallidas
```

También se renderizó el ejemplo fuera de pantalla (`sample.png`) y una matriz de estilos
(`styles.png`); ambas capturas se revisaron visualmente. Las comprobaciones que no pueden hacerse
con `DrawToBitmap` (recorte de `Region`) usan `Region.IsVisible`.

## Historial

| Fecha | Valor | Comentario |
|---|---:|---|
| 2026-11-02 | 75 % | Estimación basada en compilación y revisión de código |
| 2026-11-04 | 53 % | Revisión con ejecución real, sondas de comportamiento y comparación con `lvButtons.ctl` |
| 2026-11-05 (ronda 1) | 58 % | Corregidos fondo/`Region`, diagonales y elipses, `BackColor` derivado, `hDC` y foco |
| 2026-11-05 (ronda 2) | 70 % | Semántica VB6, alias `lv_*`, máscaras/`LopOffCorners`, `MouseIcon`, `DelayDrawing`, demo ampliada y 19 pruebas |
| 2026-11-06 (ronda 3) | **76 %** | Tres anillos de borde, degradado XP, estado marcado sin glifo, `MousePointerConstants`, `DragIcon` y 25 pruebas |

## Conclusión

El port es una **implementación funcional y compilable** de WinForms, con cobertura amplia de la API
superficial, semántica de VB6 para los tres modos, regiones/máscaras reales y bordes por estilo con
tres anillos. Todavía **no es un reemplazo visual píxel a píxel** del OCX: faltan los colores
independientes por lado, los offsets de texto, el sombreado deshabilitado exacto y la validación en
diseñador/DPI. El resto es distribución, demo completa y pruebas de comparación.
