using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

public class BrowserService
{
    private readonly IBrowser _browser;
    private readonly IPlaywright _playwright;

    public BrowserService()
    {
        _playwright = Playwright.CreateAsync().Result;
        _browser = _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 1000
        }).Result;
    }

    public async Task<IPage> CreatePageAsync()
    {
        var context = await _browser.NewContextAsync();
        return await context.NewPageAsync();
    }

    public async Task CloseBrowserAsync()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}
