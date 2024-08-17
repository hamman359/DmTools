namespace DmTools.UnitTests.ArchitectureTests;

public class ApplicationProjectTests : ArchitectureTestBase
{
    [Fact]
    void Application_ShouldNot_HaveDependenciesOnOtherProjects()
    {
        Types()
            .That()
            .Are(_applicationLayer)
            .Should()
            .NotDependOnAny(_appLayer)
            .AndShould()
            .NotDependOnAny(_infrastructureLayer)
            .AndShould()
            .NotDependOnAny(_persistenceLayer)
            .AndShould()
            .NotDependOnAny(_presentationLayer)
            .Check(_architecture);
    }
}
