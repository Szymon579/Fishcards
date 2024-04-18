using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class pull_update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19daf67d-3efe-4816-ade0-9d062b81b052");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1b56f9f-b783-4323-b587-7d198232c12a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44477ad3-7eaa-4a95-b3eb-e36cacb0c9fe", null, "User", "USER" },
                    { "4922e8bd-88e8-46f5-9b04-4f30827be3fb", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44477ad3-7eaa-4a95-b3eb-e36cacb0c9fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4922e8bd-88e8-46f5-9b04-4f30827be3fb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19daf67d-3efe-4816-ade0-9d062b81b052", null, "User", "USER" },
                    { "e1b56f9f-b783-4323-b587-7d198232c12a", null, "Admin", "ADMIN" }
                });
        }
    }
}
