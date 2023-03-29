using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class AppraisalFROMDBSFKa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "AppraiselID",
                table: "AspprasalAndCompetencies",
                newName: "AppraisalformAID");

            migrationBuilder.AlterColumn<string>(
                name: "objective",
                table: "Appraisel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AppraiselformAID",
                table: "AspprasalAndCompetencies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "objective",
                table: "Appraisel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
