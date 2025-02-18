using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Playwright;

class Program
{
    public static async Task Main()
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false, 
            SlowMo = 1000
        });

        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync("https://www.youtube.com");

        string song = "Bohemian Rhapsody Queen";
        await page.WaitForSelectorAsync("input[name='search_query']");
        await page.FillAsync("input[name='search_query']", song);

        await page.PressAsync("input[name='search_query']", "Enter");

        await page.WaitForSelectorAsync("ytd-video-renderer", new PageWaitForSelectorOptions { Timeout = 10000 });

        await page.ClickAsync("ytd-video-renderer a#thumbnail");

        Console.WriteLine($"🎵 Reproduciendo: {song}");

        try
        {
            var skipButton = await page.WaitForSelectorAsync("div.ytp-skip-ad button.ytp-skip-ad-button", new PageWaitForSelectorOptions { Timeout = 15000 });

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
            Console.WriteLine("No se detectó el botón de omitir dentro del tiempo límite.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al intentar saltar el anuncio: {ex.Message}");
        }

        await page.Keyboard.PressAsync("t");
        Console.WriteLine("Presiona cualquier tecla para cerrar el navegador...");
        Console.ReadKey();
        await browser.CloseAsync();
    }
}