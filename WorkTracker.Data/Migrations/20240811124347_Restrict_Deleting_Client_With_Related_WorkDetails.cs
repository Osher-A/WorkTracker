using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTracker.Migrations
{
    /// <inheritdoc />
    public partial class Restrict_Deleting_Client_With_Related_WorkDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorksDetails_Clients_ClientId",
                table: "WorksDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_WorksDetails_Clients_ClientId",
                table: "WorksDetails",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorksDetails_Clients_ClientId",
                table: "WorksDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_WorksDetails_Clients_ClientId",
                table: "WorksDetails",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
