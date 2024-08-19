using DmTools.Domain.Entities;
using DmTools.Domain.Repositories;

namespace DmTools.Persistence.Repositories;

public sealed class ItemListRepository : IItemListRepository
{
    readonly ApplicationDbContext _dbContext;

    public ItemListRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ItemList itemList) =>
        _dbContext
            .Set<ItemList>()
            .Add(itemList);
}
