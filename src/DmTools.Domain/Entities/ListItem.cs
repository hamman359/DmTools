using KJWT.SharedKernel.Primatives;

namespace DmTools.Domain.Entities;

public sealed class ListItem : Entity
{
    internal ListItem(
        string value,
        string? description,
        int weight,
        Guid itemListId)
        : base(Ulid.NewUlid().ToGuid())
    {
        Value = value;
        Description = description;
        Weight = weight;
        ItemListId = itemListId;
    }

    ListItem() { }

    public string Value { get; private set; }

    public string? Description { get; private set; }

    public int Weight { get; private set; }

    public Guid ItemListId { get; private set; }

}