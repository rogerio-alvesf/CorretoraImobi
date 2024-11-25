using CorretoraImobi.Domain.Entities;

namespace CorretoraImobi.Domain.Interfaces.Services
{
    public interface IImovelService
    {
        Task<(bool Success, string Message, Imovel? Imovel)> GetByIdAsync(string id);
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task AddAsync(Imovel imovel);
        Task<(bool Success, string Message)> ReplaceOneAsync(string id, Imovel imovel);
        Task<(bool Success, string Message)> UpdateLazerAsync(string id, string[] lazer);
        Task<(bool Success, string Message)> DeleteAsync(string id);
        Task<(bool Success, string Message)> DeleteAllAsync();
    }
}
