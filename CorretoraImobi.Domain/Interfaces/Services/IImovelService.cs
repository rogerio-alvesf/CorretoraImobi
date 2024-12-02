using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Enums;

namespace CorretoraImobi.Domain.Interfaces.Services
{
    public interface IImovelService
    {
        Task<(bool Success, string Message, Imovel? Imovel)> GetByIdAsync(string id);
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task AddAsync(Imovel imovel);
        Task<(bool Success, string Message)> ReplaceOneAsync(string id, Imovel imovel);
        Task<(bool Success, string Message)> DeleteAsync(string id);
        Task<(bool Success, string Message)> DeleteAllAsync();
        Task<IEnumerable<Imovel>> GetByFilter(TipoFiltroImovel tipoFiltro, string nome_campo, decimal valor_busca);
        Task<IEnumerable<Imovel>> GetByFilter(TipoFiltroImovel tipoFiltro, string nome_campo, string valor_busca);
    }
}
