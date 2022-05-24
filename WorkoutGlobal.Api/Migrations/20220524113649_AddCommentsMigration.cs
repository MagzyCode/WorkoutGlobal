using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutGlobal.Api.Migrations
{
    public partial class AddCommentsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentsBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentedVideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsBlocks_Videos_CommentedVideoId",
                        column: x => x.CommentedVideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentsBlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_CommentsBlocks_CommentsBlockId",
                        column: x => x.CommentsBlockId,
                        principalTable: "CommentsBlocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_UserAccounts_CommentatorId",
                        column: x => x.CommentatorId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                column: "ConcurrencyStamp",
                value: "46479829-6bd1-4325-a04e-fd709701b09e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                column: "ConcurrencyStamp",
                value: "674d72c4-4195-4feb-a731-f6bba4f94984");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4f4d7080-beee-4a97-be65-2ffccde5eb72", "44c4621b-52c0-4fa8-93cc-5f9818124875", "Trainer", "TRAINER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ee9ec259-16f9-4522-a07c-721082326be9", "58907dd1-d2cb-4ba5-86fc-2638f6d5e548" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentatorId",
                table: "Comment",
                column: "CommentatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentsBlockId",
                table: "Comment",
                column: "CommentsBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsBlocks_CommentedVideoId",
                table: "CommentsBlocks",
                column: "CommentedVideoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CommentsBlocks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f4d7080-beee-4a97-be65-2ffccde5eb72");

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
    }
}
