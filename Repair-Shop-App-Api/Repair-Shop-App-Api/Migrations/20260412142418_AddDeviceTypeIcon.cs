using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repair_Shop_App_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceTypeIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "DeviceTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Icon",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "DeviceTypes");
        }
    }
}
