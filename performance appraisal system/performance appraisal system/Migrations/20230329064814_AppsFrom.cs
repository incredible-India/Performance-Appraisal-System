using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class AppsFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspprasalAndCompetencies_Appraisel_AppraisalformAID",
                table: "AspprasalAndCompetencies");

            migrationBuilder.DropIndex(
                name: "IX_AspprasalAndCompetencies_AppraisalformAID",
                table: "AspprasalAndCompetencies");

            migrationBuilder.RenameColumn(
                name: "AppraisalformAID",
                table: "AspprasalAndCompetencies",
                newName: "AppraiselID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppraiselID",
                table: "AspprasalAndCompetencies",
                newName: "AppraisalformAID");

            migrationBuilder.CreateIndex(
                name: "IX_AspprasalAndCompetencies_AppraisalformAID",
                table: "AspprasalAndCompetencies",
                column: "AppraisalformAID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspprasalAndCompetencies_Appraisel_AppraisalformAID",
                table: "AspprasalAndCompetencies",
                column: "AppraisalformAID",
                principalTable: "Appraisel",
                principalColumn: "AID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
