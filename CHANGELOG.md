# Changelog

## 2026-11-06 (ronda 3)

### Añadido

- `MousePointerConstants` con los 15 valores VB6 (y alias `vb*`); `MousePointer` ahora es el enum
  y se mapea a `Cursor`; asignar `MouseIcon` implica `Custom`.
- `DragIcon` real: `Image` → cursor/HICON con `CreateIconIndirect`, liberado al asignar `null` y
  aplicado en `OnGiveFeedback`.
- `DrawButtonBackground`/`DrawButtonBorder` con tres anillos concéntricos y parejas de color por
  estilo y estado (`normal`, foco, pulsado, marcado), más el degradado vertical fijo de XP.
- CheckBox/OptionButton marcan toda la superficie con `cCheckBox`, como `Style = Graphical` de VB6.
- Reglas de forma personalizada: caption sólo en modo CheckBox/OptionButton con `Value = True`,
  imagen oculta en ese estado y encogido de 1 px al pulsar con `CustomClick = Default`.
- Seis pruebas nuevas (25 en total): mapeo de punteros, `MouseIcon → Custom`, `DragIcon`,
  superficie marcada, los 8 estilos en ambos estados y `CustomFlat` marcado.

### Corregido

- Eliminado el indicador/glyph de 14 px propio del port; ya no reserva espacio en el caption.
- El foco no se dibuja en formas `Custom*`, como `DrawFocusRectangle` de VB6.
- El anillo de foco XP/Java usa el color por estilo en vez de un azul genérico.

### Verificado

- `dotnet build .\LaVolpe.Buttons.Net.sln -c Release`: 0 errores, 0 advertencias.
- `LaVolpe.Buttons.Tests`: 25 correctas, 0 fallidas.
- `sample.png` y `styles.png` renderizados y revisados.

### Pendiente

- Colores independientes por lado de cada anillo, `FrameRgn`, offsets de texto y deshabilitado exacto.
- Comparación píxel a píxel de los 8 estilos.
- DPI 96/120/144/192, diseñador de Visual Studio y DragDrop externo.
- Formulario VB6 completo (`frmSample.frm`), paquete NuGet/instalador.

## 2026-11-05 (ronda 2)

### Añadido

- Proyecto `LaVolpe.Buttons.Tests`: ejecutable de pruebas STA sin dependencias, 19 casos de
  compatibilidad y código de salida distinto de cero si algo falla.
- `IButtonControl` (`DialogResult`, `NotifyDefault`) y sincronización `Text` ↔ `Caption`.
- Alias `lv_*` completos para `BackStyle`, `FontStyles` y `HoverColorLocks`.
- `MouseIcon` con `CreateIconIndirect`, hotspot centrado y `DestroyIcon`.
- Máscaras de `CustomFlat`/`Custom3DBorder` con `LockBits` y `LopOffCorners1..4` por estilo.
- `DelayDrawing(bool)`, `BeginDelayDrawing()` y `EndDelayDrawing()`; `DelayDrawingEnabled` queda obsoleto.
- Semántica VB6 de `Value`: rechazo en `CommandButton`, exclusividad y `GotFocus` en `OptionButton`,
  supresión del clic repetido y navegación con flechas dentro del grupo.
- Defaults de VB6: `ButtonStyle = Windows95`, `HoverColorLocks = TextAndBackColor`, hover y
  `BackColor` heredados del contenedor, y fuente heredada en vez de Times New Roman.
- Ejemplo ampliado: 8 estilos, 7 formas, 3 modos, `Default`/`Cancel`, gradiente, imagen y `CustomFlat`.
- Script `extract_frx_images.py` con rutas relativas y argumentos, sin rutas `K:\` fijas.

### Corregido

- La `Region` de las formas se calcula también antes de crear el handle y se actualiza al cambiar
  forma, estilo, tamaño o `Enabled`.
- El hover ya no mantiene un timer encendido todo el tiempo: arranca en `OnMouseEnter` y se detiene
  en `OnMouseLeave`/`OnHandleDestroyed`.
- Imágenes liberadas por el usuario ya no provocan una excepción al recalcular la región.
- El ejemplo y la matriz de estilos se renderizaron y revisaron visualmente.

### Verificado

- `dotnet build .\LaVolpe.Buttons.Net.sln -c Release`: 0 errores, 0 advertencias.
- `LaVolpe.Buttons.Tests`: 19 correctas, 0 fallidas.

### Pendiente

- Comparación píxel a píxel de los 8 estilos y las 8 rutinas `DrawButton_*`.
- Indicadores nativos de CheckBox/OptionButton.
- DPI 96/120/144/192, diseñador de Visual Studio y DragDrop externo.
- Formulario VB6 completo (`frmSample.frm`), paquete NuGet/instalador.

## Estado anterior — 2026-11-03

### Añadido

- Port WinForms para `lvButtons_H` sobre .NET 8 Windows Forms.
- Enums C# con alias `lv_*` del control VB6.
- Modos CommandButton, CheckBox y OptionButton.
- Estados hover, pulsado, foco, seleccionado y deshabilitado.
- Propiedades de caption, color, gradiente, forma, imagen y alineación.
- Eventos adaptados de mouse, teclado, click, doble click y OLE/drag-drop.
- `ValueChanged`, `CheckedChanged`, `SelectedChanged` y `PerformClick()`.
- `ToolTipText`, `MouseIcon`, `Cancel`, `Default`, `DragMode`, `Index`, `HelpContextID` y `WhatsThisHelpID`.
- Accesibilidad básica con `AccessibleName` y `AccessibleDescription`.
- Extracción de tres recursos de `frmSample.frx`.
- Integración de `image0.png`, `image1.png` e `image2.png` en el ejemplo.
- Timer administrado de 50 ms por control para actualizar `MouseOnButton`.
- Limpieza del timer, tooltip y cursor personalizado en `Dispose`.
- `global.json` para fijar SDK .NET 8.0.427.
- Carga de imágenes del ejemplo mediante clonación y liberación explícita al cerrar.
- Añadida documentación `EVIDENCIA_Y_DIFERENCIAS.md` con separación entre hechos observados, diferencias confirmadas y pruebas pendientes.

### Verificado

- Compilación Release del proyecto .NET y ejemplo.
- Resultado más reciente: 0 errores y 0 advertencias.

### Pendiente

- Comparación visual automatizada con el OCX VB6.
- Reproducción literal del callback de `modLvTimer.bas`.
- Equivalencia completa de eventos OLE y ActiveX/COM.
- Formulario generado por diseñador y persistencia de propiedades.
- Pruebas automatizadas y paquete de distribución.
