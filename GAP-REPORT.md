# Gap report

## Prioridad alta

1. **Identidad ActiveX/COM ausente.** El port no conserva CLSID, OCX, `StdPicture`, `PropertyBag` ni persistencia binaria (`LaVolpe-Buttons-Updated-/lvButtons.ctl:27`, `:2652-2762`; `LaVolpe.Buttons.Net/LaVolpeButton.cs:23-24`). Requiere una capa COM separada si es requisito de migración.
2. **Pintura no pixel-equivalente.** VB6 tiene ocho rutinas independientes con `FrameRgn`/GDI (`lvButtons.ctl:1966-2146`, `:2803-3386`); C# usa tres anillos comunes (`LaVolpeButton.cs:452-641`). Ya existe un comparador PNG (`LaVolpe.Buttons.Tests/Program.cs`) y la referencia se observa ejecutando el proyecto desde el IDE VB6; falta capturar los PNG VB6 con el mismo encuadre. El EXE independiente sigue fallando en este equipo por `Reasignación no válida del archivo DLL del sistema`.
3. **Timer diferente.** VB6 usa callback global `lv_TimerCallBack` (`modLvTimer.bas:11-40`); C# usa un `Timer` WinForms por control (`LaVolpeButton.cs:64`, `:289`, `:363-364`).
4. **DPI/fuentes sin comparación reproducible.** No hay ejecución 96/120/144/192 ni baseline visual del OCX; queda no verificable.

## Prioridad media

- `CustomFlat`/`Custom3DBorder` calcula máscara desde imagen, pero el port estira la imagen al control; VB6 normaliza/redimensiona (`lvButtons.ctl:1548-1656`, `LaVolpeButton.cs:187-216`, `:851-889`).
- OLE drag/drop mantiene eventos nominales, pero los argumentos son WinForms (`LaVolpeButton.cs:367`, `:385-394`).
- `Default`/`Cancel` implementan `IButtonControl`, pero no conectan automáticamente `Form.AcceptButton` y `Form.CancelButton` (`LaVolpeButton.cs:103-106`, `:302`).
- `Refresh` se expone como `RedrawButton`, y `ResetDefaultColors` como `ResetDefaultColorsNow`; son cambios de API fuente (`LaVolpeButton.cs:115`, `:288`).
- Validaciones exhaustivas de enums y errores de setters VB6 no tienen una matriz de excepciones equivalente (`lvButtons.ctl:353-915`).
- El sample WinForms ya reproduce los arrays de controles, posiciones, combos, checks, radios y acciones principales de `frmSample.frm` (`LaVolpe-Buttons-Updated-/frmSample.frm`, `LaVolpe.Buttons.Sample/SampleForm.cs`). La comparación pixel a pixel completa sigue pendiente porque VB6 y WinForms no usan el mismo motor GDI/WinForms y las imágenes de referencia se han extraído de la captura disponible.

## Infraestructura

`dotnet build LaVolpe.Buttons.Net.sln` falla con código 1 en el SDK 8.0.427 aunque cada proyecto compila individualmente. El diagnóstico muestra `MSB4276` para resolvers de workload y una solución metaproyecto que no ejecuta los tres proyectos. Debe repararse/recrearse la solución o validarse en Visual Studio antes de usar el build de solución como gate CI.

Todos los gaps anteriores aparecen como filas `parcial`, `diferente`, `ausente` o `no verificable` en `COMPATIBILITY-MATRIX.md`.
