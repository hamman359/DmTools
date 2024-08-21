namespace DmTools.Application.ListItems.Commands.CreateListItem;

public sealed class CreateListItemCommandHandler
    : ICommandHandler<CreateListItemCommand, Guid>
{
    readonly IItemListRepository _itemListRepository;
    readonly IListItemRepository _listItemRepository;
    readonly IUnitOfWork _unitOfWork;

    public CreateListItemCommandHandler(
        IItemListRepository itemListRepository,
        IListItemRepository listItemRepository,
        IUnitOfWork unitOfWork)
    {
        _itemListRepository = itemListRepository;
        _listItemRepository = listItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateListItemCommand request,
        CancellationToken cancellationToken)
    {
        var itemList = await _itemListRepository.GetByIdAsync(request.ItemListId, cancellationToken);

        if (itemList is null)
        {
            return Result<Guid>.NotFound(DomainErrors.ItemList.NotFound(request.ItemListId));
        }

        var item = itemList.AddItem(
            request.Value,
            request.Description,
            request.Weight);

        _listItemRepository.Add(item);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(itemList.Id);
    }
}
