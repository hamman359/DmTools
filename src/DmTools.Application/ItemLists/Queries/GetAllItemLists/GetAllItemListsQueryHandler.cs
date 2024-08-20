using DmTools.Domain.Entities;
using DmTools.Domain.Repositories;

using KJWT.SharedKernel.Messaging;
using KJWT.SharedKernel.Results;

namespace DmTools.Application.ItemLists.Queries.GetAllItemLists;

public sealed class GetAllItemListsQueryHandler
    : IQueryHandler<GetAllItemListsQuery, IList<ItemListResponse>>
{
    readonly IItemListRepository _itemListRepository;

    public GetAllItemListsQueryHandler(IItemListRepository itemListRepository)
    {
        _itemListRepository = itemListRepository;
    }

    public async Task<Result<IList<ItemListResponse>>> Handle(
        GetAllItemListsQuery request,
        CancellationToken cancellationToken)
    {
        IList<ItemList> itemLists = await _itemListRepository.GetAllAsync(cancellationToken);

        var response = itemLists
            .Select(i => new ItemListResponse(
                i.Id,
                i.Name,
                i.Description))
            .ToList();

        return response;
    }
}
