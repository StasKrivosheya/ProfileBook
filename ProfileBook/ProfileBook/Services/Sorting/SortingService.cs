using ProfileBook.Enums;
using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Sorting
{
    class SortingService : ISortingService
    {
        #region --- Private Fields ---

        private readonly ISettingsManager _settingsManager;

        #endregion

        #region --- Constructors ---

        public SortingService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #endregion

        #region --- ISortingService Implementation ---

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

        #endregion
    }
}
