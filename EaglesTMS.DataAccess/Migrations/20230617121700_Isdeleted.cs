using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EaglesTMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Isdeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Nationalities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 17, 12, 17, 0, 414, DateTimeKind.Utc).AddTicks(395),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 15, 13, 3, 1, 975, DateTimeKind.Utc).AddTicks(6047));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Jobs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 15, 13, 3, 1, 975, DateTimeKind.Utc).AddTicks(6047),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 17, 12, 17, 0, 414, DateTimeKind.Utc).AddTicks(395));
        }
    }
}
