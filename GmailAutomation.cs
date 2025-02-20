using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

class GmailAutomation
{
    private readonly string _recipient;
    private readonly string _subject;
    private readonly string _body;
    private readonly BrowserService _browserService;

    public GmailAutomation(BrowserService browserService, string recipient, string subject, string body)
    {
        _browserService = browserService;
        _recipient = recipient;
        _subject = subject;
        _body = body;
    }

    public async Task SendMail()
    {
        var page = await _browserService.CreatePageAsync();
        await page.BringToFrontAsync();

        await page.GotoAsync("https://mail.google.com/");

        await page.WaitForSelectorAsync("div.T-I.T-I-KE.L3");

        await page.ClickAsync("div.T-I.T-I-KE.L3");

        await page.WaitForSelectorAsync("input[id=':vd']", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible, Timeout = 60000 }); // 60 segundos
        await page.FillAsync("input[id=':vd']", _recipient); 

        await page.WaitForSelectorAsync("input[id=':rn']");
        await page.FillAsync("input[id=':rn']", _subject);

        await page.WaitForSelectorAsync("div[aria-label='Cuerpo del mensaje']");
        await page.FillAsync("div[aria-label='Cuerpo del mensaje']", _body);

        await page.ClickAsync("div[aria-label='Enviar ‪(Ctrl-Enter)‬']");

        Console.WriteLine("📧 Correo enviado con éxito.");
    }
}