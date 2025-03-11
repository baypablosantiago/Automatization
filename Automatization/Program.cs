using System;
using System.Threading.Tasks;

class Program
{
    public static async Task Main()
    {   
        Console.Title = "Procesos Automatizados - Consola de demostración.";
        #pragma warning disable CA1416 // Validate platform compatibility
        Console.WindowHeight = 20;  
        Console.WindowWidth = 140;  
        #pragma warning restore CA1416 // Validate platform compatibility

        BrowserService browserService;
        YouTubeAutomation player;
        SoundCloudAutomation player2;
        GmailAutomation gmailAutomation;
        PdfReaderAutomation reader;
        string pdfFile = Path.Combine(AppContext.BaseDirectory, "Resources", "ExamplePDF.pdf");
        string userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "User Data");
        bool menu = true;
        while (menu)
        {
            Console.Clear();
            Console.WriteLine("Ingrese un numero para realizar una accion automatizada:");
            Console.WriteLine(" ");
            Console.WriteLine("1 - Busqueda en YouTube de ejemplo.");
            Console.WriteLine("2 - Busqueda personalizada en YouTube, usando nueva sesion y skippeando Adds.");
            Console.WriteLine("3 - Busqueda personalizada en SoundCloud, usando una nueva sesion y aceptando cookies.");
            Console.WriteLine("4 - Lectura completa de PDF de ejemplo, analisis y extracción de informacion importante.");
            Console.WriteLine("5 - Envio de correo de ejemplo, usando sesion iniciada en Chrome.");
            Console.WriteLine("6 - Envio de correo de ejemplo, usando sesion iniciada en Chrome - MODO SIN BROWSER.");
            Console.WriteLine("7 - Lectura completa de PDF de ejemplo, enviando informacion importante por correo usando sesion iniciada en Chrome.");
            Console.WriteLine("8 - Lectura completa de PDF de ejemplo, enviando informacion importante por correo usando sesion iniciada en Chrome - MODO SIN BROWSER.");
            Console.WriteLine("9 - Salir.");
            Console.WriteLine(" ");
            Console.Write("Opcion seleccionada: ");

            string selection = Console.ReadLine() ?? string.Empty; //operador de coalescencia nula, para evitar posibles null de parte del usuario

            switch (selection)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Opción 1 - Busqueda en YouTube de ejemplo: Bohemian Rhapsody - Queen.");
                    Console.WriteLine("Inicializando Browser.");
                    Console.WriteLine("Se intentaran skipear los adds en caso de que existan. Espere unos segundos por favor...");
                    browserService = new BrowserService(false);
                    player = new YouTubeAutomation(browserService);
                    await player.PlaySong("Bohemian Rhapsody");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Opción 2 - Busqueda personalizada en YouTube, usando nueva sesion y skippeando Adds.");
                    Console.Write("Ingrese el nombre de una cancion o artista: ");
                    browserService = new BrowserService(false);
                    player = new YouTubeAutomation(browserService);
                    string search = Console.ReadLine() ?? string.Empty; 
                    Console.WriteLine("Inicializando Browser.");
                    Console.WriteLine("Se intentaran skipear los adds en caso de que existan. Espere unos segundos por favor...");
                    await player.PlaySong(search);
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Opción 3 - Busqueda personalizada en SoundCloud, usando una nueva sesion y aceptando cookies.");
                    Console.Write("Ingrese el nombre de una cancion o artista: ");
                    browserService = new BrowserService(false);
                    player2 = new SoundCloudAutomation(browserService);
                    string search2 = Console.ReadLine() ?? string.Empty; 
                    Console.WriteLine("Inicializando Browser.");
                    await player2.PlaySong(search2);
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Opción 4 - Lectura completa de PDF de ejemplo, analisis y extracción de informacion importante.");
                    reader = new PdfReaderAutomation();
                    string info = reader.ReadPdf(pdfFile);
                    Console.WriteLine(" ");
                    Console.WriteLine(info);
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para volver al menu principal.");
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Opción 5 - Envio de correo de ejemplo, usando sesion iniciada en Chrome.");
                    Console.WriteLine("Inicializando Browser.");
                    browserService = new BrowserService(false,userDataPath);
                    gmailAutomation = new GmailAutomation(browserService, "Correo automatizado de ejemplo", "Hello world, este es un correo automatizado de ejemplo.");
                    await gmailAutomation.SendMail();
                    Console.WriteLine("Correo enviado!");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Opción 6 - Envio de correo de ejemplo, usando sesion iniciada en Chrome - MODO SIN BROWSER.");
                    Console.WriteLine("Inicializando Browser.");
                    browserService = new BrowserService(true,userDataPath);
                    gmailAutomation = new GmailAutomation(browserService, "Correo automatizado de ejemplo", "Hello world, este es un correo automatizado de ejemplo.");
                    await gmailAutomation.SendMail();
                    Console.WriteLine("Correo enviado!");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;   
                case "7":
                    Console.Clear();
                    Console.WriteLine("Opción 7 - Lectura completa de PDF de ejemplo, enviando informacion importante por correo usando sesion iniciada en Chrome.");
                    Console.WriteLine("Inicializando Browser.");
                    reader = new PdfReaderAutomation();
                    string searchedItem = reader.ReadPdf(pdfFile);
                    browserService = new BrowserService(false,userDataPath);
                    gmailAutomation = new GmailAutomation(browserService, "Informacion importante", searchedItem);
                    await gmailAutomation.SendMail();
                    Console.WriteLine("Correo enviado!");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "8":
                    Console.Clear();
                    Console.WriteLine("Opción 8 - Lectura completa de PDF de ejemplo, enviando informacion importante por correo usando sesion iniciada en Chrome - MODO SIN BROWSER.");
                    Console.WriteLine("Inicializando Browser.");
                    reader = new PdfReaderAutomation();
                    string searchedItem2 = reader.ReadPdf(pdfFile);
                    browserService = new BrowserService(false,userDataPath);
                    gmailAutomation = new GmailAutomation(browserService, "Informacion importante", searchedItem2);
                    await gmailAutomation.SendMail();
                    Console.WriteLine("Correo enviado!");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "9":
                    menu = false;
                    Console.Clear();
                    Console.WriteLine("Gracias por utilizar la aplicacion!");
                    await Task.Delay(1500);
                break;

                default:
                    Console.Clear();
                    Console.WriteLine("Ingrese una opcion válida.");
                    await Task.Delay(1000);
                    break;
            }
        }
    }
}