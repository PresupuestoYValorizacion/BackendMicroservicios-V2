
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.GetUsersByPagination;

public sealed record GetUsersByPaginationQuery : PaginationParams, IQuery<PagedResults<UserDto>?>
{

}