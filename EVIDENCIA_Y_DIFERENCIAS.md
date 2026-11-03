# Evidencia y diferencias conocidas

## Evidencia observada

- `lvButtons.ctl` contiene la API pública, ocho familias de estilo, regiones geométricas, imágenes, estados, eventos de teclado/mouse y eventos OLE.
- `modLvTimer.bas` implementa callback Win32 con `CopyMemory` y propiedades de ventana.
- `frmSample.frm` usa un control array de tres botones, tres OptionButton, imágenes FRX y cambios dinámicos de modo/estilo/alineación.
- El port C# compila para `net8.0-windows` sin errores ni advertencias.
- El ejemplo publica tres PNG y los carga como copias independientes.
- `PerformClick()` no dispara `Click` cuando `Enabled == false`.
- `OptionButton` deselecciona hermanos del mismo contenedor.
- El timer WinForms usa 50 ms y consulta `GraphicsPath.IsVisible`.
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
| Regiones custom | Posibles máscaras/bitmaps | Formas geométricas administradas | `CustomFlat`/`Custom3DBorder` no tienen máscara bitmap completa |
| Fuentes | `StdFont`/OLE | `System.Drawing.Font` | Conversión de métricas y persistencia distinta |

## Evidencia no disponible todavía

- Capturas comparativas ejecutando simultáneamente VB6 y .NET.
- Medición píxel a píxel de cada estilo y estado.
- Ejecución del Designer real de Visual Studio.
- Prueba de instalación en otro equipo con Desktop Runtime.
- Verificación de hotspot/transparencia de `MouseIcon`.
- Prueba real de DragDrop externo y de todos los efectos OLE.
- Equivalencia completa del callback `modLvTimer.bas`.

Estas ausencias se conservan como limitaciones; no se convierten en afirmaciones de paridad.
