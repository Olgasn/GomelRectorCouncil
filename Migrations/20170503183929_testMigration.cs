﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GomelRectorCouncil.Migrations
{
    public partial class testMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                {
                    IndicatorId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    IndicatorDescription = table.Column<string>(nullable: true),
                    IndicatorId1 = table.Column<byte>(nullable: false),
                    IndicatorId2 = table.Column<byte>(nullable: true),
                    IndicatorId3 = table.Column<byte>(nullable: true),
                    IndicatorName = table.Column<string>(nullable: true),
                    IndicatorType = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.IndicatorId);
                });

            migrationBuilder.CreateTable(
                name: "Rectors",
                columns: table => new
                {
                    RectorId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    FirstMidName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 60, nullable: false),
                    UniversityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rectors", x => x.RectorId);
                });

            migrationBuilder.CreateTable(
                name: "Chairpersons",
                columns: table => new
                {
                    ChairpersonId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    RectorID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StopDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairpersons", x => x.ChairpersonId);
                    table.ForeignKey(
                        name: "FK_Chairpersons_Rectors_RectorID",
                        column: x => x.RectorID,
                        principalTable: "Rectors",
                        principalColumn: "RectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    UniversityId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    RectorId = table.Column<int>(nullable: false),
                    UniversityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.UniversityId);
                    table.ForeignKey(
                        name: "FK_Universities_Rectors_RectorId",
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
                        .Annotation("Autoincrement", true),
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

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    IndicatorId = table.Column<int>(nullable: false),
                    IndicatorValue = table.Column<float>(nullable: false),
                    UnivercityId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_IndicatorId",
                table: "Achievements",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_UnivercityId",
                table: "Achievements",
                column: "UnivercityId");

            migrationBuilder.CreateIndex(
                name: "IX_Chairpersons_RectorID",
                table: "Chairpersons",
                column: "RectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ChairpersonId",
                table: "Documents",
                column: "ChairpersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Universities_RectorId",
                table: "Universities",
                column: "RectorId",
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
                name: "Universities");

            migrationBuilder.DropTable(
                name: "Chairpersons");

            migrationBuilder.DropTable(
                name: "Rectors");
        }
    }
}
