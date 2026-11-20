# Memoria del proyecto

## Objective

Continuar el port VB6→WinForms de LaVolpe Buttons en `K:\Proyectos\LaVolpe-Buttons-Updated`, preservando los eventos OLE y sin añadir drag-and-drop al sample principal.

## Compile/verify commands

- `dotnet build .\LaVolpe-Buttons.Net.sln --configuration Release --no-restore --maxcpucount:1 --verbosity minimal` → 5 proyectos, 0 warnings, 0 errors.
- `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj --configuration Release --no-build` → 56 correctas, 0 fallidas.
- `dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj --configuration Release --no-build` → 56 correctas, 0 fallidas con xUnit v3/Microsoft Testing Platform.
- Captura/compare sample → `sample-client.png`: 19.450/189.992 píxeles distintos (10,24 %), delta 5.207.098 tras portar el anillo de `Custom3DBorder`.
- Verificación vigente (2026-11-20): `dotnet build .\LaVolpe.Buttons.Net.sln --configuration Debug --no-restore --maxcpucount:1 --verbosity minimal` → 5 proyectos, 0 warnings, 0 errors.
- Verificación vigente (2026-11-20): `dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj --configuration Debug --no-build` → **80 correctas, 0 fallidas**.

## Current state

- ✅ Estado documental vigente 2026-11-20: media ponderada **80,58 %** (81 % redondeado).
- ✅ Las áreas `Control WinForms funcional`, `Propiedades y enums`, `Eventos de mouse y teclado` y `CheckBox y OptionButton` aparecen al 100 % azul en `ESTADO-PORT-NET.md`, con evidencia del harness.
- ✅ `Instalación/distribución` está al 60 % por paquete NuGet/publicación reproducibles en `artifacts\\sdk\\`, sin publicación pública mientras #17 siga abierto.
- ✅ `Pruebas automatizadas` está al 80 % por 80 casos STA ejecutables.
- ✅ Issue #3 cerrado en GitHub como completado: validación física WinForms completa a 96/120/144/192 DPI.
- ⚠️ Issue #17 sigue abierto: falta confirmar licencia/redistribución del original VB6 antes de publicar NuGet.
- ⚠️ Issue #22 sigue abierto: ideas de Krypton; la paleta propia ya existe, pero faltan decisiones sobre temas, estados visuales, DPI y accesibilidad. Krypton se usa como inspiración, no como código o recursos copiados.
- ⚠️ Issue #6 no se toca hasta terminar los demás; no añadir drag-and-drop al sample principal.

- ✅ Clic físico Win32 no duplica `Click`/`Value`; `StandardClick=false` está probado.
- ✅ `AllowDrop=false` por defecto; `DesignerSample2` es un formulario limpio y no activa drag-and-drop.
- ✅ Eventos OLE conservados y cubiertos en harness; validación con aplicación externa sigue pendiente.
- ✅ `OLEStartDrag` respeta el `Effect` que establezca el manejador y permite cancelar el origen con `None`; `OLECompleteDrag` sigue notificándose en `finally`.
- ✅ `CustomFlat`/`Custom3DBorder` redimensiona el control proporcionalmente según `ScaleImage` de VB6; prueba y métrica visual mejoradas.
- ✅ El punto visual 1 añade una regresión para las máscaras amarillas de los recursos del sample; la captura actual ya no muestra el cuadrado amarillo detrás del botón custom.
- ✅ Issue #22: `LaVolpeButton.Palette` permite personalizar fondos, textos y foco por estado desde el diseñador; los colores `Empty` conservan el renderer VB6 y `ResetDefaultColors` restaura la paleta.
- ✅ Accesibilidad: `AccessibleRole` cambia entre PushButton, CheckButton y RadioButton según `Mode`; el harness lo cubre.
- ✅ Diseñador: `Palette` muestra descripciones, se serializa solo cuando cambia y ofrece `ResetPalette()`.
- ✅ PropertyGrid: la paleta muestra `(personalizada)` cuando contiene colores configurados y `(predeterminada)` tras restaurarla.
- ✅ `Palette.HoverBackColor` tiene prioridad sobre `HoverColorLocks` cuando se configura explícitamente, por lo que el color hover del diseñador funciona al mover el ratón.
- ✅ `ShouldSerializeMode()` conserva `CheckBox`/`OptionButton` en el código del diseñador; antes solo se persistían `AccessibleRole` y `Value`, y al ejecutar volvía a `CommandButton`.
- ✅ La persistencia de `Mode = lv_CheckBox` se valida en el harness; `DesignerSample2` parte de un formulario limpio con un único botón.
- ✅ `ShadeColor` reproduce las conversiones VB6 de los canales verde y azul y `BuildImageRegion` usa `NearestNeighbor`; `FillConfiguredGradient` y el borde `Round3D` reproducen la secuencia de `LoadGradientColors` con clamp VB6; la métrica visual actual es 10,24 %.
- ✅ `ButtonShape=Custom*` sin `Picture` se rechaza, `Picture=null` devuelve a rectangular y `PictureSize` fijo se rechaza en custom.
- ✅ `TimerUpdate(long)` y los hooks auxiliares OLE dejaron de ser API pública; la cobertura interna permanece.
- ✅ DPI lógico 96/120/144/192 y hook `OnDpiChangedAfterParent` cubiertos; la validación física del control WinForms pasó en `DISPLAY1` a 96, 120, 144 y 192 DPI con región/layout correctos. La comparación física del OCX VB6 sigue fuera del alcance validado.
- ⚠️ Pintado no es pixel-equivalente; los issues visuales #2 y #12 se cerraron por quedar fuera del objetivo actual de implantación .NET.
- ✅ La atribución de procedencia se añadió a `LICENSE`, `README.md`, `NOTICE.md` y al paquete NuGet; issue #17 sigue abierto porque los términos de redistribución del original no están verificados.
- ✅ Issue #16 validado en Visual Studio Enterprise: `Form1.cs [Diseño]` carga, `LaVolpeButton` aparece en el Toolbox y el PropertyGrid expone sus propiedades principales; issue cerrado en GitHub.
- ✅ El comparador visual rechaza referencia y salida en la misma carpeta para evitar resultados autorreferentes; los issues de capturas #2 y #12 están cerrados.
- ⚠️ No cerrar issues de GitHub sin evidencia; los issues abiertos se mantienen abiertos cuando falta una validación externa o legal.
- ✅ `dotnet test` descubre y ejecuta los 56 casos mediante `LaVolpe.Buttons.XunitTests`; `dotnet run` conserva el harness STA y las herramientas visuales.
- ✅ `LaVolpe.Buttons.DesignerSample` y su reemplazo anterior fueron eliminados; `DesignerSample2` se recreó desde la plantilla WinForms sin `Form1.resx`, recursos binarios ni drag-and-drop.

## Work history

- `f2a5738` — Refresh distribution and test coverage status (publicado en `origin/main`).
- `13a5ad0` — Align compatibility area percentages (publicado en `origin/main`).
- `f0f4b54` — Complete WinForms functional coverage (publicado en `origin/main`).
- `76301ce` — Document reproducible SDK artifacts (publicado en `origin/main`).

- `8599b56` — Reject self-referential visual comparisons (publicado en `origin/main`).
- `2ccbe61` — Clarify upstream licensing scope (publicado en `origin/main`).
- `a6709a3` — Continue VB6 button port and visual parity (publicado en `origin/main`).
- `e05bb5b` — Cover yellow transparency mask regression (publicado en `origin/main`).
- `676e07a` — Avoid blocked binary designer resource (publicado en `origin/main`).
- ✅ `DesignerSample2` nuevo verificado: solución Debug sin errores ni advertencias; harness 58/58 y xUnit 56/56.
- ✅ Issue #22 documentado en GitHub con límites BSD-3-Clause: solo inspiración salvo que se copie código, en cuyo caso hay que conservar copyright/licencia en `NOTICE.md`.
- ✅ Rama `codex/issue-3-dpi-validation` creada para el issue #3; el harness ahora genera `--physical-dpi-report` con la matriz de monitores/DPI físicos, valida tamaño/región/layout en cada pantalla detectada y guarda una captura PNG por monitor. El procedimiento queda documentado en `docs/DPI-VALIDATION.md`.
- ✅ `LaVolpe.Buttons.Tests` inicializa `ApplicationConfiguration`/`PerMonitorV2`; el diagnóstico dejó de quedar fijado a 96 DPI y ya detecta 120, 144, 192 y otros valores físicos.
- ✅ Issue #22: la arquitectura propia de estados de `LaVolpeButton.Palette` queda documentada en `docs\VISUAL-THEMES.md`, sin copiar código ni recursos de Krypton.

## Next steps

1. Elegir el siguiente bloque de #22: preferiblemente documentar/implementar temas propios y revisar la paleta por estado, sin copiar Krypton.
2. Resolver los términos legales originales antes de publicar NuGet (#17); no cerrar sin verificar la licencia upstream.
3. Mantener los eventos OLE declarados por compatibilidad; no tocar #6 hasta terminar los demás.

## Known gotchas

- `LaVolpe-Buttons-Updated-\lvButtons.ctl:1350-1380` confirma que VB6 llama `ScaleImage` y cambia el tamaño del control para `Custom*`.
- Corrección: el aviso de MSB3821 no se debía a un `Zone.Identifier` detectable ni a la ruta `K:`; el editor de recursos rechazaba el recurso Bitmap binario. El sample ahora usa un PNG copiado como contenido.
- La comparación full-window no es válida directamente: el cliente WinForms comparable es 508×374; el marco WinForms mide distinto al formulario VB6.
- Las 32 capturas `net8` contra sí mismas solo validan determinismo, no equivalencia VB6.
- El intento de enmarcar `Custom3DBorder` con un gradiente blanco/gris sobre la máscara mantuvo los píxeles distintos pero aumentó el delta; fue revertido.
