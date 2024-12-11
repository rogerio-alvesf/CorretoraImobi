using CorretoraImobi.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CorretoraImobi.Tests.Integrations
{
    public class ImovelControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ImovelControllerTests(WebApplicationFactory<Program> factory)
            => _httpClient = factory.CreateClient();
        
        [Fact]
        public async Task AddImovel_ReturnsCreatedStatusCode()
        {
            // Arrange
            var imovel = new Imovel
            {
                VL_Imovel = 2000,
                TP_Transacao = "Pix",
                TP_Imovel = "Casa",
                NM_Cidade = "Suzano",
                Estado = "SP",
                NM_Bairro = "Vila Urupês",
                DS_Condominio = "N/A",
                ST_Estagio = "Entregue",
                NM_Incorporadora = "N/A",
                NM_Construtora = "MRV",
                DT_Entrega = DateTime.Parse("2024-10-21"),
                Diferencial = ["Academia"],
                Lazer = ["Churrasqueira"],
                Instalacao = [],
                QT_Quarto = 3,
                QT_Garagem_Aberta= 1,
                QT_Garagem_Coberta = 0
            };

            var content = new StringContent(JsonConvert.SerializeObject(imovel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/imovel", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
        }

        [Fact]
        public async Task GetImoveis_ReturnsNotFoundStatusCode()
        {
            // Arrange
            var response = await _httpClient.GetAsync("/imoveis");

            // Assert
            Assert.True(response.StatusCode.Equals(HttpStatusCode.NotFound)); // Verifica se a resposta foi 400
        }

        [Fact]
        public async Task GetImoveisByValorMaiorQue_ReturnsSuccessStatusCode()
        {
            // Arrange
            var valorImovel = 1999;

            var response = await _httpClient.GetAsync($"/imovel/valor/maior-que?valor_imovel={valorImovel}");

            // Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(responseString);
        }

        [Fact]
        public async Task GetImoveisByConstrutora_ReturnsSuccessStatusCode()
        { 
            // Arrange
            var nomeConstrutora = "MRV";
            var response = await _httpClient.GetAsync($"/imovel/construtora/{nomeConstrutora}");
            
            // Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(responseString);
        }

        [Fact]
        public async Task DeleteImovelById_ReturnsSuccessStatusCode()
        {
            // Arrange: Defina o valor do imóvel que será usado para buscar o imóvel específico
            var valorImovel = 2000;

            // Act: Faça uma requisição GET para obter o imóvel com o valor especificado
            var imovel = await _httpClient.GetAsync($"/imovel/valor/igual?valor_imovel={valorImovel}");
            var responseString = await imovel.Content.ReadAsStringAsync();

            // Desserialize a resposta para obter o ID do imóvel
            var idImovel = JsonConvert.DeserializeObject<Imovel>(responseString)?.ID_Imovel;

            Assert.NotNull(idImovel);

            // Act: Faça uma requisição DELETE para deletar o imóvel pelo ID
            var response = await _httpClient.DeleteAsync($"/imovel/{idImovel}");

            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(responseString);
        }
    }
}