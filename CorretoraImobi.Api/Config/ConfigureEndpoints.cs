using CorretoraImobi.Api.Endpoints;

namespace CorretoraImobi.Api.Config
{
    public static class ConfigureEndpoints
    {
        public static void AddEndpoints(WebApplication app)
        {
            ImovelEndpoints.AddImovelEndpoints(app);
            LazerImovelEndpoints.AddLazerImovelEndpoints(app);
        }
    }
}
