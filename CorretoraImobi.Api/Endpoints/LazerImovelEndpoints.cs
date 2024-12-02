using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CorretoraImobi.Api.Endpoints
{
    public static class LazerImovelEndpoints
    {
        private const string Pattern= "/";

        public static void AddLazerImovelEndpoints(WebApplication app)
        {
            var imovelEndpointsApp = app.MapGroup("imovel/{imovelId}/lazer").WithTags("Lazer Imóvel");

            imovelEndpointsApp.MapPut(Pattern, async Task<Results<Ok<string>, NotFound<string>>> ([FromServices] ILazerImovelService lazerImovelService, [FromRoute] string imovelId, [FromBody] string[] lazer) =>
            {
                var result = await lazerImovelService.UpdateAllAsync(imovelId, lazer);

                if (!result.Success)
                    return TypedResults.NotFound(result.Message);

                return TypedResults.Ok(result.Message);
            })
            .Accepts<Imovel>("application/json")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "UpdateAllAsync",
                Summary = "Atualiza a lista de lazer do imóvel informado",
            });

            imovelEndpointsApp.MapDelete(Pattern, async Task<Results<Ok<string>, NotFound<string>>> ([FromServices] ILazerImovelService lazerImovelService, [FromRoute] string imovelId) =>
            {
                var result = await lazerImovelService.DeleteAllAsync(imovelId);

                if (!result.Success)
                    return TypedResults.NotFound(result.Message);

                return TypedResults.Ok(result.Message);
            }).WithOpenApi(operation => new(operation) {
                    OperationId = "DeleteAllAsync",
                    Summary = "Deleta a lista de lazer do imóvel de arcordo com o imóvel",
            });
        }
    }
}
