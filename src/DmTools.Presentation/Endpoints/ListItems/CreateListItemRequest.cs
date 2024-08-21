using DmTools.Application.ListItems.Commands.CreateListItem;

using Microsoft.AspNetCore.Mvc;

namespace DmTools.Presentation.Endpoints.ListItems;

public sealed record CreateListItemRequest
{
    [FromRoute(Name = "id")] public Guid ItemListId { get; set; }

    [FromBody] public ListItemModel ListItem { get; set; }

    public CreateListItemCommand ToCommand() =>
        new CreateListItemCommand(
            ItemListId,
            ListItem.Value,
            ListItem.Description,
            ListItem.Weight);
}

public sealed record ListItemModel
{
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Weight { get; set; }
}