# Comparación visual VB6 → .NET

## Estado

El comparador está implementado. La referencia VB6 sí se ejecuta cuando se inicia `prjSampleButtons.vbp` desde el IDE de VB6; la ventana de ejecución muestra los ocho estilos, diagonales, formas circulares, imágenes y controles de configuración. El ejecutable independiente `LaVolpe-Buttons-Updated-/prjSampleButtons.exe` devuelve en este equipo `Reasignación no válida del archivo DLL del sistema`, por lo que la captura automatizada debe hacerse desde el IDE o en otro equipo compatible.

La referencia visual del formulario completo está almacenada en `artifacts/visual/vb6/sample-form.png`. Para comparar el contenido sin barra de título se usa `artifacts/visual/vb6/sample-client.png`, un recorte de 508×374 del formulario ejecutado desde VB6. El sample WinForms puede generar el mismo tamaño con `--snapshot`.

## Generar capturas del port

Desde la raíz del repositorio, para los 32 escenarios del control:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build -- --visual-snapshot .\artifacts\visual\net8
```

Se generan 32 PNG: los ocho `BackStyle` del control y los estados `normal`, `focused`, `checked` y `disabled`.

Para capturar el sample completo .NET, con el formulario visible y sin depender del contenido que haya delante de la ventana:

```powershell
dotnet run --project .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj -c Release --no-build -- --snapshot .\artifacts\visual\net8\sample-client.png
```

El capturador usa `PrintWindow` y recorta el área cliente a 508×374. No se debe comparar `sample-form.png` directamente con esta imagen porque la referencia incluye la barra de título.

## Comparar contra capturas VB6

Coloca las capturas del mismo escenario y tamaño en `artifacts\visual\vb6` usando los mismos nombres y ejecuta:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build -- --visual-compare .\artifacts\visual\vb6 .\artifacts\visual\net8
```

El comando informa píxeles diferentes, porcentaje y delta total por imagen, y devuelve código 1 si falta una captura o tiene tamaño distinto. Tras corregir la altura de `lvButtons1(0)` a 52 píxeles (780 twips), limpiar los píxeles residuales de los recortes de laptop y disquete, ajustar el recorte interno de los iconos, corregir el extremo XP del degradado según `ShadeColor(..., &H30, True)`, reproducir el sombreado XP y `DrawGradient` por franjas de 1 píxel, trasladar los puntos `polyPts(10..15)` del borde diagonal exterior, reforzar la rasterización no suavizada dentro del área cliente, ajustar el marco azul al `BorderColor` VB6 RGB(0,0,192), el botón redondo, los textos azules de Win XP/Java, la tolerancia de máscara `NormalizeImage` a ±3 y eliminar el padding implícito de `TextRenderer`, `sample-client.png` produce 68.422/189.992 píxeles diferentes (36,01 %) y delta de canales 7.193.961; el issue #2 sigue abierto y no se declara paridad pixel a pixel.

## Escenario de referencia

La escena usa un botón de 120×40 píxeles, texto `Botón`, modo `CheckBox` y las paletas definidas por `DrawButton_Win95`, `DrawButton_Win31`, `DrawButton_WinXP`, `DrawButton_Macintosh`, `DrawButton_Flat`, `DrawButton_Hover`, `DrawButton_Netscape` y `DrawButton_Java` de `LaVolpe-Buttons-Updated-/lvButtons.ctl`.
