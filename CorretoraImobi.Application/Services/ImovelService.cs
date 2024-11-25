using CorretoraImobi.Domain.Entities;
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
            var imovelExiste = await GetByIdAsync(id);

            if (!imovelExiste.Success)
                return (imovelExiste.Success, imovelExiste.Message);

            await _imovelRepository.ReplaceOneAsync(id, imovel);

            return (true, "Imóvel atualizado com sucesso.");
        }

        public async Task<(bool Success, string Message)> UpdateLazerAsync(string id, string[] lazer)
        {
            var imovelExiste = await GetByIdAsync(id);

            if (!imovelExiste.Success)
                return (imovelExiste.Success, imovelExiste.Message);

            await _imovelRepository.UpdateLazerAsync(id, lazer);

            return (true, $"Lista de lazer do imóvel {id} atualizado com sucesso.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(string id)
        {
            var imovelExiste = await GetByIdAsync(id);

            if (!imovelExiste.Success)
                return (imovelExiste.Success, imovelExiste.Message);

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
    }
}
