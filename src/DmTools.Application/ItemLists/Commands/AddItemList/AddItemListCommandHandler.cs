using DmTools.Domain.Entities;
using DmTools.Domain.Repositories;

using KJWT.SharedKernel.Messaging;
using KJWT.SharedKernel.Persistence;
using KJWT.SharedKernel.Results;

namespace DmTools.Application.ItemLists.Commands.AddItemList;
public sealed class AddItemListCommandHandler
    : ICommandHandler<AddItemListCommand, Guid>
{
    readonly IItemListRepository _itemListRepository;
    readonly IUnitOfWork _unitOfWork;

    public AddItemListCommandHandler(
        IItemListRepository itemListRepository,
        IUnitOfWork unitOfWork)
    {
        _itemListRepository = itemListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AddItemListCommand request,
        CancellationToken cancellationToken)
    {
        ItemList itemList = ItemList.Create(
            request.Name,
            request.Description);

        _itemListRepository.Add(itemList);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(itemList.Id, $"ItemList/{itemList.Id}");
    }
}
