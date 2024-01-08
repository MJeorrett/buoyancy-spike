using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuoyancyApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlannedTimeRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlannedTime_PersonId",
                table: "PlannedTime",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedTime_ProjectId",
                table: "PlannedTime",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedTime_Person_PersonId",
                table: "PlannedTime",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedTime_Project_ProjectId",
                table: "PlannedTime",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedTime_Person_PersonId",
                table: "PlannedTime");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannedTime_Project_ProjectId",
                table: "PlannedTime");

            migrationBuilder.DropIndex(
                name: "IX_PlannedTime_PersonId",
                table: "PlannedTime");

            migrationBuilder.DropIndex(
                name: "IX_PlannedTime_ProjectId",
                table: "PlannedTime");
        }
    }
}
