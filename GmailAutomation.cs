using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

class GmailAutomation
{
    private readonly string _destinatario;
    private readonly string _asunto;
    private readonly string _contenido;
    private readonly BrowserService _browserService;

    public GmailAutomation(BrowserService browserService, string destinatario, string asunto, string contenido)
    {
        _browserService = browserService;
        _destinatario = destinatario;
        _asunto = asunto;
        _contenido = contenido;
    }

    public async Task EnviarCorreo()
    {
        var page = await _browserService.CreatePageAsync();
        await page.BringToFrontAsync();

        await page.GotoAsync("https://mail.google.com/");

        await page.WaitForSelectorAsync("div.T-I.T-I-KE.L3");

        await page.ClickAsync("div.T-I.T-I-KE.L3");

        await page.WaitForSelectorAsync("input[id=':vd']", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible, Timeout = 60000 }); // 60 segundos
        await page.FillAsync("input[id=':vd']", _destinatario); 

        await page.WaitForSelectorAsync("input[id=':rn']");
        await page.FillAsync("input[id=':rn']", _asunto);

        await page.WaitForSelectorAsync("div[aria-label='Cuerpo del mensaje']");
        await page.FillAsync("div[aria-label='Cuerpo del mensaje']", _contenido);

        await page.ClickAsync("div[aria-label='Enviar ‪(Ctrl-Enter)‬']");

        Console.WriteLine("📧 Correo enviado con éxito.");
    }
}