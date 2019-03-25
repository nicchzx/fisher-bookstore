using Microsoft.EntityFrameworkCore.Migrations;

namespace Fisher.Bookstore.Api.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publicationDate",
                table: "Books",
                newName: "PublishDate");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Books",
                newName: "publicationDate");
        }
    }
}
