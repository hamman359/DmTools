using Ardalis.ApiEndpoints;

using KJWT.SharedKernel.AspNetCore;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace DmTools.Presentation.Endpoints.ListItems;

[Route("api/ItemList")]
public sealed class Create
    : EndpointBaseAsync
    .WithRequest<CreateListItemRequest>
    .WithActionResult<Guid>
{
    readonly ISender _sender;

    public Create(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("{id}/[namespace]")]
    [TranslateResultToActionResult()]
    [SwaggerOperation(
        Summary = "Creates a new List Item within an Item List",
        Description = "Creates a new List Item within an Item List",
        OperationId = "ListItem_Create",
        Tags = ["ItemListEndpoint"])]
    public async override Task<ActionResult<Guid>> HandleAsync(
        [FromRoute] CreateListItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(request.ToCommand(), cancellationToken);

        return result.ToActionResult(this);
    }
}
