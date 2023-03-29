using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class RenameAppForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Appraisel",
                table: "Appraisel");

            migrationBuilder.RenameTable(
                name: "Appraisel",
                newName: "AppraiselForm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppraiselForm",
                table: "AppraiselForm",
                column: "AID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppraiselForm",
                table: "AppraiselForm");

            migrationBuilder.RenameTable(
                name: "AppraiselForm",
                newName: "Appraisel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appraisel",
                table: "Appraisel",
                column: "AID");
        }
    }
}
