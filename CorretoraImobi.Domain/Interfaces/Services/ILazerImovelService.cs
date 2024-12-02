namespace CorretoraImobi.Domain.Interfaces.Services
{
    public interface ILazerImovelService
    {
        Task<(bool Success, string Message)> UpdateAllAsync(string id, string[] lazer);
        Task<(bool Success, string Message)> DeleteAllAsync(string id);
    }
}
