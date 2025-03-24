namespace EmisTracking.Localization
{
    public static class LangResources
    {
        public const string ItemCannotBeNullMessage = "Модель не может быть пустый";

        public const string DuplicationMessage = "Запись уже существует, измените данные и повторите попытку снова!";

        public const string ItemNotFoundMessageTemplate = "Не существует {0} с Id='{1}'.";

        // Авторизация

        public const string PasswordMismatchError = "Пароли не совпадают";
        public const string ChangePasswordError = "Ошибка при смене пароля. Убедитесь, что старый пароль корректный, а новый пароль удовлетворяет требованиям безопасности";

        // Сущности

        public const string Area = "площадка";
        public const string Department = "подразделение";
        public const string EmissionSource = "источник выбросов";
        public const string Regime = "режим";
    }
}
