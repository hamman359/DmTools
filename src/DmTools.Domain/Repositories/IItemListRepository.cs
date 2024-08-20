

using DmTools.Domain.Entities;

namespace DmTools.Domain.Repositories;

public interface IItemListRepository
{
    void Add(ItemList itemList);
    Task<IList<ItemList>> GetAllAsync(CancellationToken cancellationToken);
    Task<ItemList?> GetByIdAsync(Guid itemlistId, CancellationToken cancellationToken);
}
