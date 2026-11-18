# OLE/Drag-and-drop VB6 → WinForms

Estado 2026-11-18: los eventos OLE permanecen declarados y cubiertos por pruebas en `LaVolpeButton`, pero el nuevo `DesignerSample2` es un formulario limpio y no activa drag-and-drop. `LaVolpeButton` deja `AllowDrop=false` por defecto, equivalente al `OLEDropMode=Manual` del OCX.

| VB6 | WinForms .NET 8 | Efecto portado | Estado |
|---|---|---|---|
| `OLEStartDrag(Data, AllowedEffects)` | `OLEStartDrag(DragEventArgs)` antes de `DoDragDrop` | Inicia con `DataObject(Caption)` y respeta el `Effect` que establezca el manejador; `None` cancela el origen | parcial |
| `OLECompleteDrag(Effect)` | `OLECompleteDrag(EventArgs)` en `finally` | Se dispara también si `DoDragDrop` falla y restaura el cursor; el efecto final no se expone en la firma actual | parcial |
| `OLEDragDrop(Data, Effect, Button, Shift, X, Y)` | `OLEDragDrop(DragEventArgs)` | Conserva datos, efecto y coordenadas WinForms | parcial por firma |
| `OLEDragOver(Data, Effect, Button, Shift, X, Y, State)` | `OLEDragOver(DragEventArgs)` | Conserva efecto y datos del evento WinForms | parcial por firma |
| `OLEGiveFeedback(Effect, DefaultCursors)` | `OLEGiveFeedback(GiveFeedbackEventArgs)` | Usa `DragIcon` y desactiva cursores predeterminados | equivalente funcional |
| `OLESetData(Data, DataFormat)` | `OLESetData(EventArgs)` | Evento expuesto para completar datos | parcial por firma |

`lvButtons.ctl` no declara una propiedad pública `DragMode`; el `DragMode` del port es una adaptación de WinForms para poder ejercitar el flujo de origen (`DoDragDrop`) en `DesignerSample`. No se configura en el sample principal, cuyo comportamiento sigue siendo manual (`OLEDropMode=Manual`).

## Evidencia

- El flujo de arrastre se encuentra en `LaVolpe.Buttons.Net/LaVolpeButton.cs:420-451`; `OLEStartDrag` puede cambiar `DragEventArgs.Effect` y ese valor se usa en `DoDragDrop` (o cancela con `None`). `OnDragEnter` publica `OLEDragOver` en `:502-508` y aplica `Copy` por defecto solo sin manejador, para que el receptor pueda aceptar o rechazar el texto. El mismo `DataObject` publica `UnicodeText` y `Text` para `OLEStartDrag` y `DoDragDrop`.
- `DragIcon` se convierte en un `Cursor` nativo y se libera al cambiar la imagen o destruir el control (`LaVolpeButton.cs:306-333`, `:399`).
- `OLECompleteDrag` está protegido por `finally`, por lo que el cursor vuelve a `MousePointer` aunque `DoDragDrop` lance una excepción.
- El harness verifica creación/liberación de `DragIcon` y los formatos del `DataObject` en `LaVolpe.Buttons.Tests/Program.cs:318-347`.
- El harness verifica también la aceptación de `Copy` durante `DragEnter` sin manejador, el respeto del rechazo explícito y que `DragDrop` entrega el texto externo al evento en `LaVolpe.Buttons.Tests/Program.cs:407-457`.
- `LaVolpe.Buttons.DesignerSample2/Form1.Designer.cs` contiene únicamente un `LaVolpeButton` para validación del diseñador.

## Prueba manual del DesignerSample2

1. Abrir `LaVolpe.Buttons.DesignerSample2` desde Visual Studio.
2. Seleccionar el `LaVolpeButton` en el diseñador.
3. Cambiar una propiedad, por ejemplo `ButtonShape` o `Mode`.
4. Ejecutar con F5 y comprobar que el formulario abre sin activar drag-and-drop.

Esta prueba confirmaría la interoperabilidad con una aplicación externa. El issue #6 permanece abierto y se deja para el final; la compilación y las 56 pruebas automáticas no sustituyen esta comprobación interactiva.

No se declara equivalencia binaria OLE: WinForms usa `DragEventArgs`/`GiveFeedbackEventArgs`, no los tipos COM de VB6. La prueba con datos arrastrados desde otra aplicación debe ejecutarse manualmente en una sesión interactiva de Windows; queda como validación técnica pendiente y el issue #6 se deja para el final.
