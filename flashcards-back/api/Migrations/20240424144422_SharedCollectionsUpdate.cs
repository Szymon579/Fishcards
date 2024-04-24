using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SharedCollectionsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44477ad3-7eaa-4a95-b3eb-e36cacb0c9fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4922e8bd-88e8-46f5-9b04-4f30827be3fb");

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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SharedCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10a87d54-3d07-4452-a808-495bf2d8e020", null, "Admin", "ADMIN" },
                    { "5c3ed836-4f0a-443b-bca0-fd5cd220362f", null, "User", "USER" }
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedCollections");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10a87d54-3d07-4452-a808-495bf2d8e020");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c3ed836-4f0a-443b-bca0-fd5cd220362f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44477ad3-7eaa-4a95-b3eb-e36cacb0c9fe", null, "User", "USER" },
                    { "4922e8bd-88e8-46f5-9b04-4f30827be3fb", null, "Admin", "ADMIN" }
                });
        }
    }
}
