# API mapping

| VB6 | .NET 8 | Compatibilidad |
|---|---|---|
| `ButtonStyle` | `BackStyle` / `ButtonStyle` | Alias conceptual; enum público distinto |
| `Mode` | `ButtonMode` | Directa |
| `Caption` | `Caption` y `Text` | `Text` sincroniza con `Caption` |
| `CaptionAlign` | `ContentAlignment` | Adaptación WinForms |
| `Picture` | `Image` | Tipo administrado, no COM |
| `PictureAlign` | `ImagePlacement` | Directa con nombres .NET |
| `PictureSize` | `ImageSize` | Directa |
| `Value` | `Value` / `Checked` / `Selected` | Semántica según `ButtonMode` |
| `MousePointer` | `MousePointerConstants` → `Cursor` | Mapeo de cursores |
| `MouseIcon` | `Image` → `Cursor` | Hotspot centrado |
| `hDC` | HDC cacheado | Adaptada, no ciclo VB6 |
| `hwnd` | `Handle` | Adaptada |
| `Refresh` | `Control.Refresh()` / `RedrawButton()` | Compatible funcionalmente; el segundo es el helper explícito del port |
| `DelayDrawing` | `DelayDrawing(bool)` / Begin/End | Compatible funcionalmente |
| `MouseOnButton` | `EventHandler<bool>` | Firma .NET |
| `OLE*` | DragDrop/GiveFeedback events | Firma y `DataObject` adaptados |
| `DefaultCancel` | `Default`, `Cancel`, `IButtonControl` | Enlaza automáticamente `Form.AcceptButton`/`CancelButton` |
| `Font` | `System.Drawing.Font` | Tipo diferente |
| `OLE_COLOR` | `System.Drawing.Color` | Conversión conceptual |
| `PropertyBag` | Propiedades WinForms | No hay persistencia ActiveX |

La lista completa de métodos internos y rutinas de pintura está en la matriz; las APIs públicas del port están declaradas en `LaVolpe.Buttons.Net/LaVolpeButton.cs:52-115`.
