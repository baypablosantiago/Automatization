using System;
using System.Threading.Tasks;

class Program
{
    public static async Task Main()
    {
        bool menu = true;
        while (menu)
        {
            var browserService = new BrowserService();
            var player = new YouTubePlayer(browserService);
            Console.Clear();
            Console.WriteLine("Ingrese un numero para seleccionar una accion automatizada:");
            Console.WriteLine("1 - Busqueda en youtube sin parametros (Bohemian Rhapsody).");
            Console.WriteLine("2 - Busqueda en youtube con parametro personalizado.");
            Console.WriteLine("3 - Envio de correo con la sesion iniciada, cuerpo de ejemplo.");
            Console.WriteLine("4 - Lectura de PDF, envio de correo con la lectura.");
            Console.WriteLine("5 - Terminar aplicativo.");
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
                    var gmailAutomation = new GmailAutomation(browserService, "bay.pablo.santiago@gmail.com", "Correo automatizado de ejemplo", "Hello world, este es un correo automatizado de ejemplo.");
                    await gmailAutomation.EnviarCorreo();
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    await browserService.CloseBrowserAsync();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Work in progress...");
                    Console.WriteLine("Presione cualquier tecla para cerrar el browser y volver al menu principal.");
                    Console.ReadKey();
                    break;
                case "5":
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