using System.ComponentModel;

namespace RevisionApplication.Models
{
    public class UserSetting
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [DisplayName("Selected Units")]
        public string SelectedUnits { get; set; }
    }
}
