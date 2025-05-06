using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations.PostgresDb
{
    /// <inheritdoc />
    public partial class PostgresV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<short>(type: "smallint", nullable: false),
                    Corpo = table.Column<string>(type: "text", nullable: false),
                    Liberado = table.Column<short>(type: "smallint", nullable: false),
                    AspNetUserInsertId = table.Column<Guid>(type: "uuid", nullable: false),
                    AspNetUserUpdateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagesPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    AspNetUserInsertId = table.Column<Guid>(type: "uuid", nullable: false),
                    AspNetUserUpdateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesPost_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagesPost_PostId",
                table: "ImagesPost",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesPost");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
