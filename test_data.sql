-- 1. Таблица: Modes (Режимы)
INSERT INTO Modes (Id, Name, ProcessCategory) VALUES
('57AFAE1E-5B0A-4D65-B65A-FD1DDF0E1A1A', N'Нормальный режим (кат. - тех.процесс)', 4), -- TechnologicalProcess
('6CCF7D9D-4367-446D-8AAB-F2FFAE1C4D3F', N'Режим выхода на мощность (кат. - тех.процесс)', 4), -- TechnologicalProcess
('C87BAE39-F85F-4368-9C8B-5E64F269F7F6', N'Режим работы в резерве (кат. - с\х об.)', 3), -- AgriculturalObject
('E4ABF713-3AF7-45E3-A514-6A57E08A2E9C', N'Аварийный режим (кат. - сж. топ.)', 1), -- BurningFuel
('84FB4E6A-C03D-4209-A3A1-2C6FA9B21E2F', N'Режим техобслуживания (кат. - исп.отх.)', 2); -- WasteProcessing

-- 2. Таблица: Areas (Площадки)
INSERT INTO Areas (Id, Number, Name) VALUES
('1B59C632-9645-4B3A-9FC2-7EF38F1A6296', 1, N'Главная площадка'),
('2E542D27-7A17-4B41-BB6E-22E095CF793E', 2, N'Северная площадка'),
('368F7A53-1343-4248-ADF1-072316B4C412', 3, N'Южная площадка'),
('5D17B64A-3D9B-4272-9502-45AB98B6FFD2', 4, N'Экспериментальная площадка');

-- 3. Таблица: Subdivisions (Подразделения)
INSERT INTO Subdivisions (Id, AreaId, Name) VALUES
('F25E8DFF-352A-4DF6-9A34-A6BB04CCE3B1', '1B59C632-9645-4B3A-9FC2-7EF38F1A6296', N'Кузнечно-прессовый цех'),
('8A4CA398-2DC0-42EF-80F1-C3AE3B147911', '1B59C632-9645-4B3A-9FC2-7EF38F1A6296', N'Термический цех'),
('C983F45B-3474-429B-85C4-F1618BFF5DDE', '2E542D27-7A17-4B41-BB6E-22E095CF793E', N'Цех подготовки поверхностей'),
('B1B4D3C7-6067-4D81-BD51-6823E3A90AC6', '368F7A53-1343-4248-ADF1-072316B4C412', N'Научно-исследовательский отдел');

-- 4. Таблица: Pollutants (Загрязняющие вещества)
INSERT INTO Pollutants (Id, Name, Formula, HazardClass, AggregateState) VALUES
('25B8F597-D911-461A-A831-5796578ED9D3', N'Диоксид углерода', N'CO2', 4, 3), -- Газ
('980319E5-F187-4B1B-98E0-213E9B3ABB3C', N'Сернистый ангидрид', N'SO2', 2, 3), -- Газ
('3C1E195D-DEB3-4DE0-A403-85C914FAC186', N'Пыль металлов', N'Metal Dust', 3, 1), -- Твёрдое
('531F60EC-5711-4D13-9050-34306BC4F33F', N'Оксид азота', N'NOx', 3, 3), -- Газ
('513C02AD-2FC3-4545-B765-661F7E001C89', N'Ацетон', N'C3H6O', 3, 2); -- Жидкость

-- 5. Таблица: ConsumptionGroups (Группы потребления)
-- INSERT INTO ConsumptionGroups (Id, MethodologyId, Name) VALUES
-- ('GUID', 'GUID', N'Значение');

-- 6. Таблица: Consumptions (Потребления)
-- INSERT INTO Consumptions (Id, ConsumptionGroupId, Mass, Month, Year) VALUES
-- ('GUID', 'GUID', 0.0, 1, 2023);

-- 7. Таблица: EmissionSources (Источники выбросов)
-- INSERT INTO EmissionSources (Id, SubdivisionId, Name, ProcessCategory, MethodologyId, ModeId) VALUES
-- ('GUID', 'GUID', N'Значение', 0, 'GUID', 'GUID');

-- 8. Таблица: GrossEmissions (Общие выбросы)
-- INSERT INTO GrossEmissions (Id, SourceSubstanceId, MethodologyId, Mass, Month, Year, TaxId) VALUES
-- ('GUID', 'GUID', 'GUID', 0.0, 1, 2023, 'GUID');

-- 9. Таблица: Methodologies (Методики)
-- INSERT INTO Methodologies (Id, Name, ShortName, Formula, ModeId) VALUES
-- ('GUID', N'Методика 1', N'M1', N'Формула 1', 'GUID');

-- 10. Таблица: MethodologyParameters (Параметры методик)
-- INSERT INTO MethodologyParameters (Id, MethodologyId, Name, FormulaName, ParameterType) VALUES
-- ('GUID', 'GUID', N'Значение', N'Формула', 0);

-- 11. Таблица: OperatingTimes (Время работы)
-- INSERT INTO OperatingTimes (Id, EmissionSourceId, Month, Year, Hours) VALUES
-- ('GUID', 'GUID', 1, 2023, 0);

-- 12. Таблица: ParameterValues (Значения параметров)
-- INSERT INTO ParameterValues (Id, MethodologyParameterId, Month, Year, Value, GrossEmissionId) VALUES
-- ('GUID', 'GUID', 1, 2023, 0.0, 'GUID');

-- 13. Таблица: SourceSubstances (Источники веществ)
-- INSERT INTO SourceSubstances (Id, EmissionSourceId, PollutantId, IsRegulated, GasCleaningUnit, PurificationPercentage) VALUES
-- ('GUID', 'GUID', 'GUID', 1, 0, 0.0);

-- 14. Таблица: SpecificIndicators (Конкретные показатели)
-- INSERT INTO SpecificIndicators (Id, PollutionGroupId, Value) VALUES
-- ('GUID', 'GUID', 0.0);

-- 15. Таблица: TaxRates (Ставки налогов)
-- INSERT INTO TaxRates (Id, HazardClass, Amount, StartDate, EndDate) VALUES
-- ('GUID', 1, 0.0, '2023-01-01', '2023-12-31');

-- 16. Таблица: Taxes (Налоги)
-- INSERT INTO Taxes (Id, HazardClass, Month, Year, TotalAmount, IsPaid) VALUES
-- ('GUID', 1, 1, 2023, 0.0, 0);