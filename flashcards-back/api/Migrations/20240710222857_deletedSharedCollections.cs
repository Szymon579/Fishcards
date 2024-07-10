using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class deletedSharedCollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedCollections");

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
                    { "13a16c5b-a247-4966-b7ac-1ef429b49111", null, "User", "USER" },
                    { "f14e7f76-cbdd-4742-86be-5a81f36a84e7", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13a16c5b-a247-4966-b7ac-1ef429b49111");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f14e7f76-cbdd-4742-86be-5a81f36a84e7");

            migrationBuilder.CreateTable(
                name: "SharedCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    SharedWithUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedCollections_AspNetUsers_SharedWithUserId",
                        column: x => x.SharedWithUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8a2724ff-8842-4cc7-9294-b59faea8e6f6", null, "User", "USER" },
                    { "cad0ffd6-0bdb-49c2-85d1-6d47d007480b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedCollections_CollectionId",
                table: "SharedCollections",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedCollections_SharedWithUserId",
                table: "SharedCollections",
                column: "SharedWithUserId");
        }
    }
}
