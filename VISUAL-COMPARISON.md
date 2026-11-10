# Comparación visual VB6 → .NET

## Estado

El comparador está implementado. La referencia VB6 sí se ejecuta cuando se inicia `prjSampleButtons.vbp` desde el IDE de VB6; la ventana de ejecución muestra los ocho estilos, diagonales, formas circulares, imágenes y controles de configuración. El ejecutable independiente `LaVolpe-Buttons-Updated-/prjSampleButtons.exe` devuelve en este equipo `Reasignación no válida del archivo DLL del sistema`, por lo que la captura automatizada debe hacerse desde el IDE o en otro equipo compatible.

La referencia visual del formulario completo está almacenada en `artifacts/visual/vb6/sample.png`. El sample WinForms reproduce ahora la distribución del `frmSample.frm`, incluidos los tres botones diagonales con sus imágenes, el botón redondo, controles de opción y combos. La paridad pixel a pixel completa sigue sin afirmarse: el motor de pintura VB6/GDI y el de WinForms no son idénticos y la captura de referencia incluye el IDE alrededor del formulario.

## Generar capturas del port

Desde la raíz del repositorio:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build -- --visual-snapshot .\artifacts\visual\net8
```

Se generan 32 PNG: los ocho `BackStyle` del control y los estados `normal`, `focused`, `checked` y `disabled`.

## Comparar contra capturas VB6

Coloca las capturas del mismo escenario y tamaño en `artifacts\visual\vb6` usando los mismos nombres y ejecuta:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build -- --visual-compare .\artifacts\visual\vb6 .\artifacts\visual\net8
```

El comando informa píxeles diferentes, porcentaje y delta total por imagen, y devuelve código 1 si falta una captura o tiene tamaño distinto.

## Escenario de referencia

La escena usa un botón de 120×40 píxeles, texto `Botón`, modo `CheckBox` y las paletas definidas por `DrawButton_Win95`, `DrawButton_Win31`, `DrawButton_WinXP`, `DrawButton_Macintosh`, `DrawButton_Flat`, `DrawButton_Hover`, `DrawButton_Netscape` y `DrawButton_Java` de `LaVolpe-Buttons-Updated-/lvButtons.ctl`.
