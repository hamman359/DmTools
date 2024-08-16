
using Microsoft.OpenApi.Models;

namespace DmTools.App.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DmTools Api", Version = "v1" });
            c.EnableAnnotations();
        });
    }
}
