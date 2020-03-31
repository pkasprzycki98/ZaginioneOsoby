using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaginioneOsoby.Migrations
{
    public partial class PhotoUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "OsobyZaginione");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "OsobyZaginione",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "OsobyZaginione");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "OsobyZaginione",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
