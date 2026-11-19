# Tutorial: usar LaVolpeButton en Visual Studio

Este tutorial usa Visual Studio 2022 y una aplicación **Windows Forms** para
`.NET 8`. El control no es un OCX y no se puede colocar directamente en WPF,
ASP.NET o MAUI.

## 1. Crear el proyecto

1. Abre Visual Studio.
2. Crea **Aplicación de Windows Forms** con C# y `.NET 8`.
3. Guarda la solución en una ruta local de confianza.

## 2. Referenciar el control

La forma recomendada durante el desarrollo es añadir una referencia de proyecto:

1. En el Explorador de soluciones, clic derecho en la solución → **Agregar → Proyecto existente**.
2. Selecciona `LaVolpe.Buttons.Net\LaVolpe.Buttons.Net.csproj`.
3. En el proyecto WinForms, clic derecho en **Dependencias → Agregar referencia de proyecto** y marca `LaVolpe.Buttons.Net`.
4. Compila la solución.

También puedes usar la DLL generada en `LaVolpe.Buttons.Net\bin\Debug\net8.0-windows`,
aunque la referencia de proyecto es más cómoda para depurar cambios.

## 3. Añadirlo al Toolbox

1. Abre el formulario en modo **Diseño**.
2. Clic derecho en el Cuadro de herramientas → **Elegir elementos...**.
3. Pulsa **Examinar...** y selecciona la DLL compilada de `LaVolpe.Buttons.Net`.
4. Marca `LaVolpeButton` y acepta.
5. Arrastra `LaVolpeButton` al formulario.

Si el control ya aparece bajo `LaVolpe.Buttons.Net`, no vuelvas a cargar la DLL:
selecciónalo y arrástralo al formulario.

## 4. Configurar propiedades

Selecciona el control y usa la ventana **Propiedades**. Las propiedades propias
están agrupadas bajo `LaVolpe`:

- `Caption`: texto visible; `Text` se mantiene sincronizado.
- `Mode`: `CommandButton`, `CheckBox` u `OptionButton`.
- `ButtonShape`: forma rectangular, diagonal, redonda o personalizada.
- `ButtonStyle`: estilo visual (`lv_w95`, `lv_XP`, `lv_Flat`, etc.).
- `CaptionAlign` y `CaptionStyle`: alineación y efecto del texto.
- `GradientMode` y `GradientColor`: degradado opcional.
- `Palette`: colores normal, hover, pulsado, marcado, deshabilitado y foco.
- `Picture`, `PictureAlign` y `PictureSize`: imagen del botón.
- `MousePointer` y `MouseIcon`: cursor estándar o cursor creado desde una imagen.

Después de cambiar una propiedad, guarda el formulario (`Ctrl+S`), cierra el
diseñador y vuelve a abrirlo para comprobar la serialización.

## 5. Usarlo desde código

```csharp
using LaVolpe.Buttons.Net;

var aceptar = new LaVolpeButton
{
    Caption = "Aceptar",
    ButtonStyle = BackStyle.WindowsXp,
    Mode = ButtonMode.CommandButton,
    DialogResult = DialogResult.OK,
    Size = new Size(120, 36),
    Location = new Point(20, 20)
};

aceptar.Click += (_, _) => MessageBox.Show("Pulsado");
Controls.Add(aceptar);
AcceptButton = aceptar;
```

Para un `CheckBox`:

```csharp
var activar = new LaVolpeButton { Caption = "Activar", Mode = ButtonMode.CheckBox };
activar.ValueChanged += (_, _) => Console.WriteLine(activar.Value);
```

Para un grupo de opciones, coloca varios `OptionButton` en el mismo contenedor
(`Form`, `Panel` o `GroupBox`). Al seleccionar uno, los hermanos del mismo
contenedor se deseleccionan.

## 6. Ejecutar y depurar

- Pulsa **F5** en el proyecto ejecutable WinForms, no en el proyecto de tipo
  biblioteca `LaVolpe.Buttons.Net`.
- Si Visual Studio avisa de **Solo mi código** en Release, elige continuar
  depurando o cambia la configuración a Debug.
- `DesignerSample2` es un formulario limpio para probar el diseñador y no activa
  drag-and-drop en el sample principal.

Si aparece `MSB3821` indicando que `Form1.resx` tiene la marca de la Web, cierra
Visual Studio y ejecuta PowerShell en la carpeta del proyecto:

```powershell
Unblock-File .\Form1.resx
```

Si el archivo no existe o el error procede de otro formulario, aplica el comando
al `.resx` que indique la salida. No desbloquees archivos que no reconozcas.

## 7. Ejecutar las pruebas

Desde la carpeta raíz del repositorio:

```powershell
dotnet build .\LaVolpe.Buttons.Net.sln --configuration Debug --no-restore --maxcpucount:1
dotnet run --project .\LaVolpe.Buttons.Tests\LaVolpe.Buttons.Tests.csproj --configuration Debug --no-build
```

El harness ejecuta controles WinForms reales en STA y debe terminar con todas las
pruebas correctas y cero fallos. La matriz de compatibilidad y sus límites están
en [ESTADO-PORT-NET.md](ESTADO-PORT-NET.md).
