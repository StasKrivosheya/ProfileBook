using System.Globalization;

namespace ProfileBook.Helpers.Localization
{
    public class CultureChangedMessage
    {
        public CultureInfo NewCultureInfo { get; }

        public CultureChangedMessage(CultureInfo newCultureInfo)
        {
            NewCultureInfo = newCultureInfo;
        }

        public CultureChangedMessage(string languageName)
        : this(new CultureInfo(languageName))
        {
            
        }
    }
}
