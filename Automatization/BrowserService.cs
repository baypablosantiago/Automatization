using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

public class BrowserService
{
    private readonly IBrowser? _browser;
    private readonly IBrowserContext? _context;
    private readonly IPlaywright _playwright;

    public BrowserService(bool headless) //este constructor usa una sesion nueva, aunque el browser tenga una logeada
    {
        _playwright = Playwright.CreateAsync().Result;
        _browser = _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless,
            SlowMo = headless ? 0 : 1000,
            ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe" //con esto uso Chrome en lugar de Chromiun
        }).Result;
    }

    public BrowserService(bool headless, string userDataPath) //constructor sobrecargado para usar una sesion ya iniciada
    {
        _playwright = Playwright.CreateAsync().Result;
        _context = _playwright.Chromium.LaunchPersistentContextAsync(
            userDataPath,
            new BrowserTypeLaunchPersistentContextOptions
            {
                Headless = headless,
                SlowMo = headless ? 0 : 1000,
                ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            }).Result;
    }

    public async Task<IPage> CreatePageAsync()
    {
        if (_context != null)
        {
            if (_context.Pages.Count > 0)
            {
                return _context.Pages[0];
            }
            return await _context.NewPageAsync();
        }
        else if (_browser != null)
        {
            var context = await _browser.NewContextAsync();
            return await context.NewPageAsync();
        }

        throw new InvalidOperationException("El navegador no está inicializado.");
    }

    public async Task CloseBrowserAsync()
    {
        if (_context != null)
        {
            await _context.CloseAsync();
        }
        else if (_browser != null)
        {
            await _browser.CloseAsync();
        }

        _playwright.Dispose();
    }
}
