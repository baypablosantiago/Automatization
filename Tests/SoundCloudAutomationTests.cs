using Xunit;
using Microsoft.Playwright;

public class SoundCloudAutomationTest // INTEGRATION TEST
{
    [Fact]
    public async Task PlaySong_ShouldOpenTheBrowserAndPlayNeverGiveYouUp()
    {
        // Arrange
        var browserService = new BrowserService(false);
        var soundCloudAutomation = new SoundCloudAutomation(browserService);
        // Act
        await soundCloudAutomation.PlaySong("Never Give You Up");
        // Assert 
        await browserService.CloseBrowserAsync();
    }
}