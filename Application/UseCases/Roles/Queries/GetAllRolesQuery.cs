using Application.UseCases.Roles.Queries.Common;

namespace Application.UseCases.Roles.Queries;

public record GetAllRolesQuery(
    PaginatedQuery PaginatedQuery) : IRequest<Result<PaginatedResult<RoleResponseQuery>>>;

public class GetAllRolesQueryHandler(
    ILogger logger,
    IRolesRepository rolesRepository)
    : IRequestHandler<GetAllRolesQuery, Result<PaginatedResult<RoleResponseQuery>>>
{
    public async Task<Result<PaginatedResult<RoleResponseQuery>>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await rolesRepository.GetAllAsync(request.PaginatedQuery, cancellationToken);

        logger.LogInformation("[Role] Retrieving all");

        return Result.Success(result.Map(t => t.ToQueryResponse()));
    }
}