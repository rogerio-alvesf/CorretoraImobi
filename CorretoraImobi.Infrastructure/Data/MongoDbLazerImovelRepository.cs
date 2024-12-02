using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace CorretoraImobi.Infrastructure.Data
{
    public class MongoDbLazerImovelRepository : ILazerImovelRepository
    {
        private readonly IMongoCollection<Imovel> _lazerImoveisCollection;

        public MongoDbLazerImovelRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("CorretoraImobi");
            _lazerImoveisCollection = database.GetCollection<Imovel>("imoveis");
        }

        public async Task UpdateAllAsync(string id, string[] lazer)
        {
            // Definição da atualização para modificar apenas um campo
            var update = Builders<Imovel>.Update.Set("lazer", lazer);

            await _lazerImoveisCollection.UpdateOneAsync(i => i.ID_Imovel == id, update);
        }
    }
}
