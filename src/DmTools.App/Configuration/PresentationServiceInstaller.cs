
using System.Net;

using KJWT.SharedKernel.AspNetCore;
using KJWT.SharedKernel.Results;

using Microsoft.OpenApi.Models;

namespace DmTools.App.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddControllers(mvcOptions => mvcOptions
                .AddResultConvention(resultStatusMap => resultStatusMap
                    .AddDefaultMap()
                    .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
                        .For("POST", HttpStatusCode.Created)
                        .For("DELETE", HttpStatusCode.NoContent))
                    .For(ResultStatus.Error, HttpStatusCode.InternalServerError)
                    .For(ResultStatus.Created, HttpStatusCode.Created))
                .UseNamespaceRouteToken())
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DmTools Api", Version = "v1" });
            c.EnableAnnotations();
        });
    }
}
