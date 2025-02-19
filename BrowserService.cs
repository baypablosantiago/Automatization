using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

public class BrowserService
{
    private readonly IBrowser? _browser;
    private readonly IBrowserContext? _context;
    private readonly IPlaywright _playwright;
    
    /// <summary>
    /// Constructor para abrir Chrome en una sesión nueva (sin sesión guardada).
    /// </summary>
    public BrowserService()
    {
        _playwright = Playwright.CreateAsync().Result;
        _browser = _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 1000,
            ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe" //con esto uso Chrome en lugar de Chromiun
        }).Result;
    }

    /// <summary>
    /// Constructor para abrir Chrome con una sesión persistente (perfil guardado).
    /// </summary>
    /// <param name="userDataPath">Ruta del perfil de usuario de Chrome.</param>
    public BrowserService(string userDataPath) //constructor sobrecargado para usar una sesion ya iniciada
    {
        _playwright = Playwright.CreateAsync().Result;
        _context = _playwright.Chromium.LaunchPersistentContextAsync(
            userDataPath,
            new BrowserTypeLaunchPersistentContextOptions
            {
                Headless = false,
                SlowMo = 1000,
                ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            }).Result;
    }

    public async Task<IPage> CreatePageAsync()
    {
        if (_context != null)
        {
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
