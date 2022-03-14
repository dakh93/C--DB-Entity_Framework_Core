using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamBuilder.Data.Migrations
{
    public partial class Add_Property_IsDeleted_To_Team_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teams");
        }
    }
}
