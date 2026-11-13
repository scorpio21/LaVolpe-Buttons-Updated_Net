# Evidencia y diferencias conocidas

Actualización 2026-11-13: la suite actual ejecuta 56 pruebas correctas y 0 fallidas; incluye comprobaciones de clic físico Win32, teclado `Space`/`Enter`, Escape, flechas, ciclo de hover, `ResetDefaultColors`, `Refresh`, desplazamiento/restauración de `CustomClick`, layout de captions con textos largos y saltos `||`, anillo degradado de `Custom3DBorder`, notificación de eventos OLE públicos, `HoverColorLocks`, límites de `ShadeColor` y redimensionado proporcional de `Custom*`.

## Evidencia observada

- `lvButtons.ctl` contiene la API pública, ocho familias de estilo, regiones geométricas, imágenes, estados, eventos de teclado/mouse y eventos OLE.
- `modLvTimer.bas` implementa callback Win32 con `CopyMemory` y propiedades de ventana.
- `frmSample.frm` usa un control array de tres botones, tres OptionButton, imágenes FRX y cambios dinámicos de modo/estilo/alineación.
- El port C# compila para `net8.0-windows` sin errores ni advertencias.
- El ejemplo publica tres PNG y los carga como copias independientes.
- `PerformClick()` no dispara `Click` cuando `Enabled == false`.
- `OptionButton` deselecciona hermanos, se selecciona al recibir foco y suprime el clic repetido.
- `Text` y `Caption` están sincronizados; `Value` rechaza `true` en `CommandButton`.
- `IButtonControl` (`DialogResult`, `NotifyDefault`) y `DelayDrawing(bool)` están implementados.
- `CustomFlat`/`Custom3DBorder` derivan la `Region` de la máscara de la imagen y aplican las reglas de caption/imagen de VB6.
- CheckBox/OptionButton marcan toda la superficie con `cCheckBox`; se eliminó el glifo de 14 px del port.
- `MousePointerConstants` mapea los 15 valores VB6 a `Cursor` y `MouseIcon` implica `Custom`.
- `DragIcon` crea un cursor/HICON propio y lo libera al asignar `null`.
- `LaVolpe.Buttons.Tests` ejecuta 56 casos sobre formularios STA: 56 correctas, 0 fallidas.
- El timer WinForms usa 50 ms por control, activo solo durante el hover, y consulta `GraphicsPath.IsVisible`.
- La publicación Release contiene DLL, EXE, runtimeconfig, dependencias, símbolos y recursos.

## Diferencias confirmadas

| Área | VB6 | .NET 8 | Impacto |
|---|---|---|---|
| Identidad | OCX/COM/ActiveX | DLL administrada WinForms | No hay compatibilidad binaria ni registro CLSID |
| Pintado | GDI32, HDC, regiones Win32 | GDI+/GraphicsPath | Bordes, antialiasing y métricas pueden diferir |
| Timer | Callback global Win32 | Timer WinForms por instancia | Orden y ciclo de vida distintos |
| OLE | Eventos `OLE*` VB6 y `DataObject` | DragDrop WinForms | Firmas y efectos no son equivalentes |
| Control arrays | `Index` y array VB6 | Controles independientes | No se recrea la semántica del array |
| Persistencia | PropertyBag/OCX | Serialización WinForms | No hay carga directa de propiedades VB6 |
| Ayuda | IDs integrados al host VB6 | IDs almacenados | Falta HelpProvider/WhatsThis completo |
| Regiones custom | Máscara/NormalizeImage y redimensionado del control | Región desde `LockBits` y redimensionado proporcional según `ScaleImage` | La forma y el tamaño se aproximan; la normalización GDI exacta aún difiere |
| Fuentes | `StdFont`/OLE | `System.Drawing.Font` | Conversión de métricas y persistencia distinta |

## Evidencia no disponible todavía

- Capturas comparativas ejecutando simultáneamente VB6 y .NET.
- Medición píxel a píxel de cada estilo y estado.
- Reapertura del diseñador y serialización de imágenes/recursos tras editar desde Visual Studio.
- Prueba de instalación en otro equipo con Desktop Runtime.
- Verificación visual de transparencia y escala de `MouseIcon` (el hotspot centrado y la liberación del `HICON` ya se comprueban).
- Prueba real de DragDrop externo y de todos los efectos OLE.
- Equivalencia completa del callback `modLvTimer.bas`.

Estas ausencias se conservan como limitaciones; no se convierten en afirmaciones de paridad.
