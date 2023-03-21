using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTest.Model.Migrations
{
    /// <inheritdoc />
    public partial class QuestionAnsweerMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Questions",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswer",
                table: "QuestionAnswerMapping",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnswer",
                table: "QuestionAnswerMapping");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Questions",
                newName: "CreateOn");
        }
    }
}
