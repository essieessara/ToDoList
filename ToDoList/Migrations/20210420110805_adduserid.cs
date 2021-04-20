using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Migrations
{
    public partial class adduserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_UsersUserID",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_UsersUserID",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "UsersUserID",
                table: "Lists");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Lists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lists_UserID",
                table: "Lists",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Users_UserID",
                table: "Lists",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_UserID",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_UserID",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Lists");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserID",
                table: "Lists",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lists_UsersUserID",
                table: "Lists",
                column: "UsersUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Users_UsersUserID",
                table: "Lists",
                column: "UsersUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
