using Ardalis.ApiEndpoints;

using DmTools.Application.ItemLists;
using DmTools.Application.ItemLists.Queries.GetAllItemLists;

using KJWT.SharedKernel.AspNetCore;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace DmTools.Presentation.Endpoints.ItemLists;

public class GetAllItemListsEndpoint
    : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<IList<ItemListResponse>>
{
    readonly ISender _sender;

    public GetAllItemListsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/ItemList/List")]
    [TranslateResultToActionResult()]
    [SwaggerOperation(
        Summary = "Gets a list of all Item Lists",
        Description = "Gets a list of all Item Lists",
        OperationId = "ItemList_List",
        Tags = ["ItemListEndpoint"])]
    public override async Task<ActionResult<IList<ItemListResponse>>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetAllItemListsQuery(), cancellationToken);

        return result.ToActionResult(this);
    }

}
