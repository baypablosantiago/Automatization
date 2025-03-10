using Xunit;
using Microsoft.Playwright;

public class GmailAutomationTests // INTEGRATION TEST
{
    [Fact]
    public async Task SendMail_ShouldSendExampleMailWithOpenSession()
    {
        // Arrange
        string userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "User Data");
        BrowserService browserService = new BrowserService(false,userDataPath);
        GmailAutomation gmailAutomation = new GmailAutomation(browserService, "Correo automatizado de ejemplo", "Hello world, este es un correo automatizado de ejemplo.");
        // Act
        await gmailAutomation.SendMail();
        // Assert no hay uno concreto, pero si llega hasta el final sin errores, el test es exitoso.
    }
}