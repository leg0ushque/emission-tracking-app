using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmisTracking.Services.Database.Migrations.EmissionDb
{
    /// <inheritdoc />
    public partial class InitialEdbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pollutants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HazardClass = table.Column<int>(type: "int", nullable: false),
                    AggregateState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pollutants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    HazardClass = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    HazardClass = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SystemUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subdivisions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AreaId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subdivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subdivisions_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Methodologies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Formula = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methodologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Methodologies_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    MethodologyId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumptionGroups_Methodologies_MethodologyId",
                        column: x => x.MethodologyId,
                        principalTable: "Methodologies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmissionSources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubdivisionId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessCategory = table.Column<int>(type: "int", nullable: false),
                    MethodologyId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    ModeId = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmissionSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmissionSources_Methodologies_MethodologyId",
                        column: x => x.MethodologyId,
                        principalTable: "Methodologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmissionSources_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmissionSources_Subdivisions_SubdivisionId",
                        column: x => x.SubdivisionId,
                        principalTable: "Subdivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MethodologyParameters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    MethodologyId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ParameterType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormulaName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodologyParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodologyParameters_Methodologies_MethodologyId",
                        column: x => x.MethodologyId,
                        principalTable: "Methodologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consumptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ConsumptionGroupId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumptions_ConsumptionGroups_ConsumptionGroupId",
                        column: x => x.ConsumptionGroupId,
                        principalTable: "ConsumptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecificIndicators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumptionGroupId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    PollutantId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecificIndicators_ConsumptionGroups_ConsumptionGroupId",
                        column: x => x.ConsumptionGroupId,
                        principalTable: "ConsumptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecificIndicators_Pollutants_PollutantId",
                        column: x => x.PollutantId,
                        principalTable: "Pollutants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperatingTimes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmissionSourceId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatingTimes_EmissionSources_EmissionSourceId",
                        column: x => x.EmissionSourceId,
                        principalTable: "EmissionSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SourceSubstances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmissionSourceId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    PollutantId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    IsRegulated = table.Column<bool>(type: "bit", nullable: false),
                    GasCleaningUnitType = table.Column<int>(type: "int", nullable: false),
                    PurificationPercentage = table.Column<double>(type: "float", nullable: false),
                    AnnualAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceSubstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceSubstances_EmissionSources_EmissionSourceId",
                        column: x => x.EmissionSourceId,
                        principalTable: "EmissionSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SourceSubstances_Pollutants_PollutantId",
                        column: x => x.PollutantId,
                        principalTable: "Pollutants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrossEmissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SourceSubstanceId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    MethodologyId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrossEmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrossEmissions_Methodologies_MethodologyId",
                        column: x => x.MethodologyId,
                        principalTable: "Methodologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrossEmissions_SourceSubstances_SourceSubstanceId",
                        column: x => x.SourceSubstanceId,
                        principalTable: "SourceSubstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrossEmissions_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParameterValues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    MethodologyParameterId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    GrossEmissionId = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterValues_GrossEmissions_GrossEmissionId",
                        column: x => x.GrossEmissionId,
                        principalTable: "GrossEmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParameterValues_MethodologyParameters_MethodologyParameterId",
                        column: x => x.MethodologyParameterId,
                        principalTable: "MethodologyParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionGroups_MethodologyId",
                table: "ConsumptionGroups",
                column: "MethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_ConsumptionGroupId",
                table: "Consumptions",
                column: "ConsumptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmissionSources_MethodologyId",
                table: "EmissionSources",
                column: "MethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmissionSources_ModeId",
                table: "EmissionSources",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmissionSources_SubdivisionId",
                table: "EmissionSources",
                column: "SubdivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_GrossEmissions_MethodologyId",
                table: "GrossEmissions",
                column: "MethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_GrossEmissions_SourceSubstanceId",
                table: "GrossEmissions",
                column: "SourceSubstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_GrossEmissions_TaxId",
                table: "GrossEmissions",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Methodologies_ModeId",
                table: "Methodologies",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodologyParameters_MethodologyId",
                table: "MethodologyParameters",
                column: "MethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingTimes_EmissionSourceId",
                table: "OperatingTimes",
                column: "EmissionSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_GrossEmissionId",
                table: "ParameterValues",
                column: "GrossEmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_MethodologyParameterId",
                table: "ParameterValues",
                column: "MethodologyParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceSubstances_EmissionSourceId",
                table: "SourceSubstances",
                column: "EmissionSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceSubstances_PollutantId",
                table: "SourceSubstances",
                column: "PollutantId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificIndicators_ConsumptionGroupId",
                table: "SpecificIndicators",
                column: "ConsumptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificIndicators_PollutantId",
                table: "SpecificIndicators",
                column: "PollutantId");

            migrationBuilder.CreateIndex(
                name: "IX_Subdivisions_AreaId",
                table: "Subdivisions",
                column: "AreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumptions");

            migrationBuilder.DropTable(
                name: "OperatingTimes");

            migrationBuilder.DropTable(
                name: "ParameterValues");

            migrationBuilder.DropTable(
                name: "SpecificIndicators");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GrossEmissions");

            migrationBuilder.DropTable(
                name: "MethodologyParameters");

            migrationBuilder.DropTable(
                name: "ConsumptionGroups");

            migrationBuilder.DropTable(
                name: "SourceSubstances");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "EmissionSources");

            migrationBuilder.DropTable(
                name: "Pollutants");

            migrationBuilder.DropTable(
                name: "Methodologies");

            migrationBuilder.DropTable(
                name: "Subdivisions");

            migrationBuilder.DropTable(
                name: "Modes");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
