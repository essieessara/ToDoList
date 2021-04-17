using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Migrations
{
    public partial class linkentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_ToDoUsersEntityUserID",
                table: "Lists");

            migrationBuilder.RenameColumn(
                name: "ToDoUsersEntityUserID",
                table: "Lists",
                newName: "UsersUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Lists_ToDoUsersEntityUserID",
                table: "Lists",
                newName: "IX_Lists_UsersUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Users_UsersUserID",
                table: "Lists",
                column: "UsersUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_UsersUserID",
                table: "Lists");

            migrationBuilder.RenameColumn(
                name: "UsersUserID",
                table: "Lists",
                newName: "ToDoUsersEntityUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Lists_UsersUserID",
                table: "Lists",
                newName: "IX_Lists_ToDoUsersEntityUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Users_ToDoUsersEntityUserID",
                table: "Lists",
                column: "ToDoUsersEntityUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
