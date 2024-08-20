using DmTools.Domain.Entities;
using DmTools.Domain.Repositories;

using KJWT.SharedKernel.Messaging;
using KJWT.SharedKernel.Persistence;
using KJWT.SharedKernel.Results;

namespace DmTools.Application.ItemLists.Commands.AddItemList;

public sealed class CreateItemListCommandHandler
    : ICommandHandler<CreateItemListCommand, Guid>
{
    readonly IItemListRepository _itemListRepository;
    readonly IUnitOfWork _unitOfWork;

    public CreateItemListCommandHandler(
        IItemListRepository itemListRepository,
        IUnitOfWork unitOfWork)
    {
        _itemListRepository = itemListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateItemListCommand request,
        CancellationToken cancellationToken)
    {
        ItemList itemList = ItemList.Create(
            request.Name,
            request.Description);

        _itemListRepository.Add(itemList);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(itemList.Id);//, $"ItemLists/{itemList.Id}");
    }
}
