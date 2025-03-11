# Procesos Automatizados

Bienvenid@, este es un proyecto de demostración.

El objetivo es dar ejemplos prácticos sobre qué tipo de actividades repetitivas de oficina pueden ser automatizadas mediante código, ganando tanto minutos de productividad como aligerando la carga mental de los trabajadores de escritorio.

Mediante una simple interfaz de consola, podrás ejecutar una serie de procesos automatizados predefinidos.

Guía paso a paso a continuación de la ficha técnica.

## Ficha técnica - Tecnologías

- Desarrollado en **[Visual Studio Code](https://code.visualstudio.com/)** utilizando el **[SDK de .NET 9](https://dotnet.microsoft.com/es-es/download/dotnet/9.0)**.
- Librerías de terceros utilizadas: **[iText](https://itextpdf.com/)** y **[Playwright](https://playwright.dev/dotnet/docs/intro)**.
- Otras implementaciones: Pruebas Unitarias y Pruebas de Integración. 


## 1. Requisitos antes de comenzar.
Para el completo funcionamiento de la aplicación, es importante que uses Windows, tengas instalado Chrome en español y una cuenta de Google logueada en el mismo browser, ya que algunos procesos automatizados consisten en enviar un correo desde el Gmail del usuario.

##  2. Instalación.
### Opcion A: Descarga directa.

Si preferís hacer solo una descarga y no instalar herramientas de desarrollo ni otras dependencias necesarias, podés descargar el ejecutable comprimido en .rar (51mb) desde el siguiente link y probar la aplicación directamente:

**Link:** https://github.com/baypablosantiago/Automatization/releases/download/v1.0/Procesos.Automatizados.rar

### Opcion B: Recomendado para desarrolladores - Clonando el repositorio e instalando dependencias desde la terminal. 

Recomendado el uso de VS Code y el SDK de .NET 9, aunque también se puede utilizar Visual Studio 2022. 
1. Abre la terminal de tu editor de texto o IDE y clona el repositorio:
   ```sh
   git clone https://github.com/baypablosantiago/Automatization
    ```
2. Muévete al repositorio clonado que contiene el proyecto:
    ```sh
   cd Automatization/Automatization
    ```
3. Instala las dependencias:
    ```sh
   dotnet restore
    ```   
4. Ejecuta desde la misma terminal:
    ```sh
   dotnet run
    ```
5. OPCIONAL: Podés generar el mismo ejecutable que se encuentra en descarga directa. Usando el siguiente comando, se crea por defecto en **Automatization/Automatization/bin/Release/net9.0/win-x64/publish**
    ```sh
   dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
    ```


## 3. Uso.
La imagen adjunta es del .exe, el funcionamiento es el mismo si ejecutás desde la terminal. Al ejecutar la aplicación verás las siguientes opciones:

![imagen](https://github.com/user-attachments/assets/8d7649d9-90e1-43ea-b01f-01a540df256c)

#### A continuación, una breve explicación de cada ítem:

*Aclaración: Los tres primeros procesos automatizados fueron pilotos y, aunque en el resultado final no reflejan una actividad diaria de oficina, demuestran automatización para navegar, completar formularios y simular clics en botones dinámicos.*

1.  **Busqueda en YouTube de ejemplo.**
Este proceso abre Chrome en una nueva sesión, ingresa a YouTube, busca la canción Bohemian Rhapsody e intenta saltar las publicidades en caso de que haya, terminando la automatización con la canción reproduciéndose.

2. **Busqueda personalizada en YouTube, usando nueva sesion y skippeando Adds.**
Similar a la automatización anterior, pero en este caso permite al usuario ingresar el nombre de la canción a buscar.

3. **Busqueda personalizada en SoundCloud, usando una nueva sesion y aceptando cookies.**
Tambien similar al proceso con YouTube, pero con SoundCloud. El usuario ingresa la canción a buscar, se abre Chrome, se navega a SoundCloud, se aceptan las cookies, busca la cancion y termina el proceso con la canción reproduciéndose.

4. **Lectura completa de PDF de ejemplo, analisis y extracción de informacion importante.**
En la carpeta "Resources" hay un PDF (ExamplePDF) con texto genérico Lorem Ipsum que puedes abrir. En medio del archivo hay una sección con título "OBJETO DE LA LICITACIÓN O EL CONTRATO", seguido de otros detalles de interés. La automatización realiza una lectura completa del archivo PDF, busca este título y copia los detalles relevantes. Finalmente, imprime esta información en pantalla.

![image](https://github.com/user-attachments/assets/a71f9a61-0ce4-46b1-a23b-31346de1229a)

5. **Envio de correo de ejemplo, usando sesion iniciada en Chrome.**
Abre Chrome, identifica la sesión del usuario e ingresa a Gmail. Redacta un correo nuevo, siendo el destinatario el mismo usuario propietario del correo, con asunto "Correo automatizado de ejemplo" y cuerpo "Hello world! Este es un correo de ejemplo".

6. **Envio de correo de ejemplo, usando sesion iniciada en Chrome - MODO SIN BROWSER.**
Realiza la mista automatizacion del punto anterior (5), pero en modo Headless, lo que significa que el browser no sera visible durante el proceso.

7. **Lectura completa de PDF de ejemplo, enviando informacion importante por correo usando sesion iniciada en Chrome.**
Una combinacion del punto 4 y 5: se realiza el analisis en el PDF de ejemplo y se busca la informacion importante, pero en lugar de imprimir esta informacion por pantalla, se envia por correo al mismo usuario de Chrome.

8. **Lectura completa de PDF de ejemplo, enviando informacion importante por correo usando sesion iniciada en Chrome - MODO SIN BROWSER.**
Realiza la mista automatizacion del punto anterior (7), pero en modo Headless, lo que significa que el browser no sera visible durante el proceso.

##  4. Tests.

Si clonaste el repositorio (Opción B: descarga para desarrolladores), podés correr los tests incluidos:
- Unitario, para la clase de lectura de PDF.
- De integración, para las clases de automatizacion de YouTube, SoundCloud y Gmail.

1. Muévete a la carpeta de los test:
   ```sh
   cd Automatization/Tests
    ```
2. Inicia los tests:
    ```sh
   dotnet test
    ```

##  5. Nota final.

> Si me ves invirtiendo 5 horas en automatizar
> 
> una tarea que demoro 5 minutos en hacer:
> 
> déjame.
> 
> Estoy exactamente donde quiero estar.
