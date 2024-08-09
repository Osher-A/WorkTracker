using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTracker.Migrations
{
    /// <inheritdoc />
    public partial class Add_Reference_To_Client_In_WorkDetails_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "WorksDetails");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "WorksDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Ensure all ClientId values have corresponding entries in Clients table
            migrationBuilder.Sql(@"
                UPDATE WorksDetails
                SET ClientId = (SELECT TOP 1 Id FROM Clients)
                WHERE ClientId NOT IN (SELECT Id FROM Clients)
            ");

            migrationBuilder.CreateIndex(
                name: "IX_WorksDetails_ClientId",
                table: "WorksDetails",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorksDetails_Clients_ClientId",
                table: "WorksDetails",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorksDetails_Clients_ClientId",
                table: "WorksDetails");

            migrationBuilder.DropIndex(
                name: "IX_WorksDetails_ClientId",
                table: "WorksDetails");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "WorksDetails");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "WorksDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
