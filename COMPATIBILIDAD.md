# Matriz de compatibilidad VB6 / .NET 8

Porcentaje significa cobertura funcional estimada, no equivalencia binaria.

| Elemento VB6 | Equivalente .NET | Estado | % | Diferencias |
|---|---|---:|---:|---|
| `ButtonStyle` | `BackStyle` | Implementado | 85 | Estilos requieren comparación píxel a píxel |
| `Mode` | `ButtonMode` | Implementado | 90 | Agrupación OptionButton depende del contenedor WinForms |
| `Caption` | `Caption` | Implementado | 95 | `||` se convierte a salto de línea |
| `CaptionAlign` | `ContentAlignment` | Implementado | 90 | Flags de DrawText no son idénticos |
| `CaptionStyle` | `CaptionStyle` | Implementado | 85 | Sombra administrada aproximada |
| `CustomClick` | `CustomClick` | Parcial | 50 | El desplazamiento se conserva, falta validar todos los estilos |
| `ButtonShape` | `ButtonShape` | Implementado | 75 | Falta comparación de regiones irregulares |
| `Picture` | `Image` | Implementado | 80 | No es `StdPicture`/COM |
| `PictureAlign` | `ImagePlacement` | Implementado | 85 | Cálculo distinto en casos extremos |
| `PictureSize` | `ImageSize` | Implementado | 80 | Falta comparar interpolación original |
| `Enabled` | `Control.Enabled` | Implementado | 95 | Colores deshabilitados administrados |
| `ShowFocusRect` | `ShowFocusRect` | Implementado | 85 | Reglas VB6 de botón Default no idénticas |
| `Value` | `Value`/`Checked`/`Selected` | Implementado | 90 | Eventos adicionales .NET |
| `MousePointer` | `Cursor` | Parcial | 70 | Enumeraciones VB6 no mapeadas una a una |
| `MouseIcon` | `Image` → `Cursor` | Parcial | 55 | Hotspot/transparencia requieren validación |
| `Font` | `System.Drawing.Font` | Implementado | 85 | Falta persistencia equivalente StdFont |
| `FontStyle` | `FontStyles` | Implementado | 85 | Conversión a Font administrada |
| `ForeColor` | `Control.ForeColor` | Implementado | 90 | OLE_COLOR no es el mismo tipo |
| `BackColor` | `BackColor` | Implementado | 90 | OLE_COLOR no es el mismo tipo |
| `GradientColor` | `Color` | Implementado | 85 | Fórmulas XP no demostradas equivalentes |
| `GradientMode` | `GradientMode` | Implementado | 85 | Usa LinearGradientBrush |
| `ResetDefaultColors` | `ResetDefaultColors` | Implementado | 85 | Defaults exactos deben validarse |
| `HoverColorLocks` | `HoverColorLocks` | Implementado | 80 | Falta validar cada combinación |
| `HoverForeColor` | `Color` | Implementado | 90 | Semántica de locks aproximada |
| `HoverBackColor` | `Color` | Implementado | 90 | Semántica de locks aproximada |
| `hDC` | `CreateGraphics().GetHdc()` | Adaptado | 50 | Ciclo de vida HDC diferente; no conservar |
| `hwnd` | `Handle` | Adaptado | 90 | Solo válido con handle creado |
| `MouseDown/Up/Move` | Eventos WinForms | Implementado | 85 | Coordenadas/tipos diferentes |
| `KeyDown/Press/Up` | Eventos WinForms | Implementado | 80 | KeyAscii no es idéntico |
| `MouseOnButton` | `MouseOnButton` + timer 50 ms | Parcial | 75 | No usa región real en todos los estilos |
| `Click` | `Click` | Implementado | 95 | Orden de eventos puede diferir |
| `DoubleClick` | `ButtonDoubleClick` | Implementado | 80 | Firma adaptada |
| OLE events | DragDrop/GiveFeedback | Parcial | 45 | `DataObject` y argumentos no son equivalentes |
| `DefaultCancel` | `Default`/`Cancel` | Adaptado | 75 | Enter/Escape solo en CommandButton; navegación usa contenedor WinForms |
| `ToolTipText` | `ToolTip` | Implementado | 85 | Componente externo WinForms |
| `Index` | `Index` | Compatibilidad | 60 | No crea control arrays VB6 |
| `HelpContextID` | `int` | Almacenado | 30 | Falta integración HelpProvider |
| `WhatsThisHelpID` | `int` | Almacenado | 30 | Falta integración de ayuda |
| `DragMode` | `DragMode` + `DoDragDrop` | Parcial | 70 | Secuencia start/complete adaptada; efectos y datos OLE distintos |
| `Tag` | `Control.Tag` | Implementado | 100 | Tipo más amplio en .NET |
| `TabIndex/TabStop` | WinForms | Implementado | 100 | Semántica nativa WinForms |
| `Left/Top/Width/Height` | WinForms | Implementado | 100 | Unidades y DPI pueden diferir |
| `Visible` | WinForms | Implementado | 100 | Semántica nativa WinForms |

## Resumen

- API superficial: **aprox. 85 %**.
- Pintado y estados: **aprox. 70 %**, pendiente comparación visual.
- Eventos: **aprox. 75 %**.
- OLE/ActiveX: **aprox. 45 %**.
- Diseñador/persistencia: **aprox. 65 %** (metadatos Toolbox/Designer añadidos; falta validación real en Visual Studio).
- Formulario de ejemplo: **aprox. 70 %**.
- Port global estimado: **aprox. 75 %**.

No se declara compatibilidad binaria COM/ActiveX.

## Incidencia de compilación de solución

La orden solicitada `dotnet build .\\LaVolpe.Buttons.Net.sln --configuration Release` no completa el restore en el SDK instalado `10.0.403` porque faltan los SDK resolvers `Microsoft.NET.SDK.WorkloadAutoImportPropsLocator` y `Microsoft.NET.SDK.WorkloadManifestTargetsLocator`. La compilación directa de `LaVolpe.Buttons.Sample.csproj` y su referencia `LaVolpe.Buttons.Net.csproj` sí termina con 0 errores y 0 advertencias.
