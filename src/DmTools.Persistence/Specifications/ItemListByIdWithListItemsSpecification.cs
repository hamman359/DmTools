using Ardalis.Specification;

using DmTools.Domain.Entities;

namespace DmTools.Persistence.Specifications;

internal sealed class ItemListByIdWithListItemsSpecification : SingleResultSpecification<ItemList>
{
    public ItemListByIdWithListItemsSpecification(Guid itemlistId)
    {
        Query.Where(x => x.Id == itemlistId)
            .Include(x => x.Items);
    }
}
