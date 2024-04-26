using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class Translations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("75781b5c-20ee-4255-bf93-070b9ec42fe3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3b36dbab-dce8-4808-8430-c075dc734847"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3d63388c-fa02-4dbe-aaa1-20228cf3abc7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("49235eec-a260-42ee-a851-2563108a52ab"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5e357573-dd0e-4870-8b9a-b934206ff590"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("66591131-af57-4752-a2ce-0f46f7949b89"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("68b49cb7-d5da-4730-a534-2ed7f32d871a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("74678fcd-49e4-4651-9a1e-5a9ed307a9ba"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("843d01fe-8bd8-42cc-b9df-cc243cf84724"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8b869bb9-a9da-4e15-ab42-8a1549c353c2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8bf961d5-4f87-4061-b23a-d15edb98b416"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("d4f44888-c30d-4532-9d21-bfa7233f44d6"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e144a0a8-03c9-4bbf-b471-a72fff1706dd"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f7504235-ac3b-4e6b-b610-7e9c2c2011b0"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("a74cd36e-3ecc-4c7b-93c0-80bfa4c36c89"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "An ingredient that can be measured by volume.", "Volumetric" },
                    { new Guid("75781b5c-20ee-4255-bf93-070b9ec42fe3"), "An ingredient that can be counted.", "Countable" },
                    { new Guid("a74cd36e-3ecc-4c7b-93c0-80bfa4c36c89"), "An ingredient that can be measured by weight.", "Weighable" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "IngredientTypeId", "Name", "UnitMultiplier" },
                values: new object[,]
                {
                    { new Guid("3b36dbab-dce8-4808-8430-c075dc734847"), "lb", new Guid("a74cd36e-3ecc-4c7b-93c0-80bfa4c36c89"), "pound", 453.592f },
                    { new Guid("3d63388c-fa02-4dbe-aaa1-20228cf3abc7"), "qt", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "quart", 946.353f },
                    { new Guid("49235eec-a260-42ee-a851-2563108a52ab"), "kg", new Guid("a74cd36e-3ecc-4c7b-93c0-80bfa4c36c89"), "kilogram", 1000f },
                    { new Guid("5e357573-dd0e-4870-8b9a-b934206ff590"), "l", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "liter", 1000f },
                    { new Guid("66591131-af57-4752-a2ce-0f46f7949b89"), "tbsp", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "tablespoon", 14.7868f },
                    { new Guid("68b49cb7-d5da-4730-a534-2ed7f32d871a"), "g", new Guid("a74cd36e-3ecc-4c7b-93c0-80bfa4c36c89"), "gram", 1f },
                    { new Guid("74678fcd-49e4-4651-9a1e-5a9ed307a9ba"), "fl oz", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "fluid ounce", 29.5735f },
                    { new Guid("843d01fe-8bd8-42cc-b9df-cc243cf84724"), "tsp", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "teaspoon", 4.92892f },
                    { new Guid("8b869bb9-a9da-4e15-ab42-8a1549c353c2"), "oz", new Guid("a74cd36e-3ecc-4c7b-93c0-80bfa4c36c89"), "ounce", 28.3495f },
                    { new Guid("8bf961d5-4f87-4061-b23a-d15edb98b416"), "ml", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "milliliter", 1f },
                    { new Guid("d4f44888-c30d-4532-9d21-bfa7233f44d6"), "pt", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "pint", 473.176f },
                    { new Guid("e144a0a8-03c9-4bbf-b471-a72fff1706dd"), "c", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "cup", 236.588f },
                    { new Guid("f7504235-ac3b-4e6b-b610-7e9c2c2011b0"), "gal", new Guid("504d0d80-a986-4ee5-ada6-dd0f9c66445c"), "gallon", 3785.41f }
                });
        }
    }
}
