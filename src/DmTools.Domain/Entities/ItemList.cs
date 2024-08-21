using KJWT.SharedKernel.Primatives;
using KJWT.SharedKernel.Results;

namespace DmTools.Domain.Entities;

public sealed class ItemList : AggregateRoot
{
    private readonly List<ListItem> _items = new();

    ItemList(
        Guid id,
        string name,
        string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public ItemList() { }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public IReadOnlyCollection<ListItem> Items => _items;

    public static ItemList Create(
        string name,
        string description)
    {
        //TODO: Add Validations
        return new(new Ulid().ToGuid(), name, description);
    }

    public Result<ListItem> AddItem(
        string value,
        string description,
        int weight)
    {
        var item = new ListItem(value, description, weight, Id);

        _items.Add(item);

        return Result.Success(item);
    }
}
