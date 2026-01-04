using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class updateExam2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_StudentExamResults_StudentExamResultId",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "SelectedOptionIds",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "IsFinashed",
                table: "Exams");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinashed",
                table: "StudentExamResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "StudentAnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentAnswerId = table.Column<int>(type: "int", nullable: false),
                    QuestionOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAnswerOptions_QuestionOptions_QuestionOptionId",
                        column: x => x.QuestionOptionId,
                        principalTable: "QuestionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAnswerOptions_StudentAnswers_StudentAnswerId",
                        column: x => x.StudentAnswerId,
                        principalTable: "StudentAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerOption_Unique",
                table: "StudentAnswerOptions",
                columns: new[] { "StudentAnswerId", "QuestionOptionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerOptions_QuestionOptionId",
                table: "StudentAnswerOptions",
                column: "QuestionOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_StudentExamResults_StudentExamResultId",
                table: "StudentAnswers",
                column: "StudentExamResultId",
                principalTable: "StudentExamResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_StudentExamResults_StudentExamResultId",
                table: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "StudentAnswerOptions");

            migrationBuilder.DropColumn(
                name: "IsFinashed",
                table: "StudentExamResults");

            migrationBuilder.AddColumn<string>(
                name: "SelectedOptionIds",
                table: "StudentAnswers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinashed",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_StudentExamResults_StudentExamResultId",
                table: "StudentAnswers",
                column: "StudentExamResultId",
                principalTable: "StudentExamResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
