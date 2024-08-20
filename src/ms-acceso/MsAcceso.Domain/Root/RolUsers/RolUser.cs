using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Domain.Root.RolUsers;

public sealed class RolUser : Entity<RolUserId>
{
    private RolUser(){}

    private RolUser(
        RolUserId id,
        RolId rolId,
        UserId userId
        ): base( id )
    {
        RolId = rolId;
        UserId = userId;
    }
    
    public RolId? RolId { get; set; }
    public UserId? UserId{ get; set; }

}