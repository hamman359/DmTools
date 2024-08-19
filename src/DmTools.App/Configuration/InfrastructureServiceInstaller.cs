using DmTools.Persistence;

using Microsoft.EntityFrameworkCore;

using Scrutor;

namespace DmTools.App.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        Infrastructure.AssemblyReference.Assembly,
                        Persistence.AssemblyReference.Assembly,
                        Domain.AssemblyReference.Assembly,
                        KJWT.SharedKernel.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        services.AddDbContext<ApplicationDbContext>(
            (sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"), default);
            });
    }
}
