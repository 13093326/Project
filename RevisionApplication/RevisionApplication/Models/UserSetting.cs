using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class UserSetting
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string SelectedUnits { get; set; }
    }
}
