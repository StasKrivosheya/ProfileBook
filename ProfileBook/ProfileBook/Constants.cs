using System;
using System.Collections.Generic;

namespace ProfileBook
{
    public abstract class Constants
    {
        public const string DATABASE_NAME = "profile_book.db";
        public const string DEFAULT_PROFILE_PIC = "pic_profile.png";
        public const string DEFAULT_LANGUAGE_TWO_LETTER_ISO_NAME = "en";

        public static string DatabasePath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static readonly List<string> SUPPORTED_LANGUAGES_TWO_LETTER_ISO_NAMES = new List<string>
        {
            "en",
            "ru"
        };
    }
}
