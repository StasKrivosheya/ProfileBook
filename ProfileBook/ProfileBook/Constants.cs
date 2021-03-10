using System;

namespace ProfileBook
{
    public abstract class Constants
    {
        public const string DATABASE_NAME = "profile_book.db";
        public const string DEFAULT_PROFILE_PIC = "pic_profile.png";

        public static string DatabasePath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
}
