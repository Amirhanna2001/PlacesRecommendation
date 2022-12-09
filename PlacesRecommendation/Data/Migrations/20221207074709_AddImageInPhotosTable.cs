using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlacesRecommendation.Data.Migrations
{
    public partial class AddImageInPhotosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTypes_AspNetUsers_UserId1",
                table: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserTypes_UserId1",
                table: "UserTypes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserTypes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Photos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favourites",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_UserId",
                table: "UserTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTypes_AspNetUsers_UserId",
                table: "UserTypes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTypes_AspNetUsers_UserId",
                table: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserTypes_UserId",
                table: "UserTypes");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Favourites",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_UserId1",
                table: "UserTypes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTypes_AspNetUsers_UserId1",
                table: "UserTypes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
