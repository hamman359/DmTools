using Ardalis.ApiEndpoints;

using DmTools.Application.ItemLists;
using DmTools.Application.ItemLists.Queries.GetItemListById;

using KJWT.SharedKernel.AspNetCore;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace DmTools.Presentation.Endpoints.ItemLists;

public class GetItemListEndpoint
    : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<ItemListResponse>
{
    readonly ISender _sender;

    public GetItemListEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("ItemList/{id}")]
    [TranslateResultToActionResult()]
    [SwaggerOperation(
        Summary = "Get a specific Item List",
        Description = "Gets a specific Item List",
        OperationId = "ItemList_GetById",
        Tags = ["ItemListEndpoint"])]
    public override async Task<ActionResult<ItemListResponse>> HandleAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetItemListByIdQuery(id), cancellationToken);

        return result.ToActionResult(this);
    }
}