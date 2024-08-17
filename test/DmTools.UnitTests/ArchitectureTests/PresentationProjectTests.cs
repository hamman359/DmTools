namespace DmTools.UnitTests.ArchitectureTests;

public class PresentationProjectTests : ArchitectureTestBase
{

    [Fact]
    void Domain_ShouldNot_HaveDependenciesOnOtherProjects()
    {
        Types()
            .That()
            .Are(_domainLayer)
            .Should()
            .NotDependOnAny(_appLayer)
            .AndShould()
            .NotDependOnAny(_applicationLayer)
            .AndShould()
            .NotDependOnAny(_infrastructureLayer)
            .AndShould()
            .NotDependOnAny(_persistenceLayer)
            .AndShould()
            .NotDependOnAny(_presentationLayer)
            .Check(_architecture);
    }

}
