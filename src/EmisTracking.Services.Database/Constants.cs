using System;

namespace EmisTracking.Services.Database
{
    public static class Constants
    {
        public static readonly string AdminUserId = Guid.NewGuid().ToString();

        public const string AdminMailboxLowerCase = "admin@mail.com";
        public const string AdminMailboxUpperCase = "ADMIN@MAIL.COM";
        public const string AdminInitialPassword = "123456_Aa";
        public const string AdminNickname = "Администратор";
        public const string AdminFirstName = "Гл.";
        public const string AdminLastName = "Администратор";
    }
}
