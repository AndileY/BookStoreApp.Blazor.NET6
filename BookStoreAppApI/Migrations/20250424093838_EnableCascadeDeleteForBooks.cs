using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAppApI.Migrations
{
    public partial class EnableCascadeDeleteForBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ToTable",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0284a2b8-98ee-4608-9dd2-1376b009888e",
                column: "ConcurrencyStamp",
                value: "dfe66879-e9f8-418e-a60a-1768c16e3491");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a123e426-b5e6-4664-a7a7-5564c344f15e",
                column: "ConcurrencyStamp",
                value: "d07c8ad7-b192-43c5-9700-c211a159711d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c6bc221-9937-49ce-99ed-293051f4f106",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f45dd86-a0c9-4709-9709-93163bfb71da", "AQAAAAEAACcQAAAAEJp09YMdeIrTcCdXMcAqGWKzaekbqyKikCEVF9wnXIRgDK2pvkYnQ72WBOPmCa8/rw==", "8211c1c6-6580-4f4f-b850-f1efd7ac1b3a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee9f8614-dc40-463b-a450-be6bc5fd6914",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5b69241-27ec-48dc-a3c4-f3446e4616b1", "AQAAAAEAACcQAAAAEFjwm5Grd6oq1lKWZrRCpoPmbp1j+0kFqjuRucEvx/we0iwEUC8NcBPrbAARc38KnQ==", "de59c45a-60f4-4ba2-8bc7-efac242eb944" });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ToTable",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ToTable",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0284a2b8-98ee-4608-9dd2-1376b009888e",
                column: "ConcurrencyStamp",
                value: "be859fe4-9cc2-40bd-b944-cf3d9073197c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a123e426-b5e6-4664-a7a7-5564c344f15e",
                column: "ConcurrencyStamp",
                value: "b9fe7555-704b-4af9-99eb-826929bce8c1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c6bc221-9937-49ce-99ed-293051f4f106",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff3e1538-840a-4f17-ba58-6035db44551e", "AQAAAAEAACcQAAAAEIW6lPYUeleQtrGrX6cnO6HRevj54y19ZbIq6pBeoG5bWTZKQl6vH6xINP1ZnBNMjw==", "1f504374-0984-42aa-abce-dd10ae0c7294" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee9f8614-dc40-463b-a450-be6bc5fd6914",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdc4f53c-bce4-47dd-bbdd-afb58b443e72", "AQAAAAEAACcQAAAAEDdxKyt3+XtNXVBSQninKmI5h35VMhqzBPkLF6wiEANQ6XMMe/meFNd/3tBJpvJZqw==", "87477fdf-7244-405c-a122-ee6aed9a3f80" });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ToTable",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}
