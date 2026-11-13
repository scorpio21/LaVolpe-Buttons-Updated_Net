# Migration notes

## Decisiones

- Se mantiene WinForms y `System.Drawing`; es la correspondencia natural con el control visual y evita introducir dependencias.
- Los ocho estilos se representan en un renderer administrado común con tres anillos y geometría `GraphicsPath`; esto conserva estados y regiones, pero no promete los colores GDI exactos.
- `Value` conserva semántica de CheckBox/OptionButton y `Text` se sincroniza con `Caption`.
- Los cursores VB6 se traducen a `Cursor`; `MouseIcon` y `DragIcon` crean/destruyen recursos nativos con ciclo de vida explícito.
- OLE se expone mediante eventos WinForms adaptados. No se intenta fingir compatibilidad binaria con `DataObject` VB6.

## Alcance COM/ActiveX

- Decisión: el producto objetivo es una biblioteca WinForms administrada para .NET 8; no se añade una capa COM/ActiveX.
- Consumidores dentro del repositorio: `LaVolpe.Buttons.Sample` y `LaVolpe.Buttons.DesignerSample` consumen el proyecto .NET mediante referencia de proyecto. El proyecto VB6 (`LaVolpe-Buttons-Updated-`) es la referencia de comportamiento, no un consumidor binario del ensamblado .NET.
- Consecuencia: no se preservan CLSID, registro OCX, `StdPicture`, `PropertyBag` ni persistencia binaria. Si aparece un consumidor VB6 que necesite insertar el control .NET como ActiveX, será un proyecto separado y requerirá un adaptador COM explícito.
- Evidencia: la búsqueda del repositorio no encuentra registros COM/ActiveX ni consumidores de `PropertyBag`; la matriz mantiene esas filas como `ausente`/`no compatible`, no como equivalentes.

## Cambios incompatibles

- No se conserva CLSID/OCX ni carga de formularios VB6.
- `StdPicture`, `StdFont`, `OLE_COLOR`, `PropertyBag` y persistencia binaria no son tipos públicos del port.
- `hDC` y `hwnd` siguen el ciclo de vida de `Control` WinForms.
- `Refresh` conserva el método público heredado de WinForms y `RedrawButton` ofrece el repintado explícito del port; `ResetDefaultColors` conserva la propiedad pública VB6 y `ResetDefaultColorsNow` es solo su helper interno.
- `Default`/`Cancel` enlazan automáticamente `Form.AcceptButton`/`Form.CancelButton`; el formulario no necesita cableado adicional para esa función.
- La fuente se hereda del contenedor WinForms, como es habitual en controles administrados; el sample fija explícitamente las fuentes observadas en `frmSample.frm` (Tahoma, Arial, MS Sans Serif y Times New Roman). `TextRenderer` con `NoPadding` aproxima `DrawText` de VB6, pero las métricas y la rasterización no son idénticas y la comparación visual debe tratarse como una diferencia conocida.

## Próximos pasos para mayor fidelidad

1. Reparar/recrear la solución y agregar un gate CI que ejecute el harness.
2. Capturar el OCX VB6 en las ocho paletas y estados a DPI 96/120/144/192.
3. Implementar comparación de imágenes y decidir si se necesita una capa GDI específica por estilo.
4. Añadir adaptador COM solo si consumidores existentes requieren registro ActiveX o persistencia OLE.
