# LaVolpe Buttons para .NET WinForms

Port del control VB6 `lvButtons_H` a C# y .NET 8 para Windows Forms.

## Uso en una solución existente

1. Agrega `LaVolpe.Buttons.Net.csproj` a la solución.
2. Agrega una referencia de proyecto desde tu aplicación.
3. Compila.
4. En el diseñador: Toolbox → Choose Items → Browse → selecciona el DLL generado.
5. Arrastra `LaVolpeButton` al formulario.

DLL de Release:

```text
LaVolpe.Buttons.Net/bin/Release/net8.0-windows/LaVolpe.Buttons.Net.dll
```

## Código

```csharp
var button = new LaVolpeButton
{
    Caption = "Aceptar",
    Size = new Size(140, 42),
    ButtonStyle = BackStyle.WindowsXp,
    ButtonShape = ButtonShape.Round3D,
    GradientMode = GradientMode.TopToBottom
};
button.Click += (_, _) => MessageBox.Show("Aceptar");
Controls.Add(button);
```

El control reemplaza el timer global de VB6 con eventos nativos de WinForms. `hWnd` se expone como `hwnd` y la obtención de HDC está disponible para escenarios de interoperabilidad, aunque el pintado normal usa `Graphics` administrado.

## Recursos extraídos

Los recursos recuperados de `frmSample.frx` se encuentran en `Resources/` y el ejemplo los copia al directorio de salida. El OCX original sigue siendo necesario si se requiere compatibilidad ActiveX/COM.

## Diseñador

1. Compila en Release.
2. Abre Toolbox → Choose Items → Browse.
3. Selecciona `LaVolpe.Buttons.Net.dll`.
4. Arrastra `LaVolpeButton` a un formulario WinForms.
5. Edita propiedades desde el panel Properties.

Las propiedades se serializan mediante los mecanismos estándar de `Control`, pero la persistencia `PropertyBag` del OCX VB6 no es compatible.

## Compilación reproducible

El repositorio incluye `global.json` para seleccionar el SDK 8.0.427 cuando está instalado. Si el SDK no está disponible, use un SDK 8 compatible. En este entorno, la compilación directa del proyecto de ejemplo valida ambos proyectos:

```powershell
$env:DOTNET_CLI_HOME = (Get-Location).Path
dotnet build .\LaVolpe.Buttons.Sample\LaVolpe.Buttons.Sample.csproj --configuration Release
```

## Limitaciones conocidas

Este proyecto es un port administrado, no una conversión binaria del OCX. La auditoría completa está en `AUDITORIA_VB6_NET8.md` y la matriz en `COMPATIBILIDAD.md`.
