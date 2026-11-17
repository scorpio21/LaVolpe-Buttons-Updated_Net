# Memoria del proyecto

## Objective

Continuar el port VB6→WinForms de LaVolpe Buttons en `K:\Proyectos\LaVolpe-Buttons-Updated`, preservando los eventos OLE y sin añadir drag-and-drop al sample principal.

## Compile/verify commands

- `dotnet build .\LaVolpe-Buttons.Net.sln --configuration Release --no-restore --maxcpucount:1 --verbosity minimal` → 5 proyectos, 0 warnings, 0 errors.
- `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj --configuration Release --no-build` → 56 correctas, 0 fallidas.
- `dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj --configuration Release --no-build` → 56 correctas, 0 fallidas con xUnit v3/Microsoft Testing Platform.
- Captura/compare sample → `sample-client.png`: 19.450/189.992 píxeles distintos (10,24 %), delta 5.207.098 tras portar el anillo de `Custom3DBorder`.

## Current state

- ✅ Clic físico Win32 no duplica `Click`/`Value`; `StandardClick=false` está probado.
- ✅ `AllowDrop=false` por defecto; solo `DesignerSample` activa `AllowDrop=true` y `DragMode=Automatic`.
- ✅ Eventos OLE conservados y cubiertos en harness; validación con aplicación externa sigue pendiente.
- ✅ `OLEStartDrag` respeta el `Effect` que establezca el manejador y permite cancelar el origen con `None`; `OLECompleteDrag` sigue notificándose en `finally`.
- ✅ `CustomFlat`/`Custom3DBorder` redimensiona el control proporcionalmente según `ScaleImage` de VB6; prueba y métrica visual mejoradas.
- ✅ El punto visual 1 añade una regresión para las máscaras amarillas de los recursos del sample; la captura actual ya no muestra el cuadrado amarillo detrás del botón custom.
- ✅ Issue #22: `LaVolpeButton.Palette` permite personalizar fondos, textos y foco por estado desde el diseñador; los colores `Empty` conservan el renderer VB6 y `ResetDefaultColors` restaura la paleta.
- ✅ Accesibilidad: `AccessibleRole` cambia entre PushButton, CheckButton y RadioButton según `Mode`; el harness lo cubre.
- ✅ Diseñador: `Palette` muestra descripciones, se serializa solo cuando cambia y ofrece `ResetPalette()`.
- ✅ PropertyGrid: la paleta muestra `(personalizada)` cuando contiene colores configurados y `(predeterminada)` tras restaurarla.
- ✅ `ShadeColor` reproduce las conversiones VB6 de los canales verde y azul y `BuildImageRegion` usa `NearestNeighbor`; `FillConfiguredGradient` y el borde `Round3D` reproducen la secuencia de `LoadGradientColors` con clamp VB6; la métrica visual actual es 10,24 %.
- ✅ `ButtonShape=Custom*` sin `Picture` se rechaza, `Picture=null` devuelve a rectangular y `PictureSize` fijo se rechaza en custom.
- ✅ `TimerUpdate(long)` y los hooks auxiliares OLE dejaron de ser API pública; la cobertura interna permanece.
- ✅ DPI lógico 96/120/144/192 y hook `OnDpiChangedAfterParent` cubiertos; la pantalla primaria del equipo actual fue medida a 96 DPI (3440×1440), por lo que la comparación física VB6 a 120/144/192 sigue pendiente.
- ⚠️ Pintado no es pixel-equivalente; los issues visuales #2 y #12 se cerraron por quedar fuera del objetivo actual de implantación .NET.
- ✅ La atribución de procedencia se añadió a `LICENSE`, `README.md`, `NOTICE.md` y al paquete NuGet; issue #17 sigue abierto porque los términos de redistribución del original no están verificados.
- ✅ Issue #16 validado en Visual Studio Enterprise: `Form1.cs [Diseño]` carga, `LaVolpeButton` aparece en el Toolbox y el PropertyGrid expone sus propiedades principales; issue cerrado en GitHub.
- ✅ El comparador visual rechaza referencia y salida en la misma carpeta para evitar resultados autorreferentes; los issues de capturas #2 y #12 están cerrados.
- ⚠️ No cerrar issues de GitHub sin evidencia; los issues abiertos se mantienen abiertos cuando falta una validación externa o legal.
- ✅ `dotnet test` descubre y ejecuta los 56 casos mediante `LaVolpe.Buttons.XunitTests`; `dotnet run` conserva el harness STA y las herramientas visuales.

## Work history

- `8599b56` — Reject self-referential visual comparisons (publicado en `origin/main`).
- `2ccbe61` — Clarify upstream licensing scope (publicado en `origin/main`).
- `a6709a3` — Continue VB6 button port and visual parity (publicado en `origin/main`).
- `e05bb5b` — Cover yellow transparency mask regression (publicado en `origin/main`).
- Pendiente de publicar — Primera paleta visual propia inspirada en ideas de Krypton, sin copiar código ni recursos.

## Next steps

1. Validar DPI físico 120/144/192 si el entorno lo permite (#3); el harness lógico ya está cubierto, pero no basta para cerrar el issue.
2. Resolver los términos legales originales antes de publicar NuGet (#17); no cerrar sin verificar la licencia upstream.
3. Mantener los eventos OLE declarados por compatibilidad; el issue #6 se cerró como no planificado porque el VB6 original solo reenvía eventos no utilizados.

## Known gotchas

- `LaVolpe-Buttons-Updated-\lvButtons.ctl:1350-1380` confirma que VB6 llama `ScaleImage` y cambia el tamaño del control para `Custom*`.
- La comparación full-window no es válida directamente: el cliente WinForms comparable es 508×374; el marco WinForms mide distinto al formulario VB6.
- Las 32 capturas `net8` contra sí mismas solo validan determinismo, no equivalencia VB6.
- El intento de enmarcar `Custom3DBorder` con un gradiente blanco/gris sobre la máscara mantuvo los píxeles distintos pero aumentó el delta; fue revertido.
