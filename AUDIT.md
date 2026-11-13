# Auditoría end-to-end: lvButtons_H → C# .NET 8

Actualización 2026-11-13: la suite actual ejecuta 56 pruebas correctas y 0 fallidas; el clic físico Win32 no duplica `Click` ni `Value`, el ciclo de teclado notifica `LvKeyDown`/`LvKeyUp`, el temporizador de hover se activa y detiene con el puntero, `ResetDefaultColors` restaura la paleta y el gradiente, `Refresh()` redibuja el control, `Default`/`Cancel` limpian y rehacen sus enlaces al cambiar de formulario, `CustomClick` desplaza/restaura el control custom, Escape y las flechas navegan como VB6, el layout de captions cubre textos largos y saltos `||`, `Custom3DBorder` dibuja el anillo blanco→gris, los eventos OLE públicos se notifican, `HoverColorLocks` sincroniza colores y `Custom*` redimensiona proporcionalmente.

Fecha de ejecución: 2026-11-11. Alcance: `LaVolpe-Buttons-Updated-/lvButtons.ctl`, `modLvTimer.bas`, `frmSample.frm`, fuentes/configuración C# y pruebas del repositorio.

## Resultado ejecutivo

El port WinForms compila por proyecto y el harness ejecuta 56 pruebas correctas, pero no es equivalente binario ni visualmente 100% compatible con el OCX ActiveX. La matriz de [COMPATIBILITY-MATRIX.md](COMPATIBILITY-MATRIX.md) contiene 46 filas: 16 equivalentes, 22 parciales, 6 diferentes, 1 ausente y 1 no verificable.

- Cobertura revisada: **45/46 = 97,8%** (todo lo que no quedó como no verificable).
- Equivalencia estricta: **13/46 = 28,3%**.
- Superficie cubierta parcialmente o con adaptación explícita: **25/46 = 54,3%**.
- No debe interpretarse como equivalencia visual o COM.

## Inventario

### VB6

`lvButtons.ctl` define 10 enums públicos (líneas 272-350), 15 eventos públicos (54-69), 29 propiedades públicas (353-931), `Refresh` y `DelayDrawing` (932-948), rutinas de layout/región/pintado (1020-2373) y handlers de teclado, ratón, foco, OLE, resize y persistencia (2375-2781). Los ocho estilos de pintura están separados en `DrawButton_Win95`, `DrawButton_Win31`, `DrawButton_WinXP`, `DrawButton_Macintosh`, `DrawButton_Flat`, `DrawButton_Hover`, `DrawButton_Netscape` y `DrawButton_Java` (2803-3386).

`modLvTimer.bas:1-40` expone el callback global `lv_TimerCallBack`, basado en mensajes Win32 y propiedades de ventana. `frmSample.frm:810-996` ejercita arrays de controles, estilos, modos, imágenes, hover, gradientes, tamaño y alineación.

### C#

`LaVolpeButton.cs:8-24` define los enums y la clase pública. La clase implementa propiedades/eventos de apariencia, imagen, selección, foco, cursor, drag/drop, accesibilidad e `IButtonControl` (`:52-115`), ciclo de vida y región (`:118-362`), interacción (`:363-394`) y pintura/layout (`:396-916`). `LaVolpe.Buttons.Tests/Program.cs` conserva el harness STA ejecutable y `LaVolpe.Buttons.XunitTests` lo expone como 56 casos xUnit v3/Microsoft Testing Platform.

## Arquitectura y riesgos

La arquitectura actual es adecuada para el port: una biblioteca WinForms, un sample y un ejecutable de pruebas, sin capas nuevas necesarias. No se detectó una violación arquitectónica que justificara refactorizar. Los límites importantes son externos al control: ActiveX/CLSID, persistencia `PropertyBag`, `StdPicture`/`DataObject` VB6, callback Win32 global, diseñador VB6 y comparación píxel a píxel.

## Evidencia de verificación

- `dotnet restore .\\LaVolpe.Buttons.Net.sln`: correcto.
- `dotnet build .\\LaVolpe.Buttons.Net.sln -c Release --no-restore --maxcpucount:1`: los cinco proyectos (biblioteca, sample, DesignerSample, harness y runner xUnit) compilan con 0 errores y 0 warnings.
- `dotnet run --project .\\LaVolpe.Buttons.Tests\\LaVolpe.Buttons.Tests.csproj -c Release --no-build`: 56 correctas, 0 fallidas, incluida la comprobación de clic físico sin duplicación, el ciclo de teclado `Space`/`Enter`, Escape y las flechas, el ciclo de hover, `ResetDefaultColors`, `Refresh`, el rebinding de `AcceptButton`/`CancelButton` al cambiar de formulario, el desplazamiento/restauración de `CustomClick`, el layout de captions con textos largos y saltos `||`, el anillo degradado de `Custom3DBorder`, la notificación de `OLECompleteDrag`/`OLESetData`, la aceptación de `Copy` en `DragEnter` sin manejador, el respeto del rechazo explícito del manejador, la entrega de texto en `DragDrop`, el sombreado XP por franjas, `GradientMode`, la protección de canales fuera de rango, la alineación vertical de `PictureAlign`, el redimensionado proporcional de `Custom*`, la conversión de `ShadeColor`, sus límites, `HoverColorLocks` y la validación de enums.
- `dotnet test .\\LaVolpe.Buttons.XunitTests\\LaVolpe.Buttons.XunitTests.csproj -c Release --no-build` ejecuta 56/56 casos con xUnit v3/Microsoft Testing Platform; el harness `dotnet run` se conserva para la comprobación STA y las herramientas visuales.
- `dotnet build .\\LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1`: correcto; 0 warnings y 0 errores. La concurrencia 1 se mantiene como gate reproducible en este entorno.

## Conclusión

La implementación actual es un port funcional de WinForms con cobertura demostrada en estados, regiones, cursores, selección, estilos y errores básicos. Las diferencias restantes están registradas en [GAP-REPORT.md](GAP-REPORT.md) y las decisiones de compatibilidad en [MIGRATION-NOTES.md](MIGRATION-NOTES.md). No se declara equivalencia total.
