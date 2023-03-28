using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class compitencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "competencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetencyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_competencies", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "competencies");
        }
    }
}
