using DmTools.Application.ListItems.Commands.CreateListItem;

namespace DmTools.UnitTests.ListItems.Commands;
public class CreateListItemCommandHandlerTests
{
    readonly Mock<IItemListRepository> _itemListRepositoryMock;
    readonly Mock<IListItemRepository> _listItemRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;

    readonly CreateListItemCommand _command = new(Guid.NewGuid(), "Name", "Description", 2);
    readonly ItemList _listItem = ItemList.Create("name", "description");

    public CreateListItemCommandHandlerTests()
    {
        _itemListRepositoryMock = new();
        _listItemRepositoryMock = new();
        _unitOfWorkMock = new();
    }


    [Fact]
    public async Task Handle_Should_CallRepositoryGetById()
    {
        var handler = new CreateListItemCommandHandler(
            _itemListRepositoryMock.Object,
            _listItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(_command, default);

        _itemListRepositoryMock.Verify(
            x => x.GetByIdAsync(_command.ItemListId, It.IsAny<CancellationToken>()),
            Times.Once);
    }


    [Fact]
    public async Task Handle_Should_ReturnNotFoundResult_WhenItemListIsNotFound()
    {
        _itemListRepositoryMock.Setup(
            x => x.GetByIdAsync(
                _command.ItemListId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ItemList?)null);

        var handler = new CreateListItemCommandHandler(
            _itemListRepositoryMock.Object,
            _listItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(_command, default);

        result.IsNotFound().Should().BeTrue();
        result.Errors.FirstOrDefault().Should().Be(DomainErrors.ItemList.NotFound(_command.ItemListId));
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWorkSaveChangesAsync()
    {
        _itemListRepositoryMock.Setup(
            x => x.GetByIdAsync(
                _command.ItemListId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_listItem);

        var handler = new CreateListItemCommandHandler(
            _itemListRepositoryMock.Object,
            _listItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(_command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        _itemListRepositoryMock.Setup(
            x => x.GetByIdAsync(
                _command.ItemListId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_listItem);

        var handler = new CreateListItemCommandHandler(
            _itemListRepositoryMock.Object,
            _listItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(_command, default);

        result.IsSuccess.Should().BeTrue();
    }
}
