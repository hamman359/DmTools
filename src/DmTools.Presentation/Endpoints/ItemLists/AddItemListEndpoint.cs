using Ardalis.ApiEndpoints;

using DmTools.Application.ItemLists.Commands.AddItemList;

using KJWT.SharedKernel.AspNetCore;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace DmTools.Presentation.Endpoints.ItemLists;

public class AddItemListEndpoint
    : EndpointBaseAsync
    .WithRequest<AddItemListCommand>
    .WithActionResult<Guid>
{
    readonly ISender _sender;

    public AddItemListEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("/ItemList/Create")]
    [TranslateResultToActionResult()]
    [SwaggerOperation(
        Summary = "Creates a new Item List",
        Description = "Creates a new Item List",
        OperationId = "ItemList_Create",
        Tags = ["CreateItemListEndpoint"])]
    public override async Task<ActionResult<Guid>> HandleAsync(
        [FromBody] AddItemListCommand request,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(request, cancellationToken);

        return result.ToActionResult(this);
    }
}
