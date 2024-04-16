using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Icon3DPack.API.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initialcommit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Tags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Tags",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Tags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "Tags",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "Tags");
        }
    }
}
