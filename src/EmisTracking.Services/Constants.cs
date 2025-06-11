namespace EmisTracking.Services
{
    public static class Constants
    {
        public const int MinPageNumber = 1;
        public const int DefaultPageSize = 10;
        public const int MinPageSize = 5;

        public const string UserIdClaimType = "userId";
        public const string RoleInfoClaimType = "roleInfo";

        public const string AdminRole = "Администратор";
        public const string OperatorRole = "Оператор";
        public const string AccountantRole = "Бухгалтер";
        public const string EcologistRole = "Эколог";
        public const string DirectorRole = "Руководитель предприятия";

        public const double DefaultParameterValue = 0.0d;
    }
}
