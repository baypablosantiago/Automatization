using Xunit;
using Microsoft.Playwright;

public class YotubeAutomationTests // INTEGRATION TEST
{
    [Fact]
    public async Task PlaySong_ShouldOpenTheBrowserAndPlayNeverGiveYouUp()
    {
        // Arrange
        var browserService = new BrowserService(false);
        var youtubeAutomation = new YouTubeAutomation(browserService);
        // Act
        await youtubeAutomation.PlaySong("Never Give You Up");
        // Assert 
        await browserService.CloseBrowserAsync();
    }
}