using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedFSOptionToDbContextAndAddedTextToFSOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilledSurveyOption_FilledSurveys_FilledSurveyId",
                table: "FilledSurveyOption");

            migrationBuilder.DropForeignKey(
                name: "FK_FilledSurveyOption_Options_OptionId",
                table: "FilledSurveyOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilledSurveyOption",
                table: "FilledSurveyOption");

            migrationBuilder.RenameTable(
                name: "FilledSurveyOption",
                newName: "FilledSurveyOptions");

            migrationBuilder.RenameIndex(
                name: "IX_FilledSurveyOption_OptionId",
                table: "FilledSurveyOptions",
                newName: "IX_FilledSurveyOptions_OptionId");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "FilledSurveyOptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilledSurveyOptions",
                table: "FilledSurveyOptions",
                columns: new[] { "FilledSurveyId", "OptionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FilledSurveyOptions_FilledSurveys_FilledSurveyId",
                table: "FilledSurveyOptions",
                column: "FilledSurveyId",
                principalTable: "FilledSurveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilledSurveyOptions_Options_OptionId",
                table: "FilledSurveyOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilledSurveyOptions_FilledSurveys_FilledSurveyId",
                table: "FilledSurveyOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_FilledSurveyOptions_Options_OptionId",
                table: "FilledSurveyOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilledSurveyOptions",
                table: "FilledSurveyOptions");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "FilledSurveyOptions");

            migrationBuilder.RenameTable(
                name: "FilledSurveyOptions",
                newName: "FilledSurveyOption");

            migrationBuilder.RenameIndex(
                name: "IX_FilledSurveyOptions_OptionId",
                table: "FilledSurveyOption",
                newName: "IX_FilledSurveyOption_OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilledSurveyOption",
                table: "FilledSurveyOption",
                columns: new[] { "FilledSurveyId", "OptionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FilledSurveyOption_FilledSurveys_FilledSurveyId",
                table: "FilledSurveyOption",
                column: "FilledSurveyId",
                principalTable: "FilledSurveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilledSurveyOption_Options_OptionId",
                table: "FilledSurveyOption",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
