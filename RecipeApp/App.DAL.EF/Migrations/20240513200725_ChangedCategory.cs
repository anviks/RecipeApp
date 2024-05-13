using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("f1f17bb7-6246-4c4d-bcd2-d1c7f850f3eb"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("028f8549-0bd2-4a0a-8637-870495fe7523"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("0988895a-ae22-42d4-87fc-aa4c08806ca7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("231b47fe-ee34-4367-8440-c8e9c2c5612f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("2bbe3383-9f16-4728-9c1d-355c0a68311d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("47a95cb3-6ce0-41e5-8a61-5dd937c81765"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("6458058a-5a2d-4a99-b623-40c4fd166f4f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("65583eee-6fc7-4c26-8447-e7af40a0a126"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8b57e2a3-3684-4d6a-9e1c-5ef6fec2461b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9ad8bbdc-2ac3-43f8-8d3d-5b21ce0bcbbf"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a0695ef2-263f-4f6e-b10e-3f0e6daa3606"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b3349e93-c5d7-45cc-92b1-30c7e3c6f4fe"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("c06868a2-13d9-4684-9532-dd644f583cb8"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("dd7dec2c-87e4-4ad7-b324-7f32bcc8e1b2"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("937c04b5-fe9d-4f3b-9e7f-43a02b2f64fc"));

            migrationBuilder.DropColumn(
                name: "BroadnessIndex",
                table: "Categories");

            migrationBuilder.AddColumn<bool>(
                name: "Edited",
                table: "Reviews",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("496b5608-bce1-41b3-91a4-7e38d076de06"), "An ingredient that can be measured by weight.", "Weighable" },
                    { new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "An ingredient that can be measured by volume.", "Volumetric" },
                    { new Guid("bfbf531d-c197-4916-bd48-047032178f4e"), "An ingredient that can be counted.", "Countable" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "IngredientTypeId", "Name", "UnitMultiplier" },
                values: new object[,]
                {
                    { new Guid("033cf7ae-9d2b-46f2-8936-bd5929e34854"), "l", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "liter", 1000f },
                    { new Guid("07050f9b-a6e3-4d50-8202-40f682753e36"), "gal", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "gallon", 3785.41f },
                    { new Guid("0ce3d087-d7a9-4df3-a04b-40ffdaa0347e"), "ml", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "milliliter", 1f },
                    { new Guid("32d7e469-d881-4f29-a665-a7e724132943"), "g", new Guid("496b5608-bce1-41b3-91a4-7e38d076de06"), "gram", 1f },
                    { new Guid("4a775c23-9f8c-43a9-9dd8-b99c7f487f58"), "fl oz", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "fluid ounce", 29.5735f },
                    { new Guid("5c1ad35f-0984-418e-94e7-076fb2ce31c3"), "tsp", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "teaspoon", 4.92892f },
                    { new Guid("777ff6ec-b5b1-46e9-8879-929d3f30c3c7"), "lb", new Guid("496b5608-bce1-41b3-91a4-7e38d076de06"), "pound", 453.592f },
                    { new Guid("97ed1573-fd8f-492e-b60e-17d379a5f74b"), "kg", new Guid("496b5608-bce1-41b3-91a4-7e38d076de06"), "kilogram", 1000f },
                    { new Guid("a00cd192-f729-432f-b471-37d9e96f4733"), "oz", new Guid("496b5608-bce1-41b3-91a4-7e38d076de06"), "ounce", 28.3495f },
                    { new Guid("b7430af0-f78c-4787-ad7e-99904e4af9c8"), "qt", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "quart", 946.353f },
                    { new Guid("bf620f4f-adce-4a20-8c45-9742c1fbe640"), "pt", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "pint", 473.176f },
                    { new Guid("d3b112ff-1def-42f3-9007-758dd44ae4eb"), "c", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "cup", 236.588f },
                    { new Guid("e960f2a2-8962-4bd8-8997-a0731dcd14bf"), "tbsp", new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"), "tablespoon", 14.7868f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("bfbf531d-c197-4916-bd48-047032178f4e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("033cf7ae-9d2b-46f2-8936-bd5929e34854"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("07050f9b-a6e3-4d50-8202-40f682753e36"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("0ce3d087-d7a9-4df3-a04b-40ffdaa0347e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("32d7e469-d881-4f29-a665-a7e724132943"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("4a775c23-9f8c-43a9-9dd8-b99c7f487f58"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5c1ad35f-0984-418e-94e7-076fb2ce31c3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("777ff6ec-b5b1-46e9-8879-929d3f30c3c7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("97ed1573-fd8f-492e-b60e-17d379a5f74b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a00cd192-f729-432f-b471-37d9e96f4733"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b7430af0-f78c-4787-ad7e-99904e4af9c8"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("bf620f4f-adce-4a20-8c45-9742c1fbe640"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("d3b112ff-1def-42f3-9007-758dd44ae4eb"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e960f2a2-8962-4bd8-8997-a0731dcd14bf"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("496b5608-bce1-41b3-91a4-7e38d076de06"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("9a9b37b7-d88e-4b1e-8946-a020b82de99e"));

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Reviews");

            migrationBuilder.AddColumn<short>(
                name: "BroadnessIndex",
                table: "Categories",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "An ingredient that can be measured by volume.", "Volumetric" },
                    { new Guid("937c04b5-fe9d-4f3b-9e7f-43a02b2f64fc"), "An ingredient that can be measured by weight.", "Weighable" },
                    { new Guid("f1f17bb7-6246-4c4d-bcd2-d1c7f850f3eb"), "An ingredient that can be counted.", "Countable" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "IngredientTypeId", "Name", "UnitMultiplier" },
                values: new object[,]
                {
                    { new Guid("028f8549-0bd2-4a0a-8637-870495fe7523"), "g", new Guid("937c04b5-fe9d-4f3b-9e7f-43a02b2f64fc"), "gram", 1f },
                    { new Guid("0988895a-ae22-42d4-87fc-aa4c08806ca7"), "l", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "liter", 1000f },
                    { new Guid("231b47fe-ee34-4367-8440-c8e9c2c5612f"), "oz", new Guid("937c04b5-fe9d-4f3b-9e7f-43a02b2f64fc"), "ounce", 28.3495f },
                    { new Guid("2bbe3383-9f16-4728-9c1d-355c0a68311d"), "qt", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "quart", 946.353f },
                    { new Guid("47a95cb3-6ce0-41e5-8a61-5dd937c81765"), "ml", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "milliliter", 1f },
                    { new Guid("6458058a-5a2d-4a99-b623-40c4fd166f4f"), "tsp", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "teaspoon", 4.92892f },
                    { new Guid("65583eee-6fc7-4c26-8447-e7af40a0a126"), "fl oz", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "fluid ounce", 29.5735f },
                    { new Guid("8b57e2a3-3684-4d6a-9e1c-5ef6fec2461b"), "gal", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "gallon", 3785.41f },
                    { new Guid("9ad8bbdc-2ac3-43f8-8d3d-5b21ce0bcbbf"), "tbsp", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "tablespoon", 14.7868f },
                    { new Guid("a0695ef2-263f-4f6e-b10e-3f0e6daa3606"), "kg", new Guid("937c04b5-fe9d-4f3b-9e7f-43a02b2f64fc"), "kilogram", 1000f },
                    { new Guid("b3349e93-c5d7-45cc-92b1-30c7e3c6f4fe"), "lb", new Guid("937c04b5-fe9d-4f3b-9e7f-43a02b2f64fc"), "pound", 453.592f },
                    { new Guid("c06868a2-13d9-4684-9532-dd644f583cb8"), "c", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "cup", 236.588f },
                    { new Guid("dd7dec2c-87e4-4ad7-b324-7f32bcc8e1b2"), "pt", new Guid("3f1b9698-2053-453a-8a26-cb172292bd2d"), "pint", 473.176f }
                });
        }
    }
}
