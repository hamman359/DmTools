using KJWT.SharedKernel.Primatives;

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
}
