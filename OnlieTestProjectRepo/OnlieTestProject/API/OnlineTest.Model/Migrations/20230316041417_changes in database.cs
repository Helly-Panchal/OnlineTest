using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTest.Model.Migrations
{
    /// <inheritdoc />
    public partial class changesindatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Users_CreatedBy",
                table: "Technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Users_ModifiedBy",
                table: "Technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Users_UserNavigationId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_UserNavigationId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_CreatedBy",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_ModifiedBy",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "UserNavigationId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Tests",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Tests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Technologies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "Questions",
                type: "DateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Tests",
                newName: "CreatedTime");

            migrationBuilder.AddColumn<int>(
                name: "UserNavigationId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_UserNavigationId",
                table: "Tests",
                column: "UserNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_CreatedBy",
                table: "Technologies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_ModifiedBy",
                table: "Technologies",
                column: "ModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Users_CreatedBy",
                table: "Technologies",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Users_ModifiedBy",
                table: "Technologies",
                column: "ModifiedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Users_UserNavigationId",
                table: "Tests",
                column: "UserNavigationId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
