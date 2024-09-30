using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Repositories;

namespace CorretoraImobi.Api.Endpoints
{
    public static class ImovelEndpoints
    {
        public static void AddImovelEndpoints(WebApplication app)
        {
            // Listar todos os imóveis
            app.MapGet("/imoveis", async (IImovelRepository imovelRepository) =>
            {
                var imoveis = await imovelRepository.GetAllAsync();
                return Results.Ok(imoveis);
            });

            // Buscar um imóvel por ID
            app.MapGet("/imoveis/{imovelId}", async (IImovelRepository imovelRepository, string imovelId) =>
            {
                var imovel = await imovelRepository.GetByIdAsync(imovelId);

                return imovel == null 
                    ? Results.NotFound() 
                    : Results.Ok(imovel);
            });

            // Criar um novo imóvel
            app.MapPost("/imovel", async (IImovelRepository imovelRepository, Imovel imovel) =>
            {
                await imovelRepository.AddAsync(imovel);
                return Results.Created($"/imoveis/{imovel.ID_Imovel}", imovel);
            });

            // Deletar todos os imóveis
            app.MapDelete("/imoveis", async (IImovelRepository imovelRepository) =>
            {
                // Supondo que você tem um método DeleteAllAsync no repositório
                await imovelRepository.DeleteAll();
                return Results.Ok(new { Message = "Todos os imóveis foram deletados." });
            });
        }
    }
}
