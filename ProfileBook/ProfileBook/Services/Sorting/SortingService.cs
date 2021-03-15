using ProfileBook.Enums;
using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Sorting
{
    class SortingService : ISortingService
    {
        private readonly ISettingsManager _settingsManager;

        public SortingService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public SortTypes GetCurrentSortType
        {
            get
            {
                if (_settingsManager.RememberedIsSortByName)
                {
                    return SortTypes.ByName;
                }

                if (_settingsManager.RememberedIsSortByNickName)
                {
                    return SortTypes.ByNickName;
                }

                return SortTypes.ByDate;
            }
        }
    }
}
