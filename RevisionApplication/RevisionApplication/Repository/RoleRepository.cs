using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace RevisionApplication.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _appDbContext;

        public RoleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IdentityUserRole<string> GetAdminRoleForUser(string userName)
        {
            // Get user and role objects. 
            var Currentuser = _appDbContext.Users.FirstOrDefault(u => u.UserName.Equals(userName));
            var adminRole = _appDbContext.Roles.FirstOrDefault(r => r.Name.Equals("Admin"));

            // Return admin role for user. 
            if (Currentuser == null)
            {
                return null;
            }
            else
            {
                return _appDbContext.UserRoles.FirstOrDefault(r => r.RoleId.Equals(adminRole.Id) && r.UserId.Equals(Currentuser.Id));
            }
        }
    }
}
