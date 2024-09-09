using CorretoraImobi.Api.Models;
using MongoDB.Driver;

namespace CorretoraImobi.Api.Endpoints
{
    public static class ImovelEndpoints
    {
        public static void AddImovelEndpoints(WebApplication app)
        {
            app.MapGet("/imoveis", async (IMongoDatabase db) =>
            {
                var imoveisCollection = db.GetCollection<Imovel>("imoveis");
                var imoveis = await imoveisCollection.Find(_ => true).ToListAsync();
                return Results.Ok(imoveis);
            });

            app.MapGet("/imoveis/{imovelId}", async (IMongoDatabase db, string imovelId) =>
            {
                var imoveisCollection = db.GetCollection<Imovel>("imoveis");
                var imovel = await imoveisCollection.Find(i => i.ID_Imovel == imovelId).FirstOrDefaultAsync();

                if (imovel == null)
                    return Results.NotFound();

                return Results.Ok(imovel);
            });

            app.MapPost("/imovel", async (IMongoDatabase db, Imovel imovel) =>
            {
                var imoveisCollection = db.GetCollection<Imovel>("imoveis");
                await imoveisCollection.InsertOneAsync(imovel);
                return Results.Created($"/imoveis/{imovel.ID_Imovel}", imovel);
            });

            app.MapDelete("/imoveis", async (IMongoDatabase db) =>
            {
                var imoveisCollection = db.GetCollection<Imovel>("imoveis");

                var result = await imoveisCollection.DeleteManyAsync(_ => true);

                return Results.Ok(new { Message = $"{result.DeletedCount} imoveis deletado/s." });
            });
        }
    }
}
