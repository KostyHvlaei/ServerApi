using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApi.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "Id", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), "Atalant b100", 2005 },
                    { new Guid("b6138124-9e39-4aaf-b039-5e149dd4c928"), "Samsung v32", 2021 },
                    { new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"), "Samsung k20", 2019 },
                    { new Guid("e3130a06-9410-44c0-bc38-6145b5e60426"), "Soyuz 1337", 1964 },
                    { new Guid("b4e73b10-115e-4371-b851-9cd08cd69740"), "Bosh c999", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DefaultQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("94fea888-0773-4d71-988a-32185bf61eee"), 6, "Pizza" },
                    { new Guid("297a1295-eb00-4447-bb88-5d9b69a1e1f1"), 1, "Juice" },
                    { new Guid("70497196-fb2d-4e29-8f17-1c2068afd916"), 10, "Apple" },
                    { new Guid("e5d96170-0301-459b-96f4-795a65783654"), 10, "Carrot" },
                    { new Guid("29f71e4f-161a-4dab-86af-e2f7aec1e2ba"), 2, "Chicken" }
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Id", "FridgeModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), "Genady Gorin's fridge", "Genady Gorin" },
                    { new Guid("ae16b99d-85f3-4121-bdad-e704c29e3981"), new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"), "Kitchen", "Some guy" },
                    { new Guid("d447d5c7-3d61-495c-8d36-89c337e3a9ef"), new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"), "Stolovaya n2", null },
                    { new Guid("8be43fc6-4398-4714-8794-edacee214946"), new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"), "Stolovaya n3", null },
                    { new Guid("92bafc65-cc77-485c-9756-81ad0aaa8008"), new Guid("e3130a06-9410-44c0-bc38-6145b5e60426"), "Mine fridge", "Man who wants to become developer" }
                });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("4e1a3a80-11b3-4add-af8d-7a8e6b33517a"), new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), new Guid("94fea888-0773-4d71-988a-32185bf61eee"), 3 },
                    { new Guid("886a0a29-3ca8-4f35-af38-e8ca3ec6d2e1"), new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"), new Guid("70497196-fb2d-4e29-8f17-1c2068afd916"), 0 },
                    { new Guid("41174222-9f3d-4a56-b422-4e9d01bb13b1"), new Guid("ae16b99d-85f3-4121-bdad-e704c29e3981"), new Guid("297a1295-eb00-4447-bb88-5d9b69a1e1f1"), 5 },
                    { new Guid("3ebf24a1-1c0f-45e2-b7ff-c80a119a53cb"), new Guid("8be43fc6-4398-4714-8794-edacee214946"), new Guid("e5d96170-0301-459b-96f4-795a65783654"), 0 },
                    { new Guid("bfc256b5-a2f1-4021-abcb-03ba7a1686bc"), new Guid("92bafc65-cc77-485c-9756-81ad0aaa8008"), new Guid("29f71e4f-161a-4dab-86af-e2f7aec1e2ba"), 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("b4e73b10-115e-4371-b851-9cd08cd69740"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("b6138124-9e39-4aaf-b039-5e149dd4c928"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("3ebf24a1-1c0f-45e2-b7ff-c80a119a53cb"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("41174222-9f3d-4a56-b422-4e9d01bb13b1"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("4e1a3a80-11b3-4add-af8d-7a8e6b33517a"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("886a0a29-3ca8-4f35-af38-e8ca3ec6d2e1"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("bfc256b5-a2f1-4021-abcb-03ba7a1686bc"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("d447d5c7-3d61-495c-8d36-89c337e3a9ef"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("8be43fc6-4398-4714-8794-edacee214946"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("92bafc65-cc77-485c-9756-81ad0aaa8008"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("ae16b99d-85f3-4121-bdad-e704c29e3981"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("297a1295-eb00-4447-bb88-5d9b69a1e1f1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("29f71e4f-161a-4dab-86af-e2f7aec1e2ba"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("70497196-fb2d-4e29-8f17-1c2068afd916"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("94fea888-0773-4d71-988a-32185bf61eee"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e5d96170-0301-459b-96f4-795a65783654"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("e3130a06-9410-44c0-bc38-6145b5e60426"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"));
        }
    }
}
