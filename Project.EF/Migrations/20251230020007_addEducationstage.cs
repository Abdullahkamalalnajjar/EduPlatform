using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class addEducationstage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEducationStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    EducationStageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEducationStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherEducationStages_EducationStages_EducationStageId",
                        column: x => x.EducationStageId,
                        principalTable: "EducationStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherEducationStages_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EducationStages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "???? ????? ?????????" },
                    { 2, "???? ?????? ?????????" },
                    { 3, "???? ?????? ?????????" },
                    { 4, "???? ?????? ?????????" },
                    { 5, "???? ?????? ?????????" },
                    { 6, "???? ?????? ?????????" },
                    { 7, "???? ????? ????????" },
                    { 8, "???? ?????? ????????" },
                    { 9, "???? ?????? ????????" },
                    { 10, "???? ????? ???????" },
                    { 11, "???? ?????? ???????" },
                    { 12, "???? ?????? ???????" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEducationStages_EducationStageId",
                table: "TeacherEducationStages",
                column: "EducationStageId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEducationStages_TeacherId_EducationStageId",
                table: "TeacherEducationStages",
                columns: new[] { "TeacherId", "EducationStageId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherEducationStages");

            migrationBuilder.DropTable(
                name: "EducationStages");
        }
    }
}
