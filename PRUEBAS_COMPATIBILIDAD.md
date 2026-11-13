# Plan y estado de pruebas de compatibilidad

**Última ejecución:** 2026-11-13
**Harness STA:** **`LaVolpe.Buttons.Tests`** (ejecutable propio, hilo STA y formularios reales).
**Runner:** **`LaVolpe.Buttons.XunitTests`** (xUnit v3/Microsoft Testing Platform, 56 casos).

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests --configuration Release
```

Resultado actual: **56 correctas, 0 fallidas**, código de salida `0`.

```powershell
dotnet test .\LaVolpe.Buttons.XunitTests\LaVolpe.Buttons.XunitTests.csproj --configuration Release
```

El runner devuelve **56 correctas, 0 fallidas** y mantiene el comparador visual como herramienta
separada del conjunto de pruebas.

## Pruebas automatizadas (2026-11-12)

Cada caso crea un `Form` real, agrega el control, ejecuta la comprobación y se cierra. `Region` se
verifica con `Region.IsVisible` porque `DrawToBitmap` no respeta la región de ventana.

| # | Caso | Resultado |
|---:|---|---|
| 1 | `Caption` → `Text` y `Text` → `Caption` | PASS |
| 2 | `Caption = null` se normaliza a cadena vacía | PASS |
| 3 | Defaults VB6: `ButtonStyle = Windows95`, `HoverColorLocks = TextAndBackColor`, hover = `BackColor` | PASS |
| 4 | `BackColor` hereda del contenedor | PASS |
| 5 | `Value = true` se rechaza en `CommandButton` | PASS |
| 6 | `PerformClick` dispara `Click` y asigna `Form.DialogResult` | PASS |
| 7 | `PerformClick` respeta `Enabled = false` | PASS |
| 8 | Dos OptionButton: el segundo deselecciona al primero | PASS |
| 9 | Clic sobre la opción ya elegida no dispara `Click` | PASS |
| 10 | `GotFocus` selecciona el OptionButton | PASS |
| 11 | CheckBox alterna `Value` en cada clic | PASS |
| 12 | `IButtonControl.DialogResult` y `NotifyDefault` | PASS |
| 13 | `Region` en `LeftDiagonal`, `RightDiagonal`, `FullDiagonal`, `Round3D`, `Round3DFixed`, `RoundFlat` | PASS |
| 14 | Rectangular XP: la esquina `(0,0)` queda fuera de la región | PASS |
| 15 | `CustomFlat` fuerza `FillScaleUpDown` y genera `Region` desde la máscara | PASS |
| 16 | Una imagen liberada no derriba el control al recalcular la región o pintar | PASS |
| 17 | `MouseIcon` crea el cursor y lo restaura con `null` | PASS |
| 18 | `DelayDrawing(bool)` acepta cambios y repinta | PASS |
| 19 | `hDC` válido con `BITSPIXEL = 32` | PASS |
| 20 | `MousePointerConstants` asigna `Arrow`, `Crosshair`, `Hourglass`, `SizeWE` y `Default` | PASS |
| 21 | `MouseIcon` implica `MousePointer = Custom` y aplica el cursor | PASS |
| 22 | `DragIcon` crea y libera su propio cursor/HICON | PASS |
| 23 | CheckBox/OptionButton marcan toda la superficie y usan `cCheckBox` | PASS |
| 24 | Los 8 estilos se pintan en estado normal y marcado sin excepciones | PASS |
| 25 | `CustomFlat` marcado pinta el caption en lugar de la imagen | PASS |

Salida esperada:

```text
LaVolpe.Buttons.Tests - pruebas de compatibilidad
----------------------------------------------------------------------
PASS  Text y Caption sincronizados
...
PASS  CheckBox/OptionButton marcan toda la superficie
----------------------------------------------------------------------
56 correctas, 0 fallidas
```

## Pruebas manuales anteriores (2026-11-05, ronda 1)

Sonda `probe3` sobre un formulario con los 7 `ButtonShape`, un `Flat` dorado y un `XP` rojo,
con captura de pantalla real y comprobaciones sobre `Region`:

| Caso | Resultado | Evidencia |
|---|---|---|
| `Region` en formas no rectangulares | PASS | las 6 formas → `Region != null` |
| Recorte real de la diagonal izquierda | PASS | `Region.IsVisible(1, 58) == false`, `IsVisible(85, 30) == true` |
| Recorte real de la diagonal derecha y completa | PASS | `IsVisible(168, 58) == false` en ambas |
| Elipse en `Round3D` | PASS | `IsVisible(2, 2) == false`, `IsVisible(85, 30) == true` |
| `BackColor` aplicado en estilo XP | PASS | `BackColor = Red` → píxel `ARGB(255,255,48,0)`; antes `#ECF2FE` |
| `BackColor` aplicado en estilo Flat | PASS | `BackColor = Gold` → píxel `ARGB(255,255,215,0)` |
| Foco invalida el control | PASS | `Focus()` → 1 evento `Invalidated`; `Focused == true` |
| `hDC` válido tras repintar | PASS | `GetDeviceCaps(hDC, BITSPIXEL) == 32`; el handle no cambia |
| Gradientes en 4 direcciones | PASS | `L=248/R=5`, `L=8/R=250`, `T=224/B=22`, `T=31/B=233` |
| `PerformClick()` con `Enabled = false` | PASS | 0 clics |

La tabla anterior conserva los 25 casos de la ronda 3. La suite actual añade 20 comprobaciones
de PropertyGrid/serialización, clic físico Win32, DPI lógico 96/120/144/192, mnemónicos y
Accept/Cancel, aceptación/rechazo/entrega de texto OLE, sombreado y gradientes VB6, `PictureAlign`,
redimensionado proporcional de `Custom*`, validación de enums y estados hover/disabled. El detalle
ejecutable y su salida actual están en `TEST-REPORT.md`.

## Capturas revisadas

| Captura | Contenido | Resultado |
|---|---|---|
| `styles.png` | Matriz de los 8 `ButtonStyle` y un botón XP con foco | PASS visual: tres anillos, estados normal/marcado y degradado XP |
| `sample.png` | `SampleForm` completa fuera de pantalla | PASS visual: 8 estilos, 7 formas, 3 modos, gradiente, imagen y `CustomFlat` |

## Pruebas estáticas / de compilación

| Caso | Resultado | Evidencia |
|---|---|---|
| Compilar los cinco proyectos de la solución en Release | PASS | `dotnet build .\LaVolpe.Buttons.Net.sln -c Release --no-restore --maxcpucount:1` → 0 errores, 0 advertencias |
| Copiar los tres recursos FRX al output | PASS | `bin/Release/net8.0-windows/Resources/*.png` |
| Cargar imágenes sin bloquear el archivo | PASS estático | el ejemplo clona con `new Bitmap(source)` |
| Liberar imágenes del ejemplo | PASS estático | `FormClosed` dispone las imágenes |
| Publicar distribución limpia | PASS | `dotnet publish -c Release -o artifacts/publish` |
| Contenido de la publicación | PASS | DLL, EXE, `runtimeconfig`, PDB y tres PNG |
| Timer `MouseOnButton` | PASS compilación | `Timer` de 50 ms por control, activo solo en hover |

## Casos pendientes de ejecución

- Comparar capturas VB6/.NET de los ocho estilos (los anillos son cercanos, no idénticos).
- Comparación del OCX VB6 en monitores físicos a 96/120/144/192 DPI.
- Enter, Escape, Space y flechas en un formulario real con varios controles y mnemónicos.
- Movimiento real del puntero para `MouseOnButton` y los colores de hover.
- DragDrop externo y efectos Copy/Move/Link.
- Parcial: Visual Studio mostró `LaVolpeButton` en el Toolbox y `Form1.cs [Diseño]`; queda confirmar edición de propiedades, cierre/reapertura y persistencia.
- Serialización de imágenes en `.Designer.cs` y persistencia de recursos del diseñador.
- Instalación limpia con únicamente .NET 8 Desktop Runtime.

## Criterio de afirmación

Los casos estáticos o de compilación no prueban equivalencia visual ni de runtime. La matriz no debe
superar el 100 % hasta completar los casos pendientes y comparar capturas bajo el mismo tamaño,
fuente, DPI y estado. Las pruebas actuales cubren comportamiento y regiones, no igualdad píxel a
píxel con el OCX.
