using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationAPI.Migrations
{
    public partial class addrelationshiplecturepromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectureId",
                table: "Promoiton",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Promoiton",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promoiton_LectureId",
                table: "Promoiton",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promoiton_Lecture_LectureId",
                table: "Promoiton",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promoiton_Lecture_LectureId",
                table: "Promoiton");

            migrationBuilder.DropIndex(
                name: "IX_Promoiton_LectureId",
                table: "Promoiton");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "Promoiton");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Promoiton");
        }
    }
}
