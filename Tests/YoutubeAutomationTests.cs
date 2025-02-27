using Xunit;
using Microsoft.Playwright;

public class YotubeAutomationTests // INTEGRATION TEST
{
    [Fact]
    public async Task PlaySong_ShouldOpenTheBrowserAndPlayNeverGiveYouUp()
    {
        // Arrange
        var browserService = new BrowserService();
        var youtubeAutomation = new YoutubeAutomation(browserService);
        // Act
        await youtubeAutomation.PlaySong("Never Give You Up");
        // Assert no hay uno concreto, pero si llega hasta el final sin errores, el test es exitoso.
    }
}