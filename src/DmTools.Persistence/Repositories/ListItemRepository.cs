using DmTools.Domain.Entities;
using DmTools.Domain.Repositories;

namespace DmTools.Persistence.Repositories;

public sealed class ListItemRepository
    : IListItemRepository
{
    readonly ApplicationDbContext _dbContext;

    public ListItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ListItem item) =>
        _dbContext
            .Set<ListItem>()
            .Add(item);
}
