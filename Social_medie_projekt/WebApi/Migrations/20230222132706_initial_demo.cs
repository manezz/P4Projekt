using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class initialdemo : Migration
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
                    Name = table.Column<string>(type: "nvarchar(32)", nullable: false)
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
                    KeyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.KeyId);
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
                        principalColumn: "UserId");
        });

            migrationBuilder.CreateTable(
                name: "PostsTags",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsTags", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostsTags_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsTags_Tag_TagId",
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
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230222132706_initial_demo.cs
                    { 1, new DateTime(2023, 2, 22, 14, 27, 6, 611, DateTimeKind.Local).AddTicks(9612), 1, "tester 1" },
                    { 2, new DateTime(2023, 2, 22, 14, 27, 6, 611, DateTimeKind.Local).AddTicks(9616), 2, "222test222" }
========
                    { 1, new DateTime(2023, 2, 23, 14, 35, 6, 112, DateTimeKind.Local).AddTicks(9726), 1, "tester 1" },
                    { 2, new DateTime(2023, 2, 23, 14, 35, 6, 112, DateTimeKind.Local).AddTicks(9730), 2, "222test222" }
>>>>>>>> dev:Social_medie_projekt/WebApi/Migrations/20230223133506_initial.cs
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Date", "Desc", "Likes", "Tags", "Title", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230222132706_initial_demo.cs
                    { 1, new DateTime(2023, 2, 22, 14, 27, 6, 611, DateTimeKind.Local).AddTicks(9631), "tadnawdnada", 1, "", "testestestest", 1 },
                    { 2, new DateTime(2023, 2, 22, 14, 27, 6, 611, DateTimeKind.Local).AddTicks(9634), "Woooooo!", 0, "", "Test!", 2 }
                });

            migrationBuilder.InsertData(
                table: "Like",
                columns: new[] { "KeyId", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 2, 2 }
                });
========
                    { 1, new DateTime(2023, 2, 23, 14, 35, 6, 112, DateTimeKind.Local).AddTicks(9745), "tadnawdnada", 1, "testestestest", 1 },
                    { 2, new DateTime(2023, 2, 23, 14, 35, 6, 112, DateTimeKind.Local).AddTicks(9748), "Woooooo!", 0, "Test!", 2 }
                });

            migrationBuilder.InsertData(
                table: "Liked",
                columns: new[] { "LikeUserId", "PostId", "LikedTime" },
                values: new object[] { 2, 1, new DateTime(2023, 2, 23, 14, 35, 6, 112, DateTimeKind.Local).AddTicks(9762) });
>>>>>>>> dev:Social_medie_projekt/WebApi/Migrations/20230223133506_initial.cs

            migrationBuilder.InsertData(
                table: "PostsTags",
                columns: new[] { "PostId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230222132706_initial_demo.cs
                name: "IX_Like_PostId",
                table: "Like",
========
                name: "IX_Liked_LikeUserId",
                table: "Liked",
                column: "LikeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Liked_PostId",
                table: "Liked",
>>>>>>>> dev:Social_medie_projekt/WebApi/Migrations/20230223133506_initial.cs
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                table: "Like",
                column: "UserId",
                unique: false); // set to false

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsTags_TagId",
                table: "PostsTags",
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
                name: "PostsTags");

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
