using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuoyancyApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredTimeProjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RequiredTime_ProjectId",
                table: "RequiredTime",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredTime_Project_ProjectId",
                table: "RequiredTime",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequiredTime_Project_ProjectId",
                table: "RequiredTime");

            migrationBuilder.DropIndex(
                name: "IX_RequiredTime_ProjectId",
                table: "RequiredTime");
        }
    }
}
