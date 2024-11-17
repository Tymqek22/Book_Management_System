using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Quantityofthebooksadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBorrowed",
                table: "Books",
                newName: "IsAvailable");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Books",
                newName: "IsBorrowed");
        }
    }
}
