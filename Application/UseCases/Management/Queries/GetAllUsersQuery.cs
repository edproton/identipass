using Application.UseCases.Management.Queries.Common;

namespace Application.UseCases.Management.Queries;

public record GetAllUsersQuery(
    PaginatedQuery PaginatedQuery) : IRequest<Result<PaginatedResult<UserResponseQuery>>>;

public class GetAllUsersQueryHandler(
    ILogger logger,
    IUsersRepository usersRepository)
    : IRequestHandler<GetAllUsersQuery, Result<PaginatedResult<UserResponseQuery>>>
{
    public async Task<Result<PaginatedResult<UserResponseQuery>>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var result = await usersRepository.GetAllAsync(request.PaginatedQuery, cancellationToken);

        logger.LogInformation("Retrieving all tenants");

        return Result.Success(result.Map(t => t.ToQueryResponse()));
    }
}