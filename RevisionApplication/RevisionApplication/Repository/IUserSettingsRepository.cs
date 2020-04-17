using RevisionApplication.Models;

namespace RevisionApplication.Repository
{
    public interface IUserSettingsRepository
    {
        UserSetting GetSettingsByUserName(string userName);
        UserSetting AddSettings(UserSetting userSettings);
        UserSetting UpdateSettings(UserSetting userSetting);
    }
}