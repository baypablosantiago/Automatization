# Procesos Automatizados

Bienvenid@, este es un proyecto de demostracion. 

Su objetivo es dar ejemplos practicos sobre que tipo de actividades repetitivas de oficina pueden ser automatizadas mediante codigo, ganando tanto minutos de productividad como aligerando la carga mental de los trabajadores de escritorio. 

A travez de una simple interfaz de consola, podras ejecutar una serie de procesos automatizados predefinidos. 

Guia paso a paso a continuacion de la ficha tecnica.

## Tecnologias - Ficha tecnica:

- Desarrollado en **[Visual Studio Code](https://code.visualstudio.com/)** utilizando el **[SDK de .NET 9](https://dotnet.microsoft.com/es-es/download/dotnet/9.0)**.
- Librerias de terceros utilizadas: **[iText](https://itextpdf.com/)** y **[Playwright](https://playwright.dev/dotnet/docs/intro)**.
- Otras implementaciones: Pruebas Unitarias y Pruebas de Integracion. 


## 1. Requisitos antes de comenzar:
Para el completo funcionamiento de la aplicacion, es importante que tengas instalado Chrome y una cuenta de google logeada en el mismo browser, ya que algunos procesos automatizados consisten en enviar un correo desde el gmail del usuario.

##  2. Instalacion 
### Opcion A: Descarga directa.

Si preferis hacer solo una descarga y no instalar herramientas de desarrollo ni otras dependencias necesarias, podes descargar el ejecutable comprimido desde el siguiente link probar la aplicacion directamente:

**Link:** Y aca va el link.

### Opcion B: Recomendado para desarrolladores - Clonando el repositorio e instalando dependencias desde la terminal. 

Recomendado el uso de VS Code y el SDK de .NET 9, aunque tambien se puede utilizar Visual Studio 2022. 
1. Abre la terminal de tu editor de texto o IDE y clona el repositorio:
   ```sh
   git clone https://github.com/baypablosantiago/Automatization
    ```
2. Muevete al repositorio clonado que contiene el proyecto:
    ```sh
   cd Automatization/Automatization
    ```
3. Instala las dependencias:
    ```sh
   dotnet restore
    ```   
4. Ejecuta desde la misma terminar:
    ```sh
   dotnet run
    ```
5. OPCIONAL: Podes generar el mismo ejecutable que se encuentra en descarga directa:
    ```sh
   dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
    ```


## 2. Uso:
Las imagenes adjuntas son del .exe, el funcionamiento es el mismo si ejecutas desde la terminal. Al ejecutar la aplicacion veras las siguientes opciones :

![image](https://github.com/user-attachments/assets/557cd9b4-25dc-41e7-a22d-3a747ab76ebc)


##### A continuacion una breve explicacion de cada item:

1.  **Busqueda en youtube sin parametros (Bohemian Rhapsody), usando una nueva sesion.**
Este proceso abre Chrome en una nueva sesion, ingresa a youtube, busca la cancion Bohemian Rhapsody e intenta skippear las publicidades en caso de que haya, terminando la automatizacion con la cancion reproduciendose. 


2. **Busqueda en youtube ingresando parametro de busqueda, usando una nueva sesion.**
Similar a la automatizacion anterior, pero en este caso permite al usuario ingresar el nombre de la cancion a buscar.


3. **Envio de correo de ejemplo, usando sesion iniciada en Chrome.**
Abre Chrome, identifica la sesion del usuario e ingresa a Gmail. Redacta un correo nuevo, siendo el destinatario el mismo usuario propietario del correo, con asunto "Correo automatizado de ejemplo" y cuerpo "Hello world! Este es un correo de ejemplo".


4. **Lectura completa de PDF de ejemplo, muestra informacion importante por consola.**
En la carpeta "Resources" hay un pdf (ExamplePDF) con texto generico Lore Ipsum. En medio del archivo hay una seccion con titulo "OBJETO DE LA LICITACION O EL CONTRATO" seguido de otros detalles de interes. La automatizacion realiza una lectura completa del archivo pdf, se busca este titulo y se copia junto con los detalles de interes. Finalmente imprime esta informacion en pantalla.


5. **Lectura completa de PDF de ejemplo, envia informacion importante por correo usando sesion iniciada en Chrome.**
Una combinacion del punto 3 y 4. Primero se realiza la lectura del PDF de ejemplo, se busca el titulo y se copia junto con la informacion de interes. En lugar de imprimir el texto en pantalla, el proceso abre Chrome, identifica la sesion del usuario, y envia la informacion por correo al mismo usuario.


6. **Ocultar Browser - No se muestra el paso a paso, procesos mas rapidos.**
Activa (o desactiva) el modo Headless, esto significa que Chrome no sera visible mientras se realizan los procesos automatizados. Tambien se desactivara el sleep artificial de un segundo que existe entre paso y paso, de esta manera se obtendran los mismos resultados en las opciones anteriores mucho mas rapido.
