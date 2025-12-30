using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class updateQuestionClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CorrectByAssistant",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectByAssistant",
                table: "Questions");
        }
    }
}
