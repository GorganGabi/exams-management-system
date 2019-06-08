using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Persistence.Migrations
{
    public partial class CourseProfOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses",
                column: "ProfessorId",
                unique: true);
        }
    }
}
