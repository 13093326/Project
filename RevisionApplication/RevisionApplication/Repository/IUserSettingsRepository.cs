using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
{
    public interface IUserSettingsRepository
    {
        UserSetting GetSettingsByUserName(string userName);
        UserSetting AddSettings(UserSetting userSettings);
        UserSetting UpdateSettings(UserSetting userSetting);
    }
}