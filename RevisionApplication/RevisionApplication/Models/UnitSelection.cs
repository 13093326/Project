namespace RevisionApplication.Models
{
    public class UnitSelection
    {
        public int Id { get; set; }
        public int SelectedUnitId { get; set; }

        public int UserSettingId { get; set; }
        public UserSetting UserSetting { get; set; }
    }
}
