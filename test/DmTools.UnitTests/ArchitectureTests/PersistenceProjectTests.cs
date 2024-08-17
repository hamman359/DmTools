namespace DmTools.UnitTests.ArchitectureTests;

public class PersistenceProjectTests : ArchitectureTestBase
{
    [Fact]
    void Presentation_ShouldNot_HaveDependenciesOnOtherProjects()
    {
        Types()
            .That()
            .Are(_presentationLayer)
            .Should()
            .NotDependOnAny(_applicationLayer)
            .Because("Domain layer should not depend on Application layer")
            .AndShould()
            .NotDependOnAny(_persistenceLayer)
            .Because("Domain layer should not depend on Persistence layer")
            .Check(_architecture);
    }
}
