# Auditoría end-to-end: lvButtons_H → C# .NET 8

Fecha de ejecución: 2026-11-11. Alcance: `LaVolpe-Buttons-Updated-/lvButtons.ctl`, `modLvTimer.bas`, `frmSample.frm`, fuentes/configuración C# y pruebas del repositorio.

## Resultado ejecutivo

El port WinForms compila por proyecto y el harness ejecuta 29 pruebas correctas, pero no es equivalente binario ni visualmente 100% compatible con el OCX ActiveX. La matriz de [COMPATIBILITY-MATRIX.md](COMPATIBILITY-MATRIX.md) contiene 45 filas: 12 equivalentes, 25 parciales, 6 diferentes, 1 ausente y 1 no verificable.

- Cobertura revisada: **44/45 = 97,8%** (todo lo que no quedó como no verificable).
- Equivalencia estricta: **12/45 = 26,7%**.
- Superficie cubierta parcialmente o con adaptación explícita: **25/45 = 55,6%**.
- No debe interpretarse como equivalencia visual o COM.

## Inventario

### VB6

`lvButtons.ctl` define 10 enums públicos (líneas 272-350), 15 eventos públicos (54-69), 29 propiedades públicas (353-931), `Refresh` y `DelayDrawing` (932-948), rutinas de layout/región/pintado (1020-2373) y handlers de teclado, ratón, foco, OLE, resize y persistencia (2375-2781). Los ocho estilos de pintura están separados en `DrawButton_Win95`, `DrawButton_Win31`, `DrawButton_WinXP`, `DrawButton_Macintosh`, `DrawButton_Flat`, `DrawButton_Hover`, `DrawButton_Netscape` y `DrawButton_Java` (2803-3386).

`modLvTimer.bas:1-40` expone el callback global `lv_TimerCallBack`, basado en mensajes Win32 y propiedades de ventana. `frmSample.frm:810-996` ejercita arrays de controles, estilos, modos, imágenes, hover, gradientes, tamaño y alineación.

### C#

`LaVolpeButton.cs:8-24` define los enums y la clase pública. La clase implementa propiedades/eventos de apariencia, imagen, selección, foco, cursor, drag/drop, accesibilidad e `IButtonControl` (`:52-115`), ciclo de vida y región (`:118-362`), interacción (`:363-394`) y pintura/layout (`:396-916`). `Program.cs` del proyecto de pruebas es un harness STA autónomo; no usa un runner xUnit/NUnit y se ejecuta como EXE (`LaVolpe.Buttons.Tests/Program.cs:1-443`).

## Arquitectura y riesgos

La arquitectura actual es adecuada para el port: una biblioteca WinForms, un sample y un ejecutable de pruebas, sin capas nuevas necesarias. No se detectó una violación arquitectónica que justificara refactorizar. Los límites importantes son externos al control: ActiveX/CLSID, persistencia `PropertyBag`, `StdPicture`/`DataObject` VB6, callback Win32 global, diseñador VB6 y comparación píxel a píxel.

## Evidencia de verificación

- `dotnet restore .\\LaVolpe.Buttons.Net.sln`: correcto.
- `dotnet build` individual de los tres `.csproj`, Release: 0 errores, 0 warnings en cada uno.
- `dotnet run --project .\\LaVolpe.Buttons.Tests\\LaVolpe.Buttons.Tests.csproj -c Release --no-build`: 29 correctas, 0 fallidas.
- `dotnet test` no descubre casos porque el proyecto es un EXE de harness, no un proyecto de test SDK.
- `dotnet build .\\LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1`: correcto; 0 warnings y 0 errores. La concurrencia 1 se mantiene como gate reproducible en este entorno.

## Conclusión

La implementación actual es un port funcional de WinForms con cobertura demostrada en estados, regiones, cursores, selección, estilos y errores básicos. Las diferencias restantes están registradas en [GAP-REPORT.md](GAP-REPORT.md) y las decisiones de compatibilidad en [MIGRATION-NOTES.md](MIGRATION-NOTES.md). No se declara equivalencia total.
