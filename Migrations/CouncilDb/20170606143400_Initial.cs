using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GomelRectorCouncil.Migrations.CouncilDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                {
                    IndicatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndicatorDescription = table.Column<string>(nullable: true),
                    IndicatorId1 = table.Column<byte>(nullable: false),
                    IndicatorId2 = table.Column<byte>(nullable: true),
                    IndicatorId3 = table.Column<byte>(nullable: true),
                    IndicatorName = table.Column<string>(nullable: true),
                    IndicatorType = table.Column<int>(nullable: true),
                    IndicatorUnit = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.IndicatorId);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    UniversityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    UniversityName = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndicatorId = table.Column<int>(nullable: false),
                    IndicatorValue = table.Column<float>(nullable: false),
                    UnivercityId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_Achievements_Indicators_IndicatorId",
                        column: x => x.IndicatorId,
                        principalTable: "Indicators",
                        principalColumn: "IndicatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achievements_Universities_UnivercityId",
                        column: x => x.UnivercityId,
                        principalTable: "Universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rectors",
                columns: table => new
                {
                    RectorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    FirstMidName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 60, nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    UniversityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rectors", x => x.RectorId);
                    table.ForeignKey(
                        name: "FK_Rectors_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chairpersons",
                columns: table => new
                {
                    ChairpersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RectorId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StopDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairpersons", x => x.ChairpersonId);
                    table.ForeignKey(
                        name: "FK_Chairpersons_Rectors_RectorId",
                        column: x => x.RectorId,
                        principalTable: "Rectors",
                        principalColumn: "RectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChairpersonId = table.Column<int>(nullable: false),
                    DocumentDescription = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    DocumentURL = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    RegistrationNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Chairpersons_ChairpersonId",
                        column: x => x.ChairpersonId,
                        principalTable: "Chairpersons",
                        principalColumn: "ChairpersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_IndicatorId",
                table: "Achievements",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_UnivercityId",
                table: "Achievements",
                column: "UnivercityId");

            migrationBuilder.CreateIndex(
                name: "IX_Chairpersons_RectorId",
                table: "Chairpersons",
                column: "RectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ChairpersonId",
                table: "Documents",
                column: "ChairpersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Rectors_UniversityId",
                table: "Rectors",
                column: "UniversityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Indicators");

            migrationBuilder.DropTable(
                name: "Chairpersons");

            migrationBuilder.DropTable(
                name: "Rectors");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
