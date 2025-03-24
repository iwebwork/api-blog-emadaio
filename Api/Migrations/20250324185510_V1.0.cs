using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class V10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Tipo = table.Column<short>(type: "INTEGER", nullable: false),
                    Corpo = table.Column<string>(type: "TEXT", nullable: false),
                    Liberado = table.Column<short>(type: "INTEGER", nullable: false),
                    AspNetUserInsertId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AspNetUserUpdateId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Inserted = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Updated = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Deleted = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
