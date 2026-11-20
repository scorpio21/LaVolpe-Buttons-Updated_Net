# Test report

Fecha: 2026-11-19. SDK fijado por `global.json` a 8.0.427.

| Comando | Resultado |
|---|---|
| `dotnet restore .\LaVolpe.Buttons.Net.sln` | Correcto |
| `dotnet build .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1` | Correcto; 0 warnings, 0 errors |
| `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build` | 80 correctas, 0 fallidas; añade el contrato directo de `LvMouseDown`/`LvMouseUp`/`LvMouseMove`/`LvKeyPress`/`ButtonDoubleClick` y `OLEGiveFeedback`, además de `ToolTipText`, `WhatsThisHelpID`, eventos y alias de `Value`, la matriz de `Picture`, `PictureAlign`, `PictureSize`, `ResetDefaultColors` tras reparentar y `ShowFocusRect` en formas compatibles; también incluye transiciones de `Mode`, OLE, PropertyGrid, serialización tipada, DPI, sombreado XP, gradientes, máscaras y validación de enums |
| `dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj -c Release --no-build` | 56 correctas, 0 fallidas con xUnit v3/Microsoft Testing Platform; el recuento lo genera el runner y cada caso ejecuta el cuerpo STA real |
| `dotnet run ... -- --visual-snapshot .\artifacts\visual\net8` | 32 capturas .NET generadas; no se consideran referencia VB6 hasta disponer de las capturas originales |
| `dotnet run ... -- --visual-compare .\artifacts\visual\net8 .\artifacts\visual\net8` | Rechazado correctamente (referencia y salida son la misma carpeta); evita una comparación autorreferente. No sustituye una referencia VB6. |
| `dotnet run --project .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-build -- --snapshot .\artifacts\visual\net8\sample-client.png` | Captura cliente reproducible de 508×374 generada correctamente |
| Comparación `sample-client.png` VB6/.NET con el comparador | 19.450/189.992 píxeles diferentes (10,24 %); delta de canales 5.207.098. La ejecución interactiva confirmó la geometría del sample y se corrigió la máscara de imágenes normales para eliminar el marco amarillo de los recursos; issue #2 permanece abierto y la métrica no es paridad pixel a pixel |
| `dotnet run --project .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-build` | Sample WinForms iniciado y verificado visualmente: abrió sin excepción de colores y mostró la interfaz; el sample no configura drag-and-drop/OLE de destino |
| Validación de archivos en `artifacts\visual\net8` | 32/32 PNG legibles, 120×40 píxeles, tamaño mayor que cero |
| `dotnet .\LaVolpe.Buttons.Tests\bin\Release\net8.0-windows\LaVolpe.Buttons.Tests.dll` | 56 correctas, 0 fallidas |
| `dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj -c Release --no-build` | 56 correctas, 0 fallidas; el comparador visual y el generador de capturas siguen disponibles en `LaVolpe.Buttons.Tests` |
| `dotnet build .\LaVolpe.Buttons.Net.sln --configuration Debug --no-restore --maxcpucount:1` | Correcto; 0 advertencias, 0 errores. `DesignerSample2` se recreó desde una plantilla WinForms limpia, sin `Form1.resx`, recursos binarios ni drag-and-drop. |
| `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj --configuration Debug --no-build` | 60 correctas, 0 fallidas después de integrar `HelpContextID` con `HelpProvider` y validar `Index` en eventos. |
| `dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj --configuration Debug --no-build` | 56 correctas, 0 fallidas después de retirar el recurso binario del diseñador. |
| `DesignerSample2` nuevo en `LaVolpe.Buttons.Net.sln` | Proyecto creado desde cero con un único `LaVolpeButton`; compila en Debug sin advertencias. Harness 58/58 y xUnit 56/56 tras el cambio. |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | Puede fallar en este entorno por compilación paralela (`MSB5021`); la variante reproducible `-m:1` sí es correcta |
| IDE VB6 + `prjSampleButtons.vbp` | Sesión interactiva verificada el 2026-11-13: el sample original ejecuta y muestra la referencia visual; el EXE independiente sigue fallando con `Reasignación no válida del archivo DLL del sistema`; aún faltan las 32 capturas sistemáticas de #12 |
| `dotnet pack .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj -c Release --no-restore --output .\artifacts\nuget` | Paquete `LaVolpe.Buttons.Net.1.0.0.nupkg` creado sin warnings; contiene DLL/XML, `LICENSE`, `README.md` y `NOTICE.md` |
| `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Debug --no-build -- --physical-dpi-report .\artifacts\dpi\issue-3` | Diagnóstico físico reproducible: monitor `DISPLAY1` a 96 DPI, botón a 96 DPI, tamaño/región/layout correctos y captura `__._DISPLAY1-dpi96.png` |
| Diagnóstico físico en `artifacts\dpi\issue-3\125` | `DISPLAY1` a 120 DPI; `Form DPI == Button DPI`, región/layout correctos, resultado `OK` |
| Diagnóstico físico en `artifacts\dpi\issue-3\150` | `DISPLAY1` a 144 DPI; `Form DPI == Button DPI`, región/layout correctos, resultado `OK` |
| Diagnóstico físico en `artifacts\dpi\issue-3\200` | `DISPLAY1` a 192 DPI; `Form DPI == Button DPI`, región/layout correctos, resultado `OK` |

## Casos cubiertos por el harness

Texto/Caption, defaults, opciones del PropertyGrid, `ShouldSerialize*`, `AllowDrop` opt-in, colores heredados, Value de Command/Check/Option, Click y DialogResult, exclusividad, foco, región de formas, `PictureAlign` en sus seis posiciones y límites del layout, máscara `CustomFlat` con tolerancia VB6, redimensionado proporcional de `Custom*`, imagen liberada, `MouseIcon`, `DelayDrawing`/`BeginDelayDrawing`/`EndDelayDrawing`, HDC, 15 punteros, `DragIcon`, aceptación OLE de texto en `DragEnter` con y sin manejador, respeto del rechazo explícito, entrega de texto en `DragDrop`, sombreado XP por filas, `GradientMode`, validación de enums fuera de rango, superficie marcada, ocho estilos, cuatro colores XP por anillo, XP disabled, offsets y caption en custom marcado.

## Limitaciones

La sesión interactiva actual del IDE VB6 permite ejecutar `prjSampleButtons.vbp` y verificar el formulario original; el EXE independiente sigue fallando antes de crear ventana con `Reasignación no válida del archivo DLL del sistema`, incluso usando compatibilidad de proceso `WINXPSP3`. La matriz lógica 96/120/144/192 y el hook `OnDpiChangedAfterParent` sí están cubiertos por el harness, pero no hay comparación del OCX VB6 en monitores físicos a esos DPI. Visual Studio mostró `LaVolpeButton` en el Toolbox, cargó `Form1.cs [Diseño]` y se validó la reapertura con persistencia de `ButtonShape`, `CheckBox` y `OptionButton`; todavía no se verificó la serialización de imágenes, el drag/drop externo ni la persistencia ActiveX. El sample VB6 no contiene un receptor OLE visible: los eventos existen en `lvButtons.ctl`, pero su interoperabilidad externa sigue separada del sample.
