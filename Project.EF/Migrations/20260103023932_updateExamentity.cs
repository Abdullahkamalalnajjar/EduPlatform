using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class updateExamentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Exams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DurationInMinutes",
                table: "Exams",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinashed",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "IsFinashed",
                table: "Exams");
        }
    }
}
