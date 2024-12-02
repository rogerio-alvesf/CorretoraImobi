using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Enums;
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
            var imovelEndpointsApp = app.MapGroup("imovel").WithTags("Imovel");

            imovelEndpointsApp.MapGet("todos", async ([FromServices] IImovelService imovelService) =>
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

            imovelEndpointsApp.MapDelete("todos", async Task<Results<Ok<string>, NotFound<string>>> ([FromServices] IImovelService imovelService) =>
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

            imovelEndpointsApp.MapGet("valor/maior-que", async ([FromServices] IImovelService imovelService, [FromQuery] decimal valor_imovel) =>
            {
                var result = await imovelService.GetByFilter(TipoFiltroImovel.Gt, "vl_imovel", valor_imovel);

                return TypedResults.Ok(result);
            }).WithOpenApi(operation => new(operation)
            {
                OperationId = "GetByFilter - Maior Que",
                Summary = "Consulta imóveis com o valor maior que o informado via Query",
            });

            imovelEndpointsApp.MapGet("valor/menor-que", async ([FromServices] IImovelService imovelService, [FromQuery] decimal valor_imovel) =>
            {
                var result = await imovelService.GetByFilter(TipoFiltroImovel.Lt, "vl_imovel", valor_imovel);

                return TypedResults.Ok(result);
            }).WithOpenApi(operation => new(operation)
            {
                OperationId = "GetByFilter - Menor Que",
                Summary = "Consulta imóveis com o valor menor que o informado via Query",
            });

            imovelEndpointsApp.MapGet("valor/igual", async ([FromServices] IImovelService imovelService, [FromQuery] decimal valor_imovel) =>
            {
                var result = await imovelService.GetByFilter(TipoFiltroImovel.Eq, "vl_imovel", valor_imovel);

                return TypedResults.Ok(result);
            }).WithOpenApi(operation => new(operation)
            {
                OperationId = "GetByFilter - Igual a",
                Summary = "Consulta imóveis com o valor igual ao informado via Query",
            });

            imovelEndpointsApp.MapGet("filtro/igual", async ([FromServices] IImovelService imovelService, [FromQuery] string nm_campo, [FromQuery] string valor_campo) =>
            {
                var result = await imovelService.GetByFilter(TipoFiltroImovel.Eq, nm_campo, valor_campo);

                return TypedResults.Ok(result);
            }).WithOpenApi(operation => new(operation)
            {
                OperationId = "GetByFilter - Nome campo igual a",
                Summary = "Consulta imóveis com o valor igual ao informado via Query",
            });

            imovelEndpointsApp.MapGet("construtora/{nm_construtora}", async ([FromServices] IImovelService imovelService, [FromRoute] string nm_construtora) =>
            {
                var result = await imovelService.GetByFilter(TipoFiltroImovel.Eq, "nm_construtora", nm_construtora);

                return TypedResults.Ok(result);
            }).WithOpenApi(operation => new(operation)
            {
                OperationId = "GetByConstrutora",
                Summary = "Consulta imóveis pela contrutora informado via Query",
            });
        }
    }
}
