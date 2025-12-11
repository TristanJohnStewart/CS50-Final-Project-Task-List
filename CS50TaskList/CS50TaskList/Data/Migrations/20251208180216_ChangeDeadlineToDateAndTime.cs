using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS50TaskList.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeadlineToDateAndTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Tasks");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Tasks",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Tasks",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Deadline",
                table: "Tasks",
                type: "datetimeoffset",
                nullable: true);
        }
    }
}
