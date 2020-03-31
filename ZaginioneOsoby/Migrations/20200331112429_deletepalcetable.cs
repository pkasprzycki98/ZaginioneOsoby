using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaginioneOsoby.Migrations
{
    public partial class deletepalcetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OsobyZaginione_Place_PlaceId",
                table: "OsobyZaginione");

            migrationBuilder.DropIndex(
                name: "IX_OsobyZaginione_PlaceId",
                table: "OsobyZaginione");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "Provices",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "OsobyZaginione");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OsobyZaginione",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Provices",
                table: "OsobyZaginione",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "OsobyZaginione",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "OsobyZaginione");

            migrationBuilder.DropColumn(
                name: "Provices",
                table: "OsobyZaginione");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "OsobyZaginione");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Place",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Provices",
                table: "Place",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Place",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "OsobyZaginione",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
