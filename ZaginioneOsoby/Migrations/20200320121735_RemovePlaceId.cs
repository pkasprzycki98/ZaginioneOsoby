using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaginioneOsoby.Migrations
{
    public partial class RemovePlaceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OsobyZaginione_Place_PlaceId1",
                table: "OsobyZaginione");

            migrationBuilder.DropIndex(
                name: "IX_OsobyZaginione_PlaceId1",
                table: "OsobyZaginione");

            migrationBuilder.DropColumn(
                name: "PlaceId1",
                table: "OsobyZaginione");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "OsobyZaginione",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_OsobyZaginione_PlaceId",
                table: "OsobyZaginione",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OsobyZaginione_Place_PlaceId",
                table: "OsobyZaginione",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OsobyZaginione_Place_PlaceId",
                table: "OsobyZaginione");

            migrationBuilder.DropIndex(
                name: "IX_OsobyZaginione_PlaceId",
                table: "OsobyZaginione");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "OsobyZaginione",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceId1",
                table: "OsobyZaginione",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsobyZaginione_PlaceId1",
                table: "OsobyZaginione",
                column: "PlaceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OsobyZaginione_Place_PlaceId1",
                table: "OsobyZaginione",
                column: "PlaceId1",
                principalTable: "Place",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
