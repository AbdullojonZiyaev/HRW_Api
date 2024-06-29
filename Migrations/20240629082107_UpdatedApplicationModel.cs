using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedApplicationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationTypes_AppicationTypeId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_AppicationTypeId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationTypeId",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationTypeId",
                table: "Applications",
                column: "ApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PositionId",
                table: "Applications",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationTypes_ApplicationTypeId",
                table: "Applications",
                column: "ApplicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Positions_PositionId",
                table: "Applications",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationTypes_ApplicationTypeId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Positions_PositionId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationTypeId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PositionId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeId",
                table: "Applications");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppicationTypeId",
                table: "Applications",
                column: "AppicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationTypes_AppicationTypeId",
                table: "Applications",
                column: "AppicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
