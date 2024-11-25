using CorretoraImobi.Domain.Entities;
using System.Linq.Expressions;

namespace CorretoraImobi.Domain.Interfaces.Repositories
{
    public interface IImovelRepository
    {
        Task<Imovel> GetByIdAsync(string id);
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task AddAsync(Imovel imovel);
        Task ReplaceOneAsync(string id, Imovel imovel);
        Task UpdateLazerAsync(string id, string[] lazer);
        Task DeleteAsync(string id);
        Task DeleteAllAsync();
    }
}
