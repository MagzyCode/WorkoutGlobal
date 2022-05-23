using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutGlobal.Api.Migrations
{
    public partial class UpdateVideoModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "4649d550-b6c0-4da0-9e11-bc5c8765292a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "36f4f49b-6118-43b9-9746-fefadb99b9c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "99c83e11-16fa-437a-8db9-2dc4624c9704", "0edee118-f9e2-45e5-909d-85b30f77ac14" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "5c14763c-430e-4b90-be26-30c6f09c4fc4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "7b8208be-53e3-480b-baf6-f86b789fdf11");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "65d10860-dc3b-4c9e-81d2-b3eae3481eb5", "da573684-00da-46c9-b198-c6462ad15d9d" });
        }
    }
}
