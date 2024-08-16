using FluentValidation;

using KJWT.SharedKernel.Behaviors;

namespace DmTools.App.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly);

            cfg.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(
            Application.AssemblyReference.Assembly,
            includeInternalTypes: true);
    }
}
