using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAppApI.Migrations
{
    public partial class SeededDefaultUserandRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0284a2b8-98ee-4608-9dd2-1376b009888e", "bdad9a9f-2b3a-409c-9e9e-4f4c9d6417c4", "Administrator", "ADMINISTRATOR" },
                    { "a123e426-b5e6-4664-a7a7-5564c344f15e", "52a1f932-bcb4-40b3-bd6b-fc99861075c2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2c6bc221-9937-49ce-99ed-293051f4f106", 0, "537bcb99-d2a9-42ad-87db-a7d64bfd9d56", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEAmP0ReT74unNC30Jc68X5QVse5IBBZG8EXLXhokeDriChyT3lSGz70Q8BGvh2Ly0A==", null, false, "630f0456-70a9-4cd1-a593-92d66af01136", false, "admin@bookstore.com" },
                    { "ee9f8614-dc40-463b-a450-be6bc5fd6914", 0, "83bb3876-0d2e-41ea-8e6e-dc7d93155831", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAENA9gYPrn6KjAvAinOxO3lEytQiCfM7w1bGkuKqCp6BcyBj8md+ZvWNzppqmSNx5tQ==", null, false, "c0d0733d-bbd8-47c4-9423-ea1509855dc3", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0284a2b8-98ee-4608-9dd2-1376b009888e", "2c6bc221-9937-49ce-99ed-293051f4f106" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a123e426-b5e6-4664-a7a7-5564c344f15e", "ee9f8614-dc40-463b-a450-be6bc5fd6914" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0284a2b8-98ee-4608-9dd2-1376b009888e", "2c6bc221-9937-49ce-99ed-293051f4f106" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a123e426-b5e6-4664-a7a7-5564c344f15e", "ee9f8614-dc40-463b-a450-be6bc5fd6914" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0284a2b8-98ee-4608-9dd2-1376b009888e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a123e426-b5e6-4664-a7a7-5564c344f15e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c6bc221-9937-49ce-99ed-293051f4f106");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee9f8614-dc40-463b-a450-be6bc5fd6914");
        }
    }
}
