namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        int RememberedUserId { get; set; }
        string RememberedUserLogin { get; set; }
    }
}
