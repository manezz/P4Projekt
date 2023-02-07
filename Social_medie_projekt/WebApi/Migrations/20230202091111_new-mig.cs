using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class newmig : Migration
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
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.cs
                values: new object[] { 1, 2, new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4536) });
========
                values: new object[] { 1, 2, new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4129) });
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.cs

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
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.cs
                    { 1, "testvej 1", new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4502), "test", "1", 1 },
                    { 2, "testvej 2", new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4507), "test", "2", 2 }
========
                    { 1, "testvej 1", new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4098), "test", "1", 1 },
                    { 2, "testvej 2", new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4102), "test", "2", 2 }
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.cs
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Date", "Desc", "Likes", "Title", "UserId" },
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.cs
                values: new object[] { 1, new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4522), "tadnawdnada", 1, "testestestest", 1 });
========
                values: new object[] { 1, new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4117), "tadnawdnada", 1, "testestestest", 1 });
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.cs

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
