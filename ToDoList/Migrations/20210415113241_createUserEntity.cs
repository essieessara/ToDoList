using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Migrations
{
    public partial class createUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoUsersEntityUserID",
                table: "Lists",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lists_ToDoUsersEntityUserID",
                table: "Lists",
                column: "ToDoUsersEntityUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Users_ToDoUsersEntityUserID",
                table: "Lists",
                column: "ToDoUsersEntityUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_ToDoUsersEntityUserID",
                table: "Lists");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Lists_ToDoUsersEntityUserID",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "ToDoUsersEntityUserID",
                table: "Lists");
        }
    }
}
