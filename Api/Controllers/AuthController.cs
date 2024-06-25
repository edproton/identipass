using Api.Controllers.Common;
using Api.Extensions;
using Application.UseCases.Auth.Commands;
using Application.UseCases.Auth.Queries;
using Application.UseCases.RefreshTokens.Comands;
using Application.UseCases.Tokens.Comands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthController(
    ISender mediator) : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Created() : result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshAuthTokensCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RevokeRefreshTokenCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess ? Ok() : result.ToActionResult();
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me([FromHeader] string authorization, [FromHeader] string refreshToken)
    {
        var result = await mediator.Send(new GetMeQuery(authorization, refreshToken));

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }
}