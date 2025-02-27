using System;
using System.Threading.Tasks;
using iText.Layout.Splitting;

class Program
{
    public static async Task Main()
    {   
        Console.Title = "Procesos Automatizados - Consola de demostracion";
        BrowserService browserService;
        YoutubeAutomation player;
        GmailAutomation gmailAutomation;
        PdfReaderAutomation reader;
        string pdfFile = Path.Combine(AppContext.BaseDirectory, "Resources", "ExamplePDF.pdf");
        string userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "User Data");
        bool headless = false;
        bool menu = true;
        while (menu)
        {
            Console.Clear();
            Console.WriteLine("Ingrese un numero para realizar una accion automatizada:");
            Console.WriteLine(" ");
            Console.WriteLine("1 - Busqueda en youtube sin parametros (Bohemian Rhapsody), usando una nueva sesion.");
            Console.WriteLine("2 - Busqueda en youtube ingresando parametro de busqueda, usando una nueva sesion.");
            Console.WriteLine("3 - Envio de correo de ejemplo, usando sesion iniciada en Chrome.");
            Console.WriteLine("4 - Lectura completa de PDF de ejemplo, muestra informacion importante por consola.");
            Console.WriteLine("5 - Lectura completa de PDF de ejemplo, envia informacion importante por correo usando sesion iniciada en Chrome.");
            Console.WriteLine($"6 - {(headless ? "Mostrar Browser - Paso a paso visible." : "Ocultar Browser - No se muestra el paso a paso, procesos mas rapidos.")}");            
            Console.WriteLine("0 - Salir.");
            Console.WriteLine(" ");
            Console.Write("Opcion seleccionada: ");

            string selection = Console.ReadLine() ?? string.Empty; 

            switch (selection)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Opción 1 - Busqueda automatizada de Bohemian Rhapsody.");
                    Console.WriteLine("Inicializando Browser...");
                    Console.WriteLine("Se intentaran skipear los adds en caso de que existan.");
                    browserService = new BrowserService(headless);
                    player = new YoutubeAutomation(browserService);
                    await player.PlaySong("Bohemian Rhapsody");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Opción 2 - Busqueda personalizada.");
                    Console.Write("Ingrese el nombre de una cancion o artista: ");
                    browserService = new BrowserService(headless);
                    player = new YoutubeAutomation(browserService);
                    string search = Console.ReadLine() ?? string.Empty; //operador de coalescencia nula, para evitar posibles null de parte del usuario
                    Console.WriteLine("Inicializando Browser...");
                    Console.WriteLine("Se intentaran skipear los adds en caso de que existan.");
                    await player.PlaySong(search);
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Opción 3 - Envio de correo de ejemplo.");
                    Console.WriteLine("Inicializando Browser...");
                    browserService = new BrowserService(headless,userDataPath);
                    gmailAutomation = new GmailAutomation(browserService, "Correo automatizado de ejemplo", "Hello world, este es un correo automatizado de ejemplo.");
                    await gmailAutomation.SendMail();
                    Console.WriteLine("Correo enviado!");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("4 - Lectura completa de PDF, mostrando informacion importante al finalizar:");
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
                    Console.WriteLine("5 - Lectura completa de PDF, enviando informacion importante por correo:");
                    Console.WriteLine("Inicializando Browser...");
                    reader = new PdfReaderAutomation();
                    string searchedItem = reader.ReadPdf(pdfFile);
                    browserService = new BrowserService(headless,userDataPath);
                    gmailAutomation = new GmailAutomation(browserService, "Informacion importante", searchedItem);
                    await gmailAutomation.SendMail();
                    Console.WriteLine("Correo enviado!");
                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "6":
                    headless = !headless;
                    Console.Clear();
                    Console.WriteLine($"{(headless ? "Modo headless ACTIVADO - No se mostrara el Browser." : "Modo headless Desactivando - Mostrando Browser.")}");
                    await Task.Delay(3000);
                    break;
                case "0":
                    menu = false;
                    Console.Clear();
                    Console.WriteLine("Gracias por utilizar la aplicacion.");
                    await Task.Delay(1500);

                break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opción no válida.");
                    await Task.Delay(1000);

                    break;
            }
        }
    }
}