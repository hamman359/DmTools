using DmTools.Application.ItemLists.Queries.GetAllItemLists;

namespace DmTools.UnitTests.ItemLists.Queries;

public class GetAllItemListsQueryHandlerTests
{
    readonly Mock<IItemListRepository> _itemListRepositoryMock;

    public GetAllItemListsQueryHandlerTests()
    {
        _itemListRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryGetAllAsync()
    {
        var query = new GetAllItemListsQuery();

        _itemListRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ItemList>());

        var handler = new GetAllItemListsQueryHandler(_itemListRepositoryMock.Object);

        _ = await handler.Handle(query, default);

        _itemListRepositoryMock.Verify(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        var query = new GetAllItemListsQuery();

        _itemListRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ItemList>());

        var handler = new GetAllItemListsQueryHandler(_itemListRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
