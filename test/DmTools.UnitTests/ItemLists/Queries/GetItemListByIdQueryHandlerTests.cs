using DmTools.Application.ItemLists.Queries.GetItemListById;

namespace DmTools.UnitTests.ItemLists.Queries;

public class GetItemListByIdQueryHandlerTests
{
    readonly Mock<IItemListRepository> _itemListRepositoryMock;

    readonly ItemList _itemList = ItemList.Create("Name", "Description");

    public GetItemListByIdQueryHandlerTests()
    {
        _itemListRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryGetById()
    {
        var query = new GetItemListByIdQuery(_itemList.Id);

        var handler = new GetItemListByIdQueryHandler(_itemListRepositoryMock.Object);

        _ = await handler.Handle(query, default);

        _itemListRepositoryMock.Verify(
            x => x.GetByIdAsync(_itemList.Id, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenItemListIsFound()
    {
        var query = new GetItemListByIdQuery(_itemList.Id);

        _itemListRepositoryMock.Setup(
            x => x.GetByIdAsync(
                query.ItemlistId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_itemList);

        var handler = new GetItemListByIdQueryHandler(_itemListRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(_itemList.Id);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundResult_WhenItemListIsNotFound()
    {
        var query = new GetItemListByIdQuery(_itemList.Id);

        _itemListRepositoryMock.Setup(
            x => x.GetByIdAsync(
                query.ItemlistId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ItemList?)null);

        var handler = new GetItemListByIdQueryHandler(_itemListRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsNotFound().Should().BeTrue();
        result.Errors.FirstOrDefault().Should().Be(DomainErrors.ItemList.NotFound(_itemList.Id));
        result.Value.Should().BeNull();
    }
}
