using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GomelRectorCouncil.Migrations
{
    public partial class ChangeUniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Universities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Universities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Universities");
        }
    }
}
