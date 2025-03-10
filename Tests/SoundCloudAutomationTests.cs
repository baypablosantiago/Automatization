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
        // Assert no hay uno concreto, pero si llega hasta el final sin errores, el test es exitoso.
    }
}