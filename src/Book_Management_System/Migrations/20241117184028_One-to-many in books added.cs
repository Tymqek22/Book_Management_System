using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Onetomanyinbooksadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BorrowRecords_BookId",
                table: "BorrowRecords");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_BookId",
                table: "BorrowRecords",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BorrowRecords_BookId",
                table: "BorrowRecords");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_BookId",
                table: "BorrowRecords",
                column: "BookId",
                unique: true,
                filter: "[BookId] IS NOT NULL");
        }
    }
}
