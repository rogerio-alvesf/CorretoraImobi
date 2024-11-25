using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CorretoraImobi.Api.Endpoints
{
    public static class ImovelEndpoints
    {
        private const string Pattern= "/";

        public static void AddImovelEndpoints(WebApplication app)
        {
            var imovelEndpointsApp = app.MapGroup("imoveis").WithTags("Imovel");

            imovelEndpointsApp.MapGet(Pattern, async ([FromServices] IImovelService imovelService) =>
            {
                var imoveis = await imovelService.GetAllAsync();

                return TypedResults.Ok(imoveis);
            }).WithOpenApi(operation => new(operation) {
                    OperationId = "GetAllAsync",
                    Summary = "Listar todos os imóveis",
            });

            imovelEndpointsApp.MapGet("{imovelId}", async Task<Results<Ok<Imovel>, NotFound<string>>> ([FromServices] IImovelService imovelService, [FromRoute] string imovelId) =>
                {
                    var result = await imovelService.GetByIdAsync(imovelId);

                    if (!result.Success)
                        return TypedResults.NotFound(result.Message);

                    return TypedResults.Ok(result.Imovel);
                }
            ).WithOpenApi(operation => new (operation) {
                    OperationId = "GetByIdAsync",
                    Summary = "Consulta o imovel pelo id informado",
                    Description = "Caso não seja encontrado, será retornado 400 (NotFound)"
            });

            imovelEndpointsApp.MapPost(Pattern, async ([FromServices] IImovelService imovelService, [FromBody] Imovel imovel) =>
            {
                await imovelService.AddAsync(imovel);

                return TypedResults.Created($"/{imovelEndpointsApp}/{imovel.ID_Imovel}", imovel);
            })
            .Accepts<Imovel>("application/json")
            .WithOpenApi(operation => new(operation){
                    OperationId = "AddAsync",
                    Summary = "Cadastra um novo imóvel",
            });

            imovelEndpointsApp.MapPut("{imovelId}", async Task<Results<Ok<string>, NotFound<string>>> ([FromServices] IImovelService imovelService, [FromRoute] string imovelId, [FromBody] Imovel imovel) =>
            {
                var result = await imovelService.ReplaceOneAsync(imovelId, imovel);

                if (!result.Success)
                    return TypedResults.NotFound(result.Message);

                return TypedResults.Ok(result.Message);
            })
            .Accepts<Imovel>("application/json")
            .WithOpenApi(operation => new(operation) {
                    OperationId = "ReplaceOneAsync",
                    Summary = "Atualiza as informações do imóvel pelo id",
            });

            imovelEndpointsApp.MapPut("{imovelId}/lazer", async Task<Results<Ok<string>, NotFound<string>>> ([FromServices] IImovelService imovelService, [FromRoute] string imovelId, [FromBody] string[] lazer) =>
            {
                var result = await imovelService.UpdateLazerAsync(imovelId, lazer);

                if (!result.Success)
                    return TypedResults.NotFound(result.Message);

                return TypedResults.Ok(result.Message);
            })
            .Accepts<Imovel>("application/json")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "UpdateLazerAsync",
                Summary = "Atualiza a lista de lazer do imóvel informado",
            });

            imovelEndpointsApp.MapDelete(Pattern, async Task<Results<Ok<string>, NotFound<string>>> ([FromServices] IImovelService imovelService) =>
            {
                var result = await imovelService.DeleteAllAsync();

                if (!result.Success)
                    return TypedResults.NotFound(result.Message);

                return TypedResults.Ok(result.Message);
            }).WithOpenApi(operation => new(operation) {
                    OperationId = "DeleteAllAsync",
                    Summary = "Deleta todos os imóveis do sistema",
            });

            imovelEndpointsApp.MapDelete("{imovelId}", async Task<Results<Ok<string>, NotFound<string>>> ([FromServices] IImovelService imovelService, [FromRoute] string imovelId) =>
            {
                var result = await imovelService.DeleteAsync(imovelId);

                if (!result.Success)
                    return TypedResults.NotFound(result.Message);

                return TypedResults.Ok(result.Message);
            }).WithOpenApi(operation => new(operation) {
                    OperationId = "DeleteAsync",
                    Summary = "Deletando imóvel pelo imovelId informado na rota",
            });
        }
    }
}
