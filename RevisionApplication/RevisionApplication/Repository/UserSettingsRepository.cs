using RevisionApplication.Models;
using System.Linq;

namespace RevisionApplication.Repository
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserSettingsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public UserSetting AddSettings(UserSetting userSettings)
        {
            _appDbContext.Add(userSettings);
            _appDbContext.SaveChanges();

            return userSettings;
        }

        public UserSetting GetSettingsByUserName(string userName)
        {
            return _appDbContext.UserSetting.FirstOrDefault(u => u.UserName.Equals(userName));
        }

        public UserSetting UpdateSettings(UserSetting userSetting)
        {
            _appDbContext.UserSetting.Update(userSetting);
            _appDbContext.SaveChanges();

            return userSetting;
        }
    }
}
