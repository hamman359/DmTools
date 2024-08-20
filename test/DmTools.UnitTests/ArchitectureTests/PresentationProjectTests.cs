namespace DmTools.UnitTests.ArchitectureTests;

public class PresentationProjectTests : ArchitectureTestBase
{

    [Fact]
    void Presentation_ShouldNot_HaveDependenciesOnOtherProjects()
    {
        Types()
            .That()
            .Are(_presentationLayer)
            .Should()
            .NotDependOnAny(_appLayer)
            .Check(_architecture);
    }

}
