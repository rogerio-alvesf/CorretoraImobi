using CorretoraImobi.Domain.Interfaces.Repositories;
using CorretoraImobi.Domain.Interfaces.Services;

namespace CorretoraImobi.Application.Services
{
    public class LazerImovelService : ILazerImovelService
    {
        private readonly IImovelService _imovelService;
        private readonly ILazerImovelRepository _lazerImovelRepository;

        public LazerImovelService(IImovelService imovelService, ILazerImovelRepository lazerImovelRepository)
        {
            _imovelService = imovelService;
            _lazerImovelRepository = lazerImovelRepository;
        }

        public async Task<(bool Success, string Message)> UpdateAllAsync(string id, string[] lazer)
        {
            var imovelExiste = await _imovelService.GetByIdAsync(id);

            if (!imovelExiste.Success)
                return (imovelExiste.Success, imovelExiste.Message);

            await _lazerImovelRepository.UpdateAllAsync(id, lazer);

            return (true, $"Lista de lazer do imóvel {id} atualizado com sucesso.");
        }

        public async Task<(bool Success, string Message)> DeleteAllAsync(string id)
        {
            var imovelExiste = await _imovelService.GetByIdAsync(id);

            if (!imovelExiste.Success)
                return (imovelExiste.Success, imovelExiste.Message);

            await _lazerImovelRepository.UpdateAllAsync(id, Array.Empty<string>());

            return (true, $"Lista de lazer do imóvel {id} foi apagada.");
        }
    }
}
