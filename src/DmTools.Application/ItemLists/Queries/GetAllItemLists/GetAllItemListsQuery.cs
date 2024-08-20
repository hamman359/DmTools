using KJWT.SharedKernel.Messaging;

namespace DmTools.Application.ItemLists.Queries.GetAllItemLists;

public sealed record GetAllItemListsQuery
    : ICachedQuery<IList<ItemListResponse>>
{
    public string CacheKey => $"item-lists";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
