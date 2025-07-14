using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_add_destinationTable_addColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogCoverImage",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogDescriptionA",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogDescriptionB",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogMiniImage",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogCoverImage",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "BlogDescriptionA",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "BlogDescriptionB",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "BlogMiniImage",
                table: "Destinations");
        }
    }
}
