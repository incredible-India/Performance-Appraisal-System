using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class AddedAppDetaisl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appraisalDetails",
                columns: table => new
                {
                    DetialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppraisalID = table.Column<int>(type: "int", nullable: false),
                    EmployeeComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeRating = table.Column<int>(type: "int", nullable: false),
                    ManagerComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerRating = table.Column<int>(type: "int", nullable: false),
                    Competency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appraisalDetails", x => x.DetialID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appraisalDetails");
        }
    }
}
