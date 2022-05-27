using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutGlobal.Api.Migrations
{
    public partial class ChangeCollectionCoursesNameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f4d7080-beee-4a97-be65-2ffccde5eb72",
                column: "ConcurrencyStamp",
                value: "61b24f4f-3487-47b9-947c-08e6eb70ae5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "1dd00070-bca7-4a6b-9526-8b3027f1fb23");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "303b73c3-9392-4b0f-9e5a-de1f0fee9394");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "eb3e3902-4d97-432d-9127-c747f91c1bdc", "41ba4bc4-cf5a-4b75-9d12-8d1089055fa6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f4d7080-beee-4a97-be65-2ffccde5eb72",
                column: "ConcurrencyStamp",
                value: "dd89d012-fccd-4aa6-8d20-1bf88d70ee0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "7659e870-d960-4aef-838c-af63723709f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "03f9cdbf-b63f-49ef-a52f-fd0bf35e1325");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b286bf7e-89e9-4e6b-8876-ab89aff4e9ad", "5d3c9aab-b352-491b-aff6-285a579b9598" });
        }
    }
}
