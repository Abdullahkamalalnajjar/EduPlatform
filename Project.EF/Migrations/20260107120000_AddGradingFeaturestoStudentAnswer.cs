using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddGradingFeaturestoStudentAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Change PointsEarned from int to decimal
            migrationBuilder.AlterColumn<decimal>(
                name: "PointsEarned",
                table: "StudentAnswers",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Add Feedback column
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "StudentAnswers",
                type: "nvarchar(max)",
                nullable: true);

            // Add GradedByUserId column
            migrationBuilder.AddColumn<string>(
                name: "GradedByUserId",
                table: "StudentAnswers",
                type: "nvarchar(450)",
                nullable: true);

            // Create index for GradedByUserId
            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_GradedByUserId",
                table: "StudentAnswers",
                column: "GradedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop index
            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_GradedByUserId",
                table: "StudentAnswers");

            // Remove columns
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "GradedByUserId",
                table: "StudentAnswers");

            // Change PointsEarned back to int
            migrationBuilder.AlterColumn<int>(
                name: "PointsEarned",
                table: "StudentAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
        }
    }
}
