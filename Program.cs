using System;
using System.Threading.Tasks;

class Program
{
    public static async Task Main()
    {
        bool menu = true;
        var browserService = new BrowserService();
        var player = new YoutubeAutomation(browserService);
        var gmailAutomation = new GmailAutomation(browserService, "bay.pablo.santiago@gmail.com", "Correo automatizado de ejemplo", "Hello world, este es un correo automatizado de ejemplo.");
        PdfReaderAutomation reader = new PdfReaderAutomation();
        string PdfFile = @"C:\Users\tepablob\Desktop\AutoTest\151807 P.pdf";  // Checkear la ruta!

        while (menu)
        {
            Console.Clear();
            Console.WriteLine("Ingrese un numero para seleccionar una accion automatizada:");
            Console.WriteLine("1 - Busqueda en youtube sin parametros (Bohemian Rhapsody).");
            Console.WriteLine("2 - Busqueda en youtube con parametro personalizado.");
            Console.WriteLine("3 - Envio de correo de ejemplo, usando sesion iniciada en Chrome.");
            Console.WriteLine("4 - Lectura de PDF, muestra informacion importante por consola.");
            Console.WriteLine("5 - Lectura de PDF, envia informacion importante por correo usando sesion iniciada en Chrome.");
            Console.WriteLine("6 - Terminar aplicativo.");
            Console.Write("Opcion seleccionada: ");

            string selection = Console.ReadLine();

            switch (selection)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Opción 1 - Busqueda automatizada de Bohemian Rhapsody.");

                    await player.PlaySongAsync("Bohemian Rhapsody");

                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Opción 2 - Busqueda personalizada.");
                    Console.Write("Ingrese el nombre de una cancion o artista: ");
                    
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
                    await gmailAutomation.SendMail();

                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("4 - Lectura de PDF, mostrando informacion importante:");

                    reader.ReadPdf(PdfFile);

                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("5 - Lectura de PDF, enviando informacion importante por correo:");

                    string toSIAFandMail = reader.ReadPdf(PdfFile);
                    browserService = new BrowserService(@"C:\Users\tepablob\AppData\Local\Google\Chrome\User Data");
                    var gmailAutomation1 = new GmailAutomation(browserService, "bay.pablo.santiago@gmail.com", "Correo automatizado de ejemplo", toSIAFandMail);
                    await gmailAutomation1.SendMail();

                    Console.WriteLine(" ");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
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
                    Console.WriteLine("Opción no válida");
                    break;
            }
        }
    }
}