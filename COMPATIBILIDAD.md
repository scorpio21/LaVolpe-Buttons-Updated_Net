# Matriz de compatibilidad VB6 / .NET 8

Porcentaje significa cobertura funcional estimada, no equivalencia binaria.

| Elemento VB6 | Equivalente .NET | Estado | % | Diferencias |
|---|---|---:|---:|---|
| `ButtonStyle` | `BackStyle` | Implementado | 80 | Tres anillos y colores por estado; los estilos rectangulares/diagonales conservan colores independientes por lado, pero el rasterizado GDI+ no es idéntico |
| `Mode` | `ButtonMode` | Implementado | 90 | Agrupación OptionButton depende del contenedor WinForms |
| `Caption` | `Caption` | Implementado | 95 | `||` se convierte a salto de línea |
| `CaptionAlign` | `ContentAlignment` | Implementado | 90 | Flags de DrawText no son idénticos |
| `CaptionStyle` | `CaptionStyle` | Implementado | 85 | Sombra administrada aproximada |
| `CustomClick` | `CustomClick` | Parcial | 80 | Los botones custom de comando desplazan temporalmente el control y restauran su posición; faltan validaciones visuales de todos los vectores |
| `ButtonShape` | `ButtonShape` | Implementado | 85 | Regiones, `LopOffCorners` y máscaras; `Custom*` redimensiona proporcionalmente como `ScaleImage` |
| `Picture` | `Image` | Implementado | 80 | No es `StdPicture`/COM |
| `PictureAlign` | `ImagePlacement` | Implementado | 85 | Cálculo distinto en casos extremos |
| `PictureSize` | `ImageSize` | Implementado | 80 | Falta comparar interpolación original |
| `Enabled` | `Control.Enabled` | Implementado | 95 | Colores deshabilitados administrados |
| `ShowFocusRect` | `ShowFocusRect` | Implementado | 85 | Reglas VB6 de botón Default no idénticas |
| `Value` | `Value`/`Checked`/`Selected` | Implementado | 95 | Semántica VB6; estado marcado en toda la superficie |
| `MousePointer` | `MousePointerConstants` → `Cursor` | Implementado | 100 | Mapa de los 15 valores VB6 y alias `vb*` |
| `MouseIcon` | `Image` → `Cursor` | Implementado | 85 | Hotspot centrado, `DestroyIcon` e integración con `MousePointer = Custom` |
| `Font` | `System.Drawing.Font` | Implementado | 95 | Hereda la fuente del contenedor como el ambiente VB6 |
| `FontStyle` | `FontStyles` | Implementado | 95 | Alias `lv_Bold`…`lv_BoldItalicUnderline` |
| `ForeColor` | `Control.ForeColor` | Implementado | 90 | OLE_COLOR no es el mismo tipo |
| `BackColor` | `BackColor` | Implementado | 90 | OLE_COLOR no es el mismo tipo |
| `GradientColor` | `Color` | Implementado | 85 | Fórmulas XP no demostradas equivalentes |
| `GradientMode` | `GradientMode` | Implementado | 85 | Franjas de 1 píxel con extremos inclusivos y canales limitados a 0–255; la paridad global de rasterizado sigue pendiente |
| `ResetDefaultColors` | `ResetDefaultColors` | Implementado | 80 | Restaura defaults y vuelve a heredar del contenedor |
| `HoverColorLocks` | `HoverColorLocks` | Implementado | 90 | Default y combinaciones de VB6 implementados |
| `HoverForeColor` | `Color` | Implementado | 90 | Semántica de locks aproximada |
| `HoverBackColor` | `Color` | Implementado | 90 | Semántica de locks aproximada |
| `hDC` | HDC cacheado del control | Adaptado | 90 | Se libera en `OnHandleDestroyed`/`Dispose`; sigue siendo un HDC administrado |
| `hwnd` | `Handle` | Adaptado | 90 | Solo válido con handle creado |
| `MouseDown/Up/Move` | Eventos WinForms | Implementado | 85 | Coordenadas/tipos diferentes |
| `KeyDown/Press/Up` | Eventos WinForms | Implementado | 80 | KeyAscii no es idéntico |
| `MouseOnButton` | `MouseOnButton` + timer 50 ms | Parcial | 80 | Timer administrado por control; hover basado en `GraphicsPath`, no en la región de máscara |
| `Click` | `Click` | Implementado | 95 | Orden de eventos puede diferir |
| `DoubleClick` | `ButtonDoubleClick` | Implementado | 85 | Firma adaptada |
| OLE events | DragDrop/GiveFeedback | Parcial | 45 | `DataObject` y argumentos no son equivalentes |
| `DefaultCancel` | `Default`/`Cancel` + `IButtonControl` | Equivalente funcional | 95 | `DialogResult`/`NotifyDefault` y enlace automático a `AcceptButton`/`CancelButton` |
| `ToolTipText` | `ToolTip` | Implementado | 85 | Componente externo WinForms |
| `Index` | `Index` | Compatibilidad | 60 | No crea control arrays VB6 |
| `HelpContextID` | `int` | Almacenado | 30 | Falta integración HelpProvider |
| `WhatsThisHelpID` | `int` | Almacenado | 30 | Falta integración de ayuda |
| `DragMode` | `DragMode` + `DoDragDrop` | Parcial | 70 | Secuencia start/complete adaptada; efectos y datos OLE distintos |
| `DragIcon` | `Image` → `Cursor` | Implementado | 85 | `CreateIconIndirect` y `UseDefaultCursors = false` durante el arrastre |
| `Tag` | `Control.Tag` | Implementado | 100 | Tipo más amplio en .NET |
| `TabIndex/TabStop` | WinForms | Implementado | 100 | Semántica nativa WinForms |
| `Left/Top/Width/Height` | WinForms | Implementado | 100 | Unidades y DPI pueden diferir |
| `Visible` | WinForms | Implementado | 100 | Semántica nativa WinForms |

## Resumen

- API superficial: **aprox. 90 %** (alias `lv_*`, `Text`↔`Caption`, `IButtonControl`, `DelayDrawing(bool)`).
- Pintado y estados: **aprox. 72 %**, tres anillos por estilo y degradado XP; pendiente comparación visual y colores por lado.
- Eventos: **aprox. 80 %**.
- OLE/ActiveX: **aprox. 45 %**.
- Diseñador/persistencia: **aprox. 65 %** (metadatos Toolbox/Designer añadidos; falta validación real en Visual Studio).
- Formulario de ejemplo: **aprox. 60 %**.
- Port global histórico estimado: **aprox. 76 %**; la revisión actual lo sitúa en el rango 65–75 % (ver `ESTADO-PORT-NET.md`).

No se declara compatibilidad binaria COM/ActiveX.

## Compilación de la solución

`dotnet build .\LaVolpe.Buttons.Net.sln --configuration Release` termina con **0 errores y 0
advertencias** con el `global.json` del repositorio (SDK 8.0.427). La afirmación anterior sobre los
resolvers de workload del SDK 10.0.403 era un problema del entorno, no del código.
