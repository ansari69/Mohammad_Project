using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.Infrastructure.Persistence.Migrations
{
    public partial class EdittableProductLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "ProductLikes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "ProductLikes");
        }
    }
}
