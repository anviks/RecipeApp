using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class CustomUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitId",
                table: "RecipeIngredients",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1680f0d3-2f82-45e7-8341-690a7e150413"), "An ingredient that can be counted.", "Countable" },
                    { new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "An ingredient that can be measured by volume.", "Volumetric" },
                    { new Guid("d2934247-2cd5-436d-ad0e-a87a72eea483"), "An ingredient that can be measured by weight.", "Weighable" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "IngredientTypeId", "Name", "UnitMultiplier" },
                values: new object[,]
                {
                    { new Guid("0b5ae24e-b06a-45a4-9a54-da816966e0ed"), "oz", new Guid("d2934247-2cd5-436d-ad0e-a87a72eea483"), "ounce", 28.3495f },
                    { new Guid("3be4c844-a6ba-4a80-8480-2a76e5392f11"), "tbsp", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "tablespoon", 14.7868f },
                    { new Guid("775e96af-22ab-4e98-96ce-382ac2838ee6"), "gal", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "gallon", 3785.41f },
                    { new Guid("8e2e2b5a-8584-4abf-865e-ff62af715a0b"), "g", new Guid("d2934247-2cd5-436d-ad0e-a87a72eea483"), "gram", 1f },
                    { new Guid("92c9210c-ec14-4a2a-9d1f-145259cf0fc6"), "c", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "cup", 236.588f },
                    { new Guid("a0d64ddc-dbe4-46f3-a7d8-51aff2a4cd6c"), "lb", new Guid("d2934247-2cd5-436d-ad0e-a87a72eea483"), "pound", 453.592f },
                    { new Guid("a2585327-f51d-44ec-b4db-3877b6fe6a21"), "kg", new Guid("d2934247-2cd5-436d-ad0e-a87a72eea483"), "kilogram", 1000f },
                    { new Guid("a71cece8-16c8-49f0-a6f1-aaff1c8918e3"), "tsp", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "teaspoon", 4.92892f },
                    { new Guid("bb1d3a76-7bee-4c9f-a7b1-a8e4a66b37ce"), "qt", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "quart", 946.353f },
                    { new Guid("d92a81bf-301d-4fc8-a491-a34b02352f23"), "l", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "liter", 1000f },
                    { new Guid("dac20572-1a9c-4b8a-ab34-32f674a39b5e"), "pt", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "pint", 473.176f },
                    { new Guid("db6b6078-9b25-4c4c-8a6a-d1498e4d0a39"), "fl oz", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "fluid ounce", 29.5735f },
                    { new Guid("efc8cbdb-4a75-4db8-99e4-fac9bbb01a42"), "ml", new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"), "milliliter", 1f }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("1680f0d3-2f82-45e7-8341-690a7e150413"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("0b5ae24e-b06a-45a4-9a54-da816966e0ed"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3be4c844-a6ba-4a80-8480-2a76e5392f11"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("775e96af-22ab-4e98-96ce-382ac2838ee6"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8e2e2b5a-8584-4abf-865e-ff62af715a0b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("92c9210c-ec14-4a2a-9d1f-145259cf0fc6"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a0d64ddc-dbe4-46f3-a7d8-51aff2a4cd6c"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a2585327-f51d-44ec-b4db-3877b6fe6a21"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a71cece8-16c8-49f0-a6f1-aaff1c8918e3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("bb1d3a76-7bee-4c9f-a7b1-a8e4a66b37ce"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("d92a81bf-301d-4fc8-a491-a34b02352f23"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("dac20572-1a9c-4b8a-ab34-32f674a39b5e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("db6b6078-9b25-4c4c-8a6a-d1498e4d0a39"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("efc8cbdb-4a75-4db8-99e4-fac9bbb01a42"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("5b2d62b8-d1b0-4cad-a810-eaffeee41cf1"));

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: new Guid("d2934247-2cd5-436d-ad0e-a87a72eea483"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitId",
                table: "RecipeIngredients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
