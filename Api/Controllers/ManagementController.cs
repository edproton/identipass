using Api.Controllers.Common;
using Api.Extensions;
using Application.Repositories.Common;
using Application.UseCases.Management.Commands;
using Application.UseCases.Management.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ManagementController(
    ISender mediator) : BaseController
{
    [HttpPost("add-user-role")]
    public async Task<ActionResult> AddUserToRole([FromBody] AddUserToRoleCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Created() : result.ToActionResult();
    }
    
    [HttpGet("users")]
    public async Task<ActionResult> GetAllUsers(PaginatedQuery paginatedQuery)
    {
        var result = await mediator.Send(new GetAllUsersQuery(paginatedQuery));

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }
    
    [HttpDelete("remove-user-role")]
    public async Task<ActionResult> RemoveUserFromRole([FromBody] RemoveUserFromRoleCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? NoContent() : result.ToActionResult();
    }
    
    [HttpPost("create-claim")]
    public async Task<ActionResult> CreateClaim([FromBody] CreateClaimCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Created() : result.ToActionResult();
    }
    
    [HttpPost("add-claim-to-user")]
    public async Task<ActionResult> AddClaimToUser([FromBody] AddClaimToUserCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Created() : result.ToActionResult();
    }
    
    // TODO: Create initial user named root@root.com with root password
    // with Root role and then the user can rename the role and assign it to other users
}