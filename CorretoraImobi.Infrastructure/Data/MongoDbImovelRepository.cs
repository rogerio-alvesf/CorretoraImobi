using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Interfaces.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

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

        public async Task ReplaceOneAsync(string id, Imovel imovel)
        {
            imovel.AtualizarIdentificador(id);

            await _imoveisCollection.ReplaceOneAsync(i => i.ID_Imovel == id, imovel);
        }

        public async Task DeleteAsync(string id)
            => await _imoveisCollection.DeleteOneAsync(imovel => imovel.ID_Imovel == id);

        public async Task DeleteAllAsync()
            => await _imoveisCollection.DeleteManyAsync(_ => true);

        public async Task<IEnumerable<Imovel>> GetByValueGreaterThan(string nome_campo, decimal valor_busca)
        {
            var parametro = Expression.Parameter(typeof(Imovel), "imovel"); 
            var propriedade = Expression.Property(parametro, nome_campo); 
            var constante = Expression.Constant(valor_busca);
            var maiorQue = Expression.GreaterThan(propriedade, constante);
            var expressaoLambda = Expression.Lambda<Func<Imovel, bool>>(maiorQue, parametro);
            
            return await _imoveisCollection.Find(expressaoLambda).ToListAsync();
        }

        public async Task<IEnumerable<Imovel>> GetByValueLessThan(string nome_campo, decimal valor_busca)
        {
            var parametro = Expression.Parameter(typeof(Imovel), "imovel");
            var propriedade = Expression.Property(parametro, nome_campo);
            var constante = Expression.Constant(valor_busca);
            var menorQue = Expression.LessThan(propriedade, constante);
            var expressaoLambda = Expression.Lambda<Func<Imovel, bool>>(menorQue, parametro);

            return await _imoveisCollection.Find(expressaoLambda).ToListAsync();
        }

        public async Task<IEnumerable<Imovel>> GetByValueEqualTo(string nome_campo, decimal valor_busca)
        {
            var filtro = Builders<Imovel>.Filter.Eq(nome_campo, valor_busca); 

            return await _imoveisCollection.Find(filtro).ToListAsync();
        }

        public async Task<IEnumerable<Imovel>> GetByValueEqualTo(string nome_campo, string valor_busca)
        {
            var filtro = Builders<Imovel>.Filter.Eq(nome_campo, valor_busca);

            return await _imoveisCollection.Find(filtro).ToListAsync();
        }
    }
}
