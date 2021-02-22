using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfileBook.Validators
{
    class StringValidator
    {
        // accepts strings that has [4, 16] symbols and starts with non-digit
        private const string LOGIN_REGEX =
            "^(?=.*[A-Za-zА-ЯЁа-яё0-9]$)[A-Za-zА-ЯЁа-яё][A-Za-zА-ЯЁа-яё\\d.-]{3,15}$";

        // accepts string with at least one lowercase, one uppercase and one digit,
        // total length is from 8 to 16
        private const string PASSWORD_REGEX =
            "^(?=.*\\d)(?=.*[a-zа-яё])(?=.*[A-ZА-ЯЁ]).{8,16}$";

        private string Pattern { get; }

        private StringValidator(string pattern)
        {
            Pattern = pattern;
        }

        public static StringValidator Login { get; }
        public static StringValidator Password { get; }

        static StringValidator()
        {
            Login = new StringValidator(LOGIN_REGEX);
            Password = new StringValidator(PASSWORD_REGEX);
        }

        public static bool Validate(string input, StringValidator type)
        {
            return !string.IsNullOrEmpty(input) &&
                   Regex.IsMatch(input, type.Pattern);
        }
    }
}
