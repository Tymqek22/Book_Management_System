using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Fixinonetoone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BorrowRecords_BorrowRecordId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowRecordId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BorrowRecordId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_BookId",
                table: "BorrowRecords",
                column: "BookId",
                unique: true,
                filter: "[BookId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRecords_Books_BookId",
                table: "BorrowRecords",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRecords_Books_BookId",
                table: "BorrowRecords");

            migrationBuilder.DropIndex(
                name: "IX_BorrowRecords_BookId",
                table: "BorrowRecords");

            migrationBuilder.AddColumn<int>(
                name: "BorrowRecordId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowRecordId",
                table: "Books",
                column: "BorrowRecordId",
                unique: true,
                filter: "[BorrowRecordId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BorrowRecords_BorrowRecordId",
                table: "Books",
                column: "BorrowRecordId",
                principalTable: "BorrowRecords",
                principalColumn: "Id");
        }
    }
}
