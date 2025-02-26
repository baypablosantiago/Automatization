using System.Threading.Tasks;
using Xunit;
using Microsoft.Playwright;
using System.IO;
using System;

public class BrowserServiceTests
{
    public async Task FirstConstructor_ShouldCreateASessionWithoutUser()
    {
        
    }


    [Fact]
    public async Task SecondConstructor_ShouldCreateAUserSession()
    {
        // Arrange
        string userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "User Data");
        var playwright = await Playwright.CreateAsync();

        // Usamos LaunchPersistentContextAsync para usar el perfil persistente
        var context = await playwright.Chromium.LaunchPersistentContextAsync(
            userDataPath,
            new BrowserTypeLaunchPersistentContextOptions
            {
                Headless = false, // Asegúrate de no usar headless para ver el navegador si es necesario
            });

        // Act
        var page = await context.NewPageAsync(); // Creamos una nueva página en el contexto de la sesión persistente
        await page.GotoAsync("https://www.google.com");

        // Assert
        // Verificar si hay un icono de usuario autenticado o alguna señal de sesión activa
        var userProfile = await page.QuerySelectorAsync("a[href*='accounts.google.com']");
        if (userProfile != null)
        {
            // Si encontramos un enlace relacionado con la cuenta de usuario, significa que está autenticado
            Console.WriteLine("Sesión activa: Usuario autenticado.");
            Assert.True(true); // El test pasa si la sesión está activa
        }
        else
        {
            // Si no encontramos el enlace de la cuenta de Google, significa que no hay sesión activa
            Console.WriteLine("No hay sesión activa.");
            Assert.True(false); // El test falla si no hay sesión activa
        }

        // Cleanup
        await context.CloseAsync();  // Cerramos el contexto y el navegador después de la prueba
    }
}