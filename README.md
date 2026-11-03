# LaVolpe Buttons .NET

Port de `lvButtons_H` de VB6/ActiveX a un control personalizado **C# .NET 8 WinForms**.

> El proyecto busca compatibilidad funcional y visual con el control original, pero no pretende ser un reemplazo binario del OCX ni afirma paridad del 100 %.

## Contenido

- `LaVolpe.Buttons.Net/` — biblioteca WinForms.
- `LaVolpe.Buttons.Sample/` — aplicación de ejemplo.
- `LaVolpe.Buttons.Net.sln` — solución .NET.
- `COMPATIBILIDAD.md` — matriz de compatibilidad.
- `AUDITORIA_VB6_NET8.md` — auditoría del control original y del port.
- `PRUEBAS_COMPATIBILIDAD.md` — pruebas realizadas y pendientes.
- `EVIDENCIA_Y_DIFERENCIAS.md` — diferencias conocidas y limitaciones.

## Requisitos

- Windows.
- .NET 8 SDK con soporte `net8.0-windows`.
- Visual Studio 2022 o `dotnet` CLI.

El repositorio incluye `global.json` para seleccionar el SDK .NET 8.0.427 cuando está instalado.

## Compilar

```powershell
dotnet build .\LaVolpe.Buttons.Net.sln --configuration Release
```

## Ejecutar el ejemplo

```powershell
dotnet run --project .\LaVolpe.Buttons.Sample --configuration Release
```

El ejemplo utiliza los recursos PNG extraídos del formulario FRX original.

## Publicar

```powershell
dotnet publish .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj `
  --configuration Release `
  --output .\artifacts\publish
```

## Características implementadas

- Caption, Value, Checked, Selected y modos CommandButton/CheckBox/OptionButton.
- Formas rectangulares, diagonales y redondeadas.
- Colores, gradientes, estilos, imágenes y alineación.
- Hover, estados pulsado/deshabilitado y foco.
- Teclado, `Default`, `Cancel` y navegación entre controles.
- Timer administrado de 50 ms para `MouseOnButton`.
- Drag-and-drop WinForms con eventos OLE adaptados.
- Propiedades de compatibilidad como `ToolTipText`, `MouseIcon`, `Index`, `hDC` y `hwnd`.

## Limitaciones

El control no conserva compatibilidad COM/ActiveX, CLSID, PropertyBag ni registro OCX. Las firmas OLE son adaptaciones WinForms, las máscaras bitmap personalizadas y la equivalencia exacta del timer Win32 requieren trabajo adicional. Consulte la documentación de auditoría antes de usarlo como sustituto directo.

## Licencia

MIT. Consulte [LICENSE](LICENSE).
