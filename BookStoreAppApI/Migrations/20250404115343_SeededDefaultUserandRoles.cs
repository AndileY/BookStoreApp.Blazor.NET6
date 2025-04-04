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
                    { "0284a2b8-98ee-4608-9dd2-1376b009888e", "da5370d7-b870-4020-b5ef-d2583ffa18ba", "Administrator", "ADMINISTRATOR" },
                    { "a123e426-b5e6-4664-a7a7-5564c344f15e", "5f6060ef-596d-4fd1-a05b-ed3062b68462", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2c6bc221-9937-49ce-99ed-293051f4f106", 0, "e14fe9fb-5992-4b20-b68c-78d4bef62833", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAECtiMM2pF+g+rn4ni5bXz9lf2Ga9o3CZznKFe+RAYTKcU2iv2j61S0wmRZO2F3YRPw==", null, false, "c4fb58b3-bbd0-45cb-b168-ab226a9843f9", false, "admin@bookstore.com" },
                    { "ee9f8614-dc40-463b-a450-be6bc5fd6914", 0, "b7978bdc-c28a-4764-8a09-4dd8d431b45c", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEHtXkvDe2l66HmEo0SyMBk/7yb0B+2PUsoW0kPuo+6OMAUOOCPsehnG9vx++yjZaoQ==", null, false, "bcc42182-eb35-4c83-90de-a8bcf2c50333", false, "user@bookstore.com" }
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
