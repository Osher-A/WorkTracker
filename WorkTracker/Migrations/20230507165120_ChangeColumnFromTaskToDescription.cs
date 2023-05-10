using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTracker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnFromTaskToDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Task",
                table: "Works",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Works",
                newName: "Task");
        }
    }
}
