using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddtitleInLectureMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NationalId",
                table: "Parents",
                newName: "ParentPhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "LectureMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "LectureMaterials");

            migrationBuilder.RenameColumn(
                name: "ParentPhoneNumber",
                table: "Parents",
                newName: "NationalId");
        }
    }
}
