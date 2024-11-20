﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Activeandinactiveborrowrecordsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BorrowRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BorrowRecords");
        }
    }
}
