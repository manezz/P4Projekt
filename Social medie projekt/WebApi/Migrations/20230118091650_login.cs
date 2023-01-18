using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class login : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Login",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Liked",
                keyColumns: new[] { "PostId", "UserId" },
                keyValues: new object[] { 1, 2 },
                column: "LikedTime",
                value: new DateTime(2023, 1, 18, 10, 16, 50, 587, DateTimeKind.Local).AddTicks(706));

            migrationBuilder.UpdateData(
                table: "Login",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "Type",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Login",
                keyColumn: "LoginId",
                keyValue: 2,
                column: "Type",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 18, 10, 16, 50, 587, DateTimeKind.Local).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 18, 10, 16, 50, 587, DateTimeKind.Local).AddTicks(677));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 18, 10, 16, 50, 587, DateTimeKind.Local).AddTicks(681));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Login");

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Liked",
                keyColumns: new[] { "PostId", "UserId" },
                keyValues: new object[] { 1, 2 },
                column: "LikedTime",
                value: new DateTime(2023, 1, 16, 13, 27, 32, 303, DateTimeKind.Local).AddTicks(9658));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 16, 13, 27, 32, 303, DateTimeKind.Local).AddTicks(9646));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 16, 13, 27, 32, 303, DateTimeKind.Local).AddTicks(9628));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 16, 13, 27, 32, 303, DateTimeKind.Local).AddTicks(9632));
        }
    }
}
