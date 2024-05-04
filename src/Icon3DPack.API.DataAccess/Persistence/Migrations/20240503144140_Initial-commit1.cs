using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Icon3DPack.API.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initialcommit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DownloadCount",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadCount",
                table: "Products");
        }
    }
}
