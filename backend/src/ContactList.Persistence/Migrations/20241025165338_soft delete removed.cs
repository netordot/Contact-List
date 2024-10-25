using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class softdeleteremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "contacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "contacts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
