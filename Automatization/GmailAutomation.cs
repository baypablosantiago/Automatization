using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

public class GmailAutomation
{
    private readonly string _subject;
    private readonly string _body;
    private readonly BrowserService _browserService;

    public GmailAutomation(BrowserService browserService, string subject, string body)
    {
        _browserService = browserService;
        _subject = subject;
        _body = body;
    }

    public async Task SendMail()
    {
        var page = await _browserService.CreatePageAsync();
        await page.BringToFrontAsync();
        await page.GotoAsync("https://mail.google.com/");

        var emailElement = await page.WaitForSelectorAsync("a[aria-label*='Cuenta de Google']", new() { Timeout = 5000 });
        string? email = null;

        if (emailElement != null)
        {
            string? ariaLabel = await emailElement.GetAttributeAsync("aria-label");
            if (!string.IsNullOrEmpty(ariaLabel))
            {
                int startIndex = ariaLabel.IndexOf('(');
                int endIndex = ariaLabel.IndexOf(')');
                if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                {
                    email = ariaLabel.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
                }
            }
        }

        if (email != null)
        {
            await page.WaitForSelectorAsync("div.T-I.T-I-KE.L3");

            await page.ClickAsync("div.T-I.T-I-KE.L3");

            await page.WaitForSelectorAsync("input[id=':vd']", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible, Timeout = 60000 }); // 60 segundos
            await page.FillAsync("input[id=':vd']", email);

            await page.WaitForSelectorAsync("input[id=':rn']");
            await page.FillAsync("input[id=':rn']", _subject);

            await page.WaitForSelectorAsync("div[aria-label='Cuerpo del mensaje']");
            await page.FillAsync("div[aria-label='Cuerpo del mensaje']", _body);

            await page.ClickAsync("div[aria-label='Enviar ‪(Ctrl-Enter)‬']");

        }
    }
}