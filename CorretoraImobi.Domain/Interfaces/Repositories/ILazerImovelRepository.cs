namespace CorretoraImobi.Domain.Interfaces.Repositories
{
    public interface ILazerImovelRepository
    {
        Task UpdateAllAsync(string id, string[] lazer);
    }
}
