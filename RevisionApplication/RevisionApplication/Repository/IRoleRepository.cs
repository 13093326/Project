using Microsoft.AspNetCore.Identity;

namespace RevisionApplication.Repository
{
    public interface IRoleRepository
    {
        IdentityUserRole<string> GetAdminRoleForUser(string userName);
    }
}
