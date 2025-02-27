using Xunit;
using Microsoft.Playwright;


public class YotubeAutomationTests // INTEGRATION TEST
{
    [Fact]
    public async Task PlaySong_ShouldOpenTheBrowserAndPlayNeverGiveYouUp()
    {
        // Arrange: Crear instancia real de Playwright
        var browserService = new BrowserService();
        var youtubeAutomation = new YoutubeAutomation(browserService);

        // Act: Ejecutar la automatización
        await youtubeAutomation.PlaySong("Never Give You Up");

        // Assert: No hay una aserción estricta aquí, pero si llega hasta el final sin errores, el test es exitoso.

    }
}