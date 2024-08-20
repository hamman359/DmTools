namespace DmTools.UnitTests.ArchitectureTests;

public class PersistenceProjectTests : ArchitectureTestBase
{
    [Fact]
    void Presentation_ShouldNot_HaveDependenciesOnOtherProjects()
    {
        Types()
            .That()
            .Are(_persistenceLayer)
            .Should()
            .NotDependOnAny(_applicationLayer)
            .AndShould()
            .NotDependOnAny(_presentationLayer)
            .Check(_architecture);
    }
}
