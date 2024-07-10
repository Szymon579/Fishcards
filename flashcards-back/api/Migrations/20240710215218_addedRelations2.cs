using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addedRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fa5eb2a-9463-4178-a837-624804c6fd70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db308ef6-cc10-49a9-aaa6-1f6f3b1720e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8a2724ff-8842-4cc7-9294-b59faea8e6f6", null, "User", "USER" },
                    { "cad0ffd6-0bdb-49c2-85d1-6d47d007480b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a2724ff-8842-4cc7-9294-b59faea8e6f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cad0ffd6-0bdb-49c2-85d1-6d47d007480b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8fa5eb2a-9463-4178-a837-624804c6fd70", null, "User", "USER" },
                    { "db308ef6-cc10-49a9-aaa6-1f6f3b1720e6", null, "Admin", "ADMIN" }
                });
        }
    }
}
