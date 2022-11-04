using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApi.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f4dcde23-67f2-4b67-80e5-58621d682737", "35813a03-d254-4f4f-9c6c-8aab65457035", "Manager", "Manager" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27485676-f2e3-405e-b3bf-d632ce1d9261", "a076d1cb-76b1-4e38-b456-f8d1b9d99fd4", "Administrator", "ADMINISTRATOR" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27485676-f2e3-405e-b3bf-d632ce1d9261");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4dcde23-67f2-4b67-80e5-58621d682737");
        }
    }
}
