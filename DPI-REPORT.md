# Matriz DPI

Actualización 2026-11-13: `LaVolpe.Buttons.Sample`, `LaVolpe.Buttons.Tests` y `LaVolpe.Buttons.DesignerSample` declaran `ApplicationHighDpiMode=PerMonitorV2`. El control reconstruye su región en `OnDpiChangedAfterParent`; sigue pendiente una comparación del OCX VB6 en monitores físicos con DPI distintos.

## Resultado

El harness verifica la geometría y el layout del control usando tamaños y fuentes equivalentes a 96, 120, 144 y 192 DPI. En cada escala se prueban las tres diagonales y `Round3D`, se genera una superficie, se comprueba que la región existe y contiene el centro, y se verifica que caption e imagen permanecen dentro del cliente; además invoca el hook `OnDpiChangedAfterParent` y verifica que reconstruye la región.

| Escala lógica | Tamaño probado | Formas | Resultado |
|---:|---:|---|---|
| 96 DPI | 120×40 | Left/Right/FullDiagonal, Round3D | Correcto |
| 120 DPI | 150×50 | Left/Right/FullDiagonal, Round3D | Correcto |
| 144 DPI | 180×60 | Left/Right/FullDiagonal, Round3D | Correcto |
| 192 DPI | 240×80 | Left/Right/FullDiagonal, Round3D | Correcto |

Comando:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build
```

El control también reconstruye su región y repinta en `OnDpiChangedAfterParent` (`LaVolpe.Buttons.Net/LaVolpeButton.cs:409`). Esta matriz verifica escalado geométrico reproducible, pero no cambia la configuración DPI del monitor de Windows. La comparación visual del OCX VB6 ejecutándose realmente a 120/144/192 DPI sigue pendiente; el issue #3 permanece abierto.
