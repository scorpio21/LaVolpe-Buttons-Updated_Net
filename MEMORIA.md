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
- ✅ `ShadeColor` reproduce las conversiones VB6 de los canales verde y azul y `BuildImageRegion` usa `NearestNeighbor`; `FillConfiguredGradient` y el borde `Round3D` reproducen la secuencia de `LoadGradientColors` con clamp VB6; la métrica visual actual es 10,24 %.
- ✅ `ButtonShape=Custom*` sin `Picture` se rechaza, `Picture=null` devuelve a rectangular y `PictureSize` fijo se rechaza en custom.
- ✅ `TimerUpdate(long)` y los hooks auxiliares OLE dejaron de ser API pública; la cobertura interna permanece.
- ✅ DPI lógico 96/120/144/192 y hook `OnDpiChangedAfterParent` cubiertos; comparación física VB6 pendiente.
- ⚠️ Pintado no es pixel-equivalente; issue visual #2 permanece abierto y el gap técnico está medido.
- ✅ La atribución de procedencia se añadió a `LICENSE`, `README.md`, `NOTICE.md` y al paquete NuGet; issue #17 sigue abierto porque los términos de redistribución del original no están verificados.
- ⚠️ No cerrar issues de GitHub sin evidencia; los issues abiertos se mantienen abiertos cuando falta una validación externa o legal.
- ✅ `dotnet test` descubre y ejecuta los 56 casos mediante `LaVolpe.Buttons.XunitTests`; `dotnet run` conserva el harness STA y las herramientas visuales.

## Work history

- No hay commits creados en esta sesión; todas las modificaciones están sin commit en el árbol de trabajo.

## Next steps

1. Repetir build, harness y captura tras cualquier cambio.
2. Revisar diferencias visuales restantes con métricas, especialmente anillos custom y tipografía, sin cambios especulativos.
3. Validar diseñador (guardar/cerrar/reabrir) y DPI físico 120/144 si el entorno lo permite.
4. Mantener #6 reservado para el final; solo después de resolver los issues anteriores repetir la prueba OLE externa real con `DesignerSample`.
5. Resolver los términos legales originales antes de publicar NuGet; la atribución ya está integrada y verificada dentro del `.nupkg`.

## Known gotchas

- `LaVolpe-Buttons-Updated-\lvButtons.ctl:1350-1380` confirma que VB6 llama `ScaleImage` y cambia el tamaño del control para `Custom*`.
- La comparación full-window no es válida directamente: el cliente WinForms comparable es 508×374; el marco WinForms mide distinto al formulario VB6.
- Las 32 capturas `net8` contra sí mismas solo validan determinismo, no equivalencia VB6.
- El intento de enmarcar `Custom3DBorder` con un gradiente blanco/gris sobre la máscara mantuvo los píxeles distintos pero aumentó el delta; fue revertido.
