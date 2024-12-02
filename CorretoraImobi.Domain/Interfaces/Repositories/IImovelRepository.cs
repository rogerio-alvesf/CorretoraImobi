using CorretoraImobi.Domain.Entities;

namespace CorretoraImobi.Domain.Interfaces.Repositories
{
    public interface IImovelRepository
    {
        Task<Imovel> GetByIdAsync(string id);
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task AddAsync(Imovel imovel);
        Task ReplaceOneAsync(string id, Imovel imovel);
        Task DeleteAsync(string id);
        Task DeleteAllAsync();
        Task<IEnumerable<Imovel>> GetByValueGreaterThan(string nome_campo, decimal valor_busca);
        Task<IEnumerable<Imovel>> GetByValueLessThan(string nome_campo, decimal valor_busca);
        Task<IEnumerable<Imovel>> GetByValueEqualTo(string nome_campo, decimal valor_busca);
        Task<IEnumerable<Imovel>> GetByValueEqualTo(string nome_campo, string valor_busca);
    }
}
