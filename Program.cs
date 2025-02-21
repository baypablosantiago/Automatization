using System;
using System.Threading.Tasks;

class Program
{
    public static async Task Main()
    {   
        Console.Title = "Procesos Automatizados 1.0";
        BrowserService browserService;
        YoutubeAutomation player;
        GmailAutomation gmailAutomation;
        PdfReaderAutomation reader;
        string pdfFile = @"C:\Users\tepablob\Desktop\AutoTest\ExamplePDF.pdf";  // Revisar la ruta del pdf a leer!
        bool menu = true;
        while (menu)
        {
            Console.Clear();
            Console.WriteLine("Ingrese un numero para seleccionar una accion automatizada:");
            Console.WriteLine("1 - Busqueda en youtube sin parametros (Bohemian Rhapsody), usando sesion nueva.");
            Console.WriteLine("2 - Busqueda en youtube con parametro personalizado, usando sesion nueva.");
            Console.WriteLine("3 - Envio de correo de ejemplo, usando sesion iniciada en Chrome.");
            Console.WriteLine("4 - Lectura completa de PDF, muestra extraccion de informacion importante por consola.");
            Console.WriteLine("5 - Lectura completa de PDF, envia informacion importante por correo usando sesion iniciada en Chrome.");
            Console.WriteLine("6 - Terminar aplicativo.");
            Console.Write("Opcion seleccionada: ");

            string selection = Console.ReadLine();

            switch (selection)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Opción 1 - Busqueda automatizada de Bohemian Rhapsody.");

                    browserService = new BrowserService();
                    player = new YoutubeAutomation(browserService);
                    await player.PlaySongAsync("Bohemian Rhapsody");
                    
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Opción 2 - Busqueda personalizada.");
                    Console.Write("Ingrese el nombre de una cancion o artista: ");
                    
                    browserService = new BrowserService();
                    player = new YoutubeAutomation(browserService);
                    string search = Console.ReadLine();
                    await player.PlaySongAsync(search);

                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Opción 3 - Envio de correo de ejemplo.");

                    browserService = new BrowserService(@"C:\Users\tepablob\AppData\Local\Google\Chrome\User Data");
                    gmailAutomation = new GmailAutomation(browserService, "bay.pablo.santiago@gmail.com", "Correo automatizado de ejemplo", "Hello world, este es un correo automatizado de ejemplo.");
                    await gmailAutomation.SendMail();

                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("4 - Lectura completa de PDF, mostrando informacion importante al finalizar:");

                    reader = new PdfReaderAutomation();
                    reader.ReadPdf(pdfFile);

                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para volver al menu principal.");
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("5 - Lectura completa de PDF, enviando informacion importante por correo:");

                    reader = new PdfReaderAutomation();
                    string toSIAFandMail = reader.ReadPdf(pdfFile);
                    browserService = new BrowserService(@"C:\Users\tepablob\AppData\Local\Google\Chrome\User Data");
                    gmailAutomation = new GmailAutomation(browserService, "bay.pablo.santiago@gmail.com", "Informacion importante", toSIAFandMail);
                    await gmailAutomation.SendMail();

                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "6":
                    menu = false;
                    Console.Clear();
                    Console.WriteLine("Gracias por utilizar la aplicacion.");
                    Console.WriteLine("Presione cualquier tecla para salir...");
                    Console.ReadKey();
                    Console.Clear();
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