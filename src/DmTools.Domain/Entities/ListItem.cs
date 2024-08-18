using KJWT.SharedKernel.Primatives;

namespace DmTools.Domain.Entities;

public sealed class ListItem : Entity
{
    internal ListItem(
        Guid id,
        string value,
        string? description,
        int weight,
        Guid itemListId)
        : base(id)
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