using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationAPI.Migrations
{
    public partial class addrelationshiplecturepromotionfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Promoiton");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Promoiton",
                type: "int",
                nullable: true);
        }
    }
}
