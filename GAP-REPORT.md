# Gap report

## Prioridad alta

1. **Identidad ActiveX/COM ausente.** El port no conserva CLSID, OCX, `StdPicture`, `PropertyBag` ni persistencia binaria (`LaVolpe-Buttons-Updated-/lvButtons.ctl:27`, `:2652-2762`; `LaVolpe.Buttons.Net/LaVolpeButton.cs:23-24`). Requiere una capa COM separada si es requisito de migración.
2. **Pintura no pixel-equivalente.** VB6 tiene ocho rutinas independientes con `FrameRgn`/GDI (`lvButtons.ctl:1966-2146`, `:2803-3386`); C# usa tres anillos comunes (`LaVolpe.Buttons.Net/LaVolpeButton.cs:452-641`). El comparador PNG está en `LaVolpe.Buttons.Tests/Program.cs:593-633` y el sample genera una captura cliente reproducible desde `LaVolpe.Buttons.Sample/Program.cs:18-92`. Se corrigió la altura de `Yes?` según 780 twips, los píxeles de texto que contaminaban los recortes de laptop/disquete, el recorte interno de los iconos, el extremo XP del degradado, el sombreado XP y `DrawGradient` por franjas de 1 píxel, la geometría `polyPts(10..15)` del borde diagonal exterior, el borde de las diagonales con rasterización no suavizada dentro del área cliente, el marco a `BorderColor = RGB(0,0,192)`, el botón redondo, el color de los textos Win XP/Java, la tolerancia de máscara `NormalizeImage` a ±3 y la medición/dibujo de texto con `TextFormatFlags.NoPadding`; con el mismo tamaño (508×374), la diferencia medida es 36,01 % y el delta de canales 7.193.961. El issue #2 sigue abierto. El EXE independiente sigue fallando en este equipo por `Reasignación no válida del archivo DLL del sistema`.
3. **Timer diferente.** VB6 usa callback global `lv_TimerCallBack` (`modLvTimer.bas:11-40`); C# usa un `Timer` WinForms por control (`LaVolpeButton.cs:64`, `:289`, `:363-364`).
4. **DPI/fuentes sin comparación del monitor real.** El control reconstruye región y repinta tras `OnDpiChangedAfterParent` (`LaVolpe.Buttons.Net/LaVolpeButton.cs:372`) y el harness cubre geometrías lógicas 96/120/144/192 (`DPI-REPORT.md`), pero todavía no existe una baseline visual del OCX VB6 ejecutado en cada DPI; el issue #3 sigue abierto.

## Prioridad media

- `CustomFlat`/`Custom3DBorder` calcula máscara desde imagen con la tolerancia VB6 de ±3, pero el port todavía estira la imagen al control y no replica toda la normalización/redimensión (`lvButtons.ctl:1548-1656`, `LaVolpeButton.cs:187-216`, `:259`, `:1030-1061`).
- OLE drag/drop mantiene eventos, publica `OLEDragOver` desde `DragEnter` y ahora restaura cursor/completa el ciclo incluso si `DoDragDrop` falla (`LaVolpe.Buttons.Net/LaVolpeButton.cs:420-448`, `:492`); las firmas siguen siendo WinForms y la prueba con datos reales desde otra aplicación queda pendiente en [OLE-MAPPING.md](OLE-MAPPING.md).
- `Default`/`Cancel` implementan `IButtonControl` y conectan automáticamente `Form.AcceptButton`/`Form.CancelButton` (`LaVolpe.Buttons.Net/LaVolpeButton.cs:154-155`, `:356-371`); el enlace está cubierto por prueba.
- `Refresh` se expone como `RedrawButton`, y `ResetDefaultColors` como `ResetDefaultColorsNow`; son cambios de API fuente (`LaVolpeButton.cs:115`, `:288`).
- Validaciones exhaustivas de enums y errores de setters VB6 no tienen una matriz de excepciones equivalente (`lvButtons.ctl:353-915`).
- El sample WinForms ya reproduce los arrays de controles, posiciones, combos, checks, radios y acciones principales de `frmSample.frm` (`LaVolpe-Buttons-Updated-/frmSample.frm`, `LaVolpe.Buttons.Sample/SampleForm.cs`). La comparación objetiva está habilitada, pero el resultado actual no es pixel-equivalente; quedan diferencias de tipografía, bordes, imágenes y composición que deben resolverse en el issue #2.

## Infraestructura

`dotnet build LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1` se ejecuta correctamente en el SDK 8.0.427 con 0 warnings y 0 errores. La variante paralela sin `-m:1` puede fallar de forma intermitente con `MSB5021` en este entorno, por lo que el gate reproducible usa concurrencia 1.

Todos los gaps anteriores aparecen como filas `parcial`, `diferente`, `ausente` o `no verificable` en `COMPATIBILITY-MATRIX.md`.
