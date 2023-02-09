using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuaLiveJA.Migrations
{
    public partial class addclassestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Evento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Evento");
        }
    }
}
