using KJWT.SharedKernel.Messaging;

namespace DmTools.Application.ItemLists.Queries.GetItemListById;

public sealed record GetItemListByIdQuery(Guid ItemlistId)
    : ICachedQuery<ItemListResponse>
{
    public string CacheKey => $"item-list-by-id-{ItemlistId}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
