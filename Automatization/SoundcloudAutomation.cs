using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

public class SoundCloudAutomation
{
    private readonly BrowserService _browserService;

    public SoundCloudAutomation(BrowserService browserService)
    {
        _browserService = browserService;
    }

    public async Task PlaySong(string search)
    {
        var page = await _browserService.CreatePageAsync();
        await page.BringToFrontAsync();
        await page.GotoAsync("https://soundcloud.com");

        try //aceptar las cookies
        {
            var acceptCookiesButton = await page.WaitForSelectorAsync("button#onetrust-accept-btn-handler", new PageWaitForSelectorOptions { Timeout = 5000 });
            if (acceptCookiesButton != null && await acceptCookiesButton.IsVisibleAsync())
            {
                await acceptCookiesButton.ClickAsync();
            }
        }
        catch (TimeoutException) { }

        await page.WaitForSelectorAsync(".headerSearch__input");
        await page.FillAsync(".headerSearch__input", search);
        await page.PressAsync(".headerSearch__input", "Enter");

        await page.WaitForSelectorAsync("a.sc-button-play.playButton", new PageWaitForSelectorOptions { Timeout = 10000 });
        await page.ClickAsync("a.sc-button-play.playButton");
    }
}