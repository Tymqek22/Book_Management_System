using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class NullFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BorrowRecords_BorrowRecordId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRecords_Members_MemberId",
                table: "BorrowRecords");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowRecordId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "BorrowRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowRecordId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRecords_Members_MemberId",
                table: "BorrowRecords",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BorrowRecords_BorrowRecordId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRecords_Members_MemberId",
                table: "BorrowRecords");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowRecordId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "BorrowRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BorrowRecordId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowRecordId",
                table: "Books",
                column: "BorrowRecordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BorrowRecords_BorrowRecordId",
                table: "Books",
                column: "BorrowRecordId",
                principalTable: "BorrowRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRecords_Members_MemberId",
                table: "BorrowRecords",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
