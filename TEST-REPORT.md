# Test report

Fecha: 2026-11-10. SDK fijado por `global.json` a 8.0.427.

| Comando | Resultado |
|---|---|
| `dotnet restore .\LaVolpe.Buttons.Net.sln` | Correcto |
| `dotnet build .\LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-restore` | Correcto; 0 warnings, 0 errors |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1` | Correcto; 0 warnings, 0 errors |
| `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build` | 29 correctas, 0 fallidas |
| `dotnet run ... -- --visual-snapshot .\artifacts\visual\net8` | 32 capturas generadas; referencia VB6 observable desde el IDE |
| `dotnet .\LaVolpe.Buttons.Tests\bin\Release\net8.0-windows\LaVolpe.Buttons.Tests.dll` | 29 correctas, 0 fallidas |
| `dotnet test` en cada `.csproj` | No descubre tests: son proyectos EXE sin Microsoft.NET.Test.Sdk |
| `dotnet build .\LaVolpe.Buttons.Net.sln -c Release` | Puede fallar en este entorno por compilación paralela (`MSB5021`); la variante reproducible `-m:1` sí es correcta |

## Casos cubiertos por el harness

Texto/Caption, defaults, opciones del PropertyGrid, colores heredados, Value de Command/Check/Option, Click y DialogResult, exclusividad, foco, región de formas, máscara `CustomFlat`, imagen liberada, `MouseIcon`, `DelayDrawing`, HDC, 15 punteros, `DragIcon`, superficie marcada, ocho estilos, cuatro colores XP por anillo, XP disabled, offsets y caption en custom marcado.

## Limitaciones

No se ejecutó el OCX VB6 ni se hizo captura píxel a píxel. No se verificaron DPI 96/120/144/192, diseñador de Visual Studio, drag/drop externo ni persistencia ActiveX.
