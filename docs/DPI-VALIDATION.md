# Validación física de DPI

Esta validación comprueba el control WinForms en monitores físicos con escalado de Windows equivalente a 96, 120, 144 y 192 DPI.

## Procedimiento

1. En Windows, abrir **Configuración > Sistema > Pantalla > Escala**.
2. Ejecutar el diagnóstico con el proyecto compilado. Usa una carpeta distinta por escala para no sobrescribir la evidencia:

   ```powershell
   dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj -c Debug --no-build -- --physical-dpi-report .\artifacts\dpi\issue-3\100
   ```

3. Repetir con carpetas `125`, `150` y `200` para los escalados **125 % (120 DPI)**, **150 % (144 DPI)** y **200 % (192 DPI)**. Si hay varios monitores, repetir en cada pantalla.
4. Guardar cada `physical-dpi-report.md` y sus capturas PNG en `artifacts\dpi\issue-3\<escala>`.

## Criterios

- El informe debe indicar el DPI efectivo esperado en cada pantalla.
- `Result` debe ser `OK` en todas las filas.
- `Button DPI` debe coincidir con `Form DPI`.
- `Button size` y `DPI-scaled expected` se registran como evidencia; el tamaño explícito asignado por código no se modifica automáticamente por este diagnóstico.
- La región, el texto y la imagen deben quedar dentro del cliente del botón.

La validación lógica 96/120/144/192 permanece en el harness normal. Este procedimiento es la evidencia física necesaria antes de cerrar el issue #3.
