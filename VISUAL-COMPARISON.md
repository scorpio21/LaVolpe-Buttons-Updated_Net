# Comparación visual VB6 → .NET

## Estado

El comparador está implementado, pero la referencia VB6 no se ha podido ejecutar en este equipo. `VB6.EXE` devuelve el error de Windows `Reasignación no válida del archivo DLL del sistema` al abrir `LaVolpe-Buttons-Updated-/prjSampleButtons.vbp`.

Por tanto, no se afirma paridad pixel a pixel. Las capturas `.NET` sirven como salida reproducible y la matriz mantiene esta diferencia como `no verificable`.

## Generar capturas del port

Desde la raíz del repositorio:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build -- --visual-snapshot .\artifacts\visual\net8
```

Se generan 24 PNG: los ocho `BackStyle` del control y los estados `normal`, `checked` y `disabled`.

## Comparar contra capturas VB6

Coloca las capturas del mismo escenario y tamaño en `artifacts\visual\vb6` usando los mismos nombres y ejecuta:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build -- --visual-compare .\artifacts\visual\vb6 .\artifacts\visual\net8
```

El comando informa píxeles diferentes, porcentaje y delta total por imagen, y devuelve código 1 si falta una captura o tiene tamaño distinto.

## Escenario de referencia

La escena usa un botón de 120×40 píxeles, texto `Botón`, modo `CheckBox` y las paletas definidas por `DrawButton_Win95`, `DrawButton_Win31`, `DrawButton_WinXP`, `DrawButton_Macintosh`, `DrawButton_Flat`, `DrawButton_Hover`, `DrawButton_Netscape` y `DrawButton_Java` de `LaVolpe-Buttons-Updated-/lvButtons.ctl`.
