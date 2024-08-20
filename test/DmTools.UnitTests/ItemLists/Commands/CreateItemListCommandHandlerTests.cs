using DmTools.Application.ItemLists.Commands.AddItemList;

namespace DmTools.UnitTests.ItemLists.Commands;
public class CreateItemListCommandHandlerTests
{
    readonly Mock<IItemListRepository> _itemListRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateItemListCommandHandlerTests()
    {
        _itemListRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryAdd()
    {
        var command = new CreateItemListCommand("Name", "Description");

        var handler = new CreateItemListCommandHandler(
            _itemListRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        _itemListRepositoryMock.Verify(
            x => x.Add(
                It.Is<ItemList>(i => i.Id == result.Value)),
                Times.Once);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWorkSaveChangesAsync()
    {
        var command = new CreateItemListCommand("Name", "Description");

        var handler = new CreateItemListCommandHandler(
            _itemListRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        var command = new CreateItemListCommand("Name", "Description");

        var handler = new CreateItemListCommandHandler(
            _itemListRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }
}
