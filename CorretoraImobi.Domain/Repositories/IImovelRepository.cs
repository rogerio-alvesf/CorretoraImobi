using CorretoraImobi.Domain.Entities;

namespace CorretoraImobi.Domain.Repositories
{
    public interface IImovelRepository
    {
        Task<Imovel> GetByIdAsync(string id);
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task AddAsync(Imovel imovel);
        Task UpdateAsync(Imovel imovel);
        Task DeleteAsync(string id);
        Task DeleteAllAsync();
    }
}
