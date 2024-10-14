using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorretoraImobi.Api.Endpoints
{
    public static class ImovelEndpoints
    {
        private const string Pattern= "/";

        public static void AddImovelEndpoints(WebApplication app)
        {
            var imovelEndpointsApp = app.MapGroup("imoveis").WithTags("Imovel");

            // Listar todos os imóveis
            imovelEndpointsApp.MapGet(Pattern, async (IImovelService imovelService) =>
            {
                var imoveis = await imovelService.GetAllAsync();
                return Results.Ok(imoveis);
            });

            imovelEndpointsApp.MapGet("{imovelId}", async (IImovelService imovelService, [FromRoute] string imovelId) =>
                {
                    var result = await imovelService.GetByIdAsync(imovelId);

                    if (!result.Success)
                        return Results.NotFound(new { Message = result.Message });

                    return Results.Ok(new { Message = result.Imovel });
                }
            ).WithOpenApi(operation => new (operation)
                {
                    OperationId = "GetByIdAsync",
                    Summary = "Consulta o imovel pelo id informado",
                    Description = "Caso não seja encontrado, será retornado 400 (NotFound)"
                }
            );

            // Criar um novo imóvel
            imovelEndpointsApp.MapPost(Pattern, async (IImovelService imovelService, [FromBody] Imovel imovel) =>
            {
                await imovelService.AddAsync(imovel);
                return Results.Created($"/{imovelEndpointsApp}/{imovel.ID_Imovel}", imovel);
            });

            //Atualizando imóvel
            imovelEndpointsApp.MapPut(Pattern, async (IImovelService imovelService, [FromBody] Imovel imovel) =>
            {
                var result = await imovelService.UpdateAsync(imovel);

                if (!result.Success)
                    return Results.NotFound(new { Message = result.Message });

                return Results.Ok(new { Message = result.Message });
            });

            // Deletar todos os imóveis
            imovelEndpointsApp.MapDelete(Pattern, async (IImovelService imovelService) =>
            {
                var result = await imovelService.DeleteAllAsync();

                if (!result.Success)
                    return Results.NotFound(new { Message = result.Message });

                return Results.Ok(new { Message = result.Message });
            });

            //Deletando imóvel por id
            imovelEndpointsApp.MapDelete("{imovelId}", async (IImovelService imovelService, [FromRoute] string imovelId) =>
            {
                var result = await imovelService.DeleteAsync(imovelId);

                if (!result.Success)
                    return Results.NotFound(new { message = result.Message });

                return Results.Ok(new { Message = result.Message });
            });
        }
    }
}
