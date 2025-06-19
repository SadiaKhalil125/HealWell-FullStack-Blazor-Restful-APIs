using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
using Domain.Models;
namespace Infrastructure.Migrations
{
    public partial class AddStatusToAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending"); // Default status if desired
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");
        }
    }
}
