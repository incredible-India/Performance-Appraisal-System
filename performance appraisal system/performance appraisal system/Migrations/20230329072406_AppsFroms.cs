using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class AppsFroms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppraiselID",
                table: "AspprasalAndCompetencies",
                newName: "AppID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppID",
                table: "AspprasalAndCompetencies",
                newName: "AppraiselID");
        }
    }
}
