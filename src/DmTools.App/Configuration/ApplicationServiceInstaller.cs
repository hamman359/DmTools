using FluentValidation;

using KJWT.SharedKernel.Behaviors;

using MediatR;

namespace DmTools.App.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                Application.AssemblyReference.Assembly,
                Domain.AssemblyReference.Assembly);

            cfg.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });

        //services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddValidatorsFromAssembly(
            Application.AssemblyReference.Assembly,
            includeInternalTypes: true);

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(LoggingPipelineBehavior<,>));
    }
}
