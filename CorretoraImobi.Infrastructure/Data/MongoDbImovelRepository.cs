using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace CorretoraImobi.Infrastructure.Data
{
    public class MongoDbImovelRepository : IImovelRepository
    {
        private readonly IMongoCollection<Imovel> _imoveisCollection;

        public MongoDbImovelRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("CorretoraImobi");
            _imoveisCollection = database.GetCollection<Imovel>("imoveis");
        }

        public async Task<Imovel> GetByIdAsync(string id)
            => await _imoveisCollection.Find(imovel => imovel.ID_Imovel == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Imovel>> GetAllAsync()
            => await _imoveisCollection.Find(_ => true).ToListAsync();

        public async Task AddAsync(Imovel imovel)
            => await _imoveisCollection.InsertOneAsync(imovel);

        public async Task UpdateAsync(Imovel imovel)
            => await _imoveisCollection.ReplaceOneAsync(i => i.ID_Imovel == imovel.ID_Imovel, imovel);

        public async Task DeleteAsync(string id)
            => await _imoveisCollection.DeleteOneAsync(imovel => imovel.ID_Imovel == id);

        public async Task DeleteAllAsync()
            => await _imoveisCollection.DeleteManyAsync(_ => true);
    }
}
