# Comparación visual VB6 → .NET

Actualización 2026-11-13: tras conservar `ForeColor` en Java, aplicar la máscara VB6 a imágenes personalizadas y normales, redimensionar `Custom*` proporcionalmente, corregir las conversiones de los canales verde y azul de `ShadeColor`, reproducir la secuencia de `LoadGradientColors` en gradientes, bordes `Round3D` y `Custom3DBorder`, y usar `NearestNeighbor` al construir la máscara, la captura queda en 19.450/189.992 píxeles distintos (10,24 %) y delta 5.207.098. La ejecución interactiva confirmó que el marco amarillo era una diferencia de máscara del recurso normal y ya está corregido. El issue #2 permanece abierto; la métrica técnica sigue sin paridad píxel a píxel.

## Estado

El comparador está implementado. La referencia VB6 se ejecutó el 2026-11-13 desde `prjSampleButtons.vbp` en el IDE y mostró los ocho estilos, diagonales, formas circulares, imágenes y controles de configuración. El ejecutable independiente `LaVolpe-Buttons-Updated-/prjSampleButtons.exe` devuelve en este equipo `Reasignación no válida del archivo DLL del sistema`, por lo que la matriz automatizada de 32 escenarios aún requiere capturas manuales o un equipo compatible.

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

El capturador usa `PrintWindow` y recorta `sample-client.png` al área cliente de 508×374. No se compara `sample-form.png` directamente: la ventana completa WinForms mide 524×413 por las métricas de marco/título del sistema, frente a 510×405 en la captura VB6.

## Comparar contra capturas VB6

Coloca las capturas del mismo escenario y tamaño en `artifacts\visual\vb6` usando los mismos nombres y ejecuta:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Release --no-build -- --visual-compare .\artifacts\visual\vb6 .\artifacts\visual\net8
```

El comando informa píxeles diferentes, porcentaje y delta total por imagen, y devuelve código 1 si falta una captura o tiene tamaño distinto. Tras corregir la altura de `lvButtons1(0)` a 52 píxeles (780 twips), limpiar los píxeles residuales de los recortes de laptop y disquete, ajustar el recorte interno de los iconos, corregir el extremo XP del degradado según `ShadeColor(..., &H30, True)`, reproducir el sombreado XP y `DrawGradient` por franjas de 1 píxel, trasladar los puntos `polyPts(10..15)` del borde diagonal exterior, reproducir `DrawButtonBorder` con 14 segmentos GDI para estilos rectangulares/diagonales, desactivar el antialiasing del control para aproximar las regiones GDI, usar `FlatStyle.Flat` y el borde 3D específico `Vb6ComboBox` en los combos, fijar `Label2` a 330×13 píxeles, ajustar el borde gris y el rectángulo efectivo de los óvalos, usar `NearestNeighbor` en las imágenes para aproximar `StretchBlt`, ajustar el marco azul al `BorderColor` VB6 RGB(0,0,192), los textos azules de Win XP/Java, la tolerancia de máscara `NormalizeImage` a ±3, eliminar el padding implícito de `TextRenderer`, redimensionar `Custom*` con la proporción de `ScaleImage`, reproducir las conversiones de los canales verde y azul de `ShadeColor`, reproducir la secuencia iterativa con clamp de `LoadGradientColors` en gradientes y `Round3D`, y usar `NearestNeighbor` al construir la máscara, `sample-client.png` produce 19.450/189.992 píxeles diferentes (10,24 %) y delta de canales 5.207.098; el issue #2 permanece abierto porque aún no hay referencia VB6 de los 32 escenarios y no se declara paridad pixel a pixel.

### Distribución regional de la diferencia actual

La misma comparación 508×374, dividida por bandas funcionales del formulario, produce esta distribución de píxeles distintos. Es un diagnóstico del port actual, no una nueva referencia VB6:

| Banda | Coordenadas Y | Píxeles distintos | Porcentaje de la diferencia |
|---|---:|---:|---:|
| Estilos superiores | 0–105 | 961 | 4,9 % |
| Botones personalizados | 106–178 | 5.354 | 27,5 % |
| Radios de modo | 179–205 | 2.798 | 14,4 % |
| Marco y botones diagonales | 206–291 | 4.060 | 20,9 % |
| Combos, botón redondo y checks | 292–373 | 6.277 | 32,3 % |

Las dos zonas prioritarias son los controles inferiores nativos y los botones personalizados; ambas requieren conservar la geometría y obtener una captura VB6 equivalente para distinguir una diferencia de rasterización de una diferencia de implementación.

## Escenario de referencia

La escena usa un botón de 120×40 píxeles, texto `Botón`, modo `CheckBox` y las paletas definidas por `DrawButton_Win95`, `DrawButton_Win31`, `DrawButton_WinXP`, `DrawButton_Macintosh`, `DrawButton_Flat`, `DrawButton_Hover`, `DrawButton_Netscape` y `DrawButton_Java` de `LaVolpe-Buttons-Updated-/lvButtons.ctl`.
