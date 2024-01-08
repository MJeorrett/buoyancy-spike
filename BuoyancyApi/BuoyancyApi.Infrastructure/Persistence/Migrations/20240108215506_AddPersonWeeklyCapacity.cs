using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuoyancyApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonWeeklyCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "WeeklyCapacity",
                table: "Person",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeeklyCapacity",
                table: "Person");
        }
    }
}
