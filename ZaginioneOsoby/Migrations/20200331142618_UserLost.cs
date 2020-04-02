using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaginioneOsoby.Migrations
{
    public partial class UserLost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OsobyZaginione_AspNetUsers_UserModelId",
                table: "OsobyZaginione");

            migrationBuilder.DropIndex(
                name: "IX_OsobyZaginione_UserModelId",
                table: "OsobyZaginione");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "OsobyZaginione");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OsobyZaginione",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsobyZaginione_UserId",
                table: "OsobyZaginione",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OsobyZaginione_AspNetUsers_UserId",
                table: "OsobyZaginione",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OsobyZaginione_AspNetUsers_UserId",
                table: "OsobyZaginione");

            migrationBuilder.DropIndex(
                name: "IX_OsobyZaginione_UserId",
                table: "OsobyZaginione");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OsobyZaginione");

            migrationBuilder.AddColumn<string>(
                name: "UserModelId",
                table: "OsobyZaginione",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsobyZaginione_UserModelId",
                table: "OsobyZaginione",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OsobyZaginione_AspNetUsers_UserModelId",
                table: "OsobyZaginione",
                column: "UserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
