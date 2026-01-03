using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class addStudentAnswer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentExamResultId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SelectedOptionIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAnswerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointsEarned = table.Column<int>(type: "int", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_StudentExamResults_StudentExamResultId",
                        column: x => x.StudentExamResultId,
                        principalTable: "StudentExamResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_QuestionId",
                table: "StudentAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentExamResultId",
                table: "StudentAnswers",
                column: "StudentExamResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAnswers");
        }
    }
}
