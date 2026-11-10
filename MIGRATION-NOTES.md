# Migration notes

## Decisiones

- Se mantiene WinForms y `System.Drawing`; es la correspondencia natural con el control visual y evita introducir dependencias.
- Los ocho estilos se representan en un renderer administrado común con tres anillos y geometría `GraphicsPath`; esto conserva estados y regiones, pero no promete los colores GDI exactos.
- `Value` conserva semántica de CheckBox/OptionButton y `Text` se sincroniza con `Caption`.
- Los cursores VB6 se traducen a `Cursor`; `MouseIcon` y `DragIcon` crean/destruyen recursos nativos con ciclo de vida explícito.
- OLE se expone mediante eventos WinForms adaptados. No se intenta fingir compatibilidad binaria con `DataObject` VB6.

## Cambios incompatibles

- No se conserva CLSID/OCX ni carga de formularios VB6.
- `StdPicture`, `StdFont`, `OLE_COLOR`, `PropertyBag` y persistencia binaria no son tipos públicos del port.
- `hDC` y `hwnd` siguen el ciclo de vida de `Control` WinForms.
- `Refresh` y `ResetDefaultColors` tienen nombres .NET (`RedrawButton`, `ResetDefaultColorsNow`).
- `Default`/`Cancel` requieren configuración explícita del formulario si se necesita `AcceptButton`/`CancelButton`.

## Próximos pasos para mayor fidelidad

1. Reparar/recrear la solución y agregar un gate CI que ejecute el harness.
2. Capturar el OCX VB6 en las ocho paletas y estados a DPI 96/120/144/192.
3. Implementar comparación de imágenes y decidir si se necesita una capa GDI específica por estilo.
4. Añadir adaptador COM solo si consumidores existentes requieren registro ActiveX o persistencia OLE.
