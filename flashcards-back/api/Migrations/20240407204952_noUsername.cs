using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class noUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ac0e6f7-a4c8-458e-bc4d-0c8411d15f0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a52e76ac-0513-4a0d-95ab-289d0dd5dcbc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a311d4d-5ef8-47d0-a107-b2fdde46ba17", null, "Admin", "ADMIN" },
                    { "76b3635b-a28e-44d1-b162-9f9e0c31dba2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "4ac0e6f7-a4c8-458e-bc4d-0c8411d15f0d", null, "Admin", "ADMIN" },
                    { "a52e76ac-0513-4a0d-95ab-289d0dd5dcbc", null, "User", "USER" }
                });
        }
    }
}
