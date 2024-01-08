using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuoyancyApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleRequiredTimeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RequiredTime_RoleId",
                table: "RequiredTime",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredTime_Role_RoleId",
                table: "RequiredTime",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequiredTime_Role_RoleId",
                table: "RequiredTime");

            migrationBuilder.DropIndex(
                name: "IX_RequiredTime_RoleId",
                table: "RequiredTime");
        }
    }
}
