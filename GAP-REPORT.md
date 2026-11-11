# Gap report

## Prioridad alta

1. **Identidad ActiveX/COM ausente.** El port no conserva CLSID, OCX, `StdPicture`, `PropertyBag` ni persistencia binaria (`LaVolpe-Buttons-Updated-/lvButtons.ctl:27`, `:2652-2762`; `LaVolpe.Buttons.Net/LaVolpeButton.cs:23-24`). Requiere una capa COM separada si es requisito de migración.
2. **Pintura no pixel-equivalente.** VB6 tiene ocho rutinas independientes con `FrameRgn`/GDI (`lvButtons.ctl:1966-2146`, `:2803-3386`); C# usa tres anillos comunes (`LaVolpe.Buttons.Net/LaVolpeButton.cs:452-641`). El comparador PNG está en `LaVolpe.Buttons.Tests/Program.cs:484-526` y el sample genera una captura cliente reproducible desde `LaVolpe.Buttons.Sample/Program.cs:18-92`. Se corrigieron los píxeles de texto que contaminaban los recortes de laptop/disquete, se reforzó el borde de las diagonales, se alinearon el marco azul, el botón redondo y el color de los textos Win XP/Java; con el mismo tamaño (508×374), la diferencia medida es 37,84 % y el delta de canales 8.146.192. El issue #2 sigue abierto. El EXE independiente sigue fallando en este equipo por `Reasignación no válida del archivo DLL del sistema`.
3. **Timer diferente.** VB6 usa callback global `lv_TimerCallBack` (`modLvTimer.bas:11-40`); C# usa un `Timer` WinForms por control (`LaVolpeButton.cs:64`, `:289`, `:363-364`).
4. **DPI/fuentes sin comparación reproducible.** No hay ejecución 96/120/144/192 ni baseline visual del OCX; queda no verificable.

## Prioridad media

- `CustomFlat`/`Custom3DBorder` calcula máscara desde imagen, pero el port estira la imagen al control; VB6 normaliza/redimensiona (`lvButtons.ctl:1548-1656`, `LaVolpeButton.cs:187-216`, `:851-889`).
- OLE drag/drop mantiene eventos nominales, pero los argumentos son WinForms (`LaVolpeButton.cs:367`, `:385-394`).
- `Default`/`Cancel` implementan `IButtonControl`, pero no conectan automáticamente `Form.AcceptButton` y `Form.CancelButton` (`LaVolpeButton.cs:103-106`, `:302`).
- `Refresh` se expone como `RedrawButton`, y `ResetDefaultColors` como `ResetDefaultColorsNow`; son cambios de API fuente (`LaVolpeButton.cs:115`, `:288`).
- Validaciones exhaustivas de enums y errores de setters VB6 no tienen una matriz de excepciones equivalente (`lvButtons.ctl:353-915`).
- El sample WinForms ya reproduce los arrays de controles, posiciones, combos, checks, radios y acciones principales de `frmSample.frm` (`LaVolpe-Buttons-Updated-/frmSample.frm`, `LaVolpe.Buttons.Sample/SampleForm.cs`). La comparación objetiva está habilitada, pero el resultado actual no es pixel-equivalente; quedan diferencias de tipografía, bordes, imágenes y composición que deben resolverse en el issue #2.

## Infraestructura

`dotnet build LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1` se ejecuta correctamente en el SDK 8.0.427 con 0 warnings y 0 errores. La variante paralela sin `-m:1` puede fallar de forma intermitente con `MSB5021` en este entorno, por lo que el gate reproducible usa concurrencia 1.

Todos los gaps anteriores aparecen como filas `parcial`, `diferente`, `ausente` o `no verificable` en `COMPATIBILITY-MATRIX.md`.
