using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class newDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a311d4d-5ef8-47d0-a107-b2fdde46ba17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76b3635b-a28e-44d1-b162-9f9e0c31dba2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "88bf886e-1303-4e7d-b545-f7ded846fb0d", null, "User", "USER" },
                    { "f9ec8993-ea90-47d2-a62b-0a5aeadc2271", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88bf886e-1303-4e7d-b545-f7ded846fb0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9ec8993-ea90-47d2-a62b-0a5aeadc2271");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a311d4d-5ef8-47d0-a107-b2fdde46ba17", null, "Admin", "ADMIN" },
                    { "76b3635b-a28e-44d1-b162-9f9e0c31dba2", null, "User", "USER" }
                });
        }
    }
}
