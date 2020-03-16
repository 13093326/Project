using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _appDbContext;

        public RoleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool isUserAdmin(string userName)
        {
            var Currentuser = _appDbContext.Users.FirstOrDefault(u => u.UserName.Equals(userName));

            var adminRole = _appDbContext.Roles.FirstOrDefault(r => r.Name.Equals("Admin"));

            var userRoles = _appDbContext.UserRoles.FirstOrDefault(r => r.RoleId.Equals(adminRole.Id) && r.UserId.Equals(Currentuser.Id));

            if (userRoles is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
