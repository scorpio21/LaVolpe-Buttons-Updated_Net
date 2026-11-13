# Gap report

Actualización 2026-11-13: la comparación visual reproducible queda en 10,24 % de píxeles distintos y delta 5.207.098 tras corregir Java, la máscara de imágenes personalizadas, el redimensionado proporcional de `Custom*`, la conversión de los canales verde y azul de `ShadeColor`, la secuencia de `LoadGradientColors` para gradientes, borde `Round3D` y `Custom3DBorder`, y la interpolación de la máscara. El resultado no alcanza paridad pixel a pixel.

## Prioridad alta

1. **Identidad ActiveX/COM ausente.** El port no conserva CLSID, OCX, `StdPicture`, `PropertyBag` ni persistencia binaria (`LaVolpe-Buttons-Updated-/lvButtons.ctl:27`, `:2652-2762`; `LaVolpe.Buttons.Net/LaVolpeButton.cs:23-24`). Requiere una capa COM separada si es requisito de migración.
2. **Pintura no pixel-equivalente.** VB6 tiene ocho rutinas independientes con `FrameRgn`/GDI (`lvButtons.ctl:1966-2146`, `:2803-3386`); C# reproduce `DrawButtonBorder` con 14 segmentos GDI para estilos rectangulares y diagonales (`LaVolpe.Buttons.Net/LaVolpeButton.cs:550-680`) y usa rasterización sin antialias para aproximar las regiones GDI. El comparador reproducible mide 19.450/189.992 píxeles distintos (10,24 %) en el mismo tamaño; el issue #2 permanece abierto y la diferencia técnica sigue documentada. El EXE independiente sigue fallando en este equipo por `Reasignación no válida del archivo DLL del sistema`.
3. **Timer diferente.** VB6 usa callback global `lv_TimerCallBack` (`modLvTimer.bas:11-40`); C# usa un `Timer` WinForms por control (`LaVolpeButton.cs:64`, `:289`, `:363-364`).
4. **DPI/fuentes sin comparación del monitor real.** El control reconstruye región y repinta tras `OnDpiChangedAfterParent` (`LaVolpe.Buttons.Net/LaVolpeButton.cs:409`) y el harness cubre geometrías lógicas 96/120/144/192 (`DPI-REPORT.md`), pero todavía no existe una baseline visual del OCX VB6 ejecutado en cada DPI; el issue #3 permanece abierto y la validación técnica sigue pendiente.

## Prioridad media

- `CustomFlat`/`Custom3DBorder` calcula máscara desde imagen con la tolerancia VB6 de ±3 y ahora redimensiona proporcionalmente el control según `ScaleImage`; la normalización GDI completa todavía no se replica (`lvButtons.ctl:1548-1656`, `LaVolpeButton.cs:187-216`, `:174-197`, `:1030-1061`).
- OLE drag/drop mantiene eventos, publica `OLEDragOver` desde `DragEnter`, acepta texto con `Copy` aunque no haya manejador y restaura cursor/completa el ciclo incluso si `DoDragDrop` falla (`LaVolpe.Buttons.Net/LaVolpeButton.cs:420-448`, `:484-510`); las firmas siguen siendo WinForms y la prueba con datos reales desde otra aplicación queda pendiente en [OLE-MAPPING.md](OLE-MAPPING.md).
- `Default`/`Cancel` implementan `IButtonControl` y conectan automáticamente `Form.AcceptButton`/`Form.CancelButton` (`LaVolpe.Buttons.Net/LaVolpeButton.cs:154-155`, `:356-371`); el enlace está cubierto por prueba.
- `Refresh` conserva el método público heredado de `Control` y `ResetDefaultColors` conserva la propiedad pública VB6; ambos tienen regresiones automatizadas.
- Las validaciones de enums principales ya ignoran valores fuera de rango como VB6 (`lvButtons.ctl:353-915`, `LaVolpeButton.cs:119-144`); quedan pendientes otras diferencias de errores y mensajes de setters.
- El sample WinForms ya reproduce los arrays de controles, posiciones, combos, checks, radios y acciones principales de `frmSample.frm` (`LaVolpe-Buttons-Updated-/frmSample.frm`, `LaVolpe.Buttons.Sample/SampleForm.cs`). La comparación objetiva está habilitada, pero el resultado actual no es pixel-equivalente; quedan diferencias de tipografía, bordes, imágenes y composición mientras el issue #2 permanece abierto.

## Infraestructura

`dotnet build LaVolpe.Buttons.Net.sln -c Release --no-restore -m:1` se ejecuta correctamente en el SDK 8.0.427 con 0 warnings y 0 errores. La variante paralela sin `-m:1` puede fallar de forma intermitente con `MSB5021` en este entorno, por lo que el gate reproducible usa concurrencia 1.

Todos los gaps anteriores aparecen como filas `parcial`, `diferente`, `ausente` o `no verificable` en `COMPATIBILITY-MATRIX.md`.
