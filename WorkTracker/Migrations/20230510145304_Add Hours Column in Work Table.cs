using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddHoursColumninWorkTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Hours",
                table: "Works",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Works");
        }
    }
}
