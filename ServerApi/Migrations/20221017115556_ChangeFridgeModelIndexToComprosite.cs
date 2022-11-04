using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApi.Migrations
{
    public partial class ChangeFridgeModelIndexToComprosite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FridgeProducts_ProductId",
                table: "FridgeProducts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "016cce0a-94bf-458c-ad5c-6c1e1a3880a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68c3b10e-07e7-4a2e-9da1-40029dda2da5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03313d03-9691-45b6-aeda-8d67969d398a", "f4556011-60e4-4cf4-bf03-0649fc839690", "Manager", "Manager" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b11d1bab-b869-46a4-9619-22c3c61c8189", "c07002a3-b02b-427b-ad73-1fe6e3859f0f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeProducts_ProductId_FridgeId",
                table: "FridgeProducts",
                columns: new[] { "ProductId", "FridgeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FridgeProducts_ProductId_FridgeId",
                table: "FridgeProducts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03313d03-9691-45b6-aeda-8d67969d398a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b11d1bab-b869-46a4-9619-22c3c61c8189");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "016cce0a-94bf-458c-ad5c-6c1e1a3880a2", "4e2e8c5a-f53d-48d2-ad5b-b2f5e955c806", "Manager", "Manager" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68c3b10e-07e7-4a2e-9da1-40029dda2da5", "73dc75fa-89e5-4485-84ab-023a25f284b9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeProducts_ProductId",
                table: "FridgeProducts",
                column: "ProductId");
        }
    }
}
