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
        GmailAutomation gmailAutomation = new GmailAutomation(browserService, "TEST CON BROWSER", "Hello world!");
        // Act
        await gmailAutomation.SendMail();
        // Assert 
        await browserService.CloseBrowserAsync();
    }

    [Fact]
    public async Task SendMail_ShouldSendExampleMailWithOpenSessionHEADLESS()
    {
        // Arrange
        string userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "User Data");
        BrowserService browserService = new BrowserService(true,userDataPath);
        GmailAutomation gmailAutomation = new GmailAutomation(browserService, "TEST HEADLESS", "Hello world!");
        // Act
        await gmailAutomation.SendMail();
        // Assert 
        await browserService.CloseBrowserAsync();
    }
}