using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Persistence.Migrations
{
    public partial class StudentExamEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StudentExam",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentExam");
        }
    }
}
