# Changelog

## Estado actual — 2026-11-03

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
