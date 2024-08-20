using DmTools.Domain.Errors;
using DmTools.Domain.Repositories;

using KJWT.SharedKernel.Messaging;
using KJWT.SharedKernel.Results;

namespace DmTools.Application.ItemLists.Queries.GetItemListById;
public sealed class GetItemListByIdQueryHandler
    : IQueryHandler<GetItemListByIdQuery, ItemListResponse>
{
    readonly IItemListRepository _itemListRepository;

    public GetItemListByIdQueryHandler(IItemListRepository itemListRepository)
    {
        _itemListRepository = itemListRepository;
    }

    public async Task<Result<ItemListResponse>> Handle(
        GetItemListByIdQuery request,
        CancellationToken cancellationToken)
    {
        var itemList = await _itemListRepository.GetByIdAsync(
            request.ItemlistId,
            cancellationToken);

        if (itemList is null)
        {
            return Result<ItemListResponse>.NotFound(DomainErrors.ItemList.NotFound(request.ItemlistId));
        }

        var response = new ItemListResponse(
            itemList.Id,
            itemList.Name,
            itemList.Description);

        return response;
    }
}
