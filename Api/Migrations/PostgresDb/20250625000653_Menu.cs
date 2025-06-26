using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations.PostgresDb
{
    /// <inheritdoc />
    public partial class Menu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesPost");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "TipoPostId",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TiposPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    AspNetUserInsertId = table.Column<Guid>(type: "uuid", nullable: false),
                    AspNetUserUpdateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    TipoPostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Liberado = table.Column<short>(type: "smallint", nullable: false),
                    Index = table.Column<short>(type: "smallint", nullable: false),
                    AspNetUserInsertId = table.Column<Guid>(type: "uuid", nullable: false),
                    AspNetUserUpdateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_TiposPost_TipoPostId",
                        column: x => x.TipoPostId,
                        principalTable: "TiposPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TipoPostId",
                table: "Posts",
                column: "TipoPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_TipoPostId",
                table: "Menus",
                column: "TipoPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_TiposPost_TipoPostId",
                table: "Posts",
                column: "TipoPostId",
                principalTable: "TiposPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_TiposPost_TipoPostId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "TiposPost");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TipoPostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TipoPostId",
                table: "Posts");

            migrationBuilder.AddColumn<short>(
                name: "Tipo",
                table: "Posts",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "ImagesPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    AspNetUserInsertId = table.Column<Guid>(type: "uuid", nullable: false),
                    AspNetUserUpdateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Inserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
    }
}
