﻿using System.Reflection.Metadata;

namespace EmisTracking.Localization
{
    public static class LangResources
    {
        public const string ErrorTitle = "Ошибка!";
        public const string Warning = "Внимание!";

        public const string ReturnButtonText = "Вернуться";
        public const string ReturnToListButtonText = "Вернуться к списку";

        public const string ToHomePageButtonText = "На домашнюю страницу";

        public const string CreateButtonText = "Создать";
        public const string UpdateButtonText = "Изменить";
        public const string SaveButtonText = "Сохранить";
        public const string DeleteButtonText = "Удалить";
        public const string CalculateButtonText = "Рассчитать";
        public const string RegisterButtonText = "Регистрация";
        public const string LoginButtonText = "Войти";
        public const string AddButtonText = "Добавить";
        public const string NoValue = "не установлено";
        public const string True = "Да";
        public const string False = "Нет";
        public const string ConfirmDeleteButtonText = "Подтвердить удаление";
        public const string ConfirmDeleteText = "Пожалуйста, подтвердите удаление";
        public const string CannotBeUndoneText = "Данное действие необратимо!";
        public const string EmptyIdText = "Поиск по пустому идентификатору невозможен!";

        public const string TypeSomething = "Введите значение";
        public const string ChooseSomething = "Выберите элемент из списка";
        public const string MustBeFilledMessage = "Поле должно быть заполнено";
        public const string MustBeChosen = "Должно быть выбрано значение из списка";
        public const string DuplicationMessage = "Запись уже существует, измените данные и повторите попытку снова!";
        public const string ItemNotFoundMessageTemplate = "Не существует {0} с Id='{1}'.";
        public const string DefaultErrorMessage = "Во время выполнения операции возникла ошибка";
        public const string ModeMethodologyCategoryHint1 = "Внимание!";
        public const string ModeMethodologyCategoryHint2 = "Установка режима для источника автоматически устанавливает категорию процесса и методику в соответствии с выбранным значением.";
        public const string ModeMethodologyCategoryHint3 = "Установка методики или категории процесса автоматически блокирует выбор режима.";
        public const string ModeMethodologyCategoryHint4 = "Если нужный режим отсутствует в списке, это значит, что ему не соответствует ни одна методика. Необходимо создать хотя бы одну.";
        public const string ModeMethodologyCategoryHint5 = "Используйте очистку поля для отмены.";
        public const string InPollutantsList = "в перечне";
        public const string ParametersNamesHint1 = "Название параметра произвольное, название в формуле - латиницей без скобок";
        public const string ParametersNamesHint2 = "Например, параметр удельного показателя, в формуле \"A*B*[Spec]\" указан как Spec";
        public const string ParameterSourceHint = "Для параметров, не относящихся к веществам источника, вещество можно не указывать";
        public const string FormulaHint = "Параметры в формуле следует записывать в квадратных скобках. Например [A]*[Mass]+[Spec1]";
        public const string IncorrectRole = "Неправильная роль";
        public const string RememberPassword = "Пожалуйста, запомните вводимый пароль. У Вас не будет другой возможности его увидеть!";
        public const string GrossEmissionCalculation = "Подсчёт валового выброса";
        public const string PleaseNotifyTechSupport = "Ошибка. Пожалуйста, обратитесь в техподдержку!";

        public const string AuthUserNotFoundErrorMessageTemplate = "Пользователь {0} не найден.";
        public const string SignInFailedErrorMessage = "Не удалось выполнить вход. Проверьте введённые логин и пароль и повторите попытку снова!";

        public const string PasswordHint = "Пароль должен иметь минимум 6 символов, среди которых должна быть\nхотя бы одна заглавная и одна прописная буквы, одна цифра,\nа также не буквенно-цифровой символ.";

        public const string EitherMethodologyAndModeMustBeSet = "Необходимо выбрать либо режим, либо методику";

        public const string BracketsBalanceError = "Квадратные скобки должны быть корректно открыты и закрыты";
        public const string DoubleOpenBracketError = "Недопустимо две открывающие скобки подряд без закрывающей между ними";

        public const string MissingParametersHint = "Следующие параметры представлены в формуле, но отсутствуют в списке ниже. Их небходимо досоздать (кликните для перехода на страницу создания)!";
        public const string ExtraParametersHint = "Следующие параметры избыточны и НЕ представлены в формуле. Они не влияют на работу системы, но возможно были упущены. Пожалуйста, проверьте список:";

        public const string MissingMethodologyPollutants = "Некоторые загрязнители отсутствуют в источнике выброса";

        public const string CalculationResults = "Результаты подсчётов";

        public const string UnknownParameterTypeTemplate = "Неизвестный тип {0} у параметра методологии с Id='{1}'";

        public const string NoApiConnectionMessage = "API сервер не отвечает. Проверьте соединение и повторите попытку, либо обратитесь в техническую поддержку!";

        public const string CantBeCalculated = "Расчёт невозможен, так как последняя проверка не завершилась успехом. Проверьте параметры или обратитесь в техническую поддержку!";

        // Названия сущностей

        public static class Entities
        {
            public const string Area = "площадка";
            public const string Subdivision = "подразделение";
            public const string Mode = "режим";
            public const string Methodology = "методика";
            public const string EmissionSource = "источник выбросов";
            public const string OperatingTime = "рабочее время";
            public const string Pollutant = "загрязняющее вещество";
            public const string SourceSubstance = "вещество источника";
            public const string MethodologyParameter = "параметр методики";
            public const string ConsumptionGroup = "расходная группа";
            public const string SpecificIndicator = "удельный показатель";
            public const string Consumption = "расход";
            public const string ParameterValue = "значение параметра";
            public const string GrossEmission = "валовый выброс";
            public const string TaxRate = "ставка налога";
            public const string Tax = "налог";
            public const string User = "пользователь";
        }

        public static class Months
        {
            public const string January = "Январь";
            public const string February = "Февраль";
            public const string March = "Март";
            public const string April = "Апрель";
            public const string May = "Май";
            public const string June = "Июнь";
            public const string July = "Июль";
            public const string August = "Август";
            public const string September = "Сентябрь";
            public const string October = "Октябрь";
            public const string November = "Ноябрь";
            public const string December = "Декабрь";
        }

        // Названия полей
        public static class Fields
        {
            public const string Area = "Площадка";
            public const string Subdivision = "Подразделение";
            public const string Mode = "Режим";
            public const string Methodology = "Методика";
            public const string EmissionSource = "Источник выбросов";
            public const string OperatingTime = "Время работы источника";
            public const string Pollutant = "Загрязняющее вещество";
            public const string SourceSubstance = "Вещество источника";
            public const string MethodologyParameter = "Параметр методики";
            public const string ConsumptionGroup = "Расходная группа";
            public const string SpecificIndicator = "Удельный показатель";
            public const string Consumption = "Расход";
            public const string ParameterValue = "Значение параметра";
            public const string GrossEmission = "Валовый выброс";
            public const string TaxRate = "Ставка налога";
            public const string Tax = "Налог";

            public const string Number = "Номер";
            public const string Name = "Название";
            public const string FormulaName = "Название в формуле";
            public const string ShortName = "Короткое название";
            public const string FullName = "Полное название";
            public const string ProcessCategory = "Категория процесса";
            public const string Formula = "Формула";
            public const string Hours = "Количество часов";
            public const string Month = "Месяц";
            public const string Year = "Год";
            public const string Period = "Период";
            public const string HazardClass = "Класс опасности";
            public const string AggregateState = "Агрегатное состояние";
            public const string IsRegulated = "Нормированность";
            public const string GasCleaningUnitType = "Газоочистная установка";
            public const string PurificationPercentage = "Процент очистки ГОУ";
            public const string AnnualAmount = "Годовое количество (кг)";
            public const string ParameterType = "Тип параметра";
            public const string Value = "Значение";
            public const string Mass = "Масса (кг)";
            public const string TaxAmount = "Сумма";
            public const string IsPaid = "Оплаченность";
            public const string StartDate = "Дата начала";
            public const string EndDate = "Дата окончания";
            public const string CalculationDate = "Дата подсчёта";
            public const string IsFilled = "Заполнен";

            // Auth & Accounts
            public const string Role = "Роль";
            public const string Nickname = "Никнейм";
            public const string Email = "Эл. почта";
            public const string Password = "Пароль";
            public const string FirstName = "Имя";
            public const string MiddleName = "Отчество";
            public const string LastName = "Фамилия";
            public const string Info = "Доп. сведения";
            public const string ConfirmPassword = "Повтор пароля";
            public const string OldPassword = "Старый пароль";
            public const string NewPassword = "Новый пароль";
        }

        public static readonly Dictionary<string, string> JqueryDatatableLang = new()
        {
            ["lengthMenuText"] = JqueryTableStrings.LengthMenuString,
            ["zeroRecordText"] = JqueryTableStrings.ZeroRecordsString,
            ["infoText"] = JqueryTableStrings.InfoString,
            ["infoEmptyText"] = JqueryTableStrings.InfoEmptyString,
            ["infoFilteredText"] = JqueryTableStrings.InfoFilteredString,
            ["emptyTableText"] = JqueryTableStrings.InfoFilteredString,
            ["loadingText"] = JqueryTableStrings.LoadingString,
            ["processingText"] = JqueryTableStrings.ProcessingString,
            ["searchText"] = JqueryTableStrings.SearchString,
            ["firstText"] = JqueryTableStrings.FirstString,
            ["lastText"] = JqueryTableStrings.LastString,
            ["nextText"] = JqueryTableStrings.NextString,
            ["previousText"] = JqueryTableStrings.PreviousString,
            ["ascText"] = JqueryTableStrings.ActivateAscending,
            ["descText"] = JqueryTableStrings.ActivateDescending,
        };

        public static class EnumTranslations
        {
            public const string ProcessCategoryNone = "отсутствует";
            public const string ProcessCategoryBurningFuel = "Сжигание топлива";
            public const string ProcessCategoryWasteProcessing = "Использование отходов";
            public const string ProcessCategoryAgriculturalObject = "Сельскохозяйственный объект";
            public const string ProcessCategoryTechnologicalProcess = "Технологический объект";

            public const string ParameterTypeNumeric = "Числовой";
            public const string ParameterTypeGasCleaningUnitPercent = "Процент очистки ГОУ";
            public const string ParameterTypeConsumptionMass = "Расход материала";
            public const string ParameterTypeOperatingTime = "Время работы источника";
            public const string ParameterTypeSpecificIndicator = "Удельный показатель";

            public const string GasCleaningUnitTypeYes = "Да";
            public const string GasCleaningUnitTypeNo = "Нет";
            public const string GasCleaningUnitTypeOther = "Иные устройства";

            public const string HazardClassI = "I";
            public const string HazardClassII = "II";
            public const string HazardClassIII = "III";
            public const string HazardClassIV = "IV";

            public const string AggregateStateSolid = "Твёрдое";
            public const string AggregateStateLiquid = "Жидкое";
            public const string AggregateStateGas = "Газообразное";
        }

        public static class JqueryTableStrings
        {
            public static string ActivateAscending => ": активируйте для сортировки по возр.";
            public static string ActivateDescending => ": активируйте для сортировки по убыв.";
            public static string EmptyTableString => "Ничего нет";
            public static string FirstString => "Первая";
            public static string InfoEmptyString => "Показано 0 из 0 эл.";
            public static string InfoFilteredString => "Ничего не найдено :(";
            public static string InfoString => "Показано _START_ - _END_ из _TOTAL_ эл.";
            public static string LastString => "Последняя";
            public static string LengthMenuString => "_MENU_ эл. на странице";
            public static string LoadingString => "Загрузка...";
            public static string NextString => "След.";
            public static string PreviousString => "Пред.";
            public static string ProcessingString => "Обработка...";
            public static string SearchString => "Поиск";
            public static string ZeroRecordsString => "Ничего не найдено :(";
        }

        public static class Titles
        {
            public const string HomeIndex = "Главная";
            public const string NameTemplate = "\"{0}\"";

            // Auth
            public const string AuthLogin = "Вход в учётную запись";
            public const string AuthRegistration = "Регистрация";
            public const string AuthChangePassword = "Смена пароля";

            // Users
            public const string UsersList = "Список пользователей";
            public const string UsersIndex = "Пользователи";
            public const string UsersCreate = "Регистрация пользователя";
            public const string UsersUpdate = "Обновление пользователя";

            // Areas
            public const string AreasList = "Список площадок";
            public const string AreasIndex = "Площадки и подразделения";
            public const string AreasCreate = "Добавление площадки";
            public const string AreasUpdate = "Изменение площадки";

            // Subdivisions
            public const string SubdivisionsList = "Список подразделений";
            public const string SubdivisionsIndex = "Подразделения";
            public const string SubdivisionsCreate = "Добавление подразделения";
            public const string SubdivisionsUpdate = "Изменение подразделения";

            // Modes
            public const string ModesList = "Список режимов";
            public const string ModesIndex = "Режимы";
            public const string ModesCreate = "Добавление режима";
            public const string ModesUpdate = "Изменение режима";

            // Methodologies
            public const string MethodologiesList = "Список методик";
            public const string MethodologiesIndex = "Методики";
            public const string MethodologiesCreate = "Добавление методики";
            public const string MethodologiesUpdate = "Изменение методики";

            // Emission Sources
            public const string EmissionSourcesList = "Список источников выбросов";
            public const string EmissionSourcesIndex = "Источники выбросов";
            public const string EmissionSourcesBySubdivision = "Источники выбросов подразделения";
            public const string EmissionSourcesCreate = "Добавление источника выбросов";
            public const string EmissionSourcesUpdate = "Изменение источника выбросов";

            // Operating Time
            public const string OperatingTimesList = "Список времени работы";
            public const string OperatingTimesIndex = "Время работы";
            public const string OperatingTimesBySourceIndex = "Время работы источника \"{0}\"";
            public const string OperatingTimesCreate = "Учёт времени работы";
            public const string OperatingTimesUpdate = "Обновление времени работы";

            // Pollutants
            public const string PollutantsList = "Список загрязняющих веществ";
            public const string PollutantsIndex = "Загрязняющие вещества";
            public const string PollutantsCreate = "Добавление загрязняющего вещества";
            public const string PollutantsUpdate = "Изменение загрязняющего вещества";

            // Source Substances
            public const string SourceSubstancesList = "Список веществ";
            public const string SourceSubstancesIndex = "Вещества источников";
            public const string SourceSubstancesCreate = "Добавление вещества источника";
            public const string SourceSubstancesUpdate = "Изменение вещества источника";

            // Methodology Parameters
            public const string MethodologyParametersList = "Список параметров методики";
            public const string MethodologyParametersIndex = "Параметры методики";
            public const string MethodologyParametersCreate = "Добавление параметра методики";
            public const string MethodologyParametersUpdate = "Изменение параметра методики";

            // Consumption Groups
            public const string ConsumptionGroupsList = "Список расходных групп";
            public const string ConsumptionGroupsIndex = "Расходные группы";
            public const string ConsumptionGroupsCreate = "Добавление расходной группы";
            public const string ConsumptionGroupsUpdate = "Изменение расходной группы";

            // Specific Indicators
            public const string SpecificIndicatorsList = "Список удельных показателей";
            public const string SpecificIndicatorsIndex = "Удельные показатели";
            public const string SpecificIndicatorsCreate = "Добавление удельного показателя";
            public const string SpecificIndicatorsUpdate = "Изменение удельного показателя";

            // Consumptions
            public const string ConsumptionsList = "Список расходов";
            public const string ConsumptionsIndex = "Расходы";
            public const string ConsumptionsCreate = "Добавление расхода";
            public const string ConsumptionsUpdate = "Изменение расхода";

            // Parameter Values
            public const string ParameterValuesList = "Список значений параметров";
            public const string ParameterValuesIndex = "Значения параметров";
            public const string ParameterValuesCreate = "Добавление значения параметра";
            public const string ParameterValuesUpdate = "Изменение значения параметра";

            // Gross Emissions
            public const string GrossEmissionsList = "Список валовых выбросов";
            public const string GrossEmissionsIndex = "Валовые выбросы";
            public const string GrossEmissionsCreate = "Добавление валового выброса";
            public const string GrossEmissionsUpdate = "Изменение валового выброса";

            // Tax Rates and Taxes
            public const string TaxRatesList = "Список ставок налога";
            public const string TaxRatesIndex = "Ставки налога";
            public const string TaxRatesCreate = "Добавление ставки налога";
            public const string TaxRatesUpdate = "Изменение ставки налога";

            public const string TaxesList = "Список налогов";
            public const string TaxesIndex = "Налоги";
            public const string TaxesCreate = "Добавление налога";
            public const string TaxesUpdate = "Изменение налога";
        }
    }
}