using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
{
    public interface IRoleRepository
    {
        bool isUserAdmin(string userName);
    }
}
