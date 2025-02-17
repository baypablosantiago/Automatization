using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Playwright;

class Program
{
    // Estructura que representará el tamaño de la pantalla
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    // Declaramos las funciones de la API de Windows para obtener la resolución de la pantalla
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();
    public static async Task Main()
    {
        using var playwright = await Playwright.CreateAsync();

        // Abrir un navegador Chromium (puedes cambiar a Firefox o WebKit)
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false, // Si es true, se ejecuta en segundo plano
            SlowMo = 1000 // Hace que las acciones sean más lentas (útil para depuración)
        });

        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        RECT desktopRect;
        GetWindowRect(GetDesktopWindow(), out desktopRect);

        int screenWidth = desktopRect.Right - desktopRect.Left;
        int screenHeight = desktopRect.Bottom - desktopRect.Top;

        // Ajusta la ventana de Playwright al tamaño de la pantalla
        await page.SetViewportSizeAsync(screenWidth, screenHeight);
        
        // 1️⃣ Ir a YouTube
        await page.GotoAsync("https://www.youtube.com");

        // 2️⃣ Esperar el campo de búsqueda y escribir la canción
        string cancion = "Bohemian Rhapsody Queen";
        await page.WaitForSelectorAsync("input[name='search_query']");
        await page.FillAsync("input[name='search_query']", cancion);

        // 3️⃣ Presionar Enter para buscar
        await page.PressAsync("input[name='search_query']", "Enter");

        // 4️⃣ Esperar a que los resultados carguen
        await page.WaitForSelectorAsync("ytd-video-renderer", new PageWaitForSelectorOptions { Timeout = 10000 });

        // 5️⃣ Hacer clic en el primer video
        await page.ClickAsync("ytd-video-renderer a#thumbnail");

        Console.WriteLine($"🎵 Reproduciendo: {cancion}");

        // 6️⃣ Esperar unos segundos antes de cerrar (opcional)
        //await Task.Delay(10000);

        await page.WaitForSelectorAsync("button.ytp-fullscreen-button");

        // 7️⃣ Hacer clic en el botón de pantalla completa
        await page.ClickAsync("button.ytp-fullscreen-button");

        
        try
{
    // Esperar a que el contenedor y el botón de "Omitir" sean visibles
    var skipButton = await page.WaitForSelectorAsync("div.ytp-skip-ad button.ytp-skip-ad-button", new PageWaitForSelectorOptions { Timeout = 15000 });

    // Verificar si el botón es visible y luego hacer clic en él
    if (skipButton != null && await skipButton.IsVisibleAsync())
    {
        Console.WriteLine("Verificando si existen adds skipeables...");
        await skipButton.ClickAsync();
    }
    else
    {
        Console.WriteLine("El botón de omitir no es visible.");
    }
}
catch (TimeoutException)
{
    // Si no se encuentra el botón dentro del tiempo límite
    Console.WriteLine("No se detectó el botón de omitir dentro del tiempo límite.");
}
catch (Exception ex)
{
    // Si ocurre cualquier otro error
    Console.WriteLine($"Error al intentar saltar el anuncio: {ex.Message}");
}

        // Mantener el navegador abierto para escuchar la canción
        Console.WriteLine("Presiona cualquier tecla para cerrar el navegador...");
        Console.ReadKey();
        await browser.CloseAsync();
    }
}   