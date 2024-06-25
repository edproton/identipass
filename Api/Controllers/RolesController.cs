using Api.Controllers.Common;
using Api.Extensions;
using Application.Repositories.Common;
using Application.UseCases.Roles.Commands;
using Application.UseCases.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class RolesController(
    ISender mediator) : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Created() : result.ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(PaginatedQuery paginatedQuery)
    {
        var result = await mediator.Send(new GetAllRolesQuery(paginatedQuery));

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }
    
    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] UpdateRoleCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? NoContent() : result.ToActionResult();
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] DeleteRoleCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? NoContent() : result.ToActionResult();
    }
}