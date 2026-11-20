# Paleta visual propia

`LaVolpeButton.Palette` es la arquitectura visual propia del control. Se inspira en la separación de estados habitual en controles WinForms, pero no contiene código, imágenes ni recursos de Krypton Toolkit.

## Estados

El renderer resuelve los estados en este orden:

1. `Disabled`
2. `Checked` para `CheckBox`/`OptionButton` marcados
3. `Pressed`
4. `Hover`
5. `Normal`

Los colores de texto tienen sus variantes `Text`, `HoverText` y `DisabledText`. `FocusColor` controla el anillo de foco cuando `ShowFocusRect` está activo; las formas custom mantienen la regla visual VB6 y no dibujan ese anillo.

## Uso

La paleta se edita desde el PropertyGrid y solo se serializa cuando contiene valores configurados:

```csharp
button.Palette.NormalBackColor = Color.FromArgb(32, 48, 64);
button.Palette.HoverBackColor = Color.FromArgb(48, 72, 96);
button.Palette.PressedBackColor = Color.FromArgb(24, 36, 48);
button.Palette.CheckedBackColor = Color.FromArgb(64, 96, 128);
button.Palette.DisabledBackColor = Color.FromArgb(96, 96, 96);
button.Palette.FocusColor = Color.Gold;
```

`Color.Empty` significa “usar el renderer VB6 existente”. `ResetPalette()` devuelve todos los estados a `Empty`, conserva la compatibilidad visual por defecto y elimina la serialización personalizada.

La cobertura del harness verifica los colores por estado, `ResetPalette`, `ShouldSerializePalette`, la presentación `(predeterminada)/(personalizada)` y la compatibilidad con `ResetDefaultColors`. La paleta no modifica eventos OLE, drag-and-drop ni la lógica de `Value`.

## Evidencia visual

Para generar una captura por estado:

```powershell
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Debug --no-build -- --palette-snapshot .\artifacts\visual\palette
```

El comando produce `palette-normal.png`, `palette-hover.png`, `palette-pressed.png`, `palette-checked.png`, `palette-disabled.png` y `palette-focus.png`.
