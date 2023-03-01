using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false)
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
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_Like_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostTag_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "LoginId", "Email", "Password", "Type" },
                values: new object[,]
                {
                    { 1, "Test1@mail.dk", "password", 0 },
                    { 2, "Test2@mail.dk", "password", 1 }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "TagId", "Name" },
                values: new object[,]
                {
                    { 1, "sax" },
                    { 2, "fax" },
                    { 3, "howdy" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Created", "LoginId", "UserName" },
                values: new object[,]
                {
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230301112129_initial.cs
                    { 1, new DateTime(2023, 3, 1, 12, 21, 29, 458, DateTimeKind.Local).AddTicks(7539), 1, "tester 1" },
                    { 2, new DateTime(2023, 3, 1, 12, 21, 29, 458, DateTimeKind.Local).AddTicks(7542), 2, "222test222" }
========
                    { 1, new DateTime(2023, 3, 1, 13, 38, 48, 420, DateTimeKind.Local).AddTicks(9310), 1, "tester 1" },
                    { 2, new DateTime(2023, 3, 1, 13, 38, 48, 420, DateTimeKind.Local).AddTicks(9313), 2, "222test222" }
>>>>>>>> dev:Social_medie_projekt/WebApi/Migrations/20230301123848_initial.cs
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Date", "Desc", "Likes", "Tags", "Title", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230301112129_initial.cs
                    { 1, new DateTime(2023, 3, 1, 12, 21, 29, 458, DateTimeKind.Local).AddTicks(7554), "tadnawdnada", 1, "", "testestestest", 1 },
                    { 2, new DateTime(2023, 3, 1, 12, 21, 29, 458, DateTimeKind.Local).AddTicks(7557), "Woooooo!", 0, "", "Test!", 2 }
                });

            migrationBuilder.InsertData(
                table: "Like",
                columns: new[] { "PostId", "UserId" },
========
                    { 1, new DateTime(2023, 3, 1, 13, 38, 48, 420, DateTimeKind.Local).AddTicks(9327), "tadnawdnada", 1, "testestestest", 1 },
                    { 2, new DateTime(2023, 3, 1, 13, 38, 48, 420, DateTimeKind.Local).AddTicks(9330), "Woooooo!", 0, "Test!", 2 }
                });

            migrationBuilder.InsertData(
                table: "Liked",
                columns: new[] { "LikeUserId", "PostId", "LikedTime" },
                values: new object[] { 2, 1, new DateTime(2023, 3, 1, 13, 38, 48, 420, DateTimeKind.Local).AddTicks(9342) });

            migrationBuilder.InsertData(
                table: "PostsTags",
                columns: new[] { "PostId", "TagId" },
>>>>>>>> dev:Social_medie_projekt/WebApi/Migrations/20230301123848_initial.cs
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostId", "TagId", "Name" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 1, 2, null },
                    { 1, 3, null },
                    { 2, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId",
                table: "Like",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                table: "Like",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                table: "Tag",
                column: "Name",
                unique: true);

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
                name: "Like");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Login");
        }
    }
}
