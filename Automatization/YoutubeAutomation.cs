using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

public class YouTubeAutomation
{
    private readonly BrowserService _browserService;

    public YouTubeAutomation(BrowserService browserService)
    {
        _browserService = browserService;
    }

    public async Task PlaySong(string search)
    {
        var page = await _browserService.CreatePageAsync();
        await page.BringToFrontAsync();
        await page.GotoAsync("https://www.youtube.com");

        await page.WaitForSelectorAsync("input[name='search_query']");
        await page.FillAsync("input[name='search_query']", search);
        await page.PressAsync("input[name='search_query']", "Enter");

        await page.WaitForSelectorAsync("ytd-video-renderer", new PageWaitForSelectorOptions { Timeout = 10000 });
        await page.ClickAsync("ytd-video-renderer a#thumbnail");

        await page.ReloadAsync(); //para asegurar que el video arranque y posible skipeo de adds
        await page.WaitForSelectorAsync("video");

        try
        {
            var skipButton = await page.WaitForSelectorAsync("div.ytp-skip-ad button.ytp-skip-ad-button", new PageWaitForSelectorOptions { Timeout = 16000 });
            if (skipButton != null && await skipButton.IsVisibleAsync())
            {
                //await skipButton.ClickAsync();
                await page.ClickAsync(".ytp-skip-ad-button");
            }
        }
        catch (TimeoutException) 
        { 
            //Si no se skipean los adds, entra a este bloque
        }
    }
}
