using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class AppraisalFROMDBSFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appraisel_AspprasalAndCompetencies_AspprasalAndCompetenciesID",
                table: "Appraisel");

            migrationBuilder.DropIndex(
                name: "IX_Appraisel_AspprasalAndCompetenciesID",
                table: "Appraisel");

            migrationBuilder.DropColumn(
                name: "AspprasalAndCompetenciesID",
                table: "Appraisel");

            migrationBuilder.AddColumn<int>(
                name: "AppraiselformAID",
                table: "AspprasalAndCompetencies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspprasalAndCompetencies_AppraiselformAID",
                table: "AspprasalAndCompetencies",
                column: "AppraiselformAID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspprasalAndCompetencies_Appraisel_AppraiselformAID",
                table: "AspprasalAndCompetencies",
                column: "AppraiselformAID",
                principalTable: "Appraisel",
                principalColumn: "AID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspprasalAndCompetencies_Appraisel_AppraiselformAID",
                table: "AspprasalAndCompetencies");

            migrationBuilder.DropIndex(
                name: "IX_AspprasalAndCompetencies_AppraiselformAID",
                table: "AspprasalAndCompetencies");

            migrationBuilder.DropColumn(
                name: "AppraiselformAID",
                table: "AspprasalAndCompetencies");

            migrationBuilder.AddColumn<int>(
                name: "AspprasalAndCompetenciesID",
                table: "Appraisel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appraisel_AspprasalAndCompetenciesID",
                table: "Appraisel",
                column: "AspprasalAndCompetenciesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appraisel_AspprasalAndCompetencies_AspprasalAndCompetenciesID",
                table: "Appraisel",
                column: "AspprasalAndCompetenciesID",
                principalTable: "AspprasalAndCompetencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
