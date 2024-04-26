using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class BetterImageSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("93c2bb0d-11b4-4eff-b2b5-2fdc1898aa3d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("05666ad7-9824-4a7f-bd58-44bb25e40792"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("39c7a527-d49c-4502-8747-2e1b351420d0"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5241ef40-dc06-4451-b996-46df9fb537cd"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("6e0c9c3a-451d-4bc4-8945-d72287dcc37b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8900a741-f031-45bc-aa25-a84e30778ca7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9a5b74d4-ff3a-4f20-8aad-9364285df827"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("baf58e9e-e27a-49e5-8091-11aeb9c69acf"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("be3be443-5fe2-4520-a1aa-e7a8547dbe5c"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("bf23b109-3e8e-4ca1-8a48-f1604f3ac56e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("def50c38-9eaa-472b-876d-cce8cf987e21"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e9c3feb9-8385-4deb-92e8-b8cb417a0b56"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("ed9748c0-e38a-4172-8f9b-f4586feef92d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f05bc460-5704-4302-925c-38902b1ee5ec"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("c41dbd19-a9c1-41ca-a752-179e3bb91666"));

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "An ingredient that can be measured by volume.", "Volumetric" },
                    { new Guid("ca3db1eb-a2fa-4285-8e9d-790f8d015ff1"), "An ingredient that can be counted.", "Countable" },
                    { new Guid("cdfba50b-da25-4158-a02c-d43958cd6803"), "An ingredient that can be measured by weight.", "Weighable" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "IngredientTypeId", "Name", "UnitMultiplier" },
                values: new object[,]
                {
                    { new Guid("29c63c42-4a20-439e-bfb0-d97d22ad6188"), "gal", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "gallon", 3785.41f },
                    { new Guid("2ca62cde-1eb2-47a8-99a9-57228724038e"), "oz", new Guid("cdfba50b-da25-4158-a02c-d43958cd6803"), "ounce", 28.3495f },
                    { new Guid("3559d31c-3d4c-4ad1-be9a-81857a32d24f"), "ml", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "milliliter", 1f },
                    { new Guid("40fec259-177e-40db-80b7-7cf8e46737bb"), "pt", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "pint", 473.176f },
                    { new Guid("5001fde0-395a-49e7-bc87-b078bfcfcc6a"), "qt", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "quart", 946.353f },
                    { new Guid("5228f545-4f52-405d-9dfe-6992fc26df78"), "g", new Guid("cdfba50b-da25-4158-a02c-d43958cd6803"), "gram", 1f },
                    { new Guid("64bab0b6-0ea2-4b12-a50e-6305467ac082"), "tsp", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "teaspoon", 4.92892f },
                    { new Guid("685976ea-0f26-4a1c-926f-67fc416d980b"), "c", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "cup", 236.588f },
                    { new Guid("9945ffa4-33f2-4197-b69d-a7928b8dc1ff"), "tbsp", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "tablespoon", 14.7868f },
                    { new Guid("a617b85b-09cd-4f9a-8f13-0ccca0d9492e"), "kg", new Guid("cdfba50b-da25-4158-a02c-d43958cd6803"), "kilogram", 1000f },
                    { new Guid("c894e555-750d-4bae-ae4f-c90fdd4266c7"), "l", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "liter", 1000f },
                    { new Guid("cfb49829-10cb-4084-8352-7de8fccad535"), "fl oz", new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"), "fluid ounce", 29.5735f },
                    { new Guid("fda1cebf-b7ef-476d-bf10-bc6648709911"), "lb", new Guid("cdfba50b-da25-4158-a02c-d43958cd6803"), "pound", 453.592f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("ca3db1eb-a2fa-4285-8e9d-790f8d015ff1"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("29c63c42-4a20-439e-bfb0-d97d22ad6188"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("2ca62cde-1eb2-47a8-99a9-57228724038e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3559d31c-3d4c-4ad1-be9a-81857a32d24f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("40fec259-177e-40db-80b7-7cf8e46737bb"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5001fde0-395a-49e7-bc87-b078bfcfcc6a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5228f545-4f52-405d-9dfe-6992fc26df78"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("64bab0b6-0ea2-4b12-a50e-6305467ac082"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("685976ea-0f26-4a1c-926f-67fc416d980b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9945ffa4-33f2-4197-b69d-a7928b8dc1ff"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a617b85b-09cd-4f9a-8f13-0ccca0d9492e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("c894e555-750d-4bae-ae4f-c90fdd4266c7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("cfb49829-10cb-4084-8352-7de8fccad535"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("fda1cebf-b7ef-476d-bf10-bc6648709911"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("82644f03-3e38-4b3d-9ceb-1b9a30c833e7"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("cdfba50b-da25-4158-a02c-d43958cd6803"));

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "An ingredient that can be measured by volume.", "Volumetric" },
                    { new Guid("93c2bb0d-11b4-4eff-b2b5-2fdc1898aa3d"), "An ingredient that can be counted.", "Countable" },
                    { new Guid("c41dbd19-a9c1-41ca-a752-179e3bb91666"), "An ingredient that can be measured by weight.", "Weighable" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "IngredientTypeId", "Name", "UnitMultiplier" },
                values: new object[,]
                {
                    { new Guid("05666ad7-9824-4a7f-bd58-44bb25e40792"), "pt", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "pint", 473.176f },
                    { new Guid("39c7a527-d49c-4502-8747-2e1b351420d0"), "lb", new Guid("c41dbd19-a9c1-41ca-a752-179e3bb91666"), "pound", 453.592f },
                    { new Guid("5241ef40-dc06-4451-b996-46df9fb537cd"), "g", new Guid("c41dbd19-a9c1-41ca-a752-179e3bb91666"), "gram", 1f },
                    { new Guid("6e0c9c3a-451d-4bc4-8945-d72287dcc37b"), "l", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "liter", 1000f },
                    { new Guid("8900a741-f031-45bc-aa25-a84e30778ca7"), "gal", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "gallon", 3785.41f },
                    { new Guid("9a5b74d4-ff3a-4f20-8aad-9364285df827"), "ml", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "milliliter", 1f },
                    { new Guid("baf58e9e-e27a-49e5-8091-11aeb9c69acf"), "fl oz", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "fluid ounce", 29.5735f },
                    { new Guid("be3be443-5fe2-4520-a1aa-e7a8547dbe5c"), "tbsp", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "tablespoon", 14.7868f },
                    { new Guid("bf23b109-3e8e-4ca1-8a48-f1604f3ac56e"), "c", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "cup", 236.588f },
                    { new Guid("def50c38-9eaa-472b-876d-cce8cf987e21"), "oz", new Guid("c41dbd19-a9c1-41ca-a752-179e3bb91666"), "ounce", 28.3495f },
                    { new Guid("e9c3feb9-8385-4deb-92e8-b8cb417a0b56"), "tsp", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "teaspoon", 4.92892f },
                    { new Guid("ed9748c0-e38a-4172-8f9b-f4586feef92d"), "qt", new Guid("18b67533-edb6-4d90-b02a-dc3f772e5e16"), "quart", 946.353f },
                    { new Guid("f05bc460-5704-4302-925c-38902b1ee5ec"), "kg", new Guid("c41dbd19-a9c1-41ca-a752-179e3bb91666"), "kilogram", 1000f }
                });
        }
    }
}
