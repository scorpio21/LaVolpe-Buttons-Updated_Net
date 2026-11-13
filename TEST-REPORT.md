# Test report

Fecha: 2026-11-13. SDK fijado por `global.json` a 8.0.427.

| Comando | Resultado |
|---|---|
| `dotnet restore .\LaVolpe.Buttons.Net.sln` | Correcto |
| `dotnet build .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1` | Correcto; 0 warnings, 0 errors |
| `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build` | 56 correctas, 0 fallidas; incluye clic físico Win32 sin doble `Click`/`Value`, teclado `Space`/`Enter`, Escape y flechas, ciclo de hover, `ResetDefaultColors`, `Refresh`, rebinding de `AcceptButton`/`CancelButton` al cambiar de formulario, desplazamiento/restauración de `CustomClick`, layout de captions con textos largos y saltos `||`, anillo degradado de `Custom3DBorder`, notificación de `OLECompleteDrag`/`OLESetData`, `AllowDrop` opt-in compatible con `OLEDropMode=Manual`, PropertyGrid, serialización, DPI, Accept/Cancel, DragEnter con y sin manejador, rechazo explícito, DragDrop, DataObject OLE, sombreado XP, GradientMode, protección de canales fuera de rango, alineación vertical de `PictureAlign`, Java/ForeColor, redimensionado proporcional de `Custom*`, conversión VB6 de ShadeColor, límites de canales, sincronización de `HoverColorLocks` y validación de enums |
| `dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj -c Release --no-build` | 56 correctas, 0 fallidas con xUnit v3/Microsoft Testing Platform; el recuento lo genera el runner y cada caso ejecuta el cuerpo STA real |
| `dotnet run ... -- --visual-snapshot .\artifacts\visual\net8` | 32 capturas .NET generadas; no se consideran referencia VB6 hasta disponer de las capturas originales |
| `dotnet run ... -- --visual-compare .\artifacts\visual\net8 .\artifacts\visual\net8` | Autocomprobación de consistencia: 32/32 capturas idénticas; 0 píxeles diferentes y delta 0. No sustituye una referencia VB6. |
| `dotnet run --project .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-build -- --snapshot .\artifacts\visual\net8\sample-client.png` | Captura cliente reproducible de 508×374 generada correctamente |
| Comparación `sample-client.png` VB6/.NET con el comparador | 19.450/189.992 píxeles diferentes (10,24 %); delta de canales 5.207.098. La ejecución interactiva confirmó la geometría del sample y se corrigió la máscara de imágenes normales para eliminar el marco amarillo de los recursos; issue #2 permanece abierto y la métrica no es paridad pixel a pixel |
| `dotnet run --project .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-build` | Sample WinForms iniciado y verificado visualmente: abrió sin excepción de colores y mostró la interfaz; el sample no configura drag-and-drop/OLE de destino |
| `DesignerSample` + Bloc de notas (sesión interactiva 2026-11-13) | Se pudo abrir y seleccionar texto en Bloc de notas, pero la automatización perdió la ventana de `DesignerSample` antes del arrastre entre ventanas; el estado del receptor no cambió. No es evidencia de interoperabilidad OLE externa. |
| Validación de archivos en `artifacts\visual\net8` | 32/32 PNG legibles, 120×40 píxeles, tamaño mayor que cero |
| `dotnet .\LaVolpe.Buttons.Tests\bin\Release\net8.0-windows\LaVolpe.Buttons.Tests.dll` | 56 correctas, 0 fallidas |
| `dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj -c Release --no-build` | 56 correctas, 0 fallidas; el comparador visual y el generador de capturas siguen disponibles en `LaVolpe.Buttons.Tests` |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | Puede fallar en este entorno por compilación paralela (`MSB5021`); la variante reproducible `-m:1` sí es correcta |
| IDE VB6 + `prjSampleButtons.vbp` | Sesión interactiva verificada el 2026-11-13: el sample original ejecuta y muestra la referencia visual; el EXE independiente sigue fallando con `Reasignación no válida del archivo DLL del sistema`; aún faltan las 32 capturas sistemáticas de #12 |
| `dotnet pack .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj -c Release --no-restore --output .\artifacts\nuget` | Paquete `LaVolpe.Buttons.Net.1.0.0.nupkg` creado sin warnings; contiene DLL/XML, `LICENSE`, `README.md` y `NOTICE.md` |

## Casos cubiertos por el harness

Texto/Caption, defaults, opciones del PropertyGrid, `ShouldSerialize*`, `AllowDrop` opt-in, colores heredados, Value de Command/Check/Option, Click y DialogResult, exclusividad, foco, región de formas, `PictureAlign` en sus seis posiciones y límites del layout, máscara `CustomFlat` con tolerancia VB6, redimensionado proporcional de `Custom*`, imagen liberada, `MouseIcon`, `DelayDrawing`/`BeginDelayDrawing`/`EndDelayDrawing`, HDC, 15 punteros, `DragIcon`, aceptación OLE de texto en `DragEnter` con y sin manejador, respeto del rechazo explícito, entrega de texto en `DragDrop`, sombreado XP por filas, `GradientMode`, validación de enums fuera de rango, superficie marcada, ocho estilos, cuatro colores XP por anillo, XP disabled, offsets y caption en custom marcado.

## Limitaciones

La sesión interactiva actual del IDE VB6 permite ejecutar `prjSampleButtons.vbp` y verificar el formulario original; el EXE independiente sigue fallando antes de crear ventana con `Reasignación no válida del archivo DLL del sistema`, incluso usando compatibilidad de proceso `WINXPSP3`. La matriz lógica 96/120/144/192 y el hook `OnDpiChangedAfterParent` sí están cubiertos por el harness, pero no hay comparación del OCX VB6 en monitores físicos a esos DPI. Visual Studio mostró `LaVolpeButton` en el Toolbox y cargó `Form1.cs [Diseño]`, pero aún no se ha confirmado la reapertura ni la serialización de imágenes; tampoco se verificaron drag/drop externo ni persistencia ActiveX. El sample VB6 no contiene un receptor OLE visible: los eventos existen en `lvButtons.ctl`, pero su interoperabilidad externa sigue separada del sample.
