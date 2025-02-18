using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

class GmailAutomation
{
    private readonly string _email = "bay.pablo.santiago@gmail.com";
    private readonly string _password = "tucontraseña";
    private readonly string _destinatario = "destinatario@example.com";
    private readonly string _asunto = "Asunto del correo";
    private readonly string _contenido = "Cuerpo del mensaje.";

    public async Task EnviarCorreo()
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,  // Esto te permite ver el navegador en acción
            SlowMo = 1000
        });

        var page = await browser.NewPageAsync();
        
        // Paso 1: Ir a Gmail
        await page.GotoAsync("https://mail.google.com/");

        // Paso 2: Ingresar el correo
        await page.FillAsync("input[type='email']", _email);
        await page.WaitForSelectorAsync("button[jsname='V67aGc']", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });

        await page.ClickAsync("button[jsname='V67aGc']");
        
        // Esperar a que el campo de la contraseña aparezca
        await page.WaitForSelectorAsync("input[type='password']", new PageWaitForSelectorOptions { Timeout = 5000 });

        // Paso 3: Ingresar la contraseña
        await page.FillAsync("input[type='password']", _password);
        await page.ClickAsync("button[jsname='LgbsSe']");

        // Paso 4: Esperar a que se cargue la bandeja de entrada
        await page.WaitForSelectorAsync("div.T-I.T-I-KE.L3");  // Selector para el botón de redacción

        // Paso 5: Redactar el correo
        await page.ClickAsync("div.T-I.T-I-KE.L3");  // Click en "Redactar"

        // Paso 6: Llenar el formulario de correo
        await page.FillAsync("textarea[name='to']", _destinatario);
        await page.FillAsync("input[name='subjectbox']", _asunto);
        await page.FillAsync("div[aria-label='Cuerpo del mensaje']", _contenido);

        // Paso 7: Enviar el correo
        await page.ClickAsync("div[aria-label='Enviar ‪(Ctrl-Enter)‬']");

        Console.WriteLine("Correo enviado con éxito.");
        
        // Cerrar el navegador
        await browser.CloseAsync();
    }
}
