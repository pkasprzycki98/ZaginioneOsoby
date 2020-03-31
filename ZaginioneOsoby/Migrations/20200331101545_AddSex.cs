using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaginioneOsoby.Migrations
{
    public partial class AddSex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Provices",
                table: "Place",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "OsobyZaginione",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "OsobyZaginione");

            migrationBuilder.AlterColumn<string>(
                name: "Provices",
                table: "Place",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
