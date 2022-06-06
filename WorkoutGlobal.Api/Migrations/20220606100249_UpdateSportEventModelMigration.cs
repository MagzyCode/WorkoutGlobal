using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutGlobal.Api.Migrations
{
    public partial class UpdateSportEventModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EventStartTime",
                table: "SportEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f4d7080-beee-4a97-be65-2ffccde5eb72",
                column: "ConcurrencyStamp",
                value: "d3c888ca-4b12-44b3-9b2f-91b126a2ffc8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "9aa8aa2b-a7c6-41b5-948f-43e0766184cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "ac6fd6e7-7376-434f-98a2-cf1000d41fb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cfd37afd-a0e4-42e2-9fa3-5083f0c08691", "9fc8d43a-a9c8-46e2-a6e0-b47e6e457b1d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventStartTime",
                table: "SportEvents");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f4d7080-beee-4a97-be65-2ffccde5eb72",
                column: "ConcurrencyStamp",
                value: "f8ac6a34-885e-4e85-95f3-7525fc949a5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "65d59954-feb8-4bce-97ca-991f995c35d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "9c6accec-e100-4a19-b3c4-7cbb0edeb3b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9f44ea84-8914-4e75-be31-bbc5fc3128ea", "fdfde3c8-a995-4cfc-91b9-3882545a5d95" });
        }
    }
}
