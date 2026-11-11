# OLE/Drag-and-drop VB6 → WinForms

| VB6 | WinForms .NET 8 | Efecto portado | Estado |
|---|---|---|---|
| `OLEStartDrag(Data, AllowedEffects)` | `OLEStartDrag(DragEventArgs)` antes de `DoDragDrop` | Inicia con `DataObject(Caption)` y `Copy` | parcial |
| `OLECompleteDrag(Effect)` | `OLECompleteDrag(EventArgs)` en `finally` | Se dispara también si `DoDragDrop` falla y restaura el cursor | equivalente funcional |
| `OLEDragDrop(Data, Effect, Button, Shift, X, Y)` | `OLEDragDrop(DragEventArgs)` | Conserva datos, efecto y coordenadas WinForms | parcial por firma |
| `OLEDragOver(Data, Effect, Button, Shift, X, Y, State)` | `OLEDragOver(DragEventArgs)` | Conserva efecto y datos del evento WinForms | parcial por firma |
| `OLEGiveFeedback(Effect, DefaultCursors)` | `OLEGiveFeedback(GiveFeedbackEventArgs)` | Usa `DragIcon` y desactiva cursores predeterminados | equivalente funcional |
| `OLESetData(Data, DataFormat)` | `OLESetData(EventArgs)` | Evento expuesto para completar datos | parcial por firma |

## Evidencia

- El flujo de arrastre se encuentra en `LaVolpe.Buttons.Net/LaVolpeButton.cs:420-448`; el mismo `DataObject` publica `UnicodeText` y `Text` para `OLEStartDrag` y `DoDragDrop`.
- `DragIcon` se convierte en un `Cursor` nativo y se libera al cambiar la imagen o destruir el control (`LaVolpeButton.cs:306-333`, `:399`).
- `OLECompleteDrag` está protegido por `finally`, por lo que el cursor vuelve a `MousePointer` aunque `DoDragDrop` lance una excepción.
- El harness verifica creación/liberación de `DragIcon` y los formatos del `DataObject` en `LaVolpe.Buttons.Tests/Program.cs:318-347`.

No se declara equivalencia binaria OLE: WinForms usa `DragEventArgs`/`GiveFeedbackEventArgs`, no los tipos COM de VB6. La prueba con datos arrastrados desde otra aplicación debe ejecutarse manualmente en una sesión interactiva de Windows; queda como pendiente del issue #6.
