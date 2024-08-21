using KJWT.SharedKernel.Messaging;

namespace DmTools.Application.ListItems.Commands.CreateListItem;

public sealed record CreateListItemCommand(
    Guid ItemListId,
    string Value,
    string Description,
    int Weight)
    : ICommand<Guid>;
