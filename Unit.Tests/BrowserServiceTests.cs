using System.Threading.Tasks;
using Xunit;
using Microsoft.Playwright;

public class BrowserServiceTests
{
    [Fact]
    public async Task CreatePageAsync_ShouldReturnPage()
    {
        // Arrange
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var service = new BrowserService();

        // Act
        var page = await service.CreatePageAsync();

        // Assert
        Assert.NotNull(page);
        
        // Cleanup
        await service.CloseBrowserAsync();
    }
}
