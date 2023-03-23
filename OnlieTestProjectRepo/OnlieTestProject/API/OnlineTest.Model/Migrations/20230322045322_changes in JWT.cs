using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTest.Model.Migrations
{
    /// <inheritdoc />
    public partial class changesinJWT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RToken_Users_User_Id",
                table: "RToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RToken",
                table: "RToken");

            migrationBuilder.RenameTable(
                name: "RToken",
                newName: "RTokens");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "RTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Refresh_Token",
                table: "RTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "Is_Stop",
                table: "RTokens",
                newName: "IsStop");

            migrationBuilder.RenameColumn(
                name: "Created_Date",
                table: "RTokens",
                newName: "CreatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_RToken_User_Id",
                table: "RTokens",
                newName: "IX_RTokens_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "RTokens",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsStop",
                table: "RTokens",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "RTokens",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RTokens",
                table: "RTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RTokens_Users_UserId",
                table: "RTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RTokens_Users_UserId",
                table: "RTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RTokens",
                table: "RTokens");

            migrationBuilder.RenameTable(
                name: "RTokens",
                newName: "RToken");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RToken",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "RToken",
                newName: "Refresh_Token");

            migrationBuilder.RenameColumn(
                name: "IsStop",
                table: "RToken",
                newName: "Is_Stop");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "RToken",
                newName: "Created_Date");

            migrationBuilder.RenameIndex(
                name: "IX_RTokens_UserId",
                table: "RToken",
                newName: "IX_RToken_User_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Refresh_Token",
                table: "RToken",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Stop",
                table: "RToken",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Date",
                table: "RToken",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RToken",
                table: "RToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RToken_Users_User_Id",
                table: "RToken",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
