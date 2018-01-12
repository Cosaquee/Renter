using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Database.Migrations
{
    public partial class RemoveBookIDFromBookRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksRatings_Books_BookId",
                table: "BooksRatings");

            migrationBuilder.RenameColumn(
                name: "BookTitle",
                table: "BooksRatings",
                newName: "ISBN");

            migrationBuilder.AlterColumn<long>(
                name: "BookId",
                table: "BooksRatings",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_BooksRatings_Books_BookId",
                table: "BooksRatings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksRatings_Books_BookId",
                table: "BooksRatings");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "BooksRatings",
                newName: "BookTitle");

            migrationBuilder.AlterColumn<long>(
                name: "BookId",
                table: "BooksRatings",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BooksRatings_Books_BookId",
                table: "BooksRatings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
