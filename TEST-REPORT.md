# Test report

Fecha: 2026-11-11. SDK fijado por `global.json` a 8.0.427.

| Comando | Resultado |
|---|---|
| `dotnet restore .\LaVolpe.Buttons.Net.sln` | Correcto |
| `dotnet build .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1` | Correcto; 0 warnings, 0 errors |
| `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build` | 39 correctas, 0 fallidas; PropertyGrid, serialización, DPI, Accept/Cancel, DragEnter con y sin manejador, rechazo explícito, DragDrop, DataObject OLE, sombreado XP, GradientMode y validación de enums verificados |
| `dotnet run ... -- --visual-snapshot .\artifacts\visual\net8` | 32 capturas generadas; referencia VB6 observable desde el IDE |
| `dotnet run ... -- --visual-compare .\artifacts\visual\net8 .\artifacts\visual\net8` | 32/32 capturas idénticas; 0 píxeles diferentes y delta 0 |
| `dotnet run --project .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-build -- --snapshot .\artifacts\visual\net8\sample-client.png` | Captura cliente reproducible de 508×374 generada correctamente |
| Comparación `sample-client.png` VB6/.NET con el comparador | 55.802/189.992 píxeles diferentes (29,37 %); delta de canales 5.820.722 tras ajustar altura VB6, recortes, tipografía, `Label2` 330×13, sombreado XP por fila, GradientMode por franjas, `DrawButtonBorder` con 14 segmentos GDI, antialiasing desactivado, borde 3D específico `Vb6ComboBox`, borde/rectángulo efectivo de óvalos, imágenes `NearestNeighbor`, máscara `NormalizeImage` ±3 y marco VB6; issue #2 permanece abierto |
| `dotnet run --project .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-build` | Sample WinForms iniciado y verificado visualmente contra `artifacts/visual/vb6/sample.png` |
| Validación de archivos en `artifacts\visual\net8` | 32/32 PNG legibles, 120×40 píxeles, tamaño mayor que cero |
| `dotnet .\LaVolpe.Buttons.Tests\bin\Release\net8.0-windows\LaVolpe.Buttons.Tests.dll` | 35 correctas, 0 fallidas |
| `dotnet test .\\LaVolpe.Buttons.Net.sln -c Release --no-build` | Código 1 sin descubrir tests: son proyectos EXE sin `Microsoft.NET.Test.Sdk`; el harness equivalente ejecuta 39/39 |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | Puede fallar en este entorno por compilación paralela (`MSB5021`); la variante reproducible `-m:1` sí es correcta |
| `dotnet pack .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj -c Release --no-restore --output .\artifacts\nuget` | Paquete `LaVolpe.Buttons.Net.1.0.0.nupkg` creado sin warnings; contiene DLL/XML, `LICENSE` y `README.md` |

## Casos cubiertos por el harness

Texto/Caption, defaults, opciones del PropertyGrid, colores heredados, Value de Command/Check/Option, Click y DialogResult, exclusividad, foco, región de formas, máscara `CustomFlat` con tolerancia VB6, imagen liberada, `MouseIcon`, `DelayDrawing`, HDC, 15 punteros, `DragIcon`, aceptación OLE de texto en `DragEnter` con y sin manejador, respeto del rechazo explícito, entrega de texto en `DragDrop`, sombreado XP por filas, `GradientMode`, validación de enums fuera de rango, superficie marcada, ocho estilos, cuatro colores XP por anillo, XP disabled, offsets y caption en custom marcado.

## Limitaciones

La captura VB6 usada procede del formulario ejecutado desde el IDE; el EXE independiente falla con `Reasignación no válida del archivo DLL del sistema`. La comparación cliente ya se ejecutó, pero no alcanza paridad pixel a pixel. No se verificaron DPI 96/120/144/192, diseñador de Visual Studio, drag/drop externo ni persistencia ActiveX.
