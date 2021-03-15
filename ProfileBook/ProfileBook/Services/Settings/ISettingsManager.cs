namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        int RememberedUserId { get; set; }

        string RememberedUserLogin { get; set; }

        bool RememberedIsSortByName { get; set; }
        bool RememberedIsSortByNickName { get; set; }
        bool RememberedIsSortByDate { get; set; }

        bool RememberedIsDarkTheme { get; set; }

        string RememberedSelectedLanguage { get; set; }
    }
}
