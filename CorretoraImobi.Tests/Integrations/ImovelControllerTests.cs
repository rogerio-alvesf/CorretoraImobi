using Microsoft.AspNetCore.Mvc.Testing;

namespace CorretoraImobi.Tests.Integrations
{
    public class ImovelControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ImovelControllerTests(WebApplicationFactory<Program> factory)
            => _httpClient = factory.CreateClient();

        [Fact]
        public async Task GetImoveis_ReturnsSuccessStatusCode()
        {
            // Arrange
            var response = await _httpClient.GetAsync("/imoveis");

            // Assert
            response.EnsureSuccessStatusCode(); // Verifica se a resposta foi 2xx
            var responseString = await response.Content.ReadAsStringAsync();

            // Outras verificações podem ser feitas aqui
            Assert.NotNull(responseString);
        }
    }
}