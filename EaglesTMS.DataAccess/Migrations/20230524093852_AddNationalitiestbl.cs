using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EaglesTMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalitiestbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nicename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    iso3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numcode = table.Column<short>(type: "smallint", nullable: false),
                    phonecode = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nationalities");
        }
    }
}
