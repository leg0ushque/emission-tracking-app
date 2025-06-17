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

        public const double Epsilon = 0.00001; // для сравнений значений массы
        public const double ZeroMass = 0.0d;
    }
}
