using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class updateCourseAddEducatinStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GradeYear",
                table: "Courses",
                newName: "EducationStageId");

            migrationBuilder.AddColumn<string>(
                name: "SubjectImageUrl",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseImageUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EducationStageId",
                table: "Courses",
                column: "EducationStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_EducationStages_EducationStageId",
                table: "Courses",
                column: "EducationStageId",
                principalTable: "EducationStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_EducationStages_EducationStageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_EducationStageId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SubjectImageUrl",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CourseImageUrl",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "EducationStageId",
                table: "Courses",
                newName: "GradeYear");
        }
    }
}
