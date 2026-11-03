# Plan y estado de pruebas de compatibilidad

## Pruebas ejecutables actuales

| Caso | Resultado | Evidencia |
|---|---|---|
| Compilar biblioteca Release | PASS | `dotnet build LaVolpe.Buttons.Net.csproj --configuration Release` |
| Compilar ejemplo Release | PASS | 0 errores, 0 advertencias |
| Copiar tres recursos FRX al output | PASS | `bin/Release/net8.0-windows/Resources/*.png` presentes |
| Cargar imágenes sin mantener bloqueo del archivo | PASS estático | `SampleForm.LoadImage` clona con `new Bitmap(source)` |
| Publicar distribución limpia | PASS | `dotnet publish --configuration Release --output artifacts/publish` |
| DLL, EXE, runtimeconfig y tres PNG en publicación | PASS | `artifacts/publish` contiene todos los archivos esperados |
| Liberar imágenes del ejemplo | PASS estático | `FormClosed` dispone imágenes asignadas |
| `PerformClick()` deshabilitado | PASS estático | método comprueba `Enabled` |
| Exclusividad OptionButton | PASS estático | `OnClick` deselecciona hermanos del mismo contenedor |
| CheckBox alterna Value | PASS estático | `OnClick` llama `SetValue(!_value)` |
| Timer MouseOnButton | PASS compilación | timer WinForms de 50 ms y `GraphicsPath.IsVisible` |

## Casos todavía requieren ejecución manual/automatizada

- Comparar capturas VB6/.NET en los ocho estilos.
- Comparar regiones diagonales, redondeadas y máscaras de imagen.
- Probar DPI 96/120/144/192.
- Probar Enter, Escape, Space y flechas dentro de un formulario real.
- Probar `Default` y `Cancel` con un formulario que tenga varios controles.
- Probar DragDrop externo y efectos Copy/Move/Link.
- Probar `MouseIcon` con alpha y hotspot.
- Abrir el DLL desde Visual Studio Toolbox, modificar propiedades y reabrir el diseñador.
- Verificar serialización de imágenes y enums en `.Designer.cs`.
- Ejecutar instalación limpia con únicamente .NET 8 Desktop Runtime.

## Criterio de afirmación

Los casos estáticos o de compilación no prueban equivalencia visual ni runtime. La matriz no debe superar el 100 % hasta completar los casos manuales y comparar capturas bajo el mismo tamaño, fuente, DPI y estado.
