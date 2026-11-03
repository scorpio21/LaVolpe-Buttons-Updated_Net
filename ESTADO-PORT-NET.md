# Estado del port de LaVolpe Buttons a .NET

**Fecha:** 2026-11-02  
Archivos Originales: k:\\Proyectos\\LaVolpe-Buttons-Updated\\LaVolpe-Buttons-Updated-\\

Archivos .NET: k:\\Proyectos\\LaVolpe-Buttons-Updated\\LaVolpe.Buttons.Net\\
Archivos .NET Sample:k:\\Proyectos\\LaVolpe-Buttons-Updated\\LaVolpe.Buttons.Sample\\ 
**Estado general estimado:** **75 %**

> Estos porcentajes son estimaciones técnicas basadas en lo implementado y compilado. No representan una equivalencia formal ni una comparación visual píxel a píxel.

## Resumen por área

|Área|Avance estimado|
|-|-:|
|Control WinForms funcional|90 %|
|Propiedades y enums|80 %|
|Eventos de mouse y teclado|80 %|
|CheckBox y OptionButton|80 %|
|Eventos OLE|50 %|
|Estados visuales|80 %|
|Formas gráficas|70 %|
|Estilos visuales|65 %|
|Imágenes y escalado|80 %|
|Recursos `.frx`|85 %|
|Formulario de ejemplo|85 %|
|Proyecto de ejemplo|90 %|
|Diseñador de Visual Studio|60 %|
|Instalación/distribución|30 %|
|Pruebas automatizadas|25 %|
|Equivalencia visual con VB6|50 %|
|**Aplicación completa exportada**|**75 %**|

## Porcentaje por propiedad VB6

| Propiedad | Implantación estimada | Estado |
|---|---:|---|
| `Name` | 100 % | Heredada de `Control`; disponible en WinForms |
| `BackColor` | 100 % | Implementada y redibuja |
| `ButtonShape` | 100 % | Implementada |
| `ButtonStyle` | 100 % | Implementada |
| `Cancel` | 65 % | Propiedad y procesamiento Escape implementados; falta integración de formulario |
| `Caption` | 100 % | Implementada |
| `CaptionAlign` | 100 % | Implementada |
| `CaptionStyle` | 100 % | Implementada |
| `CausesValidation` | 100 % | Heredada de `Control` |
| `CustomClick` | 100 % | Implementada |
| `Default` | 65 % | Propiedad y procesamiento Enter implementados; falta integración de formulario |
| `DragIcon` | 50 % | Aproximada mediante `MouseIcon`; falta semántica VB6 completa |
| `DragMode` | 65 % | Inicio automático con umbral y `DoDragDrop`; faltan efectos OLE exactos |
| `Enabled` | 100 % | Heredada y con pintura deshabilitada |
| `Font` | 100 % | Heredada de `Control` |
| `FontStyle` | 100 % | Implementada |
| `ForeColor` | 100 % | Heredada y utilizada al pintar |
| `GradientColor` | 100 % | Implementada |
| `GradientMode` | 100 % | Implementada |
| `Height` | 100 % | Heredada de `Control` |
| `HelpContextID` | 30 % | Propiedad almacenada; falta `HelpProvider` |
| `HoverBackColor` | 100 % | Implementada |
| `HoverColorLocks` | 100 % | Implementada |
| `HoverForeColor` | 100 % | Implementada |
| `Index` | 60 % | Propiedad compatible; no recrea control arrays VB6 |
| `Left` | 100 % | Heredada de `Control` |
| `Mode` | 100 % | Implementada |
| `MouseIcon` | 55 % | Se convierte a cursor; falta validar hotspot/transparencia y ciclo de vida |
| `MousePointer` | 100 % | Adaptada a `Cursor` |
| `Picture` | 100 % | Implementada |
| `PictureAlign` | 100 % | Implementada |
| `PictureSize` | 100 % | Implementada |
| `ResetDefaultColors` | 100 % | Implementada |
| `ShowFocusRect` | 100 % | Implementada |
| `TabIndex` | 100 % | Heredada de `Control` |
| `TabStop` | 100 % | Heredada de `Control` |
| `Tag` | 100 % | Heredada de `Control` |
| `ToolTipText` | 85 % | Implementada mediante componente `ToolTip` |
| `Top` | 100 % | Heredada de `Control` |
| `Value` | 100 % | Implementada |
| `Visible` | 100 % | Heredada de `Control` |
| `WhatsThisHelpID` | 30 % | Propiedad almacenada; falta integración de ayuda contextual |
| `Width` | 100 % | Heredada de `Control` |

**Promedio estimado de estas propiedades: 80 %.** La cifra considera tanto la existencia de la propiedad como la equivalencia de su comportamiento VB6; las propiedades heredadas de WinForms cuentan como implantadas cuando cubren el uso esperado.

## Ya implementado

* Proyecto C# para `net8.0-windows`.
* Control `LaVolpeButton` basado en `System.Windows.Forms.Control`.
* Estados normal, hover, pulsado, foco y deshabilitado.
* Modos CommandButton, CheckBox y OptionButton.
* Indicador visual de CheckBox y OptionButton.
* Exclusividad de OptionButton dentro del mismo contenedor.
* Eventos de mouse, teclado, doble clic y drag-and-drop básicos.
* Propiedades de colores, caption, fuente, alineación e imágenes.
* Estilos Windows 95, Windows 3.1, Windows XP, Java, Flat, Hover, Netscape y Macintosh.
* Formas rectangulares, diagonales y redondeadas.
* Gradientes y clipping geométrico.
* Escalado de imágenes con conservación de proporción.
* Compatibilidad parcial con nombres de enums VB6.
* `TimerUpdate(long)` como sustituto administrado del timer VB6.
* Formulario de ejemplo C#.
* Solución y proyectos `.csproj`.
* Documentación básica de uso.

## Pendiente para llegar al 100 %

### 1\. Conversión completa del proyecto VB6

* Convertir completamente `frmSample.frm`.
* Revisar y trasladar todas las propiedades y eventos del formulario original.
* Convertir completamente `prjSampleButtons.vbp`.
* Integrar formalmente `modLvTimer.bas` o documentar su sustitución administrada.

### 2\. Recursos

* Extraer las imágenes e iconos contenidos en los archivos `.frx`.
* Convertir máscaras y transparencias de `StdPicture` a `Image`/`Icon`.
* Incluir los recursos en el proyecto .NET.
* Verificar que se serialicen correctamente desde el diseñador.

### 3\. Compatibilidad del control

* Comparar todas las propiedades públicas del OCX original con las del control .NET.
* Completar la semántica exacta de `OLECompleteDrag` y `OLESetData`.
* Validar completamente `hDC`, `hwnd`, `MousePointer` y `MouseIcon`.
* Igualar el orden exacto de eventos respecto a VB6.
* Revisar valores predeterminados y comportamiento en casos límite.

### 4\. Equivalencia visual

* Replicar con precisión las ocho rutinas `DrawButton\\\_\\\*` del archivo `lvButtons.ctl`.
* Comparar bordes, sombras, colores, degradados y regiones.
* Comparar estados normal, hover, pulsado, seleccionado, foco y deshabilitado.
* Comparar CheckBox y OptionButton contra la versión VB6.
* Realizar comparación de capturas píxel a píxel.

### 5\. Diseñador de Visual Studio

* Probar selección desde Toolbox.
* Insertar el control en un formulario nuevo.
* Modificar y guardar propiedades.
* Cerrar y reabrir el diseñador.
* Confirmar serialización de imágenes, enums y eventos.
* Probar regeneración del código `.Designer.cs`.

### 6\. Distribución

* Crear paquete NuGet o instalador.
* Incluir DLL, documentación, recursos y ejemplo.
* Probar instalación limpia.
* Verificar que no sea necesario registrar el OCX VB6.
* Probar en otro equipo con .NET 8 Desktop Runtime.

### 7\. Pruebas

* Añadir pruebas automatizadas para estados y propiedades.
* Añadir pruebas de CheckBox y OptionButton.
* Añadir pruebas de teclado y aceleradores.
* Añadir pruebas de imágenes y formas.
* Añadir pruebas de DPI y redimensionamiento.
* Comparar la aplicación VB6 y la aplicación .NET con los mismos casos.

## Verificación actual

Los proyectos se han compilado correctamente con:

```powershell
dotnet build .\\\\\\\\LaVolpe.Buttons.Sample\\\\\\\\LaVolpe.Buttons.Sample.csproj --configuration Release
```

Resultado confirmado:

```text
Compilación correcta.
0 errores.
0 advertencias.
```

## Conclusión

La versión actual es una **implementación funcional de WinForms** y el ejemplo se puede ejecutar. El porcentaje restante corresponde principalmente a conversión completa de recursos y proyecto VB6, compatibilidad avanzada, diseñador, distribución y validación visual exacta.

