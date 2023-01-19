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

            migrationBuilder.InsertData(
                table: "Liked",
                columns: new[] { "PostId", "UserId", "LikedTime" },
                values: new object[] { 1, 2, new DateTime(2023, 1, 19, 14, 17, 23, 701, DateTimeKind.Local).AddTicks(1287) });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "LoginId", "Email", "Password", "Type" },
                values: new object[,]
                {
                    { 1, "Test1@mail.dk", "password", 0 },
                    { 2, "Test2@mail.dk", "password", 1 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "Created", "FirstName", "LastName", "LoginId" },
                values: new object[,]
                {
                    { 1, "testvej 1", new DateTime(2023, 1, 19, 14, 17, 23, 701, DateTimeKind.Local).AddTicks(1251), "test", "1", 1 },
                    { 2, "testvej 2", new DateTime(2023, 1, 19, 14, 17, 23, 701, DateTimeKind.Local).AddTicks(1255), "test", "2", 2 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Date", "Desc", "Likes", "Title", "UserId" },
                values: new object[] { 1, new DateTime(2023, 1, 19, 14, 17, 23, 701, DateTimeKind.Local).AddTicks(1273), "tadnawdnada", 1, "testestestest", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

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
                name: "Posts");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Login");
        }
    }
}
