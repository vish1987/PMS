using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.Infrastructure.Migrations
{
    public partial class RemoveForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_Task",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Task",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Task",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Task",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Task",
                table: "Tasks",
                column: "Task");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_Task",
                table: "Tasks",
                column: "Task",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
