using DmTools.Application.ItemLists;
using DmTools.Application.ItemLists.Queries.GetItemListById;
using DmTools.Presentation.Endpoints.ItemLists;

using MediatR;

namespace DmTools.UnitTests.ItemLists.Endpoints;
public class GetTests
{
    readonly Mock<ISender> _senderMock;
    readonly Get _getEndpoint;
    readonly ItemListResponse _itemListResponse = new(Guid.NewGuid(), "Name", "Description");

    public GetTests()
    {
        _senderMock = new();
        _getEndpoint = new(_senderMock.Object);
    }

    [Fact]
    public async Task HandleAsync_Should_SendGetItemListByIdQuery()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetItemListByIdQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(_itemListResponse));

        _ = await _getEndpoint.HandleAsync(Guid.NewGuid(), default);

        _senderMock.Verify(
            x => x.Send(
                It.Is<GetItemListByIdQuery>(x => x.ItemlistId == _itemListResponse.Id),
                It.IsAny<CancellationToken>()),
            Times.Once());
    }
}
