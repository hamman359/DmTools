using Ardalis.ApiEndpoints;

namespace DmTools.UnitTests.ArchitectureTests;

public abstract class ArchitectureTestBase
{
    protected static readonly Architecture _architecture =
    new ArchLoader()
        .LoadAssemblies(
            App.AssemblyReference.Assembly,
            Application.AssemblyReference.Assembly,
            Domain.AssemblyReference.Assembly,
            Infrastructure.AssemblyReference.Assembly,
            Persistence.AssemblyReference.Assembly,
            Presentation.AssemblyReference.Assembly)
        .Build();


    protected readonly IObjectProvider<IType> _appLayer
        = Types().That().ResideInAssembly(App.AssemblyReference.Assembly).As("AppLayer");

    protected readonly IObjectProvider<IType> _applicationLayer
        = Types().That().ResideInAssembly(Application.AssemblyReference.Assembly).As("ApplicationLayer");

    protected readonly IObjectProvider<IType> _domainLayer
        = Types().That().ResideInAssembly(Domain.AssemblyReference.Assembly).As("DomainLayer");

    protected readonly IObjectProvider<IType> _infrastructureLayer
        = Types().That().ResideInAssembly(Infrastructure.AssemblyReference.Assembly).As("InfrastructureLayer");

    protected readonly IObjectProvider<IType> _persistenceLayer
        = Types().That().ResideInAssembly(Persistence.AssemblyReference.Assembly).As("PersistenceLayer");

    protected readonly IObjectProvider<IType> _presentationLayer
        = Types().That().ResideInAssembly(Presentation.AssemblyReference.Assembly).As("PresentationLayer");


    protected readonly IObjectProvider<Class> _commands =
        Classes().That().ImplementInterface("ICommand").As("Commands");

    protected readonly IObjectProvider<Class> _commandHandlers =
        Classes().That().ImplementInterface("ICommandHandler").As("CommandHandlers");

    protected readonly IObjectProvider<Class> _queries =
        Classes().That().ImplementInterface("IQuery").As("Queries");

    protected readonly IObjectProvider<Class> _cachedQueries =
        Classes().That().ImplementInterface("ICachedQuery").As("CachedQueries");

    protected readonly IObjectProvider<Class> _queryHandlers =
        Classes().That().ImplementInterface("IQueryHandler").As("QueryHandlers");

    protected readonly IObjectProvider<Class> _domainEvents =
        Classes().That().ImplementInterface("IDomainEvents").As("DomainEvents");

    protected readonly IObjectProvider<Class> _domainEventHandlers =
        Classes().That().ImplementInterface("IDomainEventHandler").As("DomainEventHandlers");

    protected readonly IObjectProvider<Class> _specifications =
        Classes().That().ImplementInterface("ISpecification").As("Specifications");


    protected readonly IObjectProvider<Class> _entities =
        Classes().That().ResideInAssembly(Domain.AssemblyReference.Assembly)
            .And().ResideInNamespace("DmTools.Domain.Entities").As("Entities");

    protected readonly IObjectProvider<Class> _errors =
        Classes().That().ResideInAssembly(Domain.AssemblyReference.Assembly)
            .And().ResideInNamespace("DmTools.Domain.Errors").As("DomainErrors");

    protected readonly IObjectProvider<Class> _valueObjects =
        Classes().That().ResideInAssembly(Domain.AssemblyReference.Assembly)
            .And().ResideInNamespace("DmTools.Domain.ValueObjects").As("ValueObjects");

    protected readonly IObjectProvider<Class> _repositories =
        Classes().That().ResideInAssembly(Persistence.AssemblyReference.Assembly)
            .And().ResideInNamespace("DmTools.Persistence.Entities").As("Repositories");

    protected readonly IObjectProvider<Class> _specification =
        Classes().That().ResideInAssembly(Persistence.AssemblyReference.Assembly)
            .And().ResideInNamespace("DmTools.Domain.Entities").As("Entities");

    protected readonly IObjectProvider<Class> _endpoints =
        Classes().That().ResideInAssembly(Presentation.AssemblyReference.Assembly)
            .And().AreAssignableTo(nameof(EndpointBaseSync)).As("Endpoints");
}