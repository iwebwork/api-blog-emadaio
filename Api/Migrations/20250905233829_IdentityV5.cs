using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class IdentityV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "UsuarioToken",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioToken_UsuarioId",
                table: "UsuarioToken",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioToken_AspNetUsers_UsuarioId",
                table: "UsuarioToken",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioToken_AspNetUsers_UsuarioId",
                table: "UsuarioToken");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioToken_UsuarioId",
                table: "UsuarioToken");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "UsuarioToken");
        }
    }
}
