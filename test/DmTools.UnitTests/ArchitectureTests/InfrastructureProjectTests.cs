namespace DmTools.UnitTests.ArchitectureTests;

public class InfrastructureProjectTests : ArchitectureTestBase
{
    [Fact]
    void Infrastructure_ShouldNot_HaveDependenciesOnOtherProjects()
    {
        Types()
            .That()
            .Are(_infrastructureLayer)
            .Should()
            .NotDependOnAny(_appLayer)
            .AndShould()
            .NotDependOnAny(_presentationLayer)
            .Check(_architecture);
    }
}
