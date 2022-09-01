using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.Data.Migrations
{
    public partial class changedProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Module",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Startdate",
                table: "Course",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Course",
                newName: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Module_CourseId",
                table: "Module",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module");

            migrationBuilder.DropIndex(
                name: "IX_Module_CourseId",
                table: "Module");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Module",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Course",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Course",
                newName: "Startdate");
        }
    }
}
