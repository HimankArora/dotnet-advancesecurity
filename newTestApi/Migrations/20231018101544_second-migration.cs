using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newTestApi.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MarksId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "RegNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentRegNo",
                table: "Marks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MarksId",
                table: "Students",
                column: "MarksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Marks_MarksId",
                table: "Students",
                column: "MarksId",
                principalTable: "Marks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Marks_MarksId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MarksId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MarksId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RegNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentRegNo",
                table: "Marks");
        }
    }
}
