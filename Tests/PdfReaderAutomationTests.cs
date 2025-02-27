using Xunit;

public class PdfReaderAutomationTests // UNIT TEST
{
    [Fact]
    public void ExtractForSIAF_Returns_Expected_Text()
    {
        // Arrange
        var pdfReader = new PdfReaderAutomation();
        string fakeContent = "Algunas líneas de texto Lore Ipsum...\nOBJETO DE LA LICITACION O EL CONTRATO\nInformación relevante\n------\nOtra información Lore Ipsum";

        // Act
        string result = pdfReader.ExtractForSIAF(fakeContent);

        // Assert
        Assert.Contains("OBJETO DE LA LICITACION O EL CONTRATO", result);
        Assert.Contains("Información relevante", result);
    }

    [Fact]
    public void ExtractForSIAF_Returns_NotFoundMessage_IfKeywordMissing()
    {
        // Arrange
        var pdfReader = new PdfReaderAutomation();
        string fakeContent = "Texto aleatorio Lore Ipsum sin el patrón buscado";

        // Act
        string result = pdfReader.ExtractForSIAF(fakeContent);

        // Assert
        Assert.Equal("Campo no encontrado.", result);
    }
}
