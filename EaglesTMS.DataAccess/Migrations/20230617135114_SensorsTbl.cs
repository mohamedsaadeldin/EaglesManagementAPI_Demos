using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EaglesTMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SensorsTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 17, 13, 51, 14, 817, DateTimeKind.Utc).AddTicks(732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 17, 12, 17, 0, 414, DateTimeKind.Utc).AddTicks(395));

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 17, 12, 17, 0, 414, DateTimeKind.Utc).AddTicks(395),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 17, 13, 51, 14, 817, DateTimeKind.Utc).AddTicks(732));
        }
    }
}
