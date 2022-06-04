using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boat_2.Migrations;

public partial class AddBoatDoTabase : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Boats",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                CrewSeats = table.Column<int>(type: "int", nullable: false),
                QualificationNeed = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                Crews = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                StartingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Boats", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Persons",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1000"),
                FullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Age = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                Qualification = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                BoatId = table.Column<int>(type: "int", nullable: false),
                Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Persons", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Boats");

        migrationBuilder.DropTable(
            name: "Persons");
    }
}
