using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlipCommerce.Migrations
{
    /// <inheritdoc />
    public partial class intital5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "discount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount",
                table: "Products");
        }
    }
}
