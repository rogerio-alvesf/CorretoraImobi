using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CorretoraImobi.Api.Endpoints
{
    public static class ImovelEndpoints
    {
        private const string Pattern= "/";

        public static void AddImovelEndpoints(WebApplication app)
        {
            var imovelEndpointsApp = app.MapGroup("imoveis");

            // Listar todos os imóveis
            imovelEndpointsApp.MapGet(Pattern, async (IImovelRepository imovelRepository) =>
            {
                var imoveis = await imovelRepository.GetAllAsync();
                return Results.Ok(imoveis);
            });

            // Buscar um imóvel por ID
            imovelEndpointsApp.MapGet("{imovelId}", async (IImovelRepository imovelRepository, [FromRoute] string imovelId) =>
            {
                var imovel = await imovelRepository.GetByIdAsync(imovelId);

                return imovel == null 
                    ? Results.NotFound()
                    : Results.Ok(imovel);
            });

            // Criar um novo imóvel
            imovelEndpointsApp.MapPost(Pattern, async (IImovelRepository imovelRepository, [FromBody] Imovel imovel) =>
            {
                await imovelRepository.AddAsync(imovel);
                return Results.Created($"/{imovelEndpointsApp}/{imovel.ID_Imovel}", imovel);
            });

            //Atualizando imóvel
            imovelEndpointsApp.MapPut(Pattern, async (IImovelRepository imovelRepository, [FromBody] Imovel imovel) =>
            {
                await imovelRepository.UpdateAsync(imovel);
                return Results.Ok();
            });

            // Deletar todos os imóveis
            imovelEndpointsApp.MapDelete(Pattern, async (IImovelRepository imovelRepository) =>
            {
                // Supondo que você tem um método DeleteAllAsync no repositório
                await imovelRepository.DeleteAllAsync();
                return Results.Ok(new { Message = "Todos os imóveis foram deletados." });
            });

            //Deletando imóvel por id
            imovelEndpointsApp.MapDelete("{imovelId}", async (IImovelRepository imovelRepository, [FromRoute] string imovelId) =>
            {
                // Supondo que você tem um método DeleteAllAsync no repositório
                await imovelRepository.DeleteAsync(imovelId);
                return Results.Ok(new { Message = "Imóvel deletado com sucesso." });
            });
        }
    }
}
