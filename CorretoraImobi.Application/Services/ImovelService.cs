using CorretoraImobi.Domain.Entities;
using CorretoraImobi.Domain.Enums;
using CorretoraImobi.Domain.Interfaces.Repositories;
using CorretoraImobi.Domain.Interfaces.Services;

namespace CorretoraImobi.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly IImovelRepository _imovelRepository;

        public ImovelService(IImovelRepository imovelRepository)
            => _imovelRepository = imovelRepository;

        public async Task<(bool Success, string Message, Imovel? Imovel)> GetByIdAsync(string id)
        {
            var imovel = await _imovelRepository.GetByIdAsync(id);

            if (imovel == null)
                return (false, "Imóvel não encontrado.", imovel);

            return (true, "", imovel);
        }

        public async Task<IEnumerable<Imovel>> GetAllAsync()
            => await _imovelRepository.GetAllAsync();

        public async Task AddAsync(Imovel imovel)
            => await _imovelRepository.AddAsync(imovel);

        public async Task<(bool Success, string Message)> ReplaceOneAsync(string id, Imovel imovel)
        {
            var imovelExistente = await GetByIdAsync(id);

            if (!imovelExistente.Success)
                return (imovelExistente.Success, imovelExistente.Message);

            await _imovelRepository.ReplaceOneAsync(id, imovel);

            return (true, "Imóvel atualizado com sucesso.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(string id)
        {
            var imovelExistente = await GetByIdAsync(id);

            if (!imovelExistente.Success)
                return (imovelExistente.Success, imovelExistente.Message);

            await _imovelRepository.DeleteAsync(id);

            return (true, "Imóvel deletado com sucesso.");
        }

        public async Task<(bool Success, string Message)> DeleteAllAsync()
        {
            var imoveis = await _imovelRepository.GetAllAsync();

            if (imoveis.Any())
                return (false, "Não há imóveis cadastrado");

            await _imovelRepository.DeleteAllAsync();

            return (true, "Todos os imóveis foram deletados.");
        }

        public async Task<IEnumerable<Imovel>> GetByFilter(TipoFiltroImovel tipoFiltro, string nome_campo, decimal valor_busca)
        {
            var formasBusca = new Dictionary<TipoFiltroImovel, Func<string, decimal, Task<IEnumerable<Imovel>>>>
            {
                { TipoFiltroImovel.Eq, _imovelRepository.GetByValueEqualTo },
                { TipoFiltroImovel.Gt, _imovelRepository.GetByValueGreaterThan },
                { TipoFiltroImovel.Lt, _imovelRepository.GetByValueLessThan },
            };

            return await formasBusca[tipoFiltro](nome_campo, valor_busca);
        }

        public async Task<IEnumerable<Imovel>> GetByFilter(TipoFiltroImovel tipoFiltro, string nome_campo, string valor_busca)
        {
            Dictionary<TipoFiltroImovel, Func<string, string, Task<IEnumerable<Imovel>>>> formasBusca = new Dictionary<TipoFiltroImovel, Func<string, string, Task<IEnumerable<Imovel>>>>
            {
                { TipoFiltroImovel.Eq, _imovelRepository.GetByValueEqualTo }
            };

            return await formasBusca[tipoFiltro](nome_campo, valor_busca);
        }
    }
}
