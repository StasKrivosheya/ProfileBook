using System.Text.RegularExpressions;

namespace ProfileBook.Validators
{
    public class StringValidator
    {
        #region --- Private Constants ---

        // accepts strings that has [4, 16] symbols and starts with non-digit
        private const string LOGIN_REGEX =
            @"^(?=.*[A-Za-zА-ЯЁа-яё0-9]$)[A-Za-zА-ЯЁа-яё][A-Za-zА-ЯЁа-яё\d._]{3,15}$";

        // accepts string with at least one lowercase, one uppercase and one digit,
        // total length is from 8 to 16
        private const string PASSWORD_REGEX =
            @"^(?=.*\d)(?=.*[a-zа-яё])(?=.*[A-ZА-ЯЁ]).{8,16}$";

        #endregion

        #region --- Properties ---

        private string Pattern { get; }

        public static StringValidator Login { get; }
        public static StringValidator Password { get; }

        #endregion

        #region --- Constructors ---

        private StringValidator(string pattern)
        {
            Pattern = pattern;
        }

        static StringValidator()
        {
            Login = new StringValidator(LOGIN_REGEX);
            Password = new StringValidator(PASSWORD_REGEX);
        }

        #endregion

        #region --- Public Methods ---

        public static bool Validate(string input, StringValidator type)
        {
            return !string.IsNullOrEmpty(input) &&
                   Regex.IsMatch(input, type.Pattern);
        }

        #endregion
    }
}
