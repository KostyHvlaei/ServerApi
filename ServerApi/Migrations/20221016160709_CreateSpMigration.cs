using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApi.Migrations
{
    public partial class CreateSpMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27485676-f2e3-405e-b3bf-d632ce1d9261");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4dcde23-67f2-4b67-80e5-58621d682737");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "016cce0a-94bf-458c-ad5c-6c1e1a3880a2", "4e2e8c5a-f53d-48d2-ad5b-b2f5e955c806", "Manager", "Manager" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68c3b10e-07e7-4a2e-9da1-40029dda2da5", "73dc75fa-89e5-4485-84ab-023a25f284b9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE  [dbo].[GetFridgeProductWithZeroQuantity]
                                        @productId uniqueidentifier OUT,
                                        @fridgeId uniqueidentifier OUT,
                                        @status bit OUT
                                    AS
                                        IF (SELECT COUNT(Id) FROM FridgeProducts WHERE Quantity = 0) = 0
                                        BEGIN
                                            SET @status = 'false'
                                            SET @fridgeId = NEWID()
                                            SET @productId = NEWID()
                                            RETURN
                                        END
                                        ELSE
                                        BEGIN
                                            SET @status = 'true'
                                            SELECT TOP 1 @fridgeId = FridgeId, @productId = ProductId FROM FridgeProducts WHERE Quantity = 0
                                        END

                                    GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "f4dcde23-67f2-4b67-80e5-58621d682737", "35813a03-d254-4f4f-9c6c-8aab65457035", "Manager", "Manager" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27485676-f2e3-405e-b3bf-d632ce1d9261", "a076d1cb-76b1-4e38-b456-f8d1b9d99fd4", "Administrator", "ADMINISTRATOR" });
        }
    }
}
