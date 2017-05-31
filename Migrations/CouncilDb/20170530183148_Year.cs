using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GomelRectorCouncil.Migrations.CouncilDb
{
    public partial class Year : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rectors_UniversityId",
                table: "Rectors");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Achievements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rectors_UniversityId",
                table: "Rectors",
                column: "UniversityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rectors_UniversityId",
                table: "Rectors");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Achievements");

            migrationBuilder.CreateIndex(
                name: "IX_Rectors_UniversityId",
                table: "Rectors",
                column: "UniversityId");
        }
    }
}
