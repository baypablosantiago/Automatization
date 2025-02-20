using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

public class YoutubeAutomation
{
    private readonly BrowserService _browserService;

    public YoutubeAutomation(BrowserService browserService)
    {
        _browserService = browserService;
    }

    public async Task PlaySongAsync(string search)
    {
        var page = await _browserService.CreatePageAsync();
        await page.BringToFrontAsync();
        await page.GotoAsync("https://www.youtube.com");

        await page.WaitForSelectorAsync("input[name='search_query']");
        await page.FillAsync("input[name='search_query']", search);
        await page.PressAsync("input[name='search_query']", "Enter");

        await page.WaitForSelectorAsync("ytd-video-renderer", new PageWaitForSelectorOptions { Timeout = 10000 });
        await page.ClickAsync("ytd-video-renderer a#thumbnail");

        Console.WriteLine($"🎵 Reproduciendo: {search}");

        try
        {
            var skipButton = await page.WaitForSelectorAsync("div.ytp-skip-ad button.ytp-skip-ad-button", new PageWaitForSelectorOptions { Timeout = 15000 });
            if (skipButton != null && await skipButton.IsVisibleAsync())
            {
                Console.WriteLine("Omitiendo anuncio...");
                await skipButton.ClickAsync();
            }
        }
        catch (TimeoutException) { }

        await page.Keyboard.PressAsync("f");
    }
}
