using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class AppraisalFROMDBS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspprasalAndCompetencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppraiselID = table.Column<int>(type: "int", nullable: false),
                    Compitency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspprasalAndCompetencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Appraisel",
                columns: table => new
                {
                    AID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    AspprasalAndCompetenciesID = table.Column<int>(type: "int", nullable: false),
                    objective = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisel", x => x.AID);
                    table.ForeignKey(
                        name: "FK_Appraisel_AspprasalAndCompetencies_AspprasalAndCompetenciesID",
                        column: x => x.AspprasalAndCompetenciesID,
                        principalTable: "AspprasalAndCompetencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appraisel_AspprasalAndCompetenciesID",
                table: "Appraisel",
                column: "AspprasalAndCompetenciesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appraisel");

            migrationBuilder.DropTable(
                name: "AspprasalAndCompetencies");
        }
    }
}
