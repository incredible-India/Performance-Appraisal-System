using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace performance_appraisal_system.Migrations
{
    public partial class HRREMOVED : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Manager_MID",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "HR");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropIndex(
                name: "IX_Employees_MID",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EID",
                table: "Employees",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Employees",
                newName: "EID");

            migrationBuilder.CreateTable(
                name: "HR",
                columns: table => new
                {
                    HRID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR", x => x.HRID);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    MID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.MID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MID",
                table: "Employees",
                column: "MID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Manager_MID",
                table: "Employees",
                column: "MID",
                principalTable: "Manager",
                principalColumn: "MID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
