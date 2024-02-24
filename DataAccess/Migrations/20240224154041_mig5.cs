using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userOperationClaims",
                table: "userOperationClaims");

            migrationBuilder.RenameTable(
                name: "userOperationClaims",
                newName: "UserOperationClaims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOperationClaims",
                table: "UserOperationClaims",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOperationClaims",
                table: "UserOperationClaims");

            migrationBuilder.RenameTable(
                name: "UserOperationClaims",
                newName: "userOperationClaims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userOperationClaims",
                table: "userOperationClaims",
                column: "Id");
        }
    }
}
