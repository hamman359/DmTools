

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using DmTools.Domain.Entities;
using DmTools.Domain.Repositories;
using DmTools.Persistence.Specifications;

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

    public async Task<ItemList?> GetByIdAsync(Guid itemlistId, CancellationToken cancellationToken) =>
        await ApplySpecification(new ItemListByIdWithListItemsSpecification(itemlistId))
            .FirstOrDefaultAsync(cancellationToken);


    IQueryable<ItemList> ApplySpecification(ISpecification<ItemList> specification)
    {
        return SpecificationEvaluator
            .Default
            .GetQuery(_dbContext.Set<ItemList>().AsQueryable(), specification);
    }
}
