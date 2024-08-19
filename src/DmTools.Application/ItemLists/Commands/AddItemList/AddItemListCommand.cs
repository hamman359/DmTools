using KJWT.SharedKernel.Messaging;

namespace DmTools.Application.ItemLists.Commands.AddItemList;

public sealed record AddItemListCommand(
    string Name,
    string Description)
    : ICommand<Guid>;
