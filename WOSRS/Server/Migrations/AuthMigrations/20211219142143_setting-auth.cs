using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WOSRS.Server.Migrations.AuthMigrations
{
    public partial class settingauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_settings_settings_id",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "settings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_settings_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "settings_id",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "settings_id",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    settings_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    order_type = table.Column<int>(type: "integer", nullable: true),
                    point_system = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.settings_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_settings_id",
                table: "AspNetUsers",
                column: "settings_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_settings_settings_id",
                table: "AspNetUsers",
                column: "settings_id",
                principalTable: "settings",
                principalColumn: "settings_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
