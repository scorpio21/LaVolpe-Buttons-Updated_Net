# Matriz VB6 → C# .NET 8

Estados: `equivalente`, `parcial`, `diferente`, `ausente`, `no verificable`.

| Área | VB6 (evidencia) | C# (evidencia) | Estado | Nota |
|---|---|---|---|---|
| Enums de imagen/tamaño | `lvButtons.ctl:272-286` | `LaVolpeButton.cs:8-9` | equivalente | Valores y alias principales portados |
| Mode | `:287-291`, `:374-394` | `:10`, `:71` | equivalente | Command/Check/Option |
| ButtonStyle | `:343-371` | `:17`, `:71` | parcial | Ocho estilos, geometría y paleta no idénticas |
| Caption/Text | `:396-423` | `:65-68`, `:851-908` | equivalente | `||` se traduce a salto de línea |
| CaptionAlign | `:425-448` | `:86`, `:738-850` | parcial | WinForms mide texto distinto a DrawText |
| CaptionStyle | `:449-464` | `:15`, `:714-737`, `:890-908` | parcial | Sombra administrada, no GDI exacto |
| CustomClick | `:466-477`, `:2220-2290` | `:13`, `:661-661`, `:707-713` | equivalente | Offset cubierto |
| ButtonShape | `:479-507`, `:1341-1546` | `:11`, `:124-216`, `:678-703` | parcial | Custom estira imagen en vez de redimensionar control |
| Picture | `:509-534` | `:92`, `:851-889` | parcial | `Image`, no `StdPicture`/COM |
| PictureAlign | `:536-550` | `:8`, `:738-850` | parcial | Casos extremos pendientes |
| PictureSize | `:626-646`, `:1548-1656` | `:9`, `:738-850` | parcial | Interpolación no demostrada idéntica |
| Enabled | `:552-575` | `:303`, `:414-423`, `:714-737` | equivalente | Estados básicos verificados |
| ShowFocusRect | `:577-601` | `:643-660` | parcial | Focus ring por estilo adaptado |
| Value | `:603-624` | `:73-75`, `:279-286`, `:372-384` | equivalente | Exclusividad y eventos probados |
| MousePointer | `:648-658` | `:18`, `:94`, `:228-252` | equivalente | 15 valores + aliases |
| MouseIcon | `:660-677` | `:95`, `:217-227`, `:259-278` | parcial | Hotspot centrado; no es StdPicture |
| Font | `:679-694` | `:90`, `:64`, `:851-853` | parcial | Tipo y métricas distintas |
| FontStyle | `:696-717` | `:16`, `:852-853` | equivalente | Flags principales |
| ForeColor/BackColor | `:719-758` | `:76-80` | parcial | OLE_COLOR ≠ Color |
| GradientColor/Mode | `:760-803` | `:81-82`, `:704`, `:550-627` | parcial | El port reproduce el sombreado XP y `DrawGradient` por franjas; la paridad global de todos los estilos sigue pendiente |
| ResetDefaultColors | `:805-827` | `:115` | parcial | API renombrada (`ResetDefaultColorsNow`) |
| HoverColorLocks | `:829-867` | `:12`, `:88`, `:714-737` | parcial | Combinaciones portadas, validación visual pendiente |
| Hover colors | `:869-915` | `:79-80`, `:431-443` | parcial | Herencia/adaptación WinForms |
| hDC/hwnd | `:917-930` | `:118-123`, `:346-362` | diferente | Ciclo de vida HDC/Handle WinForms |
| Refresh | `:932-937` | `:288` (`RedrawButton`) | diferente | Nombre/API adaptados |
| DelayDrawing | `:939-953` | `:99-102`, `:297-298` | equivalente | Incluye bool y Begin/End |
| Mouse events | `:2479-2561` | `:365-368` | parcial | Tipos/coordenadas WinForms |
| Keyboard events | `:2397-2477` | `:320-344`, `:369-371` | parcial | KeyAscii y navegación no son idénticos |
| MouseOnButton/timer | `modLvTimer.bas:11-40` | `LaVolpeButton.cs:289`, `:363-364` | diferente | Timer por control vs callback global |
| Click/DoubleClick | `lvButtons.ctl:2563-2598` | `LaVolpeButton.cs:368`, `:372-384` | parcial | Orden/firma adaptados |
| OLE drag events | `lvButtons.ctl:2613-2641` | `LaVolpeButton.cs:367`, `:385-394`, `:484-507` | parcial | `DragEnter` acepta texto con `Copy` por defecto sin manejador y respeta un rechazo explícito; `DataObject` y firmas siguen sin ser los tipos COM de VB6 |
| DefaultCancel | `lvButtons.ctl:2-23` | `LaVolpeButton.cs:154-155`, `:356-371` | equivalente | Enlaza automáticamente `AcceptButton`/`CancelButton`; probado |
| Focus/selection | `lvButtons.ctl:2600-2611` | `LaVolpeButton.cs:313-319`, `:279-286` | equivalente | Selección y foco probados |
| Disabled rendering | `lvButtons.ctl:1966-2146`, `:2938-3035` | `LaVolpeButton.cs:414-477`, `:714-737` | parcial | Sombreado aproximado |
| Eight style renderers | `lvButtons.ctl:2803-3386` | `LaVolpeButton.cs:508-641` | diferente | Un renderer común, tres anillos aproximados |
| Custom image mask | `lvButtons.ctl:1548-1656` | `LaVolpeButton.cs:187-216`, `:259` | parcial | Máscara LockBits con tolerancia VB6 ±3; geometría y escalado aún diferentes |
| Resize/region | `lvButtons.ctl:2764-2781` | `LaVolpeButton.cs:124-216`, `:370-373` | parcial | La región se reconstruye en resize y cambio DPI; falta validar el monitor real |
| DPI | `lvButtons.ctl:2-23` | `LaVolpeButton.cs:64`, `:738-850`, `:372` | no verificable | La matriz lógica 96/120/144/192 es correcta, pero no hay ejecución comparativa del OCX en monitores reales 96/120/144/192 |
| Fonts visuales | `lvButtons.ctl:2-23` | `LaVolpeButton.cs:64`, `:851-908` | diferente | Times New Roman VB6 vs fuente heredada |
| Error: null caption | VB6 `:396-423` | `LaVolpeButton.cs:68` | equivalente | Normalización probada |
| Error: released image | VB6 `:509-534` | `LaVolpeButton.cs:851-889` | equivalente | Se omite imagen ante excepción; probado |
| Error: invalid enum | VB6 setters `:353-915` | `LaVolpeButton.cs:119-144` | equivalente | Los setters ignoran valores fuera del rango, cubierto por prueba |
| ActiveX/COM identity | `lvButton_H.vbp`, `lvButtons.ctl:27` | `LaVolpe.Buttons.Net.csproj` | ausente | No CLSID/OCX/PropertyBag |
| Designer/persistence | `lvButtons.ctl:2652-2762` | `LaVolpeButton.cs:23-24`, csproj | parcial | Metadatos WinForms; no persistencia VB6 |
| Sample controls | `frmSample.frm:810-996` | `SampleForm.cs:1-116` | parcial | No arrays/diseñador VB6 |
| Visual pixel parity | `lvButtons.ctl:2803-3386` | `lvButtons.ctl:1966-2004`, `lvButtons.ctl:2107-2144`, `lvButtons.ctl:2292-2370`, `LaVolpe.Buttons.Sample/Program.cs:18-92`, `LaVolpe.Buttons.Sample/SampleForm.cs:67-70`, `LaVolpe.Buttons.Sample/SampleForm.cs:138`, `LaVolpe.Buttons.Sample/SampleForm.cs:295-320`, `LaVolpe.Buttons.Net/LaVolpeButton.cs:495-680`, `LaVolpe.Buttons.Tests/Program.cs:593-633` | parcial | Comparación cliente reproducible 508×374: 55.802/189.992 píxeles distintos (29,37%); delta 5.820.722; no hay paridad pixel a pixel |

Conteo: 14 equivalentes, 24 parciales, 6 diferentes, 1 ausente, 1 no verificable; 45/46 filas revisadas.
