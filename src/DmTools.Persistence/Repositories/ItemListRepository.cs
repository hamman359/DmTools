
using DmTools.Domain.Entities;
using DmTools.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

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

    public async Task<IList<ItemList>> GetAllAsync(CancellationToken cancellationToken) =>
        await _dbContext
            .Set<ItemList>()
            .ToListAsync(cancellationToken);
}
