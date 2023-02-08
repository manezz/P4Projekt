using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class socialtmedie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Liked",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    LikedTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liked", x => new { x.UserId, x.PostId });
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag = table.Column<string>(type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostsTag",
                columns: table => new
                {
                    PostsPostId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsTag", x => new { x.PostsPostId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_PostsTag_Posts_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Liked",
                columns: new[] { "PostId", "UserId", "LikedTime" },
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230127111825_socialtmedie.cs
                values: new object[] { 1, 2, new DateTime(2023, 1, 27, 12, 18, 25, 184, DateTimeKind.Local).AddTicks(7023) });
========
                values: new object[] { 1, 2, new DateTime(2023, 2, 7, 13, 36, 35, 817, DateTimeKind.Local).AddTicks(7118) });
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230207123635_initial.cs

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "LoginId", "Email", "Password", "Type" },
                values: new object[,]
                {
                    { 1, "Test1@mail.dk", "password", 0 },
                    { 2, "Test2@mail.dk", "password", 1 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "tag" },
                values: new object[,]
                {
                    { 1, "sax" },
                    { 2, "fax" },
                    { 3, "howdy" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "Created", "FirstName", "LastName", "LoginId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230127111825_socialtmedie.cs
                    { 1, "testvej 1", new DateTime(2023, 1, 27, 12, 18, 25, 184, DateTimeKind.Local).AddTicks(6989), "test", "1", 1 },
                    { 2, "testvej 2", new DateTime(2023, 1, 27, 12, 18, 25, 184, DateTimeKind.Local).AddTicks(6993), "test", "2", 2 }
========
                    { 1, "testvej 1", new DateTime(2023, 2, 7, 13, 36, 35, 817, DateTimeKind.Local).AddTicks(7088), "test", "1", 1 },
                    { 2, "testvej 2", new DateTime(2023, 2, 7, 13, 36, 35, 817, DateTimeKind.Local).AddTicks(7092), "test", "2", 2 }
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230207123635_initial.cs
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Date", "Desc", "Likes", "Title", "UserId" },
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230127111825_socialtmedie.cs
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 27, 12, 18, 25, 184, DateTimeKind.Local).AddTicks(7007), "tadnawdnada", 1, "testestestest", 1 },
                    { 2, new DateTime(2023, 1, 27, 12, 18, 25, 184, DateTimeKind.Local).AddTicks(7010), "Ladadi ladaduuuuuuuuuu", 1, "awoo", 1 }
                });
========
                values: new object[] { 1, new DateTime(2023, 2, 7, 13, 36, 35, 817, DateTimeKind.Local).AddTicks(7106), "tadnawdnada", 1, "testestestest", 1 });
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230207123635_initial.cs

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsTag_TagsTagId",
                table: "PostsTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LoginId",
                table: "User",
                column: "LoginId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Liked");

            migrationBuilder.DropTable(
                name: "PostsTag");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Login");
        }
    }
}
