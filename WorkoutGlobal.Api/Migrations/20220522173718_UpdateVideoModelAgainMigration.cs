using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutGlobal.Api.Migrations
{
    public partial class UpdateVideoModelAgainMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "4aa9d0f6-1837-460b-a835-e8d1da6d8097");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "e27c4ebf-23ea-462b-b3cd-0249a5c5eb47");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "65d61e7a-0000-4020-80bc-5b1735e15c08", "1be95a6f-44bb-4c73-aac3-b7c63455f9cb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Videos");

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
    }
}
