using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class RenameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Reviews",
                newName: "Comment");

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("22584bc3-cead-4c88-b42b-02832a6527c0"), "An ingredient that can be measured by weight.", "Weighable" },
                    { new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "An ingredient that can be measured by volume.", "Volumetric" },
                    { new Guid("ee524346-1ba0-49b8-95e0-6d18ad71fb65"), "An ingredient that can be counted.", "Countable" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "IngredientTypeId", "Name", "UnitMultiplier" },
                values: new object[,]
                {
                    { new Guid("22dd713d-e70d-49e0-942e-4e2134bc1f34"), "lb", new Guid("22584bc3-cead-4c88-b42b-02832a6527c0"), "pound", 453.592f },
                    { new Guid("244bfb67-a973-486d-94cd-39f5b73e4a6c"), "l", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "liter", 1000f },
                    { new Guid("2d3fd2fb-78fd-4608-befa-f12e32d6cd26"), "ml", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "milliliter", 1f },
                    { new Guid("2d4056a1-2b71-45fd-b9fa-552a79762ec0"), "tbsp", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "tablespoon", 14.7868f },
                    { new Guid("3caea13e-cb83-43e3-820d-e0309033b9c0"), "fl oz", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "fluid ounce", 29.5735f },
                    { new Guid("3d2b1557-cf8e-4187-9ac2-fc8989ba7f05"), "oz", new Guid("22584bc3-cead-4c88-b42b-02832a6527c0"), "ounce", 28.3495f },
                    { new Guid("7d1c64f9-f267-4efd-953f-55224f04d95a"), "tsp", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "teaspoon", 4.92892f },
                    { new Guid("bced189b-fa73-4aff-95b7-8a7df271c318"), "pt", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "pint", 473.176f },
                    { new Guid("cd96d223-d8a7-41ed-857a-e391883b4d63"), "kg", new Guid("22584bc3-cead-4c88-b42b-02832a6527c0"), "kilogram", 1000f },
                    { new Guid("cef59534-bb03-4ae4-baca-d2c69df04ca1"), "c", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "cup", 236.588f },
                    { new Guid("d0095cc6-06f8-4cdd-95da-edcce94fb197"), "gal", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "gallon", 3785.41f },
                    { new Guid("d501d12b-567c-493f-a86a-04a44cb43448"), "g", new Guid("22584bc3-cead-4c88-b42b-02832a6527c0"), "gram", 1f },
                    { new Guid("e498aec5-c5df-4b95-87ae-82b41f2d11ed"), "qt", new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"), "quart", 946.353f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("ee524346-1ba0-49b8-95e0-6d18ad71fb65"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("22dd713d-e70d-49e0-942e-4e2134bc1f34"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("244bfb67-a973-486d-94cd-39f5b73e4a6c"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("2d3fd2fb-78fd-4608-befa-f12e32d6cd26"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("2d4056a1-2b71-45fd-b9fa-552a79762ec0"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3caea13e-cb83-43e3-820d-e0309033b9c0"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3d2b1557-cf8e-4187-9ac2-fc8989ba7f05"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("7d1c64f9-f267-4efd-953f-55224f04d95a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("bced189b-fa73-4aff-95b7-8a7df271c318"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("cd96d223-d8a7-41ed-857a-e391883b4d63"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("cef59534-bb03-4ae4-baca-d2c69df04ca1"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("d0095cc6-06f8-4cdd-95da-edcce94fb197"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("d501d12b-567c-493f-a86a-04a44cb43448"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e498aec5-c5df-4b95-87ae-82b41f2d11ed"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("22584bc3-cead-4c88-b42b-02832a6527c0"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("a5a1adb4-83b4-4a07-8278-16df48968643"));

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Reviews",
                newName: "Content");

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
    }
}
