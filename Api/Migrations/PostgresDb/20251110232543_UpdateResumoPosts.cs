using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations.PostgresDb
{
    /// <inheritdoc />
    public partial class UpdateResumoPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resumo",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resumo",
                table: "Posts");
        }
    }
}
