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
            await page.WaitForSelectorAsync("div[role='button']:has-text('Redactar')");
            await page.ClickAsync("div[role='button']:has-text('Redactar')");

            await page.WaitForSelectorAsync("input[aria-label='Destinatarios en Para']", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible, Timeout = 60000 });
            await page.FillAsync("input[aria-label='Destinatarios en Para']", email);

            await page.WaitForSelectorAsync("input[name='subjectbox']");
            await page.FillAsync("input[name='subjectbox']", _subject);

            await page.WaitForSelectorAsync("div[role='textbox']");
            await page.FillAsync("div[role='textbox']", _body);

            await page.WaitForSelectorAsync("div[role='button'][aria-label*='Enviar']", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await page.ClickAsync("div[role='button'][aria-label*='Enviar']");

        }
    }
}