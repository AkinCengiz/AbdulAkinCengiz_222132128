using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbdulAkinCengiz_222132128.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReservationEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInAt",
                table: "Reservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckedIn",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsCheckedIn",
                table: "Reservations");
        }
    }
}
