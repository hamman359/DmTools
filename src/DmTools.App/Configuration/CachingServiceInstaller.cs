using DmTools.Infrastructure.Caching;

using KJWT.SharedKernel.Caching;

namespace DmTools.App.Configuration;

public class CachingServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();
    }
}
